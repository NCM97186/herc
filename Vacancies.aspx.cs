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

public partial class Vacancies : BasePage
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

    protected void Page_Load(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(RewriteModule.RewriteContext.Current.Params["PrevYear"]))
        {
            str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Vacancies, Resources.HercResource.CurrentVacancies);
        }
        else
        {
            str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Vacancies, Resources.HercResource.OldVacancies);
        }

        ltrlBreadcrum.Text = str.ToString();

        p_Val.Path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/"; 
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			 ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);

        }
        if (!IsPostBack)
        {
            Get_Vacancies(1);
        }

        if (ViewState["lastUpdateDate"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate"].ToString();
            //Get_Vacancies(1);
        }


        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {
            if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
            {

                previous.Attributes["class"] = "current";
                headerName = Resources.HercResource.OldVacancies;
            }

            else
            {
                current.Attributes["class"] = "current";
                headerName = Resources.HercResource.CurrentVacancies;
            }
        }

        else
        {
            if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
            {

                 previousHindi.Attributes["class"] = "current";
                 headerName = Resources.HercResource.OldVacancies;
            }

            else
            {
                currentHindi.Attributes["class"] = "current";
                headerName = Resources.HercResource.CurrentVacancies;
            }
        }

       
            PageTitle = Resources.HercResource.Vacancies;
       
    }

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Get_Vacancies(1);
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Get_Vacancies(pageIndex);
    }

    #endregion

    #region function to get Vacancies

    public void Get_Vacancies(int pageIndex)
    {
	try{
        objlnkOB.PageIndex = pageIndex;
        objlnkOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        objlnkOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        if (ViewState["year"] != null)
        {
            objlnkOB.Year = ViewState["year"].ToString();
        }
        else
        {

            //objlnkOB.Year = Convert.ToString(Convert.ToInt32(System.DateTime.Now.Year) - 1);
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
        p_Val.dSet = objlnkBL.GetVacancy(objlnkOB, out p_Val.k);

        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            gvDDPapaers.DataSource = p_Val.dSet;
            gvDDPapaers.DataBind();
            lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            ViewState["lastUpdateDate"] = lastUpdatedDate;
            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
            DlastUpdate.Visible = true;
			lblmsg.Visible = false;
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
            DlastUpdate.Visible = false;
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        p_Val.Result = p_Val.k;
        if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_Val.Result, pageIndex, ddlPageSize, rptPager);
            rptPager.Visible = true;
        }
        else
        {
            rptPager.Visible = false;
        }
 	}
        catch { }
    }

    #endregion 

    #region function to Bind data in a data list for year

    public void BindYear()
    {
        pyear.Visible = true;
        p_Val.dSetChildData = objlnkBL.Get_YearVacancy();

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
        Get_Vacancies(1);
    }

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
                               "window.open('" + ResolveUrl("~/" + "ViewDetails.aspx?Link_Id=" + p_Val.stringTypeID) + "&" + "ModuleId= " + Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy) + "', 'blank' + new Date().getTime(), " +
                               "' menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
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

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion

}
