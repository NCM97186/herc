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

public partial class Ombudsman_CurrentSAA : BasePageOmbudsman
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

    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        if (RewriteModule.RewriteContext.Current.Params["menuid"] != null)
        {
            p_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        }
        p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Rti"] + "/");
        str = BreadcrumDL.DisplayBreadCrumRTIOmbudsman(Resources.HercResource.RTI, Resources.HercResource.CurrentYearApplications);
        ltrlBreadcrum.Text = str.ToString();

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

        if (!IsPostBack)
        {
            Bind_RTI(1);
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            Bind_RTI(1);
            lnkFAA.Visible = false;
            lnkRTI.Visible = false;
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

            Label lblApplicant = (Label)e.Row.FindControl("lblApplicant");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");
            //lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            //lblApplicant.Text = HttpUtility.HtmlDecode(lblApplicant.Text);
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);

            lblSAArefno1.Text = "EO/FAA-" + lblSAArefno1.Text;
            //use for format of Reply sent status
            obj_petOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            obj_petOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_petOB.RTISaaId = RTID;
            obj_petOB.StatusId = 6;
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


            if (string.IsNullOrEmpty(((HiddenField)e.Row.FindControl("hdfFile")).Value))
            {
                ((LinkButton)e.Row.FindControl("lbl_ViewDoc")).Visible = false;
            }


            HiddenField lblyear = (HiddenField)e.Row.FindControl("HypYear");
           
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
                    p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Ombudsman/" + "ViewCurrentSAADetailsOmbudsman.aspx?id=" + p_Val.stringTypeID) + "', 'blank' + new Date().getTime()," +
                                       "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                       "</script>";
                    this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
               
        }
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
        obj_petOB.LangId = Convert.ToInt32(Resources.HercResource.Lang_Id);
        obj_petOB.DepttId = 2;//HERC DEPT
        obj_petOB.year = System.DateTime.Now.Year.ToString();

        p_Val.dSet = obj_rtBL.Get_RTISAACurrent(obj_petOB, out p_Val.k);

        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {

            grdFAA_SAA_RTI.DataSource = p_Val.dSet;
            grdFAA_SAA_RTI.DataBind();
            lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            ViewState["lastUpdateDate"] = lastUpdatedDate;
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
            DlastUpdate.Visible = false;
            grdFAA_SAA_RTI.DataSource = null;
            grdFAA_SAA_RTI.DataBind();
            rptPager.Visible    = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;

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
   

    protected void lnkRTI_click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(ResolveUrl("~/RTICurrent/" + p_Val.PageID + "_" + p_Val.position) + "_Currentyearapplications.aspx");
        }
        else
        {
            Response.Redirect(ResolveUrl("~/OmbudsmanContent/Hindi/RTICurrent/" + p_Val.PageID + "_" + p_Val.position) + "_Currentyearapplications.aspx");
        }
    }

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitleOmbudsman;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion


}
