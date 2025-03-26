using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace BakeryMS.DAL
{
    public class ProductsDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BakeryDBConnection"].ConnectionString;

        // Get products
        public DataTable getProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT ProductID, Name, Price, Stock, Category FROM Products";
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        // Add
        public void addProduct(string name, decimal price, int stock, string category)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Products (Name, Price, Stock, Category) VALUES (@Name, @Price, @Stock, @Category)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@Category", category);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Update
        public void UpdateProduct(string productId, string name, string price, string stock, string category)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Products SET Name = @Name, Price = @Price, Stock = @Stock, Category = @Category WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@Category", category);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete
        public void DeleteProduct(string productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Products WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}