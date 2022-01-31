using System;
using TR.Edu.Ankara.EUB201.Finalwork.Business;

namespace TR.Edu.Ankara.EUB201.Finalwork.Gender
{
    public partial class GenderList : System.Web.UI.Page
    {
        private readonly GenderService _genderService;
        public GenderList(GenderService genderService)
        {
            _genderService = genderService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData(0);
            }
        }
        public void LoadData(int page, int resultPerPage = 25, string name = null, bool isActive = true)
        {
            GridView1.VirtualItemCount = _genderService.Count(name, isActive);
            GridView1.DataSource = _genderService.List(page, resultPerPage, name, isActive);
            GridView1.PageIndex = page;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            LoadData(e.NewPageIndex, 25, txtName.Text,checkIsActive.Checked);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadData(0, 25, txtName.Text,checkIsActive.Checked);
        }
    }
}