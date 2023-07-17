using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public class UserBL
{
    #region variable declaration zone

    UserDAL obj_UserDL = new UserDAL();
    #endregion 

    #region default construcor zone
    public UserBL()
	{

    }
    #endregion 

    #region Function to get Initial name

    public DataSet ASP_Get_Initial_Name()
    {
        try
        {
            return obj_UserDL.ASP_Get_Initial_Name();
        }
        catch
        {
            throw;
        }

    }
    #endregion

    #region Function to get Deptt Name

    public DataSet ASP_Get_Deptt_Name(UserOB usrObject)
    {
        try
        {
            return obj_UserDL.ASP_Get_Deptt_Name(usrObject);
        }
        catch
        {
            throw;

        }
        
    }
    #endregion     

    #region Function To get Country

    public DataSet ASP_Get_Country_Name()
    {
        try
        {
            return obj_UserDL.ASP_Get_Country_Name();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 

    #region Function To insert the User

    public int ASP_User_Insert(UserOB obj_UserOB)
    {
        try
        {
           return obj_UserDL.ASP_User_Insert(obj_UserOB);  
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 

    #region function to update User

    public int ASP_User_Update(UserOB obj_UserOB)
    {
        try
        {
            return obj_UserDL.ASP_User_Update(obj_UserOB);
        }
        catch
        {
            throw;
        }
     
    }
    #endregion

    #region Function to check availiability

    public bool CheckAvaliability(string Username)
    {
        try
        {
            return obj_UserDL.CheckAvaliability(Username);
        }
        catch
        {
            throw;
        }
    
    }

    #endregion 

    #region Function To display Users On the basis of Status and Deptt_id

    public DataSet ASP_User_display(UserOB obj_UserOB, out int catValue)
    {
        try
        {
            return obj_UserDL.ASP_User_display(obj_UserOB, out catValue);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 

    #region Function to Display Status Name
    public DataSet ASP_Status_Type(UserOB obj_UserOB)
    {
        try
        {
            return obj_UserDL.ASP_Status_Type(obj_UserOB);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 


    #region Function to Display Status Name for user
    public DataSet ASP_Status_TypeUser(UserOB obj_UserOB)
    {
        try
        {
            return obj_UserDL.ASP_Status_TypeUser(obj_UserOB);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 

    #region Function To display Role And User

    public DataSet ASP_Get_User_Role_Display(UserOB obj_UserOB)
    {
        try
        {
            return obj_UserDL.ASP_Get_User_Role_Display(obj_UserOB);
        }
        catch
        {
            throw;
        }
       
    }
    #endregion

    #region Function to update the status in  the user

    public int ASP_Update_status(UserOB obj_UserOB)
    {
        try
        {
            return obj_UserDL.ASP_Update_status(obj_UserOB);
        }
        catch
        {
            throw;
        }
        
    }
    #endregion 

    #region function To delete the user
    public int ASP_User_Delete(UserOB obj_UserOB)
    {
        try
        {
            return obj_UserDL.ASP_User_Delete(obj_UserOB);
        }
        catch
        {
            throw;
        }
       


    }

    #endregion 

    #region function To delete the user
    public int RestoreUser(UserOB obj_UserOB)
    {
        try
        {
            return obj_UserDL.RestoreUser(obj_UserOB);
        }
        catch
        {
            throw;
        }



    }

    #endregion

    #region Function to delete Role

    public int ASP_Role_User_Delete(UserOB obj_UserOB)
    {
        try
        {
            return obj_UserDL.ASP_Role_User_Delete(obj_UserOB);
        }
        catch
        {
            throw;
        }
        
    }
    #endregion 

    #region Function to get User Password

    public DataSet ASP_Users_GetPassword(UserOB obj_UserOB)
    {
        try
        {
            return obj_UserDL.ASP_Users_GetPassword(obj_UserOB);
           
        }
        catch
        {
            throw;
        }
        

    }
    #endregion 

    #region function to insert for the reset password

    public bool Proc_Insert_Into_RESETPASSWORD(UserOB obj_UserOB)
    {
        try
        {
            return obj_UserDL.Proc_Insert_Into_RESETPASSWORD(obj_UserOB);

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
        return obj_UserDL.Proc_Select_USER_DETAIL_BY_Name(obj_UserOB);

    }

    #endregion 


    #region  function to check the user password

    public void Proc_Check_Password(UserOB obj_UserOB, ref string check)
    {
        try
        {
            obj_UserDL.Proc_Check_Password(obj_UserOB, ref check);


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
        return obj_UserDL.Proc_Select_USER_DETAIL_BY_ID(obj_UserOB);

    }

    #endregion 


    #region function to update for the reset password

    public bool Proc_Update_Into_RESETPASSWORD(UserOB obj_UserOB)
    {
        try
        {
            return obj_UserDL.Proc_Update_Into_RESETPASSWORD(obj_UserOB);

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
            return obj_UserDL.Proc_Update_User_Password(obj_UserOB);


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
            return obj_UserDL.getReviewEmailIds(userObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Emails of publishers to send emails

    public DataSet getPublisherEmailIds(UserOB userObject)
    {
        try
        {
            return obj_UserDL.getPublisherEmailIds(userObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Emails of publishers to send emails

    public DataSet getDataEntryEmailIds(UserOB userObject)
    {
        try
        {
            return obj_UserDL.getDataEntryEmailIds(userObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion
    //public void Module_Privilage(int strArray)
    //{
       
    //    try
    //    {
    //        obj_UserDL.Module_Privilage(strArray);
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
            return obj_UserDL.Insert_AuditTrailLogin(obj_UserOB);
        }
        catch
        {
            throw;
        }
       
    }
    #endregion 

}
