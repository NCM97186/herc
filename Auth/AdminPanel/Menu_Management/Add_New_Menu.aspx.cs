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


public partial class Auth_AdminPanel_Menu_Management_Add_New_Menu : CrsfBase //System.Web.UI.Page
{
    //Area for all the data declaration

    #region Data declaration zone

    //DataSet ds = new DataSet();
    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    LinkOB lnkObject = new LinkOB();
    Project_Variables p_Var = new Project_Variables();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    UserBL obj_UserBL = new UserBL();
    Miscelleneous_BL obj_miscelBL = new Miscelleneous_BL();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion

    //End

    //Area for the page load event

    #region page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.url = "~/" + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["CMS"].ToString() + "/"; 
        if (!IsPostBack)
        {
            DataSet dsprv = new DataSet();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_RoleBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }

            Get_Deptt_Name(); 
            bindropDownlistLang();
            bindMenuTypeDLL();
            bindMenuPosition();
            Label lblModulename = Master.FindControl("lblModulename") as Label;
            if (Request.QueryString["TempLink_Id"] == null) //change the text of the label according to Add/Edit
            {
                lblModulename.Text = ": Add Menu";
                btnMenu.Text = "Save";
            }
            else
            {
                lblModulename.Text = ": Edit Menu";
                btnMenu.Text = "Update";
            }
            if (Request.QueryString["TempLink_Id"] != null)
            {
                trListBox.Visible = false;
                trLanguage.Visible = false;
                trMenu.Visible = false;
                trDepartment.Visible = false;
                bindData_For_Editing(Convert.ToInt32(Request.QueryString["statusID"]), Convert.ToInt32(Request.QueryString["TempLink_Id"]));
            }
            else
            {
                trFckEditor.Visible = true;
                trDepartment.Visible = true;
                trLanguage.Visible = true;
                trMenu.Visible = true;
                FCKeditor1.Visible = true;
                trFileupload.Visible = false;
                trLinkUrl.Visible = false;
                trListBox.Visible = true;
                bindMenu_ListBox(Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue),Convert.ToInt32(ddlDepartment.SelectedValue));
            }

        }
    }

    #endregion

    //End

    //Area for all the customValidator to validate

    #region Custom validator to validate extension of upload pdf files

    protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Get file name
        string UploadFileName = fileUpload_Menu.PostedFile.FileName;

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

    //End

    //Area for all buttons, linkbuttons, imagebuttons click events

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1");
    }

    #endregion

    #region button btnCancel click event to cancel data

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["TempLink_Id"] == null)
        {
            txtBrowserTitle.Text = "";
            txtLinkUrl.Text = "";
            txtUrlName.Text = "";
            txtMenuName.Text = "";
            txtMetaDescription.Text = "";
            txtMetaTitle.Text = "";
            txtMetaKeyword.Text = "";
            txtPageTitle.Text = "";
            FCKeditor1.Value = "";
        }
        else
        {
            bindData_For_Editing(Convert.ToInt32(Request.QueryString["statusID"]), Convert.ToInt32(Request.QueryString["TempLink_Id"]));
        }

    }

    #endregion

    #region button btnMenu click event to add new menu

    protected void btnMenu_Click(object sender, EventArgs e)
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
                    Add_New_Menu();
                }
            }
        }
		else{
			Session.Abandon();
		    Response.Redirect("~/Auth/AdminPanel/Login.aspx");
			}
    }

    #endregion

    //End

    //Area for all the dropDownlist events

    #region dropDownlist ddlMenuPosition selectedIndexChanged event zone

    protected void ddlMenuPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindMenu_ListBox(Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
    }

    #endregion

    #region dropDownlist ddlLanguage selectedIndexChanged event zone

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindMenu_ListBox(Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
    }

    #endregion

    #region dropDownlist ddlMenuType selectedIndexChanged event zone

    protected void ddlMenuType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMenuType.SelectedValue == "1")
        {
            trFckEditor.Visible = true;
            FCKeditor1.Visible = true;
            trFileupload.Visible = false;
            trLinkUrl.Visible = false;
            trPageTitle.Visible = true;
            trBrowserTitle.Visible = true;
            trUrlName.Visible = true;
            trmetakeyword.Visible = true;
            trMetadescription.Visible = true;
            trMetaTitle.Visible = true;
        }
        else if (ddlMenuType.SelectedValue == "2")
        {
            FCKeditor1.Visible = false;
            trFckEditor.Visible = false;
            lblmenuMsg.Visible = true;
            lblFileName.Visible = true;
            trFileupload.Visible = true;
            trLinkUrl.Visible = false;
        }
        else if (ddlMenuType.SelectedValue == "3")
        {
            trFckEditor.Visible = false;
            FCKeditor1.Visible = false;
            trFileupload.Visible = false;
            trPageTitle.Visible = false;
            trLinkUrl.Visible = true;
            trBrowserTitle.Visible = false;
            trUrlName.Visible = false;
            trmetakeyword.Visible = false;
            trMetadescription.Visible = false;
            trMetaTitle.Visible = false;
        }

    }

    #endregion

    #region dropDownlist ddlDepartment selectedIndexChanged event

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindMenu_ListBox(Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
    }

    #endregion

    //End

    //Area for all the user defined functions

    #region Function to add and edit menu

    public void Add_New_Menu()
    {
        if (Request.QueryString["TempLink_Id"] == null)
        {
            try
            {
                //Code added by Birendra 
                if (ListBox1.SelectedValue == "0")
                {
                    lnkObject.LinkParentId = 0;
                    p_Var.dSet = menuBL.get_levelOrder_Link_Web(lnkObject);
                    if (p_Var.dSet.Tables[0].Rows.Count > 0)
                    {
                        lnkObject.LinkOrder = Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["Link_Order"]);
                    }
                    else
                    {
                        lnkObject.LinkOrder = 0;
                    }
                    lnkObject.LinkLevel = 0;
                }
                else
                {
                    lnkObject.LinkParentId = (Convert.ToInt32(ListBox1.SelectedValue));

                    p_Var.dSet = menuBL.get_Menu_level_Link_Web(lnkObject);
                    if (p_Var.dSet.Tables[0].Rows.Count == null)
                    {
                        lnkObject.LinkLevel = 0;
                    }
                    else
                    {
                        lnkObject.LinkLevel = Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["Link_Level"]) + 1;
                    }
                }

                //End

                lnkObject.ActionType = 1;
                lnkObject.PositionId = Convert.ToInt32(ddlMenuPosition.SelectedValue);
                lnkObject.LinkParentId = Convert.ToInt32(ListBox1.SelectedValue);
                lnkObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
                lnkObject.NAME = HttpUtility.HtmlEncode(txtMenuName.Text);
                lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                lnkObject.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
                lnkObject.UrlName = HttpUtility.HtmlEncode(txtUrlName.Text);
                lnkObject.LinkTypeId = Convert.ToInt32(ddlMenuType.SelectedValue); //Type of menu
                lnkObject.PageTitle = HttpUtility.HtmlEncode(txtPageTitle.Text);
                lnkObject.BrowserTitle = HttpUtility.HtmlEncode(txtBrowserTitle.Text);
                lnkObject.MetaKeywords = HttpUtility.HtmlEncode(txtMetaKeyword.Text);
                lnkObject.MateDescription = HttpUtility.HtmlEncode(txtMetaDescription.Text);
                lnkObject.MetaTitle = HttpUtility.HtmlEncode(txtMetaTitle.Text);
                lnkObject.Date_Inserted = System.DateTime.Now;
                lnkObject.InsertedBy = Convert.ToInt32(Session["User_Id"]);
                lnkObject.UserID = Convert.ToInt32(Session["User_Id"]);
                lnkObject.IpAddress = Miscelleneous_DL.getclientIP();
                if (txtStartDate.Text != null && txtStartDate.Text != "")
                {
                    lnkObject.StartDate = null;
                }
                else
                {
                    lnkObject.StartDate = System.DateTime.Now;
                }
                if (txtEndDate.Text != null && txtEndDate.Text != "")
                {
                    lnkObject.EndDate = null;
                }
                else
                {
                    if (txtStartDate.Text != null && txtStartDate.Text != "")
                    {
                      
                        lnkObject.EndDate = null;
                       
                    }
                    else
                    {
                        
                        lnkObject.EndDate = null;
                    }
                }
                lnkObject.ModuleId = 1;

                if (ddlMenuType.SelectedValue == "1")
                {
                    lnkObject.details =obj_miscelBL.RemoveUnnecessaryHtmlTagHtml(FCKeditor1.Value);
                }
                else if (ddlMenuType.SelectedValue == "2")
                {
                    if (fileUpload_Menu.PostedFile != null && fileUpload_Menu.PostedFile.InputStream.Length != 0)
                    {
                        p_Var.ext = System.IO.Path.GetExtension(this.fileUpload_Menu.PostedFile.FileName);
                        p_Var.ext = p_Var.ext.ToUpper();
                        if (p_Var.ext.Equals(".PDF"))
                        {
                            p_Var.Path = "~/" + ConfigurationManager.AppSettings["WriteReadData"];
                            p_Var.Path = p_Var.Path + "/" + ConfigurationManager.AppSettings["CMS"];
                            p_Var.Filename = fileUpload_Menu.FileName;
                            fileUpload_Menu.SaveAs(Server.MapPath(p_Var.Path + "/" + p_Var.Filename));
                            lnkObject.FileName = p_Var.Filename;
                        }
                    }
                }
                else if (ddlMenuType.SelectedValue == "3")
                {
                    lnkObject.URL = HttpUtility.HtmlEncode(txtLinkUrl.Text);
                }

                p_Var.Result = menuBL.insert_Top_Menu(lnkObject);

                if (p_Var.Result > 0)
                {

                    obj_audit.ActionType = "I";
                    obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                    obj_audit.UserName = Session["UserName"].ToString();
                    obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                    obj_audit.IpAddress = obj_miscelBL.IpAddress();
                    obj_audit.status = "New Record"; 
                    obj_audit.Title = txtMenuName.Text;
                    obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);
 

                    Session["msg"] = "Content has been added successfully.";
                    
                    if (ddlMenuPosition.SelectedValue == "1")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=1";
                    }
                    else if (ddlMenuPosition.SelectedValue == "2")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=2";
                    }
                    else if (ddlMenuPosition.SelectedValue == "3")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=3";
                    }
                    else if (ddlMenuPosition.SelectedValue == "4")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=4";
                    }
                    else if (ddlMenuPosition.SelectedValue == "5")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=5";
                    }
                    else if (ddlMenuPosition.SelectedValue == "6")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1 &position=6";
                    }
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }

            }
            catch
            {
                throw;
            }

        }
        else  //CODE FOR EDIT DATA EITHER TEMP OR APPROVED
        {


            try
            {
                if (Convert.ToInt32(Request.QueryString["statusID"]) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                {
                    lnkObject.ActionType = (int)Module_ID_Enum.Project_Action_Type.update;
                    lnkObject.linkID = Convert.ToInt32(Request.QueryString["TempLink_Id"]);
                    lnkObject.InsertedBy = Convert.ToInt32(Session["user_id"]);
                    lnkObject.LastUpdatedBy = Convert.ToInt32(Session["user_id"]);
                }
                else
                {
                    lnkObject.ActionType = (int)Module_ID_Enum.Project_Action_Type.update;
                    lnkObject.LastUpdatedBy = Convert.ToInt32(Session["user_id"]);
                    if (ViewState["Link_ID"] != DBNull.Value)
                    {
                        lnkObject.linkID = Convert.ToInt32(Request.QueryString["TempLink_Id"]);
                    }
                    else
                    {
                        lnkObject.linkID = null;
                    }
                }
                lnkObject.BrowserTitle = HttpUtility.HtmlEncode(txtBrowserTitle.Text);
                lnkObject.URL = HttpUtility.HtmlEncode(txtLinkUrl.Text);
                lnkObject.NAME = HttpUtility.HtmlEncode(txtMenuName.Text);
                lnkObject.MateDescription = HttpUtility.HtmlEncode(txtMetaDescription.Text);
                lnkObject.MetaTitle = HttpUtility.HtmlEncode(txtMetaTitle.Text);
                lnkObject.MetaKeywords = HttpUtility.HtmlEncode(txtMetaKeyword.Text);
                lnkObject.PageTitle = HttpUtility.HtmlEncode(txtPageTitle.Text);
                
                lnkObject.details = obj_miscelBL.RemoveUnnecessaryHtmlTagHtml(FCKeditor1.Value);
                lnkObject.UrlName = HttpUtility.HtmlEncode(txtUrlName.Text);
                lnkObject.Date_Inserted = System.DateTime.Now;
                lnkObject.LinkTypeId = Convert.ToInt32(ddlMenuType.SelectedValue); //Type of menu
                lnkObject.TempLinkId = Convert.ToInt32(Request.QueryString["TempLink_Id"]);
                lnkObject.IpAddress = Miscelleneous_DL.getclientIP();
                lnkObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                if (txtStartDate.Text != null && txtStartDate.Text != "")
                {
                    lnkObject.StartDate = null;
                }
                else
                {
                    lnkObject.StartDate = System.DateTime.Now;
                }
                if (txtEndDate.Text != null && txtEndDate.Text != "")
                {
                    lnkObject.EndDate = null;
                }
                else
                {
                    if (txtStartDate.Text != null && txtStartDate.Text != "")
                    {
                       
                        lnkObject.EndDate = null;
                    }
                    else
                    {
                        
                        lnkObject.EndDate = null;
                    }
                }
                if (ddlMenuPosition.SelectedValue == "2")
                {
                    lnkObject.ModuleId = (int)Module_ID_Enum.Project_Module_ID.Menu;
                }
                else
                {
                    lnkObject.ModuleId = (int)Module_ID_Enum.Project_Module_ID.Menu;
                }
                
                if (ViewState["ParentID"] != DBNull.Value)
                {
                    lnkObject.LinkParentId = Convert.ToInt32(ViewState["ParentID"]);
                }
                else
                {
                    lnkObject.LinkParentId = null;
                }
                if (ViewState["PositionID"] != DBNull.Value)
                {
                    lnkObject.PositionId = Convert.ToInt32(ViewState["PositionID"]);
                }
                else
                {
                    lnkObject.PositionId = null;
                }
                if (ViewState["Level"] != DBNull.Value)
                {
                    lnkObject.LinkLevel = Convert.ToInt32(ViewState["Level"]);
                }
                else
                {
                    lnkObject.LinkLevel = null;
                }
                if (ViewState["LangID"] != DBNull.Value)
                {
                    lnkObject.LangId = Convert.ToInt32(ViewState["LangID"]);
                }
                else
                {
                    lnkObject.LangId = null;
                }
                if (ViewState["DeptID"] != DBNull.Value)
                {
                    lnkObject.DepttId = Convert.ToInt32(ViewState["DeptID"]);
                }
                else
                {
                    lnkObject.DepttId = null;
                }
                if (Session["Status_Id"] != null)
                {
                    if (Convert.ToInt32(Session["Status_Id"]) == (int)Module_ID_Enum.Module_Permission_ID.Approved)
                    {
                        lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.Approved;
                    }
                    else
                    {
                        lnkObject.StatusId = Convert.ToInt32(Session["Status_Id"]);

                    }
                }
                else
                {
                    lnkObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                }

                lnkObject.NAME = HttpUtility.HtmlEncode(txtMenuName.Text);

                if (ddlMenuType.SelectedValue == "1")
                {

                    lnkObject.details = obj_miscelBL.RemoveUnnecessaryHtmlTagHtml(FCKeditor1.Value);

                }
                else if (ddlMenuType.SelectedValue == "2")
                {

                    if (fileUpload_Menu.PostedFile != null && fileUpload_Menu.PostedFile.InputStream.Length != 0)
                    {
                        p_Var.ext = System.IO.Path.GetExtension(this.fileUpload_Menu.PostedFile.FileName);
                        p_Var.ext = p_Var.ext.ToUpper();
                        if (p_Var.ext.Equals(".PDF"))
                        {
                            p_Var.Path = "~/" + ConfigurationManager.AppSettings["WriteReadData"];
                            p_Var.Path = p_Var.Path + "/" + ConfigurationManager.AppSettings["CMS"];
                            p_Var.Filename = fileUpload_Menu.FileName;
                            fileUpload_Menu.SaveAs(Server.MapPath(p_Var.Path + "/" + p_Var.Filename));
                            lnkObject.FileName = p_Var.Filename;
                        }
                    }
                }
                else if (ddlMenuType.SelectedValue == "3")
                {

                    lnkObject.URL = HttpUtility.HtmlEncode(txtLinkUrl.Text);
                }

                p_Var.Result = menuBL.insert_Top_Menu(lnkObject);
                if (p_Var.Result > 0)
                {
                    obj_audit.ActionType = "U";
                    obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                    obj_audit.UserName = Session["UserName"].ToString();
                    obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                    obj_audit.IpAddress = obj_miscelBL.IpAddress();
                    string st = Request.QueryString["statusID"].Trim();
                    if (st == "3") { obj_audit.status = "Draft"; } else if (st == "4") { obj_audit.status = "For Publish"; } else if (st == "6") { obj_audit.status = "Published"; } else if (st == "8") { obj_audit.status = "Deleted"; } else if (st == "21") { obj_audit.status = "For Review"; }
                    obj_audit.Title = txtMenuName.Text;
                    obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                    Session["msg"] = "Content has been updated successfully.";

                    if (Request.QueryString["pos"].ToString() == "1")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=" + Request.QueryString["ModuleID"] + "&LangId=" + Request.QueryString["LangID"] + "&StatusID=" + Request.QueryString["StatusID"] + "&pos=" + Request.QueryString["pos"] + "&LinkParentId=" + Request.QueryString["LinkParentId"];
                    }
                    else if (Request.QueryString["pos"].ToString() == "2")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=" + Request.QueryString["ModuleID"] + "&LangId=" + Request.QueryString["LangID"] + "&StatusID=" + Request.QueryString["StatusID"] + "&pos=" + Request.QueryString["pos"] + "&LinkParentId=" + Request.QueryString["LinkParentId"];
                    }
                    if (Request.QueryString["pos"].ToString() == "3")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=" + Request.QueryString["ModuleID"] + "&LangId=" + Request.QueryString["LangID"] + "&StatusID=" + Request.QueryString["StatusID"] + "&pos=" + Request.QueryString["pos"] + "&LinkParentId=" + Request.QueryString["LinkParentId"]; 
                    }
                    if (Request.QueryString["pos"].ToString() == "4")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=" + Request.QueryString["ModuleID"] + "&LangId=" + Request.QueryString["LangID"] + "&StatusID=" + Request.QueryString["StatusID"] + "&pos=" + Request.QueryString["pos"] + "&LinkParentId=" + Request.QueryString["LinkParentId"];
                    }
                    if (Request.QueryString["pos"].ToString() == "5")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=" + Request.QueryString["ModuleID"] + "&LangId=" + Request.QueryString["LangID"] + "&StatusID=" + Request.QueryString["StatusID"] + "&pos=" + Request.QueryString["pos"] + "&LinkParentId=" + Request.QueryString["LinkParentId"];
                    }
                    else if (Request.QueryString["pos"].ToString() == "6")
                    {
                        Session["Redirect"] = "~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=" + Request.QueryString["ModuleID"] + "&LangId=" + Request.QueryString["LangID"] + "&StatusID=" + Request.QueryString["StatusID"] + "&pos=" + Request.QueryString["pos"] + "&LinkParentId=" + Request.QueryString["LinkParentId"];
                    }

                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx?ModuleID=" + Request.QueryString["ModuleID"] + "&LangId=" + Request.QueryString["LangID"] + "&StatusID=" + Request.QueryString["StatusID"] + "&pos=" + Request.QueryString["pos"] + "&LinkParentId=" + Request.QueryString["LinkParentId"]);
                }



            }
            catch
            {
                throw;
            }

        }

    }

    #endregion

    #region Function to bind dropDownlist with menu type

    public void bindMenuTypeDLL()
    {
        try
        {
            p_Var.dSet = menuBL.getMenuType();
            ddlMenuType.DataSource = p_Var.dSet.Tables[0];
            ddlMenuType.DataTextField = "Link_Type";
            ddlMenuType.DataValueField = "Link_Type_Id";
            ddlMenuType.DataBind();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to bind dropDownlist with menu position

    public void bindMenuPosition()
    {
        Menu_ManagementBL obj_menuManagementBL = new Menu_ManagementBL();
        try
        {
            p_Var.dSet = obj_menuManagementBL.getMenuPosition();
            ddlMenuPosition.DataSource = p_Var.dSet.Tables[0];
            ddlMenuPosition.DataTextField = "Position";
            ddlMenuPosition.DataValueField = "Position_Id";
            ddlMenuPosition.DataBind();


        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to bind listbox with root

    private void bindMenu_ListBox(int langid, int positionid,int departmentid)
    {
        ListBox1.Items.Clear();
        //Menu_ManagementBL _menuBL = new Menu_ManagementBL();

        ListItem li = default(ListItem);

        lnkObject.LangId = langid;
        lnkObject.LinkParentId = 0;
        lnkObject.PositionId = positionid; //1
        lnkObject.DepttId = departmentid;
        try
        {

            p_Var.dSet = menuBL.getMenuName_From_Web(lnkObject);

            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_Var.i = 0; p_Var.i <= p_Var.dSet.Tables[0].Rows.Count - 1; p_Var.i++)
                {
                    p_Var.linkid = Convert.ToInt32(p_Var.dSet.Tables[0].Rows[p_Var.i]["Link_Id"]);
                    //if (link_id != 20)
                    //{
                    li = new ListItem(p_Var.dSet.Tables[0].Rows[p_Var.i]["NAME"].ToString(), p_Var.dSet.Tables[0].Rows[p_Var.i]["Link_Id"].ToString());
                    ListBox1.Items.Add(li);
                    bindChildData(Convert.ToInt32(p_Var.dSet.Tables[0].Rows[p_Var.i]["Link_Level"]) + 1, Convert.ToInt32(p_Var.dSet.Tables[0].Rows[p_Var.i]["Link_Id"]), Convert.ToInt32(p_Var.dSet.Tables[0].Rows[p_Var.i]["Position_Id"]));
                    //}
                }
                if (langid == 1)
                {
                    ListBox1.Items.Insert(0, new ListItem("<----- On Root ------>", "0"));
                }
                else
                {
                    ListBox1.Items.Insert(0, new ListItem("<----- मुख पृष्ठ ------>", "0"));
                }
                ListBox1.Items[0].Selected = true;

            }
            else
            {
                if (langid == 1)
                {
                    ListBox1.Items.Insert(0, new ListItem("<----- On Root ------>", "0"));
                }
                else
                {
                    ListBox1.Items.Insert(0, new ListItem("<----- मुख पृष्ठ ------>", "0"));
                }
                ListBox1.Items[0].Selected = true;
            }
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get child records

    public void bindChildData(int level, int Parent_ID, int Postion_ID)
    {
        ListItem lic = default(ListItem);
        Menu_ManagementBL _subMenuBL = new Menu_ManagementBL();
        DataSet dsubLinks = new DataSet();
        try
        {
            lnkObject.LinkParentId = Parent_ID;
            lnkObject.LinkLevel = level;
            lnkObject.PositionId = Postion_ID;

            dsubLinks = _subMenuBL.get_SublinksID_of_Parant_From_Web(lnkObject);
            if (dsubLinks.Tables[0].Rows.Count > 0)
            {
                string str = "• ";
                for (int j = 0; j < level - 1; j++)
                {
                    str = str + "• ";
                }
                for (int i = 0; i <= dsubLinks.Tables[0].Rows.Count - 1; i++)
                {

                    lic = new ListItem(str + dsubLinks.Tables[0].Rows[i]["NAME"].ToString(), dsubLinks.Tables[0].Rows[i]["Link_Id"].ToString());
                    ListBox1.Items.Add(lic);
                    lnkObject.LinkParentId = Parent_ID;
                    lnkObject.LinkLevel = level + 1;
                    lnkObject.PositionId = Postion_ID;
                    bindChildData(level + 1, Convert.ToInt32(dsubLinks.Tables[0].Rows[i]["Link_Id"]), Postion_ID);
                }
            }
        }
        catch
        {
            throw;
        }

    }
    #endregion

    #region Function to bind language in dropDownlist according to permission

    public void bindropDownlistLang()
    {
        try
        {

            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            p_Var.dSet = obj_miscelBL.getLanguagePermission(obj_userOB);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true && Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
                {
                    obj_userOB.english = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.English);
                    obj_userOB.hindi = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(obj_userOB.english).Append(",");
                    p_Var.sbuilder.Append(obj_userOB.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true)
                {
                    obj_userOB.hindi = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(obj_userOB.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
                {
                    obj_userOB.english = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.English);

                    p_Var.sbuilder.Append(obj_userOB.english);
                }
                obj_userOB.LangId = p_Var.sbuilder.ToString().Trim();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                p_Var.dSet = null;
                p_Var.dSet = obj_miscelBL.getLanguage(obj_userOB);
                trLanguage.Visible = true;
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

    #region Function to bind data in EDIT mode

    public void bindData_For_Editing(int StatusID, int TempLink_Id)
    {


        try
        {

            lnkObject.StatusId = StatusID;
            lnkObject.TempLinkId = TempLink_Id;

            p_Var.dSet = menuBL.getMenu_For_Editing(lnkObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                txtBrowserTitle.Text =HttpUtility.HtmlDecode( p_Var.dSet.Tables[0].Rows[0]["Browser_Title"].ToString());
                txtLinkUrl.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Url"].ToString());
                txtMenuName.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Name"].ToString());
                txtMetaDescription.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Mate_Description"].ToString());
                txtMetaTitle.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Mate_Description"].ToString()); 
                txtMetaKeyword.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Meta_Keywords"].ToString());
                txtPageTitle.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Page_Title"].ToString());
                txtUrlName.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["PlaceholderOne"].ToString());
                p_Var.i = Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["Link_Type_Id"]);
                ddlMenuType.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["Link_Type_Id"].ToString();
                if (p_Var.dSet.Tables[0].Rows[0]["StartDate"] != DBNull.Value)
                {
                    txtStartDate.Text = obj_miscelBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["StartDate"].ToString());
                }
                else
                {
                    txtStartDate.Text = null;
                }
                if (p_Var.dSet.Tables[0].Rows[0]["EndDate"] != DBNull.Value)
                {
                    txtEndDate.Text = obj_miscelBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["EndDate"].ToString());
                }
                else
                {
                    txtEndDate.Text = null;
                }
                if (p_Var.i == 1)
                {
                    trFckEditor.Visible = true;
                    FCKeditor1.Visible = true;
                   // FCKeditor1.Value = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Details"].ToString());
                    FCKeditor1.Value = p_Var.dSet.Tables[0].Rows[0]["Details"].ToString();
                    trFileupload.Visible = false;
                    trLinkUrl.Visible = false;
                }
                if (p_Var.i == 2)
                {
                    trFckEditor.Visible = false;
                    FCKeditor1.Visible = false;
                    trFileupload.Visible = true;
                    lblmenuMsg.Visible = true;
                    lblFileName.Visible = true;
                    lblFileName.Text = p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString();

                    trLinkUrl.Visible = false;
                }
                if (p_Var.i == 3)
                {
                    trFckEditor.Visible = false;
                    FCKeditor1.Visible = false;
                    trFileupload.Visible = false;
                    trPageTitle.Visible = false;
                    trBrowserTitle.Visible = false;
                    trMetadescription.Visible = false;
                    trMetaTitle.Visible = false;
                    trmetakeyword.Visible = false;
                    trLinkUrl.Visible = true;
                    txtLinkUrl.Visible = true;
                    txtLinkUrl.Text = p_Var.dSet.Tables[0].Rows[0]["Url"].ToString();
                }


                if (p_Var.dSet.Tables[0].Rows[0]["Link_Parent_Id"] != DBNull.Value)
                {
                    ViewState["ParentID"] = Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["Link_Parent_Id"]);
                }
                else
                {
                    ViewState["ParentID"] = DBNull.Value;
                }
                if (p_Var.dSet.Tables[0].Rows[0]["Position_Id"] != DBNull.Value)
                {
                    ViewState["PositionID"] = Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["Position_Id"]);
                }
                else
                {
                    ViewState["PositionID"] = DBNull.Value;
                }
                if (p_Var.dSet.Tables[0].Rows[0]["Link_Level"] != DBNull.Value)
                {
                    ViewState["Level"] = Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["Link_Level"]);
                }
                else
                {
                    ViewState["Level"] = DBNull.Value;
                }
                if (p_Var.dSet.Tables[0].Rows[0]["Link_Type_Id"] != DBNull.Value)
                {
                    ViewState["LinkTypeId"] = Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["Link_Type_Id"]);
                }
                else
                {
                    ViewState["LinkTypeId"] = DBNull.Value;
                }
                if (p_Var.dSet.Tables[0].Rows[0]["Link_ID"] != DBNull.Value)
                {
                    ViewState["Link_ID"] = Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["Link_ID"]);
                }
                else
                {
                    ViewState["Link_ID"] = DBNull.Value;
                }

                if (p_Var.dSet.Tables[0].Rows[0]["Lang_ID"] != DBNull.Value)
                {
                    ViewState["LangID"] = p_Var.dSet.Tables[0].Rows[0]["Lang_Id"].ToString();
                }
                else
                {
                    ViewState["LangID"] = DBNull.Value;
                }

                if (p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"] != DBNull.Value)
                {
                    ViewState["DeptID"] = p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"].ToString();
                }
                else
                {
                    ViewState["DeptID"] = DBNull.Value;
                }
            }

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

            obj_userOB.ModuleId = Convert.ToInt32(Session["ModuleID"]);
            obj_userOB.DepttId = Convert.ToInt32(Session["Dept_ID"]);
            p_Var.dSet = obj_UserBL.ASP_Get_Deptt_Name(obj_userOB);
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
}
