using System;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
        protected void DetailsButton_Click(object sender, EventArgs e)
        {
            string coinName = SearchTextBox.Text.ToUpper();
            Response.Redirect($"/CoinDetails.aspx?coinId={coinName}&years=1");
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
                    decimal xrpCurrentPrice = _coinApiService.GetCurrentPrice("XRP", apiKey);
                    decimal usdtCurrentPrice = _coinApiService.GetCurrentPrice("USDT", apiKey);
                    decimal bnbCurrentPrice = _coinApiService.GetCurrentPrice("BNB", apiKey);
                    decimal solCurrentPrice = _coinApiService.GetCurrentPrice("SOL", apiKey);
                    decimal dogeCurrentPrice = _coinApiService.GetCurrentPrice("DOGE", apiKey);
                    decimal trxCurrentPrice = _coinApiService.GetCurrentPrice("TRX", apiKey);
                    decimal adaCurrentPrice = _coinApiService.GetCurrentPrice("ADA", apiKey);
                    decimal dotCurrentPrice = _coinApiService.GetCurrentPrice("DOT", apiKey);

                    DateTime yesterday = DateTime.Now.AddDays(-1).Date.AddHours(1);
                    decimal btcHistoricalPrice = _coinApiService.GetHistoricalPrice("BTC", apiKey, yesterday);
                    decimal ethHistoricalPrice = _coinApiService.GetHistoricalPrice("ETH", apiKey, yesterday);
                    decimal xrpHistoricalPrice = _coinApiService.GetHistoricalPrice("XRP", apiKey, yesterday);
                    decimal usdtHistoricalPrice = _coinApiService.GetHistoricalPrice("USDT", apiKey, yesterday);
                    decimal bnbHistoricalPrice = _coinApiService.GetHistoricalPrice("BNB", apiKey, yesterday);
                    decimal solHistoricalPrice = _coinApiService.GetHistoricalPrice("SOL", apiKey, yesterday);
                    decimal dogeHistoricalPrice = _coinApiService.GetHistoricalPrice("DOGE", apiKey, yesterday);
                    decimal trxHistoricalPrice = _coinApiService.GetHistoricalPrice("TRX", apiKey, yesterday);
                    decimal adaHistoricalPrice = _coinApiService.GetHistoricalPrice("ADA", apiKey, yesterday);
                    decimal dotHistoricalPrice = _coinApiService.GetHistoricalPrice("DOT", apiKey, yesterday);

                    BtcUsdtLabel.Text = btcCurrentPrice.ToString("F2");
                    EthUsdtLabel.Text = ethCurrentPrice.ToString("F2");
                    XrpUsdtLabel.Text = xrpCurrentPrice.ToString("F2");
                    UsdtUsdtLabel.Text = usdtCurrentPrice.ToString("F2");
                    BnbUsdtLabel.Text = bnbCurrentPrice.ToString("F2");
                    SolUsdtLabel.Text = solCurrentPrice.ToString("F2");
                    DogeUsdtLabel.Text = dogeCurrentPrice.ToString("F2");
                    TrxUsdtLabel.Text = trxCurrentPrice.ToString("F2");
                    AdaUsdtLabel.Text = adaCurrentPrice.ToString("F2");
                    DotUsdtLabel.Text = dotCurrentPrice.ToString("F2");

                    SetChangeLabel(BtcChangeLabel, btcHistoricalPrice, btcCurrentPrice);
                    SetChangeLabel(EthChangeLabel, ethHistoricalPrice, ethCurrentPrice);
                    SetChangeLabel(XrpChangeLabel, xrpHistoricalPrice, xrpCurrentPrice);
                    SetChangeLabel(UsdtChangeLabel, usdtHistoricalPrice, usdtCurrentPrice);
                    SetChangeLabel(BnbChangeLabel, bnbHistoricalPrice, bnbCurrentPrice);
                    SetChangeLabel(SolChangeLabel, solHistoricalPrice, solCurrentPrice);
                    SetChangeLabel(DogeChangeLabel, dogeHistoricalPrice, dogeCurrentPrice);
                    SetChangeLabel(TrxChangeLabel, trxHistoricalPrice, trxCurrentPrice);
                    SetChangeLabel(AdaChangeLabel, adaHistoricalPrice, adaCurrentPrice);
                    SetChangeLabel(DotChangeLabel, dotHistoricalPrice, dotCurrentPrice);
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

        private void SetChangeLabel(System.Web.UI.WebControls.Label changeLabel, decimal oldPrice, decimal newPrice)
        {
            decimal change = CalculatePercentageChange(oldPrice, newPrice);
            if (change >= 0)
            {
                changeLabel.CssClass = "change-positive";
                changeLabel.Text = "+" + change.ToString("F2") + "%";
            }
            else
            {
                changeLabel.CssClass = "change-negative";
                changeLabel.Text = change.ToString("F2") + "%";
            }
        }

        private decimal CalculatePercentageChange(decimal oldPrice, decimal newPrice)
        {
            return ((newPrice - oldPrice) / oldPrice) * 100;
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
                    searchedCoinName.Text = coinName.ToUpper() + "- USDT";
                    searchedCoin.Visible = true;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Coin aranırken hata oluştu: {ex.Message}");
                }
            }
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
    

    
