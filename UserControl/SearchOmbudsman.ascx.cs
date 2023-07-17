using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;

public partial class UserControl_SearchOmbudsman : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        ImgSearch.ToolTip = Resources.HercResource.Search;
    }



    //Area for imageButtons click events

    #region imageButton ibSearch click event to search

    protected void ImgSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect(String.Format("~/Ombudsman/SearchOmbudsman.aspx?q={0}&cx={1}&cof={2}", HttpUtility.UrlEncode(SanitizeUserInput(txtSearch.Text.Trim())), HttpUtility.UrlEncode(cx.Value), HttpUtility.UrlEncode(cof.Value)), false);
        }
        else
        {
            Response.Redirect(String.Format("~/OmbudsmanContent/Hindi/SearchOmbudsman.aspx?q={0}&cx={1}&cof={2}", HttpUtility.UrlEncode(SanitizeUserInput(txtSearch.Text.Trim())), HttpUtility.UrlEncode(cx.Value), HttpUtility.UrlEncode(cof.Value)), false);
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
            Response.Redirect(String.Format("~/Ombudsman/SearchOmbudsman.aspx?q={0}&cx={1}&cof={2}", HttpUtility.UrlEncode(SanitizeUserInput(txtSearch.Text.Trim())), HttpUtility.UrlEncode(cx.Value), HttpUtility.UrlEncode(cof.Value)), false);
        }
        else
        {
            Response.Redirect(String.Format("~/OmbudsmanContent/Hindi/SearchOmbudsman.aspx?q={0}&cx={1}&cof={2}", HttpUtility.UrlEncode(SanitizeUserInput(txtSearch.Text.Trim())), HttpUtility.UrlEncode(cx.Value), HttpUtility.UrlEncode(cof.Value)), false);
        }
    }
}