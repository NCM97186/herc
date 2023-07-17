using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NCM.DAL;
using System.Data.SqlClient;



public partial class AdminPanel_viewdetails : System.Web.UI.Page
{
    #region Data Declaration Zone

    NCMdbAccess obj_ncmdb = new NCMdbAccess();
    UserBL obj_UserBL = new UserBL();
    UserOB obj_userOB = new UserOB();
    DataSet dSet = new DataSet();
    LinkOB obj_linkOB = new LinkOB();
    LinkBL obj_linkBL = new LinkBL();
    Miscelleneous_BL obj_Miscel = new Miscelleneous_BL();

    #endregion

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Username"] == null)
        //{
        //    Response.Redirect("~/AdminPanel/Login.aspx");
        //}
        Response.CacheControl = "no-cache";
        Response.Cache.SetExpires(System.DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        if (!IsPostBack)
        {
            bindActivity();
            bind_Module_Details();
        }


    }
      

    #endregion

    #region Function to display Details

    public void bindActivity()
    {
        try
        {
            if (Request.QueryString["User_ID"] != null)
            {
                PnlUserDetails.Visible = true;
                PnlModule.Visible = false;

                int recid = Convert.ToInt32(Request.QueryString["User_ID"]);
                obj_userOB.UserId=recid;
                dSet = obj_UserBL.ASP_Get_User_Role_Display(obj_userOB);
                LblName.Text=dSet.Tables[0].Rows[0]["Name"].ToString();
                LblAddress.Text=dSet.Tables[0].Rows[0]["Address"].ToString();
                LblCity.Text=dSet.Tables[0].Rows[0]["City"].ToString();
                LblCountry.Text=dSet.Tables[0].Rows[0]["Country"].ToString();
                LblEmail.Text=dSet.Tables[0].Rows[0]["Email"].ToString();
                LblMobile.Text = dSet.Tables[0].Rows[0]["Mobile_No"].ToString();
                LblRole.Text=dSet.Tables[0].Rows[0]["Role_Name"].ToString();
            }
        }

        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to display Module Details

    public void bind_Module_Details()
    {
        try
        {
            if (Request.QueryString["Temp_Link_Id"] != null)
            {

                PnlUserDetails.Visible = false;
                PnlModule.Visible = true;
                int recid = Convert.ToInt32(Request.QueryString["Temp_Link_Id"]);
                obj_linkOB.TempLinkId = recid;
                obj_linkOB.StatusId =Convert.ToInt32(Session["Status_Id"]);
                dSet = obj_linkBL.ASP_Links_DisplayBYID(obj_linkOB);
                LblTitle.Text = dSet.Tables[0].Rows[0]["Name"].ToString();
                //Lbldescription.Text = dSet.Tables[0].Rows[0]["Details"].ToString();
                //ImgName.ImageUrl = ResolveUrl("~/" +ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/Image/")+dSet.Tables[0].Rows[0]["Image_Name"].ToString();
                LblFileName.Text = dSet.Tables[0].Rows[0]["File_Name"].ToString();
                if (dSet.Tables[0].Rows[0]["Start_Date"].ToString() != null && dSet.Tables[0].Rows[0]["Start_Date"].ToString() !="")
                {
                    LblStartDate.Text = obj_Miscel.getDateFormatddMMYYYY(dSet.Tables[0].Rows[0]["Start_Date"].ToString());
                }
                else
                {
                    LblStartDate.Text = "";
                }
                if (dSet.Tables[0].Rows[0]["End_Date"].ToString() != null && dSet.Tables[0].Rows[0]["End_Date"].ToString() !="")
                {
                    LblEndDate.Text = obj_Miscel.getDateFormatddMMYYYY(dSet.Tables[0].Rows[0]["End_Date"].ToString());
                }
                else
                {
                    LblEndDate.Text = "";
                }
            
            }
        }

        catch
        {
            throw;
        }
    }

    #endregion

  

}
