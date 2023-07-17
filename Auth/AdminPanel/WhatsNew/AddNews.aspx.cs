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
using System.Globalization;
using System.IO;


public partial class Auth_AdminPanel_WhatsNew_AddNews : CrsfBase //System.Web.UI.Page
{
    Project_Variables p_Var = new Project_Variables();
    UserOB obj_userOB = new UserOB();
    UserBL obj_UserBL = new UserBL();
    WhatsNewBL obj_wn = new WhatsNewBL();
    Miscelleneous_BL obj_miscelBL = new Miscelleneous_BL();
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    WhatNewsOB objWhatNewsOB = new WhatNewsOB();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();
    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/";
        Page.Title = "HERC:Add What's New";

        if (!IsPostBack)
        {
            chk_privilages();


            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                lblHead.Text = "Edit What's New";
                Page.Title = "HERC:Edit What's New";
                getTempTenderByID();
                BtnSubmit.Text = "Update";

                Page.Title = "Edit What's New :HERC ADMIN";
            }
            else
            {
                lblHead.Text = "Add What's New";
            }


        }
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
                    InsertNews();
                }
            }
        }
        else
        {
            Session.Abandon();
            Response.Redirect("~/Auth/AdminPanel/Login.aspx");
        }
    }

    protected void lnkFileConnectedRemove_Click(object sender, EventArgs e)
    {
        lblFileName.Text = string.Empty;
        lnkFileConnectedRemove.Visible = false;
        ltrlDownload.Visible = false;
        objWhatNewsOB.TenderId = Convert.ToInt32(Request.QueryString["ID"]);

        objWhatNewsOB.Status = Convert.ToInt32(Request.QueryString["StatusId"]);
        // p_Var.Result = obj_wn.WhatsNewFileDelete(objWhatNewsOB);

    }

    protected void BtnReset_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            getTempTenderByID();
        }
        else
        {
            txtDescription.Text = "";
            txtTenderName.Text = "";
            TxtExpirydate.Text = "";
            Txtstartdate.Text = "";

        }
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/auth/adminpanel/Whatsnew/View_News.aspx?ModuleId=7");
    }
    protected void InsertNews()
    {

        Miscelleneous_DL MisDL = new Miscelleneous_DL();
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                HttpFileCollection uploads = Request.Files;

                objWhatNewsOB.ActionType = 2;
                objWhatNewsOB.TenderId = Convert.ToInt32(Request.QueryString["ID"]);


                objWhatNewsOB.NewsTitle = txtTenderName.Text;
                objWhatNewsOB.NewsDescription = txtDescription.Text;

                objWhatNewsOB.StartDate = DateTime.ParseExact(Txtstartdate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                objWhatNewsOB.Expirydate = DateTime.ParseExact(TxtExpirydate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                objWhatNewsOB.UserId = Convert.ToInt32(Session["User_Id"]);
                objWhatNewsOB.IpAddress = Miscelleneous_DL.getclientIP();
                objWhatNewsOB.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                objWhatNewsOB.InsertedBy = Convert.ToInt32(Session["User_Id"]);
                objWhatNewsOB.ApproveStatus = Convert.ToInt32(Request.QueryString["StatusId"]);
                objWhatNewsOB.ModuleID = (int)Module_ID_Enum.Project_Module_ID.Whats_New;
                if (fileUpload_Menu.HasFile)
                {
                    if (Upload_File(ref p_Var.Filename))
                    {
                        if (p_Var.Filename != null)
                        {
                            objWhatNewsOB.FileName = p_Var.Filename;
                        }


                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(lblFileName.Text))
                    {
                        objWhatNewsOB.FileName = null;
                    }
                    else
                    {
                        objWhatNewsOB.FileName = lblFileName.Text;
                    }
                }

                p_Var.Result = obj_wn.InsertNews(objWhatNewsOB);

                if (p_Var.Result > 0)
                {
                    obj_audit.ActionType = "U";
                    obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                    obj_audit.UserName = Session["UserName"].ToString();
                    obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                    obj_audit.IpAddress = obj_miscelBL.IpAddress();
                    string st = Request.QueryString["StatusId"].Trim();
                    if (st == "3") { obj_audit.status = "Draft"; } else if (st == "4") { obj_audit.status = "For Publish"; } else if (st == "6") { obj_audit.status = "Published"; } else if (st == "8") { obj_audit.status = "Deleted"; } else if (st == "21") { obj_audit.status = "For Review"; }
                    obj_audit.Title = Txtstartdate.Text + ", " + txtTenderName.Text;
                    obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                    Session["msg"] = "What's new record has been updated successfully";
                    Session["Redirect"] = "~/auth/adminpanel/WhatsNew/View_News.aspx?ModuleID=" + Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New);
                    Response.Redirect("~/auth/adminpanel/ConfirmationPage.aspx");
                }
                else
                {

                    Session["msg"] = "What's new record has not been updated successfully";
                    Session["Redirect"] = "~/auth/adminpanel/WhatsNew/View_News.aspx?ModuleID=" + Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New);
                    Response.Redirect("~/auth/adminpanel/ConfirmationPage.aspx");
                }


            }
            else
            {

                objWhatNewsOB.ActionType = 1;
                //objWhatNewsOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);

                objWhatNewsOB.NewsTitle = txtTenderName.Text;
                objWhatNewsOB.NewsDescription = txtDescription.Text;

                objWhatNewsOB.StartDate = DateTime.ParseExact(Txtstartdate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                objWhatNewsOB.Expirydate = DateTime.ParseExact(TxtExpirydate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                objWhatNewsOB.IpAddress = Miscelleneous_DL.getclientIP();
                objWhatNewsOB.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                objWhatNewsOB.InsertedBy = Convert.ToInt32(Session["User_Id"]);

                objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.draft;
                if (fileUpload_Menu.HasFile)
                {
                    if (Upload_File(ref p_Var.Filename))
                    {
                        if (p_Var.Filename != null)
                        {
                            objWhatNewsOB.FileName = p_Var.Filename;
                        }

                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(lblFileName.Text))
                    {
                        objWhatNewsOB.FileName = null;
                    }
                    else
                    {
                        objWhatNewsOB.FileName = lblFileName.Text;
                    }
                }
                objWhatNewsOB.TenderId = obj_wn.InsertNews(objWhatNewsOB);
                if (objWhatNewsOB.TenderId > 0)
                {
                    obj_audit.ActionType = "I";
                    obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                    obj_audit.UserName = Session["UserName"].ToString();
                    obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                    obj_audit.IpAddress = obj_miscelBL.IpAddress();
                    obj_audit.status = "New Record";
                    obj_audit.Title = Txtstartdate.Text + ", " + txtTenderName.Text;
                    obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                    Session["msg"] = "What's new record has been added sucessfully";
                    Session["Redirect"] = "~/auth/adminpanel/WhatsNew/View_News.aspx?ModuleID=" + Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New);
                    Response.Redirect("~/auth/adminpanel/ConfirmationPage.aspx");
                }
                else
                {

                    Session["msg"] = "What's new record has not been added sucessfully";
                    Session["Redirect"] = "~/auth/adminpanel/WhatsNew/View_News.aspx?ModuleID=" + Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New);
                    Response.Redirect("~/auth/adminpanel/ConfirmationPage.aspx");
                }

            }
        }
        catch
        {
            throw;
        }

    }
    protected void reset()
    {

        txtTenderName.Text = "";
        txtDescription.Text = "";

        Txtstartdate.Text = "";
        TxtExpirydate.Text = "";
    }



    protected void getTempTenderByID()
    {
        objWhatNewsOB.ActionType = 2;
        objWhatNewsOB.ApproveStatus = Convert.ToInt32(Request.QueryString["StatusId"]);
        // objWhatNewsOB.LangId = Convert.ToInt32(Request.QueryString["LangID"]);
        objWhatNewsOB.TenderId = Convert.ToInt32(Request.QueryString["ID"]);
        objWhatNewsOB.PageIndex = 0;
        objWhatNewsOB.PageSize = 0;

        DataSet ds = obj_wn.ASP_Get_News(objWhatNewsOB);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //ddlLanguage.SelectedValue = ds.Tables[0].Rows[0]["FK_LANG_ID"].ToString();

            txtDescription.Text = ds.Tables[0].Rows[0]["NewsDecription"].ToString();


            TxtExpirydate.Text = ds.Tables[0].Rows[0]["LAST_DT"].ToString();


            Txtstartdate.Text = ds.Tables[0].Rows[0]["OPENING_DT"].ToString();

            txtTenderName.Text = ds.Tables[0].Rows[0]["News_TITLE"].ToString();

            if (ds.Tables[0].Rows[0]["FileName"] != DBNull.Value && ds.Tables[0].Rows[0]["FileName"] != "")
            {
                lblFileName.Visible = true;
                lnkFileConnectedRemove.Visible = true;
                ltrlDownload.Visible = true;

                p_Var.sbuilder.Append("<a href='" + ResolveUrl(p_Var.url) + ds.Tables[0].Rows[0]["FileName"].ToString() + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='Download File' width='15' alt='Download File' height=\"15\" /> " + "</a>");

                ltrlDownload.Text = p_Var.sbuilder.ToString();

                lblFileName.Visible = true;
                lnkFileConnectedRemove.Visible = true;
                lblFileName.Text = ds.Tables[0].Rows[0]["FileName"].ToString();
            }
            else
            {
                lnkFileConnectedRemove.Visible = false;
            }


        }

    }

    public void chk_privilages()
    {
        //DataSet dsprv = new DataSet();

        string url1 = Request.Url.ToString();

        obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
        p_Var.dSet = obj_RoleBL.ASP_CheckPrivilagesALL_For_Master(obj_userOB);
        int id = (from DataRow dr in p_Var.dSet.Tables[0].Rows
                  where (int)dr["Module_Id"] == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New)
                  select (int)dr["Module_Id"]).FirstOrDefault();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {



            if (id != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New))
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }




        }
    }


    #region Function to upload files

    private bool Upload_File(ref string filename)
    {


        try
        {
            p_Var.Filename = fileUpload_Menu.FileName;
            p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(fileUpload_Menu.FileName);
            p_Var.ext = Path.GetExtension(fileUpload_Menu.FileName);
            //For Unique File Name
            filename = obj_miscelBL.getUniqueFileName(p_Var.Filename, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);

            fileUpload_Menu.PostedFile.SaveAs(Server.MapPath(p_Var.url) + filename);
        }
        catch
        {
            p_Var.uploadStatus = false;
        }
        return p_Var.uploadStatus;
    }

    #endregion

    protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
    {
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
}
