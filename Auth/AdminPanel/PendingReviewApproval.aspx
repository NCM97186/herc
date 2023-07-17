<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PendingReviewApproval.aspx.cs"
    Inherits="Auth_AdminPanel_PendingReviewApproval" Theme="" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../App_Themes/Black/css/style-popup.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="more-popup">
        <asp:Panel ID="pnlContent" runat="server">
            <strong>HERC :</strong><br />
            Draft :<asp:Label ID="lblHercDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblHercReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblHercApproval" runat="server"></asp:Label><br />
            <strong>Ombudsman :</strong><br />
            Draft :<asp:Label ID="lblOmbudsmanDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblOmbudsmanReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblOmbudsmanApproval" runat="server"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlProfile" runat="server">
            <strong>HERC :</strong><br />
            Draft :<asp:Label ID="lblProfileDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblProfileReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblProfileApproval" runat="server"></asp:Label><br />
            <strong>Ombudsman :</strong><br />
            Draft :<asp:Label ID="lblOmbudsmanProfileDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblOmbudsmanProfileReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblOmbudsmanProfileApproval" runat="server"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlModules" runat="server">
            <strong>Regulations :</strong><br />
            Draft :<asp:Label ID="lblRegulationDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblRegulationReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblRegulationApproval" runat="server"></asp:Label><br />
            <strong>Codes :</strong><br />
            Draft :<asp:Label ID="lblCodesDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblCodesReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblCodesApproval" runat="server"></asp:Label><br />
            <strong>Standards :</strong><br />
            Draft :<asp:Label ID="lblStandardsDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblStandardsReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblStandardsApproval" runat="server"></asp:Label><br />
            <strong>Policies :</strong><br />
            Draft :<asp:Label ID="lblPolicyDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblPolicyReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblPolicyApproval" runat="server"></asp:Label><br />
            <strong>Guidelines :</strong><br />
            Draft :<asp:Label ID="lblGuidelinesDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblGuidelinesReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblGuidelinesApproval" runat="server"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlPublicNotice" runat="server">
            <strong>Public Notice :</strong><br />
            Draft :<asp:Label ID="lblPublicNoticeDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblPublicNoticeReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblPublicNoticeApproval" runat="server"></asp:Label><br />
        </asp:Panel>
        <asp:Panel ID="pnlAwardProunced" runat="server">
            <strong>EO Award Pronounced :</strong><br />
            Draft :<asp:Label ID="lblAwardProuncedDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblAwardProuncedReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblAwardProuncedApproval" runat="server"></asp:Label><br />
        </asp:Panel>
        <asp:Panel ID="pnlAppeal" runat="server">
            <strong>EO Appeal :</strong><br />
            Draft :<asp:Label ID="lblAppealDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblAppealReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblAppealApproval" runat="server"></asp:Label><br />
            <strong>EO Appeal Against Award :</strong><br />
            Draft :<asp:Label ID="lblAppealagainstDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblAppealagainstReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblAppealagainstApproval" runat="server"></asp:Label><br />
        </asp:Panel>
        <asp:Panel ID="pnlOrder" runat="server">
            <strong>Interim Order :</strong><br />
            Draft :<asp:Label ID="lblInterimOrderDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblInterimOrderReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblInterimOrderApproval" runat="server"></asp:Label><br />
            <strong>Final Order :</strong><br />
            Draft :<asp:Label ID="lblFinalOrderDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblFinalOrderReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblFinalOrderApproval" runat="server"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlPetition" runat="server">
            <strong>Petition :</strong><br />
            Draft :<asp:Label ID="lblPendingPetition" runat="server" Text=""></asp:Label><br />
            Review :<asp:Label ID="lblReviewPetition" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblApprovalPetition" runat="server"></asp:Label><br />
            <strong>Review Petition :</strong><br />
            Draft :<asp:Label ID="lblPendingReviewPetition" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblReviewReviewPetition" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblApprovalReviewPetition" runat="server"></asp:Label><br />
            <strong>Appeal Petition :</strong><br />
            Draft :<asp:Label ID="lblPendingAppealPetition" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblReviewAppealPetition" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblApprovalAppealPetition" runat="server"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlScheduleOfhearing" runat="server">
            <strong>HERC Schedule Of Hearing :</strong><br />
            Draft :<asp:Label ID="lblPendingHercDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblReviewHercReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblApprovalHercApproval" runat="server"></asp:Label><br />
            <strong>Ombudsman Schedule Of Hearing :</strong><br />
            Draft :<asp:Label ID="lblPendingOmbudsmanDraft" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblPendingOmbudsmanReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblPendingOmbudsmanApproval" runat="server"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlRti" runat="server">
            <strong>RTI HERC :</strong><br />
            Draft :<asp:Label ID="lblRtiHercPending" runat="server" Text=""></asp:Label><br />
            Review :<asp:Label ID="lblRtiHercReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblRtiHercApproval" runat="server"></asp:Label><br />
            <strong>RTI-FAA HERC :</strong><br />
            Draft :<asp:Label ID="lblRtifaaHercPending" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblRtifaaHercReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblRtifaaHercApproval" runat="server"></asp:Label><br />
            <strong>RTI-SAA HERC :</strong><br />
            Draft :<asp:Label ID="lblRtisaaHercPending" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblRtisaaHercReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblRtisaaHercApproval" runat="server"></asp:Label><br />
            <strong>RTI Ombudsman :</strong><br />
            Draft :<asp:Label ID="lblRtiOmbudsmanPending" runat="server" Text=""></asp:Label><br />
            Review :<asp:Label ID="lblRtiOmbudsmanReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblRtiOmbudsmanApproval" runat="server"></asp:Label><br />
            <strong>RTI-FAA Ombudsman :</strong><br />
            Draft :<asp:Label ID="lblRtifaaOmbudsmanPending" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblRtifaaOmbudsmanReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblRtifaaOmbudsmanApproval" runat="server"></asp:Label><br />
            <strong>RTI-SAA Ombudsman :</strong><br />
            Draft :<asp:Label ID="lblRtisaaOmbudsmanPending" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblRtisaaOmbudsmanReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblRtisaaOmbudsmanApproval" runat="server"></asp:Label>
        </asp:Panel>

         <asp:Panel ID="pnlTariff" runat="server">
            <strong>Tariff :</strong><br />
            Draft :<asp:Label ID="lblTariffPending" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblTariffReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblTariffApproval" runat="server"></asp:Label><br />
            <strong>FSA :</strong><br />
            Draft :<asp:Label ID="lblFSAPending" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblFSAReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblFSAApproval" runat="server"></asp:Label><br />
            <strong>General & Misc. Charges :</strong><br />
            Draft :<asp:Label ID="lblRenewalPending" runat="server"></asp:Label><br />
            Review :<asp:Label ID="lblRenewalReview" runat="server"></asp:Label><br />
            For Publish :<asp:Label ID="lblRenewalApproval" runat="server"></asp:Label><br />
           
        </asp:Panel>

    </div>
    </form>
</body>
</html>
