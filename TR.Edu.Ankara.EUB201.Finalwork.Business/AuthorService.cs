using System.Data;
using TR.Edu.Ankara.EUB201.Finalwork.Common.ExceptionManagement;
using TR.Edu.Ankara.EUB201.Finalwork.DataAccess;

namespace TR.Edu.Ankara.EUB201.Finalwork.Business
{
    public class AuthorService 
    {
        private readonly AdoNetDataRepository _dataRepository;
        public AuthorService(AdoNetDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public int Save(int? id,string name) 
        {
            var result = 0; 

            var sql = "";
            if (!id.HasValue || id.Value == 0)
            {
                sql = $@"INSERT INTO [Author]([Name])
                        SELECT  '{name}'";
            }
            else 
            {
                sql = $@"UPDATE [Author]
                SET [Name] = '{name}'
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
            var sql = $@"SELECT * FROM Author A
                        WHERE A.[Id] = {id}";

            var result = _dataRepository.Execute<DataTable>(sql);

            return result;
        }
        public bool Delete(int id)
        {
            var sql = $"SELECT COUNT(*) FROM BookDefinition A WHERE A.[AuthorId] = {id}";
            var result = _dataRepository.Execute<int>(sql);
            if (result > 0)
            {
                throw new CustomApplicationException("Silmeye çalıştığınız yazar kaydı ile ilişkili kitap kayıtları olduğu için bu kaydı silemezsiniz");
            }
            sql = $"DELETE FROM Author A WHERE A.[Id] = {id}";
            _dataRepository.Execute(sql);
            return true;
        }
        public DataTable List(int page, int resultPerPage, string name)
        {
            var sql = $@"SELECT * FROM Author A
                        WHERE A.[Name] LIKE '%{name}%'
                        ORDER BY A.Id DESC OFFSET {page * resultPerPage} ROWS FETCH NEXT {resultPerPage} ROWS ONLY";

            var result = _dataRepository.Execute<DataTable>(sql);
            return result;
        }
        public int Count(string name)
        {
            var sql = $@"SELECT COUNT(*) FROM Author A
                        WHERE A.[Name] LIKE '%{name}%'";

            var result = _dataRepository.Execute<int>(sql);
            return result;
        }
    }
}
