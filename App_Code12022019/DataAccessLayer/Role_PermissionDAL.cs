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

public class Role_PermissionDAL
{
    //Area for all the variables declaration zone

    #region variable declaration zone

     NCMdbAccess obj_ncm = new NCMdbAccess();
     Project_Variables p_Val = new Project_Variables();

    #endregion 

    //End

    //Area for all the constructors declaration zone

    #region Default Constructor Zone

    public Role_PermissionDAL()
	{

    }

    #endregion 

    //End

    //Area for all the functions to get data for display

    #region Function To get modules 

    public DataSet Asp_Module_GetModule(UserOB usrObject)
    {
        try
        {
            obj_ncm.Parameters.Add(new SqlParameter("@USERID",usrObject.UserId));
            return obj_ncm.ExecuteDataSet("Asp_Mst_Module_GetModule");
        }
        catch
        {
            throw;
        }
    }

    #endregion 

    #region Function to display permission and role for updating

    public DataSet ASP_Module_Role_Permission_Display(UserOB obj_UserOB)
    {
        try
        {
            obj_ncm.AddParameter("@Role_Id", obj_UserOB.RoleId);
            return obj_ncm.ExecuteDataSet("ASP_Module_Role_Permission_Role_Display");
        }
        catch
        {
            throw;
        }
    }

    #endregion 

    #region Function to get the role name

    public DataSet ASP_Role_GetRoleName(UserOB usrObject)
    {
        try
        {
           obj_ncm.Parameters.Add(new SqlParameter("@DeptID", usrObject.DepttId));
           obj_ncm.Parameters.Add(new SqlParameter("@UserID", usrObject.UserId));
           return obj_ncm.ExecuteDataSet("ASP_Role_Display");
        }
        catch
        {
            throw;
        }

    }

    #endregion 

    #region Function to check role availiability

    public bool ASP_Role_CheckRoleAvailability(string Role_Name,int Departmentid)
    {
        try
        {
           
            SqlParameter[] param = new SqlParameter[1];
            obj_ncm.AddParameter("@Role_Name", Role_Name);
            obj_ncm.AddParameter("@Dept_ID", Departmentid);
            param[0] = new SqlParameter("@Msg", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncm.AddParameter(param[0]);
            obj_ncm.ExecuteNonQuery("ASP_Role_CheckRoleAvailability");
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
            obj_ncm.Dispose();
        }
    }

    #endregion

    #region Function to check privilages

    public DataSet CheckPrivilagesALL(UserOB obj_UserOB)
    {
        try
        {
            obj_ncm.AddParameter("@Role_Id", obj_UserOB.RoleId);
            obj_ncm.AddParameter("@Module_Id", obj_UserOB.ModuleId);
            return obj_ncm.ExecuteDataSet("CheckPrivilagesALL");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncm.Dispose();
        }
    }

    #endregion 

    #region Function to check privilages for master page and dashboard

    public DataSet ASP_CheckPrivilagesALL_For_Master(UserOB obj_UserOB)
    {
        try
        {
            obj_ncm.AddParameter("@Role_Id", obj_UserOB.RoleId);
            //obj_ncm.AddParameter("@Module_Id", obj_UserOB.ModuleId);
            return obj_ncm.ExecuteDataSet("ASP_CheckPrivilagesALL_For_Master");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncm.Dispose();
        }
    }

    #endregion 

    #region Function to check privilages for permission page wise

    public DataSet ASP_CheckPrivilagesALLForPermission(UserOB obj_UserOB)
    {
        try
        {
            obj_ncm.AddParameter("@Role_Id", obj_UserOB.RoleId);
            obj_ncm.AddParameter("@Module_Id", obj_UserOB.ModuleId);
            return obj_ncm.ExecuteDataSet("ASP_CheckPrivilagesForPermission");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncm.Dispose();
        }
    }

    #endregion 

    #region Function to get modules name by module id

    public DataSet Asp_Module_GetModuleName(UserOB obj_UserOB)
    {
        try
        {
            obj_ncm.AddParameter("@ModuleId", obj_UserOB.ModuleId);
            return obj_ncm.ExecuteDataSet("ASP_Get_ModuleName_Mst");
        }
        catch
        {
            throw;
        }
    }

    #endregion 

    //End

    //Area for all the functions to insert

    #region Function to insert permissions

    public int ASP_Module_Role_Permission_insert(UserOB obj_UserOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncm.AddParameter(param[0]);
            obj_ncm.AddParameter("@ActionType", obj_UserOB.ActionType);
            obj_ncm.AddParameter("@Role_Id", obj_UserOB.RoleId);
            obj_ncm.AddParameter("@Module_Id", obj_UserOB.ModuleId);
            obj_ncm.AddParameter("@Draft", obj_UserOB.draft);
            obj_ncm.AddParameter("@Review", obj_UserOB.review);
            obj_ncm.AddParameter("@Publish", obj_UserOB.publish);
            obj_ncm.AddParameter("@Edit", obj_UserOB.edit);
            obj_ncm.AddParameter("@Deleted", obj_UserOB.deleted);
            obj_ncm.AddParameter("@Repealed", obj_UserOB.repealed);
            obj_ncm.AddParameter("@Hindi", obj_UserOB.hindi);
            obj_ncm.AddParameter("@English", obj_UserOB.english);
            obj_ncm.ExecuteNonQuery("ASP_Module_Role_Permission_Insert_Update");
            p_Val.Result = Convert.ToInt16(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }


    }

    #endregion 

    #region Function to insert the role

    public int ASP_Role_insert(UserOB obj_UserOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncm.Parameters.Add(param[0]);
            obj_ncm.AddParameter("@ActionType", obj_UserOB.ActionType);
            obj_ncm.AddParameter("@Role_Name", obj_UserOB.RoleName);
            obj_ncm.AddParameter("@Deptt_Id", obj_UserOB.DepttId);
            obj_ncm.AddParameter("@Inserted_By", obj_UserOB.InsertedBy);
            obj_ncm.AddParameter("@IPAddress", obj_UserOB.IpAddress);
            obj_ncm.ExecuteNonQuery("ASP_Role_Insert_Update");
            p_Val.Result = Convert.ToInt16(param[0].Value);
            return p_Val.Result;

        }
        catch
        {
            throw;
        }
    }

