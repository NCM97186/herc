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
using System.Text;

public partial class Auth_AdminPanel_Menu_Management_View_CMS_Menu : System.Web.UI.Page
{
    //Area for all the variables declarations

    #region Data declaration zone

    LinkOB lnkObject = new LinkOB();
    LinkBL obj_linkBL = new LinkBL();
    DataSet dSet = new DataSet();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Project_Variables p_Var = new Project_Variables();
    UserBL obj_UserBL = new UserBL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petBL = new PetitionBL();
    PaginationBL pagingBL = new PaginationBL();
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion

    //End

    //Area for page load event

    #region page load event zone

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

        Session.Remove("TariffCategory"); //  Tariff sessions
        Session.Remove("TariffLng");
        Session.Remove("TariffDeptt");
        Session.Remove("TariffType");
        Session.Remove("TariffStatus");

        Session.Remove("OrderYear"); //  Order sessions
        Session.Remove("OrderLng");
        Session.Remove("OrderType");
        Session.Remove("OrderStatus");
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
        lblModulename.Text = ": View  Contents";
        this.Page.Title = " Content: HERC";

        if (!IsPostBack)
        {
            DataSet dsprv = new DataSet();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_RoleBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }

            ViewState["sortOrder"] = "";
            //lblPageSize.Visible = false;
            //ddlPageSize.Visible = false;
            //ddlPageSize.Visible = false;
            //lblPageSize.Visible = false;
            BtnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            btnAddNewPage.Visible = false;
            BtnForReview.Visible = false;
            PLanguage.Visible = false;
            if (Session["menuDeptt"] != null)
            {
                Get_Deptt_Name();    // To get Deptt Name
                ddlDepartment.SelectedValue = Session["menuDeptt"].ToString();
            }
            else
            {
                Get_Deptt_Name();
            }
           
