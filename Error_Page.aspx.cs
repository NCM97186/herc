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
using System.Security.Cryptography;
using System.Threading;
using System.Text;

public partial class Error_Page :BasePage
{
    #region Data declaration zone

    public static string Url = HttpContext.Current.Request.Url.ToString() + "?format=Print";
    Project_Variables p_var = new Project_Variables();  

    #endregion

    #region OnPreInit event zone

    protected override void OnPreInit(EventArgs e)
    {
         this.Master.FindControl("AccessibilityDiv").Visible = false;
        //this.Master.FindControl("menu").Visible = false;
       this.Master.FindControl("FooterDiv").Visible = false;
        base.OnPreInit(e);
    }

    #endregion

    #region Page load event zone
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            this.Page.Title = Resources.HercResource.Error;
        }
        if (Resources.HercResource.Lang_Id == "1")
        {
          
        }
        else
        {
          
        }
        byte[] delay = new byte[1];
        RandomNumberGenerator prng = new RNGCryptoServiceProvider();
        prng.GetBytes(delay);
        Thread.Sleep(Convert.ToInt32(delay[0]));
        IDisposable disposable = prng as IDisposable;
        if ((disposable != null))
        {
            disposable.Dispose();
        }
        ErrorMsg(Resources.HercResource.Lang_Id);
        
    }

    #endregion

    //Area for all the user-defined functions
    private void ErrorMsg(string LangId)
    {

        if (LangId == "1")
        {
            p_var.urlname = ResolveUrl("~/") + "index.aspx";
            p_var.msg = "It seems that the page you were looking for has been moved or is no longer there. Or maybe you just mistyped something. Please check to make sure you've typed the URL correctly.";
            p_var.sbuilder.Append(@"<span><strong>" + p_var.msg +
                    @"<br />
                    You could return to the <a href='"+ p_var.urlname +"'>"
                + Resources.HercResource.HomePage +
                @"</a>, or return <a href="""" style=""cursor: pointer;"" onclick=""javascript:history.back(); return false;"">"
                + Resources.HercResource.Back +
                @"</a> to the page.</strong></span>");
        }
        else
        {
            p_var.urlname = ResolveUrl("~/") + "content/Hindi/index.aspx";
            p_var.msg = "यह प्रतीत होता है कि जो पृष्ठ आप देख रहे थे वह अब वहाँ नहीं है या स्थानांतरित कर दिया गया है, या शायद आपने कुछ ग़लत लिखा है। कृपया सुनिश्चित करें कि आपने URL सही ढंग से टाइप किया है। ";
            p_var.sbuilder.Append(@"<span><strong>" + p_var.msg +
                    @"<br />
                    आप <a href='" +p_var.urlname + "'>"
                + Resources.HercResource.HomePage +
                @" </a>पर जाए या <a href="""" style=""cursor: pointer;"" onclick=""javascript:history.back(); return false;"">"
                + Resources.HercResource.Back +
                @"</a> पृष्ठ पर लौटें। </strong></span>");
        }
        ltrlErrorMsg.Text = p_var.sbuilder.ToString();
    }
}
