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

public partial class Auth_AdminPanel_Appeal_AddEditAppealAward : CrsfBase //System.Web.UI.Page
{
    #region variable declaretion

    PetitionOB appealObject = new PetitionOB();
    AppealBL appealBL = new AppealBL();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    UserOB obj_userOB = new UserOB();
    LinkBL obj_LinkBL = new LinkBL();
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
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
        p_Var.url = "~/" + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/";
        if (!IsPostBack)
        {
            displayMetaLang();
            BtnUpdate.Visible = false;
            if (Request.QueryString["TID"] != null && Request.QueryString["TID"] != "")
            {
                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Edit Appeal against Award";
                this.Page.Title = " Edit Appeal against Award: HERC";

                Display_Edit();
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = true;
            }
            else
            {
                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Add Appeal against Award";
                this.Page.Title = " Add Appeal against Award: HERC";

                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                DisplayAppealNumber();
            }
        }

    }
    #endregion

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
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": Appeal against Award Management";
                    appealObject.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                    if (txtJudgement_Link.Text != null && txtJudgement_Link.Text != "")
                    {
                        appealObject.JudgementLink = txtJudgement_Link.Text;
                    }
                    else
                    {
                        appealObject.JudgementLink = null;
                    }
                    appealObject.StatusId = 3;
                    appealObject.PAStatusId = Convert.ToInt16(ddlAppealStatus.SelectedValue);
                    appealObject.RefNo = txtRefNo.Text;
                    appealObject.AppealId = Convert.ToInt32(Request.QueryString["Appeal_Id"]);
                    appealObject.WhereAppealed = txtWhere_Appealed.Text;

                    appealObject.MetaDescription = txtMetaDescription.Text;
                    appealObject.MetaKeyWords = txtMetaKeyword.Text;
                    appealObject.MetaTitle = txtMetaTitle.Text;
                    appealObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;
                    appealObject.InsertedBy = Convert.ToInt16(Session["User_Id"]);
                    appealObject.IpAddress = Miscelleneous_DL.getclientIP();

                    if (txtOtherDescription.Text != null && txtOtherDescription.Text != "")
                    {
                        appealObject.Description = txtOtherDescription.Text;
                    }
                    else
                    {
                        appealObject.Description = null;
                    }
                    if (txtdate.Text != null && txtdate.Text != "")
                    {
                        appealObject.Date = Convert.ToDateTime(miscellBL.getDateFormat(txtdate.Text.ToString()));
                    }
                    else
                    {
                        appealObject.Date = null;
                    }
                    if (txtAppealremarks.Text != null && txtAppealremarks.Text != "")
                    {
                        appealObject.Remarks = HttpUtility.HtmlEncode(txtAppealremarks.Text.Replace(Environment.NewLine, "<br />"));

                    }
                    else
                    {
                        appealObject.Remarks = null;
                    }
                    if (txtAppealSubject.Text != null && txtAppealSubject.Text != "")
                    {
                        appealObject.subject = txtAppealSubject.Text;
                    }
                    else
                    {
                        appealObject.subject = null;
                    }
                    p_Var.Result = appealBL.InsertAppealAwardTmp(appealObject);
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
                                newObj.AppealId = Convert.ToInt32(Request.QueryString["Appeal_Id"]);
                                newObj.ConnectionID = p_Var.Result;
                                //newObj.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);
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
                                if (p_Var.ext.Equals(".PDF"))
                                {
                                    p_Var.Filename = hpf.FileName;
                                    p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(hpf.FileName);
                                    p_Var.ext = Path.GetExtension(hpf.FileName);
                                    p_Var.Filename = miscellBL.getUniqueFileName(fileMultiple, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);
                                    hpf.SaveAs(Server.MapPath(p_Var.url + p_Var.Filename));
                                    newObj.FileName = p_Var.Filename;
                                }

                                if (txtdate.Text != null && txtdate.Text != "")
                                {
                                    newObj.StartDate = Convert.ToDateTime(miscellBL.getDateFormat(txtdate.Text.ToString()));
                                }
                                else
                                {
                                    newObj.StartDate = null;
                                }
                                //newObj.StartDate = Convert.ToDateTime(miscellBL.getDateFormat(txtdate.Text.ToString()));

                                int Result2 = appealBL.insertAppealAwardPronouncedFiles(newObj);
                            }
                        }
                        obj_audit.ActionType = "I";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = "New Record";
                        obj_audit.Title = lblAppealNo.Text + " (Appeal Against Award)";
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);
                        Session["msg"] = "Appeal against Award has been submitted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                    else
                    {
                        Session["msg"] = "Appeal against Award has not been submitted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
            }
        }
		
			else{
			Session.Abandon();
		    Response.Redirect("~/Auth/AdminPanel/Login.aspx");
			}
    }


    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            appealObject.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.update);
            if (Convert.ToInt16(ddlAppealStatus_Edit.SelectedValue) == 14)
            {
                if (txtJudgement_Link.Text != null && txtJudgement_Link.Text != "")
                {
                    appealObject.JudgementLink = txtJudgement_Link.Text;
                }
                else
                {
                    appealObject.JudgementLink = null;
                }
                
                if (txtAppealSubject.Text != null && txtAppealSubject.Text != "")
                {
                    appealObject.subject = txtAppealSubject.Text;
                }
                else
                {
                    appealObject.subject = null;
                }
            }
            else
            {
                appealObject.JudgementLink = null;
                appealObject.subject = null;
            }
            if (Convert.ToInt16(ddlAppealStatus_Edit.SelectedValue) == 25)
            {
                if (txtOtherDescription.Text != null && txtOtherDescription.Text != "")
                {
                    appealObject.Description = txtOtherDescription.Text;
                }
                else
                {
                    appealObject.Description = null;
                }
            }
            else
            {
                appealObject.Description = null;
            }
            appealObject.StatusId = Convert.ToInt16(Session["StatusId"]);
            appealObject.AppealId = Convert.ToInt32(Request.QueryString["Appeal_Id"]);
            
            appealObject.PAStatusId = Convert.ToInt16(ddlAppealStatus_Edit.SelectedValue);
            appealObject.RefNo = txtRefNo.Text;
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "")
            {
                appealObject.OldAppealId = Convert.ToInt32(Request.QueryString["ID"]);
            }
            else
            {
                appealObject.OldAppealId = null;
            }

            appealObject.MetaDescription = txtMetaDescription.Text;
            appealObject.MetaKeyWords = txtMetaKeyword.Text;
            appealObject.MetaTitle = txtMetaTitle.Text;
            appealObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;
            appealObject.LastUpdatedBy = Convert.ToInt16(Session["User_Id"]);
            appealObject.IpAddress = Miscelleneous_DL.getclientIP();
            appealObject.WhereAppealed = txtWhere_Appealed.Text;
            if (txtdate.Text != null && txtdate.Text != "")
            {
                appealObject.Date = Convert.ToDateTime(miscellBL.getDateFormat(txtdate.Text.ToString()));
            }
            else
            {
                appealObject.Date = null;
            }
            appealObject.TempAppealId = Convert.ToInt32(Request.QueryString["TID"]);
            if (txtAppealremarks.Text != null && txtAppealremarks.Text != "")
            {

                appealObject.Remarks = HttpUtility.HtmlEncode(txtAppealremarks.Text.Replace(Environment.NewLine, "<br />"));
            }
            else
            {
                appealObject.Remarks = null;
            }
           
            p_Var.Result = appealBL.InsertAppealAwardTmp(appealObject);
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
                        newObj.AppealId = Convert.ToInt32(Request.QueryString["appeal_Id"]);
                        newObj.ConnectionID = p_Var.Result;
                        //newObj.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);
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
                        if (p_Var.ext.Equals(".PDF"))
                        {

                            p_Var.Filename = hpf.FileName;
                            p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(hpf.FileName);
                            p_Var.ext = Path.GetExtension(hpf.FileName);
                            p_Var.Filename = miscellBL.getUniqueFileName(fileMultiple, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);
                            hpf.SaveAs(Server.MapPath(p_Var.url + p_Var.Filename));

                            newObj.FileName = p_Var.Filename;
                        }

                        if (txtdate.Text != null && txtdate.Text != "")
                        {
                            newObj.StartDate = Convert.ToDateTime(miscellBL.getDateFormat(txtdate.Text.ToString()));
                        }
                        else
                        {
                            newObj.StartDate = null;
                        }
                       

                        int Result2 = appealBL.insertAppealAwardPronouncedFiles(newObj);
                    }
                }
                obj_audit.ActionType = "U";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                obj_audit.IpAddress = miscellBL.IpAddress();
                string st = Request.QueryString["Status"].Trim();
                if (st == "3") { obj_audit.status = "Draft"; } else if (st == "4") { obj_audit.status = "For Publish"; } else if (st == "6") { obj_audit.status = "Published"; } else if (st == "8") { obj_audit.status = "Deleted"; } else if (st == "21") { obj_audit.status = "For Review"; }
                obj_audit.Title = lblAppealNo.Text+" (Appeal Against Award)";
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);
                Session["msg"] = "Appeal against Award has been updated successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
            else
            {
                Session["msg"] = "Appeal against Award has not been updated successfully";
                Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Display_Edit();
    }

    #region Function to bind Petition status in dropDownlist

    public void bindDdlPetition_review_Status_Edit()
    {
        //Miscelleneous_BL miscellBL=new Miscelleneous_BL();
        try
        {
            PSTatusEdit.Visible = true;
            appealObject.PetitionStatusId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
            p_Var.dSet = miscellBL.getStatusForPetitionAppealAward(appealObject);
            ddlAppealStatus_Edit.DataSource = p_Var.dSet;
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


    #region ddlAppealStatus_Edit selectedIndexChanged event zone

    protected void ddlAppealStatus_Edit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ddlAppealStatus_Edit.SelectedValue) != 0)
        {
            if (Convert.ToInt16(ddlAppealStatus_Edit.SelectedValue) == 14)
            {
              
                pJudgement_Edit.Visible = true;
               
                PJudgementDescription.Visible = true;
            }
            else
            {
               
                pJudgement_Edit.Visible = false;
               
                PJudgementDescription.Visible = false;
            }
            if (Convert.ToInt16(ddlAppealStatus_Edit.SelectedValue) == 25)
            {
                pOtherDesc.Visible = true;
            }
            else
            {
                pOtherDesc.Visible = false;
            }
            
        }
        else
        {
            pOtherDesc.Visible = false;
            pJudgement_Edit.Visible = false;
        }
    }

    #endregion
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal));
        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4"));
    }


    public void Display_Edit()
    {
        try
        {
            

            BtnSubmit.Visible = false;
            BtnUpdate.Visible = true;
            
            PSTatus.Visible = false;
            PSTatusEdit.Visible = true;
            bindDdlPetition_review_Status_Edit();
            appealObject.AppealId = Convert.ToInt32(Request.QueryString["appeal_Id"]);
            appealObject.StatusId = Convert.ToInt16(Session["StatusId"]);
            p_Var.dSetCompare = appealBL.GetAppealAwardPronouncedTmp(appealObject);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {
                txtdate.Text = p_Var.dSetCompare.Tables[0].Rows[0]["Appeal_Date"].ToString();
                lblAppealNo.Text += " EO/Appeal-" + p_Var.dSetCompare.Tables[0].Rows[0]["Appeal_Number"].ToString() + " Of " + p_Var.dSetCompare.Tables[0].Rows[0]["year"].ToString();
                if (p_Var.dSetCompare.Tables[0].Rows[0]["Judgement_Link"].ToString() != null && p_Var.dSetCompare.Tables[0].Rows[0]["Judgement_Link"].ToString() != "")
                {
                    txtJudgement_Link.Text = p_Var.dSetCompare.Tables[0].Rows[0]["Judgement_Link"].ToString();
                }


                txtMetaDescription.Text = p_Var.dSetCompare.Tables[0].Rows[0]["MetaDescriptions"].ToString();

                txtMetaTitle.Text = p_Var.dSetCompare.Tables[0].Rows[0]["MetaTitle"].ToString();

                txtMetaKeyword.Text = p_Var.dSetCompare.Tables[0].Rows[0]["MetaKeywords"].ToString();

                ddlMetaLang.SelectedValue = p_Var.dSetCompare.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();
               
                txtWhere_Appealed.Text = p_Var.dSetCompare.Tables[0].Rows[0]["Where_Appealed"].ToString();
                if (p_Var.dSetCompare.Tables[0].Rows[0]["Description"].ToString() != null && p_Var.dSetCompare.Tables[0].Rows[0]["Description"].ToString() != "")
                {
                    txtAppealSubject.Text = p_Var.dSetCompare.Tables[0].Rows[0]["Description"].ToString();
                }
                if (p_Var.dSetCompare.Tables[0].Rows[0]["Remarks"].ToString() != null && p_Var.dSetCompare.Tables[0].Rows[0]["Remarks"].ToString() != "")
                {
                    txtAppealremarks.Text = p_Var.dSetCompare.Tables[0].Rows[0]["Remarks"].ToString().Replace("&lt;br /&gt;", Environment.NewLine);
                }
                if (p_Var.dSetCompare.Tables[0].Rows[0]["OtherDescription"].ToString() != null && p_Var.dSetCompare.Tables[0].Rows[0]["OtherDescription"].ToString() != "")
                {
                    txtOtherDescription.Text = p_Var.dSetCompare.Tables[0].Rows[0]["OtherDescription"].ToString();
                }
                ddlAppealStatus_Edit.SelectedValue = p_Var.dSetCompare.Tables[0].Rows[0]["Award_StatusID"].ToString();
                if (p_Var.dSetCompare.Tables[0].Rows[0]["Award_StatusID"].ToString() == "14")
                {
                    pJudgement_Edit.Visible = true;
                   
                    PJudgementDescription.Visible = true;

                }
                else
                {
                    pJudgement_Edit.Visible = false;
                    
                    PJudgementDescription.Visible = false;
                }
                if (p_Var.dSetCompare.Tables[0].Rows[0]["Award_StatusID"].ToString() == "25")
                {
                    pOtherDesc.Visible = true;
                }
                else
                {
                    pOtherDesc.Visible = false;
                }
                txtRefNo.Text = p_Var.dSetCompare.Tables[0].Rows[0]["ReferenceNumber"].ToString();
                BindData(Convert.ToInt32(Request.QueryString["appeal_Id"]));
            }
        }
        catch
        {
            throw;
        }
    }

    public void DisplayAppealNumber()
    {
        appealObject.AppealId = Convert.ToInt32(Request.QueryString["appeal_Id"]);
        appealObject.StatusId = Convert.ToInt16(6);
        p_Var.dSetCompare = appealBL.GetAppealNumberDuringAward(appealObject);
        {
            
            lblAppealNo.Text += " EO/Appeal-" + p_Var.dSetCompare.Tables[0].Rows[0]["Appeal_Number"].ToString() + " Of " + p_Var.dSetCompare.Tables[0].Rows[0]["year"].ToString();
        }

    }


    #region function  bind with Repeator

    public void BindData(int AppealId)
    {
        appealObject.AppealId = AppealId;

        p_Var.dsFileName = appealBL.getFileNameForAppealAwardProunced(appealObject);
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
            p_Var.Result1 = appealBL.DeleteAppealAwardPronouncedFiles(appealObject);
            if (p_Var.Result1 > 0)
            {
                Label filename = (Label)e.Item.FindControl("lblFile");
                Label date = (Label)e.Item.FindControl("lblDate");
                Label lblComments = (Label)e.Item.FindControl("lblComments");
                Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");
                LinkButton RemoveFileLink = (LinkButton)e.Item.FindControl("lnkFileConnectedRemove");
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
            var link = e.Item.FindControl("lnkFileConnectedRemove") as LinkButton;
            var hiddenField = e.Item.FindControl("hiddenFieldAppealId") as HiddenField;
            var hiddenFieldStatusId = e.Item.FindControl("hiddenFieldStatusId") as HiddenField;
            Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");
            Label filename = (Label)e.Item.FindControl("lblFile");

            if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
            {
                if (link != null)
                {
                    if (Convert.ToInt16(Session["StatusId"]) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        link.Visible = true;//if user has right then enabled else disabled
                    }
                    else
                    {
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["appeal_Id"]) && (Convert.ToInt16(hiddenFieldStatusId.Value) != 6))
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

                    if (Convert.ToInt32(Session["StatusId"]) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        link.Visible = false;//if user has right then enabled else disabled
                    }
                    else
                    {
                        // link.Visible = true;//if user has right then enabled else disabled
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["appeal_Id"]) && (Convert.ToInt16(hiddenFieldStatusId.Value) != 6))
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
