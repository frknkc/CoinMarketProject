<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Portfolio.aspx.cs" Inherits="CoinMarketProject.Portfolio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Portföyüm</h2>
        <div class="form-group">
            <label for="PortfolioNameTextBox">Portföy Adı:</label>
            <asp:TextBox ID="PortfolioNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button ID="AddPortfolioButton" runat="server" Text="Portföy Ekle" CssClass="btn btn-primary" OnClick="AddPortfolioButton_Click" />

        <h3 class="mt-5">Portföyler</h3>
        <div class="form-group">
            <asp:DropDownList ID="PortfolioDropDownList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="PortfolioDropDownList_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="CoinDropDownList">Coin Adı:</label>
            <asp:DropDownList ID="CoinDropDownList" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="PurchasePriceTextBox">Alış Fiyatı:</label>
            <asp:TextBox ID="PurchasePriceTextBox" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="QuantityTextBox">Miktar:</label>
            <asp:TextBox ID="QuantityTextBox" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button ID="AddCoinButton" runat="server" Text="Coin Ekle" CssClass="btn btn-primary" OnClick="AddCoinButton_Click" />

        <h3 class="mt-5">Portföy Detayları</h3>
        <asp:GridView ID="PortfolioGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="CoinName" HeaderText="Coin Adı" />
                <asp:BoundField DataField="PurchasePrice" HeaderText="Alış Fiyatı" DataFormatString="{0:F4}" />
                <asp:BoundField DataField="Quantity" HeaderText="Miktar" DataFormatString="{0:F4}" />
                <asp:BoundField DataField="CurrentPrice" HeaderText="Anlık Fiyat" DataFormatString="{0:F4}" />
                <asp:BoundField DataField="ProfitLoss" HeaderText="Kâr/Zarar" DataFormatString="{0:F4}" />
                <asp:BoundField DataField="ProfitLossPercentage" HeaderText="Kâr/Zarar (%)" DataFormatString="{0:F4}" />
            </Columns>
        </asp:GridView>
    </div>
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= CoinDropDownList.ClientID %>').select2({
                placeholder: "Coin seçin",
                allowClear: true
            });
        });
    </script>
</asp:Content>
