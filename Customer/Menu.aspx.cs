using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BakeryMS.DAL;

namespace BakeryMS.Customer
{
    public partial class Menu : System.Web.UI.Page
    {
        // Use ProductsDAL to get the correct product data.
        private ProductsDAL productsDAL = new ProductsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }

        private void LoadProducts()
        {
            // This method returns a DataTable with ProductID, Name, Price, Stock, and Category.
            DataTable dtProducts = productsDAL.getProducts();
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

                // Validate that quantity is a positive number.
                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Quantity must be a positive number.');", true);
                    return;
                }

                // Get products using ProductsDAL, ensuring the DataTable includes Stock.
                DataTable dtProducts = productsDAL.getProducts();
                DataRow[] productRows = dtProducts.Select("ProductID = " + productId);

                if (productRows.Length > 0)
                {
                    int availableStock = Convert.ToInt32(productRows[0]["Stock"]);
                    if (quantity > availableStock)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('Quantity exceeds available stock.');", true);
                        return;
                    }

                    string productName = productRows[0]["Name"].ToString();
                    decimal unitPrice = Convert.ToDecimal(productRows[0]["Price"]);
                    decimal totalPrice = unitPrice * quantity;

                    // Retrieve cart from session or create a new DataTable if it doesn't exist.
                    DataTable cart = Session["Cart"] as DataTable;
                    if (cart == null)
                    {
                        cart = CreateCartTable();
                    }

                    // Add the product to the cart.
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
