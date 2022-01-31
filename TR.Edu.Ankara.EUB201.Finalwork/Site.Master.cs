using System;
using System.Web.UI;

namespace TR.Edu.Ankara.EUB201.Finalwork
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var result = false;
            bool.TryParse(Session["IsLoggedIn"]?.ToString(), out result);
            if (!result)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}