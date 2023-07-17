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

public partial class UserControl_accessibility : System.Web.UI.UserControl
{
    #region declaration zone
    string strtextsize;
    string strtheme;
    // HttpCookie aCookie;
    #endregion
    private void Page_PreInit(object sender, System.EventArgs e)
    {
        Page.Theme = "Blue";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["HERCAccessibility"] != null)
        { 
            HttpCookie aCookie = Request.Cookies["HERCAccessibility"];
            SetCSS(aCookie.Values["textSize"]);

        }
        else
        {
            SetCSS("Medium");
            setCookies("Medium", "");

        }

       
        ibBlue.ToolTip = ibBlue.AlternateText = Resources.HercResource.Theme_Blue;
        hlScreeanReader.ToolTip = Resources.HercResource.ScreenReaderAccess;
        //ibHindi.ToolTip = Resources.HercResource.Language;
        if (Resources.HercResource.Lang_Id == "1")
        {
           hlScreeanReader.NavigateUrl = ResolveUrl("~/") + "ScreenReader.aspx";
            ibDecreaseFont.ImageUrl = "~/images/font-size-small.gif";
            ibNormalFont.ImageUrl = "~/images/font-normal-size.gif";
            ibIncreaseFont.ImageUrl = "~/images/font-size-big.gif";
            ibHighContrast.ImageUrl = "~/images/high_contrast.png";
            ibStandardContrast.ImageUrl = "~/images/standercontrast.png";
         // ibHindi.ImageUrl = "~/images/hindi.png";
           //ibHindi.Width = 30;

        }
        else
        {
            hlScreeanReader.NavigateUrl = ResolveUrl("~/") + "content/Hindi/ScreenReader.aspx";
            ibDecreaseFont.ImageUrl = "~/images/font-size-small_hi.png";
            ibNormalFont.ImageUrl = "~/images/font-normal-size_hi.png";
            ibIncreaseFont.ImageUrl = "~/images/font-size-big_hi.png";
            ibHighContrast.ImageUrl = "~/images/high_contrast_hi.png";
            ibStandardContrast.ImageUrl = "~/images/standercontrast_hi.png";
           // ibHindi.ImageUrl = "~/images/english-icon.gif";
            //ibHindi.Width = 40;
        }
        ibGreen.ToolTip = ibGreen.AlternateText = Resources.HercResource.Theme_Green;
        ibOrange.ToolTip = ibOrange.AlternateText = Resources.HercResource.Theme_Orange;
        ibBlue.ToolTip = ibBlue.AlternateText = Resources.HercResource.Theme_Blue;

        ibDecreaseFont.ToolTip = ibDecreaseFont.AlternateText = Resources.HercResource.Font_Decrease;
        ibIncreaseFont.ToolTip = ibIncreaseFont.AlternateText = Resources.HercResource.Font_Increase;
        ibNormalFont.ToolTip = ibNormalFont.AlternateText = Resources.HercResource.Font_Normal;

        ibHighContrast.ToolTip = ibHighContrast.AlternateText = Resources.HercResource.HightContrast;
        ibStandardContrast.ToolTip = ibStandardContrast.AlternateText = Resources.HercResource.StandardContrast;

