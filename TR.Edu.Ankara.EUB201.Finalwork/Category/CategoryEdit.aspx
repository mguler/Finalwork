<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoryEdit.aspx.cs" Inherits="TR.Edu.Ankara.EUB201.Finalwork.Category.CategoryEdit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">    
        <asp:HiddenField ID="recordId" runat="server" />
        <div>
            <table>
                <tr>
                    <td><asp:Label ID="Label1" runat="server" Text="Adı"></asp:Label></td>
                    <td><asp:TextBox ID="txtName" runat="server" Width="75%"></asp:TextBox></td>
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