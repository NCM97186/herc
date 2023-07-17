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

public partial class Auth_AdminPanel_ViewAuditTrailReport : System.Web.UI.Page
{
    LinkBL objlnkBL = new LinkBL();
   
    Project_Variables p_Var = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            lblmsg.Visible = false;
            bindAuditReport("","");
        }
    }
    public void bindAuditReport(string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;
        p_Var.dSet = objlnkBL.getAuditReport(out p_Var.k);
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
    protected void grdAuditReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbloldval = (Label)e.Row.FindControl("lbloldval");
            Label lblNewval = (Label)e.Row.FindControl("lblNewval");
            
            if (lbloldval.Text != null && lbloldval.Text != "")
            {
                lbloldval.Text = HttpUtility.HtmlDecode(lbloldval.Text);
            }
            if (lblNewval.Text != null && lblNewval.Text != "")
            {
                lblNewval.Text = HttpUtility.HtmlDecode(lblNewval.Text);
            }
        }
    }
}
