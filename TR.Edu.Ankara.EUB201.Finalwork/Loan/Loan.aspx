<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Loan.aspx.cs" Inherits="TR.Edu.Ankara.EUB201.Finalwork.Loan.Loan" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:HiddenField ID="recordId" runat="server" />
        <div>
            <table>
                <tr>
                    <td><asp:Label ID="Label1" runat="server" Text="Kitap"></asp:Label></td>
                    <td><asp:DropDownList ID="ddlBooks" runat="server" DataTextField="Name" DataValueField="Id" Width="75%"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label2" runat="server" Text="Üye"></asp:Label></td>
                    <td><asp:DropDownList ID="ddlMembers" runat="server" DataTextField="Name" DataValueField="Id" Width="75%"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label3" runat="server" Text="Ödünç Verildiği Tarih"></asp:Label></td>
                    <td><asp:TextBox ID="txtBeginDate" runat="server" TextMode="Date"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label4" runat="server" Text="Teslim Alınacağı Tarihi(Öngörülen)"></asp:Label></td>
                    <td><asp:TextBox ID="txtEndDate" runat="server" TextMode="Date"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="errorText" Visible="false" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnSave" runat="server" Text="Kaydet" OnClick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>