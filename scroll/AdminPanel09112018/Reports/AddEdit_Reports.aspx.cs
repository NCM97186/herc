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

public partial class Auth_AdminPanel_Reports_AddEdit_Reports : CrsfBase //System.Web.UI.Page
{
    //Area for all the data declaration

    #region Data declaration zone

    //DataSet ds = new DataSet();


    TariffBL tariffBL = new TariffBL();
    TariffOB tariffOB = new TariffOB();
    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    Project_Variables p_Var = new Project_Variables();
    UserOB obj_userOB = new UserOB();
    UserBL obj_UserBL = new UserBL();
    Miscelleneous_BL obj_miscelBL = new Miscelleneous_BL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion

    //End

    //Area for the page load event
    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.url = "~/" + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/";
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

            bindropDownlistLang();
            Get_Deptt_Name();
         

            if (Request.QueryString["TempLink_Id"] == null) //change the text of the label according to Add/Edit
            {
                
                Get_CategoryName("2");
                      
                btnMenu.Text = "Save";

                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Add  Report";
                this.Page.Title = "Add Report: HERC";
            }
            else
            {
                btnMenu.Text = "Update";
                tr2.Visible = false;
                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Edit  Report";
                this.Page.Title = "Edit Report: HERC";
            }


            bindTariffType();

