using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TR.Edu.Ankara.EUB201.Finalwork.Membership
{
    public partial class MemberImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var fileName = Request.QueryString["Id"];
            try {
                Response.ContentType = "image/png;";

                var file = File.OpenRead($"{Server.MapPath("~\\MemberImages")}\\{fileName}");
                file.Position = 0;
                file.CopyTo(Response.OutputStream);
            }

            catch {
                Response.End();
                    }

        }
    }
}