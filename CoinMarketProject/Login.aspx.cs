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
        protected void Page_Load(object sender, EventArgs e)
        {
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
    }
}
