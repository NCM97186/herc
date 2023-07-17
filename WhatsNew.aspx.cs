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

public partial class WhatsNew : BasePage
{


    #region variable declaration zone

    string str = string.Empty;
    LinkBL objlnkBL = new LinkBL();
    LinkOB objlnkOB = new LinkOB();
    Project_Variables P_Val = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    PaginationBL pagingBL = new PaginationBL();
    public static string UrlPrint = string.Empty;
    public string lastUpdatedDate = string.Empty;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    PetitionOB obj_petOB = new PetitionOB();
    PetitionBL obj_petBL = new PetitionBL();

    WhatNewsOB objnewsOB = new WhatNewsOB();
    WhatsNewBL objnewsBL = new WhatsNewBL();


    #endregion 

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        str = BreadcrumDL.DisplayMemberAreaBreadCrum(Resources.HercResource.WhatNew);
        ltrlBreadcrum.Text = str.ToString();
        P_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/"); 
        if (!IsPostBack)
        {
            Get_AllWhatsNew(1);
            Bind_Petition_Grid(1);
            Bind_Public_Grid(1);
            Bind_Soh_Grid(1);
            Bind_Orders_Grid(1);
            BindInterimOrdersGrid(1);
            BindDiscussion(1);
            BindRepeator();
            Bind_ReviewPetition_Grid(1);
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

        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle =Resources.HercResource.herc+" "+ Resources.HercResource.WhatNew;
            MetaKeyword = Resources.HercResource.herc + " " + Resources.HercResource.WhatNew;
            MetaDescription = Resources.HercResource.herc + " " + Resources.HercResource.WhatNew;
        }
        else
        {
            PageTitle = Resources.HercResource.herc + " " + Resources.HercResource.WhatNew;
            MetaKeyword = Resources.HercResource.herc + " " + Resources.HercResource.WhatNew;
            MetaDescription = Resources.HercResource.herc + " " + Resources.HercResource.WhatNew;
        }
    }

    #endregion 

    #region function to bind All whats new Details

    public void Get_AllWhatsNew(int pageIndex)
    {
        try
        {

            objlnkOB.PageIndex = pageIndex;
            objlnkOB.PageSize = Convert.ToInt16(10000);
            objlnkOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            P_Val.dSet = objlnkBL.Get_AllWhatsNew(objlnkOB, out P_Val.k);
            if (P_Val.dSet.Tables[0].Rows.Count > 0)
            {
                hRegulation.InnerHtml = "Regulations/Notifications/Policies/Guidelines";
                RptrRegulation.DataSource = P_Val.dSet;
                RptrRegulation.DataBind();
                lastUpdatedDate = P_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
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
        catch { }
    }

    #endregion 

    #region repeater rptrOrders itemDataBound latest update

    protected void rptrOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
           
        }
    }

    #endregion

    #region repeater rptInterimOrders itemDataBound latest update

    protected void rptInterimOrder_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
           
        }
    }

    #endregion

    #region repeater rptPetitonNew itemDataBound latest update

    protected void rptPetitonNew_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkLatestUpdatePetition = (LinkButton)e.Item.FindControl("lnkLatestUpdatePetition");
            LinkButton lnkDetailsPetition = (LinkButton)e.Item.FindControl("lnkDetailsPetition");
            Label lblmsgPetition = (Label)e.Item.FindControl("lblmsgPetition");
            if (lnkLatestUpdatePetition.Text == "" || lnkLatestUpdatePetition == null)
            {
                lnkDetailsPetition.Visible = true;
                lnkLatestUpdatePetition.Visible = false;
                lblmsgPetition.Visible = false;
            }
            else
            {
                lblmsgPetition.Visible = true;
                lnkDetailsPetition.Visible = false;
                lnkLatestUpdatePetition.Visible = true;
            }
        }
    }

    #endregion

    #region repeater  rptrSOH itemDataBound latest update

    protected void rptrSOH_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
           
        }
    }

    #endregion

    #region repeater rptWhatsNew itemDataBound latest update

    protected void RptrRegulation_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink hyp = (HyperLink)e.Item.FindControl("hypFile");
            HiddenField hid = (HiddenField)e.Item.FindControl("hidFile");
            Label lblname = (Label)e.Item.FindControl("lblname");
            if (hid.Value.ToString() == "" || hid.Value == null)
            {
                lblname.Visible = true;
                hyp.Visible = false;
            }
            else
            {
                lblname.Visible = false;
                hyp.Visible = true;
                P_Val.sbuilder.Remove(0, P_Val.sbuilder.Length);
                if (File.Exists(Server.MapPath(hyp.NavigateUrl)))
                {
                    FileInfo finfo = new FileInfo(Server.MapPath(hyp.NavigateUrl));
                    double FileInBytes = finfo.Length;
                    P_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                    P_Val.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");
                }

                hyp.Text = hyp.Text.ToString().Replace("<p>", "").Replace("</p>", "");
                hyp.Text += P_Val.sbuilder.ToString();
            }
        }
    }

    #endregion

    #region repeater rptPublicNotice itemDataBound latest update

    protected void rptrPublic_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
           
        }
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Get_AllWhatsNew(1);
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Get_AllWhatsNew(pageIndex);
    }

    #endregion

    #region gridview Rowcommand event

    protected void gvWhatsNew_RowCommand(object sender, GridViewCommandEventArgs e)
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
    }

    #endregion 

    #region gridview RowDataBound event
    protected void gvWhatsNew_RowDataBound(object sender, GridViewRowEventArgs e)
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
    #endregion

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
        MetaLang = Resources.HercResource.Lang;
    }

    #endregion



    public void Bind_Petition_Grid(int pageindex)
    {
        try
        {
            obj_petOB.PageIndex = pageindex;
            obj_petOB.PageSize = Convert.ToInt32(10000);
            // obj_petOB.LangId = Convert.ToInt32(Resources.HercResource.Lang_Id);
            obj_petOB.ActionType = 1;
            obj_petOB.PRONo = null;
            P_Val.dSet = obj_petBL.GetLatestPetition(obj_petOB, out P_Val.k);
            if (P_Val.dSet.Tables[0].Rows.Count > 0)
            {
                hPetition.InnerText = "Petition";
                rptPetitonNew.DataSource = P_Val.dSet;
                rptPetitonNew.DataBind();
                lastUpdatedDate = P_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
                ViewState["lastUpdateDate"] = lastUpdatedDate;
            }

            P_Val.Result = P_Val.k;
        }
        catch { }
    }

    public void Bind_Public_Grid(int pageindex)
    {
        try
        {
            obj_petOB.PageIndex = pageindex;
            obj_petOB.PageSize = Convert.ToInt32(10000);
            // obj_petOB.LangId = Convert.ToInt32(Resources.HercResource.Lang_Id);
            obj_petOB.ActionType = 2;
            obj_petOB.PRONo = null;
            P_Val.dsFileName = obj_petBL.GetLatestPetition(obj_petOB, out P_Val.k);
            if (P_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                hPublic.InnerText = "Public Notice";

                rptrPublic.DataSource = P_Val.dsFileName;
                rptrPublic.DataBind();
                for (int i = 0; i < rptrPublic.Items.Count; i++)
                {
                    LinkButton lnkDetails = (LinkButton)this.rptrPublic.Items[i].FindControl("lnkDetails");


                    if (P_Val.dsFileName.Tables[0].Rows[i]["Pro"] == DBNull.Value && P_Val.dsFileName.Tables[0].Rows[i]["rp_no"] == DBNull.Value)
                    {

                    }
                    else
                    {

                    }

                }

                lastUpdatedDate = P_Val.dsFileName.Tables[0].Rows[0]["Lastupdate"].ToString();
                ViewState["lastUpdateDate"] = lastUpdatedDate;
            }

            P_Val.Result = P_Val.k;
        }
        catch { }
    }

    public void Bind_Soh_Grid(int pageindex)
    {
        try
        {
            obj_petOB.PageIndex = pageindex;
            obj_petOB.PageSize = Convert.ToInt32(10000);

            obj_petOB.ActionType = 3;
            obj_petOB.PRONo = null;
            P_Val.dsFileName = obj_petBL.GetLatestPetition(obj_petOB, out P_Val.k);
            if (P_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                hSOH.InnerText = "Schedule of Hearing";
                rptrSOH.DataSource = P_Val.dsFileName;
                rptrSOH.DataBind();



                for (int i = 0; i < rptrSOH.Items.Count; i++)
                {
                    LinkButton lnkDetailsSoh = (LinkButton)this.rptrSOH.Items[i].FindControl("lnkDetailsSoh");
                    LinkButton lnkLatestUpdateSoh = (LinkButton)this.rptrSOH.Items[i].FindControl("lnkLatestUpdateSoh");

                    Label lblmsgSoh = (Label)this.rptrSOH.Items[i].FindControl("lblmsgSoh");
                    if (P_Val.dsFileName.Tables[0].Rows[i]["PRO_No"] == DBNull.Value && P_Val.dsFileName.Tables[0].Rows[i]["RP_No"] == DBNull.Value)
                    {
                        lnkDetailsSoh.Visible = true;

                        lnkLatestUpdateSoh.Visible = false;
                        lblmsgSoh.Visible = false;

                    }
                    else
                    {
                        lnkDetailsSoh.Visible = false;

                        lnkLatestUpdateSoh.Visible = true;
                        if (P_Val.dsFileName.Tables[0].Rows[i]["PRO_No"] != DBNull.Value && P_Val.dsFileName.Tables[0].Rows[i]["PRO_No"].ToString() != "")
                        {
                            lnkLatestUpdateSoh.Text = P_Val.dsFileName.Tables[0].Rows[i]["PRO_No"].ToString();
                        }
                        else if (P_Val.dsFileName.Tables[0].Rows[i]["RP_No"] != DBNull.Value && P_Val.dsFileName.Tables[0].Rows[i]["RP_No"].ToString() != "")
                        {
                            lnkLatestUpdateSoh.Text = P_Val.dsFileName.Tables[0].Rows[i]["RP_No"].ToString();
                        }
                    }

                }

                lastUpdatedDate = P_Val.dsFileName.Tables[0].Rows[0]["Lastupdate"].ToString();
                ViewState["lastUpdateDate"] = lastUpdatedDate;
            }

            P_Val.Result = P_Val.k;

        }
        catch { }
    }



    public void Bind_Orders_Grid(int pageindex)
    {
        try
        {
            obj_petOB.PageIndex = pageindex;
            obj_petOB.PageSize = Convert.ToInt32(10000);

            obj_petOB.ActionType = 4;
            obj_petOB.PRONo = null;
            P_Val.dsFileName = obj_petBL.GetLatestPetition(obj_petOB, out P_Val.k);
            if (P_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                hOrders.InnerText = "Final Orders";
                rptrOrders.DataSource = P_Val.dsFileName;
                rptrOrders.DataBind();

                for (int i = 0; i < rptrOrders.Items.Count; i++)
                {
                    LinkButton lnkDetailsOrder = (LinkButton)this.rptrOrders.Items[i].FindControl("lnkDetailsOrder");
                    LinkButton lnkLatestUpdate = (LinkButton)this.rptrOrders.Items[i].FindControl("lnkLatestUpdate");

                    Label lblmsgOrders = (Label)this.rptrOrders.Items[i].FindControl("lblmsgOrders");

                    if (P_Val.dsFileName.Tables[0].Rows[i]["pro_no"] == DBNull.Value && P_Val.dsFileName.Tables[0].Rows[i]["rp_no"] == DBNull.Value)
                    {
                        lnkDetailsOrder.Visible = true;

                        lnkLatestUpdate.Visible = false;
                        lblmsgOrders.Visible = false;
                    }
                    else
                    {
                        lnkDetailsOrder.Visible = false;

                        lnkLatestUpdate.Visible = true;
                        if (P_Val.dsFileName.Tables[0].Rows[i]["pro_no"] != DBNull.Value && P_Val.dsFileName.Tables[0].Rows[i]["pro_no"].ToString() != "")
                        {
                            lnkLatestUpdate.Text = P_Val.dsFileName.Tables[0].Rows[i]["pro_no"].ToString();
                        }
                        else if (P_Val.dsFileName.Tables[0].Rows[i]["rp_no"] != DBNull.Value && P_Val.dsFileName.Tables[0].Rows[i]["rp_no"].ToString() != "")
                        {
                            lnkLatestUpdate.Text = P_Val.dsFileName.Tables[0].Rows[i]["rp_no"].ToString();
                        }
                    }

                }


                lastUpdatedDate = P_Val.dsFileName.Tables[0].Rows[0]["Lastupdate"].ToString();
                ViewState["lastUpdateDate"] = lastUpdatedDate;
            }

            P_Val.Result = P_Val.k;
        }
        catch { }
    }



    public void BindInterimOrdersGrid(int pageindex)
    {
        try
        {
            obj_petOB.PageIndex = pageindex;
            obj_petOB.PageSize = Convert.ToInt32(10000);

            obj_petOB.ActionType = 6;
            obj_petOB.PRONo = null;
            P_Val.dsFileName = obj_petBL.GetLatestPetition(obj_petOB, out P_Val.k);
            if (P_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                hInterimOrder.InnerText = "Interim Orders";
                rptInterimOrder.DataSource = P_Val.dsFileName;
                rptInterimOrder.DataBind();

                for (int i = 0; i < rptInterimOrder.Items.Count; i++)
                {
                    LinkButton lnkDetailsOrder = (LinkButton)this.rptInterimOrder.Items[i].FindControl("lnkDetailsOrder");
                    LinkButton lnkLatestUpdate = (LinkButton)this.rptInterimOrder.Items[i].FindControl("lnkLatestUpdate");

                    Label lblmsgOrders = (Label)this.rptInterimOrder.Items[i].FindControl("lblmsgOrders");

                    if (P_Val.dsFileName.Tables[0].Rows[i]["pro_no"] == DBNull.Value && P_Val.dsFileName.Tables[0].Rows[i]["rp_no"] == DBNull.Value)
                    {
                        lnkDetailsOrder.Visible = true;

                        lnkLatestUpdate.Visible = false;
                        lblmsgOrders.Visible = false;
                    }
                    else
                    {
                        lnkDetailsOrder.Visible = false;

                        lnkLatestUpdate.Visible = true;
                        if (P_Val.dsFileName.Tables[0].Rows[i]["pro_no"] != DBNull.Value && P_Val.dsFileName.Tables[0].Rows[i]["pro_no"].ToString() != "")
                        {
                            lnkLatestUpdate.Text = P_Val.dsFileName.Tables[0].Rows[i]["pro_no"].ToString();
                        }
                        else if (P_Val.dsFileName.Tables[0].Rows[i]["rp_no"] != DBNull.Value && P_Val.dsFileName.Tables[0].Rows[i]["rp_no"].ToString() != "")
                        {
                            lnkLatestUpdate.Text = P_Val.dsFileName.Tables[0].Rows[i]["rp_no"].ToString();
                        }
                    }

                }


                lastUpdatedDate = P_Val.dsFileName.Tables[0].Rows[0]["Lastupdate"].ToString();
                ViewState["lastUpdateDate"] = lastUpdatedDate;
            }

            P_Val.Result = P_Val.k;
        }
        catch { }
    }

    #region repeater Petition itemCommand latest update

    protected void rptPetitonNew_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {

            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                P_Val.stringTypeID = e.CommandArgument.ToString();
                P_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/ViewDetails.aspx?Petition_id=" + e.CommandArgument) + "',  'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
               
            }
            else
            {

                P_Val.stringTypeID = e.CommandArgument.ToString();
                P_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Content/Hindi/ViewDetails.aspx?Petition_id=" + e.CommandArgument) + "',  'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=Yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
               
            }

        

        }
    }

    #endregion



    #region repeater Petition itemCommand latest update

    protected void rptrPublic_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "viewPublic")
        {

            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                P_Val.stringTypeID = e.CommandArgument.ToString();
                P_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~//DetailsPage.aspx?PulicNoticeId=" + e.CommandArgument) + "',  'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
               
            }
            else
            {

                P_Val.stringTypeID = e.CommandArgument.ToString();
                P_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Content/Hindi//DetailsPage.aspx?PulicNoticeId=" + e.CommandArgument) + "',  'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=Yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
               
            }



        }
    }

    #endregion


    #region repeater Soh itemCommand latest update

    protected void rptrSOH_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "viewSoh")
        {

            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                P_Val.stringTypeID = e.CommandArgument.ToString();
                P_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/ViewDetails.aspx?Soh_id=" + e.CommandArgument) + "',  'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
               
            }
            else
            {

                P_Val.stringTypeID = e.CommandArgument.ToString();
                P_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Content/Hindi/ViewDetails.aspx?Soh_id=" + e.CommandArgument) + "',  'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=Yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
               
            }



        }
    }

    #endregion



    #region repeater Orders itemCommand latest update

    protected void rptrOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "viewOrder")
        {

            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                P_Val.stringTypeID = e.CommandArgument.ToString();
                P_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/DetailsPage.aspx?OrderId=" + e.CommandArgument) + "',  'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
               
            }
            else
            {

                P_Val.stringTypeID = e.CommandArgument.ToString();
                P_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Content/Hindi/DetailsPage.aspx?OrderId=" + e.CommandArgument) + "',  'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=Yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
               
            }



        }
    }

    #endregion

    #region repeater interim Orders itemCommand latest update

    protected void rptInterimOrder_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "viewOrder")
        {

            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                P_Val.stringTypeID = e.CommandArgument.ToString();
                P_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/DetailsPage.aspx?OrderId=" + e.CommandArgument) + "',  'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
               
            }
            else
            {

                P_Val.stringTypeID = e.CommandArgument.ToString();
                P_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Content/Hindi/DetailsPage.aspx?OrderId=" + e.CommandArgument) + "',  'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=Yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
               
            }



        }
    }

    #endregion

    public void BindDiscussion(int pageindex)
    {
        try
        {
            obj_petOB.PageIndex = pageindex;
            obj_petOB.PageSize = Convert.ToInt32(10000);
            obj_petOB.ActionType = 5;
            P_Val.dSetCompare = obj_petBL.GetLatestPetition(obj_petOB, out P_Val.k);
            if (P_Val.dSetCompare.Tables[0].Rows.Count > 0)
            {
                hDraftDiscussion.InnerText = "Draft/Discussion Paper";
                rptrDiscussion.DataSource = P_Val.dSetCompare;
                rptrDiscussion.DataBind();

            }
        }
        catch { }
    }




    protected void rptrDiscussion_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "viewDiscussion")
        {
            P_Val.stringTypeID = e.CommandArgument.ToString();
            P_Val.strPopupID = "<script language='javascript'>" +
                               "window.open('" + ResolveUrl("~/" + "ViewDetails.aspx?Link_Id=" + P_Val.stringTypeID) + "&" + "ModuleId= " + Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) + "',  'blank' + new Date().getTime()," +
                               "' menubar=no, resizable=no, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                               "</script>";
            this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);

        }
    }



    #region function to bind WhatsNew

    public void BindRepeator()
    {
        try
        {
            objnewsOB.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.update);
            objnewsOB.PageSize = 10000;
            objnewsOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            P_Val.dsFileName = objnewsBL.Get_AllWhatsNew(objnewsOB, out P_Val.k);
            if (P_Val.dsFileName.Tables[0].Rows.Count > 0)
            {


                News.InnerText = Resources.HercResource.Others;
                gvWhatsNew.DataSource = P_Val.dsFileName;
                gvWhatsNew.DataBind();
               

            }
            else
            {
                gvWhatsNew.DataSource = P_Val.dsFileName;
                gvWhatsNew.DataBind();
                //pagination.Visible = false;
            }

            P_Val.Result = P_Val.k;

        }
        catch
        {
           //throw;
        }
    }
    #endregion



  
    protected void lnkDetailsPetition_Click(Object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string WhatsNewId = btn.CommandArgument;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {
            P_Val.strPopupID = "<script language='javascript'>" +
                              "window.open('" + ResolveUrl("~/" + "ViewDetails.aspx?WhatsNewId=" + btn.CommandArgument) + "&" + "ModuleId= " + Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New) + "',  'blank' + new Date().getTime()," +
                              "' menubar=no, resizable=no, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                              "</script>";
            this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);

           
           
        }
        else
        {
            P_Val.strPopupID = "<script language='javascript'>" +
                             "window.open('" + ResolveUrl("~/Content/Hindi/" + "ViewDetails.aspx?WhatsNewId=" + btn.CommandArgument) + "&" + "ModuleId= " + Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New) + "',  'blank' + new Date().getTime()," +
                             "' menubar=no, resizable=no, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                             "</script>";
            this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
          
        }


    }


    //Review petition


    #region repeater rptPetiton Review itemDataBound latest update

    protected void rptReviewPetition_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkLatestUpdateRwPetition = (LinkButton)e.Item.FindControl("lnkLatestUpdateRwPetition");
            LinkButton lnkDetailsRwPetition = (LinkButton)e.Item.FindControl("lnkDetailsRwPetition");
            Label lblmsgPetition = (Label)e.Item.FindControl("lblmsgPetition");
            if (lnkLatestUpdateRwPetition.Text == "" || lnkLatestUpdateRwPetition == null)
            {
                lnkDetailsRwPetition.Visible = true;
                lnkLatestUpdateRwPetition.Visible = false;
                lblmsgPetition.Visible = false;
            }
            else
            {
                lblmsgPetition.Visible = true;
                lnkDetailsRwPetition.Visible = false;
                lnkLatestUpdateRwPetition.Visible = true;
            }
        }
    }

    #endregion


    #region repeater Review Petition itemCommand latest update

    protected void rptReviewPetition_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "viewReview")
        {

            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                P_Val.stringTypeID = e.CommandArgument.ToString();
                P_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/ViewDetails.aspx?RPID=" + e.CommandArgument) + "',  'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
               
            }
            else
            {

                P_Val.stringTypeID = e.CommandArgument.ToString();
                P_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Content/Hindi/ViewDetails.aspx?RPID=" + e.CommandArgument) + "',  'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=Yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", P_Val.strPopupID);
               
            }



        }
    }

    #endregion


    public void Bind_ReviewPetition_Grid(int pageindex)
    {
        try
        {
            obj_petOB.PageIndex = pageindex;
            obj_petOB.PageSize = Convert.ToInt32(10000);
            // obj_petOB.LangId = Convert.ToInt32(Resources.HercResource.Lang_Id);
            obj_petOB.ActionType = 7;
            obj_petOB.PRONo = null;
            P_Val.dSet = obj_petBL.GetLatestPetition(obj_petOB, out P_Val.k);
            if (P_Val.dSet.Tables[0].Rows.Count > 0)
            {
                hReviewPetition.InnerText = "Review Petition";
                rptReviewPetition.DataSource = P_Val.dSet;
                rptReviewPetition.DataBind();
                lastUpdatedDate = P_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
                ViewState["lastUpdateDate"] = lastUpdatedDate;
            }

            P_Val.Result = P_Val.k;

        }
        catch { }
    }


}
