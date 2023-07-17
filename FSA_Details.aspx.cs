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

public partial class FSA_Details : BasePage
{
    #region variable declaration

    TariffOB obj = new TariffOB();
    TariffBL obj_tariffBL = new TariffBL();
    Project_Variables P_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    string str;
    public string headname = string.Empty;
    public string lastUpdatedDate = string.Empty;
    public static string UrlPrint = string.Empty;
    #endregion

    #region page load zone
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ViewState["lastUpdateDate"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate"].ToString();
        }

        P_Val.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/"; 
        if (RewriteModule.RewriteContext.Current.Params["menuid"] == null || Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]) == null)
        {

        }
        else
        {
            P_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
            P_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        }
       

        
        if (!IsPostBack)
        {

            Bind_TariffDetails();
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

        if (Resources.HercResource.Lang_Id == "1")
        {
            if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["menuid"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA))
            {
                PageTitle = Resources.HercResource.FSA;
                headname = PageTitle;
                str = BreadcrumDL.DisplayBreadCrumTariff(Resources.HercResource.ConsumerEmpowerment, Resources.HercResource.FSA, null);
                ltrlBreadcrum.Text = str.ToString();
            }
            else
            {
                PageTitle = Resources.HercResource.GeneralMiscCharges;
                headname = PageTitle;
                str = BreadcrumDL.DisplayBreadCrumTariff(Resources.HercResource.ConsumerEmpowerment, Resources.HercResource.GeneralMiscCharges, null);
                ltrlBreadcrum.Text = str.ToString();
            }
        }
        else
        {
            if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["menuid"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA_Hindi))
            {
                PageTitle = Resources.HercResource.FSA;
                headname = PageTitle;
                str = BreadcrumDL.DisplayBreadCrumTariff(Resources.HercResource.ConsumerEmpowerment, Resources.HercResource.FSA, null);
                ltrlBreadcrum.Text = str.ToString();
            }
            else
            {
                PageTitle = Resources.HercResource.GeneralMiscCharges;
                headname = PageTitle;
                str = BreadcrumDL.DisplayBreadCrumTariff(Resources.HercResource.ConsumerEmpowerment, Resources.HercResource.GeneralMiscCharges, null);
                ltrlBreadcrum.Text = str.ToString();
            }
        }

    }
    #endregion

    #region function to bind tariff details

    public void Bind_TariffDetails()
    {

        if (ViewState["year"] != null)
        {
            obj.Year = ViewState["year"].ToString();
        }
        else
        {
            obj.Year = null;
        }
        obj.CatId = null;
        obj.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Tariff);
        obj.linkID = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["id"]);

        P_Val.dSet = obj_tariffBL.GetdetailsData(obj);

        
        if (P_Val.dSet.Tables[0].Rows.Count > 0)
        {
			lastUpdatedDate = P_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            ViewState["lastUpdateDate"] = lastUpdatedDate;
            if (RewriteModule.RewriteContext.Current.Params["menuid"] != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["menuid"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA) || Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["menuid"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA_Hindi))
            {
                lrtTariff.Text = HttpUtility.HtmlDecode(P_Val.dSet.Tables[0].Rows[0]["details"].ToString());
                lblyear.Text = "<strong>" + Resources.HercResource.FSA.ToString() + "</strong>" + ":- " + P_Val.dSet.Tables[0].Rows[0]["year"];
                if (P_Val.dSet.Tables[0].Rows[0]["file_name"] != DBNull.Value && P_Val.dSet.Tables[0].Rows[0]["file_name"].ToString() != "")
                {
                    ltrlPdf.Visible = true;
                    ltrlPdf.Text += " , " + "<a title='" + Resources.HercResource.ViewDocument + "' href='" + P_Val.url + P_Val.dSet.Tables[0].Rows[0]["file_name"] + "' Text='" + P_Val.dSet.Tables[0].Rows[0]["file_name"] + "' target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='" + Resources.HercResource.ViewDocument + "' width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> " ;
                    if (File.Exists(Server.MapPath(P_Val.url) + P_Val.dSet.Tables[0].Rows[0]["file_name"]))
                    {
                        FileInfo finfo = new FileInfo(Server.MapPath(P_Val.url) + P_Val.dSet.Tables[0].Rows[0]["file_name"]);
                        double FileInBytes = finfo.Length;
                        P_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                    }
                    P_Val.sbuilder.Append("</a>");
                    ltrlPdf.Text += P_Val.sbuilder.ToString();
                }
                else
                {
                    ltrlPdf.Visible = false;
                }
            }
            else if (RewriteModule.RewriteContext.Current.Params["menuid"] != null && Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["menuid"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.GeneralMiscCharges) || Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["menuid"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.GeneralMiscCharges_Hindi))
            {
                lrtTariff.Text = HttpUtility.HtmlDecode(P_Val.dSet.Tables[0].Rows[0]["details"].ToString());
                lblyear.Text = "<strong>" + Resources.HercResource.GeneralMiscCharges.ToString() + "</strong>" + ":- " + P_Val.dSet.Tables[0].Rows[0]["year"];
                if (P_Val.dSet.Tables[0].Rows[0]["file_name"] != DBNull.Value && P_Val.dSet.Tables[0].Rows[0]["file_name"].ToString() != "")
                {
                    ltrlPdf.Visible = true;
                    ltrlPdf.Text += " , " + "<a title='" + Resources.HercResource.ViewDocument + "' href='" + P_Val.url + P_Val.dSet.Tables[0].Rows[0]["file_name"] + "' Text='" + P_Val.dSet.Tables[0].Rows[0]["file_name"] + "' target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='" + Resources.HercResource.ViewDocument + "' width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> " ;
                    if (File.Exists(Server.MapPath(P_Val.url) + P_Val.dSet.Tables[0].Rows[0]["file_name"]))
                    {
                        FileInfo finfo = new FileInfo(Server.MapPath(P_Val.url) + P_Val.dSet.Tables[0].Rows[0]["file_name"]);
                        double FileInBytes = finfo.Length;
                        P_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                    }
                    P_Val.sbuilder.Append("</a>");
                    ltrlPdf.Text += P_Val.sbuilder.ToString();
                }
                else
                {
                    ltrlPdf.Visible = false;
                }

            }
            else
            {
                lrtTariff.Text = HttpUtility.HtmlDecode(P_Val.dSet.Tables[0].Rows[0]["details"].ToString());
                lblyear.Text = "<strong>" + P_Val.dSet.Tables[0].Rows[0]["catName"] + "</strong>" + ":- " + P_Val.dSet.Tables[0].Rows[0]["year"];
                if (P_Val.dSet.Tables[0].Rows[0]["file_name"] != DBNull.Value && P_Val.dSet.Tables[0].Rows[0]["file_name"].ToString() != "")
                {
                    ltrlPdf.Visible = true;
                    ltrlPdf.Text += " , " + "<a title='" + Resources.HercResource.ViewDocument + "' href='" + P_Val.url + P_Val.dSet.Tables[0].Rows[0]["file_name"] + "' Text='" + P_Val.dSet.Tables[0].Rows[0]["file_name"] + "' target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='" + Resources.HercResource.ViewDocument + "' width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> " + "</a>";
                }
                else
                {
                    ltrlPdf.Visible = false;
                }

            }


        }
        else
        {

        }



    }

    #endregion

    #region link button click event zone

    public void Lnkback_Click(object sender, EventArgs e)
    {
   try{
        if (Convert.ToInt16(P_Val.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA)|| Convert.ToInt16(P_Val.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA_Hindi))
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Response.Redirect("~/FSA/" + P_Val.PageID + "_" + P_Val.position + "_FSA.aspx");
            }
            else
            {
                Response.Redirect("~/Content/Hindi/FSA/" + P_Val.PageID + "_" + P_Val.position + "_FSA.aspx");
            }
        }
        else
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Response.Redirect("~/GeneralMiscCharges/" + P_Val.PageID + "_" + P_Val.position + "_GeneralCharges.aspx");
            }
            else
            {
                Response.Redirect("~/Content/Hindi/GeneralMiscCharges/" + P_Val.PageID + "_" + P_Val.position + "_GeneralCharges.aspx");
            }
        }
	}
	catch{}
    }

    #endregion 

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
       // PageDescription = MetaDescription;
       // PageKeywords = MetaKeyword;
    }

    #endregion


}
