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


public partial class Feedback : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text.Trim();
        string contactNumber = txtContactNumber.Text.Trim();
        string feedbackText = txtFeedback.Text.Trim();

        if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(feedbackText))
        {
            
            if (SaveFeedback(userName, contactNumber, feedbackText))
            {
                lblMessage.Text = "Feedback submitted successfully!";
                lblMessage.Visible = true;
            }
            else
            {
                lblError.Text = "Error submitting feedback. Please try again later.";
                lblError.Visible = true;
            }
        }
        else
        {
            lblError.Text = "Name and Feedback are required fields.";
            lblError.Visible = true;
        }
    }

    private bool SaveFeedback(string userName, string contactNumber, string feedbackText)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Feedback (UserName, ContactNumber, FeedbackText, DateSubmitted) VALUES (@UserName, @ContactNumber, @FeedbackText, GETDATE())";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    command.Parameters.AddWithValue("@FeedbackText", feedbackText);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            
            Console.WriteLine("Exception: " + ex.Message);
            return false;
        }
    }

}
