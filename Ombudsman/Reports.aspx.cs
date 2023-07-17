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

public partial class Ombudsman_Reports : BasePageOmbudsman
{

    #region variable declaration

    TariffOB obj = new TariffOB();
    TariffBL obj_tariffBL = new TariffBL();
    Project_Variables P_Val = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    PaginationBL pagingBL = new PaginationBL();
    public string lastUpdatedDate = string.Empty;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;
    string str;
    public static string UrlPrint = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        P_Val.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/"; 
        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.Reports);
        P_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]); 
        ltrlBreadcrum.Text = str.ToString();
        if (!IsPostBack)
        {
            if (RewriteModule.RewriteContext.Current.Params["menuid"] != null)
            {
                P_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString(); //Request.QueryString["menuid"].ToString();
            }
            if (P_Val.PageID.Length > 6)
            {
                P_Val.PageID = P_Val.PageID.Substring(6);
            }
            else
            {
                P_Val.PageID = P_Val.PageID.ToString();
            }
            if (string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                BindYear();
                Bind_Reports(1);
            }
        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            ViewState["year"] = Session["yearnew"];
            pyear.Visible = false;
            Bind_Reports(1);
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssRef);
        }
        PageTitle = Resources.HercResource.Reports;
    }
    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["year"] = drpyear.SelectedValue;
        Session["yearnew"] = drpyear.SelectedValue;
       Bind_Reports(1);

    }

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
       this.Bind_Reports(1);
    }

    #endregion


    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Bind_Reports(1);
    }

    #endregion




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


        P_Val.dSetChildData = obj_tariffBL.getYearForReport();

        if (P_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
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


    public void Bind_Reports(int pageIndex)
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
        if (ViewState["year"] != null)
        {
            obj.Year = ViewState["year"].ToString();
            drpyear.SelectedValue = ViewState["year"].ToString();
        }
        else
        {
            obj.Year = null;
        }

        //obj.CatId = Convert.ToInt16(Module_ID_Enum.Tariff_category.GenerationTariff);

        P_Val.dSet = obj_tariffBL.GetReportsdetails(obj, out P_Val.k);

        if (P_Val.dSet.Tables[0].Rows.Count > 0)
        {
            lblmsg.Visible = false;

            for (int i = 0; i < P_Val.dSet.Tables[0].Rows.Count; i++)
            {
                // This code for multiple files
                obj.TempLinkId =Convert.ToInt16(P_Val.dSet.Tables[0].Rows[i]["link_id"]);
                P_Val.dsFileName = obj_tariffBL.getFileName(obj);
                //End

                P_Val.sbuilder.Append("<ul>");
                P_Val.sbuilder.Append("<li>");
                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                {
                    if (P_Val.dSet.Tables[0].Rows[i]["details"] != DBNull.Value && P_Val.dSet.Tables[0].Rows[i]["details"] != "")
                    {
                        P_Val.sbuilder.Append("<a href='" + ResolveUrl("~/ReportsDetails/" + P_Val.dSet.Tables[0].Rows[i]["link_id"].ToString() + "_" + P_Val.dSet.Tables[0].Rows[i]["PlaceholderFour"].ToString() + "_" + RewriteModule.RewriteContext.Current.Params["menuid"] +"_"+RewriteModule.RewriteContext.Current.Params["position"]+ ".aspx") + "' Text='" + P_Val.dSet.Tables[0].Rows[i]["Name"] + "'>" + P_Val.dSet.Tables[0].Rows[i]["year"] + "</a>");
                    }
                    else
                    {
                        P_Val.sbuilder.Append(P_Val.dSet.Tables[0].Rows[i]["year"]);
                    }
                    if (P_Val.dsFileName.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < P_Val.dsFileName.Tables[0].Rows.Count; j++)
                        {
                            P_Val.sbuilder.Append(" , " + "<a href='" + P_Val.url + P_Val.dsFileName.Tables[0].Rows[j]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");
                            if (P_Val.dsFileName.Tables[0].Rows[j]["FileName"] != DBNull.Value)
                            {
                                if (File.Exists(Server.MapPath(P_Val.url) + P_Val.dsFileName.Tables[0].Rows[j]["FileName"]))
                                {
                                    FileInfo finfo = new FileInfo(Server.MapPath(P_Val.url) + P_Val.dsFileName.Tables[0].Rows[j]["FileName"]);
                                    double FileInBytes = finfo.Length;
                                    P_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                                }
                            }

                            P_Val.sbuilder.Append("</a>");

                        }
                    }
                }
                else
                {
                    if (P_Val.dSet.Tables[0].Rows[i]["details"] != DBNull.Value && P_Val.dSet.Tables[0].Rows[i]["details"] != "")
                    {
                        P_Val.sbuilder.Append("<a href='" + ResolveUrl("~/Ombudsmancontent/Hindi/ReportsDetails/" + P_Val.dSet.Tables[0].Rows[i]["link_id"].ToString() + "_" + P_Val.dSet.Tables[0].Rows[i]["PlaceholderFour"].ToString() + "_" + RewriteModule.RewriteContext.Current.Params["menuid"] +"_"+RewriteModule.RewriteContext.Current.Params["position"]+ ".aspx") + "' Text='" + P_Val.dSet.Tables[0].Rows[i]["Name"] + "'>" + P_Val.dSet.Tables[0].Rows[i]["year"] + "</a>");
                    }
                    else
                    {
                        P_Val.sbuilder.Append(P_Val.dSet.Tables[0].Rows[i]["year"]);
                    }
                    if (P_Val.dsFileName.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < P_Val.dsFileName.Tables[0].Rows.Count; j++)
                        {
                            P_Val.sbuilder.Append(" , " + "<a href='" + P_Val.url + P_Val.dsFileName.Tables[0].Rows[j]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");
                            if (P_Val.dsFileName.Tables[0].Rows[j]["FileName"] != DBNull.Value)
                            {
                                if (File.Exists(Server.MapPath(P_Val.url) + P_Val.dsFileName.Tables[0].Rows[j]["FileName"]))
                                {
                                    FileInfo finfo = new FileInfo(Server.MapPath(P_Val.url) + P_Val.dsFileName.Tables[0].Rows[j]["FileName"]);
                                    double FileInBytes = finfo.Length;
                                    P_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                                }
                            }

                            P_Val.sbuilder.Append("</a>");

                        }
                    }
                }
                P_Val.sbuilder.Append("</li>");
                P_Val.sbuilder.Append("</ul>");
            }
            lrtTariff.Text = P_Val.sbuilder.ToString();
            P_Val.sbuilder.Remove(0, P_Val.sbuilder.Length);
            lastUpdatedDate = P_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();

            MetaKeyword = P_Val.dSet.Tables[0].Rows[0]["Meta_Keywords"].ToString();
            MetaDescription = P_Val.dSet.Tables[0].Rows[0]["Mate_Description"].ToString();
            MetaLng = P_Val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = P_Val.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
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
