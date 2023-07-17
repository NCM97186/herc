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

public partial class Ombudsman_OmbudsmanSiteMap : BasePageOmbudsman
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
    public string headername = string.Empty;
    DataSet subMenuData = new DataSet();
    #endregion

    //End

    protected void Page_Load(object sender, EventArgs e)
    {
        p_var.langid = Convert.ToString(Resources.HercResource.Lang_Id);
        p_var.galleryID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        p_var.str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.Sitemap);
        ltrlBreadcrum.Text = p_var.str;
        p_var.url = Resources.HercResource.PageUrl.ToString();
        p_var.urlname = p_var.url = Resources.HercResource.OmbudsmanPageUrl.ToString();
        if (!IsPostBack)
        {
            BindRootMenu_Category();
            bindLeftSideMenu();
            BindFooterMenu();
            //displaySitemap();
          displayUpdatedDate();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssRef);
        }


        PageTitle = Resources.HercResource.Sitemap;
       
    }


    #region Function to display Updated date for sitemap

    public void displayUpdatedDate()
    {
        lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        lastUpdatedDate = menuBL.getLastUpdatedDateSiteMap(lnkObject);

    }

    #endregion
    #region Function to get Main Footer menu

    public void BindFooterMenu()
    {
        try
        {
            lnkObject.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            lnkObject.LangId = Convert.ToInt16(p_var.langid);
            lnkObject.PositionId = (int)Module_ID_Enum.Menu_Position.footer;
            lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.Approved;
            p_var.position = (int)lnkObject.PositionId;
            lnkObject.ModuleId = (int)Module_ID_Enum.Project_Module_ID.Menu;
            lnkObject.LinkParentId = (int)Module_ID_Enum.Link_parentID_Footer.parent_Footer;
            p_var.dSet = menuBL.get_FooterMenu(lnkObject);
            p_var.sbuilder.Append("<br/><hr><br/><strong class=\"footer-below-had\">" + Resources.HercResource.FooterLinks + "</strong><br/>");
            p_var.sbuilder.Append("<ul>");
            for (int i = 0; i < p_var.dSet.Tables[0].Rows.Count; i++)
            {
				if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                {
                    p_var.url = "content/";
                }
                else
                {
                    p_var.url = "content/Hindi/";
                }
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
                p_var.linkid =Convert.ToInt16( p_var.dSet.Tables[0].Rows[i]["Link_Type_Id"]);

                if (i == 0)
                {
                    if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Footer_Home || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Home_Footer_Hindi)
                    {
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + "index.aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + p_var.url + "index.aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                    }
                    else
                    {
                        if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap_Hindi)
                        {
                            p_var.sbuilder.Append("<li>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + p_var.url + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                        }
                        else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.OmbudsmanSitemap )
                        {
                            p_var.sbuilder.Append("<li>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + "OmbudsmanSiteMap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + p_var.urlname + "OmbudsmanSiteMap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                        }
                        else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer_Hindi)
                        {
                            p_var.sbuilder.Append("<li>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + p_var.url + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                        }
						  else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_Eng || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"first\">");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                    }
                        else
                        {
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                        }
                    }

                }


                else
                {
                    if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap_Hindi)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + p_var.url + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                    }

                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.OmbudsmanSitemap )
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + "OmbudsmanSiteMap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + p_var.urlname + "OmbudsmanSiteMap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                    }
                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer_Hindi)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + p_var.url + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                    }
					  else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_Eng || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"first\">");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                    }
                    else if (p_var.menu_name == "Disclaimer" || p_var.menu_name == "डिस्क्लेमर")
                    {
                        p_var.sbuilder.Append("<li >");
                        p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
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
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href=" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[0]["link_id"] + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + p_var.menu_name + "</a>");
                            }

                            else
                            {
                                p_var.sbuilder.Append("<li>");
								 if (p_var.linkid != null && p_var.linkid == Convert.ToInt16(Module_ID_Enum.link_type_id.link))
                                {
                                    p_var.sbuilder.Append("<a target='_blank'  title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' onclick=\"javascript:return confirm( 'यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।.');\" href='" + p_var.str + "'>" + HttpUtility.HtmlDecode(p_var.urlname).Replace("&", "&amp;") + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                                }
                                //p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");

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
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href=" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[0]["link_id"] + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + p_var.menu_name + "</a>");
                            }

                            else
                            {
                                p_var.sbuilder.Append("<li>");
								 if (p_var.linkid != null && p_var.linkid==Convert.ToInt16(Module_ID_Enum.link_type_id.link))
                                {
                                    p_var.sbuilder.Append("<a target='_blank' title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' onclick=\"javascript:return confirm( 'This link shall take you to a webpage outside this website. Click OK to continue. Click Cancel to stop.');\" href='" + p_var.str +"'>" + HttpUtility.HtmlDecode(p_var.urlname).Replace("&", "&amp;") + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                                }
                               // p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");

                            }

                        }
                      
                    }
                }
                p_var.sbuilder.Append("</li>");
            }
            // Bind_HomePage_leftmenu();
            p_var.sbuilder.Append("</ul>");
            ltrlMenu.Text = p_var.sbuilder.ToString();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Main Root menu

    public void BindRootMenu_Category()
    {
        try
        {
            lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            lnkObject.PositionId = 1;
            p_var.position = (int)lnkObject.PositionId;
            lnkObject.ModuleId = 1;
            lnkObject.LinkParentId = 0;


            p_var.dSet = menuBL.get_Frontside_RootMenu_Ombudsman(lnkObject);


            p_var.sbuilder.Append("<strong class=\"footer-below-had\">" + Resources.HercResource.Toplinks + "</strong><br/><br/>");
            p_var.sbuilder.Append("<ul>");
            for (p_var.i = 0; p_var.i < p_var.dSet.Tables[0].Rows.Count; p_var.i++)
            {
                p_var.menu_name = p_var.dSet.Tables[0].Rows[p_var.i]["name"].ToString();
                p_var.urlname = p_var.dSet.Tables[0].Rows[p_var.i]["placeholderone"].ToString();
                p_var.menuid = Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["link_id"]);

                if (p_var.menuid == (int)Module_ID_Enum.project_MenuID_FrontEnd.Home_ombudsman || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.HomeHindi_Ombudsman || p_var.menu_name == "Home" || p_var.menu_name == "home") //For Home page and Gallery.
                {

                    p_var.sbuilder.Append("<li >");

                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                    {
                        if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.project_MenuID_FrontEnd.Home_ombudsman)) //Only for home
                        {

                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' class=\"font-bold\"    href='" + ResolveUrl("~/") + "index.aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");

                        }
                    }
                    else
                    {

                        if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.HomeHindi_Ombudsman)) //Only for home Hindi
                        {
                            p_var.sbuilder.Append("<a  title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' class=\"font-bold\" href='" + ResolveUrl("~/content/Hindi/") + "index.aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");

                        }
                    }

                  p_var.sbuilder.Append("</li>");
                }
                else
                {
                    if (p_var.dSet.Tables[0].Rows[p_var.i]["counter_Child"] != DBNull.Value)
                    {

                        p_var.sbuilder.Append("<li class=\"line-h\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {

                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' class=\"font-bold\"  href='#'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' class=\"font-bold\"  href='#'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");

                        }

                    }
                    else
                    {
                        p_var.sbuilder.Append("<li class=\"line-h\">");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' class=\"font-bold\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + Server.HtmlDecode(p_var.menu_name) + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(p_var.menu_name)+"' class=\"font-bold\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + Server.HtmlDecode(p_var.menu_name) + "</a>");

                        }
                        //p_var.sbuilder.Append("</li>");
                    }


                }

                //function to bind child of parent menu

                BindMenuChildren(1, Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["link_id"]));

                //end

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
                        p_var.sbuilder.Append("<li class=\"current\">");

                        p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='#'>" + HttpUtility.HtmlDecode(subMenuName) + "<span class=\"sf-sub-indicator\"> »</span></a>");

                        // p_var.sbuilder.Append("<a href='" + ResolveUrl("~/ContentPage.aspx?menuid=") + subMenuID + "&position=" + Convert.ToInt16(p_var.position) + "'>" + HttpUtility.HtmlDecode(subMenuName) + "</a>");

                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "appeal/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "appeal/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_prevYear || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_prevYear_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "AppealPreYear/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "AppealPreYear/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "Appealsearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "Appealsearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealOnlineStatus || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealOnlineStatus_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + "AppealStatus/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + p_var.url + "AppealStatus/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedCurrent || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedCurrent_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + "AwardsCurrent/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + p_var.url + "AwardsCurrent/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedPrevious || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedPrevious_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + "AwardsPrevious/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + p_var.url + "AwardsPrevious/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "AwardsSearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + p_var.url + "AwardsSearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsUnderAppeal || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsUnderAppeal_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + "AwardsUnderAppeal/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "AwardsUnderAppeal/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + "RTICurrent/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "RTICurrent/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }
                    //Previous year RTI

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanPreviousYear || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanPreviousYear_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "perviousrti/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "perviousrti/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }

                    //End


                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "RTISearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "RTISearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.DistributionCharges_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + "DistributionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + p_var.url + "DistributionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.TransmissionCharges_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + "TransmissionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + p_var.url + "TransmissionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + "GenerationTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + p_var.url + "GenerationTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.WheelingCharges_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + "WheelingTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + p_var.url + "WheelingTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.CrossSubsidy_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + "CrosssubsidyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + p_var.url + "CrosssubsidyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RenewalEnergy_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + "RenewalEnergyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + p_var.url + "RenewalEnergyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.FSA_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + "FSAOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href=" + ResolveUrl("~/") + p_var.url + "FSAOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsman_Profile || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsman_ProfileHindi)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href=" + ResolveUrl("~/") + "ProfileOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + p_var.url + "ProfileOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }
                    else
                    {
                        if (subMenuDataSet.Tables[0].Rows[i]["LinkTypeId"].ToString() == "3")
                        {
                            p_var.sbuilder.Append("<li class=\"current\">");
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName) + "<span class=\"sf-sub-indicator\"> »</span></a>");

                        }

                        else
                        {

                            p_var.sbuilder.Append("<li class=\"current\">");

                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName) + "<span class=\"sf-sub-indicator\"> »</span></a>");


                        }
                        p_var.sbuilder.Append("</li>");
                    }
                }
                else
                {
                    if (parentMenuDataSet.Tables[0].Rows.Count > 0)
                    {

                        p_var.sbuilder.Append("<li class=\"current\">");

                        p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName) + "</a>");



                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + "appeal/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + p_var.url + "appeal/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_prevYear || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_prevYear_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "AppealPreYear/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "AppealPreYear/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "Appealsearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + p_var.url + "Appealsearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealOnlineStatus || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealOnlineStatus_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "AppealStatus/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "AppealStatus/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedCurrent || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedCurrent_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "AwardsCurrent/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "AwardsCurrent/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedPrevious || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedPrevious_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "AwardsPrevious/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "AwardsPrevious/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "AwardsSearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + p_var.url + "AwardsSearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsUnderAppeal || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsUnderAppeal_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + "AwardsUnderAppeal/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "AwardsUnderAppeal/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }

                    //Menu binding for the schedule of hearing

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.SOH_CurrentYear || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.SOH_CurrentYearHindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/Ombudsman/") + "ScheduleOfHearing/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "ScheduleOfHearing/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }

                    //End

                    //Menu binding for the schedule of hearing of previous years

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.SOH_PreviousYear || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.SOH_PreviousYearHindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/Ombudsman/") + "ScheduleOfHearingPreviousYear/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "ScheduleOfHearingPreviousYear/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }


                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsman_SoHSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsman_SoHSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/Ombudsman/") + "OmbudsmanSearchSOH/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "OmbudsmanSearchSOH/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }


                    //End


                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "RTICurrent/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + p_var.url + "RTICurrent/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }

                         //Previous year RTI

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanPreviousYear || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanPreviousYear_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "perviousrti/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + p_var.url + "perviousrti/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }

                    //End
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href='" + ResolveUrl("~/") + "RTISearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + p_var.url + "RTISearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.DistributionCharges_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' class=\"current\" href=" + ResolveUrl("~/") + "DistributionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' class=\"current\" href=" + ResolveUrl("~/") + p_var.url + "DistributionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.TransmissionCharges_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' class=\"current\" href=" + ResolveUrl("~/") + "TransmissionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' class=\"current\" href=" + ResolveUrl("~/") + p_var.url + "TransmissionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' class=\"current\" href=" + ResolveUrl("~/") + "GenerationTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' class=\"current\" href=" + ResolveUrl("~/") + p_var.url + "GenerationTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.WheelingCharges_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' class=\"current\" href=" + ResolveUrl("~/") + "WheelingTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' class=\"current\" href=" + ResolveUrl("~/") + p_var.url + "WheelingTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.CrossSubsidy_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  class=\"current\" href=" + ResolveUrl("~/") + "CrosssubsidyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' class=\"current\" href=" + ResolveUrl("~/") + p_var.url + "CrosssubsidyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RenewalEnergy_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' class=\"current\" href=" + ResolveUrl("~/") + "RenewalEnergyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' class=\"current\" href=" + ResolveUrl("~/") + p_var.url + "RenewalEnergyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.FSA_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' class=\"current\" href=" + ResolveUrl("~/") + "FSAOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' class=\"current\" href=" + ResolveUrl("~/") + p_var.url + "FSAOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsman_Profile || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsman_ProfileHindi)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"' href=" + ResolveUrl("~/") + "ProfileOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href=" + ResolveUrl("~/") + p_var.url + "ProfileOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName + "</a>");
                            p_var.sbuilder.Append("</li>");

                        }
                    }

                    else
                    {
                        if (subMenuDataSet.Tables[0].Rows[i]["Link_Type_Id"].ToString() == "3")
                        {
                            p_var.sbuilder.Append("<li >");
                            // p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName) + "</a>");
                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  onclick=\"javascript:return confirm('This link shall take you to a webpage outside this website. Click OK to continue. Click ancel to stop.');\" target='_blank' href=" + subMenuDataSet.Tables[0].Rows[i]["url"].ToString() + ">" + HttpUtility.HtmlDecode(subMenuName) + "</a>");

                        }

                        else
                        {

                            p_var.sbuilder.Append("<li class=\"current\">");

                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode(subMenuName)+"'  href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName) + "</a>");


                        }
                        p_var.sbuilder.Append("</li>");
                    }
                }

                //recursively called function to bind child of subchild

                BindMenuChildren(level + 1, Convert.ToInt16(subMenuDataSet.Tables[0].Rows[i]["link_id"]));

                //end
            }
            p_var.sbuilder.Append("</ul>");
        }

    }

    #endregion


    public void bindLeftSideMenu()
    {

        try
        {
            DataSet subMenuData = new DataSet();
           
            lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            lnkObject.PositionId = 3;
            lnkObject.ModuleId = 1;
            lnkObject.LinkParentId = 0;

            subMenuData = menuBL.get_Frontside_RootMenu_Ombudsman(lnkObject);
            p_var.sbuilder.Append("<ul>");
            for (int i = 0; i < subMenuData.Tables[0].Rows.Count; i++)
            {
                p_var.menu_name = subMenuData.Tables[0].Rows[i]["name"].ToString();
                headername = subMenuData.Tables[0].Rows[i]["name"].ToString();
                p_var.menuid = Convert.ToInt16(subMenuData.Tables[0].Rows[i]["link_id"]);
                p_var.urlname = subMenuData.Tables[0].Rows[i]["PlaceHolderOne"].ToString();
				p_var.position =Convert.ToInt16(lnkObject.PositionId);
                if (i == 0)
                {
                    p_var.sbuilder.Append("<li class='div-line-none'>");
                }
                else
                {
                    p_var.sbuilder.Append("<li>");
                }

                if (subMenuData.Tables[0].Rows[i]["PlaceHolderOne"] != DBNull.Value)
                {
                    if (Convert.ToInt16(subMenuData.Tables[0].Rows[i]["link_type_id"]) == Convert.ToInt16(Module_ID_Enum.link_type_id.link))
                    {

                        p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode( p_var.menu_name)+"'  target='_blank' href=" + subMenuData.Tables[0].Rows[i]["url"].ToString() + ">" + p_var.menu_name + "</a>");
                    }

                    else
                    {
                        if (Convert.ToInt16(subMenuData.Tables[0].Rows[i]["link_type_id"]) == Convert.ToInt16(Module_ID_Enum.link_type_id.link))
                        {

                            p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode( p_var.menu_name)+"' target='_blank' href=" + subMenuData.Tables[0].Rows[i]["url"].ToString() + ">" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            if (i == 0)
                            {
                                if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Reports || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Reports_Hindi)
                                {
                                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                    {
                                        p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode( p_var.menu_name)+"'  href='" + ResolveUrl("~/") + "Reports/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                    }
                                    else
                                    {
                                        p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode( p_var.menu_name)+"'  href='" + ResolveUrl("~/") + p_var.url + "Reports/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                    }
                                    //p_var.sbuilder.Append("<a href=" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + p_var.menu_name + "</a>");
                                }
                            }
                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsmancalender || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsmancalender_Hindi)
                            {
                                //p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode( p_var.menu_name)+"' href='" + ResolveUrl("~/") + "CommissionCalendar/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode( p_var.menu_name)+"'   href='" + ResolveUrl("~/") + p_var.url + "CommissionCalendar/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }


                            else
                            {

                                if (p_var.menuid != Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.OmbudsmanWhatsNew) && p_var.menuid != Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.OmbudsmanWhatsNewHindi))
                                {
                                    p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode( p_var.menu_name.Replace("'", ""))+"' href=" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.OmbudsmanWhatsNew))
                                    {
                                        p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode( p_var.menu_name.Replace("'", ""))+"'  href=" + ResolveUrl("~/OmbudsmanWhatsNew.aspx") + " >" + p_var.menu_name + "</a>");
                                    }
                                    else
                                    {
                                        p_var.sbuilder.Append("<a title='"+HttpUtility.HtmlDecode( p_var.menu_name.Replace("'", ""))+"'  href=" + ResolveUrl("~/OmbudsmanContent/Hindi/OmbudsmanWhatsNew.aspx") + " >" + p_var.menu_name + "</a>");
                                    }
                                }
                                //   Response.Redirect(ResolveUrl("~/Ombudsman/OmbudsmanWhatsNew.aspx"));
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



    protected void site_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
        {
            Response.Redirect(ResolveUrl("~/Sitemap/136_2_SiteMap.aspx"));
        }
        else
        {
            Response.Redirect(ResolveUrl("~/content/Hindi/Sitemap/231_2_Sitemap.aspx"));
        }
    }



    
}