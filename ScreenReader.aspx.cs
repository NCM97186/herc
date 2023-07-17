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

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.IO;
using iTextSharp.text.html;
using iTextSharp.text.xml;
using iTextSharp.text.html.simpleparser;
using System.Text.RegularExpressions;


public partial class ScreenReader : BasePage
{
    //Area for all the variables declaration zone

    #region Data declaration zone

    Project_Variables p_var = new Project_Variables();
    public static string UrlPrint = string.Empty;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;

    #endregion

    //End

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.ScreenReaderAccess;
        }
        else
        {
            PageTitle = Resources.HercResource.ScreenReaderAccess;
        }
        if (!IsPostBack)
        {

            string str = BreadcrumDL.DisplayBreadCrum(Resources.HercResource.ScreenReaderAccess);
            ltrlBreadcrum.Text = str;
            MainContent(Resources.HercResource.Lang_Id);

        }
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			 ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);
        }
    }

    #endregion

    #region Function to set content

    private void MainContent(string LangId)
    {
        if (Convert.ToInt16(LangId) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {
            p_var.pagetitle = "This website complies with Guidelines for Indian Government Websites. This enables people with visual impairments access the website using assistive technologies, such as screen readers. The information on the website is accessible with different screen readers such as JAWS, NVDA, Supernova and Window-Eyes. Following table lists the information about different screen readers:";
            p_var.str = "Information related to the various screen readers";
            p_var.sbuilder.Append(@"<p>" + p_var.pagetitle + "</p><br /><p><strong>" + p_var.str + "</strong></p><br />");
            p_var.sbuilder.Append(@"<table width=""100%"" title=""Screen Reader Access"" cellspacing=""0"" cellpadding=""0"" border=""0"" class=""border"" summary=""List of various screen readers"" align=""center"">
                    <tbody>
                        <tr>
                            <th width=""16%"">
                                Screen Reader
                            </th>
                            <th width=""15%"" class=""last1"">
                                Website
                            </th>
                            <th width=""15%"">
                                Free / Commercial
                            </th>

                            <tr>
                            <td>
                                Non Visual Desktop Access (NVDA)
                            </td>
                            <td>
                                <a title=""This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop."" href=""http://www.nvaccess.org/"" target=""_blank"" onclick=""javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.');"">http://www.nvaccess.org//</a><img width=""14"" height=""14"" src=""/../images/external.png"" alt=""""  />
                            </td>
                            <td align=""center"">
                                Free
                            </td>
                        </tr>
                        <tr>
                            <td>
                                System Access To Go
                            </td>
                            <td>
                                http://www.satogo.com
                            </td>
                            <td align=""center"">
                                Free
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Thunder
                            </td>
                            <td>
                                <a title=""This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop."" href=""http://www.webbie.org.uk/thunder/"" target=""_blank"" onclick=""javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.');"">http://www.webbie.org.uk/thunder/</a><img width=""14"" height=""14"" src=""/../images/external.png"" alt="""" />
                            </td>
                            <td align=""center"">
                                Free
                            </td>
                        </tr>
                        <tr>
                            <td>
                                WebAnywhere
                            </td>
                            <td>
                                http://webanywhere.cs.washington.edu
                            </td>
                            <td align=""center"">
                                Free
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                JAWS
                            </td>
                            <td>
                                <a title=""This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop."" href=""http://www.freedomscientific.com"" target=""_blank"" onclick=""javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.');"">http://www.freedomscientific.com</a><img width=""14"" height=""14"" src=""/../images/external.png"" alt="""" />
                            </td>
                            <td align=""center"">
                                Commercial
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Supernova
                            </td>
                            <td>
                                <a title=""This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop."" href=""http://www.yourdolphin.com/"" target=""_blank"" onclick=""javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.');"">http://www.yourdolphin.com/</a><img width=""14"" height=""14"" src=""/../images/external.png"" alt="""" />
                            </td>
                            <td align=""center"">
                                Commercial
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Window-Eyes
                            </td>
                            <td>
                                <a title=""This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop."" href=""http://www.gwmicro.com/Window-Eyes/"" target=""_blank"" onclick=""javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.');"">http://www.gwmicro.com/Window-Eyes/</a><img width=""14"" height=""14"" src=""/../images/external.png"" alt="""" />
                            </td>
                            <td align=""center"">
                                Commercial
                            </td>
                        </tr>
                    </tbody>
                </table>");




        }
        else
        {
            p_var.pagetitle = "यह वेबसाइट भारत सरकार की वेबसाइटों के लिए जारी दिशानिर्देशों का अनुपालन करती है। यह दृष्टिविहीन लोगों को सहायक तकनीकों जैसे कि स्क्रीन रीडर्स का उपयोग करते हुए इस वेबसाइट का उपयोग करने में सक्षम करती है। इस वेबसाइट पर जानकारी के लिए विभिन्न स्क्रीन रीडर्स का उपयोग किया जा सकता है। निम्नलिखित तालिका विभिन्न स्क्रीन रीडर्स के बारे में जानकारी प्रदान करती है। ";
            p_var.str = "विभिन्न स्क्रीन रीडर्स से संबंधित सूचना";
            p_var.sbuilder.Append(@"<p>" + p_var.pagetitle + "</p><br /><p><strong>" + p_var.str + "</strong></p><br />");
            p_var.sbuilder.Append(@"<table width=""100%"" title=""स्क्रीन रीडर का उपयोग"" cellspacing=""0"" cellpadding=""0"" border=""0"" class=""border"" summary=""List of various screen readers"">
                    <tbody>
                        <tr>
                            <th width=""16%"">
                                स्क्रीन रीडर
                            </th>
                            <th width=""15%"">
                                वेबसाइट
                            </th>
                            <th width=""15%"" >
                                नि: शुल्क / वाणिज्यिक
                            </th>
                       
                            <tr>
                            <td>
                                Non Visual Desktop Access (NVDA) 
                            </td>
                            <td>
                                <a title=""यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।"" href=""http://www.nvaccess.org/"" target=""_blank"" onclick=""javascript:return confirm( 'यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।');"">http://www.nvaccess.org/</a><img width=""14"" height=""14"" src=""/../images/external.png"" alt="""" />
                            </td>
                            <td align=""center"">
                                नि: शुल्क
                            </td>
                        </tr>
                        <tr>
                            <td>
                                System Access To Go 
                            </td>
                            <td>
                                http://www.satogo.com
                            </td>
                            <td align=""center"">
                                नि: शुल्क
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Thunder 
                            </td>
                            <td>
                                <a title=""यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।"" href=""http://www.webbie.org.uk/thunder/"" target=""_blank"" onclick=""javascript:return confirm( 'यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।');"">http://www.webbie.org.uk/thunder/</a><img width=""14"" height=""14"" src=""/../images/external.png"" alt="""" />
                            </td>
                            <td align=""center"">
                                नि: शुल्क
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 WebAnywhere 
                            </td>
                            <td>
                                http://webanywhere.cs.washington.edu
                            </td>
                            <td align=""center"">
                                नि: शुल्क
                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                               JAWS 
                            </td>
                            <td>
                                <a title=""यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।"" href=""http://www.freedomscientific.com"" target=""_blank"" onclick=""javascript:return confirm( 'यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।');"">http://www.freedomscientific.com</a><img width=""14"" height=""14"" src=""/../images/external.png"" alt="""" />
                            </td>
                            <td align=""center"">
                                वाणिज्यिक
                            </td>
                        </tr>
                        <tr>
                            <td>
                               Supernova 
                            </td>
                            <td>
                                <a title=""यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।"" href=""http://www.yourdolphin.com/"" target=""_blank"" onclick=""javascript:return confirm( 'यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।');"">http://www.yourdolphin.com/</a><img width=""14"" height=""14"" src=""/../images/external.png"" alt="""" />
                            </td>
                            <td align=""center"">
                                वाणिज्यिक
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Window-Eyes 
                            </td>
                            <td>
                                <a title=""यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।"" href=""http://www.gwmicro.com/Window-Eyes/"" target=""_blank"" onclick=""javascript:return confirm( 'यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।');"">http://www.gwmicro.com/Window-Eyes/</a><img width=""14"" height=""14"" src=""/../images/external.png"" alt="""" />
                            </td>
                            <td align=""center"">
                                वाणिज्यिक
                            </td>
                        </tr>
                    </tbody>
                </table>");
        }
        ltrlMainContent.Text = p_var.sbuilder.ToString();
    }

    #endregion


    protected void imgPdf_Click(object sender, EventArgs e)
    {
        mid.Visible = false;
       

        StringWriter sw = new StringWriter();
        HtmlTextWriter w = new HtmlTextWriter(sw);
        content.RenderControl(w);
        string htmWrite = sw.GetStringBuilder().ToString();

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=FileName.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        htmWrite = Regex.Replace(htmWrite, "</?(a|A|img|IMG).*?>", "");
        htmWrite = htmWrite.Replace("\r\n", "");
        StringReader reader = new StringReader(htmWrite);

        Document doc = new Document(PageSize.A4);
        HTMLWorker parser = new HTMLWorker(doc);
        PdfWriter.GetInstance(doc, Response.OutputStream);
        doc.Open();
        try
        {
            parser.Parse(reader);
        }
        catch (Exception ex)
        {
        }
        finally
        {
            doc.Close();
        }

    }
    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion
}
