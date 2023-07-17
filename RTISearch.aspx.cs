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

public partial class RTISearch : BasePage
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
    RtiBL obj_rtBL = new RtiBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    Miscelleneous_BL obj = new Miscelleneous_BL();
    static string strsearch;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    public static string UrlPrint = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
		if (RewriteModule.RewriteContext.Current.Params["menuid"] != null)
        {
			p_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
		}
        p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Rti"] + "/");
        str = BreadcrumDL.DisplayBreadCrumRTI(Resources.HercResource.RTI, Resources.HercResource.Searchapplication);
        ltrlBreadcrum.Text = str.ToString();
        if (!IsPostBack)
        {
            Get_Year();
            Get_Reference();
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			 ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);

            if (Session["TempTable"] != null)
            {
                GrdRTI.DataSource = (DataTable)Session["TempTable"];
                GrdRTI.DataBind();
            }
        }

        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.RTI;
        }
        else
        {
            PageTitle = Resources.HercResource.RTI;
        }
    }
    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnsearch_click(object sender, EventArgs e)
    {
        ////////////strsearch = null;// string.Empty;

        ////////////if (!string.IsNullOrEmpty(txtSubject.Text))
        ////////////{

        ////////////    // strsearch = " Subject like '%'+" + "'" + txtSubject.Text + "'" + "+'%'" + " and";
        ////////////    obj_petOB.subject = txtSubject.Text;
        ////////////}
        ////////////else
        ////////////{
        ////////////    obj_petOB.subject = null;
        ////////////}
        ////////////if (!string.IsNullOrEmpty(txtname.Text))
        ////////////{
        ////////////    //strsearch += " Applicant_Name like '%'+" + "'" + txtname.Text + "'" + "+'%'" + " and";
        ////////////    obj_petOB.ApplicantName = txtname.Text;
        ////////////}
        ////////////else
        ////////////{
        ////////////    obj_petOB.ApplicantName = null;
        ////////////}

        ////////////obj_petOB.RefNo = null;
        ////////////obj_petOB.year = null;
        //if (!string.IsNullOrEmpty(strsearch))
        //{
        //    strsearch = strsearch.Substring(0, strsearch.Length - 3);
        //}

        //obj_petOB.PageIndex = 1;
        //obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        //obj_petOB.DepttId = 1;
        //p_Val.dSet = obj_rtBL.Serch_RTI(obj_petOB);

        //if (p_Val.dSet.Tables[0].Rows.Count > 0)
        //{
        //    Session["TempTable"] = p_Val.dSet.Tables[0];
        //    GrdRTI.Visible = true;
        //    GrdRTI.DataSource = p_Val.dSet;

        //    rptPager.Visible = true;
        //    ddlPageSize.Visible = true;
        //    lblPageSize.Visible = true;
        //}
        //else
        //{
        //    GrdRTI.Visible = true;
        //    GrdRTI.DataSource = null;
        //    GrdRTI.DataBind();
        //    rptPager.Visible = false;
        //    ddlPageSize.Visible = false;
        //    lblPageSize.Visible = false;
        //}
        //GrdRTI.DataBind();
        //p_Val.Result = Convert.ToInt32(p_Val.dSet.Tables[1].Rows[0][0]);
        //if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        //{
        //    pagingBL.Paging_Show(p_Val.Result, 1, ddlPageSize, rptPager);
        //    rptPager.Visible = true;
        //}
        //else
        //{
        //    rptPager.Visible = false;
        //}

         Bind_RTI_Search(1);

    }



    protected void btnsearchYearwise_click(object sender, EventArgs e)
    {
        strsearch = null;// string.Empty;
        if (drpyear.SelectedValue != "0" || drpreference.SelectedValue != "0")
        {
            if (!string.IsNullOrEmpty(drpyear.SelectedValue))
            {
                //strsearch = "Year like '" + drpyear.SelectedValue + "'" + " and";
                obj_petOB.year = drpyear.SelectedValue;
            }
            else
            {
                obj_petOB.year = null;
            }
            if (!string.IsNullOrEmpty(drpreference.SelectedValue))
            {
                //strsearch += " Ref_No like" + "'" + drpreference.SelectedValue + "'" + " and";
                obj_petOB.RefNo = drpreference.SelectedValue;
            }
            else
            {
                obj_petOB.RefNo = null;
            }
            obj_petOB.ApplicantName = null;
            obj_petOB.subject = null;
            if (!string.IsNullOrEmpty(strsearch))
            {
                strsearch = strsearch.Substring(0, strsearch.Length - 3);
            }


            obj_petOB.PageIndex = 1;
            obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
            obj_petOB.DepttId = 1;
             //p_Val.dSet = obj_rtBL.Serch_RTI(obj_petOB);
			 p_Val.dSet = obj_rtBL.Serch_RTI(obj_petOB, out p_Val.k);
            if (p_Val.dSet.Tables[0].Rows.Count > 0)
            {
                Session["TempTable"] = p_Val.dSet.Tables[0];
                GrdRTI.Visible = true;
                GrdRTI.DataSource = p_Val.dSet;

                rptPager.Visible = true;
                ddlPageSize.Visible = true;
                lblPageSize.Visible = true;
            }
            else
            {
                GrdRTI.Visible = true;
                GrdRTI.DataSource = null;
                GrdRTI.DataBind();
                rptPager.Visible = false;
                ddlPageSize.Visible = false;
                lblPageSize.Visible = false;
            }
            GrdRTI.DataBind();
			p_Val.Result = p_Val.k;
            //p_Val.Result = Convert.ToInt32(p_Val.dSet.Tables[1].Rows[0][0]);
            if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
            {
                pagingBL.Paging_Show(p_Val.Result, 1, ddlPageSize, rptPager);
                rptPager.Visible = true;
            }
            else
            {
                rptPager.Visible = false;
            }
        }

        else
        {
            GrdRTI.Visible = false;
            rptPager.Visible = false;
        }
    }
    public void Bind_RTI_Search(int pageIndex)
    {
        if (!string.IsNullOrEmpty(txtSubject.Text))
        {

            obj_petOB.subject = txtSubject.Text;
        }
        else
        {
            obj_petOB.subject = null;
        }
        if (!string.IsNullOrEmpty(txtname.Text))
        {
            obj_petOB.ApplicantName = txtname.Text;
        }
        else
        {
            obj_petOB.ApplicantName = null;
        }

        obj_petOB.year = null;
        obj_petOB.RefNo = null;

        obj_petOB.PageIndex = pageIndex;
        obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        obj_petOB.DepttId = 1;
        //p_Val.dSet = obj_rtBL.Serch_RTI(obj_petOB);
		 p_Val.dSet = obj_rtBL.Serch_RTI(obj_petOB, out p_Val.k);
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            Session["TempTable"] = p_Val.dSet.Tables[0];
            GrdRTI.Visible = true;
            GrdRTI.DataSource = p_Val.dSet;

            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
        }
        else
        {
            GrdRTI.Visible = true;
            GrdRTI.DataSource = null;
            GrdRTI.DataBind();
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        GrdRTI.DataBind();
        //p_Val.Result = Convert.ToInt32(p_Val.dSet.Tables[1].Rows[0][0]);
		p_Val.Result = p_Val.k;
        if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_Val.Result, pageIndex, ddlPageSize, rptPager);
            rptPager.Visible = true;
        }
        else
        {
            rptPager.Visible = false;
        }


    }
    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        strsearch = null;// string.Empty;
        this.Bind_RTI_Search(pageIndex);
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {

        this.Bind_RTI_Search(1);
    }

    #endregion


    protected void GrdRTI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int RTID = Convert.ToInt32(((HiddenField)e.Row.FindControl("hyd")).Value);
            int RtistatusId = Convert.ToInt32(((HiddenField)e.Row.FindControl("status")).Value);
            Label lblApplicant = (Label)e.Row.FindControl("lblApplicant");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");
            lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            lblApplicant.Text = HttpUtility.HtmlDecode(lblApplicant.Text);
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);

            //use for format of Reply sent status
            obj_petOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            obj_petOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_petOB.RTIId = RTID;
            p_Val.dSetChildData = obj_rtBL.Get_RTIById(obj_petOB);
            if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
            {
                if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.replysent))
                {
                    ((Label)e.Row.FindControl("lblstatus")).Text = obj.FixGivenCharacters(((Label)e.Row.FindControl("lblstatus")).Text + "vide " + "<br/>Memo No:" + " " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated:" + " " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString(), 100);
                }
                if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.TransferedAuthority))
                {
                    ((Label)e.Row.FindControl("lblstatus")).Text = obj.FixGivenCharacters(((Label)e.Row.FindControl("lblstatus")).Text + ": " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + " vide " + "<br/>Memo No:" + " " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated:" + " " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString(), 100);
                }
            }
            Label lblyear = (Label)e.Row.FindControl("lblyear");
            ((LinkButton)e.Row.FindControl("lblrefno")).Text = ((LinkButton)e.Row.FindControl("lblrefno")).Text + " of " + lblyear.Text;
            obj_rtBL.Check_RTI_FAA(RTID, out p_Val.i);

            if (p_Val.i == 0)
            {
                ((LinkButton)e.Row.FindControl("lnklink")).Visible = false;
                ((LinkButton)e.Row.FindControl("lnklink")).Text = "N";
            }
            else
            {
                if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.inprocess))
                {
                    ((LinkButton)e.Row.FindControl("lnklink")).Visible = false;
                }
                else
                {
                    ((LinkButton)e.Row.FindControl("lnklink")).Visible = true;
                    ((LinkButton)e.Row.FindControl("lnklink")).Text = "YES";
                }
            }

            if (string.IsNullOrEmpty(((HiddenField)e.Row.FindControl("hdfFile")).Value))
            {
                ((LinkButton)e.Row.FindControl("lbl_ViewDoc")).Visible = false;
            }
            // e.Row.Cells[4].Text = HttpUtility.HtmlDecode(e.Row.Cells[4].Text);
            // Division of characters start from here
            //////////////e.Row.Cells[3].Text = obj.Division_characters(e.Row.Cells[3].Text, 10);

            //////////////e.Row.Cells[8].Text = obj.Division_characters(e.Row.Cells[8].Text, 10);
            // End
        }
    }

    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpyear.SelectedValue != "0")
        {
            Get_Reference();
        }
        else
        {
            drpreference.Items.Insert(0, new ListItem("Select", "0"));
        }

    }


    public void Get_Year()
    {
        try
        {
            obj_petOB.DepttId = 1;
            p_Val.dSet = obj_rtBL.GetYearRTI(obj_petOB);
            if (p_Val.dSet.Tables[0].Rows.Count > 0)
            {
                drpyear.DataSource = p_Val.dSet;
                drpyear.DataValueField = "Year";
                drpyear.DataTextField = "Year";
                drpyear.DataBind();

            }
            drpyear.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch
        {
            throw;
        }
    }



    public void Get_Reference()
    {
        try
        {
            if (drpyear.SelectedValue != "0")
            {
                obj_petOB.year = drpyear.SelectedValue.ToString();
            }
            else
            {
                obj_petOB.year = null;
            }
            obj_petOB.DepttId = 1;
            p_Val.dSet = obj_rtBL.getReferenceNumberByYear(obj_petOB);
            if (p_Val.dSet.Tables[0].Rows.Count > 0)
            {
                drpreference.DataSource = p_Val.dSet;
                drpreference.DataValueField = "Ref_No";
                drpreference.DataTextField = "Ref_No";
                drpreference.DataBind();

            }
            else
            {
                drpreference.DataSource = p_Val.dSet;
                drpreference.DataBind();

            }

            drpreference.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch
        {
            throw;
        }
    }

    protected void GrdRTI_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDoc")
        {

            string file = e.CommandArgument.ToString();
            p_Val.Path = p_Val.Path + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(p_Val.Path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(p_Val.Path));
                Response.Clear();
                Response.WriteFile(p_Val.Path);
                Response.End();
            }
        }
        if (e.CommandName == "Vdetail")
        {
           
                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                    {

                        p_Val.stringTypeID = e.CommandArgument.ToString();
                        p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewRTIDetails/" + e.CommandArgument + "_" + p_Val.PageID + "_" + p_Val.position) + ".aspx" + "', 'blank' + new Date().getTime()," +
                                           "'menubar=no, resizable=no, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                           "</script>";
                        this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                    }
                    else
                    {
                        p_Val.stringTypeID = e.CommandArgument.ToString();
                        p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "Content/Hindi/ViewRTIDetails/" + e.CommandArgument + "_" + p_Val.PageID + "_" + p_Val.position) + ".aspx" + "', 'blank' + new Date().getTime()," +
                                           "'menubar=no, resizable=no, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                           "</script>";
                        this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
               
            
        }
    }

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion
}
