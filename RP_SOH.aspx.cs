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

public partial class RP_SOH : BasePage
{
    #region variable declaration zone

    PetitionOB objpetOB = new PetitionOB();
    PetitionBL objpetBL = new PetitionBL();
    Project_Variables p_val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    string str = string.Empty;
    public static string UrlPrint = string.Empty;

    #endregion 

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Prev"] != null)
        {
            str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.CLSH, Resources.HercResource.PreviousYears);
        }
        else
        {
            str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.CLSH, Resources.HercResource.CurrentYear);
        }
        ltrlBreadcrum.Text = str.ToString();


        if (!IsPostBack)
        {
            if (Page.Request.QueryString["date"] != null)
            {
                bindSOH_by_calander(1);
            }
            else
            {
                bindSOH(1);

            }
        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			 ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);

        }
    }

    #endregion

    #region function to bind SOH Details

    public void bindSOH(int pageIndex)
    {
        objpetOB.PageIndex = pageIndex;
        objpetOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        objpetOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        objpetOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
        if (ViewState["year"] != null)
        {
            objpetOB.year = ViewState["year"].ToString();
        }
        else
        {
            objpetOB.year = null;
        }
        if (Request.QueryString["Prev"] != null || ViewState["year"] != null)
        {
            previous.Attributes.Add("class", "current");
            p_val.dSet = objpetBL.Get_PreviousYearSOHReviewPetition(objpetOB, out p_val.k);
            BindYear();
        }
        else
        {
            current.Attributes.Add("class", "current");
            p_val.dSet = objpetBL.Get_CurrentSOHReviewPetition(objpetOB, out p_val.k);
        }
        if (p_val.dSet.Tables[0].Rows.Count > 0)
        {
            gvSOH.DataSource = p_val.dSet;
            gvSOH.DataBind();
            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
            lblmsg.Visible = false;
        }
        else
        {
            
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
            //pyear.Visible = false;

        }
        p_val.Result = p_val.k;
        if (p_val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_val.Result, pageIndex, ddlPageSize, rptPager);
            rptPager.Visible = true;
        }
        else
        {
            rptPager.Visible = false;
        }
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Page.Request.QueryString["date"] != null)
        {
            this.bindSOH_by_calander(1);
        }
        else
        {
            this.bindSOH(1);

        }
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

        if (Page.Request.QueryString["date"] != null)
        {
            this.bindSOH_by_calander(pageIndex);
        }
        else
        {
            this.bindSOH(pageIndex);

        }

    }

    #endregion

    #region function to bind SOH Details

    public void bindSOH_by_calander(int pageIndex)
    {
        objpetOB.PageIndex = pageIndex;
        objpetOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        objpetOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        objpetOB.Date = Convert.ToDateTime(Request.QueryString["date"]);
        objpetOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
        p_val.dSet = objpetBL.Get_SOH_By_calander(objpetOB, out p_val.k);
        if (p_val.dSet.Tables[0].Rows.Count > 0)
        {
            gvSOH.DataSource = p_val.dSet;
            gvSOH.DataBind();
            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
        }
        else
        {
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        p_val.Result = p_val.k;
        if (p_val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_val.Result, pageIndex, ddlPageSize, rptPager);
            rptPager.Visible = true;
        }
        else
        {
            rptPager.Visible = false;
        }
    }

    #endregion


    #region function to Bind data in a data list for year

    public void BindYear()
    {
        pyear.Visible = true;
        objpetOB.PetitionType = 2;
        objpetOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
        p_val.dSetChildData = objpetBL.Get_YearSOH(objpetOB);

        if (p_val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            datalistYear.DataSource = p_val.dSetChildData;
            datalistYear.DataBind();
        }

    }


    #endregion

    #region Datalist datalistYear itemCommand event zone

    protected void datalistYear_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            p_val.str = e.CommandArgument.ToString();
            ViewState["year"] = p_val.str;
            bindSOH(1);
        }
    }

    #endregion


    protected void gvSOH_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDoc")
        {
            p_val.url = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["scheduleofHearing"] + "/");
            string file = e.CommandArgument.ToString();
            p_val.url = p_val.url + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(p_val.url);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(p_val.url));
                Response.Clear();
                Response.WriteFile(p_val.url);
                Response.End();
            }


        }
    }
    protected void gvSOH_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            string filename = DataBinder.Eval(e.Row.DataItem, "soh_file").ToString();
            ////////string temID = Convert.ToString(e.Row.Cells[1].Text);
            ////////string year = Convert.ToString(e.Row.Cells[2].Text);

            ////////temID = "HERC/PRO-" + temID + " of " + year;
            ////////e.Row.Cells[1].Text = temID;
            if (filename == null || filename == "")
            {
                lnk.Visible = false;
            }
        }
    }

    protected void lnkreview_Click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect("~/SOH.aspx");
        }
        else
        {
            Response.Redirect("~/Content/Hindi/SOH.aspx");
        }
        
    }

}
