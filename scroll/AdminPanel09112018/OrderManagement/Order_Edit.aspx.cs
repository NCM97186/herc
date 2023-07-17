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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public partial class Auth_AdminPanel_OrderManagement_Order_Edit : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration zone

    PetitionOB orderObject = new PetitionOB();
    PetitionOB orderobject1 = new PetitionOB();
    OrderBL orderBL = new OrderBL();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    PublicNoticeBL pubNoticeBL = new PublicNoticeBL();
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
            dsprv = obj_permissionBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }
            displayOrderType();

            if (Request.QueryString["orderID"] != null)    // for Edit
            {
                BindYear();
                bindOrderInEditMode(Convert.ToInt32(Request.QueryString["orderID"]));

            }

            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
            Label lblModulename = Master.FindControl("lblModulename") as Label;
            lblModulename.Text = ": Edit Order";
            this.Page.Title = " Edit Order: HERC";

        }
    }

    #endregion

    //Area for all the buttons, linkButtons, imageButtons click events

    #region button btnUpdateOrder click event to update orders

    protected void btnUpdateOrder_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (Page.IsValid)
                {
                    try
                    {
                        orderObject.ActionType = Convert.ToInt32(Module_ID_Enum.Project_Action_Type.update);
                        orderObject.OrderTitle = HttpUtility.HtmlEncode(txtOrderTitle.Text.Replace(Environment.NewLine, "<br />"));
                        orderObject.OrderDescription = HttpUtility.HtmlEncode(txtOrderDescription.Text.Replace(Environment.NewLine, "<br />"));
                        orderObject.TempOrderID = Convert.ToInt32(Request.QueryString["orderID"]);
                        orderObject.OrderID = Convert.ToInt32(Request.QueryString["orderID"]);
                        orderObject.LangId = Convert.ToInt32(Request.QueryString["LangID"]);
                        orderObject.OrderCatID = Convert.ToInt32(ChkCategory.SelectedValue);
                        orderObject.OrderTypeID = Convert.ToInt32(ddlOrderType.SelectedValue);
                        orderObject.ModuleID = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Orders);
                        orderObject.StatusId = Convert.ToInt32(Session["Status_Id"]);

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

                        if (divConnectAdd1.Visible == true)
                        {
                            foreach (ListItem li in chklstPetitionEdit.Items)
                            {
                                if (li.Selected == true)
                                {
                                    orderObject.ConnectionType = Convert.ToInt32(ddlConnectionTypeEdit.SelectedValue);
                                    break;

                                }
                                else
                                {
                                    orderObject.ConnectionType = 0;
                                }
                            }
                        }
                        else
                        {
                            orderObject.ConnectionType = 0;
                        }
                        if (pnlPetitionSection.Visible == true)
                        {
                            orderObject.petitionYear = ddlyear.SelectedValue;
                        }
                        else
                        {
                            orderObject.petitionYear = null;
                        }
                        if (ddlConnectionTypeEdit.SelectedValue == "1")
                        {
                        }
                        else if (ddlConnectionTypeEdit.SelectedValue == "2")
                        {
                            if (ddlOrderType.SelectedValue == "13")
                            {
                                orderObject.OrderTypeID = 9;
                            }
                            else if (ddlOrderType.SelectedValue == "12")
                            {
                                orderObject.OrderTypeID = 8;
                            }
                        }
                        else if (ddlConnectionTypeEdit.SelectedValue == "3")
                        {
                        }

                        //End

                        orderObject.InsertedBy = Convert.ToInt32(Session["User_Id"]);
                        orderObject.StartDate = null;

                        if (txtendate.Text != null && txtendate.Text != "")
                        {
                            orderObject.EndDate = null;
                        }
                        else
                        {
                            orderObject.EndDate = null;
                        }

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
                        orderObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                        orderObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = orderBL.insertUpdateOrderTemp(orderObject);

                        if (p_Var.Result > 0)
                        {

                            //This is for Ordercategory
                            orderObject.OrderID = p_Var.Result;
                            int ResultOrdercategory = orderBL.deleteConnectedCategoryFromOrder(orderObject);
                            foreach (ListItem liYear in chklstPetitionEdit.Items)
                            {
                                if (liYear.Selected == true)
                                {
                                    int cnt = liYear.Text.LastIndexOf(' ');
                                    orderObject.year = liYear.Text.Substring(cnt).Trim();
                                }
                            }
                            foreach (ListItem li in ChkCategory.Items)
                            {
                                if (li.Selected == true)
                                {
                                    orderObject.OrderID = p_Var.Result;
                                    orderObject.StatusId = 6;
                                    orderObject.OrderTypeID = Convert.ToInt32(ddlOrderType.SelectedValue);
                                    orderObject.OrderCatID = Convert.ToInt32(li.Value.ToString());
                                    Int32 Result = orderBL.insertConnectedOrderCategory(orderObject);
                                }
                            }

                            orderObject.OrderID = p_Var.Result;
                            p_Var.Result1 = orderBL.deleteConnectedPetitionFromOrder(orderObject);

                            foreach (ListItem li in chklstPetitionEdit.Items)
                            {
                                if (li.Selected == true)
                                {
                                    orderObject.OrderID = p_Var.Result;
                                    int cnt = li.Text.LastIndexOf(' ');
                                    orderObject.year = li.Text.Substring(cnt).Trim();
                                    orderObject.StatusId = 6;
                                    if (ddlConnectionTypeEdit.SelectedValue == "1")
                                    {
                                        orderObject.PetitionId = Convert.ToInt32(li.Value.ToString());
                                    }
                                    else if (ddlConnectionTypeEdit.SelectedValue == "2")
                                    {
                                        orderObject.RPId = Convert.ToInt32(li.Value.ToString());

                                    }
                                    else if (ddlConnectionTypeEdit.SelectedValue == "3")
                                    {
                                    }
                                    orderObject.OrderTypeID = Convert.ToInt32(ddlOrderType.SelectedValue);
                                    orderObject.DepttId = Convert.ToInt32(Module_ID_Enum.hercType.herc);
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
                                    objnew.ActionType = Convert.ToInt32(Module_ID_Enum.Project_Action_Type.insert);
                                    objnew.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft);
                                    objnew.PetitionDate = Convert.ToDateTime(miscellBL.getDateFormat(txtOrderDate.Text.ToString()));
                                    objnew.OrderID = p_Var.Result;
                                    if (psubcategory.Visible == true)
                                    {
                                        objnew.OrderSubcategory = Convert.ToInt32(ddlOrderSubCategory.SelectedValue);
                                    }
                                    else
                                    {
                                        objnew.OrderSubcategory = null;
                                    }
                                    objnew.OrderType = ddlOrderType.SelectedValue.ToString().Trim();
                                    objnew.OrderCatID = Convert.ToInt32(ChkCategory.SelectedValue);


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

                            obj_audit.ActionType = "U";
                            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                            obj_audit.UserName = Session["UserName"].ToString();
                            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                            obj_audit.IpAddress = miscellBL.IpAddress();
                            obj_audit.Title = txtOrderTitle.Text;
                            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                            Session["msg"] = "Order has been updated successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
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

    #region button btnReset click event to reset previous content

    protected void btnReset_Click(object sender, EventArgs e)
    {
        BindYear();
        bindOrderInEditMode(Convert.ToInt32(Request.QueryString["orderID"]));
        displayOrderType(Convert.ToInt32(ddlOrderType.SelectedValue));
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

    #region dropDownlist ddlOrderType selectedIndexChanged events

    protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayOrderType(Convert.ToInt32(ddlOrderType.SelectedValue));
        if (ddlOrderType.SelectedValue == "0")
        {
            TextBox1.Enabled = true;

        }
        else
        {
            TextBox1.Enabled = false;
        }

        PetitionOB objnew = new PetitionOB();
        objnew.OrderID = Convert.ToInt32(Request.QueryString["orderID"]);
        p_Var.dSetCompare = orderBL.get_ConnectedOrderCategory_Edit(objnew);
        if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
        {
            for (p_Var.i = 0; p_Var.i < p_Var.dSetCompare.Tables[0].Rows.Count; p_Var.i++)
            {
                foreach (ListItem li in ChkCategory.Items)
                {
                    if (li.Value.ToString() == p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["OrderCategoryID"].ToString())
                    {
                        li.Selected = true;

                    }


                }
            }
        }
    }

    #endregion
    
    //End

    //Area for all the user defined functions

    #region Function to bind Order in edit mode

    public void bindOrderInEditMode(int orderID)
    {
        try
        {
            p_Var.dSet = null;
            orderObject.TempOrderID = orderID;
            orderObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = orderBL.getOrdersRecordForEdit(orderObject);
            StringBuilder sbuilderYear = new StringBuilder();
            DataSet dsetYear = new DataSet();
            dsetYear = orderBL.getOrderYearForConnectionEdit(orderObject);

            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                txtMetaDescription.Text = p_Var.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
                txtMetaTitle.Text = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
                txtMetaKeyword.Text = p_Var.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
                ddlMetaLang.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();

                if (p_Var.dSet.Tables[0].Rows[0]["PlaceHolderFive"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["PlaceHolderFive"].ToString()!="")
                {
                        txtRemarks.Text =HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["PlaceHolderFive"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                }
                displayOrderType(Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["OrderTypeID"]));
                if (p_Var.dSet.Tables[0].Rows[0]["Year"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["Year"] != "")
                 {
                     if (p_Var.dSet.Tables[0].Rows[0]["Petition_ReviewPetitionYear"] != DBNull.Value)
                     {

                         for (p_Var.i = 0; p_Var.i < dsetYear.Tables[0].Rows.Count; p_Var.i++)
                         {
                             foreach (ListItem li in ddlyear.Items)
                             {
                                 if (li.Value.ToString() == dsetYear.Tables[0].Rows[p_Var.i]["year"].ToString().Trim())
                                 {
                                     li.Selected = true;
                                     sbuilderYear.Append(li.Text).Append(",");
                                 }
                             }
                         }
                     }
                     ViewState["year"] = sbuilderYear.ToString();
                }
                if (orderObject.StatusId == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                {
                    if (Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["OrderTypeID"]) == 9)
                    {

                        if (p_Var.dSet.Tables[0].Rows[0]["OrderID"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["OrderID"] != "")
                        {
                            PConnectedOrder.Visible = true;
                            TextBox1.Enabled = true;
                            PuploadFile.Visible = true;

                            if (p_Var.dSet.Tables[0].Rows[0]["OrdersubCatId"] != DBNull.Value)
                            {
                                PConnectedOrder.Visible = true;
                                PuploadFile.Visible = true;
                                BindSubOrders(Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["OrderCatID"]));
                                psubcategory.Visible = true;
                                ddlOrderSubCategory.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["OrdersubCatId"].ToString();
                                lnkConnectedOrders.Visible = false;
                                lnkConnectedOrdersNo.Visible = true;
                                ddlOrderType.Enabled = false;
                            }
                            else
                            {
                                reqFileUploader.Enabled = false;
                                PConnectedOrder.Visible = true;
                                psubcategory.Visible = false;
                                PuploadFile.Visible = true;
                            }
                         
                        }
                        else
                        {
                            PConnectedOrder.Visible = false;
                            PuploadFile.Visible = true;
                        }
                    }
                    else
                    {
                        PuploadFile.Visible = true;
                    }
                }
                else
                {
                    if (p_Var.dSet.Tables[0].Rows[0]["OrdersubCatId"] != DBNull.Value)
                    {
                        PConnectedOrder.Visible = true;
                        PuploadFile.Visible = true;
                        psubcategory.Visible = true;
                       // BindSubOrders(Convert.ToInt32(ddlOrderCategory.SelectedValue));
                        BindSubOrders(Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["OrderCatID"]));
                        ddlOrderSubCategory.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["OrdersubCatId"].ToString();
                        lnkConnectedOrders.Visible = false;
                        lnkConnectedOrdersNo.Visible = true;
                        ////ddlOrderCategory.Enabled = false;
                        ddlOrderType.Enabled = false;


                        
                    }
                    else
                    {
                        reqFileUploader.Enabled = false;
                        PConnectedOrder.Visible = false;
                        PuploadFile.Visible = true;
                    }
                }
                //New code to bind dropDownlist in edit modes

                if (p_Var.dSet.Tables[0].Rows[0]["Petition_ID"] != DBNull.Value || p_Var.dSet.Tables[0].Rows[0]["ReviewId"] != DBNull.Value)
                {
                    pnlPetitionSection.Visible = true;
                    if (p_Var.dSet.Tables[0].Rows[0]["ReviewPetition_ID"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["PetitionAppealID"] != DBNull.Value)
                    {
                        ddlConnectionTypeEdit.SelectedValue = "3";
                        bindApprovedPetitionEdit(Request.QueryString["Connection"].ToString());
                       //// bindApprovedPetitionEdit(ddlyear.SelectedValue, Request.QueryString["Connection"].ToString());
                        
                    }
                    else if (p_Var.dSet.Tables[0].Rows[0]["ReviewPetition_ID"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["PetitionAppealID"] == DBNull.Value)
                    {
                        ddlConnectionTypeEdit.SelectedValue = "2";
                        bindApprovedPetitionEdit(Request.QueryString["Connection"].ToString());
                      
                    }

                    else if (p_Var.dSet.Tables[0].Rows[0]["Petition_ID"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["Petition_ID"] != "")
                    {
                        ddlConnectionTypeEdit.SelectedValue = "1";

                        bindApprovedPetitionEdit(Request.QueryString["Connection"].ToString());
                    
                    }
                    else
                    {
                        bindApprovedPetitionEdit(Request.QueryString["Connection"].ToString());

                    }

                }
                else
                {
                    pnlPetitionSection.Visible = false;
                   
                }

                //End
                txtstartdate.Text = p_Var.dSet.Tables[0].Rows[0]["placeHolderOne"].ToString();
            
                if (p_Var.dSet.Tables[0].Rows[0]["PlaceHolderTwo"].ToString() == "01/01/1900")
                {
                    txtendate.Text = "";
                }
                else
                {
                    txtendate.Text = p_Var.dSet.Tables[0].Rows[0]["placeHolderTwo"].ToString();
                }

                if (p_Var.dSet.Tables[0].Rows[0]["ConnectionID"].ToString() == "0" || p_Var.dSet.Tables[0].Rows[0]["ConnectionID"].ToString() == "")
                {
                    petitionDetails.Visible = true;
                    txtPetitionerName.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Name"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                    txtPetitionerAdd.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                    txtPetitionerMobileNo.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Mobile_No"].ToString());
                    txtPetitionerPhoneNo.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Phone_No"].ToString());
                    txtPetitionerFaxNo.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Fax_No"].ToString());

                    txtPetitionerEmailID.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Petitioner_Email"].ToString());
                    txtRespondentName.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Name"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                    txtRespondentAdd.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                    txtRespondentMobileNo.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Mobile_No"].ToString());
                    txtRespondentPhoneNo.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Phone_No"].ToString());
                    txtRespondentFaxNo.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Fax_No"].ToString());
                    txtRespondentEmailID.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Email"].ToString());
                }
                else
                {
                    petitionDetails.Visible = false;
                }
                txtOrderTitle.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["OrderTitle"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                txtOrderDescription.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["OrderDescription"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));

                txtOrderDate.Text = p_Var.dSet.Tables[0].Rows[0]["OrderDate"].ToString();
                ddlOrderType.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["OrderTypeID"].ToString();

                if (p_Var.dSet.Tables[0].Rows[0]["OrderFile"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["OrderFile"].ToString() != "")
                {
                   
                    ViewState["filename"] = p_Var.dSet.Tables[0].Rows[0]["OrderFile"].ToString();
                    BindData(Convert.ToInt32(Request.QueryString["orderID"]));
                }
                else
                {
                   
                }



                if (p_Var.dSet.Tables[0].Rows[0]["OrderCatID"] != null && p_Var.dSet.Tables[0].Rows[0]["OrderCatID"].ToString() != "")
                {
                   /////// ViewState["ordercat"] = p_Var.dSet.Tables[0].Rows[0]["OrderCatID"].ToString();
                }
                // 6 Dec Here to bind connected filename and sub category
                PetitionOB objnew = new PetitionOB();
                objnew.OrderID = orderID;

                p_Var.dSetChildData = orderBL.get_ConnectedOrders_Edit(objnew);
                if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
                {
                    if (p_Var.dSetChildData.Tables[0].Rows[0]["FileName"] != null && p_Var.dSetChildData.Tables[0].Rows[0]["FileName"].ToString() != "")
                    {
                      
                        ViewState["filename"] = p_Var.dSetChildData.Tables[0].Rows[0]["FileName"].ToString();
                        BindData(Convert.ToInt32(Request.QueryString["orderID"]));
                     
                    }
                    else
                    {
                       

                    }
                    if (p_Var.dSetChildData.Tables[0].Rows[0]["SubCategoryID"] != DBNull.Value && p_Var.dSetChildData.Tables[0].Rows[0]["SubCategoryID"].ToString() != "")
                    {

                        BindSubOrders(Convert.ToInt32(p_Var.dSet.Tables[0].Rows[0]["OrderCatID"]));
                       
                    }

                }
            }

            PetitionOB newOB = new PetitionOB();
            newOB.OrderID = Convert.ToInt32(orderID);


            p_Var.dsFileName = orderBL.get_ConnectedOrder_Edit(newOB);

            //This is for connected categories
            p_Var.dSetCompare = orderBL.get_ConnectedOrderCategory_Edit(newOB);

            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {
                for (p_Var.i = 0; p_Var.i < p_Var.dSetCompare.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in ChkCategory.Items)
                    {
                        if (li.Value.ToString() == p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["OrderCategoryID"].ToString())
                        {
                            li.Selected = true;

                        }
                       

                    }
                }
            }
            //End

            if (p_Var.dSet.Tables[0].Rows[0]["Petition_ID"] != DBNull.Value)
            {
                //ddlConnectionTypeEdit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["Petition_ID"].ToString();
                ddlConnectionTypeEdit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["ConnectionID"].ToString();
            }
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                if (p_Var.dSet.Tables[0].Rows[0]["Petition_ID"] == DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["Petition_ID"].ToString() == "")
                {
                    ddlConnectionTypeEdit.SelectedValue = "2";
                    divConnectAdd1.Visible = true;

                }
                else
                {
                    ddlConnectionTypeEdit.SelectedValue = "1";
                    divConnectAdd1.Visible = true;

                }
            }
            else
            {
                ddlConnectionTypeEdit.SelectedValue = "0";
                divConnectAdd1.Visible = false;
                pnlPetitionSection.Visible = false;

            }
            StringBuilder strBuilder = new StringBuilder();
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (p_Var.i = 0; p_Var.i < p_Var.dsFileName.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in chklstPetitionEdit.Items)
                    {
                        if (li.Value.ToString() == p_Var.dsFileName.Tables[0].Rows[p_Var.i]["Petition_id"].ToString())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");

                        }
                        else if (li.Value.ToString() == p_Var.dsFileName.Tables[0].Rows[p_Var.i]["ReviewId"].ToString())
                        {
                            li.Selected = true;
                            strBuilder.Append(li.Text + ";");
                        }

                    }
                }
                ltrlSelected.Text = strBuilder.ToString();
            }
            else
            {
                    //ddlyear.Items.Insert(0, new ListItem("Select One", "0"));
                    ddlyear.Enabled = true;
                    ddlConnectionTypeEdit.Enabled = true;
                    //ddlConnectionTypeEdit.SelectedValue = Request.QueryString["Connection"].ToString();
                    if (ddlConnectionTypeEdit.SelectedValue == "0")
                    {
                        pnlPetitionSection.Visible = false;
                    }
                    else
                    {
                        pnlPetitionSection.Visible = true;
                    }
                    if (Request.QueryString["Connection"].ToString() != "" && Request.QueryString["Connection"] != null)
                    {
                        bindApprovedPetitionEdit(Request.QueryString["Connection"].ToString());
                    }

                   foreach (ListItem li in chklstPetitionEdit.Items)
                    {
                        if (li.Selected == true)
                        {
                            orderObject.OrderID = Convert.ToInt32(Request.QueryString["orderID"]);
                            orderObject.year = ddlyear.SelectedValue.ToString();
                            orderObject.StatusId = 6;
                            if (Request.QueryString["Connection"].ToString() == "1")
                            {
                                orderObject.PetitionId = Convert.ToInt32(li.Value.ToString());
                            }
                            else if (Request.QueryString["Connection"].ToString() == "2")
                            {
                                orderObject.RPId = Convert.ToInt32(li.Value.ToString());

                            }

                            orderObject.DepttId = Convert.ToInt32(Module_ID_Enum.hercType.herc);
                            p_Var.Result1 = orderBL.insertOrderwithPetition(orderObject);

                        }
                        else
                        {
                            BindYear();
                        }

                    }          
               
            }
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
            DataSet dsetnew = new DataSet();
            orderObject.OrderTypeID = ordertype;
            dsetnew = orderBL.getOrderCategories(orderObject);
            if (dsetnew.Tables[0].Rows.Count > 0)
            {
                pCategory.Visible = true;
                // 5 june
                TextBox1.Enabled = false;
                if (Convert.ToInt32(orderObject.OrderTypeID) == 8)
                {
                    ChkCategory.DataSource = dsetnew;
                    ChkCategory.DataTextField = "OrderCatName";
                    ChkCategory.DataValueField = "OrderCatId";
                    ChkCategory.DataBind();
                    foreach (ListItem li in ChkCategory.Items)
                    {
                        li.Selected = true;
                        li.Enabled = false;
                    }
                   
                }
                else
                {
                    ChkCategory.DataSource = dsetnew;
                    ChkCategory.DataTextField = "OrderCatName";
                    ChkCategory.DataValueField = "OrderCatId";
                    ChkCategory.DataBind();
                    ChkCategory.Enabled = true;
                   // ChkCategory.Items.Insert(0, new ListItem("Select", "0"));
                  
                }
            }
            else
            {
                ChkCategory.DataSource = dsetnew;
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

    //Area for all the custom validators

    #region customValidator cvPublicNotice serverValidate to validate file type

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

    #region Function to bind approved petition number in dropDownlist in edit mode

    public void bindApprovedPetitionEdit(string connectionId)
    {
        try
        {
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

            foreach (ListItem li in ddlyear.Items)
            {
                if (li.Selected == true)
                {
                    p_Var.sbuilder.Append(li.Text).Append(",");
                }

            }
            List<string> list = new List<string>();
            foreach (ListItem li in chklstPetitionEdit.Items)
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
           // ViewState["MyList"] = list;

            orderObject.year = p_Var.sbuilder.ToString();

            orderObject.ConnectionType = Convert.ToInt32(connectionId);
          
            p_Var.dSetChildData = orderBL.getPetitionNumberForOrders(orderObject);
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {

                chklstPetitionEdit.DataSource = p_Var.dSetChildData;
                chklstPetitionEdit.DataTextField = "PRONoValue";
                chklstPetitionEdit.DataValueField = "Petition_id";
                chklstPetitionEdit.DataBind();
                PetitionOB newOB = new PetitionOB();
                OrderBL newBL = new OrderBL();
                if (ViewState["year"] != null && ViewState["year"].ToString() != "")
                {

                    newOB.OrderID = Convert.ToInt32(Request.QueryString["orderID"]);
                    newOB.PetitionType = Convert.ToInt32(ddlConnectionTypeEdit.SelectedValue);
                    DataSet dset = new DataSet();
                    dset = newBL.getConnectedOrderEdit(newOB);


                    if (dset.Tables[0].Rows.Count > 0)
                    {

                        StringBuilder strBuilder = new StringBuilder();
                        strBuilder.Remove(0, strBuilder.Length);
                        for (p_Var.i = 0; p_Var.i < dset.Tables[0].Rows.Count; p_Var.i++)
                        {
                            foreach (ListItem li in chklstPetitionEdit.Items)
                            {
                                if (ddlConnectionTypeEdit.SelectedValue == "1")
                                {
                                    if (dset.Tables[0].Columns.Contains("Petition_id"))
                                    {
                                        if (li.Value.ToString() == dset.Tables[0].Rows[p_Var.i]["Petition_id"].ToString())
                                        {
                                            li.Selected = true;
                                            strBuilder.Append(li.Text + ";");
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
                                            strBuilder.Append(li.Text + ";");
                                        }
                                    }
                                }
                            }
                        }
                        ltrlSelected.Text = strBuilder.ToString();
                    }

                }
                
            }
            else
            {
                ltrlSelected.Text = "";
                chklstPetitionEdit.DataSource = p_Var.dSetChildData;
                chklstPetitionEdit.DataBind();
            }



        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind approved petition review number in dropDownlist in edit mode

    public void bindApprovedPetitionReviewEdit()
    {
        try
        {
            p_Var.dSetChildData = orderBL.getPetitionReviewNumber();
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {
                chklstPetitionEdit.DataSource = p_Var.dSetChildData;
                chklstPetitionEdit.DataTextField = "RP_No";
                chklstPetitionEdit.DataValueField = "RP_Id";
                chklstPetitionEdit.DataBind();
                //chklstPetitionEdit.Items.Insert(0, new ListItem("Select One", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind approved petition appeal number in dropDownlist in edit mode

    public void bindApprovedPetitionAppealEdit()
    {
        try
        {
            p_Var.dSetChildData = orderBL.getPetitionAppealNumber();
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {
                chklstPetitionEdit.DataSource = p_Var.dSetChildData;
                chklstPetitionEdit.DataTextField = "Appeal_No";
                chklstPetitionEdit.DataValueField = "PA_Id";
                chklstPetitionEdit.DataBind();
               //// chklstPetitionEdit.Items.Insert(0, new ListItem("Select One", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    protected void ddlConnectionTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlConnectionTypeEdit.SelectedValue != "0")
        {
            pnlPetitionSection.Visible = true;
            divConnectAdd1.Visible = false;
            petitionDetails.Visible = false;
            //BindYear();
            BindYear1();
            if (ddlConnectionTypeEdit.SelectedValue == "1")
            {
                ////bindApprovedPetitionEdit(ddlyear.SelectedValue.ToString(), ddlConnectionTypeEdit.SelectedValue.ToString());
                bindApprovedPetitionEdit(ddlConnectionTypeEdit.SelectedValue.ToString());
            }
            else if (ddlConnectionTypeEdit.SelectedValue == "2")
            {
                bindApprovedPetitionReviewEdit();
            }
            else if (ddlConnectionTypeEdit.SelectedValue == "3")
            {
                bindApprovedPetitionAppealEdit();
            }
        }
        else
        {
            petitionDetails.Visible = true;
            pnlPetitionSection.Visible = false;
        }
    }

    #region Function to bind  subOrder in dropDownlist

    public void BindSubOrders(int orderTypeId)
    {
        try
        {
           // psubcategory.Visible = true;
            orderObject.OrderTypeID = orderTypeId;
            p_Var.dSetCompare = orderBL.getOrderSubcategory(orderObject);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {
                ddlOrderSubCategory.DataSource = p_Var.dSetCompare;
                ddlOrderSubCategory.DataTextField = "OrderSubCategoryName";
                ddlOrderSubCategory.DataValueField = "OrderSubCategoryId";
                ddlOrderSubCategory.DataBind();
                ddlOrderSubCategory.Items.Insert(0, new ListItem("Select One", "0"));
            }
            else
            {
                ddlOrderSubCategory.DataSource = p_Var.dSetCompare;
                ddlOrderSubCategory.DataBind();
                ddlOrderSubCategory.Items.Insert(0, new ListItem("Select One", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region linkButton lnkConnectedPetition click event to display connected petition

    protected void lnkConnectedOrders_Click(object sender, EventArgs e)
    {
        PetitionOB obj = new PetitionOB();

        lnkConnectedOrdersNo.Visible = true;
        lnkConnectedOrders.Visible = false;
        BindSubOrders(Convert.ToInt32(ChkCategory.SelectedValue));
        psubcategory.Visible = true;
        //Dynamic requirefield validator to validate file uploader

        if (psubcategory.Visible == true)
        {
            reqFileUploader.ErrorMessage = "Please select any pdf file to upload.";
            reqFileUploader.Display = ValidatorDisplay.Dynamic;
            reqFileUploader.ControlToValidate = "fileUploadPdf";
            reqFileUploader.EnableClientScript = true;
            reqFileUploader.Enabled = true;
            reqFileUploader.EnableViewState = true;
            reqFileUploader.Visible = true;
            reqFileUploader.ID = "reqFileUpload";
            reqFileUploader.ValidationGroup = "Add";
            this.Page.Validators.Add(reqFileUploader);
        }
        else
        {
            reqFileUploader.Enabled = false;
        }

        //End
    }

    #endregion

    #region linkButton lnkConnectedPetitionNo click event

    protected void lnkConnectedOrdersNo_Click(object sender, EventArgs e)
    {
        lnkConnectedOrders.Visible = true;
        lnkConnectedOrdersNo.Visible = false;
        psubcategory.Visible = false;
        reqFileUploader.Enabled = false;
        BindSubOrders(0);

        psubcategory.Visible = false;

    }

    #endregion

    #region function to bind year

    public void BindYear()
    {
        if (Request.QueryString["Connection"] != null && Request.QueryString["Connection"].ToString() != "")
        {
            orderObject.ConnectionID = Convert.ToInt32(Request.QueryString["Connection"]);
        }
        else
        {
            orderObject.ConnectionID = null;
        }
        p_Var.dSetChildData = orderBL.GetYearOrdersEdit(orderObject);
        if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
        {
            ddlyear.DataSource = p_Var.dSetChildData;
            ddlyear.DataValueField = "year";
            ddlyear.DataTextField = "year";
            ddlyear.DataBind();
            //ddlyear.Items.Insert(0, new ListItem("Select One", "0"));

        }
        
    }
    #endregion

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getpetitionNumberForChkBoxConnection(Convert.ToInt32(ddlConnectionTypeEdit.SelectedValue.ToString()));
        //if (ddlyear.SelectedValue != "0" && ddlyear.SelectedValue != "")
        //{
        //    divConnectAdd1.Visible = true;
        //    bindApprovedPetitionEdit(ddlConnectionTypeEdit.SelectedValue.ToString());
        //}
        //else
        //{
        //    divConnectAdd1.Visible = false;
        //    bindApprovedPetitionEdit(ddlConnectionTypeEdit.SelectedValue.ToString());
        //}
    }

    protected void ddlOrderSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrderSubCategory.SelectedValue != "0")
        {
            BindData(Convert.ToInt32(Request.QueryString["OrderId"]));
        }
    }

    #region function  bind with Repeator
    public void BindData(int orderId)
    {
        orderObject.OrderID = orderId;
        p_Var.dsFileName = orderBL.getFileNameForOrders(orderObject);
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

    protected void datalistFileName_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("File"))
        {
           int id = Convert.ToInt32(e.CommandArgument.ToString());
           
           orderObject.ConnectionID = id;
           p_Var.Result1 = orderBL.UpdateFileStatusForOrders(orderObject);
           if (p_Var.Result1>0)
           {
              Label filename = (Label)e.Item.FindControl("lblFile");
              Label date = (Label)e.Item.FindControl("lblDate");
              Label OrderType = (Label)e.Item.FindControl("lblOrderName");
              LinkButton RemoveFileLink = (LinkButton)e.Item.FindControl("lnkFileConnectedRemove");
              Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");  
              filename.Visible = false;
              date.Visible = false;
              OrderType.Visible = false;
              RemoveFileLink.Visible = false;
              ltrlDownload.Visible = false;
           }
          bindOrderInEditMode(Convert.ToInt32(Request.QueryString["orderID"]));
           //BindData(Convert.ToInt32(Request.QueryString["orderID"]));
           
        }
    }

    protected void datalistFileName_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblOrderName = (Label)e.Item.FindControl("lblOrderName");
            HiddenField hidCat = (HiddenField)e.Item.FindControl("hidCat");
            HiddenField hidSubCat = (HiddenField)e.Item.FindControl("hidSubCat");
            HiddenField hidComments = (HiddenField)e.Item.FindControl("hidComments");
            Label lblDate = (Label)e.Item.FindControl("lblDate");
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            if (hidSubCat.Value != null && hidSubCat.Value.ToString() != "")
            {
                lblOrderName.Text = "";
            }
            if (hidComments.Value != null && hidComments.Value.ToString() != "")
            {
                lblOrderName.Text += ", " + hidComments.Value.ToString();
            }
            if (hidCat.Value != null && hidCat.Value.ToString() != "")
            {
                lblOrderName.Text += ", Category : " + hidCat.Value.ToString();
            }
            if (hidSubCat.Value != null && hidSubCat.Value.ToString() != "")
            {
                lblDate.Visible = false;
                lblOrderName.Text += ", SubCategory : " + hidSubCat.Value.ToString();
            }
            else
            {
                lblDate.Visible = true;
            }

            

            Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");
            Label filename = (Label)e.Item.FindControl("lblFile");

            var link = e.Item.FindControl("lnkFileConnectedRemove") as LinkButton;
            var hiddenField = e.Item.FindControl("hiddenFieldOrderId") as HiddenField;


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
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["orderID"]))
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
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["orderID"]))
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


    protected void chklstPetitionEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        string value = string.Empty;
        StringBuilder strBuilder = new StringBuilder();
        string result = Request.Form["__EVENTTARGET"];

        string[] checkedBox = result.Split('$');
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (chklstPetitionEdit.Items[index].Selected)
        {

            orderObject.OrderID = Convert.ToInt32(chklstPetitionEdit.Items[index].Value);
            orderObject.PetitionType =Convert.ToInt32( ddlConnectionTypeEdit.SelectedValue);
            p_Var.dSet = orderBL.get_ConnectedOrder_Add(orderObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_Var.i = 0; p_Var.i < p_Var.dSet.Tables[0].Rows.Count; p_Var.i++)
                {
                    foreach (ListItem li in chklstPetitionEdit.Items)
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
                foreach (ListItem li in chklstPetitionEdit.Items)
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
            foreach (ListItem li in chklstPetitionEdit.Items)
            {
                if (li.Selected == true)
                {
                    strBuilder.Append(li.Text + ";");
                }
            }
            ltrlSelected.Text = strBuilder.ToString();
        }
    }
    #region function to bind year

    public void BindYear1()
    {
        orderObject.TempRPId = Convert.ToInt32(ddlConnectionTypeEdit.SelectedValue);
        p_Var.dSetChildData = orderBL.GetYearAppeal(orderObject);
        if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
        {
            ddlyear.DataSource = p_Var.dSetChildData;
            ddlyear.DataValueField = "year";
            ddlyear.DataTextField = "year";
            ddlyear.DataBind();
            //ddlyear.Items.Insert(0, new ListItem("Select One", "0"));

        }
        else
        {
            ddlyear.DataSource = p_Var.dSetChildData;
            ddlyear.DataBind();
           // ddlyear.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    #endregion

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

    #region Function to get Petition numbers for connections

    public void getpetitionNumberForChkBoxConnection(int id)
    {
        orderObject.RPId = id;

        foreach (ListItem li in ddlyear.Items)
        {
            if (li.Selected == true)
            {
                p_Var.sbuilder.Append(li.Text).Append(",");
                
            }

        }
        List<string> list = new List<string>();
        foreach (ListItem li in chklstPetitionEdit.Items)
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

        orderObject.year = p_Var.sbuilder.ToString();
        p_Var.dsFileName = pubNoticeBL.getPetitionNumberForConnection(orderObject);
        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
        {
            chklstPetitionEdit.DataSource = p_Var.dsFileName;
            divConnectAdd1.Visible = true;
            if (orderObject.RPId == 1)
            {
                chklstPetitionEdit.DataValueField = "Petition_id";
                chklstPetitionEdit.DataTextField = "PROValue";
            }
            else
            {
                chklstPetitionEdit.DataValueField = "RP_Id";
                chklstPetitionEdit.DataTextField = "RPValue";
            }
            chklstPetitionEdit.DataBind();

            PetitionOB newOB = new PetitionOB();
            OrderBL newBL = new OrderBL();
            StringBuilder strBuilder = new StringBuilder();
            //StringBuilder strBuilderTemp = new StringBuilder();
            if (ViewState["year"] != null && ViewState["year"].ToString() != "")
            {

                newOB.OrderID = Convert.ToInt32(Request.QueryString["OrderId"]);
                newOB.PetitionType = Convert.ToInt32(ddlConnectionTypeEdit.SelectedValue);
                DataSet dset = new DataSet();
                dset = newBL.getConnectedOrderEdit(newOB);


                if (dset.Tables[0].Rows.Count > 0)
                {

                    strBuilder.Remove(0, strBuilder.Length);
                    for (p_Var.i = 0; p_Var.i < dset.Tables[0].Rows.Count; p_Var.i++)
                    {
                        foreach (ListItem li in chklstPetitionEdit.Items)
                        {
                            if (ddlConnectionTypeEdit.SelectedValue == "1")
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
                }

            }
            if (ViewState["MyList"] != null)
            {
                List<string> list1 = ViewState["MyList"] as List<string>;

                string[] stringArray = strBuilder.ToString().Split(';');
                list1.AddRange(stringArray);
                list1=list.Distinct().ToList();
                strBuilder.Remove(0, strBuilder.Length);
                foreach (ListItem li in chklstPetitionEdit.Items)
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
            chklstPetitionEdit.DataSource = p_Var.dsFileName;
            chklstPetitionEdit.DataBind();
            divConnectAdd1.Visible = false;
        }


    }

    #endregion
}
