<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TR.Edu.Ankara.EUB201.Finalwork.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <h1>Kütüphane yönetim uygulamasına hoşgeldiniz. kullanıcı adı ve şifreniz ile giriş yaparak uygulamayı kullanmaya başlayabilirsiniz.</h1>
        </center>
        <div style="width:250px;height:100px;margin-top:45%:150px;margin:auto">
            <asp:Login ID="Login1" runat="server" FailureText="Kullanıcı adı yada şifresi yanlış" LoginButtonText="Giriş" OnAuthenticate="Login1_Authenticate" PasswordLabelText="Şifre:" PasswordRequiredErrorMessage="Şifre Giriniz" RememberMeText="Beni hatırla" TitleText="Giriş Yap" UserNameLabelText="E-Posta:">
            </asp:Login>
        </div>
    </form>
</body>
</html>
