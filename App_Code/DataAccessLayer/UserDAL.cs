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

public class UserDAL
{
    #region Variable Declaration Zone

    NCMdbAccess obj_ncmdb = new NCMdbAccess();
    Project_Variables p_Val = new Project_Variables();
        
    #endregion 

    #region Default Constructor Zone

    public UserDAL()
	{

    }

    #endregion 

    #region Function to get Initial name

    public DataSet ASP_Get_Initial_Name()
    {
        try
        {
            return obj_ncmdb.ExecuteDataSet("MST_GET_InitialName");
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
           obj_ncmdb.Parameters.Add(new SqlParameter("@ModuleId", usrObject.ModuleId));
           obj_ncmdb.Parameters.Add(new SqlParameter("@Deptt_Id",usrObject.DepttId));
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

    #region Function To get Country

    public DataSet ASP_Get_Country_Name()
    {
        try
        {
            return obj_ncmdb.ExecuteDataSet("ASP_Mst_Country_Display");
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

    #region Function To insert the User 
    public int ASP_User_Insert(UserOB obj_UserOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@ActionType",obj_UserOB.ActionType);
            obj_ncmdb.AddParameter("@Username",obj_UserOB.UserName);
            obj_ncmdb.AddParameter("@Password",obj_UserOB.PassWord);
            obj_ncmdb.AddParameter("@Initial_Id",obj_UserOB.InitialId);
            obj_ncmdb.AddParameter("@Name",obj_UserOB.NAME);
            obj_ncmdb.AddParameter("@Address",obj_UserOB.address);
            obj_ncmdb.AddParameter("@City",obj_UserOB.city);
            obj_ncmdb.AddParameter("@Status_Id",obj_UserOB.StatusId);
            obj_ncmdb.AddParameter("@Country",obj_UserOB.country);
            obj_ncmdb.AddParameter("@Email",obj_UserOB.E_mail);
            obj_ncmdb.AddParameter("@Mobile_No",obj_UserOB.ContactNo);
            obj_ncmdb.AddParameter("@Role_Id",obj_UserOB.RoleId);
            obj_ncmdb.AddParameter("@Deptt_Id",obj_UserOB.DepttId);
            obj_ncmdb.AddParameter("@Inserted_By",obj_UserOB.InsertedBy);
            obj_ncmdb.AddParameter("@IPAddress", obj_UserOB.IpAddress);
            obj_ncmdb.ExecuteNonQuery("ASP_User_Insert_Update");
            p_Val.Result = Convert.ToInt16(param[0].Value);
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

    #region function to update User

    public int ASP_User_Update(UserOB obj_UserOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@ActionType", obj_UserOB.ActionType);
            obj_ncmdb.AddParameter("@Username", obj_UserOB.UserName);
           // obj_ncmdb.AddParameter("@Password", obj_UserOB.PassWord);
            obj_ncmdb.AddParameter("@Initial_Id", obj_UserOB.InitialId);
            obj_ncmdb.AddParameter("@Name", obj_UserOB.NAME);
            obj_ncmdb.AddParameter("@Address", obj_UserOB.address);
            obj_ncmdb.AddParameter("@City", obj_UserOB.city);
            obj_ncmdb.AddParameter("@Status_Id", obj_UserOB.StatusId);
            obj_ncmdb.AddParameter("@Country", obj_UserOB.country);
            obj_ncmdb.AddParameter("@Email", obj_UserOB.E_mail);
            obj_ncmdb.AddParameter("@Mobile_No", obj_UserOB.ContactNo);
            obj_ncmdb.AddParameter("@Role_Id", obj_UserOB.RoleId);
            obj_ncmdb.AddParameter("@Deptt_Id", obj_UserOB.DepttId);
            obj_ncmdb.AddParameter("@Last_Updated_By", obj_UserOB.UpdatedBy);
            obj_ncmdb.AddParameter("@User_Id",obj_UserOB.UserId);
            obj_ncmdb.AddParameter("@IPAddress", obj_UserOB.IpAddress);
            obj_ncmdb.ExecuteNonQuery("ASP_User_Insert_Update");
            p_Val.Result = Convert.ToInt16(param[0].Value);
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

    #region Function to check availiability

    public bool CheckAvaliability(string Username)
    {
        try
        {
            
            SqlParameter[] param = new SqlParameter[1];
            obj_ncmdb.AddParameter("@Username", Username);

            param[0] = new SqlParameter("@Msg", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.ExecuteNonQuery("ASP_User_CheckUserAvailability");
            if (Convert.ToBoolean(param[0].Value))
            {
                p_Val.flag = true;
            }
            else
            {
                p_Val.flag = false;
            }
            return p_Val.flag;
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

    #region Function To display Users On the basis of Status and Deptt_id

    public DataSet ASP_User_display(UserOB obj_UserOB, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.Parameters.Add(param[0]);
            obj_ncmdb.AddParameter("@User_Id", obj_UserOB.UserId);
            obj_ncmdb.AddParameter("@Status_Id", obj_UserOB.StatusId);
            obj_ncmdb.AddParameter("@Deptt_Id",obj_UserOB.DepttId);
           // obj_ncmdb.AddParameter("@PageIndex", obj_UserOB.PageIndex);
           // obj_ncmdb.AddParameter("@PageSize", obj_UserOB.PageSize);
            p_Val.dSet = obj_ncmdb.ExecuteDataSet("ASP_User_Display");
            catValue = Convert.ToInt16(param[0].Value);
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

    #region Function to Display Status Name
    public DataSet ASP_Status_Type(UserOB obj_UserOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@Status_Type_Id", obj_UserOB.StatusId);
            return obj_ncmdb.ExecuteDataSet("ASP_Status_Type_Status_Display");
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




    #region Function to Display Status Name for user
    public DataSet ASP_Status_TypeUser(UserOB obj_UserOB)
    {
        try
        {

            obj_ncmdb.AddParameter("@Status_Type_Id", obj_UserOB.StatusId);
            return obj_ncmdb.ExecuteDataSet("ASP_Status_Type_Status_DisplayUser");
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
 

    #region Function To display Role And User

    public DataSet ASP_Get_User_Role_Display(UserOB obj_UserOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@User_Id",obj_UserOB.UserId);
            return obj_ncmdb.ExecuteDataSet("ASP_User_Role_Display");
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

    #region Function to update the status in  the user

    public int ASP_Update_status(UserOB obj_UserOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            
            obj_ncmdb.AddParameter("@User_Id",obj_UserOB.UserId);
            obj_ncmdb.AddParameter("@Status_Id",obj_UserOB.StatusId);
            obj_ncmdb.AddParameter("@IPAddress", obj_UserOB.IpAddress);
            obj_ncmdb.AddParameter("@UserID", obj_UserOB.PageIndex);
            obj_ncmdb.ExecuteNonQuery("ASP_User_Change_status");
            p_Val.Result = Convert.ToInt16(param[0].Value);
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

    #region function To delete the user

    public int ASP_User_Delete(UserOB obj_UserOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@User_Id", obj_UserOB.UserId);
            obj_ncmdb.AddParameter("@StatusId",obj_UserOB.StatusId);
            obj_ncmdb.ExecuteNonQuery("ASP_User_Delete");
            p_Val.Result = Convert.ToInt16(param[0].Value);
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

    #region Function to Display Status Name for user
    public int RestoreUser(UserOB obj_UserOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@User_Id", obj_UserOB.UserId);
            
            obj_ncmdb.ExecuteNonQuery("ASP_User_Restore");
            p_Val.Result = Convert.ToInt16(param[0].Value);
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

    #region Function to delete Role

    public int ASP_Role_User_Delete(UserOB obj_UserOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@Roll_Id", obj_UserOB.RoleId);
            obj_ncmdb.ExecuteNonQuery("ASP_Role_User_Delete");
            p_Val.Result = Convert.ToInt16(param[0].Value);
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

    #region Function to get User Password

    public DataSet ASP_Users_GetPassword(UserOB obj_UserOB)
    {
        try
        {

            obj_ncmdb.AddParameter("@Username", obj_UserOB.UserName);
            obj_ncmdb.AddParameter("@Status_Id", obj_UserOB.StatusId);
            return obj_ncmdb.ExecuteDataSet("ASP_Users_GetPassword");
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




    #region function to insert for the reset password

    public bool Proc_Insert_Into_RESETPASSWORD(UserOB obj_UserOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@ActionType", obj_UserOB.ActionType);
            obj_ncmdb.AddParameter("@FpUserID", obj_UserOB.LOG_ID);
            obj_ncmdb.AddParameter("@FpUsername", obj_UserOB.UserName);
            obj_ncmdb.AddParameter("@FpRANDOMID", obj_UserOB.RANDOMID);
            obj_ncmdb.AddParameter("@IPAddress", obj_UserOB.IpAddress);
            int val = obj_ncmdb.ExecuteNonQuery("ASP_InsertUpdateDelete_ForgotPassword");
            if (val > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        catch (Exception)
        {

            return false;
        }
    }

    #endregion 

    #region function to get the email id by User name

    public DataSet Proc_Select_USER_DETAIL_BY_Name(UserOB obj_UserOB)
    {
        obj_ncmdb.AddParameter("@UserName", obj_UserOB.UserName);
        return obj_ncmdb.ExecuteDataSet("ASP_GetMailId_User");

    }

    #endregion 



    #region  function to check the user password

    public void Proc_Check_Password(UserOB obj_UserOB, ref string check)
    {
        try
        {
            obj_ncmdb.AddParameter("@FpUserID", obj_UserOB.LOG_ID);
            obj_ncmdb.AddParameter("@RANDOMID", obj_UserOB.RANDOMID);

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@state", SqlDbType.NVarChar, 4000);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.ExecuteDataSet("ASP_Check_Password");
            check = (param[0].Value).ToString();


        }
        catch
        {
            throw;
        }
    }

    #endregion 


    #region function to get the email id

    public DataSet Proc_Select_USER_DETAIL_BY_ID(UserOB obj_UserOB)
    {
        obj_ncmdb.AddParameter("@UserId", obj_UserOB.LOG_ID);
        return obj_ncmdb.ExecuteDataSet("ASP_GetMailId_User");

    }

    #endregion 


    #region function to update for the reset password

    public bool Proc_Update_Into_RESETPASSWORD(UserOB obj_UserOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@ActionType", obj_UserOB.ActionType);
            obj_ncmdb.AddParameter("@FpUserID", obj_UserOB.LOG_ID);
            obj_ncmdb.AddParameter("@FpRANDOMID", obj_UserOB.RANDOMID);
            int val = obj_ncmdb.ExecuteNonQuery("ASP_InsertUpdateDelete_ForgotPassword");
            if (val > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        catch (Exception)
        {

            return false;
        }
    }

    #endregion 

    #region Function to update the user password

    public bool Proc_Update_User_Password(UserOB obj_UserOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncmdb.AddParameter(param[0]);
            obj_ncmdb.AddParameter("@UserId", obj_UserOB.LOG_ID);
            obj_ncmdb.AddParameter("@Password", obj_UserOB.PassWord);
            obj_ncmdb.AddParameter("@IPAddress", obj_UserOB.IpAddress);
            obj_ncmdb.ExecuteNonQuery("ASP_UpdateUserPassword_User");
            int val = Convert.ToInt16(param[0].Value);
            if (val > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        catch (Exception)
        {

            return false;
        }
    }

    #endregion 

    #region Function to get Emails of reviewers to send emails

    public DataSet getReviewEmailIds(UserOB userObject)
    {
        try
        {
            obj_ncmdb.AddParameter("@deptt_id", userObject.DepttId);
            obj_ncmdb.AddParameter("@ModuleID", userObject.ModuleId);
            return obj_ncmdb.ExecuteDataSet("asp_getEmailIdsForSendReivewEmailConfirmation");
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

    #region Function to get Emails of publishers to send emails

    public DataSet getPublisherEmailIds(UserOB userObject)
    {
        try
        {
            obj_ncmdb.AddParameter("@ModuleID", userObject.ModuleId);
            obj_ncmdb.AddParameter("@deptt_id", userObject.DepttId);
            return obj_ncmdb.ExecuteDataSet("asp_getEmailIdsForSendForApprovalEmailConfirmation");
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

    #region Function to get Emails of publishers to send emails

    public DataSet getDataEntryEmailIds(UserOB userObject)
    {
        try
        {
            obj_ncmdb.AddParameter("@ModuleID", userObject.ModuleId);
            obj_ncmdb.AddParameter("@deptt_id", userObject.DepttId);
            return obj_ncmdb.ExecuteDataSet("asp_getEmailIdsForSendForDraftEmailConfirmation");
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
    //public void Module_Privilage(int strArray)
    //{
        
    //    int i = 0;
    //    try
    //    {
    //        obj_ncmdb.AddParameter("@Role_Id", strArray);
    //        p_Val.dSet = obj_ncmdb.ExecuteDataSet("ASP_getModule_privilages");
    //        p_Val.dt = p_Val.dSet.Tables[0];
    //        if (p_Val.dt.Rows.Count > 0)
    //        {
    //            while (i < p_Val.dt.Rows.Count)
    //            {
    //                System.Web.HttpContext.Current.Session[p_Val.dt.Rows[i]["Module_Id"].ToString()] = Convert.ToBoolean(1);
    //                i++;
    //            }
                
    //        }
    //        else
    //        {
    //            System.Web.HttpContext.Current.Session[p_Val.dt.Rows[i]["Module_Id"].ToString()] = Convert.ToBoolean(0);
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}



    #region function to insert into AuditTrailLogin

    public int Insert_AuditTrailLogin(UserOB obj_UserOB)
    {
        try
        {
            obj_ncmdb.AddParameter("@Action", obj_UserOB.moduleStatus);
            obj_ncmdb.AddParameter("@IPAddress", obj_UserOB.IpAddress);
            obj_ncmdb.AddParameter("@RequestURL", obj_UserOB.url);
            obj_ncmdb.AddParameter("@AttemptSuccess", obj_UserOB.AttemptSuccess);
            obj_ncmdb.AddParameter("@UserId", obj_UserOB.UserId);
            return obj_ncmdb.ExecuteNonQuery("Proc_Insert_AuditTrailLogin");
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
