using System;
using TR.Edu.Ankara.EUB201.Finalwork.Business;

namespace TR.Edu.Ankara.EUB201.Finalwork.Books
{
    public partial class BookList : System.Web.UI.Page
    {
        private readonly BookService _bookService;
        private readonly ReferenceDataService _referenceDataService;
        public BookList(BookService bookService,ReferenceDataService referenceDataService)
        {
            _bookService = bookService;
            _referenceDataService = referenceDataService;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlCategories.DataSource = _referenceDataService.ListAllCategories();
                ddlCategories.DataBind();

                LoadData(0);
            }
        }
        public void LoadData(int page, string title = null, string author = null, string isbn = null, int categoryId = 0, int publishmentYear = 0)
        {
            GridView1.VirtualItemCount = _bookService.Count(title, author, isbn, categoryId, publishmentYear);
            GridView1.DataSource = _bookService.List(page, 25, title, author, isbn, categoryId, publishmentYear);
            GridView1.PageIndex = page;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            var publishmentYear = 0;
            int.TryParse(txtPublismenYear.Text, out publishmentYear);
            var categoryId = 0;
            int.TryParse(ddlCategories.SelectedValue, out categoryId);
            LoadData(e.NewPageIndex, txtTitle.Text, txtAuthor.Text, txtIsbn.Text, categoryId, publishmentYear);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var publishmentYear = 0;
            int.TryParse(txtPublismenYear.Text, out publishmentYear);
            var categoryId = 0;
            int.TryParse(ddlCategories.SelectedValue, out categoryId);
            LoadData(0, txtTitle.Text, txtAuthor.Text, txtIsbn.Text, categoryId, publishmentYear);

        }
    }
}