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

public partial class ProfileDetail :BasePage
{
    //Area for data declaration zone
    Project_Variables p_Var = new Project_Variables();
    UserOB obj_userOB = new UserOB();
    UserBL obj_UserBL = new UserBL();
    Miscelleneous_BL obj_miscelBL = new Miscelleneous_BL();
    ProfileBL profileBL = new ProfileBL();
    ProfileOB profileOB = new ProfileOB();
    PaginationBL pagingBL = new PaginationBL();
    string str;
    public string Path = string.Empty;
    public string lastUpdatedDate = string.Empty;
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
        p_Var.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        //p_Var.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        if (RewriteModule.RewriteContext.Current.Params["menuid"]!= null)
        {
            p_Var.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        }

        Lnkback.Text = Resources.HercResource.Back;
       
		str = BreadcrumDL.DisplayBreadCrumPoliciesGuidelines(Resources.HercResource.Aboutus, Resources.HercResource.Profile,Resources.HercResource.Profiledetails);
        ltrlBreadcrum.Text = str.ToString();

        Path = ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Image"].ToString() + "/";
        if (!IsPostBack)
        {
            int ProID = Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["Profile_Id"].ToString());
            Viewdetail(ProID);

          
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
            PageTitle = Resources.HercResource.Profiledetails;
        }
        else
        {
            PageTitle = Resources.HercResource.Profiledetails;
        }
	}
        catch { }    
}



    public void Viewdetail(int ProfileId)
    {
        profileOB.profile_Id = ProfileId;
        p_Var.dSet =profileBL.UPS_View_Profile(profileOB);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            lastUpdatedDate = p_Var.dSet.Tables[0].Rows[0]["LastUpdatedDate"].ToString();
            lrtProfile.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["details"].ToString());


            MetaKeyword = p_Var.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
            MetaDescription = p_Var.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            MetaLng = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();

            if (p_Var.dSet.Tables[0].Rows[0]["Image_Name"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["Image_Name"].ToString() != "")
            {
                imgDiv.Visible = true;
                string imagename = p_Var.dSet.Tables[0].Rows[0]["Image_Name"].ToString();
                string url = Path + imagename;
                imag.ImageUrl = url;
                imag.AlternateText = p_Var.dSet.Tables[0].Rows[0]["Name"].ToString() + ", " + p_Var.dSet.Tables[0].Rows[0]["Designation"].ToString();
                imag.ToolTip = p_Var.dSet.Tables[0].Rows[0]["Name"].ToString() + ", " + p_Var.dSet.Tables[0].Rows[0]["Designation"].ToString();
            }
            else
            {
                imgDiv.Visible = false;
            }
            lbldesignation.Text = p_Var.dSet.Tables[0].Rows[0]["Designation"].ToString();
            
            lblName.Text = p_Var.dSet.Tables[0].Rows[0]["Name"].ToString();
           
        }
       
        
 
    }

    #region link button click event zone

    public void Lnkback_Click(object sender, EventArgs e)
    {

        if (RewriteModule.RewriteContext.Current.Params["NavigationId"].ToString() == "2")
        {
          //Response.Redirect("~/profilePrevious.aspx");
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Response.Redirect("~/profilePrevious/" + p_Var.PageID + "_" + p_Var.position + "_Profile.aspx");
            }
            else
            {
                Response.Redirect("~/content/Hindi/profilePrevious/" + p_Var.PageID + "_" + p_Var.position + "_Profile.aspx");
            }
        }
        else
        {
          //Response.Redirect("~/ProfileNew.aspx");
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Response.Redirect("~/Profile/" + p_Var.PageID + "_" + p_Var.position + "_Profile.aspx");
            }
            else
            {
                Response.Redirect("~/content/Hindi/Profile/" + p_Var.PageID + "_" + p_Var.position + "_Profile.aspx");
            }
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
