<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="TR.Edu.Ankara.EUB201.Finalwork.Category.CategoryList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <table>
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Adı"></asp:Label></td>
                <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Filtrele" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
        <div>
            <asp:GridView ID="GridView1" runat="server" PageSize="25" AllowPaging="True" AllowCustomPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="No" />
                    <asp:BoundField DataField="Name" HeaderText="Adı" />
                    <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/Category/CategoryEdit.aspx?Id={0}" Text="Düzenle" />
                </Columns>
            </asp:GridView>

        </div>
</asp:Content>
