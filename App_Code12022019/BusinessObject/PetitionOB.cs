using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public class PetitionOB
{
   #region Default constructor zone

   public PetitionOB()
	{

    }

   #endregion

   #region Data declaration zone

    //Integer Data Declaration area

    int? Action_Type = null;
    int? Connection_Type = null;
    public int? counter { get; set; }
    public int? PageIndex { get; set; }
    public int? ModuleID { get; set; }
    public int? PageSize { get; set; }
    public int? CurrentPage { get; set; }
    public int? UploadID { get; set; }
    public int? ApprovedBy { get; set; }
    public Int32? ConnectionID { get; set; }
    public Int32? ConnectedPetitionID { get; set; }

    public Int32? SohTempID { get; set; }
    public Int32? soh_ID { get; set; }
    public int? recordInsertedBy { get; set; }
    public int? recordUpdatedBy { get; set; }
    public int? DepttId { get; set; }
    public Int32? PublicNoticeID { get; set; }
    public int? Status { get; set; }
    public int? OrderSubcategory { get; set; }
    public int? PetitionType { get; set; }
    public int? AppealType { get; set; }
    public int? userID { get; set; }
    public int? ModuleType { get; set; }
    //Public Notice variables 03-08-2012

    public int? TmpPublicNoticeID { get; set; }
   
   // public string publicNotice { get; set; }
    public string MetaKeyWords{get; set;}
    public string MetaDescription { get; set; }
    public string MetaTitle { get; set; }
    public string MetaKeyLanguage { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string MemoNo { get; set; }
    public string TransferAuthority { get; set; }
    public string IpAddress { get; set; }
    public string Month { get; set; }

    //End

    //Order variables 28-09-2012

    public Int32? TempOrderID { get; set; }
    public Int32? OrderCatID { get; set; }
    public Int32? OrderID { get; set; }
    public Int32? OrderTypeID { get; set; }
    public Int32? oldRaID { get; set; }
    public string OrderTitle { get; set; }
    public string OrderDescription { get; set; }
    public string OrderFile { get; set; }
    public DateTime? OrderDate { get; set; }

    //End

    int? Temp_Petition_Id = null;
    int? Petition_Status_Id = null;
    int? RtiStatusID = null;
    int? Review = null;
    int? Status_Id = null;
    int? Old_Petition_Id = null;
    int? Lang_Id = null;
    int? Inserted_By = null;
    int? Last_Updated_By = null;
    int? Temp_PA_Id = null;
    int? Temp_RP_Id = null;
    int? PA_Status_Id = null;
    int? PA_Id = null;
    int? Old_PA_Id = null;
    Int32? Temp_Appeal_Id = null;
    int? Appeal_Status_Id = null;
    int? Appeal = null;
    Int32? Old_Appeal_Id = null;
    int? Temp_RA_Id = null;
    Int32? Appeal_Id = null;
    int? RA_Status_Id = null;
    int? Petition_Id = null;
    int? RP_Status_Id = null;
    int? Old_RP_Id = null;
    int? Temp_RTI_Id = null;
    int? RTI_Status_Id = null;
    int? Deptt_Id = null;
    int? Old_RTI_Id = null;
    int? Temp_RTI_FAA_Id = null;
    int? RTI_Id = null;
    int? RTI_FAA_Status_Id = null;
    int? Old_RTI_FAA_Id = null;
    int? Temp_RTI_SAA_Id = null;
    int? RTI_FAA_Id = null;
    public int? RTISaaId { get; set; }
    int? RTI_SAA_Status_Id = null;
    int? Old_RTI_SAA_Id = null;
    int? RP_Id = null;
    double? Petitioner_Contact_No = null;
    string Applicant_Contact_No = string.Empty;

    //End

    //String Data Declaration area

    string PRO_No = string.Empty;
    string Year = string.Empty;
    string Petitioner_Name = string.Empty;
    public string petitionIDstr { get; set; }
    public string petitionYear  { get; set; }
    public string PetitionerMobileNo { get; set; }
    public string PetitionerPhoneNo { get; set; }
    public string PetitionerFaxNo { get; set; }
    public string PetitionerAddress { get; set; }
    public string PetitionFile { get; set; }
    string Petitioner_Email = string.Empty;
    string Respondent = string.Empty;
    public string RespondentName { get; set; }
    public string RespondentMobileNo { get; set; }
    public string RespondentPhone_No { get; set; }
    public string RespondentFaxNo { get; set; }
    public string Respondentmail { get; set; }
    public string RespondentAddress { get; set; }

    public string PetitionNotice { get; set; }
    public string ReviewPetitionNotice { get; set; }
    public string PetitionIntrimOrder { get; set; }
    public string ReviewPetitionIntrim_Order { get; set; }
    public string PlaceHolderOne { get; set; }
    public string PlaceHolderTwo { get; set; }
    public string PlaceHolderThree { get; set; }
    public string PlaceHolderFour { get; set; }
    public string PlaceHolderFive { get; set; }
	public string PlaceHolderSix { get; set; }
    public string PlaceHolderSeven { get; set; }
    public string OrderType { get; set; }
    public string SohVenue { get; set; }
    public string SohTime { get; set; }
    public string Time { get; set; }
    public string Venue { get; set; }
    public string SohFile { get; set; }

    string Subject = string.Empty;
    string File_Name = string.Empty;
    string Link_URL = string.Empty;   
    string Public_Notice = string.Empty;
    string Appeal_No = string.Empty;
    string Where_Appealed = string.Empty;
    string Judgement_Link = string.Empty;
    string Applicant_Name = string.Empty;
    string Applicant_Email = string.Empty;
    public string ApplicantAddress { get; set; }
    public string ApplicantMobileNo { get; set; }
    public string ApplicantPhoneNo { get; set; }
    public string ApplicantFaxNo { get; set; }
    public string Remarks { get; set; }
    string RP_No = string.Empty;
    string Ref_No = string.Empty;
    string FAA = string.Empty;
    string SAA = string.Empty;
    string SAARef_No = string.Empty;
    string Deptt_Name = string.Empty;

    //End

    //Datetime Data Declaration area

    DateTime? Rec_Insert_Date = null;
    DateTime? Rec_Update_Date = null;
    DateTime? SOH = null;
    public DateTime? PetitionDate { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public DateTime? SohDate { get; set; }
    public DateTime? Date { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? ApplicationDate { get; set; }
    public DateTime? AppealDate { get; set; }
    public DateTime? AwardDate { get; set; }
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

    public int? ConnectionType
    {
        get
        {
            return Connection_Type;
        }
        set
        {
            Connection_Type = value;
        }
    }
    public int? TempPetitionId
    {
        get
        {
            return Temp_Petition_Id;
        }
        set
        {
            Temp_Petition_Id = value;
        }
    }
    public int? PetitionStatusId
    {
        get
        {
            return Petition_Status_Id;
        }
        set
        {
            Petition_Status_Id = value;
        }
    }
    public int? ReView
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
    public int? OldPetitionId
    {
        get
        {
            return Old_Petition_Id;
        }
        set
        {
            Old_Petition_Id = value;
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
    public int? TempPAId
    {
        get
        {
            return Temp_PA_Id;
        }
        set
        {
            Temp_PA_Id = value;
        }
    }
    public int? PAId
    {
        get
        {
            return PA_Id ;
        }
        set
        {
            PA_Id = value;
        }
    }
    public int? TempRPId
    {
        get
        {
            return Temp_RP_Id;
        }
        set
        {
            Temp_RP_Id = value;
        }
    }
    public int? RPId
    {
        get
        {
            return RP_Id;
        }
        set
        {
            RP_Id = value;
        }
    }
    public int? PAStatusId
    {
        get
        {
            return PA_Status_Id;
        }
        set
        {
            PA_Status_Id = value;
        }
    }
    public int? OldPAId
    {
        get
        {
            return Old_PA_Id;
        }
        set
        {
            Old_PA_Id = value;
        }
    }
    public Int32? TempAppealId
    {
        get
        {
            return Temp_Appeal_Id;
        }
        set
        {
            Temp_Appeal_Id = value;
        }
    }
    public int? AppealStatusId
    {
        get
        {
            return Appeal_Status_Id;
        }
        set
        {
            Appeal_Status_Id = value;
        }
    }
    public int? appeal
    {
        get
        {
            return Appeal;
        }
        set
        {
            Appeal = value;
        }
    }
    public Int32? OldAppealId
    {
        get
        {
            return Old_Appeal_Id;
        }
        set
        {
            Old_Appeal_Id = value;
        }
    }
    public int? TempRAId
    {
        get
        {
            return Temp_RA_Id;
        }
        set
        {
            Temp_RA_Id = value;
        }
    }
    public Int32? AppealId
    {
        get
        {
            return Appeal_Id;
        }
        set
        {
            Appeal_Id = value;
        }
    }
    public int? RAStatusId
    {
        get
        {
            return RA_Status_Id;
        }
        set
        {
            RA_Status_Id = value;
        }
    }
    public int? PetitionId
    {
        get
        {
            return Petition_Id;
        }
        set
        {
            Petition_Id = value;
        }
    }
    public int? RPStatusId
    {
        get
        {
            return RP_Status_Id;
        }
        set
        {
            RP_Status_Id = value;
        }
    }
    public int? OldRPId
    {
        get
        {
            return Old_RP_Id;
        }
        set
        {
            Old_RP_Id = value;
        }
    }
    public int? TempRTIId
    {
        get
        {
            return Temp_RTI_Id;
        }
        set
        {
            Temp_RTI_Id = value;
        }
    }
    public int? RTIStatusId
    {
        get
        {
            return RTI_Status_Id;
        }
        set
        {
            RTI_Status_Id = value;
        }
    }
    public int? DepttsId
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
    public int? OldRTIId
    {
        get
        {
            return Old_RTI_Id;
        }
        set
        {
            Old_RTI_Id = value;
        }
    }
    public int? TempRTIFAAId
    {
        get
        {
            return Temp_RTI_FAA_Id;
        }
        set
        {
            Temp_RTI_FAA_Id = value;
        }
    }
    public int? RTIId
    {
        get
        {
            return RTI_Id;
        }
        set
        {
            RTI_Id = value;
        }
    }
    public int? RTIFAAStatusId
    {
        get
        {
            return RTI_FAA_Status_Id;
        }
        set
        {
            RTI_FAA_Status_Id = value;
        }
    }
    public int? OldRTIFAAId
    {
        get
        {
            return Old_RTI_FAA_Id;
        }
        set
        {
            Old_RTI_FAA_Id = value;
        }
    }
    public int? TempRTISAAId
    {
        get
        {
            return Temp_RTI_SAA_Id;
        }
        set
        {
            Temp_RTI_SAA_Id = value;
        }
    }
    public int? RTIFAAId
    {
        get
        {
            return RTI_FAA_Id;
        }
        set
        {
            RTI_FAA_Id = value;
        }
    }
    public int? RTISAAStatusId
    {
        get
        {
            return RTI_SAA_Status_Id;
        }
        set
        {
            RTI_SAA_Status_Id = value;
        }
    }
    public int? OldRTISAAId
    {
        get
        {
            return Old_RTI_SAA_Id;
        }
        set
        {
            Old_RTI_SAA_Id = value;
        }
    }
    
    public double? PetitionerContactNo
    {
        get
        {
            return Petitioner_Contact_No;
        }
        set
        {
            Petitioner_Contact_No = value;
        }
    }
    public string ApplicantContactNo
    {
        get
        {
            return Applicant_Contact_No;
        }
        set
        {
            Applicant_Contact_No = value;
        }
    }

    //End

    //Properties for string variables

    public string PRONo
    {
        get
        {
            return PRO_No;
        }
        set
        {
            PRO_No = value;
        }
    }
    public string year
    {
        get
        {
            return Year;
        }
        set
        {
            Year = value;
        }
    }
    public string PetitionerName
    {
        get
        {
            return Petitioner_Name;
        }
        set
        {
            Petitioner_Name = value;
        }
    }
    public string PetitionerEmail
    {
        get
        {
            return Petitioner_Email;
        }
        set
        {
            Petitioner_Email = value;
        }
    }
    public string ResPondent
    {
        get
        {
            return Respondent;
        }
        set
        {
            Respondent = value;
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
    public string LinkURL
    {
        get
        {
            return Link_URL;
        }
        set
        {
            Link_URL = value;
        }
    }
    public string PublicNotice
    {
        get
        {
            return Public_Notice;
        }
        set
        {
            Public_Notice = value;
        }
    }
    public string AppealNo
    {
        get
        {
            return Appeal_No;
        }
        set
        {
            Appeal_No = value;
        }
    }
    public string WhereAppealed
    {
        get
        {
            return Where_Appealed;
        }
        set
        {
            Where_Appealed = value;
        }
    }
    public string JudgementLink
    {
        get
        {
            return Judgement_Link;
        }
        set
        {
            Judgement_Link = value;
        }
    }
    public string ApplicantName
    {
        get
        {
            return Applicant_Name;
        }
        set
        {
            Applicant_Name = value;
        }
    }
    public string ApplicantEmail
    {
        get
        {
            return Applicant_Email;
        }
        set
        {
            Applicant_Email = value;
        }
    }
    public string RPNo
    {
        get
        {
            return RP_No;
        }
        set
        {
            RP_No = value;
        }
    }
    public string RefNo
    {
        get
        {
            return Ref_No;
        }
        set
        {
            Ref_No = value;
        }
    }
    public string Faa
    {
        get
        {
            return FAA;
        }
        set
        {
            FAA = value;
        }
    }
    public string Saa
    {
        get
        {
            return SAA;
        }
        set
        {
            SAA = value;
        }
    }

    public string SaaRef_No
    {
        get
        {
            return SAARef_No;
        }
        set
        {
            SAARef_No = value;
        }
    }
    public string DepttName
    {
        get
        {
            return Deptt_Name;
        }
        set
        {
            Deptt_Name = value;
        }
    }

   

    public string keyword { get; set; }

    
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
    public DateTime? Soh
    {
        get
        {
            return SOH;
        }
        set
        {
            SOH = value;
        }
    }

    //End
    

    #endregion
}
