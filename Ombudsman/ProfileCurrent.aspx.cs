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

public partial class Ombudsman_ProfileCurrent : BasePageOmbudsman
{
    #region variable declaration zone

    string str = string.Empty;
    Project_Variables p_Var = new Project_Variables();
    UserOB obj_userOB = new UserOB();
    UserBL obj_UserBL = new UserBL();
    Miscelleneous_BL obj_miscelBL = new Miscelleneous_BL();
    ProfileBL profileBL = new ProfileBL();
    ProfileOB profileOB = new ProfileOB();
    PaginationBL pagingBL = new PaginationBL();
    public string lastUpdatedDate = string.Empty;
    public string menuid = string.Empty;
    public string position = string.Empty;
    public static string UrlPrint = string.Empty;

    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    #endregion 

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.AboutOmbudsman, Resources.HercResource.Profile);
        ltrlBreadcrum.Text = str.ToString();
        if (RewriteModule.RewriteContext.Current.Params["menuid"] != null)
        {
            p_Var.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        }
        p_Var.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        menuid = p_Var.PageID;
        position = p_Var.position.ToString();
        if (!IsPostBack)
        {
            Bind_Grid(1);
           // P2.Visible = false;
        }
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            Bind_Grid(1);
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssRef);
        }
        PageTitle = Resources.HercResource.Profile;
    }

    #endregion 

    #region Function To bind the gridView with menu

    public void Bind_Grid(int pageIndex)
    {
	try{

        grdCMSMenu.Visible = true;
        profileOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        if (ViewState["NevigationId"] == null)
        {
            profileOB.NevigationId = 1;
        }

        profileOB.ModuleId = 28;

        profileOB.PageIndex = pageIndex;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            profileOB.PageSize = 10000;
        }
        else
        {
            profileOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }
        profileOB.DepttId =Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
        p_Var.dSet = profileBL.USP_Profile_DisplayWithPaging(profileOB, out p_Var.k);


        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            lastUpdatedDate = p_Var.dSet.Tables[0].Rows[0]["LastUpdatedDate"].ToString();
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
            grdCMSMenu.DataSource = p_Var.dSet;
            grdCMSMenu.DataBind();
            p_Var.dSet = null;

            Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
            Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            p_Var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);

            p_Var.dSet = null;
            //lblmsg.Visible = false;
        }
        else
        {
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        p_Var.Result = p_Var.k;
        if (p_Var.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_Var.Result, pageIndex, ddlPageSize, rptPager);
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

catch{}


    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid(1);
    }
    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        Bind_Grid(pageIndex);
    }

    #endregion

    #region link button click event zone

    public void LnkViewOld_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {
            Response.Redirect("~/ProfilePreviousOmbudsman/" + p_Var.PageID + "_" + p_Var.position + "_Profile.aspx");
        }
        else
        {
            Response.Redirect("~/OmbudsmanContent/Hindi/ProfilePreviousOmbudsman/" + p_Var.PageID + "_" + p_Var.position + "_Profile.aspx");
        }
       // Response.Redirect("~/ProfilePreviousOmbudsman/" + p_Var.PageID + "_" + p_Var.position + "_Profile.aspx");
       
    }

    #endregion 

    protected void grdCMSMenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDetails = (Label)e.Row.FindControl("lblDetails");
            HyperLink lnkButton = (HyperLink)e.Row.FindControl("lnkButton");
            HiddenField hyd = (HiddenField)e.Row.FindControl("hid");
            string profileid = DataBinder.Eval(e.Row.DataItem, "Profile_Id").ToString();
            string moduleid = DataBinder.Eval(e.Row.DataItem, "Module_Id").ToString();
            string NevigationId = DataBinder.Eval(e.Row.DataItem, "Nevigation_Id").ToString();
            Label lblEmail = (Label)e.Row.FindControl("lblEmail");
            lblEmail.Text = lblEmail.Text.Replace(".", "[dot]").Replace("@", "[at]");
            if (hyd.Value != null && hyd.Value != "")
            {
                lnkButton.Visible = true;
                lblDetails.Visible = false;
                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                {

                    lnkButton.NavigateUrl = ResolveUrl("~/ProfileDetailsOmbudsman/" + profileid + "_" + moduleid + "_" + NevigationId + "_" + menuid + "_" + position + ".aspx");

                }
                else
                {
                    lnkButton.NavigateUrl = ResolveUrl("~/OmbudsmanContent/Hindi/ProfileDetailsOmbudsman/" + profileid + "_" + moduleid + "_" + NevigationId + "_" + menuid + "_" + position + ".aspx");
                }
            }
            else
            {
                lnkButton.Visible = false;
                lblDetails.Visible = true;
            }

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


}
