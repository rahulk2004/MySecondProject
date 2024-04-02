using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class User_UserMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null && Session["UserName"] != null)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            string userName = Session["UserName"].ToString();

            lblWelcomeMessage.Text = "Welcome, " + userName;
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
}
