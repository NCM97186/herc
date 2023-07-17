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

public partial class UserControl_leftmenu_Homepage : System.Web.UI.UserControl
{
    #region Data declaration zone

    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    Project_Variables p_var = new Project_Variables();
    LinkOB lnkObject = new LinkOB();

    #endregion

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        p_var.url = Resources.HercResource.PageUrl.ToString();
        if (!IsPostBack)
        {

            BindRootMenu_Category();
        }

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

            //for (p_var.i = 0; p_var.i < p_var.dSet.Tables[0].Rows.Count; p_var.i++)
            if (p_var.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_var.i = p_var.dSet.Tables[0].Rows.Count - 1; p_var.i >= 0; p_var.i--)
                {
                    p_var.menu_name = p_var.dSet.Tables[0].Rows[p_var.i]["name"].ToString();
                    p_var.urlname = p_var.dSet.Tables[0].Rows[p_var.i]["placeholderone"].ToString();
                    p_var.menuid = Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["link_id"]);

                    if (p_var.i == 0)
                    {
                        p_var.sbuilder.Append("<li class=\"line-height\">");
                    }
                    else if (p_var.i == p_var.dSet.Tables[0].Rows.Count - 1)
                    {

                        p_var.sbuilder.Append("<li class=\"margin\">");
                    }
                    else
                    {
                        if (p_var.menuid != 773 && p_var.menuid != 775)
                        {
                            p_var.sbuilder.Append("<li>");
                        }
                    }

                    
					if (p_var.menu_name == "APTEL" || p_var.menuid == 861|| p_var.menuid==862)
                    {
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            if (p_var.dSet.Tables[0].Rows[p_var.i]["counter_Child"] != DBNull.Value)
                            {
                                p_var.sbuilder.Append("<a  class=\"directive-icon\"  href='javascript:void(0);'  title='" + Server.HtmlDecode(p_var.menu_name) + "'>" + Server.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a  class=\"directive-icon\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'  title='" + Server.HtmlDecode(p_var.menu_name) + "'>" + Server.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                        }
                        else
                        {
                            if (p_var.dSet.Tables[0].Rows[p_var.i]["counter_Child"] != DBNull.Value)
                            {
                                p_var.sbuilder.Append("<a  class=\"directive-icon\"  href='javascript:void(0);'  title='" + Server.HtmlDecode(p_var.menu_name) + "'>" + Server.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a  class=\"directive-icon\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'  title='" + Server.HtmlDecode(p_var.menu_name) + "'>" + Server.HtmlDecode(p_var.menu_name) + "</a>");
                            }
                        }
                        
                    }
					
                    if (p_var.menu_name == "Parliament / Assembly Question Answer" || p_var.menuid == 213|| p_var.menuid==200)
                    {
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            if (p_var.dSet.Tables[0].Rows[p_var.i]["counter_Child"] != DBNull.Value)
                            {
                                p_var.sbuilder.Append("<a  class=\"praliament-icon\"  href='javascript:void(0);'  title='" + Server.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "'>" + Server.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a  class=\"praliament-icon\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'  title='" + Server.HtmlDecode(p_var.menu_name) + "'>" + p_var.menu_name.Replace("&","&amp;") + "</a>");
                            }
                        }
                        else
                        {
                            if (p_var.dSet.Tables[0].Rows[p_var.i]["counter_Child"] != DBNull.Value)
                            {
                                p_var.sbuilder.Append("<a  class=\"praliament-icon\"  href='javascript:void(0);'  title='" + Server.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "'>" + Server.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a  class=\"praliament-icon\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'  title='" + Server.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "'>" + p_var.menu_name.Replace("&", "&amp;") + "</a>");
                            }
                        }

                    }

                    //function to bind child of parent menu

