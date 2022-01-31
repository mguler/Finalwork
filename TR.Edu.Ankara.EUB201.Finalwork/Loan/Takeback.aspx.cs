using System;
using TR.Edu.Ankara.EUB201.Finalwork.Business;
using TR.Edu.Ankara.EUB201.Finalwork.Common.ExceptionManagement;

namespace TR.Edu.Ankara.EUB201.Finalwork.Loan
{
    public partial class Takeback : System.Web.UI.Page
    {
        private readonly LibraryOperationsService _libraryOperationsService;
        public Takeback(LibraryOperationsService libraryOperationsService)
        {
            _libraryOperationsService = libraryOperationsService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var operationId = Convert.ToInt32(Request.QueryString["Id"]);
                if (!_libraryOperationsService.TakeBack(operationId))
                {
                    throw new CustomApplicationException("işleminiz yapılamadı. Daha sonra tekrar deneyiniz");
                }
                Response.Write("<script>alert('Başarılı bir şekilde kaydedildi')</script>");
            }
            catch (CustomApplicationException ex)
            {
                Response.Write($"<script>alert('{ex.Message}')</script>");
            }
            catch
            {
                Response.Write("<script>alert('işleminiz yapılamadı. Daha sonra tekrar deneyiniz')</script>");
            }
            Response.Redirect("LoanList.aspx");
        }
    }
}