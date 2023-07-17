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

public partial class GuideLines : BasePage
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL = new PetitionBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    MixModuleBL mixModuleBL = new MixModuleBL();
    public string headerName = string.Empty;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    public string path = string.Empty;
    public string lastUpdatedDate = string.Empty;
    public static string UrlPrint = string.Empty;
    LinkOB lnkObject = new LinkOB();
    #endregion

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        herc.Title = Resources.HercResource.herc.ToString();
        other.Title = Resources.HercResource.Others.ToString();
        repealed.Title = Resources.HercResource.RepealedGuidelines.ToString();
        herc_Hindi.Title = Resources.HercResource.herc.ToString();
        other_Hindi.Title = Resources.HercResource.Others.ToString();
        repealed_Hindi.Title = Resources.HercResource.RepealedGuidelines.ToString();
        path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["pdf"] + "/";
		if (RewriteModule.RewriteContext.Current.Params["id"] != null)
        {
        if (RewriteModule.RewriteContext.Current.Params["id"].ToString() == "1")
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                herc.Attributes["class"] = "current";
            }
            else
            {
                herc_Hindi.Attributes["class"] = "current";
            }
            str = BreadcrumDL.DisplayBreadCrumPoliciesGuidelines(Resources.HercResource.policiesGuidelines,Resources.HercResource.Guidelines, Resources.HercResource.herc);
        }
        else if (RewriteModule.RewriteContext.Current.Params["id"].ToString() == "4")
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                other.Attributes["class"] = "current";
            }
            else
            {
                other_Hindi.Attributes["class"] = "current";
            }
            str = BreadcrumDL.DisplayBreadCrumPublication(Resources.HercResource.Guidelines, Resources.HercResource.Others);
        }
        else
        {
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                repealed.Attributes["class"] = "current";
            }
            else
            {
                repealed_Hindi.Attributes["class"] = "current";
            }
            str = BreadcrumDL.DisplayBreadCrumPoliciesGuidelines(Resources.HercResource.policiesGuidelines,Resources.HercResource.Guidelines, Resources.HercResource.RepealedGuidelines);
        }
		}
        ltrlBreadcrum.Text = str.ToString();
        p_Val.Path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["pdf"] + "/";
        //obj_miscel.MakeAccessible(gvRegulations);
        Bindgrid(1);

        if (RewriteModule.RewriteContext.Current.Params["id"] != null)
        {
		try{
            if (Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["id"].ToString()) == 1)
            {
                headerName = Resources.HercResource.herc;
            }
            else
            {
                headerName = Resources.HercResource.RepealedGuidelines;
            }
		}
			catch{}
        }

        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.policiesGuidelines;
        }
        else
        {
            PageTitle = Resources.HercResource.policiesGuidelines;
        }


        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            Bindgrid(1);
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
			 ((HtmlGenericControl)this.Page.Master.FindControl("divScroller")).Visible = false;
            Page.Header.Controls.Add(cssRef);
        }
    }

    #endregion


    //Area for all the user defined functions

    ////public void Bindgrid(int pageIndex)
    ////{
    ////    obj_petOB.PageIndex = pageIndex;
    ////    obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
    ////    obj_petOB.ActionType = Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["id"].ToString());

    ////    p_Val.dSet = mixModuleBL.Get_Guidelines(obj_petOB, out p_Val.k);


    ////    if (p_Val.dSet.Tables[0].Rows.Count > 0)
    ////    {
    ////        rptPager.Visible = true;
    ////        ddlPageSize.Visible = true;
    ////        lblPageSize.Visible = true;
    ////        for (int i = 0; i < p_Val.dSet.Tables[0].Rows.Count; i++)
    ////        {

    ////            p_Val.sbuilder.Append("<ul>");
    ////            p_Val.sbuilder.Append("<li>");
    ////          //  p_Val.sbuilder.Append("<a href='" + p_Val.Path + p_Val.dSet.Tables[0].Rows[i]["File_Name"] + "' Text='" + p_Val.dSet.Tables[0].Rows[i]["Name"] + "' target='_blank'>" + p_Val.dSet.Tables[0].Rows[i]["Name"].ToString().Replace("&lt;p&gt;", "").Replace("&lt;/p&gt;", "") + "</a>");

    ////            if (p_Val.dSet.Tables[0].Rows[i]["url"] != DBNull.Value && p_Val.dSet.Tables[0].Rows[i]["url"].ToString() != "")
    ////            {
    ////                p_Val.sbuilder.Append("<a onclick=\"javascript:return confirm('This link shall take you to a webpage outside this website. Click OK to continue. Click ancel to stop.');\" href='" + p_Val.dSet.Tables[0].Rows[i]["url"] + "' Text='" + p_Val.dSet.Tables[0].Rows[i]["Name"] + "' target='_blank'>" + HttpUtility.HtmlDecode(p_Val.dSet.Tables[0].Rows[i]["Name"].ToString().Replace("&lt;p&gt;", "").Replace("&lt;/p&gt;", "")) + "</a><img height=\"14px\" width=\"14px\" src=" + ResolveUrl("~/images/external.png") + "></img>");
    ////            }
    ////            else if (p_Val.dSet.Tables[0].Rows[i]["File_Name"] != DBNull.Value && p_Val.dSet.Tables[0].Rows[i]["File_Name"].ToString() != "")
    ////            {
    ////                p_Val.sbuilder.Append("<a href='" + p_Val.Path + p_Val.dSet.Tables[0].Rows[i]["File_Name"] + "' Text='" + p_Val.dSet.Tables[0].Rows[i]["Name"] + "' target='_blank'>" + HttpUtility.HtmlDecode(p_Val.dSet.Tables[0].Rows[i]["Name"].ToString().Replace("&lt;p&gt;", "").Replace("&lt;/p&gt;", "")) + "</a>");
    ////            }
    ////            else
    ////            {
    ////                p_Val.sbuilder.Append(p_Val.dSet.Tables[0].Rows[i]["Name"].ToString().Replace("&lt;p&gt;", "").Replace("&lt;/p&gt;", ""));
    ////            }




    ////            p_Val.sbuilder.Append("</li>");
    ////            p_Val.sbuilder.Append("</ul>");
    ////        }
    ////        LtrRegulation.Text = p_Val.sbuilder.ToString();
    ////        lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
    ////    }
    ////    else
    ////    {
    ////        rptPager.Visible = false;
    ////        ddlPageSize.Visible = false;
    ////        lblPageSize.Visible = false;
    ////    }
    ////    p_Val.Result = p_Val.k;
    ////    if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
    ////    {
    ////        pagingBL.Paging_Show(p_Val.Result, pageIndex, ddlPageSize, rptPager);
    ////        rptPager.Visible = true;
    ////    }
    ////    else
    ////    {
    ////        rptPager.Visible = false;
    ////    }


    ////}

    //End


    public void Bindgrid(int pageIndex)
    {
       try{
        obj_petOB.PageIndex = pageIndex;
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            obj_petOB.PageSize = 10000;
        }
        else
        {
            obj_petOB.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        }
        obj_petOB.ActionType = Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["id"]);

        p_Val.dSet = mixModuleBL.Get_Guidelines(obj_petOB, out p_Val.k);


        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            gvRegulations.DataSource = p_Val.dSet;
            gvRegulations.DataBind();
            lastUpdatedDate = p_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                rptPager.Visible = false;
                ddlPageSize.Visible = false;
                lblPageSize.Visible = false;
            }
            else
            {
                rptPager.Visible = true;
                ddlPageSize.Visible = true;
                lblPageSize.Visible = true;
            }
        }
        else
        {
            DlastUpdate.Visible = false;
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;

			   lblmsg.Visible      = true;
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
           // lblmsg.ForeColor = System.Drawing.Color.Red;
        }
        p_Val.Result = p_Val.k;
        if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_Val.Result, pageIndex, ddlPageSize, rptPager);
            if (!string.IsNullOrEmpty(Request.QueryString["format"]))
            {
                rptPager.Visible = false;
            }
            else
            {
                rptPager.Visible = true;
            }
        }
        else
        {
            rptPager.Visible = false;
        }
		}	
		
		catch{}

    }


    //Area for all the gridView events

    protected void gvRegulations_RowCommand(object sender, GridViewCommandEventArgs e)
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

 
    }

    protected void gvRegulations_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("LinkButton1");
           
            //HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            string filename = DataBinder.Eval(e.Row.DataItem, "File_Name").ToString();
            Label lblRegNo = (Label)e.Row.FindControl("lblRegNumber");
            HiddenField hydfield = (HiddenField)e.Row.FindControl("hydRegNumber");
            Label lbllinkid = (Label)e.Row.FindControl("lbllinkId");
            Label lblambandment = (Label)e.Row.FindControl("lblambandment");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            Label lblname = (Label)e.Row.FindControl("lblname");
            HyperLink hyp = (HyperLink)e.Row.FindControl("hypFile");
            HiddenField hid = (HiddenField)e.Row.FindControl("hidFile");
            lnkObject.linkID = Convert.ToInt16(lbllinkid.Text);
            Label lblambandmentId = (Label)e.Row.FindControl("lblAmendmentid");
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            if (Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["id"]) == Convert.ToInt16(Module_ID_Enum.hercType.repealed))
            {
                gvRegulations.Columns[3].Visible = true;
                if (lblambandment.Text.ToString() != "null" && lblambandment.Text.ToString() != "")
                {
                    lblambandmentId.Visible = true;
                    lblRemarks.Visible = false;
                }
                else
                {
                    lblRemarks.Visible = true;
                    lblambandmentId.Visible = false;
                }

                lblRemarks.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                gvRegulations.Columns[3].Visible = false;

            }

            if (lblambandment.Text.ToString() != "null" && lblambandment.Text.ToString() != "")
            {
                lblambandmentId.Visible = true;
                lnkObject.regulationNumber = Convert.ToInt16(lblRegNo.Text);
                lnkObject.TempLinkId = Convert.ToInt16(lbllinkid.Text);
                lnkObject.ModuleId = 26;
                p_Val.dsFileName = mixModuleBL.USP_GetAmendmentID(lnkObject);
                if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
                {

                    lblambandmentId.Text = p_Val.dsFileName.Tables[0].Rows[0]["Remarks"].ToString();
                    lblambandmentId.ForeColor =System.Drawing.Color.Red;
                    lblRemarks.Visible = false;
                }

            }
            p_Val.str = mixModuleBL.getRegulationNumberCurrent(lnkObject);
            if (p_Val.str != null && p_Val.str != "" && p_Val.str != "0")
            {
                lblRegNo.Text = p_Val.str;
            }
            else
            {
                lblRegNo.Text = "";
            }
            if (hid.Value.ToString() == "" || hid.Value == null)
            {
                lblname.Visible = true;
                hyp.Visible = false;
            }
            else
            {
                if (File.Exists(Server.MapPath(path + hid.Value)))
                {
                    lblname.Visible = false;
                    hyp.Visible = true;

                    FileInfo finfo = new FileInfo(Server.MapPath(path + hid.Value));
                    double FileInBytes = finfo.Length;

                    p_Val.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");
                    p_Val.sbuilder.Append("(" + obj_miscel.fileSize(FileInBytes) + ")");
                    hyp.Text = hyp.Text.ToString().Replace("<p>", "").Replace("</p>", "");
                    hyp.Text += p_Val.sbuilder.ToString();
                }

            }

            if (hydfield.Value != null && hydfield.Value != "")
            {
                lblambandment.Visible = true;
                if (lblRegNo.Text == hydfield.Value)
                {
                    lblRegNo.Text = "";

                }
            }

            if (filename == null || filename == "")
            {
                lnk.Visible = false;
            }
            else
            {
                if (File.Exists(Server.MapPath(path + hid.Value)))
                {
                    lblname.Visible = false;
                }
                else
                {
                    lblname.Visible = true;
                    hyp.Visible = false;
                }
            }
            if (lblRegNo.Text == "0")
            {
                lblRegNo.Text = "";
            }

        }
    }

    //End

    //Area for all the buttons, linkButtons, imageButtons events

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Bindgrid(pageIndex);
    }

    #endregion

    //End

    //Area for all the dropDownlist events


    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bindgrid(1);
    }

    #endregion


    //End

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitle;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion

 
}