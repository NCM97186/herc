using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;


public partial class Auth_AdminPanel_Petition_Management_PetitionReview_Insert_Update : CrsfBase //System.Web.UI.Page
{
    #region Data declaration zone

    PetitionOB petitionObject = new PetitionOB();
    PetitionBL petPetitionBL = new PetitionBL();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    UserOB obj_userOB = new UserOB();
    OrderBL ObjOrderBL = new OrderBL();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();
    #endregion

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.url = "~/" + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/";
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

            bindPetitionYearinDdl();
            BindData(Convert.ToInt32(Request.QueryString["rp_id"]));
            divConnectAdd.Visible = false;
            if (Request.QueryString["rp_id"] != null)    // for Edit
            {
                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Edit  Review Petition";
                this.Page.Title = "Edit Review Petition: HERC";

                pnlEdit.Visible = true;
                pnlPetitionAdd.Visible = false;

                displayMetaLang1();
                bindPetitionYearinDdlEdit();


                bindDdlPetition_review_Status_Edit(); //Display the petition stattus in dropdownlist
                LblPetitionPRO.Visible = true;

                bindPetition_Review_IN_Edit_Mode(Convert.ToInt32(Request.QueryString["rp_id"]));
            }
            else
            {

                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Add  Review Petition";
                this.Page.Title = "Add Review Petition: HERC";


                pnlEdit.Visible = false;
                pnlPetitionAdd.Visible = true;

                pPetition_Review.Visible = true;

                if (Request.QueryString["Petion_id"] != null && Request.QueryString["Petion_id"] != "")
                {
                    bind_Petitioner_Details(Convert.ToInt32(Request.QueryString["Petion_id"]));
                }

            }
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }

    }

    #endregion

    //Area for all the buttons, linkbuttons, imagebutton click events

    #region button btnAddReviewPetition click event to add petition review

    protected void btnAddReviewPetition_Click(object sender, EventArgs e)
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
                        pdate.Visible = false;
                        ppublicnoiotice.Visible = false;
                        petitionObject.ActionType = 1;
                        petitionObject.RPNo = HttpUtility.HtmlEncode(txtPetitionRPno.Text);

                        DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
                        petitionObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
                        petitionObject.PetitionDate = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
                        petitionObject.ApplicantName = HttpUtility.HtmlEncode(txtApplicantName.Text.Replace(Environment.NewLine, "<br />"));
                        // 1 march 2013 

                        petitionObject.PetitionerAddress = HttpUtility.HtmlEncode(txtPetitionerAdd.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.RespondentAddress = HttpUtility.HtmlEncode(txtRespondentAdd.Text.Replace(Environment.NewLine, "<br />"));

                        petitionObject.MetaDescription = txtMetaDescription.Text;
                        petitionObject.MetaKeyWords = txtMetaKeyword.Text;
                        petitionObject.MetaTitle = txtMetaTitle.Text;
                        petitionObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;
                        petitionObject.PlaceHolderFive = HttpUtility.HtmlEncode(txtURL.Text);
                        petitionObject.PlaceHolderFour = HttpUtility.HtmlEncode(txtURLDescription.Text);
                        petitionObject.RespondentPhone_No = HttpUtility.HtmlEncode(txtRespondentPhoneNo.Text);
                        petitionObject.RespondentMobileNo = HttpUtility.HtmlEncode(txtRespondentMobileNo.Text);
                        petitionObject.RespondentFaxNo = HttpUtility.HtmlEncode(txtRespondentFaxNo.Text);
                        petitionObject.Respondentmail = HttpUtility.HtmlEncode(txtRespondentEmailID.Text);

                        //end
                        if (txtApplicantMobileNo.Text != null && txtApplicantMobileNo.Text != "")
                        {
                            petitionObject.ApplicantMobileNo = HttpUtility.HtmlEncode(txtApplicantMobileNo.Text);
                        }
                        else
                        {
                            petitionObject.ApplicantMobileNo = null;
                        }
                        if (txtApplicantPhoneNo.Text != null && txtApplicantPhoneNo.Text != "")
                        {
                            petitionObject.ApplicantPhoneNo = HttpUtility.HtmlEncode(txtApplicantPhoneNo.Text);
                        }
                        else
                        {
                            petitionObject.ApplicantPhoneNo = null;
                        }
                        if (txtApplicantFaxNo.Text != null && txtApplicantFaxNo.Text != "")
                        {
                            petitionObject.ApplicantFaxNo = HttpUtility.HtmlEncode(txtApplicantFaxNo.Text);
                        }
                        else
                        {
                            petitionObject.ApplicantFaxNo = null;
                        }
                        if (txtRemarks.Text != null && txtRemarks.Text != "")
                        {
                            petitionObject.Description = txtRemarks.Text;
                        }
                        else
                        {
                            petitionObject.Description = null;

                        }

                        petitionObject.ApplicantEmail = HttpUtility.HtmlEncode(txtApplicantEmailID.Text);
                        petitionObject.ResPondent = HttpUtility.HtmlEncode(txtRespondentName.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.subject = HttpUtility.HtmlEncode(txtPetReviewSubject.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.RPStatusId = Convert.ToInt16(ddlPetReviewStatus.SelectedValue);
                        petitionObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                        petitionObject.LangId = Convert.ToInt16(Module_ID_Enum.Language_ID.English);

                        petitionObject.appeal = 0;


                        if (txtHearingDate.Text != null && txtHearingDate.Text != "")
                        {
                            petitionObject.Soh = miscellBL.getDateFormat(HttpUtility.HtmlEncode(txtHearingDate.Text));
                        }
                        else
                        {
                            petitionObject.Soh = null;
                        }
                        petitionObject.InsertedBy = Convert.ToInt16(Session["User_Id"]);
                        petitionObject.IpAddress = Miscelleneous_DL.getclientIP();
                        petitionObject.PRONo = Convert.ToString(Request.QueryString["P_Number"]);

                        petitionObject.PetitionId = Convert.ToInt32(Request.QueryString["Petion_id"]);
                        if (!String.IsNullOrEmpty(txtApplicantName.Text) && !String.IsNullOrEmpty(txtPetitionerAdd.Text) && !String.IsNullOrEmpty(txtPetReviewSubject.Text))
                        {
                            p_Var.Result = petPetitionBL.insert_Review_Petition(petitionObject);
                            if (p_Var.Result > 0)
                            {
                                foreach (ListItem li in chklstPetition.Items)
                                {
                                    if (li.Selected == true)
                                    {
                                        petitionObject.RPId = p_Var.Result;
                                        petitionObject.ConnectedPetitionID = Convert.ToInt16(li.Value.ToString());
                                        int cnt = li.Text.LastIndexOf(' ');
                                        petitionObject.year = li.Text.Substring(cnt).Trim();
                                        p_Var.Result1 = petPetitionBL.insertConnectedPetitionReview(petitionObject);

                                    }
                                }



                                if (rdioConnectedOrder.SelectedValue != "" && rdioConnectedOrder.SelectedValue != null)
                                {
                                    petitionObject.year = drpConnectedOrderYear.SelectedValue;
                                    petitionObject.OrderTypeID = Convert.ToInt16(rdioConnectedOrder.SelectedValue);
                                    petitionObject.OrderID = Convert.ToInt16(drpConnectedOrderId.SelectedValue);
                                    petitionObject.ReView = p_Var.Result;
                                    int connectedOrder = ObjOrderBL.InsertIntoReviewPetitionConnectedOrder(petitionObject);
                                }



                                string fileMultiple = string.Empty;
                                HttpFileCollection hfc = Request.Files;
                                for (int i = 0; i < hfc.Count; i++)
                                {
                                    HttpPostedFile hpf = hfc[i];

                                    fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
                                    if (fileMultiple != null && fileMultiple != "")
                                    {
                                        PetitionOB newObj = new PetitionOB();
                                        newObj.PetitionId = Convert.ToInt32(Request.QueryString["Petion_id"]);
                                        newObj.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                                        newObj.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);
                                        newObj.PetitionDate = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
                                        newObj.RPId = p_Var.Result;
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
                                            p_Var.Filename = miscellBL.getUniqueFileName(fileMultiple, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);
                                            hpf.SaveAs(Server.MapPath(p_Var.url + p_Var.Filename));
                                            newObj.PetitionFile = p_Var.Filename;
                                        }
                                        int Result1 = petPetitionBL.insertConnectedReviewPetitionFiles(newObj);
                                    }
                                }

                                obj_audit.ActionType = "I";
                                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                                obj_audit.UserName = Session["UserName"].ToString();
                                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                                obj_audit.IpAddress = miscellBL.IpAddress();
                                obj_audit.status = "New Record";

                                DateTime date1 = miscellBL.getDateFormat(txtPetitionDate.Text);
                                int year = date1.Year;

                                obj_audit.Title = "HERC/RA- " + txtPetitionRPno.Text + " of " + year.ToString();
                                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                                Session["msg"] = "Review Petition has been submitted successfully.";
                                Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Review_Petition_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                            }
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(txtApplicantName.Text))
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter petitioner name');", true);
                            }
                            else if (String.IsNullOrEmpty(txtPetitionerAdd.Text))
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter petitioner address');", true);
                            }

                            else if (String.IsNullOrEmpty(txtPetReviewSubject.Text))
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter subject');", true);
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

        else
        {
            Session.Abandon();
            Response.Redirect("~/Auth/AdminPanel/Login.aspx");
        }
    }

    #endregion

    #region button btnReset click event to reset all modified data of textboxes

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtApplicantEmailID.Text = "";
        txtApplicantFaxNo.Text = "";
        txtApplicantMobileNo.Text = "";
        txtApplicantName.Text = "";
        txtApplicantPhoneNo.Text = "";
        txtHearingDate.Text = "";
        TextBox1.Text = "";
        txtPetitionDate.Text = "";
        txtPetitionerAdd.Text = "";
        txtPetitionRPno.Text = "";
        txtPetReviewSubject.Text = "";
        txtRemarks.Text = "";
        txtRespondentAdd.Text = "";
        txtRespondentEmailID.Text = "";
        txtRespondentFaxNo.Text = "";
        txtRespondentMobileNo.Text = "";
        txtRespondentName.Text = "";
        txtRespondentPhoneNo.Text = "";

    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/Petition_Management/") + "Review_Petition_Display.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
    }

    #endregion

    #region button btnUpdateReviewPetition click event update

    protected void btnUpdateReviewPetition_Click(object sender, EventArgs e)
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
                if (Page.IsValid)
                {
                    try
                    {
                        pSOHEdit.Visible = false;
                        PUploadEdit.Visible = false;
                        petitionObject.ActionType = 2;
                        petitionObject.StatusId = Convert.ToInt16(Session["Status_Id"]);
                        petitionObject.TempRPId = Convert.ToInt32(Request.QueryString["rp_id"]);
                        petitionObject.OldRPId = Convert.ToInt32(Request.QueryString["rp_id"]);
                        petitionObject.LangId = Convert.ToInt16(Request.QueryString["LangID"]);
                        petitionObject.RPNo = HttpUtility.HtmlEncode(txtPetitionRPno_Edit.Text);
                        DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDateEdit.Text.ToString()));
                        petitionObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
                        petitionObject.PetitionDate = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDateEdit.Text.ToString()));

                        petitionObject.PetitionerAddress = HttpUtility.HtmlEncode(txtPetitionerAddr_Edit.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.RespondentAddress = HttpUtility.HtmlEncode(txtRespondentAddr_Edit.Text.Replace(Environment.NewLine, "<br />"));

                        petitionObject.MetaDescription = txtMetaDescriptionEdit.Text;
                        petitionObject.MetaKeyWords = txtMetaKeywordEdit.Text;
                        petitionObject.MetaTitle = txtMetaTitleEdit.Text;
                        petitionObject.MetaKeyLanguage = DropDownList1.SelectedValue;

                        petitionObject.RespondentMobileNo = HttpUtility.HtmlEncode(txtRespondentMobileNo_Edit.Text);
                        petitionObject.RespondentPhone_No = HttpUtility.HtmlEncode(txtRespondentPhoneNo_Edit.Text);
                        petitionObject.RespondentFaxNo = HttpUtility.HtmlEncode(txtRespondentFaxNo_Edit.Text);
                        petitionObject.Respondentmail = HttpUtility.HtmlEncode(txtRespondentEmailID_Edit.Text);
                        petitionObject.PlaceHolderFive = HttpUtility.HtmlEncode(txtURLEdit.Text);
                        petitionObject.PlaceHolderFour = HttpUtility.HtmlEncode(txtURLDescriptionEdit.Text);
                        petitionObject.ApplicantName = HttpUtility.HtmlEncode(txtApplicantName_Edit.Text.Replace(Environment.NewLine, "<br />"));
                        if (txtApplicantMobileNo_Edit.Text != null && txtApplicantMobileNo_Edit.Text != "")
                        {
                            petitionObject.ApplicantMobileNo = HttpUtility.HtmlEncode(txtApplicantMobileNo_Edit.Text);
                        }
                        else
                        {
                            petitionObject.ApplicantMobileNo = null;
                        }
                        if (txtApplicantPhoneNo_Edit.Text != null && txtApplicantPhoneNo_Edit.Text != "")
                        {
                            petitionObject.ApplicantPhoneNo = HttpUtility.HtmlEncode(txtApplicantPhoneNo_Edit.Text);
                        }
                        else
                        {
                            petitionObject.ApplicantPhoneNo = null;
                        }
                        if (txtApplicantFaxNo_Edit.Text != null && txtApplicantFaxNo_Edit.Text != "")
                        {
                            petitionObject.ApplicantFaxNo = HttpUtility.HtmlEncode(txtApplicantFaxNo_Edit.Text);
                        }
                        else
                        {
                            petitionObject.ApplicantFaxNo = null;
                        }
                        if (txtRemarksEdit.Text != null && txtRemarksEdit.Text != "")
                        {
                            petitionObject.Description = txtRemarksEdit.Text;
                        }
                        else
                        {
                            petitionObject.Description = null;

                        }
                        petitionObject.ApplicantEmail = HttpUtility.HtmlEncode(txtApplicantEmailID_Edit.Text);
                        petitionObject.ResPondent = HttpUtility.HtmlEncode(txtRespondentName_Edit.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.subject = HttpUtility.HtmlEncode(txtApplicantSubject_Edit.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.RPStatusId = Convert.ToInt16(ddlPetReviewStatus_Edit.SelectedValue);
                        petitionObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
                        petitionObject.appeal = 0;
                        petitionObject.PetitionId = Convert.ToInt32(ViewState["petitionid"]);
                        petitionObject.IpAddress = Miscelleneous_DL.getclientIP();

                        if (txtHearingDate_Edit.Text != null && txtHearingDate_Edit.Text != "")
                        {
                            petitionObject.Soh = miscellBL.getDateFormat(HttpUtility.HtmlEncode(txtHearingDate_Edit.Text));
                        }
                        else
                        {
                            petitionObject.Soh = null;
                        }
                        petitionObject.InsertedBy = Convert.ToInt16(Session["User_Id"]);
                        petitionObject.LastUpdatedBy = Convert.ToInt16(Session["User_Id"]);
                        if (!String.IsNullOrEmpty(txtApplicantName_Edit.Text) && !String.IsNullOrEmpty(txtPetitionerAddr_Edit.Text) && !String.IsNullOrEmpty(txtApplicantSubject_Edit.Text))
                        {
                            p_Var.Result = petPetitionBL.insert_Review_Petition(petitionObject);

                            if (p_Var.Result > 0)
                            {
                                petitionObject.RPId = p_Var.Result;
                                p_Var.Result1 = petPetitionBL.deleteConnectedPetitionReview(petitionObject);
                                foreach (ListItem li in chklstPetitionEdit.Items)
                                {
                                    if (li.Selected == true)
                                    {
                                        petitionObject.RPId = p_Var.Result;
                                        petitionObject.ConnectedPetitionID = Convert.ToInt16(li.Value.ToString());
                                        int cnt = li.Text.LastIndexOf(' ');
                                        petitionObject.year = li.Text.Substring(cnt).Trim();
                                        //petitionObject.year = ddlYearEdit.SelectedValue.ToString();
                                        if (lnkConnectedPetitionEditNo.Visible != false)
                                        {
                                            p_Var.Result1 = petPetitionBL.insertConnectedPetitionReview(petitionObject);
                                        }
                                    }
                                }

                                int rslt = petPetitionBL.deleteConnectedPetitionReviewMultiple(petitionObject);

                                //28 Aug 2013 Connected Order with review petition
                                int connectedorderReview = ObjOrderBL.deleteConnectedOrderReviewPetition(petitionObject);
                                if (lnkConnectedOrderEditNo.Visible == true)
                                {
                                    if (rdioConnectedOrderEdit.SelectedValue != "" && rdioConnectedOrderEdit.SelectedValue != null)
                                    {
                                        petitionObject.year = drpConnectedOrderYearEdit.SelectedValue;
                                        petitionObject.OrderTypeID = Convert.ToInt32(rdioConnectedOrderEdit.SelectedValue);
                                        petitionObject.OrderID = Convert.ToInt32(drpConnectedOrderIdEdit.SelectedValue);
                                        petitionObject.ReView = p_Var.Result;
                                        int connectedOrder = ObjOrderBL.InsertIntoReviewPetitionConnectedOrder(petitionObject);
                                    }
                                }


                                //End

                                //if (fileReviewPetition_Edit.PostedFile != null && fileReviewPetition_Edit.PostedFile.ContentLength != 0)
                                //{
                                string fileMultiple = string.Empty;
                                HttpFileCollection hfc = Request.Files;

                                for (int i = 0; i < hfc.Count; i++)
                                {
                                    HttpPostedFile hpf = hfc[i];

                                    fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
                                    if (fileMultiple != null && fileMultiple != "")
                                    {
                                        PetitionOB newObEdit = new PetitionOB();
                                        newObEdit.PetitionId = Convert.ToInt32(ViewState["petitionid"]);
                                        newObEdit.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                                        newObEdit.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);
                                        newObEdit.PetitionDate = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDateEdit.Text.ToString()));
                                        newObEdit.RPId = p_Var.Result;
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
                                            p_Var.Filename = miscellBL.getUniqueFileName(fileMultiple, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);
                                            hpf.SaveAs(Server.MapPath(p_Var.url + p_Var.Filename));
                                            //newObj.PetitionFile = "P_" + p_Var.Filename;
                                            newObEdit.PetitionFile = p_Var.Filename;
                                        }
                                        int Result2 = petPetitionBL.insertConnectedReviewPetitionFiles(newObEdit);
                                    }

                                }

                                obj_audit.ActionType = "U";
                                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                                obj_audit.UserName = Session["UserName"].ToString();
                                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                                obj_audit.IpAddress = miscellBL.IpAddress();
                                string st = Request.QueryString["Status"].Trim();
                                if (st == "3") { obj_audit.status = "Draft"; } else if (st == "4") { obj_audit.status = "For Publish"; } else if (st == "6") { obj_audit.status = "Published"; } else if (st == "8") { obj_audit.status = "Deleted"; } else if (st == "21") { obj_audit.status = "For Review"; }
                                DateTime date1 = miscellBL.getDateFormat(txtPetitionDateEdit.Text);
                                int year = date1.Year;

                                obj_audit.Title = "HERC/RA- " + txtPetitionRPno_Edit.Text + " of " + year.ToString();

                                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);
                                Session["msg"] = "Review Petition has been updated successfully.";
                                Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Review_Petition_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                            }
                        }

                        else
                        {
                            if (String.IsNullOrEmpty(txtApplicantName_Edit.Text))
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter petitioner name');", true);
                            }
                            else if (String.IsNullOrEmpty(txtPetitionerAddr_Edit.Text))
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter petitioner address');", true);
                            }

                            else if (String.IsNullOrEmpty(txtApplicantSubject_Edit.Text))
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter subject');", true);
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

    #region button btnReset_Edit click event to reset all modified data of textboxes

    protected void btnReset_Edit_Click(object sender, EventArgs e)
    {

        TextBox2.Text = "";
        bindPetition_Review_IN_Edit_Mode(Convert.ToInt32(Request.QueryString["rp_id"]));
    }

    #endregion

    #region button btnBack_Edit click event to go back

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

    #region linkbutton lnkFileRemove click event to remove file

    protected void lnkFileRemove_Click(object sender, EventArgs e)
    {

    }

    #endregion

    #region linkbutton lnkPublicNoticeRemove click event to remove public notice pdf

    protected void lnkPublicNoticeRemove_Click(object sender, EventArgs e)
    {

    }

    #endregion

    //End

    //Area for all the server side validations

    #region custom validator cusPetitionRPno to validate RP number during add(RP)

    protected void cusPetitionRPno_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            p_Var.dSet = null;
            petitionObject.RPNo = HttpUtility.HtmlEncode(txtPetitionRPno.Text);
            DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
            petitionObject.year = date.Year.ToString();

            p_Var.dSet = petPetitionBL.getRP_Number(petitionObject);
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

    #region  custom validator cus_PetitionPROno_Edit to validate Rp Number(in edit mode)

    protected void cus_PetitionPROno_Edit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            p_Var.dSet = null;
            petitionObject.RPNo = HttpUtility.HtmlEncode(txtPetitionRPno_Edit.Text);
            DateTime date = miscellBL.getDateFormat(txtPetitionDateEdit.Text);
            petitionObject.year = date.Year.ToString();
            petitionObject.TempRPId = Convert.ToInt32(Request.QueryString["rp_id"]);
            p_Var.dSet = petPetitionBL.getRP_Number_In_EditMode(petitionObject);
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

    #region custom validator cvFileReviewPetition to validate pdf

    protected void cvFileReviewPetition_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }

    #endregion

    #region custom validator cvPublicNotice to validate pdf

    protected void cvPublicNotice_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string UploadFileName = fileReviewPetition.PostedFile.FileName;

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
                    CustomValidator3.Text = "Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)";
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
                    CustomValidator3.Text = "Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)";
                    TextBox1.Text = "";
                }
            }

        }
    }

    #endregion



    #region custom validator cu_ReviewPetition_Edit to validate pdf(in edit mode)

    protected void cu_ReviewPetition_Edit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string UploadFileName = fileReviewPetition_Edit.PostedFile.FileName;

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
                    cu_ReviewPetition_Edit.Text = "Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)";
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
                    cu_ReviewPetition_Edit.Text = "Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)";
                    TextBox1.Text = "";
                }
            }
        }
    }

    #endregion

    //End

    //Area for all the dropDownlist events

    #region dropDownlist ddlPetReviewStatus selectedIndexChanged event

    protected void ddlPetReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ddlPetReviewStatus.SelectedValue) != 0)
        {
            if (Convert.ToInt16(ddlPetReviewStatus.SelectedValue) != 10)
            {
                pPetition_Review.Visible = true;
            }
            else
            {
                pPetition_Review.Visible = false;
            }
        }
        else
        {
            pPetition_Review.Visible = false;
        }
    }

    #endregion

    #region dropDownlist ddlPetReviewStatus_Edit selectedIndexChanged event zone

    protected void ddlPetReviewStatus_Edit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ddlPetReviewStatus_Edit.SelectedValue) != 0)
        {
            if (Convert.ToInt16(ddlPetReviewStatus_Edit.SelectedValue) != 10)
            {
                pPetition_Review_Edit.Visible = true;
            }
            else
            {
                pPetition_Review_Edit.Visible = false;
            }
        }
        else
        {
            pPetition_Review_Edit.Visible = false;
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

            //obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            //obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            //p_Var.dSet = miscellBL.getLanguagePermission(obj_userOB);
            //if (p_Var.dSet.Tables[0].Rows.Count > 0)
            //{
            //    UserOB usrObject = new UserOB();
            //    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true && Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
            //    {
            //        usrObject.english = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.English);
            //        usrObject.hindi = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Hindi);

            //        p_Var.sbuilder.Append(usrObject.english).Append(",");
            //        p_Var.sbuilder.Append(usrObject.hindi);
            //    }
            //    else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true)
            //    {
            //        usrObject.hindi = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Hindi);

            //        p_Var.sbuilder.Append(usrObject.hindi);
            //    }
            //    else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
            //    {
            //        usrObject.english = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.English);

            //        p_Var.sbuilder.Append(usrObject.english);
            //    }
            //    usrObject.LangId = p_Var.sbuilder.ToString().Trim();
            //    p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            //    p_Var.dSet = null;
            //    p_Var.dSet = miscellBL.getLanguage(usrObject);
            //    PLanguage.Visible = true;
            //    ddlLanguage.DataSource = p_Var.dSet;
            //    ddlLanguage.DataTextField = "Language";
            //    ddlLanguage.DataValueField = "Lang_Id";
            //    ddlLanguage.DataBind();

            //}
            //p_Var.dSet = null;


        }
        catch
        {

        }
    }

    #endregion

    #region Function to bind Petition status in dropDownlist

    public void bindDdlPetitionStatus()
    {
        try
        {

            petitionObject.PetitionStatusId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
            p_Var.dSet = miscellBL.getStatusAccordingtoModule(petitionObject);
            ddlPetReviewStatus.DataSource = p_Var.dSet;
            ddlPetReviewStatus.DataTextField = "Status";
            ddlPetReviewStatus.DataValueField = "Status_Id";
            ddlPetReviewStatus.DataBind();
            ddlPetReviewStatus.Items.Insert(0, new ListItem("Select Status", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind petition review in edit mode

    public void bindPetition_Review_IN_Edit_Mode(int rp_ID)
    {
        try
        {
            //PLanguage.Visible = false;
            btnAddReviewPetition.Visible = false;
            btnUpdateReviewPetition.Visible = true;

            petitionObject.TempRPId = Convert.ToInt32(rp_ID);
            petitionObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = petPetitionBL.get_Temp_Review_Petition_RecordsEdit(petitionObject);

            txtPetitionRPno_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["RP_No"].ToString();
            LblPetitionPRO.Visible = true;
            txtURLEdit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["placeholderfive"].ToString());
            txtMetaDescriptionEdit.Text = p_Var.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            txtMetaTitleEdit.Text = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
            txtMetaKeywordEdit.Text = p_Var.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
            DropDownList1.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();

            LblPetitionPRO.Text += p_Var.dSet.Tables[0].Rows[0]["PRO_NO"].ToString() + " Of Year :" + p_Var.dSet.Tables[0].Rows[0]["proYear"].ToString();

            //txtYear_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["year"].ToString();
            txtApplicantName_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Applicant_Name"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtApplicantMobileNo_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Mobile_No"].ToString();
            txtApplicantFaxNo_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Fax_No"].ToString();
            txtApplicantPhoneNo_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Phone_No"].ToString();
            txtApplicantEmailID_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Email"].ToString();
            txtRespondentName_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtApplicantSubject_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Subject"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtPetitionDateEdit.Text = p_Var.dSet.Tables[0].Rows[0]["ReviewpetitionDate"].ToString();
            txtPetitionerAddr_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtRespondentAddr_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));

            txtRespondentMobileNo_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Mobile_No"].ToString());
            txtRespondentPhoneNo_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Phone_No"].ToString());
            txtRespondentFaxNo_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Fax_No"].ToString());
            txtRespondentEmailID_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Email"].ToString());

            ddlPetReviewStatus_Edit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["RP_Status_id"].ToString();
            txtRemarksEdit.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
            txtURLDescriptionEdit.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceholderFour"].ToString();
            txtURLEdit.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceholderFive"].ToString();


            if (p_Var.dSet.Tables[0].Rows[0]["Petition_Id"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["Petition_Id"].ToString() != "")
            {
                ViewState["petitionid"] = p_Var.dSet.Tables[0].Rows[0]["Petition_Id"];
            }
            else
            {
                ViewState["petitionid"] = null;
            }
            if (p_Var.dSet.Tables[0].Rows[0]["File_Name"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString() != "")
            {

                ViewState["filename"] = p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString();
            }
            else
            {

            }
            if (p_Var.dSet.Tables[0].Rows[0]["Public_Notice"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["Public_Notice"].ToString() != "")
            {
                lblOldPublicNotice.Visible = true;
                lblPublicNotice.Visible = true;
                lnkPublicNoticeRemove.Visible = true;
                lblPublicNotice.Text = p_Var.dSet.Tables[0].Rows[0]["Public_Notice"].ToString().Remove(0, 3);
                ViewState["publicNotice"] = p_Var.dSet.Tables[0].Rows[0]["Public_Notice"].ToString();

            }
            else
            {
                lblOldPublicNotice.Visible = false;
                lblPublicNotice.Visible = false;
                lnkPublicNoticeRemove.Visible = false;
            }
            if (Convert.ToInt16(ddlPetReviewStatus_Edit.SelectedValue) != 10)
            {
                pPetition_Review.Visible = true;
            }
            else
            {
                pPetition_Review.Visible = false;
            }

            if (p_Var.dSet.Tables[0].Rows[0]["SOH"] != DBNull.Value)
            {
                txtHearingDate_Edit.Text = miscellBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["SOH"].ToString());
            }
            else
            {
                txtHearingDate_Edit.Text = "";
            }

            PetitionOB newOB = new PetitionOB();
            PetitionBL newBL = new PetitionBL();
            newOB.RPId = Convert.ToInt32(rp_ID);
            p_Var.dSetChildData = newBL.get_ConnectedPetitionReview_Edit(newOB);
            DataSet dsetYear = new DataSet();
            StringBuilder sbuilderYear = new StringBuilder();
            dsetYear = newBL.getReviewPetitionYearForConnectionEdit(newOB);
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
                        if (li.Value.ToString() == p_Var.dSetChildData.Tables[0].Rows[p_Var.i]["Connected_RP_Id"].ToString())
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







            //29 Aug 2013 Connected Order with review petition

            petitionObject.ReView = Convert.ToInt32(rp_ID);
            p_Var.dsFileName = ObjOrderBL.GetReviewPetitionConnectedOrder(petitionObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                lnkConnectedOrderEdit.Visible = true;
                pnlConnectedOrderEdit.Visible = true;
                PConnectedOrderIdEdit.Visible = true;
                lnkConnectedOrderEditNo.Visible = true;
                GetYearConnectedOrderEdit(Convert.ToInt32(p_Var.dsFileName.Tables[0].Rows[0]["OrderTypeId"]));
                GetConnectedOrderIdEdit(Convert.ToInt32(p_Var.dsFileName.Tables[0].Rows[0]["OrderTypeId"]), p_Var.dsFileName.Tables[0].Rows[0]["Year"].ToString());
                drpConnectedOrderYearEdit.SelectedValue = p_Var.dsFileName.Tables[0].Rows[0]["Year"].ToString();
                rdioConnectedOrderEdit.SelectedValue = p_Var.dsFileName.Tables[0].Rows[0]["OrderTypeId"].ToString();
                drpConnectedOrderIdEdit.SelectedValue = p_Var.dsFileName.Tables[0].Rows[0]["orderid"].ToString();
            }

            //End


        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind Petition status in dropDownlist

    public void bindDdlPetition_review_Status_Edit()
    {

        try
        {

            petitionObject.PetitionStatusId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
            p_Var.dSet = miscellBL.getStatusAccordingtoModule(petitionObject);
            ddlPetReviewStatus_Edit.DataSource = p_Var.dSet;
            ddlPetReviewStatus_Edit.DataTextField = "Status";
            ddlPetReviewStatus_Edit.DataValueField = "Status_Id";
            ddlPetReviewStatus_Edit.DataBind();
            ddlPetReviewStatus_Edit.Items.Insert(0, new ListItem("Select Status", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind Petitioner's details during filling review of petition

    public void bind_Petitioner_Details(int petition_ID)
    {
        try
        {
            //PLanguage.Visible = false;
            petitionObject.TempPetitionId = Convert.ToInt32(petition_ID);
            petitionObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = petPetitionBL.get_Temp_Petition_Records_Edit(petitionObject);
            //txtYear.Text = p_Var.dSet.Tables[0].Rows[0]["year"].ToString();
            txtApplicantName.Text = p_Var.dSet.Tables[0].Rows[0]["Petitioner_Name"].ToString();
            txtApplicantMobileNo.Text = p_Var.dSet.Tables[0].Rows[0]["Petitioner_Mobile_No"].ToString();
            txtApplicantPhoneNo.Text = p_Var.dSet.Tables[0].Rows[0]["Petitioner_Phone_No"].ToString();
            txtApplicantFaxNo.Text = p_Var.dSet.Tables[0].Rows[0]["Petitioner_Fax_No"].ToString();
            txtApplicantEmailID.Text = p_Var.dSet.Tables[0].Rows[0]["Petitioner_Email"].ToString();
            txtRespondentName.Text = p_Var.dSet.Tables[0].Rows[0]["Respondent_Name"].ToString();
            txtPetReviewSubject.Text = p_Var.dSet.Tables[0].Rows[0]["Subject"].ToString();
            txtPetitionerAdd.Text = p_Var.dSet.Tables[0].Rows[0]["Petitioner_Address"].ToString();
            txtRespondentAdd.Text = p_Var.dSet.Tables[0].Rows[0]["Respondent_Address"].ToString();
            txtRespondentMobileNo.Text = p_Var.dSet.Tables[0].Rows[0]["Respondent_Mobile_No"].ToString();
            txtRespondentPhoneNo.Text = p_Var.dSet.Tables[0].Rows[0]["Respondent_Phone_No"].ToString();
            txtRespondentFaxNo.Text = p_Var.dSet.Tables[0].Rows[0]["Respondent_Fax_No"].ToString();
            txtRespondentEmailID.Text = p_Var.dSet.Tables[0].Rows[0]["Respondent_Email"].ToString();

            if (Request.QueryString["rp_id"] == null)
            {
                LblPetitionPRO.Visible = true;
                LblPetitionPRO.Text = string.Empty;
                LblPetitionPRO.Text += Request.QueryString["P_Number"].ToString();
                //+ " Of Year :" + p_Var.dSet.Tables[0].Rows[0]["year"].ToString();
            }
            //txtHearingDate.Text =miscellBL.getDateFormatddMMYYYY( p_Var.dSet.Tables[0].Rows[0]["SOH"].ToString());
            //  ddlPetReviewStatus.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["Petition_Status_Id"].ToString();

            //////if (p_Var.dSet.Tables[0].Rows[0]["File_Name"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString() !="")
            //////{
            //////    LblOldFile.Visible = true;
            //////    lnkFileRemove.Visible = true;
            //////    lblFileName.Visible = true;
            //////    lblFileName.Text = p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString().Remove(0, 3);
            //////    ViewState["filename"] = p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString();
            //////}
            //////else
            //////{
            //////    LblOldFile.Visible = false;
            //////    lnkFileRemove.Visible = false;
            //////    lblFileName.Visible = false;
            //////}
            //////if (p_Var.dSet.Tables[0].Rows[0]["Public_Notice"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["Public_Notice"].ToString() != "")
            //////{
            //////    lblOldPublicNotice.Visible = true;
            //////    lblPublicNotice.Visible = true;
            //////    lnkPublicNoticeRemove.Visible = true;
            //////    lblPublicNotice.Text = p_Var.dSet.Tables[0].Rows[0]["Public_Notice"].ToString().Remove(0,3);
            //////    ViewState["publicNotice"] = p_Var.dSet.Tables[0].Rows[0]["Public_Notice"].ToString();

            //////}
            //////else
            //////{
            //////    lblOldPublicNotice.Visible = false;
            //////    lblPublicNotice.Visible = false;
            //////    lnkPublicNoticeRemove.Visible = false;
            //////}
            //////if (Convert.ToInt16(ddlPetitionStatus_Edit.SelectedValue) != 10)
            //////{
            //////    pPetition.Visible = true;
            //////}
            //////else
            //////{
            //////    pPetition.Visible = false;
            //////}

            //////if (p_Var.dSet.Tables[0].Rows[0]["SOH"] != DBNull.Value)
            //////{
            //////    txtHearingDate_Edit.Text = miscellBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["SOH"].ToString());
            //////}
            //////else
            //////{
            //////    txtHearingDate_Edit.Text = "";
            //////}

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
    public void BindData(int rp_id)
    {
        petitionObject.RPId = rp_id;
        //petitionObject.Status = Convert.ToInt32(Session["Status_Id"]);
        p_Var.dsFileName = petPetitionBL.getFileNameForReviewPetition(petitionObject);
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
            p_Var.Result1 = petPetitionBL.UpdateFileStatusForReviewPetitions(petitionObject);
            if (p_Var.Result1 > 0)
            {
                Label filename = (Label)e.Item.FindControl("lblFile");
                //Label date = (Label)e.Item.FindControl("lblDate");
                Label lblComments = (Label)e.Item.FindControl("lblComments");
                LinkButton RemoveFileLink = (LinkButton)e.Item.FindControl("lnkFileConnectedRemove");
                Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");
                filename.Visible = false;
                //date.Visible = false;
                lblComments.Visible = false;
                RemoveFileLink.Visible = false;
                ltrlDownload.Visible = false;
            }
            bindPetition_Review_IN_Edit_Mode(Convert.ToInt32(Request.QueryString["rp_id"]));
        }
    }

    #region linkButton lnkConnectedPetition click event to display connected petition

    protected void lnkConnectedPetition_Click(object sender, EventArgs e)
    {
        lnkConnectedPetitionNo.Visible = true;
        lnkConnectedPetition.Visible = true;
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
        divConnectAdd.Visible = false;
    }

    #endregion

    #region Function to get Review Petition numbers for ChkBoxConnection

    public void getpetitionNumberForChkBoxConnection()
    {
        // petitionObject.year = ddlYear.SelectedValue;
        p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

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
        p_Var.dSet = petPetitionBL.getPetitionReviewNumberForConnection(petitionObject);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            chklstPetition.DataSource = p_Var.dSet;
            chklstPetition.DataValueField = "RP_Id";
            chklstPetition.DataTextField = "RPNoValue";
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
                    }
                }
                // li.Selected = true;
            }
        }
    }

    #endregion

    #region Function to bind petition Year

    public void bindPetitionYearinDdl()
    {
        p_Var.dSet = petPetitionBL.GetYearPetitionReview_AddEdit();
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

    #region linkButton lnkConnectedPetitionEdit click event to display connected petition

    protected void lnkConnectedPetitionEdit_Click(object sender, EventArgs e)
    {
        lnkConnectedPetitionEditNo.Visible = true;
        lnkConnectedPetitionEdit.Visible = true;
        pnlEditConnectedPetition.Visible = true;
        bindPetitionYearinDdlEdit();//17 jan 2013
        getpetitionNumberForConnectionYesNo();
        divConnectEdit.Visible = true;

    }

    #endregion

    #region linkButton lnkConnectedPetitionEditNo click event

    protected void lnkConnectedPetitionEditNo_Click(object sender, EventArgs e)
    {
        lnkConnectedPetitionEditNo.Visible = false;
        lnkConnectedPetitionEdit.Visible = true;
        pnlEditConnectedPetition.Visible = false;
        //getpetitionNumberForConnectionYesNo();
        getpetitionNumberForChkBoxConnection();
        divConnectEdit.Visible = false;
    }

    #endregion

    #region Function to bind petition Year

    public void bindPetitionYearinDdlEdit()
    {
        p_Var.dSet = petPetitionBL.GetYearPetitionReview_AddEdit();
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

    #region Function to get Petition numbers for connections

    public void getpetitionNumberForConnectionYesNo()
    {
        PetitionOB newOB = new PetitionOB();
        PetitionBL newBL = new PetitionBL();
        newOB.RPId = Convert.ToInt32(Request.QueryString["rp_id"]);

        DataSet dsetYear = new DataSet();
        StringBuilder sbuilderYear = new StringBuilder();
        dsetYear = newBL.getReviewPetitionYearForConnectionEdit(newOB);

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
                // li.Selected = true;
            }
        }
        ////p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

        ////foreach (ListItem li in ddlYear.Items)
        ////{
        ////    if (li.Selected == true)
        ////    {
        ////        p_Var.sbuilder.Append(li.Text).Append(",");
        ////    }

        ////}
        ////List<string> list = new List<string>();

        ////foreach (ListItem li in chklstPetition.Items)
        ////{
        ////    if (li.Selected == true)
        ////    {
        ////        list.Add(li.Value);
        ////    }

        ////}

        ////ViewState["MyList"] = list;
        ////petitionObject.year = p_Var.sbuilder.ToString();
        //////petitionObject.year = ddlYear.SelectedValue;
        ////p_Var.dSet = petPetitionBL.getPetitionReviewNumberForConnection(petitionObject);
        ////if (p_Var.dSet.Tables[0].Rows.Count > 0)
        ////{
        ////    chklstPetitionEdit.DataSource = p_Var.dSet;
        ////    chklstPetitionEdit.DataValueField = "RP_Id";
        ////    chklstPetitionEdit.DataTextField = "RPNoValue";
        ////    chklstPetitionEdit.DataBind();
        ////}
        ////else
        ////{
        ////    chklstPetitionEdit.DataSource = p_Var.dSet;
        ////    chklstPetitionEdit.DataBind();
        ////}

        ////if (ViewState["MyList"] != null)
        ////{
        ////    List<string> list1 = ViewState["MyList"] as List<string>;
        ////    foreach (ListItem li in chklstPetition.Items)
        ////    {
        ////        foreach (string val in list1)
        ////        {
        ////            if (li.Value == val)
        ////            {
        ////                li.Selected = true;
        ////            }
        ////        }
        ////        // li.Selected = true;
        ////    }
        ////}
    }

    #endregion

    #region Function to get Petition numbers for connections in Edit mode

    public void getpetitionNumberForConnectionEdit()
    {
        //petitionObject.year = year;
        p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
        StringBuilder strBuilder = new StringBuilder();
        foreach (ListItem li in ddlYearEdit.Items)
        {
            if (li.Selected == true)
            {
                p_Var.sbuilder.Append(li.Text).Append(",");
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

        ViewState["MyList"] = list;

        petitionObject.year = p_Var.sbuilder.ToString();
        petitionObject.RPId = Convert.ToInt32(Request.QueryString["rp_id"]);
        p_Var.dSet = petPetitionBL.getPetitionReviewNumberForConnectionEdit(petitionObject);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            chklstPetitionEdit.DataSource = p_Var.dSet;
            chklstPetitionEdit.DataValueField = "RP_Id";
            chklstPetitionEdit.DataTextField = "RPNoValue";
            chklstPetitionEdit.DataBind();
        }
        else
        {
            chklstPetitionEdit.DataSource = p_Var.dSet;

            chklstPetitionEdit.DataBind();
        }


        PetitionOB newOB = new PetitionOB();
        DataSet dsnew1 = new DataSet();
        newOB.RPId = Convert.ToInt32(Request.QueryString["rp_id"]);
        newOB.year = p_Var.sbuilder.ToString();
        dsnew1 = petPetitionBL.getConnectedReviewPetitionEditNew(newOB);

        for (p_Var.i = 0; p_Var.i < dsnew1.Tables[0].Rows.Count; p_Var.i++)
        {
            foreach (ListItem li in chklstPetitionEdit.Items)
            {
                if (li.Value.ToString() == dsnew1.Tables[0].Rows[p_Var.i]["Connected_RP_Id"].ToString().Trim())
                {
                    li.Selected = true;
                    strBuilder.Append(li.Value + ";");

                }

            }
        }

        if (ViewState["MyList"] != null)
        {
            List<string> list1 = ViewState["MyList"] as List<string>;
            string[] stringArray = strBuilder.ToString().Split(';');
            list1.AddRange(stringArray);

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
                // li.Selected = true;
            }
        }
        ltrlSelectedEdit.Text = strBuilder.ToString();

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

    protected void chklstPetition_SelectedIndexChanged(object sender, EventArgs e)
    {
        string value = string.Empty;
        StringBuilder strBuilder = new StringBuilder();
        string result = Request.Form["__EVENTTARGET"];

        string[] checkedBox = result.Split('$');
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (chklstPetition.Items[index].Selected)
        {

            petitionObject.RPId = Convert.ToInt16(chklstPetition.Items[index].Value);
            p_Var.dSet = petPetitionBL.get_ConnectedPetitionReview_Edit(petitionObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_Var.i = 0; p_Var.i < p_Var.dSet.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in chklstPetition.Items)
                    {

                        if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["Connected_RP_Id"].ToString())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");
                        }

                    }

                }
            }
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
    protected void chklstPetitionEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        string value = string.Empty;
        StringBuilder strBuilder = new StringBuilder();
        string result = Request.Form["__EVENTTARGET"];

        string[] checkedBox = result.Split('$');
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (chklstPetitionEdit.Items[index].Selected)
        {

            petitionObject.RPId = Convert.ToInt16(chklstPetitionEdit.Items[index].Value);
            p_Var.dSet = petPetitionBL.get_ConnectedPetitionReview_Edit(petitionObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_Var.i = 0; p_Var.i < p_Var.dSet.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in chklstPetitionEdit.Items)
                    {

                        if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["Connected_RP_Id"].ToString())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");
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




    #region Function to get Petition numbers for ChkBoxConnection

    public void getpetitionNumberForChkBoxConnection1()
    {

        //////////////////////p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

        //////////////////////foreach (ListItem li in ddlYear1.Items)
        //////////////////////{
        //////////////////////    if (li.Selected == true)
        //////////////////////    {
        //////////////////////        p_Var.sbuilder.Append(li.Text).Append(",");
        //////////////////////    }

        //////////////////////}
        //////////////////////List<string> list = new List<string>();

        //////////////////////foreach (ListItem li in chklstPetition1.Items)
        //////////////////////{
        //////////////////////    if (li.Selected == true)
        //////////////////////    {
        //////////////////////        list.Add(li.Value);
        //////////////////////    }

        //////////////////////}

        //////////////////////ViewState["MyList"] = list;
        //////////////////////petitionObject.year = p_Var.sbuilder.ToString();

        //////////////////////p_Var.dSet = petPetitionBL.getPetitionNumberForConnection(petitionObject);

        //////////////////////if (p_Var.dSet.Tables[0].Rows.Count > 0)
        //////////////////////{
        //////////////////////    chklstPetition1.DataSource = p_Var.dSet;
        //////////////////////    chklstPetition1.DataValueField = "Petition_id";
        //////////////////////    chklstPetition1.DataTextField = "PRONoValue";
        //////////////////////    chklstPetition1.DataBind();
        //////////////////////}
        //////////////////////else
        //////////////////////{
        //////////////////////    chklstPetition1.DataSource = p_Var.dSet;

        //////////////////////    chklstPetition1.DataBind();
        //////////////////////}

        //////////////////////if (ViewState["MyList"] != null)
        //////////////////////{
        //////////////////////    List<string> list1 = ViewState["MyList"] as List<string>;
        //////////////////////    foreach (ListItem li in chklstPetition1.Items)
        //////////////////////    {
        //////////////////////        foreach (string val in list1)
        //////////////////////        {
        //////////////////////            if (li.Value == val)
        //////////////////////            {
        //////////////////////                li.Selected = true;
        //////////////////////            }
        //////////////////////        }
        //////////////////////        // li.Selected = true;
        //////////////////////    }
        //////////////////////}
    }

    #endregion

    protected void lnkConnectedPetition1_Click(object sender, EventArgs e)
    {

    }
    protected void lnkConnectedPetitionNo1_Click(object sender, EventArgs e)
    {

    }
    protected void chklstPetition1SelectedIndexChangd(object sender, EventArgs e)
    {

    }
    protected void ddlYear1_SelectedIndexChanged(object sender, EventArgs e)
    {
        getpetitionNumberForChkBoxConnection1();
    }



    #region Function to get Petition numbers for connections

    public void getpetitionNumberForConnectionYesNo1()
    {
        //petitionObject.year = ddlYear.SelectedValue;
        //////////////////// PetitionOB newOB = new PetitionOB();
        //////////////////// PetitionBL newBL = new PetitionBL();
        //////////////////// newOB.RPId = Convert.ToInt32(Request.QueryString["rp_id"]);

        //////////////////// DataSet dsetYear = new DataSet();
        //////////////////// StringBuilder sbuilderYear = new StringBuilder();
        //////////////////////// dsetYear = newBL.getPetitionYearForConnectionEdit(newOB);
        //////////////////// dsetYear = newBL.Connected_multiplePetitionReviewForConnectionEdit(newOB);
        //////////////////// if (dsetYear.Tables[0].Rows.Count > 0)
        //////////////////// {
        ////////////////////     for (p_Var.i = 0; p_Var.i < dsetYear.Tables[0].Rows.Count; p_Var.i++)
        ////////////////////     {
        ////////////////////         foreach (ListItem liEdit in ddlYearEdit1.Items)
        ////////////////////         {
        ////////////////////             if (liEdit.Value.ToString() == dsetYear.Tables[0].Rows[p_Var.i]["year"].ToString().Trim())
        ////////////////////             {
        ////////////////////                 liEdit.Selected = true;
        ////////////////////                 sbuilderYear.Append(liEdit.Text).Append(",");

        ////////////////////             }

        ////////////////////         }
        ////////////////////     }
        //////////////////// }
        //////////////////// List<string> list = new List<string>();

        //////////////////// foreach (ListItem li in chklstPetition.Items)
        //////////////////// {
        ////////////////////     if (li.Selected == true)
        ////////////////////     {
        ////////////////////         list.Add(li.Value);
        ////////////////////     }

        //////////////////// }

        //////////////////// ViewState["MyList"] = list;

        //////////////////// petitionObject.year = sbuilderYear.ToString();

        //////////////////// ViewState["year"] = sbuilderYear.ToString();
        //////////////////// getpetitionNumberForConnectionEdit();



        //////////////////// if (ViewState["MyList"] != null)
        //////////////////// {
        ////////////////////     List<string> list1 = ViewState["MyList"] as List<string>;
        ////////////////////     foreach (ListItem li in chklstPetitionEdit1.Items)
        ////////////////////     {
        ////////////////////         foreach (string val in list1)
        ////////////////////         {
        ////////////////////             if (li.Value == val)
        ////////////////////             {
        ////////////////////                 li.Selected = true;
        ////////////////////             }
        ////////////////////         }
        ////////////////////         // li.Selected = true;
        ////////////////////     }
        //////////////////// }
    }

    #endregion

    protected void lnkConnectedPetitionEdit1_Click(object sender, EventArgs e)
    {


    }
    protected void lnkConnectedPetitionEditNo1_Click(object sender, EventArgs e)
    {

    }
    protected void ddlYearEdit1_SelectedIndexChanged(object sender, EventArgs e)
    {
        getpetitionNumberForConnectionEdit1();
    }
    protected void chklstPetitionEdit1_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    #region Function to get Petition numbers for connections in Edit mode

    public void getpetitionNumberForConnectionEdit1()
    {
        // petitionObject.year = ddlYear.SelectedValue;
        //////////////////////// p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

        //////////////////////// foreach (ListItem li in ddlYearEdit1.Items)
        //////////////////////// {
        ////////////////////////     if (li.Selected == true)
        ////////////////////////     {
        ////////////////////////         p_Var.sbuilder.Append(li.Text).Append(",");
        ////////////////////////     }

        //////////////////////// }
        //////////////////////// List<string> list = new List<string>();

        //////////////////////// foreach (ListItem li in chklstPetitionEdit1.Items)
        //////////////////////// {
        ////////////////////////     if (li.Selected == true)
        ////////////////////////     {
        ////////////////////////         list.Add(li.Value);
        ////////////////////////     }

        //////////////////////// }

        //////////////////////// ViewState["MyList"] = list;


        //////////////////////// petitionObject.year = p_Var.sbuilder.ToString();
        //////////////////////// petitionObject.RPId = Convert.ToInt16(Request.QueryString["rp_id"]);
        //////////////////////// ////p_Var.dSet = petPetitionBL.getPetitionNumberForConnectionEdit(petitionObject);
        //////////////////////// p_Var.dSet = petPetitionBL.getPetitionReviewForConnectionEdit(petitionObject);

        //////////////////////// if (p_Var.dSet.Tables[0].Rows.Count > 0)
        //////////////////////// {
        ////////////////////////     chklstPetitionEdit1.DataSource = p_Var.dSet;
        ////////////////////////     chklstPetitionEdit1.DataValueField = "Petition_id";
        ////////////////////////     chklstPetitionEdit1.DataTextField = "PRONoValue";
        ////////////////////////     chklstPetitionEdit1.DataBind();
        //////////////////////// }
        //////////////////////// else
        //////////////////////// {
        ////////////////////////     chklstPetitionEdit1.DataSource = p_Var.dSet;

        ////////////////////////     chklstPetitionEdit1.DataBind();
        //////////////////////// }

        //////////////////////// PetitionOB newOB = new PetitionOB();
        //////////////////////// DataSet dsnew1 = new DataSet();
        //////////////////////// newOB.RPId = Convert.ToInt16(Request.QueryString["rp_id"]);
        //////////////////////// newOB.year = p_Var.sbuilder.ToString();
        ////////////////////////// dsnew1 = petPetitionBL.getConnectedPetitionEditNew(newOB);
        //////////////////////// dsnew1 = petPetitionBL.getConnectedPetitionReviewEditNew(newOB);

        //////////////////////// for (p_Var.i = 0; p_Var.i < dsnew1.Tables[0].Rows.Count; p_Var.i++)
        //////////////////////// {
        ////////////////////////     foreach (ListItem li in chklstPetitionEdit1.Items)
        ////////////////////////     {
        ////////////////////////         if (li.Value.ToString() == dsnew1.Tables[0].Rows[p_Var.i]["Connected_Petition_id"].ToString().Trim())
        ////////////////////////         {
        ////////////////////////             li.Selected = true;


        ////////////////////////         }

        ////////////////////////     }
        //////////////////////// }

        //////////////////////// if (ViewState["MyList"] != null)
        //////////////////////// {
        ////////////////////////     List<string> list1 = ViewState["MyList"] as List<string>;
        ////////////////////////     foreach (ListItem li in chklstPetitionEdit1.Items)
        ////////////////////////     {
        ////////////////////////         foreach (string val in list1)
        ////////////////////////         {
        ////////////////////////             if (li.Value == val)
        ////////////////////////             {
        ////////////////////////                 li.Selected = true;
        ////////////////////////             }
        ////////////////////////         }
        ////////////////////////         // li.Selected = true;
        ////////////////////////     }
        //////////////////////// }
    }

    #endregion


    #region Function to connected order Click

    protected void lnkConnectedOrder_Click(object sender, EventArgs e)
    {
        pnlConnectedOrder.Visible = true;
        lnkConnectedOrderNo.Visible = true;
    }
    protected void lnkConnectedOrderNo_Click(object sender, EventArgs e)
    {
        pnlConnectedOrder.Visible = false;
        lnkConnectedOrderNo.Visible = false;
        PConnectedYear.Visible = false;
        PConnectedOrderId.Visible = false;
        rdioConnectedOrder.ClearSelection();

    }

    #endregion
    protected void rdioConnectedOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetYearConnectedOrder();
        GetConnectedOrderId();
    }
    protected void drpConnectedOrderYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetConnectedOrderId();
    }



    public void GetYearConnectedOrder()
    {
        petitionObject.OrderTypeID = Convert.ToInt32(rdioConnectedOrder.SelectedValue);
        p_Var.dSetCompare = ObjOrderBL.get_OrderYearForPetition(petitionObject);
        if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
        {
            PConnectedYear.Visible = true;

            drpConnectedOrderYear.DataSource = p_Var.dSetCompare;
            drpConnectedOrderYear.DataTextField = "year";
            drpConnectedOrderYear.DataValueField = "year";
            drpConnectedOrderYear.DataBind();
        }
        else
        {
            //PconnectedOrder.Visible = false;
            drpConnectedOrderYear.Items.Add(new ListItem("Select", "0"));


        }

    }


    public void GetYearConnectedOrderEdit(int OrderTypeId)
    {
        petitionObject.OrderTypeID = OrderTypeId;
        p_Var.dSetCompare = ObjOrderBL.get_OrderYearForPetition(petitionObject);
        if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
        {
            PConnectedYearEdit.Visible = true;

            drpConnectedOrderYearEdit.DataSource = p_Var.dSetCompare;
            drpConnectedOrderYearEdit.DataTextField = "year";
            drpConnectedOrderYearEdit.DataValueField = "year";
            drpConnectedOrderYearEdit.DataBind();
        }
        else
        {
            drpConnectedOrderYearEdit.Items.Add(new ListItem("Select", "0"));
        }

    }


    public void GetConnectedOrderId()
    {
        petitionObject.OrderTypeID = Convert.ToInt32(rdioConnectedOrder.SelectedValue);
        petitionObject.year = drpConnectedOrderYear.SelectedValue;
        p_Var.dsFileName = ObjOrderBL.get_OrderIdForPetitionYearWise(petitionObject);
        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
        {
            PConnectedOrderId.Visible = true;

            drpConnectedOrderId.DataSource = p_Var.dsFileName;
            drpConnectedOrderId.DataTextField = "OrderDesc";
            drpConnectedOrderId.DataValueField = "OrderID";

            drpConnectedOrderId.DataBind();

            for (int i = 0; i < drpConnectedOrderId.Items.Count; i++)
            {
                drpConnectedOrderId.Items[i].Attributes.Add("title", p_Var.dsFileName.Tables[0].Rows[i]["OrderFullDesc"].ToString());
                //drpConnectedOrderId.ToolTip= p_Var.dsFileName.Tables[0].Rows[i]["OrderDesc"].ToString());
            }
        }
        else
        {
            drpConnectedOrderId.Items.Add(new ListItem("Select", "0"));


        }

    }


    public void GetConnectedOrderIdEdit(int OrderTypeID, string Year)
    {
        petitionObject.OrderTypeID = OrderTypeID;
        petitionObject.year = Year;
        p_Var.dSetCompare = ObjOrderBL.get_OrderIdForPetitionYearWise(petitionObject);
        if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
        {
            PConnectedOrderIdEdit.Visible = true;

            drpConnectedOrderIdEdit.DataSource = p_Var.dSetCompare;
            drpConnectedOrderIdEdit.DataTextField = "OrderDesc";
            drpConnectedOrderIdEdit.DataValueField = "OrderID";
            drpConnectedOrderIdEdit.DataBind();

            for (int i = 0; i < drpConnectedOrderIdEdit.Items.Count; i++)
            {
                drpConnectedOrderIdEdit.Items[i].Attributes.Add("title", p_Var.dSetCompare.Tables[0].Rows[i]["OrderFullDesc"].ToString());

            }

        }
        else
        {
            //PconnectedOrder.Visible = false;
            drpConnectedOrderIdEdit.Items.Add(new ListItem("Select", "0"));


        }

    }

    protected void lnkConnectedOrderEdit_Click(object sender, EventArgs e)
    {
        pnlConnectedOrderEdit.Visible = true;
        lnkConnectedOrderEditNo.Visible = true;

        petitionObject.ReView = Convert.ToInt32(Request.QueryString["rp_id"]);
        p_Var.dsFileName = ObjOrderBL.GetReviewPetitionConnectedOrder(petitionObject);
        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
        {

            GetYearConnectedOrderEdit(Convert.ToInt32(p_Var.dsFileName.Tables[0].Rows[0]["OrderTypeId"]));
            GetConnectedOrderIdEdit(Convert.ToInt32(p_Var.dsFileName.Tables[0].Rows[0]["OrderTypeId"]), p_Var.dsFileName.Tables[0].Rows[0]["Year"].ToString());
            drpConnectedOrderYearEdit.SelectedValue = p_Var.dsFileName.Tables[0].Rows[0]["Year"].ToString();
            rdioConnectedOrderEdit.SelectedValue = p_Var.dsFileName.Tables[0].Rows[0]["OrderTypeId"].ToString();
            drpConnectedOrderIdEdit.SelectedValue = p_Var.dsFileName.Tables[0].Rows[0]["orderid"].ToString();
        }

    }
    protected void lnkConnectedOrderEditNo_Click(object sender, EventArgs e)
    {
        pnlConnectedOrderEdit.Visible = false;
        lnkConnectedOrderEditNo.Visible = false;
        PConnectedYearEdit.Visible = false;
        PConnectedOrderIdEdit.Visible = false;
        //rdioConnectedOrderEdit.ClearSelection();


    }
    protected void rdioConnectedOrderEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetYearConnectedOrderEdit(Convert.ToInt32(rdioConnectedOrderEdit.SelectedValue));
        GetConnectedOrderIdEdit(Convert.ToInt32(rdioConnectedOrderEdit.SelectedValue), drpConnectedOrderYearEdit.SelectedValue.ToString());
    }
    protected void drpConnectedOrderYearEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetConnectedOrderIdEdit(Convert.ToInt32(rdioConnectedOrderEdit.SelectedValue), drpConnectedOrderYearEdit.SelectedValue.ToString());
    }
    protected void drpConnectedOrderIdEdit_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void datalistFileName_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

            Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");
            Label filename = (Label)e.Item.FindControl("lblFile");
            var link = e.Item.FindControl("lnkFileConnectedRemove") as LinkButton;
            var hiddenField = e.Item.FindControl("hiddenFieldRpID") as HiddenField;
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
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["rp_Id"]))
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
                        // link.Visible = true;//if user has right then enabled else disabled
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["rp_Id"]))
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

            p_Var.sbuilder.Append("<a href='" + ResolveUrl(p_Var.url) + filename.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='Download File' width='15' alt='Download File' height=\"15\" /> " + "</a>");
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

    protected void cus_filePublicNotice_Edit_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }
}
