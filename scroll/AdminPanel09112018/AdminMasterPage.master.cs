using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

public partial class AdminPanel_AdminMasterPage : System.Web.UI.MasterPage
{
    
    #region Data Declaration Zone

    public string AdminUrl = "";
    string randumid = "", logoutPath = "";
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    UserBL obj_UserBL = new UserBL();
    UserOB obj_UserOB = new UserOB();
    DataSet ds = new DataSet();
    Project_Variables P_Val = new Project_Variables();
    Random random = new Random();
    Miscelleneous_BL obj_miscell = new Miscelleneous_BL();
    #endregion

    //End

    #region Page_Init event zone
   protected void Page_Init(object sender, EventArgs e)
    {
     

        if (HttpContext.Current.Cache["browser"] != Session["browser"])
        {

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Cache-Control", "no-cache");
            Response.CacheControl = "no-cache";
            Response.Expires = -1;
            Response.ExpiresAbsolute = new DateTime(1900, 1, 1);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Session.Abandon();
            Response.Redirect("~/Auth/AdminPanel/Login.aspx");
        }
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //if (Session["salt"] != null)
        //{
        //    Page.ViewStateUserKey = Session["salt"].ToString();
        //}

    
}

    #endregion

    //Area for page load event zone

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Role_Id"] == null || Session["Role_Id"].ToString() == "0" || Session["Role_Id"].ToString() == "")
        {
            Session.Abandon();
            Response.Redirect("~/Auth/AdminPanel/Login.aspx");
        }
        if (Convert.ToInt16(Session["Role_Id"].ToString()) == 18)
        {
            LiAudit.Visible = true;
        }
        else
        {
            LiAudit.Visible = false;
        }
		
        Page.ClientScript.RegisterClientScriptInclude("SCRIPT1", ResolveUrl("js/jquery-1.js"));
        Page.ClientScript.RegisterClientScriptInclude("SCRIPT2", ResolveUrl("js/admin.js"));
       // Page.ClientScript.RegisterClientScriptInclude("SCRIPT4", ResolveUrl("js/jquery.js"));
        Page.ClientScript.RegisterClientScriptInclude("SCRIPT4", ResolveUrl("js/jquery-3.2.1.min.js"));
        Page.ClientScript.RegisterClientScriptInclude("SCRIPT5", ResolveUrl("js/jquery_002.js"));
        if (!IsPostBack)
        {
            False_all();
            chk_privilages();
            getusername();
            getModulename();
        }

        // This is for Session Expire

       


        //End

