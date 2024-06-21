using System;
using System.Web.UI;

namespace CoinMarketProject
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string username = UsernameTextBox.Text;
                    string password = PasswordTextBox.Text;
                    string apiKey = ApiKeyTextBox.Text;
                    string email = EmailTextBox.Text;
                    string fullName = FullNameTextBox.Text;

                    DataAccess dataAccess = new DataAccess();
                    dataAccess.AddUser(username, password, apiKey, email, fullName);

                    Response.Redirect("Login.aspx");
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = $"Kayıt yapılırken bir hata oluştu: {ex.Message}";
                }
            }
        }
    }
}
