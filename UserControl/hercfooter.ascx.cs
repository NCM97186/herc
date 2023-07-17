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

public partial class UserControl_hercfooter : System.Web.UI.UserControl
{
    #region Data declaration zone

    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    Project_Variables p_var = new Project_Variables();
    LinkOB lnkObject = new LinkOB();
    BreadcrumDL brdl = new BreadcrumDL();
    #endregion

    #region page load event zone
    static string strlatestdate;

    protected void Page_Load(object sender, EventArgs e)
    {
    
      p_var.url = Resources.HercResource.PageUrl.ToString();
     // p_var.FilenameUrl = p_var.url = Resources.HercResource.OmbudsmanPageUrl.ToString();
	  p_var.FilenameUrl = Resources.HercResource.OmbudsmanPageUrl.ToString();
      p_var.langid = Resources.HercResource.Lang_Id;
       if (!IsPostBack)
      {
          BindFooterMenu();
           // lblVisitors.Text =  Convert.ToString((Application["myCount"]));
          
            lblVisitors.Text =Resources.HercResource.Updated  + brdl.Get_Latest_Update();
       }
    }

    #endregion

    #region Function to get Main Root menu

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
                p_var.linkid =Convert.ToInt16( p_var.dSet.Tables[0].Rows[i]["Link_Type_Id"]);
                if (i == 0 || i==6 )
                {
                    if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Footer_Home || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Home_Footer_Hindi)
                    {
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<li class=\"first\">");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + "index.aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li class=\"first\">");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + "index.aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                    }
                    else
                    {
                        if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap_Hindi)
                        {
                            p_var.sbuilder.Append("<li >");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                        }

                        else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.OmbudsmanSitemap)
                        {
                            p_var.sbuilder.Append("<li>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + "OmbudsmanSiteMap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.urlname + "OmbudsmanSiteMap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");

                            }
                            p_var.sbuilder.Append("<br />");
                        }
                       else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_Eng || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_hindi)
                        {
                            p_var.sbuilder.Append("<li >");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                               p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                        }
                        else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer_Hindi)
                        {
                            p_var.sbuilder.Append("<li >");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                        }
                        else
                        {
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<li class=\"first\">");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<li class=\"first\">");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
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
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");

                        }
                        p_var.sbuilder.Append("<br />");
                    }

                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.OmbudsmanSitemap)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + "OmbudsmanSiteMap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.urlname + "OmbudsmanSiteMap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");

                        }
                        p_var.sbuilder.Append("<br />");
                    }
                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_Eng || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_hindi)
                    {
                        p_var.sbuilder.Append("<li >");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                    }
                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer_Hindi)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                        }
                    }
                    else if (p_var.menu_name == "Disclaimer" || p_var.menu_name == "डिस्क्लेमर")
                    {
                        p_var.sbuilder.Append("<li >");
                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
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
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[0]["link_id"] + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
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
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[0]["link_id"] + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                            }

                            else
                            {
                                p_var.sbuilder.Append("<li>");
                                if (p_var.linkid != null && p_var.linkid==Convert.ToInt16(Module_ID_Enum.link_type_id.link))
                                {
                                    p_var.sbuilder.Append("<a target='_blank'  title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' onclick=\"javascript:return confirm( 'यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।.');\" href='" + p_var.str +"'>" + HttpUtility.HtmlDecode(p_var.urlname).Replace("&", "&amp;") + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                                }
                                //p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");

                            }

                        }
                    }
                }
                p_var.sbuilder.Append("</li>");
            }

         ltrlFooter.Text = p_var.sbuilder.ToString();
        }
        catch
        {
            //throw;
        }
    }

    #endregion
}
