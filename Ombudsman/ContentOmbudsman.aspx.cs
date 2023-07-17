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
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class Ombudsman_ContentOmbudsman : BasePageOmbudsman
{
    #region Data delcaration zone

    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    Project_Variables p_var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    LinkOB lnkObject = new LinkOB();
    public string headerName = string.Empty;
    public string ParentName = string.Empty;
    public static string UrlPrint = string.Empty;
    public static int lengthCounter = 0;
    string RootParentName = string.Empty;
    string Childname = string.Empty;
    string strbreadcrum = string.Empty;
    string PositionID = HttpContext.Current.Request.QueryString["position"];
    string PageID = string.Empty; //HttpContext.Current.Request.QueryString["id"].ToString().Substring(6);
    string ParentID = string.Empty;
    string browserTitle = string.Empty;
    int RootID;

    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string PageTitle = string.Empty;
    public string lastUpdatedDate = string.Empty;

    #endregion

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        p_var.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);  //Convert.ToInt32(Request.QueryString["position"].ToString());
        p_var.LanguageID = Convert.ToInt16(Resources.HercResource.Lang_Id);
        //p_var.url = Resources.HercResource.PageUrl.ToString();
        p_var.url = Resources.HercResource.OmbudsmanPageUrl.ToString();
        if (!IsPostBack)
        {
	try{
            //p_var.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString(); //Request.QueryString["menuid"].ToString();
			if (RewriteModule.RewriteContext.Current.Params["menuid"]!=null)
            {
                p_var.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
            }
            if (p_var.PageID.Length > 6)
            {
                p_var.PageID = p_var.PageID.Substring(6);
            }
            else
            {
                p_var.PageID = p_var.PageID.ToString();
            }
             if (p_var.PageID != null && p_var.PageID != "")
            {
                if (Convert.ToInt16(p_var.PageID) == 323||Convert.ToInt16(p_var.PageID)==465)
                {
                    if (Convert.ToInt16(p_var.PageID) == 323)
                    {
                        Response.Redirect("~/Ombudsman/Ombudsman.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/content/Hindi/Ombudsman/Ombudsman.aspx");
                    }
                }
                else
                {
                    bindMainContent();
                }
            }
	}
            
	catch { }

        }
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssRef);
        }
    }

    #endregion

    #region Function to bind Main Contents

    public void bindMainContent()
    {
        DataSet subMenuData = new DataSet();
        DataSet parent = new DataSet();
        LinkOB _lnkSubmenuObject = new LinkOB();

        //Variables to get child submenu

        LinkOB subSubMenuOB = new LinkOB();
        DataSet sub_dataSet = new DataSet();

        //end

        try
        {

            lnkObject.linkID = Convert.ToInt16(p_var.PageID);

            lnkObject.LangId = p_var.LanguageID;
            p_var.dSet = menuBL.get_Cliked_Parent_Menu(lnkObject);
            p_var.parentID = p_var.dSet.Tables[0].Rows[0]["link_parent_id"].ToString();
            lnkObject.LinkParentId = Convert.ToInt16(p_var.parentID);
            p_var.position = Convert.ToInt16(p_var.dSet.Tables[0].Rows[0]["position_id"]);
            if (p_var.dSet.Tables[0].Rows.Count > 0)
            {
                lastUpdatedDate = p_var.dSet.Tables[0].Rows[0]["Last_update"].ToString();
                headerName = p_var.dSet.Tables[0].Rows[0]["name"].ToString();
                ParentName = p_var.dSet.Tables[0].Rows[0]["parent"].ToString();
                browserTitle = p_var.dSet.Tables[0].Rows[0]["Browser_Title"].ToString();
                ViewState["header"] = p_var.dSet.Tables[0].Rows[0]["placeholderone"].ToString();

                if (lnkObject.LinkParentId == 0)
                {
                    hparentId.InnerText = headerName;
                }
                else
                {
                    hparentId.InnerText = ParentName;
                }

                ltrlMainContent.Text = HttpUtility.HtmlDecode(p_var.dSet.Tables[0].Rows[0]["details"].ToString());

                //Code to get string and attach pdf image and define its size

                p_var.sbuilder.Append(ltrlMainContent.Text);
                IList<int> indeces = new List<int>();
                IList<int> indecesHtmlTag = new List<int>();
                IList<int> indecespdf = new List<int>();

                foreach (Match match in Regex.Matches(p_var.sbuilder.ToString(), "<a"))
                {
                    indeces.Add(match.Index);

                }

                foreach (Match matchpdf in Regex.Matches(p_var.sbuilder.ToString(), ".pdf"))
                {
                    indecespdf.Add(matchpdf.Index);


                }

                foreach (Match matchhtml in Regex.Matches(p_var.sbuilder.ToString(), "</a>"))
                {
                    indecesHtmlTag.Add(matchhtml.Index);
                }
                lengthCounter = 0;
                if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                {
                    for (int i = 0; i < indeces.Count; i++)
                    {
                        int str = 0;
                        int html = 0;
                        for (int j = i; j < indecespdf.Count; j++)
                        {
                            str = indecespdf[j];
                            indecespdf.Add(str);
                            break;
                        }

                        for (int k = i; k <= indecesHtmlTag.Count; k++)
                        {
                            str = indeces[i] + lengthCounter * 110;

                            int coutner;
                            coutner = (indecesHtmlTag[k] + (lengthCounter * 110)) - str; //Count number of characters between <a and </a> tags.
                            int diff = (indecesHtmlTag[k] + (lengthCounter * 110)) - str; //difference between <a and </a> tags.
                            if (diff > 0)
                            {
                                string strContent = p_var.sbuilder.ToString().Substring(str, diff); //Get string between <a and </a> tags.

                                if (strContent.Contains(".pdf")) //Condition check to find .pdf string in between <a and </a> tags.
                                {

                                    IList<int> indecesHtmlAnchor = new List<int>();
                                    IList<int> indecesAnchorpdf = new List<int>();
                                    IList<int> indecesAnchorpdfPath = new List<int>();
                                    foreach (Match matchAnchorHtml in Regex.Matches(strContent, "<a"))
                                    {
                                        indecesHtmlAnchor.Add(matchAnchorHtml.Index);

                                    }

                                    foreach (Match matchAnchorpdf in Regex.Matches(strContent, ".pdf"))
                                    {
                                        indecesAnchorpdf.Add(matchAnchorpdf.Index);

                                    }
                                    foreach (Match matchAnchorpdfPath in Regex.Matches(strContent, "WriteReadData"))
                                    {
                                        indecesAnchorpdfPath.Add(matchAnchorpdfPath.Index);

                                    }
                                    if (indecesAnchorpdfPath.Count > 0 && indecesAnchorpdf.Count > 0)
                                    {
                                        int difference = indecesAnchorpdf[0] - indecesAnchorpdfPath[0];
                                        if (difference > 0)
                                        {
                                            string myPdfFilePath = strContent.Substring(indecesAnchorpdfPath[0], difference);
                                            string myPdfFile = strContent.Substring(indecesHtmlAnchor[0], indecesAnchorpdf[0]);// get string before .pdf
                                            myPdfFile = myPdfFile.Substring(myPdfFile.LastIndexOf("/") + 1); //Get pdf name 
                                            myPdfFilePath = myPdfFilePath.Substring(0, myPdfFilePath.LastIndexOf("/") + 1);


                                            FileInfo finfo = new FileInfo(Server.MapPath(ResolveUrl("~/") + myPdfFilePath) + myPdfFile.Replace("%20", " ") + ".pdf");
                                            if (finfo.Exists)
                                            {
                                                double FileInBytes = finfo.Length;
                                                string filesize = miscellBL.fileSizeForContentPage(FileInBytes);

                                                p_var.sbuilder.Insert(indecesHtmlTag[k] + lengthCounter * 110, "<img src='../images/pdf-icon.jpg' title='View Document' width='15' alt='View Document' height=\"15\" />" + "(" + filesize + ")");
                                                lengthCounter = lengthCounter + 1;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        }

                    }

                }
                else
                {
                    for (int i = 0; i < indeces.Count; i++)
                    {
                        int str = 0;
                        int html = 0;
                        for (int j = i; j < indecespdf.Count; j++)
                        {
                            str = indecespdf[j];
                            indecespdf.Add(str);
                            break;
                        }

                        for (int k = i; k <= indecesHtmlTag.Count; k++)
                        {
                            str = indeces[i] + lengthCounter * 113;

                            int coutner;
                            coutner = (indecesHtmlTag[k] + (lengthCounter * 113)) - str; //Count number of characters between <a and </a> tags.
                            int diff = (indecesHtmlTag[k] + (lengthCounter * 113)) - str; //difference between <a and </a> tags.
                            if (diff > 0)
                            {
                                string strContent = p_var.sbuilder.ToString().Substring(str, diff); //Get string between <a and </a> tags.

                                if (strContent.Contains(".pdf")) //Condition check to find .pdf string in between <a and </a> tags.
                                {

                                    IList<int> indecesHtmlAnchor = new List<int>();
                                    IList<int> indecesAnchorpdf = new List<int>();
                                    IList<int> indecesAnchorpdfPath = new List<int>();
                                    foreach (Match matchAnchorHtml in Regex.Matches(strContent, "<a"))
                                    {
                                        indecesHtmlAnchor.Add(matchAnchorHtml.Index);

                                    }

                                    foreach (Match matchAnchorpdf in Regex.Matches(strContent, ".pdf"))
                                    {
                                        indecesAnchorpdf.Add(matchAnchorpdf.Index);

                                    }
                                    foreach (Match matchAnchorpdfPath in Regex.Matches(strContent, "WriteReadData"))
                                    {
                                        indecesAnchorpdfPath.Add(matchAnchorpdfPath.Index);

                                    }
                                    if (indecesAnchorpdfPath.Count > 0 && indecesAnchorpdf.Count > 0)
                                    {
                                        int difference = indecesAnchorpdf[0] - indecesAnchorpdfPath[0];
                                        if (difference > 0)
                                        {
                                            string myPdfFilePath = strContent.Substring(indecesAnchorpdfPath[0], difference);
                                            string myPdfFile = strContent.Substring(indecesHtmlAnchor[0], indecesAnchorpdf[0]);// get string before .pdf
                                            myPdfFile = myPdfFile.Substring(myPdfFile.LastIndexOf("/") + 1); //Get pdf name 
                                            myPdfFilePath = myPdfFilePath.Substring(0, myPdfFilePath.LastIndexOf("/") + 1);


                                            FileInfo finfo = new FileInfo(Server.MapPath(ResolveUrl("~/") + myPdfFilePath) + myPdfFile.Replace("%20", " ") + ".pdf");
                                            if (finfo.Exists)
                                            {
                                                double FileInBytes = finfo.Length;
                                                string filesize = miscellBL.fileSizeForContentPage(FileInBytes);

                                                p_var.sbuilder.Insert(indecesHtmlTag[k] + lengthCounter * 113, "<img src='../../images/pdf-icon.jpg' title='View Document' width='15' alt='View Document' height=\"15\" />" + "(" + filesize + ")");
                                                lengthCounter = lengthCounter + 1;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        }

                    }
                }


                //End
                 ltrlMainContent.Text = HttpUtility.HtmlDecode(p_var.sbuilder.ToString().Replace("&", "&amp;"));




                PageTitle = p_var.dSet.Tables[0].Rows[0]["Page_Title"].ToString();
                MetaKeyword = p_var.dSet.Tables[0].Rows[0]["Meta_Keywords"].ToString();
                MetaDescription = p_var.dSet.Tables[0].Rows[0]["Mate_Description"].ToString();
                if (p_var.parentID != "0")
                {

                    if (p_var.parentID == "248")
                    {
                        p_var.str = BreadcrumDL.DisplayBreadCrumRTI(Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Ombudsman), p_var.position, p_var.dSet.Tables[0].Rows[0]["parent"].ToString(), p_var.dSet.Tables[0].Rows[0]["name"].ToString());
                    }
                    else if (p_var.parentID == "308")
                    {
                        p_var.str = BreadcrumDL.DisplayBreadCrumRTI(Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Ombudsman_Hindi), p_var.position, p_var.dSet.Tables[0].Rows[0]["parent"].ToString(), p_var.dSet.Tables[0].Rows[0]["name"].ToString());
                    }
                    else
                    {
                        p_var.str = BreadcrumDL.DisplayBreadCrumOmbudsman(Convert.ToInt16(p_var.parentID), p_var.position, p_var.dSet.Tables[0].Rows[0]["parent"].ToString(), p_var.dSet.Tables[0].Rows[0]["name"].ToString());
                    }
                }
                else
                {
                  // p_var.str = BreadcrumDL.DisplayBreadCrum(p_var.dSet.Tables[0].Rows[0]["name"].ToString());
                    p_var.str = BreadcrumDL.DisplayBreadCrumOmbudsman(p_var.dSet.Tables[0].Rows[0]["name"].ToString());
                }
                ltrlBreadcrum.Text = p_var.str;
            }
            if (Convert.ToInt16(p_var.dSet.Tables[0].Rows[0]["Link_Level"]) == 2)
            {
                p_var.parentID = lnkObject.LinkParentId.ToString();
                //Get Root name of for implimenting third level breadcrum
                Menu_ManagementBL menuRootBL = new Menu_ManagementBL();
                LinkOB linkObject = new LinkOB();
                DataSet dDataSet = new DataSet();
                linkObject.linkID = Convert.ToInt16(p_var.parentID);
                linkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
                dDataSet = menuRootBL.getParent_name_ofRoot(linkObject);
                if (dDataSet.Tables[0].Rows.Count > 0)
                {
                    RootParentName = dDataSet.Tables[0].Rows[0]["name"].ToString();
                    RootID = Convert.ToInt16(dDataSet.Tables[0].Rows[0]["link_id"]);
                }

                //End
                Childname = p_var.dSet.Tables[0].Rows[0]["name"].ToString();
                
                strbreadcrum = BreadcrumDL.DisplayBreadCrum(RootID, Convert.ToInt16(p_var.parentID), Convert.ToInt16(PositionID), RootParentName, ParentName, Childname);
                ltrlBreadcrum.Text = strbreadcrum;
            }

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitleOmbudsman;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion

 
}
