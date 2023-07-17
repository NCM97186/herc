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

public partial class Auth_AdminPanel_PublicNotice_PublicNoticeAdd_Edit : CrsfBase//System.Web.UI.Page
{
    //Area for all the variables declaration zone

    #region Variable declaration zone

    PetitionOB pubNoticeObject = new PetitionOB();
    LinkBL lnkPubNoticeBL = new LinkBL();
    UserOB usrObject = new UserOB();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Project_Variables p_Var = new Project_Variables();
    PublicNoticeBL pubNoticeBL = new PublicNoticeBL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    LinkOB objnew = new LinkOB();
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion 

    //End

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
      //  divConnectAdd.Visible = false;
        p_Var.url = "~/" + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Petition"].ToString() + "/" + ConfigurationManager.AppSettings["Public_Notice"].ToString() + "/"; 
        if (!IsPostBack)
        {
            displayMetaLang();
            DataSet dsprv = new DataSet();
            usrObject.RoleId = Convert.ToInt32(Session["Role_Id"]);
            usrObject.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_permissionBL.ASP_CheckPrivilagesALLForPermission(usrObject);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }

            Label lblModulename = Master.FindControl("lblModulename") as Label;
            lblModulename.Text = ": Add  Public Notice";
            this.Page.Title = " Add Public Notice: HERC";

            bindPetitionYearinDdl();
            divConnectAdd.Visible = false;
            BtnUpdate.Visible = false;
           
