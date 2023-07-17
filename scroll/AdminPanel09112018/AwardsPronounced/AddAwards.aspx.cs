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

public partial class Auth_AdminPanel_AwardsPronounced_AddAwards : CrsfBase //System.Web.UI.Page
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

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.url = "~/" + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/";

        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": Add Award";
        this.Page.Title = " Add Award: HERC";

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

            bindOrdersYearinDdl();
            bindropDownlistLang();

            bindApprovedPetition(ddlYear.SelectedValue.ToString());
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
    }

    #endregion

    //Area for all buttons, linkButtons, imageButtons click events

    #region button btnAddAward click event to add new award pronounced

    protected void btnAddOrder_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (Page.IsValid)
                {
                    appealObject.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                    appealObject.OrderTitle = HttpUtility.HtmlEncode(txtOrderTitle.Text.Replace(Environment.NewLine, "<br />"));
                    appealObject.OrderDescription = HttpUtility.HtmlEncode(txtOrderDescription.Text.Replace(Environment.NewLine, "<br />"));
                    appealObject.year = ddlYear.SelectedValue;
                    appealObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.AwardPronounced);
                    appealObject.StatusId = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft);
                    appealObject.LangId = Convert.ToInt16(ddlLanguage.SelectedValue);
                    appealObject.AppealId = Convert.ToInt32(ddlPetitionNo.SelectedValue);
                    appealObject.AppealNo = ddlPetitionNo.SelectedItem.ToString();
                    appealObject.recordInsertedBy = Convert.ToInt16(Session["User_Id"]);
                    appealObject.MetaDescription = txtMetaDescription.Text;
                    appealObject.MetaKeyWords = txtMetaKeyword.Text;
                    appealObject.MetaTitle = txtMetaTitle.Text;
                    appealObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;


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
                    if (Upload_File(ref p_Var.Filename))
                    {
                        if (p_Var.Filename != null)
                        {
                            appealObject.FileName = fileUploadPdf.FileName.ToString();
                        }
                        else
                        {
                            appealObject.FileName = null;
                        }

                    }
                    else
                    {
                        appealObject.FileName = null;
                    }

                    appealObject.PlaceHolderFour = HttpUtility.HtmlEncode(txtremarks.Text.Replace(Environment.NewLine, "<br />"));
                    appealObject.userID = Convert.ToInt32(Session["User_Id"]);
                    appealObject.IpAddress = Miscelleneous_DL.getclientIP();
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
                                if (p_Var.ext.Equals(".PDF"))
                                {
                                    p_Var.Filename = hpf.FileName;
                                    p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(hpf.FileName);
                                    p_Var.ext = Path.GetExtension(hpf.FileName);
                                    p_Var.Filename = miscellBL.getUniqueFileName(fileMultiple, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);
                                    hpf.SaveAs(Server.MapPath(p_Var.url + p_Var.Filename));
                                    newObj.FileName = p_Var.Filename;
                                }
                                if (txtOrderDate.Text != null && txtOrderDate.Text != "")
                                {
                                    newObj.StartDate = miscellBL.getDateFormat(txtOrderDate.Text);

                                }
                                else
                                {
                                    newObj.StartDate = System.DateTime.Now;

                                }


                                int Result2 = appealBL.insertAppealAwardFiles(newObj);
                            }
                        }


                        obj_audit.ActionType = "I";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.Title = txtOrderDescription.Text;
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "Award pronounced has been added successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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

    #endregion

    #region button btnReset click event to reset contents

    protected void btnReset_Click(object sender, EventArgs e)
    {

        txtOrderDate.Text = "";
        txtOrderDescription.Text = "";
        txtOrderTitle.Text = "";
        txtremarks.Text = "";

        ddlPetitionNo.SelectedIndex = 0;

    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/AwardsPronounced/") + "DisplayAwards.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));
    }

    #endregion

    //End

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
                    usrObject.english = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.English);
                    usrObject.hindi = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(usrObject.english).Append(",");
                    p_Var.sbuilder.Append(usrObject.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true)
                {
                    usrObject.hindi = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(usrObject.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
                {
                    usrObject.english = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.English);

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

    #region Function to bind order type in dropDownlist

    public void displayOrderType()
    {
        try
        {
            p_Var.dSet = orderBL.getOrderType();
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                //ddlOrderType.DataSource = p_Var.dSet;
                //ddlOrderType.DataTextField = "Order_Type";
                //ddlOrderType.DataValueField = "Order_Type_ID";
                //ddlOrderType.DataBind();
                //ddlOrderType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind order category based on order type in dropDownlist

    public void displayOrderType(int ordertype)
    {
        try
        {
            //orderObject.OrderTypeID = ordertype;
            //p_Var.dSet = orderBL.getOrderCategories(orderObject);
            //if (p_Var.dSet.Tables[0].Rows.Count > 0)
            //{
            //    ddlOrderCategory.DataSource = p_Var.dSet;
            //    ddlOrderCategory.DataTextField = "OrderCatName";
            //    ddlOrderCategory.DataValueField = "OrderCatId";
            //    ddlOrderCategory.DataBind();
            //    ddlOrderCategory.Items.Insert(0, new ListItem("Select", "0"));
            //}
            //else
            //{
            //    ddlOrderCategory.DataSource = p_Var.dSet;
            //    ddlOrderCategory.DataBind();
            //    ddlOrderCategory.Items.Insert(0, new ListItem("Select", "0"));
            //}
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
            filename = miscellBL.getUniqueFileName(p_Var.Filename, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);

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

    //Area for all the customValidators serverValidate events

    #region customValidator CustvalidFileUplaod to validate pdf file only

    protected void CustvalidFileUplaod_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Get file name
        string UploadFileName = fileUploadPdf.PostedFile.FileName;

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

    #region Function to bind approved petition number in dropDownlist

    public void bindApprovedPetition(string year)
    {
        try
        {
            appealObject.year = year.Trim();
            p_Var.dSet = appealBL.getAppealNumberforAwardPronounced(appealObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlPetitionNo.DataSource = p_Var.dSet;
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

    #region Function to bind approved petition review number in dropDownlist

    public void bindApprovedPetitionReview()
    {
        try
        {
            p_Var.dSet = orderBL.getPetitionReviewNumber();
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlPetitionNo.DataSource = p_Var.dSet;
                ddlPetitionNo.DataTextField = "RP_No";
                ddlPetitionNo.DataValueField = "RP_Id";
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

    #region Function to bind approved petition appeal number in dropDownlist

    public void bindApprovedPetitionAppeal()
    {
        try
        {
            p_Var.dSet = orderBL.getPetitionAppealNumber();
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlPetitionNo.DataSource = p_Var.dSet;
                ddlPetitionNo.DataTextField = "Appeal_No";
                ddlPetitionNo.DataValueField = "PA_Id";
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

    #region Function to bind Orders Year

    public void bindOrdersYearinDdl()
    {
        appealObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal);
        p_Var.dSet = appealBL.GetYear(appealObject);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = p_Var.dSet;
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
