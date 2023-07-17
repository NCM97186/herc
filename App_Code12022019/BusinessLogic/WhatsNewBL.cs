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

/// <summary>
/// Summary description for WhatsNew
/// </summary>
public class WhatsNewBL
{
    WhatsNewDL obj_wndl = new WhatsNewDL();
    public WhatsNewBL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region insert News

    public int InsertNews(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            //return TenderDL_Obj.InsertTender(TenderOB);
            return obj_wndl.insert_news(objWhatNewsOB);
            
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region insert News

    public int RestoreNews(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            //return TenderDL_Obj.InsertTender(TenderOB);
            return obj_wndl.Restore_news(objWhatNewsOB);

        }
        catch
        {
            throw;
        }
    }
    #endregion
    public int ASP_ChangeStatus_TempNews(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            //return TenderDL_Obj.ASP_ChangeStatus_TempTender(objTender);
            return obj_wndl.ASP_ChangeStatus_TempNews(objWhatNewsOB);
           // return obj_wndl.
        }
        catch
        {
            throw;
        }
    }

    public DataSet News_BL_get_AprovedEdit(WhatNewsOB objWhatNewsOB)
    {
        return obj_wndl.NewsDl_get_ApprovedEdit(objWhatNewsOB);
    }
    public DataSet ASP_Get_News(WhatNewsOB objWhatNewsOB)
    {
        try
        {
           // return TenderDL_Obj.ASP_Get_Tender(objTender);
            return obj_wndl.ASP_Get_News(objWhatNewsOB);
        }
        catch
        {
            throw;
        }
    }
    #region Get files according to Tender Id
    public int UpdateFiles(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            return obj_wndl.UpdateFiles(objWhatNewsOB);
        }
        catch
        {
            throw;
        }
    }
    #endregion
    #region insert Tender
    public int InsertFiles(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            return obj_wndl.InsertFiles(objWhatNewsOB);
        }
        catch
        {
            throw;
        }
    }

    #endregion
    #region Get files according to Tender Id

    public DataSet getTenderFiles(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            return obj_wndl.getTenderFiles(objWhatNewsOB);
        }
        catch
        {
            throw;
        }
    }

    #endregion
    #region Get files according to  Id

    public int ASP_InsertUpdateDelete_News(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            return obj_wndl.ASP_InsertUpdateDelete_News(objWhatNewsOB);
        }
        catch
        {
            throw;
        }
    }

    #endregion
    #region WhatsNew_Delete
    public int News_Delete(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            //return TenderDL_Obj.Tender_Delete(objTender);
            return obj_wndl.News_Delete(objWhatNewsOB);
        }
        catch
        {
            throw;
        }
    }
    #endregion
    #region WhatsNew_Mul_Delete
    public int News_Mul_Delete(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            //return TenderDL_Obj.Tender_Delete(objTender);
            return obj_wndl.News_Mul_Delete(objWhatNewsOB);
        }
        catch
        {
            throw;
        }
    }
    #endregion

    //Front End
    #region function to show latest whatsnew

    public DataSet GetWhatsNew(WhatNewsOB objNews)
    {
        try
        {
            return obj_wndl.GetWhatsNew(objNews);
        }
        catch
        {
            throw;
        }
       
    }
    #endregion 


    #region function to bind  all  Whats new  details

    public DataSet Get_AllWhatsNew(WhatNewsOB ObjNews, out int catValue)
    {
        try
        {
            return obj_wndl.Get_AllWhatsNew(ObjNews,out catValue);
        }
        catch
        {
            throw;
        }
        
    }
    #endregion 

    #region function to show latest whatsnew by Id

    public DataSet GetWhatsNewById(WhatNewsOB objNews)
    {
        try
        {
            return obj_wndl.GetWhatsNewById(objNews);
        }
        catch
        {
            throw;
        }
       
    }
    #endregion 


    #region function to show latest whatsnew of Act/Rules

    public DataSet GetWhatsNewActRules(WhatNewsOB objNews)
    {
        try
        {
            return obj_wndl.GetWhatsNewActRules(objNews);
        }
        catch
        {
            throw;
        }
        
    }
    #endregion 


    #region delete file from whats new
    public int WhatsNewFileDelete(WhatNewsOB objWhatsNewOB)
    {
        try
        {
            return obj_wndl.WhatsNewFileDelete(objWhatsNewOB);

        }
        catch
        {
            throw;
        }
        
    }

    #endregion


}
