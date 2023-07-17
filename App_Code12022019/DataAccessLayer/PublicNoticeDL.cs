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
using NCM.DAL;
using System.Data.SqlClient;

public class PublicNoticeDL
{
    #region Default constructors zone

    public PublicNoticeDL()
	{

    }

    #endregion

    //Area for all the variables declaration zone

    NCMdbAccess ncmdbObject = new NCMdbAccess();
    Project_Variables p_Var = new Project_Variables();

    //End

    //Area for all the functions to insert, update and delete

    #region Function to insert new public notice

    public Int32 PublicNoticeInsertUpdate(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@ActionType", petObject.ActionType);
            ncmdbObject.AddParameter("@PublicNoticeID_tmp", petObject.TmpPublicNoticeID);
            ncmdbObject.AddParameter("@PublicNoticeID", petObject.PublicNoticeID);
            ncmdbObject.AddParameter("@Module_Id", petObject.ModuleID);
            ncmdbObject.AddParameter("@PublicNotice", petObject.PublicNotice);
            ncmdbObject.AddParameter("@Title", petObject.Title);
            ncmdbObject.AddParameter("@Description", petObject.Description);
            ncmdbObject.AddParameter("@Status_Id", petObject.StatusId);
            ncmdbObject.AddParameter("@Lang_Id", petObject.LangId);
            ncmdbObject.AddParameter("@Start_Date", petObject.StartDate);
            ncmdbObject.AddParameter("@End_Date", petObject.EndDate);
            ncmdbObject.AddParameter("@PetitionType", petObject.PetitionType);
            ncmdbObject.AddParameter("@Inserted_By", petObject.InsertedBy);
			ncmdbObject.AddParameter("@Last_Updated_By", petObject.LastUpdatedBy);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.AddParameter("@PlaceHolderFive", petObject.PlaceHolderFive);
            ncmdbObject.AddParameter("@PlaceHolderTwo", petObject.ApplicantEmail);
            ncmdbObject.AddParameter("@PlaceHolderThree", petObject.ApplicantMobileNo);

            ncmdbObject.AddParameter("@PlaceHolderSix", petObject.PlaceHolderSix);
            ncmdbObject.AddParameter("@PlaceHolderSeven", petObject.PlaceHolderSeven);
            ncmdbObject.AddParameter("@MetaKeyWords", petObject.MetaKeyWords);
            ncmdbObject.AddParameter("@MetaDescription", petObject.MetaDescription);
            ncmdbObject.AddParameter("@MetaTitle", petObject.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", petObject.MetaKeyLanguage);

            ncmdbObject.ExecuteNonQuery("ASP_tmp_PublicNotice_Insert_Update");
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

    #region Function to update the public notice status

    public Int32 PublicNoticeUpdateStatus(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", petObject.StatusId);
            ncmdbObject.AddParameter("@PublicNoticeID_tmp", petObject.TmpPublicNoticeID);
            ncmdbObject.AddParameter("@UserID", petObject.userID);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Temp_PublicNotice_Change_status");
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

    #region Function to approve public notice and send it into final table

    public Int32 PublicNoticeApprove(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@PublicNoticeID_tmp", petObject.TmpPublicNoticeID);
            ncmdbObject.AddParameter("@IPAddress", petObject.IpAddress);
            ncmdbObject.AddParameter("@UserID", petObject.userID);
            ncmdbObject.ExecuteNonQuery("insert_update_Web_PublicNotice");
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

    //Area for all the functions to display records

    #region Function To Display public notice with paging

    public DataSet DisplayPublicNoticesWithPaging(PetitionOB petObject, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Lang_Id", petObject.LangId);
            ncmdbObject.AddParameter("@status_id", petObject.StatusId);
            ncmdbObject.AddParameter("@module_Id", petObject.ModuleID);
            ncmdbObject.AddParameter("@PetitionType", petObject.PetitionType);
            //ncmdbObject.AddParameter("@DepartmentID", petObject.DepttId);
            ncmdbObject.AddParameter("@PageIndex", petObject.PageIndex);
            ncmdbObject.AddParameter("@PageSize", petObject.PageSize);
            ncmdbObject.AddParameter("@Year",petObject.year);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_Tmp_PublicNotice_Display");
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

    #region Function to display public notice by id for Editing

    public DataSet DisplayPublicNoticeByID(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@status_id", petObject.StatusId);
            ncmdbObject.AddParameter("@PublicNoticeID_tmp", petObject.TmpPublicNoticeID);
            return ncmdbObject.ExecuteDataSet("ASP_Tmp_PublicNoice_Display_Edit");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    #region Function to get public notice id  for edit 

    public DataSet PublicNoticeId(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@PublicNoticeID", petObject.PublicNoticeID);
            return ncmdbObject.ExecuteDataSet("ASP_web_PublicNotice_Id_ForEdit");
        }
        catch
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

    ////#region Function to get Petition numbers for connections

    ////public DataSet getPetitionNumberForConnection(PetitionOB petObject)
    ////{
    ////    try
    ////    {
    ////        ncmdbObject.AddParameter("@year", petObject.year);
    ////        return ncmdbObject.ExecuteDataSet("ASP_GetApprovedPetitionNumberConnected");
    ////    }
    ////    catch
    ////    {
    ////        throw;
    ////    }
    ////    finally
    ////    {
    ////        ncmdbObject.Dispose();
    ////    }

    ////}

    ////#endregion

    #region Function to insert PublicNoticeWithPetition

    public Int32 insertPublicNoticewithPetition(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_id", petObject.ConnectedPetitionID));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", petObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@PublicNoticeID", petObject.PublicNoticeID));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@ReviewId", petObject.RPId));
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedPetitionWithPublicNotice_Insert_Update");
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

    public DataSet getPetitionWithPublicNoticeEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@PublicNoticeID", petObject.PublicNoticeID));

            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedPetitionWithPublicNotice");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get connected PublicNotice for Petition(For Edit)

    public DataSet get_ConnectedPubicNotice_Edit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@PublicNoticeID", petObject.PublicNoticeID));
            ncmdbObject.Parameters.Add(new SqlParameter("@PetitionType", petObject.PetitionType));
            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedPetitionWithPublicNoticeEdit");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get connected PublicNotice for Petition(For new editing)

    public DataSet getConnectedPubicNoticeEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@PublicNoticeID", petObject.PublicNoticeID));
           // ncmdbObject.Parameters.Add(new SqlParameter("@PetitionType", petObject.PetitionType));
            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedPetitionWithPubNotice");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    #region Function to delete Connectedpetition from public notice

    public Int32 deleteConnectedPetitionFromPublicNotice(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@PublicNoticeID", petObject.PublicNoticeID));
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedPetitionWithPublicNotice_Delete");
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


    #region Function To Display public notice on Index page

    public DataSet displayPublicNotice(LinkOB lnkObject)
    {
        try
        {

            ncmdbObject.AddParameter("@LangId", lnkObject.LangId);



            return p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_Get_PublicNotice");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    #region function to get publicNotice information
    public DataSet Get_publiceNotceDetails(LinkOB objlinkOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            ncmdbObject.AddParameter("@PageIndex", objlinkOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", objlinkOB.PageSize);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_GetPublicNoticedetails");
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

    #region function to get publicNotice information old
    public DataSet Get_publiceNotceoldDetails(LinkOB objlinkOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Year", objlinkOB.Year);
            ncmdbObject.AddParameter("@PageIndex", objlinkOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", objlinkOB.PageSize);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_GetPublicNoticeolddetails");
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

    #region function to get year of Petition Admin

    public DataSet GetYearPetition_Admin(LinkOB objlinkOB)
    {
        try
        {
            ncmdbObject.AddParameter("@PetitionReviewId", objlinkOB.CatId);
            return ncmdbObject.ExecuteDataSet("ASP_GetYearPetition_Admin");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get Petition numbers for connections in Public Notices

    public DataSet getPetitionNumberForConnection(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@year", petObject.year);
            ncmdbObject.AddParameter("@categoryId", petObject.RPId);
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedPetitionNumberConnected");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region Function to Delete the public notices either from temp or from final

    public Int32 Delete_PublicNotices(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@StatusId", petObject.StatusId);
            ncmdbObject.AddParameter("@PublicNoticeID_tmp", petObject.TmpPublicNoticeID);
            ncmdbObject.ExecuteNonQuery("ASP_Delete_PublicNotices");
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



    #region function to get Connected publicNotice information old
    public DataSet Get_ConnectedpubliceNotce(PetitionOB petObject)
    {

        try
        {
            ncmdbObject.AddParameter("@PetitionId", petObject.PetitionId);
            return ncmdbObject.ExecuteDataSet("USP_GetConnectedPublicNoticedetails");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }
    #endregion



    #region function to get Connected publicNotice information old
    public DataSet Get_ConnectedpubliceNotceForReview(PetitionOB petObject)
    {

        try
        {
            ncmdbObject.AddParameter("@ReviewPetitionId", petObject.RPId);
            return ncmdbObject.ExecuteDataSet("USP_GetConnectedPublicNoticeReviewPetition");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }
    #endregion


    #region Function to update the  public notice status for delete

    public Int32 updatePublicStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            //ncmdbObject.AddParameter("@Status_Id", rtiObject.StatusId);
            ncmdbObject.AddParameter("@PublicNoticeID", rtiObject.PublicNoticeID);
            ncmdbObject.ExecuteNonQuery("ASP_PublicNotice_ChangestatusDelete");
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


    #region function to get Connected publicNotice information Id
    public DataSet Get_ConnectedpubliceNotceById(PetitionOB petObject)
    {

        try
        {
            ncmdbObject.AddParameter("@PublicNoticeID", petObject.PublicNoticeID);
            return ncmdbObject.ExecuteDataSet("USP_GetPublicNoticedetailsById");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }
    #endregion


    #region function to get year of Public Notice Admin

    public DataSet GetYearPublicNotice_Admin()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYearPublicNotice_Admin");
        }
        catch
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

    public Int32 insertConnectedPublicNoticeFiles(PetitionOB petObject)
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
            ncmdbObject.AddParameter("@PublicNoticeId", petObject.PublicNoticeID);
            ncmdbObject.AddParameter("@Comments", petObject.Description);
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedPublicNoticesFiles_Insert_Update");
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

    #region Function to get FileName for public notices

    public DataSet getFileNameForPublicNotice(PetitionOB publicNoticeObject)
    {
        try
        {
            ncmdbObject.AddParameter("@PublicNoticeId", publicNoticeObject.PublicNoticeID);
            return ncmdbObject.ExecuteDataSet("ASP_GetFileNamePublicNotice");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region Function to Update status for Files in public notices

    public Int32 UpdateFileStatusForPublicNotices(PetitionOB orderObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Id", orderObject.ConnectionID);
            ncmdbObject.ExecuteNonQuery("ASP_Update_FilePetitionPublicNoticesStatus");
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


    #region Function to get connected Public notice file name

    public DataSet getPublicNoticeFileNames(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@PublicNoticeId", objpetOB.PublicNoticeID);
            return ncmdbObject.ExecuteDataSet("USP_GetPublicNoticeId_FileName");
        }
        catch
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

    public DataSet GetPublicNoticesYear()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("USP_GetPublicNoticeYear");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region function to get publicNotice information

    public DataSet Get_publiceNotceDetailsforReviewPetition(LinkOB objlinkOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            ncmdbObject.AddParameter("@PageIndex", objlinkOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", objlinkOB.PageSize);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_GetPublicNoticedetailsforReviewPetition");
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


    #region function to get publicNotice information old

    public DataSet Get_publiceNotceoldDetailsForReviewPetition(LinkOB objlinkOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Year", objlinkOB.Year);
            ncmdbObject.AddParameter("@PageIndex", objlinkOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", objlinkOB.PageSize);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_GetPublicNoticeolddetailsforReviewPetition");
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


    #region function to get Connected publicNotice information Id for Details
    public DataSet Get_ConnectedpubliceNotceByIdDetails(PetitionOB petObject)
    {

        try
        {
            ncmdbObject.AddParameter("@PublicNoticeID", petObject.PublicNoticeID);
            return ncmdbObject.ExecuteDataSet("GetPublicNotice");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }
    #endregion


    #region Function to get PublicNotic year for connections

    public DataSet getPublicNoticYearForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@PublicNoticeID", petObject.PublicNoticeID);

            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedPublicNoticeYearEdit");
        }
        catch
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

    public DataSet getEmailIdForSendingMail(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@publicNoticeId", petObject.TmpPublicNoticeID);
            return ncmdbObject.ExecuteDataSet("asp_GetPublicNoticesEmails");
        }
        catch
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

    public DataSet getEmailIdForSendingSms(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@publicNoticeId", petObject.TmpPublicNoticeID);
            return ncmdbObject.ExecuteDataSet("asp_GetPublicNoticesMobileNumber");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    public DataSet getPublicNoticeURLwithFile(PetitionOB publicNoticeObject)
    {
        try
        {
            ncmdbObject.AddParameter("@PublicNoticeID", publicNoticeObject.PublicNoticeID);
            p_Var.dSet = ncmdbObject.ExecuteDataSet("usp_getPublicNoticeURlname");
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
}
