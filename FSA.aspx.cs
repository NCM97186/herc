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

public partial class FSA : BasePage
{
    #region variable declaration

    TariffOB obj = new TariffOB();
    TariffBL obj_tariffBL = new TariffBL();
    Project_Variables P_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    public string lastUpdatedDate = string.Empty;
    string str;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;
   
    public static string UrlPrint = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        P_Val.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/"; 
        str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.ConsumerEmpowerment, Resources.HercResource.FSA);
        ltrlBreadcrum.Text = str.ToString();
        if (RewriteModule.RewriteContext.Current.Params["menuid"] != null)
        {
            P_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        }
        P_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]); 
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                BindYear();
                Bind_Tariff(1);
            }
        }
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            ViewState["year"] = Session["yearnew"];
            pyear.Visible = false;
            Bind_Tariff(1);
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			 ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);

        }
        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.FSA;
        }
        else
        {
            PageTitle = Resources.HercResource.FSA;
        }
    }


    public void Bind_Tariff(int pageIndex)
    {
        obj.PageIndex = pageIndex;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            obj.PageSize = 10000;
        }
        else
        {
            obj.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }
        obj.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        P_Val.sbuilder.Remove(0, P_Val.sbuilder.Length);
        if (ViewState["year"] != null)
        {
            obj.Year = ViewState["year"].ToString();
            drpyear.SelectedValue = ViewState["year"].ToString();
        }
        else
        {
            obj.Year = null;
        }
        obj.CatId = Convert.ToInt16(Module_ID_Enum.Tariff_subcategory.FSA);

        P_Val.dSet = obj_tariffBL.GetdetailsFSA(obj,out P_Val.k);

        if (P_Val.dSet.Tables[0].Rows.Count > 0)
        {
            lblmsg.Visible = false;

            for (int i = 0; i < P_Val.dSet.Tables[0].Rows.Count; i++)
            {

                P_Val.sbuilder.Append("<ul>");
                P_Val.sbuilder.Append("<li>");
                 if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                {
                    P_Val.sbuilder.Append("<a title='" + Resources.HercResource.ClickHereToViewDetails + "' href='" + ResolveUrl("~/FsaDetails/" + P_Val.dSet.Tables[0].Rows[i]["link_id"].ToString() + "_" + P_Val.PageID + "_" + P_Val.position + ".aspx") + "' >" + P_Val.dSet.Tables[0].Rows[i]["year"] + "</a>");
                }
                 else
                 {
                     P_Val.sbuilder.Append("<a title='" + Resources.HercResource.ClickHereToViewDetails + "' href='" + ResolveUrl("~/Content/Hindi/FsaDetails/" + P_Val.dSet.Tables[0].Rows[i]["link_id"].ToString() + "_" + P_Val.PageID + "_" + P_Val.position + ".aspx") + "'>" + P_Val.dSet.Tables[0].Rows[i]["year"] + "</a>");
                 }
                if (P_Val.dSet.Tables[0].Rows[i]["file_name"] != DBNull.Value && P_Val.dSet.Tables[0].Rows[i]["file_name"] != "")
                {

                    P_Val.sbuilder.Append(" , " + "<a title='" + Resources.HercResource.ViewDocument + "' href='" + P_Val.url + P_Val.dSet.Tables[0].Rows[i]["file_name"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> ");
                    if (File.Exists(Server.MapPath(P_Val.url) + P_Val.dSet.Tables[0].Rows[i]["file_name"]))
                    {
                        FileInfo finfo = new FileInfo(Server.MapPath(P_Val.url) + P_Val.dSet.Tables[0].Rows[i]["file_name"]);
                        double FileInBytes = finfo.Length;
                        P_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                    }
                    P_Val.sbuilder.Append("</a>");
                }
                P_Val.sbuilder.Append("</li>");
                P_Val.sbuilder.Append("</ul>");
            }
            lrtTariff.Text = P_Val.sbuilder.ToString();


            MetaKeyword = P_Val.dSet.Tables[0].Rows[0]["Meta_Keywords"].ToString();
            MetaDescription = P_Val.dSet.Tables[0].Rows[0]["Mate_Description"].ToString();
            MetaLng = P_Val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = P_Val.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();

            lastUpdatedDate = P_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            P_Val.sbuilder.Remove(0, P_Val.sbuilder.Length);
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
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
            pyear.Visible = false;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            rptPager.Visible = false;
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

    #region function to Bind data in a data list for year

    public void BindYear()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            pyear.Visible = false;
        }
        else
        {
            pyear.Visible = true;
        }
        ddlPageSize.Visible = false;
        obj.CatId = Convert.ToInt16(Module_ID_Enum.Tariff_subcategory.FSA);

        P_Val.dSetChildData = obj_tariffBL.getYear(obj);

        if (P_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            //datalistYear.DataSource = P_Val.dSetChildData;
            //datalistYear.DataBind();
            drpyear.DataSource = P_Val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();
        }
        else
        {
            drpyear.DataSource = P_Val.dSetChildData;
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
            P_Val.str = e.CommandArgument.ToString();
            ViewState["year"] = P_Val.str;
            Bind_Tariff(1);
        }
    }

    #endregion


    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Bind_Tariff(pageIndex);
    }

    #endregion

    //End

    //Area for all the dropDownlist events


    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Tariff(1);
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

    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["year"] = drpyear.SelectedValue;
        Session["yearnew"] = drpyear.SelectedValue;
        Bind_Tariff(1);
    }

    protected void imgPdf_Click(object sender, EventArgs e)
    {

        string url = HttpContext.Current.Request.Url.AbsoluteUri; //Request.Url.AbsoluteUri;
        string pdfname = "FSA";
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
