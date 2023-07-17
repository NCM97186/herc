using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NCM.DAL;
public class BreadcrumDL
{
    #region Data declaration zone


    string language = "Hindi";
    NCMdbAccess ncmdbObject = new NCMdbAccess();
    string HomePage = Resources.HercResource.HomePage;
    string OmbodsmanHomePage = Resources.HercResource.Ombudsman;
   

    #endregion

    #region Default constructor

    public BreadcrumDL()
    {

    }

    #endregion 

    public static string DisplayBreadCrum(string PageName)
    {
        Project_Variables pvar = new Project_Variables();

        BreadcrumDL bc = new BreadcrumDL();
        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        string strHome = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";

        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {
            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage+  "</a></li><li class=\"breadcrum-icon\"> " + PageName + "</li>";
        }
        else
        {
            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/Content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a></li><li class=\"breadcrum-icon\"> " + PageName + "</li>";
        }

        return strHome;

    }

    public static string DisplayMemberAreaBreadCrum(string PageName)
    {
        Project_Variables pvar = new Project_Variables();

        BreadcrumDL bc = new BreadcrumDL();
        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        string strHome = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";

        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {
            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a></li><li class=\"breadcrum-icon\"> " + "<a class=\"current\" href='" + p.ResolveUrl("~/") + "DiscussionPapers.aspx' title='" + bc.HomePage + "'>" + "</a></li><li class=\"breadcrum-icon\"> " + PageName + "</li>";
        }
        else
        {
            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/Content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a></li><li class=\"breadcrum-icon\"> " + PageName + "</li>";
        }

        return strHome;

    }

   


    public static string DisplayGalleryBreadCrum( string PageName)
    {
        Project_Variables pvar = new Project_Variables();

        BreadcrumDL bc = new BreadcrumDL();
        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        string strHome = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";

        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {

            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a></li> <li class=\"breadcrum-icon\"> <a class=\"current\" href='" + p.ResolveUrl("~/") + "PhotoGalleryCategoryIntermediate.aspx' title='" + Resources.HercResource.PhotoGallery + "'>" + Resources.HercResource.PhotoGallery + "</a> </li> <li class=\"breadcrum-icon\">" + PageName + "</li>";
        }
        else
        {
            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/Content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a></li> <li class=\"breadcrum-icon\"> <a class=\"current\" href='" + p.ResolveUrl("~/Content/") + bc.language + "/" + "PhotoGalleryCategoryIntermediate.aspx' title='" + Resources.HercResource.PhotoGallery + "'>" + Resources.HercResource.PhotoGallery + "</a></li><li class=\"breadcrum-icon\"> " + PageName + "</li>";
        }

        return strHome;

    }

    public static string DisplayGalleryBreadCrum(int parentid, string parentname,string PageName)
    {
        Project_Variables pvar = new Project_Variables();

        BreadcrumDL bc = new BreadcrumDL();
        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        string strHome = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";

        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {

            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a></li> <li class=\"breadcrum-icon\">"+ Resources.HercResource.PhotoGallery + "</li> <li class=\"breadcrum-icon\"> "+ parentname + " </li> <li class=\"breadcrum-icon\">" + PageName + "</li>";
        }
        else
        {
            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/Content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a></li> <li class=\"breadcrum-icon\"> " + Resources.HercResource.PhotoGallery + "</li> <li class=\"breadcrum-icon\"> " + parentname + "</li> <li class=\"breadcrum-icon\">" + PageName + "</li>";
        }

        return strHome;

    }

  
 

    public static string DisplayBreadCrum(int PId, int Position, string ParentName, string ChildName)
    {
        BreadcrumDL bc = new BreadcrumDL();
        string strHomeChild = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        string galleryName = ParentName;
        string galleryParent = Resources.HercResource.PhotoGallery;

        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {
           
             if (PId == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer)
            {
                strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"> " + ParentName + "</li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
            }
            else
            {

                strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"> " + ParentName + " </li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
            }
        }
        else
        {
           if (PId == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Hindi)
            {
                strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"> " + ParentName + "</li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
            }
            else
            {
                strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"> " + ParentName + "</li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
            }
        }
        return strHomeChild;
    }

