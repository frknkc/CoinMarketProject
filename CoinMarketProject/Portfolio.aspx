<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Portfolio.aspx.cs" Inherits="CoinMarketProject.Portfolio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Portföyüm</h2>
        <div class="form-group">
            <label for="PortfolioNameTextBox">Portföy Adı:</label>
            <asp:TextBox ID="PortfolioNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button ID="AddPortfolioButton" runat="server" Text="Portföy Ekle" CssClass="btn button-primary" OnClick="AddPortfolioButton_Click" />

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
        <asp:Button ID="AddCoinButton" runat="server" Text="Coin Ekle" CssClass="btn button-primary" OnClick="AddCoinButton_Click" />
        <asp:Button ID="DeletePortfolioButton" runat="server" Text="Portföy Sil" CssClass="btn btn-danger" OnClick="DeletePortfolioButton_Click" />

        <h3 class="mt-5">Portföy Detayları</h3>

        <asp:Panel ID="Panel1" runat="server">
            <asp:Label ID="Label1" runat="server" Text="Label" Style="font-size: 18px; font-weight: bold; text-align: center; display: block; margin: 10px 0;"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="Label" Style="font-size: 18px; font-weight: bold; text-align: center; display: block; margin: 10px 0;"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="Label" Style="font-size: 18px; font-weight: bold; text-align: center; display: block; margin: 10px 0;"></asp:Label>
        </asp:Panel>

        <asp:GridView ID="PortfolioGridView" runat="server" CssClass="table table-striped" OnRowCommand="PortfolioGridView_RowCommand" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="CoinName" HeaderText="Coin Adı" />
                <asp:BoundField DataField="PurchasePrice" HeaderText="Alış Fiyatı" DataFormatString="{0:F4}" />
                <asp:BoundField DataField="Quantity" HeaderText="Miktar" DataFormatString="{0:F4}" />
                <asp:BoundField DataField="CurrentPrice" HeaderText="Anlık Fiyat" DataFormatString="{0:F4}" />
                <asp:BoundField DataField="ProfitLoss" HeaderText="Kâr/Zarar" DataFormatString="{0:F4}" />
                <asp:BoundField DataField="ProfitLossPercentage" HeaderText="Kâr/Zarar (%)" DataFormatString="{0:F4}" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="DeleteButton" runat="server" CommandName="DeleteCoin" CommandArgument='<%# Eval("CoinName") %>' Text="Sil" CssClass="btn btn-danger" />
                        <asp:Button ID="DetailsButton" runat="server" CommandName="ViewDetails" CommandArgument='<%# Eval("CoinName") %>' Text="Detaylar" CssClass="btn btn-info" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #ffffe6; /* Sarı tonu */
        }
        .container {
            background-color: #ffffff; /* Beyaz */
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .form-group {
            margin-bottom: 20px;
        }
        h2, h3 {
            color: #ff6600; /* Turuncu tonu */
        }
    </style>

    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
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
