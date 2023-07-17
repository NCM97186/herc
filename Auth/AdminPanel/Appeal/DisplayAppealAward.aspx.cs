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
using System.Text;

public partial class Auth_AdminPanel_Appeal_DisplayAppealAward : CrsfBase //System.Web.UI.Page
{
    #region variable declaretion

    Project_Variables p_var = new Project_Variables();
    AppealBL appealBL = new AppealBL();
    PetitionOB appealobject = new PetitionOB();
    UserBL obj_UserBL = new UserBL();
    UserOB obj_userOB = new UserOB();
    PetitionBL petPetitionBL = new PetitionBL();
    PetitionOB petObject = new PetitionOB();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();
    #endregion

    #region page load zone
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Remove("WhatsNewStatus"); // What's New sessions
        Session.Remove("Lng"); // RTI sessions
        Session.Remove("deptt");
        Session.Remove("Status");
        Session.Remove("year");
        Session.Remove("FAALng"); // FAARTI sessions
        Session.Remove("FAAdeptt");
        Session.Remove("FAAStatus");
        Session.Remove("FAAyear");
        Session.Remove("SAALng"); // SAARTI sessions
        Session.Remove("SAAdeptt");
        Session.Remove("SAAStatus");
        Session.Remove("SAAyear");

        Session.Remove("MixLng"); // Mix module sessions
        Session.Remove("Mixdeptt");
        Session.Remove("MixStatus");
        Session.Remove("MixModule");

        Session.Remove("PStatus");// Public Notice module sessions
        Session.Remove("PYear");
        Session.Remove("PLng");

        Session.Remove("MLng"); //  module sessions
        Session.Remove("Mdeptt");
        Session.Remove("MStatus");

        Session.Remove("RoleDeptt");//Role Sessions

        Session.Remove("UsrDeptt");//User Sessions
        Session.Remove("UsrStatus");

        Session.Remove("ProfileLng"); // Profile module sessions
        Session.Remove("ProfileNvg");
        Session.Remove("ProfileDeptt");
        Session.Remove("profileStatus");

        Session.Remove("menulang"); //  menu sessions
        Session.Remove("menuposition");
        Session.Remove("menulst");
        Session.Remove("menustatus");

        Session.Remove("TariffCategory"); //  Tariff sessions
        Session.Remove("TariffLng");
        Session.Remove("TariffDeptt");
        Session.Remove("TariffType");
        Session.Remove("TariffStatus");

        Session.Remove("OrderYear"); //  Order sessions
        Session.Remove("OrderLng");
        Session.Remove("OrderType");
        Session.Remove("OrderStatus");

        Session.Remove("AwardYear"); //  Award sessions
        Session.Remove("AwardLng");
        Session.Remove("AwardStatus");



        Session.Remove("PetAppealLng"); //  Appeal petition sessions
        Session.Remove("PetAppealYear");
        Session.Remove("PetAppealStatus");

        Session.Remove("PetRvYear");    // review  petition sessions
        Session.Remove("PetRvLng");
        Session.Remove("PetRvStatus");

        Session.Remove("PetLng"); //  Petition sessions
        Session.Remove("PetYear");
        Session.Remove("PetStatus");


        Session.Remove("Sohdeptt"); // SOH sessions
        Session.Remove("SohLng");
        Session.Remove("SohYear");
        Session.Remove("SohStatus");

        Session.Remove("SohPetitionAppeal");
        Session.Remove("SohAppeal");
        Session.Remove("appealYear1");

        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": View  EO Appeal against Award";
        this.Page.Title = " Appeal against Award: HERC";

        if (!IsPostBack)
        {
            DataSet dsprv = new DataSet();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_permissionBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }

