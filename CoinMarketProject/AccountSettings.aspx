<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountSettings.aspx.cs" Inherits="CoinMarketProject.AccountSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Hesap Ayarları</h2>
        <div class="form-group">
            <label for="ApiKeyTextBox">API Key:</label>
            <asp:TextBox ID="ApiKeyTextBox" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button ID="SaveSettingsButton" runat="server" Text="Kaydet" CssClass="btn btn-primary" OnClick="SaveSettingsButton_Click" />
        <asp:Button ID="TestApiKeyButton" runat="server" Text="API Key'i Test Et" CssClass="btn btn-secondary ml-2" OnClick="TestApiKeyButton_Click" />
        <asp:Label ID="SuccessLabel" runat="server" CssClass="text-success mt-3"></asp:Label>
        <asp:Label ID="ErrorLabel" runat="server" CssClass="text-danger mt-3"></asp:Label>
    </div>
</asp:Content>