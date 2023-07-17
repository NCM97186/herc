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

public partial class CategorywiseOrders : BasePage
{
    #region variable declaretion

    PetitionOB orderObject = new PetitionOB();
    OrderBL orderBL = new OrderBL();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    UserOB obj_userOB = new UserOB();
    LinkBL obj_LinkBL = new LinkBL();
    PaginationBL pagingBL = new PaginationBL();
    string str = string.Empty;
    public static string UrlPrint = string.Empty;

    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;

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

        p_Var.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Orders"].ToString() + "/"; 
        str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Orders,Resources.HercResource.CategorywiseOrders);
        ltrlBreadcrum.Text = str.ToString();
        if (!IsPostBack)
        {
            ddlPageSize.Visible = false;
            displayOrderType();
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
    }

    #endregion 


    #region Function to bind order category in dropDownlist

    public void displayOrderType()
    {
        try
        {
            orderObject.OrderTypeID = 9;
            p_Var.dSet = orderBL.getOrderCategories(orderObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlOrderCategory.DataSource = p_Var.dSet;
                ddlOrderCategory.DataTextField = "OrderCatName";
                ddlOrderCategory.DataValueField = "OrderCatId";
                ddlOrderCategory.DataBind();
                ddlOrderCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlOrderCategory.DataSource = p_Var.dSet;
                ddlOrderCategory.DataBind();
                ddlOrderCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch
        {
            throw;
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
        this.Bind_Orders(pageIndex);
    }

    #endregion


    #region function declaration zone

    public void Bind_Orders(int pageIndex)
    {
        orderObject.PageIndex = pageIndex;
        orderObject.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        orderObject.OrderCatID = Convert.ToInt16(ddlOrderCategory.SelectedValue);
        p_Var.dSet = orderBL.Get_OrderCategoryWise(orderObject, out p_Var.k);

        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            Session["TempTable"] = p_Var.dSet.Tables[0];
            gvDailyOrders.Visible = true;
            gvDailyOrders.DataSource = p_Var.dSet;
            gvDailyOrders.DataBind();
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            for (int i = 0; i < gvDailyOrders.Rows.Count; i++)
            {
                Label lblNumber = gvDailyOrders.Rows[i].FindControl("lblnumber") as Label;

                if (p_Var.dSet.Tables[0].Rows[i]["Pro_No"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[i]["Pro_No"].ToString() != "")
                {
                    lblNumber.Text = p_Var.dSet.Tables[0].Rows[i]["Pro_No"].ToString();

                }
                else
                {
                    lblNumber.Text = p_Var.dSet.Tables[0].Rows[i]["RP_No"].ToString();

                }
            }
            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
            lblmsg.Visible = false;
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
            gvDailyOrders.Visible = false;
            
        }
        p_Var.Result = p_Var.k;
        if (p_Var.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_Var.Result, pageIndex, ddlPageSize, rptPager);
            rptPager.Visible = true;
        }
        else
        {
            rptPager.Visible = false;
        }

    }

    #endregion

    protected void ddlOrderCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrderCategory.SelectedValue != "0")
        {
            Bind_Orders(1);
        }
        else
        {
            gvDailyOrders.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
            lblmsg.Visible = false;
            rptPager.Visible = false;
        }
    }

    #region Gridview RowdataBound event zone

    protected void gvDailyOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            PetitionOB objnew = new PetitionOB();
            Label orderId = (Label)e.Row.FindControl("lblorderId");
            Literal orderConnectedFile = (Literal)e.Row.FindControl("lnkConnectedFile");
            Label lblPetitioner = (Label)e.Row.FindControl("lblPetitioner");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            if (lblPetitioner.Text != null && lblPetitioner.Text != "")
            {
                lblPetitioner.Text = HttpUtility.HtmlDecode(lblPetitioner.Text).Replace("&", "&amp;");
            }
            if (lblRespondent.Text != null && lblRespondent.Text != "")
            {
                lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text).Replace("&", "&amp;");
            }
            objnew.OrderID = Convert.ToInt16(orderId.Text);
            p_Var.dsFileName = orderBL.Get_pdf(objnew);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Var.sbuilder.Append("<a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='" + Resources.HercResource.ViewDocument + "'>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value)
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + ", ");
                        if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                        {
                            p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        }
                        p_Var.sbuilder.Append(" Dated: " + p_Var.dsFileName.Tables[0].Rows[i]["Date"]);

                       // p_Var.sbuilder.Append("<br /><a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                        p_Var.sbuilder.Append("<br />"+  "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='" + Resources.HercResource.ViewDocument + "' width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> ");
                        if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {
                                FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }
						p_Var.sbuilder.Append("<br/>");

                        gvDailyOrders.Columns[7].Visible = true;
                    }
                    else
                    {
                        if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                        {
                            p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        }
                       // p_Var.sbuilder.Append("<br /><a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>"  + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                        p_Var.sbuilder.Append("<br />"+ "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='" + Resources.HercResource.ViewDocument + "' width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> ");
                        if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {
                                FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }
						p_Var.sbuilder.Append("<br/>");
                        gvDailyOrders.Columns[7].Visible = true;
                    }
                }
                p_Var.sbuilder.Append("</a><hr/>");
                orderConnectedFile.Text = p_Var.sbuilder.ToString();

            }
            else
            {
               // gvDailyOrders.Columns[7].Visible = false;
            }
            if (string.IsNullOrEmpty(((HiddenField)e.Row.FindControl("hdfFile")).Value))
            {
                ((LinkButton)e.Row.FindControl("lbl_ViewDoc")).Visible = false;
            }

           
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



    protected void gvDailyOrders_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "viewDetails")
        {
          
                    p_Var.stringTypeID = e.CommandArgument.ToString();
                    p_Var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewOrderDetails.aspx?OrderId=" + p_Var.stringTypeID) + "', 'blank' + new Date().getTime()," +
                                       "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
                                       "</script>";
                    this.Page.RegisterStartupScript("PopupScript", p_Var.strPopupID);
            
        }



    }
}