        if (Session["Temp"] != null)
        {
            if (Request.Cookies["Temp"] == null || !Request.Cookies["Temp"].Value.Equals(Session["Temp"].ToString()))
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }
            else
            {
                // Session["Temp"] = random.Next(59999, 199999).ToString();
               string temp1 = random.Next(59999, 199999).ToString();
                string temp2 = random.Next(59999, 199999).ToString();
                string temp3 = random.Next(59999, 199999).ToString();
                string temp4 = random.Next(59999, 199999).ToString();
                string temp5 = random.Next(59999, 199999).ToString();
                string temp6 = random.Next(59999, 199999).ToString();
                Session["Temp"] = temp1 + temp2 + temp3 + temp4 + temp5 + temp6;
                HttpCookie cookie = new HttpCookie("Temp", Session["Temp"].ToString());
                Response.Cookies.Add(cookie);
            }
        }

        Response.CacheControl = "no-cache";
        Response.Cache.SetExpires(System.DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();


        Page.ClientScript.RegisterClientScriptInclude("SCRIPT1", ResolveUrl("js/jquery-1.js"));

        Page.ClientScript.RegisterClientScriptInclude("SCRIPT2", ResolveUrl("js/admin.js"));

        //Page.ClientScript.RegisterClientScriptInclude("SCRIPT4", ResolveUrl("js/jquery.js"));
        Page.ClientScript.RegisterClientScriptInclude("SCRIPT4", ResolveUrl("js/jquery-3.2.1.min.js"));

        Page.ClientScript.RegisterClientScriptInclude("SCRIPT5", ResolveUrl("js/jquery_002.js"));
       
        this.Page.MaintainScrollPositionOnPostBack = true;

        Page.Header.DataBind();
        //LblUserName.Text = Session["UserName"].ToString();
    }

    #endregion 

    //End

    //Area for all the buttons, linkButtons, imageButtons click event

    #region  linkButton lnkLogout click event to logout from admin

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Random Rand = new Random();

        obj_UserOB.UserId = Convert.ToInt16(Session["User_Id"]);
        obj_UserOB.moduleStatus = "Logout";
        obj_UserOB.IpAddress = obj_miscell.IpAddress();
        obj_UserOB.AttemptSuccess = true;
        HttpContext context = HttpContext.Current;
        obj_UserOB.url = context.Request.Url.ToString();
        obj_UserBL.Insert_AuditTrailLogin(obj_UserOB);

        randumid = Rand.Next(59999, 199999).ToString();
        logoutPath = ResolveUrl("~/Auth/AdminPanel/Logout.aspx?id=" + randumid);
        Response.Redirect(ResolveUrl(logoutPath), false);
    }

    #endregion 

    //End

    //Area for all the user-defined functions

    #region Function to mak visible false to li menu

    public void False_all()
    {
        AnnualReportli.Visible      = false;
        
        Notificationli.Visible      = false;
        publicli.Visible            = false;
     
        Vacancyli.Visible           = false;
        Menuli.Visible              = false;
        Discussionli.Visible        = false;
        Userli.Visible              = false;
        Roleli.Visible              = false;
        sohli.Visible               = false;
        Bannerli.Visible            = false;
        tariffli.Visible            = false;
        Ordersli.Visible            = false;
        Petitionli.Visible = false;
        Appealli.Visible = false;
        Awardli.Visible = false;
        RTIli.Visible = false;
        //moduleli.Visible = false;
        Profilesli.Visible = false;
        Reportsli.Visible = false;
		whatsNewli.Visible = false;
    }

    #endregion

    #region Function to check the permisions (Privilages)

    public void chk_privilages()
    {
        DataSet dsprv = new DataSet();
        obj_UserOB.RoleId =Convert.ToInt32(Session["Role_Id"]);
        dsprv = obj_RoleBL.ASP_CheckPrivilagesALL_For_Master(obj_UserOB);
        if (dsprv.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsprv.Tables[0].Rows.Count; i++)
            {
                if (dsprv.Tables[0].Rows[i][1].ToString().Equals("1"))
                {
                    Menuli.Visible = true;
                }
                if (dsprv.Tables[0].Rows[i][1].ToString().Equals("3"))
                {
                    Petitionli.Visible = true;

                }
                if (dsprv.Tables[0].Rows[i][1].ToString().Equals("4"))
                {
                    Appealli.Visible = true;
                }
                if (dsprv.Tables[0].Rows[i][1].ToString().Equals("5"))
                {
                    RTIli.Visible = true;
                }


                if (dsprv.Tables[0].Rows[i][1].ToString().Equals("6"))
                {
                    AnnualReportli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("7"))
                {
                   whatsNewli.Visible=true ;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("8"))
                {
                    sohli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("9"))
                {
                    publicli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("10"))
                {
                    Notificationli.Visible = true;
                }
                //else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("11"))
                //{
                //    Licensesli.Visible = true;
                //}
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("12"))
                {
                    Discussionli.Visible= true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("13"))
                {
                    Vacancyli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("14"))
                {
                    Roleli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("15"))
                {
                    Userli.Visible = true;
                }
                
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("17"))
                {
                    Ordersli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("18"))
                {
                    Bannerli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("19"))
                {
                    Awardli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("20"))
                {
                   // moduleli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("27"))
                {
                    tariffli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("28"))
                {
                    Profilesli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("29"))
                {
                    Reportsli.Visible = true;
                }
               
            }
        }
    }

    #endregion

    #region Function to display username on admin-side

    public void getusername()
    
    {
        try
        {
            if (Session["User_Id"] != null)
            {
                obj_UserOB.UserName= Session["Username"].ToString();
            }
            obj_UserOB.StatusId= 1;
            ds = obj_UserBL.ASP_Users_GetPassword(obj_UserOB);
            if (Session["User_Id"] != null)
            {
                if (ds != null & ds.Tables[0].Rows.Count > 0)
                {
                    ltrladminame.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    LblUserName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                }
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to display modulename on admin-side

    public void getModulename()
    {
        try
        {

            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"].Trim() != "20" && Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"].Trim() != "3" && Request.QueryString["ModuleID"].Trim() != "4" && Request.QueryString["ModuleID"].Trim() != "10" && Request.QueryString["ModuleID"].Trim() != "18" && Request.QueryString["ModuleID"].Trim() != "27" && Request.QueryString["ModuleID"].Trim() != "6" && Request.QueryString["ModuleID"].Trim() != "12" && Request.QueryString["ModuleID"].Trim() != "13" && Request.QueryString["ModuleID"].Trim() != "15" && Request.QueryString["ModuleID"].Trim() != "14" && Request.QueryString["ModuleID"].Trim() != "28" && Request.QueryString["ModuleID"].Trim() != "1" && Request.QueryString["ModuleID"].Trim() != "9" && Request.QueryString["ModuleID"].Trim() != "8" && Request.QueryString["ModuleID"].Trim() != "17" && Request.QueryString["ModuleID"].Trim() != "5" && Request.QueryString["ModuleID"].Trim() != "19" && Request.QueryString["ModuleID"].Trim() != "29")
            {
                int moduleid = Convert.ToInt16(Request.QueryString["ModuleID"]);
                obj_UserOB.ModuleId = moduleid;
                P_Val.dSet = obj_RoleBL.Asp_Module_GetModuleName(obj_UserOB);
                lblModulename.Text = " : " + P_Val.dSet.Tables[0].Rows[0]["Module_Name"].ToString() + " " + "Management";
            }

            //if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"].Trim() == "20")
            //{
            //    lblModulename.Text = " : " + "Regulations/Codes/Standards/Policies/Guidelines";
            //}
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End
}
