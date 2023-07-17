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

public partial class Auth_AdminPanel_ProfileManagement_ProfileDisplay : CrsfBase //System.Web.UI.Page
{
    //Area for data declaration zone

    Project_Variables p_Var = new Project_Variables();
    UserOB obj_userOB = new UserOB();
    UserBL obj_UserBL = new UserBL();
    Miscelleneous_BL obj_miscelBL = new Miscelleneous_BL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    ProfileBL profileBL = new ProfileBL();
    ProfileOB profileOB = new ProfileOB();
    PaginationBL pagingBL = new PaginationBL();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petBL = new PetitionBL();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    //End

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
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
        //bindropDownlistLang();

        Session.Remove("MLng"); //  module sessions
        Session.Remove("Mdeptt");
        Session.Remove("MStatus");

        Session.Remove("RoleDeptt");//Role Sessions

        Session.Remove("UsrDeptt");//User Sessions
        Session.Remove("UsrStatus");

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
        dsprv = obj_permissionBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);

        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": View  Profiles";
        this.Page.Title = " Profile: HERC";

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
        if (Session["ProfileLng"] != null)
        {
            bindropDownlistLang();
            ddlLanguage.SelectedValue = Session["ProfileLng"].ToString();
        }
        else
        {
            bindropDownlistLang();
        }
        if (Session["ProfileNvg"] != null)
        {
            bindProfileNevigation();
            ddlNevigation.SelectedValue = Session["ProfileNvg"].ToString();
        }
        else
        {
            bindProfileNevigation();
        }
        if (Session["ProfileDeptt"] != null)
        {
            Get_Deptt_Name();
            ddlDepartment.SelectedValue = Session["ProfileDeptt"].ToString();
        }
        else
        {
            Get_Deptt_Name();
        }
        if (Session["profileStatus"] != null)
        {
            binddropDownlistStatus();
            ddlStatus.SelectedValue = Session["profileStatus"].ToString();
            Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
        }
        else
        {
            binddropDownlistStatus();
            if(Convert.ToInt32(ddlStatus.SelectedValue)==0)
            {
                P3.Visible = false;
            }
        }
       
    }

    }

    #endregion

    #region Function to bind language in dropDownlist according to permission

    public void bindropDownlistLang()
    {
        try
        {

            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            p_Var.dSet = obj_miscelBL.getLanguagePermission(obj_userOB);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true && Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
                {
                    obj_userOB.english = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.English);
                    obj_userOB.hindi = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(obj_userOB.english).Append(",");
                    p_Var.sbuilder.Append(obj_userOB.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true)
                {
                    obj_userOB.hindi = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(obj_userOB.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
                {
                    obj_userOB.english = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.English);

                    p_Var.sbuilder.Append(obj_userOB.english);
                }
                obj_userOB.LangId = p_Var.sbuilder.ToString().Trim();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                p_Var.dSet = null;
                p_Var.dSet = obj_miscelBL.getLanguage(obj_userOB);
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
            P3.Visible = true;
            //pddlStatus.Visible = true;
            UserOB usrObject = new UserOB();
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft)).Append(",");
               // btnAddNewPage.Visible = true;
                btnAddProfile.Visible = true;

                //code written on date 23sep 2013
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end
            }
            else
            {
                btnAddProfile.Visible = false;
            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)).Append(",");

            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover)).Append(",");
                // 27 feb
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
            //BtnForReview.Visible = false;
        }

        p_Var.dSet = null;
    }

    #endregion

    #region Function to bind dropDownlist with Profile Nevigation

    public void bindProfileNevigation()
    {
        ProfileBL profileBL = new ProfileBL();
        try
        {
            p_Var.dSet = profileBL.getNavigation();
            ddlNevigation.Items.Clear();
            ddlNevigation.DataSource = p_Var.dSet.Tables[0];
            ddlNevigation.DataTextField = "NavigationName";
            ddlNevigation.DataValueField = "Navigation_Id";
            ddlNevigation.DataBind();


        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function To bind the gridView with menu

    public void Bind_Grid(int departmentid, string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;

        grdCMSMenu.Visible = true;
        profileOB.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
        profileOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Profiles);
        profileOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
        profileOB.NevigationId = Convert.ToInt32(ddlNevigation.SelectedValue);
    
        profileOB.DepttId = departmentid;
        p_Var.dSet = profileBL.ASP_Profile_DisplayWithPaging(profileOB, out p_Var.k);


        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {

            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                grdCMSMenu.Columns[6].HeaderText = "Purge";
            }
            else
            {
                grdCMSMenu.Columns[6].HeaderText = "Delete";
            }
            //Codes for the sorting of records

            DataView myDataView = new DataView();
            myDataView = p_Var.dSet.Tables[0].DefaultView;

            if (sortExp != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            }

            //End

            grdCMSMenu.DataSource = myDataView;
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
                   // btnDisApprove.Visible = false;
                }

                if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][6]) == true)
                {
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                    {
                        grdCMSMenu.Columns[5].Visible = false;//This is for Edit
                        grdCMSMenu.Columns[7].Visible = true;//This is for restore
                        grdCMSMenu.Columns[8].Visible = false;
                    }
                    else
                    {
                        grdCMSMenu.Columns[5].Visible = true;
                        grdCMSMenu.Columns[7].Visible = false;
                    }
                    //grdCMSMenu.Columns[5].Visible = true;
                }
                else
                {
                    grdCMSMenu.Columns[5].Visible = false;
                }
                if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                {
                    ////grdCMSMenu.Columns[6].Visible = true;

                    // modify on date 21 Sep 2013 by ruchi

                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                    {
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                        {
                            grdCMSMenu.Columns[6].Visible = true;
                            grdCMSMenu.Columns[8].Visible = false;
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                            {
                                grdCMSMenu.Columns[6].Visible = true;
                                grdCMSMenu.Columns[8].Visible = false;
                            }
                            else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                            {
                                if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                                {
                                    grdCMSMenu.Columns[8].Visible = true;
                                }
                                else
                                {
                                    grdCMSMenu.Columns[8].Visible = false;
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                                {
                                    grdCMSMenu.Columns[6].Visible = true;
                                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                                    {
                                        grdCMSMenu.Columns[8].Visible = true;
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                        {
                                            grdCMSMenu.Columns[8].Visible = true;
                                        }
                                        else
                                        {
                                            grdCMSMenu.Columns[8].Visible = false;
                                        }
                                    }
                                }
                                else
                                {
                                    grdCMSMenu.Columns[8].Visible = false;
                                    grdCMSMenu.Columns[6].Visible = false;
                                }
                            }
                        }
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                    {
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                        {
                            grdCMSMenu.Columns[6].Visible = true;
                            grdCMSMenu.Columns[8].Visible = false;
                        }

                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                    {
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                        {
                            grdCMSMenu.Columns[6].Visible = true;
                        }

                    }

                    //End  
                }
                else
                {
                    grdCMSMenu.Columns[6].Visible = false;
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



        p_Var.Result = p_Var.k;
       
        Session["Status_Id"] = ddlStatus.SelectedValue.ToString();
        Session["Nevigation_ID"] = ddlNevigation.SelectedValue.ToString();
        Session["Lang_ID"] = ddlLanguage.SelectedValue.ToString();
        
    }

    #endregion

    //Area for all the buttons, linkButtons, imageButtons click events

    #region button btnAddProfile_Click click event to add new profiles

    protected void btnAddProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/ProfileManagement/") +"ProfileAdd.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));
    }

    #endregion

    //End

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue != "0")
        {
            //binddropDownlistStatus();
            grdCMSMenu.Visible = false;
            Session["ProfileLng"] = ddlLanguage.SelectedValue;
        }

        if (ddlLanguage.SelectedValue == "0" || ddlStatus.SelectedValue == "0" || ddlDepartment.SelectedValue == "0" || ddlNevigation.SelectedValue=="0")
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
            grdCMSMenu.Visible = false;
            BtnForReview.Visible = false;
            btnApprove.Visible = false;
            BtnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            binddropDownlistStatus();
            //ddlPageSize.Visible = false;
            //lblPageSize.Visible = false;
            //rptPager.Visible = false;
        }
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
        Session["profileStatus"] = ddlStatus.SelectedValue;

        if (ddlStatus.SelectedValue == "0" || ddlDepartment.SelectedValue == "0" || ddlNevigation.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    protected void grdCMSMenu_RowCommand(object sender, GridViewCommandEventArgs e)
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
                if (e.CommandName == "Delete")
                {

                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    Label lblStatus = (Label)row.Cells[0].FindControl("lblStatus");
                    profileOB.temp_profile_Id = Convert.ToInt32(e.CommandArgument);
                    profileOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                    profileOB.StatusId = Convert.ToInt32(lblStatus.Text);

                    p_Var.Result = profileBL.Delete_Pending_Approved_Record(profileOB);
                    if (p_Var.Result > 0)
                    {
                        obj_audit.ActionType = "D";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = obj_miscelBL.IpAddress();
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        Label lblname = row.FindControl("lblname") as Label;
                        obj_audit.Title = "Department: " + ddlDepartment.SelectedItem.ToString() + ", " + "Name: " + lblname.Text;
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "Profile has been deleted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                    else
                    {
                        Session["msg"] = "Profile has not been deleted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }

                }

                else if (e.CommandName == "Restore")
                {
                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    ProfileOB objnew = new ProfileOB();
                    objnew.profile_Id = Int32.Parse(e.CommandArgument.ToString());
                    p_Var.Result = profileBL.UpdateStatusProfile(objnew);
                    if (p_Var.Result > 0)
                    {

                        obj_audit.ActionType = "R";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        Label lblname = row.FindControl("lblname") as Label;
                        obj_audit.Title = "Department: "+ddlDepartment.SelectedItem.ToString() +", "+ "Name: "+ lblname.Text;
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "Profile has been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                    else
                    {
                        Session["msg"] = "Profile has not been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
                else if (e.CommandName == "Audit")
                {
                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    DataSet dSetAuditTrail = new DataSet();
                    petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
                    petObject.ModuleID = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Profiles);
                    petObject.ModuleType = null;
                    dSetAuditTrail = petBL.AuditTrailRecords(petObject);
                    Label lblprono = row.FindControl("lblName") as Label;
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

    protected void grdCMSMenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.Image img = e.Row.FindControl("imgedit") as System.Web.UI.WebControls.Image;
            System.Web.UI.WebControls.Image img1 = e.Row.FindControl("emgnotedit") as System.Web.UI.WebControls.Image;

            ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

            //This is for delete/permanently delete 3 june 2013 

            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {

                imgDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to purge the profile of- " + DataBinder.Eval(e.Row.DataItem, "Name") + "')");

            }
            else
            {

                imgDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete the profile of- " + DataBinder.Eval(e.Row.DataItem, "Name") + "')");

            }

            //END
            Literal imageFile = (Literal)e.Row.FindControl("ltrlImage");
            if (imageFile.Text != null && imageFile.Text != "")
            {
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                p_Var.sbuilder.Append("<a href='" + p_Var.imageUrl + imageFile.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/imageDownload.png") + "' title='View Image' width='15' alt='View Image' height=\"15\" /> " + "</a>");
                imageFile.Text = p_Var.sbuilder.ToString();
            }

            profileOB.profile_Id = Convert.ToInt32(grdCMSMenu.DataKeys[e.Row.RowIndex].Value);
            p_Var.dSet = profileBL.Profile__Get_Profile_Id_ForEdit(profileOB);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dSet.Tables[0].Rows.Count; i++)
                {
                    if (p_Var.dSet.Tables[0].Rows[i]["Profile_Id"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(grdCMSMenu.DataKeys[e.Row.RowIndex].Value) == Convert.ToInt32(p_Var.dSet.Tables[0].Rows[i]["Profile_Id"]))
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
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
                img1.Visible = false;
            }
            p_Var.dSet = null;
        }
    }

    protected void grdCMSMenu_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bind_Pending_Approve_Grid(1);
    }
    #endregion


    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
       // bind_Pending_Approve_Grid(pageIndex);
    }

    #endregion

    protected void ddlNevigation_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (Convert.ToInt32(ddlNevigation.SelectedIndex.ToString()) == 0)
        {

           P3.Visible = false;
           grdCMSMenu.Visible = false;
        }
       else
       {
           binddropDownlistStatus();
           grdCMSMenu.Visible = false;
           Session["ProfileNvg"] = ddlNevigation.SelectedValue;
       }

        if (ddlLanguage.SelectedValue == "0" || ddlStatus.SelectedValue == "0" || ddlDepartment.SelectedValue == "0" || ddlNevigation.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
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
        //ChkApprove_Disapprove();
    }
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

        //try
        //{
        //    foreach (GridViewRow row in grdCMSMenu.Rows)
        //    {
        //        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
        //        if ((selCheck.Checked == true))
        //        {
        //            p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
        //            profileOB.temp_profile_Id = p_Var.dataKeyID;

        //            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
        //            {
        //                profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
        //            }
        //            else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
        //            {
        //                profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
        //            }
        //            else
        //            {
        //                profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
        //            }
        //            //profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
        //            profileOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
        //            profileOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
        //            profileOB.IpAddress = obj_miscelBL.IpAddress();
        //            p_Var.Result = profileBL.ASP_ChangeStatus_ProfileTmpPermission(profileOB);

        //        }
        //    }
        //    if (p_Var.Result > 0)
        //    {
        //        Session["msg"] = "Profile has been disapproved successfully.";

        //        Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";

        //        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
        //    }
        //    else
        //    {
        //        Session["msg"] = "Profile has been disapproved successfully.";

        //        Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";

        //        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
        //    }
        //}
        //catch
        //{
        //    throw;
        //}
    }

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {

            p_Var.dSetChildData = profileBL.ASP_Get_Deptt_Name();
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {
                if (Session["Dept_ID"].ToString() == "1")
                {
                    p_Var.dSetChildData.Tables[0].Rows.RemoveAt(1);
                    ddlDepartment.DataSource = p_Var.dSetChildData;
                }
                else if (Session["Dept_ID"].ToString() == "2")
                {
                    p_Var.dSetChildData.Tables[0].Rows.RemoveAt(0);
                    ddlDepartment.DataSource = p_Var.dSetChildData;
                }
                else
                {
                    ddlDepartment.DataSource = p_Var.dSetChildData;
                }
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

    #region dropDownlist ddlDepartment selectedIndexChanged event

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
       
        //ddlPageSize.Visible = false;
        //lblPageSize.Visible = false;
        //rptPager.Visible = false;
       
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue),"","");
        Session["ProfileDeptt"] = ddlDepartment.SelectedValue;

        if (ddlLanguage.SelectedValue == "0" || ddlStatus.SelectedValue == "0" || ddlDepartment.SelectedValue == "0" || ddlNevigation.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    #endregion

    //Codes for sorting of the grid

    protected void grdCMSMenu_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), e.SortExpression, sortOrder);
    }

    #region gridView grdCMSMenu pageIndexChanging Event zone

    protected void grdCMSMenu_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCMSMenu.PageIndex = e.NewPageIndex;
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

    public void BindGridDetails()
    {
        grdProfilePdf.Visible = true;
        profileOB.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
        profileOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Profiles);
        profileOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
        profileOB.NevigationId = Convert.ToInt32(ddlNevigation.SelectedValue);

        profileOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
        p_Var.dSet = profileBL.ASP_Profile_DisplayWithPaging(profileOB, out p_Var.k);


        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            grdProfilePdf.DataSource = p_Var.dSet;
            grdProfilePdf.DataBind();
            p_Var.dSet = null;
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Profile_" + System.DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdProfilePdf.AllowPaging = false;
        grdProfilePdf.DataBind();
        grdProfilePdf.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();
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
                    foreach (GridViewRow row in grdCMSMenu.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblName = (Label)row.FindControl("lblName");
                        ViewState["profile"] = lblName.Text;
                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("Profile - <b>" + lblName.Text + "<br/> </b>");
                            p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                            profileOB.temp_profile_Id = p_Var.dataKeyID;
                            profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            profileOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                            profileOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                            profileOB.UserID = Convert.ToInt32(Session["User_Id"]);
                            profileOB.IpAddress = miscellBL.IpAddress();
                            p_Var.Result = profileBL.ASP_ChangeStatus_ProfileTmpPermission(profileOB);
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
                            if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                            {
                                p_Var.sbuilder.Append("Record pending for Review : " + sbuilder.ToString());
                                p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                                p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                                //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                                string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                                p_Var.sbuildertmp.Append(email);
                                obj_miscelBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record pending for Review", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
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
                                obj_miscelBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "E/O - Record pending for Review", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                           
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
                                                textmessage = "HERC - Record pending for Review -";

                                                miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                            }

                                        }
                                    
                                }
                            }
                            
                        /* End */
                        Session["msg"] = "Profile has been sent for review successfully.";

                        Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";

                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                }
                else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdCMSMenu.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblName = (Label)row.FindControl("lblName");
                        ViewState["profile"] = lblName.Text;
                        if ((selCheck.Checked == true))
                        {

                            sbuilder.Append("Profile - <b>" + lblName.Text + "<br/> </b>");
                            p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                            profileOB.temp_profile_Id = p_Var.dataKeyID;
                            profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            profileOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                            profileOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                            profileOB.UserID = Convert.ToInt32(Session["User_Id"]);
                            profileOB.IpAddress = miscellBL.IpAddress();
                            p_Var.Result = profileBL.ASP_ChangeStatus_ProfileTmpPermission(profileOB);

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
                            if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                            {
                                p_Var.sbuilder.Append("Record pending for Publish : " + sbuilder.ToString());
                                p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                                p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                                //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                                string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                                p_Var.sbuildertmp.Append(email);
                                obj_miscelBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record pending for Publish", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                            }
                            else
                            {
                                p_Var.sbuilder.Append("Record pending for Publish : " + sbuilder.ToString());
                                p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                                p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                                //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                                string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                                p_Var.sbuildertmp.Append(email);
                                obj_miscelBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "E/O - Record pending for Publish", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                            
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
                        Session["msg"] = "Profile has been sent for publish successfully.";

                        Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";

                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                    }
                }
                else //Here code is to approve records on date 12-05-2014
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);

                    foreach (GridViewRow row in grdCMSMenu.Rows)
                    {

                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblName = (Label)row.FindControl("lblName");
                        ViewState["profile"] = lblName.Text;
                       
                        if (selCheck.Checked == true)
                        {
                            sbuilder.Append("Profile - <b>" + lblName.Text + "<br/> </b>");
                            profileOB.temp_profile_Id = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                            profileOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                            profileOB.UserID = Convert.ToInt32(Session["User_Id"]);
                            profileOB.IpAddress = miscellBL.IpAddress();
                            p_Var.Result = profileBL.insert_Top_Profile_in_Web(profileOB);
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
                              //  p_Var.sbuildertmp.Append(li.Value + ";");
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
                                obj_miscelBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record Published", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                            }
                            else
                            {
                                p_Var.sbuilder.Append("Record Published : " + sbuilder.ToString());
                                p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                                p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                                //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                                string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                                p_Var.sbuildertmp.Append(email);
                                obj_miscelBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "E/O - Record Published", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                            
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

                        Session["msg"] = "Profile has been published successfully.";

                        Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";

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
                        profileOB.temp_profile_Id = p_Var.dataKeyID;
                        profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        profileOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        profileOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                        profileOB.UserID = Convert.ToInt32(Session["User_Id"]);
                        profileOB.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = profileBL.ASP_ChangeStatus_ProfileTmpPermission(profileOB);
                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Profile has been sent for review successfully.";

                    Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";

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
                        profileOB.temp_profile_Id = p_Var.dataKeyID;
                        profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        profileOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        profileOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                        profileOB.UserID = Convert.ToInt32(Session["User_Id"]);
                        profileOB.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = profileBL.ASP_ChangeStatus_ProfileTmpPermission(profileOB);

                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Profile has been sent for publish successfully.";

                    Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";

                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }

            }
            else
            {
                foreach (GridViewRow row in grdCMSMenu.Rows)
                {

                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if (selCheck.Checked == true)
                    {

                        profileOB.temp_profile_Id = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                        profileOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                        profileOB.UserID = Convert.ToInt32(Session["User_Id"]);
                        profileOB.IpAddress = miscellBL.IpAddress();
                        p_Var.Result = profileBL.insert_Top_Profile_in_Web(profileOB);
                    }
                }

                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Profile has been published successfully.";

                    Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";

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
                foreach (GridViewRow row in grdCMSMenu.Rows)
                {
                    Label lblName = (Label)row.FindControl("lblName");
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append("Profile - <b>" + lblName.Text + ";<br/> </b>");
                        p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                        profileOB.temp_profile_Id = p_Var.dataKeyID;

                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                        {
                            profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                        }
                        else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                        {
                            profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        }
                        else
                        {
                            profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        }
                        //profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                        profileOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        profileOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                        profileOB.IpAddress = obj_miscelBL.IpAddress();
                        p_Var.Result = profileBL.ASP_ChangeStatus_ProfileTmpPermission(profileOB);

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
                        if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                        {
                            p_Var.sbuilder.Append("Record Disapproved : " + sbuilder.ToString());
                            p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            obj_miscelBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record disapproved", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                        }
                        else
                        {
                            p_Var.sbuilder.Append("Record Disapproved : " + sbuilder.ToString());
                            p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            obj_miscelBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "E/O - Record disapproved", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                        
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
                    Session["msg"] = "Profile has been disapproved successfully.";

                    Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";

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
                    profileOB.temp_profile_Id = p_Var.dataKeyID;

                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    
                    profileOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    profileOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                    profileOB.IpAddress = obj_miscelBL.IpAddress();
                    p_Var.Result = profileBL.ASP_ChangeStatus_ProfileTmpPermission(profileOB);

                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "Profile has been disapproved successfully.";

                Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";

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
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Profiles);
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
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Profiles);
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
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Profiles);
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
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Profiles);
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
