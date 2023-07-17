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


public partial class Auth_AdminPanel_Role_DisplayRole : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration 

    #region Variable declaration zone

    Role_PermissionBL obj_roleBL = new Role_PermissionBL();
    UserBL obj_UserBL = new UserBL();
    UserOB obj_UserOB = new UserOB();
    Project_Variables p_Val = new Project_Variables();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petPetitionBL = new PetitionBL();

    #endregion 

    //End

    //Area for page load event

    #region Page load event zone

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
        lblModulename.Text = ": View  Roles";
        this.Page.Title = " Role: HERC";

        if (!IsPostBack)
        {
            DataSet dsprv = new DataSet();
            obj_UserOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_UserOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_roleBL.ASP_CheckPrivilagesALLForPermission(obj_UserOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }

            GrdDisplayRole.Visible = false;
            ViewState["sortOrder"] = "";
            if (Convert.ToInt16(Session["User_ID"]) == 6)
            {
                if (Session["RoleDeptt"] != null)
                {
                    Get_Deptt_Name();
                    pnlDearptment.Visible = true;
                    ddlDepartment.SelectedValue = Session["RoleDeptt"].ToString();
                    GrdDisplayRole.Visible = true;
                }
                else
                {
                    Get_Deptt_Name();
                    pnlDearptment.Visible = true;
                }
            }
            else
            {
                pnlDearptment.Visible = false;
            }

            GetRoleName("", "");
        }

    }

    #endregion 

    //End

    //Area for all the buttons, linkButtons, imageButtons click events

    #region linkButton lnkAdd click event to add new role

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/Role/") + "Add_Role.aspx?ModuleID="+Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Role));
    }

    #endregion

    //End 

    //Area for all the gridView events

    #region gridview GrdDisplayRole rowCommand event Zone

    protected void GrdDisplayRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                UserBL obj_userBL = new UserBL();
                p_Val.stringTypeID = e.CommandArgument.ToString();
                if (e.CommandName == "delete")
                {
                    obj_UserOB.RoleId = Convert.ToInt32(p_Val.stringTypeID);
                    p_Val.Result = obj_userBL.ASP_Role_User_Delete(obj_UserOB);
                    if (p_Val.Result > 0)
                    {
                        Session["msg"] = "Role has been deleted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Role/DisplayRole.aspx?ModuleID=" + Convert.ToInt16(Request.QueryString["ModuleID"]);
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                    else
                    {
                        Session["msg"] = "Role is not deleted because users associated with it.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Role/DisplayRole.aspx?ModuleID=" + Convert.ToInt16(Request.QueryString["ModuleID"]);
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                    GetRoleName("", "");
                }
                else if (e.CommandName == "Audit")
                {
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    DataSet dSetAuditTrail = new DataSet();
                    petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
                    petObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Role);
                    petObject.ModuleType = null;
                    dSetAuditTrail = petPetitionBL.AuditTrailRecords(petObject);
                    Label lblprono = row.FindControl("lblRole") as Label;
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


                    }
                    else
                    {
                        ltrlCreation.Text = "";
                        ltrlLastEdited.Text = "";
                        ltrlPetitionNo.Text = "";
                        ltrlPetitionNo.Text = "";

                    }
                    this.mdpAuditTrail.Show();
                }
            }
        }
    }

    #endregion

    #region gridview GrdDisplayRole rowDeleting event zone

    protected void GrdDisplayRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GetRoleName("","");
    }

    #endregion

    //End 

    //Area for all the dropDownlist events

    #region dropDownlist ddlDepartment selectedIndexChanged event

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != "0")
        {
            GrdDisplayRole.Visible = true; 
            GetRoleName("","");
            Session["RoleDeptt"] = ddlDepartment.SelectedValue;
        }
        else
        {
            GrdDisplayRole.Visible = false;
        }
    }

    #endregion

    //End

    //Area for all the user-defind functions

    #region  function to display role in gridView

    public void GetRoleName(string sortExp, string sortDir)
    {
        try
        {
            ViewState["o"] = sortDir;
            ViewState["e"] = sortExp;

            if (Convert.ToInt16(Session["User_ID"]) == 6)
            {
                obj_UserOB.DepttId = Convert.ToInt16(ddlDepartment.SelectedValue);
            }
            else
            {
                obj_UserOB.DepttId = Convert.ToInt16(Session["Dept_ID"]);
            }
            obj_UserOB.UserId =Convert.ToInt16( Session["User_ID"]);
            p_Val.dSet = obj_roleBL.ASP_Role_GetRoleName(obj_UserOB);

            //Codes for the sorting of records

            DataView myDataView = new DataView();
            myDataView = p_Val.dSet.Tables[0].DefaultView;

            if (sortExp != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            }

            //End

            if (p_Val.dSet.Tables[0].Rows.Count > 0)
            {
                GrdDisplayRole.DataSource = myDataView;
            }
            else
            {
                GrdDisplayRole.DataSource = null;
            }
            GrdDisplayRole.DataBind();
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
            obj_UserOB.ModuleId=Convert.ToInt16(Request.QueryString["ModuleID"]);
            obj_UserOB.DepttId = Convert.ToInt16(Session["Dept_ID"]);
            p_Val.dSet = obj_UserBL.ASP_Get_Deptt_Name(obj_UserOB);
            if (p_Val.dSet.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = p_Val.dSet;
                ddlDepartment.DataValueField = "Deptt_Id";
                ddlDepartment.DataTextField = "Deptt_Name";
                ddlDepartment.DataBind();
            }
            ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End 

    protected void GrdDisplayRole_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //This is for delete/permanently delete 3 june 2013 
            ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");


            BtnDelete.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete Record No- " + DataBinder.Eval(e.Row.DataItem, "Role_Id") + "')");

            //END
        }
    }

    protected void GrdDisplayRole_Sorting(object sender, GridViewSortEventArgs e)
    {
        GetRoleName(e.SortExpression, sortOrder);
    }

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
}
