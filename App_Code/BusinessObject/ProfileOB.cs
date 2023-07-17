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


public class ProfileOB
{
	public ProfileOB()
	{
		
	}
    #region Data declaration zone

    //Integer Data Declaration area

    int? Action_Type = null;
    int? Link_Type_Id = null;
    int? Temp_Link_Id = null;
    int? Temp_Profile_Id = null;
    int? Module_Id = null;
    int? Link_Parent_Id = null;
    int? Link_Order = null;
    int? Link_Level = null;
    int? Cat_Id = null;
    int? Position_Id = null;
    int? Status_Id = null;
    int? Old_Link_Id = null;
    int? Old_Profile_Id = null;
    int? Lang_Id = null;
    int? Repealed = null;
    int? Inserted_By = null;
    int? Last_Updated_By = null;
    int? Temp_Cat_Id = null;
    int? Cat_Type_Id = null;
    int? Old_Cat_Id = null;
    int? Temp_Cat_Type_Id = null;
    int? Old_Cat_Type_Id = null;
    int? Deptt_Id = null;
    int? Status = null;
    int? LinkID = null;
    int? pageindex = null;
    int? pagesize = null;
    int? currentpage = null;
    int? userid = null;
    Int64? Epabxte = null;
  
    int? Profile_Id =null;

    //End

    //String Data Declaration area
    string Phone = string.Empty;
    string Link_Type = string.Empty;
    string Email = string.Empty;
    string Name = string.Empty;
    string Subject = string.Empty;
    string Details = string.Empty;
    string Browser_Title = string.Empty;
    string Page_Title = string.Empty;
    string Meta_Keywords = string.Empty;
    string Mate_Description = string.Empty;
    string Url = string.Empty;
    string File_Name = string.Empty;
    string Image_Name = string.Empty;
    string Alt_Tag = string.Empty;
    string Cat_Name = string.Empty;
    string Cat_Desc = string.Empty;
    string Cat_Type_Name = string.Empty;
    string url_name = string.Empty;
    string Ip_Address = string.Empty;
    string SmallDetails = string.Empty;
    string Designation = string.Empty;
    public string MetaKeyWords { get; set; }
    public string MetaDescription { get; set; }
    public string MetaTitle { get; set; }
    public string MetaKeyLanguage { get; set; }

    public string Year { get; set; }
    //End

    //Datetime Data Declaration area

    DateTime? Start_Date = null;
    DateTime? End_Date = null;
    DateTime? Rec_Insert_Date = null;
    DateTime? Rec_Update_Date = null;
    DateTime? DateInserted = null;

    //End

    #endregion

    #region Properties declaration zone

    //Properties for integer variables

    public int? temp_profile_Id
    {
        get
        {
            return Temp_Profile_Id;
        }
        set
        {
            Temp_Profile_Id = value;
        }
    }

    public int? profile_Id
    {
        get
        {
            return Profile_Id;
        }
        set
        {
            Profile_Id = value;
        }
    }
    public int? old_Profile_Id
    {
        get
        {
            return Old_Profile_Id;
        }
        set
        {
            Old_Profile_Id = value;
        }
    }
    public Int64? epabxte
    {
        get
        {
            return Epabxte;
        }
        set
        {
            Epabxte = value;
        }
    }
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
    public string phone
    {
        get
        {
            return Phone;
        }
        set
        {
            Phone = value;
        }
    }
    public int? PageIndex { get; set; }