                    BindMenuChildren(1, Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["link_id"]));

                    //end
                  // p_var.sbuilder.Append("</li>");
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

                            p_var.sbuilder.Append("<a class=\"regulation-icon\" href='javascript:void(0);' title='" + Resources.HercResource.Regulations + "'>" + Resources.HercResource.Regulations + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            
                            p_var.sbuilder.Append("<a href=\"Regulation/1.aspx\" title='" + Resources.HercResource.herc + "'>" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string urlname = ds.Tables[0].Rows[i]["placeholderone"].ToString();
                                if (Convert.ToInt16(ds.Tables[0].Rows[i]["link_id"]) == 192)
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[i]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "</a>");
                                }
                            }
                            
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"Regulation/8.aspx\" title='" + Resources.HercResource.RepealedRegulations + "' >" + Resources.HercResource.RepealedRegulations + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }
                        else
                        {

                            p_var.sbuilder.Append("<a class=\"regulation-icon\" href='javascript:void(0);' title='" + Resources.HercResource.Regulations + "'>" + Resources.HercResource.Regulations + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=" + ResolveUrl("~/content/Hindi/Regulation/1.aspx") + " title='" + Resources.HercResource.herc + "' >" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string urlname = ds.Tables[0].Rows[i]["placeholderone"].ToString();
                                if (Convert.ToInt16(ds.Tables[0].Rows[i]["link_id"]) == 203)
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[i]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "</a>");
                                }
                            }

                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=" + ResolveUrl("~/content/Hindi/Regulation/8.aspx") + " title='" + Resources.HercResource.RepealedRegulations + "'>" + Resources.HercResource.RepealedRegulations + "</a>");
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
                           // p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a class=\"code-icon\" href='javascript:void(0);' title='" + Resources.HercResource.Codes + "'>" + Resources.HercResource.Codes + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"Codes/1.aspx\" title='" + Resources.HercResource.herc + "'>" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string urlname = ds.Tables[0].Rows[i]["placeholderone"].ToString();
                                if (Convert.ToInt16(ds.Tables[0].Rows[i]["link_id"]) == 195)
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[i]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "</a>");
                                }
                            }
                            //p_var.sbuilder.Append("<a href=\"Codes/4.aspx\" title=\"Others\" class=\"margin\">" + Resources.HercResource.Others + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"Codes/8.aspx\" title='" + Resources.HercResource.RepealedCodes + "'>" + Resources.HercResource.RepealedCodes + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }
                        else
                        {
                            //p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a class=\"code-icon\" href='javascript:void(0);' title='" + Resources.HercResource.Codes + "'>" + Resources.HercResource.Codes + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=" + ResolveUrl("~/content/Hindi/Codes/1.aspx") + " title='" + Resources.HercResource.herc + "'>" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string urlname = ds.Tables[0].Rows[i]["placeholderone"].ToString();
                                if (Convert.ToInt16(ds.Tables[0].Rows[i]["link_id"]) == 207)
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[i]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "</a>");
                                }
                            }
                           
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=" + ResolveUrl("~/content/Hindi/Codes/8.aspx") + " title='" + Resources.HercResource.RepealedCodes + "'>" + Resources.HercResource.RepealedCodes + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("</ul>");
                            p_var.sbuilder.Append("</li>");
                        }

                    }
                    if (p_var.i == 3)
                    {

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                        {
                           // p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a class=\"standard-icon\" href='javascript:void(0);' title='" + Resources.HercResource.Standards + "'>" + Resources.HercResource.Standards + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"Standards/1.aspx\" title='" + Resources.HercResource.herc + "'>" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                         
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string urlname = ds.Tables[0].Rows[i]["placeholderone"].ToString();
                                if (Convert.ToInt16( ds.Tables[0].Rows[i]["link_id"]) == 198)
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[i]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "</a>");
                                }
                            }
                            //p_var.sbuilder.Append("<a href=\"Standards/4.aspx\" title=\"Others\" class=\"margin\">" + Resources.HercResource.Others + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"Standards/8.aspx\" title='" + Resources.HercResource.RepealedStandard + "'>" + Resources.HercResource.RepealedStandard + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }
                        else
                        {
                           // p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a class=\"standard-icon\" href='javascript:void(0);' title='" + Resources.HercResource.Standards + "'>" + Resources.HercResource.Standards + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=" + ResolveUrl("~/content/Hindi/Standards/1.aspx") + " title='" + Resources.HercResource.herc + "'>" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");

                            p_var.sbuilder.Append("<li>");


                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {

                                string urlname = ds.Tables[0].Rows[i]["placeholderone"].ToString();
                                if (Convert.ToInt16(ds.Tables[0].Rows[i]["link_id"]) == 211)
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds.Tables[0].Rows[i]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["name"].ToString()) + "</a>");
                                }
                            }
                            //p_var.sbuilder.Append("<a href="+ResolveUrl("~/content/Hindi/Standards/4.aspx")+" title=\"Others\" class=\"margin\">" + Resources.HercResource.Others + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=" + ResolveUrl("~/content/Hindi/Standards/8.aspx") + " title='" + Resources.HercResource.RepealedStandard + "'>" + Resources.HercResource.RepealedStandard + "</a>");
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
                            objnew.LinkParentId = 773 ;//771 
                        }
                        else
                        {
                            objnew.LinkParentId = 775; //773  
                        }
                        DataSet ds1 = subMenuBL.get_Frontside_Submenu_of_RootMenu(objnew);
                        if(ds1.Tables[0].Rows.Count>0)
                        {
                           string urlname1 = ds1.Tables[0].Rows[0]["placeholderone"].ToString();
                        }
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                        {
                         
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a class=\"notification-icon\" href='javascript:void(0);' title='" + Resources.HercResource.Notifications + "'>" + Resources.HercResource.Notifications + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"Notifications/1.aspx\" title='" + Resources.HercResource.herc + "' >" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");


                            string urlname1 = ds1.Tables[0].Rows[0]["placeholderone"].ToString();
                            if (Convert.ToInt16(ds1.Tables[0].Rows[0]["link_id"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Notification))
                                {
                                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + ds1.Tables[0].Rows[0]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname1.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds1.Tables[0].Rows[0]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds1.Tables[0].Rows[0]["name"].ToString()) + "</a>");
                                }
                            
                            //p_var.sbuilder.Append("<a href=\"Notifications/4.aspx\" title=\"Others\" class=\"margin\">" + Resources.HercResource.Others + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"Notifications/8.aspx \" title='" + Resources.HercResource.RepealedNotifications + "'>" + Resources.HercResource.RepealedNotifications + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a class=\"notification-icon\" href='javascript:void(0);' title='" + Resources.HercResource.Notifications + "'>" + Resources.HercResource.Notifications + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=" + ResolveUrl("~/content/Hindi/Notifications/1.aspx") + " title='" + Resources.HercResource.herc + "'>" + Resources.HercResource.herc + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");

                            string urlname1 = ds1.Tables[0].Rows[0]["placeholderone"].ToString();
                            if (Convert.ToInt16(ds1.Tables[0].Rows[0]["link_id"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Notification_Hindi))
                            {
                                p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + p_var.url + ds1.Tables[0].Rows[0]["Link_Id"] + "_" + Convert.ToInt16(p_var.position) + "_" + urlname1.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(ds1.Tables[0].Rows[0]["name"].ToString()) + "'>" + HttpUtility.HtmlDecode(ds1.Tables[0].Rows[0]["name"].ToString()) + "</a>");
                            }
                            //p_var.sbuilder.Append("<a href="+ResolveUrl("~/content/Hindi/Notifications/4.aspx")+" title=\"Others\" class=\"margin\">" + Resources.HercResource.Others + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=" + ResolveUrl("~/content/Hindi/Notifications/8.aspx") + " title='" + Resources.HercResource.RepealedNotifications + "'>" + Resources.HercResource.RepealedNotifications + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }

                        //END

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a class=\"daily-order-icon\" href='javascript:void(0);' title='" + Resources.HercResource.DailyOrders + "'>" + Resources.HercResource.DailyOrders + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"CurrentDailyOrders.aspx\" title='" + Resources.HercResource.CurrentYear + "' >" + Resources.HercResource.CurrentYear + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"PrevYear/1.aspx\" title='" + Resources.HercResource.PreviousYears + "' >" + Resources.HercResource.PreviousYears + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"SearchDailyOrders.aspx\" title='" + Resources.HercResource.DailyOrdersSearch + "' >" + Resources.HercResource.DailyOrdersSearch + "</a>");
                            p_var.sbuilder.Append("</li>");

                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a class=\"daily-order-icon\" href='javascript:void(0);' title='" + Resources.HercResource.DailyOrders + "'>" + Resources.HercResource.DailyOrders + "</a>");
                            p_var.sbuilder.Append(" <ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=" + ResolveUrl("~/content/Hindi/CurrentDailyOrders.aspx") + " title='" + Resources.HercResource.CurrentYear + "'>" + Resources.HercResource.CurrentYear + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=" + ResolveUrl("~/content/Hindi/PrevYear/1.aspx") + " title='" + Resources.HercResource.PreviousYears + "' >" + Resources.HercResource.PreviousYears + "</a>");
                            p_var.sbuilder.Append("</li>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=" + ResolveUrl("~/content/Hindi/SearchDailyOrders.aspx") + " title='" + Resources.HercResource.DailyOrdersSearch + "' >" + Resources.HercResource.DailyOrdersSearch + "</a>");
                            p_var.sbuilder.Append("</li>");

                            p_var.sbuilder.Append(" </ul>");
                            p_var.sbuilder.Append("</li>");
                        }
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                        {
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a class=\"draft-icon\" href='javascript:void(0);' title='" + Resources.HercResource.DDPapers + "'>" + Resources.HercResource.DDPapers + "</a>");

                            p_var.sbuilder.Append("<ul>");
                            p_var.sbuilder.Append("<li>");
                            p_var.sbuilder.Append("<a href=\"DiscussionPapers.aspx\" title='" + Resources.HercResource.CurrentDraftDisscussionPaper + "'>" + Resources.HercResource.CurrentDraftDisscussionPaper + "</a>");
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
                            p_var.sbuilder.Append("<a class=\"draft-icon\" href='javascript:void(0);' title='" + Resources.HercResource.DDPapers + "'>" + Resources.HercResource.DDPapers + "</a>");

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
           

           // p_var.sbuilder.Append("</li>");
            ltrlMenu.Text = p_var.sbuilder.ToString();
        }
        catch
        {
           // throw;
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

                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(subMenuName) + "'>" + HttpUtility.HtmlDecode(subMenuName) + "</a>");
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

                    p_var.sbuilder.Append("<a  href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx' title='" + HttpUtility.HtmlDecode(subMenuName) + "'>" + HttpUtility.HtmlDecode(subMenuName) + "</a>");
                    p_var.sbuilder.Append("</li>");



                }
                p_var.sbuilder.Append("</ul>");
            }
            p_var.sbuilder.Append("</li>");
        }
    }

    #endregion


}

