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

public partial class Ombudsman_PreviousRTIPageOmbudsman : BasePageOmbudsman
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
    RtiBL obj_rtBL = new RtiBL();
    PetitionOB obj_petOB = new PetitionOB();


    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        str = BreadcrumDL.DisplayBreadCrumRTIOmbudsman(Resources.HercResource.RTI, Resources.HercResource.PreviousYearRti);
        ltrlBreadcrum.Text = str.ToString();
       // p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Rti"] + "/");
        
        p_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        p_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        if (!IsPostBack)
        {
           
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssRef);
        }
    }


    protected void lnkFAA_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/PreviousFAAOmbudsman/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/OmbudsmanContent/Hindi/PreviousFAAOmbudsman/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
    }
    protected void lnkSAA_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/PreviousSAAOmbudsman/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/OmbudsmanContent/Hindi/PreviousSAAOmbudsman/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
    }
    protected void lnkRTI_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/perviousrti/" + p_Val.PageID + "_" + p_Val.position) + "_Previousyearapplications.aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/OmbudsmanContent/Hindi/perviousrti/" + p_Val.PageID + "_" + p_Val.position) + "_Previousyearapplications.aspx");
        }
    }
}
