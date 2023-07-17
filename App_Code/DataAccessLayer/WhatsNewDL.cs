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
using NCM.DAL;
using System.Data.SqlClient;

/// <summary>
/// Summary description for WhatsNewDL
/// </summary>
public class WhatsNewDL
{
    #region variable declaration zone

    NCMdbAccess obj_ncm = new NCMdbAccess();
    Project_Variables p_Val = new Project_Variables();

    #endregion 

	public WhatsNewDL()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #region insert news
    public int insert_news(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@MSG", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncm.AddParameter(param[0]);
            obj_ncm.AddParameter("@Flag", objWhatNewsOB.ActionType);
            //obj_ncm.AddParameter("@FK_LANG_ID", objWhatNewsOB.LangId);
            obj_ncm.AddParameter("@News_ID", objWhatNewsOB.TenderId);
            obj_ncm.AddParameter("@News_REF_NO", objWhatNewsOB.RefNo);
            obj_ncm.AddParameter("@News_TITLE", objWhatNewsOB.NewsTitle);
            if (objWhatNewsOB.ApproveStatus != 6)
            {
                obj_ncm.AddParameter("@APPRV_STATUS", objWhatNewsOB.ApproveStatus);
            }
            else
            {
                obj_ncm.AddParameter("@APPRV_STATUS", 3);
            }
            obj_ncm.AddParameter("@NewsDecription", objWhatNewsOB.NewsDescription);
            obj_ncm.AddParameter("@News_VALUE", objWhatNewsOB.TenderCost);
            obj_ncm.AddParameter("@News_EMD", objWhatNewsOB.EarnestMoney);
            obj_ncm.AddParameter("@InsertedBy", objWhatNewsOB.InsertedBy);
            obj_ncm.AddParameter("@Name", objWhatNewsOB.Name);
            obj_ncm.AddParameter("@Email_Id", objWhatNewsOB.EmailId);
            obj_ncm.AddParameter("@PhoneNo", objWhatNewsOB.MobileNo);
            obj_ncm.AddParameter("@OPENING_DT", objWhatNewsOB.StartDate);
            obj_ncm.AddParameter("@LAST_DT ", objWhatNewsOB.Expirydate);

            obj_ncm.AddParameter("@IpAddress", objWhatNewsOB.IpAddress);
            obj_ncm.AddParameter("@FileName", objWhatNewsOB.FileName);

           obj_ncm.ExecuteNonQuery("Proc_InsertUpdateDelete_News1");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
             
        }
        catch
        {
            throw;
        }
    } 
      #endregion

    #region restore_news
    public int Restore_news(WhatNewsOB objWhatNewsOB)
    {
        obj_ncm.AddParameter("@News_Id", objWhatNewsOB.TenderId);
        obj_ncm.AddParameter("@Approve_status", objWhatNewsOB.ApproveStatus);
        obj_ncm.AddParameter("@ModuleId",objWhatNewsOB.ModuleID);
      int y=  obj_ncm.ExecuteNonQuery("restore_news");
      return y;
    }
    #endregion

    #region Function To Update Files

    public int UpdateFiles(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncm.AddParameter(param[0]);
            obj_ncm.AddParameter("@ActionType", objWhatNewsOB.Flag);
            obj_ncm.AddParameter("@ApproveStatus", objWhatNewsOB.ApproveStatus);
            obj_ncm.AddParameter("@TenderId", objWhatNewsOB.TenderId);
            obj_ncm.AddParameter("@Status", objWhatNewsOB.Status);
            obj_ncm.AddParameter("@TenderIdTmp", objWhatNewsOB.TenderIdTmp);
            return obj_ncm.ExecuteNonQuery("InsertUpdateDeleteFiles");

        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region Function To Insert File

    public int InsertFiles(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncm.AddParameter(param[0]);
            obj_ncm.AddParameter("@ActionType", objWhatNewsOB.Flag);
            obj_ncm.AddParameter("@Filename", objWhatNewsOB.FileName);
            obj_ncm.AddParameter("@RefID", objWhatNewsOB.RefNo);
            obj_ncm.AddParameter("@Title", objWhatNewsOB.FileTitle);

            obj_ncm.AddParameter("@FileExtention", objWhatNewsOB.FileExtension);
            obj_ncm.AddParameter("@ModuleID", objWhatNewsOB.ModuleID);
            obj_ncm.AddParameter("@FileTypeID", objWhatNewsOB.FileTypeID);
            obj_ncm.AddParameter("@InsertedBy", objWhatNewsOB.InsertedBy);
            obj_ncm.AddParameter("@ApproveStatus", objWhatNewsOB.ApproveStatus);
            obj_ncm.AddParameter("@TenderId", objWhatNewsOB.TenderId);

            return obj_ncm.ExecuteNonQuery("InsertUpdateDeleteFiles");

        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region Function To Update Files

    public DataSet getTenderFiles(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            obj_ncm.CommandText = "getNewsFiles";
            obj_ncm.Parameters.Add(new SqlParameter("@ActionType", objWhatNewsOB.ActionType));
            obj_ncm.Parameters.Add(new SqlParameter("@tenderId", objWhatNewsOB.TenderId));
            obj_ncm.Parameters.Add(new SqlParameter("@moduleId", objWhatNewsOB.ModuleID));
            obj_ncm.Parameters.Add(new SqlParameter("@Status", objWhatNewsOB.Status));
            obj_ncm.Parameters.Add(new SqlParameter("@refNo", objWhatNewsOB.RefNo));
            obj_ncm.Parameters.Add(new SqlParameter("@FileTypeID", objWhatNewsOB.FileTypeID));
            return obj_ncm.ExecuteDataSet();
        }
        catch
        {
            throw;
        }

    }
    #endregion


    public DataSet ASP_Get_News(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            if (objWhatNewsOB.ApproveStatus == 8)
            {
                obj_ncm.CommandText = "get_mul_delete";
                obj_ncm.Parameters.Add(new SqlParameter("@APPRV_STATUS", objWhatNewsOB.ApproveStatus));
            }
            else
            {
                obj_ncm.CommandText = "ASP_Get_News";

                //obj_ncm.Parameters.Add(new SqlParameter("@FK_LANG_ID", objWhatNewsOB.LangId));
                obj_ncm.Parameters.Add(new SqlParameter("@Flag", objWhatNewsOB.ActionType));
                obj_ncm.Parameters.Add(new SqlParameter("@PageIndex", objWhatNewsOB.PageIndex));
                obj_ncm.Parameters.Add(new SqlParameter("@PageSize", objWhatNewsOB.PageSize));
                obj_ncm.Parameters.Add(new SqlParameter("@News_ID", objWhatNewsOB.TenderId));
                obj_ncm.Parameters.Add(new SqlParameter("@APPRV_STATUS", objWhatNewsOB.ApproveStatus));
            }
            return obj_ncm.ExecuteDataSet();
        }
        catch
        {
            throw;
        }

    }
    public int ASP_ChangeStatus_TempNews(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncm.AddParameter(param[0]);
            obj_ncm.AddParameter("@TempNews_ID", objWhatNewsOB.TenderId);
            obj_ncm.AddParameter("@Status", objWhatNewsOB.Status);
            obj_ncm.AddParameter("@APPRV_STATUS", objWhatNewsOB.ApproveStatus);
            obj_ncm.AddParameter("@updatedBy", objWhatNewsOB.UserId);

            obj_ncm.AddParameter("@UserID", objWhatNewsOB.UserId);
            obj_ncm.AddParameter("@IpAddress", objWhatNewsOB.IpAddress);
            obj_ncm.ExecuteNonQuery("ASP_ChangeStatus_TempNews");
            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncm.Dispose();
        }
    }

    #region News_Delete

    public int News_Delete(WhatNewsOB objWhatNewsOB)
    {
        try
        {

            obj_ncm.AddParameter("@Flag", objWhatNewsOB.ActionType);
            obj_ncm.AddParameter("@News_ID", objWhatNewsOB.TenderId);
           // obj_ncm.AddParameter("@News_REF_NO", objTender.RefNo);
            obj_ncm.AddParameter("@APPRV_STATUS", objWhatNewsOB.ApproveStatus);
            obj_ncm.AddParameter("@ModuleID", objWhatNewsOB.ModuleID);
            return obj_ncm.ExecuteNonQuery("News_delete1");


        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncm.Dispose();
        }
    }
    #endregion

    #region News_Mul_Delete
    public int News_Mul_Delete(WhatNewsOB objWhatNewsOB)
    {
        try
        {

            obj_ncm.AddParameter("@Status", objWhatNewsOB.Status);
            obj_ncm.AddParameter("@News_ID", objWhatNewsOB.RefNo);

            obj_ncm.AddParameter("@APPRV_STATUS", objWhatNewsOB.ApproveStatus);
            obj_ncm.AddParameter("@ModuleID", objWhatNewsOB.ModuleID);
            int i= obj_ncm.ExecuteNonQuery("News_Multiple_Delete");
            return i;


        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncm.Dispose();
        }
    }
    #endregion

    #region approved_edit
    public DataSet NewsDl_get_ApprovedEdit(WhatNewsOB objWhatNewsOB)
    {
        obj_ncm.Parameters.Add(new SqlParameter("@tmpactid", objWhatNewsOB.TenderId));
        return obj_ncm.ExecuteDataSet("asp_get_news_approvededit_data");
    }
    #endregion

    public int ASP_InsertUpdateDelete_News(WhatNewsOB objWhatNewsOB)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            obj_ncm.AddParameter("@TempNews_ID", objWhatNewsOB.TenderId);
            obj_ncm.AddParameter("@Status", objWhatNewsOB.Status);
            obj_ncm.AddParameter("@APPRV_STATUS", objWhatNewsOB.ApproveStatus);
            obj_ncm.AddParameter("@InsertedBy", objWhatNewsOB.UserId);
            obj_ncm.AddParameter("@IPAddress", objWhatNewsOB.IpAddress);
            obj_ncm.AddParameter("@UserID", objWhatNewsOB.UserId);
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncm.Parameters.Add(param[0]);
            obj_ncm.ExecuteNonQuery("ASP_InsertUpdateDelete_News");

            p_Val.Result = Convert.ToInt32(param[0].Value);
            return p_Val.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncm.Dispose();
        }
    }

    //Front End
    #region function to show latest whatsnew

    public DataSet GetWhatsNew(WhatNewsOB objNews)
    {
        try
        {
            obj_ncm.AddParameter("@ActionType",objNews.ActionType);
            return obj_ncm.ExecuteDataSet("GetLatestWhatsNew");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncm.Dispose();
        }
    }
    #endregion 

    #region function to bind  all  Whats new  details

    public DataSet Get_AllWhatsNew(WhatNewsOB ObjNews, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            obj_ncm.Parameters.Add(param[0]);
            obj_ncm.AddParameter("@ActionType", ObjNews.ActionType);
            
           // obj_ncm.AddParameter("@Lang_Id", ObjNews.LangId);
            p_Val.dSet = obj_ncm.ExecuteDataSet("GetLatestWhatsNew");
            catValue = Convert.ToInt32(param[0].Value);
            return p_Val.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncm.Dispose();
        }
    }
    #endregion 

    #region function to show latest whatsnew by Id

    public DataSet GetWhatsNewById(WhatNewsOB objNews)
    {
        try
        {
            obj_ncm.AddParameter("@NewsId", objNews.AwardId);
            return obj_ncm.ExecuteDataSet("getWhatsNewById");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncm.Dispose();
        }
    }
    #endregion 

    #region function to show latest whatsnew of Act/Rules

    public DataSet GetWhatsNewActRules(WhatNewsOB objNews)
    {
        try
        {
            obj_ncm.AddParameter("@moduleId", objNews.ModuleID);
            return obj_ncm.ExecuteDataSet("GetActForWhatsNew");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncm.Dispose();
        }
    }
    #endregion 

    #region delete file from whats new
    public int WhatsNewFileDelete(WhatNewsOB objWhatsNewOB)
    {
        try
        {
            obj_ncm.AddParameter("@Id",objWhatsNewOB.TenderId);
            obj_ncm.AddParameter("@ststus",objWhatsNewOB.Status);
            return obj_ncm.ExecuteNonQuery("DeletefileWhatsNew");
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_ncm.Dispose();
        }
    }

    #endregion
}   
    
   
