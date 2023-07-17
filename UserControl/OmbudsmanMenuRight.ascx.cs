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

public partial class UserControl_OmbudsmanMenuRight : System.Web.UI.UserControl
{
    #region Data declaration zone

    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    Project_Variables p_var = new Project_Variables();
    LinkOB lnkObject = new LinkOB();
    public string headername = string.Empty;
    DataSet subMenuData = new DataSet();
    #endregion

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        p_var.position = 3;

        p_var.url = Resources.HercResource.OmbudsmanPageUrl.ToString();
        if (!IsPostBack)
        {

            bindLeftSideMenu();

        }
    }

    #endregion

    public void bindLeftSideMenu()
    {

        try
        {

            lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            lnkObject.PositionId = Convert.ToInt16(p_var.position);
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

                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' target='_blank' href='" + subMenuData.Tables[0].Rows[i]["url"].ToString() + ">" + p_var.menu_name + "</a>");
                    }

                    else
                    {
                        if (Convert.ToInt16(subMenuData.Tables[0].Rows[i]["link_type_id"]) == Convert.ToInt16(Module_ID_Enum.link_type_id.link))
                        {

                            p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' target='_blank' href=" + subMenuData.Tables[0].Rows[i]["url"].ToString() + ">" + p_var.menu_name + "</a>");
                        }
                        else
                        {
                            if (i == 0)
                            {
                                if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Reports || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Reports_Hindi)
                                {
                                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                    {
                                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + "Reports/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                    }
                                    else
                                    {
                                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + "Reports/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                    }
                                    //p_var.sbuilder.Append("<a href=" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx>" + p_var.menu_name + "</a>");
                                }
                            }
                            else if (p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsmancalender || p_var.menuid == (int)Module_ID_Enum.Menu_ID_Fixed.Ombudsmancalender_Hindi)
                            {
                                //p_var.sbuilder.Append("<li>");

                                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + "CommissionCalendar/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + "CommissionCalendar/" + p_var.menuid + "_" + p_var.position + "_" + p_var.urlname.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                            }


                            else
                            {

                                if (p_var.menuid != Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.OmbudsmanWhatsNew) && p_var.menuid != Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.OmbudsmanWhatsNewHindi))
                                {
                                    p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/") + p_var.url + p_var.menuid + "_" + Convert.ToInt16(p_var.position) + "_" + p_var.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_var.menu_name + "</a>");
                                }
                                else
                                {
                                    if (p_var.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.OmbudsmanWhatsNew))
                                    {
                                        p_var.sbuilder.Append("<a title='Whats New' href='" + ResolveUrl("~/OmbudsmanWhatsNew.aspx") + "' >" + p_var.menu_name + "</a>");
                                    }
                                    else
                                    {
                                        p_var.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_var.menu_name) + "' href='" + ResolveUrl("~/OmbudsmanContent/Hindi/OmbudsmanWhatsNew.aspx") + "' >" + p_var.menu_name + "</a>");
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
           
            ltrlleftMenu.Text = p_var.sbuilder.ToString();


        }

        catch
        {
            throw;
        }



    }
}
