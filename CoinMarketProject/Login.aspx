<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CoinMarketProject.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>Giriş Yap</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
        }
        .market-table-container {
            margin-top: 50px;
            width: 60%;
        }
        .market-item {
            display: flex;
            align-items: center;
            margin-bottom: 15px;
            padding: 15px;
            border: 1px solid #dee2e6;
            border-radius: 8px;
            background-color: #ffffff;
        }
        .market-item img {
            width: 50px;
            height: 50px;
            margin-right: 15px;
        }
        .market-item .info {
            flex: 1;
        }
        .market-item .info h5 {
            margin: 0;
            font-size: 1.25rem;
        }
        .market-item .info .price {
            font-size: 1rem;
            color: #495057;
        }
        .market-item .change {
            font-size: 1rem;
            color: #28a745;
        }
        .market-item .change.negative {
            color: #dc3545;
        }
        .login-container {
            position: fixed;
            right: 0;
            top: 0;
            height: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
            width: 350px;
            padding: 20px;
            background-color: #ffffff;
            box-shadow: -3px 0 5px rgba(0, 0, 0, 0.1);
        }
        .login-form {
            width: 100%;
        }
        
}
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="login-container">
            <div class="login-form">
                <h2 class="mb-4">Giriş Yap</h2>
                <div class="form-group">
                    <label for="UsernameTextBox">Kullanıcı Adı:</label>
                    <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="PasswordTextBox">Parola:</label>
                    <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:Button ID="LoginButton" runat="server" Text="Giriş Yap" CssClass="btn btn-primary btn-block" OnClick="LoginButton_Click" />
                <asp:Label ID="ErrorLabel" runat="server" CssClass="text-danger mt-3"></asp:Label>
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
