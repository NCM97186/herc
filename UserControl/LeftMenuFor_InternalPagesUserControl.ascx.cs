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

public partial class UserControl_LeftMenuFor_InternalPagesUserControl : System.Web.UI.UserControl
{

    #region Data declaration zone

    Project_Variables p_var = new Project_Variables();
    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    LinkOB lnkObject = new LinkOB();
    public string headername = string.Empty;

    #endregion

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
       p_var.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
       // p_var.position = Convert.ToInt16(Request.QueryString["position"]);
        p_var.LanguageID = Convert.ToInt16(Resources.HercResource.Lang_Id);
        p_var.url = Resources.HercResource.PageUrl.ToString();
        if (!IsPostBack)
        {
           

            if (RewriteModule.RewriteContext.Current.Params["menuid"] != null)
            {
                p_var.PageID = RewriteModule.RewriteContext.Current.Params["menuid"];// Request.QueryString["menuid"].ToString();
                if (p_var.PageID.Length > 6)
                {
                    p_var.PageID = p_var.PageID.Substring(6);
                }
                else
                {
                    p_var.PageID = p_var.PageID.ToString();
                }

                if (Convert.ToInt16(p_var.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.licensees) || Convert.ToInt16(p_var.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.licensees_Hindi) || Convert.ToInt16(p_var.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.StateAdvisoryCommittee) || Convert.ToInt16(p_var.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.StateAdvisoryCommittee_Hindi) || Convert.ToInt16(p_var.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.CoordinationForum) || Convert.ToInt16(p_var.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.CoordinationForum_Hindi) || Convert.ToInt16(p_var.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.parliament_Assembly_Questions) || Convert.ToInt16(p_var.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.parliament_Assembly_Questions_Hindi) || Convert.ToInt16(p_var.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Abbreviations) || Convert.ToInt16(p_var.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Abbreviations_Hindi))
                {
                    bindLeftSideMenu_Single();
                }
                else
                {
                    bindLeftSideMenu();
                }
               
            }
        }
    }

    #endregion

    #region Function to bind Left side menu

    public void bindLeftSideMenu()
    {
        LinkOB _lnkSubmenuObject = new LinkOB();
        try
        {

            lnkObject.linkID = Convert.ToInt16(p_var.PageID);
            lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            p_var.dSet = menuBL.get_Cliked_Parent_Menu(lnkObject);

            if (p_var.dSet.Tables[0].Rows.Count > 0)
            {
                headername = p_var.dSet.Tables[0].Rows[0]["name"].ToString();
            }
            //Code to check level and parentID
            Menu_ManagementBL _subMenuBL = new Menu_ManagementBL();
            DataSet subMenuData = new DataSet();
            LinkOB _lnkOB = new LinkOB();

            if (Convert.ToInt16(p_var.dSet.Tables[0].Rows[0]["link_parent_id"]) == 0)
            {
                if (Convert.ToInt16(p_var.position) == 1)
                {
                    _lnkSubmenuObject.LinkParentId = lnkObject.linkID;
                }
                else
                {
                    if (lnkObject.linkID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_HercHome))
                    {
                        _lnkSubmenuObject.LinkParentId = Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_HercHome);
                    }
                    else if (lnkObject.linkID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.WebsitePolicy) || lnkObject.linkID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.WebsitePolicy_Hindi))
                    {
                        _lnkSubmenuObject.LinkParentId = Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.WebsitePolicy);
                    }
                    else
                    {
                        _lnkSubmenuObject.LinkParentId = Convert.ToInt16(p_var.dSet.Tables[0].Rows[0]["link_parent_id"]);
                    }
                    
                }
            }

            else
            {
                _lnkSubmenuObject.LinkParentId = Convert.ToInt16(p_var.dSet.Tables[0].Rows[0]["link_parent_id"]);
            }

           
                _lnkSubmenuObject.LangId = p_var.LanguageID;
                _lnkSubmenuObject.PositionId = Convert.ToInt16(p_var.position);
                subMenuData = menuBL.get_Cliked_Parent_Child_Menu(_lnkSubmenuObject);

                p_var.sbuilder.Append("<div class=\"left-nav-btn\">");
                p_var.sbuilder.Append("<ul>");
                for (int i = 0; i < subMenuData.Tables[0].Rows.Count; i++)
                {
                    p_var.menu_name = subMenuData.Tables[0].Rows[i]["name"].ToString();
                    headername = subMenuData.Tables[0].Rows[i]["ParentName"].ToString();
                    p_var.menuid = Convert.ToInt16(subMenuData.Tables[0].Rows[i]["link_id"]);
                    p_var.urlname = subMenuData.Tables[0].Rows[i]["PlaceHolderOne"].ToString();


                    if (Convert.ToInt16(p_var.PageID) == p_var.menuid)
                    {
                        p_var.sbuilder.Append("<li>");

                        if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Footer_Feedback || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Footer_Feedback_Hindi)
                        {

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + "Feedback/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "Feedback/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                               

                            }
                        }
                        else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Notification || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Notification_Hindi)
                        {
                            bind_Notification();
                        }
                        else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_Eng || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_hindi)
                        {
                            p_var.sbuilder.Append("<li>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                        }
                        else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Hercsearch_RTI) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Hercsearch_RTI_Hindi))
                        {

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + "searchRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "searchRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                        }

                        else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.profile) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.profile_Hindi))
                        {

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + "Profile/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "Profile/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                        }

                        else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA_Hindi))
                        {

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + "FSA/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "FSA/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                        }

                        else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.GeneralMiscCharges) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.GeneralMiscCharges_Hindi))
                        {

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + "GeneralMiscCharges/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "GeneralMiscCharges/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                        }

                        else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.LocateUs) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.LocateUs_Hindi))
                        {

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + "Locate/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "Locate/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                        }

                        else
                        {


                            if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap_Hindi)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }


                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Hindi || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer_Hindi)
                            {
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a  title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                                }
                            }


                            else
                            {


                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");


                            }
                        }

                    }
                    else
                    {
                        if (subMenuData.Tables[0].Rows[i]["PlaceHolderOne"] != DBNull.Value)
                        {
                            if (Convert.ToInt16(subMenuData.Tables[0].Rows[i]["link_type_id"]) == Convert.ToInt16(Module_ID_Enum.link_type_id.link))
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' onclick=\"javascript:return confirm( 'This link shall take you to a webpage outside this website. Click OK to continue. Click Cancel to stop.');\" target='_blank' href=" + subMenuData.Tables[0].Rows[i]["url"].ToString() + ">" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                            else if (p_var.dSet.Tables[0].Rows[0]["counter_Child"] != DBNull.Value)
                            {

                                p_var.sbuilder.Append("<li class=\"line-h\">");
                                
                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {

                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"font-bold\"  href=''>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
                                }


                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' class=\"font-bold\"  href=''>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");

                                }

                            }

                            else
                            {
                                p_var.sbuilder.Append("<li>");

                                if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Footer_Home)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "index.aspx'>" + p_var.menu_name + "</a>");
                                }
                                else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Home_Footer_Hindi)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "index.aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {

                                    if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.OmbudsmanSitemap)
                                    {
                                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                                    }
                                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap_Hindi||p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.OmbudsmanSitemap_Hindi )
                                    {
                                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                    }

                                    else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Herc_Prev_RTI) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Herc_Prev_RTI_Hindi))
                                    {
                                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                        {
                                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "PrevRTI/" + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                        }
                                        else
                                        {
                                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "PrevRTI/" + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                                        }
                                    }

                                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_Eng || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.vacancy_hindi)
                                    {
                                        
                                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                        {
                                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                                        }
                                        else
                                        {
                                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Vacancies/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                                        }
                                    }

                                    else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Hercsearch_RTI) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Hercsearch_RTI_Hindi))
                                    {

                                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                        {
                                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "searchRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                        }
                                        else
                                        {
                                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "searchRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                        }
                                    }

                                    else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.profile) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.profile_Hindi))
                                    {

                                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                        {
                                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Profile/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                        }
                                        else
                                        {
                                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Profile/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                        }
                                    }
                                   
                                    else
                                    {
                                        if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.stackHolder) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.stackHolder_Hindi))
                                        {
                                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "StakeHolder/" + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                                               
                                            }
                                            else
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "StakeHolder/" + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                                              
                                            }
                                        }
                                        else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Herc)|| p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Herc_Hindi))
                                        {

                                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "CurrentRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                            }
                                            else
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "CurrentRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                            }
                                        }
                                        else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Grievance) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Grievance_Hindi))
                                        {
                                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Grievance/" + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                                              
                                            }
                                            else
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Grievance/" + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                               
                                            }
                                        }
                                        else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA_Hindi))
                                        {

                                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + "FSA/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                            }
                                            else
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "FSA/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                            }
                                        }

                                        else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.GeneralMiscCharges) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.GeneralMiscCharges_Hindi))
                                        {

                                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "GeneralMiscCharges/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                            }
                                            else
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "GeneralMiscCharges/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                            }
                                        }
                                        else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.LocateUs) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.LocateUs_Hindi))
                                        {

                                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Locate/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                            }
                                            else
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Locate/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                                            }
                                        }

                                        else
                                        {
                                            if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_HercHome))
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "RTI/" + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                                            }
                                            else
                                            {
                                                //New code added on date 06-12-2012

                                                DataSet ds = new DataSet();
                                                _lnkSubmenuObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
                                                _lnkSubmenuObject.PositionId = Convert.ToInt16(p_var.position);
                                                _lnkSubmenuObject.LinkParentId = p_var.menuid;
                                                ds = menuBL.get_Cliked_Parent_Child_Menu(_lnkSubmenuObject);
                                                if (ds.Tables[0].Rows.Count > 0)
                                                {
                                                    if (Convert.ToInt16(ds.Tables[0].Rows[0]["link_id"]) == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff || Convert.ToInt16(ds.Tables[0].Rows[0]["link_id"]) == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff_Hindi)
                                                    {
                                                        if (Convert.ToInt16(ds.Tables[0].Rows[0]["link_id"]) == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff || Convert.ToInt16(ds.Tables[0].Rows[0]["link_id"]) == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff_Hindi)
                                                        {

                                                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                                            {
                                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[0]["parentName"].ToString().Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + "GenerationTariff/" + ds.Tables[0].Rows[0]["link_id"] + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[0]["parentName"].ToString()).Replace("&", "&amp;") + "</a>");

                                                            }
                                                            else
                                                            {
                                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[0]["parentName"].ToString().Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "GenerationTariff/" + ds.Tables[0].Rows[0]["link_id"] + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[0]["parentName"].ToString()).Replace("&", "&amp;") + "</a>");
                                                               
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {

                                                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[0]["link_id"] + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                           
                                                    }
                                                    
                                                }
                                                else
                                                {
                                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                                                }

                                               

                                            }

                                            
                                        }
                                    }

                                }

                            }

                        }

                        else
                        { 
                            if (Convert.ToInt16(p_var.dSet.Tables[0].Rows[i]["linktypeid"]) == Convert.ToInt16(Module_ID_Enum.link_type_id.link))
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' onclick=\"javascript:return confirm( 'This link shall take you to a webpage outside this website. Click OK to continue. Click Cancel to stop.');\" target='_blank' href=" + subMenuData.Tables[0].Rows[i]["url"].ToString() + ">" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<li>");
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                        }
                    }


                    p_var.sbuilder.Append("</li>");
                }
                p_var.sbuilder.Append("</ul>");
                p_var.sbuilder.Append("</div>");
                ltlrSubMenu.Text = p_var.sbuilder.ToString();
               
            }
        

        catch
        {
            //throw;
        }
        
       
        
    }

    #endregion

    #region Function to bind Left side menu

    public void bindLeftSideMenu_Single()
    {
        LinkOB _lnkSubmenuObject = new LinkOB();
        try
        {

            
        }


        catch
        {
            //throw;
        }



    }

    #endregion

    public  void bind_Notification()
    {
        p_var.sbuilder.Length = 0;
        Menu_ManagementBL subMenuBL = new Menu_ManagementBL();
        LinkOB objnew = new LinkOB();
        objnew.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {
            objnew.LinkParentId = 773;
        }
        else
        {
            objnew.LinkParentId = 775;
        }
        DataSet ds1 = subMenuBL.get_Frontside_Submenu_of_RootMenu(objnew);
        if(ds1.Tables[0].Rows.Count>0)
        {
           string urlname1 = ds1.Tables[0].Rows[0]["placeholderone"].ToString();
        }
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {
            p_var.sbuilder.Append("<div class=\"left-nav-btn\">");
            p_var.sbuilder.Append(" <ul>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a href='" + ResolveUrl("~/Notifications/1.aspx") + "' title='" + HttpUtility.HtmlDecode(Resources.HercResource.herc) + "'>" + Resources.HercResource.herc + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            string urlname1 = ds1.Tables[0].Rows[0]["placeholderone"].ToString();
            if (Convert.ToInt16(ds1.Tables[0].Rows[0]["link_id"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Notification))
                {
                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(ds1.Tables[0].Rows[0]["name"].ToString()) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + ds1.Tables[0].Rows[0]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname1.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(ds1.Tables[0].Rows[0]["name"].ToString()).Replace("&","&amp;") + "</a>");
                }
            
            //p_var.sbuilder.Append("<a href=\"Notifications/4.aspx\" title=\"Others\" class=\"margin\">" + Resources.HercResource.Others + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a href='"+ResolveUrl("~/Notifications/8.aspx")+"' title='" + HttpUtility.HtmlDecode(Resources.HercResource.RepealedNotifications)+ "'>" + Resources.HercResource.RepealedNotifications + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append(" </ul>");
            p_var.sbuilder.Append("</div>");
        }
        else
        {
            p_var.sbuilder.Append("<div class=\"left-nav-btn\">");
          
            p_var.sbuilder.Append(" <ul>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a href='"+ResolveUrl("~/content/Hindi/Notifications/1.aspx")+"' title='" + HttpUtility.HtmlDecode(Resources.HercResource.herc)+ "'>" + Resources.HercResource.herc + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");

            string urlname1 = ds1.Tables[0].Rows[0]["placeholderone"].ToString();
            if (Convert.ToInt16(ds1.Tables[0].Rows[0]["link_id"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Notification_Hindi))
            {
                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(ds1.Tables[0].Rows[0]["name"].ToString()) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + ds1.Tables[0].Rows[0]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname1.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(ds1.Tables[0].Rows[0]["name"].ToString()).Replace("&", "&amp;") + "</a>");
            }
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append("<li>");
            p_var.sbuilder.Append("<a href='"+ResolveUrl("~/content/Hindi/Notifications/8.aspx")+"' title='" + HttpUtility.HtmlDecode(Resources.HercResource.RepealedNotifications)+ "' >" + Resources.HercResource.RepealedNotifications + "</a>");
            p_var.sbuilder.Append("</li>");
            p_var.sbuilder.Append(" </ul>");
            p_var.sbuilder.Append("</div>");
        }

    }
}


