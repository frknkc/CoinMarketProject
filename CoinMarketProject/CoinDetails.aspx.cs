using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;

namespace CoinMarketProject
{
    public partial class CoinDetails : System.Web.UI.Page
    {
        private DataAccess dataAccess = new DataAccess();
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
                string coinId = Request.QueryString["coinId"];
                if (!string.IsNullOrEmpty(Request.QueryString["years"]))
                {
                    int years = 1;
                    if (int.TryParse(Request.QueryString["years"], out years))
                    {
                        LoadCoinDetails(coinId);
                        LoadHistoricalPrices(coinId, years);
                    }
                }
            }
        }

        private void LoadCoinDetails(string coinId)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            DataTable coin_logs = dataAccess.GetCoinById(coinId, UserId);

            string apiKey = Session["ApiKey"].ToString();
            //var coinDetails = _coinApiService.GetCoinData<MyCoinDetails>($"v1/assets/{coinId}", apiKey);
            DataTable dt = new DataTable();
            dt.Columns.Add("CoinName");
            dt.Columns.Add("PurchasePrice");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("CurrentPrice");
            dt.Columns.Add("ProfitLoss");
            dt.Columns.Add("ProfitLossPercentage");

            foreach (DataRow row in coin_logs.Rows)
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

            CoinLogsView.DataSource = dt;
            CoinLogsView.DataBind();
        }

        private void LoadHistoricalPrices(string coinId, int years)
        {
            string apiKey = Session["ApiKey"].ToString();
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddYears(-years);

            CoinCodeHiddenField.Value = coinId;
            var historicalPrices = _coinApiService.GetHistoricalPrices(coinId, apiKey, startDate, endDate);

            var historicalPricesJson = JsonConvert.SerializeObject(historicalPrices);

            // JSON verilerini front-end'de kullanmak için bir hidden field'a aktar
            HistoricalPricesHiddenField.Value = historicalPricesJson;
        }
    }

    public class MyCoinDetails
    {
        public string AssetId { get; set; }
        public decimal PriceUsd { get; set; }
    }
}
