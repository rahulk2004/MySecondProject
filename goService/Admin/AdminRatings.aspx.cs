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

public partial class Admin_AdminRatings : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadServices();
        }
    }

    protected void LoadServices()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT s.ServiceID, s.ServiceName, s.Description, s.Category, s.Subcategory, s.Location, s.Price, u.UserName, s.ApprovalStatus, s.ImagePath, " +
                           "(SELECT AVG(CAST(RatingValue AS FLOAT)) FROM Rating WHERE ServiceID = s.ServiceID) AS OverallRating " +
                           "FROM Services s INNER JOIN [User] u ON s.ProviderID = u.UserID " +
                           "ORDER BY OverallRating DESC"; 
            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                GridViewServices.DataSource = reader;
                GridViewServices.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }


    protected void btnShowRatings_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "ShowRatings")
        {
            int serviceID = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("AdminShowRating.aspx?ServiceID={ServiceID}");
        }
    }
}
