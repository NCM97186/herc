using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class Auth_AdminPanel_ConfirmEmail : System.Web.UI.Page
{
    #region variable declration

    UserOB obj_UserOB = new UserOB();
    UserBL obj = new UserBL();
    DataSet ds = new DataSet();
    int Result;
    string randomid;
    int lenth = -1;
    int Uid = -1;

    #endregion 
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.ViewStateUserKey = Session.SessionID;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Page.Theme = "";
    }
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
    protected void btnEmail_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                CaptchaControl1.ValidateCaptcha(txtCaptcha.Text.Trim());

                if (CaptchaControl1.UserValidated)
                {
                    obj_UserOB.LOG_ID = Uid;
                    ds = obj.Proc_Select_USER_DETAIL_BY_ID(obj_UserOB);
                    if (ds.Tables[0].Rows[0]["Email"].ToString() == txtEmail.Text)
                    {
                        Response.Redirect("~/Auth/AdminPanel/ResetPassword.aspx?uid=" + Request.QueryString["uid"]);

                    }
                    else
                    {
                        lblmsg.Text = "Please enter correct email address";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        return;

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
        catch
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
                pnlEmail.Visible = false;
            }
            else if (check == "deactivated")
            {
                Session["ResetMSG"] = "ACTIVELINK";
                //Response.Redirect("Notification.aspx", false);
                pnlwelcome.Visible = true;
                pnlEmail.Visible = false;
            }
            else if (check == "Timeout")
            {
                Session["ResetMSG"] = "TIMEOUT";
                pnlwelcome.Visible = true;
                pnlEmail.Visible = false;
                // Response.Redirect("Notification.aspx", false);
            }
            else
            {
                pnlEmail.Visible = true;
                pnlwelcome.Visible = false;
            }
        }
        catch (Exception)
        {
            Session["ResetMSG"] = "DISTURBANCE";
            Response.Redirect("Notification.aspx", false);
        }

    }
    protected void btnMyMail_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

        }

    }
  
}