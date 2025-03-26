using System;
using System.Web.UI.WebControls;
using BakeryMS.DAL;
using BakeryMS.Helpers;

namespace BakeryMS.Customer
{
    public partial class CustomerSignUp : System.Web.UI.Page
    {
        AccManagementDAL accManager = new AccManagementDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string FirstName = txtFirstName.Text.Trim();
                string LastName = txtLastName.Text.Trim();
                string Email = txtEmail.Text.Trim();
                string Password = txtPassword.Text.Trim();
                string HashedPassword = SecurityHelper.HashPassword(Password); // hash before saving
                string Status = "Active"; // default status


                if (!accManager.isEmailUnique(Email))
                {
                    lblMessage.Text = "Email already exist. Try using other email.";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                    return;
                }


                accManager.AddCustomer(FirstName, LastName, HashedPassword, Email, Status);

                lblMessage.Text = "Account created successfully!";
                lblMessage.CssClass = "fw-bold text-success";
                lblMessage.Visible = true;


                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtEmail.Text = "";
                txtPassword.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "There's an error. Try again";
                lblMessage.CssClass = "fw-bold text-danger";
                lblMessage.Visible = true;
            }
        }
    }
}
