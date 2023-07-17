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

public partial class usercontrol_OmbudsmanLeftMenuFor_InternalPagesUserControl : System.Web.UI.UserControl
{

    #region Data declaration zone

    Project_Variables p_var = new Project_Variables();
    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    LinkOB lnkObject = new LinkOB();
    public string headername = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        p_var.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        // p_var.position = Convert.ToInt16(Request.QueryString["position"]);
        p_var.LanguageID = Convert.ToInt16(Resources.HercResource.Lang_Id);
        p_var.url = Resources.HercResource.OmbudsmanPageUrl.ToString();
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
                    _lnkSubmenuObject.LinkParentId = Convert.ToInt16(p_var.dSet.Tables[0].Rows[0]["link_parent_id"]);
                }
            }
            else
            {
                _lnkSubmenuObject.LinkParentId = Convert.ToInt16(p_var.dSet.Tables[0].Rows[0]["link_parent_id"]);
            }
           

            _lnkSubmenuObject.LangId = p_var.LanguageID;
            _lnkSubmenuObject.PositionId = Convert.ToInt16(p_var.position);
            subMenuData = menuBL.get_Cliked_ParentChild_Menuombudsman(_lnkSubmenuObject);

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

                    if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_Hindi)
                        {
                            //p_var.sbuilder.Append("<li>");

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "appeal/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "appeal/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                            }
                        }
                      else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_prevYear || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_prevYear_Hindi)
                      {
                        //p_var.sbuilder.Append("<li>");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a  class=\"current\" href='" + ResolveUrl("~/") + "AppealPreYear/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' class=\"current\"  href='" + ResolveUrl("~/") + p_var.url + "AppealPreYear/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                    }
                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AppealSearch || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AppealSearch_Hindi)
                    {
                        //p_var.sbuilder.Append("<li>");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "Appealsearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "Appealsearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                    }
                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AppealOnlineStatus || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AppealOnlineStatus_Hindi)
                    {
                        //p_var.sbuilder.Append("<li>");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "AppealStatus/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\"  href='" + ResolveUrl("~/") + p_var.url + "AppealStatus/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                    }
                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedCurrent || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedCurrent_Hindi)
                    {
                        //p_var.sbuilder.Append("<li>");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "AwardsCurrent/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "AwardsCurrent/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                    }
                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedPrevious || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedPrevious_Hindi)
                    {
                        //p_var.sbuilder.Append("<li>");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "AwardsPrevious/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "AwardsPrevious/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                    }

                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedSearch || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedSearch_Hindi)
                    {
                        //p_var.sbuilder.Append("<li>");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "AwardsSearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "AwardsSearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                    }

                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsUnderAppeal || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsUnderAppeal_Hindi)
                    {
                        //p_var.sbuilder.Append("<li>");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "AwardsUnderAppeal/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "AwardsUnderAppeal/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                    }

                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.RTI || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_Hindi)
                    {
                        //p_var.sbuilder.Append("<li>");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "RTICurrent/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\"  href='" + ResolveUrl("~/") + p_var.url + "RTICurrent/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                    }
                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanPreviousYear || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanPreviousYear_Hindi)
                    {
                        //p_var.sbuilder.Append("<li>");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "perviousrti/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\"  href='" + ResolveUrl("~/") + p_var.url + "perviousrti/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                    }

                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanSearch || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanSearch_Hindi)
                    {
                        //p_var.sbuilder.Append("<li>");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "RTISearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\"  href='" + ResolveUrl("~/") + p_var.url + "RTISearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                    }
                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsmancalender || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsmancalender_Hindi)
                    {
                        //p_var.sbuilder.Append("<li>");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "CommissionCalendar/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\"  href='" + ResolveUrl("~/") + p_var.url + "CommissionCalendar/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                    }
                    else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Reports || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Reports_Hindi)
                    {
                        //p_var.sbuilder.Append("<li>");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "Reports/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "Reports/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                        }
                    }
                    else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA_ombudsman))
                    {

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "FSAOmbudsman/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "FSAOmbudsman/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");

                        }
                    }
                    else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Ombudsman_Profile) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Ombudsman_ProfileHindi))
                    {

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "ProfileOmbudsman/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "ProfileOmbudsman/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");

                        }
                    }


                    else
                    {

                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");

                    }
                    }             
                else
                {
                    if (subMenuData.Tables[0].Rows[i]["PlaceHolderOne"] != DBNull.Value)
                    {
                        if (Convert.ToInt16(subMenuData.Tables[0].Rows[i]["link_type_id"]) == Convert.ToInt16(Module_ID_Enum.link_type_id.link))
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' onclick=\"javascript:return confirm('This link shall take you to a webpage outside this website. Click OK to continue. Click ancel to stop.');\" target='_blank' href=" + subMenuData.Tables[0].Rows[i]["url"].ToString() + ">" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");


                            if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_Hindi)
                            {
                                //p_var.sbuilder.Append("<li >");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "appeal/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "appeal/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }
                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_prevYear || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_prevYear_Hindi)
                            {
                               // p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AppealPreYear/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AppealPreYear/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }
                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AppealSearch || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AppealSearch_Hindi)
                            {
                               // p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "Appealsearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "Appealsearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }
                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AppealOnlineStatus || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AppealOnlineStatus_Hindi)
                            {
                               // p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AppealStatus/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a  title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AppealStatus/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }
                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedCurrent || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedCurrent_Hindi)
                            {
                                //p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AwardsCurrent/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "'  href='" + ResolveUrl("~/") + p_var.url + "AwardsCurrent/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }
                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedPrevious || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedPrevious_Hindi)
                            {
                                //p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AwardsPrevious/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "'  href='" + ResolveUrl("~/") + p_var.url + "AwardsPrevious/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }

                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedSearch || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedSearch_Hindi)
                            {
                                //p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AwardsSearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AwardsSearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }
                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsUnderAppeal || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsUnderAppeal_Hindi)
                            {
                                //p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AwardsUnderAppeal/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AwardsUnderAppeal/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }
                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.RTI || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_Hindi)
                            {
                                //p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "RTICurrent/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a  title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "RTICurrent/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }
                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanPreviousYear || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanPreviousYear_Hindi)
                            {
                                //p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "perviousrti/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "perviousrti/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }
                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanSearch || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanSearch_Hindi)
                            {
                                //p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "RTISearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "RTISearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }

                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsmancalender || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsmancalender_Hindi)
                            {
                                //p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "CommissionCalendar/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "CommissionCalendar/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }

                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Reports || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Reports_Hindi)
                            {
                                //p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "Reports/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "Reports/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }
                            else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA_ombudsman))
                            {

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "FSAOmbudsman/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "FSAOmbudsman/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");

                                }
                            }
                            else if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Ombudsman_Profile) || p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Ombudsman_ProfileHindi))
                            {

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "ProfileOmbudsman/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "ProfileOmbudsman/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");

                                }
                            }


                           else
                            {
                                DataSet ds = new DataSet();
                                _lnkSubmenuObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
                                _lnkSubmenuObject.PositionId = Convert.ToInt16(p_var.position);
                                _lnkSubmenuObject.LinkParentId = p_var.menuid;
                                ds = menuBL.get_Cliked_Parent_ChildOmbudsman_Menu(_lnkSubmenuObject);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToInt16(ds.Tables[0].Rows[0]["link_id"]) == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff_ombudsman )
                                    {
                                        //if (Convert.ToInt16(ds.Tables[0].Rows[0]["link_id"]) == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff || Convert.ToInt16(ds.Tables[0].Rows[0]["link_id"]) == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff_Hindi)
                                        //{

                                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[0]["parentName"].ToString()) + "' href='" + ResolveUrl("~/") + "GenerationTariffOmbudsman/" + ds.Tables[0].Rows[0]["link_id"] + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + ds.Tables[0].Rows[0]["parentName"] + "</a>");

                                            }
                                            else
                                            {
                                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[0]["parentName"].ToString()) + "' href='" + ResolveUrl("~/") + p_var.url + "GenerationTariffOmbudsman/" + ds.Tables[0].Rows[0]["link_id"] + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + ds.Tables[0].Rows[0]["parentName"] + "</a>");
                                                p_var.sbuilder.Append("</li>");

                                            }
                                        //}
                                    }

                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }

                            }
                      }
          
                    }
                    else
                    {
                        if (Convert.ToInt16(p_var.dSet.Tables[0].Rows[i]["linktypeid"]) == Convert.ToInt16(Module_ID_Enum.link_type_id.link))
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' target='_blank' href=" + subMenuData.Tables[0].Rows[i]["url"].ToString() + ">" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
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
}
