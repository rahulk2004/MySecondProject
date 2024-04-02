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

public partial class User_UserProfile : System.Web.UI.Page
{
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            LoadUserProfile();
        }
    }

    private void LoadUserProfile()
    {
       
        int userId = GetUserIdFromSession();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM [User] WHERE UserID = @UserID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                       
                        txtUsername.Text = reader["UserName"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        txtContactNumber.Text = reader["ContactNumber"].ToString();
                        ddlGender.SelectedValue = reader["Gender"].ToString();
                        txtShopName.Text = reader["ShopName"].ToString();

                       
                        string imagePath = reader["Image"].ToString();
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            imgUser.ImageUrl = imagePath;
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    
                    Response.Write("Error loading user profile: " + ex.Message);
                }
            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
       
        EnableEditMode(true);

        
        btnUpdate.Visible = true;
        btnEdit.Visible = false;
        fileUploadImage.Visible = true;
        txtShopName.ReadOnly = false;
        txtUsername.ReadOnly = false;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
       
        int userId = GetUserIdFromSession();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query;
            SqlCommand command;

           
            if (fileUploadImage.HasFile)
            {
                query = "UPDATE [User] SET UserName = @UserName, Email = @Email, Address = @Address, ContactNumber = @ContactNumber, " +
                        "Gender = @Gender, ShopName = @ShopName, Image = @Image " +
                        "WHERE UserID = @UserID";

                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Image", SaveImageAndGetPath(fileUploadImage));
            }
            else
            {
                query = "UPDATE [User] SET UserName = @UserName, Email = @Email, Address = @Address, ContactNumber = @ContactNumber, " +
                        "Gender = @Gender, ShopName = @ShopName " +
                        "WHERE UserID = @UserID";

                command = new SqlCommand(query, connection);
            }

            
            command.Parameters.AddWithValue("@UserName", txtUsername.Text);
            command.Parameters.AddWithValue("@Email", txtEmail.Text);
            command.Parameters.AddWithValue("@Address", txtAddress.Text);
            command.Parameters.AddWithValue("@ContactNumber", txtContactNumber.Text);
            command.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
            command.Parameters.AddWithValue("@ShopName", txtShopName.Text);
            command.Parameters.AddWithValue("@UserID", userId);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    
                    EnableEditMode(false);

                    
                    btnUpdate.Visible = false;
                    btnEdit.Visible = true;

                    
                    LoadUserProfile();
                }
                else
                {
                   
                    Response.Write("Profile update failed.");
                }
            }
            catch (Exception ex)
            {
               
                Response.Write("Error updating user profile: " + ex.Message);
            }
        }
    }

    private string SaveImageAndGetPath(FileUpload fileUpload)
    {
        
        string imageDirectory = Server.MapPath("~/User/Upload/");
        
        string imagePath = "";
        if (fileUpload.HasFile)
        {
            string fileName = Path.GetFileName(fileUpload.FileName);
            imagePath = Path.Combine(imageDirectory, fileName);
            fileUpload.SaveAs(imagePath);
            imagePath = "~/User/Upload/" + fileName;
        }

        return imagePath;
    }

    private int GetUserIdFromSession()
    {
       
        if (Session["UserID"] != null)
        {
            return Convert.ToInt32(Session["UserID"]);
        }
        else
        {
            return -1;
        }
    }

    private void EnableEditMode(bool enable)
    {
        
        txtUsername.Enabled = enable;
        txtEmail.Enabled = false;   
        txtAddress.Enabled = enable;
        txtContactNumber.Enabled = enable;
        ddlGender.Enabled = enable;
        txtShopName.Enabled = enable; 
    }
}
