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


public class AppealBL
{
    #region Default constructors zone

    public AppealBL()
    {

    }

    #endregion

    //Area for all the variables declaration zone

    #region Data declaration zone

    AppealDL appealDL = new AppealDL();

    #endregion

    //End

    //Area for all the user defined functions to insert and update

    #region Function to insert Appeal records into temp table

    public Int32 insertUpdateTempAppeal(PetitionOB appealObject)
    {
        try
        {
            return appealDL.insertUpdateTempAppeal(appealObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to update the temporary appeal status

    public Int32 updateAppealStatus(PetitionOB appealObject)
    {
        try
        {
            return appealDL.updateAppealStatus(appealObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function To insert appeal into web final table

    public Int32 InsertAppealIntoWeb(PetitionOB appealObject)
    {
        try
        {
            return appealDL.InsertAppealIntoWeb(appealObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    //Area for all the user defined functions to display records

    #region Function to get temporary appeal records

    public DataSet getTempAppealRecords(PetitionOB appealObject, out int catValue)
    {
        try
        {
            return appealDL.getTempAppealRecords(appealObject, out catValue);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get appeal number

    public DataSet getAppealNumber(PetitionOB appealObject)
    {
        try
        {
            return appealDL.getAppealNumber(appealObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get appeal record for edit

    public DataSet getAppealRecordForEdit(PetitionOB appealObject)
    {
        try
        {
            return appealDL.getAppealRecordForEdit(appealObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region Function to get appeal record

    public DataSet getAppealRecord(PetitionOB appealObject)
    {
        try
        {
           return appealDL.getAppealRecord(appealObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get appeal status

    public DataSet getAppealStatus()
    {
        try
        {
            return appealDL.getAppealStatus();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get appeal id from temp table for comparision

    public DataSet get_ID_For_Compare(PetitionOB appealObject)
    {
        try
        {
            return appealDL.get_ID_For_Compare(appealObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Appeal numbers in edit mode

    public DataSet getAppealNumberInEditMode(PetitionOB appealObject)
    {
        try
        {
            return appealDL.getAppealNumberInEditMode(appealObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to update the appeal status

    public Int32 modifyAppealStatus(PetitionOB appealObject)
    {
        try
        {
            return appealDL.modifyAppealStatus(appealObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    //End

    //Area for Award pronounced for the appeal on date 06-11-2012

    #region Function to get appeal numbers

    public DataSet getAppealNumberforAwardPronounced(PetitionOB appealObject)
    {
        try
        {
            return appealDL.getAppealNumberforAwardPronounced(appealObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    public DataSet getAppealNumberYearWise(PetitionOB petObject)
    {
        try
        {
            return appealDL.getAppealNumberYearWise(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #region Function to get appeal numbers for edit

    public DataSet getAppealNumberforAwardPronouncedEdit(PetitionOB appealObject)
    {
        try
        {
            return appealDL.getAppealNumberforAwardPronouncedEdit(appealObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get temp/final Award pronounced (Conditional)

    public DataSet getAwardPronounced(PetitionOB petObject, out int catValue)//Created on date 07-11-2012
    {
        try
        {
            return appealDL.getAwardPronounced(petObject, out catValue);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Temp petition(For Edit)

    public DataSet getAppealAwardForEdit(PetitionOB petObject)
    {
        try
        {
            return appealDL.getAppealAwardForEdit(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Review Appeal ID from temp table for comparision

    public DataSet ReviewAppealIDforComparision(PetitionOB orderObject)
    {
        try
        {
            return appealDL.ReviewAppealIDforComparision(orderObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    //Area for all the user defined function to insert and update

    #region Function to insert-update temp Review Appeal table records

    public Int32 insertUpdateReviewAppealTemp(PetitionOB orderObject)
    {
        try
        {
            return appealDL.insertUpdateReviewAppealTemp(orderObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function To insert record into web_AppealReview

    public Int32 ApproveAwardPronounced(PetitionOB petObject)
    {
        try
        {
            return appealDL.ApproveAwardPronounced(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to update the appeal award status

    public Int32 AppealAwardUpdateStatus(PetitionOB orderObject)
    {
        try
        {
            return appealDL.AppealAwardUpdateStatus(orderObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    //Display clint side Appeal
    #region
    public DataSet Get_appeal(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return appealDL.Get_appeal(obj_petOB, out catValue);
        }
        catch
        {
            throw;
        }
    }
    #endregion

    //end
    //Display client side Appeal prev year
    #region
    public DataSet Get_appeal_prevyear(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return appealDL.Get_appeal_prevyear(obj_petOB, out catValue);
        }
        catch
        {
            throw;
        }
    }
    #endregion
    //end


    


    #region function to get online status information
    public DataSet Get_OnlineStatus(PetitionOB obj_petOB)
    {

        try
        {
            return appealDL.Get_OnlineStatus(obj_petOB);
        }
        catch
        {
            throw;
        }

    }
    #endregion

    //*********************************Award Pronounced User Side *******************************************//


    #region Function to get current Year Award Pronouncde
    public DataSet Get_Award_pronounced_CurrentYear(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return appealDL.Get_Award_pronounced_CurrentYear(obj_petOB, out catValue);
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region Function to get current Year Award Pronouncd for what's new section

    public DataSet GetWhatsNewAwards(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return appealDL.GetWhatsNewAwards(obj_petOB, out catValue);

        }
        catch
        {
            throw;
        }
    }
    #endregion

    //Appeal detail By AppealId
    #region
    public DataSet Get_Appeal_Detail_By_ID(PetitionOB obj_petOB)
    {
        try
        {
            return appealDL.Get_Appeal_Detail_By_ID(obj_petOB);
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region Function to get Prev Year Award Pronouncde
    public DataSet Get_Award_pronounced_PrevYear(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return appealDL.Get_Award_pronounced_PrevYear(obj_petOB, out catValue);
        }
        catch
        {
            throw;
        }
    }
    #endregion

    //  Function to search award
    #region

    public DataSet Search_AwardRecord(PetitionOB obj_petOB, string strsearchcond)
    {
        try
        {
            return appealDL.Search_AwardRecord(obj_petOB, strsearchcond);
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region function to get years of awards

    public DataSet GetAwardYear()
    {
        try
        {
            return appealDL.GetAwardYear();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get years of award under appeal

    public DataSet GetAwardUnderAppealYear()
    {
        try
        {
            return appealDL.GetAwardUnderAppealYear();
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region function to get years of award underappeal

    public DataSet GetAward_UnderAppealYear()
    {
        try
        {
            return appealDL.GetAward_UnderAppealYear();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 

   

    #region function to get years for Award Under Appeal

    public DataSet GetAwardUnderAppealYearOmbudsman()
    {
        try
        {
            return appealDL.GetAwardUnderAppealYearOmbudsman();
        }
        catch
        {
            throw;
        }
       
    }

     #endregion 

    #region function to get AppealNumber of award

    public DataSet GetAppealNumberAward(PetitionOB obj_petOB)
    {
        try
        {
            return appealDL.GetAppealNumberAward(obj_petOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 


    #region Function to get current Year Award Pronouncde

    public DataSet GetAwardUnderAppeal(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return appealDL.GetAwardUnderAppeal(obj_petOB, out catValue);

        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region Function to get current Year Award Pronounced details

    public DataSet GetAwardUnderAppealDetails(PetitionOB obj_petOB)
    {
        try
        {

            return appealDL.GetAwardUnderAppealDetails(obj_petOB);


        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region function to get appeal Id

    public DataSet Get_Appeal(PetitionOB obj_petOB)
    {
        try
        {
            return appealDL.Get_Appeal(obj_petOB);
        }
        catch
        {
            throw;
        }

    }


    #endregion

    #region function to bind year

    public DataSet GetYear(PetitionOB appealObject)
    {
        try
        {
            return appealDL.GetYear(appealObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region function to bind year from AppealAwardPronouncedTmp(both table)

    public DataSet GetYearAward()
    {
        try
        {
            return appealDL.GetYearAward();
        }
        catch
        {
            throw;
        }
      
    }

    #endregion 


    #region function to delete pending or approved record

    public Int32 Delete_Pending_Approved_RecordAward(PetitionOB objpet)
    {
        try
        {
            return appealDL.Delete_Pending_Approved_RecordAward(objpet);
        }
        catch
        {
            throw;
        }

    }


    #endregion



    #region Function to update Web_Review_Appeal for Restore

    public Int32 Web_Review_Appeal_Restore(PetitionOB rtiObject)
    {
        try
        {
            return appealDL.Web_Review_Appeal_Restore(rtiObject);

        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get connected SOH of Appeal

    public DataSet getConnectedSOH_Appeal(PetitionOB objpetOB)
    {
        try
        {
            return appealDL.getConnectedSOH_Appeal(objpetOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to get year

    public DataSet Get_Year_Appeal()
    {
        try
        {
            return appealDL.Get_Year_Appeal();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to GetAwardForAppeal

    public DataSet GetAwardForAppeal(PetitionOB objpetOB)
    {
        try
        {

            return appealDL.GetAwardForAppeal(objpetOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to get connected Award Appeal files

    public DataSet getConnectedAwardApealFiles(PetitionOB objpetOB)
    {
        try
        {
            return appealDL.getConnectedAwardApealFiles(objpetOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region function to get year of Award Admin

    public DataSet GetYearAward_Admin()
    {
        try
        {
            return appealDL.GetYearAward_Admin();
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to getAward Under Appeals

    public DataSet getAwardUnderAppealDetails(PetitionOB objpetOB)
    {
        try
        {
            return appealDL.getAwardUnderAppealDetails(objpetOB);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region Function to get Appeal number during deleting record from reviewAppeal table

    public DataSet getAppeal_Number_for_DeleteReviewAppeal(PetitionOB petObject)
    {
        try
        {
            return appealDL.getAppeal_Number_for_DeleteReviewAppeal(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion



    #region Functiont to delete AppealReview either temp or final

    public Int32 Delete_AppealReview(PetitionOB petObject)
    {
        try
        {
            return appealDL.Delete_AppealReview(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Functiont to delete AppealReview with award either temp or final

    public Int32 Delete_AwardwithAppealReview(PetitionOB petObject)
    {
        try
        {
            return appealDL.Delete_AwardwithAppealReview(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function to update Web_Review_Appeal and Award also for Restore

    public Int32 AppealAward_Restore(PetitionOB rtiObject)
    {
        try
        {
            return appealDL.AppealAward_Restore(rtiObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion



    #region Function To insert appealAward into web final table

    public Int32 InsertAppealAwardTmp(PetitionOB appealObject)
    {
        try
        {

            return appealDL.InsertAppealAwardTmp(appealObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 



    #region Function to get temporary appeal Award records

    public DataSet getTempAppealAwardRecords(PetitionOB appealObject, out int catValue)
    {
        try
        {
            return appealDL.getTempAppealAwardRecords(appealObject,out catValue);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function to update the temporary appeal Award status

    public Int32 updateAppealAwardStatus(PetitionOB appealObject)
    {
        try
        {
            return appealDL.updateAppealAwardStatus(appealObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region Function To insert appeal into web final table

    public Int32 InsertAppealAwardIntoWeb(PetitionOB appealObject)
    {
        try
        {
            return appealDL.InsertAppealAwardIntoWeb(appealObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion 

    #region Function To Get appeal

    public DataSet GETAppealID_Award(PetitionOB appealObject)
    {
        try
        {
            return appealDL.GETAppealID_Award(appealObject);

        }
        catch
        {
            throw;
        }
       
    }

    #endregion 

    #region Function to get appeal Award record for edit

    public DataSet GetAppealAwardPronouncedTmp(PetitionOB appealObject)
    {
        try
        {
            return appealDL.GetAppealAwardPronouncedTmp(appealObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get appeal number record

    public DataSet GetAppealNumberDuringAward(PetitionOB appealObject)
    {
        try
        {
            return appealDL.GetAppealNumberDuringAward(appealObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function to get appealAward id from temp table for comparision

    public DataSet get_ID_For_CompareAward(PetitionOB appealObject)
    {
        try
        {
            return appealDL.get_ID_For_CompareAward(appealObject);  
        }
        catch
        {
            throw;
        }
     
    }

    #endregion


    #region Function to insert AppealAward  files

    public Int32 insertAppealAwardFiles(PetitionOB petObject)
    {
        try
        {
            return appealDL.insertAppealAwardFiles(petObject);
         
        }
        catch
        {
            throw;
        }
       

    }

    #endregion


    #region Function to insert AppealAwardPronouncedFiles

    public Int32 insertAppealAwardPronouncedFiles(PetitionOB petObject)
    {
        try
        {
            return appealDL.insertAppealAwardPronouncedFiles(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get FileName for public notices

    public DataSet getFileNameForAppealAward(PetitionOB PetObject)
    {
        try
        {
            return appealDL.getFileNameForAppealAward(PetObject);
        }
        catch
        {
            throw;
        }
       

    }

    #endregion



    #region Function to get FileName AppealAwardProunced

    public DataSet getFileNameForAppealAwardProunced(PetitionOB PetObject)
    {
        try
        {
            return appealDL.getFileNameForAppealAwardProunced(PetObject);
        }
        catch
        {
            throw;
        }
        

    }

    #endregion



    #region Function to delete  Files in AppealAward

    public Int32 DeleteFileForAppealAward(PetitionOB PetObject)
    {
        try
        {
            return appealDL.DeleteFileForAppealAward(PetObject);
        }
        catch
        {
            throw;
        }
       

    }

    #endregion


    #region Function to delete  Files in DeleteAppealAwardPronouncedFiles

    public Int32 DeleteAppealAwardPronouncedFiles(PetitionOB PetObject)
    {
        try
        {
            return appealDL.DeleteAppealAwardPronouncedFiles(PetObject);
        }
        catch
        {
            throw;
        }
        

    }

    #endregion




    #region Function to Delete the Appeal Award either from temp or from final

    public Int32 Delete_AppealAward(PetitionOB petObject)
    {
        try
        {
            return appealDL.Delete_AppealAward(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion



    #region Function to update AppealAwardPronouncedWeb for Restore

    public Int32 AppealAwardPronouncedWeb_Restore(PetitionOB rtiObject)
    {
        try
        {
            return appealDL.AppealAwardPronouncedWeb_Restore(rtiObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function to get connected Award of Appeal

    public DataSet getConnectedAwardFiles(PetitionOB objpetOB)
    {
        try
        {
            return appealDL.getConnectedAwardFiles(objpetOB);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get connected Award of Appeal

    public DataSet getAppealAwardPronounced(PetitionOB objpetOB)
    {
        try
        {

            return appealDL.getAppealAwardPronounced(objpetOB);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion


    #region Function to get connected AppealAwardPronouncedFiles

    public DataSet getAppealAwardPronouncedFiles(PetitionOB objpetOB)
    {
        try
        {
            return appealDL.getAppealAwardPronouncedFiles(objpetOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    //Display Appeal Search
    #region Appeal Search
    public DataSet Get_appeal_search(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return appealDL.Get_appeal_search(obj_petOB, out catValue);



        }
        catch
        {
            throw;
        }
    }
    #endregion
    //end

    public DataSet SearchAward(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return appealDL.SearchAward(obj_petOB,out catValue);

        }
        catch
        {
            throw;
        }
      

    }

    #region function to USP_GetYearAppealForSearch for Appeal

    public DataSet GetYearAppealForSearch()
    {
        try
        {
            return appealDL.GetYearAppealForSearch();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 

    #region function to USP_GetYearAwardForSearch for Appeal

    public DataSet GetYearAwardForSearch()
    {
        try
        {
            return appealDL.GetYearAwardForSearch();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 

    #region Function to get current Year new Award Pronouncde
    public DataSet GetLatestAward(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return appealDL.GetLatestAward(obj_petOB, out catValue);

        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region Function to get current Year new Appeal

    public DataSet GetLatestAppeal(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return appealDL.GetLatestAppeal(obj_petOB, out catValue);

        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    // 2 Aug 2013
    #region Function to insert ConnectedMultipleReviewAppeal

    public Int32 insertConnectedMultipleReviewAppeal(PetitionOB petObject)
    {
        try
        {
            return appealDL.insertConnectedMultipleReviewAppeal(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get connected Appeal Review(For Edit)

    public DataSet get_ConnectMultipleReviewAppeal_Edit(PetitionOB petObject)
    {
        try
        {
            return appealDL.get_ConnectMultipleReviewAppeal_Edit(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get Appeal Review year for connections

    public DataSet Connected_multipleReviewAppealForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            return appealDL.Connected_multipleReviewAppealForConnectionEdit(petObject);
        }
        catch
        {
            throw;
        }
       

    }

    #endregion

    #region Function to delete ConnectedReviewAppeal

    public Int32 deleteConnectedReviewAppealMultiple(PetitionOB petObject)
    {
        try
        {
            return appealDL.deleteConnectedReviewAppealMultiple(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get connected review  appeal Details

    public DataSet get_ConnectedReviewAppealDetails(PetitionOB petObject)
    {
        try
        {

            return appealDL.get_ConnectedReviewAppealDetails(petObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region Function to get connected ReviewAppeal

    public DataSet get_ConnectedReviewAppeal(PetitionOB petObject)
    {
        try
        {
            return appealDL.get_ConnectedReviewAppeal(petObject);
           
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get connected Award details

    public DataSet get_AwardDetails(PetitionOB petObject)
    {
        try
        {
            return appealDL.get_AwardDetails(petObject);
           
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region function to get email ids to send email.

    public DataSet getEmailIdToSendAppealEmail(PetitionOB appealObject)
    {
        try
        {
            return appealDL.getEmailIdToSendAppealEmail(appealObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End


    #region Function to get FileName AppealAwardProunced

    public DataSet getFileNameAwardProunced(PetitionOB PetObject)
    {
        try
        {
            return appealDL.getFileNameAwardProunced(PetObject);
        }
        catch
        {
            throw;
        }
       

    }

    #endregion

    #region function to get email ids to send Appeal email

    public DataSet getEmailIdForSendingAwardAppealMail(PetitionOB petObject)
    {
        try
        {
            return appealDL.getEmailIdForSendingAwardAppealMail(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion
    #region function to get Mobile Number  to send Appeal Sms

    public DataSet getEmailIdForSendingAwardAppealSMS(PetitionOB petObject)
    {
        try
        {
            return appealDL.getEmailIdForSendingAwardAppealSMS(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

}
