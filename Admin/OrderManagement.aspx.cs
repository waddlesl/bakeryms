using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using BakeryMS.DAL;

namespace BakeryMS.Customer
{
    public partial class OrderManagement : System.Web.UI.Page
    {
        private OrderManagementDAL orderDAL = new OrderManagementDAL();
        private string connectionString = ConfigurationManager.ConnectionStrings["BakeryDBConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx"); // Redirect to login page
            }

            if (!IsPostBack)
            {
                LoadOrders();
                LoadOrderSummary();
                LoadTopOrderedProducts();
                LoadLeastOrderedProducts();
            }
        }

        private void LoadOrders()
        {
            DataTable dt = GetAllOrders();
            gvOrders.DataSource = dt;
            gvOrders.DataBind();
        }

        private DataTable GetAllOrders()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT OrderID, OrderDate, Status FROM Orders";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private void LoadOrderSummary()
        {
            DataTable dt = GetOrderSummary();
            if (dt.Rows.Count > 0)
            {
                lblTotalOrders.InnerText = dt.Rows[0]["TotalOrders"].ToString();
                lblTotalItems.InnerText = dt.Rows[0]["TotalItemsOrdered"].ToString();
                decimal totalSales = dt.Rows[0]["TotalSales"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["TotalSales"]) : 0;
                lblTotalSales.InnerText = "₱" + totalSales.ToString("N2");
            }
        }

        private DataTable GetOrderSummary()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
                    SELECT COUNT(*) AS TotalOrders, 
                           SUM(od.Quantity) AS TotalItemsOrdered, 
                           SUM(od.Price) AS TotalSales
                    FROM Orders o
                    INNER JOIN OrderDetails od ON o.OrderID = od.OrderID
                    WHERE o.Status = 'Accepted'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private void LoadTopOrderedProducts()
        {
            DataTable dt = GetTopOrderedProducts(5);
            gvTopProducts.DataSource = dt;
            gvTopProducts.DataBind();
        }

        private DataTable GetTopOrderedProducts(int topCount)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
                    SELECT TOP (@TopCount) p.Name, SUM(od.Quantity) AS TotalOrdered
                    FROM OrderDetails od
                    INNER JOIN Products p ON od.ProductID = p.ProductID
                    GROUP BY p.Name
                    ORDER BY TotalOrdered DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TopCount", topCount);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private void LoadLeastOrderedProducts()
        {
            DataTable dt = GetLeastOrderedProducts(5);
            gvLeastProducts.DataSource = dt;
            gvLeastProducts.DataBind();
        }

        private DataTable GetLeastOrderedProducts(int topCount)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
                    SELECT TOP (@TopCount) p.Name, SUM(od.Quantity) AS TotalOrdered
                    FROM OrderDetails od
                    INNER JOIN Products p ON od.ProductID = p.ProductID
                    GROUP BY p.Name
                    ORDER BY TotalOrdered ASC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TopCount", topCount);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AcceptOrder")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                AcceptOrder(orderId);
                LoadOrders();
                LoadOrderSummary();
            }
            else if (e.CommandName == "CancelOrder")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                orderDAL.CancelOrder(orderId);
                LoadOrders();
                LoadOrderSummary();
            }
        }

        private void AcceptOrder(int orderId)
        {
            // Check the current status of the order before accepting
            string currentStatus = GetOrderStatus(orderId);
            if (currentStatus == "Pending")
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = "UPDATE Orders SET Status = 'Accepted' WHERE OrderID = @OrderID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetOrderStatus(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT Status FROM Orders WHERE OrderID = @OrderID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                conn.Open();
                object statusObj = cmd.ExecuteScalar();
                return statusObj != null ? statusObj.ToString() : string.Empty;
            }
        }
    }
}
