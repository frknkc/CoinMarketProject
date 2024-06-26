using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace CoinMarketProject
{
    public class DataAccess
    {
        private string connectionString;

        public DataAccess()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public void AddUser(string username, string password, string apiKey, string email, string fullName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Username, Password, ApiKey, Email, FullName) VALUES (@Username, @Password, @ApiKey, @Email, @FullName)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@ApiKey", apiKey);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@FullName", fullName);

                string query2 = "INSERT INTO Portfolios (UserId, PortfolioName) VALUES ((SELECT UserId FROM Users WHERE Username = @Username), 'Default')";
                SqlCommand command2 = new SqlCommand(query2, connection);
                command2.Parameters.AddWithValue("@Username", username);


                connection.Open();
                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();

            }
        }

        public string GetApiKey(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ApiKey FROM Users WHERE Username = @Username AND Password = @Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    object result = command.ExecuteScalar();
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                // Daha ayrıntılı hata bilgisi için loglama yapılabilir
                throw new Exception("GetApiKey hatası: " + ex.Message);
            }
        }


        public void UpdateApiKey(string username, string apiKey)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET ApiKey = @ApiKey WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApiKey", apiKey);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int AddPortfolio(int userId, string portfolioName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Portfolios (UserId, PortfolioName) OUTPUT INSERTED.PortfolioId VALUES (@UserId, @PortfolioName)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@PortfolioName", portfolioName);

                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public void AddPortfolioItem(int portfolioId, string coinName, decimal purchasePrice, decimal quantity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO PortfolioItems (PortfolioId, CoinName, PurchasePrice, Quantity) VALUES (@PortfolioId, @CoinName, @PurchasePrice, @Quantity)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PortfolioId", portfolioId);
                command.Parameters.AddWithValue("@CoinName", coinName);
                command.Parameters.AddWithValue("@PurchasePrice", purchasePrice);
                command.Parameters.AddWithValue("@Quantity", quantity);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int GetUserId(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT UserId FROM Users WHERE Username = @Username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    object result = command.ExecuteScalar();
                    return result != null ? (int)result : 0;
                }
            }
            catch (Exception ex)
            {
                // Daha ayrıntılı hata bilgisi için loglama yapılabilir
                throw new Exception("GetUserId hatası: " + ex.Message);
            }
        }

        public DataTable GetPortfolios(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Portfolios WHERE UserId = @UserId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", userId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Portföyleri getirirken hata oluştu.", ex);
            }
        }

        public DataTable GetPortfolioItems(int portfolioId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM PortfolioItems WHERE PortfolioId = @PortfolioId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PortfolioId", portfolioId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
        }

        public DataTable GetCoinById(string coinId, int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define the query to get the Coin by Id but owned by the user
                string query = @"
            SELECT pi.* 
            FROM PortfolioItems pi
            INNER JOIN Portfolios p ON pi.PortfolioId = p.PortfolioId
            INNER JOIN Users u ON p.UserId = u.UserId
            WHERE pi.CoinName = @CoinId AND u.UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@CoinId", coinId);
                    command.Parameters.AddWithValue("@UserId", userId);

                    // Create a data adapter to fill the DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}