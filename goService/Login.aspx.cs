using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

public partial class Login : System.Web.UI.Page
{
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string email = txtLoginEmail.Text;
        string password = HashPassword(txtLoginPassword.Text);

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand checkUser = new SqlCommand("SELECT UserID, UserName, UserRole, ApprovalStatus FROM [User] WHERE Email = @Email AND Password = @Password", connection))
            {
                checkUser.Parameters.AddWithValue("@Email", email);
                checkUser.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = checkUser.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string approvalStatus = reader["ApprovalStatus"].ToString();

                        if (approvalStatus == "Approved")
                        {
                            int userId = Convert.ToInt32(reader["UserID"]);
                            string userRole = reader["UserRole"].ToString();
                            string userName = reader["UserName"].ToString();

                            Session["UserID"] = userId;
                            Session["UserName"] = userName;

                            if (userRole == "Admin")
                            {
                                Response.Redirect("Admin/AdminHome.aspx");
                            }
                            else
                            {
                                Response.Redirect("User/UserHome.aspx");
                            }
                        }
                        else if (approvalStatus == "Pending")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Your account is pending approval. Please wait for approval.');", true);
                        }
                        else if (approvalStatus == "Rejected")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Your account has been rejected. Please contact support.');", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Invalid email or password');", true);
                    }
                }
            }
        }
    }
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
