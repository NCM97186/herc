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

public partial class Ombudsman_appealonline :BasePageOmbudsman
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    public static string UrlPrint = string.Empty;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    #endregion 

    #region page load zone
    protected void Page_Load(object sender, EventArgs e)
    {
        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.Appeal, Resources.HercResource.OnlineStatus);
        ltrlBreadcrum.Text = str.ToString();
        if (!IsPostBack)
        {
            BindYear();
            BindAppealNumber();
            //Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.Appeal;
        }
        else
        {
            PageTitle = Resources.HercResource.Appeal;
        }
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssRef);

            if (Session["TempTable"] != null)
            {
                gvOnlineStatus.DataSource = (DataTable)Session["TempTable"];
                gvOnlineStatus.DataBind();
            }
        }
    }
    #endregion 


   #region function to Bind data in a data list for year

    public void BindYear()
    {


        p_Val.dSetChildData = obj_petBL.GetYearAppealForSearch();
        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();
        }

        drpyear.Items.Insert(0, new ListItem("Select", "0"));



    }


    #endregion


    #region function to Bind data in a data list for year

    public void BindAppealNumber()
    {


        // p_Val.dSetChildData = obj_petBL.GetApprovedAppealNoForSearch();
        obj_petOB.year = drpyear.SelectedValue;
        p_Val.dSetChildData = obj_petBL.getAppealNumberYearWise(obj_petOB);
        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            ddlappealNumber.DataSource = p_Val.dSetChildData;
            ddlappealNumber.DataTextField = "Appeal_Number";
            ddlappealNumber.DataValueField = "Appeal_Number";
            ddlappealNumber.DataBind();
        }
        else
        {
            ddlappealNumber.DataSource = p_Val.dSetChildData;

            ddlappealNumber.DataBind();
            ddlappealNumber.Items.Insert(0, new ListItem("Select", "0"));
        }
    }


    #endregion

    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAppealNumber();
        
    }
    #region Button search click event
    protected void btnsearch_click(object sender, EventArgs e)
    {
        Bind_OnlineStatus();
    }
    #endregion 

    protected void btnsearchYearwise_click(object sender, EventArgs e)
    {
        Bind_OnlineStatus();
    }

    #region function to bind online status 

    public void Bind_OnlineStatus()
    {
        obj_petOB.year = drpyear.SelectedValue;
        obj_petOB.AppealNo = ddlappealNumber.SelectedValue;
        p_Val.dSet = obj_petBL.Get_OnlineStatus(obj_petOB);
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
           Session["TempTable"] = p_Val.dSet.Tables[0];
           gvOnlineStatus.DataSource = p_Val.dSet;
           MetaKeyword = p_Val.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
           MetaDescription = p_Val.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
           MetaLng = p_Val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
           MetaTitles = p_Val.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();

        }
        else
        {
           gvOnlineStatus.DataSource = null;

        }
        gvOnlineStatus.DataBind();
		// Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
    }

    #endregion

    protected void gvOnlineStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "View")
        {

			  
					p_Val.stringTypeID = e.CommandArgument.ToString();
					p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Ombudsman/" + "ViewAppealDetails.aspx?Appeal_id=" + p_Val.stringTypeID) + "', 'blank' + new Date().getTime()," +
									   "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
									   "</script>";
					this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
				
	    }
			
        

    }

    protected void gvOnlineStatus_RowDataBound(object sender, GridViewRowEventArgs e)
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
