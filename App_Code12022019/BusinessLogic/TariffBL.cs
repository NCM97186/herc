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


public class TariffBL
{
    #region Variable declaration zone

    TariffDL obj_tariffDL = new TariffDL();

    #endregion 
	public TariffBL()
	{
		
	}


    #region Function to get tariff type
    public DataSet getTariffType()
    {
        try
        {

            return obj_tariffDL.getTariffType();
        }
        catch
        {
            throw;
        }

    }

    #endregion



    #region function to insert New Tariff

    public int insert_New_Tariff(TariffOB tariffOB)
    {
        try
        {
            return obj_tariffDL.insert_New_Tariff(tariffOB);

        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to update the link_Temp status for permission

    public int ASP_ChangeStatus_LinkTmpPermission(TariffOB tariffOB)
    {
        try
        {
            return obj_tariffDL.ASP_ChangeStatus_LinkTmpPermission(tariffOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion 
    #region function to insert top menu in links web table

    public int insert_Top_Tariff_in_Web(TariffOB tariffOB)
    {
        try
        {
            return obj_tariffDL.insert_Top_Tariff_in_Web(tariffOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to delete pending or approved record

    public int Delete_Pending_Approved_Record(TariffOB tariffOB)
    {
        try
        {
            return obj_tariffDL.Delete_Pending_Approved_Record(tariffOB);
        }
        catch
        {
            throw;
        }

    }


    #endregion

    #region Function to get Link Id  for edit from WEB LINKS

    public DataSet links_web_Get_Link_Id_ForEdit(TariffOB tariffOB)
    {
        try
        {
            return obj_tariffDL.links_web_Get_Link_Id_ForEdit(tariffOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion
    #region Function To Display Details With Paging

    public DataSet ASP_Links_DisplayWithPaging(TariffOB tariffOB, out int catValue)
    {
        try
        {
            return obj_tariffDL.ASP_Links_DisplayWithPaging(tariffOB, out catValue);
        }
        catch
        {
            throw;
        }

    }

    #endregion 

    #region Function to get data for edit either from links table or links_web table

    public DataSet getMenu_For_Editing(TariffOB tariffOB)
    {
        try
        {
            return obj_tariffDL.getMenu_For_Editing(tariffOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region function to get year
    public DataSet getYear(TariffOB obj)
    {
        try
        {
            return obj_tariffDL.getYear(obj);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 



    #region function to get tariff details
    public DataSet Getdetails(TariffOB obj, out int catValue)
    {
        try
        {
            return obj_tariffDL.Getdetails(obj, out catValue);
        }
        catch
        {
            throw;
        }
        
    }
    #region function to get tariff details data
    public DataSet GetdetailsData(TariffOB obj)
    {
        try
        {


            return obj_tariffDL.GetdetailsData(obj);


        }
        catch
        {
            throw;
        }
       
    }


    #endregion


    #endregion.

    #region function to get Categoty
    public DataSet getCategoty(TariffOB obj)
    {
        try
        {

            return obj_tariffDL.getCategoty(obj);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 


    #region function to get FSA details
    public DataSet GetdetailsFSA(TariffOB obj, out int catValue)
    {
        try
        {

            return obj_tariffDL.GetdetailsFSA(obj,out catValue);
        }
        catch
        {
            throw;
        }
       
    }


    #endregion

    #region function to get year

    public DataSet getYearForTariff(TariffOB obj)
    {
        try
        {
            return obj_tariffDL.getYearForTariff(obj);
        }
        catch
        {
            throw;
        }
    }

    #endregion 


    // This is for reports in ombudsman
    #region function to get year

    public DataSet getYearForReport()
    {
        try
        {

            return obj_tariffDL.getYearForReport();
        }
        catch
        {
            throw;
        }
      
    }

    #endregion 



    #region function to get Reports details
    public DataSet GetReportsdetails(TariffOB obj, out int catValue)
    {
        try
        {
            return obj_tariffDL.GetReportsdetails(obj, out catValue);
        }
        catch
        {
            throw;
        }
       
    }


    #endregion
    // This is for multiple files of Report

    #region Function to Insert report files
    public int InsertReportFiles(TariffOB objNew)
    {
        try
        {
            return obj_tariffDL.InsertReportFiles(objNew);
           
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 

    #region Function to Update status for Files in Reports

    public int UpdateFileStatusForReports(TariffOB objNew)
    {
        try
        {
            return obj_tariffDL.UpdateFileStatusForReports(objNew);
        }
        catch
        {
            throw;
        }
        

    }

    #endregion


    #region Function to get FileName for Report

    public DataSet getFileNameForReport(TariffOB objNew)
    {
        try
        {
            return obj_tariffDL.getFileNameForReport(objNew);
        }
        catch
        {
            throw;
        }
        

    }

    #endregion


    #region Function to get FileName for Report Front End

    public DataSet getFileName(TariffOB objNew)
    {
        try
        {
            return obj_tariffDL.getFileName(objNew);
        }
        catch
        {
            throw;
        }
        

    }

    #endregion


		 #region Function to delete Files in tariff

    public int DeleteFileTariff(TariffOB objNew)
    {
        try
        {
            return obj_tariffDL.DeleteFileTariff(objNew);
        }
        catch
        {
            throw;
        }
        

    }

    #endregion


    #region Function to get FileName for Tariff

    public DataSet getFileNameTariff(TariffOB objNew)
    {
        try
        {
            return obj_tariffDL.getFileNameTariff(objNew);
        }
        catch
        {
            throw;
        }

    }

    #endregion


}



