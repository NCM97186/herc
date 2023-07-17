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

public class PetitionDL
{
    //Area for all the constructors declaration

    #region Default constructor zone

    public PetitionDL()
    {

    }

    #endregion

    //End

    //Area for all the data declaration

    #region Data declaration zone

    NCMdbAccess ncmdbObject = new NCMdbAccess();
    Project_Variables p_Var = new Project_Variables();

    #endregion

    //End
       
    //Area for all the  functions to display data

    #region Function to get PRO numbers(Conditional)

    public DataSet getPRO_Number(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@PRO_No", petObject.PRONo));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", petObject.year));
            return ncmdbObject.ExecuteDataSet("ASP_Petition_get_PRO_Number");
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

    #region Function to get PRO numbers in edit mode

    public DataSet getPRO_Number_In_EditMode(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@PRO_No", petObject.PRONo));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", petObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Petition_Id", petObject.TempPetitionId));
            return ncmdbObject.ExecuteDataSet("ASP_Petition_get_PRO_Number_In_EditMode");
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

    #region Function to get Temp petition(Conditional)

    public DataSet get_Temp_Petition_Records(PetitionOB petObject, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Petition_Id", petObject.TempPetitionId));
            //ncmdbObject.AddParameter("@DepartmentID", petObject.DepttId);
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", petObject.StatusId));
            ncmdbObject.AddParameter("@PageIndex", petObject.PageIndex);
            ncmdbObject.AddParameter("@PageSize", petObject.PageSize);
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            ncmdbObject.AddParameter("@Year", petObject.year);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_Tmp_Petiton_Display");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to get temp/final petition schedule of hearing(Conditional)

    public DataSet getPetition_ScheduleOfHearing(PetitionOB petObject, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@soh_id_Temp", petObject.SohTempID));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", petObject.StatusId));
            ncmdbObject.AddParameter("@year", petObject.year);
            ncmdbObject.AddParameter("@Petition_Type", petObject.AppealStatusId);
            ncmdbObject.AddParameter("@PageIndex", petObject.PageIndex);
            ncmdbObject.AddParameter("@PageSize", petObject.PageSize);
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            ncmdbObject.AddParameter("@Deptt_Id", petObject.DepttId);
            ncmdbObject.AddParameter("@AppealPetition", petObject.keyword);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ScheduleOfHearingDisplay");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to get temp/final appeal schedule of hearing(Conditional)

    public DataSet getAppeal_ScheduleOfHearing(PetitionOB petObject, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@soh_id_Temp", petObject.SohTempID));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", petObject.StatusId));
            ncmdbObject.AddParameter("@year", petObject.year);
            ncmdbObject.AddParameter("@PageIndex", petObject.PageIndex);
            ncmdbObject.AddParameter("@PageSize", petObject.PageSize);
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            ncmdbObject.AddParameter("@Deptt_Id", petObject.DepttId);
            ncmdbObject.AddParameter("@Petition_Type", petObject.keyword);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ScheduleOfHearingAppealDisplay");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to get Temp petition(For Edit)

    public DataSet get_Temp_Petition_Records_Edit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Petition_Id", petObject.TempPetitionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            return ncmdbObject.ExecuteDataSet("ASP_Tmp_Petiton_Display_Edit");
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

    #region Function to get petition id from temp table for comparision

    public DataSet get_ID_For_Compare(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_id", petObject.PetitionId));
            return ncmdbObject.ExecuteDataSet("ASP_Web_Petition_Petition_Id");
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

    #region Function to get petition id from temp_Review_Petition table

    public DataSet get_PetitionID_From_Temp_PetReview(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_id", petObject.PetitionId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_Petition_Id_from_Tmp_Final");
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

    #region Function to get Review and appeal number during deleting record from petition table

    public DataSet get_Review_Appeal_Number_for_Delete(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_id", petObject.PetitionId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_Review_AppealNumber");
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

    #region Function to get uploader id which contains files

    public bool getUploaderID(PetitionOB petitionObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@PetitionID", petitionObject.PetitionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_ID", petitionObject.StatusId));
            p_Var.Result = Convert.ToInt32(ncmdbObject.ExecuteScalar("ASP_CheckFileAvailability_Petition"));
            if (p_Var.Result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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

    #region function to get SOH information of Previous Year

    public DataSet Get_PreviousYearSOH(PetitionOB obj_PetOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@year", obj_PetOB.year);
            ncmdbObject.AddParameter("@PageIndex", obj_PetOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_PetOB.PageSize);
            ncmdbObject.AddParameter("@Lang_Id", obj_PetOB.LangId);
            ncmdbObject.AddParameter("@deptt_id", obj_PetOB.DepttId);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_Get_PreviousSOH");
            catValue = Convert.ToInt16(param[0].Value);
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

	#region function to get SOH information of Next Year

    public DataSet Get_NextYearSOH(PetitionOB obj_PetOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@year", obj_PetOB.year);
            ncmdbObject.AddParameter("@PageIndex", obj_PetOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_PetOB.PageSize);
            ncmdbObject.AddParameter("@Lang_Id", obj_PetOB.LangId);
            ncmdbObject.AddParameter("@deptt_id", obj_PetOB.DepttId);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_Get_NextSOH");
            catValue = Convert.ToInt16(param[0].Value);
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

    //End

    //Area for all the functions to insert

    #region Function to insert petition into temp

    public Int32 insertPetition(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", petObject.ActionType));
            ncmdbObject.Parameters.Add(new SqlParameter("@PRO_No", petObject.PRONo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Deptt_Id", petObject.DepttId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", petObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Name", petObject.PetitionerName));
            //ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Contact_No", petObject.PetitionerContactNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Email", petObject.PetitionerEmail));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent", petObject.ResPondent));
            ncmdbObject.Parameters.Add(new SqlParameter("@Subject", petObject.subject));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_Status_Id", petObject.PetitionStatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            // ncmdbObject.Parameters.Add(new SqlParameter("@File_Name", petObject.FileName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Link_URL", petObject.LinkURL));
            ncmdbObject.Parameters.Add(new SqlParameter("@Review", petObject.ReView));
            //ncmdbObject.Parameters.Add(new SqlParameter("@Public_Notice", petObject.PublicNotice));
            ncmdbObject.Parameters.Add(new SqlParameter("@SOH", petObject.Soh));
            ncmdbObject.Parameters.Add(new SqlParameter("@Old_Petition_Id", petObject.OldPetitionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Inserted_By", petObject.InsertedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Petition_Id", petObject.TempPetitionId));

            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Mobile_No", petObject.PetitionerMobileNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Phone_No", petObject.PetitionerPhoneNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Fax_No", petObject.PetitionerFaxNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Address", petObject.PetitionerAddress));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_Date", petObject.PetitionDate));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_File", petObject.PetitionFile));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Name", petObject.RespondentName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Mobile_No", petObject.RespondentMobileNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Phone_No", petObject.RespondentPhone_No));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Fax_No", petObject.RespondentFaxNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Email", petObject.Respondentmail));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Address", petObject.RespondentAddress));

            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderOne", petObject.PlaceHolderOne));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderTwo", petObject.PlaceHolderTwo));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderThree", petObject.PlaceHolderThree));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderFour", petObject.PlaceHolderFour));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderFive", petObject.PlaceHolderFive));

            ncmdbObject.Parameters.Add(new SqlParameter("@Remarks", petObject.Remarks));
            ncmdbObject.AddParameter("@MetaKeyWords", petObject.MetaKeyWords);
            ncmdbObject.AddParameter("@MetaDescription", petObject.MetaDescription);
            ncmdbObject.AddParameter("@MetaTitle", petObject.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", petObject.MetaKeyLanguage);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_Petiton_Insert_Update");
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

    #region Function to insert Connectedpetition

    public Int32 insertConnectedPetition(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@Connected_Petition_id", petObject.ConnectedPetitionID));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_id", petObject.PetitionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", petObject.year));
            ncmdbObject.ExecuteNonQuery("ASP_Connected_Petition_Insert_Update");
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

    #region Function to delete Connectedpetition

    public Int32 deleteConnectedPetition(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_id", petObject.PetitionId));
            ncmdbObject.ExecuteNonQuery("ASP_Connected_Petition_Delete");
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

    #region Function to get connected Petition(For Edit)

    public DataSet get_ConnectedPetition_Edit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_id", petObject.PetitionId));

            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedPetition");
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

    #region Function to get connected Petition(For Edit)

    public DataSet getConnectedPetitionEditNew(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_id", petObject.PetitionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", petObject.year));
            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedPetitionEdit");
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

    #region Function to get connected Petition(For Edit)

    public DataSet getConnectedReviewPetitionEditNew(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", petObject.year));
            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedReviewPetitionEdit");
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

    #region Function To insert Into web_Petiton

    public Int32 ASP_Insert_Web_Petiton(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Temp_Petition_Id", petObject.TempPetitionId);
            ncmdbObject.AddParameter("@UserID", petObject.userID);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Web_Petition_Insert_Update");
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

    //End

    //Area for all the functions to update

    #region Function to update the temp_Petition status

    public Int32 ASP_TempPetition_Update_Status_Id(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", petObject.StatusId);
            ncmdbObject.AddParameter("@Temp_Petition_Id", petObject.TempPetitionId);
            ncmdbObject.AddParameter("@UserID", petObject.userID);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_tmp_Petition_Change_status");
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

    #region Function to update petition status

    public Int32 petitionStatusUpdate(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Petition_id", petObject.PetitionId);
            ncmdbObject.AddParameter("@Petition_Status_Id", petObject.PetitionStatusId);
            ncmdbObject.ExecuteNonQuery("ASP_Petition_UpdatePetitionStatus");
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

    #region Function to update Reviewpetition status

    public Int32 ReviewpetitionStatusUpdate(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@RP_ID", petObject.RPId);
            ncmdbObject.AddParameter("@RP_Status_Id", petObject.PetitionStatusId);
            ncmdbObject.ExecuteNonQuery("ASP_ReviewPetition_UpdateStatus");
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

    #region Function to update Appealpetition status

    public Int32 AppealpetitionStatusUpdate(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@PA_Id", petObject.PAId);
            ncmdbObject.AddParameter("@PA_Status_Id", petObject.PetitionStatusId);
            ncmdbObject.ExecuteNonQuery("ASP_AppealPetition_UpdateStatus");
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

    //End

    //Area for all the functions to delete

    #region Functiont to delete petition either temp or final

    public Int32 Delete_Petition(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            ncmdbObject.AddParameter("@Temp_Petition_Id", petObject.RPId);
            ncmdbObject.AddParameter("@Status_ID", petObject.StatusId);
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_Petiton_Delete");
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

    //End
  
    //Area for all the user defined functions to display data

    #region Function to get RP numbers(Conditional) during insertion

    public DataSet getRP_Number(PetitionOB petObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@RP_No", petObject.RPNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@year",petObject.year));
            return ncmdbObject.ExecuteDataSet("ASP_Review_Petition_get_RP_Number");

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

    #region Function to get RP numbers in edit mode

    public DataSet getRP_Number_In_EditMode(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_No", petObject.RPNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", petObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RP_Id", petObject.TempRPId));
            return ncmdbObject.ExecuteDataSet("ASP_Review_Petition_get_RP_Number_In_EditMode");
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

    #region Function to get Temp petition review(Conditional)

    public DataSet get_Temp_Review_Petition_Records(PetitionOB petObject, out int catValue)
    {
        try
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RP_Id", petObject.TempRPId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageIndex", petObject.PageIndex));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageSize", petObject.PageSize));
            ncmdbObject.AddParameter("@Year", petObject.year);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_Tmp_Review_Petition_Display");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to get Temp petition review(Edit)

    public DataSet get_Temp_Review_Petition_RecordsEdit(PetitionOB petObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RP_Id", petObject.TempRPId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));

            return ncmdbObject.ExecuteDataSet("ASP_Tmp_Review_Petition_DisplayEdit");

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

    #region Function to get petition id from temp table for comparision

    public DataSet get_ID_For_Review_Comparison(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));
            return ncmdbObject.ExecuteDataSet("ASP_Web_Review_Petition_Id");
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

    #region Function to get Appeal id from temp_Appeal_Petition table

    public DataSet get_RpID_From_Temp_PetAppeal(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_RP_Id_from_Tmp_Appeal");
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

    #region Function to get Appeal number during deleting record from review table

    public DataSet getAppeal_Number_for_Delete(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_AppealNumber_from_ReviewNumber");
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

    //Area for all the function to insert

    #region Function to insert petition review in review petition data table

    public Int32 insert_Review_Petition(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", petObject.ActionType));
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_No", petObject.RPNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", petObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Name", petObject.ApplicantName));


            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Contact_No", petObject.ApplicantContactNo));

            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Mobile_No", petObject.ApplicantMobileNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Phone_No", petObject.ApplicantPhoneNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Fax_No", petObject.ApplicantFaxNo));

            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Email", petObject.ApplicantEmail));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent", petObject.ResPondent));
            ncmdbObject.Parameters.Add(new SqlParameter("@Subject", petObject.subject));
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Status_Id", petObject.RPStatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@File_Name", petObject.FileName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Link_URL", petObject.LinkURL));
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal", petObject.appeal));
            ncmdbObject.Parameters.Add(new SqlParameter("@Public_Notice", petObject.PublicNotice));
            ncmdbObject.Parameters.Add(new SqlParameter("@SOH", petObject.Soh));
            ncmdbObject.Parameters.Add(new SqlParameter("@Old_RP_Id", petObject.OldRPId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Inserted_By", petObject.InsertedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Last_Updated_By", petObject.LastUpdatedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RP_Id", petObject.TempRPId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_Id", petObject.PetitionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Address", petObject.PetitionerAddress));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Address", petObject.RespondentAddress));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Mobile_No", petObject.RespondentMobileNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Phone_No", petObject.RespondentPhone_No));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Fax_No", petObject.RespondentFaxNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Email", petObject.Respondentmail));
            ncmdbObject.Parameters.Add(new SqlParameter("@Placeholderfive", petObject.PlaceHolderFive));
            ncmdbObject.Parameters.Add(new SqlParameter("@Placeholderfour", petObject.PlaceHolderFour));
            ncmdbObject.AddParameter("@MetaKeyWords", petObject.MetaKeyWords);
            ncmdbObject.AddParameter("@MetaDescription", petObject.MetaDescription);
            ncmdbObject.AddParameter("@MetaTitle", petObject.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", petObject.MetaKeyLanguage);

            ncmdbObject.AddParameter("@Comments",petObject.Description);
            ncmdbObject.AddParameter("@Petition_Date", petObject.PetitionDate);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_Review_Petition_Insert_Update");
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

    #region Function To insert Into web_Petiton_REVIEW

    public Int32 ASP_Insert_Web_Petiton_REVIEW(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Temp_RP_Id", petObject.TempPetitionId);
            ncmdbObject.AddParameter("@UserID",petObject.userID);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Web_Review_Petition_Insert_Update");
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

    //End

    //Area for all the functions to update

    #region Function to update the temp_Review_Petition status

    public Int32 ASP_TempReview_Petition_Update_Status_Id(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", petObject.StatusId);
            ncmdbObject.AddParameter("@Temp_RP_Id", petObject.TempRPId);

            ncmdbObject.AddParameter("@UserID", petObject.userID);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_Review_Petition_Change_status");
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

    //End

    //Area for all the functions to delete

    #region Functiont to delete petition review either temp or final

    public Int32 Delete_Petition_Review(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            ncmdbObject.AddParameter("@Temp_RP_Id", petObject.RPId);
            ncmdbObject.AddParameter("@Status_ID", petObject.StatusId);
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_Review_Petition_Delete");
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

    //End
    //Area for all the functions to display data

    #region Function to get Appeal numbers(Conditional) during insertion

    public DataSet getAppeal_Number(PetitionOB petObject)
    {
        try
        {
		    ncmdbObject.Parameters.Add(new SqlParameter("@Appeal_No", petObject.AppealNo));
			ncmdbObject.Parameters.Add(new SqlParameter("@year", petObject.year));
            return ncmdbObject.ExecuteDataSet("ASP_Appeal_Petition_get_Appeal_Number_14092018");
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

    #region Function to get appeal numbers in edit mode

    public DataSet getAppeal_Number_In_EditMode(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal_No", petObject.AppealNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", petObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_PA_Id", petObject.TempPAId));
            return ncmdbObject.ExecuteDataSet("ASP_Appeal_Petition_get_Appeal_Number_In_EditMode");
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

    #region Function to get Temp petition appeal(Conditional)

    public DataSet get_Temp_Appeal_Petition_Records(PetitionOB petObject, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_PA_Id", petObject.TempPAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageIndex", petObject.PageIndex));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageSize", petObject.PageSize));
            ncmdbObject.AddParameter("@Year", petObject.year);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_Tmp_Petition_Appeal_Display");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to get Temp petition appeal(Edit)

    public DataSet get_Temp_Appeal_Petition_RecordsEdit(PetitionOB petObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_PA_Id", petObject.TempPAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));

            return ncmdbObject.ExecuteDataSet("ASP_Tmp_Petition_Appeal_DisplayEdit");


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

    #region Function to get petition appeal id from temp table for comparision

    public DataSet get_ID_For_Appeal_Comparison(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@PA_Id", petObject.PAId));
            return ncmdbObject.ExecuteDataSet("ASP_Web_Appeal_Petition_Id");
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

    #region function to bind year

    public DataSet Get_YearSOH(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@depttId",petObject.DepttId);
            ncmdbObject.AddParameter("@PetitionType", petObject.PetitionType);
            return ncmdbObject.ExecuteDataSet("USP_GetYearSOH ");
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

    #region function to bind year next

    public DataSet GetYearNextSOH(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@depttId", petObject.DepttId);
            ncmdbObject.AddParameter("@PetitionType", petObject.PetitionType);
            return ncmdbObject.ExecuteDataSet("USP_GetYearNextForSOH ");
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


    #region function to bind year for Ombudsman

    public DataSet Get_YearSOHForOmbudsman(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@depttId", petObject.DepttId);
            ncmdbObject.AddParameter("@PetitionType", petObject.PetitionType);
            return ncmdbObject.ExecuteDataSet("USP_GetYearSOHForOmbudsman ");
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

    //Area for all the functions to insert 

    #region Function to insert petition appeal in temp_Appeal petition data table

    public Int32 insert_Appeal_Petition(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", petObject.ActionType));
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_PA_Id", petObject.TempPAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal_No", petObject.AppealNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@PA_Status_Id", petObject.PAStatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@File_Name", petObject.FileName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Where_Appealed", petObject.WhereAppealed));
            ncmdbObject.Parameters.Add(new SqlParameter("@Judgement_Link", petObject.JudgementLink));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Old_PA_Id", petObject.OldPAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            //ncmdbObject.Parameters.Add(new SqlParameter("@Appeal", petObject.appeal));
            ncmdbObject.AddParameter("@Year", petObject.year);
            ncmdbObject.AddParameter("@AppealDate", petObject.AppealDate);
            ncmdbObject.Parameters.Add(new SqlParameter("@Inserted_By", petObject.InsertedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Last_Updated_By", petObject.LastUpdatedBy));
           // ncmdbObject.Parameters.Add(new SqlParameter("@Rec_Insert_Date", petObject.RecInsertDate));
           // ncmdbObject.Parameters.Add(new SqlParameter("@Rec_Update_Date", petObject.RecUpdateDate));
            ncmdbObject.Parameters.Add(new SqlParameter("@Remarks", petObject.Remarks));
            ncmdbObject.AddParameter("@refWhereAppealed",petObject.RefNo);
            ncmdbObject.AddParameter("@JudgementDesc", petObject.Description);
            
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Name", petObject.ApplicantName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Mobile_No", petObject.ApplicantMobileNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Phone_No", petObject.ApplicantPhoneNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Fax_No", petObject.ApplicantFaxNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Email", petObject.ApplicantEmail));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent", petObject.ResPondent));
            ncmdbObject.Parameters.Add(new SqlParameter("@Subject", petObject.subject));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Address", petObject.PetitionerAddress));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Address", petObject.RespondentAddress));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Mobile_No", petObject.RespondentMobileNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Phone_No", petObject.RespondentPhone_No));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Fax_No", petObject.RespondentFaxNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Email", petObject.Respondentmail));
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.AddParameter("@MetaKeyWords", petObject.MetaKeyWords);
            ncmdbObject.AddParameter("@MetaDescription", petObject.MetaDescription);
            ncmdbObject.AddParameter("@MetaTitle", petObject.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", petObject.MetaKeyLanguage);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_Petition_Appeal_Insert_Update");
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

    #region Function To insert Into web Petiton  APPEAL table

    public Int32 ASP_Insert_Web_Petiton_APPEAL(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Temp_PA_Id", petObject.TempPAId);
            ncmdbObject.AddParameter("@UserID", petObject.userID);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Web_Petition_Appeal_Insert_Update");
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

    //End

    //Area for all the functions to update

    #region Function to update the temp Appeal Petition status

    public Int32 ASP_Temp_Appeal_Petition_Update_Status_Id(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", petObject.StatusId);
            ncmdbObject.AddParameter("@Temp_PA_Id", petObject.TempPAId);
            ncmdbObject.AddParameter("@UserID", petObject.userID);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_Appeal_Petition_Change_status");
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

    //End

    //Area for all the functions to delete

    #region Functiont to delete petition appeal either temp or final

    public Int32 Delete_Petition_Appeal(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            ncmdbObject.AddParameter("@Temp_PA_Id", petObject.PAId);
            ncmdbObject.AddParameter("@Status_ID", petObject.StatusId);
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_Petition_Appeal_Delete");
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

    //End

   
    //Area for all the function to display records

    //Old


    #region Function to get Petition numbers

    public DataSet getPetitionNumber(PetitionOB petObject)
    {
        try
        {
            //ncmdbObject.AddParameter("@petitionType", petObject.PetitionType);
            ncmdbObject.AddParameter("@year", petObject.year);
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedPetitionForScheduleOfHearing");
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
    //New

    #region Function to get Petition numbers(NEW)

    public DataSet getPetitionNumberNew(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@year", petObject.year);
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedPetitionForScheduleOfHearing_NEW");
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

    #region Function to get Petition numbers for connections

    public DataSet getPetitionNumberForConnection(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@year", petObject.year);
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedPetitionNumberConnected1");
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


    #region Function to get Petition Review numbers

    public DataSet getPetitionReviewNumber(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@year", petObject.year);
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedReview_PetitionForScheduleOfHearing");
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

    #region Function to get Petition Appeal numbers

    public DataSet getPetitionAppealNumber()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedAppeal_PetitionForScheduleOfHearing");
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

    #region Function to get Temp schedule of hearing (For Edit)

    public DataSet getScheduleOfHearing_Edit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@soh_id_Temp", petObject.SohTempID));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            return ncmdbObject.ExecuteDataSet("ASP_ScheduleOfHearingDisplay_Edit");
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

    #region Function to update the temp_ScheduleOfHearing status

    public Int32 updateScheduleOfHearingStatus(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", petObject.StatusId);
            ncmdbObject.AddParameter("@soh_id_Temp", petObject.SohTempID);
            ncmdbObject.AddParameter("@UserID", petObject.userID);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_ScheduleOfHearing_Change_status");
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

    #region Function to get schedule of hearing id from temp table for comparision

    public DataSet scheduleOfHearingIDForComparison(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Soh_ID", petObject.soh_ID));
            return ncmdbObject.ExecuteDataSet("ASP_Web_ScheduleOfHearing_SohId");
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

    //Area for all the functions to insert records into schedule of hearing table

    #region Function To insert Into temp Schedule of hearing table

    public Int32 InsertTmpScheduleOfHearing(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@ActionType", petObject.ActionType);
            ncmdbObject.AddParameter("@soh_id_Temp", petObject.SohTempID);
            ncmdbObject.AddParameter("@Soh_ID", petObject.soh_ID);
            ncmdbObject.AddParameter("@Petition_ID", petObject.PetitionId);
            ncmdbObject.AddParameter("@ReviewPetition_ID", petObject.RPId);
            ncmdbObject.AddParameter("@AppealPetition_ID", petObject.AppealId);
            ncmdbObject.AddParameter("@Date", petObject.Date);
            ncmdbObject.AddParameter("@Subject", petObject.subject);
            ncmdbObject.AddParameter("@Time", petObject.Time);
            ncmdbObject.AddParameter("@Venue", petObject.Venue);
            ncmdbObject.AddParameter("@Status_ID", petObject.StatusId);
            ncmdbObject.AddParameter("@Lang_Id", petObject.LangId);
            ncmdbObject.AddParameter("@soh_file", petObject.SohFile);
            ncmdbObject.AddParameter("@Deptt_Id", petObject.DepttId);
            ncmdbObject.AddParameter("@year", petObject.year);
            ncmdbObject.AddParameter("@Rec_Inserted_By", petObject.recordInsertedBy);
            ncmdbObject.AddParameter("@Rec_Updated_By", petObject.recordUpdatedBy);
            ncmdbObject.AddParameter("@AppealPetitionId",petObject.keyword);
            ncmdbObject.AddParameter("@PetitionType", petObject.PetitionType);
            ncmdbObject.AddParameter("@Remarks", petObject.Remarks);
            ncmdbObject.AddParameter("@EmailId", petObject.ApplicantEmail);
            ncmdbObject.AddParameter("@MobileNo", petObject.ApplicantMobileNo);
            ncmdbObject.AddParameter("@MetaKeyWords", petObject.MetaKeyWords);
            ncmdbObject.AddParameter("@MetaDescription", petObject.MetaDescription);
            ncmdbObject.AddParameter("@MetaTitle", petObject.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", petObject.MetaKeyLanguage);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.AddParameter("@PlaceHolderOne", petObject.PlaceHolderOne);
            ncmdbObject.AddParameter("@PlaceHolderTwo", petObject.PlaceHolderTwo);
            if (petObject.AppealNo != "")
            {
                ncmdbObject.AddParameter("@AppealNo", petObject.AppealNo);
            }
            else
            {
                ncmdbObject.AddParameter("@AppealNo", null);
            }
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_ScheduleOfHearing_Insert_Update");
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

    #region Function To insert record into web_ScheduleOfHearing

    public Int32 ApproveScheduleOfHearing(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@soh_id_Temp", petObject.SohTempID);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.AddParameter("@UserID", petObject.userID);
            ncmdbObject.ExecuteNonQuery("ASP_Web_ScheduleOfHearing_Insert_Update");
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

    //End

    //Area for all the functions to delete records into schedule of hearing table

    #region Function to Delete the data from tables for schedule of hearing

    public Int32 Delete_ScheduleOfHearing(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@StatusId", petObject.StatusId);
            ncmdbObject.AddParameter("@soh_id_Temp", petObject.SohTempID);
            ncmdbObject.ExecuteNonQuery("ASP_Delete_ScheduleOfHearing");
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

    #region Function to dateforcalenderevant For HERC commission calender

    public DataSet get_dateforcalenderevant(PetitionOB sohObject)
    {
        try
        {
            ncmdbObject.AddParameter("@deptt_id", sohObject.DepttId);
            ncmdbObject.AddParameter("@langid", sohObject.LangId);
            ncmdbObject.AddParameter("@month", sohObject.Month);
            ncmdbObject.AddParameter("@year", sohObject.year);
            return ncmdbObject.ExecuteDataSet("UPS_GetDateforcalenderEvent");
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

    //Front End 

    #region function to get Petition information
    public DataSet Get_Petition(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@year", obj_petOB.year);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_GetPetition");
            catValue = Convert.ToInt16(param[0].Value);
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

    public DataSet Get_Year()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("USP_GetYear");
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

    #region function to get Petition information of Previous Year
    public DataSet Get_Petition_PrevYear(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@year", obj_petOB.year);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_GetPetitionPrevYear");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to get Temp petition(For Details)

    public DataSet get_Petition_Details(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Petition_Id", petObject.TempPetitionId));

            return ncmdbObject.ExecuteDataSet("USP_Petiton_DisplayDetails");
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

    #region Function to  petition review

    public DataSet get_ReviewPetition_Details(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@PRO_NO", petObject.PRONo));
            ncmdbObject.AddParameter("@RP_NO", petObject.RPId);
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            return ncmdbObject.ExecuteDataSet("USP_ReviewPetition_Display");
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

    #region function to get online status information
    public DataSet Get_OnlineStatus(PetitionOB obj_petOB)
    {

        try
        {
           
            ncmdbObject.AddParameter("@PetitionNumber", obj_petOB.PRONo);
            ncmdbObject.AddParameter("@Year", obj_petOB.year);
            ncmdbObject.AddParameter("@ConnectionType",obj_petOB.ConnectionType);
            return ncmdbObject.ExecuteDataSet("USP_GetPetition_onlineStatus");
           
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

    #region function to get  top 2 Schedule of Hearings details

    public DataSet Get_SOH(PetitionOB objPetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Lang_Id", objPetOB.LangId);
            return ncmdbObject.ExecuteDataSet("USP_Get_SOH");
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

    #region function to get  current Schedule of Hearings details

    public DataSet Get_CurrentSOH(PetitionOB objPetOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Lang_Id", objPetOB.LangId);
            ncmdbObject.AddParameter("@PageIndex", objPetOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", objPetOB.PageSize);
            ncmdbObject.AddParameter("@deptt_id", objPetOB.DepttId);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_Get_CutrrentSOH");
            catValue = Convert.ToInt16(param[0].Value);
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

    //End

    #region function to get Petion Search information
    public DataSet Get_PetionSearch(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@ActionType", obj_petOB.ActionType);
            ncmdbObject.AddParameter("@PetitionerName", obj_petOB.PetitionerName);
            ncmdbObject.AddParameter("@RespondentName", obj_petOB.RespondentName);
            ncmdbObject.AddParameter("@Subject", obj_petOB.subject);
            ncmdbObject.AddParameter("@PetitionNumber", obj_petOB.PRONo);
            ncmdbObject.AddParameter("@PetitionStatusId", obj_petOB.PetitionStatusId);
            ncmdbObject.AddParameter("@year", obj_petOB.year);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@ConnectionType", obj_petOB.ConnectionType);
            p_Var.dSet= ncmdbObject.ExecuteDataSet("USP_GetPetition_Search");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region function to get Notifications Details
    public DataSet Get_Notifications(PetitionOB obj_petOB, out int catValue)
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
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_Get_Notification");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region function to insert the status in Mst_Status table

    public Int32 Insert_Status(PetitionOB objpetOB, out int id)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Status_Type_Id", objpetOB.StatusId);
            ncmdbObject.AddParameter("@Status", objpetOB.subject);
            p_Var.Result = ncmdbObject.ExecuteNonQuery("USP_insert_Mst_Status");
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

    #region function to get  Schedule of Hearings details by calander

    public DataSet Get_SOH_By_calander(PetitionOB objPetOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Lang_Id", objPetOB.LangId);
            ncmdbObject.AddParameter("@PageIndex", objPetOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", objPetOB.PageSize);
            ncmdbObject.AddParameter("@deptt_id", objPetOB.DepttId);
            ncmdbObject.AddParameter("@date", objPetOB.Date);

            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_Get_Forcommision_cal_For_SOH");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to get connected order of petition

    public DataSet getConnectedOrders(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Petition_ID", objpetOB.PetitionId);
            return ncmdbObject.ExecuteDataSet("USP_GetOrder_Petition");
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

    #region Function to get connected order of Reviewpetition

    public DataSet getConnectedOrdersForReview(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@ReviewPetition_ID", objpetOB.RPNo);
            return ncmdbObject.ExecuteDataSet("USP_GetOrder_ReviewPetition");
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

    #region Function to get order details for review petition 

    public DataSet getOrdersForReview(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@ReviewPetition_ID", objpetOB.RPNo);
            return ncmdbObject.ExecuteDataSet("USP_GetOrderFromReviewPetition");
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

    #region Function to get connected petition file name 

    public DataSet getPetitionFileNames(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Petition_ID", objpetOB.PetitionId);
            return ncmdbObject.ExecuteDataSet("USP_GetPetition_FileName");
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

    #region Function to get connected Appeal file name

    public DataSet getAppealFileNames(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@AppealId", objpetOB.AppealId);
            return ncmdbObject.ExecuteDataSet("USP_GetAppealFileName");
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

    #region Function to get connected petition file name

    public DataSet getReviewPetitionFileNames(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@RPID", objpetOB.RPId);
            return ncmdbObject.ExecuteDataSet("USP_GetReviewPetition_FileName");
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

    #region Function to get connected SOH of petition

    public DataSet getConnectedSOH(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Petition_ID", objpetOB.PetitionId);
            return ncmdbObject.ExecuteDataSet("USP_GetSOH_Petition");
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

    #region Function to get Public Notice of petition

    public DataSet getConnectedPublicNotice(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Petition_ID", objpetOB.PetitionId);
            return ncmdbObject.ExecuteDataSet("USP_GetPublicNotice_Petition");
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

    #region function to get year of Petition Admin

    public DataSet GetYearPetition_Admin()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYearPetition_Admin1");
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

    #region function to get year of Petition Admin

    public DataSet GetYearPetition_AddEdit()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYearPetition_AddEdit");
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

    #region function to get year of Review Petition Admin

    public DataSet GetYearReviewPetition_Admin()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYearReviewPetition_Admin");
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

    #region function to get year of Petition Appeal Admin

    public DataSet GetYearPetitionAppeal_Admin()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYearPetitionAppeal_Admin");
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

    #region Function to get times

    public DataSet getTime()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("asp_getTime");
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

    #region Function to get hours

    public DataSet getHours()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("asp_getHours");
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

    #region Function to get ampm

    public DataSet getampm()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("asp_getampm");
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

    #region Function to get RP from temp_Review_Petition table

    public DataSet get_RP_From_Temp_PetReview(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RPID", petObject.RPId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_RPId_from_Tmp_Final");
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

    #region Function to  petition review

    public DataSet get_AppealPetition_Details(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@PRO_NO", petObject.PRONo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            return ncmdbObject.ExecuteDataSet("USP_ApepalPetition_Display");
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

    #region get current Petition review

    public DataSet Get_CurrentPetitionReview(PetitionOB pt_obj, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", pt_obj.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageIndex", pt_obj.PageIndex));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageSize", pt_obj.PageSize));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", pt_obj.year));

            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_get_CurrentPetitionReview");

            catValue = Convert.ToInt16(param[0].Value);
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

    #region get current Petition Appeal

    public DataSet Get_CurrentPetitionAppeal(PetitionOB pt_obj, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", pt_obj.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageIndex", pt_obj.PageIndex));
            ncmdbObject.Parameters.Add(new SqlParameter("@PageSize", pt_obj.PageSize));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", pt_obj.year));

            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_get_CurrentPetitionAppeal");

            catValue = Convert.ToInt16(param[0].Value);
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

    #region function to get year of Petition review for previous

    public DataSet GetYearPetitionReviewPrevious()
    {
        try
        {

            return ncmdbObject.ExecuteDataSet("ASP_GetYearReviewPrevious");
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

    #region function to get year of Petition Appeal for previous

    public DataSet GetYearPetitionAppealPrevious()
    {
        try
        {

            return ncmdbObject.ExecuteDataSet("ASP_GetYearAppealPrevious");
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

    #region Function to get connected petition

    public DataSet get_ConnectedPetition(PetitionOB petObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_Id", petObject.PetitionId));

            p_Var.dSet = ncmdbObject.ExecuteDataSet("usp_getConnectedPetitionDetails");
          
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


    #region Function to get connected Reviewpetition

    public DataSet get_ConnectedReviewPetition(PetitionOB petObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));

            p_Var.dSet = ncmdbObject.ExecuteDataSet("usp_getConnectedReviewPetitionDetails");

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




    #region Function to get connected petition

    public DataSet getConnectedForPetition(PetitionOB petObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_Id", petObject.PetitionId));

            //ncmdbObject.Parameters.Add(new SqlParameter("@ConnectionId", petObject.ConnectionID));
            p_Var.dSet = ncmdbObject.ExecuteDataSet("usp_getConnectedPetitionDetailsForConnected");

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

    #region Function to update the  Petition status for delete

    public Int32 updatePetitionStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            //ncmdbObject.AddParameter("@Status_Id", rtiObject.StatusId);
            ncmdbObject.AddParameter("@Petition_id", rtiObject.PetitionId);
            ncmdbObject.ExecuteNonQuery("ASP_Web_Petition_ChangestatusDelete");
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

    #region function to get year of ScheduleOfHearing Admin

    public DataSet GetYearScheduleOfHearing_Admin(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@petitionType",petObject.AppealStatusId);
            return ncmdbObject.ExecuteDataSet("ASP_GetYearScheduleOfhearing_Admin");
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

    #region function to get year of ScheduleOfHearing for Appeal Admin

    public DataSet GetYearScheduleOfHearingforAppeal_Admin()
    {
        try
        {

            return ncmdbObject.ExecuteDataSet("ASP_GetAppealYearFor_Admin");
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

    #region Function to insert ConnectedPetition

    public Int32 insertConnectedPetitionFiles(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@FileName", petObject.PetitionFile));    
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.AddParameter("@Date", petObject.PetitionDate);
            ncmdbObject.AddParameter("@Petition_ID", petObject.PetitionId);
            ncmdbObject.AddParameter("@Comments", petObject.Description);
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedPetitionFiles_Insert_Update");
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

    #region Function to insert ConnectedAppeal

    public Int32 insertConnectedAppealFiles(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@FileName", petObject.PetitionFile));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.AddParameter("@Date", petObject.PetitionDate);
            ncmdbObject.AddParameter("@AppealID", petObject.AppealId);
            ncmdbObject.AddParameter("@Comments", petObject.Description);
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedAppealFiles_Insert_Update");
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

    #region Function to get FileName for petition

    public DataSet getFileNameForPetition(PetitionOB orderObject)
    {
        try
        {
            ncmdbObject.AddParameter("@PetitionId", orderObject.PetitionId);
            //ncmdbObject.AddParameter("@Status", orderObject.Status);
            return ncmdbObject.ExecuteDataSet("ASP_GetFileNamePetition");
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

    #region Function to get FileName for Appeal petition

    public DataSet getFileNameForAppealpetition(PetitionOB orderObject)
    {
        try
        {
            ncmdbObject.AddParameter("@Temp_PA_Id", orderObject.AppealId);  
            return ncmdbObject.ExecuteDataSet("ASP_GetFileNameAppealPetition");
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

    #region Function to get FileName for Review petition

    public DataSet getFileNameForReviewPetition(PetitionOB orderObject)
    {
        try
        {
            ncmdbObject.AddParameter("@RPID", orderObject.RPId);
       
            return ncmdbObject.ExecuteDataSet("ASP_GetFileNameReviewPetition");
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

    #region Function to Update status for Files in Petitions

    public Int32 UpdateFileStatusForPetitions(PetitionOB orderObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Id", orderObject.ConnectionID);
            ncmdbObject.ExecuteNonQuery("ASP_Update_FilePetitionStatus");
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

    #region Function to Update status for Files in AppealPettion

    public Int32 UpdateFileStatusForAppealPettion(PetitionOB orderObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Id", orderObject.ConnectionID);
            ncmdbObject.ExecuteNonQuery("ASP_Update_FileAppealStatus");
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

    #region Function to Update status for Files in Petitions

    public Int32 UpdateFileStatusForReviewPetitions(PetitionOB orderObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Id", orderObject.ConnectionID);
            ncmdbObject.ExecuteNonQuery("ASP_Update_FileReviewPetitionStatus");
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

    #region Function to insert ConnectedReviewPetition

    public Int32 insertConnectedReviewPetitionFiles(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@FileName", petObject.PetitionFile));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.AddParameter("@Date", petObject.PetitionDate);
            ncmdbObject.AddParameter("@Petition_ID", petObject.PetitionId);
            ncmdbObject.AddParameter("@RPId", petObject.RPId);
            ncmdbObject.AddParameter("@Comments", petObject.Description);
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedReviewPetitionFiles_Insert_Update");
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

    #region Function to  petition review for Popup 

    public DataSet get_ReviewPetitionForPopup(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            return ncmdbObject.ExecuteDataSet("USP_ReviewPetitionDetails");
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

    #region Function to update Soh for delete

    public Int32 SOH_Changestatus_Delete(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            //ncmdbObject.AddParameter("@Status_Id", rtiObject.StatusId);
            ncmdbObject.AddParameter("@Temp_Soh_ID", rtiObject.soh_ID);
            ncmdbObject.ExecuteNonQuery("ASP_SOH_Changestatus_Delete");
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

 
    //New code for review petition on date 04-03-2013

    #region Function to get Petition Review numbers for connections

    public DataSet getPetitionReviewNumberForConnection(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@year", petObject.year);
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedReviewPetitionNumberConnected1");
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

    #region function to get year of Petition Admin

    public DataSet GetYearPetitionReview_Admin()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYearReviewPetition_Admin1");
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

    #region function to get year of Petition Admin

    public DataSet GetYearPetitionReview_AddEdit()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYearReviewPetition_AddEdit");
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

    #region Function to insert Connected petition review

    public Int32 insertConnectedPetitionReview(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@Connected_RP_Id", petObject.ConnectedPetitionID));
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", petObject.year));
            ncmdbObject.ExecuteNonQuery("ASP_Connected_PetitionReview_Insert_Update");
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
    
    #region Function to get connected Petition Review(For Edit)

    public DataSet get_ConnectedPetitionReview_Edit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));

            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedPetitionReview");
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

    #region Function to delete Connected petition review

    public Int32 deleteConnectedPetitionReview(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Rp_id", petObject.RPId));
            ncmdbObject.ExecuteNonQuery("ASP_Connected_PetitionReview_Delete");
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

    #region Function to get connected Petition 

    public DataSet getConnectedPetition(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Id", petObject.PetitionId));

            return ncmdbObject.ExecuteDataSet("getconnectedPetitionId");
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


    #region Function to get connected ReviewPetition

    public DataSet getConnectedReviewPetition(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Id", petObject.RPId));

            return ncmdbObject.ExecuteDataSet("getconnectedReviewPetitionId");
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

    #region Function to get connected SOH of Reviewpetition

    public DataSet getReviewConnectedSOH(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@ReviewPetition_ID", objpetOB.RPId);
            return ncmdbObject.ExecuteDataSet("USP_GetSOH_ReviewPetition");
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

    #region Function to insert SOHWithPetition

    public Int32 insertSoHwithPetition(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_id", petObject.PetitionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", petObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@SOHID", petObject.soh_ID));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@ReviewId", petObject.RPId));
            ncmdbObject.Parameters.Add(new SqlParameter("@AppealId", petObject.AppealId));
            ncmdbObject.Parameters.Add(new SqlParameter("@DepttId", petObject.DepttId));
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedPetitionWithSOH_Insert_Update");
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

    #region Function to get connected SOH for Petition(For Edit)

    public DataSet get_ConnectedSOH_Edit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@SOHID", petObject.soh_ID));
            ncmdbObject.Parameters.Add(new SqlParameter("@PetitionType", petObject.PetitionType));
            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedSOHWithPublicNoticeEdit");
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

    #region Function to get connected SOH for Petition(For EditMode)

    public DataSet get_ConnectedSOHEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@SOHID", petObject.soh_ID));
            ncmdbObject.Parameters.Add(new SqlParameter("@petitionType", petObject.PetitionType));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_ID", petObject.StatusId)); 

            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedSOHWithPublicNoticeEditMode");
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

    #region Function to delete Connectedpetition from SOH

    public Int32 deleteConnectedPetitionFromSOH(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@SOHID", petObject.soh_ID));
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedPetitionWithSOH_Delete");
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

    #region Function to update the  ReviewPetition status for delete

    public Int32 updateReviewPetitionStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            
            ncmdbObject.AddParameter("@RP_Id", rtiObject.RPId);
            ncmdbObject.ExecuteNonQuery("ASP_Web_RP_ChangestatusDelete");
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

    #region Function to update the  updateAppealPetitionStatusDelete  status for delete

    public Int32 updateAppealPetitionStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);

            ncmdbObject.AddParameter("@PA_Id", rtiObject.PAId);
            ncmdbObject.ExecuteNonQuery("ASP_Web_PA_ChangestatusDelete");
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

    #region Function to get Petition numbers for connections

    public DataSet getPetitionNumberForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@peittionID", petObject.PetitionId);
            ncmdbObject.AddParameter("@year", petObject.year);
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedPetitionNumberConnectedEdit");
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

    #region Function to get Petition Review numbers for connections

    public DataSet getPetitionReviewNumberForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@rp_id", petObject.RPId);
            ncmdbObject.AddParameter("@year", petObject.year);
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedReviewPetitionNumberConnectedEdit");
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

    #region function to get  current Schedule of Hearings details for Review Petition

    public DataSet Get_CurrentSOHReviewPetition(PetitionOB objPetOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Lang_Id", objPetOB.LangId);
            ncmdbObject.AddParameter("@PageIndex", objPetOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", objPetOB.PageSize);
            ncmdbObject.AddParameter("@deptt_id", objPetOB.DepttId);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_Get_CutrrentSOHforRP");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region function to get SOH information of Previous Year
    public DataSet Get_PreviousYearSOHReviewPetition(PetitionOB obj_PetOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@year", obj_PetOB.year);
            ncmdbObject.AddParameter("@PageIndex", obj_PetOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_PetOB.PageSize);
            ncmdbObject.AddParameter("@Lang_Id", obj_PetOB.LangId);
            ncmdbObject.AddParameter("@deptt_id", obj_PetOB.DepttId);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_Get_PreviousSOHforRP");
            catValue = Convert.ToInt16(param[0].Value);
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

    //Ombudsman Schedule of hearing functions

    #region function to get  current Schedule of Hearings for Ombudsman

    public DataSet GetCurrentSOHForOmbudsman(PetitionOB objPetOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Lang_Id", objPetOB.LangId);
            ncmdbObject.AddParameter("@PageIndex", objPetOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", objPetOB.PageSize);
            ncmdbObject.AddParameter("@deptt_id", objPetOB.DepttId);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_Get_CutrrentSOHOmbudsman");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region function to get SOH information of Previous Year for Ombudsman

    public DataSet GetPreviousYearSOHForOmbudsman(PetitionOB obj_PetOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@year", obj_PetOB.year);
            ncmdbObject.AddParameter("@PageIndex", obj_PetOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_PetOB.PageSize);
            ncmdbObject.AddParameter("@Lang_Id", obj_PetOB.LangId);
            ncmdbObject.AddParameter("@deptt_id", obj_PetOB.DepttId);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_Get_PreviousSOHOmbudsman");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region function to get  Schedule of Hearings details by calander

    public DataSet GetSOHcalanderforOmbudsman(PetitionOB objPetOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Lang_Id", objPetOB.LangId);
            ncmdbObject.AddParameter("@PageIndex", objPetOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", objPetOB.PageSize);
            ncmdbObject.AddParameter("@deptt_id", objPetOB.DepttId);
            ncmdbObject.AddParameter("@date", objPetOB.Date);

            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_Get_Forcommision_cal_For_SOHOmbudsman");
            catValue = Convert.ToInt16(param[0].Value);
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

    //End



    #region function to get SOH Search information
    public DataSet Get_SOHSearch(PetitionOB obj_petOB, out int catValue)
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
            ncmdbObject.AddParameter("@Applicant_Name", obj_petOB.ApplicantName);
            ncmdbObject.AddParameter("@RespondentName", obj_petOB.RespondentName);
            ncmdbObject.AddParameter("@Subject", obj_petOB.subject);
            ncmdbObject.AddParameter("@Year", obj_petOB.year);
            ncmdbObject.AddParameter("@Date", obj_petOB.Date);
            ncmdbObject.AddParameter("@PetitionNumber", obj_petOB.PRONo);
            ncmdbObject.AddParameter("@ConnectionType", obj_petOB.ConnectionType);
          
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_GetSOh_Search");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region function to get Ombudsman SOH Search information
    public DataSet Get_OmbudsmanSOHSearch(PetitionOB obj_petOB, out int catValue)
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
            ncmdbObject.AddParameter("@Applicant_Name", obj_petOB.ApplicantName);
            ncmdbObject.AddParameter("@RespondentName", obj_petOB.RespondentName);
            ncmdbObject.AddParameter("@Subject", obj_petOB.subject);
            ncmdbObject.AddParameter("@Year", obj_petOB.year);
            ncmdbObject.AddParameter("@Date", obj_petOB.Date);
            ncmdbObject.AddParameter("@AppealNumber", obj_petOB.AppealNo);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_GetOmdudsmanSOh_Search");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to insert Connected Public notice files

    public Int32 insertConnectedSohFiles(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@FileName", petObject.FileName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.AddParameter("@Date", petObject.StartDate);
            ncmdbObject.AddParameter("@SohId", petObject.soh_ID);
            ncmdbObject.AddParameter("@Comments", petObject.Description);
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedSohFiles_Insert");
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

    #region Function to get FileName for schedule of hearing

    public DataSet getFileNameForSoh(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@SohId", petObject.soh_ID);
            return ncmdbObject.ExecuteDataSet("ASP_GetFileNameSoh");
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

    #region Function to Update status for Files in Soh

    public Int32 UpdateFileStatusForSoh(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Id", petObject.ConnectionID);
            ncmdbObject.ExecuteNonQuery("ASP_Update_FilePetitionSohStatus");
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

    #region Function to get temp/final schedule of hearing(Conditional)

    public DataSet get_ScheduleOfHearingDetails(PetitionOB petObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@Soh_ID", petObject.SohTempID));
           
           
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));

            return p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_GetSOHDetailsById");
           
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

    #region Function to get connected SOH file name

    public DataSet getSohFileNames(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@SohId", objpetOB.soh_ID);
            return ncmdbObject.ExecuteDataSet("USP_GetSohId_FileName");
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

    #region function to get  current Schedule of Hearings for Ombudsman details

    public DataSet GetCurrentSOHForOmbudsmanDetails(PetitionOB objPetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Soh_Id", objPetOB.soh_ID);
            ncmdbObject.AddParameter("@deptt_id", objPetOB.DepttId);
            return ncmdbObject.ExecuteDataSet("USP_Get_CutrrentSOHOmbudsmanDetails");
           
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

    #region get current Petition Appeal Details

    public DataSet Get_CurrentPetitionAppealDetails(PetitionOB pt_obj)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@PA_Id", pt_obj.PAId));
            return ncmdbObject.ExecuteDataSet("USP_get_CurrentPetitionAppealDetails");
          
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

    #region get Petition and review petition number based on review petition id 

    public DataSet GetPetitionReviewPetitionNumbers(PetitionOB pt_obj)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", pt_obj.RPId));
            return ncmdbObject.ExecuteDataSet("ASP_Petition_ReviewPetitionNumbers");

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

    #region function to get year of Petition Front side

    public DataSet GetYearPetitionSearch(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@ConnectionType",petObject.ConnectionType);
            return ncmdbObject.ExecuteDataSet("USP_GetYearPetition");
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


    #region function to get year of Petition OnlineStatus Front side

    public DataSet GetYearPetitionOnlineStatusSearch()
    {
        try
        {

            return ncmdbObject.ExecuteDataSet("USP_GetYearPetitionOnlineStatus");
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




    #region function to bind year for search

    public DataSet Get_YearSOHSearch(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@depttId", petObject.DepttId);
           
    
            return ncmdbObject.ExecuteDataSet("USP_GetYearSOHForSearch ");
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



    #region function to bind year for search

    public DataSet Get_YearSOHSearchHerc(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@depttId", petObject.DepttId);
            ncmdbObject.AddParameter("@connectionType", petObject.ConnectionType);
        
            return ncmdbObject.ExecuteDataSet("USP_GetYearSOHForSearchHERC");
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


    #region function to bind Appeal number for search

    public DataSet Get_AppealNumberSOHSearch(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@Year", petObject.year);

            return ncmdbObject.ExecuteDataSet("USP_GetAppealNumberForSearch");
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



    #region function to bind Petition number for search

    public DataSet Get_PetitionNumberSOHSearch(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@connectionType", petObject.ConnectionType);
            ncmdbObject.AddParameter("@Year", petObject.year);
            return ncmdbObject.ExecuteDataSet("USP_GetPRONumberForSearch");
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



    #region Function to get Petition and review petition numbers for connections

    public DataSet getPetition_ReviewNumbers(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@year", petObject.year);
            ncmdbObject.AddParameter("@ConnectionType", petObject.ConnectionType);
            return ncmdbObject.ExecuteDataSet("ASP_GetPetition_ReviewNumber");
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


    #region Function to get current Year new Petition

    public DataSet GetLatestPetition(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            //ncmdbObject.AddParameter("@Lang_Id", obj_petOB.LangId);
            ncmdbObject.AddParameter("@ActionType", obj_petOB.ActionType);
            ncmdbObject.AddParameter("@PRONO", obj_petOB.PRONo);

            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_GetPetitionWhatsNew");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to get Petition year for connections

    public DataSet getPetitionYearForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@peittionID", petObject.PetitionId);

            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedPetitionYearEdit");
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

    #region Function to get Review Petition year for connections

    public DataSet getReviewPetitionYearForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@reviewPeittionID", petObject.RPId);

            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedReviewPetitionYearEdit");
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

    #region Function to get Schedule Of Hearing year for connections

    public DataSet getScheduleOfHearingYearForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@scheduleOfHearingID", petObject.soh_ID);
            ncmdbObject.AddParameter("@statusid", petObject.StatusId);
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedScheduleOfHearingYearEdit");
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




    #region Function to insert ConnectedMultiplePetitionReview

    public Int32 insertConnectedMultiplePetitionReview(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@Connected_Petition_id", petObject.ConnectedPetitionID));
            //ncmdbObject.Parameters.Add(new SqlParameter("@Petition_id", petObject.PetitionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", petObject.year));
            ncmdbObject.ExecuteNonQuery("ASP_MultiplePetitionreview_Insert_Update");
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


    #region Function to get connected Petition(For Edit)

    public DataSet get_ConnectedPetitionMultiple_Edit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));

            return ncmdbObject.ExecuteDataSet("ASP_Connected_multiplePetitionReview");
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



    #region Function to get Petition year for connections

    public DataSet Connected_multiplePetitionReviewForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@RP_Id", petObject.RPId);

            return ncmdbObject.ExecuteDataSet("ASP_Connected_multiplePetitionReviewYearEdit");
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


    #region Function to get connected PetitionReview(For Edit)

    public DataSet getConnectedPetitionReviewEditNew(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", petObject.year));
            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedPetitionReviewEdit");
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



    #region Function to get PetitionReview numbers for connections

    public DataSet getPetitionReviewForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@Rp_Id", petObject.RPId);
            ncmdbObject.AddParameter("@year", petObject.year);
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedPetitionReviewConnectedEdit");
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



    #region Function to delete ConnectedpetitionReview

    public Int32 deleteConnectedPetitionReviewMultiple(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));
            ncmdbObject.ExecuteNonQuery("ASP_Connected_PetitionReviewMultiple_Delete");
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


    #region Function to get connected petition review

    public DataSet get_ConnectedPetitionReview(PetitionOB petObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));

            p_Var.dSet = ncmdbObject.ExecuteDataSet("usp_getConnectedPetitionReviewDetails");

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


    #region Function to get connected petition review Details

    public DataSet get_ConnectedPetitionReviewDetails(PetitionOB petObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));

            p_Var.dSet = ncmdbObject.ExecuteDataSet("Usp_GetReviewPetitionMultiple");

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

    //Code to get email ids for sending emails

    #region function to get email ids to send email.

    public DataSet getEmailIdForSendingMail(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@petitionID", petObject.TempPetitionId);
            return ncmdbObject.ExecuteDataSet("asp_GetPetitionerRespondentEmail");
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

    public DataSet getEmailIdForSendingMailForReviewPetition(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@reviewPetitionID", petObject.TempPetitionId);
            return ncmdbObject.ExecuteDataSet("asp_GetPetitionerRespondentForReviewPetitionEmail");
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

    //Count Numbers of draft/review/approval records.

    #region function to count total pending petitions

    public  string  countPetitionPending()
    {
        try
        {

            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_PetitionCountPending"));
            return p_Var.str;
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

    public DataSet countPetitionPendingAll()
    {
        try
        {

            p_Var.dSet =ncmdbObject.ExecuteDataSet("ASP_PetitionCountAll");
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

    

    #region function to count total review petitions

    public string countPetitionReview()
    {
        try
        {

            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_PetitionCountReview"));
            return p_Var.str;
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

    #region function to count total review for all petitions

    public DataSet countPetitionReviewall()
    {
        try
        {

            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_PetitionCountReviewAll");
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

    #region function to count total approval petitions

    public string countPetitionApproval()
    {
        try
        {

            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_PetitionCountApproval"));
            return p_Var.str;
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

    #region function to count total approval petitions

    public DataSet countPetitionApprovalAll()
    {
        try
        {

            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_PetitionCountApprovalAll");
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

    #region function to count total approval publicNotice

    public string countPublicNoticeApproval()
    {
        try
        {

            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_PublicNoticeCountApproval"));
            return p_Var.str;
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

    #region function to count total pending publicNotice

    public string countPublicNoticePending()
    {
        try
        {

            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_PublicNoticeCountPending"));
            return p_Var.str;
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

    #region function to count total review publicNotice

    public string countPublicNoticeReview()
    {
        try
        {

            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_PublicNoticeCountReview"));
            return p_Var.str;
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

    #region function to count total pending Schedule of Hearing

    public string countScheduleOfHearingPending()
    {
        try
        {

            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ScheduleOfHearingCountPending"));
            return p_Var.str;
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

    #region function to count total pending Schedule of Hearing all

    public DataSet countScheduleOfHearingPendingall()
    {
        try
        {

            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ScheduleOfHearingCountPendingall");
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

    #region function to count total pending orders all

    public DataSet countOrdersPendingall()
    {
        try
        {

            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_OrdersountPendingall");
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

    #region function to count total review orders all

    public DataSet countOrdersReviewall()
    {
        try
        {

            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_OrdersountReviewall");
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

    #region function to count total appraval orders all

    public DataSet countOrdersApprovalall()
    {
        try
        {

            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_OrdersountApprovalall");
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


    #region function to count total pending public notices

    public DataSet countPublicNoticePendingall()
    {
        try
        {

            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_PublicNoticeCountPendingall");
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

    #region function to count total review public notices

    public DataSet countPublicNoticeReviewall()
    {
        try
        {

            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_PublicNoticeCountReviewall");
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

    #region function to count total approval public notices

    public DataSet countPublicNoticeApprovalall()
    {
        try
        {

            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_PublicNoticeCountApprovalall");
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

    #region function to count total review Schedule of Hearing

    public string countScheduleOfHearingReview()
    {
        try
        {

            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ScheduleOfHearingCountReview"));
            return p_Var.str;
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

    #region function to count total review Schedule of Hearing

    public DataSet countScheduleOfHearingReviewAll()
    {
        try
        {

            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ScheduleOfHearingCountReviewAll");
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


    #region function to count total approval Schedule of Hearing

    public string countScheduleOfHearingApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ScheduleOfHearingCountApproval"));
            return p_Var.str;
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

    #region function to count total approval Schedule of Hearing

    public DataSet countScheduleOfHearingApprovalAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ScheduleOfHearingCountApprovalAll");
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

    #region function to count total pending orders

    public string countOrdersPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_OrdersCountPending"));
            return p_Var.str;
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

    #region function to count total review orders

    public string countOrdersReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_OrdersCountReview"));
            return p_Var.str;
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

    #region function to count total approval orders

    public string countOrdersApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_OrdersCountApproval"));
            return p_Var.str;
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

    #region function to count total pending notifications

    public string countNotificationPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_NotificationsCountPending"));
            return p_Var.str;
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

    #region function to count total review notifications

    public string countNotificationReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_NotificationsCountReview"));
            return p_Var.str;
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

    #region function to count total approval notifications

    public string countNotificationApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_NotificationsCountApproval"));
            return p_Var.str;
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

    #region function to count total pending tariff

    public string countTariffPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_TariffCountPending"));
            return p_Var.str;
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

    #region function to count total review tariff

    public string countTariffReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_TariffCountReview"));
            return p_Var.str;
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

    #region function to count total approval tariff

    public string countTariffApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_TariffCountApproval"));
            return p_Var.str;
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

    #region function to count total pending annaul report

    public string countAnnualReportPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_AnnualCountPending"));
            return p_Var.str;
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

    #region function to count total review annaul report

    public string countAnnualReportReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_AnnualReportReviewCount"));
            return p_Var.str;
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

    #region function to count total approval annaul report

    public string countAnnualReportApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_AnnualReportApprovalCount"));
            return p_Var.str;
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

    #region function to count total pending disscussion paper

    public string countDisscussionPaperPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_DisscussionPaperCountPending"));
            return p_Var.str;
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

    #region function to count total review disscussion paper

    public string countDisscussionPaperReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_DisscussionPaperCountReview"));
            return p_Var.str;
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

    #region function to count total approval disscussion paper

    public string countDisscussionPaperApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_DisscussionPaperCountApproval"));
            return p_Var.str;
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


    #region function to count total pending Vacancy

    public string countVacancyPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_vacancyCountPending"));
            return p_Var.str;
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

    #region function to count total review vacancy

    public string countVacancyReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_vacancyCountReview"));
            return p_Var.str;
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

    #region function to count total approval vacancy

    public string countVacancyApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_vacancyCountApproval"));
            return p_Var.str;
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

    #region function to count total pending Report

    public string countReportPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ReportCountPending"));
            return p_Var.str;
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

    #region function to count total review Report

    public string countReportReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ReportCountReview"));
            return p_Var.str;
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

    #region function to count total approval Report

    public string countReportApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ReportCountApproval"));
            return p_Var.str;
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

    #region function to count total pending Modules

    public string countModulesPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ModulesCountPending"));
            return p_Var.str;
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

    #region function to count total pending Modules

    public DataSet countModulesPendingAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ModulesCountPendingAll");
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

    #region function to count total pending Modules

    public DataSet countModulesReviewAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ModulesCountReviewall");
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

    #region function to count total pending Modules

    public DataSet countModulesApprovalAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ModulesCountApprovalall");
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

    #region function to count total review Modules

    public string countModulesReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ModulesCountReview"));
            return p_Var.str;
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

    #region function to count total approval Modules

    public string countModulesApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ModulesCountApproval"));
            return p_Var.str;
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


    #region function to count total pending Profile

    public string countProfilePending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ProfileCountPending"));
            return p_Var.str;
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

    #region function to count total review Profile

    public string countProfileReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ProfileCountReview"));
            return p_Var.str;
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

    #region function to count total approval Profile

    public string countProfileApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ProfileCountApproval"));
            return p_Var.str;
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

    #region function to count total pending contents

    public string countContentPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ContentCountPending"));
            return p_Var.str;
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

    #region function to count total pending contents

    public DataSet countContentPendingAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ContentCountPendingAll");
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

    #region function to count total review contents

    public string countContentReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ContentCountReview"));
            return p_Var.str;
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

    #region function to count total review contents

    public DataSet countContentReviewAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ContentCountReviewAll");
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


    #region function to count total approval contents

    public string countContentApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_ContentCountApproval"));
            return p_Var.str;
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

    #region function to count total approval contents

    public DataSet countContentApprovalAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ContentCountApprovalAll");
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

    #region function to count total pending RTI

    public string countRTIPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_RTICountPending"));
            return p_Var.str;
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

    #region function to count total pending RTI

    public DataSet countRTIPendingAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_RTICountPendingAll");
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

    #region function to count total pending Tariff modules

    public DataSet countTariffPendingAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_TariffCountPendingAll");
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

    #region function to count total review Tariff modules

    public DataSet countTariffReviewAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_TariffCountReviewAll");
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

    #region function to count total approval Tariff modules

    public DataSet countTariffApprovalAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_TariffCountApprovalAll");
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

    #region function to count total review RTI

    public string countRTIReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_RTICountReview"));
            return p_Var.str;
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

    #region function to count total review RTI

    public DataSet countRTIReviewAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_RTICountReviewAll");
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

    #region function to count total pending appeal

    public DataSet countAppealPendingAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_AppealCountPendingall");
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

    #region function to count total Review appeal

    public DataSet countAppealReviewAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_AppealCountReviewall");
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

    #region function to count total Review appeal

    public DataSet countAppealApprovalAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_AppealCountApprovalall");
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

    #region function to count total approval RTI

    public string countRTIApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_RTICountApproval"));
            return p_Var.str;
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

    #region function to count total approval RTI

    public DataSet countRTIApprovalAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_RTICountApprovalAll");
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

    #region function to count total pending Appeal

    public string countAppealPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_AppealCountPending"));
            return p_Var.str;
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

    #region function to count total review Appeal

    public string countAppealReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_AppealCountReview"));
            return p_Var.str;
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

    #region function to count total approval Appeal

    public string countAppealApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_AppealCountApproval"));
            return p_Var.str;
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

    #region function to count total pending AwardPronounced

    public string countAwardPronouncedPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_AwardPronouncedCountPending"));
            return p_Var.str;
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

    #region function to count total review AwardPronounced

    public string countAwardPronouncedReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_AwardPronouncedCountReview"));
            return p_Var.str;
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

    #region function to count total approval AwardPronounced

    public string countAwardPronouncedApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_AwardPronouncedCountApproval"));
            return p_Var.str;
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

    #region function to count total pending AwardPronounced all

    public DataSet countAwardPronouncedPendingAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_AwardProuncedCountPendingall");
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

    #region function to count total pending AwardPronounced all

    public DataSet countAwardPronouncedReviewAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_AwardProuncedCountReviewall");
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

    #region function to count total pending AwardPronounced all

    public DataSet countAwardPronouncedApproveAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_AwardProuncedCountApproveall");
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

    #region function to count total pending Banner

    public string countBannerPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_BannerCountPending"));
            return p_Var.str;
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

    #region function to count total review Banner

    public string countBannerReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_BannerCountReview"));
            return p_Var.str;
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

    #region function to count total approval Banner

    public string countBannerApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_BannerCountApproval"));
            return p_Var.str;
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

    #region Function to get order details for Appeal petition

    public DataSet getOrdersForAppeal(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@AppealPetition_ID", objpetOB.RPNo);
            return ncmdbObject.ExecuteDataSet("USP_GetOrderFromAppealPetition");
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

    #region function to get email ids to send schedule of hearing email for herc.

    public DataSet getEmailIdForSendingSohMail(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@sohId", petObject.SohTempID);

            return ncmdbObject.ExecuteDataSet("asp_GetSohEmailsForHerc");
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

    #region function to get Mobile numbers to send schedule of hearing sms for herc.

    public DataSet getMobileNumberForSendingSohSms(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@sohId", petObject.SohTempID);
            return ncmdbObject.ExecuteDataSet("asp_GetSohMobileForHerc");
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

    #region function to get email ids to send schedule of hearing email for ombudsman.

    public DataSet getEmailIdForSendingSohOmbudsmanMail(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@sohId", petObject.SohTempID);
            return ncmdbObject.ExecuteDataSet("asp_GetSohEmailsForOmbudsman");
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

    #region function to get mobile numbers to send schedule of hearing sms for ombudsman.

    public DataSet getMobileNumberForSendingSohOmbudsmanSMS(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@sohId", petObject.SohTempID);
            return ncmdbObject.ExecuteDataSet("asp_GetSohMobileForOmbudsman");
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

    #region function to insert message in database for mobile sms testing

    public Int32 insertMobileSMS(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@MobileNumber", petObject.PetitionerMobileNo);
            ncmdbObject.AddParameter("@Message", petObject.Description);
            ncmdbObject.AddParameter("@ModuleID", petObject.ModuleID);
            return ncmdbObject.ExecuteNonQuery("insertMobileSms");
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


    #region function to count total pending profiles

    public DataSet countProfilesPendingAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ProfileCountPendingAll");
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

    #region function to count total review profiles

    public DataSet countProfilesReviewAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ProfileCountReviewAll");
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

    #region function to count total approval profiles

    public DataSet countProfilesApprovalAll()
    {
        try
        {
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_ProfileCountApprovalAll");
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

    #region function to count total pending whatsnew

    public string countwhatsnewPending()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_whatsNewCountPending"));
            return p_Var.str;
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

    #region function to count total review whatsnew

    public string countwhatsnewReview()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_BannerCountReview"));
            return p_Var.str;
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

    #region function to count total approval whatsnew

    public string countwhatsnewApproval()
    {
        try
        {
            p_Var.str = Convert.ToString(ncmdbObject.ExecuteScalar("ASP_whatsNewCountApproval"));
            return p_Var.str;
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

    #region Function to get audit details

    public DataSet AuditTrailRecords(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@petitionID", petObject.PetitionId);
            ncmdbObject.AddParameter("@ModuleType", petObject.ModuleType);
            ncmdbObject.AddParameter("@ModuleId", petObject.ModuleID); 
            return ncmdbObject.ExecuteDataSet("asp_getAuditReprot");
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

    public DataSet getPetitionURLwithFile(PetitionOB petitionObject)
    {
        try
        {
            ncmdbObject.AddParameter("@Petition_ID", petitionObject.PetitionId);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("usp_getURlname");
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

    public DataSet getScheduleOfHearingURLwithFile(PetitionOB soh)
    {
        try
        {
            ncmdbObject.AddParameter("@Soh_ID", soh.soh_ID);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("usp_getSohURlname");
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

    public DataSet getPetitionURLwithFileReview(PetitionOB petitionObject)
    {
        try
        {
            ncmdbObject.AddParameter("@Petition_ID", petitionObject.RPNo);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("usp_getURlnameReview");
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

    //End
}


