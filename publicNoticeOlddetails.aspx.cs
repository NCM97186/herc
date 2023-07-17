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

public partial class publicNoticeOlddetails :BasePage
{
    #region Private Properties Declaration zone

    private int CurrentPage
    {
        get
        {
            object objPage = ViewState["_CurrentPage"];
            int _CurrentPage = 0;
            if (objPage == null)
            {
                _CurrentPage = 0;
            }
            else
            {
                _CurrentPage = (int)objPage;
            }
            return _CurrentPage;
        }
        set
        {
            ViewState["_CurrentPage"] = value;
        }
    }
    private int fistIndex
    {
        get
        {

            int _FirstIndex = 0;
            if (ViewState["_FirstIndex"] == null)
            {
                _FirstIndex = 0;
            }
            else
            {
                _FirstIndex = Convert.ToInt32(ViewState["_FirstIndex"]);
            }
            return _FirstIndex;
        }
        set
        {
            ViewState["_FirstIndex"] = value;
        }
    }
    private int lastIndex
    {
        get
        {

            int _LastIndex = 0;
            if (ViewState["_LastIndex"] == null)
            {
                _LastIndex = 0;
            }
            else
            {
                _LastIndex = Convert.ToInt32(ViewState["_LastIndex"]);
            }
            return _LastIndex;
        }
        set
        {
            ViewState["_LastIndex"] = value;
        }
    }

    #endregion

    #region PagedDataSource

    PagedDataSource _PageDataSource = new PagedDataSource();

    #endregion


    #region variable declaration zone

