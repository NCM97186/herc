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

public partial class PetitionSearch : BasePage
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL = new PetitionBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    public static string UrlPrint = string.Empty;


    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        
         str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Petitions, Resources.HercResource.PetitionsSearch);

         gvOnlineStatus.ToolTip = Resources.HercResource.PetitionsSearch;
        ltrlBreadcrum.Text = str.ToString();
        p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/");
        obj_miscel.MakeAccessible(gvOnlineStatus);

        if (!IsPostBack)
        {
            bindRtiYearinDdl(ddlconnectionYearWise.SelectedValue.ToString());
            BindPetitionNumber(ddlconnectionYearWise.SelectedValue.ToString(), drpyear.SelectedValue);
            ddlPageSize.Visible = false;
            bindDdlPetitionStatus();
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

    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPetitionNumber(ddlconnectionYearWise.SelectedValue, drpyear.SelectedValue);
    }


    #region button btnsearch click event to search records

    protected void btnsearch_click(object sender, EventArgs e)
    {

        Bind_Petitionerserch(1);

    }

    #endregion


    protected void btnsearchYearwise_click(object sender, EventArgs e)
    {
        obj_petOB.ActionType = 1;
        obj_petOB.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        obj_petOB.PageIndex = 1;
        obj_petOB.PRONo = drppro.SelectedValue;
        obj_petOB.year = drpyear.SelectedValue;
        obj_petOB.ConnectionType = Convert.ToInt16(ddlconnectionYearWise.SelectedValue);

        p_Val.dsFileName = obj_petBL.Get_PetionSearch(obj_petOB, out p_Val.k);
        if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
        {
            MetaKeyword = p_Val.dsFileName.Tables[0].Rows[0]["MetaKeywords"].ToString();
            MetaDescription = p_Val.dsFileName.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            MetaLng = p_Val.dsFileName.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = p_Val.dsFileName.Tables[0].Rows[0]["MetaTitle"].ToString();
            Session["TempTable"] = p_Val.dsFileName.Tables[0];
            gvOnlineStatus.DataSource = p_Val.dsFileName;
            gvOnlineStatus.DataBind();

            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
        }
        else
        {
            gvOnlineStatus.DataSource = p_Val.dsFileName;
            gvOnlineStatus.DataBind();
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        p_Val.Result = p_Val.k;
        if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_Val.Result, 1, ddlPageSize, rptPager);
            rptPager.Visible = true;
        }
        else
        {
            rptPager.Visible = false;
        }

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
        //else
        //{
            drpyear.Items.Insert(0,new ListItem("Select", "0"));
        //}
    }

    #endregion

    #region function to bind search petition in gridview

    public void Bind_Petitionerserch(int pageIndex)
    {

        obj_petOB.ActionType = 2;
        obj_petOB.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        obj_petOB.PageIndex = pageIndex;
        obj_petOB.ConnectionType = Convert.ToInt16(ddlConnectionType.SelectedValue);
        obj_petOB.PRONo = null;
        if (txtname.Text != null && txtname.Text != "")
        {
            obj_petOB.PetitionerName = txtname.Text;
        }
        else
        {
            obj_petOB.PetitionerName = null;
        }
        if (ddlPetitionStatusUpdate.SelectedValue != null &&  ddlPetitionStatusUpdate.SelectedValue != "0")
        {
            obj_petOB.PetitionStatusId =Convert.ToInt16(ddlPetitionStatusUpdate.SelectedValue);
            if (Convert.ToInt16(ddlPetitionStatusUpdate.SelectedValue) == 14)
            {
                gvOnlineStatus.Columns[9].Visible = true;
            }
            else
            {
                gvOnlineStatus.Columns[9].Visible = false;
            }
        }
        else
        {
            gvOnlineStatus.Columns[9].Visible = false;
            obj_petOB.PetitionStatusId = null;
        } 
        if (txtrespodent.Text != null && txtrespodent.Text != "")
        {
            obj_petOB.RespondentName = txtrespodent.Text;
        }
        else
        {
            obj_petOB.RespondentName = null;
        }
        if (txtsubject.Text != null && txtsubject.Text != "")
        {
            obj_petOB.subject = txtsubject.Text;
        }
        else
        {
            obj_petOB.subject = null;
        }

        p_Val.dsFileName = obj_petBL.Get_PetionSearch(obj_petOB, out p_Val.k);
        if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
        {
            gvOnlineStatus.Visible = true;
            Session["TempTable"] = p_Val.dsFileName.Tables[0];
            gvOnlineStatus.DataSource = p_Val.dsFileName;
            gvOnlineStatus.DataBind();


            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
        }
        else
        {
            gvOnlineStatus.DataSource = p_Val.dsFileName;
            gvOnlineStatus.DataBind();
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        p_Val.Result = p_Val.k;
        if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_Val.Result, pageIndex, ddlPageSize, rptPager);
            rptPager.Visible = true;
        }
        else
        {
            rptPager.Visible = false;
        }

    }

    #endregion

    #region Button click event zone

    protected void btnOnlineStatus_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            Bind_Petitionerserch(1);

        }
    }

    #endregion

    #region Function to bind Petition status in dropDownlist

    public void bindDdlPetitionStatus()
    {
        //Miscelleneous_BL miscellBL=new Miscelleneous_BL();
        try
        {

            obj_petOB.PetitionStatusId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
            p_Val.dSet = miscellBL.getPetitionStatusAccordingtoModule(obj_petOB);
            ddlPetitionStatusUpdate.DataSource = p_Val.dSet;
            ddlPetitionStatusUpdate.DataTextField = "Status";
            ddlPetitionStatusUpdate.DataValueField = "Status_Id";
            ddlPetitionStatusUpdate.DataBind();
            ddlPetitionStatusUpdate.Items.Insert(0, new ListItem("Select Status", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    protected void gvOnlineStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "View")
        {
			
						p_Val.stringTypeID = e.CommandArgument.ToString();
					 if (ddlConnectionType.SelectedValue == "1")
                    {
				
						p_Val.strPopupID = "<script language='javascript'>" +
									   "window.open('ViewDetails.aspx?Petition_id=" + p_Val.stringTypeID + "', 'blank' + new Date().getTime()," +
									   "' menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
									   "</script>";
					}
					  else
					{
                        p_Val.strPopupID = "<script language='javascript'>" +
                                           "window.open('ViewDetails.aspx?RPID=" + p_Val.stringTypeID + "', 'blank' + new Date().getTime()," +
                                           "' menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                           "</script>";
                    }
					this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
				
        }
        if (e.CommandName == "ViewRP")
        {
			
					p_Val.stringTypeID = e.CommandArgument.ToString();
					p_Val.strPopupID = "<script language='javascript'>" +
									   "window.open('ViewDetails.aspx?RPID=" + p_Val.stringTypeID + "', 'blank' + new Date().getTime()," +
									   "' menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
									   "</script>";
					this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
					
			
        }
       
    }

    protected void gvOnlineStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //This is for popup details
            LinkButton lnkDetails = (LinkButton)e.Row.FindControl("lnkDetails");
            LinkButton lnkDetailsRP = (LinkButton)e.Row.FindControl("lnkDetailsRP");
            if (ddlConnectionType.SelectedValue == "1")
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

            //This is for popup details year wise
           
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

            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            string filename = DataBinder.Eval(e.Row.DataItem, "Petition_File").ToString();

            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
            Label lblPetitioner = (Label)e.Row.FindControl("lblPetitioner");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");
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
           
            if (filename == null || filename == "")
            {
                // lnk.Visible = false;
            }
        
            //connected Petition

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            obj_petOB.PetitionId = Convert.ToInt16(gvOnlineStatus.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Val.dsFileName = obj_petBL.getPetitionFileNames(obj_petOB);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    if (p_Val.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Val.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                    }
                  

                    p_Val.sbuilder.Append("<a href='" + p_Val.url + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' target='_blank'>" + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");

                    p_Val.sbuilder.Append("<br/><hr/>");

                }
                orderConnectedFile.Text = p_Val.sbuilder.ToString();

            }

            //End
        }
    }


    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Petitionerserch(1);
    }

    #endregion


    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Bind_Petitionerserch(pageIndex);
    }

    #endregion

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


    #region dropDownlist ddlConnectionType selectedIndexChanged events

    protected void ddlConnectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvOnlineStatus.Visible = false;
		 rptPager.Visible = false;
		 ddlPageSize.Visible    = false;
		txtname.Text = "";
         txtrespodent.Text = "";
         txtsubject.Text = "";
    }

    #endregion



    protected void ddlconnectionYearWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindRtiYearinDdl(ddlconnectionYearWise.SelectedValue.ToString());
        BindPetitionNumber(ddlconnectionYearWise.SelectedValue.ToString(), drpyear.SelectedValue);

    }
}
