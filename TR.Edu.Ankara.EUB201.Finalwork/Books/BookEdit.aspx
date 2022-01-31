<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookEdit.aspx.cs" Inherits="TR.Edu.Ankara.EUB201.Finalwork.Books.BookEdit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:HiddenField ID="recordId" runat="server" />
        <div>
            <table>
                <tr>
                    <td><asp:Label ID="Label1" runat="server" Text="Adı"></asp:Label></td>
                    <td><asp:TextBox ID="txtTitle" runat="server" Width="75%"></asp:TextBox></td>
                    <td><asp:Label ID="Label2" runat="server" Text="ISBN"></asp:Label></td>
                    <td><asp:TextBox ID="txtIsbn" runat="server" Width="75%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label3" runat="server" Text="Kategori" ></asp:Label></td>
                    <td><asp:DropDownList ID="ddlCategories" runat="server" DataTextField="Name" DataValueField="Id" Width="75%"></asp:DropDownList></td>
                    <td><asp:Label ID="Label4" runat="server" Text="Yazar"></asp:Label></td>
                    <td><asp:DropDownList ID="ddlAuthors" runat="server" DataTextField="Name" DataValueField="Id" Width="75%"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label5" runat="server" Text="Sayfa Sayısı"></asp:Label></td>
                    <td><asp:TextBox ID="txtPageCount" runat="server" TextMode="Number" Width="75%"></asp:TextBox></td>
                    <td><asp:Label ID="Label6" runat="server" Text="Yayın Yılı"></asp:Label></td>
                    <td><asp:TextBox ID="txtYear" runat="server" TextMode="Number" Width="75%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label7" runat="server" Text="Ciltlenmiş"></asp:Label></td>
                    <td><asp:CheckBox ID="checkIsCovered" runat="server" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label8" runat="server" Text="Detay"></asp:Label></td>
                    <td colspan="3"><asp:TextBox ID="txtDescription" runat="server" Width="100%" TextMode="MultiLine" Height="87px" ></asp:TextBox></td>
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