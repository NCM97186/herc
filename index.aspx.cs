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
using System.Text;
public partial class Index : BasePage
{
    #region variable declaration

    Project_Variables p_val = new Project_Variables();
    PetitionBL objPetBL = new PetitionBL();
    PetitionOB objPetOB = new PetitionOB();
    LinkOB lnkObject = new LinkOB();
    PublicNoticeBL pubNoticeBL = new PublicNoticeBL();
    LinkBL objlnkBL = new LinkBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    string PageTitle = string.Empty;


    #endregion

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = Resources.HercResource.HomePage;
        p_val.url = Resources.HercResource.PageUrl.ToString();
        Page.Header.DataBind();
        if (ViewState["Blue"] != null)
        {
            string theme = ViewState["Blue"].ToString();
        }
        else
        {
            ViewState["Blue"] = "Blue";
            string theme = ViewState["Blue"].ToString();
        }

        if (!IsPostBack)
        {

            BindPublicSectorLinks();
            Bind_SOH();
            // BindWhatsNew();
            bind_PublicNotice();
            Bind_Licensees();
            Bind_IntrimOrder_Grid(1);
            Bind_Petition_Grid(1);
            Get_AllWhatsNew(1);
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            BindImagesTheme();
        }


        if (Resources.HercResource.Lang_Id == Module_ID_Enum.Language_ID.English.ToString())
        {
            PageTitle = Resources.HercResource.HomePage;
        }
        else
        {
            PageTitle = Resources.HercResource.HomePage;
        }
    }

    #endregion

    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    #region function to bind All whats new Details

    public void Get_AllWhatsNew(int pageIndex)
    {
        try
        {
            lnkObject.PageIndex = 1;
            lnkObject.PageSize = 100000;
            lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            p_val.dSet = objlnkBL.Get_AllWhatsNew(lnkObject, out p_val.k);
            if (p_val.dSet.Tables[0].Rows.Count > 0)
            {
                // hRegulation.InnerHtml = "Regulation";
                RptrRegulation.DataSource = p_val.dSet;
                RptrRegulation.DataBind();
                //  lastUpdatedDate = P_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
                //rptPager.Visible = true;
                //ddlPageSize.Visible = true;
                //lblPageSize.Visible = true;
            }
            else
            {
                //rptPager.Visible = false;
                //ddlPageSize.Visible = false;
                //lblPageSize.Visible = false;
            }


        }
        catch { }
    }

    #endregion


    #region repeater rptWhatsNew itemDataBound latest update

    protected void RptrRegulation_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink hyp = (HyperLink)e.Item.FindControl("hypFile");
            HiddenField hid = (HiddenField)e.Item.FindControl("hidFile");
            Label lblname = (Label)e.Item.FindControl("lblname");
            if (hid.Value.ToString() == "" || hid.Value == null)
            {
                lblname.Visible = true;
                lblname.Text = HttpUtility.HtmlDecode(hyp.Text);
                hyp.Visible = false;
            }
            else
            {
                lblname.Visible = false;
                hyp.Visible = true;
                hyp.Text = hyp.Text.ToString().Replace("<p>", "").Replace("</p>", "");
                hyp.Text = hyp.Text.ToString().Replace("<p>", "").Replace("</p>", "").Replace("<font face=\"Verdana\" size=\"2\">", "").Replace("</font>", "");
                hyp.Text = miscellBL.FixGivenCharacters(hyp.Text, 100);
            }
        }
    }

    #endregion

    public void Bind_SOH()
    {
        try
        {
            objPetOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            p_val.dSet = objPetBL.Get_SOH(objPetOB);
            if (Session["TempTable"] != null)
            {
                Session.Remove("TempTable");
            }
            Session.Remove("TempTable");
            p_val.sbuilder.Remove(0, p_val.sbuilder.Length);
            if (p_val.dSet.Tables[0].Rows.Count > 0)
            {

                rptrSOH.DataSource = p_val.dSet;
                rptrSOH.DataBind();

            }
        }
        catch { }
    }




    public void bind_PublicNotice()
    {
        try
        {
            lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            p_val.dSet = pubNoticeBL.displayPublicNotice(lnkObject);
            // p_val.sbuilder.Remove(0, p_val.sbuilder.Length);
            if (p_val.dSet.Tables[0].Rows.Count > 0)
            {
                if (p_val.dSet.Tables[0].Rows.Count > 0)
                {
                    rptpublicNotice.DataSource = p_val.dSet;
                    rptpublicNotice.DataBind();

                }


            }
        }
        catch { }

    }

    protected void rptpublicNotice_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "ViewDetails")
        {

            p_val.stringTypeID = e.CommandArgument.ToString();
            p_val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "DetailsPage.aspx?PulicNoticeId=" + p_val.stringTypeID) + "', 'blank' + new Date().getTime()," +
                               "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                               "</script>";
            this.Page.RegisterStartupScript("PopupScript", p_val.strPopupID);
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());


        }
    }

    protected void rptrSOH_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "view")
        {

            p_val.stringTypeID = e.CommandArgument.ToString();
            p_val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewDetails.aspx?Soh_id=" + p_val.stringTypeID) + "', 'blank' + new Date().getTime()," +
                               "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                               "</script>";
            this.Page.RegisterStartupScript("PopupScript", p_val.strPopupID);
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());


        }
    }

    public void Bind_Licensees()
    {
        try
        {
            p_val.sbuilder.Length = 0;
            lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);

            p_val.dSet = objlnkBL.Bind_Licensees(lnkObject);

            if (p_val.dSet.Tables[0].Rows.Count > 0)
            {
                for (p_val.i = 0; p_val.i < p_val.dSet.Tables[0].Rows.Count; p_val.i++)
                {
                    p_val.menuid = Convert.ToInt16(p_val.dSet.Tables[0].Rows[p_val.i]["Link_Id"]);
                    p_val.menu_name = p_val.dSet.Tables[0].Rows[p_val.i]["Name"].ToString();
                    p_val.position = Convert.ToInt16(p_val.dSet.Tables[0].Rows[p_val.i]["Position_id"]);
                    if (p_val.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.licensees) || p_val.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.licensees_Hindi))
                    {
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            ltrLicensees.Text = ("<a class=\"licesens-icon\"  href='" + ResolveUrl("~/") + "content/354_5_Licenseen.aspx' title= '" + p_val.dSet.Tables[0].Rows[0]["Page_Title"].ToString() + "'>" + Resources.HercResource.Licensees + " </a>");
                        }
                        else
                        {
                            ltrLicensees.Text = ("<a class=\"licesens-icon\"  href='" + ResolveUrl("~/content/Hindi/") + "353_5_Licenseen.aspx' title= '" + p_val.dSet.Tables[0].Rows[0]["Page_Title"].ToString() + "'>" + Resources.HercResource.Licensees + " </a>");
                        }
                    }
                    else if (p_val.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.StateAdvisoryCommittee) || p_val.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.StateAdvisoryCommittee_Hindi))
                    {
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_val.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_val.menu_name) + "' class=\"state-advisory-icon\"  href='" + ResolveUrl("~/") + p_val.url + p_val.menuid + "_" + Convert.ToInt16(p_val.position) + "_" + p_val.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_val.menu_name + "</a>");
                        }
                        else
                        {
                            p_val.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_val.menu_name) + "' class=\"state-advisory-icon\" href='" + ResolveUrl("~/") + p_val.url + p_val.menuid + "_" + Convert.ToInt16(p_val.position) + "_" + p_val.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_val.menu_name + "</a>");
                        }
                        ltrStatelink.Text = p_val.sbuilder.ToString();
                    }

                    else if (p_val.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.CoordinationForum) || p_val.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.CoordinationForum_Hindi))
                    {
                        p_val.sbuilder.Remove(0, p_val.sbuilder.Length);
                        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                        {
                            p_val.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_val.menu_name) + "' class=\"ordination-icon\"  href='" + ResolveUrl("~/") + p_val.url + p_val.menuid + "_" + Convert.ToInt16(p_val.position) + "_" + p_val.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_val.menu_name + "</a>");
                        }
                        else
                        {
                            p_val.sbuilder.Append("<a title='" + HttpUtility.HtmlDecode(p_val.menu_name) + "' class=\"ordination-icon\" href='" + ResolveUrl("~/") + p_val.url + p_val.menuid + "_" + Convert.ToInt16(p_val.position) + "_" + p_val.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + p_val.menu_name + "</a>");
                        }
                        ltrlOrdination.Text = p_val.sbuilder.ToString();
                    }


                }

            }
        }
        catch { }
    }

    public void BindPublicSectorLinks()
    {
        try
        {
            p_val.sbuilder.Remove(0, p_val.sbuilder.Length);
            lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            p_val.dSet = objlnkBL.Bind_PublicSectorLinksHome(lnkObject);
            if (p_val.dSet.Tables[0].Rows.Count > 0)
            {
                p_val.sbuilder.Append("<ul id='vertical-ticker'>");
                for (p_val.i = 0; p_val.i < p_val.dSet.Tables[0].Rows.Count; p_val.i++)
                {
                    p_val.menuid = Convert.ToInt16(p_val.dSet.Tables[0].Rows[p_val.i]["Link_Id"]);
                    p_val.menu_name = p_val.dSet.Tables[0].Rows[p_val.i]["Name"].ToString();
                    p_val.position = Convert.ToInt16(p_val.dSet.Tables[0].Rows[p_val.i]["Position_id"]);



                    //p_val.sbuilder.Append("<ul>");
                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                    {
                        p_val.sbuilder.Append("<li>");
                        p_val.sbuilder.Append("<a class=\"ordination-icon\"  href='" + ResolveUrl("~/") + p_val.url + p_val.menuid + "_" + Convert.ToInt16(p_val.position) + "_" + p_val.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx' title='" + Resources.HercResource.ClickHereToViewDetails + "'>" + p_val.menu_name + "</a>");
                    }
                    else
                    {
                        p_val.sbuilder.Append("<li>");
                        p_val.sbuilder.Append("<a class=\"ordination-icon\" href='" + ResolveUrl("~/") + p_val.url + p_val.menuid + "_" + Convert.ToInt16(p_val.position) + "_" + p_val.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx' title='" + Resources.HercResource.ClickHereToViewDetails + "'>" + p_val.menu_name + "</a>");
                    }
                    p_val.sbuilder.Append("</li>");
                    // p_val.sbuilder.Append("</ul>");

                }
                p_val.sbuilder.Append("</ul>");
                ltrlPublicSectorLinks.Text = p_val.sbuilder.ToString();
            }
        }
        catch { }
    }

    #region linkButton lnkPublicSectorLinks click events to see more public sector links

    protected void lnkPublicSectorLinks_Click(object sender, EventArgs e)
    {

        p_val.sbuilder.Remove(0, p_val.sbuilder.Length);
        lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        p_val.dSet = objlnkBL.Bind_PublicSectorLinks(lnkObject);
        if (p_val.dSet.Tables[0].Rows.Count > 0)
        {

            for (p_val.i = 0; p_val.i < 1; p_val.i++)
            {
                p_val.menuid = Convert.ToInt16(p_val.dSet.Tables[0].Rows[p_val.i]["Link_Id"]);
                p_val.menu_name = p_val.dSet.Tables[0].Rows[p_val.i]["Name"].ToString();
                p_val.position = Convert.ToInt16(p_val.dSet.Tables[0].Rows[p_val.i]["Position_id"]);

                Response.Redirect(ResolveUrl("~/") + p_val.url + p_val.menuid + "_" + Convert.ToInt16(p_val.position) + "_" + p_val.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx");
            }

        }
    }

    #endregion

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {
        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        // PageDescription = MetaDescription;
        // PageKeywords = MetaKeyword;
    }

    #endregion


    public void Bind_Petition_Grid(int pageindex)
    {
        try
        {
            objPetOB.PageIndex = 1;
            objPetOB.PageSize = 100000000;

            objPetOB.ActionType = 4;
            objPetOB.PRONo = null;
            p_val.dsFileName = objPetBL.GetLatestPetition(objPetOB, out p_val.k);
            if (p_val.dsFileName.Tables[0].Rows.Count > 0)
            {

                rptPetitonNew.DataSource = p_val.dsFileName;
                rptPetitonNew.DataBind();

            }

            p_val.Result = p_val.k;

        }
        catch { }
    }


    #region repeater Petition itemCommand latest update

    protected void rptPetitonNew_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                p_val.stringTypeID = e.CommandArgument.ToString();
                p_val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/DetailsPage.aspx?OrderId=" + e.CommandArgument) + "', 'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", p_val.strPopupID);
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }
            else
            {

                p_val.stringTypeID = e.CommandArgument.ToString();
                p_val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Content/Hindi/DetailsPage.aspx?OrderId=" + e.CommandArgument) + "', 'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=Yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", p_val.strPopupID);
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }


        }
    }

    #endregion


    #region repeater rptPetitonNew itemDataBound latest update

    protected void rptPetitonNew_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //LinkButton lnkLatestUpdatePetition = (LinkButton)e.Item.FindControl("lnkLatestUpdatePetition");
            LinkButton lnkDetailsPetition = (LinkButton)e.Item.FindControl("lnkDetailsPetition");
            Label lblmsgPetition = (Label)e.Item.FindControl("lblmsgPetition");

            if (lnkDetailsPetition.Text != null && lnkDetailsPetition.Text != "")
            {
                lnkDetailsPetition.Text = miscellBL.FixGivenCharacters(lnkDetailsPetition.Text, 110);
            }

        }
    }

    #endregion

    protected void rptpublicNotice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkTitle = (LinkButton)e.Item.FindControl("lnkTitle");
            lnkTitle.Text = miscellBL.FixGivenCharacters(HttpUtility.HtmlDecode(lnkTitle.Text), 100);
        }
    }

    protected void rptrSOH_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkDetailsSoh = (LinkButton)e.Item.FindControl("lnkDetailsSoh");
            lnkDetailsSoh.Text = miscellBL.FixGivenCharacters(HttpUtility.HtmlDecode(lnkDetailsSoh.Text), 100).Replace("&", "&amp;");
        }
    }


    #region repeater rptInterimOrders itemDataBound latest update

    protected void rptInterimOrder_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            LinkButton lnkDetailsOrder = (LinkButton)e.Item.FindControl("lnkDetailsOrder");


            if (lnkDetailsOrder.Text != null && lnkDetailsOrder.Text != "")
            {
                lnkDetailsOrder.Text = miscellBL.FixGivenCharacters(lnkDetailsOrder.Text, 100);
            }

        }
    }

    #endregion

    #region repeater interim Orders itemCommand latest update

    protected void rptInterimOrder_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "viewOrder")
        {

            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                p_val.stringTypeID = e.CommandArgument.ToString();
                p_val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/DetailsPage.aspx?OrderId=" + e.CommandArgument) + "', 'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", p_val.strPopupID);
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            else
            {

                p_val.stringTypeID = e.CommandArgument.ToString();
                p_val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Content/Hindi/DetailsPage.aspx?OrderId=" + e.CommandArgument) + "', 'blank' + new Date().getTime()," +
                                   "'menubar=no, resizable=Yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", p_val.strPopupID);
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }



        }
    }

    #endregion


    public void Bind_IntrimOrder_Grid(int pageindex)
    {
        try
        {
            objPetOB.PageIndex = 1;
            objPetOB.PageSize = 100000000;

            objPetOB.ActionType = 6;
            objPetOB.PRONo = null;
            p_val.dsFileName = objPetBL.GetLatestPetition(objPetOB, out p_val.k);
            if (p_val.dsFileName.Tables[0].Rows.Count > 0)
            {

                rptInterimOrder.DataSource = p_val.dsFileName;
                rptInterimOrder.DataBind();

            }

            p_val.Result = p_val.k;

        }
        catch { }
    }

    public void BindImagesTheme()
    {
        StringBuilder str = new StringBuilder();

        if (ViewState["Blue"].ToString() == "Blue")
        {

            str.Append("<a href='javascript:void(0);' tabindex=\"0\">");

            str.Append("<img alt=\"Left\" id=\"prev\" src= '" + ResolveUrl("~/App_Themes/Blue/images/prev-icon.png") + "' title=\"Move Left\"/>");

            str.Append("</a>");
            str.Append("<a href='javascript:void(0);' tabindex=\"0\" >");
            str.Append("<img alt=\"Right\" id=\"next\" src='" + ResolveUrl("~/App_Themes/Blue/images/next-icon.png") + "' title=\"Move Right\"/>");
            str.Append("</a>");
            LtrTheme.Text = str.ToString();
        }
        else
            if (ViewState["Blue"].ToString() == "Orange")
            {

                str.Append("<a href='javascript:void(0);' tabindex=\"0\" >");

                str.Append("<img alt=\"Left\" id=\"prev\" src= '" + ResolveUrl("~/App_Themes/Orange/images/prev-icon.png") + "' title=\"Move Left\"/>");

                str.Append("</a>");
                str.Append("<a href='javascript:void(0);' tabindex=\"0\" >");
                str.Append("<img alt=\"Right\" id=\"next\" src='" + ResolveUrl("~/App_Themes/Orange/images/next-icon.png") + "' title=\"Move Right\"/>");
                str.Append("</a>");

            }

            else
                if (ViewState["Blue"].ToString() == "Green")
                {

                    str.Append("<a href='javascript:void(0);' tabindex=\"0\" >");

                    str.Append("<img alt=\"Left\" id=\"prev\" src= '" + ResolveUrl("~/App_Themes/Green/images/prev-icon.png") + "' title=\"Move Left\"/>");

                    str.Append("</a>");
                    str.Append("<a href='javascript:void(0);' tabindex=\"0\" >");
                    str.Append("<img alt=\"Right\" id=\"next\" src='" + ResolveUrl("~/App_Themes/Green/images/next-icon.png") + "' title=\"Move Right\"/>");
                    str.Append("</a>");

                }
                else
                    if (ViewState["Blue"].ToString() == "HighContrast")
                    {

                        str.Append("<a href='javascript:void(0);' tabindex=\"0\" >");

                        str.Append("<img alt=\"Left\" id=\"prev\" src= '" + ResolveUrl("~/App_Themes/HighContrast/images/prev-icon.png") + "' title=\"Move Left\"/>");

                        str.Append("</a>");
                        str.Append("<a href='javascript:void(0);' tabindex=\"0\" >");
                        str.Append("<img alt=\"Right\" id=\"next\" src='" + ResolveUrl("~/App_Themes/HighContrast/images/next-icon.png") + "' title=\"Move Right\"/>");
                        str.Append("</a>");

                    }
        LtrTheme.Text = str.ToString();

    }

}
