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

public partial class Abbreation : BasePage
{
    #region variable declaration zone

    string str = string.Empty;
    LinkOB objlnkOB = new LinkOB();
    LinkBL objlnkBL = new LinkBL();
    Project_Variables P_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();

    #endregion

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["prev"] != null && Request.QueryString["prev"] != "" && Request.QueryString["prev"].ToString() == "1")
        {
          //  previous.Attributes["class"] = "current";
        }
        else
        {
           // current.Attributes["class"] = "current";
        }

        str = BreadcrumDL.DisplayMemberAreaBreadCrum(Resources.HercResource.Abbreviations);
        ltrlBreadcrum.Text = str.ToString();
        P_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/");
        if (!IsPostBack)
        {
            Bind_AnnualReport(1);
        }
    }

    #endregion


    public void Bind_AnnualReport(int pageIndex)
    {
        objlnkOB.PageIndex = pageIndex;
        objlnkOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        objlnkOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        if (ViewState["year"] != null)
        {
            objlnkOB.Year = ViewState["year"].ToString();
        }
        else
        {
            objlnkOB.Year = null;
        }
        if (Request.QueryString["Prev"] != null && ViewState["year"] == null)
        {
            P_Val.dSet = objlnkBL.Get_Annual_PrevYear(objlnkOB, out P_Val.k);
            BindYear();
        }
        else
        {
            P_Val.dSet = objlnkBL.Get_AnnualReports(objlnkOB, out P_Val.k);
        }
        if (P_Val.dSet.Tables[0].Rows.Count > 0)
        {
            lblmsg.Visible = false;
            gvAnnualReport.DataSource = P_Val.dSet;
            gvAnnualReport.DataBind();
            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
        }
        else
        {
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }

        P_Val.Result = P_Val.k;
        if (P_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(P_Val.Result, pageIndex, ddlPageSize, rptPager);
            rptPager.Visible = true;
        }
        else
        {
            rptPager.Visible = false;
        }

    }


    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_AnnualReport(1);
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Bind_AnnualReport(pageIndex);
    }

    #endregion



    protected void gvAnnualReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "ViewDoc")
        {

            string file = e.CommandArgument.ToString();
            P_Val.Path = P_Val.Path + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(P_Val.Path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(P_Val.Path));
                Response.Clear();
                Response.WriteFile(P_Val.Path);
                Response.End();
            }
        }
        if (e.CommandName == "View")
        {
            P_Val.stringTypeID = e.CommandArgument.ToString();
            P_Val.strPopupID = "<script language='javascript'>" +
                               "window.open('ViewDetails.aspx?Link_Id=" + P_Val.stringTypeID + "&" + "ModuleId= " + Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report) + "', 'mywindow', " +
                               "' menubar=no, resizable=no, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                               "</script>";
            this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
        }



    }

    protected void gvAnnualReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            string filename = DataBinder.Eval(e.Row.DataItem, "File_Name").ToString();


            if (filename == null || filename == "")
            {
                lnk.Visible = false;
            }
        }
    }


    #region function to Bind data in a data list for year

    public void BindYear()
    {
        pyear.Visible = true;
        P_Val.dSetChildData = objlnkBL.Get_YearLink();

        if (P_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            datalistYear.DataSource = P_Val.dSetChildData;
            datalistYear.DataBind();
        }

    }


    #endregion

    #region Datalist datalistYear itemCommand event zone

    protected void datalistYear_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            P_Val.str = e.CommandArgument.ToString();
            ViewState["year"] = P_Val.str;
            Bind_AnnualReport(1);
        }
    }

    #endregion
}
