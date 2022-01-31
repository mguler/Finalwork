using System;
using System.Data;
using TR.Edu.Ankara.EUB201.Finalwork.Business;
using TR.Edu.Ankara.EUB201.Finalwork.Common.ExceptionManagement;

namespace TR.Edu.Ankara.EUB201.Finalwork.Author
{
    public partial class AuthorEdit : System.Web.UI.Page
    {
        private readonly AuthorService _authorService;
        public AuthorEdit(AuthorService authorService)
        {
            _authorService = authorService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            errorText.Text = "";
            errorText.Visible = false;
            if (!Page.IsPostBack)
            {
                var id = Request.QueryString["Id"];
                var result = 0;
                int.TryParse(id, out result);
                LoadData(result);
            }
        }
        public void LoadData(int id)
        {
            var data = _authorService.GetDetail(id);
            if (data.Rows.Count > 0)
            {
                recordId.Value = data.Rows[0].Field<int>("Id").ToString();
                txtName.Text = data.Rows[0].Field<string>("Name");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var id = 0;
                int.TryParse(recordId.Value, out id);
                var genderId = 0;
                var birthDate = default(DateTime);
                var result = _authorService.Save(id, txtName.Text);

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
            Response.Redirect("AuthorEdit.aspx");
        }
        protected void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                var id = 0;
                int.TryParse(recordId.Value, out id);
                var result = _authorService.Delete(id);
                if(!result) throw new CustomApplicationException("işleminiz yapılamadı. Daha sonra tekrar deneyiniz");
                Response.Redirect("/Successful.aspx?url=/Author/AuthorList.aspx");
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