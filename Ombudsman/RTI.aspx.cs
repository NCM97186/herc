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

public partial class Ombudsman_RTI :BasePageOmbudsman
{

    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
    RtiBL obj_rtBL = new RtiBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    public static string UrlPrint = string.Empty;
    public string lastUpdatedDate = string.Empty;

    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string PageTitle = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
	try{
        p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Rti"] + "/");
        p_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        p_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        if (!Page.IsPostBack)
        {
            str = BreadcrumDL.DisplayBreadCrumRTIOmbudsman(Resources.HercResource.RTI, Resources.HercResource.CurrentYearApplications);
            ltrlBreadcrum.Text = str.ToString();

            Bind_RTI(1);
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

        }

        if (ViewState["lastUpdateDate"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate"].ToString();
           
            if (ViewState["pageIndex"] != null)
            {
                Bind_RTI(Convert.ToInt16(ViewState["pageIndex"]));
            }
            else
            {
                Bind_RTI(1);
            }
        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            Bind_RTI(1);
            lnkFAA.Visible = false;
            lnkSAA.Visible = false;
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssRef);
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
	catch{}
	
	}
    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }
    

    #region function declaration zone

    public void Bind_RTI(int pageIndex)
    {
        obj_petOB.PageIndex = pageIndex;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            obj_petOB.PageSize = 10000;
        }
        else
        {
            obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }
        obj_petOB.LangId = Convert.ToInt32( Resources.HercResource.Lang_Id);
        obj_petOB.DepttId = 2;//OMBUDSMAN DEPT
        obj_petOB.year = System.DateTime.Now.Year.ToString();
       
            p_Val.dSet = obj_rtBL.Get_RTI(obj_petOB, out p_Val.k); 
        
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
				 lblmsg.Visible = false;
            }
        }
        else
        {
            Grdappeal.DataSource = null;
            Grdappeal.DataBind();
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;

			lblmsg.Visible = true;
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
           // lblmsg.ForeColor = System.Drawing.Color.Red;

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
            int RtistatusId = Convert.ToInt32(((HiddenField)e.Row.FindControl("status")).Value);
            //use for format of Reply sent status
            obj_petOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            obj_petOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_petOB.RTIId = RTID;
            LinkButton lblrefno = (LinkButton)e.Row.FindControl("lblrefno");
            Label lblyear = (Label)e.Row.FindControl("lblyear");

            Label lblApplicant = (Label)e.Row.FindControl("lblApplicant");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");

            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);

            Label lblStatusname = (Label)e.Row.FindControl("lblStatusname");


            lblrefno.Text = "EO/RTI-" + lblrefno.Text + " of " + lblyear.Text;
            p_Val.dSetChildData = obj_rtBL.Get_RTIById(obj_petOB);
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.replysent))
            {
                lblStatusname.Text = "Reply Sent" + "<br/>vide " + "Memo No: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();

            }
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.TransferedAuthority))
            {
                ((Label)e.Row.FindControl("lblStatusname")).Text = "Transferred To:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + " <br/>vide " + "Memo No: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();

            }
            obj_rtBL.Check_RTI_FAA(RTID, out p_Val.i);

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
                else
                {
                    ((LinkButton)e.Row.FindControl("lnklink")).Visible = true;
                    ((LinkButton)e.Row.FindControl("lnklink")).Text = "YES";
                }
            }

            if (string.IsNullOrEmpty(((HiddenField)e.Row.FindControl("hdfFile")).Value))
            {
                ((LinkButton)e.Row.FindControl("lbl_ViewDoc")).Visible = false;
            }


        }
    }

   
    protected void Grdappeal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Vdetail")
        //{
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
                            p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewRTIOmbudsmanDetails/" + e.CommandArgument + "_" + p_Val.PageID + "_" + p_Val.position) + ".aspx" + "', 'blank' + new Date().getTime()," +
                                               "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                               "</script>";
                            this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
                        else
                        {

                            p_Val.stringTypeID = e.CommandArgument.ToString();
                            p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "OmbudsmanContent/Hindi/ViewRTIOmbudsmanDetails/" + e.CommandArgument + "_" + p_Val.PageID + "_" + p_Val.position) + ".aspx" + "', 'blank' + new Date().getTime()," +
                                               "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
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
            Response.Redirect(ResolveUrl("~/CurrentFAAOmbudsman/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/OmbudsmanContent/Hindi/CurrentFAAOmbudsman/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
    }
    protected void lnkSAA_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/CurrentSAAOmbudsman/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/OmbudsmanContent/Hindi/CurrentSAAOmbudsman/" + p_Val.PageID + "_" + p_Val.position) + ".aspx");
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

