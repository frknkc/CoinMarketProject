using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoinMarketProject
{
    public partial class Login : System.Web.UI.Page
    {
        private CoinApiService coinApiService = new CoinApiService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ApiKey"] != null)
                {
                    UpdateCryptoPrices();
                }
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string username = UsernameTextBox.Text;
                string password = PasswordTextBox.Text;

                DataAccess dataAccess = new DataAccess();
                string apiKey = dataAccess.GetApiKey(username, password);

                if (apiKey != null)
                {
                    Session["Username"] = username;
                    Session["ApiKey"] = apiKey;
                    int userId = dataAccess.GetUserId(username);
                    Session["UserId"] = userId;
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    ErrorLabel.Text = "Kullanıcı adı veya parola yanlış.";
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
                    decimal btcCurrentPrice = coinApiService.GetCurrentPrice("BTC", apiKey);
                    decimal ethCurrentPrice = coinApiService.GetCurrentPrice("ETH", apiKey);
                    decimal usdtCurrentPrice = coinApiService.GetCurrentPrice("USDT", apiKey);

                    DateTime yesterday = DateTime.Now.AddDays(-1).Date.AddHours(1); // Dünkü saat 01:00
                    decimal btcHistoricalPrice = coinApiService.GetHistoricalPrice("BTC", apiKey, yesterday);
                    decimal ethHistoricalPrice = coinApiService.GetHistoricalPrice("ETH", apiKey, yesterday);
                    decimal usdtHistoricalPrice = coinApiService.GetHistoricalPrice("USDT", apiKey, yesterday);

                    //BtcUsdtLabel.Text = btcCurrentPrice.ToString("F2");
                    //EthUsdtLabel.Text = ethCurrentPrice.ToString("F2");
                    //UsdtTryLabel.Text = usdtCurrentPrice.ToString("F2");

                    //// Değişim yüzdelerini hesaplayın
                    //BtcChangeLabel.Text = CalculatePercentageChange(btcHistoricalPrice, btcCurrentPrice).ToString("F2") + "%";
                    //EthChangeLabel.Text = CalculatePercentageChange(ethHistoricalPrice, ethCurrentPrice).ToString("F2") + "%";
                    //UsdtChangeLabel.InnerText = CalculatePercentageChange(usdtHistoricalPrice, usdtCurrentPrice).ToString("F2") + "%";
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = $"Fiyatlar güncellenirken hata oluştu: {ex.Message}";
                }
            }
            else
            {
                ErrorLabel.Text = "API anahtarı bulunamadı.";
            }
        }

        private decimal CalculatePercentageChange(decimal oldPrice, decimal newPrice)
        {
            return ((newPrice - oldPrice) / oldPrice) * 100;
        }
    }
}
