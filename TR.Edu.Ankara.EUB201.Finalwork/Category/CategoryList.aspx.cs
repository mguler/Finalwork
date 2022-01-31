using System;
using TR.Edu.Ankara.EUB201.Finalwork.Business;

namespace TR.Edu.Ankara.EUB201.Finalwork.Category
{
    public partial class CategoryList : System.Web.UI.Page
    {
        private readonly CategoryService _categoryService;
        public CategoryList(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData(0);
            }
        }
        public void LoadData(int page, int resultPerPage = 25, string name = null)
        {
            GridView1.VirtualItemCount = _categoryService.Count(name);
            GridView1.DataSource = _categoryService.List(page, resultPerPage, name);
            GridView1.PageIndex = page;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            LoadData(e.NewPageIndex, 25, txtName.Text);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadData(0, 25, txtName.Text);
        }
    }
}