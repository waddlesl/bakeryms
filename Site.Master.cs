using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BakeryMS
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNavigation();
            }
        }

        private void LoadNavigation()
        {
            if (Session["UserRole"] != null)
            {
                string role = Session["UserRole"].ToString();
                if (role == "Admin")
                {
                    litNav.Text = @"
                                <nav class='navbar navbar-expand-lg'>
                                    <div class='container-fluid'>
                                        <ul class='navbar-nav'>
                                            <li class='nav-item'><a class='nav-link' href='/Admin/AdminAccManagement.aspx'>Accounts</a></li>   
                                            <li class='nav-item'><a class='nav-link' href='/Admin/ProductManagement.aspx'>Products</a></li>
                                            <li class='nav-item'><a class='nav-link' href='/Admin/OrderManagement.aspx'>Orders</a></li>
                                            <li class='nav-item'><a class='nav-link' href='/Admin/OrderDetails.aspx'>OrderDetails</a></li>
                                            <li class='nav-item'><a class='nav-link' href='/Admin/SalesReport.aspx'>Sales Report</a></li>
                                            <li class='nav-item'><a class='nav-link' href='/Account/UserAccSettings.aspx'>My Account</a></li>
                                            <li class='nav-item'><a class='nav-link' href='/Account/Logout.aspx'>Logout</a></li>
                                        </ul>
                                    </div>
                                </nav>";
                }
                else if (role == "Customer")
                {
                    litNav.Text = @"
                                <nav class='navbar navbar-expand-lg'>
                                    <div class='container-fluid'>
                                        <ul class='navbar-nav'>
                                            <li class='nav-item'><a class='nav-link' href='/Customer/Home.aspx'>Home</a></li>
                                            <li class='nav-item'><a class='nav-link' href='/Customer/Menu.aspx'>Menu</a></li>   
                                            <li class='nav-item'><a class='nav-link' href='/Customer/CustomerOrders.aspx'>My Orders</a></li>      
                                            <li class='nav-item'><a class='nav-link' href='/Account/UserAccSettings.aspx'>My Account</a></li>
                                            <li class='nav-item'><a class='nav-link' href='/Account/Logout.aspx'>Logout</a></li>
                                        </ul>
                                    </div>
                                </nav>";
                }



            }

            // not logged in
            else
            {
                litNav.Text = @"
                                <nav class='navbar navbar-expand-lg'>
                                    <div class='container-fluid'>
                                        <ul class='navbar-nav'>
                                            <li class='nav-item'><a class='nav-link' href='/Customer/Home.aspx'>Home</a></li>
                                            <li class='nav-item'><a class='nav-link' href='/Customer/Menu.aspx'>Menu</a></li>
                                            <li class='nav-item'><a class='nav-link' href='/Customer/CustomerOrders.aspx'>My Orders</a></li>      
                                            <li class='nav-item'><a class='nav-link' href='/Account/Login.aspx'>Login</a></li>
                                        </ul>
                                    </div>
                                </nav>";
            }


        }
    }
}
