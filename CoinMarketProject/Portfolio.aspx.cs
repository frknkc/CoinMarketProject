using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.Xml.Schema;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

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

            var existingCoin = dataAccess.GetPortfolioItem(portfolioId, coinName);
            if (existingCoin != null)
            {
                decimal newQuantity = existingCoin.Quantity + quantity;
                decimal newPurchasePrice = ((existingCoin.Quantity * existingCoin.PurchasePrice) + (quantity * purchasePrice)) / newQuantity;
                dataAccess.UpdatePortfolioItem(portfolioId, coinName, newPurchasePrice, newQuantity);
            }
            else
            {
                dataAccess.AddPortfolioItem(portfolioId, coinName, purchasePrice, quantity);
            }

            QuantityTextBox.Text = string.Empty;
            PurchasePriceTextBox.Text = string.Empty;
            CoinDropDownList.SelectedIndex = 0;
            LoadPortfolioItems(portfolioId);
            //int portfolioId = Convert.ToInt32(PortfolioDropDownList.SelectedValue);
            //string coinName = CoinDropDownList.SelectedValue;
            //decimal purchasePrice = Convert.ToDecimal(PurchasePriceTextBox.Text);
            //decimal quantity = Convert.ToDecimal(QuantityTextBox.Text);
            //QuantityTextBox.Text = string.Empty;
            //PurchasePriceTextBox.Text = string.Empty;
            //CoinDropDownList.SelectedIndex = 0;
            //dataAccess.AddPortfolioItem(portfolioId, coinName, purchasePrice, quantity);
            //LoadPortfolioItems(portfolioId);
        }
        
        protected void DeletePortfolioButton_Click(object sender, EventArgs e)
        {
            int portfolioId = Convert.ToInt32(PortfolioDropDownList.SelectedValue);
            dataAccess.DeletePortfolio(portfolioId);
            LoadPortfolios(); // Reload the portfolios into the dropdown
            PortfolioGridView.DataSource = null; // Clear the portfolio details grid
            PortfolioGridView.DataBind();
            Label1.Text = string.Empty;
            Label2.Text = string.Empty;
            Label3.Text = string.Empty;
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
            decimal totalkar = 0;
            decimal totalsuan = 0;
            decimal totalyatir = 0;
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
                totalkar += profitLoss;
                totalsuan += totalCurrentPrice;
                totalyatir += totalPurchasePrice;
                
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

            Label1.Text = "Toplam Yatırım: " + totalyatir.ToString("F4")+ " USD"; 
            Label2.Text = "Toplam Şuandaki Değer: "+ totalsuan.ToString("F4") + " USD";
            Label3.Text = "Toplam Kâr/Zarar: " + totalkar.ToString("F4") + " USD";
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


protected void PortfolioGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteCoin" || e.CommandName == "ViewDetails")
            {
                int portfolioId = Convert.ToInt32(PortfolioDropDownList.SelectedValue);
                string coinName = e.CommandArgument.ToString();

                if (e.CommandName == "DeleteCoin")
                {
                    dataAccess.DeletePortfolioItem(portfolioId, coinName);
                    LoadPortfolioItems(portfolioId);
                }
                else if (e.CommandName == "ViewDetails")
                {
                    Response.Redirect($"/CoinDetails.aspx?coinId={coinName}&years=1");
                }
            }
        }

        
    }


}
