using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using TR.Edu.Ankara.EUB201.Finalwork.Business;
using TR.Edu.Ankara.EUB201.Finalwork.Common.ExceptionManagement;

namespace TR.Edu.Ankara.EUB201.Finalwork.Membership
{
    public partial class MemberEdit : System.Web.UI.Page
    {
        private readonly LibraryMembershipService _libraryMembershipService;
        private readonly ReferenceDataService _referenceDataService;
        public MemberEdit(LibraryMembershipService libraryMembershipService, ReferenceDataService referenceDataService)
        {
            _libraryMembershipService = libraryMembershipService;
            _referenceDataService = referenceDataService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            errorText.Text = "";
            errorText.Visible = false;
            if (!Page.IsPostBack)
            {
                var id = Request.QueryString["Id"];
                var result = 0;
                int.TryParse(id, out result);
                ddlGender.DataSource = _referenceDataService.ListAllGenderDefinitions();
                ddlGender.DataBind();
                LoadData(result);
            }
        }
        public void LoadData(int id)
        {
            var data = _libraryMembershipService.GetDetail(id);
            if (data.Rows.Count > 0)
            {
                recordId.Value = data.Rows[0].Field<int>("Id").ToString();
                txtFirstname.Text= data.Rows[0].Field<string>("Firstname");
                txtLastname.Text = data.Rows[0].Field<string>("Lastname");
                ddlGender.SelectedValue = data.Rows[0].Field<int>("GenderId").ToString();
                txtBirthDate.TextMode = System.Web.UI.WebControls.TextBoxMode.SingleLine;
                txtBirthDate.Text = data.Rows[0].Field<DateTime?>("Birthdate").Value.ToString("yyyy-MM-dd");
                txtBirthDate.TextMode = System.Web.UI.WebControls.TextBoxMode.Date;
                checkIsActive.Checked = data.Rows[0].Field<bool>("IsActive");
                imgMember.ImageUrl = $"~\\Membership\\MemberImage.aspx?Id={data.Rows[0].Field<string>("MemberImage")}";
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var id = 0;
                int.TryParse(recordId.Value, out id);
                var firstname = txtFirstname.Text;
                var lastname = txtLastname.Text;
                var genderId = 0;
                int.TryParse(ddlGender.SelectedValue, out genderId);
                var birthDate = default(DateTime);
                DateTime.TryParse(txtBirthDate.Text, out birthDate);
                var isActive = checkIsActive.Checked;

                var result = _libraryMembershipService.Save(id, firstname, lastname, genderId, birthDate, DateTime.Now, isActive);

                if (id == 0)
                {
                    recordId.Value = result.ToString();
                }

                if (result != 0)
                {
                    Response.Write("<script>alert('Başarılı bir şekilde kaydedildi')</script>");
                }
                else
                {
                    throw new CustomApplicationException("işleminiz yapılamadı. Daha sonra tekrar deneyiniz");
                }
            }
            catch (CustomApplicationException ex)
            {
                errorText.Visible = true;
                errorText.Text = ex.Message;
            }
            catch 
            {
                errorText.Visible = true;
                errorText.Text = "Hata meydana geldi ve işleminiz tamamlanmamış olabilir. Daha sonra tekrar deneyiniz";
            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberEdit.aspx");
        }
        protected void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                var id = 0;
                int.TryParse(recordId.Value, out id);
                var result = _libraryMembershipService.Delete(id);
                if(!result) throw new CustomApplicationException("işleminiz yapılamadı. Daha sonra tekrar deneyiniz");
                Response.Redirect("/Successful.aspx?url=/Membership/MemberList.aspx");
            }
            catch (CustomApplicationException ex)
            {
                errorText.Visible = true;
                errorText.Text = ex.Message;
            }
            catch 
            {
                errorText.Visible = true;
                errorText.Text = "Hata meydana geldi ve işleminiz tamamlanmamış olabilir. Daha sonra tekrar deneyiniz";
            }
        }

        protected void btnImageSave_Click(object sender, EventArgs e)
        {
            var fileName = Guid.NewGuid().ToString();
            try
            {
                var id = 0;
                int.TryParse(recordId.Value, out id);

                if (id == 0) throw new CustomApplicationException("Üye bilgisi hatası");

                if (!_libraryMembershipService.SetMemberImage(id, fileName))
                {
                    throw new CustomApplicationException("İşleminiz yapılamadı,lütfen daha sonra tekrar deneyiniz");
                }

                if (!fileMemberImage.HasFile) throw new CustomApplicationException("Resim seçmediniz");
                using (var fileStream = new FileStream($"{Server.MapPath("~\\MemberImages")}\\{fileName}", FileMode.CreateNew))
                {
                    fileMemberImage.FileContent.Position = 0;
                    fileMemberImage.FileContent.CopyTo(fileStream);
                    fileStream.Position = 0;
                    fileStream.Flush();
                }

            }
            catch (CustomApplicationException ex)
            {
                errorText.Visible = true;
                errorText.Text = ex.Message;
            }
            catch(Exception ex)
            {
                errorText.Visible = true;
                errorText.Text = "Hata meydana geldi ve işleminiz tamamlanmamış olabilir. Daha sonra tekrar deneyiniz";
            }
        }
    }
}