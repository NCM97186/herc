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

public partial class TariffDetailsaspx : BasePage
{

    #region variable declaration

    TariffOB obj = new TariffOB();
    TariffBL obj_tariffBL = new TariffBL();
    Project_Variables P_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
	Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    public static string UrlPrint = string.Empty;
    string str;
    public string lastUpdatedDate = string.Empty;
    public string headname;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;

    #endregion

    #region page load zone
    protected void Page_Load(object sender, EventArgs e)
    {
        P_Val.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/";
        if (ViewState["lastUpdateDate"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate"].ToString();
        }
        if (Request.QueryString["menuid"] == null || Convert.ToInt16(Request.QueryString["position"]) == null)
        {

        }
        else
        {
            P_Val.PageID = Request.QueryString["menuid"].ToString();
            P_Val.position = Convert.ToInt16(Request.QueryString["position"]);
        }
        //This is for bredcrumb
		 if (RewriteModule.RewriteContext.Current.Params["catid"] != null)
        {
        if (RewriteModule.RewriteContext.Current.Params["catid"].ToString() == "1")
        {
            headname = Resources.HercResource.DistributionCharges + " ";

        }
        else if (RewriteModule.RewriteContext.Current.Params["catid"].ToString() == "2")
        {
            headname = Resources.HercResource.TransmissionCharges + " ";

        }
        else if (RewriteModule.RewriteContext.Current.Params["catid"].ToString() == "3")
        {
            headname = Resources.HercResource.GenerationTariff + " ";

        }
        else if (RewriteModule.RewriteContext.Current.Params["catid"].ToString() == "4")
        {
            headname = Resources.HercResource.WheelingCharges + " ";

        }
        else if (RewriteModule.RewriteContext.Current.Params["catid"].ToString() == "5")
        {
            headname = Resources.HercResource.Cross + " ";

        }
        else
        {
            headname = Resources.HercResource.Renewal + " ";

        }
		}
        //str = BreadcrumDL.DisplayBreadCrumTariff(Resources.HercResource.ConsumerEmpowerment, Resources.HercResource.Tariff, headname);

		 str = BreadcrumDL.DisplayBreadCrumTariffDetails(Resources.HercResource.ConsumerEmpowerment, Resources.HercResource.Tariff, headname,Resources.HercResource.Details);
        ltrlBreadcrum.Text = str.ToString();
	if (RewriteModule.RewriteContext.Current.Params["catid"] != null)
        {
        if (RewriteModule.RewriteContext.Current.Params["catid"].ToString() == "1")
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Dist.Attributes.Add("class", "current");
            }
            else
            {
                Dist_Hin.Attributes.Add("class", "current");
            }
        }
        else if (RewriteModule.RewriteContext.Current.Params["catid"].ToString() == "2")
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Trans.Attributes.Add("class", "current");
            }
            else
            {
                Trans_Hin.Attributes.Add("class", "current");
            }

        }
        else if (RewriteModule.RewriteContext.Current.Params["catid"].ToString() == "3")
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Gen.Attributes.Add("class", "current");
            }
            else
            {
                Gen_Hin.Attributes.Add("class", "current");
            }
        }
        else if (RewriteModule.RewriteContext.Current.Params["catid"].ToString() == "4")
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Wheeling.Attributes.Add("class", "current");
            }
            else
            {
                Wheeling_Hin.Attributes.Add("class", "current");
            }
        }
        else if (RewriteModule.RewriteContext.Current.Params["catid"].ToString() == "5")
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                cross.Attributes.Add("class", "current");
            }
            else
            {
                Cross_Hin.Attributes.Add("class", "current");
            }
        }
        else
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                renewal.Attributes.Add("class", "current");
            }
            else
            {
                renewal_Hin.Attributes.Add("class", "current");
            }
        }
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
        if (!IsPostBack)
        {

            Bind_TariffDetails();
        }

        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.Tariff;
        }
        else
        {
            PageTitle = Resources.HercResource.Tariff;
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
        obj.linkID = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["id"]);
        obj.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Tariff);
        P_Val.dSet = obj_tariffBL.GetdetailsData(obj);

        if (P_Val.dSet.Tables[0].Rows.Count > 0)
        {

            lastUpdatedDate = P_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            ViewState["lastUpdateDate"] = lastUpdatedDate;

            if (Request.QueryString["menuid"] != null && Convert.ToInt16(Request.QueryString["menuid"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA))
            {
                lrtTariff.Text = HttpUtility.HtmlDecode(P_Val.dSet.Tables[0].Rows[0]["details"].ToString());
                lblyear.Text = "<strong>" + "FSA" + "</strong>" + ":- " + P_Val.dSet.Tables[0].Rows[0]["year"];
            }
            else if (Request.QueryString["menuid"] != null && Convert.ToInt16(Request.QueryString["menuid"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.GeneralMiscCharges))
            {
                lrtTariff.Text = HttpUtility.HtmlDecode(P_Val.dSet.Tables[0].Rows[0]["details"].ToString());
                lblyear.Text = "<strong>" + "General & Misc Charges" + "</strong>" + ":- " + P_Val.dSet.Tables[0].Rows[0]["year"];

            }
            else
            {
                lrtTariff.Text = HttpUtility.HtmlDecode(P_Val.dSet.Tables[0].Rows[0]["details"].ToString());
                lblyear.Text = "<strong>" + P_Val.dSet.Tables[0].Rows[0]["catName"] + "</strong>" + ":- " + P_Val.dSet.Tables[0].Rows[0]["year"];
                if (P_Val.dSet.Tables[0].Rows[0]["file_name"] != DBNull.Value && P_Val.dSet.Tables[0].Rows[0]["file_name"].ToString() != "")
                {
                    ltrlPdf.Visible = true;
                    //ltrlPdf.Text += " , " + "<a href='" + P_Val.url + P_Val.dSet.Tables[0].Rows[0]["file_name"] + "' Text='" + P_Val.dSet.Tables[0].Rows[0]["file_name"] + "' target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>";
					ltrlPdf.Text += " , " + "<a  title='" + Resources.HercResource.ViewDocument + "' href='" + P_Val.url + P_Val.dSet.Tables[0].Rows[0]["file_name"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> " ;
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
                //lrtTariff.Text = HttpUtility.HtmlDecode(P_Val.dSet.Tables[0].Rows[0]["details"].ToString());
                //lblyear.Text = "<strong>" + P_Val.dSet.Tables[0].Rows[0]["catName"] + "</strong>" + ":- " + P_Val.dSet.Tables[0].Rows[0]["year"];


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
        if ((Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["catid"]) == Convert.ToInt16(Module_ID_Enum.Tariff_category.DistributionCharges)))
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Response.Redirect("~/Tariff.aspx");
            }
            else
            {
                Response.Redirect("~/content/Hindi/Tariff/" + ResolveUrl(Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.DistributionCharges_Hindi).ToString()) + "_1_Tariff.aspx");
            }
        }
        else if ((Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["catid"]) == Convert.ToInt16(Module_ID_Enum.Tariff_category.GenerationTariff)))
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Response.Redirect("~/GenerationTariff.aspx");
            }
            else
            {
                Response.Redirect("~/content/Hindi/GenerationTariff/" + ResolveUrl(Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.GenerationTariff_Hindi).ToString()) + "_1_GenerationTariff.aspx");

            }
        }
        else if ((Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["catid"]) == Convert.ToInt16(Module_ID_Enum.Tariff_category.RenewalEnergy)))
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Response.Redirect("~/RenewalEnergy.aspx");
            }
            else
            {
                Response.Redirect("~/content/Hindi/RenewalEnergy/" + ResolveUrl(Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RenewalEnergy_Hindi).ToString()) + "_1_RenewalEnergy.aspx");

            }
        }
        else if ((Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["catid"]) == Convert.ToInt16(Module_ID_Enum.Tariff_category.CrossSubsidydditionalSurcharge)))
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Response.Redirect("~/Crosssubsidy.aspx");
            }
            else
            {
                Response.Redirect("~/content/Hindi/Crosssubsidy/" + ResolveUrl(Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.CrossSubsidy_Hindi).ToString()) + "_1_Crosssubsidy.aspx");

            }
        }
        else if ((Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["catid"]) == Convert.ToInt16(Module_ID_Enum.Tariff_category.WheelingCharges)))
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Response.Redirect("~/WheelingCharges.aspx");
            }
            else
            {
                Response.Redirect("~/content/Hindi/WheelingCharges/" + ResolveUrl(Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.WheelingCharges_Hindi).ToString()) + "_1_WheelingCharges.aspx");
            }
        }
        else if ((Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["catid"]) == Convert.ToInt16(Module_ID_Enum.Tariff_category.TransmissionCharges)))
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                Response.Redirect("~/TransmissionCharges.aspx");
            }
            else
            {
                Response.Redirect("~/content/Hindi/TransmissionCharges/" + ResolveUrl(Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.TransmissionCharges_Hindi).ToString()) + "_1_TransmissionCharges.aspx");
            }
        }
        else if (Convert.ToInt16(P_Val.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA))
        {
            Response.Redirect("~/FSA/" + P_Val.PageID + "_" + P_Val.position + "_FSA.aspx");
        }
        else
        {
            Response.Redirect("~/GeneralMiscCharges/" + P_Val.PageID + "_" + P_Val.position + "_GeneralCharges.aspx");
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
