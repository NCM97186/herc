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

public partial class Auth_AdminPanel_MixModules_Module_Display : System.Web.UI.Page
{
    #region Variable declaration zone

    LinkOB obj_LinkOB = new LinkOB();
    LinkBL obj_LinkBL = new LinkBL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    MixModuleBL mixModuleBL = new MixModuleBL();
    Project_Variables p_Var = new Project_Variables();
    HtmlSanitizer removerBL = new HtmlSanitizer();
    PaginationBL pagingBL = new PaginationBL();
    UserBL obj_UserBL = new UserBL();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petBL = new PetitionBL();

    #endregion

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
        p_Var.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/");


        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": View  Regulations/Codes/Standards/Policies/Guidelines";
        this.Page.Title = " Regulations/Codes/Standards/Policies/Guidelines: HERC";


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
            BtnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            btnAddNew.Visible = false;
            BtnForReview.Visible = false;
            PLanguage.Visible = false;
            if (Session["MixLng"] != null)
            {
                bindropDownlistLang();
                ddlLanguage.SelectedValue = Session["MixLng"].ToString();
                
            }
            else
            {
                bindropDownlistLang();

            }
            if (Session["MixDeptt"] != null)
            {
                Get_Deptt_Name();
                ddlDepartment.SelectedValue = Session["MixDeptt"].ToString();
            }
            else
            {
                Get_Deptt_Name();
            }
            if (Session["MixModule"] != null)
            {
                Get_Module_Name();
                ddlModules.SelectedValue = Session["MixModule"].ToString();
            }
            else
            {
                Get_Module_Name();
            }
            
