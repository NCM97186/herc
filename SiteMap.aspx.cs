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

public partial class SiteMap : BasePage
{
    //Area for all the variables declaration zone

    #region Data declaration zone

    public static string UrlPrint = string.Empty;
    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    Project_Variables p_var = new Project_Variables();
    LinkOB lnkObject = new LinkOB();
    LinkBL objlnkBL = new LinkBL();
    public string lastUpdatedDate = string.Empty;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    #endregion

    //End

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            p_var.langid = Convert.ToString(Resources.HercResource.Lang_Id);
            p_var.galleryID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
            p_var.str = BreadcrumDL.DisplayBreadCrum(Resources.HercResource.Sitemap);
            ltrlBreadcrum.Text = p_var.str;
            p_var.url = Resources.HercResource.PageUrl.ToString();
            if (!IsPostBack)
            {

                displaySitemap();
                displayUpdatedDate();
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


            PageTitle = Resources.HercResource.Sitemap;

        }
        catch { }
    }
    #endregion

    //Area for all the user defined functions

    #region Function to bind main menu in Sitemap

    public void displaySitemap()
    {
        try
        {
            lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            lnkObject.PositionId = 1;
            p_var.position = (int)lnkObject.PositionId;
            lnkObject.ModuleId = 1;
            lnkObject.LinkParentId = 0;

            p_var.dSet = menuBL.get_Frontside_RootMenu(lnkObject);

            if (p_var.dSet.Tables[0].Rows.Count > 0)
            {
                p_var.sbuilder.Append("<ul>");
                for (p_var.i = 0; p_var.i < p_var.dSet.Tables[0].Rows.Count; p_var.i++)
                {
                    p_var.menu_name = p_var.dSet.Tables[0].Rows[p_var.i]["name"].ToString();
                    p_var.urlname = p_var.dSet.Tables[0].Rows[p_var.i]["placeholderone"].ToString();
                    p_var.menuid = Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["link_id"]);

                    if (p_var.menuid == (int)Module_ID_Enum.project_MenuID_FrontEnd.Home || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Home_Hindi || p_var.menu_name == "Home" || p_var.menu_name == "home") //For Home page and Gallery.
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.project_MenuID_FrontEnd.Home)) //Only for home
                            {

                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + "index.aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");

                            }
                        }
                        else
                        {
                            if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Home_Hindi)) //Only for home Hindi
                            {
                                p_var.sbuilder.Append("<a  title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/content/Hindi/") + "index.aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");

                            }
                        }
                        //p_var.sbuilder.Append("</li>");
                    }
                    else
                    {
                        if (p_var.dSet.Tables[0].Rows[p_var.i]["counter_Child"] != DBNull.Value)
                        {

                            p_var.sbuilder.Append("<li>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                // Change by gaurav 18-9 -2012


                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "'  href='#'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                            }
                            else
                            {

                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='#'>" + p_var.menu_name.Replace("&", "&amp;") + "</a>");

                            }

                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + Server.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + Server.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                            }

                        }


                    }

                    //function to bind child of parent menu

                    BindMenuChildren(1, Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["link_id"]));

                    //end

                    p_var.sbuilder.Append("</li>");
                }
            }
            p_var.sbuilder.Append("</ul>");
            BindTopUpperLinks();
            BindRootMenu_Category();
            Bind_Middle();
            BindFooterMenu();

            ltrlMenu.Text = p_var.sbuilder.ToString();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to bind child menu of root menu

    void BindMenuChildren(int level, int parent_ID)
    {
        Menu_ManagementBL subMenuBL = new Menu_ManagementBL();

        DataSet subMenuDataSet = new DataSet();
        DataSet parentMenuDataSet = new DataSet();
        LinkOB _lnkObject = new LinkOB();
        LinkOB _lnkParentObject = new LinkOB();
        LinkBL lnkBL = new LinkBL();
        string subMenuName = string.Empty;
        int subMenuID;
        _lnkObject.LinkParentId = parent_ID;
        _lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);


        subMenuDataSet = subMenuBL.get_Frontside_Submenu_of_RootMenu(_lnkObject);
        if (subMenuDataSet.Tables[0].Rows.Count > 0)
        {

            p_var.sbuilder.Append("<ul>");
            for (int i = 0; i < subMenuDataSet.Tables[0].Rows.Count; i++)
            {
                Menu_ManagementBL parentMenuBL = new Menu_ManagementBL();
                _lnkParentObject.LinkParentId = Convert.ToInt16(subMenuDataSet.Tables[0].Rows[i]["link_id"]);
                parentMenuDataSet = parentMenuBL.get_Link_Menu_ID(_lnkParentObject);
                subMenuName = subMenuDataSet.Tables[0].Rows[i]["name"].ToString();
                p_var.urlname = subMenuDataSet.Tables[0].Rows[i]["placeholderone"].ToString();
                subMenuID = Convert.ToInt16(subMenuDataSet.Tables[0].Rows[i]["link_id"]);

                if (subMenuDataSet.Tables[0].Rows[i]["counter_Child"] != DBNull.Value)
                {
                    if (parentMenuDataSet.Tables[0].Rows.Count > 0)
                    {
                        p_var.sbuilder.Append("<li>");


                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='#'>" + HttpUtility.HtmlDecode(subMenuName) + "</a>");

                    }
                    else
                    {
                        if (subMenuDataSet.Tables[0].Rows[i]["LinkTypeId"].ToString() == "3")
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "</a>");

                        }

                        else
                        {

                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "</a>");


                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    BindMenuChildren(level + 1, Convert.ToInt16(subMenuDataSet.Tables[0].Rows[i]["link_id"]));
                    p_var.sbuilder.Append("</li>");
                }
                else
                {
                    if (parentMenuDataSet.Tables[0].Rows.Count > 0)
                    {

                        p_var.sbuilder.Append("<li>");
                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "</a>");



                    }
                    else
                    {
                        if (subMenuDataSet.Tables[0].Rows[i]["Link_Type_Id"].ToString() == "3")
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "</a>");


                        }

                        else
                        {

                            if (subMenuID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Herc) || subMenuID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Herc_Hindi))
                            {
                                p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "CurrentRTI/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                                }
                                else
                                {

                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "CurrentRTI/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                            }
                            else if (subMenuID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Herc_Prev_RTI) || subMenuID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Herc_Prev_RTI_Hindi))
                            {
                                p_var.sbuilder.Append("<li>");
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "PrevRTI/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "PrevRTI/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                            }
                            else if (subMenuID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Hercsearch_RTI) || subMenuID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Hercsearch_RTI_Hindi))
                            {
                                p_var.sbuilder.Append("<li>");
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "searchRTI/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "searchRTI/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                            }
                            else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.profile || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.profile_Hindi)
                            {
                                p_var.sbuilder.Append("<li>");
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Profile/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Profile/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                            }
                            else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.DistributionCharges || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.DistributionCharges_Hindi)
                            {
                                p_var.sbuilder.Append("<li>");
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Tariff/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Tariff/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                            }
                            else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.TransmissionCharges || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.TransmissionCharges_Hindi)
                            {
                                p_var.sbuilder.Append("<li>");
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "TransmissionCharges/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "TransmissionCharges/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                            }
                            else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff_Hindi)
                            {
                                p_var.sbuilder.Append("<li>");
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "GenerationTariff/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "GenerationTariff/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                                }
                            }
                            else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.WheelingCharges || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.WheelingCharges_Hindi)
                            {
                                p_var.sbuilder.Append("<li>");
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "WheelingCharges/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "WheelingCharges/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                            }

                            else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.CrossSubsidy || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.CrossSubsidy_Hindi)
                            {
                                p_var.sbuilder.Append("<li>");
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Crosssubsidy/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Crosssubsidy/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                            }

                            else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RenewalEnergy || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RenewalEnergy_Hindi)
                            {
                                p_var.sbuilder.Append("<li>");
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "RenewalEnergy/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "RenewalEnergy/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                            }
                            else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.FSA || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.FSA_Hindi)
                            {
                                p_var.sbuilder.Append("<li>");
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "FSA/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "FSA/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                            }

                            else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.GeneralMiscCharges || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.GeneralMiscCharges_Hindi)
                            {
                                p_var.sbuilder.Append("<li>");
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "GeneralMiscCharges/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "GeneralMiscCharges/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                            }
                            else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.LocateUs || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.LocateUs_Hindi)
                            {
                                p_var.sbuilder.Append("<li>");
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Locate/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Locate/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                            }

                            else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.LocateUs || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.LocateUs_Hindi)
                            {
                                p_var.sbuilder.Append("<li>");
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Locate/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Locate/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                                }
                            }
                            else
                            {
                                p_var.sbuilder.Append("<li>");

                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "</a>");

                            }
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                }


            }
            p_var.sbuilder.Append("</ul>");
        }

    }

    #endregion

    #region Function to get Main Footer menu

    public void BindFooterMenu()
    {
        try
        {
            lnkObject.LangId = Convert.ToInt16(p_var.langid);
            lnkObject.PositionId = (int)Module_ID_Enum.Menu_Position.footer;
            lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.Approved;
            p_var.position = (int)lnkObject.PositionId;
            lnkObject.ModuleId = (int)Module_ID_Enum.Project_Module_ID.Menu;
            lnkObject.LinkParentId = (int)Module_ID_Enum.Link_parentID_Footer.parent_Footer;
            p_var.dSet = menuBL.get_Frontside_RootMenu(lnkObject);
            p_var.sbuilder.Append("<br/><hr/><br/><strong class=\"footer-below-had\">" + Resources.HercResource.FooterLinks + "</strong><br/>");
            p_var.sbuilder.Append("<ul>");
            for (int i = 0; i < p_var.dSet.Tables[0].Rows.Count; i++)
            {
                p_var.menu_name = HttpUtility.HtmlDecode(p_var.dSet.Tables[0].Rows[i]["name"].ToString());
                p_var.menuid = Convert.ToInt16(p_var.dSet.Tables[0].Rows[i]["link_id"]);
                if (p_var.dSet.Tables[0].Rows[i]["PlaceHolderOne"] != DBNull.Value && p_var.dSet.Tables[0].Rows[i]["PlaceHolderOne"].ToString() != "")
                {
                    p_var.urlname = p_var.dSet.Tables[0].Rows[i]["PlaceHolderOne"].ToString();
                }
                else
                {
                    p_var.urlname = p_var.dSet.Tables[0].Rows[i]["name"].ToString();
                }

                p_var.str = p_var.dSet.Tables[0].Rows[i]["url"].ToString();
                p_var.linkid = Convert.ToInt16(p_var.dSet.Tables[0].Rows[i]["Link_Type_Id"]);

                if (i == 0)
                {
                    if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Footer_Home || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Home_Footer_Hindi)
                    {
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "index.aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "index.aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                    }
                    else
                    {
                        if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap_Hindi)
                        {

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                        }
                        else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer_Hindi)
                        {

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                        }
                        else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_Eng || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_hindi)
                        {

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                        }
                        else
                        {
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                        }
                    }

                }


                else
                {
                    if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap_Hindi)
                    {

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                    }
                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer_Hindi)
                    {

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                    }
                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_Eng || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_hindi)
                    {

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                    }
                    else if (p_var.menu_name == "Disclaimer" || p_var.menu_name == "डिस्क्लेमर")
                    {
                        p_var.sbuilder.Append("<li>");
                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                    }
                    else
                    {
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            //Code to remove 
                            LinkOB _lnkSubmenuObject = new LinkOB();
                            DataSet ds = new DataSet();
                            _lnkSubmenuObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
                            _lnkSubmenuObject.PositionId = (int)Module_ID_Enum.Menu_Position.footer;
                            _lnkSubmenuObject.LinkParentId = p_var.menuid;
                            ds = menuBL.get_Cliked_Parent_Child_Menu(_lnkSubmenuObject);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[0]["link_id"] + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                            }

                            else
                            {

                                p_var.sbuilder.Append("<li>");
                                if (p_var.linkid != null && p_var.linkid == Convert.ToInt16(Module_ID_Enum.link_type_id.link))
                                {
                                    p_var.sbuilder.Append("<a target='_blank' title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' onclick=\"javascript:return confirm( 'This link shall take you to a webpage outside this website. Click OK to continue. Click Cancel to stop.');\" href='" + p_var.str + "'>" + HttpUtility.HtmlDecode(p_var.urlname).Replace("&", "&amp;") + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                                }

                            }

                        }
                        else
                        {
                            //Code to remove 
                            LinkOB _lnkSubmenuObject = new LinkOB();
                            DataSet ds = new DataSet();
                            _lnkSubmenuObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
                            _lnkSubmenuObject.PositionId = (int)Module_ID_Enum.Menu_Position.footer;
                            _lnkSubmenuObject.LinkParentId = p_var.menuid;
                            ds = menuBL.get_Cliked_Parent_Child_Menu(_lnkSubmenuObject);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[0]["link_id"] + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                            }

                            else
                            {
                                p_var.sbuilder.Append("<li>");
                                if (p_var.linkid != null && p_var.linkid == Convert.ToInt16(Module_ID_Enum.link_type_id.link))
                                {
                                    p_var.sbuilder.Append("<a target='_blank'  title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' onclick=\"javascript:return confirm( 'यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।.');\" href='" + p_var.str + "'>" + HttpUtility.HtmlDecode(p_var.urlname).Replace("&", "&amp;") + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                                }


                            }

                        }
                    }
                }
                p_var.sbuilder.Append("</li>");
            }

            p_var.sbuilder.Append("</ul>");
            ltrlMenu.Text = p_var.sbuilder.ToString();
        }
        catch
        {
            throw;
        }
    }

    #endregion



    #region Bind Home page childleftmenu
    void BindHomeLeft_Menu_Children(int level, int parent_ID)
    {
        Menu_ManagementBL subMenuBL = new Menu_ManagementBL();

        DataSet subMenuDataSet = new DataSet();
        DataSet parentMenuDataSet = new DataSet();
        LinkOB _lnkObject = new LinkOB();
        LinkOB _lnkParentObject = new LinkOB();
        LinkBL lnkBL = new LinkBL();
        string subMenuName = string.Empty;
        int subMenuID;
        _lnkObject.LinkParentId = parent_ID;
        _lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);


        subMenuDataSet = subMenuBL.get_Frontside_Submenu_of_RootMenu(_lnkObject);
        if (subMenuDataSet.Tables[0].Rows.Count > 0)
        {

            p_var.sbuilder.Append("<ul>");
            for (int i = 0; i < subMenuDataSet.Tables[0].Rows.Count; i++)
            {
                Menu_ManagementBL parentMenuBL = new Menu_ManagementBL();
                _lnkParentObject.LinkParentId = Convert.ToInt16(subMenuDataSet.Tables[0].Rows[i]["link_id"]);
                parentMenuDataSet = parentMenuBL.get_Link_Menu_ID(_lnkParentObject);
                subMenuName = subMenuDataSet.Tables[0].Rows[i]["name"].ToString();
                p_var.urlname = subMenuDataSet.Tables[0].Rows[i]["placeholderone"].ToString();
                subMenuID = Convert.ToInt16(subMenuDataSet.Tables[0].Rows[i]["link_id"]);


                p_var.sbuilder.Append("<li>");
                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' class=\"margin\" href='" + ResolveUrl("~/") + Resources.HercResource.PageUrl + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName) + "</a>");
                p_var.sbuilder.Append("</li>");



            }
            p_var.sbuilder.Append("</ul>");

        }
    }
    #endregion

    #region Function to display Updated date for sitemap

    public void displayUpdatedDate()
    {
        lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        lastUpdatedDate = menuBL.getLastUpdatedDateSiteMap(lnkObject);

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



    #region Function to get Main Root menu

    public void BindRootMenu_Category()
    {
        try
        {
            lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            lnkObject.PositionId = 3;
            p_var.position = (int)lnkObject.PositionId;
            lnkObject.ModuleId = 1;
            lnkObject.LinkParentId = 0;

            if (Request.QueryString["DepttId"] != null)
            {
                p_var.dSet = menuBL.get_Frontside_RootMenu_Ombudsman(lnkObject);
            }
            else
            {
                p_var.dSet = menuBL.get_Frontside_RootMenu(lnkObject);
            }
            p_var.sbuilder.Append("<br/><hr/><br/><strong class=\"footer-below-had\">" + Resources.HercResource.Leftlinks + "</strong><br/><br/>");
            p_var.sbuilder.Append("<ul>");
            if (p_var.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_var.i = p_var.dSet.Tables[0].Rows.Count - 1; p_var.i >= 0; p_var.i--)
                {
                    p_var.menu_name = p_var.dSet.Tables[0].Rows[p_var.i]["name"].ToString();
                    p_var.urlname = p_var.dSet.Tables[0].Rows[p_var.i]["placeholderone"].ToString();
                    p_var.menuid = Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["link_id"]);

                    if (p_var.i == 0)
                    {
                        p_var.sbuilder.Append("<li>");
                    }
                    else if (p_var.i == p_var.dSet.Tables[0].Rows.Count - 1)
                    {

                        p_var.sbuilder.Append("<li>");
                    }
                    else
                    {
                        if (p_var.menuid != 773)
                        {
                            p_var.sbuilder.Append("<li>");
                        }
                    }

                    if (p_var.menu_name == "APTEL" || p_var.menuid == 861 || p_var.menuid == 862)
                    {
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            if (p_var.dSet.Tables[0].Rows[p_var.i]["counter_Child"] != DBNull.Value)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='#'>" + Server.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + Server.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                        }
                        else
                        {
                            if (p_var.dSet.Tables[0].Rows[p_var.i]["counter_Child"] != DBNull.Value)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='#'>" + Server.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + Server.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                        }

                    }
                    if (p_var.menu_name == "Parliament / Assembly Question Answer" || p_var.menuid == 213 || p_var.menuid == 200)
                    {
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            if (p_var.dSet.Tables[0].Rows[p_var.i]["counter_Child"] != DBNull.Value)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='#'>" + Server.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + Server.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                        }
                        else
                        {
                            if (p_var.dSet.Tables[0].Rows[p_var.i]["counter_Child"] != DBNull.Value)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='#'>" + Server.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + Server.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                        }

                    }

                  

                    //function to bind child of parent menu

                    BindMenuChildrenHome(1, Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["link_id"]));

                    //end

                    //15 jan 2013
                    Menu_ManagementBL subMenuBL = new Menu_ManagementBL();

                    LinkOB _lnkObject = new LinkOB();
                    _lnkObject.LinkParentId = Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["link_id"]);
                    _lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);


                    DataSet ds = subMenuBL.get_Frontside_Submenu_of_RootMenu(_lnkObject);
                    if (p_var.i == 5)
                    {
                        //CODE FOR REGULATIONS ON DATE 03-12-2012

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                        {
                           
                            p_var.sbuilder.Append("<a href=\"#\" title='" + Resources.HercResource.Regulations + "'>" + Resources.HercResource.Regulations + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");

                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "Regulation/1.aspx' title='" + Resources.HercResource.herc + "'>" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string urlname = ds.Tables[0].Rows[i]["placeholderone"].ToString();
                                if (Convert.ToInt16(ds.Tables[0].Rows[i]["link_id"]) == 192)
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[i]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "</a>");
                                }
                            }


                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "Regulation/8.aspx' title='" + Resources.HercResource.RepealedRegulations + "' >" + Resources.HercResource.RepealedRegulations + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }
                        else
                        {

                            p_var.sbuilder.Append("<a href=\"#\" title='" + Resources.HercResource.Regulations + "'>" + Resources.HercResource.Regulations + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");

                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/content/Hindi/Regulation/1.aspx") + "' title='" + Resources.HercResource.herc + "' >" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string urlname = ds.Tables[0].Rows[i]["placeholderone"].ToString();
                                if (Convert.ToInt16(ds.Tables[0].Rows[i]["link_id"]) == 203)
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[i]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "</a>");
                                }
                            }


                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/content/Hindi/Regulation/8.aspx") + "' title='" + Resources.HercResource.RepealedRegulations + "'>" + Resources.HercResource.RepealedRegulations + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }

                        //END
                    }
                    if (p_var.i == 4)
                    {
                        //CODE FOR CODES ON DATE 03-12-2012
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                        {

                            p_var.sbuilder.Append("<a href=\"#\" title='" + Resources.HercResource.Codes + "'>" + Resources.HercResource.Codes + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "Codes/1.aspx' title='" + Resources.HercResource.herc + "'>" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string urlname = ds.Tables[0].Rows[i]["placeholderone"].ToString();
                                if (Convert.ToInt16(ds.Tables[0].Rows[i]["link_id"]) == 195)
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[i]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "</a>");
                                }
                            }

                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "Codes/8.aspx' title='" + Resources.HercResource.RepealedCodes + "'>" + Resources.HercResource.RepealedCodes + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }
                        else
                        {
                            //p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"#\" title='" + Resources.HercResource.Codes + "'>" + Resources.HercResource.Codes + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/content/Hindi/Codes/1.aspx") + "' title='" + Resources.HercResource.herc + "'>" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string urlname = ds.Tables[0].Rows[i]["placeholderone"].ToString();
                                if (Convert.ToInt16(ds.Tables[0].Rows[i]["link_id"]) == 207)
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[i]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "</a>");
                                }
                            }

                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/content/Hindi/Codes/8.aspx") + "' title='" + Resources.HercResource.RepealedCodes + "'>" + Resources.HercResource.RepealedCodes + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("</ul>");
                            p_var.sbuilder.Append("</li>");
                        }

                        //END
                    }
                    if (p_var.i == 3)
                    {

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                        {
                            // p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"#\" title='" + Resources.HercResource.Standards + "'>" + Resources.HercResource.Standards + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "Standards/1.aspx' title='" + Resources.HercResource.herc + "'>" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string urlname = ds.Tables[0].Rows[i]["placeholderone"].ToString();
                                if (Convert.ToInt16(ds.Tables[0].Rows[i]["link_id"]) == 198)
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[i]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "</a>");
                                }
                            }

                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "Standards/8.aspx' title='" + Resources.HercResource.RepealedStandard + "'>" + Resources.HercResource.RepealedStandard + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }
                        else
                        {
                            // p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"#\" title='" + Resources.HercResource.Standards + "'>" + Resources.HercResource.Standards + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/content/Hindi/Standards/1.aspx") + "' title='" + Resources.HercResource.herc + "'>" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");

                            p_var.sbuilder.Append("<li>");


                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {

                                string urlname = ds.Tables[0].Rows[i]["placeholderone"].ToString();
                                if (Convert.ToInt16(ds.Tables[0].Rows[i]["link_id"]) == 211)
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[i]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "</a>");
                                }
                            }

                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/content/Hindi/Standards/8.aspx") + "' title='" + Resources.HercResource.RepealedStandard + "'>" + Resources.HercResource.RepealedStandard + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }

                        //END

                        //CODE FOR NOTIFICATIONS ON DATE 04-12-2012

                        LinkOB objnew = new LinkOB();
                        objnew.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                        {
                            objnew.LinkParentId = 773;//771 
                        }
                        else
                        {
                            objnew.LinkParentId = 775; //773  
                        }
                        DataSet ds1 = subMenuBL.get_Frontside_Submenu_of_RootMenu(objnew);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            string urlname1 = ds1.Tables[0].Rows[0]["placeholderone"].ToString();
                        }
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                        {

                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"#\" title='" + Resources.HercResource.Notifications + "'>" + Resources.HercResource.Notifications + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "Notifications/1.aspx' title='" + Resources.HercResource.herc + "' >" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");


                            string urlname1 = ds1.Tables[0].Rows[0]["placeholderone"].ToString();
                            if (Convert.ToInt16(ds1.Tables[0].Rows[0]["link_id"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Notification))
                            {
                                p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds1.Tables[0].Rows[0]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname1.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds1.Tables[0].Rows[0]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds1.Tables[0].Rows[0]["name"].ToString()) + "</a>");
                            }


                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "Notifications/8.aspx' title='" + Resources.HercResource.RepealedNotifications + "'>" + Resources.HercResource.RepealedNotifications + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"#\" title='" + Resources.HercResource.Notifications + "'>" + Resources.HercResource.Notifications + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/content/Hindi/Notifications/1.aspx") + "' title='" + Resources.HercResource.herc + "'>" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");

                            string urlname1 = ds1.Tables[0].Rows[0]["placeholderone"].ToString();
                            if (Convert.ToInt16(ds1.Tables[0].Rows[0]["link_id"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Notification_Hindi))
                            {
                                p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + p_var.url + ds1.Tables[0].Rows[0]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname1.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds1.Tables[0].Rows[0]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds1.Tables[0].Rows[0]["name"].ToString()) + "</a>");
                            }

                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/content/Hindi/Notifications/8.aspx") + "' title='" + Resources.HercResource.RepealedNotifications + "'>" + Resources.HercResource.RepealedNotifications + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }

                        //END

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"#\" title='" + Resources.HercResource.DailyOrders + "'>" + Resources.HercResource.DailyOrders + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "CurrentDailyOrders.aspx' title='" + Resources.HercResource.CurrentYear + "' >" + Resources.HercResource.CurrentYear + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "PrevYear/1.aspx' title='" + Resources.HercResource.PreviousYears + "' >" + Resources.HercResource.PreviousYears + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "SearchDailyOrders.aspx' title='" + Resources.HercResource.DailyOrdersSearch + "' >" + Resources.HercResource.DailyOrdersSearch + "</a>");
                            p_var.sbuilder.Append("</li>");

                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"#\" title='" + Resources.HercResource.DailyOrders + "'>" + Resources.HercResource.DailyOrders + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/content/Hindi/CurrentDailyOrders.aspx") + "' title='" + Resources.HercResource.CurrentYear + "'>" + Resources.HercResource.CurrentYear + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/content/Hindi/PrevYear/1.aspx") + "' title='" + Resources.HercResource.PreviousYears + "' >" + Resources.HercResource.PreviousYears + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/content/Hindi/SearchDailyOrders.aspx") + "' title='" + Resources.HercResource.DailyOrdersSearch + "' >" + Resources.HercResource.DailyOrdersSearch + "</a>");
                            p_var.sbuilder.Append("</li>");

                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"#\" title='" + Resources.HercResource.DDPapers + "'>" + Resources.HercResource.DDPapers + "</a>");

                            p_var.sbuilder.Append("<ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "DiscussionPapers.aspx' title='" + Resources.HercResource.CurrentDraftDisscussionPaper + "'>" + Resources.HercResource.CurrentDraftDisscussionPaper + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/DiscussionPapersPrevYear/1.aspx") + "' title='" + Resources.HercResource.OldDraftDisscussionPaper + "'>" + Resources.HercResource.OldDraftDisscussionPaper + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"#\" title='" + Resources.HercResource.DDPapers + "'>" + Resources.HercResource.DDPapers + "</a>");

                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/content/Hindi/DiscussionPapers.aspx") + "' title='" + Resources.HercResource.CurrentDraftDisscussionPaper + "'>" + Resources.HercResource.CurrentDraftDisscussionPaper + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/content/Hindi/DiscussionPapersPrevYear/1.aspx") + "' title='" + Resources.HercResource.OldDraftDisscussionPaper + "'>" + Resources.HercResource.OldDraftDisscussionPaper + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }


                    }
                }
            }



            //p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("</ul>");
            BindOrders();
            BindPetition();
            ltrlMenu.Text = p_var.sbuilder.ToString();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind child menu of root menu

    void BindMenuChildrenHome(int level, int parent_ID)
    {
        Menu_ManagementBL subMenuBL = new Menu_ManagementBL();

        DataSet subMenuDataSet = new DataSet();
        DataSet parentMenuDataSet = new DataSet();
        LinkOB _lnkObject = new LinkOB();
        LinkOB _lnkParentObject = new LinkOB();
        LinkBL lnkBL = new LinkBL();
        string subMenuName = string.Empty;
        int subMenuID;
        _lnkObject.LinkParentId = parent_ID;
        _lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);


        subMenuDataSet = subMenuBL.get_Frontside_Submenu_of_RootMenu(_lnkObject);
        if (p_var.menu_name == "Parliament / Assembly Question Answer" || p_var.menuid == 213 || p_var.menuid == 200)
        {
            if (subMenuDataSet.Tables[0].Rows.Count > 0)
            {

                p_var.sbuilder.Append("<ul>");
                for (int i = 0; i < subMenuDataSet.Tables[0].Rows.Count; i++)
                {
                    Menu_ManagementBL parentMenuBL = new Menu_ManagementBL();
                    _lnkParentObject.LinkParentId = Convert.ToInt16(subMenuDataSet.Tables[0].Rows[i]["link_id"]);
                    parentMenuDataSet = parentMenuBL.get_Link_Menu_ID(_lnkParentObject);
                    subMenuName = subMenuDataSet.Tables[0].Rows[i]["name"].ToString();
                    p_var.urlname = subMenuDataSet.Tables[0].Rows[i]["placeholderone"].ToString();
                    subMenuID = Convert.ToInt16(subMenuDataSet.Tables[0].Rows[i]["link_id"]);


                    p_var.sbuilder.Append("<li>");

                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'>" + HttpUtility.HtmlDecode(subMenuName) + "</a>");
                    p_var.sbuilder.Append("</li>");



                }
                p_var.sbuilder.Append("</ul>");
            }
            p_var.sbuilder.Append("</li>");
        }
        if (p_var.menu_name == "APTEL" || p_var.menuid == 861 || p_var.menuid == 862)
        {
            if (subMenuDataSet.Tables[0].Rows.Count > 0)
            {

                p_var.sbuilder.Append("<ul>");
                for (int i = 0; i < subMenuDataSet.Tables[0].Rows.Count; i++)
                {
                    Menu_ManagementBL parentMenuBL = new Menu_ManagementBL();
                    _lnkParentObject.LinkParentId = Convert.ToInt16(subMenuDataSet.Tables[0].Rows[i]["link_id"]);
                    parentMenuDataSet = parentMenuBL.get_Link_Menu_ID(_lnkParentObject);
                    subMenuName = subMenuDataSet.Tables[0].Rows[i]["name"].ToString();
                    p_var.urlname = subMenuDataSet.Tables[0].Rows[i]["placeholderone"].ToString();
                    subMenuID = Convert.ToInt16(subMenuDataSet.Tables[0].Rows[i]["link_id"]);


                    p_var.sbuilder.Append("<li>");

                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName) + "</a>");
                    p_var.sbuilder.Append("</li>");



                }
                p_var.sbuilder.Append("</ul>");
            }
            p_var.sbuilder.Append("</li>");
        }

    }

    #endregion


    public void Bind_Middle()
    {
        //p_var.sbuilder.Length = 0;
        lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);

        p_var.dSet = objlnkBL.Bind_Licensees(lnkObject);
        p_var.sbuilder.Append("<br/><hr/><br/><strong class=\"footer-below-had\">" + Resources.HercResource.MiddleLinks + "</strong><br/>");
        if (p_var.dSet.Tables[0].Rows.Count > 0)
        {
            p_var.sbuilder.Append("<ul>");
            p_var.sbuilder.Append("<li>");
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
            {
                p_var.sbuilder.Append("<a title='" + Resources.HercResource.Ombudsman + "' target=\"_blank\"  href='" + ResolveUrl("~/Ombudsman/Ombudsman.aspx") + "'>" + Resources.HercResource.Ombudsman + "</a>");
            }
            else
            {
                p_var.sbuilder.Append("<a title='" + Resources.HercResource.Ombudsman + "' target=\"_blank\"  href='" + ResolveUrl("~/content/Hindi/Ombudsman/Ombudsman.aspx") + "'>" + Resources.HercResource.Ombudsman + "</a>");
            }
            p_var.sbuilder.Append("</li>");
            //p_var.sbuilder.Append("<li>");
            for (p_var.i = 0; p_var.i < p_var.dSet.Tables[0].Rows.Count; p_var.i++)
            {
                p_var.menuid = Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["Link_Id"]);
                p_var.menu_name = p_var.dSet.Tables[0].Rows[p_var.i]["Name"].ToString();
                p_var.position = Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["Position_id"]);
                if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.licensees) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.licensees_Hindi))
                {
                    p_var.sbuilder.Append("<li>");
                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                    {


                        p_var.sbuilder.Append("<a title='" + Resources.HercResource.Licensees + "' class=\"licesens-icon\"  href='" + ResolveUrl("~/") + "content/354_5_Licenseen.aspx'>" + Resources.HercResource.Licensees + " </a>");


                    }
                    else
                    {

                        p_var.sbuilder.Append("<a title='" + Resources.HercResource.Licensees + "' class=\"licesens-icon\"  href='" + ResolveUrl("~/content/Hindi/") + "353_5_Licenseen.aspx'>" + Resources.HercResource.Licensees + " </a>");

                    }
                    p_var.sbuilder.Append("</li>");
                }
                if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.StateAdvisoryCommittee) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.StateAdvisoryCommittee_Hindi))
                {
                    p_var.sbuilder.Append("<li>");
                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                    {
                        p_var.sbuilder.Append("<a title='" + p_var.menu_name.Replace("&", "and") + "' class=\"state-advisory-icon\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                    }
                    else
                    {
                        p_var.sbuilder.Append("<a title='" + p_var.menu_name.Replace("&", "and") + "' class=\"state-advisory-icon\" href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                    }
                    p_var.sbuilder.Append("</li>");
                    //ltrlMenu.Text = p_var.sbuilder.ToString();
                }

                if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.CoordinationForum) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.CoordinationForum_Hindi))
                {
                    p_var.sbuilder.Append("<li>");
                    // p_var.sbuilder.Remove(0, p_var.sbuilder.Length);
                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                    {
                        p_var.sbuilder.Append("<a title='" + p_var.menu_name.Replace("&", "and") + "'  class=\"ordination-icon\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                    }
                    else
                    {
                        p_var.sbuilder.Append("<a title='" + p_var.menu_name.Replace("&", "and") + "'  class=\"ordination-icon\" href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                    }

                    p_var.sbuilder.Append("</li>");


                }


            }

            BindRightLinks();
            p_var.sbuilder.Append("</ul>");
            ltrlMenu.Text = p_var.sbuilder.ToString();

        }
    }

    protected void site_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
        {
            Response.Redirect(ResolveUrl("~/OmbudsmanSiteMap/853_2_OmbudsmanSiteMap.aspx"));
        }
        else
        {
            Response.Redirect(ResolveUrl("~/OmbudsmanContent/Hindi/OmbudsmanSiteMap/854_2_OmbudsmanSiteMap.aspx"));
        }
    }

    public void Bind_Abbreviations()
    {
        //p_var.sbuilder.Length = 0;
        lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);

        p_var.dSet = objlnkBL.Bind_Abbreviations(lnkObject);

        if (p_var.dSet.Tables[0].Rows.Count > 0)
        {
            for (p_var.i = 0; p_var.i < p_var.dSet.Tables[0].Rows.Count; p_var.i++)
            {
                p_var.menuid = Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["Link_Id"]);
                p_var.menu_name = p_var.dSet.Tables[0].Rows[p_var.i]["Name"].ToString();
                p_var.position = Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["Position_id"]);
                if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Abbreviations) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Abbreviations_Hindi))
                {
                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                    {


                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");


                    }
                    else
                    {


                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "&amp;").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");

                    }
                }


            }
        }
    }

    public void BindTopUpperLinks()
    {
        p_var.sbuilder.Append("<ul>");
        p_var.sbuilder.Append("<li>");
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
        {
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.AnnualReports + "'  href='" + ResolveUrl("~/AnnualReport.aspx") + "'>" + Resources.HercResource.AnnualReports + "</a>");
        }
        else
        {
            p_var.sbuilder.Append("<a  title='" + Resources.HercResource.AnnualReports + "' href='" + ResolveUrl("~/Content/Hindi/AnnualReport.aspx") + "'>" + Resources.HercResource.AnnualReports + "</a>");
        }
        p_var.sbuilder.Append("</li>");
        p_var.sbuilder.Append("<li>");
        Bind_Abbreviations();
        p_var.sbuilder.Append("</li>");

        p_var.sbuilder.Append("<li>");

        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
        {
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.Commissioncalendar + "'  href='" + ResolveUrl("~/scheduleOfHearings.aspx") + "'>" + Resources.HercResource.Commissioncalendar + "</a>");
        }
        else
        {
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.Commissioncalendar + "'  href='" + ResolveUrl("~/Content/Hindi/scheduleOfHearings.aspx") + "'>" + Resources.HercResource.Commissioncalendar + "</a>");
        }
        p_var.sbuilder.Append("</li>");
        p_var.sbuilder.Append("<li>");

        p_var.sbuilder.Append("<a title='" + Resources.HercResource.HERCMail + "' onclick=\"javascript:return confirm('This link shall take you to a webpage outside this website. Click OK to continue. Click Cancel to stop.');\" target=\"_blank\" href='" + ResolveUrl("https://mail.gov.in/") + "' >" + Resources.HercResource.HERCMail + "</a>");
        p_var.sbuilder.Append("</li>");

        p_var.sbuilder.Append("</ul>");
    }

    public void BindOrders()
    {

        p_var.sbuilder.Append("<br/><hr/><br/><strong class=\"footer-below-had\">" + Resources.HercResource.Orderlinks + "</strong><br/>");
        p_var.sbuilder.Append("<ul>");

        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
        {
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.CurrentYearOrder + "'  href='" + ResolveUrl("~/CurrentYearOrders.aspx") + "'>" + Resources.HercResource.CurrentYearOrder + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.PreviousYearOrder + "'  href='" + ResolveUrl("~/CurrentYearOrders/1.aspx") + "'>" + Resources.HercResource.PreviousYearOrder + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.OrdersunderAppeal + "'  href='" + ResolveUrl("~/OrderUnderAppeal.aspx") + "'>" + Resources.HercResource.OrdersunderAppeal + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.OrdersSearch + "'  href='" + ResolveUrl("~/CurrentOrderSearch.aspx") + "'>" + Resources.HercResource.OrdersSearch + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.CategorywiseOrders + "'  href='" + ResolveUrl("~/CategorywiseOrders.aspx") + "'>" + Resources.HercResource.CategorywiseOrders + "</a>");
            p_var.sbuilder.Append("</li>");
        }
        else
        {
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.CurrentYearOrder + "'  href='" + ResolveUrl("~/content/Hindi/CurrentYearOrders.aspx") + "'>" + Resources.HercResource.CurrentYearOrder + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.PreviousYearOrder + "'  href='" + ResolveUrl("~/content/Hindi/CurrentYearOrders/1.aspx") + "'>" + Resources.HercResource.PreviousYearOrder + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a  title='" + Resources.HercResource.OrdersunderAppeal + "' href='" + ResolveUrl("~/content/Hindi/OrderUnderAppeal.aspx") + "'>" + Resources.HercResource.OrdersunderAppeal + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.OrdersSearch + "'  href='" + ResolveUrl("~/content/Hindi/CurrentOrderSearch.aspx") + "'>" + Resources.HercResource.OrdersSearch + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.CategorywiseOrders + "'  href='" + ResolveUrl("~/content/Hindi/CategorywiseOrders.aspx") + "'>" + Resources.HercResource.CategorywiseOrders + "</a>");
            p_var.sbuilder.Append("</li>");
        }

        p_var.sbuilder.Append("</ul>");
    }

    public void BindPetition()
    {
        p_var.sbuilder.Append("<br/><hr/><br/><strong class=\"footer-below-had\">" + Resources.HercResource.Petitionlinks + "</strong><br/>");
        p_var.sbuilder.Append("<ul>");

        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
        {
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.CurrentPetition + "'  href='" + ResolveUrl("~/petition.aspx") + "'>" + Resources.HercResource.CurrentPetition + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.PreviousYearPetitions + "'  href='" + ResolveUrl("~/petition/1.aspx") + "'>" + Resources.HercResource.PreviousYearPetitions + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.PetitionsSearch + "'  href='" + ResolveUrl("~/PetitionSearch.aspx") + "'>" + Resources.HercResource.PetitionsSearch + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.OnlineStatus + "'  href='" + ResolveUrl("~/OnlineStatus.aspx") + "'>" + Resources.HercResource.OnlineStatus + "</a>");
            p_var.sbuilder.Append("</li>");

        }
        else
        {
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a  title='" + Resources.HercResource.CurrentPetition + "' href='" + ResolveUrl("~/content/Hindi/petition.aspx") + "'>" + Resources.HercResource.CurrentPetition + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.PreviousYearPetitions + "'  href='" + ResolveUrl("~/content/Hindi/petition/1.aspx") + "'>" + Resources.HercResource.PreviousYearPetitions + "</a>");
            p_var.sbuilder.Append("</li>");

            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.PetitionsSearch + "'  href='" + ResolveUrl("~/content/Hindi/PetitionSearch.aspx") + "'>" + Resources.HercResource.PetitionsSearch + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.OnlineStatus + "'  href='" + ResolveUrl("~/content/Hindi/OnlineStatus.aspx") + "'>" + Resources.HercResource.OnlineStatus + "</a>");
            p_var.sbuilder.Append("</li>");
        }

        p_var.sbuilder.Append("</ul>");
    }

    public void BindRightLinks()
    {

        p_var.sbuilder.Append("<li>");
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
        {
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.WhatNew.Replace("&", "and").Replace("'", "") + "'  href='" + ResolveUrl("~/WhatsNew.aspx") + "'>" + Resources.HercResource.WhatNew + "</a>");
        }
        else
        {
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.WhatNew.Replace("&", "and").Replace("'", "") + "'  href='" + ResolveUrl("~/content/Hindi/WhatsNew.aspx") + "'>" + Resources.HercResource.WhatNew + "</a>");
        }
        p_var.sbuilder.Append("</li>");
        p_var.sbuilder.Append("<li>");
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
        {
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.PowerSectorLinks.Replace("&", "and") + "'  href='" + ResolveUrl("~/content/391_4_Appellate.aspx") + "'>" + Resources.HercResource.PowerSectorLinks + "</a>");
        }
        else
        {
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.PowerSectorLinks.Replace("&", "and") + "'  href='" + ResolveUrl("~/content/Hindi/622_4_अपीलीयप्राधिकरण.aspx") + "'>" + Resources.HercResource.PowerSectorLinks + "</a>");
        }
        p_var.sbuilder.Append("</li>");
        p_var.sbuilder.Append("<li>");
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
        {
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.CLSH.Replace("&", "and") + "'  href='" + ResolveUrl("~/SOH.aspx") + "'>" + Resources.HercResource.CLSH + "</a>");
        }
        else
        {
            p_var.sbuilder.Append("<a title='" + Resources.HercResource.CLSH.Replace("&", "and") + "'  href='" + ResolveUrl("~/content/Hindi/SOH.aspx") + "'>" + Resources.HercResource.CLSH + "</a>");
        }
        p_var.sbuilder.Append("</li>");
        p_var.sbuilder.Append("<li>");
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
        {
            p_var.sbuilder.Append("<a  title='" + Resources.HercResource.PublicNotice.Replace("&", "and") + "' href='" + ResolveUrl("~/PublicNoticeDetails.aspx") + "'>" + Resources.HercResource.PublicNotice + "</a>");
        }
        else
        {
            p_var.sbuilder.Append("<a  title='" + Resources.HercResource.PublicNotice.Replace("&", "and") + "'  href='" + ResolveUrl("~/content/Hindi/PublicNoticeDetails.aspx") + "'>" + Resources.HercResource.PublicNotice + "</a>");
        }
        p_var.sbuilder.Append("</li>");

    }

    //End
}

