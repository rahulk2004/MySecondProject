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

public partial class GiveRating : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSubmitRating_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string contactNumber = txtContactNumber.Text;
        int ratingValue = Convert.ToInt32(ddlRating.SelectedValue);
        string additionalInfo = txtAdditionalInfo.Text;

        if (!string.IsNullOrEmpty(Request.QueryString["ServiceID"]))
        {
            int serviceID = Convert.ToInt32(Request.QueryString["ServiceID"]);

            int providerID = GetProviderIDForService(serviceID);

            InsertRating(username, contactNumber, ratingValue, additionalInfo, serviceID, providerID);

            Response.Redirect("ServiceDetails.aspx?ServiceID=" + serviceID);
        }
        else
        {
          
        }
    }

    private int GetProviderIDForService(int serviceID)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT ProviderID FROM Services WHERE ServiceID = @ServiceID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ServiceID", serviceID);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                   
                    return -1;
                }
            }
        }
    }

    private void InsertRating(string username, string contactNumber, int ratingValue, string additionalInfo, int serviceID, int providerID)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Rating (UserName, ContactNumber, RatingValue, AdditionalInfo, ServiceID, ProviderID) " +
                           "VALUES (@UserName, @ContactNumber, @RatingValue, @AdditionalInfo, @ServiceID, @ProviderID)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserName", username);
                command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                command.Parameters.AddWithValue("@RatingValue", ratingValue);
                command.Parameters.AddWithValue("@AdditionalInfo", additionalInfo);
                command.Parameters.AddWithValue("@ServiceID", serviceID);
                command.Parameters.AddWithValue("@ProviderID", providerID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
