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


public partial class Auth_AdminPanel_OrderManagement_Order_Add : CrsfBase // System.Web.UI.Page
{
    //Area for all the variables declaration zone

    PetitionOB orderObject = new PetitionOB();
    PetitionOB orderobject1 = new PetitionOB();
    OrderBL orderBL = new OrderBL();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    UserOB obj_userOB = new UserOB();
    LinkBL obj_LinkBL = new LinkBL();
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    //End

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.url = "~/" + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Orders"].ToString() + "/";
        if (!IsPostBack)
        {
            displayMetaLang();
            DataSet dsprv = new DataSet();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_RoleBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/AdminPanel/Login.aspx");
            }

            bindropDownlistLang();
            if (ddlConnectionType.SelectedValue == "0")
            {
                displayOrderTypePetition();

            }
            else if (ddlConnectionType.SelectedValue == "1")
            {
                displayOrderTypePetition();

            }
            else
            {
                displayOrderType();// this is for review petition
            }
            displayOrderType(0);

            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": Add  Order";
        this.Page.Title = " Add Order: HERC";

    }

    #endregion

    //Area for all the dropDownlist events

    #region dropDownlist ddlOrderType selectedIndexChanged event

    protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayOrderType(Convert.ToInt16(ddlOrderType.SelectedValue));
        if (ddlOrderType.SelectedValue == "0")
        {
            TextBox1.Enabled = true;
        }
        else
        {
            TextBox1.Enabled = false;
        }

    }

    #endregion

    //End

    //Area for all buttons, linkButtons, imageButtons click events

    #region button btnAddOrder click event to add new order

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
                    orderObject.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                    orderObject.OrderTitle = HttpUtility.HtmlEncode(txtOrderTitle.Text.Replace(Environment.NewLine, "<br />"));
                    orderObject.OrderDescription = HttpUtility.HtmlEncode(txtOrderDescription.Text.Replace(Environment.NewLine, "<br />"));
                    orderObject.OrderCatID = Convert.ToInt16(ChkCategory.SelectedValue);
                    orderObject.OrderTypeID = Convert.ToInt16(ddlOrderType.SelectedValue);

                    orderObject.MetaDescription = txtMetaDescription.Text;
                    orderObject.MetaKeyWords = txtMetaKeyword.Text;
                    orderObject.MetaTitle = txtMetaTitle.Text;
                    orderObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;

                    //Petitioner & Respondent Details

                    orderObject.PetitionerName = HttpUtility.HtmlEncode(txtPetitionerName.Text.Replace(Environment.NewLine, "<br />"));
                    orderObject.PetitionerMobileNo = HttpUtility.HtmlEncode(txtPetitionerMobileNo.Text);
                    orderObject.PetitionerPhoneNo = HttpUtility.HtmlEncode(txtPetitionerPhoneNo.Text);
                    orderObject.PetitionerFaxNo = HttpUtility.HtmlEncode(txtPetitionerFaxNo.Text);
                    orderObject.PetitionerAddress = HttpUtility.HtmlEncode(txtPetitionerAdd.Text.Replace(Environment.NewLine, "<br />"));
                    orderObject.PetitionerEmail = HttpUtility.HtmlEncode(txtPetitionerEmailID.Text);
                    orderObject.RespondentName = HttpUtility.HtmlEncode(txtRespondentName.Text.Replace(Environment.NewLine, "<br />"));
                    orderObject.RespondentPhone_No = HttpUtility.HtmlEncode(txtRespondentPhoneNo.Text);
                    orderObject.RespondentMobileNo = HttpUtility.HtmlEncode(txtRespondentMobileNo.Text);
                    orderObject.RespondentFaxNo = HttpUtility.HtmlEncode(txtRespondentFaxNo.Text);
                    orderObject.RespondentAddress = HttpUtility.HtmlEncode(txtRespondentAdd.Text.Replace(Environment.NewLine, "<br />"));
                    orderObject.Respondentmail = HttpUtility.HtmlEncode(txtRespondentEmailID.Text);
                    orderObject.PlaceHolderFive = HttpUtility.HtmlEncode(txtRemarks.Text.Replace(Environment.NewLine, "<br />"));
                    //End

                    if (Convert.ToInt16(Request.QueryString["ModuleID"]) == Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Orders))
                    {
                        orderObject.ConnectionType = Convert.ToInt16(ddlConnectionType.SelectedValue);
                        if (pnlPetitionSection.Visible == true)
                        {


                            orderObject.petitionYear = ddlyear.SelectedValue;
                        }
                        else
                        {
                            orderObject.petitionYear = null;
                        }
                        if (ddlConnectionType.SelectedValue == "1")
                        {
                            //orderObject.PetitionId = Convert.ToInt16(ddlPetitionNo.SelectedValue);
                        }
                        else if (ddlConnectionType.SelectedValue == "2")
                        {
                            //orderObject.RPId = Convert.ToInt16(ddlPetitionNo.SelectedValue);
                            if (ddlOrderType.SelectedValue == "13")
                            {
                                orderObject.OrderTypeID = 9;
                            }
                            else if (ddlOrderType.SelectedValue == "12")
                            {
                                orderObject.OrderTypeID = 8;
                            }
                        }
                        else if (ddlConnectionType.SelectedValue == "3")
                        {
                            //orderObject.AppealId = Convert.ToInt16(ddlPetitionNo.SelectedValue);
                        }

                    }
                    else
                    {
                        orderObject.PetitionId = null;
                        orderObject.RPId = null;
                        orderObject.AppealId = null;
                    }

                    //End
                    orderObject.StartDate = null;

                    if (txtendate.Text != null && txtendate.Text != "")
                    {
                        orderObject.EndDate = null;
                    }
                    else
                    {
                        orderObject.EndDate = null;

                    }
                    orderObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Orders);
                    orderObject.StatusId = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft);
                    orderObject.LangId = Convert.ToInt16(ddlLanguage.SelectedValue);
                    if (txtOrderDate.Text != null && txtOrderDate.Text != "")
                    {
                        orderObject.OrderDate = miscellBL.getDateFormat(txtOrderDate.Text);
                    }
                    else
                    {
                        orderObject.OrderDate = System.DateTime.Now;
                    }


                    DateTime date = miscellBL.getDateFormat(txtOrderDate.Text);
                    orderObject.year = date.Year.ToString();
                    orderObject.userID = Convert.ToInt32(Session["User_Id"]);
                    orderObject.InsertedBy = Convert.ToInt32(Session["User_Id"]);
                    orderObject.IpAddress = Miscelleneous_DL.getclientIP();
                    p_Var.Result = orderBL.insertUpdateOrderTemp(orderObject);
                    ViewState["orderId"] = p_Var.Result;
                    if (p_Var.Result > 0)
                    {


                        foreach (ListItem liYear in chklstPetition.Items)
                        {
                            if (liYear.Selected == true)
                            {
                                int cnt = liYear.Text.LastIndexOf(' ');
                                orderObject.year = liYear.Text.Substring(cnt).Trim();
                            }
                        }

                        //This is for Ordercategory
                        foreach (ListItem li in ChkCategory.Items)
                        {
                            if (li.Selected == true)
                            {
                                orderObject.OrderID = p_Var.Result;

                                orderObject.StatusId = 6;
                                orderObject.OrderTypeID = Convert.ToInt16(ddlOrderType.SelectedValue);
                                orderObject.OrderCatID = Convert.ToInt16(li.Value.ToString());
                                int Result = orderBL.insertConnectedOrderCategory(orderObject);
                            }
                        }
                        //This is for petition/Reviewpetition
                        foreach (ListItem li in chklstPetition.Items)
                        {
                            if (li.Selected == true)
                            {
                                orderObject.OrderID = p_Var.Result;
                                //orderObject.year = ddlyear.SelectedValue.ToString();
                                int cnt = li.Text.LastIndexOf(' ');
                                orderObject.year = li.Text.Substring(cnt).Trim();
                                orderObject.StatusId = 6;
                                if (ddlConnectionType.SelectedValue == "1")
                                {
                                    orderObject.PetitionId = Convert.ToInt16(li.Value.ToString());
                                }
                                else if (ddlConnectionType.SelectedValue == "2")
                                {
                                    orderObject.RPId = Convert.ToInt16(li.Value.ToString());

                                }
                                else if (ddlConnectionType.SelectedValue == "3")
                                {
                                    //orderObject.AppealId = Convert.ToInt16(ddlPetitionNo.SelectedValue);
                                }
                                orderObject.OrderTypeID = Convert.ToInt16(ddlOrderType.SelectedValue);
                                orderObject.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
                                p_Var.Result1 = orderBL.insertOrderwithPetition(orderObject);

                            }

                        }


                        //New code to file upload



                        string fileMultiple = string.Empty;
                        HttpFileCollection hfc = Request.Files;

                        for (int i = 0; i < hfc.Count; i++)
                        {
                            HttpPostedFile hpf = hfc[i];

                            fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
                            if (fileMultiple != null && fileMultiple != "")
                            {
                                PetitionOB objnew = new PetitionOB();
                                objnew.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                                //objnew.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);
                                objnew.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft);
                                objnew.PetitionDate = Convert.ToDateTime(miscellBL.getDateFormat(txtOrderDate.Text.ToString()));
                                objnew.OrderID = p_Var.Result;
                                // objnew.OrderSubcategory = Convert.ToInt16(ddlOrderSubCategory.SelectedValue);
                                objnew.OrderType = ddlOrderType.SelectedValue.ToString().Trim();
                                objnew.OrderCatID = Convert.ToInt16(ChkCategory.SelectedValue);


                                objnew.PetitionId = p_Var.Result;

                                if (i == 0)
                                {
                                    objnew.Remarks = TextBox1.Text;
                                }
                                else
                                {
                                    int j = i - 1;


                                    string strId = "txt" + j.ToString();
                                    objnew.Remarks = Request.Form[strId].ToString();
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
                                    objnew.FileName = p_Var.Filename;
                                }

                                if (txtOrderDate.Text != null && txtOrderDate.Text != "")
                                {
                                    objnew.OrderDate = miscellBL.getDateFormat(txtOrderDate.Text);
                                }
                                else
                                {
                                    objnew.OrderDate = System.DateTime.Now;
                                }
                                p_Var.Result1 = orderBL.insertConnectedOrders(objnew);
                            }
                        }



                        obj_audit.ActionType = "I";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = "New Record";
                        string ordertype = ddlOrderType.SelectedItem.ToString();
                        string orderdate = txtOrderDate.Text;
                        obj_audit.Title = ordertype + ", " + orderdate + ", " + txtOrderTitle.Text;
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "Order has been added successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }

                }

            }
        }

        else
        {
            Session.Abandon();
            Response.Redirect("~/Auth/AdminPanel/Login.aspx");
        }
    }

    #endregion

    #region button btnReset click event to reset contents

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtPetitionerName.Text = "";
        txtPetitionerAdd.Text = "";
        txtPetitionerEmailID.Text = "";
        txtPetitionerFaxNo.Text = "";
        txtPetitionerMobileNo.Text = "";
        txtPetitionerName.Text = "";
        txtPetitionerPhoneNo.Text = "";
        txtRespondentAdd.Text = "";
        txtRespondentEmailID.Text = "";
        txtRespondentFaxNo.Text = "";
        txtRespondentMobileNo.Text = "";
        txtRespondentName.Text = "";
        txtRespondentPhoneNo.Text = "";
        txtRemarks.Text = "";
        txtOrderDate.Text = "";
        txtOrderDescription.Text = "";
        txtOrderTitle.Text = "";
        ddlConnectionType.SelectedIndex = 0;
        ddlOrderType.SelectedIndex = 0;


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
                ddlOrderType.DataSource = p_Var.dSet;

                ddlOrderType.DataTextField = "Status";
                ddlOrderType.DataValueField = "Status_Id";
                ddlOrderType.DataBind();
                ddlOrderType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind order type in dropDownlist

    public void displayOrderTypePetition()
    {
        try
        {
            p_Var.dSet = orderBL.getOrderTypeDisplay();
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlOrderType.DataSource = p_Var.dSet;
                ddlOrderType.DataTextField = "Order_Type";
                ddlOrderType.DataValueField = "Order_Type_ID";

                ddlOrderType.DataBind();
                ddlOrderType.Items.Insert(0, new ListItem("Select", "0"));
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
            orderObject.OrderTypeID = ordertype;
            p_Var.dSet = orderBL.getOrderCategories(orderObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                pCategory.Visible = true;
                if (Convert.ToInt16(orderObject.OrderTypeID) == 8)
                {
                    ChkCategory.DataSource = p_Var.dSet;
                    ChkCategory.DataTextField = "OrderCatName";
                    ChkCategory.DataValueField = "OrderCatId";
                    ChkCategory.DataBind();

                    foreach (ListItem li in ChkCategory.Items)
                    {
                        li.Selected = true;
                        li.Enabled = false;
                    }
                    //ChkCategory.Enabled = false;
                }
                else
                {
                    ChkCategory.DataSource = p_Var.dSet;
                    ChkCategory.DataTextField = "OrderCatName";
                    ChkCategory.DataValueField = "OrderCatId";
                    ChkCategory.DataBind();
                    ChkCategory.Enabled = true;
                    //ChkCategory.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ChkCategory.DataSource = p_Var.dSet;
                ChkCategory.DataBind();
                pCategory.Visible = false;
                //ChkCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion


    //End

    //Area for all the customValidators serverValidate events

    #region customValidator CustvalidFileUplaod to validate pdf file only

    protected void CustvalidFileUplaod_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string UploadFileName = fileUploadPdf.PostedFile.FileName;

        if (UploadFileName == "")
        {
            // There is no file selected
            args.IsValid = false;
        }
        else
        {
            string Extension = UploadFileName.Substring(UploadFileName.LastIndexOf('.') + 1).ToLower();

            int count = fileUploadPdf.PostedFile.FileName.Split('.').Length - 1;
            if (Extension == "pdf" && count == 1)
            {
                args.IsValid = true; // Valid file type
            }
            else
            {
                args.IsValid = false; // Not valid file type
                return;
            }
        }
        string fileMultiple = string.Empty;
        HttpFileCollection hfc = Request.Files;
        string str = string.Empty;
        bool strem = true;
        Regex regex = new Regex(@"^[a-z | 0-9 | /,|,:;()#&]*$", RegexOptions.IgnoreCase);
        for (int i = 0; i < hfc.Count; i++)
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
        // Get file name
        //string UploadFileName = fileUploadPdf.PostedFile.FileName;

        //if (UploadFileName == "")
        //{
        //    // There is no file selected
        //    args.IsValid = false;
        //}
        //else
        //{
        //    string Extension = UploadFileName.Substring(UploadFileName.LastIndexOf('.') + 1).ToLower();

        //    if (Extension == "pdf")
        //    {
        //        args.IsValid = true; // Valid file type
        //    }
        //    else
        //    {
        //        args.IsValid = false; // Not valid file type
        //    }
        //}
    }

    #endregion

    //End

    #region dropDownlist ddlConnectionType selectedIndexChanged event

    protected void ddlConnectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pPetitionNumber.Visible = false;
        if (ddlyear.SelectedValue == "0")
        {
            pPetitionNumber.Visible = false;

        }

        if (ddlConnectionType.SelectedValue != "0")
        {
            pnlPetitionSection.Visible = true;
            petitionDetails.Visible = false;

            BindYear();
        }
        else
        {

            pnlPetitionSection.Visible = false;
            petitionDetails.Visible = true;
        }
    }

    #endregion

    #region Function to bind approved petition number in dropDownlist

    public void bindApprovedPetition()
    {
        try
        {
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            pPetitionNumber.Visible = true;
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Remove(0, strBuilder.Length);
            orderObject.ConnectionType = Convert.ToInt16(ddlConnectionType.SelectedValue);

            foreach (ListItem li in ddlyear.Items)
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
                    //strBuilder.Append(li.Text + ";");
                }

            }

            ViewState["MyList"] = list;

            orderObject.year = p_Var.sbuilder.ToString();
            p_Var.dSet = orderBL.getPetitionNumberForOrders(orderObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                chklstPetition.DataSource = p_Var.dSet;
                chklstPetition.DataTextField = "PRONoValue";
                chklstPetition.DataValueField = "Petition_id";
                chklstPetition.DataBind();
                //chklstPetition.Items.Insert(0, new ListItem("Select One", "0"));
            }
            else
            {
                chklstPetition.DataSource = p_Var.dSet;
                chklstPetition.DataBind();
                // chklstPetition.Items.Insert(0, new ListItem("Select One", "0"));
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
                            strBuilder.Append(li.Text + ";");
                        }
                    }
                    // li.Selected = true;
                }
            }
            ltrlSelected.Text = strBuilder.ToString();
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
                chklstPetition.DataSource = p_Var.dSet;
                chklstPetition.DataTextField = "RP_No";
                chklstPetition.DataValueField = "RP_Id";
                chklstPetition.DataBind();
                //chklstPetition.Items.Insert(0, new ListItem("Select One", "0"));
            }
            else
            {
                chklstPetition.DataSource = p_Var.dSet;
                chklstPetition.DataBind();
                // chklstPetition.Items.Insert(0, new ListItem("Select One", "0"));
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
                chklstPetition.DataSource = p_Var.dSet;
                chklstPetition.DataTextField = "Appeal_No";
                chklstPetition.DataValueField = "PA_Id";
                chklstPetition.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion



    #region function to bind year

    public void BindYear()
    {
        orderObject.TempRPId = Convert.ToInt16(ddlConnectionType.SelectedValue);
        p_Var.dSetChildData = orderBL.GetYearAppeal(orderObject);
        if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
        {
            ddlyear.DataSource = p_Var.dSetChildData;
            ddlyear.DataValueField = "year";
            ddlyear.DataTextField = "year";
            ddlyear.DataBind();
        }
        else
        {
            ddlyear.DataSource = p_Var.dSetChildData;
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    #endregion

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedValue != "0")
        {
            bindApprovedPetition();
        }
        else
        {
            pPetitionNumber.Visible = false;
        }
    }


    protected void chklstPetition_SelectedIndexChanged(object sender, EventArgs e)
    {
        string value = string.Empty;
        StringBuilder strBuilder = new StringBuilder();
        string result = Request.Form["__EVENTTARGET"];

        string[] checkedBox = result.Split('$');
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (chklstPetition.Items[index].Selected)
        {

            orderObject.OrderID = Convert.ToInt16(chklstPetition.Items[index].Value);
            orderObject.PetitionType = Convert.ToInt16(ddlConnectionType.SelectedValue);
            p_Var.dSet = orderBL.get_ConnectedOrder_Add(orderObject);
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



    protected void valInquiry_ServerValidation(object source, ServerValidateEventArgs args)
    {
        if (ddlOrderType.SelectedValue != "0")
        {
            args.IsValid = ChkCategory.SelectedItem != null;
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
