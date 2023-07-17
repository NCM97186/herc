using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NCM.DAL;
using System.Data.SqlClient;



public partial class AdminPanel_AdminLogin : System.Web.UI.Page
{
    #region region to create class object

    string randumid = "", logoutPath = "";
    Random random = new Random();
    //string randumid = "", logoutPath = ""; 
    UserBL obj_UserBL = new UserBL();
    UserOB obj_UserOB = new UserOB();
	Miscelleneous_BL obj_miscell = new Miscelleneous_BL();

    #endregion 

    #region code for PreInIt event

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.ViewStateUserKey = Session.SessionID;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Page.Theme = "";
    }

    #endregion 

    #region code for Page Load

    protected void Page_Load(object sender, EventArgs e)
    { 
	
	
	//txtPwd.Text="";
	
        //Used to get a collection of cookies sent by the client when page loads for first time
        this.Page.ClientScript.RegisterClientScriptInclude("SCRIPT1", ResolveUrl("~/js/sha512.js"));
        if (!IsPostBack)
        {
            Session["salt"] = random.Next(59999, 199999).ToString();
        }
      
    }

    #endregion

    #region code for sign in button click

    protected void btnLogin_Click(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();
       
        if (Session["Username"] != null)
        {
            if (Session["Username"].ToString() != txtUserName.Text)
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }
        }

        if (Session["salt"] == null)
        {
            Session.Abandon();
            Response.Redirect("~/Auth/adminpanel/Login.aspx");
        }

        Session["msg"] = "";
        HttpCookie cookie = new HttpCookie("Temp");
				string temp1 = random.Next(59999, 199999).ToString();
                string temp2 = random.Next(59999, 199999).ToString();
                string temp3 = random.Next(59999, 199999).ToString();
                string temp4 = random.Next(59999, 199999).ToString();
                string temp5 = random.Next(59999, 199999).ToString();
                string temp6 = random.Next(59999, 199999).ToString();
                Session["Temp"] = temp1 + temp2 + temp3 + temp4 + temp5 + temp6;
        cookie.Value = Session["Temp"].ToString();
        Response.Cookies.Add(cookie);
        CaptchaControl1.ValidateCaptcha(txtCaptcha.Text.Trim());

        if (CaptchaControl1.UserValidated)
        {
            obj_UserOB.UserName = txtUserName.Text.Trim();
            obj_UserOB.StatusId = 1;
            ds = obj_UserBL.ASP_Users_GetPassword(obj_UserOB);

            if (ds.Tables[0].Rows.Count > 0)
            {
                
                string password = ds.Tables[0].Rows[0]["Password"].ToString();
                string Tpass = hashcodegenerate.GetSHA512(ds.Tables[0].Rows[0]["Password"].ToString() + Session["salt"].ToString());

                string status = ds.Tables[0].Rows[0]["Status_Id"].ToString();
                string Upass = hfpwd.Value;

                if (status == "1")
                {
                    if (Tpass == Upass)
                    {
                        Session["User_Id"] = ds.Tables[0].Rows[0]["User_Id"].ToString();
                        Session["UserName"] = txtUserName.Text;
                        Session["Role_Id"] = ds.Tables[0].Rows[0]["Role_Id"].ToString();
                        Session["Dept_ID"] = ds.Tables[0].Rows[0]["deptt_id"].ToString();
                        obj_UserOB.UserId = Convert.ToInt16(ds.Tables[0].Rows[0]["User_Id"]);
                        obj_UserOB.moduleStatus = "Login";
                        obj_UserOB.IpAddress = obj_miscell.IpAddress();
                        obj_UserOB.AttemptSuccess = true;
                        HttpContext context = HttpContext.Current;
                        obj_UserOB.url = context.Request.Url.ToString();
                        obj_UserBL.Insert_AuditTrailLogin(obj_UserOB);
                        Session["Email"] = ds.Tables[0].Rows[0]["email"].ToString();
                        CustomPrincipalSerializer objSerializer = new CustomPrincipalSerializer();
                        objSerializer.Id = Session["User_Id"].ToString();
                        objSerializer.UserName = txtUserName.Text;
                        objSerializer.IsAdmin = 1;//Convert.ToInt16(user.IsAdmin);
                        int[] marks = new int[] { 99, 98, 92, 97, 95, 3 };
                        objSerializer.Modules = marks;
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        string userData = serializer.Serialize(objSerializer);
                        FormsAuthenticationTicket formAuthTicket = null;
                        formAuthTicket = new FormsAuthenticationTicket(1, objSerializer.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData);

                        string encformAuthTicket = FormsAuthentication.Encrypt(formAuthTicket);

                        HttpCookie formAuthCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encformAuthTicket);
                        System.Web.HttpContext.Current.Response.Cookies.Add(formAuthCookie);
                        Response.Redirect("DashBoard.aspx");

                    }
                    else
                    {
						obj_UserOB.UserId = Convert.ToInt16(Session["User_Id"]);
                        obj_UserOB.moduleStatus = "Login Attempt Failed";
                        obj_UserOB.IpAddress = obj_miscell.IpAddress();
                        obj_UserOB.AttemptSuccess = true;
                        HttpContext context = HttpContext.Current;
                        obj_UserOB.url = context.Request.Url.ToString();
                        obj_UserBL.Insert_AuditTrailLogin(obj_UserOB);
                        lblmsg.Text = "Invalid Username or Password.";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
						txtUserName.Text = "";
                    }
                }
                else
                {	
					obj_UserOB.UserId = Convert.ToInt16(Session["User_Id"]);
                    obj_UserOB.moduleStatus = "Login Attempt Failed";
                    obj_UserOB.IpAddress = obj_miscell.IpAddress();
                    obj_UserOB.AttemptSuccess = true;
                    HttpContext context = HttpContext.Current;
                    obj_UserOB.url = context.Request.Url.ToString();
                    obj_UserBL.Insert_AuditTrailLogin(obj_UserOB);
                    lblmsg.Text = "Invalid Username or Password.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    txtUserName.Text = "";
                    lblmsg.Text = " Not a valid user.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }

            }

            else
            {
                obj_UserOB.UserId = Convert.ToInt16(Session["User_Id"]);
                obj_UserOB.moduleStatus = "Login Attempt Failed";
                obj_UserOB.IpAddress = obj_miscell.IpAddress();
                obj_UserOB.AttemptSuccess = true;
                HttpContext context = HttpContext.Current;
                obj_UserOB.url = context.Request.Url.ToString();
                obj_UserBL.Insert_AuditTrailLogin(obj_UserOB);
				lblmsg.ForeColor = System.Drawing.Color.Red;
                txtUserName.Focus();
                txtUserName.Text = "";
                lblmsg.Text = "Invalid Username or Password.";
				txtUserName.Text = "";
                return;
            }

            
        }
        else
        {

            lblmsg.ForeColor = System.Drawing.Color.Red;
            txtCaptcha.Focus();
            lblmsg.Text = "Code is not matched. Please enter code again.";
            txtCaptcha.Text = "";
			txtUserName.Text = "";
            return;

        }
    }

    #endregion

    protected void LnkFrt_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Auth/adminpanel/ForgotPassword.aspx");
    }

   
}
