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

public partial class PreviousAppeal : BasePage
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

    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Judgement_Pronounced"] + "/");
        str = BreadcrumDL.DisplayMemberAreaBreadCrum(Resources.HercResource.Petitions);
        ltrlBreadcrum.Text = str.ToString();

        if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
        {
            lnkreview.Text = "View Previous Years Petitions";
            lnkAppeal.Text = " View Previous Years Review Petitions";
            // hPetition.InnerText = Resources.HercResource.PreviousPetition;

        }
        else
        {
            lnkreview.Text = "View Previous Years Petitions";
            lnkAppeal.Text = " View Previous Years Review Petitions";
            //hPetition.InnerText = Resources.HercResource.CurrentPetition;

        }

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                Bind_Appeal(1);
            }
        }
        if (ViewState["lastUpdateDate"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate"].ToString();
            Bind_Appeal(1);
        }
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            ViewState["year"] = Session["yearnew"];
            pyear.Visible = false;
            Bind_Appeal(1);
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

    #region function to bind previous year appeal

    public void Bind_Appeal(int pageIndex)
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

        p_Val.dSet = obj_petBL.Get_CurrentPetitionAppeal(obj_petOB, out p_Val.k);
        BindYear();
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {

            grdAppeal.DataSource = p_Val.dSet;
            grdAppeal.DataBind();
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
            DlastUpdate.Visible = true;

        }
        else
        {
            DlastUpdate.Visible = false;
            grdAppeal.DataSource = null;
            grdAppeal.DataBind();
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
        this.Bind_Appeal(pageIndex);
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Appeal(1);
    }

    #endregion

    protected void grdAppeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filename = DataBinder.Eval(e.Row.DataItem, "File_Name").ToString();
            LinkButton lnk = (LinkButton)e.Row.FindControl("lblViewDocAppeal");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
            if (filename == null || filename == "")
            {
                lnk.Visible = false;
            }

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

        }
    }
    protected void grdAppeal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDocAppeal")
        {
            Bind_Appeal(1);
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
            //if (Session["update"] != null && ViewState["update"] != null)
            //{

            //    if (Session["update"].ToString() == ViewState["update"].ToString())
            //    {
            p_Val.stringTypeID = e.CommandArgument.ToString();
            p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewDetails.aspx?PDID=" + p_Val.stringTypeID) + "', 'mywindow', " +
                               "'menubar=no, resizable=yes, scrollbars=yes, width=700,height=530,top=0,left=0 ')" +
                               "</script>";
            this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
         


        }


    }


    #region Datalist datalistYear itemCommand event zone

    protected void datalistYear_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            p_Val.str = e.CommandArgument.ToString();
            ViewState["year"] = p_Val.str;
            Bind_Appeal(1);
        }
    }

    #endregion


    #region function to Bind data in a data list for year

    public void BindYear()
    {
       

        p_Val.dSetChildData = obj_petBL.GetYearPetitionAppealPrevious();

        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                pyear.Visible = false;
            }
            else
            {
                pyear.Visible = true;
            }
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();
            //datalistYear.DataSource = p_Val.dSetChildData;
            //datalistYear.DataBind();
        }
        else
        {
            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                pyear.Visible = false;
            }
            else
            {
                pyear.Visible = true;
            }
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataBind();
            drpyear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Year", "Year"));
            // pyear.Visible = false;
        }

    }
    #endregion

    protected void lnkreview_Click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            //if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
            //{
            //    Response.Redirect("~/Petition/1.aspx");
            //}
            //else
            //{
            Response.Redirect("~/Petition/1.aspx");

            // }

        }
        else
        {

            //if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["PrevYear"]) == 1)
            //{
            //    Response.Redirect("~/content/Hindi/PreviousReview.aspx");

            //}
            //else
            //{
            Response.Redirect("~/content/Hindi/Petition/1.aspx");
            //}

        }
    }

    protected void lnkAppeal_Click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect("~/PreviousReview.aspx");
        }
        else
        {
            Response.Redirect("~/content/Hindi/PreviousReview.aspx");
        }
    }

    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["year"] = drpyear.SelectedValue;
        Session["yearnew"] = drpyear.SelectedValue;
        Bind_Appeal(1);
    }
    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion

    protected void imgPdf_Click(object sender, EventArgs e)
    {

        string url = HttpContext.Current.Request.Url.AbsoluteUri; //Request.Url.AbsoluteUri;
        string pdfname = "PetitionAppealPrevious";
        url = url + "?format=Print";



        //output PDF file Path
        string path = Server.MapPath("~/WriteReadData/upload/" + pdfname.Replace(" ", "") + ".pdf");
        FileInfo file = new FileInfo(path);
        if (file.Exists)
        {
            file.Delete();

        }
        string filepath = Path.Combine(Server.MapPath("~/WriteReadData/upload/"), pdfname.Replace(" ", "") + ".pdf");

        //variable to store pdf file content
        byte[] fileContent = null;

        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = false;


        //set the executable location
        process.StartInfo.FileName = Path.Combine(Server.MapPath("~/Bin/"), "wkhtmltopdf.exe");
        //set the arguments to the exectuable
        // wkhtmltopdf [OPTIONS]... <input fileContent> [More input fileContents] <output fileContent>
        process.StartInfo.Arguments = url + " " + filepath;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardInput = true;

        //run the executable
        process.Start();

        //wait until the conversion is done
        process.WaitForExit();

        // read the exit code, close process    
        int returnCode = process.ExitCode;
        process.Close();

        //initialize the filestream with filepath
        FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);

        fileContent = new byte[(int)fs.Length];



        //read the content
        fs.Read(fileContent, 0, (int)fs.Length);


        //close the stream
        fs.Close();

        if (fileContent != null)
        {
            Response.Redirect(ResolveUrl("~/WriteReadData/upload/") + pdfname + ".pdf");
            //Response.Clear();
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-length", fileContent.Length.ToString());
            //Response.BinaryWrite(fileContent);
            //Response.End();
        }



    }
}
