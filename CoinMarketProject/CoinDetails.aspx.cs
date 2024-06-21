using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoinMarketProject
{
    public partial class CoinDetails : System.Web.UI.Page
    {
        private CoinApiService _coinApiService = new CoinApiService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string coinId = Request.QueryString["coinId"];
                LoadCoinDetails(coinId);
            }
        }

        private void LoadCoinDetails(string coinId)
        {
            string apiKey = Session["ApiKey"].ToString();
            var coinDetails = _coinApiService.GetCoinData<MyCoinDetails>($"v1/assets/{coinId}", apiKey);
            CoinNameLabel.InnerText = coinDetails.AssetId;
            CurrentPriceLabel.InnerText = coinDetails.PriceUsd.ToString();
        }
    }
    public class MyCoinDetails
    {
        public string AssetId { get; set; }
        public decimal PriceUsd { get; set; }
    }
}