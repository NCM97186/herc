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


public partial class Auth_AdminPanel_Module_Module_Display : System.Web.UI.Page
{
    //Area for all the variables declaration 

    #region variable declaration zone

    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    Project_Variables p_Var = new Project_Variables();
    LinkBL obj_linkBL = new LinkBL();
    LinkOB obj_inkOB = new LinkOB();
    UserBL obj_UserBL = new UserBL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    PaginationBL pagingBL = new PaginationBL();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petBL = new PetitionBL();
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion

    //End

    //Area for the page load event 

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["pdf"].ToString() + "/";
        p_Var.imageUrl = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Image"].ToString() + "/"; 
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
       
        if (!IsPostBack)
        {

            DataSet dsprv = new DataSet();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_RoleBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/AdminPanel/Login.aspx");
            }
           

            ViewState["sortOrder"] = "";
           
            BtnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            btnAddNew.Visible = false;
            BtnForReview.Visible = false;
            PLanguage.Visible = false;
            if (Session["MLng"] != null)
            {
                bindropDownlistLang();
                ddlLanguage.SelectedValue = Session["MLng"].ToString();

            }
            else
            {
                bindropDownlistLang();

            }
           
            if (Session["MDeptt"] != null)
            {
                Get_Deptt_Name();
                ddlDepartment.SelectedValue = Session["MDeptt"].ToString();
            }
            else
            {
                Get_Deptt_Name();
            }
            if (Session["MStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["MStatus"].ToString();
                Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
            }
            else
            {
                binddropDownlistStatus();
            }
           
            lblmsg.Visible = false;
            if (Request.QueryString["ModuleID"] != null)
            {
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Whats_New)
                {
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": View  What's New";
                    this.Page.Title = " What's New: HERC";

                    btnAddNew.Text = "Add What's New";
                    btnAddNew.ToolTip = "Add New What's New"; 
                }
                else if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Vacancy)
                {
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": View Vacancy";
                    this.Page.Title = "Vacancy: HERC";

                    btnAddNew.Text = "Add Vacancy";
                    btnAddNew.ToolTip = "Add New Vacancy";
                }
                else if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Annual_Report)
                {
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": View Annual Report";
                    this.Page.Title = "Annual Report: HERC";

                    btnAddNew.Text = "Add Annual Report";
                    btnAddNew.ToolTip = "Add New Annual Report";
                }
                else if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Public_Notice)
                {
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": View Public Notice";
                    this.Page.Title = "Public Notice: HERC";

                    btnAddNew.Text = "Add Public Notice";
                    btnAddNew.ToolTip = "Add New public Notice";
                }
                else if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Notification)
                {
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": View Notification";
                    this.Page.Title = "Notification: HERC";

                    btnAddNew.Text = "Add Notification";
                    btnAddNew.ToolTip = "Add New Notification";
                }
                else if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Banner)
                {
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": View Banner";
                    this.Page.Title = "Banner: HERC";

                    btnAddNew.Text = "Add Banner";
                    btnAddNew.ToolTip = "Add New Banner";
                }
                else if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Licenses)
                {
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": View Licenses";
                    this.Page.Title = "Licenses: HERC";

                    btnAddNew.Text = "Add Licenses";
                    btnAddNew.ToolTip = "Add New Licenses";
                }
                else if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Discussion_Paper)
                {
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": View Discussion Paper";
                    this.Page.Title = "Discussion Paper: HERC";

                    btnAddNew.Text = "Add Discussion Paper";
                    btnAddNew.ToolTip = "Add New Discussion Paper";
                }

                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Discussion_Paper)
                {
                    GvAdd_Details.Columns[6].HeaderText = "Last Date of Receiving Comments";
                }
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Vacancy)
                {
                    GvAdd_Details.Columns[6].HeaderText = "Last Date of Receiving applications";
                }
            }
        }
    }

    #endregion

    //End

    //Area for all the gridviews, repeaters events 

    #region Gridview  GvAdd_Details RowCommand Event

    protected void GvAdd_Details_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
        Label lblTitle = (Label)row.Cells[0].FindControl("lblTitle");

        if (e.CommandName == "View")
        {
            p_Var.stringTypeID = e.CommandArgument.ToString();
            p_Var.strPopupID = "<script language='javascript'>" +
                               "window.open('../viewdetails.aspx?Temp_Link_Id=" + p_Var.stringTypeID + "', 'mywindow', " +
                               "' menubar=no, resizable=no, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                               "</script>";
            this.Page.RegisterStartupScript("PopupScript", p_Var.strPopupID);
        }

        if (e.CommandName == "delete")
        {
            LinkOB obj_linkOB = new LinkOB();
            obj_linkOB.TempLinkId = Int32.Parse(e.CommandArgument.ToString());
           // GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            Label lblStatus = (Label)row.Cells[0].FindControl("lblStatus");

            obj_linkOB.StatusId = Convert.ToInt32(lblStatus.Text);
            obj_linkOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
            obj_linkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            p_Var.Result = obj_linkBL.Delete_ModulesRecords(obj_linkOB);
            if (p_Var.Result > 0)
            {
                if (ddlStatus.SelectedValue == "8")
                {
                    Session["msg"] = "Record has been deleted (purged) permanently.";
                }
                else
                {
                    Session["msg"] = "Record has been deleted successfully.";
                }

                obj_audit.ActionType = "D";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                obj_audit.IpAddress = miscellBL.IpAddress();
                obj_audit.Title = lblTitle.Text;
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);


                //Session["msg"] = "Record has been deleted successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
            else
            {
                Session["msg"] = "Record has not been deleted successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }

        if (e.CommandName == "Repealed")
        {
            LinkOB obj_LinkOB = new LinkOB();
            //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblStatus = (Label)row.Cells[0].FindControl("lblStatus");
            obj_LinkOB.TempLinkId = Convert.ToInt32(e.CommandArgument);
            ViewState["tempID"] = obj_LinkOB.TempLinkId;
            if (lblStatus.Text == "6")
            {
                obj_LinkOB.StatusId = 7;
                

            }
            else
            {
                obj_LinkOB.StatusId = 6;
            }
                int Result = obj_linkBL.ASP_Update_status(obj_LinkOB);

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
                    Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
               }         

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
                obj_audit.Title = lblTitle.Text;
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                Session["msg"] = "Record has been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
            else
            {
                Session["msg"] = "Record has not been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }
        else if (e.CommandName == "Audit")
        {
          //  GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            DataSet dSetAuditTrail = new DataSet();
            petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
            petObject.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            petObject.ModuleType = null;
            dSetAuditTrail = petBL.AuditTrailRecords(petObject);
            Label lblprono = row.FindControl("lblTitle") as Label;
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

    #region Gridview GvAdd_Details RowCreated Event

    protected void GvAdd_Details_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == DataControlRowState.Normal | e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
            CheckBox chkBxHeader = (CheckBox)this.GvAdd_Details.HeaderRow.FindControl("chkSelectAll");
            //Add client side function childclick on check boxes
            chkBxSelect.Attributes.Add("onclick", string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID));
        }
    }

    #endregion

    #region Gridview GvAdd_Details RowDataBound Event

    protected void GvAdd_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.Image img = e.Row.FindControl("imgedit") as System.Web.UI.WebControls.Image;
            System.Web.UI.WebControls.Image img1 = e.Row.FindControl("emgnotedit") as System.Web.UI.WebControls.Image;
            obj_inkOB.LinkTypeId = Convert.ToInt32(GvAdd_Details.DataKeys[e.Row.RowIndex].Value);
            Literal ltrl = (Literal)e.Row.FindControl("ltrlPublicHearingDate");
           
            p_Var.dSet = obj_linkBL.links_web_Get_Link_Id_ForEdit(obj_inkOB);

            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dSet.Tables[0].Rows.Count; i++)
                {
                    if (p_Var.dSet.Tables[0].Rows[i]["Link_Id"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(GvAdd_Details.DataKeys[e.Row.RowIndex].Value) == Convert.ToInt32(p_Var.dSet.Tables[0].Rows[i]["Link_Id"]))
                        {

                            img.Visible = false;
                            img1.ImageUrl = "~/Auth/AdminPanel/images/th_star.png";
                            img1.Height = 10;
                            img1.Width = 10;


                        }
                        else
                        {
                            img1.Visible = false;
                            img.ImageUrl = "~/Auth/AdminPanel/images/edit.gif";
                        }

                    }
                }
            }
            else
            {
                p_Var.dSet = null;
                img1.Visible = false;
            }

            //This is for Repealed Record
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkrpld");
                Label lblstatus = (Label)e.Row.FindControl("lblStatus");
                if (lblstatus.Text == "6")
                {
                    lnk.Text = "Repeal";
                    lnk.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure to repeal record ?')");
                }
                else if (lblstatus.Text == "7")
                {
                    lnk.Text = "UnRepeal";
                    lnk.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure to unrepeal record ?')");
                }



                //This is for delete/permanently delete 3 june 2013 

                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
                    ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to purge record no- " + DataBinder.Eval(e.Row.DataItem, "Name") + "')");

                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
                   "confirm('Are you sure you want to restore record no- " + DataBinder.Eval(e.Row.DataItem, "Name") + "')");

                }
                else
                {
                    ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
                    ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to delete record no- " + DataBinder.Eval(e.Row.DataItem, "Name") + "')");

                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
                  "confirm('Are you sure you want to restore record no- " + DataBinder.Eval(e.Row.DataItem, "Name") + "')");
 
                }

            //END
            //}
                if (ltrl.Text.Length > 12)
                {
                    ltrl.Visible = true;
                }
                else
                {
                    ltrl.Visible = false;
                }


                // 01-10 2013 by birendra to download file


                Literal orderFile = (Literal)e.Row.FindControl("ltrlFile");
                Literal imageFile = (Literal)e.Row.FindControl("ltrlImage");
                if (orderFile.Text != null && orderFile.Text != "")
                {
                    p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                    p_Var.sbuilder.Append("<a href='" + p_Var.url + orderFile.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                    orderFile.Text = p_Var.sbuilder.ToString();
                }
                if (imageFile.Text != null && imageFile.Text != "")
                {
                    p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                    p_Var.sbuilder.Append("<a href='" + p_Var.imageUrl + imageFile.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/imageDownload.png") + "' title='View Image' width='15' alt='View Image' height=\"15\" /> " + "</a>");
                    imageFile.Text = p_Var.sbuilder.ToString();
                }

            //End
        }
    }

    #endregion

    //End 
   
    //Area for all buttons, image buttons and link buttons click events

    #region buttn btnForApprove  click event zone

    protected void btnForApprove_Click(object sender, EventArgs e)  //permission review
    {

        pnlPopUpEmails.Visible = true;
        pnlGrid.Visible = false;
        bindCheckBoxListWithApproverEmailIDs();

       
    }

    #endregion

    #region Button for review Click Event Zone

    protected void BtnForReview_Click(object sender, EventArgs e) //creator send the detail for review  
    {

        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;

      

    }

    #endregion

    #region Button btnAddNew Click Event to add new news

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/Module/") +"Module_Add.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));

    }

    #endregion

    #region Button Approve Click event

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;
        //ChkApprove_Disapprove();
    }

    #endregion

    #region button bntDisapprove click event to disapprove

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
        //this.Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), pageIndex);
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue),"","");
    }

    #endregion

    //End

    //Area for all dropdownlists, listviews events 

    #region Dropdownlist ddlStatus selectedIndexChanged event

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlStatus.SelectedValue == "0")
        {
            GvAdd_Details.Visible = false;
            BtnForReview.Visible = false;
            btnApprove.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            lblmsg.Visible = false;
        }
        else
        {
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
            {
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification))
                {
                    GvAdd_Details.Columns[11].Visible = true;//7
                    
                }
                else
                {
                    GvAdd_Details.Columns[11].Visible = false;
                }
            }
            else
            {
                GvAdd_Details.Columns[11].Visible = false;

            }

          

            Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue),"","");
            Session["MStatus"] = ddlStatus.SelectedValue;
        }

        if (ddlLanguage.SelectedValue == "0"  || ddlStatus.SelectedValue == "0" || ddlDepartment.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
        
    }

    #endregion

    #region Dropdownlist ddlLanguage selectedIndexChanged event

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue != "0")
        {
            binddropDownlistStatus();
            GvAdd_Details.Visible = false;
            Session["MLng"] = ddlLanguage.SelectedValue;
        }

        if (ddlLanguage.SelectedValue == "0" || ddlStatus.SelectedValue == "0" || ddlDepartment.SelectedValue == "0")
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
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue),"","");
    }

    #endregion

    #region dropDownlist ddlDepartment selectedIndexChanged event

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
       // grdCMSMenu.Visible = false;
       
        //Session["mlang"] = ddlLanguage.SelectedValue;
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
        Session["MDeptt"] = ddlDepartment.SelectedValue;
       // bindMenu_ListBox(Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));

        if (ddlLanguage.SelectedValue == "0" || ddlStatus.SelectedValue == "0" || ddlDepartment.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    #endregion

    //End

    //Area for all user-defined functions


    #region Function To bind the grid

    public void Bind_Grid(int departmentid, string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;

        if (ddlStatus.SelectedValue == "0")
        {
            GvAdd_Details.Visible = false;
            BtnForReview.Visible = false;


        }
        else
        {
            
            GvAdd_Details.Visible = true;

            //obj_inkOB.PageIndex = pageIndex;
            obj_inkOB.DepttId = departmentid;
            //obj_inkOB.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            obj_inkOB.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            obj_inkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            obj_inkOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            p_Var.dSet = obj_linkBL.ASP_Links_DisplayWithPaging(obj_inkOB, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    GvAdd_Details.Columns[10].HeaderText = "Purge";
                }
                else
                {
                    GvAdd_Details.Columns[10].HeaderText = "Delete";
                }
                //Codes for the sorting of records

                DataView myDataView = new DataView();
                myDataView = p_Var.dSet.Tables[0].DefaultView;

                if (sortExp != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper))
                {
                    GvAdd_Details.Columns[6].Visible = true; //Last Date of Receiving Comments
                    GvAdd_Details.Columns[7].Visible = true; //Date of Public Hearing
                    GvAdd_Details.Columns[8].Visible = true; //Venue
                }
                else
                {
                    GvAdd_Details.Columns[6].Visible = false;
                    GvAdd_Details.Columns[7].Visible = false;
                    GvAdd_Details.Columns[8].Visible = false;
                }
                //End
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
                {
                    
                    GvAdd_Details.Columns[6].Visible = true; //Last Date of Receiving Comments
                }
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Discussion_Paper)
                {
                    GvAdd_Details.Columns[6].HeaderText = "Last Date of Receiving Comments";
                }
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Vacancy)
                {
                    GvAdd_Details.Columns[6].HeaderText = "Last Date of Receiving applications";
                }

                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report) || Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
                {

                    GvAdd_Details.Columns[13].Visible = true; //Last Date of Receiving Comments
                }
                else
                {
                    GvAdd_Details.Columns[13].Visible = false;
                }
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification))
                {
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == 8)
                    {
                        GvAdd_Details.Columns[11].Visible = false;//7
                    }
                    else
                    {
                        GvAdd_Details.Columns[11].Visible = true;//7
                    }

					 GvAdd_Details.Columns[14].Visible = true; // This is for inserted date
                }
                else
                {
                    GvAdd_Details.Columns[11].Visible = false;
					 GvAdd_Details.Columns[14].Visible = false; // This is for inserted date
                }

                GvAdd_Details.DataSource = myDataView;
                GvAdd_Details.DataBind();
                
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
                        GvAdd_Details.Columns[0].Visible = false;
                    }
                    else
                    {
                        GvAdd_Details.Columns[0].Visible = true;
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
                        //  btnDisApprove.Visible = false;
                    }

                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][6]) == true)
                    {
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                        {
                            GvAdd_Details.Columns[9].Visible = false;//This is for Edit
                            GvAdd_Details.Columns[12].Visible = true;//This is for restore
                        }
                        else
                        {
                            GvAdd_Details.Columns[9].Visible = true;
                            GvAdd_Details.Columns[12].Visible = false;
                        }
                        //GvAdd_Details.Columns[5].Visible = true;
                    }
                    else
                    {
                        GvAdd_Details.Columns[9].Visible = false;
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {
                        ////GvAdd_Details.Columns[10].Visible = true;



                        // modify on date 21 Sep 2013 by ruchi

                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                            {
                                GvAdd_Details.Columns[10].Visible = true;
                                GvAdd_Details.Columns[15].Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    GvAdd_Details.Columns[10].Visible = true;
                                    GvAdd_Details.Columns[15].Visible = false;
                                }
                                else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                {
                                    if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                                    {
                                        GvAdd_Details.Columns[15].Visible = true;
                                    }
                                    else
                                    {
                                        GvAdd_Details.Columns[15].Visible = false;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                                    {
                                        GvAdd_Details.Columns[10].Visible = true;
                                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                                        {
                                            GvAdd_Details.Columns[15].Visible = true;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                            {
                                                GvAdd_Details.Columns[15].Visible = true;
                                            }
                                            else
                                            {
                                                GvAdd_Details.Columns[15].Visible = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        GvAdd_Details.Columns[10].Visible = false;
                                        GvAdd_Details.Columns[15].Visible = false;
                                    }
                                }
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                            {
                                GvAdd_Details.Columns[10].Visible = true;
                                GvAdd_Details.Columns[15].Visible = false;
                            }

                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                GvAdd_Details.Columns[10].Visible = true;
                            }

                        }

                        //End   
                    }
                    else
                    {
                        GvAdd_Details.Columns[10].Visible = false;
                    }
                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {
                //rptPager.Visible = false;
                //lblPageSize.Visible = false;
                //ddlPageSize.Visible = false;
                GvAdd_Details.Visible = false;
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
        // Session["priv"] = p_Var.dSet;     //session hold the dsprv values  
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
                btnAddNew.Visible = true;


                //code written on date 23 sep 2013
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end
            }
            else
            {
                btnAddNew.Visible = false;
            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)).Append(",");

            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover)).Append(","); 
                // 18 feb
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

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            obj_userOB.DepttId = Convert.ToInt32(Session["Dept_ID"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
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

    protected void grdCMSMenu_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), e.SortExpression, sortOrder);
    }

    #region gridView grdCMSMenu pageIndexChanging Event zone

    protected void grdCMSMenu_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvAdd_Details.PageIndex = e.NewPageIndex;
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), ViewState["e"].ToString(), ViewState["o"].ToString());
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
    protected void GvAdd_Details_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }


    public void BindGridDetails()
    {
        if (ddlStatus.SelectedValue == "0")
        {
            grdModulePdf.Visible = false;
        }
        else
        {
            grdModulePdf.Visible = true;

            //obj_inkOB.PageIndex = pageIndex;
            obj_inkOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            //obj_inkOB.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            obj_inkOB.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            obj_inkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            obj_inkOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            p_Var.dSet = obj_linkBL.ASP_Links_DisplayWithPaging(obj_inkOB, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper))
                {
                    grdModulePdf.Columns[6].Visible = true; //Last Date of Receiving Comments
                    grdModulePdf.Columns[7].Visible = true; //Date of Public Hearing
                    grdModulePdf.Columns[8].Visible = true; //Venue
                }
                else
                {
                    grdModulePdf.Columns[6].Visible = false;
                    grdModulePdf.Columns[7].Visible = false;
                    grdModulePdf.Columns[8].Visible = false;
                }
                //End

                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report))
                {
                    grdModulePdf.Columns[9].Visible = true;
                    grdModulePdf.Columns[10].Visible = true;
                    grdModulePdf.Columns[11].Visible = true;
                }
                else
                {
                    grdModulePdf.Columns[9].Visible = false;
                    grdModulePdf.Columns[10].Visible = false;
                    grdModulePdf.Columns[11].Visible = false;
                }
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
                {

                    grdModulePdf.Columns[6].Visible = true; //Last Date of Receiving Comments
                }
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Discussion_Paper)
                {
                    grdModulePdf.Columns[6].HeaderText = "Last Date of Receiving Comments";
                }
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == (int)Module_ID_Enum.Project_Module_ID.Vacancy)
                {
                    grdModulePdf.Columns[6].HeaderText = "Last Date of Receiving applications";
                }
               
                grdModulePdf.DataSource = p_Var.dSet;
                grdModulePdf.DataBind();
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Modules_" + System.DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdModulePdf.AllowPaging = false;
        grdModulePdf.DataBind();
        grdModulePdf.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();
    }


    protected void btnSendEmails_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sbuilder = new StringBuilder();
            StringBuilder sbuilderSms = new StringBuilder();
            Label lblModulename = Master.FindControl("lblModulename") as Label;
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
            {
                foreach (GridViewRow row in GvAdd_Details.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblTitle = (Label)row.FindControl("lblTitle");
                    ViewState["mix"] = lblTitle.Text;
                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append(lblModulename.Text.Substring(7, lblModulename.Text.Length-7) + "- <b>" + lblTitle.Text + "<br/> </b>");
                        //sbuilder.Append(lblTitle.Text + "; ");
                        p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                        obj_inkOB.TempLinkId = p_Var.dataKeyID;
                        obj_inkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        obj_inkOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        obj_inkOB.UserID = Convert.ToInt32(Session["User_Id"]);
                        obj_inkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_inkOB.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = obj_linkBL.ASP_Temp_Links_Update_Status_Id(obj_inkOB);


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
                        p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
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
                                            textmessage = "HERC - Record pending for review -";

                                            miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                        }

                                    }
                              
                            }
                        }
                       
                    
                    /* End */
                    Session["msg"] = "Record has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {
                sbuilder.Remove(0, p_Var.sbuildertmp.Length);
               
                foreach (GridViewRow row in GvAdd_Details.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblTitle = (Label)row.FindControl("lblTitle");
                    ViewState["mix"] = lblTitle.Text;
                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append(lblModulename.Text.Substring(7, lblModulename.Text.Length - 7) + "- <b>" + lblTitle.Text + "<br/> </b>");
                        p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                        obj_inkOB.TempLinkId = p_Var.dataKeyID;
                        obj_inkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        obj_inkOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        obj_inkOB.UserID = Convert.ToInt32(Session["User_Id"]);
                        obj_inkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_inkOB.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = obj_linkBL.ASP_Temp_Links_Update_Status_Id(obj_inkOB);


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
                       // p_Var.sbuilder.Append("You have records for publish : " + sbuilder.ToString().Remove(sbuilder.Length - 1));
                        p_Var.sbuilder.Append("Record pending for Publish : " + sbuilder.ToString());
                        p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
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
                                            textmessage = "HERC - Record pending for publish -";

                                            miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                        }

                                    }
                                
                            }
                        }
                      
                    
                    /* End */

                    Session["msg"] = "Record has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else
            {
                sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                LinkOB obj_linkOB1 = new LinkOB();
                foreach (GridViewRow row in GvAdd_Details.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblTitle = (Label)row.FindControl("lblTitle");
                    ViewState["mix"] = lblTitle.Text;
                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append(lblModulename.Text.Substring(7, lblModulename.Text.Length - 7) + "- <b>" + lblTitle.Text + "<br/> </b>");
                        p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                        obj_linkOB1.TempLinkId = p_Var.dataKeyID;
                        obj_linkOB1.UserID = Convert.ToInt32(Session["User_Id"]);
                        obj_linkOB1.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_linkOB1.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = obj_linkBL.ASP_Insert_Web_Links(obj_linkOB1);


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
                        p_Var.sbuilder.Append("Record Published : " + sbuilder.ToString());
                        p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
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
                                            textmessage = "HERC - Record published -";

                                            miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                        }

                                    }
                                
                            }
                        }
                       
                    
                    /* End */

                    Session["msg"] = " Record has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
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
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
            {
                foreach (GridViewRow row in GvAdd_Details.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                        obj_inkOB.TempLinkId = p_Var.dataKeyID;
                        obj_inkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        obj_inkOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        obj_inkOB.UserID = Convert.ToInt32(Session["User_Id"]);
                        obj_inkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_inkOB.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = obj_linkBL.ASP_Temp_Links_Update_Status_Id(obj_inkOB);


                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Record has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                }
            }
            else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {
                foreach (GridViewRow row in GvAdd_Details.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                        obj_inkOB.TempLinkId = p_Var.dataKeyID;
                        obj_inkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        obj_inkOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        obj_inkOB.UserID = Convert.ToInt32(Session["User_Id"]);
                        obj_inkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_inkOB.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = obj_linkBL.ASP_Temp_Links_Update_Status_Id(obj_inkOB);


                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Record has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else
            {
                LinkOB obj_linkOB1 = new LinkOB();
                foreach (GridViewRow row in GvAdd_Details.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {

                        p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                        obj_linkOB1.TempLinkId = p_Var.dataKeyID;
                        obj_linkOB1.UserID = Convert.ToInt32(Session["User_Id"]);
                        obj_linkOB1.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_linkOB1.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = obj_linkBL.ASP_Insert_Web_Links(obj_linkOB1);


                    }
                }

                if (p_Var.Result > 0)
                {
                    Session["msg"] = " Record has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            StringBuilder sbuilder = new StringBuilder();
            StringBuilder sbuilderSms = new StringBuilder();
            Label lblModulename = Master.FindControl("lblModulename") as Label;
            foreach (GridViewRow row in GvAdd_Details.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                Label lblTitle = (Label)row.FindControl("lblTitle");
                ViewState["mix"] = lblTitle.Text;
                if ((selCheck.Checked == true))
                {
                    p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                    obj_inkOB.TempLinkId = p_Var.dataKeyID;
                    sbuilder.Append(lblModulename.Text.Substring(7, lblModulename.Text.Length - 7) + "- <b>" + lblTitle.Text + "<br/> </b>");
                    //sbuilder.Append(lblTitle.Text + "; ");
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        obj_inkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        obj_inkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        obj_inkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    //obj_inkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    obj_inkOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    p_Var.Result = obj_linkBL.ASP_Temp_Links_Update_Status_Id(obj_inkOB);
                   
                  
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
                    p_Var.sbuilder.Append("Record disapproved : " + sbuilder.ToString());
                    p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                    p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                    //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                    string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    p_Var.sbuildertmp.Append(email);
                    miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record Disapproved", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

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
                                        textmessage = "HERC - Record disapproved -";

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }
                            
                        }
                    }
                   
               
                /* End */
                Session["msg"] = "Record has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
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
            foreach (GridViewRow row in GvAdd_Details.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                    p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                    obj_inkOB.TempLinkId = p_Var.dataKeyID;
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        obj_inkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        obj_inkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        obj_inkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    //obj_inkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    obj_inkOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    p_Var.Result = obj_linkBL.ASP_Temp_Links_Update_Status_Id(obj_inkOB);
                    if (p_Var.Result > 0)
                    {
                        Session["msg"] = "Record has been disapproved successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                    }
                    else
                    {
                        Session["msg"] = "Record has not been disapproved successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
            }
            foreach (GridViewRow row in GvAdd_Details.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                selCheck.Checked = false;
            }
            CheckBox chkBxHeader = (CheckBox)this.GvAdd_Details.HeaderRow.FindControl("chkSelectAll");
            chkBxHeader.Checked = false;

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
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
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
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
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
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
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
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
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
