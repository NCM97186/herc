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
using System.Text;

public partial class Auth_AdminPanel_Tariff_View_Taritt : CrsfBase //System.Web.UI.Page
{


    //Area for all the data declaration zone

    #region Data declaration zone

    TariffBL tariffBL = new TariffBL();
    TariffOB tariffOB = new TariffOB();
    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    Project_Variables p_Var = new Project_Variables();
    UserOB obj_userOB = new UserOB();
    UserBL obj_UserBL = new UserBL();
    Miscelleneous_BL obj_miscelBL = new Miscelleneous_BL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    PaginationBL pagingBL = new PaginationBL();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petPetitionBL = new PetitionBL();
    LinkBL obj_linkBL = new LinkBL();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion

    //End


    //Page Load Event zone

    #region Page Load Event zone
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Remove("WhatsNewStatus"); // What's New sessions
        Session.Remove("Lng");  // RTI sessions
        Session.Remove("deptt");
        Session.Remove("Status");
        Session.Remove("year");
        Session.Remove("FAALng");  // FAARTI sessions
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

        Session.Remove("Appealyear"); //Appeal module sessions
        Session.Remove("AppealLng");
        Session.Remove("Appealdeptt");
        Session.Remove("AppealStatus");

        Session.Remove("PStatus");// Public Notice module sessions
        Session.Remove("PYear");
        Session.Remove("PLng");

        Session.Remove("MLng"); //  module sessions
        Session.Remove("Mdeptt");
        Session.Remove("MStatus");

        Session.Remove("ProfileLng"); // Profile module sessions
        Session.Remove("ProfileNvg");
        Session.Remove("ProfileDeptt");
        Session.Remove("profileStatus");

        Session.Remove("menulang"); //  menu sessions
        Session.Remove("menuposition");
        Session.Remove("menulst");
        Session.Remove("menustatus");

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
        lblModulename.Text = ": View  Tariff";
        this.Page.Title = " Tariff: HERC";

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
            //bindropDownlistLang();
            ViewState["sortOrder"] = "";
            //lblPageSize.Visible     = false;
            //ddlPageSize.Visible     = false;   
            BtnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            BtnForReview.Visible = false;

            if (Session["TariffDeptt"] != null)
            {
                if (Convert.ToInt32(Session["TariffDeptt"]) == Convert.ToInt32(Module_ID_Enum.hercType.ombudsman))
                {
                    Pcategory.Visible = false;
                }
                else
                {
                    Pcategory.Visible = true;

                }
                Get_Deptt_Name();    // To get Deptt Name
                ddlDepartment.SelectedValue = Session["TariffDeptt"].ToString();
            }
            else
            {
                Get_Deptt_Name();
            }
            if (Session["TariffCategory"] != null)
            {

                Get_CategoryName(ddlDepartment.SelectedValue.ToString());

                ddlcategory.SelectedValue = Session["TariffCategory"].ToString();
                if (Session["TariffCategory"].ToString() != "8" && Session["TariffCategory"].ToString() != "9")
                {
                    if (ddlcategory.SelectedValue == "10")
                    {
                        Pcategory.Visible = false;
                    }
                    else
                    {
                        Pcategory.Visible = true;
                    }

                }
                else
                {
                    Pcategory.Visible = false;
                }
            }
            else
            {
                Get_CategoryName(ddlDepartment.SelectedValue.ToString());
                //Get_CategoryName();
            }

            if (Session["TariffLng"] != null)
            {
                bindropDownlistLang();
                ddlLanguage.SelectedValue = Session["TariffLng"].ToString();
            }
            else
            {
                bindropDownlistLang();
            }



            if (Session["TariffType"] != null)
            {
                bindTariffType();
                ddlTariffType.SelectedValue = Session["TariffType"].ToString();
            }
            else
            {
                bindTariffType();
            }

