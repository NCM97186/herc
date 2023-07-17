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

public class PublicNoticeBL
{
    #region Default constructor zone

    public PublicNoticeBL()
	{

    }

    #endregion

    //Area for all the variables declaration zone

    PublicNoticeDL pubNoticeDL = new PublicNoticeDL();

    //End

    //Area for all the functions to insert, update and delete

    #region Function to insert new public notice

    public Int32 PublicNoticeInsertUpdate(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.PublicNoticeInsertUpdate(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to update the public notice status

    public Int32 PublicNoticeUpdateStatus(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.PublicNoticeUpdateStatus(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion 

    #region Function to approve public notice and send it into final table

    public Int32 PublicNoticeApprove(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.PublicNoticeApprove(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion 

    //End

    //Area for all the functions to display records

    #region Function To Display public notice with paging

    public DataSet DisplayPublicNoticesWithPaging(PetitionOB petObject, out int catValue)
    {
        try
        {
            return pubNoticeDL.DisplayPublicNoticesWithPaging(petObject, out catValue);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to display public notice by id for Editing

    public DataSet DisplayPublicNoticeByID(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.DisplayPublicNoticeByID(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get public notice id  for edit

    public DataSet PublicNoticeId(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.PublicNoticeId(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    //End

    //#region Function to get Petition numbers for connections

    //public DataSet getPetitionNumberForConnection(PetitionOB petObject)
    //{
    //    try
    //    {
    //        return pubNoticeDL.getPetitionNumberForConnection(petObject);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //#endregion

    #region Function to insert PublicNoticeWithPetition

    public Int32 insertPublicNoticewithPetition(PetitionOB petObject)
    {
        try
        {
           return pubNoticeDL.insertPublicNoticewithPetition(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get connected Petition(For Edit)

    public DataSet getPetitionWithPublicNoticeEdit(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.getPetitionWithPublicNoticeEdit(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get connected PublicNotice for Petition(For Edit)

    public DataSet get_ConnectedPubicNotice_Edit(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.get_ConnectedPubicNotice_Edit(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get connected PublicNotice for Petition(For new editing)

    public DataSet getConnectedPubicNoticeEdit(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.getConnectedPubicNoticeEdit(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to delete Connectedpetition from public notice

    public Int32 deleteConnectedPetitionFromPublicNotice(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.deleteConnectedPetitionFromPublicNotice(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function To Display public notice On Index page
    public DataSet displayPublicNotice(LinkOB lnkObject)
    {
        try
        {
            return pubNoticeDL.displayPublicNotice(lnkObject);
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region function to get publicNotice information
    public DataSet Get_publiceNotceDetails(LinkOB objlinkOB, out int catValue)
    {

        try
        {


            return pubNoticeDL.Get_publiceNotceDetails(objlinkOB, out catValue);
        }
        catch
        {
            throw;
        }

    }
    #endregion

    #region function to get publicNotice information
    public DataSet Get_publiceNotceoldDetails(LinkOB objlinkOB, out int catValue)
    {

        try
        {


            return pubNoticeDL.Get_publiceNotceoldDetails(objlinkOB, out catValue);
        }
        catch
        {
            throw;
        }

    }
    #endregion 

    #region function to get year of Petition Admin

    public DataSet GetYearPetition_Admin(LinkOB objlinkOB)
    {
        try
        {
            return pubNoticeDL.GetYearPetition_Admin(objlinkOB);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Petition numbers for connections in Public Notices

    public DataSet getPetitionNumberForConnection(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.getPetitionNumberForConnection(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to Delete the public notices either from temp or from final

    public Int32 Delete_PublicNotices(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.Delete_PublicNotices(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region function to get Connected publicNotice information old
    public DataSet Get_ConnectedpubliceNotce(PetitionOB petObject)
    {

        try
        {
            return pubNoticeDL.Get_ConnectedpubliceNotce(petObject);
        }
        catch
        {
            throw;
        }
        
    }
    #endregion


    #region function to get Connected publicNotice information old
    public DataSet Get_ConnectedpubliceNotceForReview(PetitionOB petObject)
    {

        try
        {
            return pubNoticeDL.Get_ConnectedpubliceNotceForReview(petObject);
        }
        catch
        {
            throw;
        }
       
    }
    #endregion


    #region Function to update the  public notice status for delete

    public Int32 updatePublicStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            return pubNoticeDL.updatePublicStatusDelete(rtiObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to get Connected publicNotice information Id
    public DataSet Get_ConnectedpubliceNotceById(PetitionOB petObject)
    {

        try
        {
            return pubNoticeDL.Get_ConnectedpubliceNotceById(petObject);
        }
        catch
        {
            throw;
        }
       
    }
    #endregion


    #region function to get year of Public Notice Admin

    public DataSet GetYearPublicNotice_Admin()
    {
        try
        {
            return pubNoticeDL.GetYearPublicNotice_Admin();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to insert Connected Public notice files

    public Int32 insertConnectedPublicNoticeFiles(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.insertConnectedPublicNoticeFiles(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get FileName for public notices

    public DataSet getFileNameForPublicNotice(PetitionOB publicNoticeObject)
    {
        try
        {
            return pubNoticeDL.getFileNameForPublicNotice(publicNoticeObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to Update status for Files in public notices

    public Int32 UpdateFileStatusForPublicNotices(PetitionOB orderObject)
    {
        try
        {
            return pubNoticeDL.UpdateFileStatusForPublicNotices(orderObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region Function to get connected Public notice file name

    public DataSet getPublicNoticeFileNames(PetitionOB objpetOB)
    {
        try
        {

            return pubNoticeDL.getPublicNoticeFileNames(objpetOB);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region function to get year

    public DataSet GetPublicNoticesYear()
    {
        try
        {
            return pubNoticeDL.GetPublicNoticesYear();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get publicNotice information

    public DataSet Get_publiceNotceDetailsforReviewPetition(LinkOB objlinkOB, out int catValue)
    {

        try
        {
            return pubNoticeDL.Get_publiceNotceDetailsforReviewPetition(objlinkOB, out catValue);
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region function to get publicNotice information old

    public DataSet Get_publiceNotceoldDetailsForReviewPetition(LinkOB objlinkOB, out int catValue)
    {

        try
        {
            return pubNoticeDL.Get_publiceNotceoldDetailsForReviewPetition(objlinkOB, out catValue);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get Connected publicNotice information Id for Details
    public DataSet Get_ConnectedpubliceNotceByIdDetails(PetitionOB petObject)
    {

        try
        {
            return pubNoticeDL.Get_ConnectedpubliceNotceByIdDetails(petObject);
        }
        catch
        {
            throw;
        }
        
    }
    #endregion

    #region Function to get PublicNotic year for connections

    public DataSet getPublicNoticYearForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.getPublicNoticYearForConnectionEdit(petObject);
        }
        catch
        {
            throw;
        }
        


    }

    #endregion

    #region function to get email ids to send email.

    public DataSet getEmailIdForSendingMail(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.getEmailIdForSendingMail(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to get mobile numbers to send sms.

    public DataSet getEmailIdForSendingSms(PetitionOB petObject)
    {
        try
        {
            return pubNoticeDL.getEmailIdForSendingSms(petObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region function to get mobile numbers to send sms.

    public DataSet getPublicNoticeURLwithFile(PetitionOB publicNoticeObject)
    {
        try
        {
            return pubNoticeDL.getPublicNoticeURLwithFile(publicNoticeObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion
}
