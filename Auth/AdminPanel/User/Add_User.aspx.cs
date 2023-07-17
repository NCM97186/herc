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


public partial class Auth_AdminPanel_User_Add_User : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declare zone

    #region variable declaration zone

    UserBL obj_UserBL = new UserBL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    DataSet ds = new DataSet();

    #endregion 

    //End

    //Area for Page load event zone

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
     {
       
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
            Get_Initial_Name();        // To get Initial Name
            
           //// Get_Country_Name();        // To get Country Name
            if (Request.QueryString["User_Id"] != null)
            {
                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Edit  User";
                this.Page.Title = "Edit User: HERC";

                btnUpdate.Visible = true;
                txtUserName.Attributes.Add("ReadOnly", "true");
                txtName.Attributes.Add("ReadOnly", "true");
                display(Request.QueryString["User_Id"].ToString());
                Get_Deptt_Name();          // To get Deptt Name
                Get_Role_Name(); 
                password.Visible = false;
                confirmpass.Visible = false;
            }
            else
            {
                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Add  User";
                this.Page.Title = "Add User: HERC";

                ViewState.Remove("departmentid");
                ViewState.Remove("roleid");
                Get_Deptt_Name();          // To get Deptt Name
                Get_Role_Name();           // To get Role Name
                btnUpdate.Visible = false;
            }
        }
    }

    #endregion 

    //End

    //Area for all buttons,linkButtons, imageButtons click events

    #region button btnSubmit click event to add new user

    protected void btnSubmit_Click(object sender, EventArgs e)
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
                        Add_User();
                    }
                    catch
                    {
                        throw;
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

    #region button btnCancel click event to cancel

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["User_Id"] == null)
        {
            //txtPassword.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContactno.Text = "";
            //txtConfirmPassword.Text = "";
            txtCity.Text = "";
            txtAddress.Text = "";
            txtUserName.Text = "";
            //15 Nov
            txtCountry.Text = "";
            ddlDeptname.SelectedIndex = 0;
            ddlRole.SelectedIndex = 0;
        }
        else
        {
            display(Request.QueryString["User_Id"].ToString());
        }
    }

    #endregion

    #region button btnBack click event to back

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/User/") + "View_Users.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.User));
    }

    #endregion

    #region button btnUpdate click event to update user

    protected void btnUpdate_Click(object sender, EventArgs e)
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
                       Updte_User();
                    }
                    catch
                    {
                        throw;
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

    //End

    //Area for all the user-defined functions

    #region Function to initial of name in dropDownlist

    public void Get_Initial_Name()
    {
        try
        {
            ds = obj_UserBL.ASP_Get_Initial_Name();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlInitial.DataSource = ds;
                ddlInitial.DataValueField = "Initial_Id";
                ddlInitial.DataTextField = "Initial_Name";
                ddlInitial.DataBind();
            }
            ddlInitial.Items.Insert(0, new ListItem("Select", "0"));
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
            ddlDeptname.Items.Insert(0, new ListItem("Select", "0"));
            if (ViewState["departmentid"] != null)
            {
                ddlDeptname.SelectedValue = ViewState["departmentid"].ToString();
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind role in dropDownlist

    public void Get_Role_Name()
    {
        try
        {
            ddlRole.Items.Clear();
            if (Convert.ToInt16(Session["User_ID"]) == 6)
            {
                obj_userOB.DepttId = Convert.ToInt16(ddlDeptname.SelectedValue);
            }
            else
            {
                obj_userOB.DepttId = Convert.ToInt16(Session["Dept_ID"]);
            }
            obj_userOB.UserId = Convert.ToInt16(Session["User_ID"]);
            ds = obj_RoleBL.ASP_Role_GetRoleName(obj_userOB);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlRole.DataSource = ds;
                ddlRole.DataValueField = "Role_Id";
                ddlRole.DataTextField = "Role_Name";
                ddlRole.DataBind();
            }
            ddlRole.Items.Insert(0, new ListItem("Select", "0"));
            if (ViewState["roleid"] != null)
            {
                ddlRole.SelectedValue = ViewState["roleid"].ToString();
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

 

    #region Function to add new user

    public void Add_User()
    {
        obj_userOB.ActionType = 1;
        if (obj_UserBL.CheckAvaliability(txtUserName.Text))
        {
            lblmsg.Text = "Username already exists please select another username";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
        obj_userOB.UserName = txtUserName.Text;
        obj_userOB.address = txtAddress.Text;
        obj_userOB.E_mail = txtEmail.Text;
        obj_userOB.PassWord = hfpwd.Value;
        obj_userOB.country = txtCountry.Text;
        if (txtContactno.Text != null && txtContactno.Text != "")
        {
            obj_userOB.ContactNo = Convert.ToDouble(txtContactno.Text);
        }
        else
        {
            obj_userOB.ContactNo = null;
        }
        //obj_userOB.ContactNo = Convert.ToDouble(txtContactno.Text);
        obj_userOB.city = txtCity.Text;
        obj_userOB.StatusId = 1;
        obj_userOB.NAME = txtName.Text;
        obj_userOB.InitialId = Convert.ToInt32(ddlInitial.SelectedValue);
        obj_userOB.DepttId = Convert.ToInt32(ddlDeptname.SelectedValue);
        obj_userOB.RoleId = Convert.ToInt32(ddlRole.SelectedValue);
        obj_userOB.InsertedBy =Convert.ToInt16( Session["User_ID"]);
        obj_userOB.IpAddress = Miscelleneous_DL.getclientIP();
        int check = obj_UserBL.ASP_User_Insert(obj_userOB);
        if (check > 0)
        {
            Session["msg"] = "User has been added successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/User/View_Users.aspx?ModuleID=" + Convert.ToInt16(Request.QueryString["ModuleID"]);
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        }
    }

    #endregion

    #region Function to display data in edit mode

    public void display(string User_Id)
    {
        DataSet ds1 = new DataSet();
        btnSubmit.Visible = false;
        obj_userOB.UserId = Convert.ToInt32(User_Id);
        ds1 = obj_UserBL.ASP_Get_User_Role_Display(obj_userOB);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            txtUserName.Text = ds1.Tables[0].Rows[0]["Username"].ToString();
            txtAddress.Text = ds1.Tables[0].Rows[0]["Address"].ToString();
            txtEmail.Text = ds1.Tables[0].Rows[0]["Email"].ToString();
            //txtPassword.Text = ds1.Tables[0].Rows[0]["Password"].ToString();
             txtCountry.Text = ds1.Tables[0].Rows[0]["Country"].ToString();
            txtContactno.Text = ds1.Tables[0].Rows[0]["Mobile_No"].ToString();
            txtCity.Text = ds1.Tables[0].Rows[0]["City"].ToString();
            txtName.Text = ds1.Tables[0].Rows[0]["Name"].ToString();
            ddlInitial.SelectedValue = ds1.Tables[0].Rows[0]["Initial_Id"].ToString();
            ViewState["departmentid"] = ds1.Tables[0].Rows[0]["Deptt_Id"].ToString();
            

            //Code is uncommented by birendra on date 03-12-2013

            ddlDeptname.SelectedValue = ds1.Tables[0].Rows[0]["Deptt_Id"].ToString();
            ddlDeptname.Enabled = false;
            ddlRole.SelectedValue = ds1.Tables[0].Rows[0]["Role_Id"].ToString();

            //End

            ViewState["roleid"] = ds1.Tables[0].Rows[0]["Role_Id"].ToString();
            if (ds1.Tables[0].Rows[0]["Status_Id"] != null)
            {
                ViewState["statusid"] = ds1.Tables[0].Rows[0]["Status_Id"].ToString();
            }
        }
    }

    #endregion

    #region Function to update user

    public void Updte_User()
    {
        obj_userOB.ActionType = 2;
        obj_userOB.UserName = txtUserName.Text;
        obj_userOB.address = txtAddress.Text;
        obj_userOB.E_mail = txtEmail.Text;
        //obj_userOB.PassWord = hfpwd.Value;
        obj_userOB.country = txtCountry.Text;
        if (txtContactno.Text != null && txtContactno.Text != "")
        {
            obj_userOB.ContactNo = Convert.ToDouble(txtContactno.Text);
        }
        else
        {
            obj_userOB.ContactNo = null;
        }
        //obj_userOB.ContactNo = Convert.ToDouble(txtContactno.Text);
        obj_userOB.city = txtCity.Text;
        if (ViewState["statusid"] != null)
        {
            obj_userOB.StatusId = Convert.ToInt16(ViewState["statusid"]);
        }
        else
        {
            obj_userOB.StatusId = 1;
        }
        obj_userOB.NAME = txtName.Text;
        obj_userOB.InitialId = Convert.ToInt32(ddlInitial.SelectedValue);
        obj_userOB.DepttId = Convert.ToInt32(ddlDeptname.SelectedValue);
        obj_userOB.RoleId = Convert.ToInt32(ddlRole.SelectedValue);
        obj_userOB.UpdatedBy = Convert.ToInt16(Session["User_ID"]);
        obj_userOB.UserId = Convert.ToInt32(Request.QueryString["User_Id"]);
        obj_userOB.IpAddress = Miscelleneous_DL.getclientIP();
        int check = obj_UserBL.ASP_User_Update(obj_userOB);
        if (check > 0)
        {
            Session["msg"] = "User has been updated successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/User/View_Users.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.User);
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

        }
    }

    #endregion

    //End

    //Area for all the dropDownlist events

    #region dropDownlist ddlDeptname selectedIndexChanged event zone

    protected void ddlDeptname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeptname.SelectedValue != "0")
        {
            Get_Role_Name();
        }
    }

    #endregion

    //End
}
