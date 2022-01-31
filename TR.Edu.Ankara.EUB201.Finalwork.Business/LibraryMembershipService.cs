using System;
using System.Data;
using TR.Edu.Ankara.EUB201.Finalwork.Common.ExceptionManagement;
using TR.Edu.Ankara.EUB201.Finalwork.DataAccess;

namespace TR.Edu.Ankara.EUB201.Finalwork.Business
{
    public class LibraryMembershipService
    {
        private readonly AdoNetDataRepository _dataRepository;
        public LibraryMembershipService(AdoNetDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public int Save(int? id, string firstname,string lastname,int genderId,DateTime birthDate,DateTime registrationDate,bool isActive)
        {
            var result = 0;

            var sql = "";
            if (!id.HasValue || id.Value == 0)
            {
                sql = $@"INSERT INTO [LibraryMembership]([Firstname],[Lastname],[GenderId],[Birthdate],[RegistrationDate],[IsActive]))
                        SELECT  '{firstname}','{lastname}',{genderId},'{birthDate}','{registrationDate}',{(isActive ? 1 : 0)}";
            }
            else
            {
                sql = $@"UPDATE [LibraryMembership]
                SET [Firstname] = '{firstname}'
                    ,[Lastname] = '{lastname}'
                    ,[GenderId] = {genderId}
                    ,[Birthdate] = '{birthDate}'
                    ,[RegistrationDate] = '{registrationDate}'
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
            var sql = $@"SELECT * FROM [LibraryMembership] A
                        WHERE A.[Id] = {id}";
            var result = _dataRepository.Execute<DataTable>(sql);
            return result;
        }
        public bool Delete(int id)
        {
            var sql = $"SELECT COUNT(*) FROM BooksOnLoad A WHERE A.[MemberId] = {id}";
            var result = _dataRepository.Execute<int>(sql);
            if (result > 0)
            {
                throw new CustomApplicationException("Silmeye çalıştığınız üye ile ilişkili ödünç alma kayıtları olduğu için bu üye kaydını silemezsiniz");
            }
            sql = $"DELETE FROM [LibraryMembership] A WHERE A.[Id] = {id}";
            _dataRepository.Execute(sql);
            return true;
        }
        public DataTable List(int page, int resultPerPage, string firstname, string lastname, int? genderId, DateTime? birthDateBegin, DateTime? birthDateEnd, DateTime? registrationDateBegin, DateTime? registrationDateEnd, bool? isActive)
        {
            var sql = $@"SELECT * FROM [LibraryMembership] A
                        WHERE A.[Firstname] LIKE '%{firstname}%'
                        AND [Lastname] LIKE '%{lastname}%'
                        AND [GenderId] = {(genderId.HasValue ? genderId.Value.ToString():"[GenderId]") }
                        AND [Birthdate] BETWEEN '{birthDateBegin.Value.ToString("yyyy-MM-dd 00:00:00")}' AND '{birthDateEnd.Value.ToString("yyyy-MM-dd  23:59:59")}'
                        AND [RegistrationDate] BETWEEN '{registrationDateBegin.Value.ToString("yyyy-MM-dd 00:00:00")}' AND '{registrationDateEnd.Value.ToString("yyyy-MM-dd 23:59:59")}'
                        AND [IsActive] = '{isActive}'
                        ORDER BY A.Id DESC OFFSET {page * resultPerPage} ROWS FETCH NEXT {resultPerPage} ROWS ONLY";
            var result = _dataRepository.Execute<DataTable>(sql);
            return result;
        }
        public int Count(string firstname, string lastname, int? genderId, DateTime? birthDateBegin, DateTime? birthDateEnd, DateTime? registrationDateBegin, DateTime? registrationDateEnd, bool isActive)
        {
            var sql = $@"SELECT COUNT(*) FROM [LibraryMembership] A
                        WHERE A.[Firstname] LIKE '%{firstname}%'
                        AND [Lastname] LIKE '%{lastname}%'
                        AND [GenderId] = {(genderId.HasValue ? genderId.Value.ToString() : "[GenderId]") }
                        AND [Birthdate] BETWEEN '{birthDateBegin.Value.ToString("yyyy-MM-dd 00:00:00")}' AND '{birthDateEnd.Value.ToString("yyyy-MM-dd  23:59:59")}'
                        AND [RegistrationDate] BETWEEN '{registrationDateBegin.Value.ToString("yyyy-MM-dd 00:00:00")}' AND '{registrationDateEnd.Value.ToString("yyyy-MM-dd 23:59:59")}'
                        AND [IsActive] = '{isActive}'";
            var result = _dataRepository.Execute<int>(sql);
            return result;
        }

        public bool SetMemberImage(int id,string fileName)
        {
            var sql = $@"Update [LibraryMembership] SET MemberImage = '{fileName}'
                        WHERE [Id] = {id}";
            _dataRepository.Execute(sql);

            var result = _dataRepository.Execute<int>("SELECT @@ROWCOUNT") > 0;
            return result;
        }
    }
}
