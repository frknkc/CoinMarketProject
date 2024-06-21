<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CoinMarketProject.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Dashboard</h2>
        <h3>Son 24 Saatte En Çok Artış Gösteren Coinler</h3>
        <asp:GridView ID="TopCoinsGridView" runat="server" CssClass="table table-striped mb-4" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="AssetId" HeaderText="Coin Adı" />
                <asp:BoundField DataField="PriceUsd" HeaderText="Fiyat (USD)" DataFormatString="{0:F4}" />
            </Columns>
        </asp:GridView>

        <h3>Portföyüm</h3>
        <asp:GridView ID="UserPortfolioGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
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
</asp:Content>