            if (Session["MixStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["MixStatus"].ToString();
                Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
            }
            else
            {
                binddropDownlistStatus();
            }
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/MixModules/") +"Add_Modules.aspx?ModuleID=" + Convert.ToInt32(Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Modules)));
    }
    protected void ddlModules_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue != "0")
        {
            binddropDownlistStatus();
            GvAdd_Details.Visible = false;
            Session["MixModule"] = ddlModules.SelectedValue;

        }
    }
    protected void BtnForReview_Click(object sender, EventArgs e)
    {

        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;

    }
    protected void btnForApprove_Click(object sender, EventArgs e)
    {

        pnlPopUpEmails.Visible = true;
        pnlGrid.Visible = false;
        bindCheckBoxListWithApproverEmailIDs();

    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;
       
    }
    protected void btnDisApprove_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
        {
            pnlPopUpEmailsDis.Visible = true;
            pnlGrid.Visible = false;
            bindCheckBoxListWithEmailIDDiaapproves();
            
        }
        else
        {
            pnlPopUpEmailsDis.Visible = true;
            pnlGrid.Visible = false;
            bindCheckBoxListWithDataEntryEmailIDs();
           
        }
       
    }

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        //this.Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), pageIndex);
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
    }

    #endregion


    protected void GvAdd_Details_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDoc")
        {

            string file = e.CommandArgument.ToString();
            p_Var.Path =ResolveUrl(p_Var.Path)+ file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(p_Var.Path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(p_Var.Path));
                Response.Clear();
                Response.WriteFile(p_Var.Path);
                Response.End();
            }
        }
        if (e.CommandName == "delete")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            Label lblStatus = (Label)row.Cells[0].FindControl("lblStatus");
            obj_LinkOB.TempLinkId = Convert.ToInt32(e.CommandArgument);
            obj_LinkOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
            obj_LinkOB.StatusId = Convert.ToInt32(lblStatus.Text);
            obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);

            p_Var.Result = mixModuleBL.DeleteMixModuleRecords(obj_LinkOB);
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
                
                Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=20 ";
              
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
            else
            {
               
            }
           
        }
        if (e.CommandName == "Repealed")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblStatus = (Label)row.Cells[0].FindControl("lblStatus");
            obj_LinkOB.TempLinkId = Convert.ToInt32(e.CommandArgument);
            obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
            ViewState["tempID"] = obj_LinkOB.TempLinkId;
            if (lblStatus.Text== "6")
            {
                obj_LinkOB.StatusId = 7;
                this.ModalPopupExtender1.Show();

            }
            else 
            {
                obj_LinkOB.StatusId = 6;
                int Result = obj_LinkBL.ASP_Update_status(obj_LinkOB);

                if (Result > 0)
                {
                    
                
                    Session["msg"] = "Record has been unrepealed successfully.";
                
                    Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
           
        }
        else if (e.CommandName == "Restore")
        {

            LinkOB obj_linkOBNew = new LinkOB();
            obj_linkOBNew.TempLinkId = Int32.Parse(e.CommandArgument.ToString());


            p_Var.Result = mixModuleBL.updateWebStatusRestore(obj_linkOBNew);
            if (p_Var.Result > 0)
            {
                Session["msg"] = "Record has been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
            else
            {
                Session["msg"] = "Record has not been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }
        else if (e.CommandName == "Audit")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            DataSet dSetAuditTrail = new DataSet();
            petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
            petObject.ModuleID = Convert.ToInt32(ddlModules.SelectedValue);
            petObject.ModuleType = null;
            dSetAuditTrail = petBL.AuditTrailRecords(petObject);
            Label lblprono = row.FindControl("LnkTitle") as Label;
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
    protected void GvAdd_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            LinkButton lbl_ViewDoc = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
                HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
                string filename = DataBinder.Eval(e.Row.DataItem, "File_Name").ToString();


                if (filename == null || filename == "")
                {
                    lbl_ViewDoc.Visible = false;
                }
           
            //Code for show or hide edit image buttons

            System.Web.UI.WebControls.Image img = e.Row.FindControl("imgedit") as System.Web.UI.WebControls.Image;
            System.Web.UI.WebControls.Image img1 = e.Row.FindControl("emgnotedit") as System.Web.UI.WebControls.Image;
            ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
            obj_LinkOB.LinkTypeId = Convert.ToInt32(GvAdd_Details.DataKeys[e.Row.RowIndex].Value);
            p_Var.dSetChildData = obj_LinkBL.links_web_Get_Link_Id_ForEdit(obj_LinkOB);

            for (int i = 0; i < p_Var.dSetChildData.Tables[0].Rows.Count; i++)
            {
                if (p_Var.dSetChildData.Tables[0].Rows[i]["Link_Id"] != DBNull.Value)
                {

                    if (Convert.ToInt32(GvAdd_Details.DataKeys[e.Row.RowIndex].Value) == Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[i]["Link_Id"]))
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

            //End
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkrpld");
            Label lblAmbendmen = (Label)e.Row.FindControl("lblregulationAmbendment");
            Label lblregu = (Label)e.Row.FindControl("lblRegulation");
            Label lblstatus = (Label)e.Row.FindControl("lblStatus");
            Label LblModuleID = (Label)e.Row.FindControl("LblModuleID");
            //if (LblModuleID.Text.ToString() == "20" || LblModuleID.Text.ToString()=="22" || LblModuleID.Text.ToString()=="23")
            if (LblModuleID.Text.ToString() == "20")
            {
                GvAdd_Details.Columns[5].Visible = true;
               //GvAdd_Details.Columns[8].Visible = false;
            }
            else
            {
                if (LblModuleID.Text.ToString() == "22" || LblModuleID.Text.ToString() == "23")
                {
                    GvAdd_Details.Columns[5].Visible = true;
                   // GvAdd_Details.Columns[8].Visible = true;
                }
                else
                {
                    GvAdd_Details.Columns[5].Visible = true;
                   // GvAdd_Details.Columns[8].Visible = true;
                }
            }
            if (lblAmbendmen.Text != null && lblAmbendmen.Text != "")
            {
                lblAmbendmen.Visible = true;
                lblAmbendmen.Text += " (Amendment)";
                lblregu.Visible = false;
            }
            else 
            {
                lblregu.Visible = true;
                lblAmbendmen.Visible = false;
            }
            
            if (lblstatus.Text == "6")
            {
                lnk.Text = "Repeal";
                lnk.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure to repeal record?')");
            }
            else if(lblstatus.Text == "7")
            {
                lnk.Text = "UnRepeal";
                lnk.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure to unrepealed record?')");
            }
            //This is for to replace zero by blank
            Label lblRegulation = (Label)e.Row.FindControl("lblRegulation");
            if (lblRegulation.Text == "0")
            {
                lblRegulation.Text = "";
            }
            ImageButton lnkbtnresult = (ImageButton)e.Row.FindControl("BtnDelete");
            LinkButton lnkrpld = (LinkButton)e.Row.FindControl("lnkrpld");
            ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
            if (lblAmbendmen.Text != null && lblAmbendmen.Text != "")
            {
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + lblAmbendmen.Text + "')");
            }
            else
            {
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + lblRegulation.Text + "')");
            }
            //This is for repeal
            if (lblAmbendmen.Text != null && lblAmbendmen.Text != "")
            {
                lnkrpld.Attributes.Add("onclick", "javascript:return ConfirmationBoxRepeal('" + lblAmbendmen.Text + "')");
            }
            else
            {
                lnkrpld.Attributes.Add("onclick", "javascript:return ConfirmationBoxRepeal('" + lblRegulation.Text + "')");
            }
            //This is for Restore
            if (lblAmbendmen.Text != null && lblAmbendmen.Text != "")
            {
                BtnRestore.Attributes.Add("onclick", "javascript:return ConfirmationBoxRestore('" + lblAmbendmen.Text + "')");
            }
            else
            {
                BtnRestore.Attributes.Add("onclick", "javascript:return ConfirmationBoxRestore('" + lblRegulation.Text + "')");
            }
        }
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue != "0")
        {
            binddropDownlistStatus();
            GvAdd_Details.Visible = false;
            Session["MixLng"] = ddlLanguage.SelectedValue;
        }

        if (ddlLanguage.SelectedValue == "0" || ddlModules.SelectedValue == "0" || ddlStatus.SelectedValue == "0" || ddlDepartment.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        
        GvAdd_Details.Visible = false;
        GvAdd_Details.Visible = false;
        GvAdd_Details.Visible = false;
        GvAdd_Details.Visible = false;
        Session["Mixdeptt"] = ddlDepartment.SelectedValue;

        if (ddlLanguage.SelectedValue == "0" || ddlModules.SelectedValue == "0" || ddlStatus.SelectedValue == "0" || ddlDepartment.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }

    }
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
                GvAdd_Details.Columns[7].Visible = true;
             

            }
            else
            {
                GvAdd_Details.Columns[7].Visible = false;
            }
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                GvAdd_Details.Columns[10].Visible = true;
            }
            else
            {
                GvAdd_Details.Columns[10].Visible = false;
            }

            Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
            Session["MixStatus"] = ddlStatus.SelectedValue;
        }

        if (ddlLanguage.SelectedValue == "0" || ddlModules.SelectedValue == "0" || ddlStatus.SelectedValue == "0" || ddlDepartment.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
    }

    #endregion

    //Area for all the user defined functions

    #region Function to bind language in dropDownlist according to permission

    public void bindropDownlistLang()
    {
        try
        {
            Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
            Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Modules);
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
            throw;
        }
    }

    #endregion

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {

            p_Var.dSet = mixModuleBL.getDepartmentName();
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = p_Var.dSet;
                ddlDepartment.DataValueField = "Deptt_Id";
                ddlDepartment.DataTextField = "Deptt_Name";
                ddlDepartment.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function bind module name in dropDownlist

    public void Get_Module_Name()
    {
        try
        {
            // obj_userOB.DepttId = Convert.ToInt32(Session["Dept_ID"]);
            p_Var.dSet = mixModuleBL.getModuleName();
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlModules.DataSource = p_Var.dSet;
                ddlModules.DataValueField = "Module_Id";
                ddlModules.DataTextField = "Module_name";
                ddlModules.DataBind();
            }
            //ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind status in dropDownlist according to permission

    public void binddropDownlistStatus()
    {
        Miscelleneous_BL miscDdlStatus = new Miscelleneous_BL();
        Miscelleneous_BL miscdlStatus = new Miscelleneous_BL();

        obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
        obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Modules);
        p_Var.dSet = miscDdlStatus.getLanguagePermission(obj_userOB);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            UserOB usrObject = new UserOB();
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft)).Append(",");
                btnAddNew.Visible = true;

                //code written on date 23sep 2013
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

            
            obj_LinkOB.DepttId = departmentid;
          //  obj_LinkOB.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            obj_LinkOB.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
            obj_LinkOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            p_Var.dSet = mixModuleBL.ASP_Links_DisplayWithPaging(obj_LinkOB, out p_Var.k);

            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations))
                {
                    //GvAdd_Details.HeaderRow.Cells[7].Text = "Regulation No";
                    GvAdd_Details.Columns[5].HeaderText = "Regulation No";
                }
                else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Standard))
                {
                    GvAdd_Details.Columns[5].HeaderText = "Standard No";
                }
                else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Codes))
                {
                    GvAdd_Details.Columns[5].HeaderText = "Code No";
                }
                else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Guidelines))
                {
                    GvAdd_Details.Columns[5].HeaderText = "Guideline No";
                }
                else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Policies))
                {
                    GvAdd_Details.Columns[5].HeaderText = "Policies No";
                }

                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    GvAdd_Details.Columns[10].Visible = true;
                    //GvAdd_Details.Columns[6].Visible = false;
                    //GvAdd_Details.Columns[8].Visible = false;
                }
                else
                {
                    //GvAdd_Details.Columns[6].Visible = true;
                    //GvAdd_Details.Columns[8].Visible = true;
                    GvAdd_Details.Columns[10].Visible = false;
                }
                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    GvAdd_Details.Columns[7].HeaderText = "Purge";
                }
                else
                {
                    GvAdd_Details.Columns[7].HeaderText = "Delete";
                }
             
                //Codes for the sorting of records

                DataView myDataView = new DataView();
                myDataView = p_Var.dSet.Tables[0].DefaultView;

                if (sortExp != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                //End
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
                        GvAdd_Details.Columns[8].Visible = true;
                    }
                    else
                    {
                        GvAdd_Details.Columns[0].Visible = true;
                        GvAdd_Details.Columns[8].Visible = false;
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

                            GvAdd_Details.Columns[6].Visible = false; // Edit
                            GvAdd_Details.Columns[8].Visible = false; //Restore
                            GvAdd_Details.Columns[12].Visible = false;
                        }
                        else
                        {
                            GvAdd_Details.Columns[6].Visible = true;
                           // GvAdd_Details.Columns[8].Visible = true;
                           
                        }
                        //GvAdd_Details.Columns[6].Visible = true;
                    }
                    else
                    {
                        GvAdd_Details.Columns[6].Visible = false;
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {
                        

                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                            {
                                GvAdd_Details.Columns[7].Visible = true;
                                GvAdd_Details.Columns[12].Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    GvAdd_Details.Columns[7].Visible = true;
                                    GvAdd_Details.Columns[12].Visible = false;
                                }
                                else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                {
                                    if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                                    {
                                        GvAdd_Details.Columns[12].Visible = true;
                                        GvAdd_Details.Columns[7].Visible = true;
                                    }
                                    else
                                    {
                                        GvAdd_Details.Columns[12].Visible = false;
                                       // GvAdd_Details.Columns[7].Visible = true;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                                    {
                                        GvAdd_Details.Columns[7].Visible = true;
                                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                                        {
                                            GvAdd_Details.Columns[12].Visible = true;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                            {
                                                GvAdd_Details.Columns[12].Visible = true;
                                                GvAdd_Details.Columns[7].Visible = true;
                                            }
                                            else
                                            {
                                                GvAdd_Details.Columns[12].Visible = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        GvAdd_Details.Columns[7].Visible = false;
                                        GvAdd_Details.Columns[12].Visible = false;
                                    }
                                }
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                            {
                                GvAdd_Details.Columns[7].Visible = true;
                                GvAdd_Details.Columns[12].Visible = false;
                            }


                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                GvAdd_Details.Columns[7].Visible = true;
                            }

                        }

                        //End  
                        
                    }
                    else
                    {
                        GvAdd_Details.Columns[7].Visible = false;
                    }

                   
                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {
                
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
        Session["priv"] = p_Var.dSet;     //session hold the dsprv values  
        if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
        {
            GvAdd_Details.Columns[8].Visible = true;
        }
        else
        {
            GvAdd_Details.Columns[8].Visible = false;
        }
    }

    #endregion

    //End


    #region button btnReply click event to reply query

    protected void btnReply_Click(object sender, EventArgs e) 
    {
        obj_LinkOB.TempLinkId = Convert.ToInt32(ViewState["tempID"]);
        obj_LinkOB.Remarks = txtRemarks.Text;
        obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
        obj_LinkOB.StatusId = 7;

        int Result = obj_LinkBL.ASP_Update_status(obj_LinkOB);
        
        if (Result > 0)
        {
            Session["msg"] = "Record has been repealed successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        }
    }

    #endregion

    protected void GvAdd_Details_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), e.SortExpression, sortOrder);
    }

    #region gridView GvAdd_Details pageIndexChanging Event zone

    protected void GvAdd_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
            grdMixPdf.Visible = false;
            BtnForReview.Visible = false;


        }
        else
        {

            grdMixPdf.Visible = true;


            obj_LinkOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_LinkOB.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
            obj_LinkOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            p_Var.dSet = mixModuleBL.ASP_Links_DisplayWithPaging(obj_LinkOB, out p_Var.k);

            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations))
                {
                    grdMixPdf.Columns[2].HeaderText = "Regulation No";
                }
                else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Standard))
                {
                    grdMixPdf.Columns[2].HeaderText = "Standard No";
                }
                else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Codes))
                {
                    grdMixPdf.Columns[2].HeaderText = "Code No";
                }
                else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Guidelines))
                {
                    grdMixPdf.Columns[2].HeaderText = "Guideline No";
                }
                else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Policies))
                {
                    grdMixPdf.Columns[2].HeaderText = "Policies No";
                }
                grdMixPdf.DataSource = p_Var.dSet;
                grdMixPdf.DataBind();
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Regulations_" + System.DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdMixPdf.AllowPaging = false;
        grdMixPdf.DataBind();
        grdMixPdf.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();

    }

    protected void grdMixPdf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            LinkButton lbl_ViewDoc = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            string filename = DataBinder.Eval(e.Row.DataItem, "File_Name").ToString();


            obj_LinkOB.LinkTypeId = Convert.ToInt32(grdMixPdf.DataKeys[e.Row.RowIndex].Value);
            p_Var.dSetChildData = obj_LinkBL.links_web_Get_Link_Id_ForEdit(obj_LinkOB);

           
            //End
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkrpld");
            Label lblAmbendmen = (Label)e.Row.FindControl("lblregulationAmbendment");
            Label lblregu = (Label)e.Row.FindControl("lblRegulation");
            Label lblstatus = (Label)e.Row.FindControl("lblStatus");
            Label LblModuleID = (Label)e.Row.FindControl("LblModuleID");
            //if (LblModuleID.Text.ToString() == "20" || LblModuleID.Text.ToString()=="22" || LblModuleID.Text.ToString()=="23")
            if (LblModuleID.Text.ToString() == "20")
            {
                grdMixPdf.Columns[2].Visible = true;
                // GvAdd_Details.Columns[8].Visible = false;
            }
            else
            {
                if (LblModuleID.Text.ToString() == "22" || LblModuleID.Text.ToString() == "23")
                {
                    grdMixPdf.Columns[2].Visible = true;
                    // GvAdd_Details.Columns[8].Visible = true;
                }
                else
                {
                    grdMixPdf.Columns[2].Visible = true;
                    // GvAdd_Details.Columns[8].Visible = true;
                }
            }
            if (lblAmbendmen.Text != null && lblAmbendmen.Text != "")
            {
                lblAmbendmen.Visible = true;
                lblAmbendmen.Text += " (Amendment)";
                lblregu.Visible = false;
            }
            else
            {
                lblregu.Visible = true;
                lblAmbendmen.Visible = false;
            }

           
            Label lblRegulation = (Label)e.Row.FindControl("lblRegulation");
            if (lblRegulation.Text == "0")
            {
                lblRegulation.Text = "";
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
                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in GvAdd_Details.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label LnkTitle = (Label)row.FindControl("LnkTitle");
                        ViewState["regulation"] = LnkTitle.Text;
                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append(ddlModules.SelectedItem.ToString() +" - <b>" + LnkTitle.Text + "<br/></b>");
                            //sbuilder.Append(LnkTitle.Text + "; ");
                            p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                            obj_LinkOB.TempLinkId = p_Var.dataKeyID;
                            obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            obj_LinkOB.UserID = Convert.ToInt32(Session["User_Id"]);
                            obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
                            obj_LinkOB.IpAddress = miscellBL.IpAddress();
                            obj_LinkOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                            p_Var.Result = mixModuleBL.ASP_Temp_Links_Update_Status_Id(obj_LinkOB);

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

                       
                            string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "").Replace("<p>","").Replace("</p>","");
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
                        Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }

                }
                else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in GvAdd_Details.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label LnkTitle = (Label)row.FindControl("LnkTitle");
                        ViewState["regulation"] = LnkTitle.Text;
                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append(ddlModules.SelectedItem.ToString() + " - <b>" + LnkTitle.Text + "<br/></b>");
                            //sbuilder.Append("Regulations/Codes/Standards/Policies/Guidelines - <b>" + LnkTitle.Text + "<br/></b>");
                            //sbuilder.Append(LnkTitle.Text + "; ");
                            p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                            obj_LinkOB.TempLinkId = p_Var.dataKeyID;
                            obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            obj_LinkOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                            obj_LinkOB.UserID = Convert.ToInt32(Session["User_Id"]);
                            obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
                            obj_LinkOB.IpAddress = miscellBL.IpAddress();
                            p_Var.Result = mixModuleBL.ASP_Temp_Links_Update_Status_Id(obj_LinkOB);

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

                        string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "").Replace("<p>", "").Replace("</p>", "");
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
                        Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
                else //Here code is to approve records on date 12-05-2014
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    LinkOB obj_linkOB1 = new LinkOB();
                    foreach (GridViewRow row in GvAdd_Details.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label LnkTitle = (Label)row.FindControl("LnkTitle");
                        ViewState["regulation"] = LnkTitle.Text;

                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append(ddlModules.SelectedItem.ToString() + " - <b>" + LnkTitle.Text + "<br/></b>");
                            //sbuilder.Append("Regulations/Codes/Standards/Policies/Guidelines - <b>" + LnkTitle.Text + "<br/></b>");
                            p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                            obj_linkOB1.TempLinkId = p_Var.dataKeyID;
                            obj_linkOB1.UserID = Convert.ToInt32(Session["User_Id"]);
                            obj_linkOB1.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
                            obj_linkOB1.IpAddress = miscellBL.IpAddress();
                            p_Var.Result = mixModuleBL.InsertRegulationsInWeb(obj_linkOB1);


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

                        string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "").Replace("<p>", "").Replace("</p>", "");
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
                        Session["msg"] = "Record has been published successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
            {
                foreach (GridViewRow row in GvAdd_Details.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                        obj_LinkOB.TempLinkId = p_Var.dataKeyID;
                        obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        obj_LinkOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        obj_LinkOB.UserID = Convert.ToInt32(Session["User_Id"]);
                        obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
                        obj_LinkOB.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = mixModuleBL.ASP_Temp_Links_Update_Status_Id(obj_LinkOB);

                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Record has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
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
                        obj_LinkOB.TempLinkId = p_Var.dataKeyID;
                        obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        obj_LinkOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        obj_LinkOB.UserID = Convert.ToInt32(Session["User_Id"]);
                        obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
                        obj_LinkOB.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = mixModuleBL.ASP_Temp_Links_Update_Status_Id(obj_LinkOB);

                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Record has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
                        obj_linkOB1.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
                        obj_linkOB1.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = mixModuleBL.InsertRegulationsInWeb(obj_linkOB1);


                    }
                }

                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Record has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            foreach (GridViewRow row in GvAdd_Details.Rows)
            {
                Label LnkTitle = (Label)row.FindControl("LnkTitle");
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                    sbuilder.Append(ddlModules.SelectedItem.ToString() + " - <b>" + LnkTitle.Text + "<br/></b>");
                    //sbuilder.Append("Regulations/Codes/Standards/Policies/Guidelines - <b>" + LnkTitle.Text + "<br/></b>");
                    p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                    obj_LinkOB.TempLinkId = p_Var.dataKeyID;

                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    //obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    obj_LinkOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    p_Var.Result = mixModuleBL.ASP_Temp_Links_Update_Status_Id(obj_LinkOB);

                }
            }
            if (p_Var.Result > 0)
            {
                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                foreach (System.Web.UI.WebControls.ListItem li in chkSendEmailsDis.Items)
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
                    p_Var.sbuilder.Append("Record Disapproved : " + sbuilder.ToString());
                    p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                    p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                    //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                    string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    p_Var.sbuildertmp.Append(email);
                    miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record Disapproved", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

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


                string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "").Replace("<p>", "").Replace("</p>", "");
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
                Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            foreach (GridViewRow row in GvAdd_Details.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                    p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                    obj_LinkOB.TempLinkId = p_Var.dataKeyID;

                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    //obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    obj_LinkOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    p_Var.Result = mixModuleBL.ASP_Temp_Links_Update_Status_Id(obj_LinkOB);

                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "Record has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations);
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
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations);
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
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations);
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
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations);
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