            ViewState["sortOrder"] = "";
            btnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            if (Session["AppealAwardyear"] != null)
            {
                bindOrdersYearinDdl();
                ddlYear.SelectedValue = Session["AppealAwardyear"].ToString();
            }
            else
            {
                bindOrdersYearinDdl();
            }
            if (Session["AppealAwardLng"] != null)
            {
                bindropDownlistLang();
                ddlLanguage.SelectedValue = Session["AppealAwardLng"].ToString();
            }
            else
            {
                bindropDownlistLang();
            }
            if (Session["AppealAwardStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["AppealAwardStatus"].ToString();
                Display("", "");
            }
            else
            {
                binddropDownlistStatus();
            }

        }
    }
    #endregion

    public void Display(string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;
        appealobject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
        appealobject.year = ddlYear.SelectedValue;
        p_var.dSet = appealBL.getTempAppealAwardRecords(appealobject, out p_var.k);
        if (p_var.dSet.Tables[0].Rows.Count > 0)
        {
            grdAppeal.Visible = true;

            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                grdAppeal.Columns[9].HeaderText = "Purge";
            }
            else
            {
                grdAppeal.Columns[9].HeaderText = "Delete";
            }
            //Codes for the sorting of records

            DataView myDataView = new DataView();
            myDataView = p_var.dSet.Tables[0].DefaultView;

            if (sortExp != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            }

            //End

            grdAppeal.DataSource = myDataView;
            grdAppeal.DataBind();

            p_var.dSet = null;

            Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
            Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            p_var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);
            if (p_var.dSet.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                {
                    grdAppeal.Columns[0].Visible = false;


                    foreach (GridViewRow row in grdAppeal.Rows)
                    {
                        Image imgedit = (Image)row.FindControl("imgEdit");
                        Image imgnotedit = (Image)row.FindControl("imgnotedit");
                        Label lblRTI_ID = (Label)row.FindControl("lblAppeal_ID");

                        appealobject.AppealId = Convert.ToInt32(lblRTI_ID.Text);

                        p_var.dSetCompare = appealBL.get_ID_For_CompareAward(appealobject);
                        for (p_var.i = 0; p_var.i < p_var.dSetCompare.Tables[0].Rows.Count; p_var.i++)
                        {
                            if (p_var.dSetCompare.Tables[0].Rows[p_var.i]["ID"] != DBNull.Value)
                            {
                                if (Convert.ToInt32(lblRTI_ID.Text) == Convert.ToInt32(p_var.dSetCompare.Tables[0].Rows[p_var.i]["ID"]))
                                {
                                    imgnotedit.Visible = true;
                                    imgedit.Visible = false;

                                }
                                else
                                {
                                    imgnotedit.Visible = false;
                                    imgedit.Visible = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    grdAppeal.Columns[0].Visible = true;

                }
                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                {
                    btnForReview.Visible = true;
                }
                else
                {
                    btnForReview.Visible = false;
                }

                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                {
                    btnForApprove.Visible = true;
                    btnDisApprove.Visible = true;
                }
                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                {
                    btnApprove.Visible = true;
                    btnDisApprove.Visible = true;
                }
                else
                {
                    btnApprove.Visible = false;
                    btnForApprove.Visible = false;
                    btnDisApprove.Visible = false;
                }

                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                {
                    btnApprove.Visible = true;
                    btnDisApprove.Visible = true;
                    btnForApprove.Visible = false;
                }
                else
                {

                    btnApprove.Visible = false;

                }

                if (Convert.ToBoolean(p_var.dSet.Tables[0].Rows[0][6]) == true)
                {
                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                    {
                        grdAppeal.Columns[8].Visible = false;//This is for Edit
                        grdAppeal.Columns[10].Visible = true;//This is for restore
                        grdAppeal.Columns[11].Visible = false;
                    }
                    else
                    {
                        grdAppeal.Columns[8].Visible = true; //This is for Edit
                        grdAppeal.Columns[10].Visible = false; //This is for restore
                    }


                }
                else
                {
                    grdAppeal.Columns[8].Visible = false;
                }
                if (Convert.ToBoolean(p_var.dSet.Tables[0].Rows[0][7]) == true)
                {
                    // modify on date 21 Sep 2013 by ruchi

                    if (Convert.ToBoolean(p_var.dSet.Tables[0].Rows[0][3]) == true)
                    {
                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                        {
                            grdAppeal.Columns[9].Visible = true;
                            grdAppeal.Columns[11].Visible = false;
                        }
                        else
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                            {
                                grdAppeal.Columns[9].Visible = true;
                                grdAppeal.Columns[11].Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                {
                                    grdAppeal.Columns[9].Visible = true;
                                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                                    {
                                        grdAppeal.Columns[11].Visible = true;
                                    }
                                    else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                    {
                                        grdAppeal.Columns[11].Visible = true;
                                    }
                                    else
                                    {
                                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                        {
                                            if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                            {
                                                grdAppeal.Columns[11].Visible = true;
                                            }
                                            else
                                            {
                                                grdAppeal.Columns[11].Visible = false;
                                            }
                                        }
                                        else
                                        {
                                            grdAppeal.Columns[11].Visible = false;
                                        }
                                    }
                                }
                                else
                                {
                                    grdAppeal.Columns[9].Visible = false;
                                    grdAppeal.Columns[11].Visible = false;
                                }
                            }
                        }
                    }
                    if (Convert.ToBoolean(p_var.dSet.Tables[0].Rows[0][4]) == true)
                    {
                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                        {
                            grdAppeal.Columns[9].Visible = true;
                            grdAppeal.Columns[11].Visible = false;
                        }

                    }
                    if (Convert.ToBoolean(p_var.dSet.Tables[0].Rows[0][5]) == true)
                    {
                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                        {
                            grdAppeal.Columns[9].Visible = true;
                        }

                    }

                    //End  
                    //// grdAppeal.Columns[9].Visible = true;  // Commented on date 30 sep 2013
                }
                else
                {
                    grdAppeal.Columns[9].Visible = false;
                }
            }
            p_var.dSet = null;
            lblmsg.Visible = false;
        }
        else
        {
            grdAppeal.Visible = false;
            btnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnApprove.Visible = false;
            btnDisApprove.Visible = false;

            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = "No record found.";
        }


        p_var.Result = p_var.k;

        Session["StatusId"] = ddlStatus.SelectedValue.ToString();
        Session["Lanuage"] = ddlLanguage.SelectedValue;
    }



    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlLanguage.SelectedValue != "0")
        {
            binddropDownlistStatus();
            grdAppeal.Visible = false;
            Session["AppealAwardLng"] = ddlLanguage.SelectedValue;
        }

        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlStatus.SelectedValue == "0")
        {
            grdAppeal.Visible = false;
            btnForReview.Visible = false;
            btnApprove.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            lblmsg.Visible = false;
        }
        else
        {
            Display("", "");
            Session["AppealAwardStatus"] = ddlStatus.SelectedValue;
        }

        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    protected void grdAppeal_Sorting(object sender, GridViewSortEventArgs e)
    {

        Display(e.SortExpression, sortOrder);

    }

    //New codes for sorting

    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }

    //End



    #region Function to bind status in dropDownlist according to permission

    public void binddropDownlistStatus()
    {
        Miscelleneous_BL miscDdlStatus = new Miscelleneous_BL();
        Miscelleneous_BL miscdlStatus = new Miscelleneous_BL();

        obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
        obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
        p_var.dSet = miscDdlStatus.getLanguagePermission(obj_userOB);
        if (p_var.dSet.Tables[0].Rows.Count > 0)
        {
            UserOB usrObject = new UserOB();
            if (Convert.ToBoolean(p_var.dSet.Tables[0].Rows[0][3]) == true)
            {
                p_var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft)).Append(",");
                // btnAddAppeal.Visible = true;

                //code written on date 23sep 2013
                p_var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end
            }
            else
            {
                // btnAddAppeal.Visible = false;
            }
            if (Convert.ToBoolean(p_var.dSet.Tables[0].Rows[0][4]) == true)
            {
                p_var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)).Append(",");

            }
            if (Convert.ToBoolean(p_var.dSet.Tables[0].Rows[0][5]) == true)
            {
                p_var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");
                p_var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover)).Append(",");
                p_var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete));
            }

            usrObject.ModulestatusID = p_var.sbuilder.ToString();
            p_var.sbuilder.Remove(0, p_var.sbuilder.Length);
            p_var.dSet = null;
            p_var.dSet = miscdlStatus.getStatusPermissionwise(usrObject);
            ddlStatus.DataSource = p_var.dSet;
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "Status_Id";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Select Status", "0"));

            //BtnForReview.Visible = false;
        }

        p_var.dSet = null;
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
            p_var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);
            if (p_var.dSet.Tables[0].Rows.Count > 0)
            {
                UserOB usrObject = new UserOB();
                if (Convert.ToBoolean(p_var.dSet.Tables[0].Rows[0][9]) == true && Convert.ToBoolean(p_var.dSet.Tables[0].Rows[0][10]) == true)
                {
                    usrObject.english = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.English);
                    usrObject.hindi = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_var.sbuilder.Append(usrObject.english).Append(",");
                    p_var.sbuilder.Append(usrObject.hindi);
                }
                else if (Convert.ToBoolean(p_var.dSet.Tables[0].Rows[0][9]) == true)
                {
                    usrObject.hindi = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_var.sbuilder.Append(usrObject.hindi);
                }
                else if (Convert.ToBoolean(p_var.dSet.Tables[0].Rows[0][10]) == true)
                {
                    usrObject.english = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.English);

                    p_var.sbuilder.Append(usrObject.english);
                }
                usrObject.LangId = p_var.sbuilder.ToString().Trim();
                p_var.sbuilder.Remove(0, p_var.sbuilder.Length);
                p_var.dSet = null;
                p_var.dSet = miscdlLanguage.getLanguage(usrObject);
                PLanguage.Visible = true;
                ddlLanguage.DataSource = p_var.dSet;
                ddlLanguage.DataTextField = "Language";
                ddlLanguage.DataValueField = "Lang_Id";
                ddlLanguage.DataBind();

            }
            p_var.dSet = null;


        }
        catch
        {

        }
    }

    #endregion

    #region button add appeal click event

    protected void btnAddAppeal_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/Appeal/AddEditAppealAward.aspx?ModuleID=4"));
    }

    #endregion

    #region button for review click event

    protected void btnForReview_Click(object sender, EventArgs e)
    {

        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;

    }

    #endregion

    #region button for Approve click event

    protected void btnForApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        pnlGrid.Visible = false;
        bindCheckBoxListWithApproverEmailIDs();

    }

    #endregion

    #region button Approve click event
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;
        //ChkApprove();

    }

    #endregion


    #region button for DisApprove click event
    protected void btnDisApprove_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
        {
            pnlPopUpEmailsDis.Visible = true;
            pnlGrid.Visible = false;
            bindCheckBoxListWithEmailIDDiaapproves();
            //this.mpeSendEmailDis.Show();
        }
        else
        {
            pnlPopUpEmailsDis.Visible = true;
            pnlGrid.Visible = false;
            bindCheckBoxListWithDataEntryEmailIDs();
            // this.mpeSendEmailDis.Show();
        }


    }
    #endregion


    #region Function to approve appeal records

    public void ChkApprove()
    {
        // LinkOB obj_linkOB1 = new LinkOB();
        //foreach (GridViewRow row in grdAppeal.Rows)
        //{
        //    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
        //    if ((selCheck.Checked == true))
        //    {
        //        Label lblid = (Label)row.FindControl("lblAppeal_ID");
        //        p_var.dataKeyID = Convert.ToInt32(lblid.Text);
        //        appealobject.TempAppealId = p_var.dataKeyID;
        //        p_var.Result = appealBL.InsertAppealAwardIntoWeb(appealobject);
        //    }
        //}
        //if (p_var.Result > 0)
        //{
        //    Session["msg"] = "Appeal against Award has been published successfully";
        //    Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
        //    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        //}
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Display("", "");
    }

    #endregion


    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        //this.Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt16(ddlDepartment.SelectedValue), pageIndex);
        Display("", "");
    }

    #endregion

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        grdAppeal.Visible = false;
        Session["AppealAwardyear"] = ddlYear.SelectedValue;
    }

    #region Function to bind Orders Year

    public void bindOrdersYearinDdl()
    {

        p_var.dSet = appealBL.GetYearAward();
        if (p_var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = p_var.dSet;
            ddlYear.DataTextField = "Year";
            ddlYear.DataValueField = "Year";
            ddlYear.DataBind();
        }
        else
        {
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    #endregion

    #region gridView grdAppeal rowCreated event to select checkboxes
    protected void grdAppeal_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == DataControlRowState.Normal | e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
            CheckBox chkBxHeader = (CheckBox)this.grdAppeal.HeaderRow.FindControl("chkSelectAll");
            //Add client side function childclick on check boxes
            chkBxSelect.Attributes.Add("onclick", string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID));


        }
    }
    #endregion

    protected void btnAppealAward_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=4"));
    }
    protected void grdAppeal_RowCommand(object sender, GridViewCommandEventArgs e)
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
                if (e.CommandName == "Delete")
                {
                    p_var.commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });
                    p_var.rp_id = Convert.ToInt32(p_var.commandArgs[0]);
                    p_var.status_id = Convert.ToInt32(p_var.commandArgs[1]);

                    appealobject.ConnectionID = p_var.rp_id;
                    appealobject.StatusId = p_var.status_id;

                    p_var.Result = appealBL.Delete_AppealAward(appealobject);
                    if (p_var.Result > 0)
                    {
                        if (ddlStatus.SelectedValue == "8")
                        {
                            Session["msg"] = "Appeal against Award has been deleted (purged) permanently.";
                        }
                        else
                        {
                            Session["msg"] = "Appeal against Award has been deleted successfully.";
                        }

                        GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                        obj_audit.ActionType = "D";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        Label lblAppalNumber = row.FindControl("lblAppalNumber") as Label;
                        //if (lblAppalNumber == null) { return; }
                        obj_audit.Title = lblAppalNumber.Text + " of " + ddlYear.SelectedItem.ToString() + " (Appeal Against Award)";
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        // Session["msg"] = "Appeal has been deleted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                    else
                    {
                        Session["msg"] = "Appeal against Award has not been deleted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }

                }


                if (e.CommandName == "Restore")
                {

                    appealobject.ConnectionID = Convert.ToInt32(e.CommandArgument);
                    p_var.Result = appealBL.AppealAwardPronouncedWeb_Restore(appealobject);
                    if (p_var.Result > 0)
                    {

                        GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                        obj_audit.ActionType = "R";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        Label lblAppalNumber = row.FindControl("lblAppalNumber") as Label;
                        //if (lblAppalNumber == null) { return; }
                        obj_audit.Title = lblAppalNumber.Text + " of " + ddlYear.SelectedItem.ToString() + " (Appeal Against Award)";
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "Appeal against Award has been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                    else
                    {
                        Session["msg"] = "Appeal against Award has not been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                }

                else if (e.CommandName == "Audit")
                {

                    DataSet dSetAuditTrail = new DataSet();
                    petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
                    petObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal);
                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    petObject.ModuleType = 1;
                    dSetAuditTrail = petPetitionBL.AuditTrailRecords(petObject);
                    Label lblprono = row.FindControl("lblAppealNo") as Label;
                    if (dSetAuditTrail.Tables[0].Rows.Count > 0)
                    {
                        ltrlPetitionNo.Text = "<strong>" + lblprono.Text + "</strong>";
                        if (dSetAuditTrail.Tables[0].Rows[0]["createdBy"] != DBNull.Value)
                        {
                            ltrlCreation.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["createdBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["CreatedDateTime"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["CreatedIPAddress"].ToString();
                        }
                        else
                        {
                            ltrlCreation.Text = "";
                        }
                        if (dSetAuditTrail.Tables[0].Rows[0]["editedBy"] != DBNull.Value)
                        {
                            ltrlLastEdited.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["editedBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["EditedDateTime"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["UpdatedIPAddress"].ToString();
                        }
                        else
                        {
                            ltrlLastEdited.Text = "";
                        }
                        if (dSetAuditTrail.Tables[0].Rows[0]["reviewedBy"] != DBNull.Value)
                        {
                            ltrlLastReviewed.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["reviewedBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["ReviewedDate"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["ReviewedIPAddress"].ToString();
                        }
                        else
                        {
                            ltrlLastReviewed.Text = "";
                        }
                        if (dSetAuditTrail.Tables[0].Rows[0]["publishedBy"] != DBNull.Value)
                        {
                            ltrlLastPublished.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["publishedBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["PublishedDateTime"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["PublishedIPAddress"].ToString();
                        }
                        else
                        {
                            ltrlLastPublished.Text = "";
                        }
                    }
                    else
                    {
                        ltrlCreation.Text = "";
                        ltrlLastEdited.Text = "";
                        ltrlLastPublished.Text = "";
                        ltrlLastReviewed.Text = "";
                        ltrlPetitionNo.Text = "";
                    }
                    this.mdpAuditTrail.Show();
                }
            }
        }
    }
    protected void grdAppeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //This is for delete/permanently delete 19 june 2013 
            ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");

            ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
            string number = DataBinder.Eval(e.Row.DataItem, "Appeal_Number1").ToString();
            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
            {

                if (number != null && number != "")
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to purge Appeal against Award No- " + DataBinder.Eval(e.Row.DataItem, "Appeal_Number1") + "')");
                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
                   "confirm('Are you sure want to restore Appeal against Award No- " + DataBinder.Eval(e.Row.DataItem, "Appeal_Number1") + "')");
                }
                else
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                   "confirm('Are you sure you want to purge Appeal against Award No-" + DataBinder.Eval(e.Row.DataItem, "Appeal_Number1") + "')");
                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
                   "confirm('Are you sure want to restore Appeal against Award No- " + DataBinder.Eval(e.Row.DataItem, "Appeal_Number1") + "')");
                }

            }
            else
            {
                if (number != null && number != "")
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to delete Appeal against Award No- " + DataBinder.Eval(e.Row.DataItem, "Appeal_Number1") + "')");
                }
                else
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                   "confirm('Are you sure you want to delete Appeal against Award No- " + DataBinder.Eval(e.Row.DataItem, "Appeal_Number1") + "')");

                }

            }

            //END
        }
    }


    public void BindGridDetails()
    {
        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdAppealPdf.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {

            grdAppealPdf.Visible = true;
            appealobject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            appealobject.year = ddlYear.SelectedValue;
            p_var.dSet = appealBL.getTempAppealAwardRecords(appealobject, out p_var.k);

            if (p_var.dSet.Tables[0].Rows.Count > 0)
            {
                grdAppealPdf.DataSource = p_var.dSet;
                grdAppealPdf.DataBind();
                p_var.dSet = null;
            }
        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void btnPdf_Click(object sender, EventArgs e)
    {
        BindGridDetails();
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "AppealAward_" + System.DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdAppealPdf.AllowPaging = false;
        grdAppealPdf.DataBind();
        grdAppealPdf.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();

    }

    protected void grdAppealPdf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal ltrlConnectedFile1 = (Literal)e.Row.FindControl("ltrlConnectedFile1");
            appealobject.AppealId = Convert.ToInt32(grdAppealPdf.DataKeys[e.Row.RowIndex].Value.ToString());
            p_var.sbuilder.Remove(0, p_var.sbuilder.Length);
            p_var.dsFileName = appealBL.getFileNameForAppealAwardProunced(appealobject);
            if (p_var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    if (p_var.dsFileName.Tables[0].Rows[i]["Comments"] != null && p_var.dsFileName.Tables[0].Rows[i]["Comments"] != "")
                    {
                        p_var.sbuilder.Append("<asp:label >" + p_var.dsFileName.Tables[0].Rows[i]["FileName"] + "," + p_var.dsFileName.Tables[0].Rows[i]["Comments"] + "</Label>");
                    }
                    else
                    {
                        p_var.sbuilder.Append("<asp:label >" + p_var.dsFileName.Tables[0].Rows[i]["FileName"] + "</Label>");
                    }

                    p_var.sbuilder.Append("<br/><hr/>");

                }
                ltrlConnectedFile1.Text = p_var.sbuilder.ToString();


            }
            else
            {

            }
        }
    }
    protected void btnSendEmails_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sbuilder = new StringBuilder();
            StringBuilder sbuilderSms = new StringBuilder();
            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
            {
                sbuilder.Remove(0, p_var.sbuildertmp.Length);
                foreach (GridViewRow row in grdAppeal.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblAppealNo = (Label)row.FindControl("lblAppealNo");
                    ViewState["AppealAwardNumber"] = lblAppealNo.Text;
                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append("<b>Appeal against Award - " + lblAppealNo.Text + "<br/></b>");
                        Label lblid = (Label)row.FindControl("lblAppeal_ID");
                        p_var.dataKeyID = Convert.ToInt32(lblid.Text);
                        appealobject.TempAppealId = p_var.dataKeyID;
                        appealobject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        appealobject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_var.Result = appealBL.updateAppealAwardStatus(appealobject);
                    }
                }
                if (p_var.Result > 0)
                {
                    p_var.sbuildertmp.Remove(0, p_var.sbuildertmp.Length);
                    foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                    {

                        if (li.Selected == true)
                        {
                            // p_var.sbuildertmp.Append(li.Value + ";");
                            int statindex = li.Text.IndexOf("(") + 1;
                            p_var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                            sbuilderSms.Append(li.Value + ";");
                        }

                    }
                    if (p_var.sbuildertmp.ToString().EndsWith(";"))
                    {
                        p_var.sbuilder.Append("Record pending for Review : " + sbuilder.ToString() + "<br/>");
                        p_var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                        p_var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                        //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                        string email = p_var.sbuildertmp.ToString().Substring(0, p_var.sbuildertmp.ToString().Length - 1);
                        p_var.sbuildertmp.Remove(0, p_var.sbuildertmp.Length);
                        p_var.sbuildertmp.Append(email);
                        miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_var.sbuildertmp.ToString().Trim(), "E/O - Record pending for Review", "no-reply.herc@nic.in", p_var.sbuilder.ToString());

                    }
                    ///* Code to send sms */
                    char[] splitter = { ';' };
                    PetitionOB petObjectNew = new PetitionOB();
                    DataSet dsSms = new DataSet();
                    string textmessage;
                    string strUrl = sbuilderSms.ToString();
                    string[] split = strUrl.Split(';');
                    ArrayList list = new ArrayList();
                    foreach (string item in split)
                    {
                        list.Add(item.Trim());
                    }


                    string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "");
                    string[] stringSeparators = new string[] { "<br/>" };
                    string[] splitPublicNotice = strpublicnotice.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    ArrayList listPublicNotice = new ArrayList();
                    foreach (string itemPublicNotice in splitPublicNotice)
                    {
                        listPublicNotice.Add(itemPublicNotice.Trim());
                    }

                    foreach (string strPublicNotice in listPublicNotice)
                    {
                        if (strPublicNotice != string.Empty && strPublicNotice != "")
                        {
                            //loop through cells in that row
                            foreach (string str in list)
                            {
                                if (str != string.Empty)
                                {

                                    //string message = ViewState["Title"].ToString();
                                    string message = strPublicNotice;

                                    if (message.Length > 150)
                                    {
                                        message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                    }
                                    else
                                    {
                                        message = strPublicNotice.ToString();
                                    }
                                    textmessage = "EO - Record pending for review - ";

                                    miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                }

                            }

                        }
                    }


                    /* End */
                    Session["msg"] = "Appeal against Award has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }

            else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {
                foreach (GridViewRow row in grdAppeal.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblAppealNo = (Label)row.FindControl("lblAppealNo");
                    ViewState["AppealAwardNumber"] = lblAppealNo.Text;
                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append("<b>Appeal against Award - " + lblAppealNo.Text + "<br/></b>");
                        Label lblid = (Label)row.FindControl("lblAppeal_ID");
                        p_var.dataKeyID = Convert.ToInt32(lblid.Text);
                        appealobject.TempAppealId = p_var.dataKeyID;
                        appealobject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        appealobject.IpAddress = Miscelleneous_DL.getclientIP();
                        appealobject.userID = Convert.ToInt16(Session["User_Id"]);
                        p_var.Result = appealBL.updateAppealAwardStatus(appealobject);
                    }
                }
                if (p_var.Result > 0)
                {
                    p_var.sbuildertmp.Remove(0, p_var.sbuildertmp.Length);
                    foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                    {

                        if (li.Selected == true)
                        {
                            //p_var.sbuildertmp.Append(li.Value + ";");
                            int statindex = li.Text.IndexOf("(") + 1;
                            p_var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                            sbuilderSms.Append(li.Value + ";");
                        }

                    }
                    if (p_var.sbuildertmp.ToString().EndsWith(";"))
                    {
                        p_var.sbuilder.Append("Record pending for Publish : " + sbuilder.ToString() + "<br/>");
                        p_var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                        p_var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                        //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                        string email = p_var.sbuildertmp.ToString().Substring(0, p_var.sbuildertmp.ToString().Length - 1);
                        p_var.sbuildertmp.Remove(0, p_var.sbuildertmp.Length);
                        p_var.sbuildertmp.Append(email);
                        miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_var.sbuildertmp.ToString().Trim(), "E/O - Record pending for Publish", "no-reply.herc@nic.in", p_var.sbuilder.ToString());

                    }
                    ///* Code to send sms */
                    char[] splitter = { ';' };
                    PetitionOB petObjectNew = new PetitionOB();
                    DataSet dsSms = new DataSet();
                    string textmessage;
                    string strUrl = sbuilderSms.ToString();
                    string[] split = strUrl.Split(';');
                    ArrayList list = new ArrayList();
                    foreach (string item in split)
                    {
                        list.Add(item.Trim());
                    }


                    string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "");
                    string[] stringSeparators = new string[] { "<br/>" };
                    string[] splitPublicNotice = strpublicnotice.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    ArrayList listPublicNotice = new ArrayList();
                    foreach (string itemPublicNotice in splitPublicNotice)
                    {
                        listPublicNotice.Add(itemPublicNotice.Trim());
                    }

                    foreach (string strPublicNotice in listPublicNotice)
                    {
                        if (strPublicNotice != string.Empty && strPublicNotice != "")
                        {
                            //loop through cells in that row

                            foreach (string str in list)
                            {
                                if (str != string.Empty)
                                {

                                    //string message = ViewState["Title"].ToString();
                                    string message = strPublicNotice;

                                    if (message.Length > 150)
                                    {
                                        message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                    }
                                    else
                                    {
                                        message = strPublicNotice.ToString();
                                    }
                                    textmessage = "EO - Record pending for publish -";

                                    miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                }

                            }

                        }
                    }


                    /* End */
                    Session["msg"] = "Appeal against Award has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else
            {
                foreach (GridViewRow row in grdAppeal.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblAppealNo = (Label)row.FindControl("lblAppealNo");
                    ViewState["AppealAwardNumber"] = lblAppealNo.Text;
                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append("<b>Appeal against Award - " + lblAppealNo.Text + " <br/></b>");
                        Label lblid = (Label)row.FindControl("lblAppeal_ID");
                        p_var.dataKeyID = Convert.ToInt32(lblid.Text);
                        appealobject.TempAppealId = p_var.dataKeyID;
                        appealobject.IpAddress = Miscelleneous_DL.getclientIP();
                        appealobject.userID = Convert.ToInt16(Session["User_Id"]);
                        p_var.Result = appealBL.InsertAppealAwardIntoWeb(appealobject);
                    }
                }
                if (p_var.Result > 0)
                {
                    p_var.sbuildertmp.Remove(0, p_var.sbuildertmp.Length);
                    foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                    {

                        if (li.Selected == true)
                        {
                            // p_var.sbuildertmp.Append(li.Value + ";");
                            int statindex = li.Text.IndexOf("(") + 1;
                            p_var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                            sbuilderSms.Append(li.Value + ";");
                        }

                    }
                    if (p_var.sbuildertmp.ToString().EndsWith(";"))
                    {
                        p_var.sbuilder.Append("Record published : " + sbuilder.ToString() + "<br/>");
                        p_var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                        p_var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                        //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                        string email = p_var.sbuildertmp.ToString().Substring(0, p_var.sbuildertmp.ToString().Length - 1);
                        p_var.sbuildertmp.Remove(0, p_var.sbuildertmp.Length);
                        p_var.sbuildertmp.Append(email);
                        miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_var.sbuildertmp.ToString().Trim(), "E/O - Record published", "no-reply.herc@nic.in", p_var.sbuilder.ToString());

                    }
                    ///* Code to send sms */
                    char[] splitter = { ';' };
                    PetitionOB petObjectNew = new PetitionOB();
                    DataSet dsSms = new DataSet();
                    string textmessage;
                    string strUrl = sbuilderSms.ToString();
                    string[] split = strUrl.Split(';');
                    ArrayList list = new ArrayList();
                    foreach (string item in split)
                    {
                        list.Add(item.Trim());
                    }


                    string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "");
                    string[] stringSeparators = new string[] { "<br/>" };
                    string[] splitPublicNotice = strpublicnotice.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    ArrayList listPublicNotice = new ArrayList();
                    foreach (string itemPublicNotice in splitPublicNotice)
                    {
                        listPublicNotice.Add(itemPublicNotice.Trim());
                    }

                    foreach (string strPublicNotice in listPublicNotice)
                    {
                        if (strPublicNotice != string.Empty && strPublicNotice != "")
                        {
                            //loop through cells in that row
                            foreach (string str in list)
                            {
                                if (str != string.Empty)
                                {

                                    //string message = ViewState["Title"].ToString();
                                    string message = strPublicNotice;

                                    if (message.Length > 150)
                                    {
                                        message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                    }
                                    else
                                    {
                                        message = strPublicNotice.ToString();
                                    }
                                    textmessage = "EO - Record published - ";

                                    miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                }

                            }

                        }
                    }


                    /* End */

                    Session["msg"] = "Appeal against Award has been published successfully";
                    Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnSendEmailsWithoutEmails_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
            {
                foreach (GridViewRow row in grdAppeal.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblAppeal_ID");
                        p_var.dataKeyID = Convert.ToInt32(lblid.Text);
                        appealobject.TempAppealId = p_var.dataKeyID;
                        appealobject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        appealobject.IpAddress = Miscelleneous_DL.getclientIP();
                        appealobject.userID = Convert.ToInt16(Session["User_Id"]);
                        p_var.Result = appealBL.updateAppealAwardStatus(appealobject);
                    }
                }
                if (p_var.Result > 0)
                {
                    Session["msg"] = "Appeal against Award has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }

            else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {
                foreach (GridViewRow row in grdAppeal.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblAppeal_ID");
                        p_var.dataKeyID = Convert.ToInt32(lblid.Text);
                        appealobject.TempAppealId = p_var.dataKeyID;
                        appealobject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        appealobject.IpAddress = Miscelleneous_DL.getclientIP();
                        appealobject.userID = Convert.ToInt16(Session["User_Id"]);
                        p_var.Result = appealBL.updateAppealAwardStatus(appealobject);
                    }
                }
                if (p_var.Result > 0)
                {
                    Session["msg"] = "Appeal against Award has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else
            {
                foreach (GridViewRow row in grdAppeal.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblAppeal_ID");
                        p_var.dataKeyID = Convert.ToInt32(lblid.Text);
                        appealobject.TempAppealId = p_var.dataKeyID;
                        appealobject.IpAddress = Miscelleneous_DL.getclientIP();
                        appealobject.userID = Convert.ToInt16(Session["User_Id"]);
                        p_var.Result = appealBL.InsertAppealAwardIntoWeb(appealobject);
                    }
                }
                if (p_var.Result > 0)
                {
                    Session["msg"] = "Appeal against Award has been published successfully";
                    Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnCancelEmail_Click(object sender, EventArgs e)
    {
        pnlGrid.Visible = true;
        pnlPopUpEmails.Visible = false;
    }
    protected void btnSendEmailsDis_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                StringBuilder sbuilder = new StringBuilder();
                StringBuilder sbuilderSms = new StringBuilder();
                foreach (GridViewRow row in grdAppeal.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblAppealNo = (Label)row.FindControl("lblAppealNo");
                    ViewState["DisAppealAwardNumber"] = lblAppealNo.Text;
                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append("<b>Appeal against Award - " + lblAppealNo.Text + "<br/></b>");
                        Label lblid = (Label)row.FindControl("lblAppeal_ID");
                        p_var.dataKeyID = Convert.ToInt32(lblid.Text);
                        appealobject.TempAppealId = p_var.dataKeyID;

                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                        {
                            appealobject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                        }
                        else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                        {
                            appealobject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        }
                        else
                        {
                            appealobject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        }
                        //appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                        p_var.Result = appealBL.updateAppealAwardStatus(appealobject);
                    }
                }
                if (p_var.Result > 0)
                {
                    p_var.sbuildertmp.Remove(0, p_var.sbuildertmp.Length);
                    foreach (System.Web.UI.WebControls.ListItem li in chkSendEmailsDis.Items)
                    {

                        if (li.Selected == true)
                        {
                            //p_var.sbuildertmp.Append(li.Value + ";");
                            int statindex = li.Text.IndexOf("(") + 1;
                            p_var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                            sbuilderSms.Append(li.Value + ";");
                        }

                    }
                    if (p_var.sbuildertmp.ToString().EndsWith(";"))
                    {
                        p_var.sbuilder.Append("Record disapproved: " + sbuilder.ToString() + "<br/>");
                        p_var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                        p_var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                        //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                        string email = p_var.sbuildertmp.ToString().Substring(0, p_var.sbuildertmp.ToString().Length - 1);
                        p_var.sbuildertmp.Remove(0, p_var.sbuildertmp.Length);
                        p_var.sbuildertmp.Append(email);
                        miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_var.sbuildertmp.ToString().Trim(), "E/O - Record disapproved", "no-reply.herc@nic.in", p_var.sbuilder.ToString());

                    }
                    ///* Code to send sms */
                    char[] splitter = { ';' };
                    PetitionOB petObjectNew = new PetitionOB();
                    DataSet dsSms = new DataSet();
                    string textmessage;
                    string strUrl = sbuilderSms.ToString();
                    string[] split = strUrl.Split(';');
                    ArrayList list = new ArrayList();
                    foreach (string item in split)
                    {
                        list.Add(item.Trim());
                    }


                    string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "");
                    string[] stringSeparators = new string[] { "<br/>" };
                    string[] splitPublicNotice = strpublicnotice.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    ArrayList listPublicNotice = new ArrayList();
                    foreach (string itemPublicNotice in splitPublicNotice)
                    {
                        listPublicNotice.Add(itemPublicNotice.Trim());
                    }

                    foreach (string strPublicNotice in listPublicNotice)
                    {
                        if (strPublicNotice != string.Empty && strPublicNotice != "")
                        {
                            //loop through cells in that row
                            foreach (string str in list)
                            {
                                if (str != string.Empty)
                                {

                                    //string message = ViewState["Title"].ToString();
                                    string message = strPublicNotice;

                                    if (message.Length > 150)
                                    {
                                        message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                    }
                                    else
                                    {
                                        message = strPublicNotice.ToString();
                                    }
                                    textmessage = "EO - Record disapproved -";

                                    miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                }

                            }

                        }
                    }


                    /* End */
                    Session["msg"] = "Appeal against Award has been disapproved successfully";
                    Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnSendEmailsWithoutEmailsDis_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grdAppeal.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                    Label lblid = (Label)row.FindControl("lblAppeal_ID");
                    p_var.dataKeyID = Convert.ToInt32(lblid.Text);
                    appealobject.TempAppealId = p_var.dataKeyID;

                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        appealobject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        appealobject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        appealobject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    //appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    p_var.Result = appealBL.updateAppealAwardStatus(appealobject);
                }
            }
            if (p_var.Result > 0)
            {
                Session["msg"] = "Appeal against Award has been disapproved successfully";
                Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4";
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnCancelEmailDis_Click(object sender, EventArgs e)
    {
        pnlGrid.Visible = true;
        pnlPopUpEmailsDis.Visible = false;
    }

    #region Function to bind emailid of reviewers status in checkboxlist

    public void bindCheckBoxListWithEmailIDs()
    {

        try
        {
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal);
            p_var.dSetCompare = obj_UserBL.getReviewEmailIds(obj_userOB);
            lblSelectors.Text = "Select the Reviewer(s)";
            lblSelectors.Font.Bold = true;
            chkSendEmails.DataSource = p_var.dSetCompare;
            chkSendEmails.DataTextField = "newEmail";
            chkSendEmails.DataValueField = "Mobile_No";
            chkSendEmails.DataBind();

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind emailid of reviewers status in checkboxlist

    public void bindCheckBoxListWithApproverEmailIDs()
    {

        try
        {
            lblSelectors.Text = "Select the Publisher(s)";
            lblSelectors.Font.Bold = true;
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal);
            p_var.dSetCompare = obj_UserBL.getPublisherEmailIds(obj_userOB);

            chkSendEmails.DataSource = p_var.dSetCompare;
            chkSendEmails.DataTextField = "newEmail";
            chkSendEmails.DataValueField = "Mobile_No";
            chkSendEmails.DataBind();

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind emailid of reviewers status in checkboxlist

    public void bindCheckBoxListWithEmailIDDiaapproves()
    {

        try
        {
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal);
            p_var.dSetCompare = obj_UserBL.getReviewEmailIds(obj_userOB);
            lblSelectorsDis.Text = "Select the Reviewer(s)";
            lblSelectorsDis.Font.Bold = true;
            chkSendEmailsDis.DataSource = p_var.dSetCompare;
            chkSendEmailsDis.DataTextField = "newEmail";
            chkSendEmailsDis.DataValueField = "Mobile_No";
            chkSendEmailsDis.DataBind();

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind emailid of Data entry operator status in checkboxlist

    public void bindCheckBoxListWithDataEntryEmailIDs()
    {

        try
        {
            lblSelectorsDis.Text = "Select the Data Entry Personnel";
            lblSelectorsDis.Font.Bold = true;
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal);
            p_var.dSetCompare = obj_UserBL.getDataEntryEmailIds(obj_userOB);

            chkSendEmailsDis.DataSource = p_var.dSetCompare;
            chkSendEmailsDis.DataTextField = "newEmail";
            chkSendEmailsDis.DataValueField = "Mobile_No";
            chkSendEmailsDis.DataBind();

        }
        catch
        {
            throw;
        }
    }

    #endregion
}
