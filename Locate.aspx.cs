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

public partial class Locate :BasePage
{
    #region variable declaration

    string str = string.Empty;
	string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;

    #endregion 

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.ContactUs, Resources.HercResource.LocateUs);
        ltrlBreadcrum.Text = str.ToString();

		if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.LocateUs;
        }
        else
        {
            PageTitle = Resources.HercResource.LocateUs;
        }
    }

    #endregion 

	#region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion
}