        //ibHindi.ImageUrl = "~/images/" + Resources.ECoSFMResources.LangImageUrl;
       // ibHindi.ToolTip = ibHindi.AlternateText = Resources.HercResource.Language;

    }
    protected void ibBlue_Click(object sender, ImageClickEventArgs e)
    {
        HttpCookie aCookie = Request.Cookies["HERCAccessibility"];
        strtextsize = aCookie.Values["textSize"].ToString();
        setCookies(strtextsize, "Blue");
        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
    }
    protected void ibGreen_Click(object sender, ImageClickEventArgs e)
    {
        HttpCookie aCookie = Request.Cookies["HERCAccessibility"];
        strtextsize = aCookie.Values["textSize"].ToString();
        setCookies(strtextsize, "Green");
        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
    }
    protected void ibOrange_Click(object sender, ImageClickEventArgs e)
    {
        HttpCookie aCookie = Request.Cookies["HERCAccessibility"];
        strtextsize = aCookie.Values["textSize"].ToString();
        setCookies(strtextsize, "Orange");
        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
    }
    protected void ibDecreaseFont_Click(object sender, ImageClickEventArgs e)
    {
        HttpCookie aCookie = Request.Cookies["HERCAccessibility"];
        strtheme = aCookie.Values["Theme"].ToString();
        setCookies("Smaller", strtheme);
        Response.Redirect(Request.Url.ToString());
    }
    protected void ibNormalFont_Click(object sender, ImageClickEventArgs e)
    {
        HttpCookie aCookie = Request.Cookies["HERCAccessibility"];
        strtheme = aCookie.Values["Theme"].ToString();
        setCookies("Normal", strtheme);
        Response.Redirect(Request.Url.ToString());
    }
    protected void ibIncreaseFont_Click(object sender, ImageClickEventArgs e)
    {
        HttpCookie aCookie = Request.Cookies["HERCAccessibility"];
        strtheme = aCookie.Values["Theme"].ToString();
        setCookies("Larger", strtheme);
        Response.Redirect(Request.Url.ToString());
    }
    protected void ibHighContrast_Click(object sender, ImageClickEventArgs e)
    {
        HttpCookie aCookie = Request.Cookies["HERCAccessibility"];
        strtextsize = aCookie.Values["textSize"].ToString();
        setCookies(strtextsize, "HighContrast");
        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
    }
    protected void ibStandardContrast_Click(object sender, ImageClickEventArgs e)
    {
        strtheme = "Blue";
        setCookies("", strtheme);
        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);

    }

    public void setCookies(string textSize, string theme)
    {
        HttpCookie aCookie = new HttpCookie("HERCAccessibility");
        aCookie.Values["textSize"] = textSize;
        if (theme != "")
        {
            aCookie.Values["Theme"] = theme;
        }
        else
        {
            //DataSet dSet = new DataSet();
            //NCMdbAccess ncmdbObject = new NCMdbAccess();
            //dSet = ncmdbObject.ExecuteDataSet("sp_get_Theme_Name");
            //Page.Theme = "Green";
            if (theme == "")
            {
                theme = "Blue";
            }
            aCookie.Values["Theme"] = theme;
        }
        aCookie.Expires = DateTime.Now.AddDays(365);
        Response.Cookies.Add(aCookie);
    }

    public void SetCSS(string txtSize)
    {
        //SET TEXT SIZE
        string strtxtSize = string.Empty;
        if (Resources.HercResource.Lang_Id == "1")
        {
             strtxtSize = "86%";
            switch (txtSize)
            {
                case "Larger":
                    strtxtSize = "90%";
                    break;
                case "Normal":
                    strtxtSize = "86%";
                    break;
                case "Smaller":
                    strtxtSize = "80%";
                    break;

                default:
                    txtSize = "Normal";
                    strtxtSize = "86%";
                    break;
            }
        }
        else
        {
             strtxtSize = "86%";
            switch (txtSize)
            {
                case "Larger":
                    strtxtSize = "90%";
                    break;
                case "Normal":
                    strtxtSize = "86%";
                    break;
                case "Smaller":
                    strtxtSize = "84%";
                    break;

                default:
                    txtSize = "Normal";
                    strtxtSize = "86%";
                    break;
            }
        }
        if (Request.Cookies["HERCAccessibility"] != null)
        {
            string strtheme = "";
            HttpCookie aCookie = Request.Cookies["HERCAccessibility"];
            if (aCookie.Values["Theme"] != null)
            {
                strtheme = aCookie.Values["Theme"].ToString();
            }
            else
            {
                strtheme = "";
            }
            setCookies(txtSize, strtheme);
        }
        else
        {
            setCookies(txtSize, "");
        }

        Page.Header.Controls.Add(new LiteralControl("<style type=\"text/css\" media=\"screen\">" + Environment.NewLine.ToString() + " html, body{" + Environment.NewLine.ToString() + "" + Environment.NewLine.ToString() + "font-size:" + strtxtSize + ";" + Environment.NewLine.ToString() + "}" + Environment.NewLine.ToString() + "</style>"));
    }
    //protected void ibHindi_Click(object sender, ImageClickEventArgs e)
    //{
    //    if (Resources.HercResource.Lang_Id == "1")
    //    {
    //        Response.Redirect(ResolveUrl("~/content/Hindi/index.aspx"));
    //    }
    //    else
    //    {
    //        Response.Redirect(ResolveUrl("~/index.aspx"));
    //    }

    //}
}


