using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace CoinMarketProject
{
    public partial class CoinDetails : System.Web.UI.Page
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
                string coinId = Request.QueryString["coinId"];
                if (!string.IsNullOrEmpty(Request.QueryString["years"]))
                {
                    int years = 1;
                    if (int.TryParse(Request.QueryString["years"], out years))
                    {
                        //LoadCoinDetails(coinId);
                        LoadHistoricalPrices(coinId, years);
                    }
                }
            }
        }

        private void LoadCoinDetails(string coinId)
        {
            string apiKey = Session["ApiKey"].ToString();
            var coinDetails = _coinApiService.GetCoinData<MyCoinDetails>($"v1/assets/{coinId}", apiKey);
            CoinNameLabel.InnerText = coinDetails.AssetId;
            CurrentPriceLabel.InnerText = coinDetails.PriceUsd.ToString();
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
