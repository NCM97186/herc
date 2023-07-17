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

public partial class Ombudsman_currentyearawardpronounce :BasePageOmbudsman
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
	Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
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
        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.AwardsPronounced, Resources.HercResource.Currentyearawards);
        ltrlBreadcrum.Text = str.ToString();
        p_Val.Path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Pdf"] + "/";
        obj_miscel.MakeAccessible(Grdaaward);
        if (ViewState["lastUpdateDate"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate"].ToString();
			if (ViewState["pageIndex"] != null)
            {
                Bind_Award_Grid(Convert.ToInt16(ViewState["pageIndex"]));
            }
            else
            {
                Bind_Award_Grid(1);
            }
        }
        if (!Page.IsPostBack)
        {
            Bind_Award_Grid(1);
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
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



    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    public void Bind_Award_Grid(int pageindex)
    {
        try
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

            p_Val.dSet = obj_petBL.Get_Award_pronounced_CurrentYear(obj_petOB, out p_Val.k);

            if (p_Val.dSet.Tables[0].Rows.Count > 0)
            {

                Grdaaward.DataSource = p_Val.dSet;
                Grdaaward.DataBind();
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
                    lblmsg.Visible = false;
                }
            }
            else
            {
                Grdaaward.DataSource = p_Val.dSet;
                Grdaaward.DataBind();
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
        catch { }
       
    }




    protected void Grdaaward_RowCommand(object sender, GridViewCommandEventArgs e)
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
									   "window.open('ViewDetails.aspx?Petition_id=" + p_Val.stringTypeID + "', 'blank' + new Date().getTime()," +
									   "' menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
									   "</script>";
					this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
					

        }
        if (e.CommandName == "getappeal_Detail")
        {
               p_Val.id = e.CommandArgument.ToString();

                 p_Val.strPopupID = "<script language='javascript'>" +
                                   "window.open('" + ResolveUrl("~/") + "Ombudsman/ViewAppealDetails.aspx?Appeal_id=" + p_Val.id + "', 'blank' + new Date().getTime()," +
                                   "' menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
             

                
                this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
           
        }
    }

    protected void Grdaaward_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            

            //LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
         
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            Label lblPetitioner = (Label)e.Row.FindControl("lblPetitioner");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            LinkButton lblSubject = (LinkButton)e.Row.FindControl("lblSubject");
            lblPetitioner.Text = HttpUtility.HtmlDecode(lblPetitioner.Text);
            lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
            lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
     

            //connected Award files
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            Literal orderAwardFile = (Literal)e.Row.FindControl("ltrlConnectedAwardProunced");
            obj_petOB.AppealId = Convert.ToInt16(hdf.Value);
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            p_Val.dsFileName = obj_petBL.getConnectedAwardFiles(obj_petOB);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Val.sbuilder.Append(" <a title='" + Resources.HercResource.ViewDocument + "' href='" + p_Val.Path + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "Award,Dated:" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> ");
                   // p_Val.sbuilder.Append(" <a title='" + Resources.HercResource.ViewDocument + "' href='" + p_Val.Path + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "' target='_blank'>" + "Award,Dated:" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> ");

                    if (p_Val.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Val.Path) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(p_Val.Path) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }
                    p_Val.sbuilder.Append("</a><br/><hr/>");

                }
                orderAwardFile.Text = p_Val.sbuilder.ToString();

            }
            else
            {

            }
        }
    }




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
    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
		ViewState["pageIndex"] = pageIndex;
        this.Bind_Award_Grid(pageIndex);
    }

    #endregion
    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Award_Grid(1);
    }

    #endregion

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
