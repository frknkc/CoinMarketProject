<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CoinMarketProject.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>Giriş Yap</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #000000; /* Arka plan rengini siyah yaptık */
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
            position: absolute;
            transform: translate(-50%, -50%);
            top: 50%;
            left: 50%;
            width: 400px;
            height: 520px;
            padding: 50px 35px;
            background-color: rgba(255, 255, 255, 0.13);
            border-radius: 10px;
            backdrop-filter: blur(10px);
            border: 2px solid rgba(255, 255, 255, 0.1);
            box-shadow: 0 0 40px rgba(8, 7, 16, 0.6);
        }
        .login-container * {
            font-family: 'Poppins', sans-serif;
            color: #ffffff;
            letter-spacing: 0.5px;
            outline: none;
            border: none;
        }
        .login-container h2 {
            font-size: 32px;
            font-weight: 500;
            line-height: 42px;
            text-align: center;
        }
        .login-container label {
            display: block;
            margin-top: 30px;
            font-size: 16px;
            font-weight: 500;
        }
        .login-container input {
            display: block;
            height: 50px;
            width: 100%;
            background-color: rgba(255, 255, 255, 0.07);
            border-radius: 3px;
            padding: 0 10px;
            margin-top: 8px;
            font-size: 14px;
            font-weight: 300;
        }
        .login-container ::placeholder {
            color: #e5e5e5;
        }
        .login-container button {
            margin-top: 50px;
            width: 100%;
            background-color: #ffffff;
            color: #080710;
            padding: 15px 0;
            font-size: 18px;
            font-weight: 600;
            border-radius: 5px;
            cursor: pointer;
        }
        .background {
            width: 430px;
            height: 520px;
            position: absolute;
            transform: translate(-50%, -50%);
            left: 50%;
            top: 50%;
        }
        .background .shape {
            height: 200px;
            width: 200px;
            position: absolute;
            border-radius: 50%;
        }
        .background .shape:first-child {
            background: linear-gradient(#1845ad, #23a2f6);
            left: -80px;
            top: -80px;
        }
        .background .shape:last-child {
            background: linear-gradient(to right, #ff512f, #f09819);
            right: -30px;
            bottom: -80px;
        }
    </style>
</head>
<body>
    <div class="background">
        <div class="shape"></div>
        <div class="shape"></div>
    </div>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Giriş Yap</h2>
            <div class="form-group">
                <label for="UsernameTextBox">Kullanıcı Adı:</label>
                <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="form-control" placeholder="Kullanıcı Adı"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="PasswordTextBox">Parola:</label>
                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" CssClass="form-control" placeholder="Parola"></asp:TextBox>
            </div>
            <asp:Button ID="LoginButton" runat="server" Text="Giriş Yap" CssClass="btn btn-primary btn-block" OnClick="LoginButton_Click" />
            <asp:Button ID="RegisterPageButton" runat="server" Text="Kayıt Ol" CssClass="btn btn-primary btn-block" OnClick="RegisterPageButton_Click" />
            <asp:Label ID="ErrorLabel" runat="server" CssClass="text-danger mt-3"></asp:Label>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>