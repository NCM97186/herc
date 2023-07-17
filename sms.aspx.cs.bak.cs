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

public partial class sms : System.Web.UI.Page
{

    #region Data declaration zone

    LinkOB lnkObject = new LinkOB();
    LinkBL obj_linkBL = new LinkBL();
    DataSet dSet = new DataSet();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Project_Variables p_Var = new Project_Variables();
    UserBL obj_UserBL = new UserBL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petBL = new PetitionBL();
    PaginationBL pagingBL = new PaginationBL();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbuilder = new StringBuilder();
        StringBuilder sbuilderSms = new StringBuilder();
        sbuilder.Append("<b>HERC - CMS Menu - <br/></b>");
        ///* Code to send sms */
        char[] splitter = { ';' };
        PetitionOB petObjectNew = new PetitionOB();
        DataSet dsSms = new DataSet();
        string textmessage;
        string strUrl = sbuilderSms.ToString();
        string[] split = strUrl.Split(';');
        ArrayList list = new ArrayList();
        foreach (string item in split)
        {
            list.Add(item.Trim());
        }




        //code to get multiple public notice in list

        string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "");
        string[] stringSeparators = new string[] { "<br/>" };
        string[] splitPublicNotice = strpublicnotice.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
        ArrayList listPublicNotice = new ArrayList();
        foreach (string itemPublicNotice in splitPublicNotice)
        {
            listPublicNotice.Add(itemPublicNotice.Trim());
        }
        //miscellBL.Sendsms("this is testing", Session["UserName"].ToString(), "8743073005", "this is testing");
        foreach (string strPublicNotice in listPublicNotice)
        {
            if (strPublicNotice != string.Empty && strPublicNotice != "")
            {
                list[0] = "8743073005";
                foreach (string str in list)
                {
                    if (str != string.Empty)
                    {

                        //string message = ViewState["Title"].ToString();
                        string message = strPublicNotice;

                        if (message.Length > 150)
                        {
                            message = strPublicNotice.ToString().Substring(0, 150) + "...";
                        }
                        else
                        {
                            message = strPublicNotice.ToString();
                        }
                        textmessage = "HERC - Record published -";

                        miscellBL.Sendsms(message, "admin", str, textmessage);

                    }

                }

            }
        }

        /* End */
    }
}