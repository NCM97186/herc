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

public partial class SearchSOH : BasePage
{
    #region variable declaration zone

    PetitionOB obj_petOB = new PetitionOB();
    PetitionBL objpetBL = new PetitionBL();
    Project_Variables p_val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    Miscelleneous_BL miscelobj = new Miscelleneous_BL();
    string str = string.Empty;
    public static string UrlPrint = string.Empty;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;
    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
          str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.CLSH, Resources.HercResource.Search);
          ltrlBreadcrum.Text = str.ToString();
          if (!IsPostBack)
          {
              BindYear(ddlconnectionYearWise.SelectedValue.ToString());
              BindPetitionNumber(ddlconnectionYearWise.SelectedValue.ToString(),drpyear.SelectedValue);
            
              ddlPageSize.Visible = false;
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
                  gvSOH.DataSource = (DataTable)Session["TempTable"];
                  gvSOH.DataBind();
              }
          }

          if (Resources.HercResource.Lang_Id == "1")
          {
              PageTitle = Resources.HercResource.ScheduleOfHearings;
          }
          else
          {
              PageTitle = Resources.HercResource.ScheduleOfHearings;
          }
    }

    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    #region Button click event zone

    protected void btnOnlineStatus_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

           // Bind_Petitionerserch(1);

        }
    }

    #endregion

    #region function to Bind data in a data list for year

    public void BindPetitionNumber(string ConnectionType,string Year)
    {

        obj_petOB.year = Year.Trim();
        obj_petOB.ConnectionType = Convert.ToInt16(ConnectionType);
        p_val.dSetChildData = objpetBL.Get_PetitionNumberSOHSearch(obj_petOB);
        if (p_val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drppro.DataSource = p_val.dSetChildData;
            drppro.DataTextField = "PRO_No";
            drppro.DataValueField = "PRO_No";
            drppro.DataBind();
        }
        else
        {
            drppro.DataSource = p_val.dSetChildData;
            drppro.DataBind();
            drppro.Items.Insert(0, new ListItem("Select", "0"));
        }
    }


    #endregion

    #region function to bind search petition in gridview

    public void Bind_Petitionerserch(int pageIndex, int actionType)
    {

        obj_petOB.ActionType = actionType;
        obj_petOB.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        obj_petOB.PageIndex = pageIndex;

        if (actionType == 2)
        {
            obj_petOB.ConnectionType =Convert.ToInt16(ddlConnectionType.SelectedValue);
            obj_petOB.PRONo = null;
            if (txtname.Text != null && txtname.Text != "")
            {
                obj_petOB.ApplicantName = txtname.Text;
            }
            else
            {
                obj_petOB.ApplicantName = null;
            }
            if (txtrespodent.Text != null && txtrespodent.Text != "")
            {
                obj_petOB.RespondentName = txtrespodent.Text;
            }
            else
            {
                obj_petOB.RespondentName = null;
            }
            if (txtsubject.Text != null && txtsubject.Text != "")
            {
                obj_petOB.subject = txtsubject.Text;
            }
            else
            {
                obj_petOB.subject = null;
            }
        }
        else if (actionType == 3)
        {
            obj_petOB.ConnectionType = Convert.ToInt16(ddlDate.SelectedValue);
            if (txtDate.Text != null && txtDate.Text != "")
            {
                if (Resources.HercResource.Lang_Id == "1")
                {
                    obj_petOB.Date = Convert.ToDateTime(miscelobj.getDateFormat(txtDate.Text));
                }
                else
                {
                    obj_petOB.Date = Convert.ToDateTime(txtDate.Text);
                }
            }
            else
            {
                obj_petOB.Date = null;
            }
        }

        p_val.dsFileName = objpetBL.Get_SOHSearch(obj_petOB, out p_val.k);
        if (p_val.dsFileName.Tables[0].Rows.Count > 0)
        {
            Session["TempTable"] = p_val.dsFileName.Tables[0];
            gvSOH.DataSource = p_val.dsFileName;
            gvSOH.DataBind();

            MetaKeyword = p_val.dsFileName.Tables[0].Rows[0]["MetaKeywords"].ToString();
            MetaDescription = p_val.dsFileName.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            MetaLng = p_val.dsFileName.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = p_val.dsFileName.Tables[0].Rows[0]["MetaTitle"].ToString();

            for (int i = 0; i < gvSOH.Rows.Count; i++)
            {
                Label lblNumber = gvSOH.Rows[i].FindControl("lblnumber") as Label;


                if (p_val.dsFileName.Tables[0].Rows[i]["PRO_No"] == DBNull.Value)
                {

                    lblNumber.Text = p_val.dsFileName.Tables[0].Rows[i]["RP_No"].ToString();
                }
                else
                {
                    lblNumber.Text = p_val.dsFileName.Tables[0].Rows[i]["PRO_No"].ToString();

                }
            }
            
            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
        }
        else
        {
            gvSOH.DataSource = p_val.dsFileName;
            gvSOH.DataBind();
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

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["date"] != null)
        {
            ViewState.Remove("search");
            this.Bind_Petitionerserch(1, 3);
        }
        else
        {
            this.Bind_Petitionerserch(1, 2);
            ViewState.Remove("date");
        }
        //this.Bind_Petitionerserch(1);
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        if (ViewState["date"] != null)
        {
            ViewState.Remove("search");
            this.Bind_Petitionerserch(pageIndex, 3);
        }
        else
        {
            this.Bind_Petitionerserch(pageIndex, 2);
            ViewState.Remove("date");
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
        if (e.CommandName == "ViewDetails")
        {
           
                    p_val.stringTypeID = e.CommandArgument.ToString();
                    p_val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewDetails.aspx?Soh_id=" + p_val.stringTypeID) + "', 'blank' + new Date().getTime()," +
                                       "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                       "</script>";
                    this.Page.RegisterStartupScript("PopupScript", p_val.strPopupID);
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
           
        }
    }
    protected void gvSOH_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            Label lblPetitioner = (Label)e.Row.FindControl("lblPetitioner");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            LinkButton lblSubject = (LinkButton)e.Row.FindControl("lblSubject");
            lblPetitioner.Text = HttpUtility.HtmlDecode(lblPetitioner.Text).Replace("&","&amp;");
            lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text).Replace("&", "&amp;");
            string filename = DataBinder.Eval(e.Row.DataItem, "soh_file").ToString();
       
			Label lblRemarksSoh = (Label)e.Row.FindControl("lblRemarksSoh");
            if (lblRemarksSoh.Text != null && lblRemarksSoh.Text != "")
            {
                lblRemarksSoh.Text = HttpUtility.HtmlDecode(lblRemarksSoh.Text).Replace("&", "&amp;");
            }

            if (filename == null || filename == "")
            {
                lnk.Visible = false;
            }
            if (lblSubject.Text != null && lblSubject.Text != "")
            {
                lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text).Replace("&", "&amp;");
            }
        }
    }

    protected void btndate_click(object sender, EventArgs e)
    {
        ViewState["date"] = "3";
        if (ViewState["search"] != null)
        {
            ViewState.Remove("search");
        }
        Bind_Petitionerserch(1, 3);
        
    }


    protected void btnsearch_click(object sender, EventArgs e)
    {
        if (ViewState["date"] != null)
        {
            ViewState.Remove("date");
        }
        ViewState["search"] = "2";
        Bind_Petitionerserch(1,2);
    }


    protected void btnsearchYearwise_click(object sender, EventArgs e)
    {
        ViewState.Remove("date");
        ViewState.Remove("search");
        obj_petOB.ActionType = 1;
        obj_petOB.PageIndex = 1;
       
        obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        obj_petOB.ConnectionType = Convert.ToInt16(ddlconnectionYearWise.SelectedValue);
        obj_petOB.year = drpyear.SelectedValue.Trim();
        obj_petOB.PRONo = drppro.SelectedValue;

        p_val.dsFileName = objpetBL.Get_SOHSearch(obj_petOB, out p_val.k);
        if (p_val.dsFileName.Tables[0].Rows.Count > 0)
        {
            Session["TempTable"] = p_val.dsFileName.Tables[0];
            gvSOH.DataSource = p_val.dsFileName;
            gvSOH.DataBind();
            for (int i = 0; i < gvSOH.Rows.Count; i++)
            {
                Label lblNumber = gvSOH.Rows[i].FindControl("lblnumber") as Label;


                if (p_val.dsFileName.Tables[0].Rows[i]["PRO_No"] == DBNull.Value)
                {

                    lblNumber.Text = p_val.dsFileName.Tables[0].Rows[i]["RP_No"].ToString();
                }
                else
                {
                    lblNumber.Text = p_val.dsFileName.Tables[0].Rows[i]["PRO_No"].ToString();

                }
            }
            MetaKeyword = p_val.dsFileName.Tables[0].Rows[0]["MetaKeywords"].ToString();
            MetaDescription = p_val.dsFileName.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            MetaLng = p_val.dsFileName.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = p_val.dsFileName.Tables[0].Rows[0]["MetaTitle"].ToString();
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        else
        {
            gvSOH.DataSource = p_val.dsFileName;
            gvSOH.DataBind();
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        p_val.Result = p_val.k;
       


    }

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion


    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        
            BindPetitionNumber(ddlconnectionYearWise.SelectedValue,drpyear.SelectedValue);
      

    }


    #region function to Bind data in a data list for year

    public void BindYear(string connectionType )
    {

        obj_petOB.ConnectionType = Convert.ToInt16(connectionType);
        obj_petOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
        p_val.dSetChildData = objpetBL.Get_YearSOHSearchHerc(obj_petOB);

        if (p_val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = p_val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();
        }
        else
        {
            drpyear.DataSource = p_val.dSetChildData;
            drpyear.DataBind();
        }
        drpyear.Items.Insert(0, new ListItem("Select", "0"));

    }

    #endregion





    protected void ddlconnectionYearWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindYear(ddlconnectionYearWise.SelectedValue.ToString());
        BindPetitionNumber(ddlconnectionYearWise.SelectedValue.ToString(), drpyear.SelectedValue);

    }

    protected void ddlConnectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtname.Text = "";
        txtrespodent.Text = "";
        txtsubject.Text = "";
    }
    protected void ddlDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDate.Text = "";
    }
}
