using System;
using System.Web;
using System.Web.UI;

namespace CoinMarketProject
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                UsernameLiteral.Text = Session["Username"].ToString();
            }

            // Tema seçimine göre CSS dosyasını yükle
            string theme = Session["Theme"]?.ToString() ?? "Light";
            string cssFile = theme == "Dark" ? "dark-theme.css" : "light-theme.css";
            LiteralControl cssLink = new LiteralControl($"<link href='Style/{cssFile}' rel='stylesheet' />");
            Page.Header.Controls.Add(cssLink);
            ChangeThemeButton.Text = theme == "Dark" ? "Açık Tema" : "Koyu Tema";
        }

        protected void ChangeThemeButton_Click(object sender, EventArgs e)
        {
            if (Session["Theme"] == null || Session["Theme"].ToString() == "Light")
            {
                Session["Theme"] = "Dark";
            }
            else
            {
                Session["Theme"] = "Light";
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        public void ShowErrorMessage(string message)
        {
            ErrorMessageLiteral.Text = $"<div class='alert alert-danger'>{message}</div>";
        }
    }
}
