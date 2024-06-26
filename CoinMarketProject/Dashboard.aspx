<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CoinMarketProject.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="bg container market-table-container">
            <div class="row text-center">
                <div class="col-md-2">
                    <div class="card mb-4 shadow-sm">
                        <div class="card-body">
                            <img src="https://s2.coinmarketcap.com/static/img/coins/64x64/1.png" alt="BTC" class="mb-3">
                            <h5 class="card-title">BTC</h5>
                            <p class="card-text"><asp:Label ID="BtcUsdtLabel" runat="server"></asp:Label> USDT</p>
                            <asp:Label ID="BtcChangeLabel" runat="server" CssClass="change"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card mb-4 shadow-sm">
                        <div class="card-body">
                            <img src="https://s2.coinmarketcap.com/static/img/coins/64x64/1027.png" alt="ETH" class="mb-3">
                            <h5 class="card-title">ETH</h5>
                            <p class="card-text"><asp:Label ID="EthUsdtLabel" runat="server"></asp:Label> USDT</p>
                            <asp:Label ID="EthChangeLabel" runat="server" CssClass="change"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card mb-4 shadow-sm">
                        <div class="card-body">
                            <img src="https://s2.coinmarketcap.com/static/img/coins/64x64/52.png" alt="XRP" class="mb-3">
                            <h5 class="card-title">XRP</h5>
                            <p class="card-text"><asp:Label ID="XrpUsdtLabel" runat="server"></asp:Label> USDT</p>
                            <asp:Label ID="XrpChangeLabel" runat="server" CssClass="change"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card mb-4 shadow-sm">
                        <div class="card-body">
                            <img src="https://s2.coinmarketcap.com/static/img/coins/64x64/825.png" alt="USDT" class="mb-3">
                            <h5 class="card-title">USDT</h5>
                            <p class="card-text"><asp:Label ID="UsdtUsdtLabel" runat="server"></asp:Label> USDT</p>
                            <asp:Label ID="UsdtChangeLabel" runat="server" CssClass="change"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card mb-4 shadow-sm">
                        <div class="card-body">
                            <img src="https://s2.coinmarketcap.com/static/img/coins/64x64/1839.png" alt="BNB" class="mb-3">
                            <h5 class="card-title">BNB-USDT</h5>
                            <p class="card-text"><asp:Label ID="BnbUsdtLabel" runat="server"></asp:Label> USDT</p>
                            <asp:Label ID="BnbChangeLabel" runat="server" CssClass="change"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row text-center">
                <div class="col-md-2">
                    <div class="card mb-4 shadow-sm">
                        <div class="card-body">
                            <img src="https://s2.coinmarketcap.com/static/img/coins/64x64/5426.png" alt="SOL" class="mb-3">
                            <h5 class="card-title">SOL</h5>
                            <p class="card-text"><asp:Label ID="SolUsdtLabel" runat="server"></asp:Label> USDT</p>
                            <asp:Label ID="SolChangeLabel" runat="server" CssClass="change"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card mb-4 shadow-sm">
                        <div class="card-body">
                            <img src="https://s2.coinmarketcap.com/static/img/coins/64x64/74.png" alt="DOGE" class="mb-3">
                            <h5 class="card-title">DOGE-USDT</h5>
                            <p class="card-text"><asp:Label ID="DogeUsdtLabel" runat="server"></asp:Label> USDT</p>
                            <asp:Label ID="DogeChangeLabel" runat="server" CssClass="change"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card mb-4 shadow-sm">
                        <div class="card-body">
                            <img src="https://s2.coinmarketcap.com/static/img/coins/64x64/1958.png" alt="TRX" class="mb-3">
                            <h5 class="card-title">TRX</h5>
                            <p class="card-text"><asp:Label ID="TrxUsdtLabel" runat="server"></asp:Label> USDT</p>
                            <asp:Label ID="TrxChangeLabel" runat="server" CssClass="change"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card mb-4 shadow-sm">
                        <div class="card-body">
                            <img src="https://s2.coinmarketcap.com/static/img/coins/64x64/1831.png" alt="ADA" class="mb-3">
                            <h5 class="card-title">ADA</h5>
                            <p class="card-text"><asp:Label ID="AdaUsdtLabel" runat="server"></asp:Label> USDT</p>
                            <asp:Label ID="AdaChangeLabel" runat="server" CssClass="change"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card mb-4 shadow-sm">
                        <div class="card-body">
                            <img src="https://s2.coinmarketcap.com/static/img/coins/64x64/2010.png" alt="DOT" class="mb-3">
                            <h5 class="card-title">DOT</h5>
                            <p class="card-text"><asp:Label ID="DotUsdtLabel" runat="server"></asp:Label> USDT</p>
                            <asp:Label ID="DotChangeLabel" runat="server" CssClass="change"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="search-container mt-5">
    <h2>Coin Ara</h2>
    <asp:TextBox ID="SearchTextBox" runat="server" CssClass="form-control" placeholder="Coin ismi girin..."></asp:TextBox>
    <asp:Button ID="SearchButton" runat="server" Text="Ara" CssClass="btn button-primary mt-2" OnClick="SearchButton_Click" />
    <div id="searchResult" class="mt-3">
        <div class="card" id="searchedCoin" runat="server" visible="false">
            <div class="card-body">
                <h5 class="card-title"><asp:Label ID="searchedCoinName" runat="server"></asp:Label></h5>
                <p class="card-text">
                    Fiyat: <asp:Label ID="SearchedCoinPriceLabel" runat="server"></asp:Label> USDT
                </p>
                <asp:Button ID="DetailsButton" runat="server" Text="Detaylar" CssClass="btn btn-info" OnClick="DetailsButton_Click" />
            </div>
        </div>
    </div>
</div>

<style>
    .card {
        border: 1px solid #ddd;
        border-radius: 5px;
        box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        transition: 0.3s;
    }

    .card:hover {
        box-shadow: 0 8px 16px rgba(0,0,0,0.2);
    }

    .card-body {
        padding: 20px;
    }

    .card-title {
        font-size: 1.5rem;
        margin-bottom: 15px;
    }

    .card-text {
        font-size: 1.2rem;
        margin-bottom: 15px;
    }

    .btn {
        width: 100%;
    }
    .change-positive {
        color: green;
    }
    .change-negative {
        color: red;
    }
</style>
    </div>

</asp:Content>
