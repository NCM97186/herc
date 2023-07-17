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

public partial class OnlineStatus : BasePage
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL = new PetitionBL();
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

    protected void Page_Load(object sender, EventArgs e)
    {
		current.Title = Resources.HercResource.CurrentYearPetitions;
        currentHindi.Title = Resources.HercResource.CurrentYearPetitions;
        previous.Title = Resources.HercResource.PreviousYearPetitions;
        previousHindi.Title = Resources.HercResource.PreviousYearPetitions;

        str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Petitions, Resources.HercResource.OnlineStatus);
        gvOnlineStatus.ToolTip = Resources.HercResource.OnlineStatus;
        ltrlBreadcrum.Text = str.ToString();
        p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/");
        obj_miscel.MakeAccessible(gvOnlineStatus);
        if (!IsPostBack)
        {
            bindRtiYearinDdl(ddlconnectionYearWise.SelectedValue.ToString());
            BindPetitionNumber(ddlconnectionYearWise.SelectedValue.ToString(), drpyear.SelectedValue);
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			 ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);

            if (Session["TempTable"] != null)
            {
                gvOnlineStatus.DataSource = (DataTable)Session["TempTable"];
                gvOnlineStatus.DataBind();
            }
        }
        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.Petitions;
        }
        else
        {
            PageTitle = Resources.HercResource.Petitions;
        }

    }

    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }



    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        BindPetitionNumber(ddlconnectionYearWise.SelectedValue.ToString(), drpyear.SelectedValue);
    }

   

    #region function to bind gridview with current status of petition

    public void Bind_OnlineStatus()
    {

        obj_petOB.ConnectionType = Convert.ToInt16(ddlconnectionYearWise.SelectedValue);
        obj_petOB.PRONo = drppro.SelectedValue;
        obj_petOB.year = drpyear.SelectedValue;
        p_Val.dSet = obj_petBL.Get_OnlineStatus(obj_petOB);
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            Session["TempTable"] = p_Val.dSet.Tables[0];
            gvOnlineStatus.Visible = true;
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
    }

    #endregion

    #region Button click event zone

    protected void btnOnlineStatus_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            Bind_OnlineStatus();


        }
    }

    #endregion

    protected void btnsearchYearwise_click(object sender, EventArgs e)
    {
        Bind_OnlineStatus(); 

    }

    protected void gvPetition_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "View")
        {
			

					p_Val.stringTypeID = e.CommandArgument.ToString();
					p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewDetails.aspx?Petition_id=" + p_Val.stringTypeID) + "', 'blank' + new Date().getTime()," +
									   "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
									   "</script>";
					this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
					
			
        }

        if (e.CommandName == "ViewRP")
        {
			
					p_Val.stringTypeID = e.CommandArgument.ToString();
					p_Val.strPopupID = "<script language='javascript'>" +
									   "window.open('ViewDetails.aspx?RPID=" + p_Val.stringTypeID + "', 'mywindow', " +
									   "' menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
									   "</script>";
					this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
					
			
        }

    }

    protected void gvPetition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            string filename = DataBinder.Eval(e.Row.DataItem, "Petition_File").ToString();
         
            if (filename == null || filename == "")
            {
               
            }

            //connected Petition

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            obj_petOB.PetitionId = Convert.ToInt16(gvOnlineStatus.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Val.dsFileName = obj_petBL.getPetitionFileNames(obj_petOB);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {

                    p_Val.sbuilder.Append("Dated: " + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + " ");

                    p_Val.sbuilder.Append("<a href='" + p_Val.url + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' target='_blank'>" + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");

                    p_Val.sbuilder.Append("<br/><hr/>");

                }
                orderConnectedFile.Text = p_Val.sbuilder.ToString();

            }

            //End

            //This is for popup details
            LinkButton lnkDetails = (LinkButton)e.Row.FindControl("lnkDetails");
            LinkButton lnkDetailsRP = (LinkButton)e.Row.FindControl("lnkDetailsRP");
            if (ddlconnectionYearWise.SelectedValue == "1")
            {
                lnkDetails.Visible = true;
                lnkDetailsRP.Visible = false;
            }
            else
            {
                lnkDetails.Visible = false;
                lnkDetailsRP.Visible = true;
            }
            //End

			  Label lblremark = (Label)e.Row.FindControl("lblRemarks");
            if (lblremark.Text != null && lblremark.Text != "")
            {
                lblremark.Text = HttpUtility.HtmlDecode(lblremark.Text);
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


    protected void ddlconnectionYearWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindRtiYearinDdl(ddlconnectionYearWise.SelectedValue.ToString());
        BindPetitionNumber(ddlconnectionYearWise.SelectedValue.ToString(), drpyear.SelectedValue);
        gvOnlineStatus.Visible = false;
    }



    #region Function to bind Petition Year

    public void bindRtiYearinDdl(string connectionType)
    {
        obj_petOB.ConnectionType = Convert.ToInt16(connectionType);
        p_Val.dSet = obj_petBL.GetYearPetitionSearch(obj_petOB);

        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = p_Val.dSet;
            drpyear.DataTextField = "year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();
        }
       
        drpyear.Items.Insert(0, new ListItem("Select", "0"));
       
    }

    #endregion



    #region function to Bind data in a data list for year

    public void BindPetitionNumber(string ConnectionType, string Year)
    {

        obj_petOB.year = Year.Trim();
        obj_petOB.ConnectionType = Convert.ToInt16(ConnectionType);
        p_Val.dSetChildData = obj_petBL.getPetition_ReviewNumbers(obj_petOB);
        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drppro.DataSource = p_Val.dSetChildData;
            drppro.DataTextField = "PRO_No";
            drppro.DataValueField = "PRO_No";
            drppro.DataBind();
        }
        else
        {
            drppro.DataSource = p_Val.dSetChildData;
            drppro.DataBind();
            drppro.Items.Insert(0, new ListItem("Select", "0"));
        }
    }


    #endregion 
}