            if (Session["TariffStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["TariffStatus"].ToString();
                Bind_Grid("", "");
            }
            else
            {
                binddropDownlistStatus();
            }


        }


    }

    #endregion

    //end

    // Button Event zone

    #region Button btnAddNewPage click event to add new Tariff

    protected void btnAddNewPage_Click(object sender, EventArgs e)
    {
        try
        {

            //lnkObject.PositionId = Convert.ToInt32(Request.QueryString["position"]);
            Response.Redirect("~/Auth/AdminPanel/TariffModule/Add_New_Tariff.aspx?ModuleID=27");
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region button btnDisApprove click event to disapprove menu

    protected void btnDisApprove_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
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

    #region button btnForReview click event to send Tariff for review

    protected void BtnForReview_Click(object sender, EventArgs e)
    {

        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;

    }

    #endregion

    #region button btnForApprove click event to move Tariff for approval

    protected void btnForApprove_Click(object sender, EventArgs e)
    {

        pnlPopUpEmails.Visible = true;
        pnlGrid.Visible = false;
        bindCheckBoxListWithApproverEmailIDs();

    }

    #endregion

    #region button btnApprove click event to approve Tariff

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;
        //ChkApprove_Disapprove();
    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        //this.Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), pageIndex);
    }

    #endregion

    //end


    //User Define function zone

    #region Function To bind the gridView with menu

    public void Bind_Grid(string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;

        grdCMSMenu.Visible = true;
        tariffOB.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
        tariffOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Tariff);
        tariffOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
        if (Convert.ToInt32(ddlDepartment.SelectedValue) == 1)
        {
            if (ddlcategory.SelectedValue == "8" || ddlcategory.SelectedValue == "9")
            {
                tariffOB.CatId = null;
            }
            else
            {
                tariffOB.CatId = Convert.ToInt32(ddlTariffType.SelectedValue);
            }
        }
        else
        {
            tariffOB.CatId = null;
        }

        tariffOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
        tariffOB.CatTypeId = Convert.ToInt32(ddlcategory.SelectedValue);
        ////tariffOB.PageIndex = pageIndex;
        ////tariffOB.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        p_Var.dSet = tariffBL.ASP_Links_DisplayWithPaging(tariffOB, out p_Var.k);


        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {

            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                grdCMSMenu.Columns[7].HeaderText = "Purge";
            }
            else
            {
                grdCMSMenu.Columns[7].HeaderText = "Delete";
            }

            //grdCMSMenu.DataSource = p_Var.dSet;

            //Codes for the sorting of records

            DataView myDataView = new DataView();
            myDataView = p_Var.dSet.Tables[0].DefaultView;

            if (sortExp != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            }

            //End
            grdCMSMenu.DataSource = myDataView;
            grdCMSMenu.DataBind();
            p_Var.dSet = null;

            Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
            Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            p_Var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                {
                    grdCMSMenu.Columns[0].Visible = false;
                }
                else
                {
                    grdCMSMenu.Columns[0].Visible = true;
                }
                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                {
                    BtnForReview.Visible = true;
                }
                else
                {
                    BtnForReview.Visible = false;
                }

                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                {
                    btnForApprove.Visible = true;
                    btnDisApprove.Visible = true;
                }
                else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
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

                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                {
                    btnApprove.Visible = true;
                    btnDisApprove.Visible = true;
                    btnForApprove.Visible = false;
                }
                else
                {

                    btnApprove.Visible = false;
                    //btnDisApprove.Visible = false;
                }

                if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][6]) == true)
                {
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                    {
                        grdCMSMenu.Columns[6].Visible = true; ;//This is for Edit
                        grdCMSMenu.Columns[8].Visible = true;//This is for restore
                        grdCMSMenu.Columns[9].Visible = false;
                    }
                    else
                    {
                        grdCMSMenu.Columns[6].Visible = true; ;
                        grdCMSMenu.Columns[8].Visible = false;
                    }
                }
                else
                {
                    grdCMSMenu.Columns[6].Visible = false;
                }
                if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                {
                    ////grdCMSMenu.Columns[7].Visible = true; // commented on date 23 Sep 2013 by ruchi

                    // modify on date 21 Sep 2013 by ruchi

                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                    {
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                        {
                            grdCMSMenu.Columns[7].Visible = true;
                            grdCMSMenu.Columns[9].Visible = false;
                        }
                        else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                        {
                            if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                            {
                                grdCMSMenu.Columns[9].Visible = true;
                            }
                            else
                            {
                                grdCMSMenu.Columns[9].Visible = false;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                            {
                                grdCMSMenu.Columns[7].Visible = true;
                                grdCMSMenu.Columns[9].Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                                {
                                    grdCMSMenu.Columns[7].Visible = true;
                                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                                    {
                                        grdCMSMenu.Columns[9].Visible = true;
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                        {
                                            grdCMSMenu.Columns[9].Visible = true;
                                        }
                                        else
                                        {
                                            grdCMSMenu.Columns[9].Visible = false;
                                        }
                                    }
                                }
                                else
                                {
                                    grdCMSMenu.Columns[9].Visible = false;
                                    grdCMSMenu.Columns[7].Visible = false;
                                }
                            }
                        }
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                    {
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                        {
                            grdCMSMenu.Columns[7].Visible = true;
                            grdCMSMenu.Columns[9].Visible = false;
                        }

                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                    {
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                        {
                            grdCMSMenu.Columns[7].Visible = true;
                        }

                    }

                    //End  
                }
                else
                {
                    grdCMSMenu.Columns[7].Visible = false;
                }
            }
            p_Var.dSet = null;
            lblmsg.Visible = false;
        }
        else
        {

            grdCMSMenu.Visible = false;
            BtnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnApprove.Visible = false;
            btnDisApprove.Visible = false;
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = "No record found.";
        }

        p_Var.Result = p_Var.k;

        Session["Status_Id"] = ddlStatus.SelectedValue.ToString();
        Session["Deptt_Id"] = ddlDepartment.SelectedValue.ToString();
        Session["CatTypeId"] = ddlcategory.SelectedValue.ToString();

    }

    #endregion


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

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            obj_userOB.ModuleId = Convert.ToInt32(Convert.ToInt32(Request.QueryString["ModuleID"]));
            //obj_userOB.DepttId = Convert.ToInt32(Session["Dept_ID"]);
            obj_userOB.DepttId = 1;
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
            ddlTariffType.Items.Clear();
            p_Var.dSet = tariffBL.getTariffType();
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

    #region Function to bind status in dropDownlist according to permission

    public void binddropDownlistStatus()
    {
        Miscelleneous_BL miscDdlStatus = new Miscelleneous_BL();
        Miscelleneous_BL miscdlStatus = new Miscelleneous_BL();

        obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
        obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
        p_Var.dSet = miscDdlStatus.getLanguagePermission(obj_userOB);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            pddlStatus.Visible = true;
            UserOB usrObject = new UserOB();
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft)).Append(",");
                btnAddNewPage.Visible = true;
                //code written on date 23sep 2013
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end
            }
            else
            {
                btnAddNewPage.Visible = false;
            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)).Append(",");

            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover)).Append(",");
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete));
            }

            usrObject.ModulestatusID = p_Var.sbuilder.ToString();
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            p_Var.dSet = null;
            p_Var.dSet = miscdlStatus.getStatusPermissionwise(usrObject);
            ddlStatus.DataSource = p_Var.dSet;
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "Status_Id";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Select Status", "0"));
            BtnForReview.Visible = false;
        }

        p_Var.dSet = null;
    }

    #endregion



    //end

    //Area for all gridView, repeater, datalist events

    #region gridView grdCMSMenu rowCreated event zone

    protected void grdCMSMenu_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == DataControlRowState.Normal | e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
            CheckBox chkBxHeader = (CheckBox)this.grdCMSMenu.HeaderRow.FindControl("chkSelectAll");
            //Add client side function childclick on check boxes
            chkBxSelect.Attributes.Add("onclick", string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID));

        }

    }

    #endregion

    #region gridview grdCMSMenu rowCommand event zone

    protected void grdCMSMenu_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (e.CommandName == "Delete")
                {

                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    Label lblStatus = (Label)row.Cells[0].FindControl("lblStatus");
                    tariffOB.TempLinkId = Convert.ToInt32(e.CommandArgument);
                    tariffOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                    tariffOB.StatusId = Convert.ToInt32(lblStatus.Text);
                    tariffOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Tariff);
                    p_Var.Result = tariffBL.Delete_Pending_Approved_Record(tariffOB);
                    if (p_Var.Result > 0)
                    {
                        if (ddlStatus.SelectedValue == "8")
                        {
                            Session["msg"] = "Tariff has been deleted (purged) permanently.";
                        }
                        else
                        {
                            Session["msg"] = "Tariff has been deleted successfully.";
                        }

                        obj_audit.ActionType = "D";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = obj_miscelBL.IpAddress();
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        Label lblTariffHeading = row.FindControl("lblTariffHeading") as Label;
                        Label lblstartdate = row.FindControl("lblstartdate") as Label;
                        if (lblstartdate == null) { return; }
                        obj_audit.Title = lblstartdate.Text + ", " + lblTariffHeading.Text;
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        // Session["msg"] = "Tariff has been deleted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/TariffModule/View_Tariff.aspx?ModuleID=27";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                    else
                    {
                        Session["msg"] = "Tariff has not been deleted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/TariffModule/View_Tariff.aspx?ModuleID=27";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }

                }

                else if (e.CommandName == "Restore")
                {
                    GridViewRow row1 = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    LinkOB obj_linkOBNew = new LinkOB();
                    obj_linkOBNew.TempLinkId = Int32.Parse(e.CommandArgument.ToString());


                    p_Var.Result = obj_linkBL.updateWebStatusDelete(obj_linkOBNew);
                    if (p_Var.Result > 0)
                    {
                        obj_audit.ActionType = "R";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = obj_miscelBL.IpAddress();
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        Label lblTariffHeading = row1.FindControl("lblTariffHeading") as Label;
                        Label lblstartdate = row1.FindControl("lblstartdate") as Label;
                        if (lblstartdate == null) { return; }
                        obj_audit.Title = lblstartdate.Text + ", " + lblTariffHeading.Text;
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "Tariff has been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/TariffModule/View_Tariff.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                    else
                    {
                        Session["msg"] = "Tariff has not been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/TariffModule/View_Tariff.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
                else if (e.CommandName == "Audit")
                {

                    DataSet dSetAuditTrail = new DataSet();
                    petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
                    petObject.ModuleID = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Tariff);
                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    petObject.ModuleType = null;
                    dSetAuditTrail = petPetitionBL.AuditTrailRecords(petObject);
                    Label lblprono = row.FindControl("lblTariffHeading") as Label;
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

    #endregion

    #region gridview grdCMSMenu RowDataBound event zone

    protected void grdCMSMenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.Image img = e.Row.FindControl("imgedit") as System.Web.UI.WebControls.Image;
            System.Web.UI.WebControls.Image img1 = e.Row.FindControl("emgnotedit") as System.Web.UI.WebControls.Image;
            ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
            ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
            //This is for delete/permanently delete 3 june 2013 

            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {

                imgDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to purge record no- " + DataBinder.Eval(e.Row.DataItem, "Temp_Link_Id") + "')");

                BtnRestore.Attributes.Add("onclick", "javascript:return " +
               "confirm('Are you sure you want to restore record no- " + DataBinder.Eval(e.Row.DataItem, "Temp_Link_Id") + "')");

            }
            else
            {

                imgDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete record no- " + DataBinder.Eval(e.Row.DataItem, "Temp_Link_Id") + "')");

                BtnRestore.Attributes.Add("onclick", "javascript:return " +
               "confirm('Are you sure you want to restore record no- " + DataBinder.Eval(e.Row.DataItem, "Temp_Link_Id") + "')");
            }

            //END


            tariffOB.LinkTypeId = Convert.ToInt32(grdCMSMenu.DataKeys[e.Row.RowIndex].Value);
            p_Var.dSet = tariffBL.links_web_Get_Link_Id_ForEdit(tariffOB);

            for (int i = 0; i < p_Var.dSet.Tables[0].Rows.Count; i++)
            {
                if (p_Var.dSet.Tables[0].Rows[i]["Link_Id"] != DBNull.Value)
                {

                    if (Convert.ToInt32(grdCMSMenu.DataKeys[e.Row.RowIndex].Value) == Convert.ToInt32(p_Var.dSet.Tables[0].Rows[i]["Link_Id"]))
                    {

                        img.Visible = false;
                        img1.Visible = true;
                        img1.Height = 10;
                        img1.Width = 10;

                    }
                    else
                    {
                        img1.Visible = false;
                        img.Visible = true;

                    }

                }
            }
        }
    }

    #endregion

    //End

    // ddl selected event zone

    #region selecte event zone of dropdown list page size

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    #endregion

    #endregion

    #region dropDownlist ddlLanguage selectedIndexChanged event zone

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Visible = false;
        binddropDownlistStatus();
        grdCMSMenu.Visible = false;
        //ddlPageSize.Visible = false;
        //lblPageSize.Visible = false;
        //rptPager.Visible = false;
        Session["TariffLng"] = ddlLanguage.SelectedValue;

    }

    #endregion

    #region dropDownlist ddlDepartment selectedIndexChanged event

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_CategoryName("1");
        binddropDownlistStatus();
        grdCMSMenu.Visible = false;
        //ddlPageSize.Visible = false;
        //lblPageSize.Visible = false;
        //rptPager.Visible = false;
        if (ddlDepartment.SelectedValue.ToString() == "2")
        {
            Pcategory.Visible = false;
        }
        else
        {
            Pcategory.Visible = true;
        }
        Session["TariffDeptt"] = ddlDepartment.SelectedValue;

    }

    #endregion

    #region dropDownlist ddlStatus selectedIndexChanged event zone

    public void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "0")
        {
            grdCMSMenu.Visible = false;
            BtnForReview.Visible = false;
            btnApprove.Visible = false;
            BtnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            binddropDownlistStatus();
            //ddlPageSize.Visible = false;
            //lblPageSize.Visible = false;
            //rptPager.Visible = false;
        }

        Bind_Grid("", "");
        Session["TariffStatus"] = ddlStatus.SelectedValue;




    }

    #endregion

    //end

    protected void ddlTariffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Visible = false;
        grdCMSMenu.Visible = false;
        if (Convert.ToInt32(ddlTariffType.SelectedValue.ToString()) == 0)
        {
            pddlStatus.Visible = false;
        }
        else
        {
            binddropDownlistStatus();
            Session["TariffType"] = ddlTariffType.SelectedValue;
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

        }
        catch
        {
            throw;
        }
    }

    #endregion

    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        Session["TariffCategory"] = ddlcategory.SelectedValue;
        grdCMSMenu.Visible = false;
        if (ddlcategory.SelectedValue == "8" || ddlcategory.SelectedValue == "9" || ddlcategory.SelectedValue == "10")
        {
            Pcategory.Visible = false;


        }
        else
        {
            Pcategory.Visible = true;

        }
    }



    protected void grdCMSMenu_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid(e.SortExpression, sortOrder);

    }

    #region gridView grdCMSMenu pageIndexChanging Event zone

    protected void grdCMSMenu_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCMSMenu.PageIndex = e.NewPageIndex;
        Bind_Grid(ViewState["e"].ToString(), ViewState["o"].ToString());
    }

    #endregion


    //End
    protected void btnSendEmails_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                StringBuilder sbuilder = new StringBuilder();
                StringBuilder sbuilderSms = new StringBuilder();
                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdCMSMenu.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblTariffHeading = (Label)row.FindControl("lblTariffHeading");
                        ViewState["tariff"] = lblTariffHeading.Text;
                        if ((selCheck.Checked == true))
                        {
                            //sbuilder.Append(lblTariffHeading.Text + "; ");
                            if (ddlcategory.SelectedItem.ToString() == "Tariff")
                            {
                                sbuilder.Append("<b>" + ddlcategory.SelectedItem.ToString() + "  : " + ddlTariffType.SelectedItem.ToString() + "-</b> " + lblTariffHeading.Text + "<br/> ");
                            }
                            else
                            {
                                sbuilder.Append("<b>" + ddlcategory.SelectedItem.ToString() + " -</b> " + lblTariffHeading.Text + "<br/> ");
                            }
                            p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                            tariffOB.TempLinkId = p_Var.dataKeyID;
                            tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            tariffOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                            tariffOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                            tariffOB.UserID = Convert.ToInt32(Session["User_Id"]);
                            tariffOB.IpAddress = Miscelleneous_DL.getclientIP();
                            tariffOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                            p_Var.Result = tariffBL.ASP_ChangeStatus_LinkTmpPermission(tariffOB);
                        }
                    }
                    if (p_Var.Result > 0)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                        {

                            if (li.Selected == true)
                            {
                                int statindex = li.Text.IndexOf("(") + 1;
                                p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                                sbuilderSms.Append(li.Value + ";");
                            }

                        }
                        if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                        {
                            p_Var.sbuilder.Append("Record pending for Review : " + sbuilder.ToString());
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            obj_miscelBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record pending for Review", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                        }
                        ///* Code to send sms */
                        char[] splitter = { ';' };
                        string textmessage;
                        PetitionOB petObjectNew = new PetitionOB();
                        DataSet dsSms = new DataSet();
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
                                        if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                                        {
                                            textmessage = "HERC - Record pending for review, Tariff - ";
                                        }
                                        else
                                        {

                                            textmessage = "HERC - Record pending for review, Tariff - ";
                                        }

                                        obj_miscelBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }

                            }
                        }

                        /* End */
                        Session["msg"] = "Tariff has been sent for review successfully.";

                        Session["Redirect"] = "~/Auth/AdminPanel/TariffModule/View_Tariff.aspx?ModuleID=27";

                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");


                    }


                }
                else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdCMSMenu.Rows)
                    {
                        Label lblTariffHeading = (Label)row.FindControl("lblTariffHeading");
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        ViewState["tariff"] = lblTariffHeading.Text;
                        if ((selCheck.Checked == true))
                        {
                            if (ddlcategory.SelectedItem.ToString() == "Tariff")
                            {
                                sbuilder.Append("<b>" + ddlcategory.SelectedItem.ToString() + "  : " + ddlTariffType.SelectedItem.ToString() + "-</b> " + lblTariffHeading.Text + "<br/> ");
                            }
                            else
                            {
                                sbuilder.Append("<b>" + ddlcategory.SelectedItem.ToString() + " -</b> " + lblTariffHeading.Text + "<br/> ");
                            }
                            p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                            tariffOB.TempLinkId = p_Var.dataKeyID;
                            tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            tariffOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                            tariffOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                            tariffOB.UserID = Convert.ToInt32(Session["User_Id"]);
                            tariffOB.IpAddress = Miscelleneous_DL.getclientIP();
                            tariffOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                            p_Var.Result = tariffBL.ASP_ChangeStatus_LinkTmpPermission(tariffOB);

                        }
                    }
                    if (p_Var.Result > 0)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        sbuilderSms.Remove(0, sbuilderSms.Length);
                        foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                        {

                            if (li.Selected == true)
                            {
                                // p_Var.sbuildertmp.Append(li.Value + ";");
                                int statindex = li.Text.IndexOf("(") + 1;
                                p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                                sbuilderSms.Append(li.Value + ";");
                            }

                        }
                        if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                        {
                            p_Var.sbuilder.Append("Record pending for Publish : " + sbuilder.ToString());
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            obj_miscelBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record pending for Publish", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                        }

                        ///* Code to send sms */
                        char[] splitter = { ';' };
                        string textmessage;
                        PetitionOB petObjectNew = new PetitionOB();
                        DataSet dsSms = new DataSet();
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
                                        if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                                        {
                                            textmessage = "HERC - Record pending for publish, Tariff - ";
                                        }
                                        else
                                        {

                                            textmessage = "HERC - Record pending for publish, Tariff - ";
                                        }

                                        obj_miscelBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }

                            }
                        }

                        /* End */
                        Session["msg"] = "Tariff has been sent for publish successfully.";

                        Session["Redirect"] = "~/Auth/AdminPanel/TariffModule/View_Tariff.aspx?ModuleID=27";

                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");


                    }

                }

                else
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdCMSMenu.Rows)
                    {
                        Label lblTariffHeading = (Label)row.FindControl("lblTariffHeading");
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        ViewState["tariff"] = lblTariffHeading.Text;


                        if (selCheck.Checked == true)
                        {
                            //sbuilder.Append("<b>Tariff -<b/>" + lblTariffHeading.Text + "<br/> ");
                            if (ddlcategory.SelectedItem.ToString() == "Tariff")
                            {
                                sbuilder.Append("<b>" + ddlcategory.SelectedItem.ToString() + "  : " + ddlTariffType.SelectedItem.ToString() + "-</b> " + lblTariffHeading.Text + "<br/> ");
                            }
                            else
                            {
                                sbuilder.Append("<b>" + ddlcategory.SelectedItem.ToString() + " -</b> " + lblTariffHeading.Text + "<br/> ");
                            }
                            tariffOB.TempLinkId = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                            tariffOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                            tariffOB.UserID = Convert.ToInt32(Session["User_Id"]);
                            tariffOB.IpAddress = Miscelleneous_DL.getclientIP();
                            tariffOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                            p_Var.Result = tariffBL.insert_Top_Tariff_in_Web(tariffOB);
                        }
                    }

                    if (p_Var.Result > 0)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        sbuilderSms.Remove(0, sbuilderSms.Length);
                        foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                        {

                            if (li.Selected == true)
                            {
                                int statindex = li.Text.IndexOf("(") + 1;
                                p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                                sbuilderSms.Append(li.Value + ";");
                            }

                        }
                        if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                        {
                            p_Var.sbuilder.Append("Record Published : " + sbuilder.ToString());
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            obj_miscelBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record Published", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                        }

                        ///* Code to send sms */
                        char[] splitter = { ';' };
                        string textmessage;
                        PetitionOB petObjectNew = new PetitionOB();
                        DataSet dsSms = new DataSet();
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
                                        if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                                        {
                                            textmessage = "HERC - Record published, Tariff - ";
                                        }
                                        else
                                        {

                                            textmessage = "HERC - Record published, Tariff - ";
                                        }

                                        obj_miscelBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }

                            }
                        }

                        /* End */

                        Session["msg"] = "Tariff has been published successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/TariffModule/View_Tariff.aspx?ModuleID=27";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                    }
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
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
            {
                foreach (GridViewRow row in grdCMSMenu.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                        tariffOB.TempLinkId = p_Var.dataKeyID;
                        tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        tariffOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        tariffOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                        tariffOB.UserID = Convert.ToInt32(Session["User_Id"]);
                        tariffOB.IpAddress = Miscelleneous_DL.getclientIP();
                        tariffOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        p_Var.Result = tariffBL.ASP_ChangeStatus_LinkTmpPermission(tariffOB);
                    }
                }
                if (p_Var.Result > 0)
                {

                    Session["msg"] = "Tariff has been sent for review successfully.";

                    Session["Redirect"] = "~/Auth/AdminPanel/TariffModule/View_Tariff.aspx?ModuleID=27";

                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");


                }
            }
            else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {
                foreach (GridViewRow row in grdCMSMenu.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                        tariffOB.TempLinkId = p_Var.dataKeyID;
                        tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        tariffOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        tariffOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                        tariffOB.UserID = Convert.ToInt32(Session["User_Id"]);
                        tariffOB.IpAddress = Miscelleneous_DL.getclientIP();
                        tariffOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        p_Var.Result = tariffBL.ASP_ChangeStatus_LinkTmpPermission(tariffOB);

                    }
                }
                if (p_Var.Result > 0)
                {


                    Session["msg"] = "Tariff has been sent for publish successfully.";

                    Session["Redirect"] = "~/Auth/AdminPanel/TariffModule/View_Tariff.aspx?ModuleID=27";

                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");


                }

            }
            else
            {
                foreach (GridViewRow row in grdCMSMenu.Rows)
                {

                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if (selCheck.Checked == true)
                    {

                        tariffOB.TempLinkId = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                        tariffOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                        tariffOB.UserID = Convert.ToInt32(Session["User_Id"]);
                        tariffOB.IpAddress = Miscelleneous_DL.getclientIP();
                        tariffOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        p_Var.Result = tariffBL.insert_Top_Tariff_in_Web(tariffOB);
                    }
                }

                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Tariff has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/TariffModule/View_Tariff.aspx?ModuleID=27";
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
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
            StringBuilder sbuilder = new StringBuilder();
            StringBuilder sbuilderSms = new StringBuilder();
            foreach (GridViewRow row in grdCMSMenu.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                Label lblTariffHeading = (Label)row.FindControl("lblTariffHeading");

                if ((selCheck.Checked == true))
                {
                    //sbuilder.Append("<b>Tariff -<b/>" + lblTariffHeading.Text + "<br/> ");
                    if (ddlcategory.SelectedItem.ToString() == "Tariff")
                    {
                        sbuilder.Append("<b>" + ddlcategory.SelectedItem.ToString() + "  : " + ddlTariffType.SelectedItem.ToString() + "-</b> " + lblTariffHeading.Text + "<br/> ");
                    }
                    else
                    {
                        sbuilder.Append("<b>" + ddlcategory.SelectedItem.ToString() + " -</b> " + lblTariffHeading.Text + "<br/> ");
                    }
                    p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                    tariffOB.TempLinkId = p_Var.dataKeyID;
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    // tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    tariffOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    tariffOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                    tariffOB.IpAddress = obj_miscelBL.IpAddress();
                    p_Var.Result = tariffBL.ASP_ChangeStatus_LinkTmpPermission(tariffOB);

                }
            }
            if (p_Var.Result > 0)
            {
                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                foreach (System.Web.UI.WebControls.ListItem li in chkSendEmailsDis.Items)
                {

                    if (li.Selected == true)
                    {
                        int statindex = li.Text.IndexOf("(") + 1;
                        p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                        sbuilderSms.Append(li.Value + ";");
                    }

                }
                if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                {
                    p_Var.sbuilder.Append("Record Disapproved : " + sbuilder.ToString());
                    //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                    string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    p_Var.sbuildertmp.Append(email);
                    obj_miscelBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record Disapproved", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

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
                                if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                                {
                                    textmessage = "HERC - Record disapproved, Tariff - ";
                                }
                                else
                                {

                                    textmessage = "HERC - Record disapproved, Tariff - ";
                                }

                                obj_miscelBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                            }

                        }

                    }
                }

                /* End */

                Session["msg"] = "Tariff has been disapproved successfully.";

                Session["Redirect"] = "~/Auth/AdminPanel/TariffModule/View_Tariff.aspx?ModuleID=27";

                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

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
            foreach (GridViewRow row in grdCMSMenu.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                    p_Var.dataKeyID = Convert.ToInt32(grdCMSMenu.DataKeys[row.RowIndex].Value);
                    tariffOB.TempLinkId = p_Var.dataKeyID;
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    // tariffOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    tariffOB.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    tariffOB.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                    tariffOB.IpAddress = obj_miscelBL.IpAddress();
                    p_Var.Result = tariffBL.ASP_ChangeStatus_LinkTmpPermission(tariffOB);

                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "Tariff has been disapproved successfully.";

                Session["Redirect"] = "~/Auth/AdminPanel/TariffModule/View_Tariff.aspx?ModuleID=27";

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
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Tariff);
            p_Var.dSetCompare = obj_UserBL.getReviewEmailIds(obj_userOB);
            lblSelectors.Text = "Select the Reviewer(s)";
            lblSelectors.Font.Bold = true;
            chkSendEmails.DataSource = p_Var.dSetCompare;
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
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Tariff);
            p_Var.dSetCompare = obj_UserBL.getPublisherEmailIds(obj_userOB);

            chkSendEmails.DataSource = p_Var.dSetCompare;
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
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Tariff);
            p_Var.dSetCompare = obj_UserBL.getReviewEmailIds(obj_userOB);
            lblSelectorsDis.Text = "Select the Reviewer(s)";
            lblSelectorsDis.Font.Bold = true;
            chkSendEmailsDis.DataSource = p_Var.dSetCompare;
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
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Tariff);
            p_Var.dSetCompare = obj_UserBL.getDataEntryEmailIds(obj_userOB);

            chkSendEmailsDis.DataSource = p_Var.dSetCompare;
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
