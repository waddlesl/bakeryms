using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BakeryMS.DAL;

namespace BakeryMS.Account
{
    public partial class Login : System.Web.UI.Page
    {
        AccManagementDAL accManager = new AccManagementDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false; // Hide message on page load
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Retrieve both UserID and Role from the database
            var userDetails = accManager.GetUserDetails(email, password);

            if (userDetails == null)
            {
                lblMessage.Visible = true;
                lblMessage.CssClass = "text-danger";
                lblMessage.Text = "Invalid email or password. Try again.";
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.CssClass = "text-success";
                lblMessage.Text = "Login successful! Redirecting...";

                // Store user details in session
                Session["UserID"] = userDetails.UserID;  // Store UserID
                Session["UserEmail"] = email;
                Session["UserRole"] = userDetails.Role;

                // Redirect based on role
                if (userDetails.Role == "Admin")
                {
                    Response.Redirect("~/Admin/AdminAccManagement.aspx");
                }
                else if (userDetails.Role == "Customer")
                {
                    Response.Redirect("~/Customer/Home.aspx");
                }
            }
        }

    }
}
