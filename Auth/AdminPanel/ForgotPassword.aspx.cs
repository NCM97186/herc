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

public partial class Auth_AdminPanel_ForgotPassword : System.Web.UI.Page
{
    #region region to create class object
 
    string randumid = "", logoutPath = "";
    Random random = new Random();
    //string randumid = "", logoutPath = ""; 
    UserBL obj_UserBL = new UserBL();
    UserOB obj_UserOB = new UserOB();
    DataSet ds = new DataSet();

    #endregion 
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.ViewStateUserKey = Session.SessionID;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Page.Theme = "";
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                CaptchaControl1.ValidateCaptcha(txtCaptcha.Text.Trim());
                if ((CaptchaControl1.UserValidated))
                {
                }
                else
                {
                    lblmsg.Text = "This code has not been accepted please try again.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    txtCaptcha.Text = "";
                    txtUserName.Text = "";
                    return;
                }
                if (txtUserName.Text != "")
                {
                    obj_UserOB.UserName = txtUserName.Text;
                    ds = obj_UserBL.Proc_Select_USER_DETAIL_BY_Name(obj_UserOB);

                }


                if (ds.Tables[0].Rows.Count > 0)
                {
                    string userid = ds.Tables[0].Rows[0]["User_Id"].ToString();
                    string emailid = ds.Tables[0].Rows[0]["Email"].ToString();
                    string password = ds.Tables[0].Rows[0]["PASSWORD"].ToString();
                    string ran = random.Next(1000, 9999).ToString() + userid + random.Next(1000, 9999).ToString();

                    string strMSG = "</br><b>To reset your password please follow the below given link :-</b><br><a href=" + System.Configuration.ConfigurationManager.AppSettings["SITE_ADMIN_URL"].ToString() + "ConfirmEmail.aspx?uid=" + ran + "><b>" + System.Configuration.ConfigurationManager.AppSettings["SITE_ADMIN_URL"].ToString() + "ConfirmEmail.aspx</b></a>";
                    try
                    {
                        if (SendTime(Convert.ToInt32(ds.Tables[0].Rows[0]["User_Id"]), ran))
                        {
                            Miscelleneous_BL ObjMiscel_mail = new Miscelleneous_BL();
                            if (ObjMiscel_mail.SendEmailForLogin(emailid, "", "", "Forgot password", "no-reply.herc@nic.in", strMSG))
                            {
                                Session["msg"] = "Thank you, Your reset password link has been sent on your email.";
                                Session["Redirect"] = "~/Auth/AdminPanel/Login.aspx";
                                Response.Redirect("~/Auth/AdminPanel/ConfirmationPageAdmin.aspx");

                                //lblmsg.Text = "Thank you, Your reset password link has been sent on your email.";
                                //lblmsg.ForeColor = System.Drawing.Color.Green;
                                //txtCaptcha.Text = "";
                                //txtUserName.Text = "";
                            }
                            else
                            {
                                lblmsg.Text = "Email id does not exist.";
                                txtCaptcha.Text = "";
                                txtUserName.Text = "";
                                lblmsg.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            lblmsg.Text = "An error occured.";
                            txtCaptcha.Text = "";
                            txtUserName.Text = "";
                            lblmsg.ForeColor = System.Drawing.Color.Red;

                        }
                    }
                    catch (Exception exp)
                    {
                        lblmsg.Text = "An error has been occured";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        txtCaptcha.Text = "";
                        txtUserName.Text = "";
                        //Response.Write(exp.Message);
                    }

                }
                else
                {
                    lblmsg.Text = "User name or Email id does not exist";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    txtCaptcha.Text = "";
                    txtUserName.Text = "";
                    return;
                }


            }
            catch (Exception e2)
            {
                lblmsg.Text = "An error has been occured";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                txtCaptcha.Text = "";
                txtUserName.Text = "";

            }
        }
        else
        {
            txtCaptcha.Text = "";
            txtUserName.Text = "";

        }
    }

    protected bool SendTime(int id, string rand1)
    {
        obj_UserOB.ActionType = 1;
        obj_UserOB.IpAddress = Miscelleneous_DL.getclientIP();
        obj_UserOB.LOG_ID = id;
        obj_UserOB.RANDOMID = rand1;
        obj_UserOB.USER_TYPE = "Admin";
        return obj_UserBL.Proc_Insert_Into_RESETPASSWORD(obj_UserOB);


    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Auth/AdminPanel/login.aspx");
    }
}
