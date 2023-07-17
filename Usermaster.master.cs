using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class UserMaster : System.Web.UI.MasterPage
{
  
    #region variable declaration

    Project_Variables p_val = new Project_Variables();
    LinkOB lnkObject = new LinkOB();
    LinkBL objlnkBL = new LinkBL();


    #endregion 

    private void Page_PreInit(object sender, System.EventArgs e)
    {

        Page.Theme = "Blue";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        p_val.url = Resources.HercResource.PageUrl.ToString();
        Page.Header.DataBind();
        Bind_Abbreviations();
    }


    public void Bind_Abbreviations()
    {
	try{	
        p_val.sbuilder.Length = 0;
        lnkObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);

        p_val.dSet = objlnkBL.Bind_Abbreviations(lnkObject);

        if (p_val.dSet.Tables[0].Rows.Count > 0)
        {
            for (p_val.i = 0; p_val.i < p_val.dSet.Tables[0].Rows.Count; p_val.i++)
            {
                p_val.menuid = Convert.ToInt16(p_val.dSet.Tables[0].Rows[p_val.i]["Link_Id"]);
                p_val.menu_name = p_val.dSet.Tables[0].Rows[p_val.i]["Name"].ToString();
                p_val.position = Convert.ToInt16(p_val.dSet.Tables[0].Rows[p_val.i]["Position_id"]);
                if (p_val.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Abbreviations) || p_val.menuid == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.Abbreviations_Hindi))
                {
                    if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                    {


                       ltrAbbreviations.Text = ("<a title='" + HttpUtility.HtmlDecode(p_val.menu_name) + "' href='" + ResolveUrl("~/") + p_val.url + p_val.menuid + "_" + Convert.ToInt16(p_val.position) + "_" + p_val.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_val.menu_name) + "</a>");


                    }
                    else
                    {


                       ltrAbbreviations.Text = ("<a title='" + HttpUtility.HtmlDecode(p_val.menu_name) + "' href='" + ResolveUrl("~/") + p_val.url + p_val.menuid + "_" + Convert.ToInt16(p_val.position) + "_" + p_val.menu_name.Replace(" ", "").Replace("&", "and").Replace("'", "").Replace("_", "") + ".aspx'>" + HttpUtility.HtmlDecode(p_val.menu_name) + "</a>");

                    }
                }


            }
        }
	}
        catch { }
    }

   
}
