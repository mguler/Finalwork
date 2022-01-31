<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MemberEdit.aspx.cs" Inherits="TR.Edu.Ankara.EUB201.Finalwork.Membership.MemberEdit" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:HiddenField ID="recordId" runat="server" />
        <div>
            <table>
                <tr>
                    <td colspan="4">
                        <center>
                        <asp:Image ID="imgMember" runat="server" style="min-height:200px!important;max-height:200px!important;min-width:200px!important;max-width:200px!important"   Height="200px" Width="150px" AlternateText="Üye Resmi" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" ></asp:Image></center></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:FileUpload ID="fileMemberImage" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="btnImageSave" runat="server" Text="Resmi Kaydet" OnClick="btnImageSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label1" runat="server" Text="Adı"></asp:Label></td>
                    <td><asp:TextBox ID="txtFirstname" runat="server" Width="75%"></asp:TextBox></td>
                    <td><asp:Label ID="Label2" runat="server" Text="Soyadı"></asp:Label></td>
                    <td><asp:TextBox ID="txtLastname" runat="server" Width="75%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label3" runat="server" Text="Cinsiyet" ></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlGender" runat="server" DataTextField="Name" DataValueField="Id">
                        </asp:DropDownList>
                    </td>
                    <td><asp:Label ID="Label4" runat="server" Text="Doğum Tarihi"></asp:Label></td>
                    <td><asp:TextBox ID="txtBirthDate" runat="server" Width="75%" TextMode="Date"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label7" runat="server" Text="Aktif"></asp:Label></td>
                    <td><asp:CheckBox ID="checkIsActive" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="errorText" Visible="false" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnNew" runat="server" Text="Yeni" OnClick="btnNew_Click"/>
                        <asp:Button ID="btnSave" runat="server" Text="Kaydet" OnClick="btnSave_Click" />
                        <asp:Button ID="btnSil" runat="server" Text="Sil" OnClick="btnSil_Click" />
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>