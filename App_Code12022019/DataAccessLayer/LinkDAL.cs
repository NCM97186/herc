using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NCM.DAL;
using System.Data.SqlClient;

public class LinkDAL
{
   //Area for all the variables declaration

    #region Variable Declaration 

    NCMdbAccess obj_ncmdb = new NCMdbAccess();
    Project_Variables p_Val = new Project_Variables();

    #endregion 

    //End

    //Area for all the constructors declaration

    #region default constructor

    public LinkDAL()
	{

    }

    #endregion 

    //End

    //Area for all the function to get records

    #region Function Get Language

    public DataSet ASP_Get_Language()
    {
        try
        {
            return obj_ncmdb.ExecuteDataSet("MST_Get_Language");
        }
        catch
        {
            throw;

        }
        finally
        {
            obj_ncmdb.Dispose();
        }

    }

    #endregion

    #region Function to display details

    public DataSet ASP_Links_Display(LinkOB obj_linkOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@status_id", obj_linkOB.StatusId);
            //obj_ncmdb.AddParameter("@module_Id",obj_linkOB.ModuleId);
            obj_ncmdb.AddParameter("@Lang_Id", obj_linkOB.LangId);
            obj_ncmdb.AddParameter("@position_Id", obj_linkOB.PositionId);
            obj_ncmdb.AddParameter("@List_value", obj_linkOB.LinkParentId);
            return obj_ncmdb.ExecuteDataSet("ASP_Tmp_Link_Display");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion

    #region Function to display details for modules

    public DataSet ASP_Links_Display_forModules(LinkOB obj_linkOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@Lang_Id", obj_linkOB.LangId);
            obj_ncmdb.AddParameter("@status_id", obj_linkOB.StatusId);
            obj_ncmdb.AddParameter("@module_Id", obj_linkOB.ModuleId);
            return obj_ncmdb.ExecuteDataSet("ASP_Tmp_Link_Display");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion

    #region Function to display records by id for Editing

    public DataSet ASP_Links_DisplayBYID(LinkOB obj_linkOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@status_id", obj_linkOB.StatusId);
            obj_ncmdb.AddParameter("@Temp_Link_Id", obj_linkOB.TempLinkId);
            //obj_ncmdb.AddParameter("@Lang_Id", obj_linkOB.LangId);
            return obj_ncmdb.ExecuteDataSet("ASP_Tmp_Link_Display_Edit");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion 

    #region Function to get Link Id  for edit from WEB LINKS

    public DataSet links_web_Get_Link_Id_ForEdit(LinkOB obj_linkOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@Link_Id", obj_linkOB.LinkTypeId);
            return obj_ncmdb.ExecuteDataSet("ASP_web_links__Get_Link_Id_ForEdit");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion

    #region Function MST_Get_Status

    public DataSet MST_Get_Status()
    {
        try
        {
            return obj_ncmdb.ExecuteDataSet("MST_Get_Status");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion 

    #region Function To Display Details With Paging

    public DataSet ASP_Links_DisplayWithPaging(LinkOB obj_linkOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.Parameters.Add(param[0]);
            obj_ncmdb.AddParameter("@Lang_Id", obj_linkOB.LangId);
            obj_ncmdb.AddParameter("@status_id", obj_linkOB.StatusId);
            obj_ncmdb.AddParameter("@module_Id", obj_linkOB.ModuleId);
            obj_ncmdb.AddParameter("@position_Id", obj_linkOB.PositionId);
            obj_ncmdb.AddParameter("@DepartmentID", obj_linkOB.DepttId);
            obj_ncmdb.AddParameter("@List_value", obj_linkOB.LinkParentId);
            obj_ncmdb.AddParameter("@PageIndex", obj_linkOB.PageIndex);
            obj_ncmdb.AddParameter("@PageSize", obj_linkOB.PageSize);
            //obj_ncmdb.AddParameter("@catId", obj_linkOB.CatId);
            //p_Val.dSet = obj_ncmdb.ExecuteDataSet("ASP_Get_Link_Tmp");
            p_Val.dSet = obj_ncmdb.ExecuteDataSet("ASP_Tmp_Link_Display");
            catValue = Convert.ToInt32(param[0].Value);

            return p_Val.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion 

    //End

    //Area for all the functions to insert

    #region Function to insert new link

    public int ASP_Links_Insert(LinkOB obj_linkOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@ActionType", obj_linkOB.ActionType);
            obj_ncmdb.AddParameter("@Module_Id", obj_linkOB.ModuleId);
            obj_ncmdb.AddParameter("@Deptt_Id", obj_linkOB.DepttId);
            obj_ncmdb.AddParameter("@Name", obj_linkOB.NAME);
            if (obj_linkOB.ModuleId == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
            {
                obj_ncmdb.AddParameter("@Name_Reg", obj_linkOB.Name_Regional);
                obj_ncmdb.AddParameter("@Details_Reg", obj_linkOB.Details_Regional);
                obj_ncmdb.AddParameter("@Alt_Tag", obj_linkOB.AltTag);
                obj_ncmdb.AddParameter("@AltTag_Reg", obj_linkOB.AltTagReg);

            }
            if (obj_linkOB.ModuleId == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) || obj_linkOB.ModuleId == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
            {
                obj_ncmdb.AddParameter("@LastDateReceiving", obj_linkOB.LastDateReceiving);
            }
            if (obj_linkOB.ModuleId == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper))
            {
            
                obj_ncmdb.AddParameter("@PublicHearingDate", obj_linkOB.PublicHearingDate);
                obj_ncmdb.AddParameter("@Venu", obj_linkOB.venu);
                obj_ncmdb.AddParameter("@Time", obj_linkOB.Time);
            }
          
            obj_ncmdb.AddParameter("@SmallDetails",obj_linkOB.Smalldetails);
            obj_ncmdb.AddParameter("@Details", obj_linkOB.details);
            obj_ncmdb.AddParameter("@File_Name", obj_linkOB.FileName);
            obj_ncmdb.AddParameter("@Image_Name", obj_linkOB.ImageName);
            obj_ncmdb.AddParameter("@Status_Id", obj_linkOB.StatusId);
            obj_ncmdb.AddParameter("@Lang_Id", obj_linkOB.LangId);
            obj_ncmdb.AddParameter("@Start_Date", obj_linkOB.StartDate);
            obj_ncmdb.AddParameter("@End_Date", obj_linkOB.EndDate);
            obj_ncmdb.AddParameter("@Inserted_By",obj_linkOB.InsertedBy);
            obj_ncmdb.AddParameter("@MetaTitle", obj_linkOB.MetaTitle);
            obj_ncmdb.AddParameter("@MetaLanguage", obj_linkOB.MetaLanguage);
            obj_ncmdb.AddParameter("@Meta_Keywords", obj_linkOB.MetaKeywords);
            obj_ncmdb.AddParameter("@Mate_Description", obj_linkOB.MateDescription);
            obj_ncmdb.AddParameter("@IPAddress", obj_linkOB.IpAddress);
            obj_ncmdb.ExecuteNonQuery("ASP_Tmp_link_Insert_Update");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion 

    #region Function To insert Into web_Links

    public int ASP_Insert_Web_Links(LinkOB obj_linkOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@Temp_Link_Id", obj_linkOB.TempLinkId);
            obj_ncmdb.AddParameter("@IPAddress", obj_linkOB.IpAddress);
            obj_ncmdb.AddParameter("@UserID", obj_linkOB.UserID);
            obj_ncmdb.AddParameter("@ModuleID", obj_linkOB.ModuleId);
            obj_ncmdb.ExecuteNonQuery("insert_update_Web_Link");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion 

    //End

    //Area for all the functions to update

    #region Function To update Links

    public Int32 ASP_Links_Update(LinkOB obj_linkOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@ActionType", obj_linkOB.ActionType);
            obj_ncmdb.AddParameter("@Temp_Link_Id",obj_linkOB.TempLinkId);
            obj_ncmdb.AddParameter("@Link_Id", obj_linkOB.OldLinkId);
            obj_ncmdb.AddParameter("@Deptt_Id", obj_linkOB.DepttId);
            obj_ncmdb.AddParameter("@Module_Id", obj_linkOB.ModuleId);       
            obj_ncmdb.AddParameter("@Name", obj_linkOB.NAME);
            if (obj_linkOB.ModuleId == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
            {
                obj_ncmdb.AddParameter("@Name_Reg", obj_linkOB.Name_Regional);
                obj_ncmdb.AddParameter("@Details_Reg", obj_linkOB.Details_Regional);
                obj_ncmdb.AddParameter("@Alt_Tag", obj_linkOB.AltTag);
                obj_ncmdb.AddParameter("@AltTag_Reg", obj_linkOB.AltTagReg);

            }
            if (obj_linkOB.ModuleId == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) || obj_linkOB.ModuleId == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
            {
                obj_ncmdb.AddParameter("@LastDateReceiving", obj_linkOB.LastDateReceiving);
            }
            if (obj_linkOB.ModuleId == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper))
            {
                obj_ncmdb.AddParameter("@Time", obj_linkOB.Time);
                obj_ncmdb.AddParameter("@PublicHearingDate", obj_linkOB.PublicHearingDate);
                obj_ncmdb.AddParameter("@Venu", obj_linkOB.venu);
            }
         
           
            obj_ncmdb.AddParameter("@SmallDetails", obj_linkOB.Smalldetails);
            obj_ncmdb.AddParameter("@Details", obj_linkOB.details);
            obj_ncmdb.AddParameter("@File_Name", obj_linkOB.FileName);
            obj_ncmdb.AddParameter("@Image_Name", obj_linkOB.ImageName);
            obj_ncmdb.AddParameter("@Status_Id", obj_linkOB.StatusId);
            obj_ncmdb.AddParameter("@Lang_Id",obj_linkOB.LangId);
            obj_ncmdb.AddParameter("@Start_Date", obj_linkOB.StartDate);
            obj_ncmdb.AddParameter("@End_Date", obj_linkOB.EndDate);
			obj_ncmdb.AddParameter("@Last_Updated_By", obj_linkOB.UserID);
            obj_ncmdb.AddParameter("@IPAddress", obj_linkOB.IpAddress);
            obj_ncmdb.AddParameter("@MetaTitle", obj_linkOB.MetaTitle);
            obj_ncmdb.AddParameter("@MetaLanguage", obj_linkOB.MetaLanguage);
            obj_ncmdb.AddParameter("@Meta_Keywords", obj_linkOB.MetaKeywords);
            obj_ncmdb.AddParameter("@Mate_Description", obj_linkOB.MateDescription);
            obj_ncmdb.ExecuteNonQuery("ASP_Tmp_link_Insert_Update");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion 

    #region Function to update the temp_links status

    public int ASP_Temp_Links_Update_Status_Id(LinkOB obj_linkOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@Status_Id",obj_linkOB.StatusId);
            obj_ncmdb.AddParameter("@Temp_Link_Id",obj_linkOB.TempLinkId);
            obj_ncmdb.AddParameter("@Status", obj_linkOB.status);
            obj_ncmdb.AddParameter("@UserID", obj_linkOB.UserID);
            obj_ncmdb.AddParameter("@IPAddress", obj_linkOB.IpAddress);
            obj_ncmdb.AddParameter("@ModuleID", obj_linkOB.ModuleId);
            obj_ncmdb.ExecuteNonQuery("ASP_Temp_Links_Change_status");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion 

    #region Function to Delete the data from tables for modules

    public int Delete_ModulesRecords(LinkOB obj_linkOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@StatusId", obj_linkOB.StatusId);
            obj_ncmdb.AddParameter("@TempLinkId", obj_linkOB.TempLinkId);
            obj_ncmdb.AddParameter("@ModuleID", obj_linkOB.ModuleId);
            obj_ncmdb.ExecuteNonQuery("ASP_Delete_Link_Tmp_Link_ForModules");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion 

    #region Function to update the link_Temp status for permission

    public int ASP_ChangeStatus_LinkTmpPermission(LinkOB obj_linkOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@StatusId", obj_linkOB.StatusId);
            obj_ncmdb.AddParameter("@TempLinkId", obj_linkOB.TempLinkId);
            obj_ncmdb.AddParameter("@Status", obj_linkOB.status);
            obj_ncmdb.AddParameter("@UserID", obj_linkOB.UserID);
            obj_ncmdb.AddParameter("@IPAddress", obj_linkOB.IpAddress);
            obj_ncmdb.AddParameter("@ModuleID", obj_linkOB.ModuleId);
            obj_ncmdb.AddParameter("@updatedBy", obj_linkOB.LastUpdatedBy);
            obj_ncmdb.ExecuteNonQuery("ASP_ChangeStatus_LinkTmpPermission");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion 
    
    #region Function to get Deptt Name

    public DataSet ASP_Get_Deptt_Name(UserOB usrObject)
    {
        try
        {
            obj_ncmdb.AddParameter("@ModuleId",usrObject.ModuleId);
            obj_ncmdb.Parameters.Add(new SqlParameter("@Deptt_Id", usrObject.DepttId));
            return obj_ncmdb.ExecuteDataSet("MST_GET_Department");
        }
        catch
        {
            throw;

        }
        finally
        {
            obj_ncmdb.Dispose();
        }

    }

    #endregion     
    //End


    //Front End 17 Nov 2012


    #region function to bind annual report details

    public DataSet Get_AnnualReports(LinkOB obj_linkOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.Parameters.Add(param[0]);
            obj_ncmdb.AddParameter("@PageIndex", obj_linkOB.PageIndex);
            obj_ncmdb.AddParameter("@PageSize", obj_linkOB.PageSize);
            obj_ncmdb.AddParameter("@Lang_Id",obj_linkOB.LangId);
            obj_ncmdb.AddParameter("@year", obj_linkOB.Year);

            p_Val.dSet = obj_ncmdb.ExecuteDataSet("USP_GetAnnualReport");
            catValue = Convert.ToInt32(param[0].Value);
            return p_Val.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }
    #endregion 

    #region function to get Annual information of Previous Year
    public DataSet Get_Annual_PrevYear(LinkOB obj_lnkOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.Parameters.Add(param[0]);
            obj_ncmdb.AddParameter("@year", obj_lnkOB.Year);
            obj_ncmdb.AddParameter("@PageIndex", obj_lnkOB.PageIndex);
            obj_ncmdb.AddParameter("@PageSize", obj_lnkOB.PageSize);
            p_Val.dSet = obj_ncmdb.ExecuteDataSet("USP_GetAnnualReportPrevYear");
            catValue = Convert.ToInt32(param[0].Value);
            return p_Val.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }
    #endregion 

    #region function to bind  top 1 Whats new details

    //public DataSet Get_WhatsNew(LinkOB objlnkOB)
    //{
    //    try
    //    {
    //        obj_ncmdb.AddParameter("@Lang_Id",objlnkOB.LangId);

    //        return obj_ncmdb.ExecuteDataSet("USP_GetWhatsNew");
           
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //    finally
    //    {
    //        obj_ncmdb.Dispose();
    //    }
    //}
    public DataSet Get_WhatsNew(LinkOB objlnkOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@Lang_Id", objlnkOB.LangId);

            return obj_ncmdb.ExecuteDataSet("USP_GetWhatsNewHomePage");

        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }
    #endregion 


    #region function to bind  all  Whats new  details

    public DataSet Get_AllWhatsNew(LinkOB obj_linkOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.Parameters.Add(param[0]);
            obj_ncmdb.AddParameter("@PageIndex", obj_linkOB.PageIndex);
            obj_ncmdb.AddParameter("@PageSize", obj_linkOB.PageSize);
            obj_ncmdb.AddParameter("@Lang_Id",obj_linkOB.LangId);
            p_Val.dSet = obj_ncmdb.ExecuteDataSet("USP_GetAllWhatsNew");
            catValue = Convert.ToInt32(param[0].Value);
            return p_Val.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }
    #endregion 

    #region Function to display Datails  by id In Front End

    public DataSet Link_DisplayDetails(LinkOB obj_linkOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@Link_Id", obj_linkOB.linkID);
            obj_ncmdb.AddParameter("@moduleId", obj_linkOB.ModuleId);
            obj_ncmdb.AddParameter("@Lang_Id", obj_linkOB.LangId);
            return obj_ncmdb.ExecuteDataSet("USP_Link_DisplayDetails");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion 

    #region Function to Bind Licensees

    public DataSet Bind_Licensees(LinkOB lnkObject)
    {
        try
        {

            obj_ncmdb.Parameters.Add(new SqlParameter("@langid", lnkObject.LangId));

            return obj_ncmdb.ExecuteDataSet("USP_Bind_Licensees");


        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }


    #endregion

    #region function to bind DDPapers

    public DataSet GetDiscussionPapers(LinkOB objlnkOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.Parameters.Add(param[0]);
            obj_ncmdb.AddParameter("@PageIndex", objlnkOB.PageIndex);
            obj_ncmdb.AddParameter("@PageSize", objlnkOB.PageSize);
            obj_ncmdb.AddParameter("@Lang_Id", objlnkOB.LangId);
            obj_ncmdb.AddParameter("@Actiontype",objlnkOB.ActionType);
            obj_ncmdb.AddParameter("@Year", objlnkOB.Year);
            p_Val.dSet = obj_ncmdb.ExecuteDataSet("USP_GetDiscussionPapers");
            catValue = Convert.ToInt32(param[0].Value);
            return p_Val.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }

    }

    #endregion 

    #region Function to dateforcalenderevant

    public DataSet get_dateforcalenderevant(LinkOB lnkObject)
    {
        try
        {


            obj_ncmdb.AddParameter("@langid", lnkObject.LangId);
            return obj_ncmdb.ExecuteDataSet("UPS_GetDateforcalenderEvent");


        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }


    #endregion

    #region function to bind year   

    public DataSet Get_YearLink()
    {
        try
        {
            return obj_ncmdb.ExecuteDataSet("USP_GetYearLink");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion 

    #region Function to Bind Public Sector Links

    public DataSet Bind_PublicSectorLinks(LinkOB lnkObject)
    {
        try
        {

            obj_ncmdb.Parameters.Add(new SqlParameter("@langid", lnkObject.LangId));

            return obj_ncmdb.ExecuteDataSet("USP_Bind_PublicSectorLink");


        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }


    #endregion

    #region Function to Bind Public Sector Links Home

    public DataSet Bind_PublicSectorLinksHome(LinkOB lnkObject)
    {
        try
        {

            obj_ncmdb.Parameters.Add(new SqlParameter("@langid", lnkObject.LangId));

            return obj_ncmdb.ExecuteDataSet("USP_Bind_PublicSectorLinkHome");


        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }


    #endregion

    #region Function to Bind Abbreviations

    public DataSet Bind_Abbreviations(LinkOB lnkObject)
    {
        try
        {

            obj_ncmdb.Parameters.Add(new SqlParameter("@langid", lnkObject.LangId));

            return obj_ncmdb.ExecuteDataSet("USP_Bind_Abbreviations");


        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }


    #endregion

    #region Function to update the status in  the web_link

    public int ASP_Update_status(LinkOB objlnkOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);

            obj_ncmdb.AddParameter("@Link_Id", objlnkOB.TempLinkId);
            obj_ncmdb.AddParameter("@Status_Id", objlnkOB.StatusId);
            obj_ncmdb.AddParameter("@Remarks", objlnkOB.Remarks);
            obj_ncmdb.AddParameter("@ModuleID", objlnkOB.ModuleId);
            obj_ncmdb.ExecuteNonQuery("ASP_User_Change_statusRegulation");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }
    #endregion 
    
    #region function to get year

    public DataSet Get_Year()
    {
        try
        {
            return obj_ncmdb.ExecuteDataSet("USP_GetYearDiscussion");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion


    #region function to get year for vacancy

    public DataSet Get_YearVacancy()
    {
        try
        {
            return obj_ncmdb.ExecuteDataSet("USP_GetYearVacancy");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion

    #region Function to update the  Web link status for delete

    public int updateWebStatusDelete(LinkOB lnkObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);

            obj_ncmdb.AddParameter("@Temp_Link_Id", lnkObject.TempLinkId);
            obj_ncmdb.ExecuteNonQuery("ASP_Web_Link_ChangestatusDelete");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion

    #region Function to insert Connected Discussion files

    public int insertConnectedDiscussionFiles(LinkOB lnkObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.Parameters.Add(param[0]);
            obj_ncmdb.Parameters.Add(new SqlParameter("@FileName", lnkObject.FileName));
            obj_ncmdb.Parameters.Add(new SqlParameter("@Status_Id", lnkObject.StatusId));
            obj_ncmdb.AddParameter("@Date", lnkObject.StartDate);
            obj_ncmdb.AddParameter("@LinkId", lnkObject.linkID);
            obj_ncmdb.AddParameter("@Module_Id", lnkObject.ModuleId);
            obj_ncmdb.AddParameter("@Comments", lnkObject.Remarks);
            obj_ncmdb.ExecuteNonQuery("ASP_ConnectedDiscussionFiles_Insert");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion

    #region Function to get connected Discussion file name

    public DataSet getDiscussionFileNames(LinkOB lnkObject)
    {
        try
        {
            obj_ncmdb.AddParameter("@Link_ID", lnkObject.linkID);
            obj_ncmdb.AddParameter("@Module_id", lnkObject.ModuleId);
            return obj_ncmdb.ExecuteDataSet("USP_GetDiscussion_FileName");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion
    
    #region Function to get FileName for Discussion

    public DataSet getFileNameForDiscussion(LinkOB lnkObject)
    {
        try
        {
            obj_ncmdb.AddParameter("@LinkId", lnkObject.linkID);
            //obj_ncmdb.AddParameter("@Status", lnkObject.StatusId);
            obj_ncmdb.AddParameter("@Module_Id",lnkObject.ModuleId);
            return obj_ncmdb.ExecuteDataSet("ASP_GetFileNameDiscussion");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }

    }

    #endregion

    #region Function to Update status for Files in Discussion

    public int UpdateFileStatusForDiscussion(LinkOB lnkObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@Id", lnkObject.linkID);
            obj_ncmdb.ExecuteNonQuery("ASP_Update_FileDiscussionStatus");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }

    }

    #endregion


    #region function to bind Vacancy

    public DataSet GetVacancy(LinkOB objlnkOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.Parameters.Add(param[0]);
            obj_ncmdb.AddParameter("@PageIndex", objlnkOB.PageIndex);
            obj_ncmdb.AddParameter("@PageSize", objlnkOB.PageSize);
            obj_ncmdb.AddParameter("@Lang_Id", objlnkOB.LangId);
            obj_ncmdb.AddParameter("@Actiontype", objlnkOB.ActionType);
            obj_ncmdb.AddParameter("@Year", objlnkOB.Year);
            p_Val.dSet = obj_ncmdb.ExecuteDataSet("USP_Vacancies");
            catValue = Convert.ToInt32(param[0].Value);
            return p_Val.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }

    }

    #endregion 

    // Reports multiple files

    #region Function to Insert Notification/Annual files
    public int InsertFiles(LinkOB objlnkOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@LinkID", objlnkOB.TempLinkId);
            obj_ncmdb.AddParameter("@FileName", objlnkOB.FileName);
            obj_ncmdb.AddParameter("@StatusId", objlnkOB.StatusId);
            obj_ncmdb.AddParameter("@ModuleID", objlnkOB.ModuleId);
            return obj_ncmdb.ExecuteNonQuery("ASP_InsertModuleFiles");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion 

    #region Function to get FileName for Notification/Annual Report

    public DataSet getFileNameForModules(LinkOB objlnkOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@LinkID", objlnkOB.TempLinkId);
            obj_ncmdb.AddParameter("@ModuleId", objlnkOB.ModuleId);
            return obj_ncmdb.ExecuteDataSet("ASP_GetFileNameModules");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }

    }

    #endregion

    #region Function to Update status for Files in Modules

    public int UpdateFileStatusdelete(LinkOB objlnkOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@Id", objlnkOB.linkID);
            obj_ncmdb.ExecuteNonQuery("ASP_Update_FileModules");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }

    }

    #endregion

    #region Function to get FileName for Modules Front End

    public DataSet getFileName(LinkOB objlnkOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@LinkId", objlnkOB.TempLinkId);
            obj_ncmdb.AddParameter("@moduleId", objlnkOB.ModuleId);
            return obj_ncmdb.ExecuteDataSet("USP_GetAnnualNotificationFileName");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }

    }

    #endregion

    #region Function to audit login/logoff Report

    public DataSet getAuditLoginLogoffReport(out int catValue)
    {
        try
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.Parameters.Add(param[0]);
            p_Val.dSet = obj_ncmdb.ExecuteDataSet("ASP_GetAuditTrailLoginLogoffReport");
            catValue = Convert.ToInt32(param[0].Value);

            return p_Val.dSet;

        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion

    #region function To delete the user

    public int DeleteLoginLogoffDetails(UserOB obj_UserOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@date", obj_UserOB.RecUpdateDate);

            obj_ncmdb.ExecuteNonQuery("ASP_DeleteLoginLogout");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }


    }

    #endregion 


    #region Function to audit Report

    public DataSet getAuditReport(out int catValue)
    {
        try
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.Parameters.Add(param[0]);

            p_Val.dSet = obj_ncmdb.ExecuteDataSet("ASP_GetAuditTrailReport");
            catValue = Convert.ToInt32(param[0].Value);

            return p_Val.dSet;

        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncmdb.Dispose();
        }
    }

    #endregion
}
