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

public partial class Admin_AdminFeedbacks : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFeedbacks();
        }
    }

    private void BindFeedbacks()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT FeedbackID ,UserName, ContactNumber, FeedbackText, DateSubmitted FROM Feedback";
            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptFeedbacks.DataSource = dt;
                rptFeedbacks.DataBind();
            }
            catch (Exception ex)
            {
               
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Button btnDelete = (Button)sender;
        string feedbackId = btnDelete.CommandArgument;

        
        DeleteFeedback(feedbackId);

       
        BindFeedbacks();
    }

    private void DeleteFeedback(string feedbackId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Feedback WHERE FeedbackID = @FeedbackID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@FeedbackID", feedbackId);

            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}

