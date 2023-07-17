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

public partial class UserControl_homepage_logo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
 if (Resources.HercResource.Lang_Id == "1")
        {
            ibindiagov.ImageUrl = ResolveUrl("~/images/india.gov.in.jpg");
            ibindiagov.AlternateText = Resources.HercResource.IndianPortal;
            ibindiagov.ToolTip = "india.gov.in: This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.";
            ibindiagov.Width = 150;
            ibindiagov.Height = 70;

            ibDialgovEng.ImageUrl = ResolveUrl("~/images/dial.gov.in.jpg");
            ibDialgovEng.AlternateText = Resources.HercResource.DialGov;
            ibDialgovEng.ToolTip = "dial.gov.in: This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.";
            ibDialgovEng.Width = 150;
            ibDialgovEng.Height = 70;

            ibharyanagov.ImageUrl = ResolveUrl("~/images/haryana.gov.in.jpg");
            ibharyanagov.AlternateText = Resources.HercResource.HaryanaPortal;
            ibharyanagov.ToolTip = "haryana.gov.in: This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.";
            ibharyanagov.Width = 150;
            ibharyanagov.Height = 70;

            imgRTIEng.ImageUrl = ResolveUrl("~/images/rti.jpg");


        }
        else
        {
            ibindiagovHindi.ImageUrl =  ResolveUrl("~/images/bharat.gov.in.jpg");
            ibindiagovHindi.AlternateText = Resources.HercResource.IndianPortal;

            ibindiagovHindi.ToolTip = "Bharat.gov.in: यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।";
            ibindiagovHindi.Width = 150;
            ibindiagovHindi.Height = 68;

            ibDialgovHin.ImageUrl = ResolveUrl("~/images/dial.gov.in.jpg");
            ibDialgovHin.AlternateText = Resources.HercResource.DialGov;
            ibDialgovHin.ToolTip = "Bharat.gov.in: यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।";
            ibDialgovHin.Width = 150;
            ibDialgovHin.Height = 70;

            ibharyanagovHindi.ImageUrl =  ResolveUrl("~/images/haryana.gov.in.jpg");
            ibharyanagovHindi.AlternateText = Resources.HercResource.HaryanaPortal;
            ibharyanagovHindi.ToolTip = "haryana.gov.in: यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।";
            ibharyanagovHindi.Width = 150;
            ibharyanagovHindi.Height = 70;
            imgRTIHin.ImageUrl =  ResolveUrl("~/images/rti-in-hindi.jpg");
        }
    }
    protected void ibIndiaPortal_Click(object sender, ImageClickEventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            Response.Redirect("http://india.gov.in/");
        }
        else
        {
            Response.Redirect("http://bharat.gov.in/");
        }
    }

    protected void Dialgov_Click(object sender, ImageClickEventArgs e)
    {

       Response.Redirect("http://dial.gov.in/");
       
    }

    protected void DialgovHin_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("http://dial.gov.in/indexh.html");
    }

    protected void ibharyanagov_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("http://haryana.gov.in/");
    }
	    protected void imgRTIEng_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(ResolveUrl("~/rti/838_1_RTI.aspx"));
    }
    protected void imgRTIHin_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(ResolveUrl("~/content/Hindi/rti/839_1_RTI.aspx"));
    }
}
