using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BakeryMS.DAL;

namespace BakeryMS.Admin
{
    public partial class ProductManagement : System.Web.UI.Page
    {
        ProductsDAL prodManager = new ProductsDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx"); // Redirect to login page
            }

            if (!IsPostBack)
            {
                displayProducts();
            }
        }


        protected void displayProducts()
        {
            DataTable dt = prodManager.getProducts();
            gvProducts.DataSource = dt;
            gvProducts.DataBind();
        }


        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProducts.EditIndex = e.NewEditIndex;
            displayProducts();
        }

        protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProducts.EditIndex = -1;
            displayProducts();
        }

        protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvProducts.Rows[e.RowIndex];
            string productId = gvProducts.DataKeys[e.RowIndex].Value.ToString();
            string name = ((TextBox)row.Cells[1].Controls[0]).Text;
            string price = ((TextBox)row.Cells[2].Controls[0]).Text;
            string stock = ((TextBox)row.Cells[3].Controls[0]).Text;
            string category = ((TextBox)row.Cells[4].Controls[0]).Text;


            prodManager.UpdateProduct(productId, name, price, stock, category);

            gvProducts.EditIndex = -1;
            displayProducts();
        }


        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string productId = gvProducts.DataKeys[e.RowIndex].Value.ToString();


            prodManager.DeleteProduct(productId);

            displayProducts();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text))
            {
                return;
            }

            string name = txtName.Text.Trim();
            decimal price;
            int stock;
            if (!decimal.TryParse(txtPrice.Text.Trim(), out price) || !int.TryParse(txtStock.Text.Trim(), out stock))
            {
                return;
            }

            string category = drpCategory.SelectedValue;

            prodManager.addProduct(name, price, stock, category);

            displayProducts();


            txtName.Text = "";
            txtPrice.Text = "";
            txtStock.Text = "";
            drpCategory.SelectedIndex = 0;
        }
        protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProducts.PageIndex = e.NewPageIndex;
            displayProducts();
        }


        protected void gvProducts_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = prodManager.getProducts(); // Get data as DataTable
            if (dt != null)
            {
                DataView dv = dt.DefaultView;

                // Toggle sorting direction
                if (ViewState["SortExpression"] as string == e.SortExpression)
                {
                    ViewState["SortDirection"] = (ViewState["SortDirection"] as string == "ASC") ? "DESC" : "ASC";
                }
                else
                {
                    ViewState["SortDirection"] = "ASC";
                }

                dv.Sort = e.SortExpression + " " + ViewState["SortDirection"];
                gvProducts.DataSource = dv;
                gvProducts.DataBind();
            }
        }




    }
}