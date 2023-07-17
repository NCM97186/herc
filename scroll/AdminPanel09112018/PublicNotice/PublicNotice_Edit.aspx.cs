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
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


public partial class Auth_AdminPanel_PublicNotice_PublicNotice_Edit : CrsfBase //System.Web.UI.Page
{
    #region Variables declaration zone

    Project_Variables p_Var = new Project_Variables();
    UserOB usrObject = new UserOB();
    LinkBL lnkPubNoticeBL = new LinkBL();
    PetitionOB pubNoticeObject = new PetitionOB();
    PublicNoticeBL pubNoticeBL = new PublicNoticeBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    LinkOB objnew = new LinkOB();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
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
            lblModulename.Text = ": Edit  Public Notice";
            this.Page.Title = " Edit Public Notice: HERC";

            BindData(Convert.ToInt32(Request.QueryString["publicNoticeID"]));
            BtnUpdate.Visible = false;
      
            bindropDownlistLang(); // Get the Language privilage
            if (Request.QueryString["publicNoticeID"] != null)    // for Edit
            {
                
                PDepartment.Visible = false;
                Display(Request.QueryString["publicNoticeID"].ToString());
            }
            else
            {
                PDepartment.Visible = true;
                Get_Deptt_Name();
            }
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
    }

    #endregion

    //Area for all the buttons, linkButtons, imageButtons click event

    protected void lnkFileRemove_Click(object sender, EventArgs e)
    {

    }

    #region button btnUpdate click event to update public notice

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (Page.IsValid)
                {
                    Miscelleneous_BL obj_Miscel1 = new Miscelleneous_BL();
                    try
                    {
                        pubNoticeObject.ActionType = 2;
                        pubNoticeObject.TmpPublicNoticeID = Convert.ToInt32(Request.QueryString["publicNoticeID"]);
                        pubNoticeObject.PublicNoticeID = Convert.ToInt32(Request.QueryString["publicNoticeID"]);
                        pubNoticeObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
                        pubNoticeObject.Description = miscellBL.RemoveUnnecessaryHtmlTagHtml(FCKeditor1.Value);
                        pubNoticeObject.Title = txtTitle.Text;
                        pubNoticeObject.PlaceHolderSix = txtURLEdit.Text;
                        pubNoticeObject.PlaceHolderSeven = txtURLDescriptionEdit.Text;
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
                        pubNoticeObject.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);

                        pubNoticeObject.MetaDescription = txtMetaDescription.Text;
                        pubNoticeObject.MetaKeyWords = txtMetaKeyword.Text;
                        pubNoticeObject.MetaTitle = txtMetaTitle.Text;
                        pubNoticeObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;
                        pubNoticeObject.IpAddress = Miscelleneous_DL.getclientIP();
                        pubNoticeObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                        pubNoticeObject.PlaceHolderFive = HttpUtility.HtmlEncode(txtRemarks.Text.Replace(Environment.NewLine, "<br />"));// This is for remarks

                        pubNoticeObject.LangId = Convert.ToInt32(Session["Lanuage"]);
                        if (ViewState["DeptID"] != DBNull.Value)
                        {
                            pubNoticeObject.DepttId = Convert.ToInt32(ViewState["DeptID"]);
                        }
                        else
                        {
                            pubNoticeObject.DepttId = null;
                        }
                        if (pnlPetitionConnection.Visible == true)
                        {
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
                            pubNoticeObject.PublicNoticeID = p_Var.Result;
                            p_Var.Result1 = pubNoticeBL.deleteConnectedPetitionFromPublicNotice(pubNoticeObject);

                            foreach (ListItem li in chklstPetition.Items)
                            {
                                if (li.Selected == true)
                                {
                                    pubNoticeObject.PublicNoticeID = p_Var.Result;
                                    if (Convert.ToInt32(Session["Status_Id"]) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                                    {
                                        pubNoticeObject.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft);
                                    }
                                    else
                                    {
                                        pubNoticeObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
                                    }
                                    if (ddlConnectionType.SelectedValue == "1")
                                    {
                                        pubNoticeObject.ConnectedPetitionID = Convert.ToInt32(li.Value.ToString());
                                    }
                                    else
                                    {
                                        pubNoticeObject.RPId = Convert.ToInt32(li.Value.ToString());
                                    }
                                    Int32 cnt = li.Text.LastIndexOf(' ');
                                    pubNoticeObject.year = li.Text.Substring(cnt).Trim();
                                    if (lnkConnectedPetitionNo.Visible != false)
                                    {
                                        p_Var.Result1 = pubNoticeBL.insertPublicNoticewithPetition(pubNoticeObject);
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
                                        PetitionOB newObj = new PetitionOB();
                                        newObj.PublicNoticeID = p_Var.Result;
                                        newObj.ActionType = Convert.ToInt32(Module_ID_Enum.Project_Action_Type.insert);
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
                                            p_Var.Filename = miscellBL.getUniqueFileName(fileMultiple, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);
                                            hpf.SaveAs(Server.MapPath(p_Var.url + p_Var.Filename));
                                            newObj.FileName = p_Var.Filename;
                                            newObj.StartDate = System.DateTime.Now;
                                        }
                                        int Result2 = pubNoticeBL.insertConnectedPublicNoticeFiles(newObj);
                                    }
                                }


                                obj_audit.ActionType = "U";
                                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                                obj_audit.UserName = Session["UserName"].ToString();
                                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                                obj_audit.IpAddress = miscellBL.IpAddress();
                                obj_audit.Title = txtTitle.Text;
                                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                                Session["msg"] = "Public notice has been updated successfully.";
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

    #region button btnReset click event to reset public notice

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["publicNoticeID"] != null)
        {
            Display(Request.QueryString["publicNoticeID"].ToString());

        }
    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
        object refUrl = ViewState["RefUrl"];
        if (refUrl != null)
        {
            Response.Redirect((string)refUrl);
        }
    }

    #endregion

    //End

    //Area for all the custom validators to validate 

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
        Regex regex = new Regex(@"^[a-z | 0-9 | /,|,:;()#&]*$", RegexOptions.IgnoreCase);
        string str = string.Empty;
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

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            usrObject.DepttId = Convert.ToInt32(Session["Dept_ID"]);
            p_Var.dSet = lnkPubNoticeBL.ASP_Get_Deptt_Name(usrObject);
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

    #region Function to display data in edit mode

    public void Display(string tmpPublicNoticeID)
    {
        PLanguage.Visible = false;
        BtnUpdate.Visible = true;

        pubNoticeObject.TmpPublicNoticeID = Convert.ToInt32(tmpPublicNoticeID);
        pubNoticeObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
        p_Var.dSet = pubNoticeBL.DisplayPublicNoticeByID(pubNoticeObject);
        txtTitle.Text = p_Var.dSet.Tables[0].Rows[0]["Title"].ToString();
        txtMetaDescription.Text = p_Var.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
        txtURLEdit.Text = p_Var.dSet.Tables[0].Rows[0]["Placeholdersix"].ToString();
        txtURLDescriptionEdit.Text = p_Var.dSet.Tables[0].Rows[0]["Placeholderseven"].ToString();
        txtMetaTitle.Text = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
        txtMetaKeyword.Text = p_Var.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
        ddlMetaLang.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();
        if (p_Var.dSet.Tables[0].Rows[0]["PlaceholderFive"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["PlaceholderFive"].ToString() != "")
        {
            txtRemarks.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["PlaceholderFive"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
        }

        if (p_Var.dSet.Tables[0].Rows[0]["Start_Date"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["Start_Date"].ToString() != "")
        {
            txtstartdate.Text = miscellBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["Start_Date"].ToString());
        }
        else
        {
            txtstartdate.Text = "";
        }
        if (p_Var.dSet.Tables[0].Rows[0]["End_Date"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["End_Date"].ToString() != "")
        {
            txtendate.Text = miscellBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["End_Date"].ToString());
        }
        else
        {
            txtendate.Text = "";
        }
        if (p_Var.dSet.Tables[0].Rows[0]["PlaceHolderTwo"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["PlaceHolderTwo"].ToString() != "")
        {
            txtEmailID.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceHolderTwo"].ToString();
        }
        if (p_Var.dSet.Tables[0].Rows[0]["PlaceHolderThree"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["PlaceHolderThree"].ToString() != "")
        {
          txtMobileNo.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceHolderThree"].ToString();
        }
        FCKeditor1.Value = p_Var.dSet.Tables[0].Rows[0]["Description"].ToString();
     
       
        if (p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"] != DBNull.Value)
        {
            ViewState["DeptID"] = p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"].ToString();
        }
        else
        {
            ViewState["DeptID"] = DBNull.Value;
        }


        PetitionOB newOB = new PetitionOB();
        PublicNoticeBL newBL = new PublicNoticeBL();
        newOB.PublicNoticeID = Convert.ToInt32(tmpPublicNoticeID);
        p_Var.dSetChildData = newBL.getConnectedPubicNoticeEdit(newOB);

        if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
        {
            if (p_Var.dSetChildData.Tables[0].Columns.Contains("Petition_id"))
            {
                if (p_Var.dSetChildData.Tables[0].Rows[0]["Petition_id"].ToString() != null && p_Var.dSetChildData.Tables[0].Rows[0]["Petition_id"].ToString() != "")
                {
                    //newOB.RPId = Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["Petition_id"].ToString());
                    newOB.RPId = 1;
                    ddlConnectionType.SelectedValue = "1";
                }
            }
            else
            {
                //newOB.RPId = Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["ReviewId"].ToString());
                newOB.RPId = 2;
                ddlConnectionType.SelectedValue = "2";
            }
            DataSet dsetYear = new DataSet();
            StringBuilder sbuilderYear = new StringBuilder();
            dsetYear = pubNoticeBL.getPublicNoticYearForConnectionEdit(newOB);

            bindPetitionYearinDdl();
            for (p_Var.i = 0; p_Var.i < dsetYear.Tables[0].Rows.Count; p_Var.i++)
            {
                foreach (ListItem li in ddlYear.Items)
                {
                    if (li.Value.ToString() == dsetYear.Tables[0].Rows[p_Var.i]["year"].ToString().Trim())
                    {
                        li.Selected = true;
                        sbuilderYear.Append(li.Text).Append(",");

                    }
                }
            }

            newOB.year = sbuilderYear.ToString();

            getpetitionNumberForChkBoxConnection((Int16)newOB.RPId);
     
            ViewState["year"] = sbuilderYear.ToString();
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Remove(0, strBuilder.Length);
            for (p_Var.i = 0; p_Var.i < p_Var.dSetChildData.Tables[0].Rows.Count; p_Var.i++)
            {
                foreach (ListItem li in chklstPetition.Items)
                {
                    if (p_Var.dSetChildData.Tables[0].Columns.Contains("Petition_id"))
                    {
                        if (li.Value.ToString() == p_Var.dSetChildData.Tables[0].Rows[p_Var.i]["Petition_id"].ToString())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");
                        }
                    }
                    else
                    {
                        if (li.Value.ToString() == p_Var.dSetChildData.Tables[0].Rows[p_Var.i]["ReviewId"].ToString())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");
                        }
                    }

                }
            }
            ltrlSelected.Text = strBuilder.ToString();
            lnkConnectedPetitionNo.Visible = true;
            lnkConnectedPetition.Visible = true;
           
            pnlPetitionConnection.Visible = true;
            divConnectAdd.Visible = true;
        }
        else
        {
            bindPetitionYearinDdl();
            lnkConnectedPetition.Visible = true;
            lnkConnectedPetitionNo.Visible = false;
            pnlPetitionConnection.Visible = false;
            divConnectAdd.Visible = false;
        }

        p_Var.dSet = null;
    }

    #endregion 

    //End
    protected void lnkConnectedPetition_Click(object sender, EventArgs e)
    {
        lnkConnectedPetitionNo.Visible = true;
        lnkConnectedPetition.Visible = true;
        getpetitionNumberForChkBoxConnection();
        pnlPetitionConnection.Visible = true;
        divConnectAdd.Visible = true;
    }
    protected void lnkConnectedPetitionNo_Click(object sender, EventArgs e)
    {
        lnkConnectedPetition.Visible = true;
        lnkConnectedPetitionNo.Visible = false;
        getpetitionNumberForChkBoxConnection();
        pnlPetitionConnection.Visible = false;
        divConnectAdd.Visible = false;
    }

    #region Function to get Petition numbers for connections

    public void getpetitionNumberForChkBoxConnection(int id)
    {
        pubNoticeObject.RPId = id;

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
        if (list.Count > 0)
        {
            ViewState["MyList"] = list;
        }
        else
        {
            ViewState["MyList"] = ViewState["MyList"];
        }

        pubNoticeObject.year = p_Var.sbuilder.ToString();
        //pubNoticeObject.year = year;
        p_Var.dsFileName = pubNoticeBL.getPetitionNumberForConnection(pubNoticeObject);
        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
        {
            chklstPetition.DataSource = p_Var.dsFileName;
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

            PetitionOB newOB = new PetitionOB();
            PublicNoticeBL newBL = new PublicNoticeBL();
            StringBuilder strBuilder = new StringBuilder();
            if (ViewState["year"] != null && ViewState["year"].ToString() != "")
            {
                
                newOB.PublicNoticeID = Convert.ToInt32(Request.QueryString["publicNoticeID"]);
                newOB.PetitionType = Convert.ToInt32(ddlConnectionType.SelectedValue);
                DataSet dset = new DataSet();
                dset = newBL.getConnectedPubicNoticeEdit(newOB);


                if (dset.Tables[0].Rows.Count > 0)
                {
                    strBuilder.Remove(0, strBuilder.Length);
                    for (p_Var.i = 0; p_Var.i < dset.Tables[0].Rows.Count; p_Var.i++)
                    {
                        foreach (ListItem li in chklstPetition.Items)
                        {
                            if (ddlConnectionType.SelectedValue == "1")
                            {
                                if (dset.Tables[0].Columns.Contains("Petition_id"))
                                {
                                    if (li.Value.ToString() == dset.Tables[0].Rows[p_Var.i]["Petition_id"].ToString())
                                    {
                                        li.Selected = true;
                                        strBuilder.Append(li.Value + ";");
                                    }
                                }
                            }
                            else
                            {
                                if (dset.Tables[0].Columns.Contains("ReviewId"))
                                {
                                    if (li.Value.ToString() == dset.Tables[0].Rows[p_Var.i]["ReviewId"].ToString())
                                    {
                                        li.Selected = true;
                                        strBuilder.Append(li.Value + ";");
                                    }
                                }
                            }
                        }
                    }
                    //ltrlSelected.Text = strBuilder.ToString();
                }
               
            }

            if (ViewState["MyList"] != null)
            {
                List<string> list1 = ViewState["MyList"] as List<string>;

                string[] stringArray = strBuilder.ToString().Split(';');
                list1.AddRange(stringArray);
                list1 = list.Distinct().ToList();
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
                }

            }
            ltrlSelected.Text = strBuilder.ToString();   
        }
        else
        {
            ltrlSelected.Text = "";
            chklstPetition.DataSource = p_Var.dsFileName;
            chklstPetition.DataBind();
        }

        
    }

    #endregion

    #region Function to get Petition numbers for connections

    public void getpetitionNumberForChkBoxConnection()
    {
        p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
        pubNoticeObject.RPId = Convert.ToInt32(ddlConnectionType.SelectedValue);

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

        pubNoticeObject.year = p_Var.sbuilder.ToString();

        ////pubNoticeObject.year = ddlYear.SelectedValue;
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
        objnew.CatId = Convert.ToInt32(ddlConnectionType.SelectedValue);
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
        getpetitionNumberForChkBoxConnection(Convert.ToInt32(ddlConnectionType.SelectedValue.ToString()));
    }

    #region dropDownlist ddlConnectionType selectedIndexChanged events

    protected void ddlConnectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindPetitionYearinDdl();
        getpetitionNumberForChkBoxConnection();

        ltrlSelected.Text = "";
    }

    #endregion

    #region function  bind with Repeator

    public void BindData(Int32 publicnotice)
    {
        pubNoticeObject.PublicNoticeID = publicnotice;
        //petitionObject.Status = Convert.ToInt32(Session["Status_Id"]);
        p_Var.dsFileName = pubNoticeBL.getFileNameForPublicNotice(pubNoticeObject);
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
            pubNoticeObject.ConnectionID = id;
            p_Var.Result1 = pubNoticeBL.UpdateFileStatusForPublicNotices(pubNoticeObject);
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
          
            Display(Request.QueryString["publicNoticeID"].ToString());

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
            var hiddenField = e.Item.FindControl("hiddenFieldPublicNoticeID") as HiddenField;


            if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
            {
                if (link != null)
                {
                    if (Convert.ToInt32(Session["Status_Id"]) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        link.Visible = true;//if user has right then enabled else disabled
                    }
                    else
                    {
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["PublicNoticeID"]))
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

                    if (Convert.ToInt32(Session["Status_Id"]) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        link.Visible = false;//if user has right then enabled else disabled
                    }
                    else
                    {
                        // link.Visible = true;//if user has right then enabled else disabled
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["PublicNoticeID"]))
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

    protected void chklstPetition_SelectedIndexChanged(object sender, EventArgs e)
    {
        string value = string.Empty;
        StringBuilder strBuilder = new StringBuilder();
        string result = Request.Form["__EVENTTARGET"];

        string[] checkedBox = result.Split('$');
        Int32 index = Int32.Parse(checkedBox[checkedBox.Length - 1]);

        if (chklstPetition.Items[index].Selected)
        {

            pubNoticeObject.PublicNoticeID = Convert.ToInt32(chklstPetition.Items[index].Value);
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
