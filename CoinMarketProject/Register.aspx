<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CoinMarketProject.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>Kayıt Ol</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body, html {
            height: 100%;
            margin: 0;
        }
        .bg {
            background-image: url('https://kriptokoin.com/wp-content/uploads/2024/04/6dba9019480a9829bdb56c9463e6878d-1200x633.jpg');
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
            height: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .form-container {
            background: rgba(255, 255, 255, 0.9);
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.1);
        }
    </style>
</head>
<body>
    <div class="bg">
        <form id="form1" runat="server" class="form-container">
            <div class="container">
                <h2 class="mb-4">Kayıt Ol</h2>
                <div class="form-group">
                    <label for="UsernameTextBox">Kullanıcı Adı:</label>
                    <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="PasswordTextBox">Parola:</label>
                    <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="ApiKeyTextBox">API Key:</label>
                    <asp:TextBox ID="ApiKeyTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="EmailTextBox">Email:</label>
                    <asp:TextBox ID="EmailTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="FullNameTextBox">Tam Adı:</label>
                    <asp:TextBox ID="FullNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:Button ID="RegisterButton" runat="server" Text="Kayıt Ol" CssClass="btn btn-primary" OnClick="RegisterButton_Click" />
                <asp:Label ID="ErrorLabel" runat="server" CssClass="text-danger mt-3"></asp:Label>
            </div>
        </form>
    </div>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
