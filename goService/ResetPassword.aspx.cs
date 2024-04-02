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



public partial class ResetPassword : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlResetPassword.Visible = false;
        }
    }

    protected void btnVerifyEmail_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text;
        bool emailExists = CheckEmailExists(email);

        if (emailExists)
        {
            pnlResetPassword.Visible = true;
            lblMessage.Text = "";
        }
        else
        {
            lblMessage.Text = "Email does not exist.";
        }
    }

    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text;
        string newPassword = txtNewPassword.Text;

        bool passwordReset = ResetPasswordByEmail(email, newPassword);

        if (passwordReset)
        {
            lblMessage.Text = "Password reset successfully.";
        }
        else
        {
            lblMessage.Text = "Failed to reset password.";
        }
    }

    private bool CheckEmailExists(string email)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT COUNT(*) FROM [User] WHERE Email = @Email";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);

            connection.Open();
            int count = (int)command.ExecuteScalar();

            return count > 0;
        }
    }

    private bool ResetPasswordByEmail(string email, string newPassword)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE [User] SET Password = @NewPassword WHERE Email = @Email";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NewPassword", HashPassword(newPassword));
            command.Parameters.AddWithValue("@Email", email);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
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
