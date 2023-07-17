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

public partial class Auth_AdminPanel_RTI_RTI_Add : CrsfBase // System.Web.UI.Page
{
    //Area for all the data declaration zone

    PetitionOB rtiObject = new PetitionOB();
    RtiBL rtiBL = new RtiBL();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    UserOB obj_userOB = new UserOB();
    LinkBL obj_LinkBL = new LinkBL();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    //End

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": Add  RTI";
        this.Page.Title = " Add RTI: HERC";

        if (!IsPostBack)
        {
            displayMetaLang();
            DataSet dsprv = new DataSet();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_permissionBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }
            bindropDownlistLang();
            Get_Deptt_Name();
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
    }

    #endregion

    //Area for all the buttons, linkButtons, imageButtons click events

    #region button btnAddRTI click event to add RTI record

    protected void btnAddRTI_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (Page.IsValid)
                {
                    rtiObject.ActionType = Convert.ToInt32(Module_ID_Enum.Project_Action_Type.insert);
                    rtiObject.RefNo = HttpUtility.HtmlEncode(txtReferenceNo.Text);
                    //2 jan 2013
                    DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
                    rtiObject.year = HttpUtility.HtmlEncode(date.Year.ToString());

                    rtiObject.ApplicationDate = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
                    rtiObject.ApplicantName = HttpUtility.HtmlEncode(txtApplicantName.Text.Replace(Environment.NewLine, "<br />"));


                    rtiObject.ApplicantAddress = HttpUtility.HtmlEncode(txtApplicantAdd.Text.Replace(Environment.NewLine, "<br />"));
                    rtiObject.ApplicantMobileNo = HttpUtility.HtmlEncode(txtApplicantMobileNo.Text);
                    rtiObject.ApplicantPhoneNo = HttpUtility.HtmlEncode(txtApplicantPhoneNo.Text);
                    rtiObject.ApplicantFaxNo = HttpUtility.HtmlEncode(txtApplicantFaxNo.Text);
                    rtiObject.ApplicantEmail = HttpUtility.HtmlEncode(txtApplicantEmailID.Text);

                    rtiObject.MetaDescription = txtMetaDescription.Text;
                    rtiObject.MetaKeyWords = txtMetaKeyword.Text;
                    rtiObject.MetaTitle = txtMetaTitle.Text;
                    rtiObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;

                    rtiObject.subject = HttpUtility.HtmlEncode(txtRtiSubject.Text.Replace(Environment.NewLine, "<br />"));
                    rtiObject.RTIStatusId = Convert.ToInt32(Module_ID_Enum.rti_Status.inprocess);
                    rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    rtiObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
                    rtiObject.InsertedBy = Convert.ToInt32(Session["User_Id"]);
                    rtiObject.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
                    rtiObject.appeal = 0;
                    rtiObject.PlaceHolderFive = HttpUtility.HtmlEncode(txtremarks.Text); //Remarks in Rti
                    rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                    if (!String.IsNullOrEmpty(txtApplicantName.Text) && !String.IsNullOrEmpty(txtApplicantAdd.Text) && !String.IsNullOrEmpty(txtRtiSubject.Text))
                    {
                        p_Var.Result = rtiBL.insertUpdateTempRTI(rtiObject);
                        if (p_Var.Result > 0)
                        {


                            obj_audit.ActionType = "I";
                            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                            obj_audit.UserName = Session["UserName"].ToString();
                            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                            obj_audit.IpAddress = miscellBL.IpAddress();
                            obj_audit.Title = txtMetaTitle.Text;
                            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                            Session["msg"] = "RTI application has been added successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(txtApplicantName.Text))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter applicant name');", true);
                        }
                        else if (String.IsNullOrEmpty(txtApplicantAdd.Text))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter applicant address');", true);
                        }
                        else if (String.IsNullOrEmpty(txtRtiSubject.Text))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter subject');", true);
                        }
                    }
                }
            }
        }
		else{
			Session.Abandon();
		    Response.Redirect("~/Auth/AdminPanel/Login.aspx");
			}
    }

    #endregion

    #region button btnReset click event to reset textboxes

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtApplicantAdd.Text = "";
        txtApplicantEmailID.Text = "";
        txtApplicantFaxNo.Text = "";
        txtApplicantMobileNo.Text = "";
        txtApplicantName.Text = "";
        txtApplicantPhoneNo.Text = "";
        txtApplicationDate.Text = "";
        txtReferenceNo.Text = "";
        txtRtiSubject.Text = "";
        txtremarks.Text = "";
        //txtYear.Text = "";
        

    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
         Response.Redirect(ResolveUrl("~/auth/adminpanel/RTIModule/") + "DisplayRTI.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));
    }

    #endregion

    #region button btnRtiUpdate click event to update rti

    protected void btnRTIUpdate_Click(object sender, EventArgs e)
    {

    }

    #endregion

    #region button btnReset_edit click event to reset textboxes contents

    protected void btnReset_Edit_Click(object sender, EventArgs e)
    {

    }

    #endregion

    #region button btnBack_Edit click event to go back

    protected void btnBack_Edit_Click(object sender, EventArgs e)
    {

    }

    #endregion

    //End

    //Area for all the custome validators events

    #region customValidator cusReferenceNo serverValidate event to validate reference no. duplicacy

    protected void cusReferenceNo_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            p_Var.dSet = null;
            rtiObject.RefNo = HttpUtility.HtmlEncode(txtReferenceNo.Text);
            //rtiObject.year = txtYear.Text.Trim();
            //2 jan 2013
            DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
            rtiObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
            rtiObject.LangId =Convert.ToInt32(ddlLanguage.SelectedValue);
            rtiObject.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            p_Var.dSet = rtiBL.getReferenceNumber(rtiObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region customValidator cusReferenceNo_Edit serverValidate event to validate reference no. duplicacy in edit mode

    protected void cus_ReferenceNo_Edit_ServerValidate(object source, ServerValidateEventArgs args)
    {

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

      

    //End

    //Area for al the dropDownlist events 

    #region dropDownlist ddlRtiStatus_edit selectedIndexChanged event 

    protected void ddlRTIStatus_Edit_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    #endregion


    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            PDepartment.Visible = true;
            obj_userOB.DepttId = Convert.ToInt32(Session["Dept_ID"]);
            p_Var.dSet = obj_LinkBL.ASP_Get_Deptt_Name(obj_userOB);
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


    protected void CusomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        
        if (Convert.ToInt32(args.Value.ToString().Substring(6,4)) > DateTime.Today.Year)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }

    #region function to bind metalanguage

    public void displayMetaLang()
    {
        p_Var.dSet = miscellBL.getMetaLanguage();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlMetaLang.DataSource = p_Var.dSet;
            ddlMetaLang.DataTextField = "languagename";
            ddlMetaLang.DataValueField = "LanguageKey";
            ddlMetaLang.DataBind();
        }
    }

    #endregion
}
