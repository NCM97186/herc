using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class AdminPanel_ConfirmationPage : System.Web.UI.Page
{
    #region Data Declaration Zone

    public string page = string.Empty;

    #endregion

    #region Page Load Event Zone

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Username"] == null)
        //{
        //    Response.Redirect("~/AdminPanel/Login.aspx");
        //}
        if (Session["msg"] != null)
        {
            lblmsg.Text = Session["msg"].ToString();
        }
        if (Session["Redirect"] != null)
        {
            page = Session["Redirect"].ToString();
        }
      //  Session["Language"] = Session["Language"].ToString();
       
    }

    #endregion

    #region button btnBack click event zone to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session.Remove("msg");
        Session.Remove("Redirect");
    //    Session["Language"] = Session["Language"].ToString();
        Response.Redirect(page);
        
    }

    #endregion
}
