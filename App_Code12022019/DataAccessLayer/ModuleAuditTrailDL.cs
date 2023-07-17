using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NCM.DAL;
using System.Data.SqlClient;
/// <summary>
/// Summary description for ModuleAuditTrailDL
/// </summary>
public class ModuleAuditTrailDL
{
    NCMdbAccess ncmdbObject = new NCMdbAccess();
    public ModuleAuditTrailDL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void InsertModuleAuditTrailDetails(ModuleAuditTrail moduleAuditTrail)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@UserId", moduleAuditTrail.UserId));
            ncmdbObject.AddParameter("@UserName", moduleAuditTrail.UserName);
            ncmdbObject.Parameters.Add(new SqlParameter("@IpAddress", moduleAuditTrail.IpAddress));
            ncmdbObject.AddParameter("@ActionType", moduleAuditTrail.ActionType);
            ncmdbObject.AddParameter("@Title", moduleAuditTrail.Title);
            ncmdbObject.AddParameter("@ModuleID", moduleAuditTrail.ModuleID);
          ncmdbObject.ExecuteNonQuery("InsertModuleAuditTrailDetails");

        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    public DataSet GetModuleAuditTrailDetails(int? ModuleID)
    {
        ncmdbObject.AddParameter("@ModuleID", ModuleID);
      return  ncmdbObject.ExecuteDataSet("GetModuleAuditTrailDetails");
    }
}