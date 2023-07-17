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

public partial class PublicNoticeReviewPetitionDetails : BasePage
{
    #region variable declaration zone

    LinkOB objlinkOB = new LinkOB();
    PublicNoticeBL pubNoticeBL = new PublicNoticeBL();
    PetitionOB publicNoticeObject = new PetitionOB();
    Project_Variables p_val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    string str = string.Empty;
    public static string UrlPrint = string.Empty;
    public string lastUpdatedDate = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        p_val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["pdf"] + "/");

        str = BreadcrumDL.DisplayMemberAreaBreadCrumpubNoticeold(Resources.HercResource.CurrentYear);
        ltrlBreadcrum.Text = str.ToString();
        if (!IsPostBack)
        {
            bindPublicNoticDetails(1);
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			 ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);

        }

    }

    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }



    public void bindPublicNoticDetails(int pageIndex)
    {
        objlinkOB.PageIndex = pageIndex;
        objlinkOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);


        p_val.dSet = pubNoticeBL.Get_publiceNotceDetailsforReviewPetition(objlinkOB, out p_val.k);
        if (p_val.dSet.Tables[0].Rows.Count > 0)
        {
            gvPubNotice.DataSource = p_val.dSet;
            gvPubNotice.DataBind();
            lastUpdatedDate = p_val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        p_val.Result = p_val.k;
        if (p_val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_val.Result, pageIndex, ddlPageSize, rptPager);
            rptPager.Visible = true;
        }
        else
        {
            rptPager.Visible = false;
        }
    }

    protected void gvPubNotice_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "ViewDoc")
        {

            string file = e.CommandArgument.ToString();
            p_val.Path = p_val.Path + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(p_val.Path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(p_val.Path));
                Response.Clear();
                Response.WriteFile(p_val.Path);
                Response.End();

            }
        }

        if (e.CommandName == "ViewDetails")
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                p_val.stringTypeID = e.CommandArgument.ToString();
                p_val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "DetailsPage.aspx?PulicNoticeId=" + p_val.stringTypeID) + "', 'mywindow', " +
                                   "'menubar=no, resizable=no, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", p_val.strPopupID);
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }

        }
    }


    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.bindPublicNoticDetails(1);
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.bindPublicNoticDetails(pageIndex);
    }

    #endregion



    protected void gvPubNotice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            p_val.sbuilder.Remove(0, p_val.sbuilder.Length);
            LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            Label lblDesc = (Label)e.Row.FindControl("lblDesc");
            LinkButton lnkTitle = (LinkButton)e.Row.FindControl("lnkTitle");
            Label lblTitle = (Label)e.Row.FindControl("lblTitle");
            string filename = DataBinder.Eval(e.Row.DataItem, "PublicNotice").ToString();

            //if (filename == null || filename == "")
            //{
            //    lnk.Visible = false;
            //}

            if (lblDesc.Text == null || lblDesc.Text == "")
            {
                lblTitle.Visible = true;
                lnkTitle.Visible = false;
            }
            else
            {
                lblTitle.Visible = false;
                lnkTitle.Visible = true;
            }



            //connected Public notices

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            publicNoticeObject.PublicNoticeID = Convert.ToInt16(gvPubNotice.DataKeys[e.Row.RowIndex].Value.ToString());
            p_val.dsFileName = pubNoticeBL.getPublicNoticeFileNames(publicNoticeObject);
            if (p_val.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    if (p_val.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value)
                    {
                        p_val.sbuilder.Append(p_val.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                    }
                    p_val.sbuilder.Append("Dated: " + p_val.dsFileName.Tables[0].Rows[i]["Date"] + " ");

                    p_val.sbuilder.Append("<a href='" + p_val.url + p_val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_val.dsFileName.Tables[0].Rows[i]["FileName"] + "' target='_blank'>" + p_val.dsFileName.Tables[0].Rows[i]["FileName"] + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");

                    p_val.sbuilder.Append("<br/><hr/>");

                }
                orderConnectedFile.Text = p_val.sbuilder.ToString();

            }

            //End



        }
    }
    protected void lnkreview_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PublicNoticeDetails.aspx");
    }
}
