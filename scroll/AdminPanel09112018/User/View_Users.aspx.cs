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


public partial class Auth_AdminPanel_User_View_Users : System.Web.UI.Page
{
    //Area for all the variables declaration zone

    #region variable declaration zone
    Project_Variables p_Var = new Project_Variables();
    UserBL obj_UserBL = new UserBL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    DataSet ds = new DataSet();
    PaginationBL pagingBL = new PaginationBL();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petPetitionBL = new PetitionBL();

    #endregion

    //End

    //Area for the page load event zone

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

        Session.Remove("RoleDeptt");//Role Sessions

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
        lblModulename.Text = ": View  Users";
        this.Page.Title = " User: HERC";
        if (!IsPostBack)
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

            ViewState["sortOrder"] = "";
            if (Session["UsrDeptt"] != null)
            {
                Get_Deptt_Name();    // To get Deptt Name
                ddlDeptname.SelectedValue = Session["UsrDeptt"].ToString();
            }
            else
            {
                Get_Deptt_Name();    
            }
            if (Session["UsrStatus"] != null)
            {
                Get_Status_Name();
                ddlStatus.SelectedValue = Session["UsrStatus"].ToString();
                Bind_Grid_Users("","");
            }
            else
            {
                Get_Status_Name();   // To get Status Name
            }  
                   
        }
    }

    #endregion

    //End

    //Area for all the buttons, linkButtons, imageButtons click events

    #region linkButton lnkAddUser click event to add new user

    protected void lnkAddUser_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/User/") +"Add_User.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.User));
    }

    #endregion 

    

    //End

   //Area for all the dropDownlist events

    #region dropDownlist ddlDeptname selectedIndexChanged event

    protected void ddlDeptname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeptname.SelectedValue == "0")
        {
            //GrdViewUsers.Visible = false;

        }
        GrdViewUsers.Visible = false;
        Get_Status_Name();
        Session["UsrDeptt"] = ddlDeptname.SelectedValue;
    }

    #endregion 

    #region dropDownlist ddlStatus selectedIndexChanged event

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "0")
        {
            GrdViewUsers.Visible = false;          
        }
      else
       {
          //  GrdViewUsers.Visible = true;
            Bind_Grid_Users("","");
            Session["UsrStatus"] = ddlStatus.SelectedValue;
       }

    }

    #endregion 

    //End

    //Area for all the gridView, repeater, datagrid events

    #region gridView griViewUsers RowCommand event

    protected void GrdViewUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            string strId = e.CommandArgument.ToString();
            string popupScript = "<script language='javascript'>" +
                "window.open('../viewdetails.aspx?User_ID=" + strId + "', 'mywindow', " +
                "' menubar=no, resizable=no, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                "</script>";
            Page.RegisterStartupScript("PopupScript", popupScript);

        }
        if (e.CommandName == "Status")
        {
            UserOB obj_usrOB = new UserOB();
           
            obj_usrOB.UserId = Int32.Parse(e.CommandArgument.ToString());
            obj_usrOB.PageIndex = Convert.ToInt16(Session["User_ID"]);
            obj_usrOB.IpAddress = Miscelleneous_DL.getclientIP();
            if (ddlStatus.SelectedValue == "1")
            {
                obj_usrOB.StatusId = 2;
            }
            else if (ddlStatus.SelectedValue == "2")
            {
                obj_usrOB.StatusId = 1;
            }
            int result = obj_UserBL.ASP_Update_status(obj_usrOB);
            if (result > 0)
            {
                Bind_Grid_Users("","");
                if (ddlStatus.SelectedValue == "1")
                {
                    Session["msg"] = "User has been inactivated successfully.";
                }
                else if (ddlStatus.SelectedValue == "2")
                {
                    Session["msg"] = "User has been activated successfully.";
                }
                Session["Redirect"] = "~/Auth/AdminPanel/User/View_Users.aspx?ModuleID=" + Convert.ToInt16(Request.QueryString["ModuleID"]);
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
          

        }
        if (e.CommandName == "delete")
        {
            UserOB obj_usrOB1 = new UserOB();
            obj_usrOB1.UserId = Convert.ToInt16(e.CommandArgument.ToString());
            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.DeletedUser))
            {
                obj_usrOB1.StatusId = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.DeletedUser);
            }
            else
            {
                obj_usrOB1.StatusId = null;
            }
            int check = obj_UserBL.ASP_User_Delete(obj_usrOB1);
            if (check > 0)
            {
                Session["msg"] = "User has been deleted successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/User/View_Users.aspx?ModuleID=" + Convert.ToInt16(Request.QueryString["ModuleID"]);
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
            else
            {
                Session["msg"] = "User has not been deleted successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/User/View_Users.aspx?ModuleID=" + Convert.ToInt16(Request.QueryString["ModuleID"]);
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }

        else if (e.CommandName == "Audit")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            DataSet dSetAuditTrail = new DataSet();
            petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
            petObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.User);
            petObject.ModuleType = null;
            dSetAuditTrail = petPetitionBL.AuditTrailRecords(petObject);
            LinkButton lblprono = row.FindControl("LnkDetails") as LinkButton;
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
                ltrlPetitionNo.Text = "";
            }
            this.mdpAuditTrail.Show();
        }
        else if (e.CommandName == "Restore")
        {

            UserOB obj_usrOB1 = new UserOB();
            obj_usrOB1.UserId = Convert.ToInt16(e.CommandArgument.ToString());


            int check = obj_UserBL.RestoreUser(obj_usrOB1);
            if (check > 0)
            {
                Session["msg"] = "User has been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/User/View_Users.aspx?ModuleID=" + Convert.ToInt16(Request.QueryString["ModuleID"]);
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
            else
            {
                Session["msg"] = "User has not been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/User/View_Users.aspx?ModuleID=" + Convert.ToInt16(Request.QueryString["ModuleID"]);
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }
        //Bind_Grid_Users("","");
    }

    #endregion

    #region gridView grdViewUsers rowDataBound event 

    protected void GrdViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("LnkStatus");
            if (ddlStatus.SelectedValue == "1")
            {
                lnk.Text = "Inactive";
                lnk.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure to inactive user?')");
            }
            else
            {
                lnk.Text = "Active";
                lnk.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure to active user?')");
            }

            //This is for delete/permanently delete 3 june 2013 
            ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");


            BtnDelete.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete User:- " + DataBinder.Eval(e.Row.DataItem, "Username") + "')");

            //END
        }
    }

    #endregion 

    //End

    //Area for all the user-defind functions

    #region Function Function to bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            obj_userOB.ModuleId = Convert.ToInt16(Request.QueryString["ModuleID"]);
            obj_userOB.DepttId = Convert.ToInt16(Session["Dept_ID"]);
            ds = obj_UserBL.ASP_Get_Deptt_Name(obj_userOB);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDeptname.DataSource = ds;
                ddlDeptname.DataValueField = "Deptt_Id";
                ddlDeptname.DataTextField = "Deptt_Name";
                ddlDeptname.DataBind();
            }
           // ddlDeptname.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind status in dropDownlist

    public void Get_Status_Name()
    {
        try
        {
            obj_userOB.StatusId = 1;
            ds = obj_UserBL.ASP_Status_TypeUser(obj_userOB);
            ddlStatus.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = ds;
                ddlStatus.DataTextField = "Status";
                ddlStatus.DataValueField = "Status_Id";
                ddlStatus.DataBind();
            }
            ddlStatus.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind users in gridView

    public void Bind_Grid_Users(string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;

        UserOB obj_userOB1 = new UserOB();
        obj_userOB1.UserId=Convert.ToInt16(Session["User_ID"]);
        obj_userOB1.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
        obj_userOB1.DepttId = Convert.ToInt32(ddlDeptname.SelectedValue);
        
        p_Var.dSet = obj_UserBL.ASP_User_display(obj_userOB1, out p_Var.k);

        //Codes for the sorting of records


        //End
        //GrdViewUsers.DataSource = myDataView;
        //GrdViewUsers.DataBind();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            GrdViewUsers.Visible = true;
            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.DeletedUser))
            {
                GrdViewUsers.Columns[6].HeaderText = "Purge";
                GrdViewUsers.Columns[7].Visible = true;
            }
            else
            {
                GrdViewUsers.Columns[6].HeaderText = "Delete";
                GrdViewUsers.Columns[7].Visible = false;
            }


            DataView myDataView = new DataView();
            myDataView = p_Var.dSet.Tables[0].DefaultView;

            if (sortExp != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            }
            GrdViewUsers.DataSource = myDataView;
            GrdViewUsers.DataBind();
            LblMsg.Visible = false;
        }
        else
        {
            GrdViewUsers.DataSource = null;
            //rptPager.Visible = false;
            //lblPageSize.Visible = false;
            //ddlPageSize.Visible = false;
            GrdViewUsers.Visible = false;
            LblMsg.Visible = true;
            LblMsg.ForeColor = System.Drawing.Color.Red;
            LblMsg.Text = "No record found.";
        }
        p_Var.Result = p_Var.k;
       
       
    }

    #endregion

    

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        Bind_Grid_Users( "", "");
    }

    #endregion

    protected void GrdViewUsers_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid_Users(e.SortExpression, sortOrder);
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
    protected void GrdViewUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdViewUsers.PageIndex = e.NewPageIndex;
        Bind_Grid_Users(ViewState["e"].ToString(), ViewState["o"].ToString());
    }
}

