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

public partial class Ombudsman_ScheduleOfHearingPreviousYear : BasePageOmbudsman
{
    #region variable declaration zone

    PetitionOB objpetOB = new PetitionOB();
    PetitionBL objpetBL = new PetitionBL();
    Project_Variables p_val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    string str = string.Empty;
    public string lastUpdatedDate = string.Empty;
    public static string UrlPrint = string.Empty;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;

    #endregion

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.CLSH, Resources.HercResource.PreviousYears);
        ltrlBreadcrum.Text = str.ToString();
        bool IsPageRefresh = false;

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            ViewState["year"] = Session["yearnew"];
            //pyear.Visible = false;
            if (Page.Request.QueryString["date"] != null)
            {
                bindSOH_by_calander(1);
            }
            else
            {
                bindSOH(1);
                drpyear.Enabled = false;
            }

            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssRef);
        }
        else if (!IsPostBack)
        {
            Session.Remove("yearnew");
            ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();

            Session["SessionId"] = ViewState["ViewStateId"].ToString();

            if (Page.Request.QueryString["date"] != null)
            {
                bindSOH_by_calander(1);
            }
            else
            {
                if (string.IsNullOrEmpty(Request.QueryString["format"]))
                {
                    bindSOH(1);
                }

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
                bindSOH(1);
            }

        }
        if (ViewState["lastUpdateDate1"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate1"].ToString();

            if (IsPageRefresh == true)
            {
                ViewState["year"] = Session["yearnew"];
                bindSOH_by_calander(1);
            }

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
        try
        {
            BindYear();
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
                drpyear.SelectedValue = ViewState["year"].ToString();
            }
            else
            {
                objpetOB.year = null;
            }

            //previous.Attributes.Add("class", "current");
            p_val.dSet = objpetBL.GetPreviousYearSOHForOmbudsman(objpetOB, out p_val.k);



            if (p_val.dSet.Tables[0].Rows.Count > 0)
            {
                //  BindYear();

                gvSOH.DataSource = p_val.dSet;
                gvSOH.DataBind();
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
                gvSOH.DataSource = p_val.dSet;
                gvSOH.DataBind();
                lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
                lblmsg.ForeColor = System.Drawing.Color.Red;
                rptPager.Visible = false;
                ddlPageSize.Visible = false;
                lblPageSize.Visible = false;
                // pyear.Visible = false;

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
        objpetOB.Date = Convert.ToDateTime(Request.QueryString["date"]);
        objpetOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
        p_val.dSet = objpetBL.Get_SOH_By_calander(objpetOB, out p_val.k);
        if (p_val.dSet.Tables[0].Rows.Count > 0)
        {
            gvSOH.DataSource = p_val.dSet;
            gvSOH.DataBind();

            lastUpdatedDate = p_val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            ViewState["lastUpdateDate1"] = lastUpdatedDate;
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
            gvSOH.DataSource = p_val.dSet;
            gvSOH.DataBind();

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
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            pyear.Visible = true;
        }
        else
        {
            pyear.Visible = true;
        }
        objpetOB.PetitionType = 2;
        objpetOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
        p_val.dSetChildData = objpetBL.Get_YearSOHForOmbudsman(objpetOB);

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


        }
    }


}
