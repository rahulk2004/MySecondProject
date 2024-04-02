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

public partial class ProviderProfile : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            LoadProviderDetails();
        }
    }

    private void LoadProviderDetails()
    {
        if (Request.QueryString["ProviderID"] != null)
        {
            int providerID = Convert.ToInt32(Request.QueryString["ProviderID"]);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT UserName, Email, Address, ContactNumber, Gender, ShopName, Image FROM [User] WHERE UserID = @ProviderID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProviderID", providerID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            lblProviderName.Text = "User Name: " + reader["UserName"];
                            lblEmail.Text = "Email: " + reader["Email"];
                            lblAddress.Text = "Address: " + reader["Address"];
                            lblContactNumber.Text = "Contact Number: " + reader["ContactNumber"];
                            lblGender.Text = "Gender: " + reader["Gender"];
                            lblShopName.Text = "Shop Name: " + reader["ShopName"];

                            if (!DBNull.Value.Equals(reader["Image"]))
                            {
                                string imagePath = reader["Image"].ToString();
                                imgProvider.Src = ResolveUrl(imagePath);
                                imgProvider.Visible = true;
                            }
                            else
                            {
                                imgProvider.Visible = false;
                            }
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        lblProviderName.Text = "Error loading provider details: " + ex.Message;
                    }
                }
            }
        }
        else
        {
            lblProviderName.Text = "Provider ID is missing in the query string.";
        }
    }
}

