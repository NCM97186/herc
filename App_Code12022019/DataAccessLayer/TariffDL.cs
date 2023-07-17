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


public class TariffDL
{
	public TariffDL()
	{
		
	}


    //Area for all the variables declaration zone

    NCMdbAccess ncmdbObject = new NCMdbAccess();
    Project_Variables p_Var = new Project_Variables();

    //End

    #region Function to get Tariff Type

    public DataSet getTariffType()
    {
        try
        {
            ncmdbObject.CommandText = "[ASP_Get_Tariff_Type]";
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


    #region function to insert NewTariff

    public int insert_New_Tariff(TariffOB tariffOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            //ncmdbObject.CommandText = "ASP_Tmp_link_Insert_Update_Tariff";
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", tariffOB.ActionType));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", tariffOB.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Link_Type_Id", tariffOB.LinkTypeId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Link_Id", tariffOB.linkID));
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Link_Id", tariffOB.TempLinkId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Browser_Title", tariffOB.BrowserTitle));
            ncmdbObject.Parameters.Add(new SqlParameter("@Page_Title", tariffOB.PageTitle));
            ncmdbObject.Parameters.Add(new SqlParameter("@Meta_Keywords", tariffOB.MetaKeywords));
            ncmdbObject.Parameters.Add(new SqlParameter("@Mate_Description", tariffOB.MateDescription));

            ncmdbObject.Parameters.Add(new SqlParameter("@MetaTitle", tariffOB.MetaTitle));
            ncmdbObject.Parameters.Add(new SqlParameter("@MetaLanguage", tariffOB.MetaKeyLanguage));

            ncmdbObject.Parameters.Add(new SqlParameter("@Module_Id", tariffOB.ModuleId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Name", tariffOB.NAME));
            ncmdbObject.Parameters.Add(new SqlParameter("@Cat_Id", tariffOB.CatId));
            ncmdbObject.Parameters.Add(new SqlParameter("@CatTypeId", tariffOB.CatTypeId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id",tariffOB.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Deptt_Id", tariffOB.DepttId));
            ncmdbObject.Parameters.Add(new SqlParameter("@File_Name", tariffOB.FileName));
            
            ncmdbObject.Parameters.Add(new SqlParameter("@UrlName", tariffOB.UrlName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Start_Date", tariffOB.StartDate));
            ncmdbObject.Parameters.Add(new SqlParameter("@End_Date", tariffOB.EndDate));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", tariffOB.Year));
            ncmdbObject.Parameters.Add(new SqlParameter("@Last_Updated_By", tariffOB.LastUpdatedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Inserted_By", tariffOB.InsertedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@IPAddress", tariffOB.IpAddress));
            ncmdbObject.Parameters.Add(new SqlParameter("@Details", tariffOB.details));
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            //End
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_link_Insert_Update_Tariff");
            p_Var.Result = Convert.ToInt32(param[0].Value);
            return p_Var.Result;
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

    public int ASP_ChangeStatus_LinkTmpPermission(TariffOB tariffOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@StatusId", tariffOB.StatusId);
            ncmdbObject.AddParameter("@TempLinkId", tariffOB.TempLinkId);
            ncmdbObject.AddParameter("@Status", tariffOB.status);
            ncmdbObject.AddParameter("@IpAddress", tariffOB.IpAddress);
            ncmdbObject.AddParameter("@UserID", tariffOB.UserID);

            ncmdbObject.AddParameter("@ModuleID", tariffOB.ModuleId);
            ncmdbObject.AddParameter("@updatedBy", tariffOB.LastUpdatedBy);

            ncmdbObject.ExecuteNonQuery("ASP_ChangeStatus_LinkTmpPermission_Tariff");
            p_Var.Result = Convert.ToInt32(param[0].Value);
            return p_Var.Result;
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

    #region function to insert Tariff in links web table

    public int insert_Top_Tariff_in_Web(TariffOB tariffOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            ncmdbObject.Parameters.Add(new SqlParameter("@TempLinkId", tariffOB.TempLinkId));
            ncmdbObject.Parameters.Add(new SqlParameter("@UpdatedBY", tariffOB.LastUpdatedBy));
            ncmdbObject.AddParameter("@IPAddress", tariffOB.IpAddress);
            ncmdbObject.AddParameter("@UserID", tariffOB.UserID);
            ncmdbObject.AddParameter("@ModuleID", tariffOB.ModuleId);
            // ncmdbObject.Parameters.Add(new SqlParameter("@IpAddress", lnkObject.IpAddress));
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.ExecuteNonQuery("ASP_InsertUpdateDelete_Link_Tariff");
            p_Var.Result = Convert.ToInt32(param[0].Value);
            return p_Var.Result;
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

    public int Delete_Pending_Approved_Record(TariffOB tariffOB)
    {
        try
        {
            SqlParameter param = new SqlParameter();
            ncmdbObject.AddParameter("@TempLinkId", tariffOB.TempLinkId);
            ncmdbObject.AddParameter("@StatusId", tariffOB.StatusId);
            ncmdbObject.AddParameter("@ModuleId", tariffOB.ModuleId);
            ncmdbObject.AddParameter("@UpdatedBy", tariffOB.LastUpdatedBy);
            param = new SqlParameter("@recordCount", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param);
            ncmdbObject.ExecuteNonQuery("ASP_Delete_Link_Tmp_Link_Tariff");


            p_Var.Result = Convert.ToInt32(param.Value);
            return p_Var.Result;
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


    #region Function to get Link Id  for edit from WEB LINKS

    public DataSet links_web_Get_Link_Id_ForEdit(TariffOB tariffOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Link_Id", tariffOB.LinkTypeId);
            return ncmdbObject.ExecuteDataSet("ASP_web_links__Get_Link_Id_ForEdit_Tariff");
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

    public DataSet ASP_Links_DisplayWithPaging(TariffOB tariffOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Lang_Id", tariffOB.LangId);
            ncmdbObject.AddParameter("@status_id", tariffOB.StatusId);
            ncmdbObject.AddParameter("@module_Id", tariffOB.ModuleId);
           
            ncmdbObject.AddParameter("@DepartmentID", tariffOB.DepttId);
         
            ncmdbObject.AddParameter("@PageIndex", tariffOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", tariffOB.PageSize);
            ncmdbObject.AddParameter("@catId", tariffOB.CatId);
            ncmdbObject.AddParameter("@CatTypeId", tariffOB.CatTypeId);

            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_Tmp_Link_Display_Tariff");
            catValue = Convert.ToInt32(param[0].Value);

            return p_Var.dSet;
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

    public DataSet getMenu_For_Editing(TariffOB tariffOB)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@statusId", tariffOB.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@CatId", tariffOB.CatId));
            ncmdbObject.Parameters.Add(new SqlParameter("@TempLinkId", tariffOB.TempLinkId));
            ncmdbObject.Parameters.Add(new SqlParameter("@LangId", tariffOB.LangId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_Link_Tmp_Edit_Tariff");
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

    #region function to get year
    public DataSet getYear(TariffOB obj)
    {
        try
        {
            ncmdbObject.AddParameter("@CategoryId", obj.CatId);
            return ncmdbObject.ExecuteDataSet("GetYearcategoryWise");
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


    #region function to get tariff details
    public DataSet Getdetails(TariffOB obj, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@PageIndex", obj.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj.PageSize);
            ncmdbObject.AddParameter("@CategoryId", obj.CatId);
            ncmdbObject.AddParameter("@Year",obj.Year);
            ncmdbObject.AddParameter("@Id", obj.linkID);
            p_Var.dSet= ncmdbObject.ExecuteDataSet("USP_GetTariff");
            catValue = Convert.ToInt32(param[0].Value);
            return p_Var.dSet;
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

    #region function to get tariff details data
    public DataSet GetdetailsData(TariffOB obj)
    {
        try
        {
            
            ncmdbObject.AddParameter("@CategoryId", obj.CatId);
            ncmdbObject.AddParameter("@Year", obj.Year);
            ncmdbObject.AddParameter("@Id", obj.linkID);
            ncmdbObject.AddParameter("@ModuleId", obj.ModuleId);
            return ncmdbObject.ExecuteDataSet("USP_GetTariffDetails");
          
            
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


    #region function to get Categoty
    public DataSet getCategoty(TariffOB obj)
    {
        try
        {
            ncmdbObject.AddParameter("@DepttId", obj.DepttId);

            return ncmdbObject.ExecuteDataSet("USP_GetCategortTariff");
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


    #region function to get FSA details
    public DataSet GetdetailsFSA(TariffOB obj, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@CategoryId", obj.CatId);
            ncmdbObject.AddParameter("@Year", obj.Year);
            ncmdbObject.AddParameter("@Id", obj.linkID);
            ncmdbObject.AddParameter("@PageIndex", obj.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj.PageSize);
            p_Var.dSet= ncmdbObject.ExecuteDataSet("USP_GetFSA");
            catValue = Convert.ToInt32(param[0].Value);

            return p_Var.dSet;
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

    #region function to get year

    public DataSet getYearForTariff(TariffOB obj)
    {
        try
        {
            ncmdbObject.AddParameter("@CategoryId", obj.CatId);
            return ncmdbObject.ExecuteDataSet("GetYearcategoryWiseTariff");
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

    // This is for reports in ombudsman
    #region function to get year

    public DataSet getYearForReport()
    {
        try
        {

            return ncmdbObject.ExecuteDataSet("GetYearReportsOmbudsman");
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



    #region function to get Reports details
    public DataSet GetReportsdetails(TariffOB obj, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@PageIndex", obj.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj.PageSize);  
            ncmdbObject.AddParameter("@Year", obj.Year);
            ncmdbObject.AddParameter("@Id", obj.linkID);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_GetReportsOmbudsman");
            catValue = Convert.ToInt32(param[0].Value);
            return p_Var.dSet;
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

// Reports multiple files

    #region Function to Insert report files 
    public int InsertReportFiles(TariffOB objNew)
    {
        try
        {
            ncmdbObject.AddParameter("@ReportID",objNew.TempLinkId);
            ncmdbObject.AddParameter("@FileName",objNew.FileName);
            ncmdbObject.AddParameter("@StatusId",objNew.StatusId);
            return ncmdbObject.ExecuteNonQuery("ASP_InsertRepotFiles");
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


    #region Function to Update status for Files in Reports

    public int UpdateFileStatusForReports(TariffOB objNew)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Id", objNew.linkID);
            ncmdbObject.ExecuteNonQuery("ASP_Update_FileReportStatus");
            p_Var.Result = Convert.ToInt32(param[0].Value);
            return p_Var.Result;
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


    #region Function to get FileName for Report

    public DataSet getFileNameForReport(TariffOB objNew)
    {
        try
        {
            ncmdbObject.AddParameter("@ReportID", objNew.TempLinkId);
            return ncmdbObject.ExecuteDataSet("ASP_GetFileNameReports");
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

    #region Function to get FileName for Report Front End

    public DataSet getFileName(TariffOB objNew)
    {
        try
        {
            ncmdbObject.AddParameter("@ReportId", objNew.TempLinkId);
            return ncmdbObject.ExecuteDataSet("USP_GetReportsFileName");
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



#region Function to delete Files in tariff

    public int DeleteFileTariff(TariffOB objNew)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Id", objNew.linkID);
            ncmdbObject.AddParameter("@StatusId",objNew.StatusId);

            ncmdbObject.ExecuteNonQuery("ASP_delete_FileTariff");
            p_Var.Result = Convert.ToInt32(param[0].Value);
            return p_Var.Result;
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


    #region Function to get FileName for Tariff

    public DataSet getFileNameTariff(TariffOB objNew)
    {
        try
        {
            ncmdbObject.AddParameter("@ID", objNew.TempLinkId);
            ncmdbObject.AddParameter("@Status", objNew.StatusId);
            return ncmdbObject.ExecuteDataSet("ASP_GetFileNameTariff");
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
