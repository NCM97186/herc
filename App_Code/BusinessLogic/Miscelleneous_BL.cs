using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text;

public class Miscelleneous_BL
{
    #region Default constructor zone

    public Miscelleneous_BL()
	{

    }

    #endregion

    #region Data declaration zone

    Miscelleneous_DL miscellDL = new Miscelleneous_DL();

    #endregion

    #region Function to get date in mm/dd/yyyy format

    public DateTime getDateFormat(string mydate)
    {
        try
        {
            return miscellDL.getDateFormat(mydate);
        }
        catch
        {
            throw;
        }

    }

    #endregion

		    #region Function to get date in mm/dd/yyyy format

    public DateTime getDateFormatSohCalender(string mydate)
    {
        try
        {
            return miscellDL.getDateFormatSohCalender(mydate);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get date in dd/mm/yyyy format

    public string getDateFormatddMMYYYY(string mydate)
    {
        try
        {
            return miscellDL.getDateFormatddMMYYYY(mydate);
        }
        catch
        {
            
            throw;
        }

    }

    #endregion

    #region code to Create Directory

    public void MakeDirectoryIfExists(string NewDirectory)
    {
       try
        {
            miscellDL.MakeDirectoryIfExists(NewDirectory);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to check File existance during uploading

    public string getUniqueFileName(string filname, string filepath, string fileNameWithoutExt, string ext)
    {
        try
        {
            return miscellDL.getUniqueFileName(filname, filepath, fileNameWithoutExt, ext);
        }
        catch
        {
            throw;
        }
        
    }


    #endregion

    #region Function to get status

    public DataSet getStatusType()
    {
        try
        {
            return miscellDL.getStatusType();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 

    #region Function to get Language

    public DataSet getLanguage(UserOB usrObject)
    {
        try
        {
            return miscellDL.getLanguage(usrObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region Function to get Status according to permission

    public DataSet getStatusPermissionwise(UserOB usrObject)
    {
        try
        {
            return miscellDL.getStatusPermissionwise(usrObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get Language  as per given permission

    public DataSet getLanguagePermission(UserOB usrObject)
    {
        try
        {
            return miscellDL.getLanguagePermission(usrObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to limit string length upto 40 characters

    public static string FixCharacters(object Desc, int length)
    {
        try
        {
            return FixCharacters(Desc, length);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to fix on cs page string length upto given characters

    public string FixGivenCharacters(string Desc, int length)
    {
        try
        {
            Miscelleneous_DL myMiscDL = new Miscelleneous_DL();
            return myMiscDL.FixGivenCharacters(Desc, length);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Categoty For Banner Mgmt
    public  DataSet Get_Category_ForBanner()
    {
        try
        {
            return miscellDL.Get_Category_ForBanner();
        }
        catch
        {
            throw;
        }
    }
    #endregion 

    #region function to check file extension

    public bool CheckImageFileExtension(string extension)
    {
        try
        {
            return miscellDL.CheckImageFileExtension(extension);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get the module Information
    public DataSet ASP_Master_Module_Get_Module()
    {
        try
        {
            return miscellDL.ASP_Master_Module_Get_Module();
        }
        catch
        {
            throw;
        }
    }

    #endregion


    //Function to get actual file type

    public  bool GetActualFileType(Stream stream)
    {

        try
        {
            Miscelleneous_DL myMiscDL = new Miscelleneous_DL();
            return myMiscDL.GetActualFileType(stream);
        }

        catch 
        {

            throw;

        }

    }

    public bool GetActualFileType_pdf(Stream stream)
    {

        try
        {
            Miscelleneous_DL myMiscDL = new Miscelleneous_DL();
            return myMiscDL.GetActualFileType_pdf(stream);
        }

        catch
        {

            throw;

        }

    }


    public bool SendEmailForLogin(string To, string Cc, string Bcc, string Subject, string From, string Msg)// send mail 
    {
        try
        {
            Miscelleneous_DL myMiscDL = new Miscelleneous_DL();
            return myMiscDL.SendEmailForLogin(To, Cc, Bcc, Subject, From, Msg);
        }
        catch
        {
            throw;
        }
    }

    public bool SendEmail(string To, string Cc, string Bcc, string Subject, string From, string Msg)// send mail 
    {
        try
        {
            Miscelleneous_DL myMiscDL = new Miscelleneous_DL();
            return myMiscDL.SendEmail(To, Cc, Bcc, Subject, From, Msg);
        }
        catch
        {
            throw;
        }
    }

    #region Function to get Status according to modules

    public DataSet getStatusAccordingtoModule(PetitionOB petObject)
    {
        try
        {
            return miscellDL.getStatusAccordingtoModule(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Status for petitions

    public DataSet getStatusPetition(PetitionOB petObject)
    {
        try
        {
            return miscellDL.getStatusPetition(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to get petition Status according to modules

    public DataSet getPetitionStatusAccordingtoModule(PetitionOB petObject)
    {
        try
        {
            return miscellDL.getPetitionStatusAccordingtoModule(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get petition Appeal Status according to modules

    public DataSet getPetitionAppealStatusAccordingtoModule(PetitionOB petObject)
    {
        try
        {
            return miscellDL.getPetitionAppealStatusAccordingtoModule(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to find IP Address

    public string IpAddress()
    {
        try
        {

            return miscellDL.IpAddress();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to remove XXS(Cross-site scripting)

    public string strip_dangerous_tags(string text_with_tags)
    {
        return miscellDL.strip_dangerous_tags(text_with_tags);
    }

    #endregion

    public string RemoveFCKEditorTagHtml(string html)
    {

        return miscellDL.RemoveFCKEditorTagHtml(html);

    }

    //End

    public string Division_characters(string text, int length)
    {
        return miscellDL.Division_characters(text, length);

        // End
    }

    #region Function to get Status for petition appeal

    public DataSet getStatusForPetitionAppeal(PetitionOB petObject)
    {
        try
        {
            return miscellDL.getStatusForPetitionAppeal(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to get Status for petition appeal Award

    public DataSet getStatusForPetitionAppealAward(PetitionOB petObject)
    {
        try
        {
            return miscellDL.getStatusForPetitionAppealAward(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    public bool SendEmailWithAttachments(string To, string Cc, string Bcc, string Subject, string From, string Msg, string attachment)// send mail 
    {
        try
        {
            Miscelleneous_DL myMiscDL = new Miscelleneous_DL();
            return myMiscDL.SendEmailWithAttachments(To, Cc, Bcc, Subject, From, Msg,attachment);
        }
        catch
        {
            throw;
        }
    }

    #region Function to get file length

    public string fileSize(double fileLength)
    {

        return miscellDL.fileSize(fileLength);
    }

    #endregion

    #region Function to get file length

    public string fileSizeForContentPage(double fileLength)
    {

        return miscellDL.fileSizeForContentPage(fileLength);
    }

    #endregion

    public  string RemoveUnnecessaryHtmlTagHtml(string html)
    {
        return miscellDL.RemoveUnnecessaryHtmlTagHtml(html);
    }

    #region Function to get Meta Language

    public DataSet getMetaLanguage()
    {
        try
        {

            return miscellDL.getMetaLanguage();
        }
        catch
        {
            throw;
        }

    }

    #endregion




    #region function to get mobile numbers to send sms.

    public DataSet GetMobileNumberForSendingSms(PetitionOB petObject)
    {
        try
        {
            return miscellDL.GetMobileNumberForSendingSms(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    public void SendsmsApprove(string Msg, string MobileNumber, string text)
    {
        miscellDL.SendsmsApprove(Msg, MobileNumber, text);
    }

    public void Sendsms(string Msg, string UserName, string MobileNumber,string text)
    {
        miscellDL.Sendsms(Msg, UserName, MobileNumber, text);
    }


}
