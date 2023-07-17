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

public partial class Ombudsman_AwardsUnderAppeal : BasePageOmbudsman
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

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.AwardsPronounced, Resources.HercResource.AwardUnderAppeal);
        ltrlBreadcrum.Text = str.ToString();
        p_Val.Path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Pdf"] + "/";
        obj_miscel.MakeAccessible(grdappeal);
        bool IsPageRefresh = false;

        if (!Page.IsPostBack)
        {
			  BindYear();
            Session.Remove("yearnew");
            ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();

            Session["SessionId"] = ViewState["ViewStateId"].ToString();

            if (string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                Bind_Award_Grid(1);
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
                Bind_Award_Grid(1);
            }
        }
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {

            ViewState["year"] = Session["yearnew"];
            pyear.Visible = false;
            Bind_Award_Grid(1);
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssRef);
        }

        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.AwardsPronounced;
        }
        else
        {
            PageTitle = Resources.HercResource.AwardsPronounced;
        }
    }

    #endregion 


    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    //Area for all the buttons, linkButtons, imageButtons click events

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        ViewState["pageIndex"] = pageIndex;
        this.Bind_Award_Grid(pageIndex);
    }

    #endregion

    //End

    //Area for all the dropDownlist events

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Award_Grid(1);
    }

    #endregion

    //End

    //Area for all the gridView,listView,Datalist events

    #region Datalist datalistYear itemCommand event zone

    protected void datalistYear_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            p_Val.str = e.CommandArgument.ToString();
            ViewState["year"] = p_Val.str;
            Bind_Award_Grid(1);
        }

    }

    #endregion

    //End

    //Area for all the user defined functions

    public void Bind_Award_Grid(int pageindex)
    {
        obj_petOB.PageIndex = pageindex;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            obj_petOB.PageSize = 10000;
        }
        else
        {
            obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }
        obj_petOB.LangId = Convert.ToInt32(Resources.HercResource.Lang_Id);
        obj_petOB.DepttId = 2;//Dept Id Ombandson
        if (ViewState["year"] != null)
        {
            obj_petOB.year = ViewState["year"].ToString();
            drpyear.SelectedValue = ViewState["year"].ToString();
        }
        else
        {
           // obj_petOB.year = (System.DateTime.Now.Year).ToString();
		    obj_petOB.year = (drpyear.SelectedValue).ToString();
        }

        p_Val.dSet = obj_petBL.GetAwardUnderAppeal(obj_petOB, out p_Val.k);
       
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            BindYear();
            grdappeal.DataSource = p_Val.dSet;
            grdappeal.DataBind();
            lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            ViewState["lastUpdateDate"] = lastUpdatedDate;

            MetaKeyword = p_Val.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
            MetaDescription = p_Val.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            MetaLng = p_Val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = p_Val.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();

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
            grdappeal.DataSource = p_Val.dSet;
            grdappeal.DataBind();
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
            pagingBL.Paging_Show(p_Val.Result, pageindex, ddlPageSize, rptPager);
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

    #region function to Bind data in a data list for year

    public void BindYear()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            pyear.Visible = false;
        }
        else
        {
            pyear.Visible = true;
        }
       // p_Val.dSetChildData = obj_petBL.GetAwardUnderAppealYear();
        p_Val.dSetChildData = obj_petBL.GetAwardUnderAppealYearOmbudsman();
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
        Bind_Award_Grid(1);
    }


    protected void grdappeal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDoc")
        {

            string file = e.CommandArgument.ToString();
            p_Val.Path = p_Val.Path + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(p_Val.Path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(p_Val.Path));
                Response.Clear();
                Response.WriteFile(p_Val.Path);
                Response.End();
            }
        }
        if (e.CommandName == "getappeal_Detail")
        {
             p_Val.id = e.CommandArgument.ToString();

                    p_Val.strPopupID = "<script language='javascript'>" +
                                     "window.open('" + ResolveUrl("~/") + "Ombudsman/ViewAppealDetails.aspx?Awardid=" + p_Val.id + "', 'blank' + new Date().getTime()," +
                                     "' menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                     "</script>";
                    this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
               
        }
    }
    //End

    protected void grdappeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            //LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");

            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");


            //connected Award files
            Literal orderAwardFile = (Literal)e.Row.FindControl("ltrlConnectedAwardProunced");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
            obj_petOB.AppealId = Convert.ToInt16(hdf.Value);
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            //p_Val.dsFileName = obj_petBL.getConnectedAwardFiles(obj_petOB);
            p_Val.dsFileName = obj_petBL.getAppealAwardPronounced(obj_petOB);
            
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Val.sbuilder.Append(" <a href='" + p_Val.Path + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "' target='_blank'>" + "Award,Dated:" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "</a>");
                    p_Val.sbuilder.Append("<br/><hr/>");

                }
                orderAwardFile.Text = p_Val.sbuilder.ToString();

            }
            else
            {

            }

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