            if (Request.QueryString["TempLink_Id"] == null) //change the text of the label according to Add/Edit
            {
               
            }
            else
            {
                trLanguage.Visible = false;
                trMenu.Visible = false;
                trDepartment.Visible = false;

                bindData_For_Editing(Convert.ToInt32(Request.QueryString["statusID"].ToString()), Convert.ToInt32(Request.QueryString["TempLink_Id"].ToString()));
                BindData(Convert.ToInt32(Request.QueryString["TempLink_Id"].ToString()));
            }

            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }

    }

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

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            //obj_userOB.DepttId = Convert.ToInt32(Session["Dept_ID"]);
            obj_userOB.DepttId =Convert.ToInt32(Module_ID_Enum.hercType.ombudsman);
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

    #region Function to bind dropDownlist with Tariff Category

    public void bindTariffType()
    {
        TariffBL obj_tariffBL = new TariffBL();
        try
        {
            p_Var.dSet = obj_tariffBL.getTariffType();
            ddlTariffType.DataSource = p_Var.dSet.Tables[0];
            ddlTariffType.DataTextField = "TariffName";
            ddlTariffType.DataValueField = "Cat_Id";
            ddlTariffType.DataBind();


        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region dropDownlist ddlDepartment selectedIndexChanged event

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_CategoryName(ddlDepartment.SelectedValue.ToString());
        Get_CategoryName("2");
        if (ddlDepartment.SelectedValue.ToString() == "2")
        {
            trMenu.Visible = false;
        }
        else
        {
            //trMenu.Visible = true;
        }
    }

    #endregion

    #region dropDownlist ddlLanguage selectedIndexChanged event zone

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    #endregion

    #region dropDownlist ddlMenuPosition selectedIndexChanged event zone

    protected void ddlTariffType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    #endregion

    #region button btnMenu click event to add new menu

    protected void btnMenu_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (Page.IsValid)
                {
                    Add_New_Tariff();
                }
            }
        }
		
		else{
			Session.Abandon();
		    Response.Redirect("~/Auth/AdminPanel/Login.aspx");
			}
    }

    #endregion

    #region button click event to Back

    protected void btnBack_Click(object sender, EventArgs e)
    {
       Response.Redirect(ResolveUrl("~/auth/adminpanel/Reports/") + "View_Reports.aspx?ModuleID=29");

       
    }

    #endregion


    //User define fuction Area


    #region Function to bind data in EDIT mode

    public void bindData_For_Editing(int StatusID, int TempLink_Id)
    {


        try
        {
            if (Request.QueryString["Catid"] != null && Request.QueryString["Catid"] != "")
            {
                tariffOB.CatId = Convert.ToInt32(Request.QueryString["Catid"]);
            }
            else
            {
                tariffOB.CatId = null;

            }
            tariffOB.StatusId = Convert.ToInt32(Request.QueryString["statusID"]);
            tariffOB.TempLinkId = TempLink_Id;

            p_Var.dSet = tariffBL.getMenu_For_Editing(tariffOB);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                txtMetaDescription.Text = p_Var.dSet.Tables[0].Rows[0]["Mate_Description"].ToString();
                txtMetaTitle.Text = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
                txtMetaKeyword.Text = p_Var.dSet.Tables[0].Rows[0]["Meta_Keywords"].ToString();
                ddlMetaLang.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();

                txtYear.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Year"].ToString());

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

                trFckEditor.Visible = true;
                FCKeditor1.Visible = true;
                FCKeditor1.Value = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Details"].ToString());

                if (p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString() != "")
                {
                    //lblFileName.Visible = true;
                    //lnkFileConnectedRemove.Visible = true;
                   // ltrlDownload.Visible = true;

                    //p_Var.sbuilder.Append("<a href='" + ResolveUrl(p_Var.url) + p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString() + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='Download File' width='15' alt='Download File' height=\"15\" /> " + "</a>");
                   // p_Var.sbuilder.Append("<hr />");
                    //ltrlDownload.Text = p_Var.sbuilder.ToString();

                   // lnkFileConnectedRemove.OnClientClick = "javascript:return confirm('Are you sure you want to delete this file :- " + p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString() + " ?')";

                    lblFileName.Text = p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString();
                }
                else
                {
                    lnkFileConnectedRemove.Visible = false;
                    ltrlDownload.Visible = false;
                    lblFileName.Visible = false;
                    //lnkFileRemove.Visible = false;
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

    protected void Add_New_Tariff()
    {

        if (Request.QueryString["TempLink_Id"] == null)
        {
            try
            {
                //End

                tariffOB.ActionType = 1;
                tariffOB.Year = HttpUtility.HtmlEncode(txtYear.Text);
                tariffOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
                tariffOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
                if (Convert.ToInt32(ddlDepartment.SelectedValue) == 1)
                {
                    tariffOB.CatId = Convert.ToInt32(ddlTariffType.SelectedValue);
                }
                else
                {
                    tariffOB.CatId = null;
                }

                tariffOB.CatTypeId = Convert.ToInt32(ddlcategory.SelectedValue);
                
                tariffOB.Date_Inserted = System.DateTime.Now;
                tariffOB.InsertedBy = Convert.ToInt32(Session["User_Id"]);
                tariffOB.UserID = Convert.ToInt32(Session["User_Id"]);
                tariffOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);   //should be change according to the module
                
                tariffOB.IpAddress = Miscelleneous_DL.getclientIP();
               
                tariffOB.MateDescription = txtMetaDescription.Text;
                tariffOB.MetaKeywords = txtMetaKeyword.Text;
                tariffOB.MetaTitle = txtMetaTitle.Text;
                tariffOB.MetaKeyLanguage = ddlMetaLang.SelectedValue;

                tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                if (txtStartDate.Text != null && txtStartDate.Text != "")
                {
                    tariffOB.StartDate = obj_miscelBL.getDateFormat(txtStartDate.Text);
                   
                }
                else
                {
                    tariffOB.StartDate = System.DateTime.Now;
                }
                if (txtEndDate.Text != null && txtEndDate.Text != "")
                {
                    tariffOB.EndDate = obj_miscelBL.getDateFormat(txtEndDate.Text);
                }
                else
                {
                    tariffOB.EndDate = null;
                }
                tariffOB.ModuleId = 29;
                tariffOB.details = HttpUtility.HtmlEncode(obj_miscelBL.RemoveUnnecessaryHtmlTagHtml(FCKeditor1.Value));

                p_Var.Result = tariffBL.insert_New_Tariff(tariffOB);

                if (p_Var.Result > 0)
                {
                    //This is for Report files
                    if (fileUpload_Menu.PostedFile != null && fileUpload_Menu.PostedFile.InputStream.Length != 0)
                    {
                        tariffOB.TempLinkId = p_Var.Result;
                        tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.Approved;
                        p_Var.ext = System.IO.Path.GetExtension(this.fileUpload_Menu.PostedFile.FileName);
                        p_Var.ext = p_Var.ext.ToUpper();
                            if (p_Var.ext.Equals(".PDF"))
                            {
                                p_Var.Path = p_Var.url;
                                p_Var.Filename = fileUpload_Menu.FileName;
                                p_Var.ext = Path.GetExtension(fileUpload_Menu.FileName);
                                p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(fileUpload_Menu.FileName);
                                p_Var.Filename = obj_miscelBL.getUniqueFileName(p_Var.Filename, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);

                                fileUpload_Menu.SaveAs(Server.MapPath(p_Var.Path + p_Var.Filename));
                                tariffOB.FileName = p_Var.Filename;

                            }

                            tariffBL.InsertReportFiles(tariffOB);
                    }

                    obj_audit.ActionType = "I";
                    obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                    obj_audit.UserName = Session["UserName"].ToString();
                    obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                    obj_audit.IpAddress = obj_miscelBL.IpAddress();
                    obj_audit.Title = "Report";
                    obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                    Session["msg"] = "Report has been added successfully.";

                    Session["Redirect"] = "~/Auth/AdminPanel/Reports/View_Reports.aspx?ModuleID=29";

                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            catch
            {
                throw;
            }

        }
        else
        {

            try
            {
                
                tariffOB.ActionType = (int)Module_ID_Enum.Project_Action_Type.update;
                tariffOB.LastUpdatedBy = Convert.ToInt32(Session["user_id"]);
                if (ViewState["Link_ID"] != DBNull.Value)
                {
                    tariffOB.linkID = Convert.ToInt32(Request.QueryString["TempLink_Id"]);
                }
                else
                {
                    tariffOB.linkID = null;
                }
               
                tariffOB.Year = HttpUtility.HtmlEncode(txtYear.Text);
                tariffOB.ModuleId = 29;
                tariffOB.DepttId = Convert.ToInt32(Session["Deptt_Id"]);
                if (Convert.ToInt32(Session["Deptt_Id"]) == 1)
                {
                    tariffOB.CatId = Convert.ToInt32(Request.QueryString["Catid"].ToString());
                }
                else
                {
                    tariffOB.CatId = null;
                }
                
                tariffOB.CatTypeId = Convert.ToInt32(Session["CatTypeId"]);
                tariffOB.MateDescription = txtMetaDescription.Text;
                tariffOB.MetaKeywords = txtMetaKeyword.Text;
                tariffOB.MetaTitle = txtMetaTitle.Text;
                tariffOB.MetaKeyLanguage = ddlMetaLang.SelectedValue;
                tariffOB.details = HttpUtility.HtmlDecode(obj_miscelBL.RemoveUnnecessaryHtmlTagHtml(FCKeditor1.Value));
                tariffOB.Date_Inserted = System.DateTime.Now;
                tariffOB.TempLinkId = Convert.ToInt32(Request.QueryString["TempLink_Id"]);
                tariffOB.UserID = Convert.ToInt32(Session["User_Id"]);
                tariffOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);   //should be change according to the module

                tariffOB.IpAddress = Miscelleneous_DL.getclientIP();
                
                if (txtStartDate.Text != null && txtStartDate.Text != "")
                {
                    tariffOB.StartDate = obj_miscelBL.getDateFormat(txtStartDate.Text);

                }
                else
                {
                    tariffOB.StartDate = System.DateTime.Now;
                }
				if (txtEndDate.Text != null && txtEndDate.Text != "")
                {
                    tariffOB.EndDate = obj_miscelBL.getDateFormat(txtEndDate.Text);
                }
                else
                {
                    
                        tariffOB.EndDate = null;
                 
                }
                if (ViewState["LangID"] != DBNull.Value)
                {
                    tariffOB.LangId = Convert.ToInt32(ViewState["LangID"]);
                }
                else
                {
                    tariffOB.LangId = null;
                }
               
                tariffOB.StatusId = Convert.ToInt32(Session["Status_Id"]);
               
                p_Var.Result = tariffBL.insert_New_Tariff(tariffOB);
                if (p_Var.Result > 0)
                {
                    //This is for Report files
                    if (fileUpload_Menu.PostedFile != null && fileUpload_Menu.PostedFile.InputStream.Length != 0)
                    {
                        tariffOB.TempLinkId =   p_Var.Result;
                        tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.Approved;
                        p_Var.ext = System.IO.Path.GetExtension(this.fileUpload_Menu.PostedFile.FileName);
                        p_Var.ext = p_Var.ext.ToUpper();
                        if (p_Var.ext.Equals(".PDF"))
                        {
                            p_Var.Path = p_Var.url;
                            p_Var.Filename = fileUpload_Menu.FileName;
                            p_Var.ext = Path.GetExtension(fileUpload_Menu.FileName);
                            p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(fileUpload_Menu.FileName);
                            p_Var.Filename = obj_miscelBL.getUniqueFileName(p_Var.Filename, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);

                            fileUpload_Menu.SaveAs(Server.MapPath(p_Var.Path + p_Var.Filename));
                            tariffOB.FileName = p_Var.Filename;

                          
                        }

                        tariffBL.InsertReportFiles(tariffOB);
                    }
                    obj_audit.ActionType = "U";
                    obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                    obj_audit.UserName = Session["UserName"].ToString();
                    obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                    obj_audit.IpAddress = obj_miscelBL.IpAddress();
                    obj_audit.Title = "Report";
                    obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);


                    Session["msg"] = "Report has been updated successfully.";


                    Session["Redirect"] = "~/Auth/AdminPanel/Reports/View_Reports.aspx?ModuleID=29";


                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx?ModuleID=29");
                }
            }
            catch
            {
                throw;
            }

        }
    }
    //end
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if(Request.QueryString["TempLink_Id"]!=null)
        {
             bindData_For_Editing(Convert.ToInt32(Request.QueryString["statusID"].ToString()), Convert.ToInt32(Request.QueryString["TempLink_Id"].ToString()));
        }
        else
        {
            txtEndDate.Text = "";
            txtStartDate.Text = "";
            txtYear.Text = "";
            FCKeditor1.Value = "";
        }
    }


    #region Function bind Category name in dropDownlist

    public void Get_CategoryName(string DepttId)
    {
        try
        {
            tariffOB.DepttId = Convert.ToInt32(DepttId);
            p_Var.dSetChildData = tariffBL.getCategoty(tariffOB);
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {
                ddlcategory.DataSource = p_Var.dSetChildData;
                ddlcategory.DataValueField = "Cat_Id";
                ddlcategory.DataTextField = "TariffName";
                ddlcategory.DataBind();
            }
            //ddlcategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcategory.SelectedValue == "8" || ddlcategory.SelectedValue == "9")
        {
            trMenu.Visible = false;
        }
        else
        {
            trMenu.Visible = false;
        }
    }
    protected void lnkFileConnectedRemove_Click(object sender, EventArgs e)
    {
        lblFileName.Text = string.Empty;
        lnkFileConnectedRemove.Visible = false;
        ltrlDownload.Visible = false;
    }

    // This is for report files
   

    protected void datalistFileName_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("File"))
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            tariffOB.linkID = id;
            p_Var.Result1 = tariffBL.UpdateFileStatusForReports(tariffOB);

            if (p_Var.Result1 > 0)
            {

                Label filename = (Label)e.Item.FindControl("lblFile");
                Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");

                LinkButton RemoveFileLink = (LinkButton)e.Item.FindControl("lnkFileConnectedRemove");

                filename.Visible = false;
                //lblComments.Visible = false;
                RemoveFileLink.Visible = false;
                ltrlDownload.Visible = false;
            }


            bindData_For_Editing(Convert.ToInt32(Request.QueryString["statusID"].ToString()), Convert.ToInt32(Request.QueryString["TempLink_Id"].ToString()));

        }
    }

    protected void datalistFileName_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var link = e.Item.FindControl("lnkFileConnectedRemove") as LinkButton;
            var hiddenField = e.Item.FindControl("hiddenFieldID") as HiddenField;


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
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["TempLink_Id"]))
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
                        link.Visible = true;//if user has right then enabled else disabled
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["TempLink_Id"]))
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

            p_Var.sbuilder.Append("<a href='" + ResolveUrl(p_Var.url) + filename.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='Download File' width='15' alt='Download File' height=\"15\" /> " + "</a>");
            p_Var.sbuilder.Append("<hr />");
            ltrlDownload.Text = p_Var.sbuilder.ToString();

            LinkButton lnkFileConnectedRemove = (LinkButton)e.Item.FindControl("lnkFileConnectedRemove");
            lnkFileConnectedRemove.OnClientClick = "javascript:return confirm('Are you sure you want to delete this file :- " + filename.Text + " ?')";
        }
    }


    #region function  bind with Repeator
    public void BindData(int ReportId)
    {
        tariffOB.TempLinkId = ReportId;
        p_Var.dsFileName = tariffBL.getFileNameForReport(tariffOB);
        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
        {
            trdataList.Visible = true;
            
            datalistFileName.DataSource = p_Var.dsFileName;
            datalistFileName.DataBind();
        }
        else
        {
            trdataList.Visible = false;
        }
    }


    #endregion 

    #region function to bind metalanguage

    public void displayMetaLang()
    {
        p_Var.dSet = obj_miscelBL.getMetaLanguage();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlMetaLang.DataSource = p_Var.dSet;
            ddlMetaLang.DataTextField = "languagename";
            ddlMetaLang.DataValueField = "LanguageKey";
            ddlMetaLang.DataBind();
        }
    }

    #endregion

    #region Custom validator to validate extension of upload pdf files

    protected void CustvalidFileUplaod_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Get file name
        p_Var.ext = System.IO.Path.GetExtension(fileUpload_Menu.PostedFile.FileName);
        p_Var.ext = p_Var.ext.ToLower();
        if (p_Var.ext == ".pdf")
        {
            p_Var.flag = obj_miscelBL.GetActualFileType_pdf(fileUpload_Menu.PostedFile.InputStream);
        }
        else
        {
            p_Var.flag = obj_miscelBL.GetActualFileType(fileUpload_Menu.PostedFile.InputStream);
        }

        if (p_Var.flag == true)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }

        //string fileMultiple = string.Empty;
        //HttpFileCollection hfc = Request.Files;
        //bool strem = true; ;
        //for (int i = 0; i < hfc.Count; i++)
        //{
        //    HttpPostedFile hpf = hfc[i];

        //    fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
        //    strem = obj_miscelBL.GetActualFileType_pdf(hfc[i].InputStream);
        //    if (strem == true)
        //    {

        //        args.IsValid = true;
        //    }
        //    else
        //    {
        //        args.IsValid = false;
        //        break;
        //    }

        //}

    }

    #endregion
}
