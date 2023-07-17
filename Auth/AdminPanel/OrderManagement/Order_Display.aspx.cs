using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text;


public partial class Auth_AdminPanel_OrderManagement_Order_Display : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration zone

    Project_Variables p_Var = new Project_Variables();
    LinkBL obj_linkBL = new LinkBL();
    LinkOB obj_inkOB = new LinkOB();
    UserBL obj_UserBL = new UserBL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    PetitionOB orderObject = new PetitionOB();
    OrderBL orderBL = new OrderBL();
    PaginationBL pagingBL = new PaginationBL();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petPetitionBL = new PetitionBL();


    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();
    //End

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Remove("WhatsNewStatus"); // What's New sessions
        Session.Remove("Lng"); // RTI sessions
        Session.Remove("deptt");
        Session.Remove("Status");
        Session.Remove("year");
        Session.Remove("FAALng"); // FAARTI sessions
        Session.Remove("FAAdeptt");
        Session.Remove("FAAStatus");
        Session.Remove("FAAyear");
        Session.Remove("SAALng"); // SAARTI sessions
        Session.Remove("SAAdeptt");
        Session.Remove("SAAStatus");
        Session.Remove("SAAyear");


        Session.Remove("MixLng"); // Mix module sessions
        Session.Remove("Mixdeptt");
        Session.Remove("MixStatus");
        Session.Remove("MixModule");

        Session.Remove("Appealyear"); //Appeal module sessions
        Session.Remove("AppealLng");
        Session.Remove("Appealdeptt");
        Session.Remove("AppealStatus");

        Session.Remove("PStatus");// Public Notice module sessions
        Session.Remove("PYear");
        Session.Remove("PLng");

        Session.Remove("MLng"); //  module sessions
        Session.Remove("Mdeptt");
        Session.Remove("MStatus");
        Session.Remove("RoleDeptt");//Role Sessions

        Session.Remove("UsrDeptt");//User Sessions
        Session.Remove("UsrStatus");

        Session.Remove("ProfileLng"); // Profile module sessions
        Session.Remove("ProfileNvg");
        Session.Remove("ProfileDeptt");
        Session.Remove("profileStatus");

        Session.Remove("menulang"); //  menu sessions
        Session.Remove("menuposition");
        Session.Remove("menulst");
        Session.Remove("menustatus");

        Session.Remove("TariffCategory"); //  Tariff sessions
        Session.Remove("TariffLng");
        Session.Remove("TariffDeptt");
        Session.Remove("TariffType");
        Session.Remove("TariffStatus");

        Session.Remove("AwardYear"); //  Award sessions
        Session.Remove("AwardLng");
        Session.Remove("AwardStatus");

        Session.Remove("PetAppealLng"); //  Appeal petition sessions
        Session.Remove("PetAppealYear");
        Session.Remove("PetAppealStatus");

        Session.Remove("PetRvYear");    // review  petition sessions
        Session.Remove("PetRvLng");
        Session.Remove("PetRvStatus");

        Session.Remove("PetLng"); //  Petition sessions
        Session.Remove("PetYear");
        Session.Remove("PetStatus");

        Session.Remove("Sohdeptt"); // SOH sessions
        Session.Remove("SohLng");
        Session.Remove("SohYear");
        Session.Remove("SohStatus");

        Session.Remove("SohPetitionAppeal");
        Session.Remove("SohAppeal");
        Session.Remove("appealYear1");

        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": View  Order";
        this.Page.Title = " Order: HERC";

        if (!IsPostBack)
        {
            DataSet dsprv = new DataSet();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_permissionBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }

            ViewState["sortOrder"] = "";

            this.Page.Form.Enctype = "multipart/form-data";

            //rptPager.Visible = false;
            //lblPageSize.Visible = false;
            //ddlPageSize.Visible = false;
            btnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            btnAddOrder.Visible = false;
            if (Session["OrderYear"] != null)
            {
                bindOrdersYearinDdl();
                ddlYear.SelectedValue = Session["OrderYear"].ToString();
            }
            else
            {
                bindOrdersYearinDdl();
            }
            if (Session["OrderLng"] != null)
            {
                bindropDownlistLang();
                ddlLanguage.SelectedValue = Session["OrderLng"].ToString();
            }
            else
            {
                bindropDownlistLang();

            }

            if (Session["OrderType"] != null)
            {
                displayOrderType();
                ddlOrderType.SelectedValue = Session["OrderType"].ToString();
            }
            else
            {
                displayOrderType();
            }

            displayOrderTypeCategory(0);
            if (Session["OrderStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["OrderStatus"].ToString();
                Bind_Grid("", "");
            }
            else
            {
                binddropDownlistStatus();
            }
            // Get_Deptt_Name();
        }
    }

    #endregion

    //Area for all the dropDownlist events

    #region dropDownlist ddlLanguage selectedIndexChanged event

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlLanguage.SelectedValue != "0")
        {
            binddropDownlistStatus();
            grdOrders.Visible = false;
            Session["OrderLng"] = ddlLanguage.SelectedValue;
        }
        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlOrderType.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    #endregion

    #region dropDownlist ddlStatus selectedIndexChanged event

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlOrderType.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
        if (ddlStatus.SelectedValue == "0")
        {
            grdOrders.Visible = false;
            btnForReview.Visible = false;
            btnApprove.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;

            lblmsg.Visible = false;
        }
        else
        {
            Bind_Grid("", "");
            //BindGridDetails();
            Session["OrderStatus"] = ddlStatus.SelectedValue;
        }
        if (Convert.ToInt16(Session["Status_Id"]) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
        {
            if (Convert.ToInt16(ddlOrderType.SelectedValue) == 8)
            {
                grdOrders.Columns[10].Visible = false;
                grdOrders.Columns[7].Visible = false;
            }
            else
            {
                grdOrders.Columns[10].Visible = false;
                grdOrders.Columns[7].Visible = true;
            }
        }
        else
        {
            grdOrders.Columns[10].Visible = false;
            if (Convert.ToInt16(ddlOrderType.SelectedValue) == 8)
            {
                grdOrders.Columns[7].Visible = false;
            }
            else
            {
                grdOrders.Columns[7].Visible = true;
            }
        }
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid("", "");
    }

    #endregion

    //End

    //Area for all the buttons, linkButtons, imageButtons click events

    #region button btnAddOrder click event to redirect the page to the Add Order page

    protected void btnAddOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/OrderManagement/") + "Order_Add.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));
    }

    #endregion

    #region button btnForReview click event to send records for review

    protected void btnForReview_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;


    }

    #endregion

    #region button btnForApprove click event to send records for approval

    protected void btnForApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        pnlGrid.Visible = false;
        bindCheckBoxListWithApproverEmailIDs();


    }

    #endregion

    #region button btnApprove click event to approve records

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;
        //ChkApprove();
    }

    #endregion

    #region button btnDisApprove click event to disapprove records

    protected void btnDisApprove_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
        {
            pnlPopUpEmailsDis.Visible = true;
            pnlGrid.Visible = false;
            bindCheckBoxListWithEmailIDDiaapproves();
            //this.mpeSendEmailDis.Show();
        }
        else
        {
            pnlPopUpEmailsDis.Visible = true;
            pnlGrid.Visible = false;
            bindCheckBoxListWithDataEntryEmailIDs();
            // this.mpeSendEmailDis.Show();
        }

    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        Bind_Grid("", "");

    }

    #endregion

    //End

    //Area for all the gridView events

    #region gridView grdOrders rowCommand event

    protected void grdOrders_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        string current = Convert.ToString(Session["CurrentRequestUrl"]);
        current = current.Replace("%20", "+");
        string httpref = Convert.ToString(Request.ServerVariables["HTTP_REFERER"]);
        httpref = httpref.Replace("%20", "+");

        if (current != null && current.Equals(httpref))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (e.CommandName == "Repealed")
                {

                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    Label lblStatus = (Label)row.Cells[0].FindControl("lblStatus");
                    orderObject.TempOrderID = Convert.ToInt32(e.CommandArgument);
                    //ViewState["tempID"] = obj_inkOB.TempLinkId;
                    if (lblStatus.Text == "6")
                    {
                        orderObject.StatusId = 7;


                    }
                    else
                    {
                        orderObject.StatusId = 6;
                    }
                    int Result = orderBL.OrdersUpdateStatus_repealed(orderObject);

                    if (Result > 0)
                    {

                        if (lblStatus.Text == "6")
                        {
                            Session["msg"] = "Record has been repealed successfully.";
                        }
                        else
                        {
                            Session["msg"] = "Record has been unrepealed successfully.";
                        }
                        Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }

                else if (e.CommandName == "delete")
                {
                    p_Var.commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });
                    ViewState["commandArgs"] = p_Var.commandArgs;
                    p_Var.pa_id = Convert.ToInt32(p_Var.commandArgs[0]);
                    p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

                }

                else if (e.CommandName == "Restore")
                {
                    GridViewRow row1 = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    orderObject.OrderID = Convert.ToInt32(e.CommandArgument);
                    p_Var.Result = orderBL.ConnectedOrder_Restore(orderObject);
                    if (p_Var.Result > 0)
                    {

                        obj_audit.ActionType = "R";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        string ordertype = ddlOrderType.SelectedItem.ToString();
                        Label lbldate = row1.FindControl("lbldate") as Label;
                        string orderdate = lbldate.Text;
                        Label lblOrderTitle = row1.FindControl("lblOrderTitle") as Label;
                        string desc = lblOrderTitle.Text;
                        obj_audit.Title = ordertype + ", " + orderdate + ", " + desc;
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "Order has been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                    else
                    {
                        Session["msg"] = "Order has not been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                }
                else if (e.CommandName == "Audit")
                {

                    DataSet dSetAuditTrail = new DataSet();
                    petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
                    petObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Orders);
                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    petObject.ModuleType = null;
                    dSetAuditTrail = petPetitionBL.AuditTrailRecords(petObject);
                    Label lblprono = row.FindControl("lblOrderTitle") as Label;
                    if (dSetAuditTrail.Tables[0].Rows.Count > 0)
                    {
                        ltrlPetitionNo.Text = "<strong>" + lblprono.Text + "</strong>";
                        if (dSetAuditTrail.Tables[0].Rows[0]["createdBy"] != DBNull.Value)
                        {
                            ltrlCreation.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["createdBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["CreatedDateTime"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["CreatedIPAddress"].ToString();
                        }
                        else
                        {
                            ltrlCreation.Text = "";
                        }
                        if (dSetAuditTrail.Tables[0].Rows[0]["editedBy"] != DBNull.Value)
                        {
                            ltrlLastEdited.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["editedBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["EditedDateTime"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["UpdatedIPAddress"].ToString();
                        }
                        else
                        {
                            ltrlLastEdited.Text = "";
                        }
                        if (dSetAuditTrail.Tables[0].Rows[0]["reviewedBy"] != DBNull.Value)
                        {
                            ltrlLastReviewed.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["reviewedBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["ReviewedDate"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["ReviewedIPAddress"].ToString();
                        }
                        else
                        {
                            ltrlLastReviewed.Text = "";
                        }
                        if (dSetAuditTrail.Tables[0].Rows[0]["publishedBy"] != DBNull.Value)
                        {
                            ltrlLastPublished.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["publishedBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["PublishedDateTime"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["PublishedIPAddress"].ToString();
                        }
                        else
                        {
                            ltrlLastPublished.Text = "";
                        }
                    }
                    else
                    {
                        ltrlCreation.Text = "";
                        ltrlLastEdited.Text = "";
                        ltrlLastPublished.Text = "";
                        ltrlLastReviewed.Text = "";
                        ltrlPetitionNo.Text = "";
                    }
                    this.mdpAuditTrail.Show();
                }
            }
        }
    }
    #endregion

    #region gridView grdOrders rowCreated event to check checkboxes

    protected void grdOrders_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == DataControlRowState.Normal | e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
            CheckBox chkBxHeader = (CheckBox)this.grdOrders.HeaderRow.FindControl("chkSelectAll");
            //Add client side function childclick on check boxes
            chkBxSelect.Attributes.Add("onclick", string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID));


        }
    }

    #endregion

    #region gridView grdOrders rowDataBound event

    protected void grdOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label reviewpetition = (Label)e.Row.FindControl("lblReview");
            Label petition = (Label)e.Row.FindControl("lblPetition");
            Label lblconnectionName = e.Row.FindControl("lblconnectionName") as Label;
            Label lblconnection = e.Row.FindControl("lblconnection") as Label;
            Label lblOrderRemarks = (Label)e.Row.FindControl("lblOrderRemarks");
            Label lblOrderTitle = (Label)e.Row.FindControl("lblOrderTitle");
            lblOrderRemarks.Text = HttpUtility.HtmlDecode(lblOrderRemarks.Text);
            lblOrderTitle.Text = HttpUtility.HtmlDecode(lblOrderTitle.Text);
            if (reviewpetition.Text != null && reviewpetition.Text != "")
            {
                reviewpetition.Visible = true;
                reviewpetition.Text = reviewpetition.Text;
                petition.Visible = false;
            }
            else if (petition.Text != null && petition.Text != "")
            {
                reviewpetition.Visible = false;
                petition.Visible = true;
                petition.Text = petition.Text;
                lblconnection.Visible = true;
            }
            if (petition.Text == null || petition.Text == "")
            {

                lblconnectionName.Text = "";
                lblconnection.Visible = false;
            }
            else
            {
                lblconnection.Visible = true;
                lblconnectionName.Visible = false;
            }
            if (reviewpetition.Text == null || reviewpetition.Text == "")
            {

                lblconnectionName.Text = "";
                //lblconnection.Visible = false;
            }
            else
            {
                lblconnection.Visible = true;
                lblconnectionName.Visible = false;
            }

            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkrpld");
            Label lblstatus = (Label)e.Row.FindControl("lblStatus");
            if (lblstatus.Text == "6")
            {
                lnk.Text = "Repeal";

                if (petition.Text != null && petition.Text != "")
                {
                    lnk.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure to repeal order against this PRO No- " +
                        DataBinder.Eval(e.Row.DataItem, "PRONO1") + "')");
                }
                else if (reviewpetition.Text != null && reviewpetition.Text != "")
                {
                    lnk.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure to repeal order against this RA No- " +
                        DataBinder.Eval(e.Row.DataItem, "Review1") + "')");
                }
                else
                {

                    lnk.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure to repeal order?')");

                }

            }
            else if (lblstatus.Text == "7")
            {
                lnk.Text = "UnRepeal";


                if (petition.Text != null && petition.Text != "")
                {
                    lnk.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure to unrepeal order against this PRO No- " +
                        DataBinder.Eval(e.Row.DataItem, "PRONO1") + "')");
                }
                else if (reviewpetition.Text != null && reviewpetition.Text != "")
                {
                    lnk.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure to unrepeal order against this RA No- " +
                        DataBinder.Eval(e.Row.DataItem, "Review1") + "')");
                }
                else
                {

                    lnk.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure to unrepeal order?')");

                }

            }


            //This is for delete/permanently delete 30 may 2013 
            ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
            ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");

            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
            {

                if (petition.Text != null && petition.Text != "")
                {
                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to restore order against PRO No-" + DataBinder.Eval(e.Row.DataItem, "PRONO1") + "')");

                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
    "confirm('Are you sure you want to purge order against PRO No-" + DataBinder.Eval(e.Row.DataItem, "PRONO1") + " permanently? " + "')");
                }
                else if (reviewpetition.Text != null && reviewpetition.Text != "")
                {
                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to restore order against RA No-" + DataBinder.Eval(e.Row.DataItem, "Review1") + "')");

                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
    "confirm('Are you sure you want to purge order against RA No-" + DataBinder.Eval(e.Row.DataItem, "Review1") + " permanently? " + "')");
                }
                else
                {
                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
                   "confirm('Are you sure you want to restore order ?" + "')");

                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
    "confirm('Are you sure you want to purge order" + " permanently ? " + "')");
                }
            }
            else
            {

                if (petition.Text != null && petition.Text != "")
                {

                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to delete order against PRO No- " + DataBinder.Eval(e.Row.DataItem, "PRONO1") + "')");
                }
                else if (reviewpetition.Text != null && reviewpetition.Text != "")
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                   "confirm('Are you sure you want to delete order against RA No- " + DataBinder.Eval(e.Row.DataItem, "Review1") + "')");
                }
                else
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                   "confirm('Are you sure you want to delete order ? " + "')");
                }
            }

            //END
        }
    }

    #endregion

    #region gridView grdOrders rowDeleting event to delete records

    protected void grdOrders_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row1 = (GridViewRow)grdOrders.Rows[e.RowIndex];
        orderObject.OrderID = Convert.ToInt32(p_Var.pa_id);

        orderObject.TempOrderID = Convert.ToInt32(p_Var.pa_id);
        orderObject.StatusId = Convert.ToInt32(p_Var.status_id);
        int record = orderBL.OrdersUpdateStatusDelete(orderObject);
        if (record > 0)
        {
            if (ddlStatus.SelectedValue == "8")
            {
                Session["msg"] = "Order has been deleted (purged) permanently.";
            }
            else
            {
                Session["msg"] = "Order has been deleted successfully.";
            }


            obj_audit.ActionType = "D";
            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
            obj_audit.UserName = Session["UserName"].ToString();
            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            obj_audit.IpAddress = miscellBL.IpAddress();
            obj_audit.status = ddlStatus.SelectedItem.ToString();
            string ordertype = ddlOrderType.SelectedItem.ToString();
            Label lbldate = row1.FindControl("lbldate") as Label;
            string orderdate = lbldate.Text;
            Label lblOrderTitle = row1.FindControl("lblOrderTitle") as Label;
            string desc = lblOrderTitle.Text;
            obj_audit.Title = ordertype + ", " + orderdate + ", " + desc;
            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

            Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

        }
        //}


    }

    #endregion


    #region button btnDelete click event to delete petition

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        p_Var.commandArgs = (string[])ViewState["commandArgs"];
        p_Var.rp_id = Convert.ToInt32(p_Var.commandArgs[0]);
        p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

        orderObject.RPId = p_Var.rp_id;
        orderObject.StatusId = p_Var.status_id;

        p_Var.Result = orderBL.Delete_OrderStatus(orderObject);
        if (p_Var.Result > 0)
        {
            if (ddlStatus.SelectedValue == "8")
            {
                Session["msg"] = "Order has been deleted (purged) successfully.";
            }
            else
            {
                Session["msg"] = "Order has been deleted successfully.";
            }


            Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        }

    }

    #endregion

    //End

    //Area for all the user defined functions

    #region Function to bind language in dropDownlist according to permission

    public void bindropDownlistLang()
    {
        try
        {
            Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
            Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            p_Var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                UserOB usrObject = new UserOB();
                if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true && Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
                {
                    usrObject.english = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.English);
                    usrObject.hindi = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(usrObject.english).Append(",");
                    p_Var.sbuilder.Append(usrObject.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true)
                {
                    usrObject.hindi = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(usrObject.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
                {
                    usrObject.english = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.English);

                    p_Var.sbuilder.Append(usrObject.english);
                }
                usrObject.LangId = p_Var.sbuilder.ToString().Trim();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                p_Var.dSet = null;
                p_Var.dSet = miscdlLanguage.getLanguage(usrObject);
                PLanguage.Visible = true;
                ddlLanguage.DataSource = p_Var.dSet;
                ddlLanguage.DataTextField = "Language";
                ddlLanguage.DataValueField = "Lang_Id";
                ddlLanguage.DataBind();

            }
            p_Var.dSet = null;


        }
        catch
        {

        }
    }

    #endregion

    #region Function to bind status in dropDownlist according to permission

    public void binddropDownlistStatus()
    {
        Miscelleneous_BL miscDdlStatus = new Miscelleneous_BL();
        Miscelleneous_BL miscdlStatus = new Miscelleneous_BL();

        obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
        obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
        p_Var.dSet = miscDdlStatus.getLanguagePermission(obj_userOB);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            UserOB usrObject = new UserOB();
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft)).Append(",");
                btnAddOrder.Visible = true;

                //code written on date 20 sep 2013
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end
            }
            else
            {
                btnAddOrder.Visible = false;
            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)).Append(",");

            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover)).Append(",");
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete));
            }

            usrObject.ModulestatusID = p_Var.sbuilder.ToString();
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            p_Var.dSet = null;
            p_Var.dSet = miscdlStatus.getStatusPermissionwise(usrObject);
            ddlStatus.DataSource = p_Var.dSet;
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "Status_Id";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Select Status", "0"));

            //BtnForReview.Visible = false;
        }

        p_Var.dSet = null;
    }

    #endregion

    #region Function to bind orders in gridView

    public void Bind_Grid(string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;
        if (ddlStatus.SelectedValue == "0")
        {
            grdOrders.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {
            grdOrders.Visible = true;

            orderObject.year = ddlYear.SelectedValue.ToString();
            orderObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            orderObject.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            orderObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            orderObject.OrderTypeID = Convert.ToInt32(ddlOrderType.SelectedValue);


            //orderObject.OrderCatID = Convert.ToInt16(ddlOrderCategory.SelectedValue);

            orderObject.year = ddlYear.SelectedValue.ToString();
            p_Var.dSet = orderBL.DisplayOrdersWithPaging(orderObject, out p_Var.k);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    grdOrders.Columns[9].HeaderText = "Purge";
                }
                else
                {
                    grdOrders.Columns[9].HeaderText = "Delete";
                }

                //Codes for the sorting of records

                DataView myDataView = new DataView();
                myDataView = p_Var.dSet.Tables[0].DefaultView;

                if (sortExp != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }


                //End
                grdOrders.DataSource = myDataView;
                grdOrders.DataBind();
                p_Var.dSet = null;

                Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
                Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
                obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
                obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                p_Var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);
                if (p_Var.dSet.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        grdOrders.Columns[0].Visible = false;
                        foreach (GridViewRow row in grdOrders.Rows)
                        {

                            Image imgedit = (Image)row.FindControl("imgEdit");
                            Image imgnotedit = (Image)row.FindControl("imgnotedit");
                            Label lblOrderID = (Label)row.FindControl("lblOrderID");

                            orderObject.OrderID = Convert.ToInt32(lblOrderID.Text);

                            p_Var.dSetCompare = orderBL.OrderIDforComparision(orderObject);
                            for (p_Var.i = 0; p_Var.i < p_Var.dSetCompare.Tables[0].Rows.Count; p_Var.i++)
                            {
                                if (p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["OrderID"] != DBNull.Value)
                                {
                                    if (Convert.ToInt32(lblOrderID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["OrderID"]))
                                    {
                                        imgnotedit.Visible = true;
                                        imgedit.Visible = false;

                                    }
                                    else
                                    {
                                        imgnotedit.Visible = false;
                                        imgedit.Visible = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        grdOrders.Columns[0].Visible = true;
                    }
                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                    {
                        btnForReview.Visible = true;
                    }
                    else
                    {
                        btnForReview.Visible = false;
                    }

                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        btnForApprove.Visible = true;
                        btnDisApprove.Visible = true;
                    }
                    else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        btnApprove.Visible = true;
                        btnDisApprove.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnForApprove.Visible = false;
                        btnDisApprove.Visible = false;
                    }

                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        btnApprove.Visible = true;
                        btnDisApprove.Visible = true;
                        btnForApprove.Visible = false;
                    }
                    else
                    {

                        btnApprove.Visible = false;

                    }

                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][6]) == true)
                    {
                        grdOrders.Columns[8].Visible = true;//7
                    }
                    else
                    {
                        grdOrders.Columns[8].Visible = false;
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {

                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                        {
                            grdOrders.Columns[8].Visible = false;
                            grdOrders.Columns[12].Visible = true;
                            grdOrders.Columns[10].Visible = false;
                            grdOrders.Columns[14].Visible = false;
                        }
                        else
                        {
                            grdOrders.Columns[8].Visible = true;
                            grdOrders.Columns[12].Visible = false;
                            if (Convert.ToInt16(ddlOrderType.SelectedValue) == 8)
                            {
                                grdOrders.Columns[10].Visible = false;
                            }
                            else
                            {
                                grdOrders.Columns[10].Visible = false;
                            }

                        }


                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                            {
                                grdOrders.Columns[9].Visible = true;
                                grdOrders.Columns[14].Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    grdOrders.Columns[9].Visible = true;
                                    grdOrders.Columns[14].Visible = false;
                                }
                                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                {
                                    if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdOrders.Columns[14].Visible = true;
                                    }
                                    else
                                    {
                                        grdOrders.Columns[14].Visible = false;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdOrders.Columns[9].Visible = true;
                                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                                        {
                                            grdOrders.Columns[14].Visible = true;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                            {
                                                grdOrders.Columns[14].Visible = true;
                                            }
                                            else
                                            {
                                                grdOrders.Columns[14].Visible = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        grdOrders.Columns[9].Visible = false;
                                        grdOrders.Columns[14].Visible = false;
                                    }
                                }
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                            {
                                grdOrders.Columns[9].Visible = true;
                                grdOrders.Columns[14].Visible = false;
                            }
                            else
                            {
                                //grdCMSMenu.Columns[7].Visible = false;
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdOrders.Columns[9].Visible = true;
                            }
                            else
                            {
                                //grdCMSMenu.Columns[7].Visible = false;
                            }
                        }


                        //End    

                    }
                    else
                    {
                        grdOrders.Columns[9].Visible = false;
                        //grdOrders.Columns[10].Visible = true;
                    }
                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {
                //lblPageSize.Visible = false;
                //ddlPageSize.Visible = false;
                //rptPager.Visible = false;
                grdOrders.Visible = false;
                btnForReview.Visible = false;
                btnForApprove.Visible = false;
                btnApprove.Visible = false;
                btnDisApprove.Visible = false;
                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "No record found.";
            }
        }
        p_Var.Result = p_Var.k;

        Session["Status_Id"] = ddlStatus.SelectedValue.ToString();
        Session["Lanuage"] = ddlLanguage.SelectedValue;
        // Session["priv"] = p_Var.dSet;     //session hold the dsprv values  
    }

    #endregion



    //End

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        grdOrders.Visible = false;
        Session["OrderYear"] = ddlYear.SelectedValue;
    }

    #region Function to bind Orders Year

    public void bindOrdersYearinDdl()
    {
        p_Var.dSet = orderBL.GetYearOrders();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = p_Var.dSet;
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "year";
            ddlYear.DataBind();
        }
    }

    #endregion

    #region Function to bind order type in dropDownlist

    public void displayOrderType()
    {
        try
        {
            p_Var.dSet = orderBL.getOrderTypeDisplay();
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlOrderType.DataSource = p_Var.dSet;
                ddlOrderType.DataTextField = "Order_Type";
                ddlOrderType.DataValueField = "Order_Type_ID";
                ddlOrderType.DataBind();
                ddlOrderType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind order category based on order type in dropDownlist

    public void displayOrderTypeCategory(int ordertype)
    {
        try
        {

        }
        catch
        {
            throw;
        }
    }

    #endregion

    protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    {


        grdOrders.Visible = false;
        btnForReview.Visible = false;
        btnApprove.Visible = false;
        btnForApprove.Visible = false;
        btnDisApprove.Visible = false;
        binddropDownlistStatus();
        displayOrderTypeCategory(Convert.ToInt32(ddlOrderType.SelectedValue));
        grdOrders.Visible = false;
        Session["OrderType"] = ddlOrderType.SelectedValue;

        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlOrderType.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }
    protected void ddlOrderCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlOrderType.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
        binddropDownlistStatus();
    }



    protected void grdCMSMenu_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid(e.SortExpression, sortOrder);

    }

    #region gridView grdCMSMenu pageIndexChanging Event zone

    protected void grdCMSMenu_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdOrders.PageIndex = e.NewPageIndex;

        Bind_Grid(ViewState["e"].ToString(), ViewState["o"].ToString());
    }

    #endregion

    //End

    //New codes for sorting

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

    //End

    protected void btnPdf_Click(object sender, EventArgs e)
    {
        BindGridDetails();
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Orders_" + System.DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdOrderPdf.AllowPaging = false;
        grdOrderPdf.DataBind();
        grdOrderPdf.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    public void BindGridDetails()
    {

        if (ddlStatus.SelectedValue == "0")
        {
            grdOrders.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {
            grdOrders.Visible = true;

            orderObject.year = ddlYear.SelectedValue.ToString();
            orderObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            orderObject.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            orderObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            orderObject.OrderTypeID = Convert.ToInt32(ddlOrderType.SelectedValue);

            orderObject.year = ddlYear.SelectedValue.ToString();
            p_Var.dSet = orderBL.DisplayOrdersWithPaging(orderObject, out p_Var.k);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                grdOrderPdf.Visible = true;
                grdOrderPdf.DataSource = p_Var.dSet;
                grdOrderPdf.DataBind();
                p_Var.dSet = null;

                Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
                Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
                obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
                obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                p_Var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);

                p_Var.dSet = null;
                lblmsg.Visible = false;
            }

        }

    }

    #region Gridview grdOrderPdf event RowDataBound

    protected void grdOrderPdf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal ltrlConnectedFile1 = (Literal)e.Row.FindControl("ltrlConnectedFile1");
            Label lblPetitionerName = (Label)e.Row.FindControl("lblPetitionerName");
            Label lblRespondentName = (Label)e.Row.FindControl("lblRespondentName");

            Label lblPetitionerAddress = (Label)e.Row.FindControl("lblPetitionerAddress");
            Label lblRespondentAddress = (Label)e.Row.FindControl("lblRespondentAddress");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            lblPetitionerName.Text = HttpUtility.HtmlDecode(lblPetitionerName.Text);
            lblRespondentName.Text = HttpUtility.HtmlDecode(lblRespondentName.Text);

            Label lblTitle = (Label)e.Row.FindControl("lblTitle");
            lblTitle.Text = HttpUtility.HtmlDecode(lblTitle.Text);

            lblPetitionerAddress.Text = HttpUtility.HtmlDecode(lblPetitionerAddress.Text);
            lblRespondentAddress.Text = HttpUtility.HtmlDecode(lblRespondentAddress.Text);
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);

            orderObject.OrderID = Convert.ToInt32(grdOrderPdf.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = orderBL.getConnectedOrders(orderObject);
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    if (p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value)
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + ", ");

                        if (p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] != DBNull.Value)
                        {
                            p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] + ", ");
                        }
                        if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                        {
                            p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        }

                        p_Var.sbuilder.Append(" Dated: " + p_Var.dsFileName.Tables[0].Rows[i]["Date"]);
                        p_Var.sbuilder.Append("<br />" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                        p_Var.sbuilder.Append("<br/><hr/>");

                    }
                    else
                    {

                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");

                        p_Var.sbuilder.Append("<br />" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                        p_Var.sbuilder.Append("<br/><hr/>");
                    }
                }

                ltrlConnectedFile1.Text = p_Var.sbuilder.ToString();

            }
        }
    }

    #endregion


    protected void btnSendEmails_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                StringBuilder sbuilder = new StringBuilder();
                StringBuilder sbuilderSms = new StringBuilder();
                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdOrders.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblOrderTitle = (Label)row.FindControl("lblOrderTitle");
                        Label lblOrderCategoryName = (Label)row.FindControl("lblOrderCategoryName");

                        ViewState["Order"] = lblOrderTitle.Text;

                        ViewState["OrderCat"] = lblOrderCategoryName.Text;


                        if ((selCheck.Checked == true))
                        {
                            if (lblOrderCategoryName.Text.ToString() == "Interim Order")
                            {
                                sbuilder.Append(lblOrderCategoryName.Text + "- " + lblOrderTitle.Text + "<br/> ");
                            }
                            else
                            {
                                sbuilder.Append("Final Order - " + lblOrderTitle.Text + "<br/> ");
                            }
                            p_Var.dataKeyID = Convert.ToInt32(grdOrders.DataKeys[row.RowIndex].Value);
                            orderObject.TempOrderID = p_Var.dataKeyID;
                            orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            orderObject.userID = Convert.ToInt32(Session["User_Id"]);
                            orderObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = orderBL.OrdersUpdateStatus(orderObject);

                        }
                    }
                    if (p_Var.Result > 0)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                        {

                            if (li.Selected == true)
                            {
                                // p_Var.sbuildertmp.Append(li.Value + ";");
                                int statindex = li.Text.IndexOf("(") + 1;
                                p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                                sbuilderSms.Append(li.Value + ";");
                            }

                        }
                        if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                        {
                            p_Var.sbuilder.Append("Record pending for Review : " + sbuilder.ToString());
                            p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record pending for Review", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

                        }
                        ///* Code to send sms */
                        char[] splitter = { ';' };
                        PetitionOB petObjectNew = new PetitionOB();
                        DataSet dsSms = new DataSet();
                        string textmessage;
                        string strUrl = sbuilderSms.ToString();
                        string[] split = strUrl.Split(';');
                        ArrayList list = new ArrayList();
                        foreach (string item in split)
                        {
                            list.Add(item.Trim());
                        }


                        string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "");
                        string[] stringSeparators = new string[] { "<br/>" };
                        string[] splitPublicNotice = strpublicnotice.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        ArrayList listPublicNotice = new ArrayList();
                        foreach (string itemPublicNotice in splitPublicNotice)
                        {
                            listPublicNotice.Add(itemPublicNotice.Trim());
                        }

                        foreach (string strPublicNotice in listPublicNotice)
                        {
                            if (strPublicNotice != string.Empty && strPublicNotice != "")
                            {
                                //loop through cells in that row
                                foreach (string str in list)
                                {
                                    if (str != string.Empty)
                                    {

                                        //string message = ViewState["Title"].ToString();
                                        string message = strPublicNotice;

                                        if (message.Length > 150)
                                        {
                                            message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                        }
                                        else
                                        {
                                            message = strPublicNotice.ToString();
                                        }
                                        if (ViewState["OrderCat"].ToString() == "Interim Order")
                                        {
                                            textmessage = "HERC - Record pending for review - ";
                                        }
                                        else
                                        {
                                            textmessage = "HERC - Record pending for review - ";
                                        }

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }

                            }
                        }



                        /* End */

                        Session["msg"] = "Order has been sent for review successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }

                }
                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    foreach (GridViewRow row in grdOrders.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblOrderTitle = (Label)row.FindControl("lblOrderTitle");
                        Label lblOrderCategoryName = (Label)row.FindControl("lblOrderCategoryName");

                        ViewState["Order"] = lblOrderTitle.Text;
                        ViewState["OrderCat"] = lblOrderCategoryName.Text;
                        if ((selCheck.Checked == true))
                        {
                            if (lblOrderCategoryName.Text.ToString() == "Interim Order")
                            {
                                sbuilder.Append(lblOrderCategoryName.Text + "- " + lblOrderTitle.Text + "<br/> ");
                            }
                            else
                            {
                                sbuilder.Append("Final Order - " + lblOrderTitle.Text + "<br/> ");
                            }
                            p_Var.dataKeyID = Convert.ToInt32(grdOrders.DataKeys[row.RowIndex].Value);
                            orderObject.TempOrderID = p_Var.dataKeyID;
                            orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            orderObject.userID = Convert.ToInt32(Session["User_Id"]);
                            orderObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = orderBL.OrdersUpdateStatus(orderObject);

                        }
                    }
                    if (p_Var.Result > 0)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        sbuilderSms.Remove(0, sbuilderSms.Length);
                        foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                        {

                            if (li.Selected == true)
                            {
                                //p_Var.sbuildertmp.Append(li.Value + ";");
                                int statindex = li.Text.IndexOf("(") + 1;
                                p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                                sbuilderSms.Append(li.Value + ";");
                            }

                        }
                        if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                        {
                            p_Var.sbuilder.Append("Record pending for Publish : " + sbuilder.ToString());
                            p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record pending for Publish", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

                        }

                        ///* Code to send sms */
                        char[] splitter = { ';' };
                        PetitionOB petObjectNew = new PetitionOB();
                        DataSet dsSms = new DataSet();
                        string textmessage;
                        string strUrl = sbuilderSms.ToString();
                        string[] split = strUrl.Split(';');
                        ArrayList list = new ArrayList();
                        foreach (string item in split)
                        {
                            list.Add(item.Trim());
                        }


                        string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "");
                        string[] stringSeparators = new string[] { "<br/>" };
                        string[] splitPublicNotice = strpublicnotice.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        ArrayList listPublicNotice = new ArrayList();
                        foreach (string itemPublicNotice in splitPublicNotice)
                        {
                            listPublicNotice.Add(itemPublicNotice.Trim());
                        }

                        foreach (string strPublicNotice in listPublicNotice)
                        {
                            if (strPublicNotice != string.Empty && strPublicNotice != "")
                            {
                                //loop through cells in that row

                                foreach (string str in list)
                                {
                                    if (str != string.Empty)
                                    {

                                        //string message = ViewState["Title"].ToString();
                                        string message = strPublicNotice;

                                        if (message.Length > 150)
                                        {
                                            message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                        }
                                        else
                                        {
                                            message = strPublicNotice.ToString();
                                        }
                                        if (ViewState["OrderCat"].ToString() == "Interim Order")
                                        {
                                            textmessage = "HERC - Record pending for publish - ";
                                        }
                                        else
                                        {
                                            textmessage = "HERC - Record pending for publish - ";
                                        }

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }

                            }
                        }


                        /* End */


                        Session["msg"] = "Order has been sent for publish successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }

                }

                else
                {
                    LinkOB obj_linkOB1 = new LinkOB();
                    foreach (GridViewRow row in grdOrders.Rows)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblOrderTitle = (Label)row.FindControl("lblOrderTitle");
                        Label lblOrderCategoryName = (Label)row.FindControl("lblOrderCategoryName");

                        ViewState["Order"] = lblOrderTitle.Text;
                        ViewState["OrderCat"] = lblOrderCategoryName.Text;
                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblOrderID");
                            //Label lblOrderCategoryName = (Label)row.FindControl("lblOrderCategoryName");
                            if (lblOrderCategoryName.Text.ToString() == "Interim Order")
                            {
                                sbuilder.Append(lblOrderCategoryName.Text + "- " + lblOrderTitle.Text + "<br/> ");
                            }
                            else
                            {
                                sbuilder.Append("Final Order - " + lblOrderTitle.Text + "<br/> ");
                            }
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            orderObject.TempOrderID = p_Var.dataKeyID;
                            orderObject.userID = Convert.ToInt32(Session["User_Id"]);
                            orderObject.IpAddress = Miscelleneous_DL.getclientIP();
                            petObject.OrderID = p_Var.dataKeyID;


                            char[] splitter = { ';' };
                            DataSet dsMail = new DataSet();
                            DataSet dsSms = new DataSet();
                            dsSms = orderBL.getMobileNumberForSendingOrderSms(petObject);
                            dsMail = orderBL.getEmailIdForSendingOrderMail(petObject);

                            //End
                            p_Var.Result = orderBL.InsertOrdersIntoWeb(orderObject);


                            /* Function to get email id of petitioners/respondents to send email */

                            p_Var.sbuildertmp.Append(" <table width='90%' cellpadding='0' cellspacing='0' style='margin:0px auto; border:1px solid #cccccc; border-bottom:none;  border-right:none' align='center'>");
                            if (lblOrderCategoryName.Text.ToString() == "Interim Order")
                            {
                                p_Var.sbuildertmp.Append("<tr><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'><strong>" + lblOrderCategoryName.Text + " Date:</strong></td><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'>");
                            }
                            else
                            {
                                p_Var.sbuildertmp.Append("<tr><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'><strong>Final Order Date:</strong></td><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'>");
                            }
                            p_Var.sbuildertmp.Append(HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["Date"].ToString()));
                            p_Var.sbuildertmp.Append("</td></tr>");
                            p_Var.sbuildertmp.Append("<tr><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'><strong><strong>Subject:</strong></th><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'>");
                            p_Var.sbuildertmp.Append(dsMail.Tables[0].Rows[0]["OrderTitle"].ToString());
                            p_Var.sbuildertmp.Append("</th></tr>");


                            p_Var.sbuildertmp.Append("</table>");

                            p_Var.sbuildertmp.Append("<br />");
                            p_Var.sbuildertmp.Append("<br/>for details visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                            p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                            p_Var.sbuildertmp.Append("query, you may contact Law Officer at 0172-2569602<br/><br/>");
                            p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.");
                            p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");

                            /* Function to get email id of petitioners/respondents to send email*/

                            if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != "")
                            {
                                foreach (DataRow drRow in dsMail.Tables[0].Rows)
                                {

                                    string strEmailHerc = drRow["Petitioner_Email"].ToString();
                                    if (strEmailHerc.StartsWith(";"))
                                    {
                                        strEmailHerc = strEmailHerc.Substring(1, strEmailHerc.Length - 1);
                                    }

                                    if (lblOrderCategoryName.Text.ToString() == "Interim Order")
                                    {
                                        miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + strEmailHerc.Trim(), "HERC- " + lblOrderCategoryName.Text, "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());

                                    }
                                    else
                                    {
                                        miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + strEmailHerc.Trim(), "HERC- Final Order", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());
                                    }
                                }
                            }

                            /* Code to send sms */

                            if (dsSms.Tables[0].Rows.Count > 0 && dsSms.Tables[0].Rows[0]["Petitioner_Mobile"] != DBNull.Value && dsSms.Tables[0].Rows[0]["Petitioner_Mobile"] != "")
                            {


                                foreach (DataRow drRow in dsSms.Tables[0].Rows)
                                {
                                    //loop through cells in that row
                                    string strUrl = drRow["Petitioner_Mobile"].ToString();
                                    string[] split = strUrl.Split(';');
                                    string textmessage;
                                    ArrayList list = new ArrayList();
                                    foreach (string item in split)
                                    {
                                        list.Add(item.Trim());
                                    }

                                    foreach (string str in list)
                                    {
                                        if (str != string.Empty)
                                        {
                                            string message = dsSms.Tables[0].Rows[0]["Date"].ToString() + ", " + dsSms.Tables[0].Rows[0]["Subject"].ToString();
                                            if (message.Length > 150)
                                            {
                                                message = dsSms.Tables[0].Rows[0]["Date"].ToString() + ", " + dsSms.Tables[0].Rows[0]["Subject"].ToString().Substring(0, 150) + "..." + Environment.NewLine + "For details and disclaimer visit  : http://herc.gov.in";
                                            }
                                            else
                                            {
                                                message = dsSms.Tables[0].Rows[0]["Date"].ToString() + ", " + dsSms.Tables[0].Rows[0]["Subject"].ToString() + Environment.NewLine + "For details and disclaimer visit  : http://herc.gov.in";
                                            }

                                            if (lblOrderCategoryName.Text.ToString() == "Interim Order")
                                            {
                                                textmessage = "HERC Interim Order - ";
                                            }
                                            else
                                            {
                                                textmessage = "HERC Final Order - ";
                                            }

                                            miscellBL.SendsmsApprove(message, str, textmessage);


                                        }

                                    }

                                }
                            }
                            /* End */
                        }


                        /* End */



                    }

                    if (p_Var.Result > 0)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        sbuilderSms.Remove(0, sbuilderSms.Length);
                        foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                        {

                            if (li.Selected == true)
                            {
                                //p_Var.sbuildertmp.Append(li.Value + ";");
                                int statindex = li.Text.IndexOf("(") + 1;
                                p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                                sbuilderSms.Append(li.Value + ";");
                            }

                        }
                        if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                        {
                            p_Var.sbuilder.Append("Record published : " + sbuilder.ToString());
                            p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record Published", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

                        }
                        ///* Code to send sms */
                        char[] splitter = { ';' };
                        PetitionOB petObjectNew = new PetitionOB();
                        DataSet dsSms = new DataSet();
                        string textmessage;
                        string strUrl = sbuilderSms.ToString();
                        string[] split = strUrl.Split(';');
                        ArrayList list = new ArrayList();
                        foreach (string item in split)
                        {
                            list.Add(item.Trim());
                        }


                        string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "");
                        string[] stringSeparators = new string[] { "<br/>" };
                        string[] splitPublicNotice = strpublicnotice.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        ArrayList listPublicNotice = new ArrayList();
                        foreach (string itemPublicNotice in splitPublicNotice)
                        {
                            listPublicNotice.Add(itemPublicNotice.Trim());
                        }

                        foreach (string strPublicNotice in listPublicNotice)
                        {
                            if (strPublicNotice != string.Empty && strPublicNotice != "")
                            {
                                //loop through cells in that row
                                foreach (string str in list)
                                {
                                    if (str != string.Empty)
                                    {

                                        string message = strPublicNotice;

                                        if (message.Length > 150)
                                        {
                                            message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                        }
                                        else
                                        {
                                            message = strPublicNotice.ToString();
                                        }
                                        if (ViewState["OrderCat"].ToString() == "Interim Order")
                                        {
                                            textmessage = "HERC - Record published - ";
                                        }
                                        else
                                        {
                                            textmessage = "HERC - Record published - ";
                                        }

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }

                            }
                        }

                        /* End */
                        Session["msg"] = " Order has been published successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnSendEmailsWithoutEmails_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
            {
                foreach (GridViewRow row in grdOrders.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        p_Var.dataKeyID = Convert.ToInt32(grdOrders.DataKeys[row.RowIndex].Value);
                        orderObject.TempOrderID = p_Var.dataKeyID;
                        orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        orderObject.userID = Convert.ToInt32(Session["User_Id"]);
                        orderObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = orderBL.OrdersUpdateStatus(orderObject);

                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Order has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }

            }
            else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {
                foreach (GridViewRow row in grdOrders.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        p_Var.dataKeyID = Convert.ToInt32(grdOrders.DataKeys[row.RowIndex].Value);
                        orderObject.TempOrderID = p_Var.dataKeyID;
                        orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        orderObject.userID = Convert.ToInt32(Session["User_Id"]);
                        orderObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = orderBL.OrdersUpdateStatus(orderObject);

                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Order has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else
            {
                LinkOB obj_linkOB1 = new LinkOB();
                foreach (GridViewRow row in grdOrders.Rows)
                {
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblOrderID");
                        Label lblOrderCategoryName = (Label)row.FindControl("lblOrderCategoryName");

                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        orderObject.TempOrderID = p_Var.dataKeyID;
                        orderObject.userID = Convert.ToInt32(Session["User_Id"]);
                        orderObject.IpAddress = Miscelleneous_DL.getclientIP();
                        petObject.OrderID = p_Var.dataKeyID;


                        char[] splitter = { ';' };
                        DataSet dsMail = new DataSet();
                        DataSet dsSms = new DataSet();
                        dsSms = orderBL.getMobileNumberForSendingOrderSms(petObject);
                        dsMail = orderBL.getEmailIdForSendingOrderMail(petObject);

                        //End
                        p_Var.Result = orderBL.InsertOrdersIntoWeb(orderObject);


                        /* Function to get email id of petitioners/respondents to send email*/
                        p_Var.sbuildertmp.Append(" <table width='90%' cellpadding='0' cellspacing='0' style='margin:0px auto; border:1px solid #cccccc; border-bottom:none;  border-right:none' align='center'>");
                        if (lblOrderCategoryName.Text.ToString() == "Interim Order")
                        {
                            p_Var.sbuildertmp.Append("<tr><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'><strong>" + lblOrderCategoryName.Text + " Date:</strong></td><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'>");
                        }
                        else
                        {
                            p_Var.sbuildertmp.Append("<tr><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'><strong>Final Order Date:</strong></td><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'>");
                        }
                        p_Var.sbuildertmp.Append(HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["Date"].ToString()));
                        p_Var.sbuildertmp.Append("</td></tr>");
                        p_Var.sbuildertmp.Append("<tr><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'><strong><strong>Subject:</strong></th><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'>");
                        p_Var.sbuildertmp.Append(dsMail.Tables[0].Rows[0]["OrderTitle"].ToString());
                        p_Var.sbuildertmp.Append("</th></tr>");


                        p_Var.sbuildertmp.Append("</table>");

                        p_Var.sbuildertmp.Append("<br />");
                        p_Var.sbuildertmp.Append("For details visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                        p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                        p_Var.sbuildertmp.Append("query, you may contact Law Officer at 0172-2569602<br/><br/>");
                        p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.");
                        p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");

                        /* Function to get email id of petitioners/respondents to send email*/

                        if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != "")
                        {
                            foreach (DataRow drRow in dsMail.Tables[0].Rows)
                            {

                                string strEmailHerc = drRow["Petitioner_Email"].ToString();
                                if (strEmailHerc.StartsWith(";"))
                                {
                                    strEmailHerc = strEmailHerc.Substring(1, strEmailHerc.Length - 1);
                                }

                                if (lblOrderCategoryName.Text.ToString() == "Interim Order")
                                {
                                    miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + strEmailHerc.Trim(), "HERC- " + lblOrderCategoryName.Text, "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());

                                }
                                else
                                {
                                    miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + strEmailHerc.Trim(), "HERC- Final Order", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());
                                }
                            }
                        }

                        /* Code to send sms */

                        if (dsSms.Tables[0].Rows.Count > 0 && dsSms.Tables[0].Rows[0]["Petitioner_Mobile"] != DBNull.Value && dsSms.Tables[0].Rows[0]["Petitioner_Mobile"] != "")
                        {
                            foreach (DataRow drRow in dsSms.Tables[0].Rows)
                            {
                                //loop through cells in that row
                                string strUrl = drRow["Petitioner_Mobile"].ToString();
                                string[] split = strUrl.Split(';');
                                string textmessage;
                                ArrayList list = new ArrayList();
                                foreach (string item in split)
                                {
                                    list.Add(item.Trim());
                                }

                                foreach (string str in list)
                                {
                                    if (str != string.Empty)
                                    {
                                        string message = dsSms.Tables[0].Rows[0]["Date"].ToString() + ", " + dsSms.Tables[0].Rows[0]["Subject"].ToString();
                                        if (message.Length > 150)
                                        {
                                            message = dsSms.Tables[0].Rows[0]["Date"].ToString() + ", " + dsSms.Tables[0].Rows[0]["Subject"].ToString().Substring(0, 150) + "..." + Environment.NewLine + "For details and disclaimer visit  : http://herc.gov.in";
                                        }
                                        else
                                        {
                                            message = dsSms.Tables[0].Rows[0]["Date"].ToString() + ", " + dsSms.Tables[0].Rows[0]["Subject"].ToString() + Environment.NewLine + "For details and disclaimer visit  : http://herc.gov.in";
                                        }

                                        if (lblOrderCategoryName.Text.ToString() == "Interim Order")
                                        {
                                            textmessage = "HERC Interim Order - ";
                                        }
                                        else
                                        {
                                            textmessage = "HERC Final Order - ";
                                        }

                                        miscellBL.SendsmsApprove(message, str, textmessage);


                                    }

                                }

                            }
                        }
                        /* End */
                    }


                    /* End */



                }

                if (p_Var.Result > 0)
                {
                    Session["msg"] = " Order has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }

        }
        catch
        {
            throw;
        }
    }
    protected void btnCancelEmail_Click(object sender, EventArgs e)
    {
        pnlGrid.Visible = true;
        pnlPopUpEmails.Visible = false;
    }
    protected void btnSendEmailsDis_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                StringBuilder sbuilder = new StringBuilder();
                StringBuilder sbuilderSms = new StringBuilder();
                foreach (GridViewRow row in grdOrders.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblOrderTitle = (Label)row.FindControl("lblOrderTitle");
                    ViewState["DisOrder"] = lblOrderTitle.Text;
                    Label lblOrderCategoryName = (Label)row.FindControl("lblOrderCategoryName");
                    ViewState["DisOrderCat"] = lblOrderCategoryName.Text;
                    if ((selCheck.Checked == true))
                    {
                        if (lblOrderCategoryName.Text.ToString() == "Interim Order")
                        {
                            sbuilder.Append(lblOrderCategoryName.Text + "- " + lblOrderTitle.Text + "<br/> ");
                        }
                        else
                        {
                            sbuilder.Append("Final Order - " + lblOrderTitle.Text + "<br/> ");
                        }
                        Label lblid = (Label)row.FindControl("lblOrderID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        orderObject.TempOrderID = p_Var.dataKeyID;
                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                        {
                            orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                        }
                        else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                        {
                            orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        }
                        else
                        {
                            orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        }
                        // orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                        p_Var.Result = orderBL.OrdersUpdateStatus(orderObject);
                    }
                }
                if (p_Var.Result > 0)
                {
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (System.Web.UI.WebControls.ListItem li in chkSendEmailsDis.Items)
                    {

                        if (li.Selected == true)
                        {

                            int statindex = li.Text.IndexOf("(") + 1;
                            p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                            sbuilderSms.Append(li.Value + ";");
                        }

                    }
                    if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                    {
                        p_Var.sbuilder.Append("Record disapproved : " + sbuilder.ToString());
                        p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                        p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                        //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                        string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        p_Var.sbuildertmp.Append(email);
                        miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record disapproved", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

                    }
                    /* Code to send sms */
                    char[] splitter = { ';' };
                    PetitionOB petObjectNew = new PetitionOB();
                    DataSet dsSms = new DataSet();
                    string textmessage;
                    string strUrl = sbuilderSms.ToString();
                    string[] split = strUrl.Split(';');
                    ArrayList list = new ArrayList();
                    foreach (string item in split)
                    {
                        list.Add(item.Trim());
                    }


                    string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "");
                    string[] stringSeparators = new string[] { "<br/>" };
                    string[] splitPublicNotice = strpublicnotice.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    ArrayList listPublicNotice = new ArrayList();
                    foreach (string itemPublicNotice in splitPublicNotice)
                    {
                        listPublicNotice.Add(itemPublicNotice.Trim());
                    }

                    foreach (string strPublicNotice in listPublicNotice)
                    {
                        if (strPublicNotice != string.Empty && strPublicNotice != "")
                        {
                            //loop through cells in that row
                            foreach (string str in list)
                            {
                                if (str != string.Empty)
                                {

                                    string message = strPublicNotice;

                                    if (message.Length > 150)
                                    {
                                        message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                    }
                                    else
                                    {
                                        message = strPublicNotice.ToString();
                                    }
                                    if (ViewState["DisOrderCat"].ToString() == "Interim Order")
                                    {
                                        textmessage = "HERC - Record disapproved - ";
                                    }
                                    else
                                    {
                                        textmessage = "HERC - Record disapproved - ";
                                    }

                                    miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                }

                            }
                        }
                    }


                    /* End */
                    Session["msg"] = "Order has been disapproved successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnSendEmailsWithoutEmailsDis_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grdOrders.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                    Label lblid = (Label)row.FindControl("lblOrderID");
                    p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                    orderObject.TempOrderID = p_Var.dataKeyID;
                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    // orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    p_Var.Result = orderBL.OrdersUpdateStatus(orderObject);
                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "Order has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }

        }
        catch
        {
            throw;
        }
    }
    protected void btnCancelEmailDis_Click(object sender, EventArgs e)
    {
        pnlGrid.Visible = true;
        pnlPopUpEmailsDis.Visible = false;
    }

    #region Function to bind emailid of reviewers status in checkboxlist

    public void bindCheckBoxListWithEmailIDs()
    {

        try
        {
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Orders);
            p_Var.dSetCompare = obj_UserBL.getReviewEmailIds(obj_userOB);
            lblSelectors.Text = "Select the Reviewer(s)";
            lblSelectors.Font.Bold = true;
            chkSendEmails.DataSource = p_Var.dSetCompare;
            chkSendEmails.DataTextField = "newEmail";
            chkSendEmails.DataValueField = "Mobile_No";
            chkSendEmails.DataBind();

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind emailid of reviewers status in checkboxlist

    public void bindCheckBoxListWithApproverEmailIDs()
    {

        try
        {
            lblSelectors.Text = "Select the Publisher(s)";
            lblSelectors.Font.Bold = true;
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Orders);
            p_Var.dSetCompare = obj_UserBL.getPublisherEmailIds(obj_userOB);

            chkSendEmails.DataSource = p_Var.dSetCompare;
            chkSendEmails.DataTextField = "newEmail";
            chkSendEmails.DataValueField = "Mobile_No";
            chkSendEmails.DataBind();

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind emailid of reviewers status in checkboxlist

    public void bindCheckBoxListWithEmailIDDiaapproves()
    {

        try
        {
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Orders);
            p_Var.dSetCompare = obj_UserBL.getReviewEmailIds(obj_userOB);
            lblSelectorsDis.Text = "Select the Reviewer(s)";
            lblSelectorsDis.Font.Bold = true;
            chkSendEmailsDis.DataSource = p_Var.dSetCompare;
            chkSendEmailsDis.DataTextField = "newEmail";
            chkSendEmailsDis.DataValueField = "Mobile_No";
            chkSendEmailsDis.DataBind();

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind emailid of Data entry operator status in checkboxlist

    public void bindCheckBoxListWithDataEntryEmailIDs()
    {

        try
        {
            lblSelectorsDis.Text = "Select the Data Entry Personnel";
            lblSelectorsDis.Font.Bold = true;
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Orders);
            p_Var.dSetCompare = obj_UserBL.getDataEntryEmailIds(obj_userOB);

            chkSendEmailsDis.DataSource = p_Var.dSetCompare;
            chkSendEmailsDis.DataTextField = "newEmail";
            chkSendEmailsDis.DataValueField = "Mobile_No";
            chkSendEmailsDis.DataBind();

        }
        catch
        {
            throw;
        }
    }

    #endregion
}
