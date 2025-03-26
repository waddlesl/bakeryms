using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BakeryMS.DAL;

namespace BakeryMS.Admin
{
    public partial class SalesReport : System.Web.UI.Page
    {
        private readonly SalesReportDAL salesReportDAL = new SalesReportDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx"); // Redirect to login page
            }

            if (!IsPostBack)
            {
                LoadYearDropdown();
                ClearGrid();
            }
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedPeriod = Request.Form["reportType"];
                string customDate = GetSelectedDate(selectedPeriod);

                LoadSalesReport(selectedPeriod, customDate);
            }
            catch (Exception ex)
            {
                ShowMessage($"Error generating report: {ex.Message}");
            }
        }

        private void LoadSalesReport(string period, string customDate)
        {
            DataTable dt = salesReportDAL.GetSalesReport(period, customDate);
            if (dt != null && dt.Rows.Count > 0)
            {
                gvSalesReport.DataSource = dt;
                gvSalesReport.DataBind();
            }
            else
            {
                ClearGrid();
                ShowMessage("No records found for the selected period.");
            }
        }

        private void LoadYearDropdown()
        {
            int currentYear = DateTime.Now.Year;
            ddlYear.Items.Clear();
            ddlMonthYear.Items.Clear();

            for (int i = currentYear; i >= 2000; i--)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlMonthYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        private void ClearGrid()
        {
            gvSalesReport.DataSource = null;
            gvSalesReport.DataBind();
        }

        private string GetSelectedDate(string selectedPeriod)
        {
            if (selectedPeriod == "yearly")
                return ddlYear.SelectedValue;
            else if (selectedPeriod == "monthly")
                return $"{ddlMonthYear.SelectedValue}-{ddlMonth.SelectedValue}";

            return null; // Daily report case (if needed, handle separately)
        }

        private void ShowMessage(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }
    }
}
