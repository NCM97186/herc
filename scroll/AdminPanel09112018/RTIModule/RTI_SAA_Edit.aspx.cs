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

public partial class Auth_AdminPanel_RTI_RTI_SAA_Edit : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration zone

    #region Data declaration zone

    PetitionOB rtiObject = new PetitionOB();
    RtiBL rtiBL = new RtiBL();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    UserOB obj_userOB = new UserOB();

    #endregion

    //End

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": Edit RTI SAA";
        this.Page.Title = " Edit RTI SAA: HERC";

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

            if (Request.QueryString["rti_saa_Id"] != null)    // for Edit
            {
                bindRtiStatus();
                bindRtiSaaInEditMode(Convert.ToInt32(Request.QueryString["rti_saa_Id"]));
            }

            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
    }

    #endregion

    //Area for all the buttons click event 

    #region button btnUpdateRtiSaa click event to update rti saa

    protected void btnUpdateRtiSaa_Click(object sender, EventArgs e)
    {
          string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (Page.IsValid)
                {
                    rtiObject.ActionType = Convert.ToInt32(Module_ID_Enum.Project_Action_Type.update);
                    rtiObject.RefNo = HttpUtility.HtmlEncode(lblReferenceNo.Text);
                    rtiObject.TempRTISAAId = Convert.ToInt32(Request.QueryString["rti_saa_id"]);
                    rtiObject.OldRTISAAId = Convert.ToInt32(Request.QueryString["rti_saa_id"]);

                    rtiObject.MetaDescription = txtMetaDescription.Text;
                    rtiObject.MetaKeyWords = txtMetaKeyword.Text;
                    rtiObject.MetaTitle = txtMetaTitle.Text;
                    rtiObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;

                    DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
                    rtiObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
                    rtiObject.ApplicationDate = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
                    rtiObject.ApplicantName = HttpUtility.HtmlEncode(txtApplicantName.Text);
                    rtiObject.ApplicantAddress = HttpUtility.HtmlEncode(txtApplicantAdd.Text);
                    rtiObject.ApplicantMobileNo = HttpUtility.HtmlEncode(txtApplicantMobileNo.Text);
                    rtiObject.ApplicantPhoneNo = HttpUtility.HtmlEncode(txtApplicantPhoneNo.Text);
                    rtiObject.ApplicantFaxNo = HttpUtility.HtmlEncode(txtApplicantFaxNo.Text);
                    rtiObject.ApplicantEmail = HttpUtility.HtmlEncode(txtApplicantEmailID.Text);
                    if (txtSAppletAuthority.Text != null && txtSAppletAuthority.Text != "")
                    {
                        rtiObject.Saa = HttpUtility.HtmlEncode(txtSAppletAuthority.Text);
                    }
                    else
                    {
                        rtiObject.Saa = null;
                    }
                    rtiObject.SaaRef_No = HttpUtility.HtmlEncode(txtSAA.Text);
                    rtiObject.subject = HttpUtility.HtmlEncode(txtRtiSubject.Text);
                    rtiObject.RTISAAStatusId = Convert.ToInt32(ddlRTISAAStatus_Edit.SelectedValue);
                    rtiObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
                    rtiObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                    rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                    if (ViewState["DeptID"] != DBNull.Value)
                    {
                        rtiObject.DepttId = Convert.ToInt32(ViewState["DeptID"]);
                    }
                    else
                    {
                        rtiObject.DepttId = null;
                    }
                    if (ViewState["Lang_Id"] != DBNull.Value)
                    {
                        rtiObject.LangId = Convert.ToInt32(ViewState["Lang_Id"]);
                    }
                    else
                    {
                        rtiObject.LangId = null;
                    }
                    if (ViewState["RTI_FAA_Id"] != DBNull.Value)
                    {
                        rtiObject.RTIFAAId = Convert.ToInt32(ViewState["RTI_FAA_Id"]);
                    }
                    else
                    {
                        rtiObject.RTIFAAId = null;
                    }

                    rtiObject.appeal = 0;
                    rtiObject.PlaceHolderFive = HttpUtility.HtmlEncode(txtremarks_Edit.Text); //Remarks in Rti
                    if (!String.IsNullOrEmpty(txtApplicantName.Text) && !String.IsNullOrEmpty(txtApplicantAdd.Text) && !String.IsNullOrEmpty(txtRtiSubject.Text))
                    {
                        p_Var.Result = rtiBL.insertUpdateTempRTISAA(rtiObject);

                        if (p_Var.Result > 0)
                        {
                            Session["msg"] = "RTI application for this SAA has been updated successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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

    #region button btnReset click event to reset contents

    protected void btnReset_Click(object sender, EventArgs e)
    {
        bindRtiSaaInEditMode(Convert.ToInt32(Request.QueryString["rti_saa_id"]));
    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/RTIModule/") + "DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"]);
    }

    #endregion

    //End

    //Area for all the user defined functions

    #region Function to bind rti-saa in edit mode

    public void bindRtiSaaInEditMode(int rtisaaID)
    {
        try
        {

            rtiObject.TempRTISAAId = rtisaaID;
            rtiObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = rtiBL.getRtiSAARecordForEdit(rtiObject);

         

            txtMetaDescription.Text = p_Var.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            txtMetaTitle.Text = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
            txtMetaKeyword.Text = p_Var.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
            ddlMetaLang.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();
            if (Convert.ToInt32(Request.QueryString["DepttID"]) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
            {
                lblReferenceNo.Text += " HERC/RTI-" + p_Var.dSet.Tables[0].Rows[0]["ref_no"].ToString() + " Of " + p_Var.dSet.Tables[0].Rows[0]["rtiyear"].ToString() + " , HERC/FAA-" + p_Var.dSet.Tables[0].Rows[0]["RTIFAA"].ToString() + " Of " + p_Var.dSet.Tables[0].Rows[0]["RTIFAAYEAR"].ToString();
            }
            else
            {
                lblReferenceNo.Text += " EO/RTI-" + p_Var.dSet.Tables[0].Rows[0]["ref_no"].ToString() + " Of " + p_Var.dSet.Tables[0].Rows[0]["rtiyear"].ToString() + " , EO/FAA-" + p_Var.dSet.Tables[0].Rows[0]["RTIFAA"].ToString() + " Of " + p_Var.dSet.Tables[0].Rows[0]["RTIFAAYEAR"].ToString();
            }
            txtSAppletAuthority.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["saa"].ToString());
            txtSAA.Text = p_Var.dSet.Tables[0].Rows[0]["SAA_RefNo"].ToString();
            txtApplicantName.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Applicant_Name"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtApplicantMobileNo.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Mobile_No"].ToString();
            txtApplicantPhoneNo.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Phone_No"].ToString();
            txtApplicantFaxNo.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Fax_No"].ToString();
            txtApplicantEmailID.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Email"].ToString();
            txtRtiSubject.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Subject"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtApplicantAdd.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Applicant_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtApplicationDate.Text = p_Var.dSet.Tables[0].Rows[0]["Application_Date"].ToString();
            txtremarks_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceholderFive"].ToString(); //use for remarks
            ddlRTISAAStatus_Edit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["RTI_SAA_Status_Id"].ToString();

            if (p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"] != DBNull.Value)
            {
                ViewState["DeptID"] = p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"].ToString();
            }
            else
            {
                ViewState["DeptID"] = DBNull.Value;
            }
            if (p_Var.dSet.Tables[0].Rows[0]["Lang_Id"] != DBNull.Value)
            {
                ViewState["Lang_Id"] = p_Var.dSet.Tables[0].Rows[0]["Lang_Id"].ToString();
            }
            else
            {
                ViewState["Lang_Id"] = DBNull.Value;
            }
            if (p_Var.dSet.Tables[0].Rows[0]["RTI_FAA_Id"] != DBNull.Value)
            {
                ViewState["RTI_FAA_Id"] = p_Var.dSet.Tables[0].Rows[0]["RTI_FAA_Id"].ToString();
            }
            else
            {
                ViewState["RTI_FAA_Id"] = DBNull.Value;
            }


        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind status of rti

    public void bindRtiStatus()
    {
        p_Var.dSet = rtiBL.getRtiStatus_SAA();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlRTISAAStatus_Edit.DataSource = p_Var.dSet;
            ddlRTISAAStatus_Edit.DataTextField = "STATUS";
            ddlRTISAAStatus_Edit.DataValueField = "STATUS_ID";
            ddlRTISAAStatus_Edit.DataBind();
        }
    }

    #endregion

    //End


    #region customValidator cusReferenceNo serverValidate event to validate reference no. duplicacy
    protected void cusReferenceNoSAA_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            p_Var.dSet = null;
            rtiObject.RefNo = HttpUtility.HtmlEncode(txtSAA.Text);
            //rtiObject.year = txtYear.Text.Trim();
            //1 feb 2013
            DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
            rtiObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
            rtiObject.LangId = Convert.ToInt32(ViewState["Lang_Id"]);
            rtiObject.RTISaaId = Convert.ToInt32(Request.QueryString["rti_saa_Id"]);
            rtiObject.DepttId = Convert.ToInt32(Request.QueryString["DepttID"]);
            p_Var.dSet = rtiBL.getReferenceNumberSAAEdit(rtiObject);
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

    protected void CusomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (Convert.ToInt32(args.Value.ToString().Substring(6, 4)) > DateTime.Today.Year)
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
