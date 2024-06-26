using System;
using System.Web.UI.WebControls;
using System.Data;

namespace CoinMarketProject
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        private DataAccess dataAccess = new DataAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            DataTable users = dataAccess.GetAllUsers();
            UsersGridView.DataSource = users;
            UsersGridView.DataBind();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string userId = btn.CommandArgument;

            // Delete user logic here
            // Example:
            // dataAccess.DeleteUser(userId);
            // Reload users after deletion
            LoadUsers();
        }

        protected void ChangeRoleButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split(';');
            string userId = args[0];
            string currentRole = args[1];

            // Change role logic here
            string newRole = currentRole == "Admin" ? "User" : "Admin";
            dataAccess.ChangeUserRole(Convert.ToInt32(userId), newRole);

            // Reload users after role change
            LoadUsers();
        }
    }
}
