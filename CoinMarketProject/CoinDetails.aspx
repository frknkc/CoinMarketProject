<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CoinDetails.aspx.cs" Inherits="CoinMarketProject.CoinDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Coin Detayları</h2>
        <div class="card">
            <div class="card-body">
                <h5 class="card-title" id="CoinNameLabel" runat="server"></h5>
                <p class="card-text">Mevcut Fiyat: <span id="CurrentPriceLabel" runat="server"></span></p>
            </div>
        </div>
    </div>
</asp:Content>