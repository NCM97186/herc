<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.Net.Mail" %>
<%@ Import Namespace="System.Net" %>

<script runat="server">

    public static int count = 0;
    string lastcount = "";

    void Application_BeginRequest(object sender, EventArgs e)
    {
	 //if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("https://www.herc.gov.in") || HttpContext.Current.Request.Url.ToString().ToLower().Contains("164.100.90.38") || HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://www.herc.gov.in") || HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://herc.gov.in"))	

	//if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("164.100.90.38"))
       // {

       //     string postedURL = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
       //     HttpContext.Current.Response.Status = "301 Moved Permanently";
       //     HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace(postedURL, "https://herc.gov.in"));

       // }


        //10032018 HttpContext.Current.Response.AddHeader("x-frame-options", "SAMEORIGIN");
    }

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        Application["myCount"] = count;
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        HttpUnhandledException httpUnhandledException =
new HttpUnhandledException(Server.GetLastError().Message, Server.GetLastError());
        SendEmailWithErrors(httpUnhandledException.GetHtmlErrorMessage());

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        //// count = Convert.ToInt32(Application["myCount"]);
        Application["myCount"] = gethitcounts();

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.


    }


    private static void SendEmailWithErrors(string result)
    {
        try
        {
            MailMessage objMail = new MailMessage();
            //objMail.To.Add(new MailAddress(To));

            string[] tos = ConfigurationManager.AppSettings["adminEmail"].ToString().Split(';');

            for (int i = 0; i < tos.Length; i++)
            {

                objMail.To.Add(new MailAddress(tos[i]));

            }

            objMail.Subject = "Exception raised from IP : "+Miscelleneous_DL.getclientIP().ToString();

            objMail.From = new MailAddress(ConfigurationManager.AppSettings["from"].ToString());

            objMail.IsBodyHtml = true;
            objMail.Body = result;
            objMail.Priority = MailPriority.High;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["SMTP_Server"].ToString();
            smtp.Send(objMail);


        }
        catch (System.Web.HttpException ehttp)
        {
            // Write o the event log.
        }
    }

    #region Function to visitor coutner

    public string gethitcounts()
    {

        string lastcount = "";

        try
        {

            StreamReader SR = File.OpenText(Server.MapPath("~/WriteReadData/Hitcounter.txt"));

            string getcount = null;

            while ((getcount = SR.ReadLine()) != null)
            {

                lastcount = lastcount + getcount;

            }

            SR.Close();

            long newcount = Convert.ToInt64(lastcount);

            newcount++;

            TextWriter TxtWtr = new StreamWriter(Server.MapPath("~/WriteReadData/Hitcounter.txt"));

            TxtWtr.WriteLine(Convert.ToString(newcount));

            TxtWtr.Close();

            SR = File.OpenText(Server.MapPath("~/WriteReadData/Hitcounter.txt"));

            getcount = null;

            lastcount = "";

            while ((getcount = SR.ReadLine()) != null)
            {

                lastcount = lastcount + getcount;

            }

            SR.Close();

        }

        catch (Exception ex)
        {

            // TextWriter TxtWtr = new StreamWriter(Server.MapPath("~/WriteReadData/Hitcounter.txt"));

            // TxtWtr.WriteLine(Convert.ToString("1"));

            // TxtWtr.Close();

            lastcount = "1";

        }

        return lastcount;

    }

    #endregion

    protected void Application_PreSendRequestHeaders()
    {
        Response.Headers.Remove("Server");
        Response.Headers.Remove("X-AspNet-Version");
        Response.Headers.Remove("X-AspNetMvc-Version");
    }
    public override void Init()
    {
        this.PostAuthenticateRequest +=
            new EventHandler(MyOnPostAuthenticateRequestHandler);
        base.Init();
    }

    private void MyOnPostAuthenticateRequestHandler(object sender, EventArgs e)
    {
        
        if (FormsAuthentication.CookiesSupported == true)
        {

            HttpCookie formAuthCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (formAuthCookie != null)
            {
                //Decrypt the cookie value.  
                FormsAuthenticationTicket formAuthTicket = FormsAuthentication.Decrypt(formAuthCookie.Value);
                System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //Deserialize the cookie value  
                CustomPrincipalSerializer serializeModel = objSerializer.Deserialize<CustomPrincipalSerializer>(formAuthTicket.UserData);
                MyCustomPrincipal newUser = new MyCustomPrincipal(formAuthTicket.Name);
                newUser.Id = serializeModel.Id;
                newUser.UserName = serializeModel.UserName;
                newUser.IsAdmin = serializeModel.IsAdmin;
                newUser.UserType = serializeModel.UserType;
                // newUser.Modules = serializeModel.Modules;
                //Save details in the httpcontext  
                HttpContext.Current.User = newUser;
            }
        }
    }
    protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
    {
    }


</script>
