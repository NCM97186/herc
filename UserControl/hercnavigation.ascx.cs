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

public partial class UserControl_hercnavigation : System.Web.UI.UserControl
{
    #region Data declaration zone

    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    Project_Variables p_var = new Project_Variables();
    LinkOB lnkObject = new LinkOB();
    int i = 1;

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
            lnkObject.PositionId = 1;
            p_var.position = (int)lnkObject.PositionId;
            lnkObject.ModuleId = 1;
            lnkObject.LinkParentId = 0;
            p_var.dSet = menuBL.get_Frontside_RootMenu(lnkObject);

            for (p_var.i = 0; p_var.i < p_var.dSet.Tables[0].Rows.Count; p_var.i++)
            {
                p_var.menu_name = p_var.dSet.Tables[0].Rows[p_var.i]["name"].ToString();
                p_var.urlname = p_var.dSet.Tables[0].Rows[p_var.i]["placeholderone"].ToString();
                p_var.menuid = Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["link_id"]);

                if (p_var.menuid == (int)Module_ID_Enum.project_MenuID_FrontEnd.Home || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Home_Hindi || p_var.menu_name == "Home" || p_var.menu_name == "home") //For Home page and Gallery.
                {

                    p_var.sbuilder.Append("<li class='MenuLi MenuLiFirst MenuLi" + i + "'>");

                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                    {
                        if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.project_MenuID_FrontEnd.Home)) //Only for home
                        {

                            
                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&","&amp;")) + "' class=\"menuFirstNode homeicon\"    href='" + ResolveUrl("~/") + "index.aspx'>" + "<img src='" + ResolveUrl("~/images/home.png") + "' alt='Home' />" + "</a>");

                        }
                    }
                    else
                    {
                        if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Home_Hindi)) //Only for home Hindi
                        {
                            p_var.sbuilder.Append("<a  title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"menuFirstNode homeicon\" href='" + ResolveUrl("~/content/Hindi/") + "index.aspx'>" + "<img src='" + ResolveUrl("~/images/home.png") + "' alt='Home' />" + "</a>");

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

                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"menuFirstNode\"  href='#'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "<span class=\"sf-sub-indicator\"></span></a>");
                            }


                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"menuFirstNode\"  href='#'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "<span class=\"sf-sub-indicator\"></span></a>");

                            }

                        }
                        else
                        {
                            p_var.sbuilder.Append("<li class='MenuLi MenuLi" + i + "'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"menuFirstNode\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"menuFirstNode\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }

                        }
                    }
                    else
                    {

                        if (p_var.dSet.Tables[0].Rows[p_var.i]["counter_Child"] != DBNull.Value)
                        {

                            p_var.sbuilder.Append("<li class='MenuLi MenuLi" + i + " last'>");

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {

                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"menuFirstNode\"  href='#'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "<span class=\"sf-sub-indicator\"></span></a>");
                            }


                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"menuFirstNode\"  href='#'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "<span class=\"sf-sub-indicator\"></span></a>");

                            }

                        }
                        else
                        {
                            p_var.sbuilder.Append("<li class='MenuLi MenuLi" + i + " last'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"menuFirstNode\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name.Replace("&", "&amp;")) + "' class=\"menuFirstNode\"  href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_var.menu_name).Replace("&", "&amp;") + "</a>");

                            }

                        }
                    }


                }

                //function to bind child of parent menu

                BindMenuChildren(1, Convert.ToInt16(p_var.dSet.Tables[0].Rows[p_var.i]["link_id"]));

                //end
                i = i + 1;

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

            p_var.sbuilder.Append("<ul class='menuSubUl'>");
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

                        p_var.sbuilder.Append("<li class='current'>");

                        p_var.sbuilder.Append("<a  title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&","and")) + "' href='#'>" + HttpUtility.HtmlDecode(subMenuName) + "</a>");

                    }
                    else
                    {
                        if (subMenuDataSet.Tables[0].Rows[i]["LinkTypeId"].ToString() == "3")
                        {
                            p_var.sbuilder.Append("<li class='current'>");
                            p_var.sbuilder.Append("<a  title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' onclick=\"javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.');\" href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "<span class=\"sf-sub-indicator\"> </span></a>");
                           
                        }

                        else
                        {
                            p_var.sbuilder.Append("<li class='current'>");

                            p_var.sbuilder.Append("<a  title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");
                            
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

                        p_var.sbuilder.Append("<li class='hover'>");

                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                    }
                    else
                    {
                        if (subMenuDataSet.Tables[0].Rows[i]["Link_Type_Id"].ToString() == "3")
                        {
                            p_var.sbuilder.Append("<li class='hover'>");

                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'onclick=\"javascript:return confirm( 'This link shall take you to a webpage outside this website. Click OK to continue. Click Cancel to stop.');\" target='_blank' href=" + subMenuDataSet.Tables[0].Rows[i]["url"].ToString() + ">" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                        }
                        else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.profile || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.profile_Hindi)
                        {
                            p_var.sbuilder.Append("<li class='hover'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + "Profile/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Profile/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");
                              

                            }
                        }
                        else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.DistributionCharges || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.DistributionCharges_Hindi)
                        {
                            p_var.sbuilder.Append("<li class='hover'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + "Tariff/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + p_var.url + "Tariff/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");
                               

                            }
                        }
                        else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.TransmissionCharges || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.TransmissionCharges_Hindi)
                        {
                            p_var.sbuilder.Append("<li class='hover'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + "TransmissionCharges/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "TransmissionCharges/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                        }
                        else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.GenerationTariff_Hindi)
                        {
                            p_var.sbuilder.Append("<li class='hover'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "GenerationTariff/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + p_var.url + "GenerationTariff/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");
                               
                            }
                        }

                        else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.WheelingCharges || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.WheelingCharges_Hindi)
                        {
                            p_var.sbuilder.Append("<li class='hover'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + "WheelingCharges/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + p_var.url + "WheelingCharges/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");
                              
                            }
                        }

                        else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.CrossSubsidy || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.CrossSubsidy_Hindi)
                        {
                            p_var.sbuilder.Append("<li class='hover'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + "Crosssubsidy/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + p_var.url + "Crosssubsidy/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");
                                
                            }
                        }

                        else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RenewalEnergy || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.RenewalEnergy_Hindi)
                        {
                            p_var.sbuilder.Append("<li class='hover'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + "RenewalEnergy/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + p_var.url + "RenewalEnergy/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");
                               
                            }
                        }
                        else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.FSA || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.FSA_Hindi)
                        {
                            p_var.sbuilder.Append("<li class='hover'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + "FSA/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + p_var.url + "FSA/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                        }

                        else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.GeneralMiscCharges || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.GeneralMiscCharges_Hindi)
                        {
                            p_var.sbuilder.Append("<li class='hover'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + "GeneralMiscCharges/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + p_var.url + "GeneralMiscCharges/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");
                                
                            }
                        }
                        else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.LocateUs || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.LocateUs_Hindi)
                        {
                            p_var.sbuilder.Append("<li class='hover'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + "Locate/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");
                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "Locate/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");  

                            }
                        }

                        else if (subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.LocateUs || subMenuID == (int)Module_ID_Enum.Menu_ID_Fixed.LocateUs_Hindi)
                        {
                            p_var.sbuilder.Append("<li class='hover'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "Locate/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "'  href='" + ResolveUrl("~/") + p_var.url + "Locate/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                        }

                        else if (subMenuID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Herc) || subMenuID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Herc_Hindi))
                        {
                            p_var.sbuilder.Append("<li class='hover'>");

                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "CurrentRTI/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");
                            }
                            else
                            {

                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "CurrentRTI/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                        }
                        else if (subMenuID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Herc_Prev_RTI) || subMenuID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Herc_Prev_RTI_Hindi))
                        {
                            p_var.sbuilder.Append("<li class='hover'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "PrevRTI/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "PrevRTI/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                        }
                        else if (subMenuID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Hercsearch_RTI) || subMenuID == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Hercsearch_RTI_Hindi))
                        {
                            p_var.sbuilder.Append("<li class='hover'>");
                            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + "searchRTI/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                            else
                            {
                                p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + "searchRTI/" + subMenuID + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");

                            }
                        }
                        else
                        {

                            p_var.sbuilder.Append("<li class='hover'>");

                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(subMenuName.Replace("&", "and")) + "' href='" + ResolveUrl("~/") + p_var.url + subMenuID + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(subMenuName).Replace("&", "&amp;") + "</a>");
                            

                        }
                        p_var.sbuilder.Append("</li>");
                    }
                }

                //recursively called function to bind child of subchild

                //BindMenuChildren(level + 1, Convert.ToInt16(subMenuDataSet.Tables[0].Rows[i]["link_id"]));

                //end
            }
            p_var.sbuilder.Append("</ul>");
        }

    }

    #endregion

}
