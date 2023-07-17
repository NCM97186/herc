using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class Menu_ManagementBL

{
    #region Variable declaration zone

    Menu_ManagementDAL obj_menuDL = new Menu_ManagementDAL();

    #endregion 
 
    #region Default constructor zone

    public Menu_ManagementBL()
	{

    }

    #endregion 






    #region function to insert top menu in links temp table

    public int insert_Top_Menu(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.insert_Top_Menu(lnkObject);
          
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get menu type

    public DataSet getMenuType()
    {
        try
        {

            return obj_menuDL.getMenuType();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get menu position

    public DataSet getMenuPosition()
    {
        try
        {

            return obj_menuDL.getMenuPosition();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get menuName from Link_Web table

    public DataSet getMenuName_From_Web(LinkOB lnkObject)
    {
        try
        {

            return obj_menuDL.getMenuName_From_Web(lnkObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get menu order at the ROOT level

    public DataSet get_levelOrder_Link_Web(LinkOB lnkObject)
    {
        try
        {

            return obj_menuDL.get_levelOrder_Link_Web(lnkObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get menu level  at the ROOT level

    public DataSet get_Menu_level_Link_Web(LinkOB lnkObject)
    {
        try
        {

            return obj_menuDL.get_Menu_level_Link_Web(lnkObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region function to insert top menu in links web table

    public int insert_Top_Menu_in_Web(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.insert_Top_Menu_in_Web(lnkObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get Sublinks of Parant Menu from final table

    public DataSet get_SublinksID_of_Parant_From_Web(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.get_SublinksID_of_Parant_From_Web(lnkObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get data for edit either from links table or links_web table

    public DataSet getMenu_For_Editing(LinkOB lnkObject)
    {
        try
        {
           return obj_menuDL.getMenu_For_Editing(lnkObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function  to get ROOT MENU   BY kumar Gaurav

    public DataSet get_Frontside_RootMenu(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.get_Frontside_RootMenu(lnkObject);
        }
        catch
        {
            throw;
        }
    }
    #endregion 



    #region Function to get submenu of root menu BY kumar GAurav

    public DataSet get_Frontside_Submenu_of_RootMenu(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.get_Frontside_Submenu_of_RootMenu(lnkObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion



    #region Function to get MENU_ID to move it LEFT By kumar Gaurav

    public DataSet get_Link_Menu_ID(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.get_Link_Menu_ID(lnkObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to bind banner on home page

    public DataSet get_Banner(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.get_Banner(lnkObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    //#region Function to get child of the parent menu

    //public DataSet get_Cliked_Parent_Child_Menu(LinkOB lnkObject)
    //{
    //    try
    //    {

        
    //        return obj_menuDL.get_Cliked_Parent_Child_Menu(lnkObject);
    //    }
    //    catch
    //    {
    //        throw;
    //    }

    //}

   // #endregion

    #region Function to get child of the parent menu

    public DataSet get_Cliked_Parent_Child_Menu(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.get_Cliked_Parent_Child_Menu(lnkObject);
        }
        catch
        {
            throw;
        }

    }
    #endregion

    #region get cicked Parent Child menu for ombudsman

    public DataSet get_Cliked_Parent_ChildOmbudsman_Menu(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.get_Cliked_Parent_ChildOmbudsman_Menu(lnkObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region get cicked Parent Child menu for ombudsman
    public DataSet get_Cliked_ParentChild_Menuombudsman(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.get_Cliked_ParentChild_Menuombudsman(lnkObject);
        }
        catch
        {
            throw;
        }
        
    }
    #endregion
    #region Function to get Root menu

    public DataSet get_Cliked_Parent_Menu(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.get_Cliked_Parent_Menu(lnkObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get clicked root name

    public DataSet getParent_name_ofRoot(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.getParent_name_ofRoot(lnkObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Last updated date for sitemap

    public string getLastUpdatedDateSiteMap(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.getLastUpdatedDateSiteMap(lnkObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to delete pending or approved record

    public int Delete_Pending_Approved_Record(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.Delete_Pending_Approved_Record(lnkObject);
        }
        catch
        {
            throw;
        }
       
    }


    #endregion




    //Area of Ombudsman

    #region Function to get ROOT MENU FOR OMBUDSMAN

    public DataSet get_Frontside_RootMenu_Ombudsman(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.get_Frontside_RootMenu_Ombudsman(lnkObject);
        }
        catch
        {
            throw;
        }
        
    }


    #endregion


    #region Function to get Root menu for ombudsman

    public DataSet get_Cliked_Parent_MenuForOmbudsman(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.get_Cliked_Parent_MenuForOmbudsman(lnkObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion


    #region Function to get Footer MENU

    public DataSet get_FooterMenu(LinkOB lnkObject)
    {
        try
        {
            return obj_menuDL.get_FooterMenu(lnkObject);
        }

        catch
        {
            throw;
        }
       
    }


    #endregion

    #region Function to update the link_Temp status for permission

    public string getParentChild(LinkOB obj_linkOB)
    {
        try
        {
            return obj_menuDL.getParentChild(obj_linkOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 


}
