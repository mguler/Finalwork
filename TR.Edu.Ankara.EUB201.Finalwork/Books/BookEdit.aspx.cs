using System;
using System.Data;
using TR.Edu.Ankara.EUB201.Finalwork.Business;
using TR.Edu.Ankara.EUB201.Finalwork.Common.ExceptionManagement;

namespace TR.Edu.Ankara.EUB201.Finalwork.Books
{
    public partial class BookEdit : System.Web.UI.Page
    {
        private readonly BookService _bookService;
        private readonly ReferenceDataService _referenceDataService;
        public BookEdit(BookService bookService,ReferenceDataService referenceDataService)
        {
            _bookService = bookService;
            _referenceDataService = referenceDataService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            errorText.Text = "";
            errorText.Visible = false;
            if (!Page.IsPostBack)
            {
                ddlAuthors.DataSource = _referenceDataService.ListAllAuthors();
                ddlCategories.DataSource = _referenceDataService.ListAllCategories();
                ddlAuthors.DataBind();
                ddlCategories.DataBind();

                var id = Request.QueryString["Id"];
                var result = 0;
                int.TryParse(id, out result);
                LoadData(result);
            }
        }
        public void LoadData(int id)
        {
            var data = _bookService.GetDetail(id);
            if (data.Rows.Count > 0)
            {
                recordId.Value = data.Rows[0].Field<int>("Id").ToString();
                txtTitle.Text = data.Rows[0].Field<string>("Title");
                txtIsbn.Text = data.Rows[0].Field<string>("ISBN");
                txtDescription.Text = data.Rows[0].Field<string>("Description");
                ddlAuthors.SelectedValue = data.Rows[0].Field<int>("AuthorId").ToString();
                ddlCategories.SelectedValue = data.Rows[0].Field<int>("CategoryId").ToString();
                txtPageCount.Text = data.Rows[0].Field<int>("PageCount").ToString();
                txtYear.Text = data.Rows[0].Field<int>("PublishmentYear").ToString();
                checkIsCovered.Checked = data.Rows[0].Field<bool>("IsCovered");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var id = 0;
                int.TryParse(recordId.Value, out id);
                var title = txtTitle.Text.Replace("'", "''");
                var isbn = txtIsbn.Text.Replace("'", "''");
                var description = txtDescription.Text.Replace("'", "''");
                var authorId = 0;
                int.TryParse(ddlAuthors.SelectedValue, out authorId);
                var categoryId = 0;
                int.TryParse(ddlCategories.SelectedValue, out categoryId);
                var pageCount = 0;
                int.TryParse(txtPageCount.Text, out pageCount);
                var publishmentYear = 0;
                int.TryParse(txtYear.Text, out publishmentYear);
                var isCovered = checkIsCovered.Checked;
                var result = _bookService.Save(id, title, categoryId, description, authorId, isbn, pageCount, publishmentYear, isCovered);
                if (id == 0)
                {
                    recordId.Value = result.ToString();
                }

                if (result != 0)
                {
                    Response.Write("<script>alert('Başarılı bir şekilde kaydedildi')</script>");
                }
                else
                {
                    throw new CustomApplicationException("işleminiz yapılamadı. Daha sonra tekrar deneyiniz");
                }
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
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookEdit.aspx");
        }
        protected void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                var id = 0;
                int.TryParse(recordId.Value, out id);
                var result = _bookService.Delete(id);
                if(!result) throw new CustomApplicationException("işleminiz yapılamadı. Daha sonra tekrar deneyiniz");
                Response.Redirect("/Successful.aspx?url=/Books/BookList.aspx");
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