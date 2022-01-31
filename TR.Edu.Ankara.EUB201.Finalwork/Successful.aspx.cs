using System;

namespace TR.Edu.Ankara.EUB201.Finalwork
{
    public partial class Successful : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var url = Request.QueryString["url"];
            Response.Headers.Add("refresh", $"3; url = {url}");
        }
    }
}