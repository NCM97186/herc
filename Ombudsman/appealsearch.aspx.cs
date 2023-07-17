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

public partial class Ombudsman_appealsearch :BasePageOmbudsman
{
      #region variable declaration

        string str = string.Empty;
        Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
        PetitionBL obj_petBL1 = new PetitionBL();
        AppealBL obj_petBL = new AppealBL();
        PetitionOB obj_petOB = new PetitionOB();
        Project_Variables p_Val = new Project_Variables();
        PaginationBL pagingBL = new PaginationBL();
        static string strsearch;
        string PageTitle = string.Empty;
        string MetaKeyword = string.Empty;
        string MetaDescription = string.Empty;
        string MetaLng = string.Empty;
        string MetaTitles = string.Empty;
        public static string UrlPrint = string.Empty;

      #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.Appeal, Resources.HercResource.SearchAppeal);
        ltrlBreadcrum.Text = str.ToString();
        if (!IsPostBack)
        {
            BindYear();
            BindAppealNumber();
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
                Grdappeal.DataSource = (DataTable)Session["TempTable"];
                Grdappeal.DataBind();
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


    #region button btnsearch click event to search records

    protected void btnsearch_click(object sender, EventArgs e)
    {


        Bind_Appeal(1);
        ////////obj_petOB.ActionType = 2;
        ////////obj_petOB.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        ////////obj_petOB.PageIndex = 1;
        ////////if (txtaplNumber.Text != null && txtaplNumber.Text != "")
        ////////{
        ////////    obj_petOB.AppealNo = txtaplNumber.Text;
        ////////}
        ////////else
        ////////{
        ////////    obj_petOB.AppealNo = null;
        ////////}
        ////////if (txtname.Text != null && txtname.Text != "")
        ////////{
        ////////    obj_petOB.ApplicantName = txtname.Text;
        ////////}
        ////////else
        ////////{
        ////////    obj_petOB.ApplicantName = null;
        ////////}
        ////////if (txtrespodent.Text != null && txtrespodent.Text != "")
        ////////{
        ////////    obj_petOB.RespondentName = txtrespodent.Text;
        ////////}
        ////////else
        ////////{
        ////////    obj_petOB.RespondentName = null;
        ////////}
        ////////if (txtsubject.Text != null && txtsubject.Text != "")
        ////////{
        ////////    obj_petOB.subject = txtsubject.Text;
        ////////}
        ////////else
        ////////{
        ////////    obj_petOB.subject = null;
        ////////}

        ////////p_Val.dsFileName = obj_petBL.Get_appeal_search(obj_petOB, out p_Val.k);
        ////////if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
        ////////{
        ////////    Grdappeal.DataSource = p_Val.dsFileName;
        ////////    Grdappeal.DataBind();
        ////////    //lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
        ////////    rptPager.Visible = true;
        ////////    ddlPageSize.Visible = true;
        ////////    lblPageSize.Visible = true;
        ////////}
        ////////else
        ////////{
        ////////    Grdappeal.DataSource = p_Val.dsFileName;
        ////////    Grdappeal.DataBind();
        ////////    rptPager.Visible = false;
        ////////    ddlPageSize.Visible = false;
        ////////    lblPageSize.Visible = false;
        ////////}
        ////////p_Val.Result = p_Val.k;
        ////////if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        ////////{
        ////////    pagingBL.Paging_Show(p_Val.Result, 1, ddlPageSize, rptPager);
        ////////    rptPager.Visible = true;
        ////////}
        ////////else
        ////////{
        ////////    rptPager.Visible = false;
        ////////}
       


    }

    #endregion

    protected void btnsearchYearwise_click(object sender, EventArgs e)
    {
        obj_petOB.ActionType = 1;
        obj_petOB.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        obj_petOB.PageIndex = 1;
        obj_petOB.AppealNo = ddlappealNumber.SelectedValue;
        obj_petOB.year = drpyear.SelectedValue;

        p_Val.dsFileName = obj_petBL.Get_appeal_search(obj_petOB, out p_Val.k);
        if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
        {
            MetaKeyword = p_Val.dsFileName.Tables[0].Rows[0]["MetaKeywords"].ToString();
            MetaDescription = p_Val.dsFileName.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            MetaLng = p_Val.dsFileName.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = p_Val.dsFileName.Tables[0].Rows[0]["MetaTitle"].ToString();
            Session["TempTable"] = p_Val.dsFileName.Tables[0];
            
            Grdappeal.DataSource = p_Val.dsFileName;
            Grdappeal.DataBind();
			Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
           
            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
        }
        else
        {
            Grdappeal.DataSource = p_Val.dsFileName;
            Grdappeal.DataBind();
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

    #region function declaration zone

    public void Bind_Appeal(int pageIndex)
    {
        obj_petOB.ActionType = 2;
        obj_petOB.PageIndex = pageIndex;
        obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        //if (txtaplNumber.Text != null && txtaplNumber.Text != "")
        //{
        //    obj_petOB.AppealNo = txtaplNumber.Text;
        //}
        //else
        //{
            obj_petOB.AppealNo = null;
        //}
        if (txtname.Text != null && txtname.Text != "")
        {
            obj_petOB.ApplicantName = txtname.Text;
        }
        else
        {
            obj_petOB.ApplicantName = null;
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
      

        p_Val.dSet = obj_petBL.Get_appeal_search(obj_petOB, out p_Val.k);
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            Session["TempTable"] = p_Val.dSet.Tables[0];
            ddlPageSize.Visible = true;
            Grdappeal.DataSource = p_Val.dSet;

            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
        }
        else
        {
            ddlPageSize.Visible = false;
            Grdappeal.DataSource = null;
            Grdappeal.DataBind();
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        Grdappeal.DataBind();
		 Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        strsearch = null;// string.Empty;
        this.Bind_Appeal(pageIndex);
    }

    #endregion



    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Appeal(1);
    }

    #endregion

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
