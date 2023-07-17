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
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;


public partial class Auth_AdminPanel_ProfileManagement_ProfileAdd : CrsfBase //System.Web.UI.Page
{

    Project_Variables p_Var = new Project_Variables();
    UserOB obj_userOB = new UserOB();
    UserBL obj_UserBL = new UserBL();
    Miscelleneous_BL obj_miscelBL = new Miscelleneous_BL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    ProfileBL profileBL = new ProfileBL();
    ProfileOB profileOB = new ProfileOB();
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();


    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.imageUrl = ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/Image/";
        p_Var.thumbnailUrl = ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/Image/";
        bindropDownlistLang();
        if (!IsPostBack)
        {
            ViewState.Remove("visible");
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
            bindProfileNevigation();
            Get_Deptt_Name();

            if (Request.QueryString["TempProfile_Id"] == null) //change the text of the label according to Add/Edit
            {

                Label lblModulename = Master.FindControl("lblModulename") as Label;
                BtnSubmit.Text = "Submit";
                lblModulename.Text = ": Add Profile";
                this.Page.Title = "Add Profile: HERC";
            }
            else
            {
                BtnSubmit.Text = "Update";
                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Edit Profile";
                this.Page.Title = "Edit Profile: HERC";

                PLanguage.Visible = false;
                PDepartment.Visible = false;
                P1.Visible = true;
                //trDepartment.Visible = false;

                bindData_For_Editing(Convert.ToInt32(Request.QueryString["statusID"].ToString()), Convert.ToInt32(Request.QueryString["TempProfile_Id"].ToString()));
            }
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }

    }


    #region Function to bind data in EDIT mode

    public void bindData_For_Editing(int StatusID, int TempProfile_Id)
    {

        try
        {
            LblOldImage.Visible = true;
            LblImageName.Visible = true;
            lnkImageRemove.Visible = true;
            profileOB.StatusId = Convert.ToInt32(Session["Status_Id"]);
            profileOB.NevigationId = Convert.ToInt32(Session["Nevigation_ID"]);
            profileOB.LangId = Convert.ToInt32(Session["Lang_ID"]);
            profileOB.profile_Id = Convert.ToInt32(Request.QueryString["TempProfile_Id"]);
            p_Var.dSetCompare = profileBL.GetProfileForEditing(profileOB);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {
                txtMetaDescription.Text = p_Var.dSetCompare.Tables[0].Rows[0]["MetaDescriptions"].ToString();
                txtMetaTitle.Text = p_Var.dSetCompare.Tables[0].Rows[0]["MetaTitle"].ToString();
                txtMetaKeyword.Text = p_Var.dSetCompare.Tables[0].Rows[0]["MetaKeywords"].ToString();
                ddlMetaLang.SelectedValue = p_Var.dSetCompare.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();

                txtname.Text = p_Var.dSetCompare.Tables[0].Rows[0]["Name"].ToString();
                txtDesignation.Text = p_Var.dSetCompare.Tables[0].Rows[0]["Designation"].ToString();
                txtEmail.Text = p_Var.dSetCompare.Tables[0].Rows[0]["Email"].ToString();
                txtstartdate.Text = p_Var.dSetCompare.Tables[0].Rows[0]["Start_Date"].ToString();
                txtendate.Text = p_Var.dSetCompare.Tables[0].Rows[0]["End_Date"].ToString();
                txtEpabx_Ext.Text = p_Var.dSetCompare.Tables[0].Rows[0]["Epabx_Ext"].ToString();
                FCKeditor1.Value = HttpUtility.HtmlDecode(p_Var.dSetCompare.Tables[0].Rows[0]["Details"].ToString());
                txtSubject.Text = p_Var.dSetCompare.Tables[0].Rows[0]["Subject"].ToString();
                txtPhone.Text = p_Var.dSetCompare.Tables[0].Rows[0]["Phone"].ToString();
                LblOldImage.Text = p_Var.dSetCompare.Tables[0].Rows[0]["Image_Name"].ToString();
                if (LblOldImage.Text == "")
                {
                    lnkImageRemove.Visible = false;
                    ViewState["visible"] = false;
                }
                else
                {
                    lnkImageRemove.Visible = true;
                    ViewState["visible"] = true;
                }
                ViewState["Deptt_Id"] = p_Var.dSetCompare.Tables[0].Rows[0]["Deptt_Id"].ToString();
                ddlNevigation.SelectedValue = p_Var.dSetCompare.Tables[0].Rows[0]["Nevigation_id"].ToString();
            }
        }
        catch
        {
            throw;
        }

    }

    #endregion

    protected void lnkImageRemove_Click(object sender, EventArgs e)
    {
        LblImageName.Visible = false;
       
        ViewState["visible"] = false;
        LblOldImage.Visible = false;
        lnkImageRemove.Visible = false;
    }
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
                    Add_New_Profile();
                }
            }
        }
		else{
			Session.Abandon();
		    Response.Redirect("~/Auth/AdminPanel/Login.aspx");
			}
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["TempProfile_Id"] == null)
        {
            txtPhone.Text = "";
            txtname.Text = "";
            txtEpabx_Ext.Text = "";
            txtEmail.Text = "";
            txtDesignation.Text = "";
            FCKeditor1.Value = "";
            txtendate.Text = "";
            txtstartdate.Text = "";
            txtSubject.Text = "";
        }
        else
        {
            bindData_For_Editing(Convert.ToInt32(Request.QueryString["statusID"].ToString()), Convert.ToInt32(Request.QueryString["TempProfile_Id"].ToString()));
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
         Response.Redirect(ResolveUrl("~/auth/adminpanel/ProfileManagement/") + "ProfileDisplay.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));
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

    #region Function to bind dropDownlist with Profile Nevigation

    public void bindProfileNevigation()
    {
        ProfileBL profileBL = new ProfileBL();
        try
        {
            p_Var.dSet = profileBL.getNavigation();
            ddlNevigation.DataSource = p_Var.dSet.Tables[0];
            ddlNevigation.DataTextField = "NavigationName";
            ddlNevigation.DataValueField = "Navigation_Id";
            ddlNevigation.DataBind();


        }
        catch
        {
            throw;
        }

    }

    #endregion

    # region Add new profile
    public void Add_New_Profile()
    {

        if (Request.QueryString["TempProfile_Id"] == null)
        {
            try
            {


                //End
                LblOldImage.Visible = false;
                LblImageName.Visible = false;
                lnkImageRemove.Visible = false;
                profileOB.ActionType = 1;
                profileOB.NevigationId = Convert.ToInt32(ddlNevigation.SelectedValue);
                profileOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);

                profileOB.NAME = HttpUtility.HtmlEncode(txtname.Text);
                if (txtDesignation.Text != null && txtDesignation.Text != "")
                {
                    profileOB.designation = HttpUtility.HtmlEncode(txtDesignation.Text);
                }
                else
                {
                    profileOB.designation = null;
                }

                profileOB.subject = HttpUtility.HtmlEncode(txtSubject.Text);
                profileOB.phone = txtPhone.Text.ToString();
                if (txtEpabx_Ext.Text != null && txtEpabx_Ext.Text != "")
                {
                    profileOB.epabxte = Convert.ToInt64(txtEpabx_Ext.Text);
                }
                else
                {
                    profileOB.epabxte = null;
                }
                profileOB.email = HttpUtility.HtmlEncode(txtEmail.Text);
                profileOB.Date_Inserted = System.DateTime.Now;
                profileOB.InsertedBy = Convert.ToInt32(Session["User_Id"]);
                profileOB.UserID = Convert.ToInt32(Session["User_Id"]);
                profileOB.IpAddress = Miscelleneous_DL.getclientIP();
                profileOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                profileOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);

                profileOB.MetaDescription = txtMetaDescription.Text;
                profileOB.MetaKeyWords = txtMetaKeyword.Text;
                profileOB.MetaTitle = txtMetaTitle.Text;
                profileOB.MetaKeyLanguage = ddlMetaLang.SelectedValue;
                if (txtstartdate.Text != null && txtstartdate.Text != "")
                {
                    profileOB.StartDate = obj_miscelBL.getDateFormat(txtstartdate.Text);
                }
                else
                {
                    profileOB.StartDate = System.DateTime.Now;
                }
                if (txtendate.Text != null && txtendate.Text != "")
                {
                    profileOB.EndDate = obj_miscelBL.getDateFormat(txtendate.Text);
                }
                else
                {
                    profileOB.EndDate = null;
                   
                }
                profileOB.ModuleId = 28;
                profileOB.IpAddress = obj_miscelBL.IpAddress();
                profileOB.details = HttpUtility.HtmlEncode(obj_miscelBL.RemoveUnnecessaryHtmlTagHtml(FCKeditor1.Value));
                if (FileUploadImage.PostedFile != null && FileUploadImage.PostedFile.ContentLength > 0)
                {
                    p_Var.ext = System.IO.Path.GetExtension(this.FileUploadImage.PostedFile.FileName);
                    p_Var.ext = p_Var.ext.ToUpper();
                    if (p_Var.ext.Equals(".JPEG") || p_Var.ext.Equals(".JPG") || p_Var.ext.Equals(".GIF") || p_Var.ext.Equals(".PNG"))
                    {

                        p_Var.Path = p_Var.imageUrl;
                        p_Var.Filename = FileUploadImage.FileName;
                        p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(FileUploadImage.FileName);
                        //  p_Var.ext = Path.GetExtension(ImageUploader.FileName);

                        p_Var.Filename = obj_miscelBL.getUniqueFileName(p_Var.Filename, Server.MapPath(ResolveUrl("~/") + p_Var.Path), p_Var.filenamewithout_Ext, p_Var.ext);
                        FileUploadImage.SaveAs(Server.MapPath(ResolveUrl("~/") + p_Var.Path + "/" + p_Var.Filename));
                        //code for Thumpnails images
                        Stream sourcepath = FileUploadImage.PostedFile.InputStream;
                        p_Var.targetpath = Server.MapPath(ResolveUrl("~/") + p_Var.thumbnailUrl + p_Var.Filename);
                        GenerateThumbnails(4, sourcepath, p_Var.targetpath); //function calling 
                        profileOB.ImageName = p_Var.Filename;
                    }

                }
                else
                {
                    profileOB.ImageName = "";
                }

                p_Var.Result = profileBL.insert_New_Profile(profileOB);

                if (p_Var.Result > 0)
                {

                    obj_audit.ActionType = "I";
                    obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                    obj_audit.UserName = Session["UserName"].ToString();
                    obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                    obj_audit.IpAddress = obj_miscelBL.IpAddress();
                    obj_audit.Title = txtname.Text;
                    obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);
                    Session["msg"] = "Profile has been added successfully.";

                    Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";

                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
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


                LblOldImage.Visible = true;
                LblImageName.Visible = true;
                lnkImageRemove.Visible = true;
                profileOB.ActionType = 2;
                
                profileOB.NevigationId = Convert.ToInt32(ddlNevigation.SelectedValue);
                profileOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
                profileOB.NAME = HttpUtility.HtmlEncode(txtname.Text);
                profileOB.designation = HttpUtility.HtmlEncode(txtDesignation.Text);
                profileOB.temp_profile_Id = Convert.ToInt32(Request.QueryString["TempProfile_Id"]);
                profileOB.subject = HttpUtility.HtmlEncode(txtSubject.Text);
                profileOB.phone = txtPhone.Text.ToString();
                profileOB.MetaDescription = txtMetaDescription.Text;
                profileOB.MetaKeyWords = txtMetaKeyword.Text;
                profileOB.MetaTitle = txtMetaTitle.Text;
                profileOB.MetaKeyLanguage = ddlMetaLang.SelectedValue;
                if (txtEpabx_Ext.Text != null && txtEpabx_Ext.Text != "")
                {
                    profileOB.epabxte = Convert.ToInt64(txtEpabx_Ext.Text);
                }
                else
                {
                    profileOB.epabxte = null;
                }
                profileOB.email = HttpUtility.HtmlEncode(txtEmail.Text);

                profileOB.Date_Inserted = System.DateTime.Now;
                profileOB.InsertedBy = Convert.ToInt32(Session["User_Id"]);
                profileOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                profileOB.UserID = Convert.ToInt32(Session["User_Id"]);
                profileOB.IpAddress = Miscelleneous_DL.getclientIP();
                profileOB.StatusId = Convert.ToInt32(Session["Status_Id"]);
                if (txtstartdate.Text != null && txtstartdate.Text != "")
                {
                    profileOB.StartDate = obj_miscelBL.getDateFormat(txtstartdate.Text);
                }
                else
                {
                    profileOB.StartDate = System.DateTime.Now;
                }
                if (txtendate.Text != null && txtendate.Text != "")
                {
                    profileOB.EndDate = obj_miscelBL.getDateFormat(txtendate.Text);
                }
                else
                {
                    profileOB.EndDate = null;
                  
                }
                profileOB.ModuleId = 28;
                profileOB.IpAddress = obj_miscelBL.IpAddress();
                profileOB.details = HttpUtility.HtmlEncode(obj_miscelBL.RemoveUnnecessaryHtmlTagHtml(FCKeditor1.Value));
                if (FileUploadImage.PostedFile != null && FileUploadImage.PostedFile.ContentLength > 0)
                {
                    p_Var.ext = System.IO.Path.GetExtension(this.FileUploadImage.PostedFile.FileName);
                    p_Var.ext = p_Var.ext.ToUpper();
                    if (p_Var.ext.Equals(".JPEG") || p_Var.ext.Equals(".JPG") || p_Var.ext.Equals(".GIF") || p_Var.ext.Equals(".PNG"))
                    {

                        p_Var.Path = p_Var.imageUrl;
                        p_Var.Filename = FileUploadImage.FileName;
                        p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(FileUploadImage.FileName);
                        

                        p_Var.Filename = obj_miscelBL.getUniqueFileName(p_Var.Filename, Server.MapPath(ResolveUrl("~/") + p_Var.Path), p_Var.filenamewithout_Ext, p_Var.ext);
                        FileUploadImage.SaveAs(Server.MapPath(ResolveUrl("~/") + p_Var.Path + "/" + p_Var.Filename));
                        //code for Thumpnails images
                        Stream sourcepath = FileUploadImage.PostedFile.InputStream;
                        p_Var.targetpath = Server.MapPath(ResolveUrl("~/") + p_Var.thumbnailUrl + p_Var.Filename);
                        GenerateThumbnails(4, sourcepath, p_Var.targetpath); //function calling 
                        profileOB.ImageName = p_Var.Filename;
                    }

                }

                else
                {
                    if (Convert.ToBoolean(ViewState["visible"]) == true)
                    {
                        profileOB.ImageName = LblOldImage.Text;
                    }
                    else
                    {
                        profileOB.ImageName = null;
                    }
                }
                profileOB.DepttId = Convert.ToInt32(ViewState["Deptt_Id"]);
                p_Var.Result = profileBL.insert_New_Profile(profileOB);

                if (p_Var.Result > 0)
                {

                    obj_audit.ActionType = "U";
                    obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                    obj_audit.UserName = Session["UserName"].ToString();
                    obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                    obj_audit.IpAddress = obj_miscelBL.IpAddress();
                    obj_audit.Title = txtname.Text;
                    obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);
                   
                    Session["msg"] = "Profile has been updated successfully.";

                    Session["Redirect"] = "~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=28";

                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                }
            }
            catch
            {
                throw;
            }
        }
    }


    #endregion

    #region Function to convert image in thumbnail

    private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
    {

        using (System.Drawing.Image image = System.Drawing.Image.FromStream(sourcePath))
        {
            // can given width of image as we want
            //int newWidth = (int)(image.Width / scaleFactor);
            int newWidth = 155;
            // can given height of image as we want
            //int newHeight = (int)(image.Height / scaleFactor);
            int newHeight = 155;
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(targetPath, image.RawFormat);
        }


    }

    #endregion

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            PDepartment.Visible = true;
            p_Var.dSet = profileBL.ASP_Get_Deptt_Name();
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                if (Session["Dept_ID"].ToString() == "1")
                {
                    p_Var.dSet.Tables[0].Rows.RemoveAt(1);
                    ddlDepartment.DataSource = p_Var.dSet;
                }
                else if (Session["Dept_ID"].ToString() == "2")
                {
                    p_Var.dSet.Tables[0].Rows.RemoveAt(0);
                    ddlDepartment.DataSource = p_Var.dSet;
                }
                else
                {
                    ddlDepartment.DataSource = p_Var.dSet;
                }
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

    #region Custom validator to validate extension of upload Images

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Get file name
        p_Var.ext = System.IO.Path.GetExtension(FileUploadImage.PostedFile.FileName);
        p_Var.ext = p_Var.ext.ToLower();

        if (p_Var.ext == ".jpg" || p_Var.ext == ".png" || p_Var.ext == ".jpeg" || p_Var.ext == ".gif")
        {
            p_Var.flag = obj_miscelBL.GetActualFileType(FileUploadImage.PostedFile.InputStream);

        }
        if (p_Var.flag == true)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }

    #endregion
}
