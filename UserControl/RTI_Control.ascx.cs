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

public partial class UserControl_RTI_Control : System.Web.UI.UserControl

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
               
                bindLeftSideMenu();
                
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
                    else if (lnkObject.linkID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_HercHome_Hindi))
                    {
                        _lnkSubmenuObject.LinkParentId = Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_HercHome_Hindi);
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

                /**********  Gaurav  *****/

                if (Convert.ToInt16(p_var.PageID) == p_var.menuid)
                {
                    p_var.sbuilder.Append("<li>");

                    if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Footer_Feedback || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Footer_Feedback_Hindi)
                    {

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a class=\"current\" href='" + ResolveUrl("~/") + "Feedback/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&","&amp;")) + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "Feedback/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                            
                        }
                    }
                   
                    else
                    {
                        if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.PhotoGallery)
                        {
                            p_var.sbuilder.Append("<a class=\"current\" href='" + ResolveUrl("~/") + "CategoryGallery/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                        }
                        else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap)
                        {
                            p_var.sbuilder.Append("<a  class=\"current\" href='" + ResolveUrl("~/") + "Archive.aspx?menuid=" + p_var.menuid + "&position=" + p_var.position + "'>" + p_var.menu_name + "</a>");
                           

                        }
                        else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap_Hindi)
                        {
                            p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                        }


                        else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Hindi || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer_Hindi)
                        {
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a  class=\"current\" href='" + ResolveUrl("~/") + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a  class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "Archive/" + p_var.menuid + "_" + p_var.position + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                            }
                        }
                        else
                        {
                            if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Herc) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Herc_Hindi))
                            {

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {

                                    p_var.sbuilder.Append("<a  class=\"current\" href='" + ResolveUrl("~/") + "CurrentRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                                }
                                else
                                {


                                    p_var.sbuilder.Append("<a  class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "CurrentRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                                }
                            }

                            else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Herc_Prev_RTI) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Herc_Prev_RTI_Hindi))
                            {

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a  class=\"current\" href='" + ResolveUrl("~/") + "PrevRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a  class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "PrevRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                                }
                            }

                            else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Hercsearch_RTI) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Hercsearch_RTI_Hindi))
                            {

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a  class=\"current\" href='" + ResolveUrl("~/") + "RTIsearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a  class=\"current\" href=" + ResolveUrl("~/") + p_var.url + "RTIsearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                                }
                            }
                            else
                            {
                              //  p_var.sbuilder.Append("<a  class=\"current\" href='" + ResolveUrl("~/ContentPage.aspx?menuid=") + p_var.menuid + "&position=" + Convert.ToInt16(p_var.position) + "'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "</a>");
							  if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a  class=\"current\" href='" + ResolveUrl("~/Content/") + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_RTI.aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&","&amp;")) + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a  class=\"current\" href='" + ResolveUrl("~/Content/Hindi/") + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_RTI.aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                                }
                            }
                          
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
                            p_var.sbuilder.Append("<a  target='_blank' href=" + subMenuData.Tables[0].Rows[i]["url"].ToString() + ">" +HttpUtility.HtmlDecode( p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");

                            if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Footer_Home)
                            {
                                p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "index.aspx'>" +HttpUtility.HtmlDecode( p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                            }
                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Home_Footer_Hindi)
                            {
                                p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + p_var.url + "index.aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                            }
                            else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Herc) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Herc_Hindi))
                            {

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {

                                    p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "CurrentRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                                }
                                else
                                {

                                    p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + p_var.url + "CurrentRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                                }
                            }
                            else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Herc_Prev_RTI) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Herc_Prev_RTI_Hindi))
                            {

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "PrevRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + "PrevRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                                }
                            }
                            else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Hercsearch_RTI) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Hercsearch_RTI_Hindi))
                            {

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "searchRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + p_var.url + "searchRTI/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                                }
                            }
                            else
                            {

                                if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap)
                                {

                                    p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                                  
                                }
                                else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Sitemap_Hindi)
                                {
                                    p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + p_var.url + "Sitemap/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

                                }
                                else
                                {
                                    if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.stackHolder) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.stackHolder_Hindi))
                                    {
                                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                        {
                                            p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + "StakeHolder/" + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                                            

                                        }
                                        else
                                        {
                                            p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + "StakeHolder/" + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                                           

                                        }
                                    }
                                    else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Grievance) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Grievance_Hindi))
                                    {
                                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                        {
                                            p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + "Grievance/" + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                                           

                                        }
                                        else
                                        {
                                            p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + "Grievance/" + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

             
                                        }
                                    }
                                       

                                    else
                                    {
                                        p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");

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
                            p_var.sbuilder.Append("<a  target='_blank' href=" + subMenuData.Tables[0].Rows[i]["url"].ToString() + ">" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "</a>");
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
           // throw;
        }



    }
}
    

    #endregion

    
