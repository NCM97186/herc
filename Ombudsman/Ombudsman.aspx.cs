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

public partial class Ombudsman_Ombudsman : BasePageOmbudsman
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    public static string UrlPrint = string.Empty;
    public string lastUpdatedDate = string.Empty;
    public string headerName = string.Empty;
    LinkOB lnkObject = new LinkOB();
    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    #endregion 

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        if (!IsPostBack)
        {

            if (!Page.IsPostBack)
            {
                Bind_Ombudsman();
            }
        }
        if (Resources.HercResource.Lang_Id == Module_ID_Enum.Language_ID.English.ToString())
        {
            PageTitle = Resources.HercResource.HomePage;
        }
        else
        {
            PageTitle = Resources.HercResource.HomePage;
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

    #endregion

   


    public void Bind_Ombudsman()
    {
        lnkObject.LangId =Convert.ToInt16(Resources.HercResource.Lang_Id);
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {
            lnkObject.LinkParentId =Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.OmbudsmanOverviewParent);
        }
        else
        {
            lnkObject.LinkParentId = Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.OmbudsmanOverviewParentHindi); 
        }
        p_Val.dsFileName = menuBL.get_Cliked_Parent_MenuForOmbudsman(lnkObject);
        if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
        {
            p_Val.parentID = p_Val.dsFileName.Tables[0].Rows[0]["link_parent_id"].ToString();
            ltrlMainContent.Text = HttpUtility.HtmlDecode(p_Val.dsFileName.Tables[0].Rows[0]["details"].ToString());
            headerName = HttpUtility.HtmlDecode(p_Val.dsFileName.Tables[0].Rows[0]["name"].ToString());
            lastUpdatedDate = p_Val.dsFileName.Tables[0].Rows[0]["Last_update"].ToString();

            p_Val.str = BreadcrumDL.DisplayBreadCrumOmbudsman(Convert.ToInt16(p_Val.parentID), p_Val.position, p_Val.dsFileName.Tables[0].Rows[0]["parent"].ToString(), p_Val.dsFileName.Tables[0].Rows[0]["name"].ToString());
            ltrlBreadcrum.Text = p_Val.str;
        }
        
    }

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {
        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitleOmbudsman;
        
    }

    #endregion


}
