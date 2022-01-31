using System.Data;
using TR.Edu.Ankara.EUB201.Finalwork.Common.ExceptionManagement;
using TR.Edu.Ankara.EUB201.Finalwork.DataAccess;

namespace TR.Edu.Ankara.EUB201.Finalwork.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationService
    {
        private readonly AdoNetDataRepository _dataRepository;
        public AuthenticationService(AdoNetDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public int Save(int? id, string email, string password, string firstname, string lastname, bool isActive)
        {
            var result = 0;
            var sql = "";
            if (!id.HasValue || id.Value == 0)
            {
                sql = $@"INSERT INTO [User]([Email],[Password],[Firstname],[Lastname],[IsActive])
                        SELECT  '{email}','{password}','{firstname}','{lastname}',{(isActive ? 1 : 0)}";
            }
            else
            {
                sql = $@"UPDATE [User]
                SET [Email] = '{email}'
                    ,[Password] = '{password}'
                    ,[Firstname] = {firstname}
                    ,[Lastname] = '{lastname}'
                    ,[IsActive] = {(isActive ? 1 : 0)}
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
            var sql = $@"SELECT * FROM [User] A
                        WHERE A.[Id] = {id}";
            var result = _dataRepository.Execute<DataTable>(sql);
            return result;
        }
        public bool Delete(int id)
        {
            var sql = $"SELECT COUNT(*) FROM BooksOnLoad A WHERE A.[UserId] = {id}";
            var result = _dataRepository.Execute<int>(sql);
            if (result > 0)
            {
                throw new CustomApplicationException("Silmeye çalıştığınız kullanıcının geçmişe dönük işlem kayıtları olduğu için bu üye kaydını silemezsiniz");
            }
            sql = $"DELETE FROM [User] A WHERE A.[Id] = {id}";
            _dataRepository.Execute(sql);

            var count = _dataRepository.Execute<int>("SELECT @@ROWCOUNT");
            if (count == 0)
            {
                return false;
            }

            return true;
        }
        public DataTable List(int page, int resultPerPage, string email, string firstname, string lastname, bool? isActive)
        {
            var sql = $@"SELECT * FROM [User] A
                        WHERE A.[Email] LIKE '%{email}%' 
                        AND A.[Firstname] LIKE '%{firstname}%'
                        AND A.[Lastname] LIKE '%{lastname}%'
                        AND A.[IsActive] = '{isActive}'
                        ORDER BY A.Id DESC OFFSET {page * resultPerPage} ROWS FETCH NEXT {resultPerPage} ROWS ONLY";
            var result = _dataRepository.Execute<DataTable>(sql);
            return result;
        }
        public int Count(string email, string firstname, string lastname, bool? isActive)
        {
            var sql = $@"SELECT COUNT(*) FROM [User] A
                        WHERE A.[Email] LIKE '%{email}%' 
                        AND A.[Firstname] LIKE '%{firstname}%'
                        AND A.[Lastname] LIKE '%{lastname}%'
                        AND A.[IsActive] = '{isActive}'";
            var result = _dataRepository.Execute<int>(sql);
            return result;
        }
        public int Login(string email, string password)
        {
            var query = $"SELECT Id FROM [User] WHERE Email = '{email}' AND Password = '{password}' AND IsActive = 1";
            var result = _dataRepository.Execute<int>(query);
            return result;
        }
        public bool ChangePassword(int id, string password, string newpassword, string newpasswordagain)
        {
            var query = $"UPDATE [User] SET Password = '{newpassword}' WHERE Id={id} AND Password = '{password}' AND IsActive = 1 AND '{newpassword}' = '{newpasswordagain}'";
            _dataRepository.Execute<int>(query);
            var result = _dataRepository.Execute<int>("SELECT @@ROWCOUNT");
            return result > 0;
        }
    }
}
