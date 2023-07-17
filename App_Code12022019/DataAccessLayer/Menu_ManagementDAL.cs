using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using NCM.DAL;
public class Menu_ManagementDAL
{
    //Area for all the variables declaration 

    #region variable declaration zone

    NCMdbAccess ncmdbObject = new NCMdbAccess();
    Project_Variables p_Val = new Project_Variables();
   
    #endregion 

    //End

    //Area for all the constructors declaration

    #region Default constructor zone

    public Menu_ManagementDAL()
	{

    }

    #endregion 

    //End
    
    //Area for all the functions to get data

    #region Function to get menuName from Link_Web table

    public DataSet getMenuName_From_Web(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.CommandText = "ASP_GetMenuFor_SubMenu_Link";
            ncmdbObject.Parameters.Add(new SqlParameter("@LangID", lnkObject.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@DepartmentID", lnkObject.DepttId));
            ncmdbObject.Parameters.Add(new SqlParameter("@PositionId", lnkObject.PositionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@LinkParentId", lnkObject.LinkParentId));
            return ncmdbObject.ExecuteDataSet();
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get menu type

    public DataSet getMenuType()
    {
        try
        {
            ncmdbObject.CommandText = "ASP_Get_LinkType_Mst";
            return ncmdbObject.ExecuteDataSet();
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get menu position

    public DataSet getMenuPosition()
    {
        try
        {
            ncmdbObject.CommandText = "ASP_Get_Position_Mst";
            return ncmdbObject.ExecuteDataSet();
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get menu order at the ROOT level

    public DataSet get_levelOrder_Link_Web(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@rootLevelId", lnkObject.LinkParentId));
            return ncmdbObject.ExecuteDataSet("ASP_CountRootOrder_Link");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get data for edit either from links table or links_web table

    public DataSet getMenu_For_Editing(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@statusId", lnkObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@TempLinkId", lnkObject.TempLinkId));
            ncmdbObject.Parameters.Add(new SqlParameter("@LangId", lnkObject.LangId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_Link_Tmp_Edit");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get Sublinks of Parant Menu from final table

    public DataSet get_SublinksID_of_Parant_From_Web(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@LinkParentId", lnkObject.LinkParentId));
            ncmdbObject.Parameters.Add(new SqlParameter("@LinkLevel", lnkObject.LinkLevel));
            ncmdbObject.Parameters.Add(new SqlParameter("@PositionId", lnkObject.PositionId));
            return ncmdbObject.ExecuteDataSet("ASP_GetParantSublinksID_Link");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get menu level  at the ROOT level

    public DataSet get_Menu_level_Link_Web(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@rootLevelId", lnkObject.LinkParentId));
            return ncmdbObject.ExecuteDataSet("ASP_CountRootLevel_Link");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

        //ALL FUNCTIONS ARE WRITTEN FOR THE FRONT END SIDE

        #region Function to get ROOT MENU

        public DataSet get_Frontside_RootMenu(LinkOB lnkObject)
        {
            try
            {
                ncmdbObject.Parameters.Add(new SqlParameter("@moduleid", lnkObject.ModuleId));
                ncmdbObject.Parameters.Add(new SqlParameter("@linkparentid", lnkObject.LinkParentId));
                ncmdbObject.Parameters.Add(new SqlParameter("@positionid", lnkObject.PositionId));
                ncmdbObject.Parameters.Add(new SqlParameter("@langid", lnkObject.LangId));
                //ncmdbObject.Parameters.Add(new SqlParameter("@deptt_id", lnkObject.DepttId));
                return ncmdbObject.ExecuteDataSet("USP_Get_Root_Menu");

                //return ncmdbObject.ExecuteDataSet("USP_Get_Root_Menu_Ombudsman");
            }
            catch
            {
                throw;
            }
            finally
            {
                ncmdbObject.Dispose();
            }
        }


        #endregion

        #region Function to get submenu of root menu

        public DataSet get_Frontside_Submenu_of_RootMenu(LinkOB lnkObject)
        {
            try
            {
                ncmdbObject.Parameters.Add(new SqlParameter("@link_parent_id", lnkObject.LinkParentId));
                ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", lnkObject.LangId));
                return ncmdbObject.ExecuteDataSet("USP_Get_Child_of_Root_Menu");
            }
            catch
            {
                throw;
            }
            finally
            {
                ncmdbObject.Dispose();
            }
        }


        #endregion


     


        #region get cicked Parent Child menu
        public DataSet get_Cliked_Parent_Child_Menu(LinkOB lnkObject)
        {
            try
            {
                ncmdbObject.Parameters.Add(new SqlParameter("@link_parent_id", lnkObject.LinkParentId));
                ncmdbObject.Parameters.Add(new SqlParameter("@position_id", lnkObject.PositionId));
                ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", lnkObject.LangId));
                return ncmdbObject.ExecuteDataSet("USP_GetFrontMenuSubMenu_Root");
            }
            catch
            { 
                throw;
            }
            finally
            {
                ncmdbObject.Dispose();
            }
        }
     #endregion

        #region get cicked Parent Child menu for ombudsman

        public DataSet get_Cliked_Parent_ChildOmbudsman_Menu(LinkOB lnkObject)
        {
            try
            {
                ncmdbObject.Parameters.Add(new SqlParameter("@link_parent_id", lnkObject.LinkParentId));
                ncmdbObject.Parameters.Add(new SqlParameter("@position_id", lnkObject.PositionId));
                ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", lnkObject.LangId));
                return ncmdbObject.ExecuteDataSet("USP_GetFrontMenuSubMenuOmbudsman_Root");
            }
            catch
            {
                throw;
            }
            finally
            {
                ncmdbObject.Dispose();
            }
        }

        #endregion


        #region get cicked Parent Child menu for ombudsman
        public DataSet get_Cliked_ParentChild_Menuombudsman(LinkOB lnkObject)
        {
            try
            {
                ncmdbObject.Parameters.Add(new SqlParameter("@link_parent_id", lnkObject.LinkParentId));
                ncmdbObject.Parameters.Add(new SqlParameter("@position_id", lnkObject.PositionId));
                ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", lnkObject.LangId));
                return ncmdbObject.ExecuteDataSet("USP_GetFrontMenuSubMenu_RootOmbudsman");
            }
            catch
            {
                throw;
            }
            finally
            {
                ncmdbObject.Dispose();
            }
        }
        #endregion

        #region Function to get MENU_ID to move it LEFT

        public DataSet get_Link_Menu_ID(LinkOB lnkObject)
        {
            try
            {
                ncmdbObject.Parameters.Add(new SqlParameter("@link_parent_id", lnkObject.LinkParentId));
                return ncmdbObject.ExecuteDataSet("ASP_Links_Web_Get_Menu_Parent_ID");
            }
            catch
            {
                throw;
            }
            finally
            {
                ncmdbObject.Dispose();
            }
        }

        #endregion

        #region Function to get Root menu

        public DataSet get_Cliked_Parent_Menu(LinkOB lnkObject)
        {
            try
            {
                ncmdbObject.Parameters.Add(new SqlParameter("@link_id", lnkObject.linkID));
                ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", lnkObject.LangId));
                return ncmdbObject.ExecuteDataSet("USP_GetFrontMenu_Root");
            }
            catch
            {
                throw;
            }
            finally
            {
                ncmdbObject.Dispose();
            }
        }

        #endregion

        ////////#region Function to get child of the parent menu

        ////////public DataSet get_Cliked_Parent_Child_Menu(LinkOB lnkObject)
        ////////{
        ////////    try
        ////////    {
        ////////        ncmdbObject.Parameters.Add(new SqlParameter("@link_parent_id", lnkObject.LinkParentId));
        ////////        ncmdbObject.Parameters.Add(new SqlParameter("@position_id", lnkObject.PositionId));
        ////////        ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", lnkObject.LangId));
        ////////        return ncmdbObject.ExecuteDataSet("USP_GetFrontMenuSubMenu_Root");
        ////////    }
        ////////    catch
        ////////    {
        ////////        throw;
        ////////    }
        ////////    finally
        ////////    {
        ////////        ncmdbObject.Dispose();
        ////////    }
        ////////}

        ////////#endregion

        #region Function to get clicked root name

        public DataSet getParent_name_ofRoot(LinkOB lnkObject)
        {
            try
            {
                ncmdbObject.Parameters.Add(new SqlParameter("@link_id", lnkObject.linkID));
                ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", lnkObject.LangId));
                return ncmdbObject.ExecuteDataSet("USP_GetRootMenuName");
            }
            catch
            {
                throw;
            }
            finally
            {
                ncmdbObject.Dispose();
            }

        }

        #endregion

        #region Function to get right side external link menu

        //////public DataSet get_RightSide_External_Menu(LinkOB lnkObject)
        //////{
        //////    //try
        //////    //{
        //////    //    ncmdbObject.Parameters.Add(new SqlParameter("@lang_id",lnkObject.LangId));
        //////    //    ncmdbObject.Parameters.Add(new SqlParameter("@position_id", lnkObject.PositionId));
        //////    //    ncmdbObject.Parameters.Add(new SqlParameter("@module_id", lnkObject.ModuleId));
        //////    //    ncmdbObject.Parameters.Add(new SqlParameter("@linktype_id", lnkObject.LinkTypeId));
        //////    //}
        //////    //catch
        //////    //{
        //////    //    throw;
        //////    //}
        //////    //finally
        //////    //{
        //////    //    ncmdbObject.Dispose();
        //////    //}
        //////}

        #endregion

        #region Function to bind banner on home page

        public DataSet get_Banner(LinkOB lnkObject)
        {
            try
            {
                ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", lnkObject.LangId));
                ncmdbObject.Parameters.Add(new SqlParameter("@module_id", lnkObject.ModuleId));

                return ncmdbObject.ExecuteDataSet("USP_get_Banner");
            }
            catch
            {
                throw;
            }
            finally
            {
                ncmdbObject.Dispose();
            }
        }

        #endregion

        #region Function to get Latest News

        public DataSet getLatestNews(LinkOB lnkObject)
        {
            try
            {
                ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", lnkObject.LangId));
                ncmdbObject.Parameters.Add(new SqlParameter("@module_id", lnkObject.ModuleId));
                ncmdbObject.Parameters.Add(new SqlParameter("@linkid", lnkObject.linkID));

                return ncmdbObject.ExecuteDataSet("USP_get_latestNews");
            }
            catch
            {
                throw;
            }
            finally
            {
                ncmdbObject.Dispose();
            }
        }

        #endregion

        #region Function to get ALL Latest News

        public DataSet getLatestNewsAll(LinkOB lnkObject, out int catValue)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
                param[0].Direction = ParameterDirection.Output;
                ncmdbObject.Parameters.Add(param[0]);

                ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", lnkObject.LangId));
                ncmdbObject.Parameters.Add(new SqlParameter("@module_id", lnkObject.ModuleId));
                ncmdbObject.Parameters.Add(new SqlParameter("@linkid", lnkObject.linkID));
                ncmdbObject.Parameters.Add(new SqlParameter("@PageIndex", 1));
                ncmdbObject.Parameters.Add(new SqlParameter("@PageSize", 10));
                p_Val.dSet = ncmdbObject.ExecuteDataSet("USP_get_latestNewsAll");
                catValue = Convert.ToInt32(param[0].Value);
                return p_Val.dSet;
            }
            catch
            {
                throw;
            }
            finally
            {
                ncmdbObject.Dispose();
            }
        }

        #endregion

        //End
    //End

    //Area for all the functions to insert

    #region function to insert top menu in links temp table

    public int insert_Top_Menu(LinkOB lnkObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
           // ncmdbObject.CommandText = "ASP_InsertUpdateDelete_Link_Tmp";
            ncmdbObject.CommandText = "ASP_Tmp_link_Insert_Update";
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", lnkObject.ActionType));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", lnkObject.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Link_Parent_Id", lnkObject.LinkParentId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Link_Order", lnkObject.LinkOrder));
            ncmdbObject.Parameters.Add(new SqlParameter("@Link_Level", lnkObject.LinkLevel));
            ncmdbObject.Parameters.Add(new SqlParameter("@Link_Type_Id", lnkObject.LinkTypeId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Link_Id", lnkObject.linkID));
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Link_Id", lnkObject.TempLinkId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Browser_Title", lnkObject.BrowserTitle));
            ncmdbObject.Parameters.Add(new SqlParameter("@Page_Title", lnkObject.PageTitle));
            ncmdbObject.Parameters.Add(new SqlParameter("@Meta_Keywords", lnkObject.MetaKeywords));
            ncmdbObject.Parameters.Add(new SqlParameter("@MetaTitle", lnkObject.MetaTitle));
            ncmdbObject.Parameters.Add(new SqlParameter("@Mate_Description", lnkObject.MateDescription));
            ncmdbObject.Parameters.Add(new SqlParameter("@Module_Id", lnkObject.ModuleId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Name", lnkObject.NAME));
            ncmdbObject.Parameters.Add(new SqlParameter("@Position_Id", lnkObject.PositionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", lnkObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Deptt_Id", lnkObject.DepttId));
            //ncmdbObject.Parameters.Add(new SqlParameter("@DateInserted", lnkObject.in));
            ncmdbObject.Parameters.Add(new SqlParameter("@UrlName", lnkObject.UrlName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Start_Date", lnkObject.StartDate));
            ncmdbObject.Parameters.Add(new SqlParameter("@End_Date", lnkObject.EndDate));
           // ncmdbObject.Parameters.Add(new SqlParameter("@IpAddress",lnkObject.IpAddress));
            ncmdbObject.Parameters.Add(new SqlParameter("@Last_Updated_By",lnkObject.LastUpdatedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Inserted_By", lnkObject.InsertedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@IPAddress", lnkObject.IpAddress));
          //  ncmdbObject.Parameters.Add(new SqlParameter("@User_Id", lnkObject.InsertedBy));
            if (lnkObject.LinkTypeId == 3)
            {
                ncmdbObject.Parameters.Add(new SqlParameter("@Url", lnkObject.URL));
            }
            else if (lnkObject.LinkTypeId == 1)
            {
                ncmdbObject.Parameters.Add(new SqlParameter("@Details", lnkObject.details));
            }
            else if (lnkObject.LinkTypeId == 2)
            {
                ncmdbObject.Parameters.Add(new SqlParameter("@File_Name", lnkObject.FileName));
            }
            //Output parameter to return Record count

            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            //End
            ncmdbObject.ExecuteNonQuery();
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region function to insert top menu in links web table

    public int insert_Top_Menu_in_Web(LinkOB lnkObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            ncmdbObject.Parameters.Add(new SqlParameter("@TempLinkId", lnkObject.TempLinkId));
            ncmdbObject.Parameters.Add(new SqlParameter("@UpdatedBY", lnkObject.LastUpdatedBy));
            ncmdbObject.AddParameter("@UserID", lnkObject.UserID);
            ncmdbObject.AddParameter("@IPAddress", lnkObject.IpAddress);
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.ExecuteNonQuery("ASP_InsertUpdateDelete_Link");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    //End

    //Area for all the functions to delete

    #region function to delete pending or approved record

    public int Delete_Pending_Approved_Record(LinkOB lnkObject)
    {
        try
        {
            SqlParameter param = new SqlParameter();
            ncmdbObject.AddParameter("@TempLinkId", lnkObject.TempLinkId);
            ncmdbObject.AddParameter("@StatusId", lnkObject.StatusId);
            //  ncmdbObject.AddParameter("@IpAddress", lnkObject.IpAddress);
            ncmdbObject.AddParameter("@UpdatedBy", lnkObject.LastUpdatedBy);
            param = new SqlParameter("@recordCount", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param);
            ncmdbObject.ExecuteNonQuery("ASP_Delete_Link_Tmp_Link");


            p_Val.Result = Convert.ToInt32(param.Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }


    #endregion

    //End


    //#region Function to get submenu of root menu By kumar Gaurav

    //public DataSet get_Frontside_Submenu_of_RootMenu(LinkOB lnkObject)
    //{
    //    try
    //    {
    //        ncmdbObject.Parameters.Add(new SqlParameter("@link_parent_id", lnkObject.LinkParentId));
    //        ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", lnkObject.LangId));
    //        return ncmdbObject.ExecuteDataSet("USP_Get_Child_of_Root_Menu");
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //    finally
    //    {
    //        ncmdbObject.Dispose();
    //    }
    //}
    //#endregion

    #region Function to get Last updated date for sitemap

    public string getLastUpdatedDateSiteMap(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.AddParameter("@langid", lnkObject.LangId);
            p_Val.str = ncmdbObject.ExecuteScalar("usp_GetLastUpdatedSiteMap").ToString();
            return p_Val.str;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 


    //Area of Ombudsman

    #region Function to get ROOT MENU FOR OMBUDSMAN

    public DataSet get_Frontside_RootMenu_Ombudsman(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@moduleid", lnkObject.ModuleId));
            ncmdbObject.Parameters.Add(new SqlParameter("@linkparentid", lnkObject.LinkParentId));
            ncmdbObject.Parameters.Add(new SqlParameter("@positionid", lnkObject.PositionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@langid", lnkObject.LangId));
            return ncmdbObject.ExecuteDataSet("USP_Get_Root_Menu_Ombudsman");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }


    #endregion


    #region Function to get Root menu for ombudsman

    public DataSet get_Cliked_Parent_MenuForOmbudsman(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@link_parent_id", lnkObject.LinkParentId));
            ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", lnkObject.LangId));
            return ncmdbObject.ExecuteDataSet("USP_GetFrontMenu_RootForOmbudsman");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    #region Function to get Footer MENU

    public DataSet get_FooterMenu(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@moduleid", lnkObject.ModuleId));
            ncmdbObject.Parameters.Add(new SqlParameter("@linkparentid", lnkObject.LinkParentId));
            ncmdbObject.Parameters.Add(new SqlParameter("@positionid", lnkObject.PositionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@langid", lnkObject.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@DepttId", lnkObject.DepttId));
            return ncmdbObject.ExecuteDataSet("USP_FooterSiteMap");

            //return ncmdbObject.ExecuteDataSet("USP_Get_Root_Menu_Ombudsman");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }


    #endregion

    #region Function to update the link_Temp status for permission

    public string getParentChild(LinkOB obj_linkOB)
    {
        try
        {

            ncmdbObject.AddParameter("id", obj_linkOB.TempLinkId);
            return ncmdbObject.ExecuteScalar("asp_GetMenuSubmenuforsendemail").ToString();
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

}
