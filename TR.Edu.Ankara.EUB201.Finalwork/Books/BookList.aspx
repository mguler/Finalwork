<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookList.aspx.cs" Inherits="TR.Edu.Ankara.EUB201.Finalwork.Books.BookList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <table >
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Adı"></asp:Label></td>
                <td><asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></td>
                <td><asp:Label ID="Label2" runat="server" Text="ISBN"></asp:Label></td>
                <td><asp:TextBox ID="txtIsbn" runat="server"></asp:TextBox></td>
                <td><asp:Label ID="Label3" runat="server" Text="Yazar"></asp:Label></td>
                <td><asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label4" runat="server" Text="Kategori"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlCategories" runat="server" DataTextField="Name" DataValueField="Id">
                    </asp:DropDownList>
                </td>
                <td><asp:Label ID="Label5" runat="server" Text="Yayın Yılı"></asp:Label></td>
                <td><asp:TextBox ID="txtPublismenYear" runat="server" TextMode="Number"></asp:TextBox></td>
            </tr>
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
                    <asp:BoundField DataField="Title" HeaderText="Adı" />
                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                    <asp:BoundField DataField="Category" HeaderText="Kategori" />
                    <asp:BoundField DataField="Author" HeaderText="Yazar" />
                    <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/Books/BookEdit.aspx?Id={0}" Text="Düzenle" />
                </Columns>
            </asp:GridView>

        </div>
</asp:Content>