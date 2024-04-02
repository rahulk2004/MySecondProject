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
using System.Collections.Generic;


public partial class Services : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            LoadFilterOptions(null);
            LoadServices();
        }
    }

    private void LoadAllServices()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT s.ServiceID, s.ServiceName, s.ImagePath, s.Price, s.Location, r.OverallRating " +
                           "FROM Services s " +
                           "LEFT JOIN (SELECT ServiceID, AVG(CAST(RatingValue AS FLOAT)) AS OverallRating " +
                           "           FROM Rating GROUP BY ServiceID) r ON s.ServiceID = r.ServiceID " +
                           "WHERE s.ApprovalStatus = 'Approved'";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    RepeaterServices.DataSource = reader;
                    RepeaterServices.DataBind();

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
        }
    }

    private void LoadFilterOptions(string selectedCategory)
    {
        if (!IsPostBack)
        {
            LoadDropDownListWithAll("SELECT DISTINCT Category FROM Services", ddlCategory, "Category");
        }

        if (!string.IsNullOrEmpty(selectedCategory))
        {
            LoadSubcategories(selectedCategory);
            LoadLocations(selectedCategory);

           
            ddlCategory.SelectedValue = selectedCategory;
        }
        else
        {
            if (!IsPostBack)
            {
                LoadDropDownListWithAll("SELECT DISTINCT Subcategory FROM Services", ddlSubcategory, "Subcategory");
                LoadDropDownListWithAll("SELECT DISTINCT Location FROM Services", ddlLocation, "Location");
            }
        }
    }

    protected void RepeaterServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer && RepeaterServices.Items.Count == 0)
        {
            Label lblNoData = (Label)e.Item.FindControl("lblNoData");
            if (lblNoData != null)
            {
                lblNoData.Visible = true;
            }
        }
    }



    private void LoadDropDownListWithAll(string query, DropDownList ddl, string columnName)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    ddl.Items.Clear();
                    ddl.Items.Add(new ListItem("All", string.Empty));

                    while (reader.Read())
                    {
                        string value = reader[columnName].ToString();
                        ListItem item = new ListItem(value, value);
                        ddl.Items.Add(item);

                      
                        if (!IsPostBack && ddl.SelectedValue == value)
                        {
                            item.Selected = true;
                        }
                    }


                    reader.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("Error loading filter options: " + ex.Message);
                }
            }
        }
    }

    private void LoadSubcategories(string selectedCategory)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT DISTINCT Subcategory FROM Services WHERE Category = @SelectedCategory";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SelectedCategory", selectedCategory);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                ddlSubcategory.Items.Clear();
                ddlSubcategory.Items.Add(new ListItem("All", string.Empty));

                while (reader.Read())
                {
                    ddlSubcategory.Items.Add(reader["Subcategory"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Response.Write("An error occurred while loading subcategories: " + ex.Message);
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
       
        ddlCategory.SelectedIndex = 0;
        ddlSubcategory.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0; 

        
        LoadServices();
    }


    private void LoadLocations(string selectedCategory)
    {
        LoadLocations(selectedCategory, null);
    }

    private void LoadLocations(string selectedCategory, string selectedSubcategory)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT DISTINCT Location FROM Services WHERE Category = @SelectedCategory";

            if (!string.IsNullOrEmpty(selectedSubcategory))
            {
                query += " AND Subcategory = @SelectedSubcategory";
            }

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SelectedCategory", selectedCategory);

            if (!string.IsNullOrEmpty(selectedSubcategory))
            {
                cmd.Parameters.AddWithValue("@SelectedSubcategory", selectedSubcategory);
            }

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                ddlLocation.Items.Clear();
                ddlLocation.Items.Add(new ListItem("All", string.Empty));

                while (reader.Read())
                {
                    ddlLocation.Items.Add(reader["Location"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Response.Write("An error occurred while loading locations: " + ex.Message);
            }
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadFilterOptions(ddlCategory.SelectedValue);
        LoadServices();
    }

    protected void ddlSubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLocations(ddlCategory.SelectedValue, ddlSubcategory.SelectedValue);
        LoadServices();
    }


    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadServices();
    }


    private string GetSelectedFiltersText()
    {
        
        List<string> selectedFilters = new List<string>();

        if (!string.IsNullOrEmpty(ddlCategory.SelectedValue) && ddlCategory.SelectedValue != "All")
        {
            selectedFilters.Add(" Showing Result For Category: " + ddlCategory.SelectedValue);
        }

        if (!string.IsNullOrEmpty(ddlSubcategory.SelectedValue) && ddlSubcategory.SelectedValue != "All")
        {
            selectedFilters.Add("Subcategory: " + ddlSubcategory.SelectedValue);
        }
        else if (!string.IsNullOrEmpty(ddlCategory.SelectedValue) && ddlSubcategory.SelectedValue == "All")
        {
          
            selectedFilters.Add("Category: " + ddlCategory.SelectedValue);
        }

        if (!string.IsNullOrEmpty(ddlLocation.SelectedValue) && ddlLocation.SelectedValue != "All")
        {
            selectedFilters.Add("Location: " + ddlLocation.SelectedValue);
        }
        else if (!string.IsNullOrEmpty(ddlCategory.SelectedValue) && ddlLocation.SelectedValue == "All" && ddlSubcategory.SelectedValue == "All")
        {
           
            selectedFilters.Add("Category: " + ddlCategory.SelectedValue);
        }

       
        return string.Join(" | ", selectedFilters.ToArray());
    }


    
    private void LoadServices()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT s.ServiceID, s.ServiceName, s.ImagePath, s.Price, s.Location, r.OverallRating " +
                           "FROM Services s " +
                           "LEFT JOIN (SELECT ServiceID, AVG(CAST(RatingValue AS FLOAT)) AS OverallRating " +
                           "           FROM Rating GROUP BY ServiceID) r ON s.ServiceID = r.ServiceID " +
                           "WHERE s.ApprovalStatus = 'Approved'";

            if (!string.IsNullOrEmpty(ddlCategory.SelectedValue) && ddlCategory.SelectedValue != "All")
            {
                query += " AND s.Category = @Category";
            }

            if (!string.IsNullOrEmpty(ddlSubcategory.SelectedValue) && ddlSubcategory.SelectedValue != "All")
            {
                query += " AND s.Subcategory = @Subcategory";
            }

            if (!string.IsNullOrEmpty(ddlLocation.SelectedValue) && ddlLocation.SelectedValue != "All")
            {
                query += " AND s.Location = @Location";
            }

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Category", ddlCategory.SelectedValue);
                command.Parameters.AddWithValue("@Subcategory", ddlSubcategory.SelectedValue);
                command.Parameters.AddWithValue("@Location", ddlLocation.SelectedValue);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    RepeaterServices.DataSource = reader;
                    RepeaterServices.DataBind();

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
        }

      
        string selectedFilters = GetSelectedFiltersText();
        lblSelectedFilters.Text = selectedFilters;
    }

}
