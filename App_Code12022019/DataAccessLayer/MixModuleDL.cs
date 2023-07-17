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
using NCM.DAL;
using System.Data.SqlClient;

public class MixModuleDL
{

	public MixModuleDL()
	{
		
	}

    //Area for all the variables declaration

    #region Variable Declaration

    NCMdbAccess ncmdbObject = new NCMdbAccess();
    Project_Variables p_Val = new Project_Variables();

    #endregion

    //End
    
    #region Function to Bind Modules

    public DataSet getModuleName()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("asp_StaticModule_mstModule");
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

    #region Function to get Deptt Name

    public DataSet getDepartmentName()
    {
        try
        {
           
            return ncmdbObject.ExecuteDataSet("MST_GET_DepartmentForModules");
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

    #region Function to insert new records in modules

    public int ASP_Links_Insert(LinkOB obj_linkOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@ActionType", obj_linkOB.ActionType);
            ncmdbObject.AddParameter("@Module_Id", obj_linkOB.ModuleId);
            ncmdbObject.AddParameter("@Deptt_Id", obj_linkOB.DepttId);
            ncmdbObject.AddParameter("@Name", obj_linkOB.NAME);
            ncmdbObject.AddParameter("@RegulationNo", obj_linkOB.regulationNumber);
            ncmdbObject.AddParameter("@Ambedmentid", obj_linkOB.AmbedmentID);
            ncmdbObject.AddParameter("@RegulationNoAmbendment", obj_linkOB.RegulationNoAmbendment);
            ncmdbObject.AddParameter("@SmallDetails", obj_linkOB.Smalldetails);
            ncmdbObject.AddParameter("@Details", obj_linkOB.details);
            ncmdbObject.AddParameter("@File_Name", obj_linkOB.FileName);
            ncmdbObject.AddParameter("@Image_Name", obj_linkOB.ImageName);
            ncmdbObject.AddParameter("@Status_Id", obj_linkOB.StatusId);
            ncmdbObject.AddParameter("@Lang_Id", obj_linkOB.LangId);
            ncmdbObject.AddParameter("@Start_Date", obj_linkOB.StartDate);
            ncmdbObject.AddParameter("@End_Date", obj_linkOB.EndDate);
            ncmdbObject.AddParameter("@ReferenceNo", obj_linkOB.ReferenceNo);
            ncmdbObject.AddParameter("@Url", obj_linkOB.URL);
            ncmdbObject.AddParameter("@Inserted_By",obj_linkOB.InsertedBy);
            ncmdbObject.AddParameter("@MetaTitle", obj_linkOB.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", obj_linkOB.MetaLanguage);
            ncmdbObject.AddParameter("@Meta_Keywords", obj_linkOB.MetaKeywords);
           
           
            ncmdbObject.AddParameter("@Mate_Description", obj_linkOB.MateDescription);
            ncmdbObject.AddParameter("@IPAddress", obj_linkOB.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_link_Insert_Update");
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

    #region Function to update new records in modules

    public int ASP_Links_Update(LinkOB obj_linkOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@ActionType", obj_linkOB.ActionType);
            ncmdbObject.AddParameter("@Module_Id", obj_linkOB.ModuleId);
            ncmdbObject.AddParameter("@Deptt_Id", obj_linkOB.DepttId);
            ncmdbObject.AddParameter("@Name", obj_linkOB.NAME);
            ncmdbObject.AddParameter("@RegulationNo", obj_linkOB.regulationNumber);
            ncmdbObject.AddParameter("@Ambedmentid", obj_linkOB.AmbedmentID);
            ncmdbObject.AddParameter("@RegulationNoAmbendment", obj_linkOB.RegulationNoAmbendment);
            ncmdbObject.AddParameter("@SmallDetails", obj_linkOB.Smalldetails);
            ncmdbObject.AddParameter("@Details", obj_linkOB.details);
            ncmdbObject.AddParameter("@File_Name", obj_linkOB.FileName);
            ncmdbObject.AddParameter("@Image_Name", obj_linkOB.ImageName);
            ncmdbObject.AddParameter("@Status_Id", obj_linkOB.StatusId);
            ncmdbObject.AddParameter("@Lang_Id", obj_linkOB.LangId);
            ncmdbObject.AddParameter("@Start_Date", obj_linkOB.StartDate);
            ncmdbObject.AddParameter("@End_Date", obj_linkOB.EndDate);
            ncmdbObject.AddParameter("@Temp_Link_Id", obj_linkOB.TempLinkId);
            ncmdbObject.AddParameter("@Url", obj_linkOB.URL);
            ncmdbObject.AddParameter("@ReferenceNo", obj_linkOB.ReferenceNo);
            ncmdbObject.AddParameter("@MetaTitle", obj_linkOB.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", obj_linkOB.MetaLanguage);
            ncmdbObject.AddParameter("@Meta_Keywords", obj_linkOB.MetaKeywords);
            ncmdbObject.AddParameter("@Mate_Description", obj_linkOB.MateDescription);
            ncmdbObject.AddParameter("@Link_Id", obj_linkOB.OldLinkId);
            ncmdbObject.AddParameter("@IPAddress", obj_linkOB.IpAddress);
            ncmdbObject.AddParameter("@Last_Updated_By", obj_linkOB.LastUpdatedBy);

            ncmdbObject.ExecuteNonQuery("ASP_Tmp_link_Insert_Update");
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

    #region Function To Display Details With Paging

//    public DataSet ASP_Links_DisplayWithPaging(LinkOB obj_linkOB, out int catValue)
    public DataSet ASP_Links_DisplayWithPaging(LinkOB obj_linkOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Lang_Id", obj_linkOB.LangId);
            ncmdbObject.AddParameter("@status_id", obj_linkOB.StatusId);
            ncmdbObject.AddParameter("@module_Id", obj_linkOB.ModuleId);
            ncmdbObject.AddParameter("@position_Id", obj_linkOB.PositionId);
            ncmdbObject.AddParameter("@DepartmentID", obj_linkOB.DepttId);
            ncmdbObject.AddParameter("@List_value", obj_linkOB.LinkParentId);
           // ncmdbObject.AddParameter("@PageIndex", obj_linkOB.PageIndex);
           // ncmdbObject.AddParameter("@PageSize", obj_linkOB.PageSize);
            //obj_ncmdb.AddParameter("@catId", obj_linkOB.CatId);
            //p_Val.dSet = obj_ncmdb.ExecuteDataSet("ASP_Get_Link_Tmp");
            p_Val.dSet = ncmdbObject.ExecuteDataSet("ASP_Tmp_Link_Display");
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

    #region Function to update the temp_links status

    public int ASP_Temp_Links_Update_Status_Id(LinkOB obj_linkOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", obj_linkOB.StatusId);
            ncmdbObject.AddParameter("@Temp_Link_Id", obj_linkOB.TempLinkId);
            ncmdbObject.AddParameter("@Status", obj_linkOB.status);
            ncmdbObject.AddParameter("@UserID", obj_linkOB.UserID);
            ncmdbObject.AddParameter("@IPAddress", obj_linkOB.IpAddress);
            ncmdbObject.AddParameter("@ModuleID", obj_linkOB.ModuleId);
            ncmdbObject.ExecuteNonQuery("ASP_Temp_Links_Change_status");
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

    #region Function To insert Into web_Links

    public int ASP_Insert_Web_Links(LinkOB obj_linkOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Temp_Link_Id", obj_linkOB.TempLinkId);
            ncmdbObject.ExecuteNonQuery("insert_update_Web_Link");
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

    //AREA FOR ALL THE REGULATIONS FUNCTIONS FOR FRONT END

    #region function to get Regulations Details

    public DataSet Get_Regulations(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@ActionType", obj_petOB.ActionType);
            p_Val.dSet = ncmdbObject.ExecuteDataSet("USP_Get_Regulations");
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

    //END

    //AREA FOR ALL THE STANDARDS FUNCTIONS FOR FRONT END

    #region function to get Standards Details

    public DataSet Get_Standards(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@ActionType", obj_petOB.ActionType);
            p_Val.dSet = ncmdbObject.ExecuteDataSet("USP_Get_Standards");
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

    //END

    //AREA FOR ALL THE CODES FUNCTIONS FOR FRONT END

    #region function to get CODES Details

    public DataSet Get_Codes(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@ActionType", obj_petOB.ActionType);
            p_Val.dSet = ncmdbObject.ExecuteDataSet("USP_Get_Codes");
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

    //END

    //AREA FOR ALL THE POLICIES FUNCTIONS FOR FRONT END

    #region function to get POLICIES Details

    public DataSet Get_Polices(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@ActionType", obj_petOB.ActionType);
            p_Val.dSet = ncmdbObject.ExecuteDataSet("USP_Get_Policies");
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

    //END

    //AREA FOR ALL THE GUIDELINES FUNCTIONS FOR FRONT END

    #region function to get GUIDELINES Details

    public DataSet Get_Guidelines(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@ActionType", obj_petOB.ActionType);
            p_Val.dSet = ncmdbObject.ExecuteDataSet("USP_Get_Guidelines");
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

    #region Function to get modulename

    public DataSet getModuleName(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.AddParameter("@moduleid",lnkObject.ModuleId);
            return ncmdbObject.ExecuteDataSet("getModuleName");

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

    #region Function to insert Connectedpetition

    public int insertConnectedModule(LinkOB lnkObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt32(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("Module_Id", lnkObject.ModuleId));
            ncmdbObject.Parameters.Add(new SqlParameter("Link_ID", lnkObject.linkID));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", lnkObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Deptt_Id", lnkObject.DepttId));
            ncmdbObject.ExecuteNonQuery("ASP_Connected_Module_Insert_Update");
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

    #region Function to get regulation numbers(Conditional)

    public DataSet getRegulation_Number(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RegulationNo", lnkObject.regulationNumber));
            ncmdbObject.Parameters.Add(new SqlParameter("@ModuleId",lnkObject.ModuleId));
            return ncmdbObject.ExecuteDataSet("ASP_Petition_get_Regulation_Number");
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

    #region Function to get Amendment number

    public DataSet getAmendmentNumber()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("asp_DisplayAmendment");
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

    #region Function to get Regulation number

    public DataSet getRegulationNumber(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Module_id", lnkObject.ModuleId));

            return ncmdbObject.ExecuteDataSet("getRegulationNumber");
           
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

    #region Function to display records by id for Editing

    public DataSet ASP_Links_DisplayBYID(LinkOB obj_linkOB)
    {
        try
        {
            ncmdbObject.AddParameter("@status_id", obj_linkOB.StatusId);
            ncmdbObject.AddParameter("@Temp_Link_Id", obj_linkOB.TempLinkId);
            //obj_ncmdb.AddParameter("@Lang_Id", obj_linkOB.LangId);
            return ncmdbObject.ExecuteDataSet("ASP_Tmp_Link_Display_Edit");
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

    #region Function to get get regulation number

    public string getRegulationNumberCurrent(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.AddParameter("@link_id", lnkObject.linkID);
            return ncmdbObject.ExecuteScalar("getRegulationNumber_others").ToString();
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

    #region Function to get connected modulenames

    public DataSet getConnectedModuleName(LinkOB obj_linkOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Link_Id", obj_linkOB.linkID);

            return ncmdbObject.ExecuteDataSet("asp_GetConnectedModuleName");
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

    #region function to delete pending or approved record

    public int DeleteMixModuleRecords(LinkOB lnkObject)
    {
        try
        {
            SqlParameter param = new SqlParameter();
            ncmdbObject.AddParameter("@TempLinkId", lnkObject.TempLinkId);
            ncmdbObject.AddParameter("@StatusId", lnkObject.StatusId);
            ncmdbObject.AddParameter("@ModuleID", lnkObject.ModuleId);
            ncmdbObject.AddParameter("@UpdatedBy", lnkObject.LastUpdatedBy);
            param = new SqlParameter("@recordCount", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param);
            ncmdbObject.ExecuteNonQuery("ASP_DeleteMixModules");


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

    #region Function to get ambendment type(Conditional)

    public int getAmbendmentID(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RegulationNo", lnkObject.regulationNumber));
            ncmdbObject.AddParameter("@AmbedmentID",lnkObject.AmbedmentID);
            ncmdbObject.AddParameter("@TmpLinkId", lnkObject.TempLinkId);
            ncmdbObject.AddParameter("@ModuleId", lnkObject.ModuleId);
            return Convert.ToInt32( ncmdbObject.ExecuteScalar("ASP_Regulation_Ambendmenttype"));
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

    #region Function to get Regulation number according to ambendment

    public DataSet getRegulationNumberAmbendment(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Module_id", lnkObject.ModuleId));

            ncmdbObject.AddParameter("@AmbendmentId",lnkObject.AmbedmentID);
            return ncmdbObject.ExecuteDataSet("getRegulationNumberAmbendment");

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


    #region Function to get connected modulenames for adding new regulation numbers

    public DataSet getConnectedModuleNameForRegulationSelected(LinkOB obj_linkOB)
    {
        try
        {
            ncmdbObject.AddParameter("@RegulationNo", obj_linkOB.regulationNumber);
            ncmdbObject.AddParameter("@ModuleId", obj_linkOB.ModuleId);

            return ncmdbObject.ExecuteDataSet("asp_GetConnectedModuleNameForSelectedRegulation");
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

    #region Function to delete Connected regulations modules

    public int deleteConnectedRegulations(LinkOB lnkObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@linkid", lnkObject.TempLinkId));
            ncmdbObject.ExecuteNonQuery("ASP_Connected_Regulations_Delete");
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

    #region Function to get Regulation numbers in edit mode

    public DataSet getRegulationNumber_In_EditMode(LinkOB lnkObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RegulationNo", lnkObject.regulationNumber));
            ncmdbObject.Parameters.Add(new SqlParameter("@ModuleId", lnkObject.ModuleId));
            ncmdbObject.Parameters.Add(new SqlParameter("@linkId", lnkObject.linkID));
            return ncmdbObject.ExecuteDataSet("ASP_RegulationNumber_In_EditMode");
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

    #region Function to display records GetAmendmentID

    public DataSet USP_GetAmendmentID(LinkOB obj_linkOB)
    {
        try
        {
            ncmdbObject.AddParameter("@RegNo", obj_linkOB.regulationNumber);
            ncmdbObject.AddParameter("@link_Id", obj_linkOB.TempLinkId);
            ncmdbObject.AddParameter("@ModuleID", obj_linkOB.ModuleId);
            return ncmdbObject.ExecuteDataSet("USP_DisplayRemarks_Amendment");
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

    //END

    #region Function To insert regulations/policies/guidelines/etc Into web_Links

    public int InsertRegulationsInWeb(LinkOB obj_linkOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Temp_Link_Id", obj_linkOB.TempLinkId);
            ncmdbObject.AddParameter("@IPAddress", obj_linkOB.IpAddress);
            ncmdbObject.AddParameter("@UserID", obj_linkOB.UserID);
            ncmdbObject.AddParameter("@ModuleID", obj_linkOB.ModuleId);
            ncmdbObject.ExecuteNonQuery("insert_update_WebRegulations");
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


    #region Function to update the  Web link status for Restore

    public int updateWebStatusRestore(LinkOB lnkObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);

            ncmdbObject.AddParameter("@TempLinkId", lnkObject.TempLinkId);
            ncmdbObject.ExecuteNonQuery("ASP_RestoreMixModules");
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



    #region Function to get Details of new regulation numbers

    public DataSet getDetails(LinkOB obj_linkOB)
    {
        try
        {
            ncmdbObject.AddParameter("@RegulationNo", obj_linkOB.regulationNumber);
            ncmdbObject.AddParameter("@ModuleId", obj_linkOB.ModuleId);

            return ncmdbObject.ExecuteDataSet("getDetails");
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
