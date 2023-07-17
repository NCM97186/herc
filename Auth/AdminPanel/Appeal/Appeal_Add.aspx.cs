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

public partial class Auth_AdminPanel_Appeal_Appeal_Add : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration zone

    PetitionOB appealObject = new PetitionOB();
    AppealBL appealBL = new AppealBL();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    UserOB obj_userOB = new UserOB();
    LinkBL obj_LinkBL = new LinkBL();
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    //End

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": Add EO Appeal";
        this.Page.Title = " Add EO Appeal: HERC";

        if (!IsPostBack)
        {
            displayMetaLang();
            DataSet dsprv = new DataSet();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_RoleBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }


            bindropDownlistLang();
            Get_Deptt_Name();
        }
    }

    #endregion

    //Area for all the buttons, linkButtons click events

    #region button btnAddAppeal click event to add new appeal

    protected void btnAddAppeal_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (Page.IsValid)
                {
                    appealObject.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);

                    appealObject.AppealNo = HttpUtility.HtmlEncode(txtAppealNo.Text);
                    DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
                    appealObject.year = date.Year.ToString();
                    appealObject.AppealDate = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
                    appealObject.ApplicantName = HttpUtility.HtmlEncode(txtApplicantName.Text.Replace(Environment.NewLine, "<br />"));
                    //petitionObject.PetitionerContactNo =Convert.ToDouble(HttpUtility.HtmlEncode(txtPetitionerContactNumber.Text));

                    appealObject.ApplicantAddress = HttpUtility.HtmlEncode(txtApplicantAdd.Text.Replace(Environment.NewLine, "<br />"));
                    appealObject.ApplicantMobileNo = HttpUtility.HtmlEncode(txtApplicantMobileNo.Text);
                    appealObject.ApplicantPhoneNo = HttpUtility.HtmlEncode(txtApplicantPhoneNo.Text);
                    appealObject.ApplicantFaxNo = HttpUtility.HtmlEncode(txtApplicantFaxNo.Text);
                    appealObject.ApplicantEmail = HttpUtility.HtmlEncode(txtApplicantEmailID.Text);

                    appealObject.RespondentName = HttpUtility.HtmlEncode(txtRespondentName.Text.Replace(Environment.NewLine, "<br />"));
                    appealObject.RespondentMobileNo = HttpUtility.HtmlEncode(txtRespondentMobileNo.Text);
                    appealObject.RespondentPhone_No = HttpUtility.HtmlEncode(txtRespondentPhoneNo.Text);
                    appealObject.RespondentFaxNo = HttpUtility.HtmlEncode(txtRespondentFaxNo.Text);
                    appealObject.Respondentmail = HttpUtility.HtmlEncode(txtRespondentEmailID.Text);
                    appealObject.RespondentAddress = HttpUtility.HtmlEncode(txtRespondentAddress.Text.Replace(Environment.NewLine, "<br />"));

                    appealObject.subject = HttpUtility.HtmlEncode(txtAppealSubject.Text.Replace(Environment.NewLine, "<br />"));
                    appealObject.AppealStatusId = Convert.ToInt16(Module_ID_Enum.Appeal_Status.inprocess);
                    appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    appealObject.LangId = Convert.ToInt16(ddlLanguage.SelectedValue);
                    appealObject.InsertedBy = Convert.ToInt16(Session["User_Id"]);
                    appealObject.userID = Convert.ToInt16(Session["User_Id"]);
                    appealObject.IpAddress = Miscelleneous_DL.getclientIP();
                    appealObject.DepttId = Convert.ToInt16(ddlDepartment.SelectedValue);
                    appealObject.Remarks = HttpUtility.HtmlEncode(txtremarks.Text.Replace(Environment.NewLine, "<br />"));

                    appealObject.MetaDescription = txtMetaDescription.Text;
                    appealObject.MetaKeyWords = txtMetaKeyword.Text;
                    appealObject.MetaTitle = txtMetaTitle.Text;
                    appealObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;

                    appealObject.appeal = 0;
                    p_Var.Result = appealBL.insertUpdateTempAppeal(appealObject);
                    if (p_Var.Result > 0)
                    {

                        obj_audit.ActionType = "I";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = "New Record";
                        obj_audit.Title = txtAppealNo.Text + " of " + date.Year.ToString();
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);
                        Session["msg"] = "Appeal has been added successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
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

    #region button btnReset click event to reset contents

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtAppealNo.Text = "";
        txtAppealSubject.Text = "";
        txtApplicantAdd.Text = "";
        txtApplicantEmailID.Text = "";
        txtApplicantFaxNo.Text = "";
        txtApplicantMobileNo.Text = "";
        txtApplicantName.Text = "";
        txtApplicantPhoneNo.Text = "";
        txtApplicationDate.Text = "";
        txtRespondentAddress.Text = "";
        txtRespondentEmailID.Text = "";
        txtRespondentFaxNo.Text = "";
        txtRespondentMobileNo.Text = "";
        txtRespondentName.Text = "";
        txtRespondentPhoneNo.Text = "";
        txtremarks.Text = "";
        //txtYear.Text = "";

    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
       Response.Redirect(ResolveUrl("~/Auth/AdminPanel/Appeal/") + "DisplayAppeal.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal));

    }

    #endregion

    //End

    //Area for all the custom Validators server side events

    #region customValidator cusAppealNo serverValidate event to validate appeal number existance in database

    protected void cusAppealNo_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            p_Var.dSet = null;
            appealObject.AppealNo = HttpUtility.HtmlEncode(txtAppealNo.Text);
            //Code to written on date 04-12-2012
            DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
            appealObject.year = date.Year.ToString();
            //appealObject.year = txtYear.Text.Trim();
            //End
            p_Var.dSet = appealBL.getAppealNumber(appealObject);
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

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            //obj_userOB.DepttId = Convert.ToInt16(Session["Dept_ID"]);
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
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


    protected void CusomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {

        if (Convert.ToInt16(args.Value.ToString().Substring(6, 4)) > DateTime.Today.Year)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }

    //End

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
