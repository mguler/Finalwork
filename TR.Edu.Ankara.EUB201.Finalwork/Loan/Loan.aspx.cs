using System;
using System.Data;
using TR.Edu.Ankara.EUB201.Finalwork.Business;
using TR.Edu.Ankara.EUB201.Finalwork.Common.ExceptionManagement;

namespace TR.Edu.Ankara.EUB201.Finalwork.Loan
{
    public partial class Loan : System.Web.UI.Page
    {
        private readonly LibraryOperationsService _libraryOperationsService;
        private readonly ReferenceDataService _referenceDataService;
        public Loan(LibraryOperationsService libraryOperationsService, ReferenceDataService referenceDataService)
        {
            _libraryOperationsService = libraryOperationsService;
            _referenceDataService = referenceDataService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            errorText.Text = "";
            errorText.Visible = false;
            if (!Page.IsPostBack)
            {
                ddlBooks.DataSource = _referenceDataService.ListAllBooks();
                ddlMembers.DataSource = _referenceDataService.ListAllMembers();
                ddlBooks.DataBind();
                ddlMembers.DataBind();
                txtBeginDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtEndDate.Text = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var bookId = Convert.ToInt32(ddlBooks.SelectedValue);
                var memberId = Convert.ToInt32(ddlMembers.SelectedValue);
                var userId = Convert.ToInt32(Session["UserId"]);

                var beginTemp = DateTime.MinValue;
                var endTemp = DateTime.MaxValue;
                DateTime? begin = null;
                DateTime? end = null;

                if (DateTime.TryParse(txtBeginDate.Text, out beginTemp))
                {
                    begin = beginTemp;
                }

                if (DateTime.TryParse(txtEndDate.Text, out endTemp))
                {
                    end = endTemp;
                }


                if (!_libraryOperationsService.Give(bookId, memberId, userId,begin,end))
                {
                    throw new CustomApplicationException("işleminiz yapılamadı. Daha sonra tekrar deneyiniz");
                }
                Response.Write("<script>alert('İşleminiz kaydedildi')</script>");
            }
            catch (CustomApplicationException ex)
            {
                errorText.Visible = true;
                errorText.Text = ex.Message;
            }
            catch 
            {
                errorText.Visible = true;
                errorText.Text = "Hata meydana geldi ve işleminiz tamamlanmamış olabilir. Daha sonra tekrar deneyiniz";
            }
        }
    }
}