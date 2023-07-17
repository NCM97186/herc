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

public partial class CurrentReviewPetition : BasePage
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL = new PetitionBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    public string lastUpdatedDate = string.Empty;
    public static string UrlPrint = string.Empty;

    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        str = BreadcrumDL.DisplayMemberAreaBreadCrum(Resources.HercResource.Petitions);
        ltrlBreadcrum.Text = str.ToString();
        p_Val.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/";
        bool IsPageRefresh = false;

        if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
        {
            lnkreview.Text = "View Previous Years Petitions";
        }
        else
        {
            lnkreview.Text = "View Current Year Petitions";
        }
        if (!IsPostBack)
        {
            Session.Remove("yearnew");
            ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
            Session["SessionId"] = ViewState["ViewStateId"].ToString();
            if (string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                Bind_Petition(1);
            }
           
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

        }
        else
        {
            if (ViewState["ViewStateId"] != null && Session["SessionId"]!= null)
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
            if (ViewState["pageIndex"] != null)
            {
                Bind_Petition(Convert.ToInt16(ViewState["pageIndex"]));
            }
            else
            {
                Bind_Petition(1);
            }
        }
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            Bind_Petition(1);
            lnkreview.Visible = false;
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			 ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);

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



    #region function to bind review petition in gridview

    public void Bind_Petition(int pageIndex)
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
        obj_petOB.year = System.DateTime.Now.Year.ToString();

        p_Val.dSet = obj_petBL.Get_CurrentPetitionReview(obj_petOB, out p_Val.k);

        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {

            gvReview.DataSource = p_Val.dSet;
            gvReview.DataBind();
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
            DlastUpdate.Visible = true;
            if (Convert.ToInt16(p_Val.dSet.Tables[0].Rows[0]["RP_Status_Id"]) == 12)
            {
                p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Interim_Order"] + "/");
            }
            else if (Convert.ToInt16(p_Val.dSet.Tables[0].Rows[0]["RP_Status_Id"]) == 13)
            {
                p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Order_Pronounced"] + "/");
            }
        }
        else
        {
            DlastUpdate.Visible = false;
            gvReview.DataSource = null;
            gvReview.DataBind();
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
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
        this.Bind_Petition(pageIndex);
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Petition(1);
    }

    #endregion

    protected void gvReview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filename = DataBinder.Eval(e.Row.DataItem, "File_Name").ToString();
            LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");

            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
            Label lblPetitioner = (Label)e.Row.FindControl("lblPetitioner");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject1");
           

            if (filename == null || filename == "")
            {
                //lnk.Visible = false;
            }



            //connected Petition

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            obj_petOB.RPId = Convert.ToInt16(gvReview.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Val.dsFileName = obj_petBL.getReviewPetitionFileNames(obj_petOB);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    if (p_Val.dsFileName.Tables[0].Rows[i]["Comment"] != DBNull.Value && p_Val.dsFileName.Tables[0].Rows[i]["Comment"].ToString() != "")
                    {
                        p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["Comment"] + ",  ");
                    }
                    p_Val.sbuilder.Append("  <a href='" + p_Val.url + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' target='_blank'>" + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");


                    p_Val.sbuilder.Append("<br/><hr/>");
                }
                orderConnectedFile.Text = p_Val.sbuilder.ToString();
                p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            }
            else
            {
            }
            //End
        }
    }
    protected void gvReview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDoc")
        {
            Bind_Petition(1);
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
                    p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewDetails.aspx?RPID=" + p_Val.stringTypeID) + "', 'blank' + new Date().getTime()," +
                                       "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                       "</script>";
                    this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
              

        }

    }



    protected void lnkreview_Click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
           
            Response.Redirect("~/Petition.aspx");

           
        }
        else
        {

           
            Response.Redirect("~/content/Hindi/Petition.aspx");
            
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



}
