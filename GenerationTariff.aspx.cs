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

public partial class GenerationTariff : BasePage
{
    #region variable declaration

    TariffOB obj = new TariffOB();
    TariffBL obj_tariffBL = new TariffBL();
    Project_Variables P_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    public string lastUpdatedDate = string.Empty;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string str;
    public static string UrlPrint = string.Empty;
    #endregion

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        P_Val.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/"; 
        str = BreadcrumDL.DisplayBreadCrumTariff(Resources.HercResource.ConsumerEmpowerment, Resources.HercResource.Tariff, Resources.HercResource.GenerationTariff);
        ltrlBreadcrum.Text = str.ToString();
        if (!IsPostBack)
        {
            BindYear();

            Bind_Tariff(1);
        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
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
            PageTitle = Resources.HercResource.Tariff;
        }
        else
        {
            PageTitle = Resources.HercResource.Tariff;
        }
    }

    #endregion

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
        //obj.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        //if (ViewState["year"] != null)
        //{
        //    obj.Year = ViewState["year"].ToString();
        //}
        //else
        //{
        //    obj.Year = null;
        //}

        if (ViewState["year"] != null)
        {
            obj.Year = ViewState["year"].ToString();
            drpyear.SelectedValue = ViewState["year"].ToString();
        }
        else
        {
            obj.Year = null;
        }

        obj.CatId = Convert.ToInt16(Module_ID_Enum.Tariff_category.GenerationTariff);

        P_Val.dSet = obj_tariffBL.Getdetails(obj,out P_Val.k);

        if (P_Val.dSet.Tables[0].Rows.Count > 0)
        {
            lblmsg.Visible = false;

            for (int i = 0; i < P_Val.dSet.Tables[0].Rows.Count; i++)
            {

                P_Val.sbuilder.Append("<ul>");
                P_Val.sbuilder.Append("<li>");
                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                {
                    if (P_Val.dSet.Tables[0].Rows[i]["details"] != DBNull.Value && P_Val.dSet.Tables[0].Rows[i]["details"] != "")
                    {
                        P_Val.sbuilder.Append("<a title='" + Resources.HercResource.ClickHereToViewDetails + "' href='" + ResolveUrl("~/TariffDetails/" + P_Val.dSet.Tables[0].Rows[i]["link_id"].ToString() + "_" + P_Val.dSet.Tables[0].Rows[i]["Cat_Id"].ToString() + ".aspx") + "' >" + P_Val.dSet.Tables[0].Rows[i]["year"] + "</a>");
                    }
                    else
                    {
                        P_Val.sbuilder.Append( P_Val.dSet.Tables[0].Rows[i]["year"]);
                    }

                    if (P_Val.dSet.Tables[0].Rows[i]["file_name"] != DBNull.Value && P_Val.dSet.Tables[0].Rows[i]["file_name"] != "")
                    {

                        P_Val.sbuilder.Append(" , " + "<a title='" + Resources.HercResource.ViewDocument + "' href='" + P_Val.url + P_Val.dSet.Tables[0].Rows[i]["file_name"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> ");

                        if (File.Exists(Server.MapPath(P_Val.url) + P_Val.dSet.Tables[0].Rows[i]["file_name"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(P_Val.url) + P_Val.dSet.Tables[0].Rows[i]["file_name"]);
                            double FileInBytes = finfo.Length;
                            P_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                        P_Val.sbuilder.Append("</a>");
                    }
                }
                else
                {
                    if (P_Val.dSet.Tables[0].Rows[i]["details"] != DBNull.Value && P_Val.dSet.Tables[0].Rows[i]["details"] != "")
                    {
                        P_Val.sbuilder.Append("<a title='" + Resources.HercResource.ClickHereToViewDetails + "' href='" + ResolveUrl("~/content/Hindi/TariffDetails/" + P_Val.dSet.Tables[0].Rows[i]["link_id"].ToString() + "_" + P_Val.dSet.Tables[0].Rows[i]["Cat_Id"].ToString() + ".aspx") + "' >" + P_Val.dSet.Tables[0].Rows[i]["year"] + "</a>");
                    }
                    else
                    {
                        P_Val.sbuilder.Append(P_Val.dSet.Tables[0].Rows[i]["year"]);
                    }
                    if (P_Val.dSet.Tables[0].Rows[i]["file_name"] != DBNull.Value && P_Val.dSet.Tables[0].Rows[i]["file_name"] != "")
                    {

                        P_Val.sbuilder.Append(" , " + "<a title='" + Resources.HercResource.ViewDocument + "'  href='" + P_Val.url + P_Val.dSet.Tables[0].Rows[i]["file_name"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> ");
                        if (File.Exists(Server.MapPath(P_Val.url) + P_Val.dSet.Tables[0].Rows[i]["file_name"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(P_Val.url) + P_Val.dSet.Tables[0].Rows[i]["file_name"]);
                            double FileInBytes = finfo.Length;
                            P_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                        P_Val.sbuilder.Append("</a>");
                    }
                }
                P_Val.sbuilder.Append("</li>");
                P_Val.sbuilder.Append("</ul>");
            }
            lrtTariff.Text = P_Val.sbuilder.ToString();
            P_Val.sbuilder.Remove(0, P_Val.sbuilder.Length);
            lastUpdatedDate = P_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
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


    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Tariff(1);
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
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
        Bind_Tariff(1);
    }

    #region function to Bind data in a data list for year

    public void BindYear()
    {
        pyear.Visible = true;
        ddlPageSize.Visible = false;
        obj.CatId = Convert.ToInt16(Module_ID_Enum.Tariff_category.GenerationTariff);

        P_Val.dSetChildData = obj_tariffBL.getYearForTariff(obj);

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


}
