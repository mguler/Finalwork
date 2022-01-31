using System;
using System.Data;
using System.Web.UI.WebControls;
using TR.Edu.Ankara.EUB201.Finalwork.Business;

namespace TR.Edu.Ankara.EUB201.Finalwork.Loan
{
    public partial class LoanList : System.Web.UI.Page
    {
        private readonly LibraryOperationsService _libraryOperationsService;
        public LoanList(LibraryOperationsService libraryOperationsService)
        {
            _libraryOperationsService = libraryOperationsService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData(0);
            }
        }
        public void LoadData(int page, int resultPerPage= 25, string title= null, string membername=null, string userfullname=null, string dateBegin=null, string dateEnd=null, bool isCompleted=false)
        {
            var begin = DateTime.MinValue;
            var end = DateTime.MaxValue;

            if (!DateTime.TryParse(dateBegin, out begin))
            {
                begin = DateTime.MinValue;
            }

            if (!DateTime.TryParse(dateEnd, out end))
            {
                end = DateTime.MaxValue;
            }

            GridView1.VirtualItemCount = _libraryOperationsService.Count(title, membername, userfullname, begin, end, isCompleted);
            GridView1.DataSource = _libraryOperationsService.List(page, resultPerPage, title, membername, userfullname, begin, end, isCompleted);
            GridView1.PageIndex = page;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            LoadData(e.NewPageIndex, 25, txtBookname.Text ,txtMemberName.Text,txtUserfullname.Text,txtBeginOn.Text,txtScheduledEndDate.Text, !checkIsCompleted.Checked);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadData(0, 25, txtBookname.Text, txtMemberName.Text, txtUserfullname.Text, txtBeginOn.Text, txtScheduledEndDate.Text, !checkIsCompleted.Checked);

        }
        protected void GridView_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            var dataRowView = e.Row.DataItem as DataRowView;
            if (dataRowView != null)
            {
                e.Row.Cells[7].Visible = dataRowView.Row.Field<bool>("IsCompleted");
            }
        }
    }
}