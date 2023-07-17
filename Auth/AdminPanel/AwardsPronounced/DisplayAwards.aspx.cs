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
using System.Text;

public partial class Auth_AdminPanel_AwardsPronounced_DisplayAwards : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration zone

    Project_Variables p_Var = new Project_Variables();
    AppealBL appealBL = new AppealBL();
    LinkBL obj_linkBL = new LinkBL();
    LinkOB obj_inkOB = new LinkOB();
    UserBL obj_UserBL = new UserBL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    PetitionOB orderObject = new PetitionOB();
    OrderBL orderBL = new OrderBL();
    PaginationBL pagingBL = new PaginationBL();
    PetitionOB objPetOb = new PetitionOB();
    PetitionBL petBL = new PetitionBL();
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    //End

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

        Session.Remove("OrderYear"); //  Order sessions
        Session.Remove("OrderLng");
        Session.Remove("OrderType");
        Session.Remove("OrderStatus");

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

        Session.Remove("AppealAwardyear");
        Session.Remove("AppealAwardLng");
        Session.Remove("AppealAwardStatus");

        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": View EO Award";
        this.Page.Title = " EO Award: HERC";

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
            btnAddAward.Visible = false;
            if (Session["AwardYear"] != null)
            {
                bindRtiYearinDdl(); // this is bind years
                ddlYear.SelectedValue = Session["AwardYear"].ToString();
            }
            else
            {
                bindRtiYearinDdl(); // this is bind years
            }
            if (Session["AwardLng"] != null)
            {
                bindropDownlistLang();
                ddlLanguage.SelectedValue = Session["AwardLng"].ToString();
            }
            else
            {
                bindropDownlistLang();
            }
            if (Session["AwardStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["AwardStatus"].ToString();
                Bind_Grid("", "");
            }
            else
            {
                binddropDownlistStatus();
            }

        }
    }
    protected void btnAddAward_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/AwardsPronounced/") + "AddAwards.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));
    }

    //Area for all the dropDownlist events

    #region dropDownlist ddlLanguage selectedIndexChanged event

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue != "0")
        {
            binddropDownlistStatus();
            grdOrders.Visible = false;
            Session["AwardLng"] = ddlLanguage.SelectedValue;
        }

        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
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
            Session["AwardStatus"] = ddlStatus.SelectedValue;
        }

        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
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



    //Area for all the Gridview events

    #region Gridview grdOrders rowCommand event

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

                if (e.CommandName == "delete")
                {


                    orderObject.AppealId = Convert.ToInt32(e.CommandArgument.ToString());
                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    HiddenField lblStatus = (HiddenField)row.Cells[0].FindControl("Hystatus");

                    orderObject.StatusId = Convert.ToInt16(lblStatus.Value);

                    p_Var.Result = appealBL.Delete_Pending_Approved_RecordAward(orderObject);
                    if (p_Var.Result > 0)
                    {
                        if (ddlStatus.SelectedValue == "8")
                        {
                            Session["msg"] = "Award pronounced has been deleted permanently.";
                        }
                        else
                        {
                            Session["msg"] = "Award pronounced has been deleted successfully.";
                        }

                        obj_audit.ActionType = "D";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        Label lblAppealNumber = row.FindControl("lblAppealNumber") as Label;
                        //if (lblAppealNumber == null) { return; }
                        obj_audit.Title = lblAppealNumber.Text;
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);
                        // Session["msg"] = "Award pronounced has been deleted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                    else
                    {
                        Session["msg"] = "Award pronounced has not been deleted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
                else if (e.CommandName == "Restore")
                {

                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    orderObject.AppealId = Convert.ToInt32(e.CommandArgument);
                    p_Var.Result = appealBL.Web_Review_Appeal_Restore(orderObject);
                    if (p_Var.Result > 0)
                    {

                        obj_audit.ActionType = "R";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        Label lblAppealNumber = row.FindControl("lblAppealNumber") as Label;
                        //if (lblAppealNumber == null) { return; }
                        obj_audit.Title = lblAppealNumber.Text;
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "Award pronounced has been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                    else
                    {
                        Session["msg"] = "Award pronounced has not been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                }
                else if (e.CommandName == "Audit")
                {

                    DataSet dSetAuditTrail = new DataSet();
                    objPetOb.PetitionId = Convert.ToInt32(e.CommandArgument);
                    objPetOb.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.AwardPronounced);
                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    objPetOb.ModuleType = null;
                    dSetAuditTrail = petBL.AuditTrailRecords(objPetOb);
                    Label lblprono = row.FindControl("lblAppealNumber") as Label;
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

    #region Gridview grdOrders rowCreated event

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

    #region Gridview grdOrders rowDataBound events

    protected void grdOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
            ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
            //This is for delete/permanently delete 3 june 2013 

            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
            {

                BtnDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to purge Award pronounced against Appeal No- " + DataBinder.Eval(e.Row.DataItem, "appealno") + "')");

                BtnRestore.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to restore Award pronounced against Appeal No- " + DataBinder.Eval(e.Row.DataItem, "appealno") + "')");

            }
            else
            {

                BtnDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete Award pronounced against Appeal No- " + DataBinder.Eval(e.Row.DataItem, "appealno") + "')");

                BtnRestore.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to restore Award pronounced against Appeal No- " + DataBinder.Eval(e.Row.DataItem, "appealno") + "')");

            }
        }

        //END
    }

    #endregion

    #region Gridview grdOrders rowDeleting events

    protected void grdOrders_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    #endregion

    //End

    //Area for all the buttons, linkButtons click events 

    #region button btnForReview click event to send record for review

    protected void btnForReview_Click(object sender, EventArgs e)
    {

        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;


    }

    #endregion

    #region button btnForApprove click event to send record for approve

    protected void btnForApprove_Click(object sender, EventArgs e)
    {

        pnlPopUpEmails.Visible = true;
        pnlGrid.Visible = false;
        bindCheckBoxListWithApproverEmailIDs();


    }

    #endregion

    #region button btnApprove click event to approve  the record

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;

        //ApproveRecords();
    }

    #endregion

    #region button btnDisapprove click event to disapprove record

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
        //this.Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt16(ddlDepartment.SelectedValue), pageIndex);
        this.Bind_Grid("", "");
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
                btnAddAward.Visible = true;


                //code written on date 23sep 2013
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end
            }
            else
            {
                btnAddAward.Visible = false;
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

    #region Function to bind AwardPronounced in gridView

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

            orderObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            orderObject.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            orderObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            orderObject.year = ddlYear.SelectedValue.ToString();
            p_Var.dSet = appealBL.getAwardPronounced(orderObject, out p_Var.k);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    grdOrders.Columns[10].HeaderText = "Purge";
                }
                else
                {
                    grdOrders.Columns[10].HeaderText = "Delete";
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
                            Label lblOrderID = (Label)row.FindControl("lblReviewAppealID");

                            orderObject.TempRAId = Convert.ToInt32(lblOrderID.Text);

                            p_Var.dSetCompare = appealBL.ReviewAppealIDforComparision(orderObject);
                            for (p_Var.i = 0; p_Var.i < p_Var.dSetCompare.Tables[0].Rows.Count; p_Var.i++)
                            {
                                if (p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["RA_Id"] != DBNull.Value)
                                {
                                    if (Convert.ToInt32(lblOrderID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["RA_Id"]))
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
                        //  btnDisApprove.Visible = false;
                    }

                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][6]) == true)
                    {
                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                        {
                            //grdOrders.Columns[6].Visible = true;
                            grdOrders.Columns[9].Visible = false;
                            grdOrders.Columns[11].Visible = true;
                            grdOrders.Columns[12].Visible = false;
                        }
                        else
                        {
                            grdOrders.Columns[11].Visible = false;
                            grdOrders.Columns[9].Visible = true;
                        }
                    }
                    else
                    {
                        grdOrders.Columns[9].Visible = false;
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {
                        ////grdOrders.Columns[10].Visible = true;


                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                            {
                                grdOrders.Columns[10].Visible = true;
                                grdOrders.Columns[12].Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    grdOrders.Columns[10].Visible = true;
                                    grdOrders.Columns[12].Visible = false;
                                }
                                else
                                {
                                    if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdOrders.Columns[10].Visible = true;
                                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                                        {
                                            grdOrders.Columns[12].Visible = true;
                                        }
                                        else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                        {
                                            grdOrders.Columns[12].Visible = true;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                            {
                                                if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                                {
                                                    grdOrders.Columns[12].Visible = true;
                                                }
                                                else
                                                {
                                                    grdOrders.Columns[12].Visible = false;
                                                }
                                            }
                                            else
                                            {
                                                grdOrders.Columns[12].Visible = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        grdOrders.Columns[10].Visible = false;
                                        grdOrders.Columns[12].Visible = false;
                                    }
                                }
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                            {
                                grdOrders.Columns[10].Visible = true;
                                grdOrders.Columns[12].Visible = false;
                            }

                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdOrders.Columns[10].Visible = true;
                            }

                        }

                        //End  
                    }
                    else
                    {
                        grdOrders.Columns[10].Visible = false;
                    }
                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {

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

    #region Function to approve award pronounced

    public void ApproveRecords()
    {

    }

    #endregion

    //End

    //Codes for sorting of the grid

    protected void grdOrders_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid(e.SortExpression, sortOrder);
    }

    #region gridView grdOrders pageIndexChanging Event zone

    protected void grdOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        grdOrders.Visible = false;
        Session["AwardYear"] = ddlYear.SelectedValue;

        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }


    #region Function to bind Petition Year

    public void bindRtiYearinDdl()
    {

        p_Var.dSet = appealBL.GetYearAward_Admin();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = p_Var.dSet;
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "year";
            ddlYear.DataBind();
        }
        else
        {
            ddlYear.Items.Add(new ListItem("Select", "0"));
        }
    }

    #endregion

    public void BindGridDetails()
    {
        if (ddlStatus.SelectedValue == "0")
        {
            grdAwardPdf.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {
            grdAwardPdf.Visible = true;

            orderObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            orderObject.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            orderObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            orderObject.year = ddlYear.SelectedValue.ToString();
            p_Var.dSet = appealBL.getAwardPronounced(orderObject, out p_Var.k);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                grdAwardPdf.DataSource = p_Var.dSet;
                grdAwardPdf.DataBind();
                p_Var.dSet = null;
            }
        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void btnPdf_Click(object sender, EventArgs e)
    {
        BindGridDetails();
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Award_" + System.DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdAwardPdf.AllowPaging = false;
        grdAwardPdf.DataBind();
        grdAwardPdf.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();

    }
    protected void grdAwardPdf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal ltrlConnectedFile1 = (Literal)e.Row.FindControl("ltrlConnectedFile1");
            orderObject.TempRAId = Convert.ToInt32(grdAwardPdf.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = appealBL.getFileNameAwardProunced(orderObject);
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != null && p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != "")
                    {
                        p_Var.sbuilder.Append("<asp:label >" + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "," + p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + "</Label>");
                    }
                    else
                    {
                        p_Var.sbuilder.Append("<asp:label >" + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "</Label>");
                    }

                    p_Var.sbuilder.Append("<br/><hr/>");

                }
                ltrlConnectedFile1.Text = p_Var.sbuilder.ToString();

            }

            //This is for Decoding
            Label lblApplicantName = (Label)e.Row.FindControl("lblApplicantName");
            Label lblRespondentName = (Label)e.Row.FindControl("lblRespondentName");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");

            if (lblApplicantName.Text != null && lblApplicantName.Text != "")
            {
                lblApplicantName.Text = HttpUtility.HtmlDecode(lblApplicantName.Text);
            }
            if (lblRespondentName.Text != null && lblRespondentName.Text != "")
            {
                lblRespondentName.Text = HttpUtility.HtmlDecode(lblRespondentName.Text);
            }
            if (lblSubject.Text != null && lblSubject.Text != "")
            {
                lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            }
            if (lblRemarks.Text != null && lblRemarks.Text != "")
            {
                lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
            }


        }
    }
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
                        Label lblAppealNumber = (Label)row.FindControl("lblAppealNumber");
                        ViewState["AwardProunNumber"] = lblAppealNumber.Text;
                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblReviewAppealID");
                            sbuilder.Append("<b>Award pronounced - " + lblAppealNumber.Text + "<br/></b>");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            orderObject.TempRAId = p_Var.dataKeyID;
                            orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            orderObject.userID = Convert.ToInt32(Session["User_Id"]);
                            orderObject.IpAddress = miscellBL.IpAddress();
                            //   petObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
                            p_Var.Result = appealBL.AppealAwardUpdateStatus(orderObject);

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
                            p_Var.sbuilder.Append("Record pending for Review: " + sbuilder.ToString() + "<br/>");
                            p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "EO - Record pending for Review", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

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
                                        textmessage = "EO - Record pending for review -";

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }

                            }
                        }

                        /* End */
                        Session["msg"] = "Award pronounced has been sent for review successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdOrders.Rows)
                    {
                        Label lblAppealNumber = (Label)row.FindControl("lblAppealNumber");
                        ViewState["AwardProunNumber"] = lblAppealNumber.Text;
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblReviewAppealID");
                            sbuilder.Append("<b>Award pronounced - " + lblAppealNumber.Text + "<br/></b>");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            orderObject.TempRAId = p_Var.dataKeyID;
                            orderObject.userID = Convert.ToInt32(Session["User_Id"]);
                            orderObject.IpAddress = miscellBL.IpAddress();
                            orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            p_Var.Result = appealBL.AppealAwardUpdateStatus(orderObject);


                        }
                    }
                    if (p_Var.Result > 0)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
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
                            p_Var.sbuilder.Append("Record pending for Publish : " + sbuilder.ToString() + "<br/>");
                            p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "EO - Record pending for Publish", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

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
                                        textmessage = "EO - Record pending for publish -";

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }

                            }
                        }

                        /* End */
                        Session["msg"] = "Award pronounced has been sent for publish successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
                        Label lblAppealNumber = (Label)row.FindControl("lblAppealNumber");
                        ViewState["AwardProunNumber"] = lblAppealNumber.Text;

                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblReviewAppealID");
                            HiddenField Hydold = (HiddenField)row.FindControl("Hydold");
                            sbuilder.Append("<b>Award pronounced - " + lblAppealNumber.Text + "<br/> </b>");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            orderObject.userID = Convert.ToInt32(Session["User_Id"]);
                            orderObject.IpAddress = miscellBL.IpAddress();
                            orderObject.TempRAId = p_Var.dataKeyID;
                            if (Hydold.Value != null && Hydold.Value != "")
                            {
                                objPetOb.AppealId = Convert.ToInt32(Hydold.Value);
                            }
                            else
                            {
                                objPetOb.AppealId = null;
                            }

                            char[] splitter = { ';' };
                            DataSet dsMail = new DataSet();
                            DataSet dsSms = new DataSet();
                            dsSms = appealBL.getEmailIdForSendingAwardAppealSMS(objPetOb);
                            dsMail = appealBL.getEmailIdForSendingAwardAppealMail(objPetOb);


                            p_Var.Result = appealBL.ApproveAwardPronounced(orderObject);

                            /* Function to get email id of petitioners/respondents to send email*/
                            p_Var.sbuildertmp.Append("In Appeal " + dsMail.Tables[0].Rows[0]["Appeal_Number"].ToString() + " of " + (dsMail.Tables[0].Rows[0]["Applicant_Name"].ToString() + ", Award has been pronounced on " + dsMail.Tables[0].Rows[0]["Date"].ToString() + "."));
                            p_Var.sbuildertmp.Append("<br />");
                            p_Var.sbuildertmp.Append("For details visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                            p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                            p_Var.sbuildertmp.Append("query, you may contact at 0172-2572299<br/><br/>");

                            p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.");
                            p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");

                            if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != "")
                            {
                                foreach (DataRow drRow in dsMail.Tables[0].Rows)
                                {
                                    //loop through cells in that row
                                    string strEmailHerc = drRow["Petitioner_Email"].ToString();
                                    if (strEmailHerc.StartsWith(";"))
                                    {
                                        strEmailHerc = strEmailHerc.Substring(1, strEmailHerc.Length - 1);
                                    }

                                    miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + strEmailHerc.Trim().Trim(), "EO - Award", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());

                                }
                            }

                            /* End */


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
                                            string message = "In Appeal " + dsSms.Tables[0].Rows[0]["Appeal_Number"].ToString() + " of " + dsSms.Tables[0].Rows[0]["Applicant_Name"].ToString() + ", Award has been pronounced on " + dsSms.Tables[0].Rows[0]["Date"] + Environment.NewLine + "For details and disclaimer visit  : http://herc.gov.in";

                                            textmessage = "EO - Award: ";


                                            miscellBL.SendsmsApprove(message, str, textmessage);


                                        }

                                    }

                                }
                            }
                            /* End */



                        }
                    }
                    if (p_Var.Result > 0)
                    {

                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
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
                            p_Var.sbuilder.Append("Record Published : " + sbuilder.ToString() + "<br/>");
                            p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "EO - Record Published", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

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
                                        textmessage = "EO - Record published -";

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }
                            }
                        }

                        /* End */

                        Session.Remove("AwardID");
                        Session["msg"] = "Award pronounced has been published successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
                        Label lblid = (Label)row.FindControl("lblReviewAppealID");
                        // p_Var.dataKeyID = Convert.ToInt32(grdPetition.DataKeys[row.RowIndex].Value);
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        orderObject.TempRAId = p_Var.dataKeyID;
                        orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        orderObject.userID = Convert.ToInt32(Session["User_Id"]);
                        orderObject.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = appealBL.AppealAwardUpdateStatus(orderObject);

                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Award pronounced has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
                        Label lblid = (Label)row.FindControl("lblReviewAppealID");
                        orderObject.userID = Convert.ToInt32(Session["User_Id"]);
                        orderObject.IpAddress = miscellBL.IpAddress();
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        orderObject.TempRAId = p_Var.dataKeyID;
                        orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        p_Var.Result = appealBL.AppealAwardUpdateStatus(orderObject);


                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Award pronounced has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
                        Label lblid = (Label)row.FindControl("lblReviewAppealID");
                        HiddenField Hydold = (HiddenField)row.FindControl("Hydold");

                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        orderObject.userID = Convert.ToInt32(Session["User_Id"]);
                        orderObject.IpAddress = miscellBL.IpAddress();
                        orderObject.TempRAId = p_Var.dataKeyID;
                        if (Hydold.Value != null && Hydold.Value.ToString() != "")
                        {
                            objPetOb.AppealId = Convert.ToInt32(Hydold.Value);
                        }
                        else
                        {
                            objPetOb.AppealId = null;
                        }
                        //Codes for the sending emails

                        char[] splitter = { ';' };
                        DataSet dsMail = new DataSet();
                        DataSet dsSms = new DataSet();
                        dsSms = appealBL.getEmailIdForSendingAwardAppealSMS(objPetOb);
                        dsMail = appealBL.getEmailIdForSendingAwardAppealMail(objPetOb);

                        //End
                        p_Var.Result = appealBL.ApproveAwardPronounced(orderObject);

                        /* Function to get email id of petitioners/respondents to send email*/
                        p_Var.sbuildertmp.Append("In Appeal " + dsMail.Tables[0].Rows[0]["Appeal_Number"].ToString() + " of " + (dsMail.Tables[0].Rows[0]["Applicant_Name"].ToString() + ", Award has been pronounced on " + dsMail.Tables[0].Rows[0]["Date"].ToString() + "."));
                        p_Var.sbuildertmp.Append("<br />");
                        p_Var.sbuildertmp.Append("For details visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                        p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                        p_Var.sbuildertmp.Append("query, you may contact at 0172-2572299<br/><br/>");

                        p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.");
                        p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");

                        if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != "")
                        {
                            foreach (DataRow drRow in dsMail.Tables[0].Rows)
                            {
                                //loop through cells in that row
                                string strEmailHerc = drRow["Petitioner_Email"].ToString();
                                if (strEmailHerc.StartsWith(";"))
                                {
                                    strEmailHerc = strEmailHerc.Substring(1, strEmailHerc.Length - 1);
                                }

                                miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + strEmailHerc.Trim().Trim(), "EO - Award", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());

                            }
                        }

                        /* End */


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
                                        string message = "In Appeal " + dsSms.Tables[0].Rows[0]["Appeal_Number"].ToString() + " of " + dsSms.Tables[0].Rows[0]["Applicant_Name"].ToString() + ", Award has been pronounced on " + dsSms.Tables[0].Rows[0]["Date"] + Environment.NewLine + "For details and disclaimer visit  : http://herc.gov.in";

                                        textmessage = "EO - Award: ";


                                        miscellBL.SendsmsApprove(message, str, textmessage);


                                    }

                                }

                            }
                        }
                        /* End */



                    }
                }
                if (p_Var.Result > 0)
                {
                    Session.Remove("AwardID");
                    Session["msg"] = "Award pronounced has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
                    Label lblAppealNumber = (Label)row.FindControl("lblAppealNumber");
                    ViewState["DisAwardProunNumber"] = lblAppealNumber.Text;

                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblReviewAppealID");
                        sbuilder.Append("<b>Award pronounced - " + lblAppealNumber.Text + "<br/></b>");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        orderObject.TempRAId = p_Var.dataKeyID;
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
                        //orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                        p_Var.Result = appealBL.AppealAwardUpdateStatus(orderObject);


                    }
                }
                if (p_Var.Result > 0)
                {
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (System.Web.UI.WebControls.ListItem li in chkSendEmailsDis.Items)
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
                        p_Var.sbuilder.Append("Record disapproved: " + sbuilder.ToString() + "<br/>");
                        p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                        p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                        //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                        string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        p_Var.sbuildertmp.Append(email);
                        miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "EO - Record disapproved", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

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
                                    textmessage = "EO - Record disapproved -";

                                    miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                }

                            }

                        }
                    }

                    /* End */
                    Session["msg"] = "Award pronounced has been disapproved successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
                    Label lblid = (Label)row.FindControl("lblReviewAppealID");
                    // p_Var.dataKeyID = Convert.ToInt32(grdPetition.DataKeys[row.RowIndex].Value);
                    p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                    orderObject.TempRAId = p_Var.dataKeyID;
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
                    //orderObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    p_Var.Result = appealBL.AppealAwardUpdateStatus(orderObject);


                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "Award pronounced has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.AwardPronounced);
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
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.AwardPronounced);
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
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.AwardPronounced);
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
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.AwardPronounced);
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
