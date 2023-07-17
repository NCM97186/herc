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
using System.IO;

public partial class CurrentOrderSearch : BasePage
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    OrderBL objOrderBL = new OrderBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();

    
    OrderBL objOrdersBL = new OrderBL();
    PetitionOB objPetOB = new PetitionOB();
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;

    public static string UrlPrint = string.Empty;

    #endregion

    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        current.Title = Resources.HercResource.CurrentYearOrder;
        currentHindi.Title = Resources.HercResource.CurrentYearOrder;
        previous.Title = Resources.HercResource.PreviousYearOrder;
        previousHindi.Title = Resources.HercResource.PreviousYearOrder;
    
        if (!IsPostBack)
        {
            bindYearinDdl(ddlconnectionYearWise.SelectedValue.ToString());
            BindPetitionNumber(ddlconnectionYearWise.SelectedValue.ToString(), drpyear.SelectedValue);
           
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
                gvDailyOrders.DataSource = (DataTable)Session["TempTable"];
                gvDailyOrders.DataBind();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }

        }
        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.Orders;
        }
        else
        {
            PageTitle = Resources.HercResource.Orders;
        }

        str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Orders, Resources.HercResource.OrdersSearch);

        ltrlBreadcrum.Text = str.ToString();
        p_Val.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Orders"].ToString() + "/";
    }

    #endregion

    #region Button click event zone

    protected void btnOnlineStatus_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            //Bind_search(1);


        }
    }

    #endregion

    protected void btnsearchYearwise_click(object sender, EventArgs e)
    {
        ViewState.Remove("date");
        ViewState.Remove("search");
        obj_petOB.ActionType = 1;
        obj_petOB.PageIndex = 1;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            obj_petOB.PageSize = 10000;
        }
        else
        {
            obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }
        obj_petOB.year = drpyear.SelectedValue.Trim();
        obj_petOB.PRONo = drppro.SelectedValue;

        obj_petOB.ConnectionType = Convert.ToInt16(ddlconnectionYearWise.SelectedValue);
        p_Val.dSet = objOrderBL.Get_CurrentSearchOrders(obj_petOB, out p_Val.k);
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                rptPager.Visible    = false;
                ddlPageSize.Visible = false;
                lblPageSize.Visible = false;
            }
            else
            {
                rptPager.Visible    = true;
                ddlPageSize.Visible = true;
                lblPageSize.Visible = true;
            }
            Session["TempTable"] = p_Val.dSet.Tables[0];
            gvDailyOrders.DataSource = p_Val.dSet;

        }
        else
        {
            gvDailyOrders.DataSource = null;
            rptPager.Visible    = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        gvDailyOrders.DataBind();
        p_Val.Result = p_Val.k;
        if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_Val.Result, 1, ddlPageSize, rptPager);
            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                rptPager.Visible = false;
            }
            else
            {
                rptPager.Visible = true;
            }
        }
        else
        {
            rptPager.Visible = false;
        }

    }

    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drpyear.SelectedValue != "0")
        //{
        BindPetitionNumber(ddlconnectionYearWise.SelectedValue.ToString(), drpyear.SelectedValue);
        //}
        //else
        //{
        //    drppro.Items.Insert(0, new ListItem("Select", "0"));
        //}

    }


    #region Function to bind Petition Year

    public void bindYearinDdl(string connectionType)
    {
        obj_petOB.ConnectionType = Convert.ToInt16(connectionType);
        p_Val.dSet = objOrderBL.getOrderYearForSearch(obj_petOB);

        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = p_Val.dSet;
            drpyear.DataTextField = "year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();
        }
        //else
        //{
        drpyear.Items.Insert(0, new ListItem("Select", "0"));
        //}
    }

    #endregion

    #region function to Bind data in a data list for year

    public void BindPetitionNumber(string ConnectionType, string Year)
    {

        obj_petOB.year = Year.Trim();
        obj_petOB.ConnectionType = Convert.ToInt16(ConnectionType);
        p_Val.dSetChildData = objOrderBL.getOrderPRONumbersForSearch(obj_petOB);
        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drppro.DataSource = p_Val.dSetChildData;
            drppro.DataTextField = "PRO_No";
            drppro.DataValueField = "PRO_No";
            drppro.DataBind();
        }
        else
        {
            drppro.DataSource = p_Val.dSetChildData;
            drppro.DataBind();
            drppro.Items.Insert(0, new ListItem("Select", "0"));
        }
    }


    #endregion


    protected void btndate_click(object sender, EventArgs e)
    {
        ViewState["date"] = "3";
        if (ViewState["search"] != null)
        {
            ViewState.Remove("search");
        }
        Bind_search(1, 3);




    }

    #region button btnsearch click event to search records

    protected void btnsearch_click(object sender, EventArgs e)
    {
        if (ViewState["date"] != null)
        {
            ViewState.Remove("date");
        }
        ViewState["search"] = "2";
        Bind_search(1, 2);

    }

    #endregion


    #region function to bind searched order in grid

    public void Bind_search(int pageIndex, int actionType)
    {
        try
        {
            obj_petOB.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            obj_petOB.PageIndex = pageIndex;
            obj_petOB.ActionType = actionType;
            // obj_petOB.ActionType = 2;
            if (actionType == 2)
            {
                obj_petOB.ConnectionType = Convert.ToInt16(ddlConnectionType.SelectedValue);
                obj_petOB.PRONo = null;
                if (txtname.Text != null && txtname.Text != "")
                {
                    obj_petOB.PetitionerName = txtname.Text;
                }
                else
                {
                    obj_petOB.PetitionerName = null;
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
                    obj_petOB.Date = Convert.ToDateTime(miscellBL.getDateFormat(txtDate.Text));
                }
                else
                {
                    obj_petOB.Date = null;
                }
            }

            p_Val.dsFileName = objOrderBL.Get_CurrentSearchOrders(obj_petOB, out p_Val.k);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                Session["TempTable"] = p_Val.dsFileName.Tables[0];
                gvDailyOrders.DataSource = p_Val.dsFileName;
                gvDailyOrders.DataBind();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                rptPager.Visible = true;
                ddlPageSize.Visible = true;
                lblPageSize.Visible = true;
            }
            else
            {
                gvDailyOrders.DataSource = p_Val.dsFileName;
                gvDailyOrders.DataBind();
                rptPager.Visible = false;
                ddlPageSize.Visible = false;
                lblPageSize.Visible = false;
            }
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
        catch { }
    }

    #endregion


    protected void gvDailyOrders_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "viewDetails")
        {
         
                    p_Val.stringTypeID = e.CommandArgument.ToString();
                    p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewOrderDetails.aspx?OrderId=" + p_Val.stringTypeID) + "', 'blank' + new Date().getTime()," +
                                       "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
                                       "</script>";
                    this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
           
        }

    }

    protected void gvDailyOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            string filename = DataBinder.Eval(e.Row.DataItem, "orderFile").ToString();
            Label lblPetitioner = (Label)e.Row.FindControl("lblPetitioner");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
			LinkButton lblSubject = (LinkButton)e.Row.FindControl("lblSubject");
            lblSubject.Text=HttpUtility.HtmlDecode(lblSubject.Text);
            if (lblPetitioner.Text != null && lblPetitioner.Text != "")
            {
                lblPetitioner.Text = HttpUtility.HtmlDecode(lblPetitioner.Text).Replace("&", "&amp;");
            }
            if (lblRespondent.Text != null && lblRespondent.Text != "")
            {
                lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text).Replace("&", "&amp;");
            }
            if (filename == null || filename == "")
            {
                //  lnk.Visible = false;
            }

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            objPetOB.OrderID = Convert.ToInt16(gvDailyOrders.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Val.dsFileName = objOrdersBL.getConnectedOrders(objPetOB);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
              
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Val.sbuilder.Append("<a href='" + p_Val.url + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='"+Resources.HercResource.ViewDocument+"'>");
                    if (p_Val.dsFileName.Tables[0].Rows[i]["SubCategoryName"] != DBNull.Value)
                    {
                        p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["SubCategoryName"] + ", ");
                    }
                  
                    p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + ", Dated: " + p_Val.dsFileName.Tables[0].Rows[i]["Date"]);
                    //p_Val.sbuilder.Append("<br /><a href='" + p_Val.url + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>"  + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                    p_Val.sbuilder.Append("<br />" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='"+Resources.HercResource.ViewDocument+"' width='15' alt='"+Resources.HercResource.ViewDocument+"' height=\"15\" /> ");
                    if (p_Val.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Val.url) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(p_Val.url) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }
					p_Val.sbuilder.Append("<br/>");

                }
                p_Val.sbuilder.Append("</a><hr/>");
                orderConnectedFile.Text = p_Val.sbuilder.ToString();

            }
           
        }
    }


    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["date"] != null)
        {
            ViewState.Remove("search");
            this.Bind_search(1, 3);
        }
        else
        {
            this.Bind_search(1, 2);
            ViewState.Remove("date");
        }
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        if (ViewState["date"] != null)
        {
            ViewState.Remove("search");
            this.Bind_search(pageIndex, 3);
        }
        else
        {
            this.Bind_search(pageIndex, 2);
            ViewState.Remove("date");
        }

    }

    #endregion

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion


    #region dropDownlist ddlConnectionType selectedIndexChanged events

    protected void ddlConnectionType_SelectedIndexChanged(object sender, EventArgs e)
    {

		txtname.Text = "";
        txtrespodent.Text = "";
        txtsubject.Text = "";
    }

    #endregion

    protected void ddlconnectionYearWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindYearinDdl(ddlconnectionYearWise.SelectedValue.ToString());
        BindPetitionNumber(ddlconnectionYearWise.SelectedValue.ToString(), drpyear.SelectedValue);

    }

    #region dropDownlist ddlConnectionType selectedIndexChanged events

    protected void ddlDate_SelectedIndexChanged(object sender, EventArgs e)
    {

		txtDate.Text = "";
    }

    #endregion
}
