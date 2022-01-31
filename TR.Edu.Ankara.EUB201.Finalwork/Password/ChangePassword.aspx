<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="TR.Edu.Ankara.EUB201.Finalwork.Password.ChangePassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:HiddenField ID="recordId" runat="server" />
        <div>
            <table>
                <tr>
                    <td><asp:Label ID="Label1" runat="server" Text="Şifre"></asp:Label></td>
                    <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label2" runat="server" Text="Yeni Şifre" ></asp:Label></td>
                    <td><asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label3" runat="server" Text="Yeni Şifre Tekrar"></asp:Label></td>
                    <td><asp:TextBox ID="txtNewPasswordAgain" runat="server" TextMode="Password" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="errorText" Visible="false" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnSave" runat="server" Text="Kaydet" OnClick="btnSave_Click"/>
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>