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

public partial class Ombudsman_ScheduleOfHearing : BasePageOmbudsman
{
    #region variable declaration zone

    public static string UrlPrint = string.Empty;
    PetitionOB objpetOB = new PetitionOB();
    PetitionBL objpetBL = new PetitionBL();
    Project_Variables p_val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    Miscelleneous_BL objmiscel = new Miscelleneous_BL();
    string str = string.Empty;
    public string lastUpdatedDate = string.Empty;
    public string headerName = string.Empty;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    #endregion

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.CLSH, Resources.HercResource.CurrentYear);
        ltrlBreadcrum.Text = str.ToString();

        if (RewriteModule.RewriteContext.Current.Params["prev"] != null)
        {
            str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.CLSH, Resources.HercResource.PreviousYears);
            headerName = Resources.HercResource.PreviousYears;
        }
        else if (RewriteModule.RewriteContext.Current.Params["next"] != null)
        {
            str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.CLSH, Resources.HercResource.Nextyear);
            headerName = Resources.HercResource.Nextyear;

        }
        else
        {
            str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.CLSH, Resources.HercResource.CurrentYear);
            headerName = Resources.HercResource.CurrentYear;

        }

        if (ViewState["lastUpdateDate"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate"].ToString();
            //bindSOH(1);
            if (ViewState["pageIndex"] != null)
            {
                bindSOH(Convert.ToInt16(ViewState["pageIndex"]));
            }
            else
            {
                bindSOH(1);
            }
        }

        if (ViewState["lastUpdateDate1"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate1"].ToString();
            if (ViewState["pageIndex"] != null)
            {
                bindSOH_by_calander(Convert.ToInt16(ViewState["pageIndex"]));
            }
            else
            {
                bindSOH_by_calander(1);
            }
            //bindSOH_by_calander(1);
        }
        if (!IsPostBack)
        {
            try
            {
                if (Page.Request.QueryString["date"] != null)
                {
                    int year = Convert.ToInt16(Request.QueryString["date"].Substring(Request.QueryString["date"].Length - 4));
                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                    {
                        if (year > DateTime.Now.Year)
                        {
                            next.Attributes["class"] = "current";
                            headerName = Resources.HercResource.Nextyear;
                        }
                        else if (year < DateTime.Now.Year)
                        {
                            previous.Attributes["class"] = "current";
                            headerName = Resources.HercResource.PreviousYears;
                        }
                        else
                        {
                            current.Attributes["class"] = "current";
                            headerName = Resources.HercResource.CurrentYear;
                        }
                    }
                    else
                    {
                        if (year > DateTime.Now.Year)
                        {
                            nextyearHindi.Attributes["class"] = "current";
                            headerName = Resources.HercResource.Nextyear;
                        }
                        else if (year < DateTime.Now.Year)
                        {
                            previousHindi.Attributes["class"] = "current";
                            headerName = Resources.HercResource.PreviousYears;
                        }
                        else
                        {
                            currentHindi.Attributes["class"] = "current";
                            headerName = Resources.HercResource.CurrentYear;
                        }
                    }
                    bindSOH_by_calander(1);
                }
                else
                {
                    bindSOH(1);
                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                    {
                        current.Attributes["class"] = "current";
                    }
                    else
                    {
                        currentHindi.Attributes["class"] = "current";
                    }
                }
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }
            catch { }
        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            try
            {
                if (Page.Request.QueryString["date"] != null)
                {
                    int year = Convert.ToInt16(Request.QueryString["date"].Substring(Request.QueryString["date"].Length - 4));
                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                    {
                        if (year > DateTime.Now.Year)
                        {
                            next.Attributes["class"] = "current";
                            headerName = Resources.HercResource.Nextyear;
                        }
                        else if (year < DateTime.Now.Year)
                        {
                            previous.Attributes["class"] = "current";
                            headerName = Resources.HercResource.PreviousYears;
                        }
                        else
                        {
                            current.Attributes["class"] = "current";
                            headerName = Resources.HercResource.CurrentYear;
                        }
                    }
                    else
                    {
                        if (year > DateTime.Now.Year)
                        {
                            nextyearHindi.Attributes["class"] = "current";
                            headerName = Resources.HercResource.Nextyear;
                        }
                        else if (year < DateTime.Now.Year)
                        {
                            previousHindi.Attributes["class"] = "current";
                            headerName = Resources.HercResource.PreviousYears;
                        }
                        else
                        {
                            currentHindi.Attributes["class"] = "current";
                            headerName = Resources.HercResource.CurrentYear;
                        }
                    }

                    bindSOH_by_calander(1);
                }
                else
                {
                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                    {
                        current.Attributes["class"] = "current";
                    }
                    else
                    {
                        currentHindi.Attributes["class"] = "current";
                    }
                    bindSOH(1);

                }
                HtmlLink cssRef = new HtmlLink();
                cssRef.Href = "css/print.css";
                cssRef.Attributes["rel"] = "stylesheet";
                cssRef.Attributes["type"] = "text/css";
                Page.Header.Controls.Add(cssRef);
            }
            catch { }
        }

        PageTitle = Resources.HercResource.ScheduleOfHearings;

    }

    #endregion


    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    #region function to bind SOH Details

    public void bindSOH(int pageIndex)
    {
        objpetOB.PageIndex = pageIndex;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            objpetOB.PageSize = 10000;
        }
        else
        {
            objpetOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }
        objpetOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        objpetOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
        if (ViewState["year"] != null)
        {
            objpetOB.year = ViewState["year"].ToString();
        }
        else
        {
            objpetOB.year = null;
        }
        if (Request.QueryString["Prev"] != null && ViewState["year"] == null)
        {
            // previous.Attributes.Add("class", "current");
            p_val.dSet = objpetBL.GetPreviousYearSOHForOmbudsman(objpetOB, out p_val.k);
            BindYear();
        }
        else
        {
            // current.Attributes.Add("class", "current");
            p_val.dSet = objpetBL.GetCurrentSOHForOmbudsman(objpetOB, out p_val.k);
        }
        if (p_val.dSet.Tables[0].Rows.Count > 0)
        {
            gvSOH.DataSource = p_val.dSet;
            gvSOH.DataBind();
            lastUpdatedDate = p_val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            ViewState["lastUpdateDate"] = lastUpdatedDate;
            MetaKeyword = p_val.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
            MetaDescription = p_val.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            MetaLng = p_val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = p_val.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
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
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
            pyear.Visible = false;

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

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Page.Request.QueryString["date"] != null)
        {
            this.bindSOH_by_calander(1);
        }
        else
        {
            this.bindSOH(1);

        }
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        ViewState["pageIndex"] = pageIndex;
        if (Page.Request.QueryString["date"] != null)
        {
            this.bindSOH_by_calander(pageIndex);
        }
        else
        {
            this.bindSOH(pageIndex);

        }

    }

    #endregion

    #region function to bind SOH Details

    public void bindSOH_by_calander(int pageIndex)
    {
        objpetOB.PageIndex = pageIndex;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            objpetOB.PageSize = 10000;
        }
        else
        {
            objpetOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }
        objpetOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        if (objpetOB.LangId == 1)
        {
            objpetOB.Date = Convert.ToDateTime(Request.QueryString["date"]);
        }
        else
        {
            objpetOB.Date = Convert.ToDateTime(objmiscel.getDateFormatSohCalender(Request.QueryString["date"]));
        }
        objpetOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
        p_val.dSet = objpetBL.GetSOHcalanderforOmbudsman(objpetOB, out p_val.k);
        if (p_val.dSet.Tables[0].Rows.Count > 0)
        {
            gvSOH.DataSource = p_val.dSet;
            gvSOH.DataBind();

            lastUpdatedDate = p_val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            ViewState["lastUpdateDate1"] = lastUpdatedDate;

            MetaKeyword = p_val.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
            MetaDescription = p_val.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            MetaLng = p_val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = p_val.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
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
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
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

    #endregion

    #region function to Bind data in a data list for year

    public void BindYear()
    {
        pyear.Visible = true;

        objpetOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
        p_val.dSetChildData = objpetBL.Get_YearSOH(objpetOB);

        if (p_val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            datalistYear.DataSource = p_val.dSetChildData;
            datalistYear.DataBind();
        }

    }


    #endregion

    #region Datalist datalistYear itemCommand event zone

    protected void datalistYear_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            p_val.str = e.CommandArgument.ToString();
            ViewState["year"] = p_val.str;
            bindSOH(1);
        }
    }

    #endregion

    protected void gvSOH_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetails")
        {

            p_val.stringTypeID = e.CommandArgument.ToString();
            p_val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Ombudsman/" + "ViewAppealDetails.aspx?Sohid=" + p_val.stringTypeID) + "', 'blank' + new Date().getTime()," +
                               "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                               "</script>";
            this.Page.RegisterStartupScript("PopupScript", p_val.strPopupID);


        }
    }

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitleOmbudsman;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion

    protected void gvSOH_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");

            Label lblPetitioner = (Label)e.Row.FindControl("lblApplicant");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            LinkButton lblSubject = (LinkButton)e.Row.FindControl("lblSubject");
            if (lblPetitioner.Text != null && lblPetitioner.Text != "")
            {
                lblPetitioner.Text = HttpUtility.HtmlDecode(lblPetitioner.Text);
            }
            if (lblRespondent.Text != null && lblRespondent.Text != "")
            {
                lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
            }
            if (lblSubject.Text != null && lblSubject.Text != "")
            {
                lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            }


        }
    }


}
