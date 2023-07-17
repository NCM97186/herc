using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Configuration;
using System.Text.RegularExpressions;
using System.Linq;
using System.Globalization;

public partial class Auth_AdminPanel_testmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsms_Click(object sender, EventArgs e)
    {
        try
        {
            //SendsmsApprove("Test SMS","9910268862"," testing");

            string senderusername = "hrercw.auth";
            string senderpassword = "Lj$*12Qh";

            string senderid = "NICSMS";
            string sURL;
            string text = "test 3";
            string Msg = "test sms";
            string MobileNumber = "7503285978";


            WebClient client = new WebClient(); client.Headers.Add("user-agent", "Mozilla/4.0(compatible; MSIE 6.0; Windows NT 5.2; .NET CLR1.0.3705;)");

            string textmessage = HttpUtility.HtmlDecode(text + Msg) + Environment.NewLine;
            textmessage = ConvertStringToHex(textmessage);

            client.QueryString.Add("message", Msg);

            string baseurl = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + senderusername + "&pin=" + senderpassword + "&message=" + textmessage + "&mnumber=" + "91" + MobileNumber + "&signature=" + senderid + "&msgType=UC";

            BypassCertificateError();

            Stream data = client.OpenRead(baseurl);

            BypassCertificateError();

            StreamReader reader = new StreamReader(data);

            string s = reader.ReadToEnd();

            data.Close();

            reader.Close();
            lblmsg.Text = "Success";
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();

        }

    }
    public void SendsmsApprove(string Msg, string MobileNumber, string text)
    {
        
    }
    public static void BypassCertificateError()
    {

        ServicePointManager.ServerCertificateValidationCallback +=



          delegate(object sender,

               System.Security.Cryptography.X509Certificates.X509Certificate certificate,

               System.Security.Cryptography.X509Certificates.X509Chain chain,

                System.Net.Security.SslPolicyErrors sslPolicyErrors)
          {

              return true;

          };

    }


    public static string ConvertStringToHex(string asciiString)
    {
        string hex = "";
        foreach (char c in asciiString)
        {
            int tmp = c;
            hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString())).PadLeft(4, '0');
        }
        return hex;
    }
}