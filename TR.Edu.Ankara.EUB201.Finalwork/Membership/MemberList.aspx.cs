using System;
using TR.Edu.Ankara.EUB201.Finalwork.Business;

namespace TR.Edu.Ankara.EUB201.Finalwork.Membership
{
    public partial class MemberList : System.Web.UI.Page
    {
        private readonly LibraryMembershipService _libraryMembershipService;
        private readonly ReferenceDataService _referenceDataService;
        public MemberList(LibraryMembershipService libraryMembershipService, ReferenceDataService referenceDataService)
        {
            _libraryMembershipService = libraryMembershipService;
            _referenceDataService = referenceDataService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlGender.DataSource = _referenceDataService.ListAllGenderDefinitions();
                ddlGender.DataBind();
                LoadData(0);
            }
        }
        public void LoadData(int page, int resultPerPage = 25, string firstname = null, string lastname = null, string genderId = null, string birthdateBegin = null, string birthdateEnd = null, string registrationDateBegin = null, string registrationDateEnd = null, bool isActive = true)
        {
            var bdBegin = DateTime.MinValue;
            var bdEnd = DateTime.MaxValue;
            var rdBegin = DateTime.MinValue;
            var rdEnd = DateTime.MaxValue;
            DateTime.TryParse(txtBirthdateBegin.Text, out bdBegin);

            if (!DateTime.TryParse(txtBirthdateEnd.Text, out bdEnd))
            {
                bdEnd = DateTime.MaxValue;
            }

            DateTime.TryParse(txtRegistrationDateBegin.Text, out rdBegin);
            if (!DateTime.TryParse(txtRegistrationDateEnd.Text, out rdEnd))
            {
                rdEnd = DateTime.MaxValue;
            }

            var temporaryGenderId = 0;
            var genderIdParsed = default(int?);
            if(int.TryParse(genderId,out temporaryGenderId))
            {
                genderIdParsed = temporaryGenderId;
            }

            GridView1.VirtualItemCount = _libraryMembershipService.Count(firstname, lastname, genderIdParsed, bdBegin, bdEnd, rdBegin, rdEnd, isActive);
            GridView1.DataSource = _libraryMembershipService.List(page, resultPerPage, firstname, lastname, genderIdParsed, bdBegin, bdEnd, rdBegin, rdEnd, isActive);
            GridView1.PageIndex = page;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            LoadData(e.NewPageIndex, 25, txtFirstname.Text, txtLastname.Text, ddlGender.SelectedValue, txtBirthdateBegin.Text, txtBirthdateEnd.Text, txtRegistrationDateBegin.Text, txtRegistrationDateEnd.Text, checkIsActive.Checked);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadData(0, 25, txtFirstname.Text, txtLastname.Text,ddlGender.SelectedValue, txtBirthdateBegin.Text, txtBirthdateEnd.Text, txtRegistrationDateBegin.Text, txtRegistrationDateEnd.Text, checkIsActive.Checked);
        }
    }
}