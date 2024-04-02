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
using System.IO;

public partial class User_UserAddService : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCategories();
            LoadSubcategories(ddlCategory.SelectedValue);
            LoadLocations(ddlSubcategory.SelectedValue);
        }
    }

    protected void btnAddService_Click(object sender, EventArgs e)
    {
        int providerID = GetProviderIDFromSession();

        string serviceName = txtServiceName.Text;
        string description = txtDescription.Text;
        string category = ddlCategory.SelectedValue == "AddNew" ? TextBox1.Text : ddlCategory.SelectedValue;
        string subcategory = ddlSubcategory.SelectedValue == "AddNew" ? txtSubcategory.Text : ddlSubcategory.SelectedValue;
        string location = ddlLocation.SelectedValue == "AddNew" ? TextBox3.Text : ddlLocation.SelectedValue;
        decimal price = Convert.ToDecimal(txtPrice.Text);

        string approvalStatus = "Pending";

        string filePath = Server.MapPath("Upload/");
        string fileName = System.IO.Path.GetFileName(FileUploadService.FileName);
        string extension = System.IO.Path.GetExtension(fileName);
        HttpPostedFile postedFile = FileUploadService.PostedFile;
        int size = postedFile.ContentLength;

        if (FileUploadService.HasFile && IsImageFile(extension) && size <= 5000000)
        {
            string imagePath = "~/User/Upload/" + fileName;
            FileUploadService.SaveAs(System.IO.Path.Combine(filePath, fileName));

            AddServiceWithImage(providerID, serviceName, description, category, subcategory, location, price, approvalStatus, imagePath);

            Response.Redirect("UserServices.aspx");
        }
        else
        {
          
        }
    }

    private void LoadCategories()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT DISTINCT Category FROM Services";
            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                ddlCategory.Items.Clear();
                ddlCategory.Items.Add(new ListItem("Select Existing Category", string.Empty));
                ddlCategory.Items.Add(new ListItem("Add New Category", "AddNew"));

                while (reader.Read())
                {
                    ddlCategory.Items.Add(reader["Category"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while loading categories: " + ex.Message);
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
                ddlSubcategory.Items.Add(new ListItem("Select Existing Subcategory", string.Empty));
                ddlSubcategory.Items.Add(new ListItem("Add New Subcategory", "AddNew"));

                while (reader.Read())
                {
                    ddlSubcategory.Items.Add(reader["Subcategory"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while loading subcategories: " + ex.Message);
            }
        }
    }

    private void LoadLocations(string selectedSubcategory)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT DISTINCT Location FROM Services WHERE Subcategory = @SelectedSubcategory";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SelectedSubcategory", selectedSubcategory);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                ddlLocation.Items.Clear();
                ddlLocation.Items.Add(new ListItem("Select Existing Location", string.Empty));
                ddlLocation.Items.Add(new ListItem("Add New Location", "AddNew"));

                while (reader.Read())
                {
                    ddlLocation.Items.Add(reader["Location"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while loading locations: " + ex.Message);
            }
        }
    }

    private int GetProviderIDFromSession()
    {
        return Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : -1;
    }

    private void AddServiceWithImage(int providerID, string serviceName, string description, string category, string subcategory, string location, decimal price, string approvalStatus, string imagePath)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Services (ProviderID, ServiceName, Description, Category, Subcategory, Location, Price, ApprovalStatus, ImagePath) " +
                           "VALUES (@ProviderID, @ServiceName, @Description, @Category, @Subcategory, @Location, @Price, @ApprovalStatus, @ImagePath)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProviderID", providerID);
                command.Parameters.AddWithValue("@ServiceName", serviceName);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Category", category);
                command.Parameters.AddWithValue("@Subcategory", subcategory);
                command.Parameters.AddWithValue("@Location", location);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                command.Parameters.AddWithValue("@ImagePath", imagePath);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    private bool IsImageFile(string extension)
    {
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
        return Array.IndexOf(allowedExtensions, extension.ToLower()) != -1;
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubcategories(ddlCategory.SelectedValue);
        LoadLocations(ddlSubcategory.SelectedValue); 
        UpdateDropdownState(ddlCategory, TextBox1, rfvCategory, rfvNewCategory);
    }

    protected void ddlSubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLocations(ddlSubcategory.SelectedValue); 
        UpdateDropdownState(ddlSubcategory, txtSubcategory, rfvSubcategory, rfvTxtSubcategory);
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateDropdownState(ddlLocation, TextBox3, rfvLocation, rfvNewLocation);
    }

    private void UpdateDropdownState(DropDownList dropdown, TextBox textBox, RequiredFieldValidator dropdownValidator, RequiredFieldValidator newValidator)
    {
        if (dropdown.SelectedValue == "AddNew")
        {
            textBox.Enabled = true;
            dropdownValidator.Enabled = false;
            newValidator.Enabled = true;
        }
        else
        {
            textBox.Enabled = false;
            dropdownValidator.Enabled = true;
            newValidator.Enabled = false;
        }
    }
}
