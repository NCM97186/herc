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

public partial class Ombudsman_appealprevious : BasePageOmbudsman
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    public string lastUpdatedDate = string.Empty;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    public static string UrlPrint = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.Appeal, Resources.HercResource.Previousyeaappeals);
        ltrlBreadcrum.Text = str.ToString();
        // p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/");
        obj_miscel.MakeAccessible(Grdappeal);
        //CreateDynamicTable();
        bool IsPageRefresh = false;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            ViewState["year"] = Session["yearnew"];
            // pyear.Visible = false;
            Bind_Appeal(1);
            drpyear.Enabled = false;
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

            if (string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                Bind_Appeal(1);
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
                Bind_Appeal(1);
            }

        }


        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.Appeal;
        }
        else
        {
            PageTitle = Resources.HercResource.Appeal;
        }
    }


    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

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
        p_Val.dSetChildData = obj_petBL.Get_Year_Appeal();

        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = p_Val.dSetChildData;
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
        Bind_Appeal(1);
    }

    #region function declaration zone

    public void Bind_Appeal(int pageIndex)
    {
        try
        {
            obj_petOB.PageIndex = pageIndex;
            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                obj_petOB.PageSize = 10000;
            }
            else
            {
                obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
            }
            if (ViewState["year"] != null)
            {
                obj_petOB.year = ViewState["year"].ToString();
                drpyear.SelectedValue = ViewState["year"].ToString();
            }
            else
            {
                obj_petOB.year = Convert.ToString(Convert.ToInt32(System.DateTime.Now.Year) - 1);
            }

            p_Val.dSet = obj_petBL.Get_appeal_prevyear(obj_petOB, out p_Val.k);
            BindYear();

            if (p_Val.dSet.Tables[0].Rows.Count > 0)
            {

                Grdappeal.DataSource = p_Val.dSet;
                Grdappeal.DataBind();
                lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
                if (p_Val.dSet.Tables[0].Rows[0]["MetaKeywords"] != DBNull.Value)
                {
                    MetaKeyword = p_Val.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
                }
                else
                {
                    MetaKeyword = "EO Appeal Meta Keywords";
                }
                if (p_Val.dSet.Tables[0].Rows[0]["MetaDescriptions"] != DBNull.Value)
                {
                    MetaDescription = p_Val.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
                }
                else
                {
                    MetaDescription = "EO Appeal";
                }
                if (p_Val.dSet.Tables[0].Rows[0]["MetaLanguage"] != DBNull.Value)
                {
                    MetaLng = p_Val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
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
                    lblmsg.Visible = false;
                }
            }
            else
            {
                Grdappeal.DataSource = p_Val.dSet;
                Grdappeal.DataBind();
                rptPager.Visible = false;
                ddlPageSize.Visible = false;
                lblPageSize.Visible = false;
                lblmsg.Visible = true;
                lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            p_Val.Result = p_Val.k;
            if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
            {
                pagingBL.Paging_Show(p_Val.Result, pageIndex, ddlPageSize, rptPager);
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



    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        ViewState["pageIndex"] = pageIndex;
        this.Bind_Appeal(pageIndex);
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Appeal(1);
    }

    #endregion



    protected void Grdappeal_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "View")
        {

            p_Val.stringTypeID = e.CommandArgument.ToString();
            p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Ombudsman/" + "ViewAppealDetails.aspx?Appealid=" + p_Val.stringTypeID) + "', 'blank' + new Date().getTime()," +
                               "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                               "</script>";
            this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);


        }

    }

    protected void Grdappeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblApplicant = (Label)e.Row.FindControl("lblApplicant");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");
            lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            lblApplicant.Text = HttpUtility.HtmlDecode(lblApplicant.Text);
            lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
        }
    }

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitleOmbudsman;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
        MetaLang = MetaLng;
        MetaTitle = MetaTitles;
    }

    #endregion



    protected void rptPager_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkPaging = e.Item.FindControl("lnkPage") as LinkButton;
            if (lnkPaging.Enabled == true)
            {
                lnkPaging.CssClass = "active";
            }
        }
    }
}

