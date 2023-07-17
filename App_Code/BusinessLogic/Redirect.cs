using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for redirect
/// </summary>
public class Redirect
{
	public Redirect()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void RedirectPermanent(string newPath)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Status = "301 Moved Permanently";
        HttpContext.Current.Response.AddHeader("Location", newPath);
        HttpContext.Current.Response.End();
    }
}
