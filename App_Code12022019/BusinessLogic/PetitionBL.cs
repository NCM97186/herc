using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public class PetitionBL
{
    //Area for all the constructors declaration

    #region Default constructor zone

    public PetitionBL()
    {

    }

    #endregion

    //ENd

    //Area for all the variables declaration

    #region Variables declaration zone

    PetitionDL petObjectDL = new PetitionDL();

    #endregion

    //End

    /// <summary>
    /// Area for all the functions definition for PETITION SECTION
    /// </summary>

    //Area for all display functions are here for petition

    #region Function to get PRO numbers(Conditional)

    public DataSet getPRO_Number(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getPRO_Number(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get PRO numbers in edit mode

    public DataSet getPRO_Number_In_EditMode(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getPRO_Number_In_EditMode(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Temp petition(Conditional)

    public DataSet get_Temp_Petition_Records(PetitionOB petObject, out int catValue)
    {
        try
        {
            return petObjectDL.get_Temp_Petition_Records(petObject, out  catValue);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get temp/final petition schedule of hearing(Conditional)

    public DataSet getPetition_ScheduleOfHearing(PetitionOB petObject, out int catValue)
    {
        try
        {
            return petObjectDL.getPetition_ScheduleOfHearing(petObject, out catValue);
        }
        catch
        {
            throw;
        }
    }

    #endregion 

    #region Function to get temp/final appeal schedule of hearing(Conditional)

    public DataSet getAppeal_ScheduleOfHearing(PetitionOB petObject, out int catValue)
    {
        try
        {
            return petObjectDL.getAppeal_ScheduleOfHearing(petObject, out catValue);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Temp petition(For Edit)

    public DataSet get_Temp_Petition_Records_Edit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_Temp_Petition_Records_Edit(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get petition id from temp table for comparision

    public DataSet get_ID_For_Compare(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_ID_For_Compare(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get petition id from temp_Review_Petition table

    public DataSet get_PetitionID_From_Temp_PetReview(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_PetitionID_From_Temp_PetReview(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Review and appeal number during deleting record from petition table

    public DataSet get_Review_Appeal_Number_for_Delete(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_Review_Appeal_Number_for_Delete(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get uploader id which contains files

    public bool getUploaderID(PetitionOB petitionObject)
    {
        try
        {
            return petObjectDL.getUploaderID(petitionObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion


    //End

    //Area for all the functions to insert

    #region Function to insert petition into temp

    public Int32 insertPetition(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.insertPetition(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to insert Connectedpetition

    public Int32 insertConnectedPetition(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.insertConnectedPetition(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to delete Connectedpetition

    public Int32 deleteConnectedPetition(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.deleteConnectedPetition(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get connected Petition(For Edit)

    public DataSet get_ConnectedPetition_Edit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_ConnectedPetition_Edit(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get connected Petition(For Edit)

    public DataSet getConnectedPetitionEditNew(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getConnectedPetitionEditNew(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get connected Petition(For Edit)

    public DataSet getConnectedReviewPetitionEditNew(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getConnectedReviewPetitionEditNew(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function To insert Into web_Petiton

    public Int32 ASP_Insert_Web_Petiton(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.ASP_Insert_Web_Petiton(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    //End

    //Area for all the functions to update

    #region Function to update the temp_Petition status

    public Int32 ASP_TempPetition_Update_Status_Id(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.ASP_TempPetition_Update_Status_Id(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to update petition status

    public Int32 petitionStatusUpdate(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.petitionStatusUpdate(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to update Reviewpetition status

    public Int32 ReviewpetitionStatusUpdate(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.ReviewpetitionStatusUpdate(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion


    #region Function to update Appealpetition status

    public Int32 AppealpetitionStatusUpdate(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.AppealpetitionStatusUpdate(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    //End

    //Area for all the functions to delete

    #region Functiont to delete petition either temp or final

    public Int32 Delete_Petition(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.Delete_Petition(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    //End

   
    //Area for all the user defined functions to display data

    #region Function to get RP numbers(Conditional) during insertion

    public DataSet getRP_Number(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getRP_Number(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get RP numbers in edit mode

    public DataSet getRP_Number_In_EditMode(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getRP_Number_In_EditMode(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Temp petition review(Conditional)

    public DataSet get_Temp_Review_Petition_Records(PetitionOB petObject, out int catValue)
    {
        try
        {
            return petObjectDL.get_Temp_Review_Petition_Records(petObject, out catValue);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Temp petition review(Edit)

    public DataSet get_Temp_Review_Petition_RecordsEdit(PetitionOB petObject)
    {
        try
        {

            return petObjectDL.get_Temp_Review_Petition_RecordsEdit(petObject);

        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get petition review id from temp table for comparision

    public DataSet get_ID_For_Review_Comparison(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_ID_For_Review_Comparison(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Appeal id from temp_Appeal_Petition table

    public DataSet get_RpID_From_Temp_PetAppeal(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_RpID_From_Temp_PetAppeal(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Appeal number during deleting record from review table

    public DataSet getAppeal_Number_for_Delete(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getAppeal_Number_for_Delete(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to get SOH information of Previous Year

    public DataSet Get_PreviousYearSOH(PetitionOB obj_PetOB, out int catValue)
    {

        try
        {
            return petObjectDL.Get_PreviousYearSOH(obj_PetOB, out catValue);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 

    #region function to get SOH information of Next Year

    public DataSet Get_NextYearSOH(PetitionOB obj_PetOB, out int catValue)
    {

        try
        {
            return petObjectDL.Get_NextYearSOH(obj_PetOB, out catValue);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to dateforcalenderevant For HERC commission calender

    public DataSet get_dateforcalenderevant(PetitionOB sohObject)
    {
        try
        {
            return petObjectDL.get_dateforcalenderevant(sohObject);
        }
        catch
        {
            throw;
        }
    }


    #endregion

    //End

    //Area for all the functions to insert

    #region Function to insert petition review in review petition data table

    public Int32 insert_Review_Petition(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.insert_Review_Petition(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function To insert Into web_Petiton_REVIEW

    public Int32 ASP_Insert_Web_Petiton_REVIEW(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.ASP_Insert_Web_Petiton_REVIEW(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    //Area for all the functions to update

    #region Function to update the temp_Review_Petition status

    public Int32 ASP_TempReview_Petition_Update_Status_Id(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.ASP_TempReview_Petition_Update_Status_Id(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    //End

    //Area for all the functions to delete

    #region Functiont to delete petition review either temp or final

    public Int32 Delete_Petition_Review(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.Delete_Petition_Review(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    //End


    /// <summary>
    /// Area for all the functions definition for PETITION APPEAL SECTION
    /// </summary>

    //Area for all the functions to display data

    #region Function to get Appeal numbers(Conditional) during insertion

    public DataSet getAppeal_Number(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getAppeal_Number(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get appeal numbers in edit mode

    public DataSet getAppeal_Number_In_EditMode(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getAppeal_Number_In_EditMode(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Temp petition appeal(Conditional)

    public DataSet get_Temp_Appeal_Petition_Records(PetitionOB petObject,out int catValue)
    {
        try
        {
            return petObjectDL.get_Temp_Appeal_Petition_Records(petObject,out catValue);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Temp petition appeal(Edit)

    public DataSet get_Temp_Appeal_Petition_RecordsEdit(PetitionOB petObject)
    {
        try
        {

            return petObjectDL.get_Temp_Appeal_Petition_RecordsEdit(petObject);


        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get petition appeal id from temp table for comparision

    public DataSet get_ID_For_Appeal_Comparison(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_ID_For_Appeal_Comparison(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    //End

    //Area for all the functions to insert

    #region Function to insert petition appeal in temp_Appeal petition data table

    public Int32 insert_Appeal_Petition(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.insert_Appeal_Petition(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function To insert Into web Petiton  APPEAL table

    public Int32 ASP_Insert_Web_Petiton_APPEAL(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.ASP_Insert_Web_Petiton_APPEAL(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    //End

    //Area for all the functions to update

    #region Function to update the temp Appeal Petition status

    public Int32 ASP_Temp_Appeal_Petition_Update_Status_Id(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.ASP_Temp_Appeal_Petition_Update_Status_Id(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    //End

    //Area for all the functions to delete

    #region Functiont to delete petition appeal either temp or final

    public Int32 Delete_Petition_Appeal(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.Delete_Petition_Appeal(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    //End

    /// <summary>
    /// Area for all the functions definition for SCHEDULE OF HEARING SECTION
    /// </summary>

    //Area for all the function to display records

    //New

    #region Function to get Petition numbers

    public DataSet getPetitionNumber(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getPetitionNumber(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End
    //New

    #region Function to get Petition numbers(NEW)

    public DataSet getPetitionNumberNew(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getPetitionNumberNew(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    #region Function to get Petition numbers for connections

    public DataSet getPetitionNumberForConnection(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getPetitionNumberForConnection(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Petition Review numbers

    public DataSet getPetitionReviewNumber(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getPetitionReviewNumber(petObject);
        }
        catch
        {
            throw;
        }


    }

    #endregion

    #region Function to get Petition Appeal numbers

    public DataSet getPetitionAppealNumber()
    {
        try
        {
            return petObjectDL.getPetitionAppealNumber();
        }
        catch
        {
            throw;
        }


    }

    #endregion

    #region function to bind year

    public DataSet Get_YearSOH(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.Get_YearSOH(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 

    #region function to bind year next

    public DataSet GetYearNextSOH(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.GetYearNextSOH(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion


    #region function to bind year for Ombudsman

    public DataSet Get_YearSOHForOmbudsman(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.Get_YearSOHForOmbudsman(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    //Area for all the functions to insert records into schedule of hearing table

    #region Function To insert Into temp Schedule of hearing table

    public Int32 InsertTmpScheduleOfHearing(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.InsertTmpScheduleOfHearing(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Temp schedule of hearing (For Edit)

    public DataSet getScheduleOfHearing_Edit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getScheduleOfHearing_Edit(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to update the temp_ScheduleOfHearing status

    public Int32 updateScheduleOfHearingStatus(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.updateScheduleOfHearingStatus(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function To insert record into web_ScheduleOfHearing

    public Int32 ApproveScheduleOfHearing(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.ApproveScheduleOfHearing(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get schedule of hearing id from temp table for comparision

    public DataSet scheduleOfHearingIDForComparison(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.scheduleOfHearingIDForComparison(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    //Area for all the functions to delete records into schedule of hearing table

    #region Function to Delete the data from tables for schedule of hearing

    public Int32 Delete_ScheduleOfHearing(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.Delete_ScheduleOfHearing(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    //End


    //Front End 

    #region function to get Petition information

    public DataSet Get_Petition(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            return petObjectDL.Get_Petition(obj_petOB,out catValue);
        }
        catch
        {
            throw;
        }

    }
    #endregion

    #region function to get year

    public DataSet Get_Year()
    {
        try
        {
            return petObjectDL.Get_Year();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 

    #region function to get Petition information of Previous Year

    public DataSet Get_Petition_PrevYear(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            return petObjectDL.Get_Petition_PrevYear(obj_petOB,out catValue);
           
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 

    #region Function to get Temp petition(For Details)

    public DataSet get_Petition_Details(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_Petition_Details(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to  petition review

    public DataSet get_ReviewPetition_Details(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_ReviewPetition_Details(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to get online status information
    public DataSet Get_OnlineStatus(PetitionOB obj_petOB)
    {

        try
        {
            return petObjectDL.Get_OnlineStatus(obj_petOB);
        }
        catch
        {
            throw;
        }
       
    }
    #endregion 

    #region function to get  top 2 Schedule of Hearings details

    public DataSet Get_SOH(PetitionOB objPetOB)
    {
        try
        {
            return petObjectDL.Get_SOH(objPetOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 

    #region function to get  current Schedule of Hearings details

    public DataSet Get_CurrentSOH(PetitionOB objPetOB, out int catValue)
    {
        try
        {
            return petObjectDL.Get_CurrentSOH(objPetOB, out  catValue);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 

    #region function to get PetionSearch information
    public DataSet Get_PetionSearch(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            return petObjectDL.Get_PetionSearch(obj_petOB, out catValue);
        }
        catch
        {
            throw;
        }

    }
    #endregion

    #region function to get Notifications Details
    public DataSet Get_Notifications(PetitionOB obj_petOB, out int catValue)
    {
        try
        {

            return petObjectDL.Get_Notifications(obj_petOB, out  catValue);
        }
        catch
        {
            throw;
        }

    }

    #endregion 

    #region function to insert the status in Mst_Status table

    public Int32 Insert_Status(PetitionOB objpetOB, out int id)
    {
        try
        {
            return petObjectDL.Insert_Status(objpetOB,out id);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 

    #region function to get  Schedule of Hearings details by calander

    public DataSet Get_SOH_By_calander(PetitionOB objPetOB, out int catValue)
    {
        try
        {
            return petObjectDL.Get_SOH_By_calander(objPetOB, out  catValue);
        }
        catch
        {
            throw;
        }

    }

    #endregion 

    #region Function to get connected order of petition

    public DataSet getConnectedOrders(PetitionOB objpetOB)
    {
        try
        {
            return petObjectDL.getConnectedOrders(objpetOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function to get connected order of Reviewpetition

    public DataSet getConnectedOrdersForReview(PetitionOB objpetOB)
    {
        try
        {
            return petObjectDL.getConnectedOrdersForReview(objpetOB);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region Function to get order details for review petition

    public DataSet getOrdersForReview(PetitionOB objpetOB)
    {
        try
        {
            return petObjectDL.getOrdersForReview(objpetOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get connected SOH of petition

    public DataSet getConnectedSOH(PetitionOB objpetOB)
    {
        try
        {
            return petObjectDL.getConnectedSOH(objpetOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Public Notice of petition

    public DataSet getConnectedPublicNotice(PetitionOB objpetOB)
    {
        try
        {
            return petObjectDL.getConnectedPublicNotice(objpetOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to get year of Petition Admin

    public DataSet GetYearPetition_Admin()
    {
        try
        {
            return petObjectDL.GetYearPetition_Admin();
        }
        catch
        {
            throw;
        }
        
    }
    #endregion 

    #region function to get year of Review Petition Admin

    public DataSet GetYearReviewPetition_Admin()
    {
        try
        {
            return petObjectDL.GetYearReviewPetition_Admin();
        }
        catch
        {
            throw;
        }
        
    }
    #endregion 

    #region function to get year of Petition Appeal Admin

    public DataSet GetYearPetitionAppeal_Admin()
    {
        try
        {
            return petObjectDL.GetYearPetitionAppeal_Admin();
        }
        catch
        {
            throw;
        }
        
    }
    #endregion 
    
    #region Function to get times

    public DataSet getTime()
    {
        try
        {
            return petObjectDL.getTime();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get hours

    public DataSet getHours()
    {
        try
        {
            return petObjectDL.getHours();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get ampm

    public DataSet getampm()
    {
        try
        {
            return petObjectDL.getampm();
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region Function to get RP from temp_Review_Petition table

    public DataSet get_RP_From_Temp_PetReview(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_RP_From_Temp_PetReview(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion



    #region Function to  petition review

    public DataSet get_AppealPetition_Details(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_AppealPetition_Details(petObject);
        }
        catch
        {
            throw;
        }
     
    }

    #endregion

    //End


    #region get current Petition review

    public DataSet Get_CurrentPetitionReview(PetitionOB pt_obj, out int catValue)
    {
        try
        {
            return petObjectDL.Get_CurrentPetitionReview(pt_obj, out catValue);
        }
        catch
        {
            throw;
        }
       

    }
    #endregion

    #region get current Petition Appeal

    public DataSet Get_CurrentPetitionAppeal(PetitionOB pt_obj, out int catValue)
    {
        try
        {

            return petObjectDL.Get_CurrentPetitionAppeal(pt_obj, out catValue);
        }
        catch
        {
            throw;
        }
       

    }
    #endregion


    #region function to get year of Petition review for previous

    public DataSet GetYearPetitionReviewPrevious()
    {
        try
        {

            return petObjectDL.GetYearPetitionReviewPrevious();
        }
        catch
        {
            throw;
        }
        
    }


    #endregion

    #region function to get year of Petition Appeal for previous

    public DataSet GetYearPetitionAppealPrevious()
    {
        try
        {

            return petObjectDL.GetYearPetitionAppealPrevious();
        }
        catch
        {
            throw;
        }
        
    }


    #endregion

    #region Function to get connected petition

    public DataSet get_ConnectedPetition(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_ConnectedPetition(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get connected Reviewpetition

    public DataSet get_ConnectedReviewPetition(PetitionOB petObject)
    {
        try
        {

            return petObjectDL.get_ConnectedReviewPetition(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to update the  Petition status for delete

    public Int32 updatePetitionStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            return petObjectDL.updatePetitionStatusDelete(rtiObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region function to get year of ScheduleOfHearing Admin

    public DataSet GetYearScheduleOfHearing_Admin(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.GetYearScheduleOfHearing_Admin(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to get year of ScheduleOfHearing for Appeal Admin

    public DataSet GetYearScheduleOfHearingforAppeal_Admin()
    {
        try
        {

            return petObjectDL.GetYearScheduleOfHearingforAppeal_Admin();
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region Function to insert ConnectedPetition

    public Int32 insertConnectedPetitionFiles(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.insertConnectedPetitionFiles(petObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion


    #region Function to insert ConnectedAppeal

    public Int32 insertConnectedAppealFiles(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.insertConnectedAppealFiles(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion


    #region Function to get FileName for petition

    public DataSet getFileNameForPetition(PetitionOB orderObject)
    {
        try
        {

            return petObjectDL.getFileNameForPetition(orderObject);
        }
        catch
        {
            throw;
        }
       

    }

    #endregion


    #region Function to get FileName for Appeal petition

    public DataSet getFileNameForAppealpetition(PetitionOB orderObject)
    {
        try
        {
            return petObjectDL.getFileNameForAppealpetition(orderObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion


    #region Function to get FileName for Review petition

    public DataSet getFileNameForReviewPetition(PetitionOB orderObject)
    {
        try
        {

            return petObjectDL.getFileNameForReviewPetition(orderObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to Update status for Files in Petitions

    public Int32 UpdateFileStatusForPetitions(PetitionOB orderObject)
    {
        try
        {
            return petObjectDL.UpdateFileStatusForPetitions(orderObject);
        }
        catch
        {
            throw;
        }
       

    }

    #endregion

    #region Function to Update status for Files in AppealPettion

    public Int32 UpdateFileStatusForAppealPettion(PetitionOB orderObject)
    {
        try
        {
            return petObjectDL.UpdateFileStatusForAppealPettion(orderObject);
        }
        catch
        {
            throw;
        }
       

    }

    #endregion

    #region Function to Update status for Files in Petitions

    public Int32 UpdateFileStatusForReviewPetitions(PetitionOB orderObject)
    {
        try
        {
            return petObjectDL.UpdateFileStatusForReviewPetitions(orderObject);
        }
        catch
        {
            throw;
        }
        

    }

    #endregion

    #region Function to get connected petition file name

    public DataSet getPetitionFileNames(PetitionOB objpetOB)
    {
        try
        {
            return petObjectDL.getPetitionFileNames(objpetOB);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get connected Appeal file name

    public DataSet getAppealFileNames(PetitionOB objpetOB)
    {
        try
        {
            return petObjectDL.getAppealFileNames(objpetOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get connected petition file name

    public DataSet getReviewPetitionFileNames(PetitionOB objpetOB)
    {
        try
        {
            return petObjectDL.getFileNameForReviewPetition(objpetOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to insert ConnectedReviewPetition

    public Int32 insertConnectedReviewPetitionFiles(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.insertConnectedReviewPetitionFiles(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to  petition review for Popup

    public DataSet get_ReviewPetitionForPopup(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_ReviewPetitionForPopup(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to update Soh for delete

    public Int32 SOH_Changestatus_Delete(PetitionOB rtiObject)
    {
        try
        {
            return petObjectDL.SOH_Changestatus_Delete(rtiObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region Function to get Petition Review numbers for connections

    public DataSet getPetitionReviewNumberForConnection(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getPetitionReviewNumberForConnection(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get year of Petition Review Admin

    public DataSet GetYearPetitionReview_Admin()
    {
        try
        {
            return petObjectDL.GetYearPetitionReview_Admin();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to insert Connected petition review

    public Int32 insertConnectedPetitionReview(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.insertConnectedPetitionReview(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get connected Petition Review(For Edit)

    public DataSet get_ConnectedPetitionReview_Edit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_ConnectedPetitionReview_Edit(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to delete Connected petition review

    public Int32 deleteConnectedPetitionReview(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.deleteConnectedPetitionReview(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion
    
    #region Function to get connected Petition

    public DataSet getConnectedPetition(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getConnectedPetition(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get connected ReviewPetition

    public DataSet getConnectedReviewPetition(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getConnectedReviewPetition(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion
    
    #region Function to get connected petition

    public DataSet getConnectedForPetition(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getConnectedForPetition(petObject);
           
       
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get connected SOH of Reviewpetition

    public DataSet getReviewConnectedSOH(PetitionOB objpetOB)
    {
        try
        {
            return petObjectDL.getReviewConnectedSOH(objpetOB);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion
    
    #region Function to insert SOHWithPetition

    public Int32 insertSoHwithPetition(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.insertSoHwithPetition(petObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion
    
    #region Function to get connected SOH for Petition(For Edit)

    public DataSet get_ConnectedSOH_Edit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_ConnectedSOH_Edit(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get connected SOH for Petition(For EditMode)

    public DataSet get_ConnectedSOHEdit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_ConnectedSOHEdit(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to delete Connectedpetition from SOH

    public Int32 deleteConnectedPetitionFromSOH(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.deleteConnectedPetitionFromSOH(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to get year of Petition Admin

    public DataSet GetYearPetition_AddEdit()
    {
        try
        {
            return petObjectDL.GetYearPetition_AddEdit();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get year of Petition Admin

    public DataSet GetYearPetitionReview_AddEdit()
    {
        try
        {
            return petObjectDL.GetYearPetitionReview_AddEdit();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to update the  ReviewPetition status for delete

    public Int32 updateReviewPetitionStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            return petObjectDL.updateReviewPetitionStatusDelete(rtiObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion
    
    #region Function to update the  updateAppealPetitionStatusDelete  status for delete

    public Int32 updateAppealPetitionStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            return petObjectDL.updateAppealPetitionStatusDelete(rtiObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get Petition numbers for connections

    public DataSet getPetitionNumberForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getPetitionNumberForConnectionEdit(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Petition Review numbers for connections

    public DataSet getPetitionReviewNumberForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getPetitionReviewNumberForConnectionEdit(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get  current Schedule of Hearings details for Review Petition

    public DataSet Get_CurrentSOHReviewPetition(PetitionOB objPetOB, out int catValue)
    {
        try
        {
            return petObjectDL.Get_CurrentSOHReviewPetition(objPetOB, out catValue);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get SOH information of Previous Year

    public DataSet Get_PreviousYearSOHReviewPetition(PetitionOB obj_PetOB, out int catValue)
    {

        try
        {
            return petObjectDL.Get_PreviousYearSOHReviewPetition(obj_PetOB, out catValue);
        }
        catch
        {
            throw;
        }
    }
    #endregion

    //Ombudsman Schedule of hearing functions

    #region function to get  current Schedule of Hearings for Ombudsman

    public DataSet GetCurrentSOHForOmbudsman(PetitionOB objPetOB, out int catValue)
    {
        try
        {
            return petObjectDL.GetCurrentSOHForOmbudsman(objPetOB, out catValue);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get SOH information of Previous Year for Ombudsman

    public DataSet GetPreviousYearSOHForOmbudsman(PetitionOB obj_PetOB, out int catValue)
    {

        try
        {
            return petObjectDL.GetPreviousYearSOHForOmbudsman(obj_PetOB, out catValue);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get  Schedule of Hearings details by calander

    public DataSet GetSOHcalanderforOmbudsman(PetitionOB objPetOB, out int catValue)
    {
        try
        {
            return petObjectDL.GetSOHcalanderforOmbudsman(objPetOB, out catValue);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    #region function to get SOH Search information
    public DataSet Get_SOHSearch(PetitionOB obj_petOB, out int catValue)
    {

        try
        {

            return petObjectDL.Get_SOHSearch(obj_petOB,out  catValue);
        }
        catch
        {
            throw;
        }
        
    }
    #endregion

    #region function to get Ombudsman SOH Search information
    public DataSet Get_OmbudsmanSOHSearch(PetitionOB obj_petOB, out int catValue)
    {

        try
        {

            return petObjectDL.Get_OmbudsmanSOHSearch(obj_petOB,out catValue);
        }
        catch
        {
            throw;
        }
      
    }
    #endregion

    #region Function to insert Connected Public notice files

    public Int32 insertConnectedSohFiles(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.insertConnectedSohFiles(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get FileName for schedule of hearing

    public DataSet getFileNameForSoh(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getFileNameForSoh(petObject);
        }
        catch
        {
            throw;
        }
      

    }

    #endregion

    #region Function to Update status for Files in Soh

    public Int32 UpdateFileStatusForSoh(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.UpdateFileStatusForSoh(petObject);
           
        }
        catch
        {
            throw;
        }
       

    }

    #endregion

    #region Function to get temp/final schedule of hearing(Conditional)

    public DataSet get_ScheduleOfHearingDetails(PetitionOB petObject)
    {
        try
        {

            return petObjectDL.get_ScheduleOfHearingDetails(petObject);

        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get connected SOH file name

    public DataSet getSohFileNames(PetitionOB objpetOB)
    {
        try
        {
            return petObjectDL.getSohFileNames(objpetOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to get  current Schedule of Hearings for Ombudsman details

    public DataSet GetCurrentSOHForOmbudsmanDetails(PetitionOB objPetOB)
    {
        try
        {
            return petObjectDL.GetCurrentSOHForOmbudsmanDetails(objPetOB);

        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region get current Petition Appeal Details

    public DataSet Get_CurrentPetitionAppealDetails(PetitionOB pt_obj)
    {
        try
        {

            return petObjectDL.Get_CurrentPetitionAppealDetails(pt_obj);

        }
        catch
        {
            throw;
        }
       

    }
    #endregion

    #region get Petition and review petition number based on review petition id

    public DataSet GetPetitionReviewPetitionNumbers(PetitionOB pt_obj)
    {
        try
        {

            return petObjectDL.GetPetitionReviewPetitionNumbers(pt_obj);

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get year of Petition Front side

    public DataSet GetYearPetitionSearch(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.GetYearPetitionSearch(petObject); 
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to bind year for search

    public DataSet Get_YearSOHSearch(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.Get_YearSOHSearch(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region function to bind year for search

    public DataSet Get_YearSOHSearchHerc(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.Get_YearSOHSearchHerc(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to bind Appeal number for search

    public DataSet Get_AppealNumberSOHSearch(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.Get_AppealNumberSOHSearch(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion
    
    #region function to bind Petition number for search

    public DataSet Get_PetitionNumberSOHSearch(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.Get_PetitionNumberSOHSearch(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get Petition and review petition numbers for connections

    public DataSet getPetition_ReviewNumbers(PetitionOB petObject)
    {
        try
        {

            return petObjectDL.getPetition_ReviewNumbers(petObject);
        }
        catch
        {
            throw;
        }
        

    }

    #endregion
    
    #region function to get year of Petition OnlineStatus Front side

    public DataSet GetYearPetitionOnlineStatusSearch()
    {
        try
        {

            return petObjectDL.GetYearPetitionOnlineStatusSearch();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get current Year new Petition

    public DataSet GetLatestPetition(PetitionOB obj_petOB, out int catValue)
    {
        try
        {

            return petObjectDL.GetLatestPetition(obj_petOB,out catValue);

        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get Petition year for connections

    public DataSet getPetitionYearForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getPetitionYearForConnectionEdit(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Review Petition year for connections

    public DataSet getReviewPetitionYearForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getReviewPetitionYearForConnectionEdit(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get Schedule Of Hearing year for connections

    public DataSet getScheduleOfHearingYearForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getScheduleOfHearingYearForConnectionEdit(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to insert ConnectedMultiplePetitionReview

    public Int32 insertConnectedMultiplePetitionReview(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.insertConnectedMultiplePetitionReview(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion
    
    #region Function to get connected Petition(For Edit)

    public DataSet get_ConnectedPetitionMultiple_Edit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.get_ConnectedPetitionMultiple_Edit(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get Petition year for connections

    public DataSet Connected_multiplePetitionReviewForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.Connected_multiplePetitionReviewForConnectionEdit(petObject);
        }
        catch
        {
            throw;
        }
        

    }

    #endregion

    #region Function to get connected PetitionReview(For Edit)

    public DataSet getConnectedPetitionReviewEditNew(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getConnectedPetitionReviewEditNew(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion
    
    #region Function to get PetitionReview numbers for connections

    public DataSet getPetitionReviewForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getPetitionReviewForConnectionEdit(petObject);
        }
        catch
        {
            throw;
        }
        

    }

    #endregion
    
    #region Function to delete ConnectedpetitionReview

    public Int32 deleteConnectedPetitionReviewMultiple(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.deleteConnectedPetitionReviewMultiple(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get connected petition review

    public DataSet get_ConnectedPetitionReview(PetitionOB petObject)
    {
        try
        {

            return petObjectDL.get_ConnectedPetitionReview(petObject);

        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get connected petition review Details

    public DataSet get_ConnectedPetitionReviewDetails(PetitionOB petObject)
    {
        try
        {

            return petObjectDL.get_ConnectedPetitionReviewDetails(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    //Code to get email ids for sending emails

    #region function to get email ids to send email.

    public DataSet getEmailIdForSendingMail(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getEmailIdForSendingMail(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get email ids to send email.

    public DataSet getEmailIdForSendingMailForReviewPetition(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getEmailIdForSendingMailForReviewPetition(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total pending petitions

    public string countPetitionPending()
    {
        try
        {

            return petObjectDL.countPetitionPending();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to count to pending petition

    public DataSet countPetitionPendingAll()
    {
        try
        {

            return petObjectDL.countPetitionPendingAll();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total pending annaul report

    public string countAnnualReportPending()
    {
        try
        {
            return petObjectDL.countAnnualReportPending();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region function to count total review annaul report

    public string countAnnualReportReview()
    {
        try
        {
            return petObjectDL.countAnnualReportReview();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region function to count total approval annaul report

    public string countAnnualReportApproval()
    {
        try
        {
            return petObjectDL.countAnnualReportApproval();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to count total pending vacancy

    public string countVacancyPending()
    {
        try
        {
            return petObjectDL.countVacancyPending();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total review vacancy

    public string countVacancyReview()
    {
        try
        {
            return petObjectDL.countVacancyReview();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total approval vacancy

    public string countVacancyApproval()
    {
        try
        {
            return petObjectDL.countVacancyApproval();
        }
        catch
        {
            throw;
        }

    }

    #endregion
    
    #region function to count total pending disscussion paper

    public string countDisscussionPaperPending()
    {
        try
        {
            return petObjectDL.countDisscussionPaperPending();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total review disscussion paper

    public string countDisscussionPaperReview()
    {
        try
        {
            return petObjectDL.countDisscussionPaperReview();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total approval disscussion paper

    public string countDisscussionPaperApproval()
    {
        try
        {
            return petObjectDL.countDisscussionPaperApproval();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total pending report

    public string countReportPending()
    {
        try
        {
            return petObjectDL.countReportPending();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total review report

    public string countReportReview()
    {
        try
        {
            return petObjectDL.countReportReview();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total approval report

    public string countReportApproval()
    {
        try
        {
            return petObjectDL.countReportApproval();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total pending Modules

    public string countModulesPending()
    {
        try
        {
            return petObjectDL.countModulesPending();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total pending Modules

    public DataSet countModulesApprovalAll()
    {
        try
        {
            return petObjectDL.countModulesApprovalAll();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region function to count total pending Modules

    public DataSet countModulesReviewAll()
    {
        try
        {
            return petObjectDL.countModulesReviewAll();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region function to count total pending Modules

    public DataSet countModulesPendingAll()
    {
        try
        {
            return petObjectDL.countModulesPendingAll();
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region function to count total review Modules

    public string countModulesReview()
    {
        try
        {
            return petObjectDL.countModulesReview();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total approval Modules

    public string countModulesApproval()
    {
        try
        {
            return petObjectDL.countModulesApproval();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total pending Profile

    public string countProfilePending()
    {
        try
        {
            return petObjectDL.countProfilePending();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total review Profile

    public string countProfileReview()
    {
        try
        {
            return petObjectDL.countProfileReview();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total approval Profile

    public string countProfileApproval()
    {
        try
        {
            return petObjectDL.countProfileApproval();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total pending Content

    public string countContentPending()
    {
        try
        {
            return petObjectDL.countContentPending();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total pending contents

    public DataSet countContentPendingAll()
    {
        try
        {
            return petObjectDL.countContentPendingAll();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total review Content

    public string countContentReview()
    {
        try
        {
            return petObjectDL.countContentReview();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total review contents

    public DataSet countContentReviewAll()
    {
        try
        {
            return petObjectDL.countContentReviewAll();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to count total approval Content

    public string countContentApproval()
    {
        try
        {
            return petObjectDL.countContentApproval();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total approval contents

    public DataSet countContentApprovalAll()
    {
        try
        {
            return petObjectDL.countContentApprovalAll();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region function to count total pending RTI

    public string countRTIPending()
    {
        try
        {
            return petObjectDL.countRTIPending();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total pending RTI

    public DataSet countRTIPendingAll()
    {
        try
        {
            return petObjectDL.countRTIPendingAll();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total pending Tariff modules

    public DataSet countTariffPendingAll()
    {
        try
        {
            return petObjectDL.countTariffPendingAll();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to count total review RTI

    public string countRTIReview()
    {
        try
        {
            return petObjectDL.countRTIReview();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total review RTI

    public DataSet countRTIReviewAll()
    {
        try
        {
            return petObjectDL.countRTIReviewAll();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total review Tariff modules

    public DataSet countTariffReviewAll()
    {
        try
        {
            return petObjectDL.countTariffReviewAll();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to count total approval Tariff modules

    public DataSet countTariffApprovalAll()
    {
        try
        {
            return petObjectDL.countTariffApprovalAll();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion


    #region function to count total approval RTI

    public string countRTIApproval()
    {
        try
        {
            return petObjectDL.countRTIApproval();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total approval RTI

    public DataSet countRTIApprovalAll()
    {
        try
        {
            return petObjectDL.countRTIApprovalAll();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total pending Appeal

    public string countAppealPending()
    {
        try
        {
            return petObjectDL.countAppealPending();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total pending appeal

    public DataSet countAppealPendingAll()
    {
        try
        {
            return petObjectDL.countAppealPendingAll();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to count total Review appeal

    public DataSet countAppealReviewAll()
    {
        try
        {
            return petObjectDL.countAppealReviewAll();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total Review appeal

    public DataSet countAppealApprovalAll()
    {
        try
        {
            return petObjectDL.countAppealApprovalAll();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to count total review Appeal

    public string countAppealReview()
    {
        try
        {
            return petObjectDL.countAppealReview();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total approval Appeal

    public string countAppealApproval()
    {
        try
        {
            return petObjectDL.countAppealApproval();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total pending Appeal

    public string countAwardPronouncedPending()
    {
        try
        {
            return petObjectDL.countAwardPronouncedPending();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total pending AwardPronounced all

    public DataSet countAwardPronouncedPendingAll()
    {
        try
        {
            return petObjectDL.countAwardPronouncedPendingAll();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region function to count total pending AwardPronounced all

    public DataSet countAwardPronouncedReviewAll()
    {
        try
        {
            return petObjectDL.countAwardPronouncedReviewAll();
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region function to count total pending AwardPronounced all

    public DataSet countAwardPronouncedApproveAll()
    {
        try
        {
            return petObjectDL.countAwardPronouncedApproveAll();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total review AwardPronounced

    public string countAwardPronouncedReview()
    {
        try
        {
            return petObjectDL.countAwardPronouncedReview();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total approval AwardPronounced

    public string countAwardPronouncedApproval()
    {
        try
        {
            return petObjectDL.countAwardPronouncedApproval();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total pending Banner

    public string countBannerPending()
    {
        try
        {
            return petObjectDL.countBannerPending();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total review AwardPronounced

    public string countBannerReview()
    {
        try
        {
            return petObjectDL.countBannerReview();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total approval Banner

    public string countBannerApproval()
    {
        try
        {
            return petObjectDL.countBannerApproval();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total review petitions

    public string countPetitionReview()
    {
        try
        {

            return petObjectDL.countPetitionReview();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to count total review for all petitions

    public DataSet countPetitionReviewall()
    {
        try
        {

            return petObjectDL.countPetitionReviewall();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to count total approval petitions

    public string countPetitionApproval()
    {
        try
        {

            return petObjectDL.countPetitionApproval();
        
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total approval petitions

    public DataSet countPetitionApprovalAll()
    {
        try
        {

            return petObjectDL.countPetitionApprovalAll();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to count total approval publicNotice

    public string countPublicNoticeApproval()
    {
        try
        {
            return petObjectDL.countPublicNoticeApproval();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to count total pending publicNotice

    public string countPublicNoticePending()
    {
        try
        {
            return petObjectDL.countPublicNoticePending();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total review publicNotice

    public string countPublicNoticeReview()
    {
        try
        {
            return petObjectDL.countPublicNoticeReview();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total pending Schedule of Hearing

    public string countScheduleOfHearingPending()
    {
        try
        {
            return petObjectDL.countScheduleOfHearingPending();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total pending Schedule of Hearing all

    public DataSet countScheduleOfHearingPendingall()
    {
        try
        {
            return petObjectDL.countScheduleOfHearingPendingall();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total pending orders all

    public DataSet countOrdersPendingall()
    {
        try
        {
            return petObjectDL.countOrdersPendingall();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total review orders all

    public DataSet countOrdersReviewall()
    {
        try
        {
            return petObjectDL.countOrdersReviewall();
        }
        catch
        {
            throw;
        }
    }

    #endregion
    
    #region function to count total appraval orders all

    public DataSet countOrdersApprovalall()
    {
        try
        {

            return petObjectDL.countOrdersApprovalall();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to count total pending public notices

    public DataSet countPublicNoticePendingall()
    {
        try
        {
            return petObjectDL.countPublicNoticePendingall();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total review public notices

    public DataSet countPublicNoticeReviewall()
    {
        try
        {
            return petObjectDL.countPublicNoticeReviewall();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total approval public notices

    public DataSet countPublicNoticeApprovalall()
    {
        try
        {
            return petObjectDL.countPublicNoticeApprovalall();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to count total review Schedule of Hearing

    public string countScheduleOfHearingReview()
    {
        try
        {
            return petObjectDL.countScheduleOfHearingReview();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total review Schedule of Hearing

    public DataSet countScheduleOfHearingReviewAll()
    {
        try
        {

            return petObjectDL.countScheduleOfHearingReviewAll();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total approval Schedule of Hearing

    public string countScheduleOfHearingApproval()
    {
        try
        {
            return petObjectDL.countScheduleOfHearingApproval();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total approval Schedule of Hearing

    public DataSet countScheduleOfHearingApprovalAll()
    {
        try
        {
            return petObjectDL.countScheduleOfHearingApprovalAll();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total pending orders

    public string countOrdersPending()
    {
        try
        {
            return petObjectDL.countOrdersPending();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total review orders

    public string countOrdersReview()
    {
        try
        {
            return petObjectDL.countOrdersReview();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total approval orders

    public string countOrdersApproval()
    {
        try
        {
            return petObjectDL.countOrdersApproval();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total pending notifications

    public string countNotificationPending()
    {
        try
        {
            return petObjectDL.countNotificationPending();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total review notifications

    public string countNotificationReview()
    {
        try
        {
            return petObjectDL.countNotificationReview();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total approval notifications

    public string countNotificationApproval()
    {
        try
        {
            return petObjectDL.countNotificationApproval();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion
    
    #region function to count total pending tariff

    public string countTariffPending()
    {
        try
        {
            return petObjectDL.countTariffPending();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total review tariff

    public string countTariffReview()
    {
        try
        {
            return petObjectDL.countTariffReview();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to count total approval tariff

    public string countTariffApproval()
    {
        try
        {
            return petObjectDL.countTariffApproval();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get order details for Appeal petition

    public DataSet getOrdersForAppeal(PetitionOB objpetOB)
    {
        try
        {
            return petObjectDL.getOrdersForAppeal(objpetOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to get email ids to send schedule of hearing email.

    public DataSet getEmailIdForSendingSohMail(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getEmailIdForSendingSohMail(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get email ids to send schedule of hearing email for ombudsman.

    public DataSet getEmailIdForSendingSohOmbudsmanMail(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getEmailIdForSendingSohOmbudsmanMail(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get Mobile numbers to send schedule of hearing sms for herc.

    public DataSet getMobileNumberForSendingSohSms(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getMobileNumberForSendingSohSms(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get mobile numbers to send schedule of hearing sms for ombudsman.

    public DataSet getMobileNumberForSendingSohOmbudsmanSMS(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.getMobileNumberForSendingSohOmbudsmanSMS(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to insert message in database for mobile sms testing

    public Int32 insertMobileSMS(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.insertMobileSMS(petObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region function to count total pending profiles

    public DataSet countProfilesPendingAll()
    {
        try
        {
            return petObjectDL.countProfilesPendingAll();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to count total review profiles

    public DataSet countProfilesReviewAll()
    {
        try
        {
            return petObjectDL.countProfilesReviewAll();
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region function to count total approval profiles

    public DataSet countProfilesApprovalAll()
    {
        try
        {
            return petObjectDL.countProfilesApprovalAll();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

	#region function to count total pending Banner

    public string countwhatsnewPending()
    {
        try
        {
            return petObjectDL.countwhatsnewPending();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total review whats new

    public string countwhatsnewReview()
    {
        try
        {
            return petObjectDL.countwhatsnewReview();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to count total for approval whats new

    public string countwhatsnewApproval()
    {
        try
        {
            return petObjectDL.countwhatsnewApproval();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get audit details

    public DataSet AuditTrailRecords(PetitionOB petObject)
    {
        try
        {
            return petObjectDL.AuditTrailRecords(petObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    public DataSet getPetitionURLwithFile(PetitionOB petitionObject)
    {
        try
        {
            return petObjectDL.getPetitionURLwithFile(petitionObject);
        }
        catch
        {
            throw;
        }
    }
    public DataSet getScheduleOfHearingURLwithFile(PetitionOB soh)
    {
        try
        {
            return petObjectDL.getScheduleOfHearingURLwithFile(soh);
        }
        catch
        {
            throw;
        }
       
    }

    public DataSet getPetitionURLwithFileReview(PetitionOB petitionObject)
    {
        try
        {
            return petObjectDL.getPetitionURLwithFileReview(petitionObject);
        }
        catch
        {
            throw;
        }
    }
    //End
}
