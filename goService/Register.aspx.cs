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

public partial class Register : System.Web.UI.Page
{
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string fullName = txtFullName.Text;
        string gender = ddlGender.SelectedValue;
        string email = txtEmail.Text;
        string password = HashPassword(txtPassword.Text);
        string shopName = txtShopName.Text;
        string address = txtAddress.Text;
        string contactNumber = txtContactNumber.Text;

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand checkUserExistence = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE Email = @Email", connection);
            checkUserExistence.Parameters.AddWithValue("@Email", email);

            int userCount = (int)checkUserExistence.ExecuteScalar();

            if (userCount > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email already exists');", true);
                return;
            }

            SqlCommand insertUser = new SqlCommand(
                "INSERT INTO [User] (UserName, Email, Password, UserRole, ShopName, Address, ContactNumber, Gender, ApprovalStatus) " +
                "VALUES (@UserName, @Email, @Password, @UserRole, @ShopName, @Address, @ContactNumber, @Gender, 'Pending')",
                connection);

            insertUser.Parameters.AddWithValue("@UserName", fullName);
            insertUser.Parameters.AddWithValue("@Email", email);
            insertUser.Parameters.AddWithValue("@Password", password);
            insertUser.Parameters.AddWithValue("@UserRole", "User");
            insertUser.Parameters.AddWithValue("@ShopName", shopName);
            insertUser.Parameters.AddWithValue("@Address", address);
            insertUser.Parameters.AddWithValue("@ContactNumber", contactNumber);
            insertUser.Parameters.AddWithValue("@Gender", gender);

            insertUser.ExecuteNonQuery();
        }

        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Registration request submitted. Please wait for admin approval.');", true);
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
