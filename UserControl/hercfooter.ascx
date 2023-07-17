<%@ Control Language="C#" AutoEventWireup="true" CodeFile="hercfooter.ascx.cs" Inherits="UserControl_hercfooter" %>
<div class="footer-links">
    <div class="related-links">
        <div class="links">
            <ul>
                <asp:Literal ID="ltrlFooter" runat="server"> </asp:Literal>
            </ul>
            <div class="validate">
                 <!-- <a href="http://jigsaw.w3.org/css-validator/check/referer" target="_blank">
                    <img width="65" height="24" alt="W3C CCS Validate" title="Valid CCS!" src="http://jigsaw.w3.org/css-validator/images/vcss" /> 
                </a> -->
                 <!-- <img src="<%=ResolveUrl("~/images/w3c-html-icon-.png") %>" width="65" height="24" alt="W3C HTML Validate" title="W3C HTML Validate" /> -->
            <!-- <img src="<%=ResolveUrl("~/images/w3c-css-icon.png") %>" width="65" height="24" alt="W3C CCS Validate" title="W3C CCS Validate" /> -->
            </div>
            <!--end of validate-->
        </div>
    </div>
</div>
<div class="clear">
</div>
<div class="Footer-below">
    <div class="footer-left-icon">
        <div class="cwq-footer-icon">
             <a href="<%=ResolveUrl("~/WriteReadData/Pdf/CQW.pdf") %>" target="_blank">
                <img src="<%=ResolveUrl("~/images/cwq-footer-icon.png") %>" alt="CQW" title="CQW"
                    width="50px" height="50px" /></a> 
        </div>
        <div class="w3c-html-icon">
        </div>
        <div class="w3c-css-icon">
        </div>
    </div>
    <div class="foote-mid">
        <%= HttpUtility.UrlDecode(Resources.HercResource.FooterText_site_upper)%><br/><%= HttpUtility.UrlDecode(Resources.HercResource.FooterText_site)%>
        <a onclick="javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.');"
            href="http://www.nic.in/" target="_blank">
            <%=Resources.HercResource.NIC %></a><br />
        <%=Resources.HercResource.FooterText_content%>
        <%=Resources.HercResource.hercfullname %>
    </div>
    <div class="visitor-counter">
        <div class="visitor-icon">
        </div>
        <div class="visitor-number">
            <asp:Label ID="lblVisitors" runat="server"></asp:Label>
        </div>
    </div>
</div>
