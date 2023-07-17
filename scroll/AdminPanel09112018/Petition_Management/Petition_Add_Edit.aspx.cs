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
using System.Data.SqlClient;
using System.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

public partial class Auth_AdminPanel_Petition_Management_Petition_Add_Edit : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration

    #region Variables declaration zone

    PetitionOB petitionObject = new PetitionOB();
    PetitionBL petPetitionBL = new PetitionBL();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    UserOB obj_userOB = new UserOB();
    LinkBL obj_LinkBL = new LinkBL();
    static bool flag = false;


    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    int Action;

    #endregion

    //End

    //Area for page load event

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.Path = "~/" + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/";
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

            BindData(Convert.ToInt32(Request.QueryString["Petition_Id"]));
            bindPetitionYearinDdl();
            divConnectAdd.Visible = false;
            if (Request.QueryString["Petition_Id"] != null)    // for Edit
            {

                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Edit  Petition";
                this.Page.Title = "Edit Petition: HERC";

                pnlEdit.Visible = true;
                pnlPetitionAdd.Visible = false;
                displayMetaLang1();
                bindDdlPetitionStatus_Edit();
                bindPetitionYearinDdlEdit();

                bindPetition_IN_Edit_Mode(Convert.ToInt32(Request.QueryString["Petition_Id"]));

            }
            else
            {

                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Add  Petition";
                this.Page.Title = "Add Petition: HERC";

                pnlEdit.Visible = false;
                pnlPetitionAdd.Visible = true;

                bindropDownlistLang(); // Get the Language privilage

            }
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
    }

    #endregion

    //End

    //web methods

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAutoCompleteData(string Respondent_Name)
    {
        List<string> result = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["connectionstring"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Respondent_Name from Web_Petition where " +
                "Respondent_Name like @SearchText + '%'";

                cmd.Parameters.AddWithValue("@SearchText", Respondent_Name);
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["Respondent_Name"].ToString());
                }
                conn.Close();
                return result;
            }
        }
    }


    //End

    //Area for all buttons, link buttons and image buttons click events

    #region button btnAddPetition click event to add new petition

    protected void btnAddPetition_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (Page.IsValid)
                {
                    try
                    {

                        petitionObject.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                        petitionObject.PRONo = HttpUtility.HtmlEncode(txtPetitionPROno.Text);
                        DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
                        petitionObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
                        petitionObject.PetitionerName = HttpUtility.HtmlEncode(txtPetitionerName.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.PetitionerMobileNo = HttpUtility.HtmlEncode(txtPetitionerMobileNo.Text);
                        petitionObject.PetitionerPhoneNo = HttpUtility.HtmlEncode(txtPetitionerPhoneNo.Text);
                        petitionObject.PetitionerFaxNo = HttpUtility.HtmlEncode(txtPetitionerFaxNo.Text);
                        petitionObject.PetitionerAddress = HttpUtility.HtmlEncode(txtPetitionerAdd.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.PetitionerEmail = HttpUtility.HtmlEncode(txtPetitionerEmailID.Text);
                        petitionObject.PetitionDate = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
                        petitionObject.RespondentName = HttpUtility.HtmlEncode(txtRespondentName.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.RespondentPhone_No = HttpUtility.HtmlEncode(txtRespondentPhoneNo.Text);
                        petitionObject.RespondentMobileNo = HttpUtility.HtmlEncode(txtRespondentMobileNo.Text);
                        petitionObject.RespondentFaxNo = HttpUtility.HtmlEncode(txtRespondentFaxNo.Text);
                        petitionObject.RespondentAddress = HttpUtility.HtmlEncode(txtRespondentAdd.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.Respondentmail = HttpUtility.HtmlEncode(txtRespondentEmailID.Text);
                        petitionObject.subject = HttpUtility.HtmlEncode(txtPetitionSubject.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.PetitionStatusId = Convert.ToInt16(ddlPetitionStatus.SelectedValue);
                        petitionObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                        petitionObject.LangId = Convert.ToInt16(Module_ID_Enum.Language_ID.English);
                        petitionObject.IpAddress = Miscelleneous_DL.getclientIP();
                        petitionObject.ReView = 0;

                        petitionObject.MetaDescription = txtMetaDescription.Text;
                        petitionObject.MetaKeyWords = txtMetaKeyword.Text;
                        petitionObject.MetaTitle = txtMetaTitle.Text;
                        petitionObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;
                        petitionObject.PlaceHolderFive = HttpUtility.HtmlEncode(txtURL.Text);
                        petitionObject.PlaceHolderFour = HttpUtility.HtmlEncode(txtURLDescription.Text);
                        petitionObject.counter = p_Var.count;

                        petitionObject.InsertedBy = Convert.ToInt16(Session["User_Id"]);
                        petitionObject.Remarks = HttpUtility.HtmlEncode(txtRemarks.Text.Replace(Environment.NewLine, "<br />"));
                        p_Var.Result = petPetitionBL.insertPetition(petitionObject);
                        if (p_Var.Result > 0)
                        {
                            foreach (ListItem li in chklstPetition.Items)
                            {
                                if (li.Selected == true)
                                {
                                    petitionObject.PetitionId = p_Var.Result;
                                    petitionObject.ConnectedPetitionID = Convert.ToInt16(li.Value.ToString());
                                    int cnt = li.Text.LastIndexOf(' ');
                                    petitionObject.year = li.Text.Substring(cnt).Trim();
                                    p_Var.Result1 = petPetitionBL.insertConnectedPetition(petitionObject);

                                }
                            }
                            if (p_Var.Result > 0)
                            {

                                string fileMultiple = string.Empty;
                                HttpFileCollection hfc = Request.Files;

                                for (int i = 0; i < hfc.Count; i++)
                                {
                                    HttpPostedFile hpf = hfc[i];

                                    fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
                                    if (fileMultiple != null && fileMultiple != "")
                                    {

                                        PetitionOB newObj = new PetitionOB();
                                        newObj.PetitionId = p_Var.Result;
                                        newObj.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                                        newObj.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);
                                        newObj.PetitionDate = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
                                        if (i == 0)
                                        {
                                            newObj.Description = TextBox1.Text;
                                        }
                                        else
                                        {
                                            int j = i - 1;


                                            string strId = "txt" + j.ToString();
                                            newObj.Description = Request.Form[strId].ToString();
                                        }
                                        p_Var.ext = System.IO.Path.GetExtension(fileMultiple);
                                        p_Var.ext = p_Var.ext.ToUpper();
                                        if (p_Var.ext.Equals(".PDF"))
                                        {
                                            lblmsg.Visible = false;
                                            p_Var.Filename = hpf.FileName;
                                            p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(hpf.FileName);
                                            p_Var.ext = Path.GetExtension(hpf.FileName);
                                            p_Var.Filename = miscellBL.getUniqueFileName(fileMultiple, Server.MapPath(p_Var.Path), p_Var.filenamewithout_Ext, p_Var.ext);
                                            hpf.SaveAs(Server.MapPath(p_Var.Path + p_Var.Filename));

                                            newObj.PetitionFile = p_Var.Filename;
                                        }
                                        int Result2 = petPetitionBL.insertConnectedPetitionFiles(newObj);
                                    }
                                }

                                //}

                                obj_audit.ActionType = "I";
                                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                                obj_audit.UserName = Session["UserName"].ToString();
                                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                                obj_audit.IpAddress = miscellBL.IpAddress();
                                obj_audit.Title = txtPetitionerName.Text;
                                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);


                                Session["msg"] = "Petition has been submitted successfully.";
                                Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                            }

                        }
                    }
                    catch
                    {
                        throw;
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

    #region button btnPetitionUpdate click event to update petition

    protected void btnPetitionUpdate_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {

                if (Page.IsValid)
                {
                    try
                    {

                        petitionObject.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.update);
                        petitionObject.StatusId = Convert.ToInt16(Session["Status_Id"]);
                        petitionObject.TempPetitionId = Convert.ToInt32(Request.QueryString["Petition_Id"]);
                        petitionObject.OldPetitionId = Convert.ToInt32(Request.QueryString["Petition_Id"]);
                        petitionObject.LangId = Convert.ToInt16(Request.QueryString["LangID"]);
                        petitionObject.PRONo = HttpUtility.HtmlEncode(txtPetitionPROno_Edit.Text);
                        DateTime date = miscellBL.getDateFormat(txtPetitionDate_Edit.Text);
                        petitionObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
                        petitionObject.Remarks = HttpUtility.HtmlEncode(txtRemarksEdit.Text);
                        petitionObject.PetitionDate = miscellBL.getDateFormat(txtPetitionDate_Edit.Text);
                        petitionObject.PetitionerName = HttpUtility.HtmlEncode(txtPetitionerName_Edit.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.PetitionerAddress = HttpUtility.HtmlEncode(txtPetitionerAddr_Edit.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.PetitionerMobileNo = HttpUtility.HtmlEncode(txtPetitionerMobileNo_Edit.Text);
                        petitionObject.PetitionerPhoneNo = HttpUtility.HtmlEncode(txtPetitionerPhoneNo_Edit.Text);
                        petitionObject.PetitionerFaxNo = HttpUtility.HtmlEncode(txtPetitionerFaxNo_Edit.Text);
                        petitionObject.PetitionerEmail = HttpUtility.HtmlEncode(txtPetitionerEmailID_Edit.Text);
                        petitionObject.RespondentName = HttpUtility.HtmlEncode(txtRespondentName_Edit.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.RespondentAddress = HttpUtility.HtmlEncode(txtRespondentAddr_Edit.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.RespondentMobileNo = HttpUtility.HtmlEncode(txtRespondentMobileNo_Edit.Text);
                        petitionObject.RespondentPhone_No = HttpUtility.HtmlEncode(txtRespondentPhoneNo_Edit.Text);
                        petitionObject.RespondentFaxNo = HttpUtility.HtmlEncode(txtRespondentFaxNo_Edit.Text);
                        petitionObject.Respondentmail = HttpUtility.HtmlEncode(txtRespondentEmailID_Edit.Text);
                        petitionObject.subject = HttpUtility.HtmlEncode(txtPetitionSubject_Edit.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.PetitionStatusId = Convert.ToInt16(ddlPetitionStatus_Edit.SelectedValue);

                        petitionObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
                        petitionObject.PlaceHolderFive = HttpUtility.HtmlEncode(txtURLEdit.Text);
                        petitionObject.PlaceHolderFour = HttpUtility.HtmlEncode(txtURLDescriptionEdit.Text);
                        petitionObject.MetaDescription = txtMetaDescriptionEdit.Text;
                        petitionObject.MetaKeyWords = txtMetaKeywordEdit.Text;
                        petitionObject.MetaTitle = txtMetaTitleEdit.Text;
                        petitionObject.MetaKeyLanguage = DropDownList1.SelectedValue;
                        petitionObject.IpAddress = Miscelleneous_DL.getclientIP();
                        petitionObject.ReView = 0;

                        petitionObject.InsertedBy = Convert.ToInt16(Session["User_Id"]);
                        petitionObject.Remarks = HttpUtility.HtmlEncode(txtRemarksEdit.Text.Replace(Environment.NewLine, "<br />"));
                        p_Var.Result = petPetitionBL.insertPetition(petitionObject);
                        if (p_Var.Result > 0)
                        {
                            petitionObject.PetitionId = p_Var.Result;
                            p_Var.Result1 = petPetitionBL.deleteConnectedPetition(petitionObject);
                            foreach (ListItem li in chklstPetitionEdit.Items)
                            {
                                if (li.Selected == true)
                                {
                                    petitionObject.PetitionId = p_Var.Result;
                                    petitionObject.ConnectedPetitionID = Convert.ToInt16(li.Value.ToString());

                                    int cnt = li.Text.LastIndexOf(' ');
                                    petitionObject.year = li.Text.Substring(cnt).Trim();
                                    if (lnkConnectedPetitionEditNo.Visible != false)
                                    {
                                        p_Var.Result1 = petPetitionBL.insertConnectedPetition(petitionObject);
                                    }
                                }
                            }
                            if (p_Var.Result > 0)
                            {
                                string fileMultiple = string.Empty;
                                HttpFileCollection hfc = Request.Files;

                                for (int i = 0; i < hfc.Count; i++)
                                {
                                    HttpPostedFile hpf = hfc[i];

                                    fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
                                    if (fileMultiple != null && fileMultiple != "")
                                    {
                                        PetitionOB newObEdit = new PetitionOB();
                                        newObEdit.PetitionId = p_Var.Result;
                                        newObEdit.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                                        newObEdit.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);
                                        newObEdit.PetitionDate = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate_Edit.Text.ToString()));

                                        if (i == 0)
                                        {
                                            newObEdit.Description = TextBox2.Text;
                                        }
                                        else
                                        {
                                            int j = i - 1;


                                            string strId = "txt" + j.ToString();
                                            newObEdit.Description = Request.Form[strId].ToString();
                                        }
                                        p_Var.ext = System.IO.Path.GetExtension(fileMultiple);
                                        p_Var.ext = p_Var.ext.ToUpper();
                                        if (p_Var.ext.Equals(".PDF"))
                                        {
                                            lblmsg.Visible = false;
                                            p_Var.Filename = hpf.FileName;
                                            p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(hpf.FileName);
                                            p_Var.ext = Path.GetExtension(hpf.FileName);
                                            p_Var.Filename = miscellBL.getUniqueFileName(fileMultiple, Server.MapPath(p_Var.Path), p_Var.filenamewithout_Ext, p_Var.ext);
                                            hpf.SaveAs(Server.MapPath(p_Var.Path + p_Var.Filename));
                                            newObEdit.PetitionFile = p_Var.Filename;
                                        }

                                        int Result2 = petPetitionBL.insertConnectedPetitionFiles(newObEdit);
                                    }
                                }
                                obj_audit.ActionType = "U";
                                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                                obj_audit.UserName = Session["UserName"].ToString();
                                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                                obj_audit.IpAddress = miscellBL.IpAddress();
                                obj_audit.Title = txtPetitionerName_Edit.Text;
                                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                                Session["msg"] = "Petition has been updated successfully.";
                                Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                            }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }
    }

    #endregion

    #region button btnReset click event to reset fields

    protected void btnReset_Click(object sender, EventArgs e)
    {

        txtPetitionPROno.Text = "";

        txtPetitionerName.Text = "";
        txtPetitionDate.Text = "";
        txtPetitionerEmailID.Text = "";
        txtRespondentName.Text = "";
        txtPetitionSubject.Text = "";
        txtPetitionerMobileNo.Text = "";
        txtPetitionerPhoneNo.Text = "";
        txtPetitionPROno.Text = "";
        txtRespondentAdd.Text = "";
        txtRespondentEmailID.Text = "";
        txtRespondentFaxNo.Text = "";
        txtRespondentMobileNo.Text = "";
        txtRespondentPhoneNo.Text = "";
        txtPetitionerFaxNo.Text = "";
        txtPetitionerAdd.Text = "";
        TextBox1.Text = "";
        txtRemarks.Text = "";

    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/Petition_Management/") + "Display_Petition.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));

    }

    #endregion

    #region button btnReset_Edit click event reset petition in edit mode

    protected void btnReset_Edit_Click(object sender, EventArgs e)
    {
        TextBox2.Text = "";
        bindPetition_IN_Edit_Mode(Convert.ToInt32(Request.QueryString["Petition_Id"]));
    }

    #endregion

    #region button btnBack_Edit click event to go back from edit mode

    protected void btnBack_Edit_Click(object sender, EventArgs e)
    {
        // hold the previous page reference
        object refUrl = ViewState["RefUrl"];
        if (refUrl != null)
        {
            Response.Redirect((string)refUrl);
        }
    }

    #endregion

    #region link button lnkPublicNotice click event to remove public notice

    protected void lnkPublicNoticeRemove_Click(object sender, EventArgs e)
    {

    }

    #endregion

    //End

    //Area for all user defined functions

    #region Function to bind language in dropDownlist according to permission

    public void bindropDownlistLang()
    {
        try
        {



        }
        catch
        {

        }
    }

    #endregion



    #region Function to bind Petition status in dropDownlist

    public void bindDdlPetitionStatus_Edit()
    {
        try
        {

            petitionObject.PetitionStatusId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
            p_Var.dSet = miscellBL.getStatusAccordingtoModule(petitionObject);
            ddlPetitionStatus_Edit.DataSource = p_Var.dSet;
            ddlPetitionStatus_Edit.DataTextField = "Status";
            ddlPetitionStatus_Edit.DataValueField = "Status_Id";
            ddlPetitionStatus_Edit.DataBind();
            ddlPetitionStatus_Edit.Items.Insert(0, new ListItem("Select Status", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind petition in edit mode

    public void bindPetition_IN_Edit_Mode(int petition_ID)
    {
        try
        {

            btnAddPetition.Visible = false;
            btnPetitionUpdate.Visible = true;

            petitionObject.TempPetitionId = Convert.ToInt32(petition_ID);
            petitionObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = petPetitionBL.get_Temp_Petition_Records_Edit(petitionObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                txtPetitionPROno_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["PRO_No"].ToString());

                txtPetitionDate_Edit.Text = miscellBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["Petition_Date"].ToString());
                txtPetitionerName_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Name"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                txtPetitionerAddr_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                txtPetitionerMobileNo_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Mobile_No"].ToString());
                txtPetitionerPhoneNo_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Phone_No"].ToString());
                txtPetitionerFaxNo_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Fax_No"].ToString());

                txtPetitionerEmailID_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Email"].ToString());
                txtRespondentName_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Name"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                txtRespondentAddr_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                txtRespondentMobileNo_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Mobile_No"].ToString());
                txtRespondentPhoneNo_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Phone_No"].ToString());
                txtRespondentFaxNo_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Fax_No"].ToString());
                txtRespondentEmailID_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Email"].ToString());
                txtPetitionSubject_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Subject"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                ddlPetitionStatus_Edit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["Petition_Status_Id"].ToString();
                txtURLEdit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["PlaceHolderFive"].ToString());
                txtURLDescriptionEdit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["PlaceHolderFour"].ToString());
                txtMetaDescriptionEdit.Text = p_Var.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
                txtMetaTitleEdit.Text = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
                txtMetaKeywordEdit.Text = p_Var.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
                DropDownList1.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();

                txtRemarksEdit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Remarks"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                if (p_Var.dSet.Tables[0].Rows[0]["Petition_File"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["Petition_File"].ToString() != "")
                {

                    ViewState["filename"] = p_Var.dSet.Tables[0].Rows[0]["Petition_File"].ToString();
                }
                else
                {

                }
                if (p_Var.dSet.Tables[0].Rows[0]["Public_Notice"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["Public_Notice"].ToString() != "")
                {

                    ViewState["publicNotice"] = p_Var.dSet.Tables[0].Rows[0]["Public_Notice"].ToString();

                }

                else
                {

                }
                PetitionOB newOB = new PetitionOB();
                PetitionBL newBL = new PetitionBL();
                newOB.PetitionId = Convert.ToInt32(petition_ID);
                p_Var.dSetChildData = newBL.get_ConnectedPetition_Edit(newOB);
                DataSet dsetYear = new DataSet();
                StringBuilder sbuilderYear = new StringBuilder();
                dsetYear = newBL.getPetitionYearForConnectionEdit(newOB);
                if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
                {

                    for (p_Var.i = 0; p_Var.i < dsetYear.Tables[0].Rows.Count; p_Var.i++)
                    {
                        foreach (ListItem li in ddlYearEdit.Items)
                        {
                            if (li.Value.ToString() == dsetYear.Tables[0].Rows[p_Var.i]["year"].ToString().Trim())
                            {
                                li.Selected = true;
                                sbuilderYear.Append(li.Text).Append(",");

                            }

                        }
                    }


                    newOB.year = sbuilderYear.ToString();

                    ViewState["year"] = sbuilderYear.ToString();
                    getpetitionNumberForConnectionEdit();


                    for (p_Var.i = 0; p_Var.i < p_Var.dSetChildData.Tables[0].Rows.Count; p_Var.i++)
                    {
                        foreach (ListItem li in chklstPetitionEdit.Items)
                        {
                            if (li.Value.ToString() == p_Var.dSetChildData.Tables[0].Rows[p_Var.i]["Connected_Petition_id"].ToString())
                            {
                                li.Selected = true;

                            }

                        }
                    }
                    pnlEditConnectedPetition.Visible = true;
                    lnkConnectedPetitionEdit.Visible = true;
                    lnkConnectedPetitionEditNo.Visible = true;
                    divConnectEdit.Visible = true;
                }
                else
                {
                    pnlEditConnectedPetition.Visible = false;
                    lnkConnectedPetitionEdit.Visible = true;
                    lnkConnectedPetitionEditNo.Visible = false;
                    divConnectEdit.Visible = false;
                }
            }

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Petition numbers for ChkBoxConnection

    public void getpetitionNumberForChkBoxConnection()
    {

        p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
        StringBuilder strBuilder = new StringBuilder();
        foreach (ListItem li in ddlYear.Items)
        {
            if (li.Selected == true)
            {
                p_Var.sbuilder.Append(li.Text).Append(",");
            }

        }
        List<string> list = new List<string>();

        foreach (ListItem li in chklstPetition.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
            }

        }

        ViewState["MyList"] = list;
        petitionObject.year = p_Var.sbuilder.ToString();

        p_Var.dSet = petPetitionBL.getPetitionNumberForConnection(petitionObject);

        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            chklstPetition.DataSource = p_Var.dSet;
            chklstPetition.DataValueField = "Petition_id";
            chklstPetition.DataTextField = "PRONoValue";
            chklstPetition.DataBind();
        }
        else
        {
            chklstPetition.DataSource = p_Var.dSet;

            chklstPetition.DataBind();
        }

        if (ViewState["MyList"] != null)
        {
            List<string> list1 = ViewState["MyList"] as List<string>;
            foreach (ListItem li in chklstPetition.Items)
            {
                foreach (string val in list1)
                {
                    if (li.Value == val)
                    {
                        li.Selected = true;
                        strBuilder.Append(li.Text + ";");
                    }
                }

            }
        }
        ltrlSelected.Text = strBuilder.ToString();
    }

    #endregion

    #region Function to get Petition numbers for connections

    public void getpetitionNumberForConnection()
    {
        petitionObject.year = ddlYear.SelectedValue;
        p_Var.dSet = petPetitionBL.getPetitionNumberForConnection(petitionObject);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            chklstPetition.DataSource = p_Var.dSet;
            chklstPetition.DataValueField = "Petition_id";
            chklstPetition.DataTextField = "PRONoValue";
            chklstPetition.DataBind();
        }
    }

    #endregion

    #region Function to get Petition numbers for connections in Edit mode

    public void getpetitionNumberForConnectionEdit()
    {

        p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
        StringBuilder strBuilder = new StringBuilder();
        foreach (ListItem li in ddlYearEdit.Items)
        {
            if (li.Selected == true)
            {
                p_Var.sbuilder.Append(li.Value).Append(",");
            }

        }
        List<string> list = new List<string>();

        foreach (ListItem li in chklstPetitionEdit.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
            }

        }

        // ViewState["MyList"] = list;
        if (list.Count > 0)
        {
            ViewState["MyList"] = list;
        }
        else
        {
            ViewState["MyList"] = ViewState["MyList"];
        }

        petitionObject.year = p_Var.sbuilder.ToString();
        petitionObject.PetitionId = Convert.ToInt32(Request.QueryString["Petition_Id"]);
        p_Var.dSet = petPetitionBL.getPetitionNumberForConnectionEdit(petitionObject);

        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            chklstPetitionEdit.DataSource = p_Var.dSet;
            chklstPetitionEdit.DataValueField = "Petition_id";
            chklstPetitionEdit.DataTextField = "PRONoValue";
            chklstPetitionEdit.DataBind();
        }
        else
        {
            chklstPetitionEdit.DataSource = p_Var.dSet;

            chklstPetitionEdit.DataBind();
        }

        PetitionOB newOB = new PetitionOB();
        DataSet dsnew1 = new DataSet();
        newOB.PetitionId = Convert.ToInt32(Request.QueryString["Petition_Id"]);
        newOB.year = p_Var.sbuilder.ToString();
        dsnew1 = petPetitionBL.getConnectedPetitionEditNew(newOB);

        for (p_Var.i = 0; p_Var.i < dsnew1.Tables[0].Rows.Count; p_Var.i++)
        {
            foreach (ListItem li in chklstPetitionEdit.Items)
            {
                if (li.Value.ToString() == dsnew1.Tables[0].Rows[p_Var.i]["Connected_Petition_id"].ToString().Trim())
                {
                    li.Selected = true;
                    strBuilder.Append(li.Text + ";");
                }

            }
        }

        if (ViewState["MyList"] != null)
        {
            List<string> list1 = ViewState["MyList"] as List<string>;
            string[] stringArray = strBuilder.ToString().Split(';');
            list1.AddRange(stringArray);
            // list1 = list.Distinct().ToList();
            list1 = list.Distinct().ToList();
            strBuilder.Remove(0, strBuilder.Length);
            foreach (ListItem li in chklstPetitionEdit.Items)
            {
                foreach (string val in list1)
                {
                    if (li.Value == val)
                    {
                        li.Selected = true;
                        strBuilder.Append(li.Text + ";");
                    }
                }
            }
        }
        ltrlSelectedEdit.Text = strBuilder.ToString();
    }

    #endregion

    #region Function to get Petition numbers for connections

    public void getpetitionNumberForConnectionYesNo()
    {

        PetitionOB newOB = new PetitionOB();
        PetitionBL newBL = new PetitionBL();
        newOB.PetitionId = Convert.ToInt32(Request.QueryString["Petition_Id"]);

        DataSet dsetYear = new DataSet();
        StringBuilder sbuilderYear = new StringBuilder();
        dsetYear = newBL.getPetitionYearForConnectionEdit(newOB);
        if (dsetYear.Tables[0].Rows.Count > 0)
        {
            for (p_Var.i = 0; p_Var.i < dsetYear.Tables[0].Rows.Count; p_Var.i++)
            {
                foreach (ListItem li in ddlYearEdit.Items)
                {
                    if (li.Value.ToString() == dsetYear.Tables[0].Rows[p_Var.i]["year"].ToString().Trim())
                    {
                        li.Selected = true;
                        sbuilderYear.Append(li.Text).Append(",");
                    }
                }
            }
        }
        List<string> list = new List<string>();

        foreach (ListItem li in chklstPetition.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
            }
        }

        ViewState["MyList"] = list;

        petitionObject.year = sbuilderYear.ToString();

        ViewState["year"] = sbuilderYear.ToString();
        getpetitionNumberForConnectionEdit();

        if (ViewState["MyList"] != null)
        {
            List<string> list1 = ViewState["MyList"] as List<string>;
            foreach (ListItem li in chklstPetition.Items)
            {
                foreach (string val in list1)
                {
                    if (li.Value == val)
                    {
                        li.Selected = true;
                    }
                }
            }
        }
    }

    #endregion



    //Area for all dropDownlist events


    #region dropDownlist ddlPetitionStatus_Edit selectedIndexChanged event zone

    protected void ddlPetitionStatus_Edit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ddlPetitionStatus_Edit.SelectedValue) != 0)
        {
            if (Convert.ToInt16(ddlPetitionStatus_Edit.SelectedValue) != Convert.ToInt16(Module_ID_Enum.Petition_Status.InProcess))
            {
                petitionObject.StatusId = Convert.ToInt16(ddlPetitionStatus_Edit.SelectedValue);
                petitionObject.PetitionId = Convert.ToInt32(Request.QueryString["Petition_Id"]);
                p_Var.flag = false;
                p_Var.flag = petPetitionBL.getUploaderID(petitionObject);
                if (p_Var.flag)
                {
                    lblUploadMsg.Visible = false;
                    btnPetitionUpdate.Visible = true;
                }
                else
                {

                    lblUploadMsg.Visible = true;
                    btnPetitionUpdate.Visible = false;
                    lblUploadMsg.Text = "You can not update this status, because no file has been uploaded regarding this status.";
                    lblUploadMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {

                lblUploadMsg.Visible = false;
                btnPetitionUpdate.Visible = true;
            }
        }
        else
        {

            lblUploadMsg.Visible = false;
            btnPetitionUpdate.Visible = true;
        }
    }

    #endregion

    //End

    //Area for all types of validators events

    #region CustomValidator cusPetitionPROno serverValidate to validate PRO number

    protected void cusPetitionPROno_ServerValidate(object source, ServerValidateEventArgs args)
    {

        try
        {
            p_Var.dSet = null;
            petitionObject.PRONo = HttpUtility.HtmlEncode(txtPetitionPROno.Text);
            DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
            petitionObject.year = date.Year.ToString();

            p_Var.dSet = petPetitionBL.getPRO_Number(petitionObject);
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

    #region CustomValidator cus_PetitionPROno_Edit serverValidate to validate PRO number in Edit mode

    protected void cus_PetitionPROno_Edit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            p_Var.dSet = null;
            petitionObject.PRONo = HttpUtility.HtmlEncode(txtPetitionPROno_Edit.Text);
            DateTime date = miscellBL.getDateFormat(txtPetitionDate_Edit.Text);
            petitionObject.year = date.Year.ToString();
            petitionObject.TempPetitionId = Convert.ToInt32(Request.QueryString["Petition_Id"]);
            p_Var.dSet = petPetitionBL.getPRO_Number_In_EditMode(petitionObject);
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

    #region customValidator cvFilePetition serverValidate to validate file type

    protected void cvPetition_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Get file name
        string UploadFileName = fuPetition_Edit.PostedFile.FileName;

        if (UploadFileName == "")
        {
            // There is no file selected
            args.IsValid = false;
        }
        else
        {
            string Extension = UploadFileName.Substring(UploadFileName.LastIndexOf('.') + 1).ToLower();

            if (Extension == "pdf")
            {
                args.IsValid = true; // Valid file type
            }
            else
            {
                args.IsValid = false; // Not valid file type
            }
        }


    }

    #endregion

    #region customValidator cvPublicNotice serverValidate to validate file type

    protected void cvPublicNotice_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Get file name
        string UploadFileName = fuPetition.PostedFile.FileName;

        if (UploadFileName == "")
        {
            // There is no file selected
            args.IsValid = false;
        }
        else
        {
            string Extension = UploadFileName.Substring(UploadFileName.LastIndexOf('.') + 1).ToLower();

            if (Extension == "pdf")
            {
                args.IsValid = true; // Valid file type
            }
            else
            {
                args.IsValid = false; // Not valid file type
            }
        }
        string fileMultiple = string.Empty;
        HttpFileCollection hfc = Request.Files;
        bool strem = true;
        Regex regex = new Regex(@"^[a-z | 0-9 | /,|,:;()#&]*$", RegexOptions.IgnoreCase);
        string str = string.Empty;
        for (int i = 0; i < hfc.Count; i++)
        {
            HttpPostedFile hpf = hfc[i];

            fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
            strem = miscellBL.GetActualFileType_pdf(hfc[i].InputStream);
            if (strem == true)
            {

                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
                break;
            }

            if (i == 0)
            {
                Match match = regex.Match(TextBox1.Text);
                if (match.Success == true)
                {
                    args.IsValid = true;
                }
                else
                {

                    args.IsValid = false;
                    cvPublicNotice.Text = "Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)";
                    TextBox1.Text = "";

                }
            }
            else
            {
                int j = i - 1;
                str = "txt" + j.ToString();
                Match match = regex.Match(Request.Form[str].ToString());
                if (match.Success == true)
                {
                    args.IsValid = true;
                }
                else
                {

                    args.IsValid = false;
                    cvPublicNotice.Text = "Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)";
                    TextBox1.Text = "";
                }
            }
        }
    }

    #endregion

    #region customValidator customValidator2 serverValidate to validate file type

    protected void cvPetitionEdit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Get file name

        string UploadFileName = fuPetition_Edit.PostedFile.FileName;

        if (UploadFileName == "")
        {
            // There is no file selected
            args.IsValid = false;
        }
        else
        {
            string Extension = UploadFileName.Substring(UploadFileName.LastIndexOf('.') + 1).ToLower();

            if (Extension == "pdf")
            {
                args.IsValid = true; // Valid file type
            }
            else
            {
                args.IsValid = false; // Not valid file type
            }
        }

        string fileMultiple = string.Empty;
        HttpFileCollection hfc = Request.Files;
        bool strem = true;
        Regex regex = new Regex(@"^[a-z | 0-9 | /,|,:;()#&]*$", RegexOptions.IgnoreCase);
        string str = string.Empty;
        for (int i = 0; i < hfc.Count; i++)
        {
            HttpPostedFile hpf = hfc[i];

            fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
            strem = miscellBL.GetActualFileType_pdf(hfc[i].InputStream);
            if (strem == true)
            {

                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
                break;
            }

            if (i == 0)
            {
                Match match = regex.Match(TextBox2.Text);
                if (match.Success == true)
                {
                    args.IsValid = true;
                }
                else
                {

                    args.IsValid = false;
                    CustomValidator1.Text = "Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)";
                    TextBox1.Text = "";

                }
            }
            else
            {
                int j = i - 1;
                str = "txt" + j.ToString();
                Match match = regex.Match(Request.Form[str].ToString());
                if (match.Success == true)
                {
                    args.IsValid = true;
                }
                else
                {

                    args.IsValid = false;
                    CustomValidator1.Text = "Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)";
                    TextBox1.Text = "";
                }
            }

        }

    }

    #endregion


    #region customValidator CompanySelectionValidation server side validation

    protected void CompanySelectionValidation_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (args.IsValid == true)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
        catch
        {
            args.IsValid = false;
        }
    }

    #endregion

    //End

    #region linkButton lnkConnectedPetition click event to display connected petition

    protected void lnkConnectedPetition_Click(object sender, EventArgs e)
    {
        lnkConnectedPetitionNo.Visible = true;
        lnkConnectedPetition.Visible = true;
        bindPetitionYearinDdl();
        getpetitionNumberForChkBoxConnection();

        pnlPetitionConnection.Visible = true;
        divConnectAdd.Visible = true;
    }

    #endregion

    #region linkButton lnkConnectedPetitionNo click event

    protected void lnkConnectedPetitionNo_Click(object sender, EventArgs e)
    {
        lnkConnectedPetition.Visible = true;
        lnkConnectedPetitionNo.Visible = false;
        pnlPetitionConnection.Visible = false;
        bindPetitionYearinDdl();
        divConnectAdd.Visible = false;
        ltrlSelected.Text = "";
    }

    #endregion

    #region linkButton lnkConnectedPetitionEdit click event to display connected petition

    protected void lnkConnectedPetitionEdit_Click(object sender, EventArgs e)
    {
        lnkConnectedPetitionEditNo.Visible = true;
        lnkConnectedPetitionEdit.Visible = true;
        pnlEditConnectedPetition.Visible = true;
        bindPetitionYearinDdlEdit();
        getpetitionNumberForConnectionYesNo();
        divConnectEdit.Visible = true;
        //17 jan 2013
    }

    #endregion

    #region linkButton lnkConnectedPetitionEditNo click event

    protected void lnkConnectedPetitionEditNo_Click(object sender, EventArgs e)
    {
        lnkConnectedPetitionEditNo.Visible = false;
        lnkConnectedPetitionEdit.Visible = true;
        pnlEditConnectedPetition.Visible = false;

        getpetitionNumberForChkBoxConnection();
        divConnectEdit.Visible = false;
    }

    #endregion

    #region Function to bind petition Year

    public void bindPetitionYearinDdl()
    {
        p_Var.dSet = petPetitionBL.GetYearPetition_AddEdit();
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

    #region Function to bind petition Year

    public void bindPetitionYearinDdlEdit()
    {
        p_Var.dSet = petPetitionBL.GetYearPetition_AddEdit();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYearEdit.DataSource = p_Var.dSet;
            ddlYearEdit.DataTextField = "year";
            ddlYearEdit.DataValueField = "year";
            ddlYearEdit.DataBind();
        }
        else
        {
            ddlYear.Items.Add(new ListItem("Select", "0"));
        }
    }

    #endregion

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getpetitionNumberForChkBoxConnection();
    }

    protected void ddlYearEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        getpetitionNumberForConnectionEdit();

    }

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

    protected void CusomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
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


    #region function  bind with Repeator

    public void BindData(int PetitionId)
    {
        petitionObject.PetitionId = PetitionId;
        p_Var.dsFileName = petPetitionBL.getFileNameForPetition(petitionObject);
        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
        {
            PFileName.Visible = true;
            datalistFileName.DataSource = p_Var.dsFileName;
            datalistFileName.DataBind();
        }
        else
        {
            PFileName.Visible = false;
        }
    }

    #endregion

    protected void datalistFileName_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("File"))
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            petitionObject.ConnectionID = id;
            p_Var.Result1 = petPetitionBL.UpdateFileStatusForPetitions(petitionObject);

            if (p_Var.Result1 > 0)
            {

                Label filename = (Label)e.Item.FindControl("lblFile");
                Label lblComments = (Label)e.Item.FindControl("lblComments");
                Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");

                LinkButton RemoveFileLink = (LinkButton)e.Item.FindControl("lnkFileConnectedRemove");

                filename.Visible = false;
                lblComments.Visible = false;
                RemoveFileLink.Visible = false;
                ltrlDownload.Visible = false;
            }


            bindPetition_IN_Edit_Mode(Convert.ToInt32(Request.QueryString["Petition_Id"]));

        }

    }
    protected void chklstPetitionSelectedIndexChangd(object sender, EventArgs e)
    {
        string value = string.Empty;
        StringBuilder strBuilder = new StringBuilder();
        string result = Request.Form["__EVENTTARGET"];

        string[] checkedBox = result.Split('$');
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (chklstPetition.Items[index].Selected)
        {

            petitionObject.PetitionId = Convert.ToInt32(chklstPetition.Items[index].Value);
            p_Var.dSet = petPetitionBL.get_ConnectedPetition_Edit(petitionObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_Var.i = 0; p_Var.i < p_Var.dSet.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in chklstPetition.Items)
                    {

                        if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["Connected_Petition_id"].ToString())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");
                        }

                    }

                }
            }
            else
            {
                strBuilder.Remove(0, strBuilder.Length);
                foreach (ListItem li in chklstPetition.Items)
                {
                    if (li.Selected == true)
                    {
                        strBuilder.Append(li.Text + ";");
                    }
                }


            }
            ltrlSelected.Text = strBuilder.ToString();
        }
        else
        {
            strBuilder.Remove(0, strBuilder.Length);
            foreach (ListItem li in chklstPetition.Items)
            {
                if (li.Selected == true)
                {
                    strBuilder.Append(li.Text + ";");
                }
            }

            ltrlSelected.Text = strBuilder.ToString();
        }
    }
    protected void chklstPetitionEditSelectedIndexChanged(object sender, EventArgs e)
    {
        string value = string.Empty;

        string result = Request.Form["__EVENTTARGET"];
        StringBuilder strBuilder = new StringBuilder();
        string[] checkedBox = result.Split('$');
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (chklstPetitionEdit.Items[index].Selected)
        {

            petitionObject.PetitionId = Convert.ToInt32(chklstPetitionEdit.Items[index].Value);
            p_Var.dSet = petPetitionBL.get_ConnectedPetition_Edit(petitionObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_Var.i = 0; p_Var.i < p_Var.dSet.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in chklstPetitionEdit.Items)
                    {

                        if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["Connected_Petition_id"].ToString())
                        {
                            li.Selected = true;
                        }

                    }

                }
            }

            strBuilder.Remove(0, strBuilder.Length);
            foreach (ListItem li in chklstPetitionEdit.Items)
            {
                if (li.Selected == true)
                {
                    strBuilder.Append(li.Text + ";");
                }
            }
            ltrlSelectedEdit.Text = strBuilder.ToString();
        }
        else
        {
            strBuilder.Remove(0, strBuilder.Length);
            foreach (ListItem li in chklstPetitionEdit.Items)
            {
                if (li.Selected == true)
                {
                    strBuilder.Append(li.Text + ";");
                }
            }

            ltrlSelectedEdit.Text = strBuilder.ToString();
        }
    }

    protected void datalistFileName_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var link = e.Item.FindControl("lnkFileConnectedRemove") as LinkButton;
            var hiddenField = e.Item.FindControl("hiddenFieldPetitionID") as HiddenField;


            if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
            {
                if (link != null)
                {
                    if (Convert.ToInt16(Session["Status_Id"]) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        link.Visible = true;//if user has right then enabled else disabled
                    }
                    else
                    {
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["Petition_Id"]))
                        {
                            link.Visible = true;
                        }
                        else
                        {
                            link.Visible = false;

                        }
                    }
                }
            }
            else
            {
                if (link != null)
                {

                    if (Convert.ToInt32(Session["Status_Id"]) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        link.Visible = false;//if user has right then enabled else disabled
                    }
                    else
                    {
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["Petition_Id"]))
                        {
                            link.Visible = true;
                        }
                        else
                        {
                            link.Visible = false;

                        }
                    }
                }
            }
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

            Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");
            Label filename = (Label)e.Item.FindControl("lblFile");

            p_Var.sbuilder.Append("<a href='" + ResolveUrl(p_Var.Path) + filename.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='Download File' width='15' alt='Download File' height=\"15\" /> " + "</a>");
            p_Var.sbuilder.Append("<hr />");
            ltrlDownload.Text = p_Var.sbuilder.ToString();

            LinkButton lnkFileConnectedRemove = (LinkButton)e.Item.FindControl("lnkFileConnectedRemove");
            lnkFileConnectedRemove.OnClientClick = "javascript:return confirm('Are you sure you want to delete this file :- " + filename.Text + " ?')";
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

    #region function to bind metalanguage

    public void displayMetaLang1()
    {
        p_Var.dSet = miscellBL.getMetaLanguage();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            DropDownList1.DataSource = p_Var.dSet;
            DropDownList1.DataTextField = "languagename";
            DropDownList1.DataValueField = "LanguageKey";
            DropDownList1.DataBind();
        }
    }

    #endregion
}
