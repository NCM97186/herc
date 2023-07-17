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
public class RtiDL
{
    #region Default constructors declaration zone

    public RtiDL()
	{

    }

    #endregion

    //Area for all the variables declaration zone

    #region Data declaration zone

    NCMdbAccess ncmdbObject = new NCMdbAccess();
    Project_Variables p_var = new Project_Variables();

    #endregion

    //End

    //Area for all the functions to insert, update and delete

    #region Function to insert RTI records into temp table

    public int insertUpdateTempRTI(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", rtiObject.ActionType));
            ncmdbObject.Parameters.Add(new SqlParameter("@Ref_No", rtiObject.RefNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Deptt_Id", rtiObject.DepttId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", rtiObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Name", rtiObject.ApplicantName));
            
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Email", rtiObject.ApplicantEmail));
            ncmdbObject.Parameters.Add(new SqlParameter("@Application_Date", rtiObject.ApplicationDate));
           
            ncmdbObject.Parameters.Add(new SqlParameter("@Subject", rtiObject.subject));
            ncmdbObject.Parameters.Add(new SqlParameter("@RTI_Status_Id", rtiObject.RTIStatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", rtiObject.StatusId));
          
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", rtiObject.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Inserted_By", rtiObject.InsertedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Last_Updated_By", rtiObject.LastUpdatedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RTI_Id", rtiObject.TempRTIId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Old_RTI_Id", rtiObject.OldRTIId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Mobile_No", rtiObject.ApplicantMobileNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Phone_No", rtiObject.ApplicantPhoneNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Fax_No", rtiObject.ApplicantFaxNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Address", rtiObject.ApplicantAddress));
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal", rtiObject.appeal));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderOne", rtiObject.PlaceHolderOne));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderTwo", rtiObject.PlaceHolderTwo));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderThree", rtiObject.PlaceHolderThree));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderFour", rtiObject.PlaceHolderFour));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderFive", rtiObject.PlaceHolderFive));
            ncmdbObject.AddParameter("@MetaKeyWords", rtiObject.MetaKeyWords);
            ncmdbObject.AddParameter("@MetaDescriptions", rtiObject.MetaDescription);
            ncmdbObject.AddParameter("@MetaTitle", rtiObject.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", rtiObject.MetaKeyLanguage);
            ncmdbObject.AddParameter("@IPAddress", rtiObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_RTI_Insert_Update");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Function to insert/update RTI_FAA records into temp table

    public int insertUpdateTempRTIFAA(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", rtiObject.ActionType));
           // ncmdbObject.Parameters.Add(new SqlParameter("@Ref_No", rtiObject.RefNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Deptt_Id", rtiObject.DepttId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", rtiObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Name", rtiObject.ApplicantName));
            ncmdbObject.Parameters.Add(new SqlParameter("@FAA", rtiObject.Faa));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Email", rtiObject.ApplicantEmail));
            ncmdbObject.Parameters.Add(new SqlParameter("@Application_Date", rtiObject.ApplicationDate));
           
            ncmdbObject.Parameters.Add(new SqlParameter("@Subject", rtiObject.subject));
            ncmdbObject.Parameters.Add(new SqlParameter("@RTI_FAA_Status_Id", rtiObject.RTIFAAStatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@RTI_Id", rtiObject.RTIId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", rtiObject.StatusId));
          
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", rtiObject.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Inserted_By", rtiObject.InsertedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Last_Updated_By", rtiObject.LastUpdatedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RTI_FAA_Id", rtiObject.TempRTIFAAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Old_RTI_FAA_Id", rtiObject.OldRTIFAAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Mobile_No", rtiObject.ApplicantMobileNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Phone_No", rtiObject.ApplicantPhoneNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Fax_No", rtiObject.ApplicantFaxNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Address", rtiObject.ApplicantAddress));
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal", rtiObject.appeal));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderOne", rtiObject.PlaceHolderOne));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderTwo", rtiObject.PlaceHolderTwo));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderThree", rtiObject.PlaceHolderThree));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderFour", rtiObject.PlaceHolderFour));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderFive", rtiObject.PlaceHolderFive));
            ncmdbObject.AddParameter("@MetaKeyWords", rtiObject.MetaKeyWords);
            ncmdbObject.AddParameter("@MetaDescriptions", rtiObject.MetaDescription);
            ncmdbObject.AddParameter("@MetaTitle", rtiObject.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", rtiObject.MetaKeyLanguage);
            ncmdbObject.AddParameter("@IPAddress", rtiObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_RTI_FAA_Insert_Update");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Function To insert rti into web final table

    public int InsertRtiIntoWeb(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Temp_RTI_Id", rtiObject.TempRTIId);
            ncmdbObject.AddParameter("@UserID",rtiObject.userID);
            ncmdbObject.AddParameter("@IPAddress", rtiObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Web_RTI_Insert_Update");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Function to update rti status

    public int RtiStatusUpdate(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@RTI_Id", rtiObject.RTIId);
            ncmdbObject.AddParameter("@RTI_Status_Id", rtiObject.RTIStatusId);
            ncmdbObject.ExecuteNonQuery("ASP_RTI_UpdatePetitionStatus");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    //Area for all the user defined functions to display records

    #region Function to get rti reference numbers(Conditional)

    public DataSet getReferenceNumber(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Ref_No", rtiObject.RefNo));
            ncmdbObject.AddParameter("@year", rtiObject.year);
            ncmdbObject.AddParameter("@lang_Id",rtiObject.LangId);
            ncmdbObject.AddParameter("@deptt_id", rtiObject.DepttId);
            return ncmdbObject.ExecuteDataSet("ASP_RTI_Reference_Number");
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

    #region Function to get rti reference numbers(Conditional) for edit

    public DataSet getReferenceNumberEdit(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Ref_No", rtiObject.RefNo));
            ncmdbObject.AddParameter("@year", rtiObject.year);
            ncmdbObject.AddParameter("@RTI_Id", rtiObject.RTIId);
            ncmdbObject.AddParameter("@deptt_id", rtiObject.DepttId);
            return ncmdbObject.ExecuteDataSet("ASP_RTI_Reference_NumberEdit");
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

    #region Function to get temporary rti records(Conditional)

    public DataSet getTempRTIRecords(PetitionOB rtiObject, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RTI_Id", rtiObject.TempRTIId));
            ncmdbObject.AddParameter("@DepartmentID", rtiObject.DepttId);
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", rtiObject.StatusId));
            ncmdbObject.AddParameter("@PageIndex", rtiObject.PageIndex);
            ncmdbObject.AddParameter("@PageSize", rtiObject.PageSize);
            ncmdbObject.AddParameter("@year", rtiObject.year);
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", rtiObject.LangId));
            p_var.dSet = ncmdbObject.ExecuteDataSet("ASP_Tmp_RTI_Display");
            catValue = Convert.ToInt32(param[0].Value);
            return p_var.dSet;
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

    #region Function to get Temp rti record for edit

    public DataSet getRtiRecordForEdit(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RTI_Id", rtiObject.TempRTIId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", rtiObject.StatusId));
            return ncmdbObject.ExecuteDataSet("ASP_Tmp_RTI_DisplayForEdit");
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

    #region Function to update the temporary rti status

    public int updateRtiStatus(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", rtiObject.StatusId);
            ncmdbObject.AddParameter("@Temp_RTI_Id", rtiObject.TempRTIId);
            ncmdbObject.AddParameter("@UserID", rtiObject.userID);
            ncmdbObject.AddParameter("@IPAddress", rtiObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_tmp_RTI_Change_status");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Function to update the temporary rti-faa status

    public int updateRtiFAAStatus(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", rtiObject.StatusId);
            ncmdbObject.AddParameter("@UserID", rtiObject.userID);
            ncmdbObject.AddParameter("@IPAddress", rtiObject.IpAddress);
            ncmdbObject.AddParameter("@Temp_RTI_FAA_Id", rtiObject.TempRTIFAAId);
            ncmdbObject.ExecuteNonQuery("ASP_tmp_RTIFAA_Change_status");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Function to update the rti status

    public int modifyRtiStatus(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@RTI_Status_Id", rtiObject.RTIStatusId);
            ncmdbObject.AddParameter("@Filename", rtiObject.FileName);
            ncmdbObject.AddParameter("@RTI_Id", rtiObject.RTIId);
            // 23 Nov
            ncmdbObject.AddParameter("@MemoNo",rtiObject.MemoNo);
            ncmdbObject.AddParameter("@Date",rtiObject.Date);
            ncmdbObject.AddParameter("@TransferAuthority", rtiObject.TransferAuthority);
            ncmdbObject.AddParameter("@IPAddress", rtiObject.IpAddress);
            ncmdbObject.Parameters.Add(new SqlParameter("@UserID", rtiObject.LastUpdatedBy));
            ncmdbObject.ExecuteNonQuery("ASP_RTI_UpdatePetitionStatus");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Function to get RTI id from temp table for comparision

    public DataSet get_ID_For_Compare(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RTI_id", rtiObject.RTIId));
            return ncmdbObject.ExecuteDataSet("ASP_Web_RTI_Id");
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

    #region Function to get rti id from rti table

    public DataSet GetRtiid(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RTI_Id", rtiObject.RTIId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_rtiid_from_Tmp_Final");
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

    #region Function to get rti status

    public DataSet getRtiStatus()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("GetRtiStatus");
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

    #region Function to get rti status during edit

    public DataSet getRtiStatusDuringEdit()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("GetRtiStatusDuringEdit");
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


    #region Function to get rti status FOF SAA

    public DataSet getRtiStatus_SAA()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("GetRtiStatus_SAA");
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

    #region Function to get rti id from temp_Rti_FAA table

    public DataSet getRtiIDFromTempFinalRti(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RTI_Id", rtiObject.RTIId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_RTI_Id_from_Tmp_Final");
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

    #region Function to get Temp rti first appellate authority records

    public DataSet getRtiFaaTempRecords(PetitionOB rtiObject, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RTI_FAA_Id", rtiObject.TempRPId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", rtiObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", rtiObject.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@DepartmentID", rtiObject.DepttId));
            ncmdbObject.AddParameter("@PageIndex", rtiObject.PageIndex);
            ncmdbObject.AddParameter("@PageSize", rtiObject.PageSize);
            ncmdbObject.AddParameter("@year", rtiObject.year);
            p_var.dSet= ncmdbObject.ExecuteDataSet("ASP_Tmp_RTI_FAA_Display");
            catValue = Convert.ToInt32(param[0].Value);
            return p_var.dSet;
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

    #region Function to get RTI_FAA_Id from temp table for comparision

    public DataSet getIdForrtiFAA_Comparison(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("RTI_FAA_Id", rtiObject.RTIFAAId));
            return ncmdbObject.ExecuteDataSet("ASP_Web_rti_faa_Id");
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

    #region Function to get Temp rti-faa record for edit

    public DataSet getRtiFAARecordForEdit(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RTI_FAA_Id", rtiObject.TempRTIFAAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", rtiObject.StatusId));
            return ncmdbObject.ExecuteDataSet("ASP_Tmp_RTIFAA_DisplayForEdit");
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

    #region Function To insert rti-faa into web final table

    public int InsertRtiFAAIntoWeb(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Temp_RTI_FAA_Id", rtiObject.TempRTIFAAId);
            ncmdbObject.AddParameter("@UserID", rtiObject.userID);
            ncmdbObject.AddParameter("@IPAddress", rtiObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Web_RTI_FAA_Insert_Update");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Function to update the rti-FAA status

    public int modifyRtiFAAStatus(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@RTI_FAA_Status_Id", rtiObject.RTIFAAStatusId);
            ncmdbObject.AddParameter("@Filename", rtiObject.FileName);
            ncmdbObject.AddParameter("@RTI_FAA_Id", rtiObject.RTIFAAId);
            // 23 Nov
            ncmdbObject.AddParameter("@MemoNo", rtiObject.MemoNo);
            ncmdbObject.AddParameter("@Date", rtiObject.Date);
            ncmdbObject.AddParameter("@TransferAuthority", rtiObject.TransferAuthority);
            ncmdbObject.AddParameter("@IPAddress", rtiObject.IpAddress);
            ncmdbObject.AddParameter("@UserID", rtiObject.userID);
            ncmdbObject.ExecuteNonQuery("ASP_RTI_FAA_UpdateStatus");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Function to get rtifaa id from temp_Rti_SAA table

    public DataSet getRtiFAAIDFromTempFinalRti(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RTI_FAA_Id", rtiObject.RTIFAAId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_RTIFAA_Id_from_Tmp_Final");
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

    #region Function to insert/update RTI_SAA records into temp table

    public int insertUpdateTempRTISAA(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", rtiObject.ActionType));
            // ncmdbObject.Parameters.Add(new SqlParameter("@Ref_No", rtiObject.RefNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Deptt_Id", rtiObject.DepttId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", rtiObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Name", rtiObject.ApplicantName));
            ncmdbObject.Parameters.Add(new SqlParameter("@SAA", rtiObject.Saa));
            ncmdbObject.Parameters.Add(new SqlParameter("@SAARef", rtiObject.SaaRef_No));

            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Email", rtiObject.ApplicantEmail));
            ncmdbObject.Parameters.Add(new SqlParameter("@Application_Date", rtiObject.ApplicationDate));

            ncmdbObject.Parameters.Add(new SqlParameter("@Subject", rtiObject.subject));
            ncmdbObject.Parameters.Add(new SqlParameter("@RTI_SAA_Status_Id", rtiObject.RTISAAStatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@RTI_FAA_Id", rtiObject.RTIFAAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", rtiObject.StatusId));

            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", rtiObject.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Inserted_By", rtiObject.InsertedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Last_Updated_By", rtiObject.LastUpdatedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RTI_SAA_Id", rtiObject.TempRTISAAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Old_RTI_SAA_Id", rtiObject.OldRTISAAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Mobile_No", rtiObject.ApplicantMobileNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Phone_No", rtiObject.ApplicantPhoneNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Fax_No", rtiObject.ApplicantFaxNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Address", rtiObject.ApplicantAddress));
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal", rtiObject.appeal));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderOne", rtiObject.PlaceHolderOne));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderTwo", rtiObject.PlaceHolderTwo));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderThree", rtiObject.PlaceHolderThree));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderFour", rtiObject.PlaceHolderFour));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderFive", rtiObject.PlaceHolderFive));
            ncmdbObject.AddParameter("@MetaKeyWords", rtiObject.MetaKeyWords);
            ncmdbObject.AddParameter("@MetaDescriptions", rtiObject.MetaDescription);
            ncmdbObject.AddParameter("@MetaTitle", rtiObject.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", rtiObject.MetaKeyLanguage);
            ncmdbObject.AddParameter("@IPAddress", rtiObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_RTI_SAA_Insert_Update");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Function to get Temp rti second appellate authority records

    public DataSet getRtiSaaTempRecords(PetitionOB rtiObject, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RTI_SAA_Id", rtiObject.TempRTISAAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", rtiObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", rtiObject.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", rtiObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@DepartmentID", rtiObject.DepttId));
            ncmdbObject.AddParameter("@PageIndex", rtiObject.PageIndex);
            ncmdbObject.AddParameter("@PageSize", rtiObject.PageSize);
            p_var.dSet = ncmdbObject.ExecuteDataSet("ASP_Tmp_RTI_SAA_Display");
            catValue = Convert.ToInt32(param[0].Value);
            return p_var.dSet;
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

    #region Function to update the temporary rti-saa status

    public int updateRtiSAAStatus(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", rtiObject.StatusId);
            ncmdbObject.AddParameter("@Temp_RTI_SAA_Id", rtiObject.TempRTISAAId);
            ncmdbObject.AddParameter("@UserID", rtiObject.userID);
            ncmdbObject.AddParameter("@IPAddress", rtiObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_tmp_RTISAA_Change_status");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Function To insert rti-saa into web final table

    public int InsertRtiSAAIntoWeb(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Temp_RTI_SAA_Id", rtiObject.TempRTISAAId);
            ncmdbObject.AddParameter("@UserID", rtiObject.userID);
            ncmdbObject.AddParameter("@IPAddress", rtiObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Web_RTI_SAA_Insert_Update");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Function to get RTI_SAA_Id from temp table for comparision

    public DataSet getIdForrtiSAA_Comparison(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("RTI_SAA_Id", rtiObject.RTISaaId));
            return ncmdbObject.ExecuteDataSet("ASP_Web_rti_Saa_Id");
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

    #region Function to get Temp rti-saa record for edit

    public DataSet getRtiSAARecordForEdit(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RTI_SAA_Id", rtiObject.TempRTISAAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", rtiObject.StatusId));
            return ncmdbObject.ExecuteDataSet("ASP_Tmp_RTISAA_DisplayForEdit");
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

    #region Function to update the rti-SAA status

    public int modifyRtiSAAStatus(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@RTI_SAA_Status_Id", rtiObject.RTISAAStatusId);
            ncmdbObject.AddParameter("@Filename", rtiObject.FileName);
            ncmdbObject.AddParameter("@RTI_SAA_Id", rtiObject.RTISaaId);
            // 23 Nov
            ncmdbObject.AddParameter("@MemoNo", rtiObject.MemoNo);
            ncmdbObject.AddParameter("@Date", rtiObject.Date);
            ncmdbObject.AddParameter("@TransferAuthority", rtiObject.TransferAuthority);
            ncmdbObject.AddParameter("@Url",rtiObject.JudgementLink);
            ncmdbObject.AddParameter("@IPAddress", rtiObject.IpAddress);
            ncmdbObject.AddParameter("@UserID", rtiObject.userID);
            ncmdbObject.ExecuteNonQuery("ASP_RTI_SAA_UpdateStatus");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    //All the functions are for deleting records

    #region Function to get rti id during deleting record from table

    public DataSet getRtiIDForDelete(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RTI_Id", rtiObject.RTIId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_RTI_ID_From_RTI_FAA");
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

    #region Function to get rti faa id during deleting record from table

    public DataSet getRtiFAAIDForDelete(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RTI_FAA_Id", rtiObject.RTIFAAId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_RTIFAA_Id_from_Tmp_Final");
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

    #region Functiont to delete rti either from temp or final

    public int Delete_RTI(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            ncmdbObject.AddParameter("@Temp_RTI_Id", rtiObject.TempRTIId);
            ncmdbObject.AddParameter("@Status_ID", rtiObject.StatusId);
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_RTI_Delete");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Functiont to delete rti-faa either from temp or final

    public int Delete_RTIFAA(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            ncmdbObject.AddParameter("@Temp_RTI_FAA_Id", rtiObject.TempRTIFAAId);
            ncmdbObject.AddParameter("@Status_ID", rtiObject.StatusId);
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_RTIFAA_Delete");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Functiont to delete rti-saa either from temp or final

    public int Delete_RTISAA(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            ncmdbObject.AddParameter("@Temp_RTI_SAA_Id", rtiObject.TempRTISAAId);
            ncmdbObject.AddParameter("@Status_ID", rtiObject.StatusId);
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_RTISAA_Delete");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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


    //Funtion to get RTI user side from Web_Rti
    public DataSet Get_RTI(PetitionOB pt_obj, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", pt_obj.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@DepartmentID", pt_obj.DepttId));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageIndex", pt_obj.PageIndex));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageSize", pt_obj.PageSize));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", pt_obj.year));

            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_get_RTI_final");

            catValue = Convert.ToInt32(param[0].Value);
            return p_var.dSet;
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
    //end
    // check RTI Found In RTI_FAA
    public void Check_RTI_FAA(int RTI_ID, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            ncmdbObject.Parameters.Add(new SqlParameter("@RTI_Id", RTI_ID));
            ncmdbObject.ExecuteDataSet("USP_check_RTI_FAA");
            catValue = Convert.ToInt32(param[0].Value);

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

    //end
    // Function Get FAA RTI
    public DataSet Get_FAA_RTI(PetitionOB pt_obj)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RTI", pt_obj.RTIId));
            ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", pt_obj.LangId));
            return ncmdbObject.ExecuteDataSet("USP_GetFAA_RTI");

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
    //end
    // function to check FAA to SAA Record Found
    public void Check_RTI_FAA_SAA(int RTI_ID, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            ncmdbObject.Parameters.Add(new SqlParameter("@RTI_Id", RTI_ID));
            ncmdbObject.ExecuteDataSet("USP_check_RTI_FAA_SAA");
            catValue = Convert.ToInt32(param[0].Value);

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

    //end
    // fuction to get SAA Record by FAA ID

    public DataSet Get_Saa_Faa(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@faa", rtiObject.RTIFAAId));
            return ncmdbObject.ExecuteDataSet("USP_get_SAA");
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
    //end

    //Funtion to get Previou year RTI
    public DataSet Get_PrevYear_RTI(PetitionOB pt_obj, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", pt_obj.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@dept_id", pt_obj.DepttId));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageIndex", pt_obj.PageIndex));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageSize", pt_obj.PageSize));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", pt_obj.year));

            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_get_prev_YearRTI");

            catValue = Convert.ToInt32(param[0].Value);
            return p_var.dSet;
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
    //end


    #region function to insert the status in Mst_Status table

    public int Insert_Status(PetitionOB rtiObject, out int id)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Status_Type_Id", rtiObject.StatusId);
            ncmdbObject.AddParameter("@Status", rtiObject.subject);
            p_var.Result = ncmdbObject.ExecuteNonQuery("USP_insert_Mst_Status");
            id = Convert.ToInt32(param[0].Value);
            return id;
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


    #region Function to get Status

    public DataSet get_MstStatus(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Status", petObject.subject));
            return ncmdbObject.ExecuteDataSet("ASP_get_MstStatus");
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

    #region function to get rti year

    public DataSet GetRTIYear()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("USP_GetRTIYear");
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

    //function to Search RTI

    //public DataSet Serch_RTI(PetitionOB pt_obj, string strsearchcondition)
    //{
    //    try
    //    {

    //        ncmdbObject.AddParameter("@year", pt_obj.year);
    //        ncmdbObject.AddParameter("@PageIndex", pt_obj.PageIndex);
    //        ncmdbObject.AddParameter("@PageSize", pt_obj.PageSize);
    //        ncmdbObject.AddParameter("@strquery", strsearchcondition);
    //        ncmdbObject.AddParameter("@deptt_id", pt_obj.DepttId);

    //        p_var.dSet = ncmdbObject.ExecuteDataSet("USP_RTI_SEARCH");

    //        return p_var.dSet;

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

    public DataSet Serch_RTI(PetitionOB pt_obj,out int catValue)
    {
        try
        {

			SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            ncmdbObject.AddParameter("@year", pt_obj.year);
            ncmdbObject.AddParameter("@PageIndex", pt_obj.PageIndex);
            ncmdbObject.AddParameter("@PageSize", pt_obj.PageSize);
            ncmdbObject.AddParameter("@Applicant_Name", pt_obj.ApplicantName);
            ncmdbObject.AddParameter("@Subject", pt_obj.subject);
            ncmdbObject.AddParameter("@Ref_No", pt_obj.RefNo);
            //ncmdbObject.AddParameter("@year", pt_obj.year);
            ncmdbObject.AddParameter("@deptt_id", pt_obj.DepttId);

            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_RTI_SEARCH");
			catValue = Convert.ToInt32(param[0].Value);
            return p_var.dSet;

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

    //End



    public DataSet Get_RTIById(PetitionOB pt_obj)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@lang_id", pt_obj.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@dept_id", pt_obj.DepttId));

            ncmdbObject.AddParameter("@Rti_Id",pt_obj.RTIId);
            return ncmdbObject.ExecuteDataSet("USP_get_RTIById");
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


    #region Function to get rti reference numbers(Conditional) of FAA

    public DataSet getReferenceNumberFAA(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Ref_No", rtiObject.RefNo));
            ncmdbObject.AddParameter("@year", rtiObject.year);
            ncmdbObject.AddParameter("@lang_Id", rtiObject.LangId);
            ncmdbObject.AddParameter("@deptt_id", rtiObject.DepttId);
            return ncmdbObject.ExecuteDataSet("ASP_RTI_Reference_NumberFAA");
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

    //4 jan 2013

    #region Function to get rti reference numbers(Conditional) of SAA

    public DataSet getReferenceNumberSAA(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Ref_No", rtiObject.RefNo));
            ncmdbObject.AddParameter("@year", rtiObject.year);
            ncmdbObject.AddParameter("@lang_Id", rtiObject.LangId);
            ncmdbObject.AddParameter("@deptt_id", rtiObject.DepttId);
            return ncmdbObject.ExecuteDataSet("ASP_RTI_Reference_NumberSAA");
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


    #region Function to get rti reference numbers of SAA Edit

    public DataSet getReferenceNumberSAAEdit(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Ref_No", rtiObject.RefNo));
            ncmdbObject.AddParameter("@year", rtiObject.year);
            ncmdbObject.AddParameter("@lang_Id", rtiObject.LangId);
            ncmdbObject.AddParameter("@RTI_SAA_Id",rtiObject.RTISaaId);
            ncmdbObject.AddParameter("@deptt_id", rtiObject.DepttId);
            return ncmdbObject.ExecuteDataSet("ASP_RTI_Reference_NumberSAAEdit");
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



    #region Function to get rti reference numbers of FAA Edit

    public DataSet getReferenceNumberFAAEdit(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Ref_No", rtiObject.RefNo));
            ncmdbObject.AddParameter("@year", rtiObject.year);
            ncmdbObject.AddParameter("@lang_Id", rtiObject.LangId);
            ncmdbObject.AddParameter("@RTI_FAA_Id", rtiObject.RTIFAAId);
            ncmdbObject.AddParameter("@deptt_id", rtiObject.DepttId);
            return ncmdbObject.ExecuteDataSet("ASP_RTI_Reference_NumberFAAEdit");
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


    #region function to get year of RTI

    public DataSet GetYearRTI(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.AddParameter("@DepttId",rtiObject.DepttId);
            return ncmdbObject.ExecuteDataSet("ASP_GetYearRTI");
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

    #region function to get year of RTI Admin

    public DataSet GetYearRTI_Admin()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYearRTI_Admin");
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

    #region function to get year of RTI FAA

    public DataSet GetYearRTIFAA()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYearRTI_FAA");
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

    #region function to get year of RTI FAA admin

    public DataSet GetYearRTIFAA_admin()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYearRTI_FAA_Admin");
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

    #region function to get year of RTI SAA

    public DataSet GetYearRTISAA()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYearRTI_SAA");
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

    #region function to get year of RTI SAA admin

    public DataSet GetYearRTISAAAdmin()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYearRTI_SAA_Admin");
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

    #region Function to get rti reference numbers By Year

    public DataSet getReferenceNumberByYear(PetitionOB rtiObject)
    {
        try
        {
            //ncmdbObject.Parameters.Add(new SqlParameter("@Ref_No", rtiObject.RefNo));
            ncmdbObject.AddParameter("@year", rtiObject.year);
            ncmdbObject.AddParameter("@DepttId", rtiObject.DepttId);
            return ncmdbObject.ExecuteDataSet("ASP_GetReferenceNumber");
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



    #region Function to get temporary rti records(Conditional) by ID

    public DataSet getTempRTIRecordsBYID(PetitionOB rtiObject)
    {
        try
        {
            
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RTI_Id", rtiObject.TempRTIId));
            
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", rtiObject.StatusId));
            return ncmdbObject.ExecuteDataSet("ASP_Tmp_RTI_DisplaybyId");
           
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


    #region Function to get temporary rtiFAA records(Conditional) by ID

    public DataSet getTempRTIFAARecordsBYID(PetitionOB rtiObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RTI_FAA_Id", rtiObject.RTIFAAId));

            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", rtiObject.StatusId));
            return ncmdbObject.ExecuteDataSet("ASP_Tmp_RTIFAA_DisplaybyId");

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


    #region Function to get temporary rtiSAA records(Conditional) by ID

    public DataSet getTempRTISAARecordsBYID(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RTISAA_Id", rtiObject.RTISaaId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", rtiObject.StatusId));
            return ncmdbObject.ExecuteDataSet("ASP_Tmp_RTISAA_DisplaybyId");

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


    public DataSet Get_RTIFAACurrent(PetitionOB pt_obj, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", pt_obj.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@DepartmentID", pt_obj.DepttId));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageIndex", pt_obj.PageIndex));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageSize", pt_obj.PageSize));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", pt_obj.year));

            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_get_RTIFAA_final");

            catValue = Convert.ToInt32(param[0].Value);
            return p_var.dSet;
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
    public DataSet Get_RTISAACurrent(PetitionOB pt_obj, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", pt_obj.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@DepartmentID", pt_obj.DepttId));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageIndex", pt_obj.PageIndex));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageSize", pt_obj.PageSize));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", pt_obj.year));

            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_get_RTISAA_final");

            catValue = Convert.ToInt32(param[0].Value);
            return p_var.dSet;
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


    #region function to get year of RTI FAA for previous

    public DataSet GetYearRTIFAAPrevious(PetitionOB pt_obj)
    {
        try
        {
            ncmdbObject.AddParameter("@DepttId", pt_obj.DepttId);
            return ncmdbObject.ExecuteDataSet("ASP_GetYearRTI_FAAPrevious");
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


    #region function to get year of RTI SAA for previous

    public DataSet GetYearRTISAAPrevious(PetitionOB pt_obj)
    {
        try
        {
            ncmdbObject.AddParameter("@DepttId", pt_obj.DepttId);
            return ncmdbObject.ExecuteDataSet("ASP_GetYearRTI_SAAPrevious");
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

    //RTI SAA front end details

    #region Function to get temporary rtiSAA records(Conditional) by ID

    public DataSet getRTISAABYID(PetitionOB rtiObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RTISAA_Id", rtiObject.RTISaaId));
            
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", rtiObject.StatusId));
            return ncmdbObject.ExecuteDataSet("usp_RTISAA_DisplaybyId");

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



    #region Function To Update RTI web File to null

    public int updateRTIweb(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@RTI_Id", rtiObject.RTIId);
            ncmdbObject.AddParameter("@ActionType",rtiObject.ActionType);
            ncmdbObject.ExecuteNonQuery("ASP_UpdateRTIFile");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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



    #region Function to update the  rti status for delete

    public int updateRtiStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            //ncmdbObject.AddParameter("@Status_Id", rtiObject.StatusId);
            ncmdbObject.AddParameter("@Temp_RTI_Id", rtiObject.RTIId);
            ncmdbObject.ExecuteNonQuery("ASP_tmp_RTI_ChangestatusDelete");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Function to update the  rti FAA status for delete

    public int updateRtiFAAStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            //ncmdbObject.AddParameter("@Status_Id", rtiObject.StatusId);
            ncmdbObject.AddParameter("@Temp_RTIFAA_Id", rtiObject.RTIFAAId);
            ncmdbObject.ExecuteNonQuery("ASP_tmp_RTIFAA_ChangestatusDelete");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    #region Function to update the  rti SAA status for delete

    public int updateRtiSAAStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            //ncmdbObject.AddParameter("@Status_Id", rtiObject.StatusId);
            ncmdbObject.AddParameter("@Temp_RTISAA_Id", rtiObject.RTISaaId);
            ncmdbObject.ExecuteNonQuery("ASP_tmp_RTISAA_ChangestatusDelete");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
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

    //Functions to get email ids to send emails

    #region function to get email ids to send email.

    public DataSet getEmailIdToSendRTIEmail(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.AddParameter("@rtiId", rtiObject.TempRTIId);
            return ncmdbObject.ExecuteDataSet("asp_GetRTIEmails");
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
    #region

    public DataSet getMobileToSendRTIEmail(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.AddParameter("@rtiId", rtiObject.TempRTIId);
            return ncmdbObject.ExecuteDataSet("asp_GetRTIMobiles");
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


    #region function to get email ids to send email.

    public DataSet getEmailIdToSendRTIFAAEmail(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.AddParameter("@rtiFAAId", rtiObject.TempRTIFAAId);
            return ncmdbObject.ExecuteDataSet("asp_GetRTIFAAEmails");
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

    #region function to get mobile numbers to send sms.

    public DataSet getMobileNumberToSendRTIFAASms(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.AddParameter("@rtiFAAId", rtiObject.TempRTIFAAId);
            return ncmdbObject.ExecuteDataSet("asp_GetRTIFAAMobiles");
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

    #region function to get email ids to send email.

    public DataSet getEmailIdToSendRTISAAEmail(PetitionOB rtiObject)
    {
        try
        {
            ncmdbObject.AddParameter("@rtiSAAId", rtiObject.TempRTISAAId);
            return ncmdbObject.ExecuteDataSet("asp_GetRTISAAEmails");
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
}
