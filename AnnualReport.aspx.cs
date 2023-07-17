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

public partial class AnnualReport : BasePage
{
    #region variable declaration zone

    string str = string.Empty;
    LinkOB objlnkOB = new LinkOB();
    LinkBL objlnkBL = new LinkBL();
    Project_Variables P_Val = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    PaginationBL pagingBL = new PaginationBL();
    public static string UrlPrint = string.Empty;
    public string lastUpdatedDate = string.Empty;
	string PageTitle = string.Empty;
    public string headerName = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;
	string MetaKeywords=string.Empty;
	string MetaDescription=string.Empty;

    #endregion 

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        if (RewriteModule.RewriteContext.Current.Params["prev"] != null)
        {
            headerName = Resources.HercResource.OldAnnualReport.ToString();
        }
        else
        {
            headerName = Resources.HercResource.CurrentAnnualReport.ToString();
        }

        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {
            if (RewriteModule.RewriteContext.Current.Params["prev"] != null && RewriteModule.RewriteContext.Current.Params["prev"] != "" && RewriteModule.RewriteContext.Current.Params["prev"].ToString() == "1")
            {
                previous.Attributes["class"] = "current";
                str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.AnnualReports, Resources.HercResource.OldAnnualReport);
            }
            else
            {
                current.Attributes["class"] = "current";
                str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.AnnualReports, Resources.HercResource.CurrentAnnualReport);
            }
        }
        else
        {
            if (RewriteModule.RewriteContext.Current.Params["prev"] != null && RewriteModule.RewriteContext.Current.Params["prev"] != "" && RewriteModule.RewriteContext.Current.Params["prev"].ToString() == "1")
            {
                previousHindi.Attributes["class"] = "current";
                str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.AnnualReports, Resources.HercResource.OldAnnualReport);
            }
            else
            {
                currentHindi.Attributes["class"] = "current";
                str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.AnnualReports, Resources.HercResource.CurrentAnnualReport);
            }
        }

        
        ltrlBreadcrum.Text = str.ToString();
        P_Val.Path =ResolveUrl("~/")+ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/";
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            ViewState["year"] = Session["yearnew"];
            //pyear.Visible = false;
            Bind_AnnualReport(1);
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);

        }
        else if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                Bind_AnnualReport(1);
            }
        }



		if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.AnnualReports;
        }
        else
        {
            PageTitle = Resources.HercResource.AnnualReports;
        }
    }

    #endregion 

    #region function to bind annual report

    public void Bind_AnnualReport(int pageIndex)
    {
        objlnkOB.PageIndex = pageIndex;
        objlnkOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        objlnkOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        if (ViewState["year"] != null)
        {
            objlnkOB.Year = ViewState["year"].ToString();
        }
        else
        {
            objlnkOB.Year = null;
        }
        if (RewriteModule.RewriteContext.Current.Params["prev"] != null && ViewState["year"] == null)
        {
            BindYear();
            P_Val.dSet = objlnkBL.Get_Annual_PrevYear(objlnkOB, out P_Val.k);
           
          
        }
        else
        {
            P_Val.dSet = objlnkBL.Get_AnnualReports(objlnkOB, out P_Val.k);
        }
        if (P_Val.dSet.Tables[0].Rows.Count > 0)
        {

        
      
            lblmsg.Visible = false;
            gvAnnualReport.Visible = true;
            lastUpdatedDate = P_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();

            MetaKeywords = P_Val.dSet.Tables[0].Rows[0]["Meta_Keywords"].ToString();
            MetaDescription = P_Val.dSet.Tables[0].Rows[0]["Mate_Description"].ToString();
            MetaLng = P_Val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = P_Val.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
            gvAnnualReport.DataSource = P_Val.dSet;
            gvAnnualReport.DataBind();
            
            rptPager.Visible    = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
        }
        else
        {
            gvAnnualReport.Visible = false;
            lblmsg.Visible = true;
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
            rptPager.Visible    = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }

        P_Val.Result = P_Val.k;
        if (P_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(P_Val.Result, pageIndex, ddlPageSize, rptPager);
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

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_AnnualReport(1);
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Bind_AnnualReport(pageIndex);
    }

    #endregion
     


    protected void gvAnnualReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "ViewDoc")
        {

            string file = e.CommandArgument.ToString();
            P_Val.Path = P_Val.Path + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(P_Val.Path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(P_Val.Path));
                Response.Clear();
                Response.WriteFile(P_Val.Path);
                Response.End();
            }
        }
       
    }

    protected void gvAnnualReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            string filename = DataBinder.Eval(e.Row.DataItem, "File_Name").ToString();
            Literal ltrlConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");

            Label lblModule = (Label)e.Row.FindControl("lblModule");
            Label lblLinkId = (Label)e.Row.FindControl("lblId");
            objlnkOB.TempLinkId = Convert.ToInt16(lblLinkId.Text);
            objlnkOB.ModuleId =Convert.ToInt16(lblModule.Text);
            P_Val.dsFileName = objlnkBL.getFileName(objlnkOB);
            if (P_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                P_Val.sbuilder.Remove(0, P_Val.sbuilder.Length);
                for (int i = 0; i < P_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    P_Val.sbuilder.Append("<a href='" + P_Val.Path + P_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");
                    if (P_Val.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(P_Val.Path) + P_Val.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(P_Val.Path) + P_Val.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            P_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }

                    P_Val.sbuilder.Append("</a>");

                }
            }
            ltrlConnectedFile.Text = P_Val.sbuilder.ToString();
            P_Val.sbuilder.Remove(0, P_Val.sbuilder.Length);
            
           
        }
    }


    #region function to Bind data in a data list for year

    public void BindYear()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            if (RewriteModule.RewriteContext.Current.Params["prev"] != null)
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
        P_Val.dSetChildData = objlnkBL.Get_YearLink();

        if (P_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = P_Val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "TotalYear";
            drpyear.DataBind();
        }
        else
        {
            drpyear.DataSource = P_Val.dSetChildData;
           
            drpyear.DataBind();
            drpyear.Items.Insert(0,"select");
        }

    }


    #endregion 

    #region Datalist datalistYear itemCommand event zone

    protected void datalistYear_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            P_Val.str = e.CommandArgument.ToString();
            ViewState["year"] = P_Val.str;
            Bind_AnnualReport(1);
        }
    }

    #endregion

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {
        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeywords;
        MetaLang = MetaLng;
        MetaTitle = MetaTitles;
    }

    #endregion



    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["year"] = drpyear.SelectedValue.ToString();
        Session["yearnew"] = drpyear.SelectedValue;
        Bind_AnnualReport(1);
    }


}
