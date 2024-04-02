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

public partial class User_UserRating : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (User.Identity.IsAuthenticated)
            {
               
                string userId = Session["UserID"].ToString();

                
                LoadUserRatings(userId);
            }
            else
            {
                
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }

    private void LoadUserRatings(string userId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            
            string query = "SELECT r.ServiceID, s.ServiceName, s.Price, s.Location, r.UserName, r.ContactNumber, r.RatingValue, r.AdditionalInfo " +
                           "FROM Rating r " +
                           "INNER JOIN Services s ON r.ServiceID = s.ServiceID " +
                           "WHERE r.ProviderID = @UserID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                
                command.Parameters.AddWithValue("@UserID", userId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                   
                    GridViewRatings.DataSource = reader;
                    GridViewRatings.DataBind();

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("Error loading user ratings: " + ex.Message);
                }
            }
        }
    }
}