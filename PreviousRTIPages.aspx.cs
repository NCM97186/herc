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

public partial class PreviousRTIPages : BasePage
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
    RtiBL obj_rtBL = new RtiBL();
    PetitionOB obj_petOB = new PetitionOB();
    Miscelleneous_BL obj = new Miscelleneous_BL();

    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        str = BreadcrumDL.DisplayBreadCrumRTI(Resources.HercResource.RTI, Resources.HercResource.PreviousYearApplications);
        ltrlBreadcrum.Text = str.ToString();
        p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Rti"] + "/");
       // obj_miscel.MakeAccessible(Grdappeal);

        p_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        p_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();

        if (!IsPostBack)
        {
            //Bind_RTI(1);
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
    }

    protected void lnkFAA_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/PreviousFAA/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/Content/Hindi/PreviousFAA/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
    }
    protected void lnkSAA_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/PreviousSAA/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/Content/Hindi/PreviousSAA/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
    }
    protected void lnkRTI_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/PrevRTI/" + p_Val.PageID + "_" + p_Val.position) + "_Previousyearsapplications.aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/Content/Hindi/PrevRTI/" + p_Val.PageID + "_" + p_Val.position) + "_Previousyearsapplications.aspx");
        }
    }
}
