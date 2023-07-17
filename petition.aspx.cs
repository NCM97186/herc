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
using System.Data.SqlClient;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.IO;
using iTextSharp.text.html;
using iTextSharp.text.xml;
using iTextSharp.text.html.simpleparser;
using System.Text.RegularExpressions;

public partial class petition : BasePage
{
    #region variable declaration

    string str = string.Empty;
    public Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL = new PetitionBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    public static string UrlPrint = string.Empty;
    public string lastUpdatedDate = string.Empty;

    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    #endregion

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {

        current.Title = Resources.HercResource.CurrentYearPetitions;
        currentHindi.Title = Resources.HercResource.CurrentYearPetitions;
        previous.Title = Resources.HercResource.PreviousYearPetitions;
        previousHindi.Title = Resources.HercResource.PreviousYearPetitions;
        bool IsPageRefresh = false;
        try
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
                {

                    previous.Attributes["class"] = "current";
                }
                else
                {
                    current.Attributes["class"] = "current";
                }
            }
            else
            {
                if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
                {

                    previousHindi.Attributes["class"] = "current";
                }
                else
                {
                    currentHindi.Attributes["class"] = "current";
                }
            }


            if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
            {
                lnkreview.Text = "View Previous Years Review Petitions";
                hPetition.InnerText = Resources.HercResource.PreviousYearPetitions;
                gvPetition.ToolTip = Resources.HercResource.PreviousYearPetitions;

            }
            else
            {
                lnkreview.Text = "View Current Year Review Petitions";
                hPetition.InnerText = Resources.HercResource.CurrentPetition;
                gvPetition.ToolTip = Resources.HercResource.CurrentPetition;
            }
            if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
            {
                str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Petitions, Resources.HercResource.PreviousYearPetitions);
            }
            else
            {
                str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Petitions, Resources.HercResource.CurrentPetition);
            }

            ltrlBreadcrum.Text = str.ToString();
            p_Val.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/";

            p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/");
            obj_miscel.MakeAccessible(gvPetition);
            //CreateDynamicTable();
            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                ViewState["year"] = Session["yearnew"];

                Bind_Petition(1);
                BindYear();
                if (ViewState["year"] != null)
                {
                    drpyear.SelectedValue = ViewState["year"].ToString();
                }
                drpyear.Enabled = false;
                lnkreview.Visible = false;
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
                    Bind_Petition(1);
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }

                //BindYear();

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
                    Bind_Petition(1);
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
        catch { }
    }

    #endregion

    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    #region function to bind petition in gridview

    public void Bind_Petition(int pageIndex)
    {
        try
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
            if (ViewState["year"] != null)
            {
                obj_petOB.year = ViewState["year"].ToString();

            }
            else
            {
                obj_petOB.year = null;
            }
            if (RewriteModule.RewriteContext.Current.Params["PrevYear"] != null && ViewState["year"] == null)
            {
                p_Val.dSet = obj_petBL.Get_Petition_PrevYear(obj_petOB, out p_Val.k);
                BindYear();
                if (ViewState["year"] != null)
                {
                    drpyear.SelectedValue = ViewState["year"].ToString();
                }
            }
            else
            {
                p_Val.dSet = obj_petBL.Get_Petition(obj_petOB, out p_Val.k);
            }
            if (p_Val.dSet.Tables[0].Rows.Count > 0)
            {
                gvPetition.DataSource = p_Val.dSet;
                gvPetition.DataBind();
                lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
                MetaKeyword = p_Val.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
                MetaDescription = p_Val.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
                MetaLng = p_Val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
                MetaTitles = p_Val.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
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

                lblmsg.Visible = false;
            }
            else
            {
                rptPager.Visible = false;
                ddlPageSize.Visible = false;
                lblPageSize.Visible = false;
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
        catch { }
    }

    #endregion

    protected void gvPetition_RowCommand(object sender, GridViewCommandEventArgs e)
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
            p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewDetails.aspx?Petition_id=" + p_Val.stringTypeID) + "', 'blank' + new Date().getTime()," +
                               "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
                               "</script>";
            this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());


        }

    }

    protected void gvPetition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

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
            if (lblRemarks.Text != null && lblRemarks.Text != "")
            {
                lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
            }

            if (filename == null || filename == "")
            {
                // lnk.Visible = false;
            }

            //connected Petition

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            obj_petOB.PetitionId = Convert.ToInt16(gvPetition.DataKeys[e.Row.RowIndex].Value.ToString());
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
        this.Bind_Petition(1);
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        ViewState["pageIndex"] = pageIndex;
        this.Bind_Petition(pageIndex);
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
        p_Val.dSetChildData = obj_petBL.Get_Year();

        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();


        }
        else
        {
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataBind();
            drpyear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Year", "Year"));
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
            Bind_Petition(1);
        }
    }

    #endregion

    protected void lnkreview_Click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
            {
                Response.Redirect("~/PreviousReview.aspx");
            }
            else
            {
                Response.Redirect("~/CurrentReviewPetition.aspx");

            }

        }
        else
        {

            if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
            {
                Response.Redirect("~/content/Hindi/PreviousReview.aspx");

            }
            else
            {
                Response.Redirect("~/content/Hindi/CurrentReviewPetition.aspx");
            }

        }
    }





    public override void VerifyRenderingInServerForm(Control control)
    {

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
        Bind_Petition(1);
    }
}
