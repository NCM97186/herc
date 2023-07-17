using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ModuleAuditTrail
/// </summary>
public class ModuleAuditTrail
{
    public ModuleAuditTrail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int? UserId {get; set; }
    public string UserName { get; set; }
    public string IpAddress { get; set; }
    public string ActionType { get; set; }
    public string Title { get; set; }
    public int?  ModuleID { get; set; }

}