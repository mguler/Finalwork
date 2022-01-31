using System;
using System.Data;
using TR.Edu.Ankara.EUB201.Finalwork.Business;

namespace TR.Edu.Ankara.EUB201.Finalwork
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly AuthenticationService _authenticationService;
        public Login(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var result = false;
            bool.TryParse(Session["IsLoggedIn"]?.ToString(), out result);
            if (result)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void Login1_Authenticate(object sender, System.Web.UI.WebControls.AuthenticateEventArgs e)
        {
            var userId = _authenticationService.Login(Login1.UserName, Login1.Password);
            if (userId != 0) 
            {
                var user = _authenticationService.GetDetail(userId);
                Session["IsLoggedIn"] = true;
                Session["UserId"] = userId;
                Session["Name"] = $"{user.Rows[0].Field<string>("Firstname")} {user.Rows[0].Field<string>("Lastname")}";
                Response.Redirect("Default.aspx");
            }
        }
    }
}