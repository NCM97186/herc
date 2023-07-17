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
using System.Drawing;
using System.IO;
public partial class Policies : BasePage
{

    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL = new PetitionBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    MixModuleBL mixModuleBL = new MixModuleBL();
    public string lastUpdatedDate = string.Empty;
    public string headerName = string.Empty;
    public string path = string.Empty;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    public static string UrlPrint = string.Empty;
    LinkOB lnkObject = new LinkOB();
    #endregion

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        herc.Title = Resources.HercResource.herc.ToString();
        other.Title = Resources.HercResource.Others.ToString();
        repealed.Title = Resources.HercResource.RepealedPolicies.ToString();
        herc_Hindi.Title = Resources.HercResource.herc.ToString();
        other_Hindi.Title = Resources.HercResource.Others.ToString();
        repealed_Hindi.Title = Resources.HercResource.RepealedPolicies.ToString();
		if (RewriteModule.RewriteContext.Current.Params["id"] != null)
        {
        if (RewriteModule.RewriteContext.Current.Params["id"].ToString() == (Convert.ToInt16(Module_ID_Enum.hercType.herc).ToString()))
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                herc.Attributes["class"] = "current";
                
            }
            else
            {
                herc_Hindi.Attributes["class"] = "current";
                
            }
            str = BreadcrumDL.DisplayBreadCrumPoliciesGuidelines(Resources.HercResource.policiesGuidelines, Resources.HercResource.Policies, Resources.HercResource.herc);
        }
        else if (RewriteModule.RewriteContext.Current.Params["id"].ToString() == (Convert.ToInt16(Module_ID_Enum.hercType.other).ToString()))
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                other.Attributes["class"] = "current";
               
            }
            else
            {
                other_Hindi.Attributes["class"] = "current";
              
            }
            // str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.policiesGuidelines, Resources.HercResource.Others);
        }
        else
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                repealed.Attributes["class"] = "current";
                
            }
            else
            {
                repealed_Hindi.Attributes["class"] = "current";
               
            }
            str = BreadcrumDL.DisplayBreadCrumPoliciesGuidelines(Resources.HercResource.policiesGuidelines, Resources.HercResource.Policies, Resources.HercResource.RepealedPolicies);
        }
		}
        ltrlBreadcrum.Text = str.ToString();
        path  = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["pdf"] + "/";
        p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["pdf"] + "/");
        //obj_miscel.MakeAccessible(gvRegulations);
        Bind_Data(1);
        if (RewriteModule.RewriteContext.Current.Params["id"] != null)
        {
            if (Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["id"].ToString()) == 1)
            {
                headerName = Resources.HercResource.herc;
            }
            else
            {
                headerName = Resources.HercResource.RepealedPolicies;
            }
        }

        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.policiesGuidelines;
        }
        else
        {
            PageTitle = Resources.HercResource.policiesGuidelines;
        }


        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            Bind_Data(1);
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			 ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);
            DlastUpdate.Visible = false;
        }
    }

    #endregion


   

    //Area for all the buttons, linkButtons, imageButtons events

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Bind_Data(pageIndex);
    }

    #endregion

    //End

    //Area for all the dropDownlist events


    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Data(1);
    }

    #endregion


    //End


   
    //Area for all the user defined functions

    public void Bind_Data(int pageIndex)
    {
        try{
		if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            obj_petOB.PageSize = 10000;
        }
        else
        {
            obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }
        obj_petOB.PageIndex = pageIndex;
      //  obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        obj_petOB.ActionType = Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["id"]);

        p_Val.dSet = mixModuleBL.Get_Polices(obj_petOB, out p_Val.k);


        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            gvRegulations.DataSource = p_Val.dSet;
            gvRegulations.DataBind();
            lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
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
            DlastUpdate.Visible = false;
            rptPager.Visible    = false;
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
	catch{}

    }

    //End


    protected void gvRegulations_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("LinkButton1");
            Label lblRegNo = (Label)e.Row.FindControl("lblRegNumber");
            HiddenField hydfield = (HiddenField)e.Row.FindControl("hydRegNumber");

            Label lbllinkid = (Label)e.Row.FindControl("lbllinkId");
            Label lblambandment = (Label)e.Row.FindControl("lblambandment");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            Label lblname = (Label)e.Row.FindControl("lblname");
            HyperLink hyp = (HyperLink)e.Row.FindControl("hypFile");
            HiddenField hid = (HiddenField)e.Row.FindControl("hidFile");
            string filename = DataBinder.Eval(e.Row.DataItem, "File_Name").ToString();
            //lnkObject.linkID = Convert.ToInt16(lbllinkid.Text);
            Label lblambandmentId = (Label)e.Row.FindControl("lblAmendmentid");

            


            if (Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["id"]) == Convert.ToInt16(Module_ID_Enum.hercType.repealed))
            {
                gvRegulations.Columns[3].Visible = true;
                if (lblambandment.Text.ToString() != "null" && lblambandment.Text.ToString() != "")
                {
                    lblambandmentId.Visible = true;
                    lblRemarks.Visible = false;
                }
                else
                {
                    lblRemarks.Visible = true;
                    lblambandmentId.Visible = false;
                }

                lblRemarks.ForeColor = Color.Red;
            }
            else
            {
                gvRegulations.Columns[3].Visible = false;

            }

            if (hid.Value.ToString() == "" || hid.Value == null)
            {
                lblname.Visible = true;
                hyp.Visible = false;
            }
            else
            {
                if (File.Exists(Server.MapPath(path + hid.Value)))
                {
                    lblname.Visible = false;
                    hyp.Visible = true;
                    FileInfo finfo = new FileInfo(Server.MapPath(path + hid.Value));
                    double FileInBytes = finfo.Length;



                    p_Val.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");
                    p_Val.sbuilder.Append("(" + obj_miscel.fileSize(FileInBytes) + ")");
                    hyp.Text = hyp.Text.ToString().Replace("<p>", "").Replace("</p>", "");
                    hyp.Text += p_Val.sbuilder.ToString();
                    p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
                }

               // hyp.Text = hyp.Text.ToString().Replace("<p>", "").Replace("</p>", "");
            }
            if (lblambandment.Text.ToString() != "null" && lblambandment.Text.ToString() != "")
            {
                lblambandmentId.Visible = true;
                lnkObject.regulationNumber = Convert.ToInt16(lblRegNo.Text);
                lnkObject.TempLinkId = Convert.ToInt16(lbllinkid.Text);
                lnkObject.ModuleId = 25;
                p_Val.dsFileName = mixModuleBL.USP_GetAmendmentID(lnkObject);
                if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
                {

                    lblambandmentId.Text = p_Val.dsFileName.Tables[0].Rows[0]["Remarks"].ToString();
                    lblambandmentId.ForeColor = Color.Red;
                    lblRemarks.Visible = false;
                }

            }

           
            if (hydfield.Value != null && hydfield.Value != "")
            {
                lblambandment.Visible = true;
                if (lblRegNo.Text == hydfield.Value)
                {
                    lblRegNo.Text = "";

                }
            }
         

            if (filename == null || filename == "")
            {
                lnk.Visible = false;
            }
            else
            {
                if (File.Exists(Server.MapPath(path + hid.Value)))
                {
                    lblname.Visible = false;
                }
                else
                {
                    lblname.Visible = true;
                    hyp.Visible = false;
                }
            }
        }
    }


    protected void gvRegulations_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "view")
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
        string pdfname = Resources.HercResource.Policies;
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
            Response.Redirect(ResolveUrl("~/WriteReadData/upload/") + pdfname.Replace(" ", "") + ".pdf");
           
        }




    }
}