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

public partial class Auth_AdminPanel_AwardsPronounced_EditAwards : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration zone

    PetitionOB orderObject = new PetitionOB();
    PetitionOB appealObject = new PetitionOB();
    OrderBL orderBL = new OrderBL();
    AppealBL appealBL = new AppealBL();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    UserOB obj_userOB = new UserOB();
    LinkBL obj_LinkBL = new LinkBL();
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    //End

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.url = "~/" + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/";

        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": Edit Award";
        this.Page.Title = " Edit Award: HERC";

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

            if (Request.QueryString["AwardID"] != null)    // for Edit
            {
                bindOrdersYearinDdl();
                // bindApprovedPetition(ddlYear.SelectedValue.ToString());
                bindAward_In_Edit_Mode(Convert.ToInt32(Request.QueryString["AwardID"]));
                //Session["AwardID"] = Request.QueryString["AwardID"].ToString();

            }

            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
    }

    //Area for all the buttons, linkButtons click events
    protected void CustvalidFileUplaod_ServerValidate(object source, ServerValidateEventArgs args)
    {
        p_Var.ext = System.IO.Path.GetExtension(fileUploadPdf.PostedFile.FileName);
        p_Var.ext = p_Var.ext.ToLower();
        int count = fileUploadPdf.PostedFile.FileName.Split('.').Length - 1;
        if (p_Var.ext == ".pdf" && count == 1)
        {
            p_Var.flag = miscellBL.GetActualFileType_pdf(fileUploadPdf.PostedFile.InputStream);
        }
        else
        {
            args.IsValid = false;
            return;

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



    #region button btnUpdateReviewAward click event to update award pronounced

    protected void btnUpdateReviewAward_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (Page.IsValid)
                {
                    appealObject.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.update);
                    appealObject.OrderTitle = HttpUtility.HtmlEncode(txtOrderTitle.Text.Replace(Environment.NewLine, "<br />"));
                    appealObject.OrderDescription = HttpUtility.HtmlEncode(txtOrderDescription.Text.Replace(Environment.NewLine, "<br />"));
                    appealObject.year = ddlYear.SelectedValue;
                    appealObject.TempRAId = Convert.ToInt32(Request.QueryString["AwardID"]);
                    appealObject.oldRaID = Convert.ToInt32(Request.QueryString["AwardID"]);
                    appealObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.AwardPronounced);
                    appealObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
                    appealObject.LangId = Convert.ToInt16(Request.QueryString["LangID"]);
                    appealObject.AppealId = Convert.ToInt32(ddlPetitionNo.SelectedValue);
                    appealObject.AppealNo = ddlPetitionNo.SelectedItem.ToString();
                    appealObject.MetaDescription = txtMetaDescription.Text;
                    appealObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;
                    appealObject.MetaKeyWords = txtMetaKeyword.Text;
                    appealObject.MetaTitle = txtMetaTitle.Text;
                    appealObject.IpAddress = Miscelleneous_DL.getclientIP();
                    appealObject.recordUpdatedBy = Convert.ToInt16(Session["User_Id"]);
                    if (txtOrderDate.Text != null && txtOrderDate.Text != "")
                    {
                        appealObject.AwardDate = miscellBL.getDateFormat(txtOrderDate.Text);
                        appealObject.PlaceHolderFive = miscellBL.getDateFormat(txtOrderDate.Text).Year.ToString();
                    }
                    else
                    {
                        appealObject.AwardDate = System.DateTime.Now;
                        appealObject.PlaceHolderFive = System.DateTime.Now.Year.ToString();
                    }

                    //p_Var.ext = System.IO.Path.GetExtension(fileUploadPdf.FileName);
                    //p_Var.ext = p_Var.ext.ToLower();
                    //int count = fileUploadPdf.FileName.Split('.').Length - 1;

                    //if (p_Var.ext == ".pdf" && count == 1)
                    //{
                    //    p_Var.flag = miscellBL.GetActualFileType_pdf(fileUploadPdf.PostedFile.InputStream);
                    //}
                    //if (p_Var.flag == true)
                    //{

                    if (Upload_File(ref p_Var.Filename))
                    {
                        if (p_Var.Filename != null)
                        {
                            appealObject.FileName = p_Var.Filename.ToString();
                        }
                        else
                        {
                            appealObject.FileName = null;
                        }
                    }
                    else
                    {
                        if (ViewState["filename"] != null)
                        {
                            appealObject.FileName = ViewState["filename"].ToString();
                        }
                        else
                        {
                            appealObject.FileName = null;
                        }
                    }
                    //}

                    appealObject.PlaceHolderFour = HttpUtility.HtmlEncode(txtremarks.Text.Replace(Environment.NewLine, "<br />"));

                    p_Var.Result = appealBL.insertUpdateReviewAppealTemp(appealObject);
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
                                newObj.AppealId = Convert.ToInt32(ddlPetitionNo.SelectedValue);
                                newObj.ConnectionID = p_Var.Result;
                                newObj.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft);

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
                                int count = fileMultiple.Split('.').Length - 1;
                                if (miscellBL.GetActualFileType(hpf.InputStream))
                                {
                                    if (p_Var.ext.Equals(".PDF") && count == 1)
                                    {
                                        p_Var.Filename = hpf.FileName;
                                        p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(hpf.FileName);
                                        p_Var.ext = Path.GetExtension(hpf.FileName);
                                        p_Var.Filename = miscellBL.getUniqueFileName(fileMultiple, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);
                                        hpf.SaveAs(Server.MapPath(p_Var.url + p_Var.Filename));
                                        newObj.FileName = p_Var.Filename;
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please choose valid file');", true);
                                        return;
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please choose valid file');", true);
                                    return;
                                }
                                if (txtOrderDate.Text != null && txtOrderDate.Text != "")
                                {
                                    newObj.StartDate = miscellBL.getDateFormat(txtOrderDate.Text);

                                }
                                else
                                {
                                    newObj.StartDate = System.DateTime.Now;

                                }
                                // newObj.StartDate = System.DateTime.Now;

                                int Result2 = appealBL.insertAppealAwardFiles(newObj);
                            }
                        }

                        obj_audit.ActionType = "U";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        string st = Request.QueryString["Status"].ToString();
                        if (st == "3") { obj_audit.status = "Draft"; } else if (st == "4") { obj_audit.status = "For Publish"; } else if (st == "6") { obj_audit.status = "Published"; } else if (st == "8") { obj_audit.status = "Deleted"; } else if (st == "21") { obj_audit.status = "For Review"; }
                        obj_audit.Title = ddlPetitionNo.SelectedItem.ToString() + " of " + ddlYear.SelectedItem.ToString();
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "Award pronounced has been updated successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
            }
        }

        else
        {
            //Session.Abandon();
            //Response.Redirect("~/Auth/AdminPanel/Login.aspx");
        }
    }
    #endregion

    #region button btnReset click event to reset previous values

    protected void btnReset_Click(object sender, EventArgs e)
    {
        bindAward_In_Edit_Mode(Convert.ToInt32(Request.QueryString["AwardID"]));
    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
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

    //Area for all the user defined functions

    #region Function to bind Order in edit mode

    public void bindAward_In_Edit_Mode(int ra_id)
    {
        try
        {

            appealObject.TempRAId = Convert.ToInt32(ra_id);
            appealObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = appealBL.getAppealAwardForEdit(appealObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                BindData(Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["Appeal_Id"]));
                ddlYear.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["AppealYear"].ToString();
                bindApprovedPetition(ddlYear.SelectedValue.ToString());
                ddlPetitionNo.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["Appeal_Id"].ToString();
                txtMetaDescription.Text = p_Var.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
                txtMetaTitle.Text = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
                txtMetaKeyword.Text = p_Var.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
                ddlMetaLang.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();
                txtOrderTitle.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceholderOne"].ToString().Replace("&lt;br /&gt;", Environment.NewLine);
                txtOrderDescription.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceholderTwo"].ToString().Replace("&lt;br /&gt;", Environment.NewLine);

                txtOrderDate.Text = miscellBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["AwardDate"].ToString());


                if (p_Var.dSet.Tables[0].Rows[0]["File_Name"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString() != "")
                {
                    LblOldFile.Visible = true;
                    lnkFileRemove.Visible = true;
                    lblFileName.Visible = true;
                    lblFileName.Text = p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString();
                    ViewState["filename"] = p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString();
                }
                else
                {
                    LblOldFile.Visible = false;
                    //lblPettionName.Visible = true;
                    //LblOldFile.Visible = false;
                    lnkFileRemove.Visible = false;
                    lblFileName.Visible = false;
                }

                if (p_Var.dSet.Tables[0].Rows[0]["PlaceholderFour"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["PlaceholderFour"].ToString() != "")
                {
                    txtremarks.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceholderFour"].ToString().Replace("&lt;br /&gt;", Environment.NewLine);
                }

            }

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
            appealObject.year = year.Trim();
            appealObject.AppealId = Convert.ToInt32(Request.QueryString["AppealID"]);
            p_Var.dSetCompare = appealBL.getAppealNumberforAwardPronouncedEdit(appealObject);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {
                ddlPetitionNo.DataSource = p_Var.dSetCompare;
                ddlPetitionNo.DataTextField = "Appeal_Number";
                ddlPetitionNo.DataValueField = "Appeal_Id";
                ddlPetitionNo.DataBind();
                ddlPetitionNo.Items.Insert(0, new ListItem("Select One", "0"));
            }
            else
            {
                ddlPetitionNo.DataSource = p_Var.dSet;
                ddlPetitionNo.DataBind();
                ddlPetitionNo.Items.Insert(0, new ListItem("Select One", "0"));
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
        try
        {
            p_Var.Filename = fileUploadPdf.FileName;
            p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(fileUploadPdf.FileName);
            p_Var.ext = Path.GetExtension(fileUploadPdf.FileName);
            //For Unique File Name

            int count = fileUploadPdf.FileName.Split('.').Length - 1;
            p_Var.ext = p_Var.ext.ToLower();
            if (p_Var.ext == ".pdf" && count == 1)
            {
                filename = miscellBL.getUniqueFileName(p_Var.Filename, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);
                fileUploadPdf.PostedFile.SaveAs(Server.MapPath(p_Var.url) + filename);
            }
        }
        catch
        {
            p_Var.uploadStatus = false;
        }
        return p_Var.uploadStatus;
    }

    #endregion

    //End

    #region linkButton lnkFileRemove click event to remove file

    protected void lnkFileRemove_Click(object sender, EventArgs e)
    {
        LblOldFile.Visible = false;
        lnkFileRemove.Visible = false;
        lblFileName.Visible = false;
    }

    #endregion

    #region Function to bind Orders Year

    public void bindOrdersYearinDdl()
    {
        appealObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal);
        p_Var.dsFileName = appealBL.GetYear(appealObject);
        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = p_Var.dsFileName;
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "year";
            ddlYear.DataBind();
        }
    }

    #endregion

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        bindApprovedPetition(ddlYear.SelectedValue.ToString());

    }



    #region function  bind with Repeator

    public void BindData(int AppealId)
    {
        appealObject.AppealId = AppealId;

        p_Var.dsFileName = appealBL.getFileNameForAppealAward(appealObject);
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
            appealObject.ConnectionID = id;
            p_Var.Result1 = appealBL.DeleteFileForAppealAward(appealObject);
            if (p_Var.Result1 > 0)
            {
                Label filename = (Label)e.Item.FindControl("lblFile");
                Label date = (Label)e.Item.FindControl("lblDate");
                Label lblComments = (Label)e.Item.FindControl("lblComments");
                LinkButton RemoveFileLink = (LinkButton)e.Item.FindControl("lnkFileConnectedRemove");
                Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");
                filename.Visible = false;
                date.Visible = false;
                lblComments.Visible = false;
                RemoveFileLink.Visible = false;
                ltrlDownload.Visible = false;
            }

            //Display(Request.QueryString["publicNoticeID"].ToString());

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
            var hiddenField = e.Item.FindControl("hiddenFieldAwardPronounced") as HiddenField;


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
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["AwardID"]))
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
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["AwardID"]))
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

            p_Var.sbuilder.Append("<a href='" + ResolveUrl(p_Var.url) + filename.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='Download File' width='15' alt='Download File' height=\"15\" /> " + "</a>");
            p_Var.sbuilder.Append("<hr />");
            ltrlDownload.Text = p_Var.sbuilder.ToString();
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
