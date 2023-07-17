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

public partial class RtiPages : BasePage
{
    #region variable declaration

    string str = string.Empty;
    Project_Variables p_Val = new Project_Variables();
    PetitionOB obj_petOB = new PetitionOB();
    RtiBL obj_rtBL = new RtiBL();
    PaginationBL pagingBL = new PaginationBL();
    Miscelleneous_BL obj = new Miscelleneous_BL();

    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        p_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Rti"] + "/");
        // str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.RTI, Resources.HercResource.CurrentYearApplications);
        str = BreadcrumDL.DisplayBreadCrumRTI(Resources.HercResource.RTI, Resources.HercResource.CurrentYearApplications);
        ltrlBreadcrum.Text = str.ToString();
        if (!IsPostBack)
        {
           // Bind_RTI(1);
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
    }

    protected void lnkFAA_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/CurrentFAA/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/Content/Hindi/CurrentFAA/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
    }
    protected void lnkSAA_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/CurrentSAA/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/Content/Hindi/CurrentSAA/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
    }

    protected void lnkRTI_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/CurrentRTI/" + p_Val.PageID + "_" + p_Val.position) + "_Currentyearapplications.aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/Content/Hindi/CurrentRTI/" + p_Val.PageID + "_" + p_Val.position) + "_Currentyearapplications.aspx");
        }
    }
}
