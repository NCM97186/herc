using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using NCM.DAL;

public partial class Change_Password : CrsfBase //System.Web.UI.Page
{

   
    #region variable declaration zone

    UserBL obj_usermgmtBL = new UserBL();
    UserOB obj_usermgmtOB = new UserOB();
    Project_Variables P_Var = new Project_Variables();
    UserOB obj_userentity = new UserOB();

    #endregion 
     
    #region Page Load zone 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User_Id"] == null)
        {
            Response.Redirect("~/Auth/AdminPanel/Login.aspx");
        }

        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": Change Password";
        this.Page.Title = "Change Password: HERC";

    }

    #endregion 


    // Area of Button click event 

    #region  Update Button click

    protected void update_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if ((Page.IsValid))
                {
                    CaptchaControl1.ValidateCaptcha(txtCaptcha.Text.Trim());
                    if ((CaptchaControl1.UserValidated))
                    {
                        obj_usermgmtOB.UserName = Session["Username"].ToString();

                        obj_usermgmtOB.StatusId = 1;

                        P_Var.dSet = obj_usermgmtBL.ASP_Users_GetPassword(obj_usermgmtOB);
                        if ((P_Var.dSet.Tables[0].Rows.Count > 0))
                        {

                            P_Var.str = P_Var.dSet.Tables[0].Rows[0]["Password"].ToString();
                            if (hidOldPassword.Value == P_Var.str)
                            {

                                obj_userentity.LOG_ID = Convert.ToInt32(P_Var.dSet.Tables[0].Rows[0]["User_Id"]);
                                obj_userentity.PassWord = hfpwd.Value;
                                obj_userentity.IpAddress = Miscelleneous_DL.getclientIP();
                                bool check = obj_usermgmtBL.Proc_Update_User_Password(obj_userentity);
                                if (check == true)
                                {
                                    Session["msg"] = "Password Changed successfully.";
                                    Session["Redirect"] = "~/Auth/AdminPanel/Login.aspx";
                                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPageAdmin.aspx");

                                }
                                else
                                {
                                    Session["msg"] = "New password cannot be same as the old password.";
                                    Session["Redirect"] = ResolveUrl("~/Auth/AdminPanel/Login.aspx");
                                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPageAdmin.aspx");

                                }
                            }
                            else
                            {
                                Session["msg"] = "Please Enter Your Correct Password.";
                                Session["Redirect"] = "~/Auth/AdminPanel/ChangePassword.aspx";
                                Response.Redirect("~/Auth/AdminPanel/ConfirmationPageAdmin.aspx");

                            }

                        }
                        else
                        {
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Invalid password!!";
                        }
                    }
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    txtCaptcha.Focus();
                    lblmsg.Text = "Code is not matched. Please try again.";
                    return;
                }
            }
        }
    }
    #endregion 

    #region Button click event for reset

    protected void reset_Click1(object sender, EventArgs e)
    {
       // OldPassword.Text = "";
       // NewPassword.Text = "";
       // ConfirmPassword.Text = "";
        lblmsg.Text = "";
    }

    #endregion 

    //End
}
