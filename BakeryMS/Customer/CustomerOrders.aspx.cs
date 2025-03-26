using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BakeryMS.DAL;

namespace BakeryMS.Customer
{
    public partial class CustomerOrders : System.Web.UI.Page
    {
        private OrderManagementDAL orderDAL = new OrderManagementDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Always load the cart regardless of login status
                LoadCart();

                // If the user is logged in, load their orders
                if (Session["UserID"] != null)
                {
                    int userId = Convert.ToInt32(Session["UserID"]);
                    LoadOrders(userId);
                }
            }
        }

        // Load the cart from Session; if it doesn't exist, create a new DataTable
        private void LoadCart()
        {
            DataTable cart = Session["Cart"] as DataTable;
            if (cart == null)
            {
                cart = CreateCartTable();
                Session["Cart"] = cart;
            }
            gvCart.DataSource = cart;
            gvCart.DataBind();
        }

        // Create a DataTable to hold cart items with required columns
        private DataTable CreateCartTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("TotalPrice", typeof(decimal));
            return dt;
        }

        // Load customer orders (only if user is signed in)
        private void LoadOrders(int userId)
        {
            DataTable dtOrders = orderDAL.GetCustomerOrders(userId);
            gvOrders.DataSource = dtOrders;
            gvOrders.DataBind();
        }

        protected void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            // Ensure the user is signed in before submitting the order.
            if (Session["UserID"] == null)
            {
                // Redirect to Login and return to this page after successful login.
                Response.Redirect("/Account/Login.aspx?ReturnUrl=" + Server.UrlEncode(Request.Url.PathAndQuery));
                return;
            }

            int userId = Convert.ToInt32(Session["UserID"]);
            DataTable cart = Session["Cart"] as DataTable;

            if (cart != null && cart.Rows.Count > 0)
            {
                // Create a new order for the logged-in user.
                int orderId = orderDAL.CreateOrder(userId);

                // Add each item from the cart as an order detail.
                foreach (DataRow row in cart.Rows)
                {
                    int productId = Convert.ToInt32(row["ProductID"]);
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    decimal totalPrice = Convert.ToDecimal(row["TotalPrice"]);
                    orderDAL.AddOrderDetail(orderId, productId, quantity, totalPrice);
                }

                // Clear the cart after submission.
                Session["Cart"] = CreateCartTable();
                LoadCart();
                LoadOrders(userId);
            }
        }

        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataTable cart = Session["Cart"] as DataTable;
                if (cart != null && index < cart.Rows.Count)
                {
                    cart.Rows.RemoveAt(index);
                    Session["Cart"] = cart;
                    LoadCart();
                }
            }
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CancelOrder")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                orderDAL.CancelOrder(orderId);
                if (Session["UserID"] != null)
                {
                    int userId = Convert.ToInt32(Session["UserID"]);
                    LoadOrders(userId);
                }
            }
        }

        protected void btnNewOrder_Click(object sender, EventArgs e)
        {
            // Clear the current cart to start a new order.
            Session["Cart"] = CreateCartTable();
            LoadCart();
        }
    }
}
