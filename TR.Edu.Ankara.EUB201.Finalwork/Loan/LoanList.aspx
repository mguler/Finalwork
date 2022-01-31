<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoanList.aspx.cs" Inherits="TR.Edu.Ankara.EUB201.Finalwork.Loan.LoanList" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <table>
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Kitap Adı"></asp:Label></td>
                <td><asp:TextBox ID="txtBookname" runat="server"></asp:TextBox></td>
                <td><asp:Label ID="Label5" runat="server" Text="Üye Adı"></asp:Label></td>
                <td><asp:TextBox ID="txtMemberName" runat="server"></asp:TextBox></td>
                <td><asp:Label ID="Label4" runat="server" Text="Emanette"></asp:Label><asp:CheckBox ID="checkIsCompleted" runat="server" Checked="true"/></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="İşlemi Yapan Adı"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtUserfullname" runat="server"></asp:TextBox>
                </td>
                <td><asp:Label ID="Label6" runat="server" Text="Kayıt Tarihi Aralığı"></asp:Label></td>
                <td><asp:TextBox ID="txtBeginOn" runat="server" TextMode="Date"></asp:TextBox>-<asp:TextBox ID="txtScheduledEndDate" runat="server" TextMode="Date"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Filtrele" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
        <div>
            <asp:GridView ID="GridView1" runat="server" PageSize="25" AllowPaging="True" AllowCustomPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView_RowDataBound">
                <Columns>

                    <asp:BoundField DataField="Id" HeaderText="No" />
                    <asp:BoundField DataField="BeginOn" HeaderText="Verildiği Tarih" />
                    <asp:BoundField DataField="ScheduledEndDate" HeaderText="Geri Alıncağı Tarih" />
                    <asp:BoundField DataField="Title" HeaderText="Kitap Adı" />
                    <asp:BoundField DataField="MemberName" HeaderText="Üye Adı" />
                    <asp:BoundField DataField="UserName" HeaderText="Emanete Veren" />
                    <asp:CheckBoxField DataField="IsCompleted" HeaderText="Üyede" />
                    <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/Loan/TakeBack.aspx?Id={0}" Text="Geri Al" />
                </Columns>
            </asp:GridView>
        </div>
    </asp:Content>