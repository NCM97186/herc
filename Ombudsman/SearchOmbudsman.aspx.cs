using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Ombudsman_SearchOmbudsman : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.Search);
        ltrlBreadcrum.Text = str;
    }
}