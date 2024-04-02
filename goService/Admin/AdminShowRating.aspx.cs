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

public partial class Admin_AdminShowRating : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadRatings();
        }
    }

    protected void LoadRatings()
    {
        if (Request.QueryString["ServiceID"] != null)
        {
            int serviceID;
            if (int.TryParse(Request.QueryString["ServiceID"], out serviceID))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Rating WHERE ServiceID = @ServiceID";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ServiceID", serviceID);

                    try
                    {
                        connection.Open();
                        GridViewRatings.DataSource = cmd.ExecuteReader();
                        GridViewRatings.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }
            else
            {
               
                Response.Redirect("~/ErrorPage.aspx");
            }
        }
        else
        {
           
            Response.Redirect("~/ErrorPage.aspx");
        }
    }



}

