using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public class LinkBL
{
    //Area for all the variables declaration

    #region Variable Declaration

    LinkDAL obj_linkDL = new LinkDAL();

    #endregion 

    //End

    //Area for all the constructors declaration

    #region Default Constructor Zone

    public LinkBL()
	{

    }

    #endregion

    //End

    //Area for all the function to get records

    #region Function Get Language

    public DataSet ASP_Get_Language()
    {
        try
        {
            return obj_linkDL.ASP_Get_Language();
        }
        catch
        {
            throw;

        }


    }

    #endregion

    #region Function To Display Details

    public DataSet ASP_Links_Display(LinkOB obj_linkOB)
    {
        try
        {
            return obj_linkDL.ASP_Links_Display(obj_linkOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function To Display Details for modules

    public DataSet ASP_Links_Display_forModules(LinkOB obj_linkOB)
    {
        try
        {
            return obj_linkDL.ASP_Links_Display_forModules(obj_linkOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function To display Links BY id

    public DataSet ASP_Links_DisplayBYID(LinkOB obj_linkOB)
    {
        try
        {
            return obj_linkDL.ASP_Links_DisplayBYID(obj_linkOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion 

    #region Function to get Link Id  for edit from WEB LINKS

    public DataSet links_web_Get_Link_Id_ForEdit(LinkOB obj_linkOB)
    {
        try
        {
            return obj_linkDL.links_web_Get_Link_Id_ForEdit(obj_linkOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function MST_Get_Status

    public DataSet MST_Get_Status()
    {
        try
        {
            return obj_linkDL.MST_Get_Status();
        }
        catch
        {
            throw;
        }


    }

    #endregion 

    #region Function To Display Details With Paging

    public DataSet ASP_Links_DisplayWithPaging(LinkOB obj_linkOB, out int catValue)
    {
        try
        {
            return obj_linkDL.ASP_Links_DisplayWithPaging(obj_linkOB, out catValue);
        }
        catch
        {
            throw;
        }

    }

    #endregion 

    //End

    //Area for all the functions to insert

    #region Function to insert new Links

    public int ASP_Links_Insert(LinkOB obj_linkOB)
    {
        try
        {
           return obj_linkDL.ASP_Links_Insert(obj_linkOB);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 

    #region Function To insert Into web_Links

    public int ASP_Insert_Web_Links(LinkOB obj_linkOB)
    {
        try
        {
            return obj_linkDL.ASP_Insert_Web_Links(obj_linkOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion 

    //End

    //Area for all the functions to update

    #region Function To update Links

    public Int32 ASP_Links_Update(LinkOB obj_linkOB)
    {
        try
        {
            return obj_linkDL.ASP_Links_Update(obj_linkOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 

    #region Function to update the temp_links status

    public int ASP_Temp_Links_Update_Status_Id(LinkOB obj_linkOB)
    {
        try
        {
            return obj_linkDL.ASP_Temp_Links_Update_Status_Id(obj_linkOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 

    #region Function to Delete the data from tables for modules

    public int Delete_ModulesRecords(LinkOB obj_linkOB)
    {
        try
        {
            return obj_linkDL.Delete_ModulesRecords(obj_linkOB);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 

    #region Function to update the link_Temp status for permission

    public int ASP_ChangeStatus_LinkTmpPermission(LinkOB obj_linkOB)
    {
        try
        {
            return obj_linkDL.ASP_ChangeStatus_LinkTmpPermission(obj_linkOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion 

    #region Function to get Deptt Name

    public DataSet ASP_Get_Deptt_Name(UserOB usrObject)
    {
        try
        {
            return obj_linkDL.ASP_Get_Deptt_Name(usrObject);
        }
        catch
        {
            throw;

        }

    }
    #endregion     


    #region function to bind annual report details

    public DataSet Get_AnnualReports(LinkOB obj_linkOB, out int catValue)
    {
        try
        {
            return obj_linkDL.Get_AnnualReports(obj_linkOB, out  catValue);
        }
        catch
        {
            throw;
        }

    }
    #endregion 

   
    #region function to bind annual report details

    public DataSet Get_WhatsNew(LinkOB objlnkOB)
    {
        try
        {

            return obj_linkDL.Get_WhatsNew(objlnkOB);

        }
        catch
        {
            throw;
        }
        
    }
    #endregion 

    #region function to bind  all  Whats new  details

    public DataSet Get_AllWhatsNew(LinkOB obj_linkOB, out int catValue)
    {
        try
        {
            return obj_linkDL.Get_AllWhatsNew(obj_linkOB, out  catValue);
            
        }
        catch
        {
            throw;
        }
        
    }
    #endregion 


    #region Function to display Datails  by id In Front End

    public DataSet Link_DisplayDetails(LinkOB obj_linkOB)
    {
        try
        {
            return obj_linkDL.Link_DisplayDetails(obj_linkOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 


    #region Function  to Bind Licensees

    public DataSet Bind_Licensees(LinkOB lnkObject)
    {
        try
        {
            return obj_linkDL.Bind_Licensees(lnkObject);
        }
        catch
        {
            throw;
        }
    }
    #endregion 


    #region function to bind DDPapers
    public DataSet GetDiscussionPapers(LinkOB objlnkOB, out int catValue)
    {
        try
        {
           return obj_linkDL.GetDiscussionPapers(objlnkOB,out catValue);
        }
        catch
        {
            throw;
        }


    }

    #endregion 



    #region Function  to dateforcalenderevant

    public DataSet get_dateforcalenderevant(LinkOB lnkObject)
    {
        try
        {
            return obj_linkDL.get_dateforcalenderevant(lnkObject);
        }
        catch
        {
            throw;
        }
    }
    #endregion 


    #region function to bind year

    public DataSet Get_YearLink()
    {
        try
        {
            return obj_linkDL.Get_YearLink();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 

    #region function to get Annual information of Previous Year
    public DataSet Get_Annual_PrevYear(LinkOB obj_lnkOB, out int catValue)
    {

        try
        {
           return obj_linkDL.Get_Annual_PrevYear(obj_lnkOB, out catValue);
        }
        catch
        {
            throw;
        }
       
    }
    #endregion 

    #region Function to Bind Public Sector Links

    public DataSet Bind_PublicSectorLinks(LinkOB lnkObject)
    {
        try
        {

            return obj_linkDL.Bind_PublicSectorLinks(lnkObject);


        }
        catch
        {
            throw;
        }
       
    }


    #endregion

    #region Function to Bind Public Sector Links Home

    public DataSet Bind_PublicSectorLinksHome(LinkOB lnkObject)
    {
        try
        {

            return obj_linkDL.Bind_PublicSectorLinksHome(lnkObject);


        }
        catch
        {
            throw;
        }
    }


    #endregion

    #region Function to Bind Abbreviations

    public DataSet Bind_Abbreviations(LinkOB lnkObject)
    {
        try
        {

            return obj_linkDL.Bind_Abbreviations(lnkObject);


        }
        catch
        {
            throw;
        }
      
    }


    #endregion

    #region Function to update the status in  the web_link

    public int ASP_Update_status(LinkOB objlnkOB)
    {
        try
        {
            return obj_linkDL.ASP_Update_status(objlnkOB);
        }
        catch
        {
            throw;
        }
        
    }
    #endregion 

    //End


    #region function to get year

    public DataSet Get_Year()
    {
        try
        {
            return obj_linkDL.Get_Year();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region function to get year for vacancy

    public DataSet Get_YearVacancy()
    {
        try
        {
            return obj_linkDL.Get_YearVacancy();
        }
        catch
        {
            throw;
        }
        
    }

    #endregion


    #region Function to update the  Web link status for delete

    public int updateWebStatusDelete(LinkOB lnkObject)
    {
        try
        {
            return obj_linkDL.updateWebStatusDelete(lnkObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region Function to insert Connected Discussion files

    public int insertConnectedDiscussionFiles(LinkOB lnkObject)
    {
        try
        {
            return obj_linkDL.insertConnectedDiscussionFiles(lnkObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function to get connected Discussion file name

    public DataSet getDiscussionFileNames(LinkOB lnkObject)
    {
        try
        {
            return obj_linkDL.getDiscussionFileNames(lnkObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function to get FileName for Discussion

    public DataSet getFileNameForDiscussion(LinkOB lnkObject)
    {
        try
        {
            return obj_linkDL.getFileNameForDiscussion(lnkObject);
        }
        catch
        {
            throw;
        }
       

    }

    #endregion

    #region Function to Update status for Files in Discussion

    public int UpdateFileStatusForDiscussion(LinkOB lnkObject)
    {
        try
        {
            return obj_linkDL.UpdateFileStatusForDiscussion(lnkObject);
        }
        catch
        {
            throw;
        }
       

    }

    #endregion


    #region function to bind Vacancy

    public DataSet GetVacancy(LinkOB objlnkOB, out int catValue)
    {
        try
        {
            return obj_linkDL.GetVacancy(objlnkOB, out catValue);
        }
        catch
        {
            throw;
        }
        

    }

    #endregion 


    //////#region function to bind module Name for Archive

    //////public DataSet GetModuleName_Archive()
    //////{
    //////    try
    //////    {
    //////        return obj_linkDL.GetModuleName_Archive();
    //////    }
    //////    catch
    //////    {
    //////        throw;
    //////    }
       
    //////}

    //////#endregion


    #region Function to Insert Notification/Annual files
    public int InsertFiles(LinkOB objlnkOB)
    {
        try
        {
            return obj_linkDL.InsertFiles(objlnkOB);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 


    #region Function to get FileName for Notification/Annual Report

    public DataSet getFileNameForModules(LinkOB objlnkOB)
    {
        try
        {
            return obj_linkDL.getFileNameForModules(objlnkOB);
        }
        catch
        {
            throw;
        }
       

    }

    #endregion

    #region Function to Update status for Files in Modules

    public int UpdateFileStatusdelete(LinkOB objlnkOB)
    {
        try
        {
            return obj_linkDL.UpdateFileStatusdelete(objlnkOB);
        }
        catch
        {
            throw;
        }
       

    }

    #endregion


    #region Function to get FileName for Modules Front End

    public DataSet getFileName(LinkOB objlnkOB)
    {
        try
        {
            return obj_linkDL.getFileName(objlnkOB);
        }
        catch
        {
            throw;
        }
        

    }

    #endregion

		   #region Function to audit login/logoff Report

    public DataSet getAuditLoginLogoffReport(out int catValue)
    {
        try
        {

            return obj_linkDL.getAuditLoginLogoffReport(out catValue);

        }
        catch
        {
            throw;
        }
      
    }

    #endregion


    #region Function to audit Report

    public DataSet getAuditReport(out int catValue)
    {
        try
        {

            return obj_linkDL.getAuditReport(out catValue);

        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region function To delete the user

    public int DeleteLoginLogoffDetails(UserOB obj_UserOB)
    {
        try
        {
            return obj_linkDL.DeleteLoginLogoffDetails(obj_UserOB);
        }
        catch
        {
            throw;
        }
      

    }

    #endregion 

}
