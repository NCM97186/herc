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

public partial class Ombudsman_PreviousSAAOmbudsman : BasePageOmbudsman
{
    #region variable declaration

    string str = string.Empty;
    Project_Variables p_Val = new Project_Variables();
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionOB obj_petOB = new PetitionOB();
    RtiBL obj_rtBL = new RtiBL();
    PaginationBL pagingBL = new PaginationBL();
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
        p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Rti"] + "/");
        str = BreadcrumDL.DisplayBreadCrumRTIOmbudsman(Resources.HercResource.RTI, Resources.HercResource.RTIPrevious);
        ltrlBreadcrum.Text = str.ToString();
        bool IsPageRefresh = false;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {

            ViewState["year"] = Session["yearnew"];
           // pyear.Visible = false;
            Bind_RTI(1);
            lnkFAA.Visible = false;
            lnkRTI.Visible = false;
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


    protected void grdFAA_SAA_RTI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            int RTID = Convert.ToInt32(((HiddenField)e.Row.FindControl("hyd")).Value);
            int RtistatusId = Convert.ToInt32(((HiddenField)e.Row.FindControl("Hystatus")).Value);

            LinkButton lblSAArefno1 = (LinkButton)e.Row.FindControl("lblSAArefno1");
            HiddenField HypYear = (HiddenField)e.Row.FindControl("HypYear");

            lblSAArefno1.Text = "EO/FAA-" + lblSAArefno1.Text;
            obj_petOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            obj_petOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_petOB.RTISaaId = RTID;
            obj_petOB.StatusId = 6;

            Label lblApplicant = (Label)e.Row.FindControl("lblApplicant");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");
            //lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            //lblApplicant.Text = HttpUtility.HtmlDecode(lblApplicant.Text);
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);

            p_Val.dSetChildData = obj_rtBL.getTempRTISAARecordsBYID(obj_petOB);
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.replysent))
            {
                ((Label)e.Row.FindControl("lblSAAstatus")).Text = obj_miscel.FixGivenCharacters(((Label)e.Row.FindControl("lblSAAstatus")).Text + "<br/>vide " + "Memo No: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo1"].ToString(),100);
            }
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.TransferedAuthority))
            {
                ((Label)e.Row.FindControl("lblSAAstatus")).Text = obj_miscel.FixGivenCharacters(((Label)e.Row.FindControl("lblSAAstatus")).Text + " : " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + "<br/>vide " + "Memo No: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/> Dated: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo1"].ToString(),100);
            }
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.judgement))
            {
                string strURL = p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderFour"].ToString();

                
            }
            HiddenField lblyear = (HiddenField)e.Row.FindControl("HypYear");
            //((LinkButton)e.Row.FindControl("lblSAArefno1")).Text = "HERC/SAA-" + ((LinkButton)e.Row.FindControl("lblSAArefno1")).Text + " of " + lblyear.Value;
            //((Label)e.Row.FindControl("lblSAArefno")).Text = "HERC/SIC-" + ((Label)e.Row.FindControl("lblSAArefno")).Text;// +" of " + lblyear.Text;
            if (string.IsNullOrEmpty(((HiddenField)e.Row.FindControl("hdfFile")).Value))
            {
                ((LinkButton)e.Row.FindControl("lbl_ViewDoc")).Visible = false;
            }
        }

      
    }


    protected void grdFAA_SAA_RTI_RowCommand(object sender, GridViewCommandEventArgs e)
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
            if (Session["update"] != null && ViewState["update"] != null)
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    p_Val.stringTypeID = e.CommandArgument.ToString();
                    p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Ombudsman/" + "ViewCurrentSAADetailsOmbudsman.aspx?id=" + p_Val.stringTypeID) + "', 'blank' + new Date().getTime()," +
                                       "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                       "</script>";
                    this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
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


        p_Val.dSet = obj_rtBL.Get_RTISAACurrent(obj_petOB, out p_Val.k);
       
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {

            grdFAA_SAA_RTI.DataSource = p_Val.dSet;
            grdFAA_SAA_RTI.DataBind();
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
            grdFAA_SAA_RTI.DataSource = null;
            grdFAA_SAA_RTI.DataBind();
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
        p_Val.dSetChildData = obj_rtBL.GetYearRTISAAPrevious(obj_petOB);

        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            //datalistYear.DataSource = p_Val.dSetChildData;
            //datalistYear.DataBind();
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();
        }
        else
        {
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataBind();
            drpyear.Items.Insert(0, new ListItem("Year", "Year"));
        }

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

    protected void lnkFAA_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/PreviousFAAOmbudsman/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/OmbudsmanContent/Hindi/PreviousFAAOmbudsman/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
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
