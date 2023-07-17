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

public partial class RTI : BasePage
{
    #region Data delcaration zone

    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    Project_Variables p_var = new Project_Variables();
    LinkOB lnkObject = new LinkOB();
    public string headerName = string.Empty;
    public string ParentName = string.Empty;
    public static string UrlPrint = string.Empty;

    string RootParentName = string.Empty;
    string Childname = string.Empty;
    string strbreadcrum = string.Empty;
    string PositionID = HttpContext.Current.Request.QueryString["position"];
    string PageID = string.Empty; //HttpContext.Current.Request.QueryString["id"].ToString().Substring(6);
    string ParentID = string.Empty;
    string browserTitle = string.Empty;
    int RootID;

    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string PageTitle = string.Empty;
    public string lastUpdatedDate = string.Empty;

    #endregion

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        p_var.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);  //Convert.ToInt32(Request.QueryString["position"].ToString());
        p_var.LanguageID = Convert.ToInt16(Resources.HercResource.Lang_Id);
        p_var.url = Resources.HercResource.PageUrl.ToString();

        if (!IsPostBack)
        {

            if (RewriteModule.RewriteContext.Current.Params["menuid"] != null)
            {
                p_var.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString(); //Request.QueryString["menuid"].ToString();
            }
            if (p_var.PageID.Length > 6)
            {
                p_var.PageID = p_var.PageID.Substring(6);
            }
            else
            {
                p_var.PageID = p_var.PageID.ToString();
            }
            if (p_var.PageID != null)
            {
                bindMainContent();
            }

        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);
        }
        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.RTI;
        }
        else
        {
            PageTitle = Resources.HercResource.RTI;
        }
    }

    #endregion

    #region Function to bind Main Contents

    public void bindMainContent()
    {
        DataSet subMenuData = new DataSet();
        DataSet parent = new DataSet();
        LinkOB _lnkSubmenuObject = new LinkOB();

        //Variables to get child submenu

        LinkOB subSubMenuOB = new LinkOB();
        DataSet sub_dataSet = new DataSet();

        //end

        try
        {

            lnkObject.linkID = Convert.ToInt16(p_var.PageID);

            lnkObject.LangId = p_var.LanguageID;
            p_var.dSet = menuBL.get_Cliked_Parent_Menu(lnkObject);
            if (p_var.dSet.Tables[0].Rows.Count > 0)
            {
                p_var.parentID = p_var.dSet.Tables[0].Rows[0]["link_parent_id"].ToString();
                lnkObject.LinkParentId = Convert.ToInt16(p_var.parentID);
                p_var.position = Convert.ToInt16(p_var.dSet.Tables[0].Rows[0]["position_id"]);
                if (p_var.dSet.Tables[0].Rows.Count > 0)
                {
                    lastUpdatedDate = p_var.dSet.Tables[0].Rows[0]["Last_update"].ToString();
                    headerName = p_var.dSet.Tables[0].Rows[0]["name"].ToString();
                    ParentName = p_var.dSet.Tables[0].Rows[0]["parent"].ToString();
                    browserTitle = p_var.dSet.Tables[0].Rows[0]["Browser_Title"].ToString();

                    if (lnkObject.LinkParentId == 0)
                    {
                        hparentId.InnerText = headerName;
                    }
                    else
                    {
                        hparentId.InnerText = ParentName;
                    }

                    ltrlMainContent.Text = HttpUtility.HtmlDecode(p_var.dSet.Tables[0].Rows[0]["details"].ToString());
                    if (p_var.parentID != "0")
                    {

                        p_var.str = BreadcrumDL.DisplayBreadCrum(Convert.ToInt16(p_var.parentID), p_var.position, p_var.dSet.Tables[0].Rows[0]["parent"].ToString(), p_var.dSet.Tables[0].Rows[0]["name"].ToString());
                    }
                    else
                    {
                        p_var.str = BreadcrumDL.DisplayBreadCrum(p_var.dSet.Tables[0].Rows[0]["name"].ToString());
                    }
                    ltrlBreadcrum.Text = p_var.str;
                }
                if (Convert.ToInt16(p_var.dSet.Tables[0].Rows[0]["Link_Level"]) == 2)
                {
                    p_var.parentID = lnkObject.LinkParentId.ToString();
                    //Get Root name of for implimenting third level breadcrum
                    Menu_ManagementBL menuRootBL = new Menu_ManagementBL();
                    LinkOB linkObject = new LinkOB();
                    DataSet dDataSet = new DataSet();
                    linkObject.linkID = Convert.ToInt16(p_var.parentID);
                    linkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
                    dDataSet = menuRootBL.getParent_name_ofRoot(linkObject);
                    if (dDataSet.Tables[0].Rows.Count > 0)
                    {
                        RootParentName = dDataSet.Tables[0].Rows[0]["name"].ToString();
                        RootID = Convert.ToInt16(dDataSet.Tables[0].Rows[0]["link_id"]);
                    }

                    //End
                    Childname = p_var.dSet.Tables[0].Rows[0]["name"].ToString();
                    //strbreadcrum = BreadcrumDL.DisplayBreadCrum(ParentId, Convert.ToInt16(PositionID), Parent_name, Childname);
                    strbreadcrum = BreadcrumDL.DisplayBreadCrum(RootID, Convert.ToInt16(p_var.parentID), Convert.ToInt16(PositionID), RootParentName, ParentName, Childname);
                    ltrlBreadcrum.Text = strbreadcrum;
                }
            }

        }
        catch
        {

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