    public static string DisplayBreadCrum(int RootID, int ParentID, int Position, string RootName, string ParentName, string ChildName)
    {
        BreadcrumDL bc = new BreadcrumDL();
        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        string strHomeChild_Child = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        string galleryName = string.Empty;
        if (ParentID == 126)
        {
            galleryName = "Photogallery";
        }
        else if (ParentID == 127)
        {
            galleryName = "Videogallery";
        }
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {

            strHomeChild_Child += "<a class=\"current\" href='"  + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\">" + RootName + "</li> <li class=\"breadcrum-icon\">" + ParentName + " </li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
        }
        else
        {

            strHomeChild_Child += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"> " + RootName + " </li> <li class=\"breadcrum-icon\"> " + ParentName + "</li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";

        }
        return strHomeChild_Child;
    }

    public static string DisplayBreadCrumLatestNews(string PageName, string menuname)
    {
        BreadcrumDL bc = new BreadcrumDL();
        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        string strHome = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {
            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a></li> <li class=\"breadcrum-icon\"> <a class=\"current\" href='" + p.ResolveUrl("~/") + "LatestNews.aspx'>" + PageName + "</a> </li> <li class=\"breadcrum-icon\">" + menuname + "</li>";
        }
        else
        {
            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a></li> <li class=\"breadcrum-icon\"> <a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "LatestNews.aspx'>" + PageName + "</a> </li> <li class=\"breadcrum-icon\">" + menuname + "</li>";
        }
        return strHome;
    }

