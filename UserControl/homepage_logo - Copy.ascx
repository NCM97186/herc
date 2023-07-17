<%@ Control Language="C#" AutoEventWireup="true" CodeFile="homepage_logo.ascx.cs"
    Inherits="UserControl_homepage_logo" %>
<li>
<div class="sliderBlock">
    <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
       {%>
    <asp:ImageButton ID="imgRTIEng" runat="server" 
        AlternateText="Right To Information" Width="150px" Height="70px" BorderStyle="None"
        ToolTip='<%#Resources.HercResource.RighttoInformation %>'  OnClick="imgRTIEng_Click"/>
    <%}
       else %>
    <%
        { %>
    <asp:ImageButton ID="imgRTIHin" runat="server" 
        AlternateText='<%#Resources.HercResource.RighttoInformation %>' Width="150px"
        Height="66px" BorderStyle="None" ToolTip='<%#Resources.HercResource.RighttoInformation %>' OnClick="imgRTIHin_Click"/>
    <%} %>
    </div>
</li>
<li>
<div class="sliderBlock">
<% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
       {%>
    <asp:ImageButton ID="ibindiagov" runat="server" OnClientClick="javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.',aspnetForm.target ='_blank');"
        OnClick="ibIndiaPortal_Click" CausesValidation="false" />
           <%}
       else %>
    <%
        { %>
         <asp:ImageButton ID="ibindiagovHindi" runat="server" OnClientClick="javascript:return confirm( 'यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।',aspnetForm.target ='_blank');"
         OnClick="ibIndiaPortal_Click" CausesValidation="false" />
         <%} %>

    <%-- <a href="#">
                            
                                <img src="<%=ResolveUrl("images/india-gov-logo.png") %>" alt="india-gov-in" width="128" height="34" border="0"
                                    title="india-gov-in" />
                            </a>--%>
</div>
</li>
<li>
<div class="sliderBlock">
<% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
       {%>
    <asp:ImageButton ID="ibharyanagov" runat="server" OnClientClick="javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.',aspnetForm.target ='_blank');"
        OnClick="ibharyanagov_Click" CausesValidation="false" />

         <%}
       else %>
    <%
        { %>
            <asp:ImageButton ID="ibharyanagovHindi" runat="server" OnClientClick="javascript:return confirm( 'यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।',aspnetForm.target ='_blank');"
        OnClick="ibharyanagov_Click" CausesValidation="false" />

  <%} %>
    <%--<a href="#">
                                <img src="<%=ResolveUrl("images/haryana-gov-logo.png") %>" alt="haryana-govt-logo" width="107" height="62"
                                    border="0" title="haryana-govt-logo" />
                            </a>--%>
</div>
</li>
<li>
<div class="sliderBlock">
    <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
       {%>
    <asp:ImageButton ID="ibDialgovEng" runat="server" OnClientClick="javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.',aspnetForm.target ='_blank');"
        OnClick="Dialgov_Click" CausesValidation="false" />
    <%}
       else %>
    <%
        { %>
    <asp:ImageButton ID="ibDialgovHin" runat="server" OnClientClick="javascript:return confirm( 'यह लिंक आपको हर्क की वेबसाइट के बाहर एक वेबपृष्ठ पर ले जाएगा। हर्क विषय वस्तुओं या सहबद्ध वेबसाइटों की विश्वासनीयता के लिए उत्तरदायी नहीं है और उनके अंतर्गत प्रकट ष्टिकोण से अनिवार्य रूप से समर्थन नहीं करता है। जानकारी के लिए आप वेबसाइट नीतियां के तहत हाइपरलिंकिंग नीति का संदर्भ ले सकते हैं। जारी रखने के लिए OK क्लिक करें। रद्द करने के लिए Cancel क्लिक करें।',aspnetForm.target ='_blank');"
        OnClick="DialgovHin_Click" CausesValidation="false" />
    <%} %>
    </div>
</li>


