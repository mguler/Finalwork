using System.Data;
using TR.Edu.Ankara.EUB201.Finalwork.Common.ExceptionManagement;
using TR.Edu.Ankara.EUB201.Finalwork.DataAccess;

namespace TR.Edu.Ankara.EUB201.Finalwork.Business
{
    public class BookService
    {
        private readonly AdoNetDataRepository _dataRepository;
        public BookService(AdoNetDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public int Save(int? id,string title,int categoryId,string description,int authorId,string isbn,int? pageCount,int publishmentYear,bool isCovered) 
        {
            var result = 0; 

            var sql = "";
            if (!id.HasValue || id.Value == 0)
            {
                sql = $@"INSERT INTO[dbo].[BookDefinition]([Title],[CategoryId],[Description],[AuthorId],[ISBN],[PageCount],[PublishmentYear],[IsCovered])
                        SELECT  '{title}',{categoryId},'{description}',{authorId},'{isbn}',{pageCount},{publishmentYear},{(isCovered ? 1:0)}";
            }
            else 
            {
                sql = $@"UPDATE[dbo].[BookDefinition]
                SET [Title] = '{title}'
                    ,[CategoryId] = {categoryId}
                    ,[Description] = '{description}'
                    ,[AuthorId] = {authorId}
                    ,[ISBN] = '{isbn}'
                    ,[PageCount] = {pageCount}
                    ,[PublishmentYear] = {publishmentYear}
                    ,[IsCovered] = {(isCovered ? 1 : 0)}
                    WHERE Id = {id}";
            }

            _dataRepository.Execute(sql);

            if (!id.HasValue || id.Value == 0)
            {
                result = _dataRepository.Execute<int>("SELECT SCOPE_IDENTITY()");
            }
            else 
            {
                result = _dataRepository.Execute<int>("SELECT @@ROWCOUNT");
            }
            return result;
        }
        public DataTable GetDetail(int id)
        {
            var sql = $"SELECT * FROM BookDefinition A WHERE A.[Id] = {id}";
            var result = _dataRepository.Execute<DataTable>(sql);
            return result;
        }
        public bool Delete(int id)
        {
            var sql = $"SELECT COUNT(*) FROM BooksOnLoan A WHERE A.[BookId] = {id}";
            var result = _dataRepository.Execute<int>(sql);
            if (result > 0)
            {
                throw new CustomApplicationException("Silmeye çalıştığınız kayıt ile ilişkili ödünç verme kayıtları olduğu için bu kaydı silemezsiniz");
            }
            sql = $"DELETE FROM BookDefinition WHERE [Id] = {id}";
            _dataRepository.Execute(sql);
            var count = _dataRepository.Execute<int>("SELECT @@ROWCOUNT");
            if (count == 0)
            {
                return false;
            }
            return true;
        }
        public DataTable List(int page, int resultPerPage, string title, string author, string isbn, int categoryId, int publishmentYear)
        {
            var sql = $@"SELECT A.Id,A.Title,A.ISBN,B.Name Author,C.Name Category FROM BookDefinition A
                        JOIN Author B ON B.Id = A.AuthorId 
                        JOIN Category C ON C.Id = A.CategoryId
                        WHERE A.[Title] LIKE '%{title}%'
                        AND A.[CategoryId] = {(categoryId > 0 ? categoryId.ToString() : "A.[CategoryId]")}
                        AND A.[ISBN] LIKE '%{isbn}%'
                        AND A.[PublishmentYear] = {(publishmentYear>0 ? publishmentYear.ToString() : "A.[PublishmentYear]")}
                        AND B.[Name] LIKE '%{author}%'
                        ORDER BY A.Id DESC OFFSET {page * resultPerPage} ROWS FETCH NEXT {resultPerPage} ROWS ONLY";

            var result = _dataRepository.Execute<DataTable>(sql);

            return result;
        }
        public int Count(string title, string author, string isbn, int categoryId, int publishmentYear)
        {
            var sql = $@"SELECT COUNT(*) FROM BookDefinition A
                        JOIN Author B ON B.Id = A.AuthorId 
                        JOIN Category C ON C.Id = A.CategoryId
                        WHERE A.[Title] LIKE '%{title}%'
                        AND A.[CategoryId] = {(categoryId > 0 ? categoryId.ToString() : "A.[CategoryId]")}
                        AND A.[ISBN] LIKE '%{isbn}%'
                        AND A.[PublishmentYear] = {(publishmentYear > 0 ? publishmentYear.ToString() : "A.[PublishmentYear]")}
                        AND B.[Name] LIKE '%{author}%'";

            var result = _dataRepository.Execute<int>(sql);

            return result;
        }
    }
}
