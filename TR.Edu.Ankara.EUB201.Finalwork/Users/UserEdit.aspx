<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="TR.Edu.Ankara.EUB201.Finalwork.Users.UserEdit" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <asp:HiddenField ID="recordId" runat="server" />
        <div>
            <table>
                <tr>
                    <td><asp:Label ID="Label1" runat="server" Text="Adı"></asp:Label></td>
                    <td><asp:TextBox ID="txtFirstname" runat="server" Width="75%"></asp:TextBox></td>
                    <td><asp:Label ID="Label2" runat="server" Text="Soyadı"></asp:Label></td>
                    <td><asp:TextBox ID="txtLastname" runat="server" Width="75%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label3" runat="server" Text="E-Posta" ></asp:Label></td>
                    <td><asp:TextBox ID="txtEmail" runat="server" Width="75%" TextMode="Email"></asp:TextBox></td>
                    <td><asp:Label ID="Label4" runat="server" Text="Şifre"></asp:Label></td>
                    <td><asp:TextBox ID="txtPassword" runat="server" Width="75%" TextMode="Password"></asp:TextBox></td>
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