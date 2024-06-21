using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;

namespace CoinMarketProject
{
    public partial class Portfolio : System.Web.UI.Page
    {
        private DataAccess dataAccess = new DataAccess();
        private CoinApiService coinApiService = new CoinApiService();

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
                    LoadPortfolios();
                    LoadCoins();
                }
                catch (Exception ex)
                {
                    var master = (Site)this.Master;
                    master.ShowErrorMessage($"API anahtarı geçersiz veya eksik. Lütfen Hesap Ayarları'ndan API anahtarınızı güncelleyin. Hata: {ex.Message}");
                    Response.Redirect("AccountSettings.aspx");
                }
            }
        }

        protected void AddPortfolioButton_Click(object sender, EventArgs e)
        {
            int userId = (int)Session["UserId"];
            string portfolioName = PortfolioNameTextBox.Text;

            int portfolioId = dataAccess.AddPortfolio(userId, portfolioName);
            LoadPortfolios();
        }

        protected void AddCoinButton_Click(object sender, EventArgs e)
        {
            int portfolioId = Convert.ToInt32(PortfolioDropDownList.SelectedValue);
            string coinName = CoinDropDownList.SelectedValue;
            decimal purchasePrice = Convert.ToDecimal(PurchasePriceTextBox.Text);
            decimal quantity = Convert.ToDecimal(QuantityTextBox.Text);

            dataAccess.AddPortfolioItem(portfolioId, coinName, purchasePrice, quantity);
            LoadPortfolioItems(portfolioId);
        }

        private void LoadPortfolios()
        {
            int userId = (int)Session["UserId"];
            DataTable portfolios = dataAccess.GetPortfolios(userId);
            PortfolioDropDownList.DataSource = portfolios;
            PortfolioDropDownList.DataTextField = "PortfolioName";
            PortfolioDropDownList.DataValueField = "PortfolioId";
            PortfolioDropDownList.DataBind();
            LoadPortfolioItems(Convert.ToInt32(PortfolioDropDownList.SelectedValue));
        }

        private void LoadPortfolioItems(int portfolioId)
        {
            DataTable portfolioItems = dataAccess.GetPortfolioItems(portfolioId);
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
                decimal currentPrice = coinApiService.GetCurrentPrice(coinName, apiKey);

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

            PortfolioGridView.DataSource = dt;
            PortfolioGridView.DataBind();
        }

        protected void PortfolioDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int portfolioId = Convert.ToInt32(PortfolioDropDownList.SelectedValue);
            LoadPortfolioItems(portfolioId);
        }

        private void LoadCoins()
        {
            string apiKey = Session["ApiKey"].ToString();
            List<MyCoin> coins = coinApiService.GetAllCoins(apiKey);

            CoinDropDownList.DataSource = coins;
            CoinDropDownList.DataTextField = "name";
            CoinDropDownList.DataValueField = "asset_id";
            CoinDropDownList.DataBind();
        }
    }
}
