using BakeryMS.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.DynamicData;


namespace BakeryMS.DAL
{
    public class AccManagementDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BakeryDBConnection"].ConnectionString;

        // Validate user login
        public string ValidateUser(string email, string enteredPassword)
        {
            string storedHashedPassword = String.Empty;
            string storedRole = String.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // only allows active accounts to login
                string sql = @"SELECT Password, Role
                               FROM Users
                               WHERE Email = @Email AND Status = 'Active'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            storedHashedPassword = reader["Password"].ToString();
                            storedRole = reader["Role"].ToString();
                        }
                    }

                }
            }

            // hash the entered password and compare to the stored hash
            string hashedEnteredPassword = SecurityHelper.HashPassword(enteredPassword);

            // compare in a case-sensitive manner
            if (string.Equals(storedHashedPassword, hashedEnteredPassword, StringComparison.Ordinal))
            {
                return storedRole;
            }

            return null; // invalid login
        }

        // Admin display
        public DataTable DisplayAdmins()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string sql = @"SELECT UserID, FirstName, LastName, Email, Status
                               FROM Users WHERE Role = 'Admin'";

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    return (dataTable);
                }
            }
        }

        // Admin search

        public DataTable SearchAdmins(string keyword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT UserID, FirstName, LastName, Email, Status
                               FROM Users WHERE Role = 'Admin' AND (UserID LIKE @Keyword1 
                               OR FirstName LIKE @Keyword2 
                               OR LastName LIKE @Keyword3)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Keyword1", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@Keyword2", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@Keyword3", "%" + keyword + "%");

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        return (dataTable);
                    }
                }

            }
        }

        // Admin update
        public void UpdateAdmins(int AdminID, string FirstName, string LastName, string Email, string Status)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"UPDATE Users
                               SET FirstName = @FirstName, LastName = @LastName, 
                               Email = @Email, Status = @Status
                               WHERE UserID = @AdminID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@AdminID", AdminID);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Email", Email);
                  
                    command.Parameters.AddWithValue("@Status", Status);

                    command.ExecuteNonQuery();
                }

            }
        }

        // Admin add

        public void AddAdmin(string FirstName, string LastName, string Email, string Password, string Status)
        {

            // default role = 'Admin'

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"INSERT INTO Users (FirstName, LastName, Email, Password, Role, Status)
                               VALUES (@FirstName, @LastName, @Email, @Password,'Admin',@Status)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Status", Status);

                    command.ExecuteNonQuery();
                }

            }
        }


        // Customers

        // Customer display
        public DataTable DisplayCustomers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string sql = @"SELECT UserID, FirstName, LastName, Email, Status
                               FROM Users WHERE Role = 'Customer'";

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    return (dataTable);
                }
            }
        }


        // Customer search
        public DataTable SearchCustomers(string keyword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string sql = @"SELECT UserID, FirstName, LastName, Email, Status
                               FROM Users WHERE Role = 'Customer' AND (UserID LIKE @Keyword1 
                               OR FirstName LIKE @Keyword2 
                               OR LastName LIKE @Keyword3)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Keyword1", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@Keyword2", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@Keyword3", "%" + keyword + "%");

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        return (dataTable);
                    }
                }

            }
        }

        // Customer add

        public void AddCustomer(string FirstName, string LastName, string HashedPassword, string Email, string Status)
        {
            // default role = 'Customer'

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"INSERT INTO Users (FirstName, LastName, Email, Password, Role, Status)
                               VALUES (@FirstName, @LastName, @Email, @Password,'Customer',@Status)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", HashedPassword);
                    command.Parameters.AddWithValue("@Status", Status);

                    command.ExecuteNonQuery();
                }

            }
        }

        // Check if email is unique

        public bool isEmailUnique(string email, int adminID = 0)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT COUNT(*) FROM Users WHERE Email = @Email AND UserID != @AdminID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@AdminID", adminID);
                    int count = (int)command.ExecuteScalar();
                    return count == 0; // return true if unique (ignoring the current admin's email)
                }
            }
        }

        // Methods for account settings

        // Returns a DataRow with user details based on userID.
        public DataRow GetUserDetailsById(string userId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT FirstName, LastName, Email, Role FROM Users WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        // Validates the current password for a given user.
        public bool ValidateCurrentPassword(string userId, string enteredPassword)
        {
            string storedHashedPassword = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Password FROM Users WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        storedHashedPassword = result.ToString();
                }
            }
            // Compare hashed versions.
            string hashedEnteredPassword = SecurityHelper.HashPassword(enteredPassword);
            return string.Equals(storedHashedPassword, hashedEnteredPassword, StringComparison.Ordinal);
        }

        // Deactivates a user account by setting Status to 'Inactive'
        public void DeactivateUser(string userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Status = 'Inactive' WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Existing GetUserDetails method (by email/password) remains for login purposes.
        public class UserDetails
        {
            public string UserID { get; set; }
            public string Role { get; set; }
        }

        public UserDetails GetUserDetails(string email, string password)
        {
            UserDetails user = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID, Role, Password FROM Users WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHashedPassword = reader["Password"].ToString();
                            string hashedEnteredPassword = SecurityHelper.HashPassword(password);
                            if (string.Equals(storedHashedPassword, hashedEnteredPassword, StringComparison.Ordinal))
                            {
                                user = new UserDetails
                                {
                                    UserID = reader["UserID"].ToString(),
                                    Role = reader["Role"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            return user;
        }
        // Update user details 
        public void UpdateUserDetails(string userId, string firstName, string lastName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Change password
        public void ChangePassword(string userId, string newPassword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Always store the hashed password!
                string query = "UPDATE Users SET Password = @NewPassword WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Hash the new password before storing it.
                    cmd.Parameters.AddWithValue("@NewPassword", SecurityHelper.HashPassword(newPassword));
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }

}