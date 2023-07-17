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

public partial class ProfileNew : BasePage
{

    //Area for data declaration zone
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
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    //End
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Aboutus, Resources.HercResource.Profile);
            ltrlBreadcrum.Text = str.ToString();
            // p_Var.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
            if (RewriteModule.RewriteContext.Current.Params["menuid"].ToString() != null && RewriteModule.RewriteContext.Current.Params["menuid"].ToString() != "")
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
                //bindLeftSideMenu();
                Bind_Grid(1);
                HtmlLink cssRef = new HtmlLink();
                cssRef.Href = "css/print.css";
                cssRef.Attributes["rel"] = "stylesheet";
                cssRef.Attributes["type"] = "text/css";
                ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
                Page.Header.Controls.Add(cssRef);


            }
            if (Resources.HercResource.Lang_Id == "1")
            {
                PageTitle = Resources.HercResource.Profile;
            }
            else
            {
                PageTitle = Resources.HercResource.Profile;
            }
        }
        catch { }
    }

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
        profileOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
        profileOB.PageIndex = pageIndex;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            profileOB.PageSize = 10000;
        }
        else
        {
            profileOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }
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


            MetaKeyword = p_Var.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
            MetaDescription = p_Var.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            MetaLng = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();

            p_Var.dSet = null;

            Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
            Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            p_Var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);

            p_Var.dSet = null;


        }
        else
        {
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;

			   lblmsg.Visible      = true;
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
           // lblmsg.ForeColor = System.Drawing.Color.Red;
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
        this.Bind_Grid(1);
    }
    #endregion


    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Bind_Grid(pageIndex);
    }

    #endregion


    #region link button click event zone

    public void LnkViewOld_Click(object sender, EventArgs e)
    {
        //profilePrevious/(.+)_(.+)_(.+) 
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {
            Response.Redirect("~/profilePrevious/" + p_Var.PageID + "_" + p_Var.position + "_Profile.aspx");
        }
        else
        {
            Response.Redirect("~/content/Hindi/profilePrevious/" + p_Var.PageID + "_" + p_Var.position + "_Profile.aspx");
        }
        
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

                    lnkButton.NavigateUrl = ResolveUrl("~/ProfileDetail/" + profileid + "_" + moduleid + "_" + NevigationId + "_" + menuid + "_" + position + ".aspx");

                }
                else
                {
                    lnkButton.NavigateUrl = ResolveUrl("~/Content/Hindi/ProfileDetail/" + profileid + "_" + moduleid + "_" + NevigationId + "_" + menuid + "_" + position + ".aspx");
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

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
        MetaLang = MetaLng;
        MetaTitle = MetaTitles;
    }

    #endregion

 
}
