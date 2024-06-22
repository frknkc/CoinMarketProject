using System;
using System.Web.UI;

namespace CoinMarketProject
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private CoinApiService _coinApiService = new CoinApiService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (Session["ApiKey"] != null)
                {
                    UpdateCryptoPrices();
                }
                else
                {
                    ShowErrorMessage("API anahtarı bulunamadı. Lütfen Hesap Ayarları'ndan API anahtarınızı güncelleyin.");
                    Response.Redirect("AccountSettings.aspx");
                }
            }
        }

        private void UpdateCryptoPrices()
        {
            if (Session["ApiKey"] != null)
            {
                string apiKey = Session["ApiKey"].ToString();

                try
                {
                    decimal btcCurrentPrice = _coinApiService.GetCurrentPrice("BTC", apiKey);
                    decimal ethCurrentPrice = _coinApiService.GetCurrentPrice("ETH", apiKey);
                    decimal usdtCurrentPrice = _coinApiService.GetCurrentPrice("USDT", apiKey);

                    DateTime yesterday = DateTime.Now.AddDays(-1).Date.AddHours(1);
                    decimal btcHistoricalPrice = _coinApiService.GetHistoricalPrice("BTC", apiKey, yesterday);
                    decimal ethHistoricalPrice = _coinApiService.GetHistoricalPrice("ETH", apiKey, yesterday);
                    decimal usdtHistoricalPrice = _coinApiService.GetHistoricalPrice("USDT", apiKey, yesterday);

                    BtcUsdtLabel.Text = btcCurrentPrice.ToString("F2");
                    EthUsdtLabel.Text = ethCurrentPrice.ToString("F2");
                    

                    BtcChangeLabel.Text = CalculatePercentageChange(btcHistoricalPrice, btcCurrentPrice).ToString("F2") + "%";
                    EthChangeLabel.Text = CalculatePercentageChange(ethHistoricalPrice, ethCurrentPrice).ToString("F2") + "%";
                    }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Fiyatlar güncellenirken hata oluştu: {ex.Message}");
                }
            }
            else
            {
                ShowErrorMessage("API anahtarı bulunamadı.");
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string coinName = SearchTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(coinName) && Session["ApiKey"] != null)
            {
                string apiKey = Session["ApiKey"].ToString();
                try
                {
                    decimal currentPrice = _coinApiService.GetCurrentPrice(coinName, apiKey);
                    SearchedCoinPriceLabel.Text = currentPrice.ToString("F2");
                    searchedCoinName.Text = coinName + "-USDT";
                    searchedCoin.Visible = true;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Coin aranırken hata oluştu: {ex.Message}");
                }
            }
        }

        private decimal CalculatePercentageChange(decimal oldPrice, decimal newPrice)
        {
            return ((newPrice - oldPrice) / oldPrice) * 100;
        }

        private void ShowErrorMessage(string message)
        {
            //ErrorMessageLiteral.Text = $"<div class='alert alert-danger'>{message}</div>";
        }
    }
}

        //private void LoadTopCoins()
        //{
        //    string apiKey = Session["ApiKey"].ToString();
        //    var topCoins = _coinApiService.GetTopCoins(apiKey);

        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("AssetId");
        //    dt.Columns.Add("PriceUsd");

        //    foreach (var coin in topCoins)
        //    {
        //        DataRow row = dt.NewRow();
        //        row["AssetId"] = coin.AssetId;
        //        row["PriceUsd"] = coin.PriceUsd.ToString("");
        //        dt.Rows.Add(row);
        //    }

        //    TopCoinsGridView.DataSource = dt;
        //    TopCoinsGridView.DataBind();
        //}

        //private void LoadUserPortfolio()
        //{
        //    int userId = (int)Session["UserId"];
        //    var portfolios = dataAccess.GetPortfolios(userId);
        //    UserPortfolioGridView.DataSource = portfolios;
        //    UserPortfolioGridView.DataBind();

        //    LoadPortfolioDetails(userId);
        //}

        //private void LoadPortfolioDetails(int userId)
        //{
        //    DataTable portfolioItems = dataAccess.GetPortfolioItems(userId);
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("CoinName");
        //    dt.Columns.Add("PurchasePrice");
        //    dt.Columns.Add("Quantity");
        //    dt.Columns.Add("CurrentPrice");
        //    dt.Columns.Add("ProfitLoss");
        //    dt.Columns.Add("ProfitLossPercentage");

        //    string apiKey = Session["ApiKey"].ToString();

        //    foreach (DataRow row in portfolioItems.Rows)
        //    {
        //        string coinName = row["CoinName"].ToString();
        //        decimal purchasePrice = Convert.ToDecimal(row["PurchasePrice"]);
        //        decimal quantity = Convert.ToDecimal(row["Quantity"]);
        //        decimal currentPrice = _coinApiService.GetCurrentPrice(coinName, apiKey);

        //        decimal totalPurchasePrice = purchasePrice * quantity;
        //        decimal totalCurrentPrice = currentPrice * quantity;
        //        decimal profitLoss = totalCurrentPrice - totalPurchasePrice;
        //        decimal profitLossPercentage = (profitLoss / totalPurchasePrice) * 100;

        //        DataRow newRow = dt.NewRow();
        //        newRow["CoinName"] = coinName;
        //        newRow["PurchasePrice"] = purchasePrice.ToString("F4");
        //        newRow["Quantity"] = quantity.ToString("F4");
        //        newRow["CurrentPrice"] = currentPrice.ToString("F4");
        //        newRow["ProfitLoss"] = profitLoss.ToString("F4");
        //        newRow["ProfitLossPercentage"] = profitLossPercentage.ToString("F4");

        //        dt.Rows.Add(newRow);
        //    }

        //    UserPortfolioGridView.DataSource = dt;
        //    UserPortfolioGridView.DataBind();
    

    
