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

public partial class usercontrol_OmbudsmanNavigation : System.Web.UI.UserControl
{
    #region Data declaration zone

    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    Project_Variables p_var = new Project_Variables();
    LinkOB lnkObject = new LinkOB();
	int i = 1;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        p_var.url = Resources.HercResource.OmbudsmanPageUrl.ToString();


        if (!IsPostBack)
        {

            BindRootMenu_Category();

        }
    }


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
            
           

            for (p_var.i = 0; p_var.i < p_var.dSet.Tables[0].Rows.Count; p_var.i++)
            {
                p_var.menu_name = p_var.dSet.Tables[0].Rows[p_var.i]["name"].ToString();
                p_var.urlname = p_var.dSet.Tables[0].Rows[p_var.i]["placeholderone"].ToString();
                p_var.menuid = Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["link_id"]);

                if (p_var.menuid == (int)Module_ID_Enum.project_MenuID_FrontEnd.Home_ombudsman || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.HomeHindi_Ombudsman || p_var.menu_name == "Home" || p_var.menu_name == "home") //For Home page and Gallery.
                {

                    p_var.sbuilder.Append("<li class='MenuLi MenuLiFirst MenuLi" + i + "'>");

                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                    {
                        if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.project_MenuID_FrontEnd.Home_ombudsman)) //Only for home
                        {

                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' class=\"menuFirstNode homeicon\" href='" + ResolveUrl("~/") + "index.aspx'>" + "<img src='" + ResolveUrl("~/images/home.png") + "' alt='Home' />" + "</a>");

                        }
                    }
                    else
                    {
                         
                        if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.HomeHindi_Ombudsman)) //Only for home Hindi
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' class=\"menuFirstNode homeicon\" href='" + ResolveUrl("~/content/Hindi/") + "index.aspx'>" + "<img src='" + ResolveUrl("~/images/home.png") + "' alt='Home' />" + "</a>");

                        }
                    }

                    // p_var.sbuilder.Append("</li>");
                }
                else
                {
                    if (p_var.i != p_var.dSet.Tables[0].Rows.Count - 1)
                    {
                        if (p_var.dSet.Tables[0].Rows[p_var.i]["counter_Child"] != DBNull.Value)
                        {

                            p_var.sbuilder.Append("<li class='MenuLi MenuLi" + i + "'>");

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {

                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' class=\"menuFirstNode\"  href='#'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "<span class=\"sf-sub-indicator\"></span></a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' class=\"menuFirstNode\"  href='#'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "<span class=\"sf-sub-indicator\"></span></a>");

                            }

                        }
                        else
                        {
                            p_var.sbuilder.Append("<li class='MenuLi MenuLi" + i + "'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' class=\"menuFirstNode\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + Server.HtmlDecode(p_var.menu_name) + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' class=\"menuFirstNode\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + Server.HtmlDecode(p_var.menu_name) + "</a>");

                            }
                            //p_var.sbuilder.Append("</li>");
                        }
                    }
                    else
                    {
                        if (p_var.dSet.Tables[0].Rows[p_var.i]["counter_Child"] != DBNull.Value)
                        {

                            p_var.sbuilder.Append("<li class='MenuLi MenuLi" + i + " last'>");

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {

                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' class=\"menuFirstNode\"  href='#'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "<span class=\"sf-sub-indicator\"></span></a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' class=\"menuFirstNode\"  href='#'>" + HttpUtility.HtmlDecode(p_var.menu_name) + "<span class=\"sf-sub-indicator\"></span></a>");

                            }

                        }
                        else
                        {
                            p_var.sbuilder.Append("<li class='MenuLi MenuLi" + i + " last'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' class=\"menuFirstNode\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + Server.HtmlDecode(p_var.menu_name) + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' class=\"menuFirstNode\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + Server.HtmlDecode(p_var.menu_name) + "</a>");

                            }
                            //p_var.sbuilder.Append("</li>");
                        }
                    }

                }

                //function to bind child of parent menu

                BindMenuChildren(1, Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["link_id"]));
				 i = i + 1;
                //end

                p_var.sbuilder.Append("</li>");
            }

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

                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='#'>" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "<span class=\"sf-sub-indicator\"> »</span></a>");

                        // p_var.sbuilder.Append("<a href='" + ResolveUrl("~/ContentPage.aspx?menuid=") + subMenuID + "&position=" + Convert.ToInt16(p_var.position) + "'>" + HttpUtility.HtmlDecode(subMenuName) + "</a>");

                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "appeal/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "appeal/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_prevYear || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_prevYear_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AppealPreYear/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AppealPreYear/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "Appealsearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "Appealsearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealOnlineStatus || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealOnlineStatus_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AppealStatus/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AppealStatus/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedCurrent || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedCurrent_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AwardsCurrent/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AwardsCurrent/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedPrevious || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedPrevious_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AwardsPrevious/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AwardsPrevious/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AwardsSearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AwardsSearch/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsUnderAppeal || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsUnderAppeal_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AwardsUnderAppeal/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AwardsUnderAppeal/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "RTICurrent/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "RTICurrent/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    //Previous year RTI

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanPreviousYear || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanPreviousYear_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "perviousrti/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "perviousrti/" +subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    //End


                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "RTISearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "RTISearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.DistributionCharges_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "DistributionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href=" + ResolveUrl("~/") + p_var.url + "DistributionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + subMenuName.Replace("&", "&amp;") + "</a>");
                           
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.TransmissionCharges_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "TransmissionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "TransmissionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                           

                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "GenerationTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "GenerationTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                            

                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.WheelingCharges_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "WheelingTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "WheelingTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                           

                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.CrossSubsidy_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "CrosssubsidyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "CrosssubsidyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                            

                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RenewalEnergy_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "RenewalEnergyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "RenewalEnergyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                           

                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.FSA_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "FSAOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "FSAOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                            

                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsman_Profile || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsman_ProfileHindi)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "ProfileOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "ProfileOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                           // p_var.sbuilder.Append("</li>");

                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else
                    {
                        if (subMenuDataSet.Tables[0].Rows[i]["LinkTypeId"].ToString() == "3")
                        {
                            p_var.sbuilder.Append("<li class=\"current\">");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName) + "<span class=\"sf-sub-indicator\"> »</span></a>");
                           
                        }

                        else
                        {

                            p_var.sbuilder.Append("<li class=\"current\">");

                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName) + "<span class=\"sf-sub-indicator\"> »</span></a>");
                           

                        }
                        p_var.sbuilder.Append("</li>");
                    }
                }
                else
                {
                    if (parentMenuDataSet.Tables[0].Rows.Count > 0)
                    {

                        p_var.sbuilder.Append("<li class=\"current\">");

                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "</a>");

                        p_var.sbuilder.Append("</li>");

                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "appeal/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "appeal/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_prevYear || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Appeal_prevYear_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AppealPreYear/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AppealPreYear/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "Appealsearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "Appealsearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealOnlineStatus || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AppealOnlineStatus_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AppealStatus/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AppealStatus/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedCurrent || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedCurrent_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AwardsCurrent/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AwardsCurrent/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedPrevious || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedPrevious_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AwardsPrevious/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AwardsPrevious/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsPronouncedSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AwardsSearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AwardsSearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsUnderAppeal || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.AwardsUnderAppeal_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "AwardsUnderAppeal/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "AwardsUnderAppeal/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    //Menu binding for the schedule of hearing

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.SOH_CurrentYear || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.SOH_CurrentYearHindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/Ombudsman/") + "ScheduleOfHearing/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "ScheduleOfHearing/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }

                        p_var.sbuilder.Append("</li>");
                    }

                    //End

                    //Menu binding for the schedule of hearing of previous years

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.SOH_PreviousYear || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.SOH_PreviousYearHindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/Ombudsman/") + "ScheduleOfHearingPreviousYear/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "ScheduleOfHearingPreviousYear/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.SOH_NextYear || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.SOH_NextYearHindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/Ombudsman/") + "ScheduleOfHearingNextYear/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "ScheduleOfHearingNextYear/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsman_SoHSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsman_SoHSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/Ombudsman/") + "OmbudsmanSearchSOH/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "OmbudsmanSearchSOH/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }


                    //End


                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "RTICurrent/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "RTICurrent/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }

                         //Previous year RTI

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanPreviousYear || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanPreviousYear_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "perviousrti/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "perviousrti/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    //End
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanSearch || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RTI_OmbudsmanSearch_Hindi)
                    {
                        p_var.sbuilder.Append("<li class=\"current\">");

                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "RTISearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "RTISearch/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.DistributionCharges_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "DistributionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "DistributionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                           
                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.TransmissionCharges_ombudsman )
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "TransmissionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "TransmissionTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "GenerationTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "GenerationTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                            
                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.WheelingCharges_ombudsman )
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "WheelingTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "WheelingTariffOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                           
                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.CrossSubsidy_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "CrosssubsidyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "CrosssubsidyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                           

                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RenewalEnergy_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "RenewalEnergyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "RenewalEnergyOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                           

                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.FSA_ombudsman)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + "FSAOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' class=\"current\" href='" + ResolveUrl("~/") + p_var.url + "FSAOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                           

                        }
                        p_var.sbuilder.Append("</li>");
                    }
                    else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsman_Profile || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsman_ProfileHindi)
                    {
                        p_var.sbuilder.Append("<li>");
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + "ProfileOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName + "</a>");

                        }
                        else
                        {
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + "ProfileOmbudsman/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + subMenuName.Replace("&", "&amp;") + "</a>");
                           

                        }
                        p_var.sbuilder.Append("</li>");
                    }

                    else
                    {
                        if (subMenuDataSet.Tables[0].Rows[i]["Link_Type_Id"].ToString() == "3")
                        {
                            p_var.sbuilder.Append("<li >");
                           // p_var.sbuilder.Append("<a href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName) + "</a>");
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' onclick=\"javascript:return confirm('This link shall take you to a webpage outside this website. Click OK to continue. Click ancel to stop.');\" target='_blank' href=" + subMenuDataSet.Tables[0].Rows[i]["url"].ToString() + ">" + HttpUtility.HtmlDecode(subMenuName) + "</a>");
                           
                        }

                        else
                        {
                            p_var.sbuilder.Append("<li class=\"current\">");

                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "&amp;")) + "</a>");

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

}
