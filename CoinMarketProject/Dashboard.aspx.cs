using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace CoinMarketProject
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private CoinApiService _coinApiService = new CoinApiService();
        private DataAccess dataAccess = new DataAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                try
                {
                    LoadTopCoins();
                    LoadUserPortfolio();
                }
                catch (Exception ex)
                {
                    var master = (Site)this.Master;
                    master.ShowErrorMessage($"API anahtarı geçersiz veya eksik. Lütfen Hesap Ayarları'ndan API anahtarınızı güncelleyin. Hata: {ex.Message}");
                    Response.Redirect("AccountSettings.aspx");
                }
            }
        }

        private void LoadTopCoins()
        {
            string apiKey = Session["ApiKey"].ToString();
            var topCoins = _coinApiService.GetTopCoins(apiKey);

            DataTable dt = new DataTable();
            dt.Columns.Add("AssetId");
            dt.Columns.Add("PriceUsd");

            foreach (var coin in topCoins)
            {
                DataRow row = dt.NewRow();
                row["AssetId"] = coin.AssetId;
                row["PriceUsd"] = coin.PriceUsd.ToString("");
                dt.Rows.Add(row);
            }

            TopCoinsGridView.DataSource = dt;
            TopCoinsGridView.DataBind();
        }

        private void LoadUserPortfolio()
        {
            int userId = (int)Session["UserId"];
            var portfolios = dataAccess.GetPortfolios(userId);
            UserPortfolioGridView.DataSource = portfolios;
            UserPortfolioGridView.DataBind();

            LoadPortfolioDetails(userId);
        }

        private void LoadPortfolioDetails(int userId)
        {
            DataTable portfolioItems = dataAccess.GetPortfolioItems(userId);
            DataTable dt = new DataTable();
            dt.Columns.Add("CoinName");
            dt.Columns.Add("PurchasePrice");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("CurrentPrice");
            dt.Columns.Add("ProfitLoss");
            dt.Columns.Add("ProfitLossPercentage");

            string apiKey = Session["ApiKey"].ToString();

            foreach (DataRow row in portfolioItems.Rows)
            {
                string coinName = row["CoinName"].ToString();
                decimal purchasePrice = Convert.ToDecimal(row["PurchasePrice"]);
                decimal quantity = Convert.ToDecimal(row["Quantity"]);
                decimal currentPrice = _coinApiService.GetCurrentPrice(coinName, apiKey);

                decimal totalPurchasePrice = purchasePrice * quantity;
                decimal totalCurrentPrice = currentPrice * quantity;
                decimal profitLoss = totalCurrentPrice - totalPurchasePrice;
                decimal profitLossPercentage = (profitLoss / totalPurchasePrice) * 100;

                DataRow newRow = dt.NewRow();
                newRow["CoinName"] = coinName;
                newRow["PurchasePrice"] = purchasePrice.ToString("F4");
                newRow["Quantity"] = quantity.ToString("F4");
                newRow["CurrentPrice"] = currentPrice.ToString("F4");
                newRow["ProfitLoss"] = profitLoss.ToString("F4");
                newRow["ProfitLossPercentage"] = profitLossPercentage.ToString("F4");

                dt.Rows.Add(newRow);
            }

            UserPortfolioGridView.DataSource = dt;
            UserPortfolioGridView.DataBind();
        }
    }
}
