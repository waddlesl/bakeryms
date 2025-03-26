using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace BakeryMS.Admin  // Ensure this matches the Inherits attribute in the .aspx file.
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx"); // Redirect to login page
            }

            if (!IsPostBack)
            {
                // Load all order details initially
                GetOrderDetails();
            }
        }

        // Fetch order details (loads all orders)
        private void GetOrderDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BakeryDBConnection"].ConnectionString;
            string query = @"
                SELECT 
                    U.UserID AS cus_id, 
                    U.LastName + ' ' + U.FirstName AS cus_name, 
                    O.OrderDate AS odr_date, 
                    P.Name AS pr_name,
                    OD.Quantity AS pr_qty, 
                    P.Price AS pr_price, 
                    OD.Price AS ttl_price,
                    OD.OrderDetailID,
                    O.OrderID,
                    P.ProductID,
                    OD.Quantity AS Quantity,
                    OD.Price AS Price
                FROM Orders O
                INNER JOIN Users U ON O.UserID = U.UserID
                INNER JOIN OrderDetails OD ON O.OrderID = OD.OrderID
                INNER JOIN Products P ON OD.ProductID = P.ProductID
                ORDER BY U.UserID, O.OrderDate";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    OrderDetailsGrid.DataSource = reader;
                    OrderDetailsGrid.DataBind();
                }
            }
        }

        // Search for orders by a given user ID.
        protected void searchUser_btn_Click(object sender, EventArgs e)
        {
            string searchText = searchUser_txtbox.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                searchUser_txtbox.Text = "Please enter a user ID";
                return;
            }

            if (!int.TryParse(searchText, out int userID))
            {
                searchUser_txtbox.Text = "Invalid user ID";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["BakeryDBConnection"].ConnectionString;
            string query = @"
                SELECT 
                    U.UserID AS cus_id, 
                    U.LastName + ' ' + U.FirstName AS cus_name, 
                    O.OrderDate AS odr_date, 
                    P.Name AS pr_name,
                    OD.Quantity AS pr_qty, 
                    P.Price AS pr_price, 
                    OD.Price AS ttl_price,
                    OD.OrderDetailID,
                    O.OrderID,
                    P.ProductID,
                    OD.Quantity AS Quantity,
                    OD.Price AS Price
                FROM Orders O
                INNER JOIN Users U ON O.UserID = U.UserID
                INNER JOIN OrderDetails OD ON O.OrderID = OD.OrderID
                INNER JOIN Products P ON OD.ProductID = P.ProductID
                WHERE U.UserID = @UserID
                ORDER BY O.OrderDate";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userID);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    OrderDetailsGrid.DataSource = reader;
                    OrderDetailsGrid.DataBind();
                }
            }
        }
    }
}
