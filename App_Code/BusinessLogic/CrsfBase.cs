﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Security;
using System.Security.Policy;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class CrsfBase : System.Web.UI.Page
{

   // CommonFunctions cf = new CommonFunctions();
    // Expiration time of 12 hours.
    private static readonly TimeSpan ExpirationTime =
        new TimeSpan(0, 12, 0, 0);

    private string controlStateUserName;
    private DateTime controlStateGenerationDate;

    protected override void OnInit(EventArgs e)
    {

        //base.OnInit(e);
        if (true)
            ViewStateUserKey = Session.SessionID;
        this.RegisterRequiresControlState(this);
        base.OnInit(e);
    }

    protected override void LoadControlState(object savedState)
    {
        Pair controlStatePair = (Pair)savedState;

        Pair csrfData = (Pair)controlStatePair.First;

        this.controlStateUserName = (string)csrfData.First;
        this.controlStateGenerationDate = (DateTime)csrfData.Second;

        base.LoadControlState(controlStatePair.Second);
    }

    protected override void OnLoad(EventArgs e)
    {
        if (Session["User_Id"] == null)
            Response.Redirect("~/Auth/AdminPanel/Login.aspx");

        //set url referral
        //this.Request.Headers.Add("Referer", Convert.ToString(this.Page.Request.Url.AbsoluteUri));

        //string currentrequest = this.Request.UrlReferrer.ToString();
        if (!true)
        {
            FormsAuthentication.RedirectToLoginPage();
        }
        this.PreventPostbackCSRF();
        if (!this.IsPostBack)
        {
            //Code to check the csrf attack set session 
            Session["CurrentRequestUrl"] = HttpContext.Current.Request.Url.AbsoluteUri; //Convert.ToString(Request.ServerVariables["HTTP_REFERER"]);
            //string CustomViewStateUserKey =  string.Empty;
            Guid CustomViewStateUserKeyGuid;
            CustomViewStateUserKeyGuid = Guid.NewGuid();
            string CustomViewStateUserKey = ComputeSha256Hash(CustomViewStateUserKeyGuid.ToString());
            ((HiddenField)this.Master.FindControl("hdnblank")).Value = CustomViewStateUserKey;
            Session["AntiForgeryToken"] = CustomViewStateUserKey;
        }
        else
        {
            string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);
            string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
            if (CurrentSessionId != hdnblank)
            {
                Response.Redirect("~/Auth/AdminPanel/Login.aspx"); 
            }
            if (Session["CurrentRequestUrl"] != null)
            {
                // if (!Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
                // {
                //throw new Exception("Unathurize post request !");
                // }

            }

        }
        base.OnLoad(e);
    }

    protected override object SaveControlState()
    {
        // The control state is used to store user name and date time.
        // Control state is part of the view state, but it's impossible
        // to disable it, so this solution will also work when the
        // viewstate is disabled.
        string currentUserName = User.Identity.Name;
        DateTime controlStateGenerationTime = DateTime.Now;

        Pair csrfData =
            new Pair(currentUserName, controlStateGenerationTime);

        return new Pair(csrfData, base.SaveControlState());
    }

    private void PreventPostbackCSRF()
    {
        // Validate whether ViewState contains the MAC fingerprint
        // Without a fingerprint, it's impossible to prevent CSRF.
        if (!this.Page.EnableViewStateMac)
        {
            throw new InvalidOperationException(
                "The page does NOT have the MAC enabled and the view" +
                "state is therefore vurnerable to viewstate tampering.");
        }

        if (this.IsPostBack)
        {
            string currentlyLoggedInUserName =
                HttpContext.Current.User.Identity.Name;

            ValidateUserName(currentlyLoggedInUserName);

            ValidateGenerationDate();
        }
    }

    private void ValidateUserName(string loggedInUser)
    {
        bool userIsValid = this.controlStateUserName == loggedInUser;
        bool pageIsPublic = this.IsCurrentPagePublic(loggedInUser);

        if (!userIsValid && !pageIsPublic)
        {
            string message = string.Format(
                "A possible Cross-site Request Forgery attack " +
                "is detected. ViewState was generated by user " +
                "'{0}', but the current user is '{1}'.",
                controlStateUserName, loggedInUser);

            throw new SecurityException(message);
        }
    }

    private bool IsCurrentPagePublic(string loggedInUser)
    {
        // Because it's impossible to tamper with the view state, when
        // either the control state user name or the actual username
        // are null or empty, the current page must be a public page.
        return String.IsNullOrEmpty(this.controlStateUserName) ||
            String.IsNullOrEmpty(loggedInUser);
    }

    private void ValidateGenerationDate()
    {
        if (this.IsViewStateTooOld)
        {
            throw new SecurityException("The ViewState is expired.");
        }
    }

    private bool IsViewStateTooOld
    {
        get
        {
            DateTime expirationTime =
                this.controlStateGenerationDate + ExpirationTime;

            return expirationTime < DateTime.Now;
        }
    }

    static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256   
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
