using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BakeryMS.DAL
{
    public class OrderManagementDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BakeryDBConnection"].ConnectionString;

        public DataTable GetProducts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT ProductID, Name, Price FROM Products", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public decimal GetProductPrice(int productId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Price FROM Products WHERE ProductID = @ProductID", conn);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                conn.Open();
                return (decimal)cmd.ExecuteScalar();
            }
        }

        public int CreateOrder(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Order status defaults to 'Pending'
                SqlCommand cmd = new SqlCommand("INSERT INTO Orders (UserID, Status) OUTPUT INSERTED.OrderID VALUES (@UserID, 'Pending')", conn);
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void AddOrderDetail(int orderId, int productId, int quantity, decimal price)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO OrderDetails (OrderID, ProductID, Quantity, Price) VALUES (@OrderID, @ProductID, @Quantity, @Price)", conn);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Price", price);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetCustomerOrders(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT OrderID, OrderDate, Status FROM Orders WHERE UserID = @UserID", conn);
                da.SelectCommand.Parameters.AddWithValue("@UserID", userId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void CancelOrder(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Orders SET Status = 'Cancelled' WHERE OrderID = @OrderID", conn);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