            bindropDownlistLang(); // Get the Language privilage
            if (Request.QueryString["Temp_Link_Id"] != null)    // for Edit
            {
               
            }
            else
            {
              
            }
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
    }

    #endregion

    //Area for all the custom validators to validate 

    #region Custom validator to validate extension of upload Images

    //protected void CustvalidUplaodImage_ServerValidate(object source, ServerValidateEventArgs args)
    //{
    //    // Get file name
    //    p_Var.ext = System.IO.Path.GetExtension(FileUploadImage.PostedFile.FileName);
    //    p_Var.ext = p_Var.ext.ToLower();

    //    if (p_Var.ext == ".jpg" || p_Var.ext == ".png" || p_Var.ext == ".jpeg" || p_Var.ext == ".gif")
    //    {
    //        p_Var.flag = miscellBL.GetActualFileType(FileUploadImage.PostedFile.InputStream);

    //    }
    //    if (p_Var.flag == true)
    //    {
    //        args.IsValid = true;
    //    }
    //    else
    //    {
    //        args.IsValid = false;
    //    }
    //}

    #endregion

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
        string multiple = string.Empty;
        HttpFileCollection hfc = Request.Files;
        string str=string.Empty;
        bool strem = true;
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

                strem = miscellBL.GetActualFileType_pdf(hpf.InputStream);
                if (strem == true)
                {

                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                    //break;
                }

            }
            if (i == 0)
            {
                Match match = regex.Match(TextBox1.Text);
                if (match.Success==true)
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
                if (match.Success==true)
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

     
    }

    #endregion

    //End

    //Area for all the buttons, linkButtons, imageButtons clicked events

    #region button btnSubmit click event to submit public notice

    protected void BtnSubmit_Click(object sender, EventArgs e)
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

                        pubNoticeObject.ActionType = 1;
                        pubNoticeObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
                        pubNoticeObject.Description = miscellBL.RemoveUnnecessaryHtmlTagHtml(FCKeditor1.Value);
                        pubNoticeObject.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        pubNoticeObject.PlaceHolderSix = txtURL.Text;
                        pubNoticeObject.PlaceHolderSeven = txtURLDescription.Text;
                        pubNoticeObject.Title = txtTitle.Text;
                        if (txtstartdate.Text != null && txtstartdate.Text != "")
                        {
                            pubNoticeObject.StartDate = Convert.ToDateTime(miscellBL.getDateFormat(txtstartdate.Text));
                        }
                        else
                        {
                            pubNoticeObject.StartDate = null;
                        }
                        if (txtendate.Text != null && txtendate.Text != "")
                        {
                            pubNoticeObject.EndDate = Convert.ToDateTime(miscellBL.getDateFormat(txtendate.Text));
                        }
                        else
                        {
                            pubNoticeObject.EndDate = null;
                        }

                        //This code added on date 16 Sep 2013 by ruchi

                        pubNoticeObject.ApplicantMobileNo = txtMobileNo.Text;
                        pubNoticeObject.ApplicantEmail = txtEmailID.Text;

                        //End

                        pubNoticeObject.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);   //should be change according to the module
                        pubNoticeObject.InsertedBy = Convert.ToInt32(Session["User_Id"]);
                        pubNoticeObject.IpAddress = Miscelleneous_DL.getclientIP();
                        pubNoticeObject.MetaDescription = txtMetaDescription.Text;
                        pubNoticeObject.MetaKeyWords = txtMetaKeyword.Text;
                        pubNoticeObject.MetaTitle = txtMetaTitle.Text;
                        pubNoticeObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;
                        pubNoticeObject.PlaceHolderFive = HttpUtility.HtmlEncode(txtRemarks.Text.Replace(Environment.NewLine, "<br />"));// This is for remarks


                        pubNoticeObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;  //draft

                        if (pnlPetitionConnection.Visible == true)
                        {
                            //pubNoticeObject.PetitionType = Convert.ToInt32(ddlConnectionType.SelectedValue);
                            pubNoticeObject.PetitionType = 0;
                            foreach (ListItem li in chklstPetition.Items)
                            {
                                if (li.Selected == true)
                                {
                                    pubNoticeObject.PetitionType = Convert.ToInt32(ddlConnectionType.SelectedValue);
                                    break;

                                }
                                else
                                {
                                    pubNoticeObject.PetitionType = 0;
                                }
                            }
                        }
                        else
                        {
                            pubNoticeObject.PetitionType = 0;
                        }

                        p_Var.Result = pubNoticeBL.PublicNoticeInsertUpdate(pubNoticeObject);

                        if (p_Var.Result > 0)
                        {
                            foreach (ListItem li in chklstPetition.Items)
                            {
                                if (li.Selected == true)
                                {
                                    pubNoticeObject.PublicNoticeID = p_Var.Result;
                                    // pubNoticeObject.year = ddlYear.SelectedValue.ToString();
                                    int cnt = li.Text.LastIndexOf(' ');
                                    pubNoticeObject.year = li.Text.Substring(cnt).Trim();
                                    if (ddlConnectionType.SelectedValue == "1")
                                    {
                                        pubNoticeObject.ConnectedPetitionID = Convert.ToInt32(li.Value.ToString());
                                    }
                                    else
                                    {
                                        pubNoticeObject.RPId = Convert.ToInt32(li.Value.ToString());
                                    }
                                    p_Var.Result1 = pubNoticeBL.insertPublicNoticewithPetition(pubNoticeObject);

                                }


                            }
                            if (p_Var.Result > 0)
                            {
                                // This is for connected files on date 26 feb 2013

                                //if (fileUploadPdf.PostedFile != null && fileUploadPdf.PostedFile.ContentLength != 0)
                                //{

                                string fileMultiple = string.Empty;
                                HttpFileCollection hfc = Request.Files;

                                for (int i = 0; i < hfc.Count; i++)
                                {
                                    HttpPostedFile hpf = hfc[i];

                                    fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
                                    if (fileMultiple != null && fileMultiple != "")
                                    {
                                        PetitionOB newObj = new PetitionOB();
                                        newObj.PublicNoticeID = p_Var.Result;
                                        newObj.ActionType = Convert.ToInt32(Module_ID_Enum.Project_Action_Type.insert);
                                        newObj.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);
                                        //newObj.PetitionDate = Convert.ToDateTime(miscellBL.getDateFormat(txtPetitionDate.Text.ToString()));
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
                                            //newObj.PetitionFile = "P_" + p_Var.Filename;
                                            newObj.FileName = p_Var.Filename;
                                        }

                                        newObj.StartDate = System.DateTime.Now;

                                        int Result2 = pubNoticeBL.insertConnectedPublicNoticeFiles(newObj);
                                    }
                                }

                                //}

                                obj_audit.ActionType = "I";
                                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                                obj_audit.UserName = Session["UserName"].ToString();
                                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                                obj_audit.IpAddress = miscellBL.IpAddress();
                                obj_audit.Title = txtTitle.Text;
                                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);


                                Session["msg"] = "Public notice has been submitted successfully.";
                                Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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

    #region button btnUpdate click event to update public notice

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

    }

    #endregion

    #region button btnReset click event to reset public notice

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtendate.Text = "";
        txtstartdate.Text = "";
        txtTitle.Text = "";
        FCKeditor1.Value = "";
    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
         Response.Redirect(ResolveUrl("~/auth/adminpanel/PublicNotice/") + "PublicNoticeDisplay.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));
    }

    #endregion

    #region linkButton lnkImageRemove click event to remove images

    protected void lnkImageRemove_Click(object sender, EventArgs e)
    {

    }

    #endregion

    #region linkButton lnkFileRemove click event to remove files

    protected void lnkFileRemove_Click(object sender, EventArgs e)
    {

    }

    #endregion

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

    //End

    //Area for all the user-defined functions

    #region Function to bind language in dropDownlist according to permission

    public void bindropDownlistLang()
    {
        try
        {
            Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
            Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
            usrObject.RoleId = Convert.ToInt32(Session["Role_Id"]);
            usrObject.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            p_Var.dSet = miscDdlLanguage.getLanguagePermission(usrObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                //UserOB usrObject = new UserOB();
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


    #region Function to get Petition numbers for connections

    public void getpetitionNumberForChkBoxConnection()
    {
        p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
        pubNoticeObject.RPId = Convert.ToInt32(ddlConnectionType.SelectedValue);
        StringBuilder strBuilder = new StringBuilder();
        strBuilder.Remove(0, strBuilder.Length);
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
                strBuilder.Append(li.Text + ";");
            }

        }

        ViewState["MyList"] = list;

        pubNoticeObject.year = p_Var.sbuilder.ToString();


       //// pubNoticeObject.year = ddlYear.SelectedValue;
        p_Var.dSet = pubNoticeBL.getPetitionNumberForConnection(pubNoticeObject);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            chklstPetition.DataSource = p_Var.dSet;
            if (pubNoticeObject.RPId == 1)
            {
                chklstPetition.DataValueField = "Petition_id";
                chklstPetition.DataTextField = "PROValue";
            }
            else
            {
                chklstPetition.DataValueField = "RP_Id";
                chklstPetition.DataTextField = "RPValue";
            }
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
            strBuilder.Remove(0, strBuilder.Length);
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
            
        }
        ltrlSelected.Text = strBuilder.ToString();
    }

    #endregion

   

    #region Function to upload files

    private bool Upload_File(ref string filename)
    {

        Miscelleneous_BL miscellFileBL = new Miscelleneous_BL();
        try
        {
            p_Var.Filename = fileUploadPdf.FileName;
            p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(fileUploadPdf.FileName);
            p_Var.ext = Path.GetExtension(fileUploadPdf.FileName);
            //For Unique File Name
            filename = miscellFileBL.getUniqueFileName(p_Var.Filename, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);

            fileUploadPdf.PostedFile.SaveAs(Server.MapPath(p_Var.url) + filename);
        }
        catch
        {
            p_Var.uploadStatus = false;
        }
        return p_Var.uploadStatus;
    }

    #endregion 

    //End

    #region linkButton lnkMultipleUpload click event to upload files

    //protected void lnkMultipleUpload_Click(object sender, EventArgs e)
    //{
    //    //this.ModalPopupExtender1.Show();
    //}

    #endregion

    #region button btnReply click event to reply query

    //protected void btnReply_Click(object sender, EventArgs e)
    //{
    //    //MajorActivityBL mActivityBL = new MajorActivityBL();
    //    //MajorActivityBL mActivityBL1 = new MajorActivityBL();
    //    //MajorActivityBL mActivityBL2 = new MajorActivityBL();

    //    //linkObject.ActionType = 1;
    //    //// linkObject.ActionDone = "Delete";
    //    //linkObject.LidTemp = Convert.ToInt32(ViewState["tempID"]);
    //    //linkObject.Remarks = txtRemarks.Text;
    //    //dSet = mActivityBL1.getTemp_ID_FROM_TEMP(linkObject);
    //    //if (dSet.Tables[0].Rows[0]["LID"] != DBNull.Value)
    //    //{
    //    //    linkObject.LId = Convert.ToInt32(dSet.Tables[0].Rows[0]["LID"]);
    //    //}
    //    //linkObject.ModuleId = Convert.ToInt32(dSet.Tables[0].Rows[0]["ModuleID"]);
    //    //linkObject.LangId = Convert.ToInt32(dSet.Tables[0].Rows[0]["LangID"]);
    //    //linkObject.TypeId = Convert.ToInt32(dSet.Tables[0].Rows[0]["TypeID"]);
    //    //linkObject.Name = Convert.ToString(dSet.Tables[0].Rows[0]["Name"]);
    //    //linkObject.Subject = Convert.ToString(dSet.Tables[0].Rows[0]["Subject"]);
    //    //linkObject.Details = Convert.ToString(dSet.Tables[0].Rows[0]["Details"]);
    //    //linkObject.StartDate = Convert.ToDateTime(dSet.Tables[0].Rows[0]["Start_Date"]);
    //    //if (dSet.Tables[0].Rows[0]["End_Date"] != DBNull.Value && dSet.Tables[0].Rows[0]["End_Date"] != "")
    //    //{
    //    //    linkObject.EndDate = Convert.ToDateTime(dSet.Tables[0].Rows[0]["End_Date"]);
    //    //}
    //    //linkObject.FilePhoto = Convert.ToString(dSet.Tables[0].Rows[0]["File_Photo"]);
    //    //linkObject.FileName = Convert.ToString(dSet.Tables[0].Rows[0]["FileName"]);
    //    ////linkObject.EntryBy = Convert.ToInt32(Session["UserID"]);
    //    //linkObject.EntryBy = Convert.ToInt32(dSet.Tables[0].Rows[0]["entry_userID"]);
    //    //linkObject.ApproveUserId = Convert.ToInt32(Session["UserID"]);
    //    //linkObject.ApproveUserDate = System.DateTime.Now;
    //    //linkObject.EntryDate = System.DateTime.Now;
    //    //linkObject.IPAddress = miscelBL.IpAddress();

    //    //mActivityBL2.insert_in_History(linkObject);

    //    //Result = mActivityBL.deleteActivityAuditTrail(linkObject);
    //    //if (Result > 0)
    //    //{
    //    //    Session["msg"] = "Activity has been deleted successfully.";
    //    //    Session["Redirect"] = "~/AdminPanel/MajorActivity/ViewAllActivity.aspx";
    //    //    Response.Redirect("~/AdminPanel/ConfirmationPage.aspx");
    //    //    //  Response.Redirect("~/AdminPanel/ConfirmationPage.aspx?msg=Activity Deleted Successfully..!&Redirect=~/AdminPanel/MajorActivity/ViewAllActivity.aspx");
    //    //}

    //}

    #endregion


    #region linkButton lnkConnectedPetitionNo click event

    protected void lnkConnectedPetitionNo_Click(object sender, EventArgs e)
    {
        lnkConnectedPetition.Visible = true;
        lnkConnectedPetitionNo.Visible = false;
        pnlPetitionConnection.Visible = false;
        divConnectAdd.Visible = false;
        ltrlSelected.Text = "";
    }

    #endregion

    #region Function to bind petition Year

    public void bindPetitionYearinDdl()
    {
        objnew.CatId =Convert.ToInt32(ddlConnectionType.SelectedValue);
        p_Var.dSet = pubNoticeBL.GetYearPetition_Admin(objnew);
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

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getpetitionNumberForChkBoxConnection();
    }

    #region dropDownlist ddlConnectionType selectedIndexChanged events

    protected void ddlConnectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
          bindPetitionYearinDdl();
          getpetitionNumberForChkBoxConnection();
          ltrlSelected.Text = "";
        ////////if (ddlConnectionType.SelectedValue == "1")
        ////////{
        ////////    BindYear(Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Petition));
        ////////    bindApprovedPetition("0");
        ////////}
        ////////else if (ddlConnectionType.SelectedValue == "2")
        ////////{
        ////////    BindYear(Convert.ToInt32(ddlConnectionType.SelectedValue));
        ////////    bindApprovedPetitionReview("0");
        ////////}
        
    }

    #endregion


    protected void chklstPetition_SelectedIndexChanged(object sender, EventArgs e)
    {
        string value = string.Empty;
        StringBuilder strBuilder = new StringBuilder();
        string result = Request.Form["__EVENTTARGET"];

        string[] checkedBox = result.Split('$');
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (chklstPetition.Items[index].Selected)
        {
            pubNoticeObject.PublicNoticeID = Convert.ToInt32(chklstPetition.Items[index].Value);
            pubNoticeObject.PetitionType = Convert.ToInt16(ddlConnectionType.SelectedValue);
            p_Var.dSet = pubNoticeBL.get_ConnectedPubicNotice_Edit(pubNoticeObject);
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

                        else if (li.Value.ToString() == p_Var.dSet.Tables[0].Rows[p_Var.i]["Connected_Petition_id"].ToString())
                        {
                            li.Selected = true;
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
            ltrlSelected.Text = strBuilder.ToString();
            // }
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