    public int? PageSize { get; set; }
    public int? CurrentPage { get; set; }
    public int? UserID { get; set; }
    public int? NevigationId { get; set; }
    public int? LinkTypeId
    {
        get
        {
            return Link_Type_Id;
        }
        set
        {
            Link_Type_Id = value;
        }
    }
    public int? TempLinkId
    {
        get
        {
            return Temp_Link_Id;
        }
        set
        {
            Temp_Link_Id = value;
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
    public int? LinkParentId
    {
        get
        {
            return Link_Parent_Id;
        }
        set
        {
            Link_Parent_Id = value;
        }
    }
    public int? LinkOrder
    {
        get
        {
            return Link_Order;
        }
        set
        {
            Link_Order = value;
        }
    }
    public int? LinkLevel
    {
        get
        {
            return Link_Level;
        }
        set
        {
            Link_Level = value;
        }
    }
    public int? CatId
    {
        get
        {
            return Cat_Id;
        }
        set
        {
            Cat_Id = value;
        }
    }
    public int? PositionId
    {
        get
        {
            return Position_Id;
        }
        set
        {
            Position_Id = value;
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
    public int? OldLinkId
    {
        get
        {
            return Old_Link_Id;
        }
        set
        {
            Old_Link_Id = value;
        }
    }
    public int? LangId
    {
        get
        {
            return Lang_Id;
        }
        set
        {
            Lang_Id = value;
        }
    }
    public int? RePealed
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
    public int? LastUpdatedBy
    {
        get
        {
            return Last_Updated_By;
        }
        set
        {
            Last_Updated_By = value;
        }
    }
    public int? TempCatId
    {
        get
        {
            return Temp_Cat_Id;
        }
        set
        {
            Temp_Cat_Id = value;
        }
    }
    public int? CatTypeId
    {
        get
        {
            return Cat_Type_Id;
        }
        set
        {
            Cat_Type_Id = value;
        }
    }
    public int? OldCatId
    {
        get
        {
            return Old_Cat_Id;
        }
        set
        {
            Old_Cat_Id = value;
        }
    }
    public int? TempCatTypeId
    {
        get
        {
            return Temp_Cat_Type_Id;
        }
        set
        {
            Temp_Cat_Type_Id = value;
        }
    }
    public int? OldCatTypeId
    {
        get
        {
            return Old_Cat_Type_Id;
        }
        set
        {
            Old_Cat_Type_Id = value;
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

    public int? status
    {
        get
        {
            return Status;
        }
        set
        {
            Status = value;
        }
    }
    public int? linkID
    {
        get
        {
            return LinkID;
        }
        set
        {
            LinkID = value;
        }
    }


    //End

    //Properties for string variables
    public string email
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

    public string designation
    {
        get
        {
            return Designation;
        }
        set
        {
            Designation = value;
        }
    }
    public string LinkType
    {
        get
        {
            return Link_Type;
        }
        set
        {
            Link_Type = value;
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
    public string subject
    {
        get
        {
            return Subject;
        }
        set
        {
            Subject = value;
        }
    }
    public string details
    {
        get
        {
            return Details;
        }
        set
        {
            Details = value;
        }
    }
    public string Smalldetails
    {
        get
        {
            return SmallDetails;
        }
        set
        {
            SmallDetails = value;
        }
    }
    public string BrowserTitle
    {
        get
        {
            return Browser_Title;
        }
        set
        {
            Browser_Title = value;
        }
    }

    public string PageTitle
    {
        get
        {
            return Page_Title;
        }
        set
        {
            Page_Title = value;
        }
    }
    public string MetaKeywords
    {
        get
        {
            return Meta_Keywords;
        }
        set
        {
            Meta_Keywords = value;
        }
    }
    public string MateDescription
    {
        get
        {
            return Mate_Description;
        }
        set
        {
            Mate_Description = value;
        }
    }
    public string URL
    {
        get
        {
            return Url;
        }
        set
        {
            Url = value;
        }
    }
    public string FileName
    {
        get
        {
            return File_Name;
        }
        set
        {
            File_Name = value;
        }
    }
    public string ImageName
    {
        get
        {
            return Image_Name;
        }
        set
        {
            Image_Name = value;
        }
    }
    public string AltTag
    {
        get
        {
            return Alt_Tag;
        }
        set
        {
            Alt_Tag = value;
        }
    }
    public string AltTagReg { get; set; }
    public string Details_Regional { get; set; }
    public string Name_Regional { get; set; }
    public string CatName
    {
        get
        {
            return Cat_Name;
        }
        set
        {
            Cat_Name = value;
        }
    }
    public string CatDesc
    {
        get
        {
            return Cat_Desc;
        }
        set
        {
            Cat_Desc = value;
        }
    }
    public string CatTypeName
    {
        get
        {
            return Cat_Type_Name;
        }
        set
        {
            Cat_Type_Name = value;
        }
    }
    public string UrlName { get; set; }
    public string IpAddress
    {
        get
        {
            return Ip_Address;
        }
        set
        {
            Ip_Address = value;
        }
    }

    //End

    //Properties declaration for datetime variables


    public DateTime? StartDate
    {
        get
        {
            return Start_Date;
        }
        set
        {
            Start_Date = value;
        }
    }

    public DateTime? EndDate
    {
        get
        {
            return End_Date;
        }
        set
        {
            End_Date = value;
        }
    }

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

    public DateTime? Date_Inserted
    {
        get
        {
            return DateInserted;
        }
        set
        {
            DateInserted = value;
        }
    }


    //End


    #endregion
}
