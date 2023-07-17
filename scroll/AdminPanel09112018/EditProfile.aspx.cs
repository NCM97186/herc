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

public partial class Auth_AdminPanel_EditProfile : CrsfBase
{


    #region variable declaration zone

    UserBL obj_UserBL = new UserBL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    DataSet ds = new DataSet();
    Project_Variables p_var = new Project_Variables();
    Miscelleneous_BL obj_miscel = new Miscelleneous_BL();
    #endregion

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Username"] == null)
        {
            Response.Redirect("~/Auth/AdminPanel/Login.aspx");
        }
        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": Edit Profile";
        this.Page.Title = "Edit Profile: HERC";
        if (!IsPostBack)
        {
            Get_Initial_Name();        // To get Initial Name  
            GetUserInformation();
        }
        Boolean b = false;
        XssValidation(Form.Controls, ref b);
        if (b == true)
        {

        }
    }

    #endregion

    #region Button click event zone

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {

                if (Page.IsValid)
                {
                    UpdateUserInformation();
                }
            }
			
        }
		else{
			Session.Abandon();
		    Response.Redirect("~/Auth/AdminPanel/Login.aspx");
			}
    }
    protected void Btnreset_Click(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        GetUserInformation();
    }

    #endregion



    #region Function To get Initial Name

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



    #region function to display user information

    public void GetUserInformation()
    {
        obj_userOB.UserId = Convert.ToInt32(Session["User_Id"]);
        p_var.dSet = obj_UserBL.ASP_Get_User_Role_Display(obj_userOB);
        TxtUsername.Text = p_var.dSet.Tables[0].Rows[0]["Username"].ToString();
        txtAddress.Text = p_var.dSet.Tables[0].Rows[0]["Address"].ToString();
        txtCity.Text = p_var.dSet.Tables[0].Rows[0]["City"].ToString();
        txtContactno.Text = p_var.dSet.Tables[0].Rows[0]["Mobile_No"].ToString();
        txtEmail.Text = p_var.dSet.Tables[0].Rows[0]["Email"].ToString();
        txtName.Text = p_var.dSet.Tables[0].Rows[0]["Name"].ToString();
        ddlInitial.SelectedValue = p_var.dSet.Tables[0].Rows[0]["Initial_Id"].ToString();
        txtCountry.Text = p_var.dSet.Tables[0].Rows[0]["Country"].ToString();
    }

    #endregion

    #region function to Update user information

    public void UpdateUserInformation()
    {
        try
        {
            obj_userOB.ActionType = Convert.ToInt32(Module_ID_Enum.Project_Action_Type.update);
            obj_userOB.UserId = Convert.ToInt32(Session["User_Id"]);
            obj_userOB.StatusId = 1;
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.UserName = TxtUsername.Text;
            obj_userOB.address = txtAddress.Text;
            obj_userOB.E_mail = txtEmail.Text;
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
            obj_userOB.NAME = txtName.Text;
            obj_userOB.DepttId = Convert.ToInt32(Session["Dept_ID"]);
            obj_userOB.InitialId = Convert.ToInt32(ddlInitial.SelectedValue);
            obj_userOB.UpdatedBy = Convert.ToInt32(Session["User_Id"]);
            obj_userOB.RecUpdateDate = System.DateTime.Now;
            obj_userOB.country = txtCountry.Text;
            obj_userOB.IpAddress = obj_miscel.IpAddress();
            p_var.Result = obj_UserBL.ASP_User_Update(obj_userOB);

            if (p_var.Result > 0)
            {
                Session["msg"] = "User Updated Successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/DashBoard.aspx";
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

            }
            else
            {
                Session["msg"] = "User is not Updated.";
                Session["Redirect"] = "~/Auth/AdminPanel/DashBoard.aspx";
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    public static void XssValidation(ControlCollection _allTxtCtrls, ref bool isXss)
    {
        foreach (Control ctrl in _allTxtCtrls)
        {
            if (ctrl is TextBox)
            {
                if (((TextBox)ctrl).Text.Contains("script") == true)
                {
                    ((TextBox)ctrl).Text = "error";
                    isXss = true;
                    return;
                }
            }
            XssValidation(ctrl.Controls, ref isXss);
        }
    }



}
