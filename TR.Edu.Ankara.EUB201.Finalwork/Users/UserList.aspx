<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="TR.Edu.Ankara.EUB201.Finalwork.Users.UserList" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <table>
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Adı"></asp:Label></td>
                <td><asp:TextBox ID="txtFirstname" runat="server"></asp:TextBox></td>
                <td><asp:Label ID="Label2" runat="server" Text="Soyadı"></asp:Label></td>
                <td><asp:TextBox ID="txtLastname" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label3" runat="server" Text="E-Posta"></asp:Label></td>
                <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                <td><asp:Label ID="Label4" runat="server" Text="Aktif"></asp:Label></td>
                <td><asp:CheckBox ID="checkIsActive" runat="server" Checked="true"/></td>
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
                    <asp:BoundField DataField="Firstname" HeaderText="Adı" />
                    <asp:BoundField DataField="Lastname" HeaderText="Soyadı" />
                    <asp:BoundField DataField="Email" HeaderText="E-Posta" />
                    <asp:CheckBoxField DataField="IsActive" HeaderText="Aktif" />
                    <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/Users/UserEdit.aspx?Id={0}" Text="Düzenle" />
                </Columns>
            </asp:GridView>

        </div>
</asp:Content>