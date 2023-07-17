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
using System.Data.SqlClient;
using NCM.DAL;

/// <summary>
/// Summary description for ProfileDL
/// </summary>
public class ProfileDL
{
	public ProfileDL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //Area for all the data declaration

    #region Data declaration zone

    //Area for all the variables declaration zone

    NCMdbAccess ncmdbObject = new NCMdbAccess();
    Project_Variables p_Var = new Project_Variables();
    ProfileOB profileOB = new ProfileOB();

    //End

    #endregion

    //End

    #region Function to insert Connectedpetition

    public int insertConnectedPetition(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt32(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@Connected_Petition_id", petObject.ConnectedPetitionID));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_id", petObject.PetitionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
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

    #region Function to get Navigation Type

    public DataSet getNavigation()
    {
        try
        {
            ncmdbObject.CommandText = "[ASP_Get_Navigation]";
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

    #region function to insert NewProfile

    public int insert_New_Profile(ProfileOB profileOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            ncmdbObject.CommandText = "ASP_Tmp_Profile_Insert_Update_Profile";
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", profileOB.ActionType));


            ncmdbObject.Parameters.Add(new SqlParameter("@Temp_Profile_Id", profileOB.temp_profile_Id));
            ncmdbObject.Parameters.Add(new SqlParameter("@Module_Id", profileOB.ModuleId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Nevigation_Id", profileOB.NevigationId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Name", profileOB.NAME));
            ncmdbObject.Parameters.Add(new SqlParameter("@Designation", profileOB.designation));
            ncmdbObject.Parameters.Add(new SqlParameter("@Subject", profileOB.subject));
            ncmdbObject.Parameters.Add(new SqlParameter("@Epabx_Ext", profileOB.epabxte));
            ncmdbObject.Parameters.Add(new SqlParameter("@Phone", profileOB.phone));
            ncmdbObject.Parameters.Add(new SqlParameter("@Details", profileOB.details));
            ncmdbObject.Parameters.Add(new SqlParameter("@Email", profileOB.email));
            ncmdbObject.Parameters.Add(new SqlParameter("@Image_Name", profileOB.ImageName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", profileOB.StatusId));

            ncmdbObject.Parameters.Add(new SqlParameter("@Profile_Id", profileOB.profile_Id));
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id", profileOB.LangId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Deptt_Id",profileOB.DepttId));
        
            ncmdbObject.Parameters.Add(new SqlParameter("@Start_Date", profileOB.StartDate));
            ncmdbObject.Parameters.Add(new SqlParameter("@End_Date", profileOB.EndDate));

            ncmdbObject.Parameters.Add(new SqlParameter("@Last_Updated_By", profileOB.LastUpdatedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@Inserted_By", profileOB.InsertedBy));
            ncmdbObject.AddParameter("@IpAddress", profileOB.IpAddress);
            ncmdbObject.AddParameter("@MetaKeyWords", profileOB.MetaKeyWords);
            ncmdbObject.AddParameter("@MetaDescriptions", profileOB.MetaDescription);
            ncmdbObject.AddParameter("@MetaTitle", profileOB.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", profileOB.MetaKeyLanguage);

            //Output parameter to return Record count

            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);

            //End
            ncmdbObject.ExecuteNonQuery();
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

    #region Function to update the profile_Temp status for permission

    public int ASP_ChangeStatus_ProfileTmpPermission(ProfileOB profileOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@StatusId", profileOB.StatusId);
            ncmdbObject.AddParameter("@TempProfileId", profileOB.temp_profile_Id);
            ncmdbObject.AddParameter("@Status", profileOB.status);
            
            ncmdbObject.AddParameter("@updatedBy", profileOB.LastUpdatedBy);
            ncmdbObject.AddParameter("@UserID", profileOB.UserID);
            ncmdbObject.AddParameter("@IPAddress", profileOB.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_ChangeStatus_ProfileTmpPermission_Profile");
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

    #region function to insert Profile in links web table

    public int insert_Top_Profile_in_Web(ProfileOB profileOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            ncmdbObject.Parameters.Add(new SqlParameter("@TempProfileId", profileOB.temp_profile_Id));
            ncmdbObject.Parameters.Add(new SqlParameter("@UpdatedBY", profileOB.LastUpdatedBy));
            ncmdbObject.Parameters.Add(new SqlParameter("@IpAddress", profileOB.IpAddress));
            ncmdbObject.Parameters.Add(new SqlParameter("@UserID", profileOB.UserID));
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.ExecuteNonQuery("ASP_InsertUpdateDelete_Profile");
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

    #region Function to get Profile Id  for edit from WEB Profile

    public DataSet Profile_web_Get_Profile_Id_ForEdit(ProfileOB profileOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Profile_Id", profileOB.profile_Id);
            return ncmdbObject.ExecuteDataSet("ASP_web_Profiles__Get_Profile_Id_ForEdit_Profile");
        }
        catch
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

    public DataSet ASP_Profile_DisplayWithPaging(ProfileOB profileOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Lang_Id", profileOB.LangId);
            ncmdbObject.AddParameter("@status_id", profileOB.StatusId);
            ncmdbObject.AddParameter("@module_Id", profileOB.ModuleId);
            ncmdbObject.AddParameter("@NevigationID", profileOB.NevigationId);


            ncmdbObject.AddParameter("@DepartmentID", profileOB.DepttId);
            ncmdbObject.AddParameter("@PageIndex", profileOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", profileOB.PageSize);
            //ncmdbObject.AddParameter("@catId", profileOB.CatId);

            p_Var.dSet = ncmdbObject.ExecuteDataSet("ASP_Tmp_Profile_Display_Profile");
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

    #region Function to get data for edit either from Profile table or Profile_web table

    public DataSet getProfile_For_Editing(ProfileOB profileOB)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@statusId", profileOB.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@NevigationId", profileOB.NevigationId));
            ncmdbObject.Parameters.Add(new SqlParameter("@TempProfileId", profileOB.profile_Id));
            ncmdbObject.Parameters.Add(new SqlParameter("@LangId", profileOB.LangId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_Profile_Tmp_Edit_Profile");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function To Display Details With Paging on user side

    public DataSet USP_Profile_DisplayWithPaging(ProfileOB profileOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Lang_Id", profileOB.LangId);

            ncmdbObject.AddParameter("@module_Id", profileOB.ModuleId);
            ncmdbObject.AddParameter("@NevigationID", profileOB.NevigationId);
            ncmdbObject.AddParameter("@DepttID", profileOB.DepttId);


            ncmdbObject.AddParameter("@PageIndex", profileOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", profileOB.PageSize);
            //ncmdbObject.AddParameter("@DepttId", profileOB.DepttId);

            p_Var.dSet = ncmdbObject.ExecuteDataSet("USP_Tmp_Profile_Display_Profile");
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


    #region Function to get Profile for user side

    public DataSet UPS_View_Profile(ProfileOB profileOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Profile_Id", profileOB.profile_Id);
            return ncmdbObject.ExecuteDataSet("UPS_View_Profile");
        }
        catch
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

    public int Delete_Pending_Approved_Record(ProfileOB profileOB)
    {
        try
        {
            SqlParameter param = new SqlParameter();
            ncmdbObject.AddParameter("@TempProfileId", profileOB.temp_profile_Id);
            ncmdbObject.AddParameter("@StatusId", profileOB.StatusId);

            ncmdbObject.AddParameter("@UpdatedBy", profileOB.LastUpdatedBy);
            param = new SqlParameter("@recordCount", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param);
            ncmdbObject.ExecuteNonQuery("ASP_Delete_Profile_Tmp_Profile");


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


    #region Function to get data for edit either from Tmp_Profile table or Web_Profile table

    public DataSet GetProfileForEditing(ProfileOB profileOB)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@statusId", profileOB.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Nevigation_Id", profileOB.NevigationId));
            
            ncmdbObject.Parameters.Add(new SqlParameter("@Profile_Id", profileOB.profile_Id));
            ncmdbObject.Parameters.Add(new SqlParameter("@LangId", profileOB.LangId));
            return ncmdbObject.ExecuteDataSet("ASP_Get_Profile_Tmp_Editprofile");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    #region Function to get Profile_Id for edit from Web_Profile

    public DataSet Profile__Get_Profile_Id_ForEdit(ProfileOB profileOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Profile_Id", profileOB.profile_Id);
            return ncmdbObject.ExecuteDataSet("ASP_web_Profile__Get_Profile_Id_ForEdit");
        }
        catch
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

    public DataSet ASP_Get_Deptt_Name()
    {
        try
        {

            return ncmdbObject.ExecuteDataSet("MST_GET_DepartmentProfile");
        }
        catch
        {
            throw;

        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion   
  

    #region Function to update the  Profile status for delete

    public int UpdateStatusProfile(ProfileOB profileOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Profile_Id", profileOB.profile_Id);
            ncmdbObject.ExecuteNonQuery("ASP_UpdateStatusProfileRestore");
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

    #region Function to get Profile id from temp table for comparision

    public DataSet getProfileIdForCompare(ProfileOB profileObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@ProfileId", profileObject.profile_Id));
            return ncmdbObject.ExecuteDataSet("ASP_WebProfileID");
        }
        catch
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
