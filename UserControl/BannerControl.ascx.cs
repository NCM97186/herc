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

public partial class UserControls_BannerControl : System.Web.UI.UserControl
{
    #region Data declaration zone

    LinkOB lnkObject = new LinkOB();
    Project_Variables p_var = new Project_Variables();
    Menu_ManagementBL menuBL = new Menu_ManagementBL();

    #endregion

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    #endregion

    //Area for all the user-defined function to bind banner

    public void Bind()
    {
	try{
		lnkObject.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Banner);
        p_var.dSet = menuBL.get_Banner(lnkObject);
        for (p_var.i = 0; p_var.i < p_var.dSet.Tables[0].Rows.Count; p_var.i++)
        {
            if (p_var.i == 0)
            {
                p_var.sbuilder.Append("<div class=\"item active\">");

            }
            else
            {
                p_var.sbuilder.Append("<div class=\"item\" >");

            }
           
            p_var.sbuilder.Append("<img width=\"736\" height=\"264\" alt='"+ p_var.dSet.Tables[0].Rows[p_var.i]["alt_tag"].ToString()+"'"+" src='" + ResolveUrl("~\\WriteReadData\\Image\\" + p_var.dSet.Tables[0].Rows[p_var.i]["Image_Name"].ToString() + " ") + "' />");
            p_var.sbuilder.Append("</div>");
        }

        litContent.Text = p_var.sbuilder.ToString();
		}
		catch{}
    }


    //End
}
