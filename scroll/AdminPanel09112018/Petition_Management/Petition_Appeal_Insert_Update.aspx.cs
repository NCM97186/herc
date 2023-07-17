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
using System.Text.RegularExpressions;

public partial class Auth_AdminPanel_Petition_Management_Petition_Appeal_Insert_Update : CrsfBase //System.Web.UI.Page
{
    #region Data declaration zone

    PetitionOB petitionObject = new PetitionOB();
    PetitionBL petPetitionBL = new PetitionBL();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    UserOB obj_userOB = new UserOB();
    AppealBL objAppealBL = new AppealBL();
    OrderBL ObjOrderBL = new OrderBL();
    #endregion

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



            if (Request.QueryString["pa_id"] != null)    // for Edit
            {
                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Edit  Appeal against Order Management";
                this.Page.Title = "Edit Appeal against Order Management: HERC";
                displayMetaLang1();
                BindData(Convert.ToInt32(Request.QueryString["pa_id"]));
                pnlAppealEdit.Visible = true;
                pnlAppealAdd.Visible = false;
               
                
                bindropDownlistLang();    //Display the Language according to privilage
                bindDdlPetition_review_Status_Edit(); //Display the petition stattus in dropdownlist
                LblPetitionPA.Visible = true;

                bindPetitionYearinDdlEdit1();


                bindPetition_Appeal_IN_Edit_Mode(Convert.ToInt32(Request.QueryString["pa_id"]));
            }
            else
            {
                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Add  Appeal against Order Management";
                this.Page.Title = "Add Appeal against Order Management: HERC";

                pnlAppealEdit.Visible = false;
                pnlAppealAdd.Visible = true;
              
                pJudgementLink.Visible = false;
                PJudgementDescription.Visible = false;
             
                pPetition_Appeal.Visible = false;
                bindropDownlistLang(); // Get the Language privilage
                bind_Reviewer_Details(Convert.ToInt32(Request.QueryString["RP_Id"]));
            }
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }

      }
    }

    #endregion

    //Area for all buttons, linkbuttons, imagebuttons click events

    #region button btnAddAppeal click event to add appeal

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
                    try
                    {
                        petitionObject.ActionType = 1;
                        petitionObject.AppealNo = HttpUtility.HtmlEncode(txtAppeal_no.Text);

                        petitionObject.WhereAppealed = HttpUtility.HtmlEncode(txtAppeal_Address.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.PAStatusId = Convert.ToInt16(ddlAppealStatus.SelectedValue);
                        petitionObject.JudgementLink = HttpUtility.HtmlEncode(txtAppealLink.Text);
                        petitionObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                        petitionObject.LangId = Convert.ToInt16(ddlLanguage.SelectedValue);
                        DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
                        petitionObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
                        petitionObject.AppealDate = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
                        petitionObject.Remarks = HttpUtility.HtmlEncode(txtRemarks.Text.Replace(Environment.NewLine, "<br />"));

                        petitionObject.MetaDescription = txtMetaDescription.Text;
                        petitionObject.MetaKeyWords = txtMetaKeyword.Text;
                        petitionObject.MetaTitle = txtMetaTitle.Text;
                        petitionObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;

                        petitionObject.RefNo = txtRefNo.Text;

                        if (txtAppealSubject.Text != null && txtAppealSubject.Text != "")
                        {
                            petitionObject.Description = txtAppealSubject.Text;
                        }
                        else
                        {
                            petitionObject.Description = null;
                        }
                        //31 Aug 2013

                        petitionObject.ApplicantName = HttpUtility.HtmlEncode(txtApplicantName.Text.Replace(Environment.NewLine, "<br />"));


                        petitionObject.PetitionerAddress = HttpUtility.HtmlEncode(txtPetitionerAdd.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.RespondentAddress = HttpUtility.HtmlEncode(txtRespondentAdd.Text.Replace(Environment.NewLine, "<br />"));

                        petitionObject.Respondentmail = HttpUtility.HtmlEncode(txtRespondentEmailID.Text);

                        if (txtRespondentFaxNo.Text != null && txtRespondentFaxNo.Text != "")
                        {
                            petitionObject.RespondentFaxNo = HttpUtility.HtmlEncode(txtRespondentFaxNo.Text);
                        }
                        else
                        {
                            petitionObject.RespondentFaxNo = null;
                        }


                        if (txtRespondentMobileNo.Text != null && txtRespondentMobileNo.Text != "")
                        {
                            petitionObject.RespondentMobileNo = HttpUtility.HtmlEncode(txtRespondentMobileNo.Text);
                        }
                        else
                        {
                            petitionObject.RespondentMobileNo = null;
                        }

                        if (txtRespondentPhoneNo.Text != null && txtRespondentPhoneNo.Text != "")
                        {
                            petitionObject.RespondentPhone_No = HttpUtility.HtmlEncode(txtRespondentPhoneNo.Text);
                        }
                        else
                        {
                            petitionObject.RespondentPhone_No = null;
                        }
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
                        petitionObject.ApplicantEmail = HttpUtility.HtmlEncode(txtApplicantEmailID.Text);
                        petitionObject.ResPondent = HttpUtility.HtmlEncode(txtRespondentName.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.subject = HttpUtility.HtmlEncode(txtPetReviewSubject.Text.Replace(Environment.NewLine, "<br />"));
                        //End

                        petitionObject.InsertedBy = Convert.ToInt16(Session["User_Id"]);
                        petitionObject.RPNo = Convert.ToString(Request.QueryString["rp_Number"]);
                        petitionObject.RPId = Convert.ToInt32(Request.QueryString["RP_Id"]);
                        petitionObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = petPetitionBL.insert_Appeal_Petition(petitionObject);
                        if (p_Var.Result > 0)
                        {

                            //This is for insert appeal review petition

                            foreach (ListItem li1 in chklstPetition1.Items)
                            {
                                if (li1.Selected == true)
                                {
                                    petitionObject.AppealId = p_Var.Result;
                                    petitionObject.ConnectedPetitionID = Convert.ToInt16(li1.Value.ToString());
                                    int cnt = li1.Text.LastIndexOf(' ');
                                    petitionObject.year = li1.Text.Substring(cnt).Trim();
                                    int Result = objAppealBL.insertConnectedMultipleReviewAppeal(petitionObject);

                                }
                            }



                            //28 Aug 2013 Connected Order with Appeal petition

                            if (rdioConnectedOrder.SelectedValue != "" && rdioConnectedOrder.SelectedValue != null)
                            {
                                petitionObject.year = drpConnectedOrderYear.SelectedValue;
                                petitionObject.OrderTypeID = Convert.ToInt32(rdioConnectedOrder.SelectedValue);
                                petitionObject.OrderID = Convert.ToInt32(drpConnectedOrderId.SelectedValue);
                                petitionObject.AppealId = p_Var.Result;
                                int connectedOrder = ObjOrderBL.InsertIntoAppealPetitionConnectedOrder(petitionObject);
                            }


                            //End



                            string fileMultiple = string.Empty;
                            HttpFileCollection hfc = Request.Files;

                            for (int i = 0; i < hfc.Count; i++)
                            {
                                HttpPostedFile hpf = hfc[i];

                                fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
                                if (fileMultiple != null && fileMultiple != "")
                                {

                                    PetitionOB newObj = new PetitionOB();
                                    newObj.AppealId = p_Var.Result;
                                    newObj.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                                    newObj.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);
                                    newObj.PetitionDate = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
                                    if (i == 0)
                                    {
                                        newObj.Description = txtRemarksAppeal.Text;
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
                                        //newObj.PetitionFile = "P_" + p_Var.Filename;
                                        newObj.PetitionFile = p_Var.Filename;
                                    }
                                    int Result2 = petPetitionBL.insertConnectedAppealFiles(newObj);
                                }
                            }
                            // Session["msg"] = "Petition's appeal has been submitted successfully.";
                            Session["msg"] = "Appeal record has been submitted successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                        }
                    }
                    catch
                    {

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

    #region button btnReset click event to reset previous values

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtAppeal_Address.Text = "";
        txtAppeal_no.Text = "";
        txtAppealLink.Text = "";
        txtAppealSubject.Text = "";
        txtApplicantEmailID.Text = "";
        txtApplicantFaxNo.Text = "";
        txtApplicantMobileNo.Text = "";
        txtApplicantName.Text = "";
        txtApplicantPhoneNo.Text = "";
        txtPetitionDate.Text = "";
        txtPetitionerAdd.Text = "";
        txtRefEdit.Text = "";
        txtRefNo.Text = "";
        txtRemarks.Text = "";
        txtRemarksAppeal.Text = "";
        txtRespondentAdd.Text = "";
        txtRespondentEmailID.Text = "";
        txtRespondentFaxNo.Text = "";
        txtRespondentMobileNo.Text = "";
        txtRespondentName.Text = "";
        txtRespondentPhoneNo.Text = "";
        txtPetReviewSubject.Text = "";
    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
         Response.Redirect(ResolveUrl("~/auth/adminpanel/Petition_Management/") + "Display_Petition_Appeal.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
    }

    #endregion

    #region button btnUpdateAppeal click event to update appeal 

    protected void btnUpdateAppeal_Click(object sender, EventArgs e)
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
                        petitionObject.ActionType = 2;
                        petitionObject.StatusId = Convert.ToInt16(Session["Status_Id"]);
                        petitionObject.TempPAId = Convert.ToInt32(Request.QueryString["pa_id"]);

                        petitionObject.OldPAId = Convert.ToInt32(Request.QueryString["pa_id"]);

                        petitionObject.Remarks = HttpUtility.HtmlEncode(txtRemarksEdit.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.PAStatusId = Convert.ToInt16(ddlAppealStatus_Edit.SelectedValue);
                        petitionObject.LangId = Convert.ToInt16(Request.QueryString["LangID"]);
                        petitionObject.AppealNo = HttpUtility.HtmlEncode(txtAppeal_no_Edit.Text);
                        if (ddlAppealStatus_Edit.SelectedValue == "14")
                        {
                            petitionObject.JudgementLink = txtjudgementLink_Edit.Text;
                        }
                        else
                        {
                            petitionObject.JudgementLink = null;
                        }
                        if (ddlAppealStatus_Edit.SelectedValue == "14")
                        {
                            petitionObject.Description = HttpUtility.HtmlEncode(txtJudgementDesc_Edit.Text);
                        }
                        else
                        {
                            petitionObject.Description = null;
                        }

                        petitionObject.WhereAppealed = HttpUtility.HtmlEncode(txtAppeal_Address_Edit.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.appeal = 0;
                        petitionObject.RPId = Convert.ToInt32(ViewState["RP_ID"]);
                        DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate_Edit.Text.ToString()));
                        petitionObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
                        petitionObject.AppealDate = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate_Edit.Text.ToString()));
                        petitionObject.RefNo = txtRefEdit.Text;

                        // 31 Aug 2013 

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
                        petitionObject.IpAddress = Miscelleneous_DL.getclientIP();
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
                        petitionObject.ApplicantEmail = HttpUtility.HtmlEncode(txtApplicantEmailID_Edit.Text);
                        petitionObject.ResPondent = HttpUtility.HtmlEncode(txtRespondentName_Edit.Text.Replace(Environment.NewLine, "<br />"));
                        petitionObject.subject = HttpUtility.HtmlEncode(txtApplicantSubject_Edit.Text.Replace(Environment.NewLine, "<br />"));

                        petitionObject.LastUpdatedBy = Convert.ToInt16(Session["User_Id"]);
                        petitionObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = petPetitionBL.insert_Appeal_Petition(petitionObject);
                        if (p_Var.Result > 0)
                        {


                            petitionObject.AppealId = p_Var.Result;
                            int rslt = objAppealBL.deleteConnectedReviewAppealMultiple(petitionObject);

                            foreach (ListItem li1 in chklstPetitionEdit1.Items)
                            {
                                if (li1.Selected == true)
                                {
                                    petitionObject.AppealId = p_Var.Result;
                                    petitionObject.ConnectedPetitionID = Convert.ToInt16(li1.Value.ToString());

                                    int cnt = li1.Text.LastIndexOf(' ');
                                    petitionObject.year = li1.Text.Substring(cnt).Trim();
                                    //if (lnkConnectedPetitionEditNo1.Visible != false)
                                    //{
                                    //    int Result = objAppealBL.insertConnectedMultipleReviewAppeal(petitionObject);
                                    //}

                                }
                            }



                            // 6 Sep 2013 Connected Order with Appeal petition

                            int connectedorderReview = ObjOrderBL.deleteConnectedOrderAppealPetition(petitionObject);
                            if (lnkConnectedOrderEditNo.Visible == true)
                            {
                                if (rdioConnectedOrderEdit.SelectedValue != "" && rdioConnectedOrderEdit.SelectedValue != null)
                                {
                                    petitionObject.year = drpConnectedOrderYearEdit.SelectedValue;
                                    petitionObject.OrderTypeID = Convert.ToInt32(rdioConnectedOrderEdit.SelectedValue);
                                    petitionObject.OrderID = Convert.ToInt32(drpConnectedOrderIdEdit.SelectedValue);
                                    petitionObject.AppealId = p_Var.Result;
                                    int connectedOrder = ObjOrderBL.InsertIntoAppealPetitionConnectedOrder(petitionObject);
                                }
                            }


                            //End


                            string fileMultiple = string.Empty;
                            HttpFileCollection hfc = Request.Files;

                            for (int i = 0; i < hfc.Count; i++)
                            {
                                HttpPostedFile hpf = hfc[i];

                                fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
                                if (fileMultiple != null && fileMultiple != "")
                                {

                                    PetitionOB newObj = new PetitionOB();
                                    newObj.AppealId = p_Var.Result;
                                    newObj.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                                    newObj.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);
                                    newObj.PetitionDate = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate_Edit.Text.ToString()));
                                    if (i == 0)
                                    {
                                        newObj.Description = TextBox2.Text;
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
                                    int Result2 = petPetitionBL.insertConnectedAppealFiles(newObj);
                                }
                            }
                            Session["msg"] = "Appeal record has been updated successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
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

    #region button btnReset_Edit click event to reset previous value

    protected void btnReset_Edit_Click(object sender, EventArgs e)
    {
        TextBox2.Text = "";
        bindPetition_Appeal_IN_Edit_Mode(Convert.ToInt32(Request.QueryString["pa_id"]));
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

    //End

    //Area for all user defined functions

    #region Function to bind language in dropDownlist according to permission

    public void bindropDownlistLang()
    {
        try
        {

            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            p_Var.dSet = miscellBL.getLanguagePermission(obj_userOB);
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
                p_Var.dSet = miscellBL.getLanguage(usrObject);
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

    #region Function to bind Petition status in dropDownlist

    ////////public void bindDdlPetitionStatus()
    ////////{
    ////////    try
    ////////    {

    ////////        petitionObject.PetitionStatusId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
    ////////        p_Var.dSet = miscellBL.getStatusAccordingtoModule(petitionObject);
    ////////        ddlAppealStatus.DataSource = p_Var.dSet;
    ////////        ddlAppealStatus.DataTextField = "Status";
    ////////        ddlAppealStatus.DataValueField = "Status_Id";
    ////////        ddlAppealStatus.DataBind();
    ////////        ddlAppealStatus.Items.Insert(0, new ListItem("Select Status", "0"));
    ////////    }
    ////////    catch
    ////////    {
    ////////        throw;
    ////////    }
    ////////}

    #endregion

    #region Function to bind Petition status in dropDownlist

    public void bindDdlPetition_review_Status_Edit()
    {
        //Miscelleneous_BL miscellBL=new Miscelleneous_BL();
        try
        {

            petitionObject.PetitionStatusId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
            p_Var.dsFileName = miscellBL.getStatusForPetitionAppeal(petitionObject);
            ddlAppealStatus_Edit.DataSource = p_Var.dsFileName;
            ddlAppealStatus_Edit.DataTextField = "Status";
            ddlAppealStatus_Edit.DataValueField = "Status_Id";
            ddlAppealStatus_Edit.DataBind();
            ddlAppealStatus_Edit.Items.Insert(0, new ListItem("Select Status", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind petition appeal in edit mode

    public void bindPetition_Appeal_IN_Edit_Mode(int pa_ID)
    {
        try
        {
            PLanguage.Visible = false;
     
            

            petitionObject.TempPAId = Convert.ToInt32(pa_ID);
            petitionObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = petPetitionBL.get_Temp_Appeal_Petition_RecordsEdit(petitionObject);
            txtAppeal_no_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Appeal_No"].ToString();
            txtRefEdit.Text = p_Var.dSet.Tables[0].Rows[0]["AppealRefNo"].ToString();
            txtRemarksEdit.Text = p_Var.dSet.Tables[0].Rows[0]["Remarks"].ToString().ToString().Replace("&lt;br /&gt;", Environment.NewLine);
            txtAppeal_Address_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Where_Appealed"].ToString().Replace("&lt;br /&gt;", Environment.NewLine);
            LblPetitionPA.Visible = true;
            LblPetitionPA.Text += p_Var.dSet.Tables[0].Rows[0]["PRO_NO"].ToString()+ ", HERC/RA-" +p_Var.dSet.Tables[0].Rows[0]["RP_NO"].ToString() + " Of Year :" + p_Var.dSet.Tables[0].Rows[0]["rpYear"].ToString();

            txtMetaDescriptionEdit.Text = p_Var.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            txtMetaTitleEdit.Text = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
            txtMetaKeywordEdit.Text = p_Var.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
            DropDownList1.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();

            ddlAppealStatus_Edit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["PA_Status_Id"].ToString();
            txtPetitionDate_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["AppealDate"].ToString();

            // 31 Aug 2013

            txtApplicantName_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Applicant_Name"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtApplicantMobileNo_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Mobile_No"].ToString();
            txtApplicantFaxNo_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Fax_No"].ToString();
            txtApplicantPhoneNo_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Phone_No"].ToString();
            txtApplicantEmailID_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Email"].ToString();
            txtRespondentName_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtApplicantSubject_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Subject"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));

            txtPetitionerAddr_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtRespondentAddr_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));

            txtRespondentMobileNo_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Mobile_No"].ToString());
            txtRespondentPhoneNo_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Phone_No"].ToString());
            txtRespondentFaxNo_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Fax_No"].ToString());
            txtRespondentEmailID_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Email"].ToString());

            //END
            if (p_Var.dSet.Tables[0].Rows[0]["RP_Id"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["RP_Id"].ToString() != "")
            {
                ViewState["RP_ID"] = p_Var.dSet.Tables[0].Rows[0]["RP_Id"];
            }
            else
            {
                ViewState["RP_ID"] = null;
            }
           
            if (Convert.ToInt16(ddlAppealStatus_Edit.SelectedValue) == 14)
            {
                //pPetitionAppeal_Edit.Visible = true;
                pJudgement_Edit.Visible = true;
                pJudgementDesc_Edit.Visible = true;
                txtjudgementLink_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Judgement_Link"].ToString();
                txtJudgementDesc_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["JudgementDesc"].ToString();
            }
            else
            {
                //pPetitionAppeal_Edit.Visible = false;
                pJudgementDesc_Edit.Visible = false;
                pJudgement_Edit.Visible = false;    
            }

            //Today 2 aug 2013

            PetitionOB newOBNew = new PetitionOB();
        
            newOBNew.AppealId = Convert.ToInt32(pa_ID);
            p_Var.dSetCompare = objAppealBL.get_ConnectMultipleReviewAppeal_Edit(newOBNew);
            DataSet dsetYearNew = new DataSet();
            StringBuilder sbuilderYearNew = new StringBuilder();
            dsetYearNew = objAppealBL.Connected_multipleReviewAppealForConnectionEdit(newOBNew);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {
                // getOrdersNumberForChkBoxConnection();
                for (p_Var.j = 0; p_Var.j < dsetYearNew.Tables[0].Rows.Count; p_Var.j++)
                {
                    foreach (ListItem liEdit1 in ddlYearEdit1.Items)
                    {
                        if (liEdit1.Value.ToString() == dsetYearNew.Tables[0].Rows[p_Var.j]["year"].ToString().Trim())
                        {
                            liEdit1.Selected = true;
                            sbuilderYearNew.Append(liEdit1.Text).Append(",");

                        }

                    }
                }


                newOBNew.year = sbuilderYearNew.ToString();
                getpetitionNumberForConnectionEdit1();


                for (p_Var.j = 0; p_Var.j < p_Var.dSetCompare.Tables[0].Rows.Count; p_Var.j++)
                {
                    foreach (ListItem liEdit1 in chklstPetitionEdit1.Items)
                    {
                        if (liEdit1.Value.ToString() == p_Var.dSetCompare.Tables[0].Rows[p_Var.j]["Connected_Review_id"].ToString())
                        {
                            liEdit1.Selected = true;

                        }

                    }
                }

                Panel1.Visible = true;
                pnlEditConnectedPetition1.Visible = true;
               // lnkConnectedPetitionEdit1.Visible = true;
               // lnkConnectedPetitionEditNo1.Visible = true;
                divConnectEdit1.Visible = true;
            }
            else
            {
                Panel1.Visible = false;
                pnlEditConnectedPetition1.Visible = false;
               // lnkConnectedPetitionEdit1.Visible = true;
               // lnkConnectedPetitionEditNo1.Visible = false;
                divConnectEdit1.Visible = false;
            }


            //End




            //6 SEP 2013 Connected Order with review petition

            petitionObject.AppealId = Convert.ToInt32(pa_ID);
            p_Var.dsFileName = ObjOrderBL.GeAppealPetitionConnectedOrder(petitionObject);
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
                drpConnectedOrderIdEdit.SelectedValue =p_Var.dsFileName.Tables[0].Rows[0]["orderid"].ToString();
            }

            //End


        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind Petitioner's details during filling review of petition

    public void bind_Reviewer_Details(int Reviewer_ID)
    {
        try
        {
            PLanguage.Visible = false;
          //  petitionObject.TempRPId = Convert.ToInt32(Reviewer_ID);
            petitionObject.RPId = Convert.ToInt32(Reviewer_ID);
            petitionObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = petPetitionBL.GetPetitionReviewPetitionNumbers(petitionObject);
          
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                //ddlAppealStatus.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["PA_Status_Id"].ToString();
                //if (Request.QueryString["pa_id"] == null)    // for Edit
                //{
                    LblPetitionPA.Visible = true;
                    //LblPetitionPA.Text += Request.QueryString["RP_Number"].ToString()+ p_Var.dSet.Tables[0].Rows[0]["rpYear"].ToString();
                    LblPetitionPA.Text += p_Var.dSet.Tables[0].Rows[0]["PRO_NO"].ToString() + ", HERC/RA-" + p_Var.dSet.Tables[0].Rows[0]["RP_NO"].ToString() + " of " + p_Var.dSet.Tables[0].Rows[0]["rpYear"].ToString();
               // }
            }
                     
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    //Area for all the custom validators to validate server side validation 

    #region custom validator cusAppealNumber to validate appeal number (during addition)

    protected void cusAppealNumber_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
             p_Var.dSet = null;
            petitionObject.AppealNo = HttpUtility.HtmlEncode(txtAppeal_no.Text);
            DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
            petitionObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
            p_Var.dSet = petPetitionBL.getAppeal_Number(petitionObject);
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

    #region custom validator cvFileReviewPetition to validate appeal petition (during addition)

    protected void cvFileReviewPetition_ServerValidate(object source, ServerValidateEventArgs args)
    {
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
        bool strem = true; ;
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

        }
    }

    #endregion

    #region custom validator cusAppealNumber  to validate appeal number (during updation)

    protected void cusAppealNumber_Edit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            p_Var.dSet = null;
            petitionObject.AppealNo = HttpUtility.HtmlEncode(txtAppeal_no_Edit.Text);

            DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate_Edit.Text.ToString()));
            petitionObject.year = date.Year.ToString();

            petitionObject.TempPAId = Convert.ToInt32(Request.QueryString["PA_Id"]);
            p_Var.dSet = petPetitionBL.getAppeal_Number_In_EditMode(petitionObject);
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

    #region custom validator cus_appeal_edit to validate appeal petition (during updation)

    protected void cus_Appeal_Edit_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }

    #endregion

    //End

    //Area for all the dropDownlist events

    #region dropDownlist ddlAppealStatus selectedIndexChanged event zone

    //////////protected void ddlAppealStatus_SelectedIndexChanged(object sender, EventArgs e)
    //////////{
    //////////    if (Convert.ToInt16(ddlAppealStatus.SelectedValue) != 0)
    //////////    {
    //////////        if (Convert.ToInt16(ddlAppealStatus.SelectedValue) == 14)
    //////////        {
    //////////            pPetition_Appeal.Visible = true;
    //////////            pJudgementLink.Visible = true;
    //////////        }
    //////////        else
    //////////        {
    //////////            pPetition_Appeal.Visible = false;
    //////////            pJudgementLink.Visible = false;
    //////////        }
    //////////    }
    //////////    else
    //////////    {
    //////////        pPetition_Appeal.Visible = false;
    //////////        pJudgementLink.Visible = false;
    //////////    }
    //////////}

    #endregion

    #region ddlAppealStatus_Edit selectedIndexChanged event zone

    protected void ddlAppealStatus_Edit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ddlAppealStatus_Edit.SelectedValue) != 0)
        {
            if (Convert.ToInt16(ddlAppealStatus_Edit.SelectedValue) == 14)
            {
                //pPetitionAppeal_Edit .Visible = true;
                pJudgement_Edit.Visible = true;
                pJudgementDesc_Edit.Visible = true;
            }
            else
            {
                //pPetitionAppeal_Edit.Visible = false;
                pJudgement_Edit.Visible = false;
                pJudgementDesc_Edit.Visible = false;
            }
        }
        else
        {
            //pPetitionAppeal_Edit.Visible = false;
            pJudgement_Edit.Visible = false;
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

    #region customValidator cvPublicNotice serverValidate to validate file type

    protected void cvAppeal_ServerValidate(object source, ServerValidateEventArgs args)
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
                Match match = regex.Match(TextBox2.Text);
                if (match.Success == true)
                {
                    args.IsValid = true;
                }
                else
                {

                    args.IsValid = false;
                    cvPublicNotice.Text = "Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)";
                    txtRemarksAppeal.Text = "";

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
                    txtRemarksAppeal.Text = "";
                }
            }
        }

    }

    #endregion

    #region customValidator customValidator2 serverValidate to validate file type

    protected void cvAppealEdit_ServerValidate(object source, ServerValidateEventArgs args)
    {
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
                    TextBox2.Text = "";

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
                    TextBox2.Text = "";
                }
            }

        }

        //// Get file name
        //string UploadFileName = fuPetition_Edit.PostedFile.FileName;

        //if (UploadFileName == "")
        //{
        //    // There is no file selected
        //    args.IsValid = false;
        //}
        //else
        //{
        //    string Extension = UploadFileName.Substring(UploadFileName.LastIndexOf('.') + 1).ToLower();

        //    if (Extension == "pdf")
        //    {
        //        args.IsValid = true; // Valid file type
        //    }
        //    else
        //    {
        //        args.IsValid = false; // Not valid file type
        //    }
        //}
    }

    #endregion

    #region function  bind with Repeator
    public void BindData(int Pdid)
    {
        petitionObject.AppealId = Pdid;
        p_Var.dsFileName = petPetitionBL.getFileNameForAppealpetition(petitionObject);
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
            int id = Convert.ToInt16(e.CommandArgument.ToString());

            petitionObject.ConnectionID = id;
            p_Var.Result1 = petPetitionBL.UpdateFileStatusForAppealPettion(petitionObject);
            if (p_Var.Result1 > 0)
            {
                Label filename = (Label)e.Item.FindControl("lblFile");
                //Label date = (Label)e.Item.FindControl("lblDate");
                Label lblComments = (Label)e.Item.FindControl("lblComments");
                LinkButton RemoveFileLink = (LinkButton)e.Item.FindControl("lnkFileConnectedRemove");
                Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");

                filename.Visible = false;
               // date.Visible = false;
                lblComments.Visible = false;
                RemoveFileLink.Visible = false;
                ltrlDownload.Visible = false;
            }
            bindPetition_Appeal_IN_Edit_Mode(Convert.ToInt32(Request.QueryString["pa_id"]));

        }
    }
  
    protected void ddlYear1_SelectedIndexChanged(object sender, EventArgs e)
    {
        getpetitionNumberForChkBoxConnection1();
    }
    protected void chklstPetition1SelectedIndexChangd(object sender, EventArgs e)
    {
        string value = string.Empty;

        string result = Request.Form["__EVENTTARGET"];

        string[] checkedBox = result.Split('$');
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (chklstPetition1.Items[index].Selected)
        {

            petitionObject.RPId = Convert.ToInt16(chklstPetition1.Items[index].Value);
            p_Var.dSet = petPetitionBL.get_ConnectedPetitionReview_Edit(petitionObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_Var.i = 0; p_Var.i < p_Var.dSet.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in chklstPetition1.Items)
                    {

                        if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["Connected_RP_Id"].ToString())
                        {
                            li.Selected = true;
                        }

                    }

                }
            }
            else
            {

            }
        }
    }



    #region Function to get Petition numbers for ChkBoxConnection

    public void getpetitionNumberForChkBoxConnection1()
    {
        // petitionObject.year = ddlYear.SelectedValue;
        p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

        foreach (ListItem li in ddlYear1.Items)
        {
            if (li.Selected == true)
            {
                p_Var.sbuilder.Append(li.Text).Append(",");
            }

        }
        List<string> list = new List<string>();

        foreach (ListItem li in chklstPetition1.Items)
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
            chklstPetition1.DataSource = p_Var.dSet;
            chklstPetition1.DataValueField = "RP_Id";
            chklstPetition1.DataTextField = "RPNoValue";
            chklstPetition1.DataBind();
        }
        else
        {
            chklstPetition1.DataSource = p_Var.dSet;

            chklstPetition1.DataBind();
        }

        if (ViewState["MyList"] != null)
        {
            List<string> list1 = ViewState["MyList"] as List<string>;
            foreach (ListItem li in chklstPetition1.Items)
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

    public void bindPetitionYearinDdl1()
    {
        p_Var.dSet = petPetitionBL.GetYearPetitionReview_AddEdit();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYear1.DataSource = p_Var.dSet;
            ddlYear1.DataTextField = "year";
            ddlYear1.DataValueField = "year";
            ddlYear1.DataBind();
        }
        else
        {
            ddlYear1.Items.Add(new ListItem("Select", "0"));
        }
    }

    #endregion

    //protected void lnkConnectedPetitionEdit1_Click(object sender, EventArgs e)
    //{
    //    lnkConnectedPetitionEditNo1.Visible = true;
    //    lnkConnectedPetitionEdit1.Visible = true;
    //    pnlEditConnectedPetition1.Visible = true;
    //    bindPetitionYearinDdlEdit1();
    //    getpetitionNumberForConnectionYesNo1();
    //    divConnectEdit1.Visible = true;
    //    Panel1.Visible = true;
    //}
    //protected void lnkConnectedPetitionEditNo1_Click(object sender, EventArgs e)
    //{
    //    lnkConnectedPetitionEditNo1.Visible = false;
    //    lnkConnectedPetitionEdit1.Visible = true;
    //    pnlEditConnectedPetition1.Visible = false;
    //    Panel1.Visible = false;
    //    getpetitionNumberForChkBoxConnection1();
    //    divConnectEdit1.Visible = false;
    //}
    protected void ddlYearEdit1_SelectedIndexChanged(object sender, EventArgs e)
    {
        getpetitionNumberForConnectionEdit1();
    }
    protected void chklstPetitionEdit1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    #region Function to bind petition Year

    public void bindPetitionYearinDdlEdit1()
    {
        p_Var.dSet = petPetitionBL.GetYearPetitionReview_AddEdit();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYearEdit1.DataSource = p_Var.dSet;
            ddlYearEdit1.DataTextField = "year";
            ddlYearEdit1.DataValueField = "year";
            ddlYearEdit1.DataBind();
        }
        else
        {
            ddlYear1.Items.Add(new ListItem("Select", "0"));
        }
    }

    #endregion


    #region Function to get Petition numbers for connections

    public void getpetitionNumberForConnectionYesNo1()
    {
        //petitionObject.year = ddlYear.SelectedValue;
        PetitionOB newOB = new PetitionOB();
        PetitionBL newBL = new PetitionBL();
        newOB.AppealId = Convert.ToInt32(Request.QueryString["pa_id"]);

        DataSet dsetYear = new DataSet();
        StringBuilder sbuilderYear = new StringBuilder();
        //// dsetYear = newBL.getPetitionYearForConnectionEdit(newOB);
        dsetYear = objAppealBL.Connected_multipleReviewAppealForConnectionEdit(newOB);
        if (dsetYear.Tables[0].Rows.Count > 0)
        {
            for (p_Var.i = 0; p_Var.i < dsetYear.Tables[0].Rows.Count; p_Var.i++)
            {
                foreach (ListItem liEdit in ddlYearEdit1.Items)
                {
                    if (liEdit.Value.ToString() == dsetYear.Tables[0].Rows[p_Var.i]["year"].ToString().Trim())
                    {
                        liEdit.Selected = true;
                        sbuilderYear.Append(liEdit.Text).Append(",");

                    }

                }
            }
        }
        List<string> list = new List<string>();

        foreach (ListItem li in chklstPetition1.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
            }

        }

        ViewState["MyList"] = list;

        petitionObject.year = sbuilderYear.ToString();

        ViewState["year"] = sbuilderYear.ToString();
        getpetitionNumberForConnectionEdit1();



        if (ViewState["MyList"] != null)
        {
            List<string> list1 = ViewState["MyList"] as List<string>;
            foreach (ListItem li in chklstPetitionEdit1.Items)
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

    #region Function to get Petition numbers for connections in Edit mode

    public void getpetitionNumberForConnectionEdit1()
    {
        // petitionObject.year = ddlYear.SelectedValue;
        p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

        foreach (ListItem li in ddlYearEdit1.Items)
        {
            if (li.Selected == true)
            {
                p_Var.sbuilder.Append(li.Text).Append(",");
            }

        }
        List<string> list = new List<string>();

        foreach (ListItem li in chklstPetitionEdit1.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
            }

        }

        ViewState["MyList"] = list;


        petitionObject.year = p_Var.sbuilder.ToString();
        petitionObject.RPId = Convert.ToInt32(Request.QueryString["pa_id"]);
    
        p_Var.dSet = petPetitionBL.getPetitionReviewNumberForConnectionEdit(petitionObject);
        
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            chklstPetitionEdit1.DataSource = p_Var.dSet;
            chklstPetitionEdit1.DataValueField = "RP_Id";
            chklstPetitionEdit1.DataTextField = "RPNoValue";
            chklstPetitionEdit1.DataBind();
        }
        else
        {
            chklstPetitionEdit1.DataSource = p_Var.dSet;
            chklstPetitionEdit1.DataBind();
        }

        PetitionOB newOB = new PetitionOB();
        DataSet dsnew1 = new DataSet();
        newOB.RPId = Convert.ToInt32(Request.QueryString["pa_id"]);
        newOB.year = p_Var.sbuilder.ToString();

        dsnew1 = petPetitionBL.getConnectedReviewPetitionEditNew(newOB);

        for (p_Var.i = 0; p_Var.i < dsnew1.Tables[0].Rows.Count; p_Var.i++)
        {
            foreach (ListItem li in chklstPetitionEdit1.Items)
            {
                if (li.Value.ToString() == dsnew1.Tables[0].Rows[p_Var.i]["Connected_RP_Id"].ToString().Trim())
                {
                    li.Selected = true;


                }

            }
        }

        if (ViewState["MyList"] != null)
        {
            List<string> list1 = ViewState["MyList"] as List<string>;
            foreach (ListItem li in chklstPetitionEdit1.Items)
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
        petitionObject.OrderTypeID = Convert.ToInt16(rdioConnectedOrder.SelectedValue);
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
    protected void lnkConnectedOrderEdit_Click(object sender, EventArgs e)
    {
        pnlConnectedOrderEdit.Visible = true;
        lnkConnectedOrderEditNo.Visible = true;

        petitionObject.AppealId = Convert.ToInt32(Request.QueryString["pa_id"]);
        p_Var.dsFileName = ObjOrderBL.GeAppealPetitionConnectedOrder(petitionObject);
        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
        {

            GetYearConnectedOrderEdit(Convert.ToInt32(p_Var.dsFileName.Tables[0].Rows[0]["OrderTypeId"]));
            GetConnectedOrderIdEdit(Convert.ToInt32(p_Var.dsFileName.Tables[0].Rows[0]["OrderTypeId"]), p_Var.dsFileName.Tables[0].Rows[0]["Year"].ToString());
            drpConnectedOrderYearEdit.SelectedValue = p_Var.dsFileName.Tables[0].Rows[0]["Year"].ToString();
            rdioConnectedOrderEdit.SelectedValue = p_Var.dsFileName.Tables[0].Rows[0]["OrderTypeId"].ToString();
            drpConnectedOrderIdEdit.SelectedValue =HttpUtility.HtmlDecode( p_Var.dsFileName.Tables[0].Rows[0]["orderid"].ToString());
        }

    }
    protected void lnkConnectedOrderEditNo_Click(object sender, EventArgs e)
    {
        pnlConnectedOrderEdit.Visible = false;
        lnkConnectedOrderEditNo.Visible = false;
        PConnectedYearEdit.Visible = false;
        PConnectedOrderIdEdit.Visible = false;
       

    }
    protected void drpConnectedOrderYearEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetConnectedOrderIdEdit(Convert.ToInt32(rdioConnectedOrderEdit.SelectedValue), drpConnectedOrderYearEdit.SelectedValue.ToString());
    }
    protected void rdioConnectedOrderEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetYearConnectedOrderEdit(Convert.ToInt32(rdioConnectedOrderEdit.SelectedValue));
        GetConnectedOrderIdEdit(Convert.ToInt32(rdioConnectedOrderEdit.SelectedValue), drpConnectedOrderYearEdit.SelectedValue.ToString());
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
                drpConnectedOrderIdEdit.Items[i].Attributes.Add("title", HttpUtility.HtmlDecode(p_Var.dSetCompare.Tables[0].Rows[i]["OrderFullDesc"].ToString()));

            }
            //foreach (ListItem liEdit in drpConnectedOrderIdEdit.Items)
            //{
            //    liEdit.Attributes["Title"] = liEdit.Text;
            //}
        }
        else
        {
            //PconnectedOrder.Visible = false;
            drpConnectedOrderIdEdit.Items.Add(new ListItem("Select", "0"));


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

    protected void datalistFileName_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            var link = e.Item.FindControl("lnkFileConnectedRemove") as LinkButton;
            var hiddenField = e.Item.FindControl("hiddenFieldAppealId") as HiddenField;
            Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");
            Label filename = (Label)e.Item.FindControl("lblFile");
            LinkButton lnkFileConnectedRemove = (LinkButton)e.Item.FindControl("lnkFileConnectedRemove");
            lnkFileConnectedRemove.OnClientClick = "javascript:return confirm('Are you sure you want to delete this file :- " + filename.Text + " ?')";
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
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["pa_id"]))
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
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["pa_id"]))
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




            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);



            p_Var.sbuildertmp.Append("<a href='" + ResolveUrl(p_Var.Path) + filename.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='Download File' width='15' alt='Download File' height=\"15\" /> " + "</a>");
            p_Var.sbuildertmp.Append("<hr />");
            ltrlDownload.Text = p_Var.sbuildertmp.ToString();
        }
    }

    protected void drpConnectedOrderIdEdit_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in drpConnectedOrderIdEdit.Items)
        {
            item.Text = item.Text.Replace("&lt;br /&gt;", " ");
        }
    }
    protected void drpConnectedOrderId_PreRender(object sender, EventArgs e)
    {
        foreach (ListItem item in drpConnectedOrderId.Items)
        {
            item.Text = item.Text.Replace("&lt;br /&gt;", " ");
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
