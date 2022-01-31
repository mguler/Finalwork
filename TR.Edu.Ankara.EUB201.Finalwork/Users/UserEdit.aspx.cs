using System;
using System.Data;
using TR.Edu.Ankara.EUB201.Finalwork.Business;
using TR.Edu.Ankara.EUB201.Finalwork.Common.ExceptionManagement;

namespace TR.Edu.Ankara.EUB201.Finalwork.Users
{
    public partial class UserEdit : System.Web.UI.Page
    {
        private readonly AuthenticationService _authenticationService;
        public UserEdit(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
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
            var data = _authenticationService.GetDetail(id);
            if (data.Rows.Count > 0)
            {
                recordId.Value = data.Rows[0].Field<int>("Id").ToString();
                txtFirstname.Text= data.Rows[0].Field<string>("Firstname");
                txtLastname.Text = data.Rows[0].Field<string>("Lastname");
                txtPassword.Text = data.Rows[0].Field<string>("Password");
                txtEmail.Text = data.Rows[0].Field<string>("Email");
                checkIsActive.Checked = data.Rows[0].Field<bool>("IsActive");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var id = 0;
                int.TryParse(recordId.Value, out id);
                var firstname = txtFirstname.Text;
                var lastname = txtLastname.Text;
                var email = txtEmail.Text;
                var password = txtPassword.Text;
                var isActive = checkIsActive.Checked;

                var result = _authenticationService.Save(id, email, password, firstname, lastname,isActive);
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
            Response.Redirect("UserEdit.aspx");
        }
        protected void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                var id = 0;
                int.TryParse(recordId.Value, out id);
                var result = _authenticationService.Delete(id);
                if(!result) throw new CustomApplicationException("işleminiz yapılamadı. Daha sonra tekrar deneyiniz");
                Response.Redirect("/Successful.aspx?url=/Users/UserList.aspx");
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