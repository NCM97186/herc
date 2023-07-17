using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
public partial class UserControl_Search : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
        {
            ImgSearch.ImageUrl = ResolveUrl("~/images/search.jpg");
        }
        else
        {
            ImgSearch.ImageUrl = ResolveUrl("~/images/search-hindi.jpg");
        }
        ImgSearch.ToolTip = Resources.HercResource.Search;
    }



    //Area for imageButtons click events

    #region imageButton ibSearch click event to search

    protected void ImgSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(String.Format("~/Search.aspx?q={0}&cx={1}&cof={2}", HttpUtility.UrlEncode(SanitizeUserInput(txtSearch.Text.Trim())), HttpUtility.UrlEncode(cx.Value), HttpUtility.UrlEncode(cof.Value)), false);
        }
        else
        {
            Response.Redirect(String.Format("~/content/Hindi/Search.aspx?q={0}&cx={1}&cof={2}", HttpUtility.UrlEncode(SanitizeUserInput(txtSearch.Text.Trim())), HttpUtility.UrlEncode(cx.Value), HttpUtility.UrlEncode(cof.Value)), false);
        }
    }

    #endregion

    //End

    //Area for all the user-defined functions

    private String SanitizeUserInput(String text)
    {
        if (String.IsNullOrEmpty(text))
        {
            return String.Empty;
        }

        String rxPattern = "<(?>\"[^\"]*\"|'[^']*'|[^'\">])*>";
        Regex rx = new Regex(rxPattern);
        String output = rx.Replace(text, String.Empty);

        return output;
    }

    //End
    protected void ImgSearch_Click(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(String.Format("~/Search.aspx?q={0}&cx={1}&cof={2}", HttpUtility.UrlEncode(SanitizeUserInput(txtSearch.Text.Trim())), HttpUtility.UrlEncode(cx.Value), HttpUtility.UrlEncode(cof.Value)), false);
        }
        else
        {
            Response.Redirect(String.Format("~/content/Hindi/Search.aspx?q={0}&cx={1}&cof={2}", HttpUtility.UrlEncode(SanitizeUserInput(txtSearch.Text.Trim())), HttpUtility.UrlEncode(cx.Value), HttpUtility.UrlEncode(cof.Value)), false);
        }
    }
}