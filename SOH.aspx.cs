﻿using System;
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

public partial class SOH : BasePage
{
    #region variable declaration zone

    PetitionOB objpetOB = new PetitionOB();
    PetitionBL objpetBL = new PetitionBL();
    Project_Variables p_val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    Miscelleneous_BL objmiscel = new Miscelleneous_BL();

    string str = string.Empty;
    public static string UrlPrint = string.Empty;
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
        ltrlBreadcrum.Text = str.ToString();
        bool IsPageRefresh = false;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
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
                if (RewriteModule.RewriteContext.Current.Params["prev"] != null)
                {
                    ViewState["year"] = Session["yearnew"];
                    BindYear();
                    drpyear.Enabled = false;
                }
                else if (RewriteModule.RewriteContext.Current.Params["next"] != null)
                {
                    ViewState["year"] = Session["yearnew"];
                    BindNextYear();
                    drpyear.Enabled = false;
                }
                bindSOH(1);

            }
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);

        }

        else if (!IsPostBack)
        {
            Session.Remove("yearnew");
            ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
            Session["SessionId"] = ViewState["ViewStateId"].ToString();
            if (Page.Request.QueryString["date"] != null)
            {
                try
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
                catch { }
            }
            else
            {
                if (RewriteModule.RewriteContext.Current.Params["prev"] != null)
                {
                    BindYear();
                }
                else if (RewriteModule.RewriteContext.Current.Params["next"] != null)
                {
                    ViewState["Nextyear"] = System.DateTime.Now.Year + 1;
                    BindNextYear();
                }
                bindSOH(1);

            }
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
                bindSOH_by_calander(1);
            }
            //bindSOH_by_calander(1);
        }
        if (ViewState["lastUpdateDate1"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate1"].ToString();
            if (IsPageRefresh == true)
            {
                ViewState["year"] = Session["yearnew"];
                bindSOH(1);
            }

        }



        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.ScheduleOfHearings;
        }
        else
        {
            PageTitle = Resources.HercResource.ScheduleOfHearings;
        }
    }

    #endregion



    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    #region function to bind SOH Details

    public void bindSOH(int pageIndex)
    {
        try
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
            objpetOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            if (ViewState["year"] != null)
            {
                objpetOB.year = ViewState["year"].ToString();
                drpyear.SelectedValue = ViewState["year"].ToString();
            }
            else if (ViewState["Nextyear"] != null)
            {
                objpetOB.year = ViewState["Nextyear"].ToString();
                drpyear.SelectedValue = ViewState["Nextyear"].ToString();
            }
            else
            {
                objpetOB.year = null;
            }
            if (RewriteModule.RewriteContext.Current.Params["prev"] != null || ViewState["year"] != null)
            {
                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                {
                    previous.Attributes.Add("class", "current");
                }
                else
                {
                    previousHindi.Attributes.Add("class", "current");
                }
                p_val.dSet = objpetBL.Get_PreviousYearSOH(objpetOB, out p_val.k);

                // BindYear();

            }
            else if (RewriteModule.RewriteContext.Current.Params["next"] != null || ViewState["Nextyear"] != null)
            {
                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                {
                    next.Attributes.Add("class", "current");
                }
                else
                {
                    nextyearHindi.Attributes.Add("class", "current");
                }
                p_val.dSet = objpetBL.Get_NextYearSOH(objpetOB, out p_val.k);
                pyear.Visible = false;
                // BindYear();

            }
            else
            {
                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                {
                    current.Attributes.Add("class", "current");
                }
                else
                {
                    currentHindi.Attributes.Add("class", "current");
                }
                p_val.dSet = objpetBL.Get_CurrentSOH(objpetOB, out p_val.k);
            }
            if (p_val.dSet.Tables[0].Rows.Count > 0)
            {
                gvSOH.DataSource = p_val.dSet;

                gvSOH.DataBind();
                for (int i = 0; i < gvSOH.Rows.Count; i++)
                {
                    Label lblNumber = gvSOH.Rows[i].FindControl("lblnumber") as Label;

                    Label lblPetitioner = gvSOH.Rows[i].FindControl("lblPetitioner") as Label;

                    Label lblRespondent = gvSOH.Rows[i].FindControl("lblRespondent") as Label;

                    if (p_val.dSet.Tables[0].Rows[i]["PRO_No"] == DBNull.Value)
                    {

                        lblNumber.Text = p_val.dSet.Tables[0].Rows[i]["RP_No"].ToString();
                    }
                    else
                    {
                        lblNumber.Text = p_val.dSet.Tables[0].Rows[i]["PRO_No"].ToString();

                    }

                }
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
                lblmsg.Visible = true;
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
        catch { }
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
        objpetOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
        p_val.dSet = objpetBL.Get_SOH_By_calander(objpetOB, out p_val.k);
        if (p_val.dSet.Tables[0].Rows.Count > 0)
        {
            gvSOH.DataSource = p_val.dSet;
            gvSOH.DataBind();
            for (int i = 0; i < gvSOH.Rows.Count; i++)
            {

                Label lblNumber = gvSOH.Rows[i].FindControl("lblnumber") as Label;

                Label lblPetitioner = gvSOH.Rows[i].FindControl("lblPetitioner") as Label;

                Label lblRespondent = gvSOH.Rows[i].FindControl("lblRespondent") as Label;

                if (p_val.dSet.Tables[0].Rows[i]["PRO_No"] == DBNull.Value)
                {

                    lblNumber.Text = p_val.dSet.Tables[0].Rows[i]["RP_No"].ToString();
                }
                else
                {
                    lblNumber.Text = p_val.dSet.Tables[0].Rows[i]["PRO_No"].ToString();

                }


            }
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
            lblmsg.Text = "Records are not available.";
            lblmsg.ForeColor = System.Drawing.Color.Red;
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
        objpetOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
        objpetOB.PetitionType = 1;
        p_val.dSetChildData = objpetBL.Get_YearSOH(objpetOB);

        if (p_val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = p_val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();
        }

    }


    #endregion

    #region function to Bind data in a data list for year

    public void BindNextYear()
    {
        pyear.Visible = true;
        objpetOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
        objpetOB.PetitionType = 1;
        p_val.dSetChildData = objpetBL.GetYearNextSOH(objpetOB);

        if (p_val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = p_val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();
        }

    }


    #endregion


    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["year"] = drpyear.SelectedValue;
        Session["yearnew"] = drpyear.SelectedValue;
        bindSOH(1);
    }

    protected void gvSOH_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDoc")
        {
            p_val.url = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["scheduleofHearing"] + "/");
            string file = e.CommandArgument.ToString();
            p_val.url = p_val.url + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(p_val.url);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(p_val.url));
                Response.Clear();
                Response.WriteFile(p_val.url);
                Response.End();
            }


        }

        if (e.CommandName == "ViewDetails")
        {

            p_val.stringTypeID = e.CommandArgument.ToString();
            p_val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/ViewDetails.aspx?Soh_id=" + p_val.stringTypeID) + "', 'blank' + new Date().getTime(), " +
                               "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                               "</script>";
            this.Page.RegisterStartupScript("PopupScript", p_val.strPopupID);










        }
    }
    protected void gvSOH_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            string filename = DataBinder.Eval(e.Row.DataItem, "soh_file").ToString();

            Label lblPetitioner = (Label)e.Row.FindControl("lblPetitioner");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            LinkButton lblSubject = (LinkButton)e.Row.FindControl("lblSubject");
            Label lblRemarksSoh = (Label)e.Row.FindControl("lblRemarksSoh");

            if (lblRemarksSoh.Text != null && lblRemarksSoh.Text != "")
            {
                lblRemarksSoh.Text = HttpUtility.HtmlDecode(lblRemarksSoh.Text).Replace("&", "&amp;");
            }
            if (lblPetitioner.Text != null && lblPetitioner.Text != "")
            {
                lblPetitioner.Text = HttpUtility.HtmlDecode(lblPetitioner.Text).Replace("&", "&amp;");
            }
            if (lblRespondent.Text != null && lblRespondent.Text != "")
            {
                lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text).Replace("&", "&amp;");
            }
            if (lblSubject.Text != null && lblSubject.Text != "")
            {
                lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text).Replace("&", "&amp;");
            }

            if (filename == null || filename == "")
            {
                lnk.Visible = false;
            }
        }
    }

    protected void lnkreview_Click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect("~/RP_SOH.aspx");
        }
        else
        {
            Response.Redirect("~/Content/Hindi/RP_SOH.aspx");

        }
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


}
