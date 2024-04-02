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

public partial class Admin_AdminServices : System.Web.UI.Page
{
    private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

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
            string query = "SELECT s.ServiceID, s.ServiceName, s.Description, s.Category, s.Subcategory, s.Location, s.Price, u.UserName, s.ApprovalStatus, s.ImagePath FROM Services s INNER JOIN [User] u ON s.ProviderID = u.UserID ";

            string serviceID = null;


            if (GridViewServices.EditIndex >= 0)
            {

                serviceID = GridViewServices.DataKeys[GridViewServices.EditIndex].Value.ToString();
                query += "WHERE s.ServiceID = @ServiceID ";
            }

            query += "ORDER BY CASE WHEN s.ApprovalStatus = 'Pending' THEN 1 WHEN s.ApprovalStatus = 'Approved' THEN 2 ELSE 3 END";

            SqlCommand cmd = new SqlCommand(query, connection);


            if (GridViewServices.EditIndex >= 0)
            {
                cmd.Parameters.AddWithValue("@ServiceID", serviceID);
            }

            try
            {
                connection.Open();
                GridViewServices.DataSource = cmd.ExecuteReader();
                GridViewServices.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }

    protected void GridViewServices_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewServices.EditIndex = e.NewEditIndex;
        LoadServices();
    }

    protected void GridViewServices_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewServices.EditIndex = -1;
        LoadServices();
    }

    protected void GridViewServices_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string serviceID = GridViewServices.DataKeys[e.RowIndex].Value.ToString();
        RadioButton rbApproved = (RadioButton)GridViewServices.Rows[e.RowIndex].FindControl("rbApproved");
        RadioButton rbRejected = (RadioButton)GridViewServices.Rows[e.RowIndex].FindControl("rbRejected");

        string approvalStatus = rbApproved.Checked ? "Approved" : "Rejected";

        UpdateApprovalStatus(serviceID, approvalStatus);

        GridViewServices.EditIndex = -1;
        LoadServices();

        lblSubmissionStatus.Text = "Update successful";
        lblSubmissionStatus.Visible = true;
    }

    private void UpdateApprovalStatus(string serviceID, string approvalStatus)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Services SET ApprovalStatus = @ApprovalStatus WHERE ServiceID = @ServiceID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
            command.Parameters.AddWithValue("@ServiceID", serviceID);

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

    protected void GridViewServices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
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

            if ((string)lblApprovalStatus.Text == "Approved")
            {
                rbApproved.Checked = true;
            }
            else if ((string)lblApprovalStatus.Text == "Rejected")
            {
                rbRejected.Checked = true;
            }
        }
    }

}
