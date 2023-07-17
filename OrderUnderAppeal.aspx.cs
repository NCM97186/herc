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

public partial class OrderUnderAppeal : BasePage
{
    #region variable declaration zone

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL = new PetitionBL();
    PetitionOB objPetOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    public string lastUpdatedDate = string.Empty;
    public static string UrlPrint = string.Empty;
    OrderBL objOrdersBL = new OrderBL();

    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Val.url =Server.MapPath( ResolveUrl("~/")+ ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Orders"].ToString() + "/");
        str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Orders, Resources.HercResource.OrdersunderAppeal);
        ltrlBreadcrum.Text = str.ToString();
	    bool IsPageRefresh = false;
        if (!IsPostBack)
        {
			Session.Remove("yearnew");
            ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();

            Session["SessionId"] = ViewState["ViewStateId"].ToString();
            BindYear();
            Bind_Orders(1);
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            Bind_Orders(1);
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			 ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);

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


    #region function declaration zone

    public void Bind_Orders(int pageIndex)
    {
        objPetOB.PageIndex = pageIndex;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            objPetOB.PageSize = 10000;
        }
        else
        {
            objPetOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }

        objPetOB.year = drpyear.SelectedValue.ToString();
        p_Val.dSet = objOrdersBL.OrderunderAppeal(objPetOB, out p_Val.k);


        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            grdAppeal.DataSource = p_Val.dSet;
            grdAppeal.DataBind();
            lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
			 ViewState["lastUpdateDate"] = lastUpdatedDate;

             MetaKeyword = p_Val.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
             MetaDescription = p_Val.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
             MetaLng = p_Val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
             MetaTitles = p_Val.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();

            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
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
           lblmsg.Text = "Recors are not available.";
           lblmsg.ForeColor = System.Drawing.Color.Red;
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
            //pyear.Visible = false;
        }
        p_Val.Result = p_Val.k;
        if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_Val.Result, pageIndex, ddlPageSize, rptPager);
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

    #endregion

   


   

    protected void grdAppeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filename = DataBinder.Eval(e.Row.DataItem, "File_Name").ToString();
            LinkButton lnk = (LinkButton)e.Row.FindControl("lblViewDocAppeal");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
            if (filename == null || filename == "")
            {
                lnk.Visible = false;
            }



      
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");
         
            if (lblSubject.Text != null && lblSubject.Text != "")
            {
                lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            }


            Label LnkconnectedorderAppeal = (Label)e.Row.FindControl("LnkconnectedorderAppeal");
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            objPetOB.RPNo = grdAppeal.DataKeys[e.Row.RowIndex].Value.ToString();
            p_Val.dSetCompare = obj_petBL.getOrdersForAppeal(objPetOB);
            if (p_Val.dSetCompare.Tables[0].Rows.Count > 0)
            {

                p_Val.id = p_Val.dSetCompare.Tables[0].Rows[0]["OrderId"].ToString();
                for (int i = 0; i < p_Val.dSetCompare.Tables[0].Rows.Count; i++)
                {
                    p_Val.sbuilder.Append(p_Val.dSetCompare.Tables[0].Rows[i]["OrdertypeName"] + ", ");
                    p_Val.sbuilder.Append("Dated: " + p_Val.dSetCompare.Tables[0].Rows[i]["Date"]);

                }
                LnkconnectedorderAppeal.Text = p_Val.sbuilder.ToString();
                
            }
        }
    }
    protected void grdAppeal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDocAppeal")
        {
            Bind_Orders(1);
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


        if (e.CommandName == "View")
        {
           
            p_Val.stringTypeID = e.CommandArgument.ToString();
            p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewDetails.aspx?PDID=" + p_Val.stringTypeID) + "', 'blank' + new Date().getTime()," +
                               "'menubar=no, resizable=yes, scrollbars=yes, width=700,height=530,top=0,left=0 ')" +
                               "</script>";
            this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
        
        }

    }

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



    #region function to Bind data in a data list for year

    public void BindYear()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            pyear.Visible = false;
        }
        else
        {
            pyear.Visible = true;
        }
       
        p_Val.dSetChildData = objOrdersBL.Get_YearOrderUnderAppeal();

        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataTextField = "YEAR";
            drpyear.DataValueField = "YEAR";
            drpyear.DataBind();
        }
        else
        {
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataBind();
            drpyear.Items.Insert(0, new ListItem("Select")); 
        }

    }


    #endregion
    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Orders(1);
    }
}
