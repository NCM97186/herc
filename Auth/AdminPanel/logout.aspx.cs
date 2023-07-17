using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class signout : System.Web.UI.Page
{
    #region Page Init Zone

    protected void Page_Init(object sender, System.EventArgs e)
    {
        Page.ViewStateUserKey = Session.SessionID;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }

    #endregion 

    #region Page Load Zone

    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session.Abandon();
		if (Request.Cookies["ASP.NET_SessionId"] != null)
        {
            Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
        }
        DisableCache();
    }
    private void DisableCache()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache); //Cache-Control : no-cache, Pragma : no-cache
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1)); //Expires : date time
        Response.Cache.SetNoStore(); //Cache-Control :  no-store
        Response.Cache.SetProxyMaxAge(new TimeSpan(0, 0, 0)); //Cache-Control: s-maxage=0
        Response.Cache.SetValidUntilExpires(false);
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);//Cache-Control:  must-revalidate
    }

    #endregion 
    protected void btnEmail_Click(object sender, EventArgs e)
    {

    }
    protected void btnMyMail_Click(object sender, EventArgs e)
    {

    }
}
