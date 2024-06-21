using System;
using System.Web.UI;

namespace CoinMarketProject
{
    public partial class AccountSettings : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadAccountSettings();
            }
        }

        protected void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            string username = Session["Username"].ToString();
            string apiKey = ApiKeyTextBox.Text;

            DataAccess dataAccess = new DataAccess();
            dataAccess.UpdateApiKey(username, apiKey);

            Session["ApiKey"] = apiKey;
            SuccessLabel.Text = "Ayarlar başarıyla kaydedildi.";
            ErrorLabel.Text = string.Empty;
        }

        protected void TestApiKeyButton_Click(object sender, EventArgs e)
        {
            string apiKey = ApiKeyTextBox.Text;
            CoinApiService coinApiService = new CoinApiService();

            if (coinApiService.TestApiKey(apiKey))
            {
                SuccessLabel.Text = "API Key geçerli!";
                ErrorLabel.Text = string.Empty;
            }
            else
            {
                SuccessLabel.Text = string.Empty;
                ErrorLabel.Text = "API Key geçersiz veya hatalı.";
            }
        }

        private void LoadAccountSettings()
        {
            string apiKey = Session["ApiKey"].ToString();
            ApiKeyTextBox.Text = apiKey;
        }
    }
}
