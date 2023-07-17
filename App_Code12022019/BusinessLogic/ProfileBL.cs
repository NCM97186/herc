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

/// <summary>
/// Summary description for ProfileBL
/// </summary>
public class ProfileBL
{
    ProfileDL profileDL = new ProfileDL();
	public ProfileBL()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    #region Function to get Profile Navigation
    public DataSet getNavigation()
    {
        try
        {

            return profileDL.getNavigation();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to insert New Profile

    public int insert_New_Profile(ProfileOB profileOB)
    {
        try
        {
            return profileDL.insert_New_Profile(profileOB);

        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function To Display Details With Paging

    public DataSet ASP_Profile_DisplayWithPaging(ProfileOB profileOB, out int catValue)
    {
        try
        {
            return profileDL.ASP_Profile_DisplayWithPaging(profileOB, out catValue);
        }
        catch
        {
            throw;
        }

    }

    #endregion



    #region Function to update the Profile_Temp status for permission

    public int ASP_ChangeStatus_ProfileTmpPermission(ProfileOB profileOB)
    {
        try
        {
            return profileDL.ASP_ChangeStatus_ProfileTmpPermission(profileOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region function to insert top menu in links web table

    public int insert_Top_Profile_in_Web(ProfileOB profileOB)
    {
        try
        {
            return profileDL.insert_Top_Profile_in_Web(profileOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function To Display Details With Paging

    public DataSet USP_Profile_DisplayWithPaging(ProfileOB profileOB, out int catValue)
    {
        try
        {
            return profileDL.USP_Profile_DisplayWithPaging(profileOB, out catValue);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to get Profile for user side

    public DataSet UPS_View_Profile(ProfileOB profileOB)
    {
        try
        {

            return profileDL.UPS_View_Profile(profileOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to delete pending or approved record

    public int Delete_Pending_Approved_Record(ProfileOB profileOB)
    {
        try
        {

            return profileDL.Delete_Pending_Approved_Record(profileOB);
        }
        catch
        {
            throw;
        }

    }


    #endregion


    #region Function to get data for edit either from Tmp_Profile table or Web_Profile table

    public DataSet GetProfileForEditing(ProfileOB profileOB)
    {
        try
        {

            return profileDL.GetProfileForEditing(profileOB);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion


    #region Function to get Profile_Id for edit from Web_Profile

    public DataSet Profile__Get_Profile_Id_ForEdit(ProfileOB profileOB)
    {
        try
        {

            return profileDL.Profile__Get_Profile_Id_ForEdit(profileOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function to get Deptt Name

    public DataSet ASP_Get_Deptt_Name()
    {
        try
        {

            return profileDL.ASP_Get_Deptt_Name();
        }
        catch
        {
            throw;

        }
       
    }

    #endregion     


    #region Function to update the  Profile status for delete

    public int UpdateStatusProfile(ProfileOB profileOB)
    {
        try
        {
            return profileDL.UpdateStatusProfile(profileOB);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get Profile id from temp table for comparision

    public DataSet getProfileIdForCompare(ProfileOB profileObject)
    {
        try
        {
            return profileDL.getProfileIdForCompare(profileObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion
}
