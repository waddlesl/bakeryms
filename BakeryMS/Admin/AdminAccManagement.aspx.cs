using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BakeryMS.DAL;
using BakeryMS.Helpers;

namespace BakeryMS.Admin
{
    public partial class AdminAccManagement : System.Web.UI.Page
    {
        AccManagementDAL accManager = new AccManagementDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx"); // Redirect to login page
            }

            displayAdmins();
            displayCustomers();
        }

        // Admins

        // DISPLAY
        protected void displayAdmins()
        {
            gvAdmin.DataSource = accManager.DisplayAdmins();
            gvAdmin.DataBind();

        }

        // SEARCH
        protected void btnAdminSearch_Click(object sender, EventArgs e)
        {
            gvAdmin.DataSource = accManager.SearchAdmins(txtAdminSearch.Text.Trim());
            gvAdmin.DataBind();

        }

        // ADD
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            rfvAdminPassword.Enabled = true;
            revAdminPassword.Enabled = true;

            try
            {
                string FirstName = txtAdminFName.Text.Trim();
                string LastName = txtAdminLName.Text.Trim();
                string Email = txtAdminEmail.Text.Trim();
                string Password = SecurityHelper.HashPassword(txtAdminPassword.Text.Trim());
                string Status = drpAdminStatus.SelectedValue;

                // check if email is unique
                if (!accManager.isEmailUnique(Email))
                {
                    lblAdminMessage.Visible = true;
                    lblAdminMessage.CssClass = "alert alert-danger";
                    lblAdminMessage.Text = "Email is already in use.";
                    return;
                }

                accManager.AddAdmin(FirstName, LastName, Email, Password, Status);

                // sucess message
                lblAdminMessage.Visible = true;
                lblAdminMessage.CssClass = "alert alert-success";
                lblAdminMessage.Text = $"Admin {FirstName}, {LastName} sucessfully added.";

                // reload admin gridview
                displayAdmins();
            }
            catch (Exception ex)
            {
                lblAdminMessage.Visible = true;
                lblAdminMessage.CssClass = "alert alert-danger";
                lblAdminMessage.Text = "Invalid input.";

            }
        }

        // UPDATE
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            rfvAdminPassword.Enabled = false;
            revAdminPassword.Enabled = false;

            if (string.IsNullOrWhiteSpace(txtAdminID.Text))
            {
                lblAdminMessage.Visible = true;
                lblAdminMessage.CssClass = "alert alert-danger";
                lblAdminMessage.Text = "Invalid input.";
                return;
            }

            int AdminID = int.TryParse(txtAdminID.Text.Trim(), out int admin) ? admin : 0;
            if (AdminID <= 0)
            {
                lblAdminMessage.Visible = true;
                lblAdminMessage.CssClass = "alert alert-danger";
                lblAdminMessage.Text = "Invalid Admin ID.";
                return;
            }

            string FirstName = txtAdminFName.Text.Trim();
            string LastName = txtAdminLName.Text.Trim();
            string Email = txtAdminEmail.Text.Trim();
            string Status = drpAdminStatus.SelectedValue;

            // Check if email is unique (before updating)
            if (!accManager.isEmailUnique(Email, AdminID))
            {
                lblAdminMessage.Visible = true;
                lblAdminMessage.CssClass = "alert alert-danger";
                lblAdminMessage.Text = "Email is already in use.";
                return;
            }

            try
            {
                accManager.UpdateAdmins(AdminID, FirstName, LastName, Email, Status);

                // Success message
                lblAdminMessage.Visible = true;
                lblAdminMessage.CssClass = "alert alert-success";
                lblAdminMessage.Text = $"Admin {AdminID} successfully updated.";

                // Reload admin gridview
                displayAdmins();
            }
            catch (Exception ex)
            {
                lblAdminMessage.Visible = true;
                lblAdminMessage.CssClass = "alert alert-warning";
                lblAdminMessage.Text = "Invalid input.";
            }
        }


        // Customers

        protected void displayCustomers()
        {
            gvCus.DataSource = accManager.DisplayCustomers();
            gvCus.DataBind();

        }

        protected void btnCusSearch_Click(object sender, EventArgs e)
        {
            gvCus.DataSource = accManager.SearchCustomers(txtCusSearch.Text.Trim());
            gvCus.DataBind();


        }

        

    }
}