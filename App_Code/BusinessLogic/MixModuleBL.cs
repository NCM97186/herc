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


public class MixModuleBL
{
	public MixModuleBL()
	{
		
	}

    //Area for all the user defined functions

    MixModuleDL moduleDL = new MixModuleDL();

    //End

    #region Function to Bind Modules

    public DataSet getModuleName()
    {
        try
        {
            return moduleDL.getModuleName();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Deptt Name

    public DataSet getDepartmentName()
    {
        try
        {

            return moduleDL.getDepartmentName();
        }
        catch
        {
            throw;

        }
    }

    #endregion     

    #region Function to insert new records in modules

    public int ASP_Links_Insert(LinkOB obj_linkOB)
    {
        try
        {
            return moduleDL.ASP_Links_Insert(obj_linkOB);
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
            return moduleDL.ASP_Links_DisplayWithPaging(obj_linkOB, out catValue);
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
            return moduleDL.ASP_Temp_Links_Update_Status_Id(obj_linkOB);
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
            return moduleDL.ASP_Insert_Web_Links(obj_linkOB);
        }
        catch
        {
            throw;
        }
    }

    #endregion 

    //AREA FOR ALL THE REGULATIONS FUNCTIONS FOR FRONT END

    #region function to get Regulations Details

    public DataSet Get_Regulations(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return moduleDL.Get_Regulations(obj_petOB,out catValue);

        }
        catch
        {
            throw;
        }
    }

    #endregion

    //END

    //AREA FOR ALL THE STANDARDS FUNCTIONS FOR FRONT END

    #region function to get Standards Details

    public DataSet Get_Standards(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return moduleDL.Get_Standards(obj_petOB, out catValue);

        }
        catch
        {
            throw;
        }
    }

    #endregion

    //END

    //AREA FOR ALL THE CODES FUNCTIONS FOR FRONT END

    #region function to get CODES Details

    public DataSet Get_Codes(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return moduleDL.Get_Codes(obj_petOB, out catValue);

        }
        catch
        {
            throw;
        }
    }

    #endregion

    //END

    //AREA FOR ALL THE POLICIES FUNCTIONS FOR FRONT END

    #region function to get POLICIES Details

    public DataSet Get_Polices(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return moduleDL.Get_Polices(obj_petOB, out catValue);

        }
        catch
        {
            throw;
        }
    }

    #endregion

    //END

    //AREA FOR ALL THE GUIDELINES FUNCTIONS FOR FRONT END

    #region function to get GUIDELINES Details

    public DataSet Get_Guidelines(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return moduleDL.Get_Guidelines(obj_petOB, out catValue);

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get modulename

    public DataSet getModuleName(LinkOB lnkObject)
    {
        try
        {
            return moduleDL.getModuleName(lnkObject);

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to insert Connectedpetition

    public int insertConnectedModule(LinkOB lnkObject)
    {
        try
        {
            return moduleDL.insertConnectedModule(lnkObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get regulation numbers(Conditional)

    public DataSet getRegulation_Number(LinkOB lnkObject)
    {
        try
        {
            return moduleDL.getRegulation_Number(lnkObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Amendment number

    public DataSet getAmendmentNumber()
    {
        try
        {
            return moduleDL.getAmendmentNumber();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Regulation number

    public DataSet getRegulationNumber(LinkOB lnkObject)
    {
        try
        {
            return moduleDL.getRegulationNumber(lnkObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to display records by id for Editing

    public DataSet ASP_Links_DisplayBYID(LinkOB obj_linkOB)
    {
        try
        {
            return moduleDL.ASP_Links_DisplayBYID(obj_linkOB);
        }
        catch
        {
            throw;
        }
    }

    #endregion 

    #region Function to get get regulation number

    public string getRegulationNumberCurrent(LinkOB lnkObject)
    {
        try
        {
            return moduleDL.getRegulationNumberCurrent(lnkObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get connected modulenames

    public DataSet getConnectedModuleName(LinkOB obj_linkOB)
    {
        try
        {
            return moduleDL.getConnectedModuleName(obj_linkOB);
        }
        catch
        {
            throw;
        }
    }

    #endregion 

    #region Function to update new records in modules

    public int ASP_Links_Update(LinkOB obj_linkOB)
    {
        try
        {
            return moduleDL.ASP_Links_Update(obj_linkOB);
        }
        catch
        {
            throw;
        }
    }

    #endregion 

    #region function to delete pending or approved record

    public int DeleteMixModuleRecords(LinkOB lnkObject)
    {
        try
        {
            return moduleDL.DeleteMixModuleRecords(lnkObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

     #region Function to get ambendment type(Conditional)

    public int getAmbendmentID(LinkOB lnkObject)
    {
        try
        {
            return moduleDL.getAmbendmentID(lnkObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get Regulation number according to ambendment

    public DataSet getRegulationNumberAmbendment(LinkOB lnkObject)
    {
        try
        {
            return moduleDL.getRegulationNumberAmbendment(lnkObject);

        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get connected modulenames for adding new regulation numbers

    public DataSet getConnectedModuleNameForRegulationSelected(LinkOB obj_linkOB)
    {
        try
        {
            return moduleDL.getConnectedModuleNameForRegulationSelected(obj_linkOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to delete Connected regulations modules

    public int deleteConnectedRegulations(LinkOB lnkObject)
    {
        try
        {
            return moduleDL.deleteConnectedRegulations(lnkObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get Regulation numbers in edit mode

    public DataSet getRegulationNumber_In_EditMode(LinkOB lnkObject)
    {
        try
        {
            return moduleDL.getRegulationNumber_In_EditMode(lnkObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion



    #region Function to display records GetAmendmentID

    public DataSet USP_GetAmendmentID(LinkOB obj_linkOB)
    {
        try
        {
            return moduleDL.USP_GetAmendmentID(obj_linkOB);
        }
        catch
        {
            throw;
        }
     
    }

    #endregion 

    //END

    #region Function To insert regulations/policies/guidelines/etc Into web_Links

    public int InsertRegulationsInWeb(LinkOB obj_linkOB)
    {
        try
        {
            return moduleDL.InsertRegulationsInWeb(obj_linkOB);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion 


    #region Function to update the  Web link status for Restore

    public int updateWebStatusRestore(LinkOB lnkObject)
    {
        try
        {
            return moduleDL.updateWebStatusRestore(lnkObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function to get Details of new regulation numbers

    public DataSet getDetails(LinkOB obj_linkOB)
    {
        try
        {
            return moduleDL.getDetails(obj_linkOB);
        }
        catch
        {
            throw;
        }
       
    }
    #endregion 
}

   