using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;



public class RtiBL
{
    #region Default constructors declaration zone

    public RtiBL()
    {

    }

    #endregion

    //Area for all the variables declaration zone

    #region Data declaration zone

    RtiDL rtiDL = new RtiDL();

    #endregion

    //End

    //Area for all the functions to insert, update and delete

    #region Function to insert RTI records into temp table

    public int insertUpdateTempRTI(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.insertUpdateTempRTI(rtiObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to insert/update RTI_FAA records into temp table

    public int insertUpdateTempRTIFAA(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.insertUpdateTempRTIFAA(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function To insert rti into web final table

    public int InsertRtiIntoWeb(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.InsertRtiIntoWeb(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to update rti status

    public int RtiStatusUpdate(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.RtiStatusUpdate(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    //Area for all the user defined functions to display records

    #region Function to get rti reference numbers(Conditional)

    public DataSet getReferenceNumber(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getReferenceNumber(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region Function to get rti reference numbers(Conditional) for edit

    public DataSet getReferenceNumberEdit(PetitionOB rtiObject)
    {
        try
        {

            return rtiDL.getReferenceNumberEdit(rtiObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get temporary rti records(Conditional)

    public DataSet getTempRTIRecords(PetitionOB rtiObject, out int catValue)
    {
        try
        {
            return rtiDL.getTempRTIRecords(rtiObject, out catValue);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Temp rti record for edit

    public DataSet getRtiRecordForEdit(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getRtiRecordForEdit(rtiObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to update the temporary rti status

    public int updateRtiStatus(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.updateRtiStatus(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to update the temporary rti-faa status

    public int updateRtiFAAStatus(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.updateRtiFAAStatus(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to update the rti status

    public int modifyRtiStatus(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.modifyRtiStatus(rtiObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get rti id from rti table

    public DataSet GetRtiid(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.GetRtiid(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get RTI id from temp table for comparision

    public DataSet get_ID_For_Compare(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.get_ID_For_Compare(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get rti status

    public DataSet getRtiStatus()
    {
        try
        {
            return rtiDL.getRtiStatus();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get rti id from temp_Rti_FAA table

    public DataSet getRtiIDFromTempFinalRti(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getRtiIDFromTempFinalRti(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Temp rti first appellate authority records

    public DataSet getRtiFaaTempRecords(PetitionOB rtiObject, out int catValue)
    {
        try
        {
            return rtiDL.getRtiFaaTempRecords(rtiObject, out catValue);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get RTI_FAA_Id from temp table for comparision

    public DataSet getIdForrtiFAA_Comparison(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getIdForrtiFAA_Comparison(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Temp rti-faa record for edit

    public DataSet getRtiFAARecordForEdit(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getRtiFAARecordForEdit(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function To insert rti-faa into web final table

    public int InsertRtiFAAIntoWeb(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.InsertRtiFAAIntoWeb(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to update the rti-FAA status

    public int modifyRtiFAAStatus(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.modifyRtiFAAStatus(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get rtifaa id from temp_Rti_SAA table

    public DataSet getRtiFAAIDFromTempFinalRti(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getRtiFAAIDFromTempFinalRti(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to insert/update RTI_SAA records into temp table

    public int insertUpdateTempRTISAA(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.insertUpdateTempRTISAA(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Temp rti second appellate authority records

    public DataSet getRtiSaaTempRecords(PetitionOB rtiObject, out int catValue)
    {
        try
        {
            return rtiDL.getRtiSaaTempRecords(rtiObject, out catValue);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to update the temporary rti-saa status

    public int updateRtiSAAStatus(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.updateRtiSAAStatus(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function To insert rti-saa into web final table

    public int InsertRtiSAAIntoWeb(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.InsertRtiSAAIntoWeb(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get RTI_SAA_Id from temp table for comparision

    public DataSet getIdForrtiSAA_Comparison(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getIdForrtiSAA_Comparison(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Temp rti-saa record for edit

    public DataSet getRtiSAARecordForEdit(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getRtiSAARecordForEdit(rtiObject);
        }
        catch
        {
            throw;
        }
    }


    #endregion

    #region Function to get rti status FOF SAA

    public DataSet getRtiStatus_SAA()
    {
        try
        {
            return rtiDL.getRtiStatus_SAA();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to update the rti-SAA status

    public int modifyRtiSAAStatus(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.modifyRtiSAAStatus(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    //All the functions are for deleting records

    #region Function to get rti id during deleting record from table

    public DataSet getRtiIDForDelete(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getRtiIDForDelete(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Functiont to delete rti either from temp or final

    public int Delete_RTI(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.Delete_RTI(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get rti faa id during deleting record from table

    public DataSet getRtiFAAIDForDelete(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getRtiFAAIDForDelete(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Functiont to delete rti-faa either from temp or final

    public int Delete_RTIFAA(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.Delete_RTIFAA(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Functiont to delete rti-saa either from temp or final

    public int Delete_RTISAA(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.Delete_RTISAA(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End
    //funtion to get Rti
    public DataSet Get_RTI(PetitionOB pt_obj, out int catValue)
    {
        try
        {
            return rtiDL.Get_RTI(pt_obj, out catValue);
        }
        catch
        {
            throw;
        }
    }
    //end
    //Fuction To Check RTI linkup in FAA RTI
    public void Check_RTI_FAA(int RTI_ID, out int catValue)
    {
        try
        {
            rtiDL.Check_RTI_FAA(RTI_ID, out catValue);
        }
        catch
        {
            throw;
        }


    }
    //end
    //Fuction to get FAA_RTI
    public DataSet Get_FAA_RTI(PetitionOB pt_obj)
    {
        try
        {
            return rtiDL.Get_FAA_RTI(pt_obj);
        }
        catch
        {
            throw;
        }
    }
    //end
    //Funcation Check FAA record To SAA
    public void Check_RTI_FAA_SAA(int RTI_ID, out int catValue)
    {
        try
        {
            rtiDL.Check_RTI_FAA_SAA(RTI_ID, out catValue);
        }
        catch
        {
            throw;
        }


    }
    //end
    // Function to Get SAA Record BY FAA id
    public DataSet Get_Saa_Faa(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.Get_Saa_Faa(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    //end

    //Function to Get Previous year RTI
    public DataSet Get_PrevYear_RTI(PetitionOB pt_obj, out int catValue)
    {
        try
        {
            return rtiDL.Get_PrevYear_RTI(pt_obj, out catValue);
        }
        catch
        {
            throw;
        }
    }
    //End


    #region function to insert the status in Mst_Status table

    public int Insert_Status(PetitionOB rtiObject, out int id)
    {
        try
        {
            return rtiDL.Insert_Status(rtiObject, out id);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Status

    public DataSet get_MstStatus(PetitionOB petObject)
    {
        try
        {
            return rtiDL.get_MstStatus(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to get rti year

    public DataSet GetRTIYear()
    {
        try
        {
            return rtiDL.GetRTIYear();
        }
        catch
        {
            throw;
        }
    }

    #endregion


    //Fuction to search RTI
    //public DataSet Serch_RTI(PetitionOB pt_obj, string strsearchcondition)
    //{
    //    try
    //    {
    //        return rtiDL.Serch_RTI(pt_obj, strsearchcondition);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    public DataSet Serch_RTI(PetitionOB pt_obj, out int catValue)
    {
        try
        {
            return rtiDL.Serch_RTI(pt_obj,out catValue);
        }
        catch
        {
            throw;
        }
    }

    public DataSet Get_RTIById(PetitionOB pt_obj)
    {
        try
        {
            return rtiDL.Get_RTIById(pt_obj);

        }
        catch
        {
            throw;
        }


    }

    //end


    #region Function to get rti reference numbers(Conditional) of FAA

    public DataSet getReferenceNumberFAA(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getReferenceNumberFAA(rtiObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion



    #region Function to get rti reference numbers of FAA Edit

    public DataSet getReferenceNumberFAAEdit(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getReferenceNumberFAAEdit(rtiObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    //4 jan 2013

    #region Function to get rti reference numbers(Conditional) of SAA

    public DataSet getReferenceNumberSAA(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getReferenceNumberSAA(rtiObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to get year of RTI
    public DataSet GetYearRTI(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.GetYearRTI(rtiObject);
        }
        catch
        {
            throw;
        }

    }


    #endregion

    #region function to get year of RTI Admin

    public DataSet GetYearRTI_Admin()
    {
        try
        {
            return rtiDL.GetYearRTI_Admin();
        }
        catch
        {
            throw;
        }

    }


    #endregion

    #region function to get year of RTI FAA

    public DataSet GetYearRTIFAA()
    {
        try
        {
            return rtiDL.GetYearRTIFAA();
        }
        catch
        {
            throw;
        }
    }


    #endregion

    #region function to get year of RTI FAA admin

    public DataSet GetYearRTIFAA_admin()
    {
        try
        {
            return rtiDL.GetYearRTIFAA_admin();
        }
        catch
        {
            throw;
        }

    }


    #endregion


    #region function to get year of RTI SAA

    public DataSet GetYearRTISAA()
    {
        try
        {
            return rtiDL.GetYearRTISAA();
        }
        catch
        {
            throw;
        }
    }


    #endregion

    #region function to get year of RTI SAA admin

    public DataSet GetYearRTISAAAdmin()
    {
        try
        {
            return rtiDL.GetYearRTISAAAdmin();
        }
        catch
        {
            throw;
        }
    }


    #endregion

    #region Function to get rti reference numbers By Year

    public DataSet getReferenceNumberByYear(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getReferenceNumberByYear(rtiObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to get temporary rti records(Conditional) by ID

    public DataSet getTempRTIRecordsBYID(PetitionOB rtiObject)
    {
        try
        {

            return rtiDL.getTempRTIRecordsBYID(rtiObject);

        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function to get temporary rtiFAA records(Conditional) by ID

    public DataSet getTempRTIFAARecordsBYID(PetitionOB rtiObject)
    {
        try
        {

            return rtiDL.getTempRTIFAARecordsBYID(rtiObject);

        }
        catch
        {
            throw;
        }
        
    }

    #endregion


    #region Function to get temporary rtiSAA records(Conditional) by ID

    public DataSet getTempRTISAARecordsBYID(PetitionOB rtiObject)
    {
        try
        {

            return rtiDL.getTempRTISAARecordsBYID(rtiObject);

        }
        catch
        {
            throw;
        }
        
    }

    #endregion


    public DataSet Get_RTIFAACurrent(PetitionOB pt_obj, out int catValue)
    {
        try
        {
            
            return rtiDL.Get_RTIFAACurrent(pt_obj, out catValue);
        }
        catch
        {
            throw;
        }
        
    }


    public DataSet Get_RTISAACurrent(PetitionOB pt_obj, out int catValue)
    {
        try
        {

            return rtiDL.Get_RTISAACurrent(pt_obj, out catValue);
        }
        catch
        {
            throw;
        }

    }


    #region function to get year of RTI FAA for previous

    public DataSet GetYearRTIFAAPrevious(PetitionOB pt_obj)
    {
        try
        {
            return rtiDL.GetYearRTIFAAPrevious(pt_obj);
        }
        catch
        {
            throw;
        }
        
    }


    #endregion


    #region function to get year of RTI SAA for previous

    public DataSet GetYearRTISAAPrevious(PetitionOB pt_obj)
    {
        try
        {
            return rtiDL.GetYearRTISAAPrevious(pt_obj);
        }
        catch
        {
            throw;
        }
       
    }


    #endregion

    //RTI SAA front end details

    #region Function to get rtiSAA records(Conditional) by ID

    public DataSet getRTISAABYID(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getRTISAABYID(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    #region Function to get rti status during edit

    public DataSet getRtiStatusDuringEdit()
    {
        try
        {
            return rtiDL.getRtiStatusDuringEdit();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get rti reference numbers of SAA Edit

    public DataSet getReferenceNumberSAAEdit(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getReferenceNumberSAAEdit(rtiObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function To Update RTI web File to null

    public int updateRTIweb(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.updateRTIweb(rtiObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion 

    #region Function to update the  rti status for delete

    public int updateRtiStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.updateRtiStatusDelete(rtiObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function to update the  rti FAA status for delete

    public int updateRtiFAAStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.updateRtiFAAStatusDelete(rtiObject);

        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to update the  rti SAA status for delete

    public int updateRtiSAAStatusDelete(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.updateRtiSAAStatusDelete(rtiObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    //Functions to get email ids to send emails

    #region function to get email ids to send email.

    public DataSet getEmailIdToSendRTIEmail(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getEmailIdToSendRTIEmail(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region

    public DataSet getMobileToSendRTIEmail(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getMobileToSendRTIEmail(rtiObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region function to get email ids to send email.

    public DataSet getEmailIdToSendRTIFAAEmail(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getEmailIdToSendRTIFAAEmail(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region function to get mobile numbers to send sms.

    public DataSet getMobileNumberToSendRTIFAASms(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getMobileNumberToSendRTIFAASms(rtiObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to get email ids to send email.

    public DataSet getEmailIdToSendRTISAAEmail(PetitionOB rtiObject)
    {
        try
        {
            return rtiDL.getEmailIdToSendRTISAAEmail(rtiObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

}
