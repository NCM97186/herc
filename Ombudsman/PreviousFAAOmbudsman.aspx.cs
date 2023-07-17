﻿using System;
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

public partial class Ombudsman_PreviousFAAOmbudsman : BasePageOmbudsman
{
    #region variable declaration

    string str = string.Empty;
    Project_Variables p_Val = new Project_Variables();
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionOB obj_petOB = new PetitionOB();
    RtiBL obj_rtBL = new RtiBL();
    PaginationBL pagingBL = new PaginationBL();
    static int FAA_RTI;
    public static string UrlPrint = string.Empty;
    public string lastUpdatedDate = string.Empty;

    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        if (RewriteModule.RewriteContext.Current.Params["menuid"] != null)
        {
            p_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        }
        FAA_RTI = Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["rtiid"]);
        p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Rti"] + "/");
        str = BreadcrumDL.DisplayBreadCrumRTIOmbudsman(Resources.HercResource.RTI, Resources.HercResource.RTIPrevious);
        ltrlBreadcrum.Text = str.ToString();
        bool IsPageRefresh = false;

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            ViewState["year"] = Session["yearnew"];
            //pyear.Visible = false;
            Bind_RTI(1);
            lnkRTI.Visible = false;
            lnkSAA.Visible = false;
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
                Bind_RTI(1);
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
                Bind_RTI(1);
            }
         
        }


        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.RTI;
        }
        else
        {
            PageTitle = Resources.HercResource.RTI;
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
        obj_petOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
        p_Val.dSetChildData = obj_rtBL.GetYearRTIFAAPrevious(obj_petOB);

        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();

            //datalistYear.DataSource = p_Val.dSetChildData;
            //datalistYear.DataBind();
        }
        else
        {
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataBind();
            drpyear.Items.Insert(0, new ListItem("Year", "Year"));
        }
    }
  #endregion

    protected void grdrtifAA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int RTID = Convert.ToInt32(((HiddenField)e.Row.FindControl("hyd")).Value);
            int RtistatusId = Convert.ToInt32(((HiddenField)e.Row.FindControl("status")).Value);

            //use for format of Reply sent status
            obj_petOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            obj_petOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            Label lblyear = (Label)e.Row.FindControl("lblyear");

            Label lblApplicant = (Label)e.Row.FindControl("lblApplicant");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");
            //lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            //lblApplicant.Text = HttpUtility.HtmlDecode(lblApplicant.Text);
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);

            LinkButton lblFAArefno = (LinkButton)e.Row.FindControl("lblFAArefno");
            lblFAArefno.Text = "EO/FAA-" + lblFAArefno.Text + " of " + lblyear.Text;
            obj_petOB.RTIFAAId = RTID;
            obj_petOB.StatusId = 6;
            p_Val.dSetChildData = obj_rtBL.getTempRTIFAARecordsBYID(obj_petOB);
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.replysent))
            {
                ((Label)e.Row.FindControl("lblFAAstatus")).Text = obj_miscel.FixGivenCharacters(((Label)e.Row.FindControl("lblFAAstatus")).Text + "<br/>vide " + "Memo No:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo1"].ToString(),100);
            }
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.TransferedAuthority))
            {
                ((Label)e.Row.FindControl("lblFAAstatus")).Text = obj_miscel.FixGivenCharacters(((Label)e.Row.FindControl("lblFAAstatus")).Text + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + "<br/>vide " + "Memo No:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/> Dated:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo1"].ToString(),100);
            }

            if (string.IsNullOrEmpty(((HiddenField)e.Row.FindControl("hdfFile")).Value))
            {
                ((LinkButton)e.Row.FindControl("lbl_ViewDoc")).Visible = false;
            }
           
            //((LinkButton)e.Row.FindControl("lblFAArefno")).Text = "HERC/FAA-" + ((LinkButton)e.Row.FindControl("lblFAArefno")).Text + " of " + lblyear.Text;


            obj_rtBL.Check_RTI_FAA_SAA(RTID, out p_Val.i);
            if (p_Val.i == 0)
            {
                ((LinkButton)e.Row.FindControl("lnlbtn")).Visible = false;
                ((LinkButton)e.Row.FindControl("lnlbtn")).Text = "N";
            }
            else
            {
                if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.inprocess))
                {
                    ((LinkButton)e.Row.FindControl("lnlbtn")).Visible = false;
                }
                else
                {
                    ((LinkButton)e.Row.FindControl("lnlbtn")).Visible = true;
                    ((LinkButton)e.Row.FindControl("lnlbtn")).Text = "YES";
                }
            }
        }
    }


    protected void grdrtifAA_RowCommand(object sender, GridViewCommandEventArgs e)
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
        if (e.CommandName == "View")
        {
           
                    p_Val.stringTypeID = e.CommandArgument.ToString();
                    p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Ombudsman/" + "ViewCurrentFAADetailsOmbudsman.aspx?id=" + p_Val.stringTypeID) + "', 'blank' + new Date().getTime()," +
                                       "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                       "</script>";
                    this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
                 

        }
        if (e.CommandName == "vdetail")
        {
            
                    string stringTypeID = e.CommandArgument.ToString();
                    string strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Ombudsman/" + "ViewCurrentFAADetailsOmbudsman.aspx?id=" + stringTypeID) + "', 'blank' + new Date().getTime()," +
                                         "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                         "</script>";
                    this.Page.RegisterStartupScript("PopupScript", strPopupID);

              
        }
    }

    #region function declaration zone

    public void Bind_RTI(int pageIndex)
    {
        BindYear();
        obj_petOB.PageIndex = pageIndex;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            obj_petOB.PageSize = 10000;
        }
        else
        {
            obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }
        obj_petOB.LangId = Convert.ToInt32(Resources.HercResource.Lang_Id);
        obj_petOB.DepttId = 2;
        if (ViewState["year"] != null)
        {
            obj_petOB.year = ViewState["year"].ToString();
            drpyear.SelectedValue = ViewState["year"].ToString();
        }
        else
        {
            obj_petOB.year = null;
           //obj_petOB.year = Convert.ToString(Convert.ToInt32(System.DateTime.Now.Year) - 1);
        }

        p_Val.dSet = obj_rtBL.Get_RTIFAACurrent(obj_petOB, out p_Val.k);
      
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {

            grdrtifAA.DataSource = p_Val.dSet;
            grdrtifAA.DataBind();
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
            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                pyear.Visible = true;
            }
            else
            {
                pyear.Visible = true;
            }
        }
        else
        {
            DlastUpdate.Visible = false;
            grdrtifAA.DataSource = null;
            grdrtifAA.DataBind();
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
            pyear.Visible = false;

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

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        ViewState["pageIndex"] = pageIndex;
        this.Bind_RTI(pageIndex);
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_RTI(1);
    }

    #endregion

    #region Datalist datalistYear itemCommand event zone

    protected void datalistYear_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            p_Val.str = e.CommandArgument.ToString();
            ViewState["year"] = p_Val.str;
            Bind_RTI(1);
        }
    }

    #endregion

  
    protected void lnkSAA_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/PreviousSAAOmbudsman/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/OmbudsmanContent/Hindi/PreviousSAAOmbudsman/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
    }
    protected void lnkRTI_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/perviousrti/" + p_Val.PageID + "_" + p_Val.position) + "_Previousyearapplications.aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/OmbudsmanContent/Hindi/perviousrti/" + p_Val.PageID + "_" + p_Val.position) + "_Previousyearapplications.aspx");
        }
    }

    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["year"] = drpyear.SelectedValue;
        Session["yearnew"] = drpyear.SelectedValue;

        Bind_RTI(1);
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
