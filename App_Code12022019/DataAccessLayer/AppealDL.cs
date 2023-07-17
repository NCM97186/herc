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

public class AppealDL
{
    #region Default constructors zone

    public AppealDL()
	{
		
    }

    #endregion

    //Area for all the variables declaration zone

    #region Data declaration zone

    NCMdbAccess ncmdbObject = new NCMdbAccess();
    Project_Variables p_var = new Project_Variables();

    #endregion

    //End

    //Area for all the user defined functions to insert and update

    #region Function to insert Appeal records into temp table

    public Int32 insertUpdateTempAppeal(PetitionOB appealObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", appealObject.ActionType));
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal_Number", appealObject.AppealNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Deptt_Id", appealObject.DepttId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", appealObject.year));

            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Name", appealObject.ApplicantName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Email", appealObject.ApplicantEmail));
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal_Date", appealObject.AppealDate));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Mobile_No", appealObject.ApplicantMobileNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Phone_No", appealObject.ApplicantPhoneNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Fax_No", appealObject.ApplicantFaxNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Applicant_Address", appealObject.ApplicantAddress));


            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Name", appealObject.RespondentName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Email", appealObject.Respondentmail));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Mobile_No", appealObject.RespondentMobileNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Phone_No", appealObject.RespondentPhone_No));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Fax_No", appealObject.RespondentFaxNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Address", appealObject.RespondentAddress));

            ncmdbObject.Parameters.Add(new SqlParameter("@Subject", appealObject.subject));
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal_Status_Id", appealObject.AppealStatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", appealObject.StatusId));

            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", appealObject.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Inserted_By", appealObject.InsertedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Last_Updated_By", appealObject.LastUpdatedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Appeal_Id", appealObject.TempAppealId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Old_Appeal_Id", appealObject.OldAppealId));

            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal", appealObject.appeal));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderOne", appealObject.PlaceHolderOne));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderTwo", appealObject.PlaceHolderTwo));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderThree", appealObject.PlaceHolderThree));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderFour", appealObject.PlaceHolderFour));
            ncmdbObject.Parameters.Add(new SqlParameter("@PlaceholderFive", appealObject.PlaceHolderFive));
            ncmdbObject.Parameters.Add(new SqlParameter("@Remarks", appealObject.Remarks));

            ncmdbObject.AddParameter("@MetaKeyWords", appealObject.MetaKeyWords);
            ncmdbObject.AddParameter("@MetaDescription", appealObject.MetaDescription);
            ncmdbObject.AddParameter("@MetaTitle", appealObject.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", appealObject.MetaKeyLanguage);
            
            ncmdbObject.AddParameter("@IPAddress", appealObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_Appeal_Insert_Update");
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

    #region Function to update the temporary appeal status

    public Int32 updateAppealStatus(PetitionOB appealObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", appealObject.StatusId);
            ncmdbObject.AddParameter("@Temp_Appeal_Id", appealObject.TempAppealId);
            ncmdbObject.AddParameter("@UserID", appealObject.userID);
            ncmdbObject.AddParameter("@IPAddress", appealObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_tmp_Appeal_Change_status");
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

    #region Function To insert appeal into web final table

    public Int32 InsertAppealIntoWeb(PetitionOB appealObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Temp_Appeal_Id", appealObject.TempAppealId);
            ncmdbObject.AddParameter("@UserID", appealObject.userID);
            ncmdbObject.AddParameter("@IPAddress", appealObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Web_Appeal_Insert_Update");
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

    #region Function to get temporary appeal records

    public DataSet getTempAppealRecords(PetitionOB appealObject, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Appeal_Id", appealObject.TempAppealId));
            ncmdbObject.AddParameter("@DepartmentID", appealObject.DepttId);
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", appealObject.StatusId));
            ncmdbObject.AddParameter("@year", appealObject.year);
            ncmdbObject.AddParameter("@PageIndex", appealObject.PageIndex);
            ncmdbObject.AddParameter("@PageSize", appealObject.PageSize);
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", appealObject.LangId));
            p_var.dSet = ncmdbObject.ExecuteDataSet("ASP_Tmp_Appeal_Display");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to get appeal number

    public DataSet getAppealNumber(PetitionOB appealObject)
    {
        try 
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal_No", appealObject.AppealNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", appealObject.year));
            return ncmdbObject.ExecuteDataSet("ASP_Appeal_Appeal_Number");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get appeal record for edit

    public DataSet getAppealRecordForEdit(PetitionOB appealObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Appeal_Id", appealObject.TempAppealId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", appealObject.StatusId));
            return ncmdbObject.ExecuteDataSet("ASP_Tmp_Appeal_DisplayForEdit");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get appeal record 

    public DataSet getAppealRecord(PetitionOB appealObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Appeal_Id", appealObject.TempAppealId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", appealObject.StatusId));
            return ncmdbObject.ExecuteDataSet("USP_Tmp_AppealDisplay");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get appeal status

    public DataSet getAppealStatus()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("GetAppealStatus");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get appeal id from temp table for comparision

    public DataSet get_ID_For_Compare(PetitionOB appealObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal_Id", appealObject.AppealId));
            return ncmdbObject.ExecuteDataSet("ASP_Web_Appeal_Id");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get Appeal numbers in edit mode

    public DataSet getAppealNumberInEditMode(PetitionOB appealObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal_Number", appealObject.AppealNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", appealObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Appeal_Id", appealObject.TempAppealId));
            return ncmdbObject.ExecuteDataSet("ASP_Appeal_Number_In_EditMode");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to update the appeal status

    public Int32 modifyAppealStatus(PetitionOB appealObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Appeal_Status_Id", appealObject.AppealStatusId);
            ncmdbObject.AddParameter("@Filename", appealObject.FileName);
            ncmdbObject.AddParameter("@Appeal_Id", appealObject.AppealId);
            ncmdbObject.AddParameter("@Where_Appealed", appealObject.WhereAppealed);
            ncmdbObject.ExecuteNonQuery("ASP_Appeal_UpdatePetitionStatus");
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

    //Area for Award pronounced for the appeal date from 06-11-2012

    #region Function to get appeal numbers

    public DataSet getAppealNumberforAwardPronounced(PetitionOB appealObject)
    {
        try
        {
            ncmdbObject.AddParameter("@year", appealObject.year);
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedAppealNoForAward");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region Function to get appeal numbers for edit

    public DataSet getAppealNumberforAwardPronouncedEdit(PetitionOB appealObject)
    {
        try
        {
            ncmdbObject.AddParameter("@year", appealObject.year);
            ncmdbObject.AddParameter("@AppealID", appealObject.AppealId);
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedAppealNoForAwardEdit");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region Function to get temp/final Award pronounced (Conditional) 

    public DataSet getAwardPronounced(PetitionOB petObject, out int catValue)//Created on date 07-11-2012
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RA_Id", petObject.TempRAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", petObject.StatusId));
            ncmdbObject.AddParameter("@PageIndex", petObject.PageIndex);
            ncmdbObject.AddParameter("@PageSize", petObject.PageSize);
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            ncmdbObject.AddParameter("@Year",petObject.year);
            p_var.dSet = ncmdbObject.ExecuteDataSet("ASP_Review_AppealDisplay");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to update the appeal award status

    public Int32 AppealAwardUpdateStatus(PetitionOB orderObject) //For award Pronounced 
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", orderObject.StatusId);
            ncmdbObject.AddParameter("@Temp_RA_Id", orderObject.TempRAId);
            ncmdbObject.AddParameter("@UserID", orderObject.userID);
            ncmdbObject.AddParameter("@IPAddress", orderObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Web_Review_Appeal_Change_status");
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

    #region Function To insert record into web_AppealReview

    public Int32 ApproveAwardPronounced(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Temp_RA_Id", petObject.TempRAId);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.AddParameter("@UserID", petObject.userID);
            ncmdbObject.ExecuteNonQuery("ASP_Web_Review_Appeal_Insert_Update");
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

    #region Function to get Temp petition(For Edit)

    public DataSet getAppealAwardForEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_RA_Id", petObject.TempRAId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", petObject.LangId));
            return ncmdbObject.ExecuteDataSet("ASP_Tmp_ReviewAppeal_Display_Edit");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get Review Appeal ID from temp table for comparision

    public DataSet ReviewAppealIDforComparision(PetitionOB orderObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RA_Id", orderObject.TempRAId));
            return ncmdbObject.ExecuteDataSet("ASP_Web_Review_Appeal_RA_ID");
        }
        catch
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


    //Area for all the user defined function to insert and update date from 06-11-2012

    #region Function to insert-update temp reviewAppeal table records

    public Int32 insertUpdateReviewAppealTemp(PetitionOB orderObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@ActionType", orderObject.ActionType);
            ncmdbObject.AddParameter("@Temp_RA_Id", orderObject.TempRAId);
            ncmdbObject.AddParameter("@Old_RA_Id", orderObject.oldRaID);
            ncmdbObject.AddParameter("@Appeal_Id", orderObject.AppealId);
            ncmdbObject.AddParameter("@Appeal_Number", orderObject.AppealNo);
            ncmdbObject.AddParameter("@RA_Status_Id", orderObject.RAStatusId);
            ncmdbObject.AddParameter("@File_Name", orderObject.FileName);

            ncmdbObject.AddParameter("@Where_Appealed", orderObject.WhereAppealed);
            ncmdbObject.AddParameter("@Judgement_Link", orderObject.JudgementLink);
            ncmdbObject.AddParameter("@Status_Id", orderObject.StatusId);
           // ncmdbObject.AddParameter("@Old_RA_Id", orderObject.old);
            ncmdbObject.AddParameter("@Lang_Id", orderObject.LangId);
            ncmdbObject.AddParameter("@Inserted_By", orderObject.recordInsertedBy);
            ncmdbObject.AddParameter("@Last_Updated_By", orderObject.recordUpdatedBy);

            ncmdbObject.AddParameter("@AwardDate", orderObject.AwardDate);
            ncmdbObject.AddParameter("@Rec_Insert_Date", orderObject.RecInsertDate);
            ncmdbObject.AddParameter("@Rec_Update_Date", orderObject.RecUpdateDate);
           // ncmdbObject.AddParameter("@InsertedDate", orderObject.RecInsertDate);

            ncmdbObject.AddParameter("@PlaceholderOne", orderObject.OrderTitle);
            ncmdbObject.AddParameter("@PlaceholderTwo", orderObject.OrderDescription);
            ncmdbObject.AddParameter("@PlaceholderThree", orderObject.PlaceHolderThree);
            ncmdbObject.AddParameter("@PlaceholderFour", orderObject.PlaceHolderFour);
            ncmdbObject.AddParameter("@PlaceholderFive", orderObject.PlaceHolderFive);
            ncmdbObject.AddParameter("@AppealYear", orderObject.year);

            ncmdbObject.AddParameter("@MetaKeyWords", orderObject.MetaKeyWords);
            ncmdbObject.AddParameter("@MetaDescription", orderObject.MetaDescription);
            ncmdbObject.AddParameter("@MetaTitle", orderObject.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", orderObject.MetaKeyLanguage);
            ncmdbObject.AddParameter("@IPAddress", orderObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_Review_Appeal_Insert_Update");
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
    //  Display clint side Appeal
    #region funcation to access record of appeal
    public DataSet Get_appeal(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_Getombudsman_appeal");
            catValue = Convert.ToInt16(param[0].Value);
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
    //End

    //  Display clint side Appeal Prevyear
    #region funcation to access record of appeal prev year
    public DataSet Get_appeal_prevyear(PetitionOB obj_petOB, out int catValue)
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


            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_Getappeal_PrevYear");
            catValue = Convert.ToInt16(param[0].Value);
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
    //End

   


    #region function to get online status information
    public DataSet Get_OnlineStatus(PetitionOB obj_petOB)
    {

        try
        {

            ncmdbObject.AddParameter("@appealNumber", obj_petOB.AppealNo);
            ncmdbObject.AddParameter("@Year", obj_petOB.year);
            return ncmdbObject.ExecuteDataSet("USP_Getappeal_onlineStatus");

        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }
    #endregion 

    //*********************************Award Pronounced User Side *******************************************//
    #region Function to get current Year Award Pronouncde
    public DataSet Get_Award_pronounced_CurrentYear(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@Lang_Id", obj_petOB.LangId);
            ncmdbObject.AddParameter("@Deptid", obj_petOB.DepttId);


            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_CurrentYear_Award");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to get current Year Award Pronouncd for what's new section

    public DataSet GetWhatsNewAwards(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@Lang_Id", obj_petOB.LangId);
            ncmdbObject.AddParameter("@Deptid", obj_petOB.DepttId);


            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_latest_Award");
            catValue = Convert.ToInt16(param[0].Value);
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


    //Appeal detail By AppealId
    #region
    public DataSet Get_Appeal_Detail_By_ID(PetitionOB obj_petOB)
    {
        try
        {
            ncmdbObject.AddParameter("@appealno", obj_petOB.AppealNo);


            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_GetappealDetail_AppealNo");
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

    #region Function to get Previous Year Award Pronouncde
    public DataSet Get_Award_pronounced_PrevYear(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@Lang_Id", obj_petOB.LangId);
            ncmdbObject.AddParameter("@Deptid", obj_petOB.DepttId);
            ncmdbObject.AddParameter("@year", obj_petOB.year);

            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_PrevYear_Award");
            catValue = Convert.ToInt16(param[0].Value);
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
    //  Function to search award
    #region
    public DataSet Search_AwardRecord(PetitionOB obj_petOB, string strsearchcond)
    {
        try
        {
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            //ncmdbObject.AddParameter("@Lang_Id", obj_petOB.LangId);
            //7ncmdbObject.AddParameter("@Deptid", obj_petOB.DepttId);

            ncmdbObject.AddParameter("@searchcondition", strsearchcond);
            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_Award_Search");
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

    //End


    #region function to get years of awards

    public DataSet GetAwardYear()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("USP_GetYear_AwardPronounced");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    #region function to get years of award under appeal

    public DataSet GetAwardUnderAppealYear()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("USP_GetYear_AwardUnderAppeal");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 


    #region function to get years of award underappeal

    public DataSet GetAward_UnderAppealYear()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("USP_GetYearAwardUnderAppeal");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    #region function to get years for Award Under Appeal for ombudsman

    public DataSet GetAwardUnderAppealYearOmbudsman()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("USP_YearForAwardUnderAppealOmbudsman");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    #region function to get AppealNumber of award 

    public DataSet GetAppealNumberAward(PetitionOB obj_petOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Year",obj_petOB.year);
            return ncmdbObject.ExecuteDataSet("USP_GetAppealNumberAward");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 



    #region Function to get current Year Award Pronouncde

    public DataSet GetAwardUnderAppeal(PetitionOB obj_petOB, out int catValue)
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
            //ncmdbObject.AddParameter("@Deptid", obj_petOB.DepttId);
            ncmdbObject.AddParameter("@year", obj_petOB.year);

            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_AwardUnderAppeal");
            catValue = Convert.ToInt16(param[0].Value);
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


    #region Function to get current Year Award Pronounced details

    public DataSet GetAwardUnderAppealDetails(PetitionOB obj_petOB)
    {
        try
        {
            
            //ncmdbObject.AddParameter("@year", obj_petOB.year);
            ncmdbObject.AddParameter("@AppealId", obj_petOB.AppealId);
            return ncmdbObject.ExecuteDataSet("USP_AwardUnderAppealDetails");
          

        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion





    #region function to get appeal Id

    public DataSet Get_Appeal(PetitionOB obj_petOB)
    {
        try
        {
           ncmdbObject.AddParameter("@year", obj_petOB.year);
           return ncmdbObject.ExecuteDataSet("ASP_GetApprovedAppealForScheduleOfHearing");
        }
        catch
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

    public DataSet GetYear(PetitionOB appealObject)
    {
        try
        {
            ncmdbObject.AddParameter("@moduleId", appealObject.ModuleID);
            return ncmdbObject.ExecuteDataSet("ASP_GetAppealYear");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 



    #region function to bind year from AppealAwardPronouncedTmp(both table)

    public DataSet GetYearAward()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetAppealAwardYear");
        }
        catch
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

    public Int32 Delete_Pending_Approved_RecordAward(PetitionOB objpet)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            ncmdbObject.AddParameter("@Old_RA_Id_tmp", objpet.AppealId);
            ncmdbObject.AddParameter("@StatusId", objpet.StatusId);  
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.ExecuteNonQuery("ASP_Delete_AwardProunced");


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


    #region Function to update Web_Review_Appeal for Restore

    public Int32 Web_Review_Appeal_Restore(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
 
            ncmdbObject.AddParameter("@RA_Id", rtiObject.AppealId);
            ncmdbObject.ExecuteNonQuery("ASP_Web_Review_Appeal_Restore");
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


    #region Function to get connected SOH of Appeal

    public DataSet getConnectedSOH_Appeal(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Appeal_ID", objpetOB.AppealId);
            return ncmdbObject.ExecuteDataSet("USP_GetSOH_Appeal");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    #region Function to get connected Award Appeal files

    public DataSet getConnectedAwardApealFiles(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@appealId", objpetOB.AppealId);
            return ncmdbObject.ExecuteDataSet("USP_GetConnectedAwardUnderAppeal");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    #region Function to getAward Under Appeals

    public DataSet getAwardUnderAppealDetails(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@appealId", objpetOB.AppealId);
            return ncmdbObject.ExecuteDataSet("USP_GetAwardUnderAppeal");
        }
        catch
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

    public DataSet Get_Year_Appeal()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("USP_GetYearAppeal");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to GetAwardForAppeal 

    public DataSet GetAwardForAppeal(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Appeal_ID", objpetOB.AppealId);
            return ncmdbObject.ExecuteDataSet("USP_GetAwardForAppeal");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region function to get year of Award Admin

    public DataSet GetYearAward_Admin()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYearAward_Admin");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get Appeal number during deleting record from reviewAppeal table

    public DataSet getAppeal_Number_for_DeleteReviewAppeal(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@RA_Id", petObject.AppealId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_AppealNumber_from_ReviewAppeal");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Functiont to delete AppealReview either temp or final

    public Int32 Delete_AppealReview(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            ncmdbObject.AddParameter("@Temp_Appeal_Id", petObject.AppealId);
            ncmdbObject.AddParameter("@Status_ID", petObject.StatusId);
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_ReviewAppeal_Petition_Delete");
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

    #region Functiont to delete AppealReview with award either temp or final

    public Int32 Delete_AwardwithAppealReview(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            ncmdbObject.AddParameter("@Temp_Appeal_Id", petObject.AppealId);
            ncmdbObject.AddParameter("@Status_ID", petObject.StatusId);
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_AppealAward_Delete");
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

    #region Function to update Web_Review_Appeal and Award also for Restore

    public Int32 AppealAward_Restore(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);

            ncmdbObject.AddParameter("@Temp_Appeal_Id", rtiObject.AppealId);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_AppealAward_Restore");
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

    #region Function To insert appealAward into web final table

    public Int32 InsertAppealAwardTmp(PetitionOB appealObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@ActionType", appealObject.ActionType);
            ncmdbObject.AddParameter("@Appeal_Id", appealObject.AppealId);
            ncmdbObject.AddParameter("@TmpID", appealObject.TempAppealId);
            ncmdbObject.AddParameter("@Status_Id", appealObject.StatusId);
            if (appealObject.FileName != null && appealObject.FileName != "")
            {
                ncmdbObject.AddParameter("@File_Name", appealObject.FileName);
            }
            else
            {
                ncmdbObject.AddParameter("@File_Name", null);
            }
            ncmdbObject.AddParameter("@Appeal_Date", appealObject.Date);
            ncmdbObject.AddParameter("@ReferenceNumber", appealObject.RefNo);
            ncmdbObject.AddParameter("@AwardStatusId", appealObject.PAStatusId);
            ncmdbObject.AddParameter("@Where_Appealed", appealObject.WhereAppealed);
            ncmdbObject.AddParameter("@Judgement_Link", appealObject.JudgementLink);
            ncmdbObject.AddParameter("@Remarks",appealObject.Remarks);
            ncmdbObject.AddParameter("@Description", appealObject.subject);
            ncmdbObject.AddParameter("@MetaKeyWords", appealObject.MetaKeyWords);
            ncmdbObject.AddParameter("@MetaDescription", appealObject.MetaDescription);
            ncmdbObject.AddParameter("@MetaTitle", appealObject.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", appealObject.MetaKeyLanguage);
            ncmdbObject.AddParameter("@OtherDescription", appealObject.Description);
            ncmdbObject.AddParameter("@ID", appealObject.OldAppealId);
            ncmdbObject.AddParameter("@IPAddress", appealObject.IpAddress);
            ncmdbObject.AddParameter("@PlaceholderOne", appealObject.PlaceHolderFour);

            ncmdbObject.AddParameter("@Inserted_By", appealObject.InsertedBy);
            ncmdbObject.AddParameter("@Last_Updated_By", appealObject.LastUpdatedBy);


            ncmdbObject.ExecuteNonQuery("ASP_Tmp_AppealAward_Insert_Update");
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

    #region Function to get temporary appeal Award records

    public DataSet getTempAppealAwardRecords(PetitionOB appealObject, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Appeal_Id", appealObject.TempAppealId));
           
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", appealObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", appealObject.year));
            ncmdbObject.AddParameter("@PageIndex", appealObject.PageIndex);
            ncmdbObject.AddParameter("@PageSize", appealObject.PageSize);

            p_var.dSet = ncmdbObject.ExecuteDataSet("ASP_Tmp_AppealAward_Display");
            catValue = Convert.ToInt16(param[0].Value);
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


    #region Function to update the temporary appeal Award status

    public Int32 updateAppealAwardStatus(PetitionOB appealObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", appealObject.StatusId);
            ncmdbObject.AddParameter("@TmpID", appealObject.TempAppealId);
            ncmdbObject.AddParameter("@IPAddress", appealObject.IpAddress);
            ncmdbObject.AddParameter("@UserID", appealObject.userID);
            ncmdbObject.ExecuteNonQuery("ASP_tmp_AppealAward_Change_status");
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

    #region Function To insert appeal into web final table

    public Int32 InsertAppealAwardIntoWeb(PetitionOB appealObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@TmpID", appealObject.TempAppealId);
            ncmdbObject.AddParameter("@IPAddress", appealObject.IpAddress);
            ncmdbObject.AddParameter("@UserID", appealObject.userID);
            ncmdbObject.ExecuteNonQuery("ASP_Web_AppealAward_Insert_Update");
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

    #region Function To Get appeal 

    public DataSet GETAppealID_Award(PetitionOB appealObject)
    {
        try
        {

            ncmdbObject.AddParameter("@AppealID", appealObject.TempAppealId);
            ncmdbObject.AddParameter("@StatusId",appealObject.StatusId);
            return  ncmdbObject.ExecuteDataSet("ASP_GetApealIdFrom_AppealAward");
           
           
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 


    #region Function to get appeal Award record for edit

    public DataSet GetAppealAwardPronouncedTmp(PetitionOB appealObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Appeal_Id", appealObject.AppealId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", appealObject.StatusId));
            return ncmdbObject.ExecuteDataSet("AppealAward_DisplayForEdit");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get appeal number record 

    public DataSet GetAppealNumberDuringAward(PetitionOB appealObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Appeal_Id", appealObject.AppealId));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", appealObject.StatusId));
            return ncmdbObject.ExecuteDataSet("AppealAwardDisplayAppealNumber");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    #region Function to get appealAward id from temp table for comparision

    public DataSet get_ID_For_CompareAward(PetitionOB appealObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@ID", appealObject.AppealId));
            return ncmdbObject.ExecuteDataSet("ASP_AppealAward_Id");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to insert AppealAward  files

    public Int32 insertAppealAwardFiles(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
           
            ncmdbObject.Parameters.Add(new SqlParameter("@FileName", petObject.FileName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.AddParameter("@Date", petObject.StartDate);
            ncmdbObject.AddParameter("@AppealId", petObject.AppealId);
            ncmdbObject.AddParameter("@Comments", petObject.Description);

            ncmdbObject.AddParameter("@TmpID", petObject.ConnectionID);
            ncmdbObject.ExecuteNonQuery("ASP_AppealAwardFiles_Insert");
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

    #region Function to insert AppealAwardPronouncedFiles

    public Int32 insertAppealAwardPronouncedFiles(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            ncmdbObject.Parameters.Add(new SqlParameter("@FileName", petObject.FileName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.AddParameter("@Date", petObject.StartDate);
            ncmdbObject.AddParameter("@AppealId", petObject.AppealId);
            ncmdbObject.AddParameter("@Comments", petObject.Description);

            ncmdbObject.AddParameter("@TmpID", petObject.ConnectionID);
            ncmdbObject.ExecuteNonQuery("ASP_AppealAwardPronouncedFiles_Insert");
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

    #region Function to get FileName for public notices

    public DataSet getFileNameForAppealAward(PetitionOB PetObject)
    {
        try
        {
            ncmdbObject.AddParameter("@AppealId", PetObject.AppealId);
            return ncmdbObject.ExecuteDataSet("ASP_GetFileNameAppealAwardFiles");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region Function to get FileName AppealAwardProunced

    public DataSet getFileNameForAppealAwardProunced(PetitionOB PetObject)
    {
        try
        {
            ncmdbObject.AddParameter("@AppealId", PetObject.AppealId);
            return ncmdbObject.ExecuteDataSet("ASP_AppealAwardPronouncedFiles");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region Function to delete  Files in AppealAward

    public Int32 DeleteFileForAppealAward(PetitionOB PetObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Id", PetObject.ConnectionID);
            ncmdbObject.ExecuteNonQuery("ASP_DeleteFileAppealAward");
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

    #region Function to delete  Files in DeleteAppealAwardPronouncedFiles

    public Int32 DeleteAppealAwardPronouncedFiles(PetitionOB PetObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Id", PetObject.ConnectionID);
            ncmdbObject.ExecuteNonQuery("ASP_DeleteAppealAwardPronouncedFiles");
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

    #region Function to Delete the Appeal Award either from temp or from final

    public Int32 Delete_AppealAward(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@StatusId", petObject.StatusId);
            ncmdbObject.AddParameter("@tmpID", petObject.ConnectionID);
            ncmdbObject.ExecuteNonQuery("ASP_Delete_AppealAward");
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

    #region Function to update AppealAwardPronouncedWeb for Restore

    public Int32 AppealAwardPronouncedWeb_Restore(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);

            ncmdbObject.AddParameter("@TempId", rtiObject.ConnectionID);
            ncmdbObject.ExecuteNonQuery("ASP_AppealAwardPronouncedWeb_Restore");
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

    #region Function to get connected Award of Appeal

    public DataSet getConnectedAwardFiles(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@AppealId", objpetOB.AppealId);
            return ncmdbObject.ExecuteDataSet("USP_getConnectedAwardFile");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get connected Award of Appeal

    public DataSet getAppealAwardPronounced(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@AppealId", objpetOB.AppealId);
            return ncmdbObject.ExecuteDataSet("USP_getAppealAwardPronounced");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get connected AppealAwardPronouncedFiles

    public DataSet getAppealAwardPronouncedFiles(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@AppealId", objpetOB.AppealId);
            return ncmdbObject.ExecuteDataSet("USP_getAppealAwardPronouncedFiles");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    public DataSet SearchAwardRecord(PetitionOB obj_petOB, string strsearchcond)
    {
        try
        {
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            //ncmdbObject.AddParameter("@Lang_Id", obj_petOB.LangId);
            //7ncmdbObject.AddParameter("@Deptid", obj_petOB.DepttId);

            ncmdbObject.AddParameter("@searchcondition", strsearchcond);
            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_Award_Search");
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

    //Display Appeal Search
    #region Appeal Search
    public DataSet Get_appeal_search(PetitionOB obj_petOB, out int catValue)
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
            ncmdbObject.AddParameter("@ActionType", obj_petOB.ActionType);
            ncmdbObject.AddParameter("@Applicant_Name", obj_petOB.ApplicantName);
            ncmdbObject.AddParameter("@RespondentName", obj_petOB.RespondentName);
            ncmdbObject.AddParameter("@Subject", obj_petOB.subject);
     
            ncmdbObject.AddParameter("@Date", obj_petOB.Date);
            ncmdbObject.AddParameter("@AppealNumber", obj_petOB.AppealNo);


            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_APPEALSEARCH");

            catValue = Convert.ToInt16(param[0].Value);
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
    //end
    public DataSet SearchAward(PetitionOB obj_petOB, out int catValue)
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
            p_var.dSet = ncmdbObject.ExecuteDataSet("SearchAward");
            catValue = Convert.ToInt16(param[0].Value);
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

 
    #region function to USP_GetYearAppealForSearch for Appeal

    public DataSet GetYearAppealForSearch()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("USP_GetYearAppealForSearch");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    public DataSet getAppealNumberYearWise(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@year", petObject.year);
            return ncmdbObject.ExecuteDataSet("USP_GetAppealNumber");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #region function to USP_GetYearAwardForSearch for Appeal

    public DataSet GetYearAwardForSearch()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("USPGetYearAwardPronouncedSearch");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    #region Function to get current Year new Award Pronouncde 

    public DataSet GetLatestAward(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@Lang_Id", obj_petOB.LangId);
            ncmdbObject.AddParameter("@Deptid", obj_petOB.DepttId);
            ncmdbObject.AddParameter("@AppealNumber", obj_petOB.AppealNo);

            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_CurrentYear_AwardLatestUpdate");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to get current Year new Appeal

    public DataSet GetLatestAppeal(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
           // ncmdbObject.AddParameter("@year", obj_petOB.year);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@AppealNumber", obj_petOB.AppealNo);
            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_Getombudsman_appealWhatsNew");
            catValue = Convert.ToInt16(param[0].Value);
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

    // 2 Aug 2013

    #region Function to insert ConnectedMultipleReviewAppeal

    public Int32 insertConnectedMultipleReviewAppeal(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@Connected_Review_id", petObject.ConnectedPetitionID));
            //ncmdbObject.Parameters.Add(new SqlParameter("@Petition_id", petObject.PetitionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal_Id", petObject.AppealId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", petObject.year));
            ncmdbObject.ExecuteNonQuery("ASP_MultipleReviewAppeal_Insert_Update");
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

    #region Function to get connected Appeal Review(For Edit)

    public DataSet get_ConnectMultipleReviewAppeal_Edit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal_Id", petObject.AppealId));

            return ncmdbObject.ExecuteDataSet("ASP_Connected_multipleReviewAppeal");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get Appeal Review year for connections

    public DataSet Connected_multipleReviewAppealForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@Appeal_Id", petObject.AppealId);

            return ncmdbObject.ExecuteDataSet("ASP_multiplePetitionReviewEdit");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region Function to delete ConnectedReviewAppeal

    public Int32 deleteConnectedReviewAppealMultiple(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@Appeal_Id", petObject.AppealId));
            ncmdbObject.ExecuteNonQuery("ASP_Connected_ReviewAppealMultiple_Delete");
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

    #region Function to get connected review  appeal Details

    public DataSet get_ConnectedReviewAppealDetails(PetitionOB petObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@PA_Id", petObject.PAId));
            p_var.dSet = ncmdbObject.ExecuteDataSet("Usp_GetReviewAppealMultiple");
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

    #region Function to get connected ReviewAppeal

    public DataSet get_ConnectedReviewAppeal(PetitionOB petObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@PA_Id", petObject.PAId));
            p_var.dSet = ncmdbObject.ExecuteDataSet("usp_getConnectedReviewAppalDetails");
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



    #region Function to get connected Award details

    public DataSet get_AwardDetails(PetitionOB petObject)
    {
        try
        {

            ncmdbObject.Parameters.Add(new SqlParameter("@AppealId", petObject.AppealId));
            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_AwardDetails");
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

    //Functions to get email ids to send emails

    #region function to get email ids to send email.

    public DataSet getEmailIdToSendAppealEmail(PetitionOB appealObject)
    {
        try
        {
            ncmdbObject.AddParameter("@ApealId", appealObject.TempAppealId);
            return ncmdbObject.ExecuteDataSet("asp_GetAppealEmails");
        }
        catch
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


    #region Function to get FileName AppealAwardProunced

    public DataSet getFileNameAwardProunced(PetitionOB PetObject)
    {
        try
        {
            ncmdbObject.AddParameter("@AppealId", PetObject.TempRAId);
            return ncmdbObject.ExecuteDataSet("ASP_GetFileNameAwardFiles");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion



    #region function to get email ids to send Appeal email

    public DataSet getEmailIdForSendingAwardAppealMail(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@AwardId", petObject.AppealId);
            return ncmdbObject.ExecuteDataSet("ASP_getEmailAppeal");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    #region function to get Mobile Number  to send Appeal Sms

    public DataSet getEmailIdForSendingAwardAppealSMS(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@AwardId", petObject.AppealId);
            return ncmdbObject.ExecuteDataSet("ASP_getMobileNumberAppeal");
        }
        catch
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
