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

public partial class ServiceDetails : System.Web.UI.Page
{
    private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadServiceDetails();
            LoadRatings();
        }
    }

    private void LoadServiceDetails()
    {
        if (Request.QueryString["ServiceID"] != null)
        {
            int serviceID = Convert.ToInt32(Request.QueryString["ServiceID"]);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string serviceQuery = "SELECT s.ServiceName, s.Description, s.Category, s.Subcategory, s.Location, s.Price, s.ImagePath, u.UserName as ProviderName, s.ProviderID " +
                                      "FROM Services s " +
                                      "INNER JOIN [User] u ON s.ProviderID = u.UserID " +
                                      "WHERE s.ServiceID = @ServiceID";

                SqlCommand serviceCommand = new SqlCommand(serviceQuery, connection);
                serviceCommand.Parameters.AddWithValue("@ServiceID", serviceID);

                string ratingQuery = "SELECT RatingValue FROM Rating WHERE ServiceID = @ServiceID";
                SqlCommand ratingCommand = new SqlCommand(ratingQuery, connection);
                ratingCommand.Parameters.AddWithValue("@ServiceID", serviceID);

                try
                {
                    connection.Open();

                    SqlDataReader serviceReader = serviceCommand.ExecuteReader();
                    if (serviceReader.Read())
                    {
                        string serviceName = serviceReader["ServiceName"].ToString();
                        string description = serviceReader["Description"].ToString();
                        string category = serviceReader["Category"].ToString();
                        string subcategory = serviceReader["Subcategory"].ToString();
                        string location = serviceReader["Location"].ToString();
                        decimal price = Convert.ToDecimal(serviceReader["Price"]);
                        string providerName = serviceReader["ProviderName"].ToString();
                        int providerID = Convert.ToInt32(serviceReader["ProviderID"]);

                        lblServiceDetails.Text = "<span class='label'>Service Name:</span> " + serviceName + "<br />" +
                         "<span class='label'>Description:</span> " + description + "<br />" +
                         "<span class='label'>Category:</span> " + category + "<br />" +
                         "<span class='label'>Subcategory:</span> " + subcategory + "<br />" +
                         "<span class='label'>Location:</span> " + location + "<br />" +
                         "<span class='label'>Price:</span> " + price + "<br />" +
                         "<span class='label'>Provider:</span> <a href='ProviderProfile.aspx?ProviderID=" + providerID + "'>" + providerName + "</a><br />";

                        string imagePath = serviceReader["ImagePath"].ToString();
                        imgService.ImageUrl = ResolveUrl(imagePath);

                        serviceReader.Close();
                    }

                    SqlDataReader ratingReader = ratingCommand.ExecuteReader();
                    int totalRating = 0;
                    int ratingCount = 0;

                    while (ratingReader.Read())
                    {
                        totalRating += Convert.ToInt32(ratingReader["RatingValue"]);
                        ratingCount++;
                    }

                    ratingReader.Close();

                    if (ratingCount > 0)
                    {
                        double overallRating = (double)totalRating / ratingCount;
                        lblServiceDetails.Text += "Overall Rating: " + overallRating.ToString("F1");
                    }
                }
                catch (Exception ex)
                {
                    lblServiceDetails.Text = "Error loading service details.";
                    Response.Write("Error: " + ex.Message);
                }
            }
        }
    }

    private void LoadRatings()
    {
        if (Request.QueryString["ServiceID"] != null)
        {
            int serviceID = Convert.ToInt32(Request.QueryString["ServiceID"]);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT UserName, RatingValue, AdditionalInfo FROM Rating WHERE ServiceID = @ServiceID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ServiceID", serviceID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    RepeaterRatings.DataSource = reader;
                    RepeaterRatings.DataBind();

                    reader.Close();
                }
                catch (Exception ex)
                {
                   
                    Response.Write("Error: " + ex.Message);
                }
            }
        }
    }
}
