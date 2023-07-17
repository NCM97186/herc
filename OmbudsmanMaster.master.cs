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

public partial class Ombudsman_OmbudsmanMaster : System.Web.UI.MasterPage
{
    private void Page_PreInit(object sender, System.EventArgs e)
    {

        Page.Theme = "Blue";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
    }
}
