using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;


public class UserOB
{
    #region Default constructor zone

    public UserOB()
	{
    }

    #endregion

    #region Data declaration zone

    //Integer Data Declaration area

    int? Action_Type = null;
    int? User_Id =null;
    int? Mod_Role_Map_Id = null;
    int? Admin_Id = null;
    int? Initial_Id = null;
    int? Status_Id = null;
    int? Module_Id = null;
    int? Role_Id = null;
    int? Draft = null;
    int? Review = null;
    int? Publish = null;
    int? Edit = null;
    int? Deleted = null;
    int? Repealed = null;
    int? Hindi = null;
    int? English = null;
    int? Deptt_Id = null;
    int? Inserted_By = null;
    int? Updated_By = null;
    public int LOG_ID { get; set; }
   

    public int? PageIndex { get; set; }
 
    public int? PageSize { get; set; }
    double? Contact_No = null;
    double? Pincode = null;
    double? Phone_No = null;
    double? Mobile_No = null;

    //End

    //String Data Declaration area
    public string IpAddress
    {
        get;
        set;
    }
   
    string Username = string.Empty;
    string Password = string.Empty;
    string Name = string.Empty;
    string Email = string.Empty;
    string Role_Name = string.Empty;
    string Address = string.Empty;
    string City = string.Empty;
    string State = string.Empty;
    string Country = string.Empty;
    string Module_Name = string.Empty;
    string langid = string.Empty;
   public  string moduleStatus = string.Empty;
    public string RANDOMID { get; set; }
    public string NewPassword { get; set; }
    public string Con_NewPassword { get; set; }
    public string USER_TYPE { get; set; }
    public  string url { get; set; }
    public bool? AttemptSuccess { get; set; }

    //End

    //Datetime Data Declaration area

    DateTime? Rec_Insert_Date = null;
    DateTime? Rec_Update_Date = null;
    
    //End

   
    #endregion

    #region Properties declaration zone

    //Properties for integer variables

    

    public int? ActionType
    {
        get
        {
            return Action_Type;
        }
        set
        {
            Action_Type = value;
        }
    }
    public int? UserId
    {
        get
        {
            return User_Id;
        }
        set
        {
            User_Id = value;
        }
    }
    public int? ModRoleMapId
    {
        get
        {
            return Mod_Role_Map_Id;
        }
        set
        {
            Mod_Role_Map_Id = value;
        }
    }
    
    public int? AdminId
    {
        get
        {
            return Admin_Id;
        }
        set
        {
            Admin_Id = value;
        }
    }

    public int? InitialId
    {
        get
        {
            return Initial_Id;
        }
        set
        {
            Initial_Id = value;
        }
    }

    public int? StatusId
    {
        get
        {
            return Status_Id;
        }
        set
        {
            Status_Id = value;
        }
    }
    public int? ModuleId
    {
        get
        {
            return Module_Id;
        }
        set
        {
            Module_Id = value;
        }
    }
    public int? RoleId
    {
        get
        {
            return Role_Id;
        }
        set
        {
            Role_Id = value;
        }

    }
    public int? draft
    {
        get
        {
            return Draft;
        }
        set
        {
            Draft = value;
        }
    }
    public int? review
    {
        get
        {
            return Review;
        }
        set
        {
            Review = value;
        }
    }
    public int? publish
    {
        get
        {
            return Publish;
        }
        set
        {
            Publish = value;
        }
    }
    public int? edit
    {
        get
        {
            return Edit;
        }
        set
        {
            Edit = value;
        }
    }
    public int? deleted
    {
        get
        {
            return Deleted;
        }
        set
        {
            Deleted = value;
        }
    }
    public int? repealed
    {
        get
        {
            return Repealed;
        }
        set
        {
            Repealed = value;
        }
    }
    public int? hindi
    {
        get
        {
            return Hindi;
        }
        set
        {
            Hindi = value;
        }
    }
    public int? english
    {
        get
        {
            return English;
        }
        set
        {
            English = value;
        }
    }
    public int? DepttId
    {
        get
        {
            return Deptt_Id;
        }
        set
        {
            Deptt_Id = value;
        }
    }
    public int? InsertedBy
    {
        get
        {
            return Inserted_By;
        }
        set
        {
            Inserted_By = value;
        }
    }
    public int? UpdatedBy
    {
        get
        {
            return Updated_By;
        }
        set
        {
            Updated_By = value;
        }
    }
    
    public double? ContactNo
    {
        get
        {
            return Contact_No;
        }
        set
        {
            Contact_No = value;
        }
    }
    public double? PinCode
    {
        get
        {
            return Pincode;
        }
        set
        {
            Pincode = value;
        }
    }
    public double? PhoneNo
    {
        get
        {
            return Phone_No;
        }
        set
        {
            Phone_No = value;
        }
    }
    public double? MobileNo
    {
        get
        {
            return Mobile_No;
        }
        set
        {
            Mobile_No = value;
        }
    }

    //End

    //Properties for string variables

    public string ModulestatusID
    {
        get
        {
            return moduleStatus;
        }
        set
        {
            moduleStatus = value;
        }
    }

    public string UserName
    {
        get
        {
            return Username;
        }
        set
        {
            Username = value;
        }
    }

    public string PassWord
    {
        get
        {
            return Password;
        }
        set
        {
            Password = value;
        }
    }

    public string NAME
    {
        get
        {
            return Name;
        }
        set
        {
            Name = value;
        }
    }

    public string E_mail
    {
        get
        {
            return Email;
        }
        set
        {
            Email = value;
        }
    }
    public string RoleName
    {
        get
        {
            return Role_Name;
        }
        set
        {
            Role_Name = value;
        }
    }
    public string address
    {
        get
        {
            return Address;
        }
        set
        {
            Address = value;
        }
    }
    public string city
    {
        get
        {
            return City;
        }
        set
        {
            City = value;
        }
    }
    public string state
    {
        get
        {
            return State;
        }
        set
        {
            State = value;
        }
    }
    public string country
    {
        get
        {
            return Country;
        }
        set
        {
            Country = value;
        }
    }

    public string ModuleName
    {
        get
        {
            return Module_Name;
        }
        set
        {
            Module_Name = value;

        }
    }

    public string LangId
    {
        get
        {
            return langid;
        }
        set
        {
            langid = value;

        }
    }


    //End

    //Properties declaration for datetime variables

    public DateTime? RecInsertDate
    {
        get
        {
            return Rec_Insert_Date;
        }
        set
        {
            Rec_Insert_Date = value;
        }
    }

    public DateTime? RecUpdateDate
    {
        get
        {
            return Rec_Update_Date;
        }
        set
        {
            Rec_Update_Date = value;
        }
    }

    //End


    #endregion
}
