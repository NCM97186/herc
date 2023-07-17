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
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


public partial class Auth_AdminPanel_SOH_SOH_Add_Edit : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration

    #region Data declaration zone

    Project_Variables p_Var = new Project_Variables();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petPetitionBL = new PetitionBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    LinkBL obj_LinkBL = new LinkBL();
    AppealBL objappeal = new AppealBL();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion

    //End

    //Area for the page load event

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.Path = "~/" + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["scheduleofHearing"] + "/";
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

            Get_Deptt_Name();

            // This is commented on date 26 DEp 2013 by ruchi

            if (ddlDepartment.SelectedValue == "1")
            {
                Ppetitionappeal.Visible = true;
                PApeal.Visible = false;
                txtVenue.Text = "Court Room, HERC, Panchkula";

            }
            else
            {
                PApeal.Visible = true;

                Ppetitionappeal.Visible = false;
                txtVenue.Text = "Electricity Ombudsman Office";
            }

            //End
            bindTimeinDropdownlist();
            bindHoursinDropdownlist();
            bindampminDropdownlist();
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }

            if (Convert.ToInt16(Request.QueryString["ModuleID"]) == Convert.ToInt16(Module_ID_Enum.Project_Module_ID.SOH))
            {
                pnlDropdownlist.Visible = false;
                pnlSubject.Visible = true;
                if (Request.QueryString["sohid"] != null)
                {
                    displayMetaLang1();
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": Edit Schedule Of Hearing";
                    this.Page.Title = "Edit Schedule Of Hearing: HERC";
                    bindTimeinDropdownlistEdit();
                    bindHoursinDropdownlistEdit();
                    bindampminDropdownlistEdit();
                    bindScheuduleOfHearing(Convert.ToInt32(Request.QueryString["sohid"]));
                    BindData(Convert.ToInt32(Request.QueryString["sohid"]));
                    pnlPetitionEdit.Visible = true;
                    pnlPetitionAdd.Visible = false;

                }
                else
                {
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": Add Schedule Of Hearing";
                    this.Page.Title = "Add Schedule Of Hearing: HERC";
                    bindropDownlistLang(); // Get the Language privilage
                }
            }
            else if (Convert.ToInt16(Request.QueryString["ModuleID"]) == Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal))
            {
                PAppeal.Visible = true;
                bindropDownlistLang();
                BindYear(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal));
                bindApprovedAppeal("0");
            }
            else
            {
                pnlDropdownlist.Visible = true;
                bindropDownlistLang();
                bindApprovedPetition("0");
                BindYear(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));

            }
        }
    }

    #endregion

    //End

    //Area for all the buttons, linkButtons, imageButtons click events

    #region button btnAddScheduleOfHearing click event to add schedule of hearing

    protected void btnAddScheduleOfHearing_Click(object sender, EventArgs e)
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

                    petObject.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                    DateTime date1 = Convert.ToDateTime(miscellBL.getDateFormat(txtDate.Text.ToString()));
                    petObject.year = HttpUtility.HtmlEncode(date1.Year.ToString());

                    //This code added on date 16 Sep 2013 by ruchi

                    petObject.MetaDescription = txtMetaDescription.Text;
                    petObject.MetaKeyWords = txtMetaKeyword.Text;
                    petObject.MetaTitle = txtMetaTitle.Text;
                    petObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;

                    petObject.ApplicantMobileNo = txtMobileNo.Text;
                    petObject.ApplicantEmail = txtEmailID.Text;

                    //End

                    if (ddlDepartment.SelectedValue == "1")
                    {
                        if (ddlpetitionappeal.SelectedValue != "0")
                        {

                        }
                        petObject.DepttId = 1;

                        petObject.keyword = null;
                        if (pnlDropdownlist.Visible == true)
                        {
                            petObject.PetitionType = Convert.ToInt32(ddlConnectionType.SelectedValue);
                        }
                        else
                        {
                            petObject.PetitionType = 0;
                        }


                    }
                    else if (ddlDepartment.SelectedValue == "2")
                    {
                        petObject.PetitionId = null;
                        petObject.RPId = null;
                        petObject.AppealId = null;
                        petObject.DepttId = 2;

                        if (PApeal.Visible == true)
                        {

                            petObject.keyword = ddlappeal.SelectedValue.ToString();
                        }
                        else
                        {
                            petObject.keyword = "0";
                        }
                    }
                    else
                    {
                        petObject.AppealNo = null;
                        petObject.PetitionId = null;
                        petObject.RPId = null;
                        petObject.AppealId = null;
                        petObject.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
                        petObject.keyword = null;

                    }

                    petObject.Remarks = HttpUtility.HtmlEncode(txtRemarks.Text.Replace(Environment.NewLine, "<br />"));
                    petObject.subject = HttpUtility.HtmlEncode(txtSubject.Text.Replace(Environment.NewLine, "<br />"));
                    petObject.Date = miscellBL.getDateFormat(txtDate.Text);
                    petObject.Time = ddlhours.SelectedValue + ":" + ddlmins.SelectedValue + ":" + ddlampm.SelectedItem.ToString();
                    petObject.Venue = txtVenue.Text;
                    petObject.recordInsertedBy = Convert.ToInt16(Session["User_Id"]);
                    petObject.LangId = Convert.ToInt16(Module_ID_Enum.Language_ID.English);
                    petObject.StatusId = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft);
                    petObject.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);   //should be change according to the module
                    petObject.InsertedBy = Convert.ToInt32(Session["User_Id"]);
                    petObject.IpAddress = Miscelleneous_DL.getclientIP();
                    petObject.PlaceHolderOne = HttpUtility.HtmlEncode(txtURL.Text);
                    petObject.PlaceHolderTwo = HttpUtility.HtmlEncode(txtURLDescription.Text);
                    petObject.SohFile = null;


                    p_Var.Result = petPetitionBL.InsertTmpScheduleOfHearing(petObject);
                    if (p_Var.Result > 0)
                    {
                        foreach (ListItem li in chklstPetition.Items)
                        {
                            if (li.Selected == true)
                            {
                                petObject.soh_ID = p_Var.Result;
                                int cnt = li.Text.LastIndexOf(' ');
                                petObject.year = li.Text.Substring(cnt).Trim();
                                petObject.StatusId = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft);
                                if (ddlDepartment.SelectedValue == "1")
                                {
                                    if (ddlConnectionType.SelectedValue == "1")
                                    {
                                        petObject.PetitionId = Convert.ToInt16(li.Value.ToString());
                                    }
                                    else
                                    {
                                        petObject.RPId = Convert.ToInt16(li.Value.ToString());
                                    }
                                    petObject.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
                                }

                                p_Var.Result1 = petPetitionBL.insertSoHwithPetition(petObject);

                            }
                        }
                        //This is for Appeal
                        foreach (ListItem li in CheckBoxAppeal.Items)
                        {
                            if (li.Selected == true)
                            {
                                petObject.soh_ID = p_Var.Result;
                                int cnt = li.Text.LastIndexOf(' ');
                                petObject.year = li.Text.Substring(cnt).Trim();
                                petObject.StatusId = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft);
                                petObject.AppealId = Convert.ToInt16(li.Value.ToString());
                                petObject.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);

                                p_Var.Result1 = petPetitionBL.insertSoHwithPetition(petObject);

                            }
                        }
                        // created on date 19/04/2013 
                        string fileMultiple = string.Empty;
                        HttpFileCollection hfc = Request.Files;

                        for (int i = 0; i < hfc.Count; i++)
                        {
                            HttpPostedFile hpf = hfc[i];

                            fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
                            if (fileMultiple != null && fileMultiple != "")
                            {
                                PetitionOB newObj = new PetitionOB();
                                newObj.soh_ID = p_Var.Result;
                                newObj.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                                newObj.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);

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
                                    newObj.FileName = p_Var.Filename;
                                }

                                newObj.StartDate = System.DateTime.Now;

                                int Result2 = petPetitionBL.insertConnectedSohFiles(newObj);
                            }
                        }

                        obj_audit.ActionType = "I";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = "New Record";
                        string department = "Department: " + ddlDepartment.SelectedItem.ToString();
                        string txtdate = "Date: " + txtDate.Text;
                        string time = "Time: " + ddlhours.SelectedValue + ":" + ddlmins.SelectedValue + " " + ddlampm.SelectedValue;
                        obj_audit.Title = department + "," + Environment.NewLine + txtdate + "," + Environment.NewLine + time + "," + Environment.NewLine + "Subject: " + txtSubject.Text;
                        //obj_audit.Title = txtSubject.Text;
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "Schedule of hearing has been submitted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/SOH/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
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

    #region button btnReset click event to reset fields

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtDate.Text = "";
        txtSubject.Text = "";
        txtVenue.Text = "";
        txtDate.Text = "";
        txtRemarks.Text = "";
        TextBox1.Text = "";
        txtEmailID.Text = "";
        txtMobileNo.Text = "";

    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/SOH/SOH_Display.aspx?ModuleID=" + Convert.ToInt16(Request.QueryString["ModuleID"])));

        // hold the previous page reference
        // object refUrl = ViewState["RefUrl"];
        //  if (refUrl != null)
        //  {
        //     Response.Redirect((string)refUrl);
        //}
    }

    #endregion

    #region button btnUpdate click event to update record

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            petObject.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.update);
            petObject.SohTempID = Convert.ToInt32(Request.QueryString["sohid"]);
            petObject.soh_ID = Convert.ToInt32(Request.QueryString["sohid"]);
            DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtDate_Edit.Text));
            petObject.year = date.Year.ToString();

            petObject.MetaDescription = txtMetaDescriptionEdit.Text;
            petObject.MetaKeyWords = txtMetaKeywordEdit.Text;
            petObject.MetaTitle = txtMetaTitleEdit.Text;
            petObject.MetaKeyLanguage = DropDownList1.SelectedValue;
            //This code added on date 16 Sep 2013 by ruchi
            petObject.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);   //should be change according to the module
            petObject.InsertedBy = Convert.ToInt32(Session["User_Id"]);
            petObject.IpAddress = Miscelleneous_DL.getclientIP();
            petObject.ApplicantMobileNo = txtMobileNoEdit.Text;
            petObject.ApplicantEmail = txtEmailIDEdit.Text;
            petObject.PlaceHolderOne = txtURLEdit.Text;
            petObject.PlaceHolderTwo = txtURLDescriptionEdit.Text;
            //End

            if (pnlDropdownlistEdit.Visible == true)
            {
                if (ddlConnectionTypeEdit.SelectedValue == "1")
                {
                    if (pnlDropdownlist.Visible == true)
                    {
                        petObject.PetitionType = Convert.ToInt16(ddlConnectionType.SelectedValue);
                    }
                    else
                    {
                        petObject.PetitionType = 0;
                    }
                }
                else if (ddlConnectionTypeEdit.SelectedValue == "2")
                {
                    if (pnlDropdownlist.Visible == true)
                    {
                        petObject.PetitionType = Convert.ToInt16(ddlConnectionType.SelectedValue);
                    }
                    else
                    {
                        petObject.PetitionType = 0;
                    }
                }
                else if (ddlConnectionTypeEdit.SelectedValue == "3")
                {
                }
                petObject.DepttId = Convert.ToInt16(Request.QueryString["depttID"]);
            }
            else if (PAppealedit.Visible == true)
            {
                petObject.PetitionId = null;
                petObject.RPId = null;
                petObject.AppealId = null;
                petObject.DepttId = 2;
                foreach (ListItem li in CheckBoxAppealEdit.Items)
                {
                    if (li.Selected == true)
                    {
                        petObject.keyword = ddlappealEdit.SelectedValue.ToString();
                        ViewState["liappeal"] = petObject.keyword;
                        break;

                    }
                }
                if (ViewState["liappeal"] != null)
                {
                    petObject.keyword = ddlappealEdit.SelectedValue.ToString();
                }
                else
                {
                    petObject.keyword = ddlappealEdit.SelectedValue.ToString();//0
                }
                ViewState.Remove("liappeal");

            }
            else
            {
                petObject.keyword = ddlappealEdit.SelectedValue.ToString();
                petObject.DepttId = Convert.ToInt16(Request.QueryString["depttID"]);
            }
            petObject.Remarks = HttpUtility.HtmlEncode(txtRemarksEdit.Text.Replace(Environment.NewLine, "<br />"));
            petObject.subject = HttpUtility.HtmlEncode(txtSubjectEdit.Text.Replace(Environment.NewLine, "<br />"));
            petObject.Date = Convert.ToDateTime(miscellBL.getDateFormat(txtDate_Edit.Text));

            petObject.Time = ddlhoursEdit.SelectedValue + ":" + ddlminsEdit.SelectedValue + ":" + ddlampmEdit.SelectedItem.ToString();

            petObject.Venue = txtVenue_Edit.Text;
            petObject.recordUpdatedBy = Convert.ToInt16(Session["User_Id"]);
            petObject.LangId = Convert.ToInt16(Request.QueryString["LangID"]);
            petObject.SohFile = null;
            petObject.StatusId = Convert.ToInt32(Session["Status_Id"]);


            if (pnlDropdownlistEdit.Visible == true)
            {

                foreach (ListItem li in chklstPetitionEdit.Items)
                {
                    if (li.Selected == true)
                    {
                        petObject.PetitionType = Convert.ToInt16(ddlConnectionTypeEdit.SelectedValue);
                        break;

                    }
                    else
                    {
                        petObject.PetitionType = 0;
                    }
                }
            }
            else
            {
                if (ddlappealEdit.Visible == true)
                {
                    petObject.PetitionType = null;
                }
                else
                {
                    petObject.PetitionType = 0;
                }
            }

            if (pnlDropdownlistEdit.Visible == true)
            {

                if (ddlpetitionappealEdit.SelectedValue != "0")
                {

                    foreach (ListItem li in chklstPetitionEdit.Items)
                    {
                        if (li.Selected == true)
                        {
                            petObject.PetitionType = Convert.ToInt16(ddlConnectionTypeEdit.SelectedValue);
                            break;

                        }
                        else
                        {
                            petObject.PetitionType = 0;
                        }
                    }
                }
                else
                {
                    petObject.PetitionType = 0;
                }
                //petObject.PetitionType = Convert.ToInt16(ddlConnectionTypeEdit.SelectedValue);
            }
            else
            {
                if (PAppealedit.Visible == true)
                {
                    //petObject.PetitionType = 2;
                    foreach (ListItem li in CheckBoxAppealEdit.Items)
                    {
                        if (li.Selected == true)
                        {
                            //petObject.PetitionType = 2;
                            petObject.PetitionType = null;
                            break;

                        }
                        else
                        {
                            //petObject.PetitionType = 0;
                            petObject.PetitionType = null;
                        }
                    }
                }
                else
                {
                    if (ddlappealEdit.Visible == true)
                    {
                        petObject.PetitionType = null;
                    }
                    else
                    {
                        petObject.PetitionType = 0;
                    }
                }
            }
            p_Var.Result = petPetitionBL.InsertTmpScheduleOfHearing(petObject);

            if (p_Var.Result > 0)
            {
                // 12 march

                petObject.soh_ID = p_Var.Result;
                int result2 = petPetitionBL.deleteConnectedPetitionFromSOH(petObject);


                //This is for petition/Reviewpetition
                if (pnlEditpetition.Visible == true)
                {
                    foreach (ListItem li in chklstPetitionEdit.Items)
                    {
                        //if (ddlpetitionappealEdit.SelectedValue != "0")
                        //{
                        if (li.Selected == true)
                        {
                            petObject.soh_ID = p_Var.Result;
                            //petObject.year = ddlyearEdit.SelectedValue.ToString();
                            int cnt = li.Text.LastIndexOf(' ');
                            petObject.year = li.Text.Substring(cnt).Trim();
                            //petObject.StatusId = 6;
                            if (petObject.StatusId == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                petObject.StatusId = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft);
                            }
                            if (ddlConnectionTypeEdit.SelectedValue == "1")
                            {
                                petObject.PetitionId = Convert.ToInt16(li.Value.ToString());
                            }
                            else
                            {
                                petObject.RPId = Convert.ToInt16(li.Value.ToString());
                            }
                            petObject.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);

                            p_Var.Result1 = petPetitionBL.insertSoHwithPetition(petObject);

                        }
                        //}
                        //else
                        //{
                        //    petObject.soh_ID = p_Var.Result;
                        //    int result4 = petPetitionBL.deleteConnectedPetitionFromSOH(petObject);
                        //}
                    }
                }
                //This is for Appeal
                if (PAppealedit.Visible == true)
                {
                    foreach (ListItem li in CheckBoxAppealEdit.Items)
                    {
                        if (li.Selected == true)
                        {
                            petObject.soh_ID = p_Var.Result;
                            // petObject.year = ddlyearAppealedit.SelectedValue.ToString();
                            int cnt = li.Text.LastIndexOf(' ');
                            petObject.year = li.Text.Substring(cnt).Trim();
                            if (petObject.StatusId == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                petObject.StatusId = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft);
                            }
                            petObject.AppealId = Convert.ToInt16(li.Value.ToString());
                            petObject.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);

                            p_Var.Result1 = petPetitionBL.insertSoHwithPetition(petObject);

                        }
                    }
                }

                // created on date 19/04/2013 
                string fileMultiple = string.Empty;
                HttpFileCollection hfc = Request.Files;

                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];

                    fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
                    if (fileMultiple != null && fileMultiple != "")
                    {
                        PetitionOB newObj = new PetitionOB();
                        newObj.soh_ID = p_Var.Result;
                        newObj.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                        newObj.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);

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
                            //newObj.PetitionFile = "P_" + p_Var.Filename;
                            newObj.FileName = p_Var.Filename;
                        }

                        newObj.StartDate = System.DateTime.Now;

                        int Result3 = petPetitionBL.insertConnectedSohFiles(newObj);
                    }
                }

                obj_audit.ActionType = "U";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                string st = Request.QueryString["Status"].Trim();
                if (st == "3") { obj_audit.status = "Draft"; } else if (st == "4") { obj_audit.status = "For Publish"; } else if (st == "6") { obj_audit.status = "Published"; } else if (st == "8") { obj_audit.status = "Deleted"; } else if (st == "21") { obj_audit.status = "For Review"; }
                obj_audit.IpAddress = miscellBL.IpAddress();
                string depttID = Request.QueryString["DepttID"].ToString();
                string department = "Department: " + Request.QueryString["DepttID"].ToString();
                if (depttID == "1") { department = "Department: " + "HERC"; } else { department = "Department: " + "Ombudsman"; }
                string txtdate = "Date: " + txtDate_Edit.Text;
                string time = "Time: " + ddlhoursEdit.SelectedValue + ":" + ddlminsEdit.SelectedValue + " " + ddlampm.SelectedValue;
                obj_audit.Title = department + "," + Environment.NewLine + txtdate + "," + Environment.NewLine + time + "," + Environment.NewLine + "Subject: " + txtSubjectEdit.Text;
                //obj_audit.Title = txtSubjectEdit.Text;
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                Session["msg"] = "Schedule of hearing has been updated successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/SOH/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }
    }

    #endregion

    #region button btnResetEdit click event to reset records

    protected void btnResetEdit_Click(object sender, EventArgs e)
    {
        TextBox2.Text = "";
        bindScheuduleOfHearing(Convert.ToInt32(Request.QueryString["sohid"]));
        BindData(Convert.ToInt32(Request.QueryString["sohid"]));
    }

    #endregion

    #region button btnBackEdit click event to go back

    protected void btnBackEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/soh/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"]));
    }

    #endregion

    //Area for all the user-defined functions

    #region Function to bind language in dropDownlist according to permission

    public void bindropDownlistLang()
    {
        try
        {

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind approved petition number in dropDownlist

    public void bindApprovedPetition(string year)
    {
        try
        {
            //p_Var.dSet = petPetitionBL.getPetitionNumber();
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Remove(0, strBuilder.Length);
            foreach (ListItem li in ddlyear.Items)
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
                    strBuilder.Append(li.Text + ";");
                }

            }

            ViewState["MyList"] = list;
            petObject.year = p_Var.sbuilder.ToString();


            // petObject.year = year.Trim().ToString();
            p_Var.dSet = petPetitionBL.getPetitionNumberNew(petObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                chklstPetition.DataSource = p_Var.dSet;
                chklstPetition.DataTextField = "PRONoValue";
                chklstPetition.DataValueField = "Petition_id";
                chklstPetition.DataBind();
                //chklstPetition.Items.Insert(0, new ListItem("Select One", "0"));
            }
            else
            {
                chklstPetition.DataSource = p_Var.dSet;
                chklstPetition.DataBind();
                // chklstPetition.Items.Insert(0, new ListItem("Select One", "0"));
            }

            if (ViewState["MyList"] != null)
            {
                strBuilder.Remove(0, strBuilder.Length);
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
                    // li.Selected = true;
                }
                ltrlSelected.Text = strBuilder.ToString();
            }

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind approved petition review number in dropDownlist

    public void bindApprovedPetitionReview(string year)
    {
        try
        {

            //petObject.year = year;
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Remove(0, strBuilder.Length);
            foreach (ListItem li in ddlyear.Items)
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
                    strBuilder.Append(li.Text + ";");
                }

            }

            ViewState["MyList"] = list;
            petObject.year = p_Var.sbuilder.ToString();
            p_Var.dSet = petPetitionBL.getPetitionReviewNumber(petObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                chklstPetition.DataSource = p_Var.dSet;
                chklstPetition.DataTextField = "RPNoValue";
                chklstPetition.DataValueField = "RP_Id";
                chklstPetition.DataBind();
                //  chklstPetition.Items.Insert(0, new ListItem("Select One", "0"));
            }
            else
            {
                chklstPetition.DataSource = p_Var.dSet;
                chklstPetition.DataBind();
                // chklstPetition.Items.Insert(0, new ListItem("Select One", "0"));
            }

            if (ViewState["MyList"] != null)
            {
                strBuilder.Remove(0, strBuilder.Length);
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
                    // li.Selected = true;
                }
                ltrlSelected.Text = strBuilder.ToString();
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind approved petition appeal number in dropDownlist

    public void bindApprovedPetitionAppeal()
    {
        try
        {
            p_Var.dSet = petPetitionBL.getPetitionAppealNumber();
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                chklstPetition.DataSource = p_Var.dSet;
                chklstPetition.DataTextField = "Appeal_No";
                chklstPetition.DataValueField = "PA_Id";
                chklstPetition.DataBind();
                //chklstPetition.Items.Insert(0, new ListItem("Select One", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind approved petition number in dropDownlist in edit mode

    public void bindApprovedPetitionEdit(string year)
    {
        try
        {
            DataSet dsnew = new DataSet();
            DataSet dsnew1 = new DataSet();
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Remove(0, strBuilder.Length);
            PAppealedit.Visible = false;
            pnlDropdownlistEdit.Visible = true;
            foreach (ListItem li in ddlyearEdit.Items)
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
                    strBuilder.Append(li.Text + ";");
                }

            }

            ViewState["MyList"] = list;
            //petObject.year = year.Trim();
            petObject.year = p_Var.sbuilder.ToString();
            dsnew = petPetitionBL.getPetitionNumber(petObject);
            if (dsnew.Tables[0].Rows.Count > 0)
            {
                strBuilder.Remove(0, strBuilder.Length);
                chklstPetitionEdit.DataSource = dsnew;
                chklstPetitionEdit.DataTextField = "PRONoValue";
                chklstPetitionEdit.DataValueField = "Petition_id";
                chklstPetitionEdit.DataBind();


                PetitionOB newOB = new PetitionOB();
                newOB.soh_ID = Convert.ToInt32(Request.QueryString["sohid"]);
                newOB.PetitionType = Convert.ToInt32(ddlConnectionTypeEdit.SelectedValue);
                newOB.StatusId = Convert.ToInt16(Request.QueryString["Status"]);
                dsnew1 = petPetitionBL.get_ConnectedSOHEdit(newOB);

                for (p_Var.i = 0; p_Var.i < dsnew1.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in chklstPetitionEdit.Items)
                    {
                        if (li.Value.ToString() == dsnew1.Tables[0].Rows[p_Var.i]["Petition_id"].ToString())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");
                        }
                        else if (li.Value.ToString() == dsnew1.Tables[0].Rows[p_Var.i]["ReviewId"].ToString())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");
                        }

                    }

                    foreach (ListItem li in CheckBoxAppealEdit.Items)
                    {
                        if (li.Value.ToString() == dsnew1.Tables[0].Rows[p_Var.i]["AppealId"].ToString().Trim())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");

                        }
                    }
                }
                ltrlPetitionEdit.Text = strBuilder.ToString();
            }
            else
            {
                chklstPetitionEdit.DataSource = dsnew;
                chklstPetitionEdit.DataBind();
                ltrlPetitionEdit.Text = "";
                //chklstPetitionEdit.Items.Insert(0, new ListItem("Select One", "0"));
            }
            if (ViewState["MyList"] != null)
            {
                //strBuilder.Remove(0, strBuilder.Length);
                List<string> list1 = ViewState["MyList"] as List<string>;
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
                ltrlSelected.Text = strBuilder.ToString();
            }

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind approved petition review number in dropDownlist in edit mode

    public void bindApprovedPetitionReviewEdit(string year)
    {
        try
        {
            PAppealedit.Visible = false;
            pnlDropdownlistEdit.Visible = true;
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Remove(0, strBuilder.Length);
            foreach (ListItem li in ddlyearEdit.Items)
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
                    strBuilder.Append(li.Text + ";");
                }

            }
            ViewState["MyList"] = list;
            petObject.year = p_Var.sbuilder.ToString();
            p_Var.dSetChildData = petPetitionBL.getPetitionReviewNumber(petObject);
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {
                chklstPetitionEdit.DataSource = p_Var.dSetChildData;
                chklstPetitionEdit.DataTextField = "RPNoValue";
                chklstPetitionEdit.DataValueField = "RP_Id";
                chklstPetitionEdit.DataBind();
                strBuilder.Remove(0, strBuilder.Length);
                PetitionOB newOB = new PetitionOB();
                newOB.soh_ID = Convert.ToInt32(Request.QueryString["sohid"]);
                newOB.PetitionType = Convert.ToInt32(ddlConnectionTypeEdit.SelectedValue);
                newOB.StatusId = Convert.ToInt16(Request.QueryString["Status"]);
                p_Var.dsFileName = petPetitionBL.get_ConnectedSOHEdit(newOB);

                for (p_Var.i = 0; p_Var.i < p_Var.dsFileName.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in chklstPetitionEdit.Items)
                    {
                        if (li.Value.ToString() == p_Var.dsFileName.Tables[0].Rows[p_Var.i]["Petition_id"].ToString())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");
                        }
                        else if (li.Value.ToString() == p_Var.dsFileName.Tables[0].Rows[p_Var.i]["ReviewId"].ToString())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");
                        }

                    }

                    foreach (ListItem li in CheckBoxAppealEdit.Items)
                    {
                        if (li.Value.ToString() == p_Var.dsFileName.Tables[0].Rows[p_Var.i]["AppealId"].ToString().Trim())
                        {
                            li.Selected = true;

                            strBuilder.Append(li.Text + ";");
                        }
                    }
                }

                ltrlPetitionEdit.Text = strBuilder.ToString();
            }
            else
            {
                chklstPetitionEdit.DataSource = p_Var.dSetChildData;
                chklstPetitionEdit.DataBind();
                ltrlPetitionEdit.Text = "";
                strBuilder.Remove(0, strBuilder.Length);
                //chklstPetitionEdit.Items.Insert(0, new ListItem("Select One", "0"));
            }
            ////if (ViewState["MyList"] != null)
            ////{
            ////    List<string> list1 = ViewState["MyList"] as List<string>;
            ////    foreach (ListItem li in chklstPetitionEdit.Items)
            ////    {
            ////        foreach (string val in list1)
            ////        {
            ////            if (li.Value == val)
            ////            {
            ////                li.Selected = true;
            ////                strBuilder.Append(li.Text + ";");
            ////            }
            ////        }
            ////        // li.Selected = true;
            ////    }
            ////    ltrlPetitionEdit.Text = strBuilder.ToString();
            ////}

        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region Function to display data in edit mode

    public void bindScheuduleOfHearing(int sohid)
    {
        try
        {
            petObject.SohTempID = Convert.ToInt32(sohid);
            petObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = petPetitionBL.getScheduleOfHearing_Edit(petObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                txtDate_Edit.Text = miscellBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["Date"].ToString());
                ddlhoursEdit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["time"].ToString().Substring(0, 2);
                ddlminsEdit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["time"].ToString().Substring(3, 2);

                ddlampmEdit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["time"].ToString().Substring(6, 4).Trim();
                txtURLEdit.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceHolderOne"].ToString().Trim();
                txtURLDescriptionEdit.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceHolderTwo"].ToString().Trim();
                txtVenue_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Venue"].ToString());

                txtMetaDescriptionEdit.Text = p_Var.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
                txtMetaTitleEdit.Text = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
                txtMetaKeywordEdit.Text = p_Var.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
                DropDownList1.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();

                if (p_Var.dSet.Tables[0].Rows[0]["EmailId"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["EmailId"].ToString() != "")
                {
                    txtEmailIDEdit.Text = p_Var.dSet.Tables[0].Rows[0]["EmailId"].ToString();
                }
                if (p_Var.dSet.Tables[0].Rows[0]["MoblieNo"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["MoblieNo"].ToString() != "")
                {
                    txtMobileNoEdit.Text = p_Var.dSet.Tables[0].Rows[0]["MoblieNo"].ToString();
                }

                if (p_Var.dSet.Tables[0].Rows[0]["Remarks"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["Remarks"].ToString() != "")
                {
                    //txtRemarksEdit.Text = p_Var.dSet.Tables[0].Rows[0]["Remarks"].ToString();
                    txtRemarksEdit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Remarks"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                }
                if (p_Var.dSet.Tables[0].Rows[0]["Appeal_PetitionID"] == DBNull.Value) //Check for the schedule of hearing of appeal
                {
                    p2.Visible = false;
                    if (p_Var.dSet.Tables[0].Rows[0]["PetitionType"] != null && p_Var.dSet.Tables[0].Rows[0]["PetitionType"].ToString() == "1" || p_Var.dSet.Tables[0].Rows[0]["PetitionType"].ToString() == "2" || p_Var.dSet.Tables[0].Rows[0]["PetitionType"].ToString() == "0")
                    {
                        //ddlpetitionappealEdit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["PetitionType"].ToString();
                        P1.Visible = true;
                        pnlEditpetition.Visible = true;
                        if (p_Var.dSet.Tables[0].Rows[0]["PetitionType"].ToString() != "0")
                        {
                            ddlpetitionappealEdit.SelectedValue = "1";
                            pnlEditpetition.Visible = true;
                            ddlConnectionTypeEdit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["PetitionType"].ToString();
                        }
                        else
                        {
                            ddlpetitionappealEdit.SelectedValue = "0";
                            pnlEditpetition.Visible = false;
                        }


                    }
                    else
                    {
                        P1.Visible = false;
                        pnlEditpetition.Visible = false;
                        // ddlpetitionappealEdit.SelectedValue = "0";
                    }
                }
                else //Schedule of hearing for appeal sections
                {
                    if (p_Var.dSet.Tables[0].Rows[0]["Appeal_PetitionID"] != null)
                    {
                        if (p_Var.dSet.Tables[0].Rows[0]["Appeal_PetitionID"].ToString() == "0")
                        {
                            p2.Visible = true;
                            ddlappealEdit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["Appeal_PetitionID"].ToString();
                            PAppealedit.Visible = false;
                        }
                        else
                        {
                            p2.Visible = true;
                            ddlappealEdit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["Appeal_PetitionID"].ToString();
                            PAppealedit.Visible = true;
                        }
                    }
                }
                pnlSubjectEdit.Visible = true;
                //txtSubjectEdit.Text = p_Var.dSet.Tables[0].Rows[0]["Subject"].ToString();
                txtSubjectEdit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Subject"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));

                if (p_Var.dSet.Tables[0].Rows[0]["Petition_id"] != DBNull.Value)
                {
                    ViewState["petitionid"] = p_Var.dSet.Tables[0].Rows[0]["Petition_id"];
                }

                if (p_Var.dSet.Tables[0].Rows[0]["soh_file"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["soh_file"].ToString() != "")
                {

                    ViewState["soh_file"] = p_Var.dSet.Tables[0].Rows[0]["soh_file"].ToString();
                }
                else
                {

                }
                PetitionOB newOB = new PetitionOB();
                PetitionBL newBL = new PetitionBL();
                newOB.soh_ID = Convert.ToInt32(sohid);

                newOB.StatusId = Convert.ToInt32(Session["Status_Id"]);
                if (Request.QueryString["DepttID"].ToString() == "1")
                {
                    newOB.PetitionType = Convert.ToInt16(ddlConnectionTypeEdit.SelectedValue);
                }
                else
                {
                    newOB.PetitionType = 3;
                }
                p_Var.dsFileName = petPetitionBL.get_ConnectedSOHEdit(newOB);

                DataSet dsetYear = new DataSet();
                StringBuilder sbuilderYear = new StringBuilder();
                dsetYear = newBL.getScheduleOfHearingYearForConnectionEdit(newOB);

                if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
                {
                    if (p_Var.dsFileName.Tables[0].Rows[0]["AppealId"] != DBNull.Value)
                    {
                        sbuilderYear.Remove(0, sbuilderYear.Length);
                        BindYearEdit(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal));
                        for (p_Var.i = 0; p_Var.i < dsetYear.Tables[0].Rows.Count; p_Var.i++)
                        {
                            foreach (ListItem li in ddlyearAppealedit.Items)
                            {
                                if (li.Value.ToString() == dsetYear.Tables[0].Rows[p_Var.i]["year"].ToString().Trim())
                                {
                                    li.Selected = true;
                                    sbuilderYear.Append(li.Text).Append(",");

                                }

                            }
                        }
                        bindApprovedAppealEdit(sbuilderYear.ToString());
                    }
                    if (p_Var.dsFileName.Tables[0].Rows[0]["AppealId"] == DBNull.Value)
                    {
                        if (p_Var.dSet.Tables[0].Rows[0]["PetitionType"].ToString() == "2")
                        {
                            BindYearEdit(2);
                        }
                        else
                        {
                            BindYearEdit(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
                        }
                        sbuilderYear.Remove(0, sbuilderYear.Length);
                        // ddlyearEdit.SelectedValue = p_Var.dsFileName.Tables[0].Rows[0]["Year"].ToString().Trim();
                        for (p_Var.i = 0; p_Var.i < dsetYear.Tables[0].Rows.Count; p_Var.i++)
                        {
                            foreach (ListItem li in ddlyearEdit.Items)
                            {
                                if (li.Value.ToString() == dsetYear.Tables[0].Rows[p_Var.i]["year"].ToString().Trim())
                                {
                                    li.Selected = true;
                                    sbuilderYear.Append(li.Text).Append(",");

                                }

                            }
                        }
                        ddlConnectionTypeEdit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["PetitionType"].ToString();
                        if (ddlConnectionTypeEdit.SelectedValue == "1")
                        {
                            ddlConnectionTypeEdit.SelectedValue = "1";

                            bindApprovedPetitionEdit(sbuilderYear.ToString());
                        }
                        else
                        {
                            ddlConnectionTypeEdit.SelectedValue = "2";

                            ViewState["reviewyear"] = sbuilderYear.ToString();

                            bindApprovedPetitionReviewEdit(sbuilderYear.ToString());
                        }
                    }

                    for (p_Var.i = 0; p_Var.i < p_Var.dsFileName.Tables[0].Rows.Count; p_Var.i++)
                    {
                        foreach (ListItem li in chklstPetitionEdit.Items)
                        {
                            if (li.Value.ToString() == p_Var.dsFileName.Tables[0].Rows[p_Var.i]["Petition_id"].ToString())
                            {
                                li.Selected = true;

                            }
                            else if (li.Value.ToString() == p_Var.dsFileName.Tables[0].Rows[p_Var.i]["ReviewId"].ToString())
                            {
                                li.Selected = true;
                            }

                        }

                        foreach (ListItem li in CheckBoxAppealEdit.Items)
                        {
                            if (li.Value.ToString() == p_Var.dsFileName.Tables[0].Rows[p_Var.i]["AppealId"].ToString().Trim())
                            {
                                li.Selected = true;


                            }
                        }
                    }
                }
                else
                {
                    if (Request.QueryString["depttID"] == "1")
                    {
                        div1.Visible = true;
                        pnlDropdownlistEdit.Visible = true;
                        divConnectAdd.Visible = false;
                        ddlConnectionTypeEdit.Enabled = true;
                        if (ddlConnectionTypeEdit.SelectedValue == "1")
                        {
                            BindYearEdit(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));

                            sbuilderYear.Remove(0, sbuilderYear.Length);
                            for (p_Var.i = 0; p_Var.i < dsetYear.Tables[0].Rows.Count; p_Var.i++)
                            {
                                foreach (ListItem li in ddlyearEdit.Items)
                                {
                                    if (li.Value.ToString() == dsetYear.Tables[0].Rows[p_Var.i]["year"].ToString().Trim())
                                    {
                                        li.Selected = true;
                                        sbuilderYear.Append(li.Text).Append(",");

                                    }

                                }
                            }
                            bindApprovedPetitionEdit(sbuilderYear.ToString());
                        }
                        else if (ddlConnectionTypeEdit.SelectedValue == "2")
                        {
                            BindYearEdit(Convert.ToInt32(ddlConnectionTypeEdit.SelectedValue));
                            sbuilderYear.Remove(0, sbuilderYear.Length);
                            for (p_Var.i = 0; p_Var.i < dsetYear.Tables[0].Rows.Count; p_Var.i++)
                            {
                                foreach (ListItem li in ddlyearEdit.Items)
                                {
                                    if (li.Value.ToString() == dsetYear.Tables[0].Rows[p_Var.i]["year"].ToString().Trim())
                                    {
                                        li.Selected = true;
                                        sbuilderYear.Append(li.Text).Append(",");

                                    }

                                }
                            }
                            bindApprovedPetitionReviewEdit(sbuilderYear.ToString());
                        }


                    }
                    else
                    {
                        div1.Visible = false;
                        divConnectAdd.Visible = true;
                        if (p_Var.dSet.Tables[0].Rows[0]["Appeal_PetitionID"].ToString() == "0")
                        {

                            PAppealedit.Visible = false;
                        }
                        else
                        {

                            PAppealedit.Visible = true;
                            BindYearEdit(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal));
                            sbuilderYear.Remove(0, sbuilderYear.Length);
                            for (p_Var.i = 0; p_Var.i < dsetYear.Tables[0].Rows.Count; p_Var.i++)
                            {
                                foreach (ListItem li in ddlyearAppealedit.Items)
                                {
                                    if (li.Value.ToString() == dsetYear.Tables[0].Rows[p_Var.i]["year"].ToString().Trim())
                                    {
                                        li.Selected = true;
                                        sbuilderYear.Append(li.Text).Append(",");

                                    }

                                }
                            }
                            bindApprovedAppealEdit(ddlyearAppealedit.SelectedValue.ToString());

                        }

                    }
                }

            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind approved Appeal number in dropDownlist

    public void bindApprovedAppeal(string year)
    {
        try
        {
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Remove(0, strBuilder.Length);
            foreach (ListItem li in ddlyearAppeal.Items)
            {
                if (li.Selected == true)
                {
                    p_Var.sbuilder.Append(li.Text).Append(",");
                }

            }
            List<string> list = new List<string>();

            foreach (ListItem li in CheckBoxAppeal.Items)
            {
                if (li.Selected == true)
                {
                    list.Add(li.Value);
                    strBuilder.Append(li.Text + ";");
                }

            }

            ViewState["MyList"] = list;
            petObject.year = p_Var.sbuilder.ToString();


            p_Var.dSetChildData = objappeal.Get_Appeal(petObject);
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {
                CheckBoxAppeal.DataSource = p_Var.dSetChildData;
                CheckBoxAppeal.DataTextField = "AppealNoValue";
                CheckBoxAppeal.DataValueField = "Appeal_Id";
                CheckBoxAppeal.DataBind();

            }
            else
            {
                CheckBoxAppeal.DataSource = p_Var.dSetChildData;
                CheckBoxAppeal.DataBind();

            }

            if (ViewState["MyList"] != null)
            {
                strBuilder.Remove(0, strBuilder.Length);
                List<string> list1 = ViewState["MyList"] as List<string>;
                foreach (ListItem li in CheckBoxAppeal.Items)
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
            ltrlSelectedAppeal.Text = strBuilder.ToString();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind approved Appeal number in dropDownlist in Edit Mode

    public void bindApprovedAppealEdit(string year)
    {
        try
        {
            pnlDropdownlistEdit.Visible = false;
            StringBuilder strBuilder = new StringBuilder();
            PAppealedit.Visible = true;
            foreach (ListItem li in ddlyearAppealedit.Items)
            {
                if (li.Selected == true)
                {
                    p_Var.sbuilder.Append(li.Text).Append(",");
                }

            }


            petObject.year = p_Var.sbuilder.ToString();
            p_Var.dSetChildData = objappeal.Get_Appeal(petObject);
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {
                strBuilder.Remove(0, strBuilder.Length);
                CheckBoxAppealEdit.DataSource = p_Var.dSetChildData;
                CheckBoxAppealEdit.DataTextField = "AppealNoValue";
                CheckBoxAppealEdit.DataValueField = "Appeal_Id";
                CheckBoxAppealEdit.DataBind();


                PetitionOB newOB = new PetitionOB();
                newOB.soh_ID = Convert.ToInt32(Request.QueryString["sohid"]);
                newOB.StatusId = Convert.ToInt16(Request.QueryString["Status"]);
                DataSet ds = new DataSet();
                ds = petPetitionBL.get_ConnectedSOHEdit(newOB);

                for (p_Var.i = 0; p_Var.i < ds.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in chklstPetitionEdit.Items)
                    {
                        if (li.Value.ToString() == ds.Tables[0].Rows[p_Var.i]["Petition_id"].ToString())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");
                        }
                        else if (li.Value.ToString() == ds.Tables[0].Rows[p_Var.i]["ReviewId"].ToString())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");
                        }

                    }

                    foreach (ListItem li in CheckBoxAppealEdit.Items)
                    {
                        if (li.Value.ToString() == ds.Tables[0].Rows[p_Var.i]["AppealId"].ToString().Trim())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");

                        }
                        //if (li.Selected == true)
                        //{
                        //    strBuilder.Append(li.Text + ";");
                        //}
                    }
                }

                ltrlAppealEdit.Text = strBuilder.ToString();
            }
            else
            {
                CheckBoxAppealEdit.DataSource = p_Var.dSetChildData;
                CheckBoxAppealEdit.DataBind();
                strBuilder.Remove(0, strBuilder.Length);
                ltrlAppealEdit.Text = "";

            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    #region Custom validator to validate extension of upload pdf files

    protected void CustvalidFileUplaod_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Get file name

        p_Var.ext = System.IO.Path.GetExtension(fileUploadPdf.PostedFile.FileName);
        p_Var.ext = p_Var.ext.ToLower();
        if (p_Var.ext == ".pdf")
        {
            p_Var.flag = miscellBL.GetActualFileType_pdf(fileUploadPdf.PostedFile.InputStream);
        }
        else
        {
            p_Var.flag = miscellBL.GetActualFileType(fileUploadPdf.PostedFile.InputStream);
        }

        if (p_Var.flag == true)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }

        string fileMultiple = string.Empty;
        HttpFileCollection hfc = Request.Files;
        bool strem = true;
        string str = string.Empty;
        Regex regex = new Regex(@"^[a-z | 0-9 | /,|,:;()#&]*$", RegexOptions.IgnoreCase);
        for (int i = 0; i < hfc.Count; i++)
        {
            if (i == 0)
            {
                if (p_Var.flag == true)
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            else
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
                    CustvalidFileUplaod.Text = "Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)";
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
                    CustvalidFileUplaod.Text = "Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)";
                    TextBox1.Text = "";
                }
            }
        }

        //p_Var.ext = System.IO.Path.GetExtension(fileUploadPdf.PostedFile.FileName);
        //p_Var.ext = p_Var.ext.ToLower();
        //if (p_Var.ext == ".pdf")
        //{
        //    p_Var.flag = miscellBL.GetActualFileType_pdf(fileUploadPdf.PostedFile.InputStream);
        //}
        //else
        //{
        //    p_Var.flag = miscellBL.GetActualFileType(fileUploadPdf.PostedFile.InputStream);
        //}

        //if (p_Var.flag == true)
        //{
        //    args.IsValid = true;
        //}
        //else
        //{
        //    args.IsValid = false;
        //}
    }

    #endregion

    #region Custom validator to validate extension of upload pdf files

    protected void CustvalidFileUplaod_ServerValidateEdit(object source, ServerValidateEventArgs args)
    {
        // Get file name
        string UploadFileName = fileUploadPdfEdit.PostedFile.FileName;

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
        //p_Var.ext = System.IO.Path.GetExtension(fileUploadPdfEdit.PostedFile.FileName);
        //p_Var.ext = p_Var.ext.ToLower();
        //if (p_Var.ext == ".pdf")
        //{
        //    p_Var.flag = miscellBL.GetActualFileType_pdf(fileUploadPdfEdit.PostedFile.InputStream);
        //}
        //else
        //{
        //    p_Var.flag = miscellBL.GetActualFileType(fileUploadPdfEdit.PostedFile.InputStream);
        //}

        //if (p_Var.flag == true)
        //{
        //    args.IsValid = true;
        //}
        //else
        //{
        //    args.IsValid = false;
        //}
    }

    #endregion

    //Area for all the dropDownlist events

    #region dropDownlist ddlConnectionType selectedIndexChanged events

    protected void ddlConnectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState.Remove("MyList");
        ltrlSelected.Text = "";
        if (ddlConnectionType.SelectedValue == "1")
        {
            BindYear(Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Petition));
            bindApprovedPetition("0");
        }
        else if (ddlConnectionType.SelectedValue == "2")
        {
            BindYear(Convert.ToInt32(ddlConnectionType.SelectedValue));
            bindApprovedPetitionReview("0");
        }

    }

    #endregion

    #region dropDownlist ddlConnectionTypeEdit selectedIndexChanged event

    protected void ddlConnectionTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlConnectionTypeEdit.SelectedValue == "1")
        {
            BindYearEdit(Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Petition));
            bindApprovedPetitionEdit("0");
        }
        else if (ddlConnectionTypeEdit.SelectedValue == "2")
        {
            BindYearEdit(Convert.ToInt32(ddlConnectionTypeEdit.SelectedValue));
            bindApprovedPetitionReviewEdit("0");
        }

    }

    #endregion

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            PDepartment.Visible = true;
            obj_userOB.ModuleId = Convert.ToInt16(Request.QueryString["ModuleID"]);
            obj_userOB.DepttId = Convert.ToInt16(Session["Dept_ID"]);
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

    #region function to bind year

    public void BindYear(int moduleid)
    {
        petObject.ModuleID = moduleid;
        p_Var.dSetChildData = objappeal.GetYear(petObject);
        if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToInt16(p_Var.dSetChildData.Tables[0].Rows[0]["moduleid"]) == Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition) || Convert.ToInt16(p_Var.dSetChildData.Tables[0].Rows[0]["moduleid"]) == 2)
            {
                ddlyear.DataSource = p_Var.dSetChildData;
                ddlyear.DataValueField = "year";
                ddlyear.DataTextField = "year";
                ddlyear.DataBind();
                // ddlyear.Items.Insert(0, new ListItem("Select One", "0"));

            }
            else
            {
                ddlyearAppeal.DataSource = p_Var.dSetChildData;
                ddlyearAppeal.DataValueField = "year";
                ddlyearAppeal.DataTextField = "year";
                ddlyearAppeal.DataBind();
                //  ddlyearAppeal.Items.Insert(0, new ListItem("Select One", "0"));
            }

        }
        else
        {
            ddlyear.DataSource = p_Var.dSetChildData;
            //ddlyear.DataValueField = "year";
            //ddlyear.DataTextField = "year";
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("Select One", "0"));

        }
    }
    #endregion

    #region function to bind year in edit mode

    public void BindYearEdit(int moduleid)
    {
        petObject.ModuleID = moduleid;
        p_Var.dSetCompare = objappeal.GetYear(petObject);


        if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToInt16(p_Var.dSetCompare.Tables[0].Rows[0]["moduleid"]) == Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition) || Convert.ToInt16(p_Var.dSetCompare.Tables[0].Rows[0]["moduleid"]) == Convert.ToInt16("2"))
            {
                ddlyearEdit.DataSource = p_Var.dSetCompare;
                ddlyearEdit.DataValueField = "year";
                ddlyearEdit.DataTextField = "year";
                ddlyearEdit.DataBind();
                //ddlyearEdit.Items.Insert(0, new ListItem("Select One", "0"));
            }
            else
            {
                ddlyearAppealedit.DataSource = p_Var.dSetCompare;
                ddlyearAppealedit.DataValueField = "year";
                ddlyearAppealedit.DataTextField = "year";
                ddlyearAppealedit.DataBind();
                //ddlyearAppealedit.Items.Insert(0, new ListItem("Select One", "0"));
            }
        }
    }

    #endregion

    #region dropDownlist ddlyear selectedIndexChanged events

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedValue != "0")
        {
            if (ddlConnectionType.SelectedValue == "1")
            {
                bindApprovedPetition(ddlyear.SelectedValue.ToString());
            }
            else if (ddlConnectionType.SelectedValue == "2")
            {
                bindApprovedPetitionReview(ddlyear.SelectedValue.ToString());
            }
        }
        else
        {
            if (ddlConnectionType.SelectedValue == "1")
            {
                bindApprovedPetition("0");
            }
            else if (ddlConnectionType.SelectedValue == "2")
            {
                bindApprovedPetitionReview("0");
            }
        }

    }

    #endregion

    #region dropDownlist ddlyearAppeal selectedIndexChanged events

    protected void ddlyearAppeal_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlyearAppeal.SelectedValue != "0")
        {
            bindApprovedAppeal(ddlyearAppeal.SelectedValue.ToString());
            pnlDropdownlist.Visible = false;
        }
    }

    #endregion
    //End

    #region dropDownlist ddlyearEdit selectedIndexChanged events

    protected void ddlyearEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyearEdit.SelectedValue != "0")
        {
            if (ddlConnectionTypeEdit.SelectedValue == "1")
            {
                bindApprovedPetitionEdit(ddlyearEdit.SelectedValue.ToString());
            }
            else if (ddlConnectionTypeEdit.SelectedValue == "2")
            {
                bindApprovedPetitionReviewEdit(ddlyearEdit.SelectedValue.ToString());
            }
        }
        else
        {
            if (ddlConnectionTypeEdit.SelectedValue == "1")
            {
                bindApprovedPetitionEdit("0");
            }
            else if (ddlConnectionTypeEdit.SelectedValue == "2")
            {
                bindApprovedPetitionReviewEdit("0");
            }
        }
    }

    #endregion

    #region dropDownlist ddlyearAppealedit selectedIndexChanged events

    protected void ddlyearAppealedit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyearAppealedit.SelectedValue != "0")
        {
            bindApprovedAppealEdit(ddlyearAppealedit.SelectedValue.ToString());
        }
    }

    #endregion

    #region Funtion to bind time in all dropdownlist of times

    public void bindTimeinDropdownlist()
    {
        p_Var.dSet = petPetitionBL.getTime();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {

            ddlmins.DataSource = p_Var.dSet;
            //ddlsecs.DataSource = p_Var.dSet;
            ddlmins.DataTextField = "mins";
            ddlmins.DataValueField = "mins";
            ddlmins.DataBind();
            ddlmins.SelectedValue = "30";
            //ddlsecs.DataTextField = "secs";
            //ddlsecs.DataValueField = "secs";
            //ddlsecs.DataBind();

        }
    }

    #endregion

    #region Funtion to bind time in all dropdownlist of times

    public void bindTimeinDropdownlistEdit()
    {
        p_Var.dSet = petPetitionBL.getTime();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlminsEdit.DataSource = p_Var.dSet;
            //ddlsecsEdit.DataSource = p_Var.dSet;
            ddlminsEdit.DataTextField = "mins";
            ddlminsEdit.DataValueField = "mins";
            ddlminsEdit.DataBind();
            //ddlsecsEdit.DataTextField = "secs";
            //ddlsecsEdit.DataValueField = "secs";
            //ddlsecsEdit.DataBind();

        }
    }

    #endregion

    #region Function to bind hours in all dropdownlist of hours

    public void bindHoursinDropdownlist()
    {
        p_Var.dSet = petPetitionBL.getHours();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlhours.DataSource = p_Var.dSet;

            ddlhours.DataTextField = "hours";
            ddlhours.DataValueField = "hours";
            ddlhours.DataBind();
            ddlhours.SelectedValue = "11";


        }
    }

    #endregion

    #region Function to bind hours in all dropdownlist of hours

    public void bindHoursinDropdownlistEdit()
    {
        p_Var.dSet = petPetitionBL.getHours();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlhoursEdit.DataSource = p_Var.dSet;

            ddlhoursEdit.DataTextField = "hours";
            ddlhoursEdit.DataValueField = "hours";
            ddlhoursEdit.DataBind();


        }
    }

    #endregion

    #region Function to bind hours in all dropdownlist of ampm

    public void bindampminDropdownlistEdit()
    {
        p_Var.dSet = petPetitionBL.getampm();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlampmEdit.DataSource = p_Var.dSet;

            ddlampmEdit.DataTextField = "am_pm";
            ddlampmEdit.DataValueField = "am_pm";
            ddlampmEdit.DataBind();


        }
    }

    #endregion

    #region Function to bind ampm in all dropdownlist of ampm

    public void bindampminDropdownlist()
    {
        p_Var.dSet = petPetitionBL.getampm();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlampm.DataSource = p_Var.dSet;

            ddlampm.DataTextField = "am_pm";
            ddlampm.DataValueField = "am_pm";
            ddlampm.DataBind();


        }
    }

    #endregion

    protected void ddlpetitionappeal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpetitionappeal.SelectedValue != "0")
        {
            if (ddlpetitionappeal.SelectedValue == "1")
            {
                pnlDropdownlist.Visible = true;
                PAppeal.Visible = false;
                BindYear(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
                if (ddlConnectionType.SelectedValue == "1")
                {
                    BindYear(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
                    bindApprovedPetition("0");
                }
                else if (ddlConnectionType.SelectedValue == "2")
                {
                    //BindYear(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
                    BindYear(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
                    bindApprovedPetitionReview("0");
                }
            }
        }
        else
        {
            PAppeal.Visible = false;
            pnlDropdownlist.Visible = false;
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue == "2")
        {
            PApeal.Visible = true;
            Ppetitionappeal.Visible = false;
            //Code to show and hide appeal grid
            txtVenue.Text = "Electricity Ombudsman Office";
            divConnectAdd1.Visible = false;
            divConnectAdd.Visible = true;
            ddlappeal.Items.Clear();
            ddlappeal.Items.Insert(0, new ListItem("Select", "0"));
            ddlappeal.Items.Insert(1, new ListItem("Appeal", "2"));
            ddlappeal.SelectedValue = "0";

            //End

        }
        else
        {

            PApeal.Visible = false;
            Ppetitionappeal.Visible = true;
            //Code to show and hide petition grid
            txtVenue.Text = "Court Room, HERC, Panchkula";
            divConnectAdd.Visible = false;
            divConnectAdd1.Visible = true;
            ddlpetitionappeal.Items.Clear();
            ddlpetitionappeal.Items.Insert(0, new ListItem("Select", "0"));
            ddlpetitionappeal.Items.Insert(1, new ListItem("Petition", "1"));
            ddlpetitionappeal.SelectedValue = "0";

            //End
        }
    }
    protected void ddlappeal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlappeal.SelectedValue != "0")
        {
            BindYear(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal));
            PAppeal.Visible = true;
            pnlDropdownlist.Visible = false;
            bindApprovedAppeal("0");
        }
        else
        {
            PAppeal.Visible = false;
            pnlDropdownlist.Visible = false;
        }

    }
    protected void CheckBoxAppeal_SelectedIndexChanged(object sender, EventArgs e)
    {
        string value = string.Empty;
        StringBuilder strBuilder = new StringBuilder();
        string result = Request.Form["__EVENTTARGET"];

        string[] checkedBox = result.Split('$');
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (CheckBoxAppeal.Items[index].Selected)
        {

            petObject.soh_ID = Convert.ToInt32(CheckBoxAppeal.Items[index].Value);
            p_Var.dSet = petPetitionBL.get_ConnectedSOHEdit(petObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_Var.i = 0; p_Var.i < p_Var.dSet.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in CheckBoxAppeal.Items)
                    {

                        if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["AppealId"].ToString())
                        {
                            li.Selected = true;
                            //strBuilder.Append(li.Text + ";");

                        }
                        if (li.Selected == true)
                        {
                            strBuilder.Append(li.Text + ";");
                        }
                    }

                }
                ltrlSelectedAppeal.Text = strBuilder.ToString();
            }
            else
            {
                strBuilder.Remove(0, strBuilder.Length);
                foreach (ListItem li in CheckBoxAppeal.Items)
                {
                    if (li.Selected == true)
                    {
                        strBuilder.Append(li.Text + ";");
                    }
                }
                // lblSelectedPetition.Visible = true;
                ltrlSelectedAppeal.Text = strBuilder.ToString();
            }
        }
        else
        {
            strBuilder.Remove(0, strBuilder.Length);
            foreach (ListItem li in CheckBoxAppeal.Items)
            {
                if (li.Selected == true)
                {
                    strBuilder.Append(li.Text + ";");
                }
            }
            // lblSelectedPetition.Visible = true;
            ltrlSelectedAppeal.Text = strBuilder.ToString();
        }

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

            petObject.soh_ID = Convert.ToInt32(chklstPetition.Items[index].Value);
            petObject.PetitionType = Convert.ToInt16(ddlConnectionType.SelectedValue);
            p_Var.dSet = petPetitionBL.get_ConnectedSOH_Edit(petObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_Var.i = 0; p_Var.i < p_Var.dSet.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in chklstPetition.Items)
                    {

                        if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["Petition_id"].ToString())
                        {
                            li.Selected = true;
                        }
                        //else if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["ReviewId"].ToString())
                        else if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["Connected_Petition_id"].ToString())
                        {
                            li.Selected = true;
                            // strBuilder.Append(li.Text + ";");
                        }

                    }

                }
            }
            //else
            //{
            strBuilder.Remove(0, strBuilder.Length);
            foreach (ListItem li in chklstPetition.Items)
            {
                if (li.Selected == true)
                {
                    strBuilder.Append(li.Text + ";");
                }
            }
            // lblSelectedPetition.Visible = true;

            //}
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
            // lblSelectedPetition.Visible = true;
            ltrlSelected.Text = strBuilder.ToString();
        }
    }
    protected void chklstPetitionEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        string value = string.Empty;
        StringBuilder strBuilder1 = new StringBuilder();
        string result = Request.Form["__EVENTTARGET"];

        string[] checkedBox = result.Split('$');
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (chklstPetitionEdit.Items[index].Selected)
        {

            petObject.soh_ID = Convert.ToInt32(chklstPetitionEdit.Items[index].Value);
            petObject.PetitionType = Convert.ToInt16(ddlConnectionType.SelectedValue);
            p_Var.dSet = petPetitionBL.get_ConnectedSOH_Edit(petObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_Var.i = 0; p_Var.i < p_Var.dSet.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in chklstPetitionEdit.Items)
                    {

                        if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["Petition_id"].ToString())
                        {
                            li.Selected = true;
                        }
                        //else if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["ReviewId"].ToString())
                        else if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["Connected_Petition_id"].ToString())
                        {
                            li.Selected = true;
                        }

                    }

                }
            }
            //else
            //{
            strBuilder1.Remove(0, strBuilder1.Length);
            foreach (ListItem li in chklstPetitionEdit.Items)
            {
                if (li.Selected == true)
                {
                    strBuilder1.Append(li.Text + ";");
                }
            }
            // lblSelectedPetition.Visible = true;
            ltrlPetitionEdit.Text = strBuilder1.ToString();
            //}
        }
        else
        {
            strBuilder1.Remove(0, strBuilder1.Length);
            foreach (ListItem li in chklstPetitionEdit.Items)
            {
                if (li.Selected == true)
                {
                    strBuilder1.Append(li.Text + ";");
                }
            }
            // lblSelectedPetition.Visible = true;
            ltrlPetitionEdit.Text = strBuilder1.ToString();
        }
    }
    protected void CheckBoxAppealEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        string value = string.Empty;
        StringBuilder strBuilder = new StringBuilder();
        string result = Request.Form["__EVENTTARGET"];

        string[] checkedBox = result.Split('$');
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (CheckBoxAppealEdit.Items[index].Selected)
        {

            petObject.soh_ID = Convert.ToInt32(CheckBoxAppealEdit.Items[index].Value);
            p_Var.dSet = petPetitionBL.get_ConnectedSOHEdit(petObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_Var.i = 0; p_Var.i < p_Var.dSet.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in CheckBoxAppealEdit.Items)
                    {

                        if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["AppealId"].ToString())
                        {
                            li.Selected = true;
                            //strBuilder.Append(li.Text + ";");

                        }
                        if (li.Selected == true)
                        {
                            strBuilder.Append(li.Text + ";");
                        }
                    }

                }
                ltrlAppealEdit.Text = strBuilder.ToString();
            }
            //else
            //{
            strBuilder.Remove(0, strBuilder.Length);
            foreach (ListItem li in CheckBoxAppealEdit.Items)
            {
                if (li.Selected == true)
                {
                    strBuilder.Append(li.Text + ";");
                }
            }
            // lblSelectedPetition.Visible = true;
            ltrlAppealEdit.Text = strBuilder.ToString();
            //}
        }
        else
        {
            strBuilder.Remove(0, strBuilder.Length);
            foreach (ListItem li in CheckBoxAppealEdit.Items)
            {
                if (li.Selected == true)
                {
                    strBuilder.Append(li.Text + ";");
                }
            }
            // lblSelectedPetition.Visible = true;
            ltrlAppealEdit.Text = strBuilder.ToString();
        }
    }

    #region function  bind with Repeator

    public void BindData(int sohId)
    {
        petObject.soh_ID = sohId;

        p_Var.dsFileName = petPetitionBL.getFileNameForSoh(petObject);
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

    #region datalist datalistFileName itemCommand event

    protected void datalistFileName_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("File"))
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            petObject.ConnectionID = id;
            p_Var.Result1 = petPetitionBL.UpdateFileStatusForSoh(petObject);
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

            bindScheuduleOfHearing(Convert.ToInt32(Request.QueryString["sohid"]));

        }
    }

    #endregion

    protected void datalistFileName_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

            Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");
            Label filename = (Label)e.Item.FindControl("lblFile");

            var link = e.Item.FindControl("lnkFileConnectedRemove") as LinkButton;
            var hiddenField = e.Item.FindControl("hiddenFieldSoh") as HiddenField;


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
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["sohid"]))
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
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["sohid"]))
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

            LinkButton lnkFileConnectedRemove = (LinkButton)e.Item.FindControl("lnkFileConnectedRemove");
            lnkFileConnectedRemove.OnClientClick = "javascript:return confirm('Are you sure you want to delete this file :- " + filename.Text + " ?')";

            p_Var.sbuilder.Append("<a href='" + ResolveUrl(p_Var.Path) + filename.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='Download File' width='15' alt='Download File' height=\"15\" /> " + "</a>");
            p_Var.sbuilder.Append("<hr />");
            ltrlDownload.Text = p_Var.sbuilder.ToString();
        }
    }

    protected void ddlpetitionappealEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpetitionappealEdit.SelectedValue != "0")
        {
            if (ddlpetitionappealEdit.SelectedValue == "1")
            {
                pnlEditpetition.Visible = true;
                // PAppeal.Visible = false;
                BindYear(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
                if (ddlConnectionTypeEdit.SelectedValue == "1")
                {
                    BindYear(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
                    bindApprovedPetitionEdit("0");
                }
                else if (ddlConnectionTypeEdit.SelectedValue == "2")
                {
                    //BindYear(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
                    BindYear(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
                    bindApprovedPetitionReviewEdit("0");
                }
            }
        }
        else
        {
            //PAppeal.Visible = false;
            // pnlDropdownlistEdit.Visible = false;
            pnlEditpetition.Visible = false;
        }
    }

    protected void ddlappealEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlappealEdit.SelectedValue != "0")
        {
            BindYearEdit(Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal));
            PAppealedit.Visible = true;
            pnlDropdownlistEdit.Visible = false;
            bindApprovedAppeal("0");
        }
        else
        {
            PAppealedit.Visible = false;
            pnlDropdownlistEdit.Visible = false;
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
