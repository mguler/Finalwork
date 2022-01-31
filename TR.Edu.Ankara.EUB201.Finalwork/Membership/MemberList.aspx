<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MemberList.aspx.cs" Inherits="TR.Edu.Ankara.EUB201.Finalwork.Membership.MemberList" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <table>
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Adı"></asp:Label></td>
                <td><asp:TextBox ID="txtFirstname" runat="server"></asp:TextBox></td>
                <td><asp:Label ID="Label2" runat="server" Text="Soyadı"></asp:Label></td>
                <td><asp:TextBox ID="txtLastname" runat="server"></asp:TextBox></td>
                <td><asp:Label ID="Label5" runat="server" Text="Doğum Tarihi Aralığı"></asp:Label></td>
                <td><asp:TextBox ID="txtBirthdateBegin" runat="server" TextMode="Date"></asp:TextBox>-<asp:TextBox ID="txtBirthdateEnd" runat="server" TextMode="Date"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label3" runat="server" Text="Cinsiyet"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlGender" runat="server" DataTextField="Name" DataValueField="Id" style="height: 22px">
                    </asp:DropDownList>
                </td>
                <td><asp:Label ID="Label4" runat="server" Text="Aktif"></asp:Label></td>
                <td><asp:CheckBox ID="checkIsActive" runat="server" Checked="true"/></td>
                <td><asp:Label ID="Label6" runat="server" Text="Kayıt Tarihi Aralığı"></asp:Label></td>
                <td><asp:TextBox ID="txtRegistrationDateBegin" runat="server" TextMode="Date"></asp:TextBox>-<asp:TextBox ID="txtRegistrationDateEnd" runat="server" TextMode="Date"></asp:TextBox></td>
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
                    <asp:CheckBoxField DataField="IsActive" HeaderText="Aktif" />
                    <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/Membership/MemberEdit.aspx?Id={0}" Text="Düzenle" />
                </Columns>
            </asp:GridView>

        </div>
    </asp:Content>