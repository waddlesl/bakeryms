using System;
using System.Web.UI;
using BakeryMS.DAL;
using System.Data;

namespace BakeryMS.Account
{
    public partial class UserAccSettings : Page
    {
        private readonly AccManagementDAL accManager = new AccManagementDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserDetails();
            }
        }

        private void LoadUserDetails()
        {
            string userId = Session["UserID"]?.ToString();
            if (string.IsNullOrEmpty(userId))
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            DataRow userDetails = accManager.GetUserDetailsById(userId);
            if (userDetails != null)
            {
                lblFirstName.Text = userDetails["FirstName"].ToString();
                lblLastName.Text = userDetails["LastName"].ToString();
                lblEmail.Text = userDetails["Email"].ToString();
                lblRole.Text = userDetails["Role"].ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string userId = Session["UserID"]?.ToString();
            if (!string.IsNullOrEmpty(userId))
            {
                accManager.UpdateUserDetails(userId, txtFirstName.Text.Trim(), txtLastName.Text.Trim());
                lblMessageUpdate.Text = "Details updated successfully.";
                lblMessageUpdate.CssClass = "text-success";
                lblMessageUpdate.Visible = true;
                LoadUserDetails();
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            string userId = Session["UserID"]?.ToString();
            if (!string.IsNullOrEmpty(userId) && accManager.ValidateCurrentPassword(userId, txtCurrentPassword.Text))
            {
                accManager.ChangePassword(userId, txtNewPassword.Text);
                lblMessagePassword.Text = "Password changed successfully.";
                lblMessagePassword.CssClass = "text-success"; 
                lblMessagePassword.Visible = true;
            }
            else
            {
                lblMessagePassword.Text = "Current password is incorrect.";
                lblMessagePassword.CssClass = "text-warning"; 
                lblMessagePassword.Visible = true;
            }
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            string userId = Session["UserID"]?.ToString();
            if (!string.IsNullOrEmpty(userId))
            {
                accManager.DeactivateUser(userId);
                Session.Clear();
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}
