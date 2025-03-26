using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BakeryMS.DAL;

namespace BakeryMS.Customer
{
    public partial class Menu : System.Web.UI.Page
    {
        private OrderManagementDAL orderDAL = new OrderManagementDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Allow users to view products and add items to cart regardless of login.
                LoadProducts();
            }
        }

        private void LoadProducts()
        {
            DataTable dtProducts = orderDAL.GetProducts();
            gvProducts.DataSource = dtProducts;
            gvProducts.DataBind();
        }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                int quantity = int.Parse(txtQuantity.Text);

                // Retrieve product info from the database.
                DataTable dtProducts = orderDAL.GetProducts();
                DataRow[] productRows = dtProducts.Select("ProductID = " + productId);
                if (productRows.Length > 0)
                {
                    string productName = productRows[0]["Name"].ToString();
                    decimal unitPrice = Convert.ToDecimal(productRows[0]["Price"]);
                    decimal totalPrice = unitPrice * quantity;

                    // Retrieve cart from session or create a new DataTable if it doesn't exist.
                    DataTable cart = Session["Cart"] as DataTable;
                    if (cart == null)
                    {
                        cart = CreateCartTable();
                    }

                    // Add a new row for the product.
                    DataRow newRow = cart.NewRow();
                    newRow["ProductID"] = productId;
                    newRow["ProductName"] = productName;
                    newRow["UnitPrice"] = unitPrice;
                    newRow["Quantity"] = quantity;
                    newRow["TotalPrice"] = totalPrice;
                    cart.Rows.Add(newRow);

                    Session["Cart"] = cart;
                }
            }
        }

        // Create a DataTable with columns for the cart.
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
    }
}
