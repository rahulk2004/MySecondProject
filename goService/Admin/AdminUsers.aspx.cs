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

public partial class Admin_AdminUsers : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUsers();
        }
    }



    protected void LoadUsers()
    {
       
        string userID = null;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM [User]";

           
            if (GridViewUsers.EditIndex >= 0)
            {
                
                userID = GridViewUsers.DataKeys[GridViewUsers.EditIndex].Value.ToString();
                query += " WHERE UserID = @UserID";
            }

           
            query += " ORDER BY CASE WHEN ApprovalStatus = 'Pending' THEN 1 WHEN ApprovalStatus = 'Approved' THEN 2 ELSE 3 END";

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

               
                if (GridViewUsers.EditIndex >= 0)
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                }

                GridViewUsers.DataSource = cmd.ExecuteReader();
                GridViewUsers.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }



    protected void GridViewUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewUsers.EditIndex = e.NewEditIndex;
        LoadUsers();
    }

    protected void GridViewUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewUsers.EditIndex = -1;
        LoadUsers();
    }

    protected void GridViewUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string userID = GridViewUsers.DataKeys[e.RowIndex].Value.ToString();
        RadioButton rbApproved = (RadioButton)GridViewUsers.Rows[e.RowIndex].FindControl("rbApproved");
        RadioButton rbRejected = (RadioButton)GridViewUsers.Rows[e.RowIndex].FindControl("rbRejected");

        bool approvalStatus = rbApproved.Checked;

        UpdateUserApprovalStatus(userID, approvalStatus);

        GridViewUsers.EditIndex = -1;
        LoadUsers();

        lblSubmissionStatus.Text = "Update successful";
        lblSubmissionStatus.Visible = true;
    }


    private void UpdateUserApprovalStatus(string userID, bool approvalStatus)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string statusString = approvalStatus ? "Approved" : "Rejected";
            string query = "UPDATE [User] SET ApprovalStatus = @ApprovalStatus WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@ApprovalStatus", SqlDbType.VarChar).Value = statusString;
            command.Parameters.AddWithValue("@UserID", userID);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }



    protected void GridViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && GridViewUsers.EditIndex == e.Row.RowIndex)
        {
           
            foreach (TableCell cell in e.Row.Cells)
            {
                
                foreach (Control control in cell.Controls)
                {
                   
                    TextBox textBox = control as TextBox;
                    if (textBox != null)
                    {
                        
                        textBox.Enabled = false;
                    }
                }
            }

           
            RadioButton rbApproved = (RadioButton)e.Row.FindControl("rbApproved");
            RadioButton rbRejected = (RadioButton)e.Row.FindControl("rbRejected");
            Label lblApprovalStatus = (Label)e.Row.FindControl("lblApprovalStatus");

           
            rbApproved.Enabled = true;
            rbRejected.Enabled = true;

            
            if (Convert.ToBoolean(lblApprovalStatus.Text))
            {
                rbApproved.Checked = true;
            }
            else
            {
                rbRejected.Checked = true;
            }

           
        }
    }

}
