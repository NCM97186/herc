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

public partial class Ombudsman_OmbudsmanWhatsNew : BasePageOmbudsman
{
    #region Data delcaration zone

    Menu_ManagementBL menuBL = new Menu_ManagementBL();

    LinkOB lnkObject = new LinkOB();

    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();

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
    string str = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Val.Path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Pdf"] + "/";
        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.WhatNew);
        ltrlBreadcrum.Text = str.ToString();



        if (ViewState["lastUpdateDate"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate"].ToString();
        }

        if (!IsPostBack)
        {
            Bind_Award_Grid(1);
            Bind_AppealWhatsNew(1);
        }
        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.WhatNew;
        }
        else
        {
            PageTitle = Resources.HercResource.WhatNew;
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

    public void Bind_Award_Grid(int pageindex)
    {
        obj_petOB.PageIndex = pageindex;
        obj_petOB.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        obj_petOB.LangId = Convert.ToInt32(Resources.HercResource.Lang_Id);
        obj_petOB.DepttId = 2; //Dept Id Ombandson
        obj_petOB.AppealNo = null;
        p_Val.dSet = obj_petBL.GetLatestAward(obj_petOB, out p_Val.k);
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            hAward.InnerText = "Award Pronounced";
            rptWhatsNew.DataSource = p_Val.dSet;
            rptWhatsNew.DataBind();
            lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            ViewState["lastUpdateDate"] = lastUpdatedDate;
        }

        p_Val.Result = p_Val.k;
       
    }

    public void Bind_AppealWhatsNew(int pageindex)
    {
        obj_petOB.PageIndex = pageindex;
        obj_petOB.PageSize = 50;
        obj_petOB.LangId = Convert.ToInt32(Resources.HercResource.Lang_Id);
        obj_petOB.DepttId = 2; //Dept Id Ombandson
        obj_petOB.AppealNo = null;
        p_Val.dSet = obj_petBL.GetLatestAppeal(obj_petOB, out p_Val.k);
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            hAppeal.InnerHtml = "Appeal";
            rptAppealWhatsNew.DataSource = p_Val.dSet;
            rptAppealWhatsNew.DataBind();
            lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            ViewState["lastUpdateDate"] = lastUpdatedDate;
        }

        p_Val.Result = p_Val.k;
      
    }

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Bind_Award_Grid(pageIndex);
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Award_Grid(1);
    }

    #endregion

    #region repeater rptWhatsNew itemCommand latest update

    protected void rptWhatsNew_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {

            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                p_Val.stringTypeID = e.CommandArgument.ToString();
                p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/ViewDetailsWhatsNew.aspx?ID=" + e.CommandArgument) + "', 'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            else
            {

                p_Val.stringTypeID = e.CommandArgument.ToString();
                p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/OmbudsmanContent/Hindi/ViewDetailsWhatsNew.aspx?ID=" + e.CommandArgument) + "', 'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=Yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }

            //p_Val.str = e.CommandArgument.ToString();

        }
    }

    #endregion

    #region repeater rptWhatsNew itemDataBound latest update

    protected void rptWhatsNew_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

        }
    }

    #endregion

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitleOmbudsman;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion


    protected void rptAppealWhatsNew_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "viewAppeal")
        {

            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                p_Val.stringTypeID = e.CommandArgument.ToString();
                p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Ombudsman/ViewAppealDetails.aspx?Appealid=" + e.CommandArgument) + "','blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            else
            {

                p_Val.stringTypeID = e.CommandArgument.ToString();
                p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/OmbudsmanContent/Hindi/ViewAppealDetails.aspx?Appealid=" + e.CommandArgument) + "', 'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=Yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
    }


}

