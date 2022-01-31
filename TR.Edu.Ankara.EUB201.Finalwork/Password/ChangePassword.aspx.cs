using System;
using System.Data;
using TR.Edu.Ankara.EUB201.Finalwork.Business;
using TR.Edu.Ankara.EUB201.Finalwork.Common.ExceptionManagement;

namespace TR.Edu.Ankara.EUB201.Finalwork.Password
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        private readonly AuthenticationService _authenticationService;
        public ChangePassword(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            errorText.Text = "";
            errorText.Visible = false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(Session["UserId"]);
                if (id!=0)
                {

                    var result = _authenticationService.ChangePassword(id, txtPassword.Text, txtNewPassword.Text, txtNewPasswordAgain.Text);
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
    }
}