using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public class Module_ID_Enum
{
    //Area for the constructors declaration

    #region default contructor zone

    public Module_ID_Enum()
	{

    }

    #endregion

    //End

    //Area for the enum declaration

    #region enum declaration for Project modules id

    public enum Project_Module_ID : int
    {
        Menu=1,
        Footer=2,
        Petition=3,
        Appeal=4,
        RTI=5,
        Annual_Report=6,
        Whats_New=7,
        SOH=8,
        Public_Notice=9,
        Notification=10,
        Licenses=11,
        Discussion_Paper=12,
        Vacancy=13,
        Role=14,
        User=15,
        Category=16,
        Orders=17,
        Banner=18,
        AwardPronounced=19,
        Modules=20,
        Regulations=20,
        Codes=22,
        Standard=23,
        Policies=25,
        Guidelines=26,
        Tariff=27,
        Profiles=28,
        Reports=29,

    }

    #endregion

    #region enum declaration for action types
 
    public enum Project_Action_Type : int
    {
        insert = 1,
        update = 2,
        delete = 3
    }

    #endregion

    #region enum declaration for rti status

    public enum rti_Status : int
    {
        inprocess = 19,
        replysent=20,
        judgement=23,
        TransferedAuthority=36,
        AnyOther=37,
        Rti_StatusTypeId=5,
    }

    #endregion

    #region enum declaration for Appeal status

    public enum Appeal_Status : int
    {
        inprocess = 15,
        ScheduleForHearing = 16,
        AwardPronounced = 17,
        JudgementPronounced=18,
    }

    #endregion

    #region enum declaration for Petition status

    public enum Petition_Status : int
    {
        InProcess=10,
        ScheduleForHearing = 11,
        InterimOrder = 12,
        FinalOrder=13,
        OrderPronounced = 13,
        JudgementPronounced = 14,
        PublicNotice=22,
        anyOther = 25,
        AwardPronounced=17,
        InProcessOmbudsman = 15,
    }

    #endregion

    #region enum declaration for Review petition status

    public enum ReviewPetition_Status : int
    {
        InProcess = 15,
        ScheduleForHearing = 16,
        AwardPronounced = 17,
        JudgementPronounced = 18,
    }

    #endregion

    #region enum declaration for module permission type

    public enum Module_Permission_ID : int
    {
        draft = 3,
        review = 21,
        ForApprover = 4,
        Approved = 6,
        Delete = 8,
        English = 1,
        Hindi = 2,
        Reply = 11,
        Replied = 12,
        ViewDeletedData = 17,
		DeletedUser=221,

    }

    #endregion

    public enum Language_ID : int
    {
        English = 1,
        Hindi = 2,
    }
    public enum Tariff_category
    {
        DistributionCharges = 1,
        TransmissionCharges = 2,
        GenerationTariff = 3,
        WheelingCharges = 4,
        RenewalEnergy=6,
        CrossSubsidydditionalSurcharge=5,
    }

    public enum Tariff_subcategory
    {
        Tariff=7,
        FSA=8,
        Miscell=9,
    }

    public enum Menu_ID_Fixed : int
    {
        OmbudsmanOverviewParent=239,
        OmbudsmanOverviewParentHindi=282,
        OmbudsmanWhatsNew=323,
        OmbudsmanWhatsNewHindi=465,

        Footer_Home = 45,
        Home_Hindi = 138,
        HomeHindi_Ombudsman=255,
        Home_Footer_Hindi = 54,

        Footer_Feedback = 36,
        Feedback = 12,
        Feedback_Hindi = 50,
        Footer_Feedback_Hindi = 55,
        Top_ContactUs = 13,
        Top_ContactUS_Hindi = 52,
        Gallery = 19,
        Gallery_Hindi = 100,
        PhotoGallery = -29,
        PhotoGallery_Hindi = 126,
        VideoGallery = 30,
        VideoGallery_Hindi = 127,
        Sitemap = 136,
        Sitemap_Hindi = 231,
        OmbudsmanSitemap = 844,
        OmbudsmanSitemap_Hindi=854,

        AboutUs = 2,
        AboutUs_Hindi = 78,
        Tender = 47,
        Tender_Hindi = 116,
        /*  Archive = 17,*/
        Archive = -1001,
        Archive_Hindi = 92,
        Archive_Footer = 6,
        Archive_Footer_Hindi = 55,

        EventsMeeting = 44,
        EventsMeeting_Hindi = 113,
        Quarterly_Newsletter = 1,
        Quarterly_Newsletter_Hindi = 7,
        stackHolder = -96,
        stackHolder_Hindi = -101,
        Grievance = -97,
        Grievance_Hindi = -102,
        Appeal=250,
        Appeal_Hindi=288,
        Appeal_prevYear=251,
        Appeal_prevYear_Hindi=289,
        AppealOnlineStatus=252,
        AppealOnlineStatus_Hindi=290,
        AppealSearch=253,
        AppealSearch_Hindi=291,
        RTI_Herc=220,
        RTI_Herc_Hindi=462,
        RTI_Ombudsman=447,
        RTI_Ombudsman_Hindi=627,
        RTI_HercHome=130,
        RTI_HercHome_Hindi = 174,
        RTI=276,
        RTI_Hindi=316,
        RTI_OmbudsmanPreviousYear =277,
        RTI_OmbudsmanPreviousYear_Hindi = 317,
        licensees=354,
        licensees_Hindi=353,
        AwardsPronouncedCurrent=256,
        AwardsPronouncedCurrent_Hindi=293,
        AwardsPronouncedPrevious=257,
        AwardsPronouncedPrevious_Hindi=294,
        AwardsPronouncedSearch=259,
        AwardsPronouncedSearch_Hindi=296,
        AwardsUnderAppeal=258,
        AwardsUnderAppeal_Hindi=295,
        StateAdvisoryCommittee=365,
        StateAdvisoryCommittee_Hindi=366,
        CoordinationForum = 388,
        CoordinationForum_Hindi = 389,
        SOH_CurrentYear=260,
        SOH_PreviousYear=261,
        SOH_CurrentYearHindi=298,
        SOH_PreviousYearHindi=299,
        SOH_NextYear=1005,
        SOH_NextYearHindi=1006,
        RTI_OmbudsmanSearch=278,
        RTI_OmbudsmanSearch_Hindi=318,
        Herc_Prev_RTI = 222,
        Herc_Prev_RTI_Hindi=463,
        Hercsearch_RTI=223,
        Hercsearch_RTI_Hindi=464,
        parliament_Assembly_Questions=200,
        parliament_Assembly_Questions_Hindi = 213,
        WebsitePolicy = 134,
        WebsitePolicy_Hindi = 228,
        Abbreviations=454,
        Abbreviations_Hindi=468,
        DistributionCharges = 360,
        DistributionCharges_Hindi = 369,
        TransmissionCharges=361,
        TransmissionCharges_Hindi=370,
        GenerationTariff=362,
        GenerationTariff_Hindi=371,
        WheelingCharges=363,
        WheelingCharges_Hindi=372,
        RenewalEnergy=532,
        RenewalEnergy_Hindi = 807,
        CrossSubsidy=530,
        CrossSubsidy_Hindi = 806,
        profile=102,
        profile_Hindi=146,
        FSA=358,
        FSA_Hindi=367,
        GenerationTariff_ombudsman=598,
        WheelingCharges_ombudsman=599,
        DistributionCharges_ombudsman=600,
        TransmissionCharges_ombudsman=603,
        CrossSubsidy_ombudsman=601,
        RenewalEnergy_ombudsman=602,
        FSA_ombudsman=597,
        LocateUs=446,
        LocateUs_Hindi=569,
        GeneralMiscCharges=359,
        GeneralMiscCharges_Hindi=368,
        Ombudsmancalender=327,
        Ombudsmancalender_Hindi=331,
	   
        Ombudsman_Profile=243,
        Ombudsman_ProfileHindi=286,
        policies_Herc=118,
        policies_Herc_Hindi = 162,
        policies_Other = 119,
        policies_Other_Hindi = 163,
        policies_Repealed = 120,
        policies_Repealed_Hindi = 164,
        Guidelines_Herc = 121,
        Guidelines_Herc_Hindi = 165,
        Guidelines_Other = 122,
        Guidelines_Other_Hindi = 166,
        Guidelines_Repealed = 123,
        Guidelines_Repealed_Hindi = 167,
        Standard_HERC=197,
        Standard_Repealed=199,
        Standard_HERC_Hindi = 210,
        Standard_Repealed_Hindi = 212,
        Ombudsman_SoHSearch=262,
        Ombudsman_SoHSearch_Hindi=300,
        Regulation_HERC=191,
        Regulation_Repealed=193,
        Regulation_HERC_Hindi = 202,
        Regulation_Repealed_Hindi = 204,
        Code_HERC = 194,
        Code_Repealed = 196,
        Code_HERC_Hindi = 206,
        Code_Repealed_Hindi = 208,
        vacancy_Eng=135,
        vacancy_hindi=229,
        Notification=774,
        Notification_Hindi=776,

        Reports=326,

        Reports_Hindi = 330,
        //OmbudsmanSitemap=844,

        aptel = 845,
        aptel_hindi = 849,
        //standars_
    }

    public enum project_MenuID_FrontEnd : int
    {
        Home = 94,
        Home_ombudsman=254
    }

    public enum Menu_Position : int
    {
        top = 1,
        footer = 2,
        left = 3,
        right_top = 4,
        middle = 5,
        right_bottom = 6
    }

    public enum Link_parentID_Footer : int
    {
        parent_Footer = 0
    }

    public enum link_type_id : int
    {
        content = 1,
        link = 3,
        File = 2
    }

    public enum hercType : int
    {
        herc=1,
        other=4,
        repealed=8,
        ombudsman=2,
        superadmin=18,
    }

    //End
}
