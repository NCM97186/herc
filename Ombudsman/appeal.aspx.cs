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

public partial class Ombudsman_appeal : BasePageOmbudsman
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
        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.Appeal, Resources.HercResource.Currentyearappeals);
        ltrlBreadcrum.Text = str.ToString();
        // p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/");
        obj_miscel.MakeAccessible(Grdappeal);
        //CreateDynamicTable();
        if (!IsPostBack)
        {

            Bind_Appeal(1);
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            // BindYear();

        }

        if (ViewState["lastUpdateDate"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate"].ToString();
            if (ViewState["pageIndex"] != null)
            {
                Bind_Appeal(Convert.ToInt16(ViewState["pageIndex"]));
            }
            else
            {
                Bind_Appeal(1);
            }
        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            Bind_Appeal(1);
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssRef);

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
        try
        {
            pyear.Visible = true;
            p_Val.dSetChildData = obj_petBL1.Get_Year();

            if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
            {
                datalistYear.DataSource = p_Val.dSetChildData;
                datalistYear.DataBind();
            }
        }
        catch { }
    }
    #endregion

    #region function to bind appeal

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
            }
            else
            {
                obj_petOB.year = null;
            }
            if (Request.QueryString["PrevYear"] != null && ViewState["year"] == null)
            {
                p_Val.dSet = obj_petBL.Get_appeal_prevyear(obj_petOB, out p_Val.k);
                BindYear();
            }
            else
            {
                p_Val.dSet = obj_petBL.Get_appeal(obj_petOB, out p_Val.k);
            }
            if (p_Val.dSet.Tables[0].Rows.Count > 0)
            {

                Grdappeal.DataSource = p_Val.dSet;
                Grdappeal.DataBind();
                lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();

                MetaKeyword = p_Val.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
                MetaDescription = p_Val.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
                MetaLng = p_Val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
                MetaTitles = p_Val.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
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

    #region Datalist datalistYear itemCommand event zone

    protected void datalistYear_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            p_Val.str = e.CommandArgument.ToString();
            ViewState["year"] = p_Val.str;
            Bind_Appeal(1);
        }
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


}
