using System;
using TR.Edu.Ankara.EUB201.Finalwork.Business;

namespace TR.Edu.Ankara.EUB201.Finalwork.Users
{
    public partial class UserList : System.Web.UI.Page
    {
        private readonly AuthenticationService _authenticationService;
        private readonly ReferenceDataService _referenceDataService;
        public UserList(AuthenticationService authenticationService, ReferenceDataService referenceDataService)
        {
            _authenticationService = authenticationService;
            _referenceDataService = referenceDataService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData(0);
            }
        }
        public void LoadData(int page, string firstname = null, string lastname = null, string email = null, bool isActive = true)
        {
            GridView1.VirtualItemCount = _authenticationService.Count(firstname, lastname, email, isActive);
            GridView1.DataSource = _authenticationService.List(page, 25, firstname, lastname, email, isActive);
            GridView1.PageIndex = page;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            
            LoadData(e.NewPageIndex, txtFirstname.Text, txtLastname.Text, txtEmail.Text, checkIsActive.Checked);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadData(0, txtFirstname.Text, txtLastname.Text, txtEmail.Text, checkIsActive.Checked);
        }
    }
}