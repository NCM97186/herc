using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class Auth_AdminPanel_ResetPassword : System.Web.UI.Page
{
    #region variable declration

    UserOB obj_UserOB = new UserOB();
    UserBL obj = new UserBL();
    DataSet dSet = new DataSet();
    int Result;
    string randomid;
    int lenth = -1;
    int Uid = -1;

    #endregion 

    #region code for PreInIt event
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.ViewStateUserKey = Session.SessionID;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Page.Theme = "";
    }
    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        if (Request.QueryString["uid"] == null)
        {
            Response.Redirect("~/Auth/AdminPanel/login.aspx");
        }
        randomid = Convert.ToString(Request.QueryString["uid"]);
        lenth = randomid.Length;
        Uid = Convert.ToInt32(randomid.Substring(4, lenth - 8));
        CheckLink(Uid, randomid);
        Response.CacheControl = "no-cache";
        Response.Cache.SetExpires(System.DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                randomid = Convert.ToString(Request.QueryString["uid"]);
                lenth = randomid.Length;
                Uid = Convert.ToInt32(randomid.Substring(4, lenth - 8));
                CheckLink(Uid, randomid);
                CaptchaControl1.ValidateCaptcha(txtCaptcha.Text.Trim());

                if (CaptchaControl1.UserValidated)
                {

                    obj_UserOB.PassWord = txtNewPass.Text;
                    obj_UserOB.LOG_ID = Uid;
                    bool val = obj.Proc_Update_User_Password(obj_UserOB);
                    if (val == true)
                    {
                        SendTime(Uid, randomid);
                        Session["ResetMSG"] = "PWDCHANGED";
                        // Response.Redirect("Notification.aspx");
                        Session["msg"] = "Your password has been changed successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Login.aspx";
                        Response.Redirect("~/Auth/AdminPanel/message.aspx", false);
                    }

                }
                else
                {
                    lblmsg.Text = "This code has not been accepted please try again.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

            }
        }

        catch (Exception)
        {

        }

    }

    protected void CheckLink(int id, string rand1)
    {
        try
        {
            obj_UserOB.LOG_ID = id;
            obj_UserOB.RANDOMID = rand1;
            obj_UserOB.USER_TYPE = "Admin";
            string check = "";
            obj.Proc_Check_Password(obj_UserOB, ref check);
            if (check == "expired")
            {
                Session["ResetMSG"] = "TIMEOUT";
                //  Response.Redirect("Notification.aspx", false);
                pnlwelcome.Visible = true;
                pnlReset.Visible = false;
            }
            else if (check == "deactivated")
            {
                Session["ResetMSG"] = "ACTIVELINK";
                //Response.Redirect("Notification.aspx", false);
                pnlwelcome.Visible = true;
                pnlReset.Visible = false;
            }
            else if (check == "Timeout")
            {
                Session["ResetMSG"] = "TIMEOUT";
                pnlwelcome.Visible = true;
                pnlReset.Visible = false;
                // Response.Redirect("Notification.aspx", false);
            }
            else
            {
                pnlReset.Visible = true;
                pnlwelcome.Visible = false;
            }
        }
        catch (Exception)
        {
            Session["ResetMSG"] = "DISTURBANCE";
            Response.Redirect("Notification.aspx", false);
        }

    }


    protected bool SendTime(int id, string rand1)
    {
        obj_UserOB.LOG_ID = id;
        obj_UserOB.RANDOMID = rand1;
        obj_UserOB.USER_TYPE = "Admin";
        return obj.Proc_Update_Into_RESETPASSWORD(obj_UserOB);

    }
    protected void ServerValidation(object source, ServerValidateEventArgs args)
    {
        if (txtNewPass.Text == txtConfirmPass.Text)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }
}