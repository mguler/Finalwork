using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using TR.Edu.Ankara.EUB201.Finalwork.DataAccess;
using TR.Edu.Ankara.EUB201.Finalwork.DataCollector.Model;

namespace TR.Edu.Ankara.EUB201.Finalwork.DataCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{DateTime.UtcNow.ToString("dd.MM.yyyy hh:mm")}] Started at {DateTime.UtcNow} GMT");

            Console.WriteLine($"[{DateTime.UtcNow.ToString("dd.MM.yyyy hh:mm")}] Started at {DateTime.UtcNow} GMT");
            var baseUrl = "https://www.bkmkitap.com/rest1/";

            var categoriesUrl = $"{baseUrl}category/tree/24"; //Edebiyat 
            var loginUrl = $"{baseUrl}auth/login/mobileandroidapp";
            
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;

            webClient.Headers.Add("User-Agent", "tsoftmobile-android");
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            Console.Write($"[{DateTime.UtcNow.ToString("dd.MM.yyyy hh:mm")}] Fetching categories...");
            var loginResult = webClient.UploadString(loginUrl, "POST", "pass=qGBQ4sHr");
            var token = loginResult.Match("(?<=token\":\")(.*?)(?=\")");

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Could not log in");
            }

            webClient.Headers.Add("User-Agent", "tsoftmobile-android");
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var result = webClient.UploadString(categoriesUrl, "POST", $"MobileToken=&language=tr&country_code=TR&currency=TL&token={token}&depth=1");

            var categories = JsonConvert.DeserializeObject<Response<Category>>(result);
            var books = new List<Book>();
            var resultPerPage = 25;

            Console.WriteLine("Success");

            foreach (var category in categories.Data)
            {
                Console.Write($"[{DateTime.UtcNow.ToString("dd.MM.yyyy hh:mm")}] Fetching data for the category {category.Name}...");
                webClient.Headers.Add("User-Agent", "tsoftmobile-android");
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                var booksUrl = $"{baseUrl}product/find";
                var payload = $"pg=1&category={category.Id}&country_code=IT&token={token}&fetch_product_detail=true&MobileToken=&language=tr&currency=TL&sort=0&perpage={resultPerPage}";
                var booksResult = webClient.UploadString(booksUrl, "POST", payload);
                var deserializedBooksResult = JsonConvert.DeserializeObject<Response<Book>>(booksResult);
                books.AddRange(deserializedBooksResult.Data);
                Console.WriteLine("Success");
            }

            Console.Write($"[{DateTime.UtcNow.ToString("dd.MM.yyyy hh:mm")}] Data is getting ready...");
            var authors = books.Select(book => book.Model).GroupBy(model => model)
                .Select(modelGroup =>new Author {  Name = modelGroup.Key}).ToList();

            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

            var dbConnection = new SqlConnection(connectionString);
            var dataRepository = new AdoNetDataRepository(dbConnection);

            categories.Data.ToList().ForEach(category => {

                if (category.Id == 24) { return; }
                var sql = $"INSERT INTO [dbo].[Category] ([Name]) SELECT '{category.Name}'";
                dataRepository.Execute<int>(sql);
                category.LocalId = dataRepository.Execute<int>("SELECT SCOPE_IDENTITY()");
                books.Where(book => book.Category_Id == category.Id).ToList().ForEach(book => book.Category_Id = category.LocalId);
            });

            authors.ForEach(author => {
                var sql = $"INSERT INTO Author(Name) SELECT '{author.Name}'";
                dataRepository.Execute(sql);
                author.Id = dataRepository.Execute<int>("SELECT SCOPE_IDENTITY()");
                books.Where(book => book.Model == author.Name).ToList().ForEach(book => book.AuthorId = author.Id );
            });

            Console.WriteLine("Success");
            Console.Write($"[{DateTime.UtcNow.ToString("dd.MM.yyyy hh:mm")}] Database is updating...");


            books.GroupBy(book => book.Title).Select(groupedBook => groupedBook.First()).ToList().ForEach(book =>
            {

                if (book.Category_Id == 24)
                {
                    return;
                }

                book.Detail = book.Detail.RegexReplace("<.*?>", String.Empty).Replace("'", "''");
                book.Title = book.Title.Replace("'", "''");
                var sql = $@"INSERT INTO[dbo].[BookDefinition]([Title],[CategoryId],[Description],[AuthorId],[ISBN],[PageCount],[PublishmentYear],[IsCovered])
                            SELECT '{book.Title}',{book.Category_Id},'{book.Detail}',{book.AuthorId},'{book.Barcode}',{(string.IsNullOrEmpty(book.Additional_Field_2) ? "0" : book.Additional_Field_2)},0,{Convert.ToInt32(book.Additional_Field_3 == "Ciltli")}";
                dataRepository.Execute(sql);
            });

            Console.WriteLine("Success");
            Console.WriteLine($"[{DateTime.UtcNow.ToString("dd.MM.yyyy hh:mm")}] Operation has completed successfuly at {DateTime.UtcNow} GMT");

        }
    }
}
