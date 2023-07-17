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

public partial class Prevyearapplications : BasePage
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
    RtiBL obj_rtBL = new RtiBL();
    PetitionOB obj_petOB = new PetitionOB();
    Miscelleneous_BL obj = new Miscelleneous_BL();
    public static string UrlPrint = string.Empty;
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    public string lastUpdatedDate = string.Empty;
	string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            str = BreadcrumDL.DisplayBreadCrumRTI(Resources.HercResource.RTI, Resources.HercResource.RTIPrevious);
            ltrlBreadcrum.Text = str.ToString();
            p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Rti"] + "/");
            obj_miscel.MakeAccessible(Grdappeal);
            bool IsPageRefresh = false;
            p_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
            p_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();

            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
            {

                ViewState["year"] = Session["yearnew"];
                //pyear.Visible = false;
                Bind_RTI(1);
                drpyear.Enabled = false;
                lnkSAA.Visible = false;
                lnkFAA.Visible = false;
                HtmlLink cssRef = new HtmlLink();
                cssRef.Href = "css/print.css";
                cssRef.Attributes["rel"] = "stylesheet";
                cssRef.Attributes["type"] = "text/css";
                ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
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
                //Bind_RTI(1);
            }


            if (Resources.HercResource.Lang_Id == "1")
            {
                PageTitle = Resources.HercResource.RTIPrevious;
            }
            else
            {
                PageTitle = Resources.HercResource.RTIPrevious;
            }
        }
        catch { }
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
        p_Val.dSetChildData = obj_rtBL.GetRTIYear();

        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            //datalistYear.DataSource = p_Val.dSetChildData;
            //datalistYear.DataBind();
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();
        }

    }
    #endregion

    #region function bind previous year rti in gridview

    public void Bind_RTI(int pageIndex)
    {
        BindYear();
        obj_petOB.LangId = 1;
        obj_petOB.DepttId = 1; // Herc Dept ID
        obj_petOB.PageIndex = pageIndex;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            obj_petOB.PageSize = 10000;
        }
        else
        {
            obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }
        if (ViewState["year"] != null)
        {
            obj_petOB.year = ViewState["year"].ToString();
            drpyear.SelectedValue = ViewState["year"].ToString();
        }
        else
        {
           // obj_petOB.year = Convert.ToString(Convert.ToInt32(System.DateTime.Now.Year) - 1);
            obj_petOB.year = null;
        }

        p_Val.dSet = obj_rtBL.Get_PrevYear_RTI(obj_petOB, out p_Val.k);
       
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {

            Grdappeal.DataSource = p_Val.dSet;
            Grdappeal.DataBind();
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
            }
        }
        else
        {
            Grdappeal.DataSource = p_Val.dSet;
            Grdappeal.DataBind();
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

    protected void Grdappeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int RTID = Convert.ToInt32(((HiddenField)e.Row.FindControl("hyd")).Value);
            int RtistatusId = Convert.ToInt32(((HiddenField)e.Row.FindControl("rtistatus")).Value);
            //use for format of Reply sent status

            obj_petOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            obj_petOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_petOB.RTIId = RTID;
            p_Val.dSetChildData = obj_rtBL.Get_RTIById(obj_petOB);
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.replysent))
            {
                ((Label)e.Row.FindControl("lblstatus")).Text = obj.FixGivenCharacters(((Label)e.Row.FindControl("lblstatus")).Text + " vide " + "<br/>Memo No:" + " " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated:" + " " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString(),100);
                //((Label)e.Row.FindControl("lblstatus")).Text = ((Label)e.Row.FindControl("lblstatus")).Text + "<br/>memo no:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " dated<br/>" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
            }
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.TransferedAuthority))
            {
                ((Label)e.Row.FindControl("lblstatus")).Text = obj.FixGivenCharacters(((Label)e.Row.FindControl("lblstatus")).Text + ": " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + " vide " + "<br/>Memo No:" + " " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/> Dated:" + " " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString(),100);
                //((Label)e.Row.FindControl("lblstatus")).Text = ((Label)e.Row.FindControl("lblstatus")).Text + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + "vide " + "<br/>memo no:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " dated<br/>" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
            }
            obj_rtBL.Check_RTI_FAA(RTID, out p_Val.i);
            Label lblyear = (Label)e.Row.FindControl("lblyear");
            //((Label)e.Row.FindControl("lblrefno")).Text = "HERC/RTI-" + ((Label)e.Row.FindControl("lblrefno")).Text + " of " + lblyear.Text; 


            if (p_Val.i == 0)
            {
                ((LinkButton)e.Row.FindControl("lnklink")).Visible = false;
                ((LinkButton)e.Row.FindControl("lnklink")).Text = "N";
            }
            else
            {
                if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.inprocess))
                {
                    ((LinkButton)e.Row.FindControl("lnklink")).Visible = false;
                }



                //new code added on date : 23-12-2013

                else if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.TransferedAuthority))
                {
                    ((LinkButton)e.Row.FindControl("lnklink")).Visible = true;
                    ((LinkButton)e.Row.FindControl("lnklink")).Text = "YES";
                    ((Label)e.Row.FindControl("lblstatus")).Text = obj.FixGivenCharacters( ((HiddenField)e.Row.FindControl("hydstatus")).Value + "  Memo no " + ((LinkButton)e.Row.FindControl("lblrefno")).Text + " Of " + ((Label)e.Row.FindControl("lblyear")).Text + " dated " + ((HiddenField)e.Row.FindControl("hydapdate")).Value,100);



                }
                else if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.replysent))
                {
                    ((LinkButton)e.Row.FindControl("lnklink")).Visible = true;
                    ((LinkButton)e.Row.FindControl("lnklink")).Text = "YES";
                    ((Label)e.Row.FindControl("lblstatus")).Text = obj.FixGivenCharacters(((HiddenField)e.Row.FindControl("hydstatus")).Value + " vide " + "<br/>Memo No:" + " " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated:" + " " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString(),100);
                   

                }
                else
                {
                    ((LinkButton)e.Row.FindControl("lnklink")).Visible = true;
                    ((LinkButton)e.Row.FindControl("lnklink")).Text = "YES";
                    ((Label)e.Row.FindControl("lblstatus")).Text = ((HiddenField)e.Row.FindControl("hydstatus")).Value;
                }

                //End

            }

            if (string.IsNullOrEmpty(((HiddenField)e.Row.FindControl("hdfFile")).Value))
            {
                ((LinkButton)e.Row.FindControl("lbl_ViewDoc")).Visible = false;
            }


           
        }

    }

    protected void Grdappeal_RowCommand(object sender, GridViewCommandEventArgs e)
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

        if (e.CommandName == "Vdetail")
        {
           
                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                    {
                        p_Val.stringTypeID = e.CommandArgument.ToString();
                        p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewRTIDetails/" + e.CommandArgument + "_" + p_Val.PageID + "_" + p_Val.position) + ".aspx" + "', 'blank' + new Date().getTime()," +
                                           "'menubar=no, resizable=Yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                           "</script>";
                        this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);

                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                    else
                    {
                        p_Val.stringTypeID = e.CommandArgument.ToString();
                        p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "Content/Hindi/ViewRTIDetails/" + e.CommandArgument + "_" + p_Val.PageID + "_" + p_Val.position) + ".aspx" + "', 'blank' + new Date().getTime()," +
                                           "'menubar=no, resizable=no, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                           "</script>";
                        this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
            
        }
    }



    protected void lnkFAA_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/PreviousFAA/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/Content/Hindi/PreviousFAA/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
    }
    protected void lnkSAA_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/PreviousSAA/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/Content/Hindi/PreviousSAA/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
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


    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["year"] = drpyear.SelectedValue;

        Session["yearnew"] = drpyear.SelectedValue;
        Bind_RTI(1);
    }


}


