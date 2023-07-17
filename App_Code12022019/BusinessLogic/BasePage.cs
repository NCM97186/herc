using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage:System.Web.UI.Page
{
	public BasePage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _pageTitle;
    private string _pageDescription;
    private string _pageKeywords;
    private string _MetaLang;
    private string _MetaTitle;

    public virtual string PageTitle
    {
        get
        {
            return _pageTitle;
        }
        set
        {
            _pageTitle = value;
        }
    }

    public virtual string PageDescription
    {
        get
        {
            return _pageDescription;
        }
        set
        {
            _pageDescription = Regex.Replace(value, "\\s+", " ");
        }
    }
    public virtual string MetaLang
    {
        get
        {
            return _MetaLang;
        }
        set
        {
            _MetaLang = Regex.Replace(value, "\\s+", " ");
        }
    }
    public virtual string MetaTitle
    {
        get
        {
            return _MetaTitle;
        }
        set
        {
            _MetaTitle = Regex.Replace(value, "\\s+", " ");
        }
    }
    public virtual string PageKeywords
    {
        get
        {
            return _pageKeywords;
        }
        set
        {
            _pageKeywords = Regex.Replace(value, "\\s+", " ");
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        // Default Page Title & Meta Tags
        
        this.Page.Title = "Haryana Electricity Regulatory Commission";
        _pageKeywords = "HERC";
        _MetaTitle = "HERC";
        _MetaLang = "en";
        _pageDescription = "HERC";

        PageMetaTags();
        //Set Page Title
        if (string.IsNullOrEmpty(PageTitle))
        {
            _pageTitle = this.Page.Title;
        }
        //Set Page Meta Keywords
        if (!String.IsNullOrEmpty(PageKeywords))
        {
            HtmlMeta metaTag = new HtmlMeta();
            metaTag.Name = "Keywords";
            metaTag.Content = _pageKeywords;
            Page.Header.Controls.Add(metaTag);
        }
        //Set Page Meta Description
        if (!String.IsNullOrEmpty(PageDescription))
        {
            HtmlMeta metaTag = new HtmlMeta();
            metaTag.Name = "Description";
            metaTag.Content = _pageDescription;
            Page.Header.Controls.Add(metaTag);
        }
        if (!String.IsNullOrEmpty(MetaLang))
        {
            HtmlMeta metaTag = new HtmlMeta();
            metaTag.Name = "Meta Language";
            metaTag.Content = _MetaLang;
            Page.Header.Controls.Add(metaTag);
        }
        if (!String.IsNullOrEmpty(MetaTitle))
        {
            HtmlMeta metaTag = new HtmlMeta();
            metaTag.Name = "Meta Title";
            metaTag.Content = _MetaTitle;
            Page.Header.Controls.Add(metaTag);
        }
        // Call System.Web.UI.Page.OnPreRender
        base.OnPreRender(e);
    }
    protected virtual void PageMetaTags()
    {
        //
        // TODO: Add logic here
        //        
    }

    private void Page_PreInit(object sender, System.EventArgs e)
    {
        if ((Request.Cookies["HERCAccessibility"] != null))
        {
            HttpCookie aCookie = Request.Cookies["HERCAccessibility"];
            Page.Theme = aCookie.Values["Theme"];
            ViewState["Blue"] = Page.Theme;
        }
        else
        {
            Page.Theme = "Blue";
        }
    }

    private void Page_Load(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(Session["Username"].ToString()))
        //{
        //    Response.Redirect("~/index.aspx", false);
        //}
    }
    protected override void InitializeCulture()
    {
        string controlID = System.Web.HttpContext.Current.Request.Url.ToString();
         // Comment By Gaurav
          //  string controlID1 = RewriteModule.RewriteContext.Current.Params["menuid"];
        if (controlID.Contains("Hindi"))
        {
            SetCulture("hi-IN", "hi-IN");
        }
        else
        {
            SetCulture("en-US", "en-US");
        }
        if (Session["MyUICulture"] != null && Session["MyCulture"] != null)
        {
            Thread.CurrentThread.CurrentUICulture = (CultureInfo)Session["MyUICulture"];
            Thread.CurrentThread.CurrentCulture = (CultureInfo)Session["MyCulture"];
        }
        base.InitializeCulture();
    }

    protected void SetCulture(string name, string locale)
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(name);
        Thread.CurrentThread.CurrentCulture = new CultureInfo(locale);
        Session["MyUICulture"] = Thread.CurrentThread.CurrentUICulture;
        Session["MyCulture"] = Thread.CurrentThread.CurrentCulture;
    }
}
