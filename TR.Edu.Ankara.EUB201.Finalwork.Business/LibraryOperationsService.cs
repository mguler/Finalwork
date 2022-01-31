using System;
using System.Data;
using TR.Edu.Ankara.EUB201.Finalwork.DataAccess;

namespace TR.Edu.Ankara.EUB201.Finalwork.Business
{
    public class LibraryOperationsService
    {
        private readonly AdoNetDataRepository _dataRepository;
        public LibraryOperationsService(AdoNetDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public bool Give(int bookId, int memberId, int userId, DateTime? dateBegin, DateTime? dateEnd)
        {
            var sql = $@"INSERT INTO [dbo].[BooksOnLoan]([MemberId],[BookId],[UserId],[BeginOn],[ScheduledEndDate],[IsCompleted]) 
                SELECT {memberId},{bookId},{userId},'{(dateBegin.HasValue ? dateBegin.Value.ToString("yyyy-MM-dd") : "NULL")}','{(dateEnd.HasValue ? dateEnd.Value.ToString("yyyy-MM-dd") : "NULL")}',0";
            _dataRepository.Execute(sql);

            var result = _dataRepository.Execute<int>("SELECT SCOPE_IDENTITY()")>0;
            return result;
        }
        public bool TakeBack(int id)
        {
            var sql = $"UPDATE [dbo].[BooksOnLoan] SET [IsCompleted] = 1 WHERE Id = {id}";
            _dataRepository.Execute(sql);

            var result = _dataRepository.Execute<int>("SELECT SCOPE_IDENTITY()") > 0;
            return result;
        }

        public DataTable List(int page, int resultPerPage, string title, string membername, string userfullname, DateTime? dateBegin, DateTime? dateEnd, bool? isCompleted)
        {
            var sql = $@"SELECT A.Id, B.Title,CONCAT(C.Firstname,' ',C.Lastname) MemberName,CONCAT(D.Firstname,' ',D.Lastname) UserName,A.[BeginOn],A.[ScheduledEndDate],CASE WHEN A.[IsCompleted]=1 THEN CONVERT(BIT,0) ELSE CONVERT(BIT,1) END [IsCompleted]  FROM [BooksOnLoan] A
                        JOIN [BookDefinition]  B ON B.Id = A.BookId
                        JOIN [LibraryMembership] C ON C.Id = A.MemberId
                        JOIN [User] D ON D.Id = A.MemberId
                        WHERE B.[Title] LIKE '%{title}%'
                        AND CONCAT(C.Firstname,' ',C.Lastname) LIKE '%{membername}%'
                        AND CONCAT(D.Firstname,' ',D.Lastname) LIKE '%{userfullname}%'
                        AND A.[BeginOn] BETWEEN '{dateBegin.Value.ToString("yyyy-MM-dd 00:00:00")}' AND '{dateEnd.Value.ToString("yyyy-MM-dd  23:59:59")}'
                        AND A.[IsCompleted] = '{isCompleted}'
                        ORDER BY A.Id DESC OFFSET {page * resultPerPage} ROWS FETCH NEXT {resultPerPage} ROWS ONLY";
            var result = _dataRepository.Execute<DataTable>(sql);
            return result;
        }
        public int Count(string title, string membername, string userfullname, DateTime? dateBegin, DateTime? dateEnd, bool? isCompleted)
        {
            var sql = $@"SELECT COUNT(*) FROM [BooksOnLoan] A
                        JOIN [BookDefinition]  B ON B.Id = A.BookId
                        JOIN [LibraryMembership] C ON C.Id = A.MemberId
                        JOIN [User] D ON D.Id = A.MemberId
                        WHERE B.[Title] LIKE '%{title}%'
                        AND CONCAT(C.Firstname,' ',C.Lastname) LIKE '%{membername}%'
                        AND CONCAT(D.Firstname,' ',D.Lastname) LIKE '%{userfullname}%'
                        AND A.[BeginOn] BETWEEN '{dateBegin.Value.ToString("yyyy-MM-dd 00:00:00")}' AND '{dateEnd.Value.ToString("yyyy-MM-dd  23:59:59")}'
                        AND A.[IsCompleted] = '{isCompleted}'";
            var result = _dataRepository.Execute<int>(sql);
            return result;
        }
    }
}
