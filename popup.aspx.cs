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
using System.IO;


public partial class popup : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnpop_Click(object sender, EventArgs e)
    {
        Project_Variables p_val = new Project_Variables();

       p_val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewDetails.aspx?Soh_id=" + p_val.stringTypeID) + "', + new Date().getTime()," +
                                       "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                       "</script>";
                    this.Page.RegisterStartupScript("PopupScript", p_val.strPopupID);
    }
}