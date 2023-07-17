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

public partial class CurrentYearOrders : BasePage
{
    #region variable declaration zone

    string str = string.Empty;
    Project_Variables p_Val = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    OrderBL objOrdersBL = new OrderBL();
    PetitionOB objPetOB = new PetitionOB();
    PaginationBL pagingBL = new PaginationBL();
    public static string UrlPrint = string.Empty;
    public string lastUpdatedDate = string.Empty;

    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

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
        p_Val.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Orders"].ToString() + "/";
        //str = BreadcrumDL.DisplayMemberAreaBreadCrum(Resources.HercResource.Orders);
        bool IsPageRefresh = false;

        if (RewriteModule.RewriteContext.Current.Params["PrevYear"] != null)
        {
            str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Orders, Resources.HercResource.PreviousYearOrder);
            hOrder.InnerHtml = Resources.HercResource.PreviousYearOrder;
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                previous.Attributes["class"] = "current";
            }
            else
            {
                previousHindi.Attributes["class"] = "current";
            }
        }
        else
        {
            str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Orders, Resources.HercResource.CurrentYearOrder);
            hOrder.InnerHtml = Resources.HercResource.CurrentYearOrder;
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                current.Attributes["class"] = "current";
            }
            else
            {
                currentHindi.Attributes["class"] = "current";
            }

        }
        ltrlBreadcrum.Text = str.ToString();
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            ViewState["year"] = Session["yearnew"];
            //  pyear.Visible = true;
            Bind_Orders(1);
            drpyear.Enabled = false;
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);

        }
        else if (!IsPostBack)
        {
            Session.Remove("yearnew");
            ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();

            Session["SessionId"] = ViewState["ViewStateId"].ToString();
            if (string.IsNullOrEmpty(Request.QueryString["format"]))
            {

                Bind_Orders(1);
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }

        }
        else
        {
            if (ViewState["ViewStateId"] != null && Session["SessionId"] != null)
            {
                if (ViewState["ViewStateId"].ToString() != Session["SessionId"].ToString())
                {
                    IsPageRefresh = true;
                }
            }
            Session["SessionId"] = System.Guid.NewGuid().ToString();
            ViewState["ViewStateId"] = Session["SessionId"].ToString();
        }
        if (ViewState["lastUpdateDate"] != null)
        {
            lastUpdatedDate = ViewState["lastUpdateDate"].ToString();
            if (IsPageRefresh == true)
            {
                ViewState["year"] = Session["yearnew"];
                Bind_Orders(1);
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

    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Orders(1);
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        ViewState["pageIndex"] = pageIndex;
        this.Bind_Orders(pageIndex);
    }

    #endregion

    #region function to Bind data in a data list for year

    public void BindYear()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            if (RewriteModule.RewriteContext.Current.Params["PrevYear"] != null)
            {
                pyear.Visible = true;
            }
            else
            {
                pyear.Visible = false;
            }
        }
        else
        {
            pyear.Visible = true;
        }
        objPetOB.OrderTypeID = 9;
        p_Val.dSetChildData = objOrdersBL.Get_YearOrder(objPetOB);

        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();
        }

    }


    #endregion


    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["year"] = drpyear.SelectedValue;
        Session["yearnew"] = drpyear.SelectedValue;
        Bind_Orders(1);
    }


    protected void gvDailyOrders_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDoc")
        {

            string file = e.CommandArgument.ToString();
            p_Val.url = p_Val.url + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(p_Val.url);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(p_Val.url));
                Response.Clear();
                Response.WriteFile(p_Val.url);
                Response.End();
            }
        }
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

            lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
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
                    p_Val.sbuilder.Append("<a href='" + p_Val.url + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='" + Resources.HercResource.ViewDocument + "'>");
                    if (p_Val.dsFileName.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value)
                    {
                        p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + ", ");

                        if (p_Val.dsFileName.Tables[0].Rows[i]["SubCategoryName"] != DBNull.Value)
                        {
                            p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["SubCategoryName"] + ", ");
                        }
                        if (p_Val.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Val.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                        {
                            p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        }

                        p_Val.sbuilder.Append("Dated: " + p_Val.dsFileName.Tables[0].Rows[i]["Date"]);

                        p_Val.sbuilder.Append("<br />" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='" + Resources.HercResource.ViewDocument + "' width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> ");

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
                    else
                    {
                        //This is for amendment/clarification etc
                        p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");


                        p_Val.sbuilder.Append("<br />" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" />");
                        if (p_Val.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Val.url) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {

                                FileInfo finfo = new FileInfo(Server.MapPath(p_Val.url) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Val.sbuilder.Append(miscellBL.fileSize(FileInBytes));
                            }
                        }

                        p_Val.sbuilder.Append("<br/>");
                    }
                }
                p_Val.sbuilder.Append("</a><hr/>");
                orderConnectedFile.Text = p_Val.sbuilder.ToString();

            }

            //End
        }
    }


    #region function to bind orders in gridview

    public void Bind_Orders(int pageIndex)
    {
        try
        {
            objPetOB.PageIndex = pageIndex;
            if (Request.QueryString["format"] != null && Request.QueryString["format"] != "")
            {
                objPetOB.PageSize = 10000;
            }
            else
            {
                objPetOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
            }

            if (ViewState["year"] != null)
            {
                objPetOB.year = ViewState["year"].ToString();
            }
            else
            {
                //objPetOB.year = null;
                objPetOB.year = Convert.ToString(Convert.ToInt32(System.DateTime.Now.Year) - 1);
            }
            if (RewriteModule.RewriteContext.Current.Params["PrevYear"] != null && ViewState["year"] == null)
            {
                p_Val.dSet = objOrdersBL.Get_CurrentOrderPrevious(objPetOB, out p_Val.k);
                BindYear();
            }
            else if (ViewState["year"] != null)
            {
                p_Val.dSet = objOrdersBL.Get_CurrentOrderPrevious(objPetOB, out p_Val.k);
                BindYear();
                drpyear.SelectedValue = ViewState["year"].ToString();
            }
            else
            {
                p_Val.dSet = objOrdersBL.Get_CurrentOrder(objPetOB, out p_Val.k);
            }
            if (p_Val.dSet.Tables[0].Rows.Count > 0)
            {
                gvDailyOrders.DataSource = p_Val.dSet;
                gvDailyOrders.DataBind();

                MetaKeyword = p_Val.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
                MetaDescription = p_Val.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
                MetaLng = p_Val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
                MetaTitles = p_Val.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();

                for (int i = 0; i < gvDailyOrders.Rows.Count; i++)
                {
                    Label lblNumber = gvDailyOrders.Rows[i].FindControl("lblnumber") as Label;

                    if (p_Val.dSet.Tables[0].Rows[i]["PRO_No"] == DBNull.Value)
                    {
                        lblNumber.Text = p_Val.dSet.Tables[0].Rows[i]["RP_No"].ToString();


                    }
                    else
                    {
                        lblNumber.Text = p_Val.dSet.Tables[0].Rows[i]["PRO_No"].ToString();

                    }
                }
                lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
                ViewState["lastUpdateDate"] = lastUpdatedDate;
                if (Request.QueryString["format"] != null && Request.QueryString["format"] != "")
                {
                    rptPager.Visible = false;
                    ddlPageSize.Visible = false;
                    lblPageSize.Visible = false;
                }
                else
                {
                    rptPager.Visible = true;
                    ddlPageSize.Visible = true;
                    lblPageSize.Visible = true;
                }
            }
            else
            {
                lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
                lblmsg.ForeColor = System.Drawing.Color.Red;
                rptPager.Visible = false;
                ddlPageSize.Visible = false;
                lblPageSize.Visible = false;
                pyear.Visible = false;
            }
            p_Val.Result = p_Val.k;
            if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
            {
                pagingBL.Paging_Show(p_Val.Result, pageIndex, ddlPageSize, rptPager);
                if (Request.QueryString["format"] != null && Request.QueryString["format"] != "")
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
        catch { }
    }

    #endregion

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
        MetaLang = MetaLng;
        MetaTitle = MetaTitles;
    }

    #endregion



    protected void rptPager_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkPage = (LinkButton)e.Item.FindControl("lnkPage");
            if (lnkPage.Text == ">>")
            {
                lnkPage.ToolTip = "Last";
            }
            else if (lnkPage.Text == "<<")
            {
                lnkPage.ToolTip = "First";
            }
            else
            {
                lnkPage.ToolTip = lnkPage.Text;
            }
        }
    }
}