    LinkOB objlinkOB = new LinkOB();
    PublicNoticeBL pubNoticeBL = new PublicNoticeBL();
    Project_Variables p_val = new Project_Variables();
    PetitionOB publicNoticeObject = new PetitionOB();
    PaginationBL pagingBL = new PaginationBL();
    string str = string.Empty;
    public static string UrlPrint = string.Empty;
    public string lastUpdatedDate = string.Empty;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        p_val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["pdf"] + "/");

        str = BreadcrumDL.DisplayMemberAreaBreadCrumpubNoticeold(Resources.HercResource.PreviousPublicNotice);
        ltrlBreadcrum.Text = str.ToString();
        bool IsPageRefresh = false;
        
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
            {

            }
            else
            {
                Session.Remove("yearnew");
            }
            ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
            Session["SessionId"] = ViewState["ViewStateId"].ToString();
            if (string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                BindYear();
                bindPublicNoticDetails(1);
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
          

        }
        else
        {
            if (ViewState["ViewStateId"] != null && Session["SessionId"] != null)
            {
                if (ViewState["ViewStateId"].ToString() != Session["SessionId"].ToString())
                {
                    IsPageRefresh = true;
                }
            }
            Session["SessionId"] = System.Guid.NewGuid().ToString();
            ViewState["ViewStateId"] = Session["SessionId"].ToString();
        }
        if (ViewState["lastUpdateDate"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate"].ToString();
            if (IsPageRefresh == true)
            {
                ViewState["year"] = Session["yearnew"];
                bindPublicNoticDetails(1);
            }
            
        }
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            ViewState["year"] = Session["yearnew"];
            BindYear();
            drpyear.Enabled = false;
            bindPublicNoticDetails(1);

            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			 ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);
            Session.Remove("yearnew");
        }

        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.PublicNotice;
        }
        else
        {
            PageTitle = Resources.HercResource.PublicNotice;
        }
    }
    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    public void bindPublicNoticDetails(int pageIndex)
    {
        objlinkOB.PageIndex = pageIndex;
        objlinkOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);

        //BindYear();
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            objlinkOB.PageSize = 10000;
        }
        else
        {
            objlinkOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }
        if (ViewState["year"] != null)
        {
            objlinkOB.Year = ViewState["year"].ToString();
            drpyear.SelectedValue = ViewState["year"].ToString();
        }
        else
        {
            objlinkOB.Year = drpyear.SelectedValue.ToString().Trim();
        }
        p_val.dSet = pubNoticeBL.Get_publiceNotceoldDetails(objlinkOB, out p_val.k);
        if (p_val.dSet.Tables[0].Rows.Count > 0)
        {


            this.rptpublicNotice.DataSource = p_val.dSet;
            this.rptpublicNotice.DataBind();
            if (p_val.dSet.Tables[0].Rows[0]["MetaKeywords"] != DBNull.Value)
            {
                MetaKeyword = p_val.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
            }
            else
            {
                MetaKeyword = "HERC Public Notice";
            }
            if (p_val.dSet.Tables[0].Rows[0]["MetaDescriptions"] != DBNull.Value)
            {
                MetaDescription = p_val.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            }
            else
            {
                MetaDescription = "HERC Public Notice";
            }
            if (p_val.dSet.Tables[0].Rows[0]["MetaLanguage"] != DBNull.Value)
            {
                MetaLng = p_val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
            }
            else
            {
                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                {
                    MetaLng = "en";
                }
                else
                {
                    MetaLng = "hi";
                }
            }
            if (p_val.dSet.Tables[0].Rows[0]["MetaTitle"] != DBNull.Value)
            {
                MetaTitles = p_val.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
            }
            else
            {
                MetaTitles = "HERC Public Notice";
            }
            lastUpdatedDate = p_val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            ViewState["lastUpdateDate"] = lastUpdatedDate;
            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                rptPager.Visible = false;
                ddlPageSize.Visible = false;
                lblPageSize.Visible = false;
            }
            else
            {
                rptPager.Visible = true;
                ddlPageSize.Visible = true;
                lblPageSize.Visible = true;
            }
        }
        else
        {

            this.rptpublicNotice.DataSource = p_val.dSet;
            this.rptpublicNotice.DataBind();
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;

        }
         
          
       
        p_val.Result = p_val.k;
        if (p_val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_val.Result, pageIndex, ddlPageSize, rptPager);
            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                rptPager.Visible = false;
            }
            else
            {
                rptPager.Visible = true;
            }
        }
        else
        {
            rptPager.Visible = false;
        }
    }

    protected void rptpublicNotice_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {

        
        if (e.CommandName == "ViewDetails")
        {
           
                    p_val.stringTypeID = e.CommandArgument.ToString();
                    p_val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "DetailsPage.aspx?PulicNoticeId=" + p_val.stringTypeID) + "', 'blank' + new Date().getTime()," +
                                       "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                       "</script>";
                    this.Page.RegisterStartupScript("PopupScript", p_val.strPopupID);
                   
             

        }



    }


    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
       // this.bindPublicNoticDetails(1);
        this.bindPublicNoticDetails(1);
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        //this.bindPublicNoticDetails(pageIndex);
        this.bindPublicNoticDetails(pageIndex);
    }

    #endregion


    protected void rptpublicNotice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            p_val.sbuilder.Remove(0, p_val.sbuilder.Length);
           
            LinkButton lnkTitle = (LinkButton)e.Item.FindControl("lnkTitle");
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");
           
        }
    }

    #region function to Bind data in a data list for year

    public void BindYear()
    {
	try{
        pyear.Visible = true;
        p_val.dSetChildData = pubNoticeBL.GetPublicNoticesYear();

        if (p_val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = p_val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();
        }
		}
		catch{}
    }

    #endregion 

    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session.Remove("yearnew");
        ViewState["year"] = drpyear.SelectedValue;
        Session["yearnew"] = drpyear.SelectedValue;
       // bindPublicNoticDetails(1);
        bindPublicNoticDetails(1);
    }

    #region Datalist datalistYear itemCommand event zone

    protected void datalistYear_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            p_val.str = e.CommandArgument.ToString();
            ViewState["year"] = p_val.str;
           // bindPublicNoticDetails(1);
            bindPublicNoticDetails(1);
        }
    }

    #endregion

    protected void lnkreview_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PublicNoticeReviewPetitionDetails.aspx");
    }

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
        MetaLang = MetaLng;
        MetaTitle = MetaTitles;
    }

    #endregion

    //Code for paging using datalist

    #region Function doPaging() for paging in repeater

    private void doPaging()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PageIndex");
        dt.Columns.Add("PageText");

        fistIndex = CurrentPage - 9;


        if (CurrentPage > 8)
        {
            lastIndex = CurrentPage + 1;
        }
        else
        {
            lastIndex = 10;
        }
        if (lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
        {
            lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
            fistIndex = lastIndex - 10;
        }

        if (fistIndex < 0)
        {
            fistIndex = 0;
        }

        for (int i = fistIndex; i < lastIndex; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i + 1;
            dt.Rows.Add(dr);
        }

    }

    #endregion

    #region datalist dlPaging ItemCommand event zone

    protected void dlPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.Equals("Paging"))
        {
            CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
           
        }
    }

    #endregion

    #region datalist dlPaging itemDataBound event zone

    protected void dlPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
        if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
        {
            lnkbtnPage.Enabled = false;
            lnkbtnPage.Style.Add("fone-size", "14px");
            lnkbtnPage.Font.Bold = true;
        }
    }

    #endregion

    #region button lbtnNext click event to go on next page

    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
       
    }

    #endregion

    #region button lbtnPrevious click event to go on previous page

    protected void lbtnPrevious_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
       

    }

    #endregion

    #region button lbtnLast click event to visit on last page directly

    protected void lbtnLast_Click(object sender, EventArgs e)
    {
        CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
        
    }

    #endregion

  

  
    protected void lbtnFirst_Click(object sender, EventArgs e)
    {

    }
}
