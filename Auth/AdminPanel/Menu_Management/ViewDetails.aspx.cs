using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Auth_AdminPanel_Menu_Management_ViewDetails : BasePage
{
    #region Data declaration zone

    //DataSet ds = new DataSet();
    Menu_ManagementBL menuBL = new Menu_ManagementBL();
    LinkOB lnkObject = new LinkOB();
    Project_Variables p_Var = new Project_Variables();
    UserOB obj_userOB = new UserOB();
    UserBL obj_UserBL = new UserBL();
    Miscelleneous_BL obj_miscelBL = new Miscelleneous_BL();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            HtmlContainerControl navDiv = (HtmlContainerControl)this.Master.FindControl("main_Navigation");
            HtmlContainerControl topRightMenu = (HtmlContainerControl)this.Master.FindControl("topRightMenu");
            HtmlContainerControl FooterDiv = (HtmlContainerControl)this.Master.FindControl("FooterDiv");
            HtmlContainerControl AccessibilityDiv = (HtmlContainerControl)this.Master.FindControl("AccessibilityDiv");
            
            navDiv.Visible = false;
            topRightMenu.Visible = false;
            FooterDiv.Visible = false;
            AccessibilityDiv.Visible = false;
            bindData_For_Editing(Convert.ToInt32(Request.QueryString["Id"]), Convert.ToInt32(Request.QueryString["Status_Id"]));
        }


    }

    #region Function to bind data in EDIT mode

    public void bindData_For_Editing(int TempLink_Id, int StatusID)
    {

        try
        {
            lnkObject.StatusId = StatusID;
            lnkObject.TempLinkId = TempLink_Id;
            p_Var.dSet = menuBL.getMenu_For_Editing(lnkObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ltrlMainContent.Text = p_Var.dSet.Tables[0].Rows[0]["Details"].ToString();
            }
        }
        catch
        {
            throw;
        }


    }

    #endregion
}