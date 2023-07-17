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
using System.Drawing;

public partial class Auth_AdminPanel_Role_Add_Role : System.Web.UI.Page
{
    //Area for all the variable declaration

    #region Variable declaration zone

    Role_PermissionBL obj_roleBL = new Role_PermissionBL();
    UserOB obj_UserOB = new UserOB();
    UserOB obj_userOB1 = new UserOB();
    Project_Variables p_Val = new Project_Variables();
    UserBL obj_UserBL = new UserBL();
    public int ModuleID = 0;

    #endregion

    //End

    //Area for page load event

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        
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

            ModuleID = Convert.ToInt16(Request.QueryString["ModuleID"]);
            Get_permission();
            BtnUpdate.Visible = false;

            if (Convert.ToInt16(Session["User_ID"]) == 6)
            {
                Get_Deptt_Name();
                pnlDearptment.Visible = true;
            }
            else
            {
                pnlDearptment.Visible = false;
            }
            if (Request.QueryString["Role_id"] != null)
            {
                BtnUpdate.Visible = true;
                display(Request.QueryString["Role_id"].ToString());

                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Edit  Role";
                this.Page.Title = "Edit Role: HERC";
            }
            else
            {
                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Add  Role";
                this.Page.Title = "Add Role: HERC";
            }
        }
    }

    #endregion

    //End

    //Area for all buttons, link buttons and image button events

    #region Button btnAdd click event to add new role

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            obj_userOB1.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
            int deptid = 0;
            if (ddlDepartment.SelectedValue != "0" && ddlDepartment.SelectedValue != "" && ddlDepartment.SelectedValue != null)
            {
                deptid = Convert.ToInt16(ddlDepartment.SelectedValue);
            }
            else
            {
                deptid = Convert.ToInt16(Session["Dept_ID"]);
            }
            if (obj_roleBL.ASP_Role_CheckRoleAvailability(TxtName.Text, deptid))
            {
                LblMsg.Text = "Role already exists please select another role";
                LblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            obj_userOB1.RoleName = TxtName.Text;
            if (Convert.ToInt16(Session["User_ID"]) != 6)
            {
                obj_userOB1.DepttId = Convert.ToInt16(Session["Dept_ID"]);
            }
            else
            {
                obj_userOB1.DepttId = Convert.ToInt16(ddlDepartment.SelectedValue);
            }
            obj_userOB1.InsertedBy = Convert.ToInt16(Session["User_Id"]);
            obj_userOB1.IpAddress = Miscelleneous_DL.getclientIP();
            int role = obj_roleBL.ASP_Role_insert(obj_userOB1);
            if (role > 0)
            {
                foreach (GridViewRow row in GrdPermission.Rows)
                {
                    int Module_Id = Convert.ToInt32(((Label)row.FindControl("LblModuleID")).Text.Trim());
                    CheckBox draft = (CheckBox)row.FindControl("chkDrft");
                    CheckBox review = (CheckBox)row.FindControl("chkRvw");
                    CheckBox publish = (CheckBox)row.FindControl("chkpblsh");
                    CheckBox Edit = (CheckBox)row.FindControl("chkEdit");
                    CheckBox Delete = (CheckBox)row.FindControl("chkDelete");
                    CheckBox Repealed = (CheckBox)row.FindControl("chkRepealed");
                    CheckBox Hindi = (CheckBox)row.FindControl("chkHindi");
                    CheckBox English = (CheckBox)row.FindControl("chkEnglish");
                    obj_UserOB.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert);
                    obj_UserOB.RoleId = role;
                    obj_UserOB.ModuleId = Module_Id;
                    obj_UserOB.draft = Convert.ToInt32(draft.Checked);
                    obj_UserOB.review = Convert.ToInt32(review.Checked);
                    obj_UserOB.publish = Convert.ToInt32(publish.Checked);
                    obj_UserOB.edit = Convert.ToInt32(Edit.Checked);
                    obj_UserOB.deleted = Convert.ToInt32(Delete.Checked);
                    obj_UserOB.repealed = Convert.ToInt32(Repealed.Checked);

                    if (Convert.ToBoolean(Hindi.Checked) == true || Convert.ToBoolean(English.Checked) == true)
                    {
                        obj_UserOB.hindi = Convert.ToInt32(Hindi.Checked);
                        obj_UserOB.english = Convert.ToInt32(English.Checked);
                    }
                    else
                    {
                        obj_UserOB.hindi = Convert.ToInt32(Hindi.Checked);
                        obj_UserOB.english = Convert.ToInt32(true);
                    }
                   
                    p_Val.Result = obj_roleBL.ASP_Module_Role_Permission_insert(obj_UserOB);

                }
                if (p_Val.Result > 0)
                {
                    Session["msg"] = "Role has been created successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Role/DisplayRole.aspx?ModuleID=" + Convert.ToInt16(Request.QueryString["ModuleID"]);
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
        }
    }

    #endregion

    #region Button btnUpdate click event to update permission

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                Update_Permission();
            }
        }
        catch (Exception ex)
        {
            System.Web.HttpContext.Current.Response.Write(ex.Message);
        }
    }

    #endregion

    #region Button btnBack click event to go back

    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("DisplayRole.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Role));
    }

    #endregion

    //End 

    //Area for all checkboxes events   

    #region checkBox chkheader checkChanged event to check all checkboxes

    protected void chkheader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)GrdPermission.HeaderRow.FindControl("ChkHeader");
        if (chk.Checked)
        {
            foreach (GridViewRow row in GrdPermission.Rows)
            {
                CheckBox chkrow = (CheckBox)row.FindControl("ChkChild");
                chkrow.Checked = true;
                CheckBox draft = (CheckBox)row.Cells[2].FindControl("chkDrft");
                CheckBox review = (CheckBox)row.Cells[3].FindControl("chkRvw");
                CheckBox publish = (CheckBox)row.Cells[4].FindControl("chkpblsh");
                CheckBox Edit = (CheckBox)row.Cells[5].FindControl("chkEdit");
                CheckBox delete = (CheckBox)row.Cells[6].FindControl("chkDelete");
                CheckBox Repealed = (CheckBox)row.Cells[6].FindControl("chkRepealed");
                CheckBox Hindi = (CheckBox)row.Cells[6].FindControl("chkHindi");
                CheckBox English = (CheckBox)row.Cells[6].FindControl("chkEnglish");
                draft.Checked = true;
                review.Checked = true;
                publish.Checked = true;
                Edit.Checked = true;
                delete.Checked = true;
                Repealed.Checked = true;
                Hindi.Checked = true;
                English.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow row in GrdPermission.Rows)
            {
                CheckBox chkrow = (CheckBox)row.FindControl("ChkChild");
                chkrow.Checked = false;
                CheckBox draft = (CheckBox)row.Cells[2].FindControl("chkDrft");
                CheckBox review = (CheckBox)row.Cells[3].FindControl("chkRvw");
                CheckBox publish = (CheckBox)row.Cells[4].FindControl("chkpblsh");
                CheckBox Edit = (CheckBox)row.Cells[5].FindControl("chkEdit");
                CheckBox delete = (CheckBox)row.Cells[6].FindControl("chkDelete");
                CheckBox Repealed = (CheckBox)row.Cells[6].FindControl("chkRepealed");
                CheckBox Hindi = (CheckBox)row.Cells[6].FindControl("chkHindi");
                CheckBox English = (CheckBox)row.Cells[6].FindControl("chkEnglish");
                draft.Checked = false;
                review.Checked = false;
                publish.Checked = false;
                Edit.Checked = false;
                delete.Checked = false;
                Repealed.Checked = false;
                Hindi.Checked = false;
                English.Checked = false;
            }
        }
    }

    #endregion

    #region checkBox chkchild checkedChanged event to check child checkboxes

    protected void ChkChild_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox btndetails = sender as CheckBox;
        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;

        CheckBox child = gvrow.FindControl("ChkChild") as CheckBox;
        if (child.Checked == false)
        {
            CheckBox draft = (CheckBox)gvrow.FindControl("chkDrft");
            CheckBox review = (CheckBox)gvrow.FindControl("chkRvw");
            CheckBox publish = (CheckBox)gvrow.FindControl("chkpblsh");
            CheckBox Edit = (CheckBox)gvrow.FindControl("chkEdit");
            CheckBox delete = (CheckBox)gvrow.FindControl("chkDelete");
            CheckBox Repealed = (CheckBox)gvrow.FindControl("chkRepealed");
            CheckBox Hindi = (CheckBox)gvrow.FindControl("chkHindi");
            CheckBox English = (CheckBox)gvrow.FindControl("chkEnglish");
            draft.Checked = false;
            review.Checked = false;
            publish.Checked = false;
            Edit.Checked = false;
            delete.Checked = false;
            Repealed.Checked = false;
            Hindi.Checked = false;
            English.Checked = false;

        }
        else
        {
            CheckBox draft = (CheckBox)gvrow.FindControl("chkDrft");
            CheckBox review = (CheckBox)gvrow.FindControl("chkRvw");
            CheckBox publish = (CheckBox)gvrow.FindControl("chkpblsh");
            CheckBox Edit = (CheckBox)gvrow.FindControl("chkEdit");
            CheckBox delete = (CheckBox)gvrow.FindControl("chkDelete");
            CheckBox Repealed = (CheckBox)gvrow.FindControl("chkRepealed");
            CheckBox Hindi = (CheckBox)gvrow.FindControl("chkHindi");
            CheckBox English = (CheckBox)gvrow.FindControl("chkEnglish");
            draft.Checked = true;
            review.Checked = true;
            publish.Checked = true;
            Edit.Checked = true;
            delete.Checked = true;
            Repealed.Checked = true;
            Hindi.Checked = true;
            English.Checked = true;

        }
    }

    #endregion

    //End   

    //Area for all user-defind functions

    #region Function to display permissions in the gridview

    public void Get_permission()
    {
        obj_UserOB.UserId = Convert.ToInt16(Session["User_Id"]);
        p_Val.dSet = obj_roleBL.Asp_Module_GetModule(obj_UserOB);
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            GrdPermission.DataSource = p_Val.dSet;
            GrdPermission.DataBind();
        }
        p_Val.dSet = null;
    }

    #endregion

    #region Function To display data in edit mode

    public void display(string Role_id)
    {
        BtnAdd.Visible = false;
        obj_UserOB.RoleId = Convert.ToInt32(Role_id);
        p_Val.dSet = obj_roleBL.ASP_Module_Role_Permission_Display(obj_UserOB);
        TxtName.Text = p_Val.dSet.Tables[0].Rows[0]["Role_Name"].ToString();
        ddlDepartment.SelectedValue = p_Val.dSet.Tables[0].Rows[0]["Deptt_ID"].ToString();
        foreach (GridViewRow row in GrdPermission.Rows)
        {
            Label lblModuleid = (Label)(row.Cells[0].FindControl("LblModuleID"));

            for (int i = 0; i < p_Val.dSet.Tables[0].Rows.Count; i++)
            {

                if (Convert.ToInt32(p_Val.dSet.Tables[0].Rows[i]["Module_Id"]) == Convert.ToInt16(lblModuleid.Text))
                {
                    CheckBox draft = (CheckBox)row.Cells[2].FindControl("chkDrft");
                    CheckBox review = (CheckBox)row.Cells[3].FindControl("chkRvw");
                    CheckBox publish = (CheckBox)row.Cells[4].FindControl("chkpblsh");
                    CheckBox Edit = (CheckBox)row.Cells[5].FindControl("chkEdit");
                    CheckBox delete = (CheckBox)row.Cells[6].FindControl("chkDelete");
                    CheckBox Repealed = (CheckBox)row.Cells[6].FindControl("chkRepealed");
                    CheckBox Hindi = (CheckBox)row.Cells[6].FindControl("chkHindi");
                    CheckBox English = (CheckBox)row.Cells[6].FindControl("chkEnglish");
                    draft.Checked = (bool)p_Val.dSet.Tables[0].Rows[i]["Draft"];
                    review.Checked = (bool)p_Val.dSet.Tables[0].Rows[i]["Review"];
                    publish.Checked = (bool)p_Val.dSet.Tables[0].Rows[i]["Publish"];
                    Edit.Checked = (bool)p_Val.dSet.Tables[0].Rows[i]["Edit"];
                    delete.Checked = (bool)p_Val.dSet.Tables[0].Rows[i]["Deleted"];
                    Repealed.Checked = (bool)p_Val.dSet.Tables[0].Rows[i]["Repealed"];
                    Hindi.Checked = (bool)p_Val.dSet.Tables[0].Rows[i]["Hindi"];
                    English.Checked = (bool)p_Val.dSet.Tables[0].Rows[i]["English"];
                }
            }
        }
        p_Val.dSet = null;
    }

    #endregion

    #region Function to update permissions

    public void Update_Permission()
    {
        UserOB obj_userOB2 = new UserOB();

        obj_UserOB.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.update);
        obj_UserOB.RoleId = Convert.ToInt32(Request.QueryString["Role_id"]);
        obj_UserOB.UpdatedBy = Convert.ToInt16(Session["User_Id"]);
        obj_UserOB.IpAddress = Miscelleneous_DL.getclientIP();
        obj_UserOB.RoleName = TxtName.Text;
       // obj_UserOB.UpdatedBy = 3;
        int chk = obj_roleBL.ASP_Role_Update(obj_UserOB);
        if (chk > 0)
        {
            foreach (GridViewRow row in GrdPermission.Rows)
            {
                int Module_Id = Convert.ToInt32(((Label)row.FindControl("LblModuleID")).Text.Trim());
                CheckBox draft = (CheckBox)row.FindControl("chkDrft");
                CheckBox review = (CheckBox)row.FindControl("chkRvw");
                CheckBox publish = (CheckBox)row.FindControl("chkpblsh");
                CheckBox Edit = (CheckBox)row.FindControl("chkEdit");
                CheckBox Delete = (CheckBox)row.FindControl("chkDelete");
                CheckBox Repealed = (CheckBox)row.FindControl("chkRepealed");
                CheckBox Hindi = (CheckBox)row.FindControl("chkHindi");
                CheckBox English = (CheckBox)row.FindControl("chkEnglish");
                obj_userOB2.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.update);
                obj_userOB2.RoleId = Convert.ToInt32(Request.QueryString["Role_id"]);
                obj_userOB2.ModuleId = Module_Id;
                obj_userOB2.draft = Convert.ToInt32(draft.Checked);
                obj_userOB2.review = Convert.ToInt32(review.Checked);
                obj_userOB2.publish = Convert.ToInt32(publish.Checked);
                obj_userOB2.edit = Convert.ToInt32(Edit.Checked);
                obj_userOB2.deleted = Convert.ToInt32(Delete.Checked);
                obj_userOB2.repealed = Convert.ToInt32(Repealed.Checked);
                if (Convert.ToBoolean(Hindi.Checked) == true || Convert.ToBoolean(English.Checked) == true)
                {
                    obj_userOB2.hindi = Convert.ToInt32(Hindi.Checked);
                    obj_userOB2.english = Convert.ToInt32(English.Checked);
                }
                else
                {
                    obj_userOB2.hindi = Convert.ToInt32(Hindi.Checked);
                    obj_userOB2.english = Convert.ToInt32(true);
                }
                p_Val.Result = obj_roleBL.ASP_Module_Role_Permission_Update(obj_userOB2);
                if (p_Val.Result > 0)
                {
                    ViewState["result"] = p_Val.Result.ToString();
                    
                }
            }
            p_Val.Result = Convert.ToInt16(ViewState["result"]);
            if (p_Val.Result > 0)
            {
                Session["msg"] = "Role has been updated successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Role/DisplayRole.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Role);
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
            }
        }
    }

    #endregion

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            obj_UserOB.ModuleId = Convert.ToInt16(Request.QueryString["ModuleID"]);
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
}

  