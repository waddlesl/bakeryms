using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BakeryMS.DAL
{
    public class SalesReportDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BakeryDBConnection"].ConnectionString;

        public DataTable GetSalesReport(string period, string customDate)
        {
            DataTable dt = new DataTable();
            string query = "SELECT CONVERT(VARCHAR(10), Date, 120) AS Date, TotalSales, ItemsSold FROM SalesReport WHERE 1=1";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;

                    if (period == "daily")
                    {
                        query += " AND CAST(Date AS DATE) = CAST(GETDATE() AS DATE)";
                    }
                    else if (period == "monthly" && !string.IsNullOrEmpty(customDate))
                    {
                        query += " AND YEAR(Date) = @year AND MONTH(Date) = @month";
                        var dateParts = customDate.Split('-');
                        cmd.Parameters.AddWithValue("@year", int.Parse(dateParts[0]));
                        cmd.Parameters.AddWithValue("@month", int.Parse(dateParts[1]));
                    }
                    else if (period == "yearly" && !string.IsNullOrEmpty(customDate))
                    {
                        query += " AND YEAR(Date) = @year";
                        cmd.Parameters.AddWithValue("@year", int.Parse(customDate));
                    }

                    cmd.CommandText = query;
                    con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}
