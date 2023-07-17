using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Auth_AdminPanel_ViewAuditTrailLoginLogoffReport : System.Web.UI.Page
{
    #region Data declaration zone

    LinkBL objlnkBL = new LinkBL();
    LinkOB objlnkOB = new LinkOB();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    UserOB usrObject = new UserOB();
    PaginationBL pagingBL = new PaginationBL();

    #endregion

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": View Login/Logoff Reports";
        this.Page.Title = "Login/Logoff Reports: HERC";
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            lblmsg.Visible = false;
            bindAuditReport("","");
        }
    }

    #endregion

    public void bindAuditReport(string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;
        //objlnkOB.PageIndex = pageIndex;
       // objlnkOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        p_Var.dSet = objlnkBL.getAuditLoginLogoffReport(out p_Var.k);

        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = p_Var.dSet.Tables[0].DefaultView;

            if (sortExp != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            }

            grdAuditReport.DataSource = myDataView;
            grdAuditReport.DataBind();
        }
        else
        {
            //rptPager.Visible = false;
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = "No record found.";
        }

       p_Var.Result = p_Var.k;
      

    }

   

    protected void grdAuditReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAuditReport.PageIndex = e.NewPageIndex;
        bindAuditReport(ViewState["e"].ToString(), ViewState["o"].ToString());
    }
    protected void grdAuditReport_Sorting(object sender, GridViewSortEventArgs e)
    {
        bindAuditReport(e.SortExpression, sortOrder);
    }


    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }
    protected void btnDeleteDetails_Click(object sender, EventArgs e)
    {
        DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
        usrObject.RecUpdateDate = date;
        p_Var.Result = objlnkBL.DeleteLoginLogoffDetails(usrObject);
        if (p_Var.Result > 0)
        {
            Session["msg"] = "Records has been deleted successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/ViewAuditTrailLoginLogoffReport.aspx";
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

        }

    }
}