    #endregion 

    //End

    //Area for all the functions to update

    #region Function To update Role

    public int ASP_Role_Update(UserOB obj_UserOB)
    {
        try
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncm.Parameters.Add(param[0]);
            obj_ncm.AddParameter("@ActionType", obj_UserOB.ActionType);
            obj_ncm.AddParameter("@Role_Id", obj_UserOB.RoleId);
            obj_ncm.AddParameter("@Role_Name", obj_UserOB.RoleName);
            obj_ncm.AddParameter("@Updated_By", obj_UserOB.UpdatedBy);
            obj_ncm.AddParameter("@IPAddress", obj_UserOB.IpAddress);
            obj_ncm.ExecuteNonQuery("ASP_Role_Insert_Update");
            p_Val.Result = Convert.ToInt16(param[0].Value);
            return p_Val.Result;


        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function To update Permission

    public int ASP_Module_Role_Permission_Update(UserOB obj_UserOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncm.AddParameter(param[0]);
            obj_ncm.AddParameter("@ActionType", obj_UserOB.ActionType);
            obj_ncm.AddParameter("@Role_Id", obj_UserOB.RoleId);
            //obj_ncm.AddParameter("@Mod_Role_Map_Id", obj_UserOB.ModRoleMapId);
            obj_ncm.AddParameter("@Module_Id", obj_UserOB.ModuleId);
            obj_ncm.AddParameter("@Draft", obj_UserOB.draft);
            obj_ncm.AddParameter("@Review", obj_UserOB.review);
            obj_ncm.AddParameter("@Publish", obj_UserOB.publish);
            obj_ncm.AddParameter("@Edit", obj_UserOB.edit);
            obj_ncm.AddParameter("@Deleted", obj_UserOB.deleted);
            obj_ncm.AddParameter("@Repealed", obj_UserOB.repealed);
            obj_ncm.AddParameter("@Hindi", obj_UserOB.hindi);
            obj_ncm.AddParameter("@English", obj_UserOB.english);
            obj_ncm.ExecuteNonQuery("ASP_Module_Role_Permission_Insert_Update");
            p_Val.Result = Convert.ToInt16(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
    }

    #endregion 

    //End
}