    public static string DisplayBreadCrumPublicationQuarterly(string ParentName, string ChildName, string year)
    {
        BreadcrumDL bc = new BreadcrumDL();
        string strHomeChild = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";



        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\">" + ParentName + "</li><li class=\"breadcrum-icon\">" + ChildName + "_" + year + "</li>";


        }
        else
        {
            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\">" + ParentName + "</li><li class=\"breadcrum-icon\">" + ChildName + "_" + year + "</li>";


        }
        return strHomeChild;
    }


    public static string DisplayBreadCrumPublication(string ParentName, string ChildName)
    {
        BreadcrumDL bc = new BreadcrumDL();
        string strHomeChild = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        string galleryName = ParentName;
       

        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\">" + ParentName + "</li><li class=\"breadcrum-icon\">" + ChildName + "</li>";

        }
        else
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\">" + ParentName + "</li><li class=\"breadcrum-icon\">" + ChildName + "</li>";

        }
        return strHomeChild;
    }
    #region Get Last Update 
    public string Get_Latest_Update()
    {
        string strdate = string.Empty;
        DataSet ds = new DataSet();
       ds= ncmdbObject.ExecuteDataSet("asp_get_latestupdateProjectDate");
       if (ds.Tables[0].Rows.Count > 0)
       {
           strdate = ds.Tables[0].Rows[0][0].ToString();
       }
       return strdate;
    }
    #endregion


    public static string DisplayMemberAreaBreadCrumpubNoticeold(string PageName)
    {
        Project_Variables pvar = new Project_Variables();


        BreadcrumDL bc = new BreadcrumDL();
        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        string strHome = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";

        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {
            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a></li> <li class=\"breadcrum-icon\"> " +  Resources.HercResource.PublicNotice + "</li><li class=\"breadcrum-icon\"> " + PageName + "</li>";
        }
        else
        {
            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/Content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a></li> <li class=\"breadcrum-icon\"> " + Resources.HercResource.PublicNotice + "<a class=\"current\" href='" + p.ResolveUrl("~/") + "PublicNoticeDetails.aspx' title='" + bc.HomePage + "'>" + "</a></li><li class=\"breadcrum-icon\"> " + PageName + "</li>";
        }

        return strHome;

    }


    //Breadcrumb for the Ombudsman

    public static string DisplayBreadCrumOmbudsman(string ParentName, string ChildName)
    {
        BreadcrumDL bc = new BreadcrumDL();
        string strHomeChild = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        string galleryName = ParentName;


        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li><li class=\"breadcrum-icon\"><a class=\"current\" href='" + p.ResolveUrl("~/") + "Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li> <li class=\"breadcrum-icon\">" + ParentName + "</li><li class=\"breadcrum-icon\">" + ChildName + "</li>";

        }
        else
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li><li class=\"breadcrum-icon\"><a class=\"current\" href='" + p.ResolveUrl("~/") + "Content/Hindi/Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li>  <li class=\"breadcrum-icon\">" + ParentName + "</li><li class=\"breadcrum-icon\">" + ChildName + "</li>";

        }
        return strHomeChild;
    }



    public static string DisplayBreadCrumRTI(string ParentName, string ChildName)
    {
        BreadcrumDL bc = new BreadcrumDL();
        string strHomeChild = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        string galleryName = ParentName;


        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {

			strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"><a  class=\"current\" href='" + p.ResolveUrl("~/") + "RTI/838_1_RTI.aspx' title='" + ParentName + "'>" + ParentName + "</a> </li><li class=\"breadcrum-icon\">" + ChildName + "</li>";
        }
        else
        {
            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"><a  class=\"current\"  href='" + p.ResolveUrl("~/") + "content/Hindi/839_1_RTI.aspx' title='" + ParentName + "'>" + ParentName + "</a> </li><li class=\"breadcrum-icon\">" + ChildName + "</li>";
           

        }
        return strHomeChild;
    }

    public static string DisplayBreadCrumOmbudsman(string PageName)
    {
        Project_Variables pvar = new Project_Variables();

        BreadcrumDL bc = new BreadcrumDL();
        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        string strHome = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";

        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {
            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a></li><li class=\"breadcrum-icon\"><a class=\"current\" href='" + p.ResolveUrl("~/") + "Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li> <li class=\"breadcrum-icon\"> " + PageName + "</li>";
        }
        else
        {
            strHome += "<a class=\"current\" href='" + p.ResolveUrl("~/Content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a></li><li class=\"breadcrum-icon\"><a class=\"current\" href='" + p.ResolveUrl("~/") + "Content/Hindi/Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li> <li class=\"breadcrum-icon\"> " + PageName + "</li>";
        }

        return strHome;

    }

    public static string DisplayBreadCrumOmbudsman(int PId, int Position, string ParentName, string ChildName)
    {
        BreadcrumDL bc = new BreadcrumDL();
        string strHomeChild = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        string galleryName = ParentName;
        string galleryParent = Resources.HercResource.PhotoGallery;

        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {
           
            if (PId == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Footer)
            {
                strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"><a class=\"current\" href='" + p.ResolveUrl("~/") + "Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li><li class=\"breadcrum-icon\"> " + ParentName + "</li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
            }
            else
            {

                strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li><li class=\"breadcrum-icon\"><a class=\"current\" href='" + p.ResolveUrl("~/") + "Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li> <li class=\"breadcrum-icon\"> " + ParentName + " </li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
            }
        }
        else
        {
           
            if (PId == (int)Module_ID_Enum.Menu_ID_Fixed.Archive_Hindi)
            {
                strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"> " + ParentName + "</li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
            }
            else
            {
                strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li><li class=\"breadcrum-icon\"><a class=\"current\" href='" + p.ResolveUrl("~/") + "content/Hindi/Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li> <li class=\"breadcrum-icon\"> " + ParentName + "</li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
            }
        }
        return strHomeChild;
    }
    //End




    public static string DisplayBreadCrumTariff(string ParentName, string ChildName,string subChildName)
    {
        BreadcrumDL bc = new BreadcrumDL();
        string strHomeChild = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        string galleryName = ParentName;


        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"><a  class=\"current\" href='" + p.ResolveUrl("~/") + "Content/383_1_HowToFilePetition.aspx' title='" + ParentName + "'>" + ParentName + "</a> </li><li class=\"breadcrum-icon\">" + ChildName + "</li><li class=\"breadcrum-icon\">" + subChildName + "</li>";

        }
        else
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"><a  class=\"current\" href='" + p.ResolveUrl("~/") + "Content/Hindi/169_1_HowToFilePetition.aspx' title='" + ParentName + "'>" + ParentName + "</a> </li><li class=\"breadcrum-icon\">" + ChildName + "</li><li class=\"breadcrum-icon\">" + subChildName + "</li>";

        }
        return strHomeChild;
    }


    public static string DisplayBreadCrumTariffOmbudsman(string ParentName, string ChildName, string subChildName)
    {
        BreadcrumDL bc = new BreadcrumDL();
        string strHomeChild = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        string galleryName = ParentName;


        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"><a class=\"current\" href='" + p.ResolveUrl("~/") + "Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li> <li class=\"breadcrum-icon\"><a  class=\"current\" href='" + p.ResolveUrl("~/") + "OmbudsmanContent/263_1_Howtofileappeal.aspx' title='" + ParentName + "'>" + ParentName + "</a> </li><li class=\"breadcrum-icon\">" + ChildName + "</li><li class=\"breadcrum-icon\">" + subChildName + "</li>";

        }
        else
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"><a class=\"current\" href='" + p.ResolveUrl("~/") + "Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li> <li class=\"breadcrum-icon\">" + ParentName + "</li><li class=\"breadcrum-icon\">" + ChildName + "</li><li class=\"breadcrum-icon\">" + subChildName + "</li>";

        }
        return strHomeChild;
    }


    public static string DisplayBreadCrumRTIOmbudsman(string ParentName, string ChildName)
    {
        BreadcrumDL bc = new BreadcrumDL();
        string strHomeChild = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        string galleryName = ParentName;


        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {


            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li><li class=\"breadcrum-icon\"><a class=\"current\" href='" + p.ResolveUrl("~/") + "Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li> <li class=\"breadcrum-icon\"><a  class=\"current\" href='" + p.ResolveUrl("~/") + "OmbudsmanContent/447_1_Introduction.aspx' title='" + ParentName + "'>" + ParentName + "</a> </li><li class=\"breadcrum-icon\">" + ChildName + "</li>";
        }
        else
        {
            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li><li class=\"breadcrum-icon\"><a class=\"current\" href='"+ p.ResolveUrl("~/content/") + bc.language + "/" + "Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li> <li class=\"breadcrum-icon\"><a  class=\"current\"  href='" + p.ResolveUrl("~/") + "OmbudsmanContent/Hindi/627_1_RTIact.aspx' title='" + ParentName + "'>" + ParentName + "</a> </li><li class=\"breadcrum-icon\">" + ChildName + "</li>";
            

        }
        return strHomeChild;
    }

    //New function for policies and guidelines on date 02-04-2013

    public static string DisplayBreadCrumPoliciesGuidelines(string rootName,string ParentName, string ChildName)
    {
        BreadcrumDL bc = new BreadcrumDL();
        string strHomeChild = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        
        string galleryName = ParentName;


        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li>" + "<li class=\"breadcrum-icon\">" + rootName + "</li>" + "<li class=\"breadcrum-icon\">" + ParentName + "</li><li class=\"breadcrum-icon\">" + ChildName + "</li>";

        }
        else
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> " + "<li class=\"breadcrum-icon\">" + rootName + "</li>" + "<li class=\"breadcrum-icon\">" + ParentName + "</li><li class=\"breadcrum-icon\">" + ChildName + "</li>";

        }
        return strHomeChild;
    }

    //End

    //Breadcrumb for RTI section

    public static string DisplayBreadCrumRTI(int PId, int Position, string ParentName, string ChildName)
    {
        BreadcrumDL bc = new BreadcrumDL();
        string strHomeChild = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        if (PId != Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Ombudsman) && PId != Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.RTI_Ombudsman_Hindi))
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
            {
               
                strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"> " +  ParentName + " </li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
            }
            else
            {
               
                strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"> " +  ParentName + " </li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
            }
            return strHomeChild;
        }
        else
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
            {
                //strHomeChild += "<a class=\"current\" href=" + p.ResolveUrl("~/") + "index.aspx Title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li><li class=\"breadcrum-icon\"><a class=\"current\" href=" + p.ResolveUrl("~/") + "Ombudsman/Ombudsman.aspx Title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li> <li class=\"breadcrum-icon\"> " + "<a class=\"current\" href=" + p.ResolveUrl("~/") + "OmbudsmanContent/" + PId + "_" + Position + "_" + ParentName + ".aspx>" + ParentName + "</a> </li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
                strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li><li class=\"breadcrum-icon\"><a class=\"current\" href='" + p.ResolveUrl("~/") + "Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li> <li class=\"breadcrum-icon\"> "  + ParentName + " </li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
            }
            else
            {
                //strHomeChild += "<a class=\"current\" href=" + p.ResolveUrl("~/Content/") + bc.language + "/" + "index.aspx Title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"><a class=\"current\" href=" + p.ResolveUrl("~/") + "Ombudsman/Ombudsman.aspx Title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li><li class=\"breadcrum-icon\"> " + "<a class=\"current\" href=" + p.ResolveUrl("~/OmbudsmanContent/") + bc.language + "/" + PId + "_" + Position + "_" + ParentName.Replace(" ", "").Replace("&", "and").Replace("'", "") + ".aspx>" + ParentName + "</a> </li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";

                strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/Content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"><a class=\"current\" href='"+ p.ResolveUrl("~/content/") + bc.language + "/" + "Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li><li class=\"breadcrum-icon\"> " + ParentName + 
                    "</li> <li class=\"breadcrum-icon\">" + ChildName + "</li>";
            }
            return strHomeChild;
        }
    }

    //End



    //Breadcrumb for the Ombudsman

    public static string DisplayBreadCrumOmbudsmanDetails(string ParentName, string ChildName,string Subchild)
    {
        BreadcrumDL bc = new BreadcrumDL();
        string strHomeChild = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        string galleryName = ParentName;


        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li><li class=\"breadcrum-icon\"><a class=\"current\" href='" + p.ResolveUrl("~/") + "Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li> <li class=\"breadcrum-icon\">" + ParentName + "</li><li class=\"breadcrum-icon\">" + ChildName + "</li><li class=\"breadcrum-icon\">" + Subchild + "</li>";

        }
        else
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li><li class=\"breadcrum-icon\"><a class=\"current\" href='" + p.ResolveUrl("~/") + "Content/Hindi/Ombudsman/Ombudsman.aspx' title='" + bc.OmbodsmanHomePage + "'>" + bc.OmbodsmanHomePage + "</a> </li>  <li class=\"breadcrum-icon\">" + ParentName + "</li><li class=\"breadcrum-icon\">" + Subchild + "</li><li class=\"breadcrum-icon\">" + ChildName + "</li>";

        }
        return strHomeChild;
    }




    public static string DisplayBreadCrumTariffDetails(string ParentName, string ChildName, string subChildName,string leaf)
    {
        BreadcrumDL bc = new BreadcrumDL();
        string strHomeChild = "<li>" + Resources.HercResource.YouAreHere.ToString() + "";
        string galleryName = ParentName;


        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == 1)
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/") + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"><a  class=\"current\" href='" + p.ResolveUrl("~/") + "Content/383_1_HowToFilePetition.aspx' title='" + ParentName + "'>" + ParentName + "</a> </li><li class=\"breadcrum-icon\">" + ChildName + "</li><li class=\"breadcrum-icon\">" + subChildName + "</li><li class=\"breadcrum-icon\">" + leaf + "</li>";

        }
        else
        {

            strHomeChild += "<a class=\"current\" href='" + p.ResolveUrl("~/content/") + bc.language + "/" + "index.aspx' title='" + bc.HomePage + "'>" + bc.HomePage + "</a> </li> <li class=\"breadcrum-icon\"><a  class=\"current\" href='" + p.ResolveUrl("~/") + "Content/Hindi/169_1_HowToFilePetition.aspx' title='" + ParentName + "'>" + ParentName + "</a> </li><li class=\"breadcrum-icon\">" + ChildName + "</li><li class=\"breadcrum-icon\">" + subChildName + "</li><li class=\"breadcrum-icon\">" + leaf + "</li>";

        }
        return strHomeChild;
    }
}
