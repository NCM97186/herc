using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public class Role_PermissionBL
{
    //Area for all the variables declaration zone

    #region variable declaration

    Role_PermissionDAL obj_roleDL = new Role_PermissionDAL();

    #endregion 

    //End

    //Area for all the constructors declaration zone

    #region Default Constructor Zone
    public Role_PermissionBL()
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

            return obj_roleDL.Asp_Module_GetModule(usrObject);
        }
        catch
        {
            throw;
        }
    }
    #endregion 

    #region function to get the Role Name

    public DataSet ASP_Role_GetRoleName(UserOB usrObject)
    {
        try
        {
            return obj_roleDL.ASP_Role_GetRoleName(usrObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function To display permission & Role For Updating

    public DataSet ASP_Module_Role_Permission_Display(UserOB obj_UserOB)
    {
        try
        {
            return obj_roleDL.ASP_Module_Role_Permission_Display(obj_UserOB);
        }
        catch
        {
            throw;
        }
    }
    #endregion 

    #region Function to check  Role availiability

    public bool ASP_Role_CheckRoleAvailability(string Role_Name, int Departmentid)
    {
        try
        {

            return obj_roleDL.ASP_Role_CheckRoleAvailability(Role_Name, Departmentid);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to check privilages

    public DataSet CheckPrivilagesALL(UserOB obj_UserOB)
    {
        try
        {
            return obj_roleDL.CheckPrivilagesALL(obj_UserOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to check privilages for master page and dashboard

    public DataSet ASP_CheckPrivilagesALL_For_Master(UserOB obj_UserOB)
    {
        try
        {
            return obj_roleDL.ASP_CheckPrivilagesALL_For_Master(obj_UserOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to check privilages for permission page wise

    public DataSet ASP_CheckPrivilagesALLForPermission(UserOB obj_UserOB)
    {
        try
        {
            return obj_roleDL.ASP_CheckPrivilagesALLForPermission(obj_UserOB);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 

    #region Function To get modules name by module id

    public DataSet Asp_Module_GetModuleName(UserOB obj_UserOB)
    {
        try
        {
            return obj_roleDL.Asp_Module_GetModuleName(obj_UserOB);
        }
        catch
        {
            throw;
        }
    }

    #endregion 

    //End

    //Area for all the functions to insert

    #region Function To insert the permissions
    public int ASP_Module_Role_Permission_insert(UserOB obj_UserOB)
    {
        try
        {
            return obj_roleDL.ASP_Module_Role_Permission_insert(obj_UserOB);
        }
        catch
        {
            throw;
        }


    }
    #endregion 

    #region Function to insert role
    public int ASP_Role_insert(UserOB obj_UserOB)
    {
        try
        {
            return obj_roleDL.ASP_Role_insert(obj_UserOB);
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
            return obj_roleDL.ASP_Role_Update(obj_UserOB);
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
            return obj_roleDL.ASP_Module_Role_Permission_Update(obj_UserOB);
        }
        catch
        {
            throw;
        }
    }
    #endregion 

    //End
}
