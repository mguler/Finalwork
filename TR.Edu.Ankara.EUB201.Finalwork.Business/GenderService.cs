using System.Data;
using TR.Edu.Ankara.EUB201.Finalwork.Common.ExceptionManagement;
using TR.Edu.Ankara.EUB201.Finalwork.DataAccess;

namespace TR.Edu.Ankara.EUB201.Finalwork.Business
{
    public class GenderService
    {
        private readonly AdoNetDataRepository _dataRepository;
        public GenderService(AdoNetDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public int Save(int? id,string name,bool isActive) 
        {
            var result = 0; 

            var sql = "";
            if (!id.HasValue || id.Value == 0)
            {
                sql = $@"INSERT INTO [GenderDefinition]([Name],[IsActive])
                        SELECT  '{name}','{isActive}'";
            }
            else 
            {
                sql = $@"UPDATE [GenderDefinition]
                SET [Name] = '{name}'
                ,[IsActive] = '{isActive}'
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
            var sql = $@"SELECT * FROM GenderDefinition A
                        WHERE A.[Id] = {id}";

            var result = _dataRepository.Execute<DataTable>(sql);

            return result;
        }
        public bool Delete(int id)
        {
            var sql = $"SELECT COUNT(*) FROM LibraryMembership A WHERE A.[GenderId] = {id}";
            var result = _dataRepository.Execute<int>(sql);
            if (result > 0)
            {
                throw new CustomApplicationException("Silmeye çalıştığınız yazar kaydı ile ilişkili kitap kayıtları olduğu için bu kaydı silemezsiniz");
            }
            sql = $"DELETE FROM GenderDefinition A WHERE A.[Id] = {id}";
            _dataRepository.Execute(sql);
            return true;
        }
        public DataTable List(int page, int resultPerPage, string name, bool isActive)
        {
            var sql = $@"SELECT * FROM GenderDefinition A
                        WHERE A.[Name] LIKE '%{name}%' AND [IsActive]='{isActive}'
                        ORDER BY A.Id DESC OFFSET {page * resultPerPage} ROWS FETCH NEXT {resultPerPage} ROWS ONLY";

            var result = _dataRepository.Execute<DataTable>(sql);

            return result;
        }
        public int Count(string name, bool isActive)
        {
            var sql = $@"SELECT COUNT(*) FROM GenderDefinition A 
                        WHERE A.[Name] LIKE '%{name}%' AND [IsActive]='{isActive}'";

            var result = _dataRepository.Execute<int>(sql);

            return result;
        }
    }
}
