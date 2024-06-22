<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CoinMarketProject.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="bg container market-table-container">
            <div class="market-item">
                <img src="https://s2.coinmarketcap.com/static/img/coins/64x64/1.png" alt="BTC">
                <div class="info">
                    <h5>BTC-USDT</h5>
                    <div class="price"><asp:Label ID="BtcUsdtLabel" runat="server"></asp:Label> USDT</div>
                </div>
                <asp:Label ID="BtcChangeLabel" runat="server" CssClass="change"></asp:Label>
            </div>
            <div class="market-item">
                <img src="https://s2.coinmarketcap.com/static/img/coins/64x64/1027.png" alt="ETH">
                <div class="info">
                    <h5>ETH-USDT</h5>
                    <div class="price"><asp:Label ID="EthUsdtLabel" runat="server"></asp:Label> USDT</div>
                </div>
                <asp:Label ID="EthChangeLabel" runat="server" CssClass="change"></asp:Label>
            </div>
        </div>
        <div class="search-container mt-5">
            <h2>Coin Ara</h2>
            <asp:TextBox ID="SearchTextBox" runat="server" CssClass="form-control" placeholder="Coin ismi girin..."></asp:TextBox>
            <asp:Button ID="SearchButton" runat="server" Text="Ara" CssClass="btn btn-primary mt-2" OnClick="SearchButton_Click" />
            <div id="searchResult" class="mt-3">
                <div class="market-item" id="searchedCoin" runat="server" visible="false">
                    <div class="info">
                        <asp:Label ID="searchedCoinName" runat="server"></asp:Label>
                        <div class="price"><asp:Label ID="SearchedCoinPriceLabel" runat="server"></asp:Label> USDT</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

 <%-- <h2 class="mb-4">Dashboard</h2>
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
  </asp:GridView>--%>