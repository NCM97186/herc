using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AdminPanel_message : System.Web.UI.Page
{
    #region Data Declaration Zone

    public string page = string.Empty;

    #endregion

    #region code for PreInIt event
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.ViewStateUserKey = Session.SessionID;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Page.Theme = "";
    }
    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["msg"] != null)
        {
            lblMsg.Text = Session["msg"].ToString();
        }
        if (Session["Redirect"] != null)
        {
            page = Session["Redirect"].ToString();
        }
    }

    #region button btnBack click event zone to go back

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Session.Remove("msg");
        Session.Remove("Redirect");
        Response.Redirect(page);
    }

    #endregion

}