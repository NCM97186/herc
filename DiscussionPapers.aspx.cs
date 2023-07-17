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

public partial class DiscussionPapers : BasePage
{
    #region variable declaration

    string str = string.Empty;
    LinkBL objlnkBL = new LinkBL();
    LinkOB objlnkOB = new LinkOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    public static string UrlPrint = string.Empty;
    public string lastUpdatedDate = string.Empty;

    public string headerName = string.Empty;

    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;

    #endregion

    #region page load zone
    protected void Page_Load(object sender, EventArgs e)
    {
        current.Title = Resources.HercResource.CurrentDraftDisscussionPaper;
        currentHindi.Title = Resources.HercResource.CurrentDraftDisscussionPaper;
        previous.Title = Resources.HercResource.OldDraftDisscussionPaper;
        previousHindi.Title = Resources.HercResource.OldDraftDisscussionPaper;
        p_Val.Path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/";
        bool IsPageRefresh = false;

        if (string.IsNullOrEmpty(RewriteModule.RewriteContext.Current.Params["PrevYear"]))
        {
            str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.DDPapers, Resources.HercResource.CurrentDraftDisscussionPaper);
        }
        else
        {
            str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.DDPapers, Resources.HercResource.OldDraftDisscussionPaper);
        }

        ltrlBreadcrum.Text = str.ToString();

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            ViewState["year"] = Session["yearnew"];
            // pyear.Visible = true;
            Get_Discussionpapers(1);
            drpyear.Enabled = false;
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
                Get_Discussionpapers(1);
            }
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
                Get_Discussionpapers(1);
            }
            ////Get_Discussionpapers(1);
        }
        try
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
                {

                    previous.Attributes["class"] = "current";
                    headerName = Resources.HercResource.OldDraftDisscussionPaper;
                }

                else
                {
                    current.Attributes["class"] = "current";
                    headerName = Resources.HercResource.CurrentDraftDisscussionPaper;
                }
            }

            else
            {
                if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
                {

                    headerName = Resources.HercResource.OldDraftDisscussionPaper;
                    previousHindi.Attributes["class"] = "current";
                }

                else
                {
                    headerName = Resources.HercResource.CurrentDraftDisscussionPaper;
                    currentHindi.Attributes["class"] = "current";
                }
            }
        }
        catch { }


        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.DDPapers;
        }
        else
        {
            PageTitle = Resources.HercResource.DDPapers;
        }

    }
    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Get_Discussionpapers(1);
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Get_Discussionpapers(pageIndex);
    }

    #endregion

    #region function to get Discussion Papers

    public void Get_Discussionpapers(int pageIndex)
    {
        objlnkOB.PageIndex = pageIndex;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            objlnkOB.PageSize = 10000;
        }
        else
        {
            objlnkOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }

        objlnkOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        if (ViewState["year"] != null)
        {
            objlnkOB.Year = ViewState["year"].ToString();
        }
        else
        {

            if (string.IsNullOrEmpty(RewriteModule.RewriteContext.Current.Params["PrevYear"]))
            {

            }
            else
            {
                BindYear();
                objlnkOB.Year = drpyear.SelectedValue;
            }
        }
        if (string.IsNullOrEmpty(RewriteModule.RewriteContext.Current.Params["PrevYear"]))
        {
            objlnkOB.ActionType = 1;

        }
        else
        {
            objlnkOB.ActionType = 2;
            BindYear();
            if (ViewState["year"] != null)
            {
                drpyear.SelectedValue = ViewState["year"].ToString();
            }
        }
        p_Val.dSet = objlnkBL.GetDiscussionPapers(objlnkOB, out p_Val.k);

        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            gvDDPapaers.DataSource = p_Val.dSet;
            gvDDPapaers.DataBind();
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
            }
        }
        else
        {
            gvDDPapaers.DataSource = p_Val.dSet;
            gvDDPapaers.DataBind();
            DlastUpdate.Visible = false;
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;

            lblmsg.Visible = true;
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
            //lblmsg.ForeColor = System.Drawing.Color.Red;
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

    #region gridview Rowcommand event

    protected void gvDDPapaers_RowCommand(object sender, GridViewCommandEventArgs e)
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
            p_Val.strPopupID = "<script language='javascript'>" +
                               "window.open('" + ResolveUrl("~/" + "ViewDetails.aspx?Link_Id=" + p_Val.stringTypeID) + "&" + "ModuleId= " + Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) + "', 'blank' + new Date().getTime()," +
                               "' menubar=no, resizable=no, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                               "</script>";
            this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
        }

    }

    #endregion

    #region gridview RowDataBound event
    protected void gvDDPapaers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            //LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            string filename = DataBinder.Eval(e.Row.DataItem, "File_Name").ToString();
            Label PublicHearingDate = (Label)e.Row.FindControl("lblPublicHearingDate");
            Label Venue = (Label)e.Row.FindControl("lblVenue");

            if (PublicHearingDate.Text != null && PublicHearingDate.Text != "")
            {
                Venue.Text = Venue.Text;
            }
            else
            {
                Venue.Text = "";
            }
            if (filename == null || filename == "")
            {
                //lnk.Visible = false;
            }

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            objlnkOB.linkID = Convert.ToInt16(gvDDPapaers.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Val.dsFileName = objlnkBL.getDiscussionFileNames(objlnkOB);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    if (p_Val.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Val.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                    }
                    p_Val.sbuilder.Append("Dated: " + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + " ");

                    p_Val.sbuilder.Append("<a href='" + p_Val.Path + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' target='_blank'>" + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");

                    p_Val.sbuilder.Append("<br/><hr/>");
                }
                orderConnectedFile.Text = p_Val.sbuilder.ToString();
            }

        }
    }
    #endregion

    #region function to Bind data in a data list for year

    public void BindYear()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            if (RewriteModule.RewriteContext.Current.Params["PrevYear"] != null)
            {
                pyear.Visible = true;
            }
            else
            {
                pyear.Visible = false;
            }
        }
        else
        {
            pyear.Visible = true;
        }
        p_Val.dSetChildData = objlnkBL.Get_Year();

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
        Get_Discussionpapers(1);
    }


    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion


}
