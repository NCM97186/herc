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

public partial class Auth_AdminPanel_MixModules_Add_Modules : CrsfBase //System.Web.UI.Page
{
    #region Variable declaration zone

    LinkOB obj_LinkOB = new LinkOB();
    LinkBL obj_LinkBL = new LinkBL();
    UserOB obj_userOB = new UserOB();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    MixModuleBL mixModuleBL = new MixModuleBL();
    Project_Variables p_Var = new Project_Variables();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    HtmlSanitizer removerBL = new HtmlSanitizer();
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.imageUrl = "~/" + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Image"].ToString() + "/";
        p_Var.url = "~/" + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/";

        if (Session["Role_Id"] == null || Session["Role_Id"].ToString() == "0" || Session["Role_Id"].ToString() == "")
        {
            Session.Abandon();
            Response.Redirect("~/Auth/AdminPanel/Login.aspx");
        }
        if (!IsPostBack)
        {
            displayMetaLang();
            DataSet dsprv = new DataSet();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            if (Convert.ToInt32(Request.QueryString["ModuleId"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations))
            {
                obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations);
            }
            else
            {
                obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            }

            dsprv = obj_permissionBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }

            BtnUpdate.Visible = false;
            LblOldFile.Visible = false;
            lnkFileRemove.Visible = false;
            lblRegulationNumber.Text = "Select Regulation Number";
            lblRegulationNumber.ForeColor = System.Drawing.Color.Red;
            lblTxtRegulations.Text = "Regulation Number";
            lblTxtRegulations.ForeColor = System.Drawing.Color.Red;
            bindropDownlistLang();
            Get_Module_Name();
            displayModuleName(20);
            if (Request.QueryString["Temp_Link_Id"] != null)    // for Edit
            {


                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Edit  Regulations/Codes/Standards/Policies/Guidelines";
                this.Page.Title = "Edit Regulations/Codes/Standards/Policies/Guidelines: HERC";


                ddlamendment.Enabled = false;
                LblOldFile.Visible = true;
                lnkFileRemove.Visible = true;
                PDepartment.Visible = false;
                P1.Visible = true;
                Display(Request.QueryString["Temp_Link_Id"].ToString());
                displayModuleName(Convert.ToInt32(ddlModules.SelectedValue));
                ddlModules.Enabled = false;
            }
            else
            {

                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Add  Regulations/Codes/Standards/Policies/Guidelines";
                this.Page.Title = "Add Regulations/Codes/Standards/Policies/Guidelines: HERC";

                ddlamendment.Enabled = true;
                PDepartment.Visible = true;
                P1.Visible = true;
                PModuleName.Visible = false;
                Get_Deptt_Name();
            }
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
    }

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
            throw;
        }
    }

    #endregion

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {

            p_Var.dSet = mixModuleBL.getDepartmentName();
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = p_Var.dSet;
                ddlDepartment.DataValueField = "Deptt_Id";
                ddlDepartment.DataTextField = "Deptt_Name";
                ddlDepartment.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function bind module name in dropDownlist

    public void Get_Module_Name()
    {
        try
        {
            p_Var.dSet = mixModuleBL.getModuleName();
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlModules.DataSource = p_Var.dSet;
                ddlModules.DataValueField = "Module_Id";
                ddlModules.DataTextField = "Module_name";
                ddlModules.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }
    protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Get file name
        p_Var.ext = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
        p_Var.ext = p_Var.ext.ToLower();
        int count = FileUpload2.PostedFile.FileName.Split('.').Length - 1;
        if (p_Var.ext == ".pdf" && count == 1)
        {
            p_Var.flag = miscellBL.GetActualFileType_pdf(FileUpload2.PostedFile.InputStream);
        }
        else
        {
            args.IsValid = false;
            return;
            // p_Var.flag = miscellBL.GetActualFileType(fileUploadPdf.PostedFile.InputStream);
        }

        if (p_Var.flag == true)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
            return;
        }
    }
    protected void lnkImageRemove_Click(object sender, EventArgs e)
    {

    }
    protected void lnkFileRemove_Click(object sender, EventArgs e)
    {
        lblFileName.Visible = false;
        LblOldFile.Visible = false;
        lnkFileRemove.Visible = false;
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
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
                    Add_Module();
                }
            }
        }
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
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
                try
                {
                    if (Page.IsValid)
                    {
                        Update_Data();
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Temp_Link_Id"] != null)
        {
            Display(Request.QueryString["Temp_Link_Id"].ToString());

        }
        else
        {
            txtendate.Text = "";
            txtDetail.Text = "";
            txtstartdate.Text = "";
            txtRegulationNo.Text = "";
            // FCKeditor1.Value = "";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/MixModules/") + "Module_Display.aspx?ModuleID=" + Convert.ToInt32(Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Modules)));
    }


    #region Function To Add Details

    public void Add_Module()
    {
        try
        {
            if (Page.IsValid)
            {
                obj_LinkOB.ActionType = 1;
                obj_LinkOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);

                obj_LinkOB.NAME = miscellBL.RemoveUnnecessaryHtmlTagHtml(txtDetail.Text);

                obj_LinkOB.MateDescription = txtMetaDescription.Text;
                obj_LinkOB.MetaKeywords = txtMetaKeyword.Text;
                obj_LinkOB.MetaTitle = txtMetaTitle.Text;
                obj_LinkOB.MetaLanguage = ddlMetaLang.SelectedValue;

                if (txtRegulationNo.Text != "" && txtRegulationNo.Text != null)
                {
                    obj_LinkOB.regulationNumber = Convert.ToInt32(txtRegulationNo.Text.Trim());
                }
                else if (txtStandard.Text != "" && txtStandard.Text != null)
                {
                    obj_LinkOB.regulationNumber = Convert.ToInt32(txtStandard.Text.Trim());
                }
                else
                {
                    obj_LinkOB.regulationNumber = 0;
                }


                if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations) || Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Codes) || Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Standard) || Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Policies) || Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Guidelines))
                {

                    if (ddlRegulationNumber.SelectedValue != "0" && ddlRegulationNumber.SelectedValue.ToString() != "")
                    {
                        obj_LinkOB.RegulationNoAmbendment = Convert.ToInt32(ddlRegulationNumber.SelectedValue);
                        obj_LinkOB.regulationNumber = null;
                    }
                    obj_LinkOB.AmbedmentID = Convert.ToInt32(ddlamendment.SelectedValue);

                }
                else
                {
                    obj_LinkOB.AmbedmentID = null;
                    obj_LinkOB.RegulationNoAmbendment = null;
                }


                obj_LinkOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
                obj_LinkOB.URL = txtLinks.Text;
                if (txtstartdate.Text != null && txtstartdate.Text != "")
                {
                    obj_LinkOB.StartDate = null;
                }
                else
                {
                    obj_LinkOB.StartDate = null;
                }

                if (txtendate.Text != null && txtendate.Text != "")
                {
                    obj_LinkOB.EndDate = null;
                }
                else
                {
                    obj_LinkOB.EndDate = null;
                }

                obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);   //should be change according to the module
                obj_LinkOB.UserID = Convert.ToInt32(Session["User_Id"]);
                obj_LinkOB.IpAddress = Miscelleneous_DL.getclientIP();
                obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;  //draft

                if (Upload_File(ref p_Var.Filename))
                {
                    if (p_Var.Filename != null)
                    {

                        obj_LinkOB.FileName = p_Var.Filename.ToString();
                    }
                    else
                    {
                        obj_LinkOB.FileName = null;
                    }

                }
                else
                {
                    obj_LinkOB.FileName = null;
                }
                obj_LinkOB.InsertedBy = Convert.ToInt32(Session["User_Id"]);
                if (txtDetail.Text == "")
                {
                    string script = "alert(\"Please enter description.\");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                  "ServerControlScript", script, true);

                }
                else
                {
                    p_Var.Result = mixModuleBL.ASP_Links_Insert(obj_LinkOB);
                }


                if (p_Var.Result > 0)
                {

                    obj_audit.ActionType = "I";
                    obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                    obj_audit.UserName = Session["UserName"].ToString();
                    obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                    obj_audit.IpAddress = miscellBL.IpAddress();
                    obj_audit.status = "New Record";
                    obj_audit.Title = ddlDepartment.SelectedItem.ToString() + ", " + ddlModules.SelectedItem.ToString() + ", " + HttpUtility.HtmlDecode(txtDetail.Text).Replace("<p>", "").Replace("</p>", "");
                    obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                    Session["msg"] = "Record has been submitted successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
        }
        catch
        {
            return;
            throw;
        }

    }

    #endregion

    #region Function to update the data

    public void Update_Data()
    {
        LinkOB obj_LinkOB2 = new LinkOB();
        lnkFileRemove.Visible = true;
        //lnkImageRemove.Visible = true;
        Miscelleneous_BL obj_Miscel1 = new Miscelleneous_BL();
        try
        {
            obj_LinkOB2.ActionType = 2;
            obj_LinkOB2.TempLinkId = Convert.ToInt32(Request.QueryString["Temp_Link_Id"]);
            obj_LinkOB2.StatusId = Convert.ToInt32(Session["Status_Id"]);
            obj_LinkOB2.OldLinkId = Convert.ToInt32(Request.QueryString["Temp_Link_Id"]);

            obj_LinkOB2.MateDescription = txtMetaDescription.Text;
            obj_LinkOB2.MetaKeywords = txtMetaKeyword.Text;
            obj_LinkOB2.MetaTitle = txtMetaTitle.Text;
            obj_LinkOB2.MetaLanguage = ddlMetaLang.SelectedValue;
            if (txtRegulationNo.Text != "" && txtRegulationNo.Text != null)
            {
                obj_LinkOB2.regulationNumber = Convert.ToInt32(txtRegulationNo.Text.Trim());
            }
            else if (txtStandard.Text != "" && txtStandard.Text != null)
            {
                obj_LinkOB2.regulationNumber = Convert.ToInt32(txtStandard.Text.Trim());
            }
            else
            {
                obj_LinkOB2.regulationNumber = 0;
            }


            if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations) || Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Codes) || Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Standard) || Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Policies) || Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Guidelines))
            {

                if (ddlRegulationNumber.SelectedValue != "0" && ddlRegulationNumber.SelectedValue.ToString() != "")
                {
                    obj_LinkOB2.RegulationNoAmbendment = Convert.ToInt32(ddlRegulationNumber.SelectedValue);
                    obj_LinkOB2.regulationNumber = null;
                    obj_LinkOB2.AmbedmentID = Convert.ToInt32(ddlamendment.SelectedValue);
                }
                else
                {
                    if (txtRegulationNo.Text != "" && txtRegulationNo.Text != null)
                    {
                        obj_LinkOB.regulationNumber = Convert.ToInt32(txtRegulationNo.Text.Trim());
                    }
                    else if (txtStandard.Text != "" && txtStandard.Text != null)
                    {
                        obj_LinkOB.regulationNumber = Convert.ToInt32(txtStandard.Text.Trim());
                    }
                    else
                    {
                        obj_LinkOB.regulationNumber = 0;
                    }
                    obj_LinkOB2.AmbedmentID = Convert.ToInt32(ddlamendment.SelectedValue);
                }

                //}
            }
            else
            {
                obj_LinkOB2.AmbedmentID = null;
                obj_LinkOB2.RegulationNoAmbendment = null;
            }


            obj_LinkOB2.NAME = miscellBL.RemoveUnnecessaryHtmlTagHtml(txtDetail.Text);

            if (txtstartdate.Text != null && txtstartdate.Text != "")
            {
                obj_LinkOB2.StartDate = null;
            }
            else
            {
                obj_LinkOB2.StartDate = null;
            }
            if (txtendate.Text != null && txtendate.Text != "")
            {
                obj_LinkOB2.EndDate = null;
            }
            else
            {
                obj_LinkOB2.EndDate = null;
            }

            obj_LinkOB2.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            obj_LinkOB2.UserID = Convert.ToInt32(Session["User_Id"]);
            obj_LinkOB2.IpAddress = Miscelleneous_DL.getclientIP();
            if (ddlfiles.SelectedValue.ToString() == "0")
            {
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
                {

                    if (FileUpload2.PostedFile.ContentLength != 0 && FileUpload2.PostedFile.ContentLength != null)
                    {
                        if (Upload_File(ref p_Var.Filename))
                        {
                            if (FileUpload2.PostedFile.InputStream.Length != 0 && lblFileName.Visible == false)
                            {
                                //obj_LinkOB2.FileName = FileUpload2.FileName.ToString();
                                obj_LinkOB2.FileName = p_Var.Filename;
                            }
                            else if (lblFileName.Visible == true && FileUpload2.PostedFile.InputStream.Length == 0)
                            {
                                obj_LinkOB2.FileName = lblFileName.Text;
                            }
                            else if (lblFileName.Visible == false && FileUpload2.PostedFile.InputStream.Length == 0)
                            {
                                obj_LinkOB2.FileName = null;
                            }
                            else
                            {
                                //obj_LinkOB2.FileName = FileUpload2.FileName.ToString();
                                obj_LinkOB2.FileName = p_Var.Filename;
                            }
                        }
                        else
                        {
                            obj_LinkOB2.FileName = lblFileName.Text;
                        }
                    }
                    else if (lblFileName.Visible == false && FileUpload2.PostedFile.InputStream.Length == 0)
                    {
                        obj_LinkOB2.FileName = null;
                    }
                    else if (lblFileName.Visible == true && FileUpload2.PostedFile.InputStream.Length == 0)
                    {
                        obj_LinkOB2.FileName = lblFileName.Text;
                    }

                    else
                    {
                        obj_LinkOB2.FileName = p_Var.Filename.ToString();
                    }

                }
            }
            else if (ddlfiles.SelectedValue.ToString() == "1")
            {
                obj_LinkOB2.URL = txtLinks.Text;
            }
            obj_LinkOB2.LangId = Convert.ToInt32(Session["Lanuage"]);
            if (ViewState["DeptID"] != DBNull.Value)
            {
                obj_LinkOB2.DepttId = Convert.ToInt32(ViewState["DeptID"]);
            }
            else
            {
                obj_LinkOB2.DepttId = null;
            }
            obj_LinkOB2.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
            if (txtDetail.Text == "")
            {
                string script = "alert(\"Please enter description.\");";
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                              "ServerControlScript", script, true);

            }
            else
            {
                p_Var.Result = mixModuleBL.ASP_Links_Update(obj_LinkOB2);
            }

            if (p_Var.Result > 0)
            {
                obj_LinkOB.TempLinkId = p_Var.Result;
                p_Var.Result1 = mixModuleBL.deleteConnectedRegulations(obj_LinkOB);

                if (p_Var.Result > 0)
                {
                    obj_audit.ActionType = "U";
                    obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                    obj_audit.UserName = Session["UserName"].ToString();
                    obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                    obj_audit.IpAddress = miscellBL.IpAddress();
                    string st = Request.QueryString["Status"].Trim();
                    if (st == "3") { obj_audit.status = "Draft"; } else if (st == "4") { obj_audit.status = "For Publish"; } else if (st == "6") { obj_audit.status = "Published"; } else if (st == "8") { obj_audit.status = "Deleted"; } else if (st == "21") { obj_audit.status = "For Review"; }
                    string depp = Convert.ToString(Request.QueryString["DepId"]);
                    if (depp == "1") { depp = "HERC"; } else { depp = "Other"; }
                    obj_audit.Title = depp + ", " + ddlModules.SelectedItem.ToString() + ", " + HttpUtility.HtmlDecode(txtDetail.Text).Replace("<p>", "").Replace("</p>", "");
                    obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                    Session["msg"] = "Record has been updated successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=" + 20;
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }

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
            p_Var.Filename = FileUpload2.FileName;
            p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(FileUpload2.FileName);
            p_Var.ext = Path.GetExtension(FileUpload2.FileName);
            //For Unique File Name
            filename = miscellFileBL.getUniqueFileName(p_Var.Filename, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);

            FileUpload2.PostedFile.SaveAs(Server.MapPath(p_Var.url) + filename);
        }
        catch
        {
            p_Var.uploadStatus = false;
        }
        return p_Var.uploadStatus;
    }

    #endregion

    #region Function to display data in edit mode

    public void Display(string Temp_Link_Id)
    {
        PLanguage.Visible = false;
        BtnSubmit.Visible = false;
        BtnUpdate.Visible = true;
        txtStandard.Enabled = false;
        txtRegulationNo.Enabled = false;

        obj_LinkOB.TempLinkId = Convert.ToInt32(Temp_Link_Id);
        obj_LinkOB.StatusId = Convert.ToInt32(Session["Status_Id"]);
        p_Var.dSet = mixModuleBL.ASP_Links_DisplayBYID(obj_LinkOB);
        txtDetail.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Name"].ToString());

        txtMetaDescription.Text = p_Var.dSet.Tables[0].Rows[0]["Mate_Description"].ToString();
        txtMetaTitle.Text = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
        txtMetaKeyword.Text = p_Var.dSet.Tables[0].Rows[0]["Meta_Keywords"].ToString();
        ddlMetaLang.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();


        if (p_Var.dSet.Tables[0].Rows[0]["AmbedmentID"].ToString() != "1" && p_Var.dSet.Tables[0].Rows[0]["AmbedmentID"].ToString() != "")
        {
            ambendantType.Visible = true;
            regulationNumber.Visible = false;
            pOther.Visible = false;
            ddlamendment.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["AmbedmentID"].ToString();
            displayRegulationNumber(Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["Module_id"]));
            regulationddlNumber.Visible = true;
            regulationNumber.Visible = false;
            ddlRegulationNumber.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["RegulationNoAmbendment"].ToString();

        }
        else if (p_Var.dSet.Tables[0].Rows[0]["Module_id"].ToString() == "20" || p_Var.dSet.Tables[0].Rows[0]["Module_id"].ToString() == "22" || p_Var.dSet.Tables[0].Rows[0]["Module_id"].ToString() == "23" || p_Var.dSet.Tables[0].Rows[0]["Module_id"].ToString() == "25" || p_Var.dSet.Tables[0].Rows[0]["Module_id"].ToString() == "26")
        {
            ambendantType.Visible = true;
            regulationddlNumber.Visible = false;
            pOther.Visible = false;
            if (p_Var.dSet.Tables[0].Rows[0]["Module_id"].ToString() == "20")
            {
                if (p_Var.dSet.Tables[0].Rows[0]["RegulationNo"].ToString() != "0")
                {
                    txtRegulationNo.Text = p_Var.dSet.Tables[0].Rows[0]["RegulationNo"].ToString();
                }
                else
                {
                    txtRegulationNo.Text = string.Empty;
                }
            }


        }
        else if (p_Var.dSet.Tables[0].Rows[0]["ReferenceNo"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["ReferenceNo"].ToString() != "")
        {

            regulationNumber.Visible = false;
            ambendantType.Visible = false;
            regulationddlNumber.Visible = false;

        }


        else
        {

            ambendantType.Visible = false;
            regulationNumber.Visible = true;

        }

        ViewState["deptt_id"] = p_Var.dSet.Tables[0].Rows[0]["deptt_id"].ToString();
        ddlModules.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["Module_Id"].ToString();

        if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations))
        {
            if (p_Var.dSet.Tables[0].Rows[0]["AmbedmentID"].ToString() != "1" && p_Var.dSet.Tables[0].Rows[0]["AmbedmentID"].ToString() != "")
            {
                regulationNumber.Visible = false;
            }
            else
            {
                regulationNumber.Visible = true;
            }

            pOther.Visible = false;
            lblRegulationNumber.Text = "Select Regulation Number";
            lblRegulationNumber.ForeColor = System.Drawing.Color.Red;
            lblTxtRegulations.Text = "Regulation Number";
            lblTxtRegulations.ForeColor = System.Drawing.Color.Red;
            txtRegulationNo.Text = p_Var.dSet.Tables[0].Rows[0]["RegulationNo"].ToString();

        }
        else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Codes))
        {
            if (p_Var.dSet.Tables[0].Rows[0]["AmbedmentID"].ToString() != "1" && p_Var.dSet.Tables[0].Rows[0]["AmbedmentID"].ToString() != "")
            {
                pOther.Visible = false;
            }
            else
            {
                pOther.Visible = true;
            }
            regulationNumber.Visible = false;

            lblRegulationNumber.Text = "Select Code Number";
            lblRegulationNumber.ForeColor = System.Drawing.Color.Red;
            lblStandard.Text = "Code Number";
            lblStandard.ForeColor = System.Drawing.Color.Red;
            txtStandard.Text = p_Var.dSet.Tables[0].Rows[0]["RegulationNo"].ToString();
            //chklstPetition.Visible = false;
        }
        else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Standard))
        {
            if (p_Var.dSet.Tables[0].Rows[0]["AmbedmentID"].ToString() != "1" && p_Var.dSet.Tables[0].Rows[0]["AmbedmentID"].ToString() != "")
            {
                pOther.Visible = false;
            }
            else
            {
                pOther.Visible = true;
            }
            regulationNumber.Visible = false;

            lblRegulationNumber.Text = "Select Standard Number";
            lblRegulationNumber.ForeColor = System.Drawing.Color.Red;
            lblStandard.Text = "Standard Number";
            lblStandard.ForeColor = System.Drawing.Color.Red;
            txtStandard.Text = p_Var.dSet.Tables[0].Rows[0]["RegulationNo"].ToString();
        }
        else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Policies))
        {
            if (p_Var.dSet.Tables[0].Rows[0]["AmbedmentID"].ToString() != "1" && p_Var.dSet.Tables[0].Rows[0]["AmbedmentID"].ToString() != "")
            {
                pOther.Visible = false;
            }
            else
            {
                pOther.Visible = true;
            }
            regulationNumber.Visible = false;

            lblRegulationNumber.Text = "Select Policy Number";
            lblRegulationNumber.ForeColor = System.Drawing.Color.Red;
            lblStandard.Text = "Policy Number";
            lblStandard.ForeColor = System.Drawing.Color.Red;
            txtStandard.Text = p_Var.dSet.Tables[0].Rows[0]["RegulationNo"].ToString();
        }
        else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Guidelines))
        {
            if (p_Var.dSet.Tables[0].Rows[0]["AmbedmentID"].ToString() != "1" && p_Var.dSet.Tables[0].Rows[0]["AmbedmentID"].ToString() != "")
            {
                pOther.Visible = false;
            }
            else
            {
                pOther.Visible = true;
            }
            regulationNumber.Visible = false;

            lblRegulationNumber.Text = "Select Guideline Number";
            lblRegulationNumber.ForeColor = System.Drawing.Color.Red;
            lblStandard.Text = "Guideline Number";
            lblStandard.ForeColor = System.Drawing.Color.Red;
            txtStandard.Text = p_Var.dSet.Tables[0].Rows[0]["RegulationNo"].ToString();
        }

        //Code to bind connected modules
        obj_LinkOB.linkID = Convert.ToInt32(Temp_Link_Id);
        if (Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["module_id"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations))
        {

        }
        else
        {

        }
        //End

        lblModuleName.Text = p_Var.dSet.Tables[0].Rows[0]["Module_NAME"].ToString();
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


        if (p_Var.dSet.Tables[0].Rows[0]["File_Name"] == DBNull.Value || p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString() == "")
        {
            lnkFileRemove.Visible = false;
            LblOldFile.Visible = false;
        }
        else
        {
            lnkFileRemove.Visible = true;
            LblOldFile.Visible = true;
        }

        if (p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString() != "")
        {
            pFileUpload.Visible = true;
            pExternalLinks.Visible = false;
            ddlfiles.SelectedValue = "0";
            lblFileName.Text = p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString();
        }
        else if (p_Var.dSet.Tables[0].Rows[0]["url"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["url"].ToString() != "")
        {

            pFileUpload.Visible = false;
            pExternalLinks.Visible = true;
            ddlfiles.SelectedValue = "1";
            txtLinks.Text = p_Var.dSet.Tables[0].Rows[0]["url"].ToString();
            LblOldFile.Visible = false;
            lnkFileRemove.Visible = false;
        }

        if (p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"] != DBNull.Value)
        {
            ViewState["DeptID"] = p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"].ToString();
        }
        else
        {
            ViewState["DeptID"] = DBNull.Value;
        }



        p_Var.dSet = null;
    }

    #endregion

    protected void ddlModules_SelectedIndexChanged(object sender, EventArgs e)
    {

        displayModuleName(Convert.ToInt32(ddlModules.SelectedValue));
        if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations))
        {
            regulationNumber.Visible = true;
            pOther.Visible = false;
            lblRegulationNumber.Text = "Select Regulation Number";
            lblRegulationNumber.ForeColor = System.Drawing.Color.Red;
            lblTxtRegulations.Text = "Regulation Number";
            lblTxtRegulations.ForeColor = System.Drawing.Color.Red;

        }
        else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Codes))
        {
            regulationNumber.Visible = false;
            pOther.Visible = true;
            lblRegulationNumber.Text = "Select Code Number";
            lblRegulationNumber.ForeColor = System.Drawing.Color.Red;
            lblStandard.Text = "Code Number";
            lblStandard.ForeColor = System.Drawing.Color.Red;
            //chklstPetition.Visible = false;
        }
        else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Standard))
        {
            regulationNumber.Visible = false;
            pOther.Visible = true;
            lblRegulationNumber.Text = "Select Standard Number";
            lblRegulationNumber.ForeColor = System.Drawing.Color.Red;
            lblStandard.Text = "Standard Number";
            lblStandard.ForeColor = System.Drawing.Color.Red;
        }
        else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Policies))
        {
            regulationNumber.Visible = false;
            pOther.Visible = true;
            lblRegulationNumber.Text = "Select Policies Number";
            lblRegulationNumber.ForeColor = System.Drawing.Color.Red;
            lblStandard.Text = "Policies Number";
            lblStandard.ForeColor = System.Drawing.Color.Red;
        }
        else if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Guidelines))
        {
            regulationNumber.Visible = false;
            pOther.Visible = true;
            lblRegulationNumber.Text = "Select Guidelines Number";
            lblRegulationNumber.ForeColor = System.Drawing.Color.Red;
            lblStandard.Text = "Guidelines Number";
            lblStandard.ForeColor = System.Drawing.Color.Red;
        }
        //New codes added on date 03-04-2013

        if (ddlamendment.SelectedValue == "1")
        {

            regulationddlNumber.Visible = false;


        }
        else
        {

            regulationddlNumber.Visible = true;
            // displayRegulationNumber(Convert.ToInt32(ddlamendment.SelectedValue));

        }


        //End
    }

    #region Function to bind modulename

    public void displayModuleName(int moduleid)
    {
        LinkOB lnkObject = new LinkOB();
        lnkObject.ModuleId = moduleid;
        p_Var.dSet = mixModuleBL.getModuleName(lnkObject);
        if (p_Var.dSet.Tables.Count > 0)
        {

            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                // pReferenceNumber.Visible = false;
                pnlRegulation.Visible = true;
                // chklstPetition.Visible = true;
                ddlRegulationNumber.Visible = true;
                ambendantType.Visible = true;


                displayAmendment();
                //displayRegulationNumber(moduleid);

                if (moduleid != 20)
                {
                    pnlRegulation.Visible = true;

                }
                else
                {
                    pnlRegulation.Visible = true;

                }
            }
            else
            {

            }
        }
        else
        {
            if (moduleid != 22 && moduleid != 23 && moduleid != 25 && moduleid != 26)
            {
                pnlRegulation.Visible = false;

            }
            else
            {
                pnlRegulation.Visible = true;

                ddlRegulationNumber.Visible = false;
                ambendantType.Visible = false;

            }
        }
    }


    #endregion

    #region CustomValidator cusRegulation number serverValidate to validate regulation number

    protected void cusRegulation_ServerValidate(object source, ServerValidateEventArgs args)
    {

        try
        {
            if (Request.QueryString["Temp_Link_Id"] == null)    // for Edit
            {
                if (txtRegulationNo.Text != "" && txtRegulationNo.Text != null && txtRegulationNo.Text != "0")
                {
                    p_Var.dSet = null;
                    obj_LinkOB.regulationNumber = Convert.ToInt32(txtRegulationNo.Text);
                    obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
                    p_Var.dSet = mixModuleBL.getRegulation_Number(obj_LinkOB);

                    if (p_Var.dSet.Tables[0].Rows.Count > 0)
                    {
                        args.IsValid = false;
                    }
                    else
                    {
                        args.IsValid = true;
                    }
                }
            }
            else
            {
                if (txtRegulationNo.Text != "" && txtRegulationNo.Text != null && txtRegulationNo.Text != "0")
                {
                    p_Var.dSet = null;
                    obj_LinkOB.regulationNumber = Convert.ToInt32(txtRegulationNo.Text);
                    obj_LinkOB.linkID = Convert.ToInt32(Request.QueryString["Temp_Link_Id"]);
                    obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
                    p_Var.dSet = mixModuleBL.getRegulationNumber_In_EditMode(obj_LinkOB);

                    if (p_Var.dSet.Tables[0].Rows.Count > 0)
                    {
                        args.IsValid = false;
                    }
                    else
                    {
                        args.IsValid = true;
                    }
                }
                // args.IsValid = true;
            }


        }
        catch
        {
            throw;
        }
    }

    #endregion


    protected void ddlamendment_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbldetail.Visible = false;
        if (ddlamendment.SelectedValue == "1")
        {
            if (Convert.ToInt32(ddlModules.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations))
            {
                regulationNumber.Visible = true;
                regulationddlNumber.Visible = false;
                pOther.Visible = false;
            }
            else
            {
                regulationNumber.Visible = false;
                regulationddlNumber.Visible = false;
                pOther.Visible = true;
            }

        }
        else
        {
            //pReferenceNumber.Visible = false;
            regulationddlNumber.Visible = true;
            displayRegulationNumber(Convert.ToInt32(ddlamendment.SelectedValue));
            regulationNumber.Visible = false;
            pOther.Visible = false;
        }

        obj_LinkOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations);
        p_Var.dSetCompare = mixModuleBL.getModuleName(obj_LinkOB);

        if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
        {

        }

        else
        {

        }
    }

    protected void ddlfiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlfiles.SelectedValue == "0")
        {
            pFileUpload.Visible = true;
            pExternalLinks.Visible = false;
        }
        else
        {
            pFileUpload.Visible = false;
            pExternalLinks.Visible = true;
        }
    }

    #region Function to bind amendment

    public void displayAmendment()
    {
        p_Var.dSet = mixModuleBL.getAmendmentNumber();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlamendment.DataSource = p_Var.dSet;
            ddlamendment.DataValueField = "Ambedmentid";
            ddlamendment.DataTextField = "AmbedmentName";
            ddlamendment.DataBind();
        }
    }

    #endregion

    #region Function to bind regulation numbers

    public void displayRegulationNumber(int RegulationNumber)
    {
        //  obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
        if (Request.QueryString["amb"] != null && Request.QueryString["amb"].ToString() != "")
        {
            obj_LinkOB.RegulationNoAmbendment = Convert.ToInt32(Request.QueryString["amb"]);
        }
        else
        {
            obj_LinkOB.RegulationNoAmbendment = null;
        }
        if (ddlamendment.SelectedValue != "1")
        {
            obj_LinkOB.AmbedmentID = Convert.ToInt32(ddlamendment.SelectedValue);
        }
        else
        {
            obj_LinkOB.AmbedmentID = 1;
        }
        obj_LinkOB.regulationNumber = RegulationNumber;
        // p_Var.dSetCompare = mixModuleBL.getRegulationNumber(obj_LinkOB);
        obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
        p_Var.dSetCompare = mixModuleBL.getRegulationNumberAmbendment(obj_LinkOB);
        if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
        {
            ddlRegulationNumber.DataSource = p_Var.dSetCompare;
            ddlRegulationNumber.DataValueField = "RegulationNo";
            ddlRegulationNumber.DataTextField = "RegulationNo";
            ddlRegulationNumber.DataBind();
            ddlRegulationNumber.Items.Insert(0, new ListItem("Select One", "0"));
        }
        else
        {
            ddlRegulationNumber.DataSource = p_Var.dSetCompare;
            ddlRegulationNumber.DataBind();
            ddlRegulationNumber.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    #endregion

    protected void cus_ambendment_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {


            p_Var.dSet = null;
            if (ddlRegulationNumber.SelectedValue != "0" && ddlRegulationNumber.SelectedValue.ToString() != "")
            {
                obj_LinkOB.regulationNumber = Convert.ToInt32(ddlRegulationNumber.SelectedValue);
            }
            else
            {
                if (txtRegulationNo.Text != "" && txtRegulationNo.Text != null)
                {
                    obj_LinkOB.regulationNumber = Convert.ToInt32(txtRegulationNo.Text);
                }
                else
                {
                    obj_LinkOB.regulationNumber = 0;
                }
            }

            obj_LinkOB.AmbedmentID = Convert.ToInt32(ddlamendment.SelectedValue);
            obj_LinkOB.TempLinkId = Convert.ToInt32(Request.QueryString["Temp_Link_Id"]);
            obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
            p_Var.i = Convert.ToInt32(mixModuleBL.getAmbendmentID(obj_LinkOB));

            if (p_Var.i == Convert.ToInt32(ddlamendment.SelectedValue))
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


    protected void ddlRegulationNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegulationNumber.SelectedValue != "0")
        {
            obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
            p_Var.dSetCompare = mixModuleBL.getModuleName(obj_LinkOB);



            obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
            obj_LinkOB.regulationNumber = Convert.ToInt32(ddlRegulationNumber.SelectedValue);
            p_Var.dSet = mixModuleBL.getConnectedModuleNameForRegulationSelected(obj_LinkOB);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {


            }
            else
            {

            }


            obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
            obj_LinkOB.regulationNumber = Convert.ToInt32(ddlRegulationNumber.SelectedValue);
            p_Var.dSetCompare = mixModuleBL.getDetails(obj_LinkOB);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {
                lbldetail.Visible = true;
                lbldetail.Text = p_Var.dSetCompare.Tables[0].Rows[0]["name"].ToString().Replace("&lt;p&gt;", " ").Replace("&lt;/p&gt;", " ");

            }
            else
            {
                lbldetail.Visible = false;


            }
        }

        else
        {
            obj_LinkOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Regulations);
            p_Var.dSetCompare = mixModuleBL.getModuleName(obj_LinkOB);
            lbldetail.Visible = false;

        }
    }

    #region CustomValidator cusRegulation number serverValidate to validate regulation number

    protected void cusRegulation1_ServerValidate(object source, ServerValidateEventArgs args)
    {

        try
        {
            if (Request.QueryString["Temp_Link_Id"] == null)    // for Edit
            {
                if (txtStandard.Text != "" && txtStandard.Text != null && txtStandard.Text != "0")
                {
                    p_Var.dSet = null;
                    obj_LinkOB.regulationNumber = Convert.ToInt32(txtStandard.Text);
                    obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
                    p_Var.dSet = mixModuleBL.getRegulation_Number(obj_LinkOB);

                    if (p_Var.dSet.Tables[0].Rows.Count > 0)
                    {
                        args.IsValid = false;
                    }
                    else
                    {
                        args.IsValid = true;
                    }
                }
            }
            else
            {
                if (txtStandard.Text != "" && txtStandard.Text != null && txtStandard.Text != "0")
                {
                    p_Var.dSet = null;
                    obj_LinkOB.regulationNumber = Convert.ToInt32(txtStandard.Text);
                    obj_LinkOB.linkID = Convert.ToInt32(Request.QueryString["Temp_Link_Id"]);
                    obj_LinkOB.ModuleId = Convert.ToInt32(ddlModules.SelectedValue);
                    p_Var.dSet = mixModuleBL.getRegulationNumber_In_EditMode(obj_LinkOB);

                    if (p_Var.dSet.Tables[0].Rows.Count > 0)
                    {
                        args.IsValid = false;
                    }
                    else
                    {
                        args.IsValid = true;
                    }
                }
                // args.IsValid = true;
            }


        }
        catch
        {
            throw;
        }
    }

    #endregion

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
