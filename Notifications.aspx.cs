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

public partial class Notifications :BasePage
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    PetitionBL obj_petBL = new PetitionBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    public static string UrlPrint = string.Empty;
    public string lastUpdatedDate = string.Empty;
    public string headerName = string.Empty;
    string PageTitle = string.Empty;
    public string path = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
	try{
        herc.Title = Resources.HercResource.herc.ToString();
        other.Title = Resources.HercResource.Others.ToString();
        repealed.Title = Resources.HercResource.RepealedNotifications.ToString();
        hercHindi.Title = Resources.HercResource.herc.ToString();
        otherHindi.Title = Resources.HercResource.Others.ToString();
        repealedHindi.Title = Resources.HercResource.RepealedNotifications.ToString();

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			 ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);

        }
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {
            if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["id"]) == (Convert.ToInt16(Module_ID_Enum.hercType.herc)))
            {
                herc.Attributes["class"] = "current";
                str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Notifications, Resources.HercResource.herc);
                gvNotifications.ToolTip = Resources.HercResource.Notifications;
            }
            else if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["id"]) == (Convert.ToInt16(Module_ID_Enum.hercType.other)))
            {
                other.Attributes["class"] = "current";
                str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Notifications, Resources.HercResource.Others);
            }
            else
            {
                repealed.Attributes["class"] = "current";
                str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Notifications, Resources.HercResource.RepealedNotifications);
                gvNotifications.ToolTip = Resources.HercResource.RepealedNotifications;
            }
        }
        else
        {
            if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["id"]) == (Convert.ToInt16(Module_ID_Enum.hercType.herc)))
            {
                hercHindi.Attributes["class"] = "current";
                str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Notifications, Resources.HercResource.herc);
                gvNotifications.ToolTip = Resources.HercResource.Notifications;
            }
            else if (Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["id"]) == (Convert.ToInt16(Module_ID_Enum.hercType.other)))
            {
                otherHindi.Attributes["class"] = "current";
                str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Notifications, Resources.HercResource.Others);
            }
            else
            {
                repealedHindi.Attributes["class"] = "current";
                str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Notifications, Resources.HercResource.RepealedNotifications);
                gvNotifications.ToolTip = Resources.HercResource.RepealedNotifications;
            }
        }
        ltrlBreadcrum.Text = str.ToString();
        p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["pdf"] + "/");
        path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["pdf"] + "/";
        obj_miscel.MakeAccessible(gvNotifications);
        if (!IsPostBack)
        {
            Bindgrid(1);
        }


		if (RewriteModule.RewriteContext.Current.Params["id"] != null)
        {
            if (Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["id"].ToString()) == 1)
            {
                headerName = Resources.HercResource.herc;
            }
            else
            {
                headerName = Resources.HercResource.RepealedNotifications;
            }
        }
        else
        {
            headerName = Resources.HercResource.RepealedNotifications;
        }

        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.Notifications;
        }
        else
        {
            PageTitle = Resources.HercResource.Notifications;
        }
		}
	catch{}
   }
    public void Bindgrid(int pageIndex)
    {  
        obj_petOB.PageIndex = pageIndex;
        obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        obj_petOB.ActionType = Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["id"]);

        p_Val.dSet = obj_petBL.Get_Notifications(obj_petOB, out p_Val.k);
       

        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            gvNotifications.DataSource = p_Val.dSet;
            gvNotifications.DataBind();
            //DlastUpdate.Visible = true;
            lastUpdatedDate =p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            MetaKeyword = p_Val.dSet.Tables[0].Rows[0]["Meta_Keywords"].ToString();
            MetaDescription = p_Val.dSet.Tables[0].Rows[0]["Mate_Description"].ToString();
            MetaLng = p_Val.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = p_Val.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
        }
        else
        {
            DlastUpdate.Visible = false;
            rptPager.Visible    = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;

			     lblmsg.Visible = true;
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
            //lblmsg.ForeColor = System.Drawing.Color.Red;
        }
        p_Val.Result = p_Val.k;
        if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_Val.Result, pageIndex, ddlPageSize, rptPager);
            rptPager.Visible = true;
        }
        else
        {
            rptPager.Visible = false;
        }


    }

    protected void gvNotifications_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkOB objlnkOB = new LinkOB();
            LinkBL objlnkBL = new LinkBL();
            Label lblDetails = (Label)e.Row.FindControl("lblDetails");
            LinkButton lnkTitle = (LinkButton)e.Row.FindControl("lnkTitle");
      
        
            //==================
         
            Literal ltrlConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            HiddenField hyd = (HiddenField)e.Row.FindControl("hid");
            Label lblModule = (Label)e.Row.FindControl("lblModule");
            Label lblLinkId = (Label)e.Row.FindControl("lblId");
            objlnkOB.TempLinkId = Convert.ToInt16(lblLinkId.Text);
            objlnkOB.ModuleId = Convert.ToInt16(lblModule.Text);
            p_Val.dsFileName = objlnkBL.getFileName(objlnkOB);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {

             
                p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Val.sbuilder.Append("<a href='" + path + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='" + Resources.HercResource.ViewDocument + "' width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> ");
                    if (p_Val.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(path) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(path) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }

                    p_Val.sbuilder.Append("</a>");

                }
            }

           
            ltrlConnectedFile.Text = p_Val.sbuilder.ToString();
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            if (hyd.Value != null && hyd.Value != "")
            {
                lblDetails.Visible = false;
            }
            else
            {
                lblDetails.Visible = true;
                lnkTitle.Visible = false;
            }
            //====================
           
        }
    }
    protected void gvNotifications_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "view")
        {



            string file = e.CommandArgument.ToString();
            p_Val.Path = p_Val.Path + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(p_Val.Path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(p_Val.Path));
                Response.Clear();
                Response.WriteFile(p_Val.Path);
                Response.End();
               


                

            }
        }

        if (e.CommandName == "ViewDetail")
        {
            p_Val.stringTypeID = e.CommandArgument.ToString();

            p_Val.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "ViewDetails.aspx?Link_Id=" + p_Val.stringTypeID + "&" + "ModuleId= " + Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification)) + "', 'mywindow', " +
                                                  "'menubar=no, resizable=Yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                                  "</script>";
          
            this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
        }
        
    }


    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Bindgrid(pageIndex);
    }

    #endregion


    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bindgrid(1);
    }

    #endregion

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
        MetaLang = MetaLng;
        MetaTitle = MetaTitles;
    }

    #endregion


}