            if (Session["menulang"] != null)
            {
                bindropDownlistLang();
                ddlLanguage.SelectedValue = Session["menulang"].ToString();
            }
            else
            {
                bindropDownlistLang();
            }
            if (Session["menuposition"] != null)
            {
                bindMenuPosition();
                ddlMenuPosition.SelectedValue = Session["menuposition"].ToString();
            }
            else
            {
                bindMenuPosition();
            }
            if (Session["Role_Id"] == null || Session["Role_Id"].ToString() == "0" || Session["Role_Id"].ToString() == "")
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }
            else
            {
                if (Session["menulst"] != null)
                {
                    bindMenu_ListBox(Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
                    lstCMSMenu.SelectedValue = Session["menulst"].ToString();
                }
                else
                {
                    bindMenu_ListBox(Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
                }
            }

            if (Session["menustatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["menustatus"].ToString();
                Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue),Convert.ToInt32(ddlDepartment.SelectedValue));
            }
            else
            {
                binddropDownlistStatus();
            }
        }
    }

    #endregion

    //End

    //Area for all buttons, imagebuttons, linkbuttons click events

    #region Button btnAddNewPage click event to add new menu

    protected void btnAddNewPage_Click(object sender, EventArgs e)
    {
        try
        {

            lnkObject.PositionId = Convert.ToInt32(Request.QueryString["position"]);
            Response.Redirect(ResolveUrl("~/auth/adminpanel/Menu_Management/") +"Add_New_Menu.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]) + "& position=" + Convert.ToInt32(lnkObject.PositionId));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region button btnTop click event to move menu on top

    protected void btnTop_Click(object sender, EventArgs e)
    {

    }

    #endregion

    #region button btnBottom click event to move menu down

    protected void btnBottom_Click(object sender, EventArgs e)
    {

    }

    #endregion

    #region button btnLeft click event to move menu left

    protected void btnLeft_Click(object sender, EventArgs e)
    {
        Menu_ManagementBL _menuBL = new Menu_ManagementBL();
        lnkObject.LinkParentId = Convert.ToInt32(lstCMSMenu.SelectedValue);
        lnkObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
        dSet = _menuBL.get_Menu_level_Link_Web(lnkObject);
        if (dSet.Tables[0].Rows.Count > 0)
        {
            //setLLevel(Convert.ToInt32(dSet.Tables[0].Rows[0]["link_level"]), Convert.ToInt32(lstCMSMenu.SelectedValue));
            lstCMSMenu.Items.Clear();
            bindMenu_ListBox(Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue),Convert.ToInt32(ddlDepartment.SelectedValue));
        }
    }

    #endregion

    #region button btnRight click event to move menu right

    protected void btnRight_Click(object sender, EventArgs e)
    {

    }

    #endregion

    #region button btnApprove click event to approve menu

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;

        //ChkApprove_Disapprove();
    }

    #endregion

    #region button btnForReview click event to send menu for review

    protected void BtnForReview_Click(object sender, EventArgs e)
    {

        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;
       
    }

    #endregion

    #region button btnForApprove click event to move menu for approval

    protected void btnForApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        pnlGrid.Visible = false;
        bindCheckBoxListWithApproverEmailIDs();
        
       
    }

    #endregion

    #region button btnDisApprove click event to disapprove menu

    protected void btnDisApprove_Click(object sender, EventArgs e)
    {


        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
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
        this.Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue),Convert.ToInt32(ddlDepartment.SelectedValue));
    }

    #endregion

    //End

    //Area for all gridView, repeater, datalist events

    #region gridView grdCMSMenu rowCreated event zone

    protected void grdCMSMenu_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == DataControlRowState.Normal | e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
            CheckBox chkBxHeader = (CheckBox)this.grdCMSMenu.HeaderRow.FindControl("chkSelectAll");
            //Add client side function childclick on check boxes
            chkBxSelect.Attributes.Add("onclick", string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID));

        }

    }

    #endregion

    #region gridview grdCMSMenu rowCommand event zone

    protected void grdCMSMenu_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
        Label lblMenuName = (Label)row.Cells[0].FindControl("lblMenuName"); 
        if (e.CommandName == "Delete")
        {
			//GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            Menu_ManagementBL menuDeleteBL = new Menu_ManagementBL();
            
            Label lblStatus = (Label)row.Cells[0].FindControl("lblStatus");
            
            lnkObject.TempLinkId = Convert.ToInt32(e.CommandArgument);
            lnkObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
            lnkObject.StatusId = Convert.ToInt32(lblStatus.Text);

                p_Var.Result = menuDeleteBL.Delete_Pending_Approved_Record(lnkObject);
                if (p_Var.Result > 0)
                {

                    obj_audit.ActionType = "D";
                    obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                    obj_audit.UserName = Session["UserName"].ToString();
                    obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                    obj_audit.IpAddress = miscellBL.IpAddress();
                    obj_audit.Title =lblMenuName.Text;
                    obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);
                    Session["msg"] = "Content has been deleted successfully.";

                    if (ddlMenuPosition.SelectedValue == "1")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=1";
                    }
                    else if (ddlMenuPosition.SelectedValue == "2")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=2";
                    }
                    else if (ddlMenuPosition.SelectedValue == "3")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=3";
                    }
                    else if (ddlMenuPosition.SelectedValue == "4")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=4";
                    }
                    else if (ddlMenuPosition.SelectedValue == "5")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=5";
                    }
                    else if (ddlMenuPosition.SelectedValue == "6")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=6";
                    }
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
                else
                {
                    Session["msg"] = "Content can't delete because it has submenu.If you want to delete this menu, first delete it's submenu";
                    if (ddlMenuPosition.SelectedValue == "1")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=1";
                    }
                    else if (ddlMenuPosition.SelectedValue == "2")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=2";
                    }
                    else if (ddlMenuPosition.SelectedValue == "3")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=3";
                    }
                    else if (ddlMenuPosition.SelectedValue == "4")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=4";
                    }
                    else if (ddlMenuPosition.SelectedValue == "5")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=5";
                    }
                    else if (ddlMenuPosition.SelectedValue == "6")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=6";
                    }
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
           // }
        }

        else if (e.CommandName == "Restore")
        {

            LinkOB obj_linkOBNew = new LinkOB();
            obj_linkOBNew.TempLinkId = Int32.Parse(e.CommandArgument.ToString());


            p_Var.Result = obj_linkBL.updateWebStatusDelete(obj_linkOBNew);
            if (p_Var.Result > 0)
            {

                obj_audit.ActionType = "R";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                obj_audit.IpAddress = miscellBL.IpAddress();
                obj_audit.Title = lblMenuName.Text;
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                Session["msg"] = "Content has been restored successfully.";
                if (ddlMenuPosition.SelectedValue == "1")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=1";
                }
                else if (ddlMenuPosition.SelectedValue == "2")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=2";
                }
                else if (ddlMenuPosition.SelectedValue == "3")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=3";
                }
                else if (ddlMenuPosition.SelectedValue == "4")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=4";
                }
                else if (ddlMenuPosition.SelectedValue == "5")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=5";
                }
                else if (ddlMenuPosition.SelectedValue == "6")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=6";
                }
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
           
            else
            {
                Session["msg"] = "Content has not been restored successfully.";
                if (ddlMenuPosition.SelectedValue == "1")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=1";
                }
                else if (ddlMenuPosition.SelectedValue == "2")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=2";
                }
                else if (ddlMenuPosition.SelectedValue == "3")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=3";
                }
                else if (ddlMenuPosition.SelectedValue == "4")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=4";
                }
                else if (ddlMenuPosition.SelectedValue == "5")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=5";
                }
                else if (ddlMenuPosition.SelectedValue == "6")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=6";
                }
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }
        else if (e.CommandName == "Audit")
        {
			//GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            DataSet dSetAuditTrail = new DataSet();
            petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
            petObject.ModuleID = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Menu);
            petObject.ModuleType = null;
            dSetAuditTrail = petBL.AuditTrailRecords(petObject);
            Label lblprono = row.FindControl("lblMenuName") as Label;
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

    #endregion

    #region gridview grdCMSMenu RowDataBound event zone

    protected void grdCMSMenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.Image img = e.Row.FindControl("imgedit") as System.Web.UI.WebControls.Image;
            System.Web.UI.WebControls.Image img1 = e.Row.FindControl("emgnotedit") as System.Web.UI.WebControls.Image;
            ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
            lnkObject.LinkTypeId = Convert.ToInt32(grdCMSMenu.DataKeys[e.Row.RowIndex].Value);
            p_Var.dSet = obj_linkBL.links_web_Get_Link_Id_ForEdit(lnkObject);

            for (int i = 0; i < p_Var.dSet.Tables[0].Rows.Count; i++)
            {
                if (p_Var.dSet.Tables[0].Rows[i]["Link_Id"] != DBNull.Value)
                {

                    if (Convert.ToInt32(grdCMSMenu.DataKeys[e.Row.RowIndex].Value) == Convert.ToInt32(p_Var.dSet.Tables[0].Rows[i]["Link_Id"]))
                    {

                        img.Visible = false;
                        img1.Visible = true;
                        img1.Height = 10;
                        img1.Width = 10;


                    }
                    else
                    {
                        img1.Visible = false;
                        img.Visible = true;

                    }

                   
                }
            }

            
            //This is for delete/permanently delete 2 july 2013 

            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                
                ImageButton BtnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                BtnDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to purge content- " +HttpUtility.HtmlDecode( DataBinder.Eval(e.Row.DataItem, "name").ToString()) + "')");

                ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
                BtnRestore.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to restore this content- " + HttpUtility.HtmlDecode(DataBinder.Eval(e.Row.DataItem, "name").ToString()) + "')");


            }
            else
            {

                ImageButton BtnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                BtnDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete content- " + HttpUtility.HtmlDecode(DataBinder.Eval(e.Row.DataItem, "name").ToString()) + "')");

            }

            //END
        
        }
    }

    #endregion

    //End

    // Area for all the dropDownlist, listbox events

    #region dropDownlist ddlLanguage selectedIndexChanged event zone

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        grdCMSMenu.Visible = false;
        //ddlPageSize.Visible = false;
        //lblPageSize.Visible = false;
        //rptPager.Visible = false;
        Session["menulang"] = ddlLanguage.SelectedValue;
        bindMenu_ListBox(Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
    }

    #endregion

    #region dropDownlist ddlStatus selectedIndexChanged event zone

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "0")
        {
            grdCMSMenu.Visible   = false;
            BtnForReview.Visible = false;
            btnApprove.Visible   = false;
            BtnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            binddropDownlistStatus();
            //ddlPageSize.Visible  = false;
            //lblPageSize.Visible  = false;
            //rptPager.Visible     = false;
        }
        else
        {
            if (lstCMSMenu.SelectedValue.ToString() != null && lstCMSMenu.SelectedValue.ToString() != "")
            {
                
                Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue),Convert.ToInt32(ddlDepartment.SelectedValue));
                Session["menustatus"] = ddlStatus.SelectedValue;
            }
        }
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue),Convert.ToInt32(ddlDepartment.SelectedValue));
    }

    #endregion

    #region dropDownlist ddlMenuPosition selectedIndexChanged event zone

    protected void ddlMenuPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["menuposition"] = ddlMenuPosition.SelectedValue;
        bindMenu_ListBox(Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue),Convert.ToInt32(ddlDepartment.SelectedValue));
        binddropDownlistStatus();
        grdCMSMenu.Visible = false;
        //ddlPageSize.Visible = false;
        //lblPageSize.Visible = false;
        //rptPager.Visible = false;
    }

    #endregion

    #region dropDownlist ddlDepartment selectedIndexChanged event

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        grdCMSMenu.Visible = false;
        //ddlPageSize.Visible = false;
        //lblPageSize.Visible = false;
        //rptPager.Visible    = false;
        Session["menuDeptt"] = ddlDepartment.SelectedValue;
        Session["menulang"] = ddlLanguage.SelectedValue;
        bindMenu_ListBox(Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
    }

    #endregion

    #region listBox lstCMSMenu selectedIndexChanged event zone

    protected void lstCMSMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["menulst"] = lstCMSMenu.SelectedValue;
        binddropDownlistStatus();
        //ddlPageSize.Visible = false;
        //lblPageSize.Visible = false;
        //rptPager.Visible    = false;
        grdCMSMenu.Visible  = false;

    }

    #endregion

    //End

    //Area for all the user-defined functions

    #region Function to bind dropDownlist with menu position

    public void bindMenuPosition()
    {
        Menu_ManagementBL obj_menuManagementBL = new Menu_ManagementBL();
        try
        {
            dSet = obj_menuManagementBL.getMenuPosition();
            ddlMenuPosition.DataSource = dSet.Tables[0];
            ddlMenuPosition.DataTextField = "Position";
            ddlMenuPosition.DataValueField = "Position_Id";
            ddlMenuPosition.DataBind();
            //Session["mposition"] = ddlMenuPosition.SelectedValue;
        }
        catch
        {
            throw;
        }
        finally
        {
            obj_menuManagementBL = null;
        }
    }

    #endregion

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
                    usrObject.english = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.English);
                    usrObject.hindi = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(usrObject.english).Append(",");
                    p_Var.sbuilder.Append(usrObject.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true)
                {
                    usrObject.hindi = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(usrObject.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
                {
                    usrObject.english = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.English);

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
               // Session["mlang"] = ddlLanguage.SelectedValue;
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
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft)).Append(",");
                btnAddNewPage.Visible = true;

                //code written on date 16 sep 2013
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(","); 

                //end
            }
            else
            {
                btnAddNewPage.Visible = false;
            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)).Append(",");

            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover)).Append(",");
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete)); 
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
            BtnForReview.Visible = false;
        }

        p_Var.dSet = null;
    }

    #endregion

  
    #region Function To bind the gridView with menu

    public void Bind_Grid(int list_value, int status, int langid, int positionid,int departmentid)
    {
        if (ddlStatus.SelectedValue == "0")
        {
            grdCMSMenu.Visible = false;
            BtnForReview.Visible = false;
        }
        else
        {

            grdCMSMenu.Visible = true;
            lnkObject.StatusId = status;
            lnkObject.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Menu);
            lnkObject.LangId = langid;
            lnkObject.PositionId = positionid;
            lnkObject.DepttId = departmentid;
            lnkObject.LinkParentId = list_value;
          

            p_Var.dSet = obj_linkBL.ASP_Links_DisplayWithPaging(lnkObject, out p_Var.k);
            // p_Var.dSet = obj_linkBL.ASP_Links_Display(lnkObject);

            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {


                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    grdCMSMenu.Columns[7].HeaderText = "Purge";
                }
                else
                {
                    grdCMSMenu.Columns[7].HeaderText = "Delete";
                }
                grdCMSMenu.DataSource = p_Var.dSet;

                //Codes for the sorting of records

                DataTable dt = p_Var.dSet.Tables[0];
                Cache["dt"] = dt;
                ViewState["Column_Name"] = "Brand";
                ViewState["Sort_Order"] = "ASC";

                //End

                grdCMSMenu.DataBind();

                

                p_Var.dSet = null;

                Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
                Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
                obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
                obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                p_Var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);
                if (p_Var.dSet.Tables[0].Rows.Count > 0)
                {

                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        grdCMSMenu.Columns[0].Visible = false;
                    }
                    else
                    {
                        grdCMSMenu.Columns[0].Visible = true;
                    }
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                    {
                        BtnForReview.Visible = true;
                    }
                    else
                    {
                        BtnForReview.Visible = false;
                    }

                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        btnForApprove.Visible = true;
                        btnDisApprove.Visible = true;
                    }
                    else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
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

                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        btnApprove.Visible = true;
                        btnDisApprove.Visible = true;
                        btnForApprove.Visible = false;
                    }
                    else
                    {

                        btnApprove.Visible = false;
                        //btnDisApprove.Visible = false;
                    }

                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][6]) == true)
                    {
                        grdCMSMenu.Columns[6].Visible = true;
                    }
                    else
                    {
                        grdCMSMenu.Columns[6].Visible = false;
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                        {
                            grdCMSMenu.Columns[6].Visible = false;  // This is for edit
                            grdCMSMenu.Columns[8].Visible = true;
                            
                        }
                        else
                        {
                            ////grdCMSMenu.Columns[6].Visible = true; commented on date 20 sep 2013 by ruchi
                            grdCMSMenu.Columns[8].Visible = false;
                        }

                        // modify on date 20 Sep 2013 by ruchi

                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                            {
                                grdCMSMenu.Columns[7].Visible = true;
                                grdCMSMenu.Columns[9].Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    grdCMSMenu.Columns[7].Visible = true;
                                    grdCMSMenu.Columns[9].Visible = false;
                                }
                                else
                                {
                                    if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdCMSMenu.Columns[7].Visible = true;
                                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                                        {
                                            grdCMSMenu.Columns[9].Visible = true;
                                        }
                                        else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                        {
                                            grdCMSMenu.Columns[9].Visible = true;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                            {
                                                grdCMSMenu.Columns[9].Visible = true;
                                            }
                                            else
                                            {
                                                grdCMSMenu.Columns[9].Visible = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        grdCMSMenu.Columns[7].Visible = false;
                                        grdCMSMenu.Columns[9].Visible = false;
                                    }
                                }
                            }
                        }
                         if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                            {
                                grdCMSMenu.Columns[7].Visible = true;
                                grdCMSMenu.Columns[9].Visible = false;
                            }
                            else
                            {
                                //grdCMSMenu.Columns[7].Visible = false;
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdCMSMenu.Columns[7].Visible = true;
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
                        grdCMSMenu.Columns[7].Visible = false;
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {
                        //grdCMSMenu.Columns[7].Visible = true;
                    }
                    else
                    {
                        grdCMSMenu.Columns[7].Visible = false;
                    }
                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {
              
                grdCMSMenu.Visible = false;
                BtnForReview.Visible = false;
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

    }

    #endregion

    #region Function to bind listbox with root

    private void bindMenu_ListBox(int langid, int positionid,int departmentid)
    {
        lstCMSMenu.Items.Clear();
        Menu_ManagementBL _menuBL = new Menu_ManagementBL();

        ListItem li = default(ListItem);

        lnkObject.LangId = langid;
        lnkObject.LinkParentId = 0;
        lnkObject.PositionId = positionid;
        lnkObject.DepttId = departmentid;
        //Convert.ToInt32(Request.QueryString["position"]);
        try
        {

            dSet = _menuBL.getMenuName_From_Web(lnkObject);

            if (dSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= dSet.Tables[0].Rows.Count - 1; i++)
                {
                    int link_id = Convert.ToInt32(dSet.Tables[0].Rows[i]["Link_Id"]);
                    //if (link_id != 10)
                    //{
                    li = new ListItem(HttpUtility.HtmlDecode( dSet.Tables[0].Rows[i]["NAME"].ToString()), dSet.Tables[0].Rows[i]["Link_Id"].ToString());
                    lstCMSMenu.Items.Add(li);
                    bindChildData(Convert.ToInt32(dSet.Tables[0].Rows[i]["Link_Level"]) + 1, Convert.ToInt32(dSet.Tables[0].Rows[i]["Link_Id"]), Convert.ToInt32(dSet.Tables[0].Rows[i]["Position_Id"]));
                    //}
                }
                if (langid == 1)
                {
                    lstCMSMenu.Items.Insert(0, new ListItem("<----- On Root ------>", "0"));
                }
                else
                {
                    lstCMSMenu.Items.Insert(0, new ListItem("<----- मुख पृष्ठ ------>", "0"));
                }
                lstCMSMenu.Items[0].Selected = true;
               // Session["mmenu"] = lstCMSMenu.SelectedValue;
            }
            else
            {
                if (langid == 1)
                {
                    lstCMSMenu.Items.Insert(0, new ListItem("<----- On Root ------>", "0"));
                }
                else
                {
                    lstCMSMenu.Items.Insert(0, new ListItem("<----- मुख पृष्ठ ------>", "0"));
                }
                lstCMSMenu.Items[0].Selected = true;
               

            }
        }
        catch
        {
            throw;
        }
        finally
        {
            _menuBL = null;
        }
    }

    #endregion

    #region Function to get child records

    public void bindChildData(int level, int Parent_ID, int Postion_ID)
    {
        ListItem lic = default(ListItem);
        Menu_ManagementBL _subMenuBL = new Menu_ManagementBL();
        DataSet dsubLinks = new DataSet();
        try
        {
            lnkObject.LinkParentId = Parent_ID;
            lnkObject.LinkLevel = level;
            lnkObject.PositionId = Postion_ID;

            dsubLinks = _subMenuBL.get_SublinksID_of_Parant_From_Web(lnkObject);
            if (dsubLinks.Tables[0].Rows.Count > 0)
            {
                string str = "• ";
                for (int j = 0; j < level - 1; j++)
                {
                    str = str + "• ";
                }
                for (int i = 0; i <= dsubLinks.Tables[0].Rows.Count - 1; i++)
                {

                    lic = new ListItem(str + HttpUtility.HtmlDecode( dsubLinks.Tables[0].Rows[i]["NAME"].ToString()), dsubLinks.Tables[0].Rows[i]["Link_Id"].ToString());
                    lstCMSMenu.Items.Add(lic);
                    lnkObject.LinkParentId = Parent_ID;
                    lnkObject.LinkLevel = level + 1;
                    lnkObject.PositionId = Postion_ID;
                    bindChildData(level + 1, Convert.ToInt32(dsubLinks.Tables[0].Rows[i]["Link_Id"]), Postion_ID);
                }
            }
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            obj_userOB.ModuleId = Convert.ToInt32(Convert.ToInt32(Request.QueryString["ModuleID"]));
            obj_userOB.DepttId = Convert.ToInt32(Session["Dept_ID"]);
            p_Var.dSet = obj_UserBL.ASP_Get_Deptt_Name(obj_userOB);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = p_Var.dSet;
                ddlDepartment.DataValueField = "Deptt_Id";
                ddlDepartment.DataTextField = "Deptt_Name";
                ddlDepartment.DataBind();
            }
            //ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion



    //End

    //Codes for sorting of the grid

    private void RebindData(string sColimnName, string sSortOrder)
    {
        DataTable dt = (DataTable)Cache["dt"];
        dt.DefaultView.Sort = sColimnName + " " + sSortOrder;
        grdCMSMenu.DataSource = dt;
        grdCMSMenu.DataBind();
        ViewState["Column_Name"] = sColimnName;
        ViewState["Sort_Order"] = sSortOrder;
    }

    protected void grdCMSMenu_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (e.SortExpression == ViewState["Column_Name"].ToString())
        {
            if (ViewState["Sort_Order"].ToString() == "ASC")
                RebindData(e.SortExpression, "DESC");
            else
                RebindData(e.SortExpression, "ASC");
        }
        else
        {
            RebindData(e.SortExpression, "ASC");
        }
    }


    #region gridView grdCMSMenu pageIndexChanging Event zone

    protected void grdCMSMenu_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCMSMenu.PageIndex = e.NewPageIndex;
        Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
    }

    #endregion


    //End

    #region Function to bind emailid of reviewers status in checkboxlist

    public void bindCheckBoxListWithEmailIDs()
    {

        try
        {
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Menu);
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
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Menu);
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
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Menu);
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
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Menu);
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
    protected void btnSendEmailsWithoutEmails_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
            {
                foreach (GridViewRow row in grdCMSMenu.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                        lnkObject.TempLinkId = p_Var.dataKeyID;
                        lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        lnkObject.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        lnkObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                        lnkObject.UserID = Convert.ToInt32(Session["User_Id"]);
                        lnkObject.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        lnkObject.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = obj_linkBL.ASP_ChangeStatus_LinkTmpPermission(lnkObject);
                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Content has been sent for review successfully.";
                    if (ddlMenuPosition.SelectedValue == "1")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=1";
                    }
                    else if (ddlMenuPosition.SelectedValue == "2")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=2";
                    }
                    else if (ddlMenuPosition.SelectedValue == "3")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=3";
                    }
                    else if (ddlMenuPosition.SelectedValue == "4")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=4";
                    }
                    else if (ddlMenuPosition.SelectedValue == "5")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=5";
                    }
                    else if (ddlMenuPosition.SelectedValue == "6")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=6";
                    }
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                   
                }

            }
            else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {
                foreach (GridViewRow row in grdCMSMenu.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                        lnkObject.TempLinkId = p_Var.dataKeyID;
                        lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        lnkObject.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        lnkObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                        lnkObject.UserID = Convert.ToInt32(Session["User_Id"]);
                        lnkObject.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        lnkObject.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = obj_linkBL.ASP_ChangeStatus_LinkTmpPermission(lnkObject);

                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Content has been sent for publish successfully.";
                    if (ddlMenuPosition.SelectedValue == "1")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=1";
                    }
                    else if (ddlMenuPosition.SelectedValue == "2")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=2";
                    }
                    else if (ddlMenuPosition.SelectedValue == "3")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=3";
                    }
                    else if (ddlMenuPosition.SelectedValue == "4")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=4";
                    }
                    else if (ddlMenuPosition.SelectedValue == "5")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=5";
                    }
                    else if (ddlMenuPosition.SelectedValue == "6")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=6";
                    }
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");


                }


            }

            else
            {
                 foreach (GridViewRow row in grdCMSMenu.Rows)
                 {
                     Menu_ManagementBL menuBL1 = new Menu_ManagementBL();
                     Menu_ManagementBL menuBL2 = new Menu_ManagementBL();
                     CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                     if (selCheck.Checked == true)
                     {
                         //lnkObject.IpAddress = miscellBL.IpAddress();
                         lnkObject.TempLinkId = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                         lnkObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                         lnkObject.UserID = Convert.ToInt32(Session["User_Id"]);
                         lnkObject.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                         lnkObject.IpAddress = miscellBL.IpAddress();
                         p_Var.Result = menuBL1.insert_Top_Menu_in_Web(lnkObject);
                     }
                 }

                 if (p_Var.Result > 0)
                 {
                     Session["msg"] = "Content has been published successfully.";
                     if (ddlMenuPosition.SelectedValue == "1")
                     {
                         Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=1";
                     }
                     else if (ddlMenuPosition.SelectedValue == "2")
                     {
                         Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=2";
                     }
                     else if (ddlMenuPosition.SelectedValue == "3")
                     {
                         Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=3";
                     }
                     else if (ddlMenuPosition.SelectedValue == "4")
                     {
                         Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=4";
                     }
                     else if (ddlMenuPosition.SelectedValue == "5")
                     {
                         Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=5";
                     }
                     else if (ddlMenuPosition.SelectedValue == "6")
                     {
                         Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=6";
                     }
                     Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                     //  Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue),Convert.ToInt32(ddlMenuPosition.SelectedValue));
                 }
            }
        }
        catch
        {
            throw;
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
                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                {
                    sbuilder.Remove(0, sbuilder.Length);

                    foreach (GridViewRow row in grdCMSMenu.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblMenuName = (Label)row.FindControl("lblMenuName");
                        ViewState["menu"] = lblMenuName.Text;
                        if ((selCheck.Checked == true))
                        {
                            lnkObject.TempLinkId = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                            string strMenu = menuBL.getParentChild(lnkObject);
                            
                            sbuilder.Append("<b>HERC - CMS Menu - " + strMenu + "<br/></b>");
                            p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);

                            lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            lnkObject.status = Convert.ToInt32(ddlStatus.SelectedValue);
                            lnkObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                            lnkObject.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                            lnkObject.UserID = Convert.ToInt32(Session["User_Id"]);
                            lnkObject.IpAddress = miscellBL.IpAddress();
                            p_Var.Result = obj_linkBL.ASP_ChangeStatus_LinkTmpPermission(lnkObject);
                        }
                    }
                    if (p_Var.Result > 0)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
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
                            if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                            {
                                p_Var.sbuilder.Append("Record pending for Review : " + sbuilder.ToString());
                                p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                                p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                                string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                                p_Var.sbuildertmp.Append(email);

                                miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record pending for Review", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                            }
                            else
                            {
                                p_Var.sbuilder.Append("Record pending for Review : " + sbuilder.ToString());
                                p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                                p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                                //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                                string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                                p_Var.sbuildertmp.Append(email);
                                miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "EO - Record pending for Review", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

                            }
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
                                        textmessage = "HERC - Record pending for review -";

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }
                            }
                        }


                        /* End */

                        Session["msg"] = "Content has been sent for review successfully.";
                        if (ddlMenuPosition.SelectedValue == "1")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=1";
                        }
                        else if (ddlMenuPosition.SelectedValue == "2")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=2";
                        }
                        else if (ddlMenuPosition.SelectedValue == "3")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=3";
                        }
                        else if (ddlMenuPosition.SelectedValue == "4")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=4";
                        }
                        else if (ddlMenuPosition.SelectedValue == "5")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=5";
                        }
                        else if (ddlMenuPosition.SelectedValue == "6")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=6";
                        }
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }

                }
                else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdCMSMenu.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblMenuName = (Label)row.FindControl("lblMenuName");
                        ViewState["menu"] = lblMenuName.Text;
                        if ((selCheck.Checked == true))
                        {
                            lnkObject.TempLinkId = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                            string strMenu = menuBL.getParentChild(lnkObject);
                            // sbuilder.Append(lblMenuName.Text + "; ");
                            sbuilder.Append("<b>HERC - CMS Menu - " + strMenu + "<br/></b>");
                            p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                            lnkObject.TempLinkId = p_Var.dataKeyID;
                            lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            lnkObject.status = Convert.ToInt32(ddlStatus.SelectedValue);
                            lnkObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                            lnkObject.IpAddress = Miscelleneous_DL.getclientIP();
                            lnkObject.UserID = Convert.ToInt32(Session["User_Id"]);
                            lnkObject.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                            p_Var.Result = obj_linkBL.ASP_ChangeStatus_LinkTmpPermission(lnkObject);

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
                            if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                            {
                                p_Var.sbuilder.Append("Record pending for Publish : " + sbuilder.ToString() + "<br/>");
                                p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                                p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                                //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                                string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                                p_Var.sbuildertmp.Append(email);
                                miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record pending for Publish", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                            }
                            else
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
                        }

                        ///* Code to send sms */
                        char[] splitter = { ';' };
                        //PetitionOB petObjectNew = new PetitionOB();
                        //DataSet dsSms = new DataSet();
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
                                        textmessage = "HERC - Record pending for publish -";

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }

                            }
                        }


                        /* End */

                        Session["msg"] = "Content has been sent for publish successfully.";
                        if (ddlMenuPosition.SelectedValue == "1")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=1";
                        }
                        else if (ddlMenuPosition.SelectedValue == "2")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=2";
                        }
                        else if (ddlMenuPosition.SelectedValue == "3")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=3";
                        }
                        else if (ddlMenuPosition.SelectedValue == "4")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=4";
                        }
                        else if (ddlMenuPosition.SelectedValue == "5")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=5";
                        }
                        else if (ddlMenuPosition.SelectedValue == "6")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=6";
                        }
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");


                    }


                }

                else //Here code is to approve records on date 12-05-2014
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdCMSMenu.Rows)
                    {
                        Menu_ManagementBL menuBL1 = new Menu_ManagementBL();
                        Menu_ManagementBL menuBL2 = new Menu_ManagementBL();
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblMenuName = (Label)row.FindControl("lblMenuName");
                        ViewState["menu"] = lblMenuName.Text;
                        if (selCheck.Checked == true)
                        {
                            lnkObject.TempLinkId = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                            string strMenu = menuBL.getParentChild(lnkObject);
                            // sbuilder.Append(lblMenuName.Text + "; ");
                            sbuilder.Append("<b>HERC - CMS Menu - " + strMenu + "<br/></b>");
                            lnkObject.TempLinkId = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                            lnkObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                            lnkObject.IpAddress = Miscelleneous_DL.getclientIP();
                            lnkObject.UserID = Convert.ToInt32(Session["User_Id"]);
                            lnkObject.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                            p_Var.Result = menuBL1.insert_Top_Menu_in_Web(lnkObject);
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
                                // p_Var.sbuildertmp.Append(li.Value + ";");

                                int statindex = li.Text.IndexOf("(") + 1;
                                p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                                sbuilderSms.Append(li.Value + ";");
                            }

                        }
                        if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                        {
                            if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                            {
                                p_Var.sbuilder.Append("Record Published : " + sbuilder.ToString());
                                p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                                p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                                //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                                string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                                p_Var.sbuildertmp.Append(email);
                                miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record Published", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                            }
                            else
                            {
                                p_Var.sbuilder.Append("Record published : " + sbuilder.ToString());
                                p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                                p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                                //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                                string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                                p_Var.sbuildertmp.Append(email);
                                miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "EO - Record Published", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

                            }
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




                        //code to get multiple public notice in list

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
                                        textmessage = "HERC - Record published -";

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }

                            }
                        }

                        /* End */


                        Session["msg"] = "Content has been published successfully.";
                        if (ddlMenuPosition.SelectedValue == "1")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=1";
                        }
                        else if (ddlMenuPosition.SelectedValue == "2")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=2";
                        }
                        else if (ddlMenuPosition.SelectedValue == "3")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=3";
                        }
                        else if (ddlMenuPosition.SelectedValue == "4")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=4";
                        }
                        else if (ddlMenuPosition.SelectedValue == "5")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=5";
                        }
                        else if (ddlMenuPosition.SelectedValue == "6")
                        {
                            Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=6";
                        }
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                    }
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
                foreach (GridViewRow row in grdCMSMenu.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblMenuName = (Label)row.FindControl("lblMenuName");
                    if ((selCheck.Checked == true))
                    {
                        p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                        lnkObject.TempLinkId = p_Var.dataKeyID;
                        lnkObject.TempLinkId = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                        string strMenu = menuBL.getParentChild(lnkObject);
                        // sbuilder.Append(lblMenuName.Text + "; ");
                        sbuilder.Append("<b>HERC - CMS Menu - " + strMenu + "<br/></b>");
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                        {
                            lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                        }
                        else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                        {
                            lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        }
                        else
                        {
                            lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        }
                        //lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                        lnkObject.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        lnkObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                        lnkObject.IpAddress = Miscelleneous_DL.getclientIP();
                        lnkObject.UserID = Convert.ToInt32(Session["User_Id"]);
                        p_Var.Result = obj_linkBL.ASP_ChangeStatus_LinkTmpPermission(lnkObject);

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
                        p_Var.sbuilder.Append("Record Disapproved : " + sbuilder.ToString());
                        p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                        p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                        //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                        string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        p_Var.sbuildertmp.Append(email);
                        miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "Record Disapproved", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

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
                                        message =strPublicNotice.ToString();
                                    }
                                    textmessage = "HERC - Record disapproved -";

                                    miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                }

                            }

                        }
                    }



                    /* End */
                    Session["msg"] = "Content has been disapproved successfully.";
                    if (ddlMenuPosition.SelectedValue == "1")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=1";
                    }
                    else if (ddlMenuPosition.SelectedValue == "2")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=2";
                    }
                    else if (ddlMenuPosition.SelectedValue == "3")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=3";
                    }
                    else if (ddlMenuPosition.SelectedValue == "4")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=4";
                    }
                    else if (ddlMenuPosition.SelectedValue == "5")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=5";
                    }
                    else if (ddlMenuPosition.SelectedValue == "6")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=6";
                    }
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                    //Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue));
                }
                else
                {
                    Session["msg"] = "Content has not been for disapproved successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]);
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
            foreach (GridViewRow row in grdCMSMenu.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                    p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                    lnkObject.TempLinkId = p_Var.dataKeyID;
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    //lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    lnkObject.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    lnkObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                    lnkObject.IpAddress = miscellBL.IpAddress();
                    p_Var.Result = obj_linkBL.ASP_ChangeStatus_LinkTmpPermission(lnkObject);

                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "Content has been disapproved successfully.";
                if (ddlMenuPosition.SelectedValue == "1")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=1";
                }
                else if (ddlMenuPosition.SelectedValue == "2")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=2";
                }
                else if (ddlMenuPosition.SelectedValue == "3")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=3";
                }
                else if (ddlMenuPosition.SelectedValue == "4")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=4";
                }
                else if (ddlMenuPosition.SelectedValue == "5")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=5";
                }
                else if (ddlMenuPosition.SelectedValue == "6")
                {
                    Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=6";
                }
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
}