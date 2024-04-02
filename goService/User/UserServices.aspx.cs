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


public partial class User_UserServices : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
    private FileUpload fileUploadImage;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadServices(GetLoggedInUserId());
        }
    }

    protected void LoadServices(int userId)
    {
        string query = "SELECT * FROM Services WHERE ProviderID = @UserID";
        DataTable dataTable = ExecuteQuery(query, new SqlParameter("@UserID", userId));

        gvServices.DataSource = dataTable;
        gvServices.DataBind();
    }

    private int GetLoggedInUserId()
    {
        if (Session["UserID"] != null)
        {
            int userId;
            if (int.TryParse(Session["UserID"].ToString(), out userId))
            {
                return userId;
            }
        }

        Response.Redirect("~/Login.aspx");
        return -1;
    }


    protected void gvServices_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvServices.EditIndex = e.NewEditIndex;
        LoadServices(GetLoggedInUserId());
    }

    protected void gvServices_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        fileUploadImage = (FileUpload)gvServices.Rows[e.RowIndex].FindControl("fileUploadImage");

        int serviceID = Convert.ToInt32(gvServices.DataKeys[e.RowIndex].Value);
        TextBox txtServiceName = (TextBox)gvServices.Rows[e.RowIndex].FindControl("txtServiceName");
        TextBox txtDescription = (TextBox)gvServices.Rows[e.RowIndex].FindControl("txtDescription");
        TextBox txtCategory = (TextBox)gvServices.Rows[e.RowIndex].FindControl("txtCategory");
        TextBox txtSubcategory = (TextBox)gvServices.Rows[e.RowIndex].FindControl("txtSubcategory");
        TextBox txtLocation = (TextBox)gvServices.Rows[e.RowIndex].FindControl("txtLocation");
        TextBox txtPrice = (TextBox)gvServices.Rows[e.RowIndex].FindControl("txtPrice");

        if (txtServiceName != null && txtDescription != null && txtCategory != null &&
            txtSubcategory != null && txtLocation != null && txtPrice != null)
        {
            string serviceName = txtServiceName.Text;
            string description = txtDescription.Text;
            string category = txtCategory.Text;
            string subcategory = txtSubcategory.Text;
            string location = txtLocation.Text;
            decimal price = Convert.ToDecimal(txtPrice.Text);

            UpdateService(serviceID, serviceName, description, category, subcategory, location, price, fileUploadImage);

            gvServices.EditIndex = -1;
            LoadServices(GetLoggedInUserId());
        }
    }

    protected void gvServices_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvServices.EditIndex = -1;
        LoadServices(GetLoggedInUserId());
    }

    protected void gvServices_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int serviceID = Convert.ToInt32(gvServices.DataKeys[e.RowIndex].Value);
        DeleteService(serviceID);
        LoadServices(GetLoggedInUserId());
    }

    private void UpdateService(int serviceID, string serviceName, string description, string category, string subcategory, string location, decimal price, FileUpload fileUploadImage)
    {
        string query;
        SqlParameter[] parameters;

        if (fileUploadImage.HasFile)
        {
            
            query = "UPDATE Services SET ServiceName = @ServiceName, Description = @Description, " +
                    "Category = @Category, Subcategory = @Subcategory, Location = @Location, " +
                    "Price = @Price, ImagePath = @ImagePath " +
                    "WHERE ServiceID = @ServiceID";

            parameters = new SqlParameter[]
        {
            new SqlParameter("@ServiceName", serviceName),
            new SqlParameter("@Description", description),
            new SqlParameter("@Category", category),
            new SqlParameter("@Subcategory", subcategory),
            new SqlParameter("@Location", location),
            new SqlParameter("@Price", price),
            new SqlParameter("@ServiceID", serviceID),
            new SqlParameter("@ImagePath", SaveImageAndGetPath(fileUploadImage))
        };
        }
        else
        {
            
            query = "UPDATE Services SET ServiceName = @ServiceName, Description = @Description, " +
                    "Category = @Category, Subcategory = @Subcategory, Location = @Location, " +
                    "Price = @Price " +
                    "WHERE ServiceID = @ServiceID";

            parameters = new SqlParameter[]
        {
            new SqlParameter("@ServiceName", serviceName),
            new SqlParameter("@Description", description),
            new SqlParameter("@Category", category),
            new SqlParameter("@Subcategory", subcategory),
            new SqlParameter("@Location", location),
            new SqlParameter("@Price", price),
            new SqlParameter("@ServiceID", serviceID)
        };
        }

        ExecuteNonQuery(query, parameters);
    }

    private string SaveImageAndGetPath(FileUpload fileUpload)
    {
        
        string imageDirectory = Server.MapPath("Upload/");

        
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



    private void DeleteService(int serviceID)
    {
        string query = "DELETE FROM Services WHERE ServiceID = @ServiceID";
        ExecuteNonQuery(query, new SqlParameter("@ServiceID", serviceID));
    }

    private DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                adapter.SelectCommand.Parameters.AddRange(parameters);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
        }
    }

    private void ExecuteNonQuery(string query, params SqlParameter[] parameters)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    protected void gvServices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FileUpload fileUploadImage = (FileUpload)e.Row.FindControl("fileUploadImage");
            Image imgService = (Image)e.Row.FindControl("imgService");

            if (fileUploadImage != null && imgService != null)
            {
                if (gvServices.EditIndex == e.Row.RowIndex)
                {
                    
                    fileUploadImage.Visible = true;
                    imgService.Visible = false;
                }
                else
                {
                    
                    fileUploadImage.Visible = false;
                    imgService.Visible = true;
                }
            }
        }
    }


}
