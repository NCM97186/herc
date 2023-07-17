using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AdminPanel_ModuleAuditTrail : System.Web.UI.Page
{
    ModuleAuditTrailDL _moduleauditraildl = new ModuleAuditTrailDL();
    Project_Variables p_var = new Project_Variables();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDetail();
        }

    }
    void BindDetail()
    {
        p_var.dSet = _moduleauditraildl.GetModuleAuditTrailDetails(null);
        grdAuditReport.DataSource = p_var.dSet;
        grdAuditReport.DataBind();
    }
    protected void grdAuditReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAuditReport.PageIndex = e.NewPageIndex;
        BindDetail();
    }
}