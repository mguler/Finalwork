using System.Data;
using TR.Edu.Ankara.EUB201.Finalwork.DataAccess;

namespace TR.Edu.Ankara.EUB201.Finalwork.Business
{
    public class ReferenceDataService
    {
        private readonly AdoNetDataRepository _dataRepository;
        public ReferenceDataService(AdoNetDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public DataTable ListAllAuthors()
        {
            var sql = $@"SELECT * FROM (SELECT NULL [Id],'<Seçiniz>' [Name] UNION SELECT [Id],[Name] FROM Author) A ORDER BY A.Name";
            var result = _dataRepository.Execute<DataTable>(sql);
            return result;
        }

        public DataTable ListAllCategories()
        {
            var sql = $@"SELECT * FROM (SELECT NULL [Id],'<Seçiniz>' [Name] UNION SELECT [Id],[Name] FROM Category) A ORDER BY A.Name";
            var result = _dataRepository.Execute<DataTable>(sql);
            return result;
        }
        public DataTable ListAllGenderDefinitions()
        {
            var sql = $@"SELECT * FROM (SELECT NULL [Id],'<Seçiniz>' [Name] UNION SELECT [Id],[Name] FROM GenderDefinition WHERE IsACtive = 1 ) A ORDER BY A.Id";
            var result = _dataRepository.Execute<DataTable>(sql);
            return result;
        }
        public DataTable ListAllBooks()
        {
            var sql = $@"SELECT * FROM (SELECT NULL [Id],'<Seçiniz>' [Name] UNION SELECT [Id],[Title] [Name] FROM BookDefinition) A ORDER BY A.Id";
            var result = _dataRepository.Execute<DataTable>(sql);
            return result;
        }
        public DataTable ListAllUsers()
        {
            var sql = $@"SELECT * FROM (SELECT NULL [Id],'<Seçiniz>' [Name] UNION SELECT [Id],CONCAT([Firstname],' ',[Lastname]) [Name] FROM [User]) A ORDER BY A.Id";
            var result = _dataRepository.Execute<DataTable>(sql);
            return result;
        }
        public DataTable ListAllMembers()
        {
            var sql = $@"SELECT * FROM (SELECT NULL [Id],'<Seçiniz>' [Name] UNION SELECT [Id],CONCAT([Firstname],' ',[Lastname]) [Name] FROM [LibraryMembership]) A ORDER BY A.Id";
            var result = _dataRepository.Execute<DataTable>(sql);
            return result;
        }

    }
}
