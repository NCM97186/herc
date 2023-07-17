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

public partial class Ombudsman_searchaward : BasePageOmbudsman
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    static string strsearchcondition;
    static string strsearch;
    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;
    string MetaLng = string.Empty;
    string MetaTitles = string.Empty;

    public static string UrlPrint = string.Empty;

    #endregion 

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.AwardsPronounced, Resources.HercResource.AwardsSearch);
        ltrlBreadcrum.Text = str.ToString();
        p_Val.Path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Pdf"] + "/";
        if (!IsPostBack)
        {
            BindYear();
            BindAppealNumber();
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssRef);

            if (Session["TempTable"] != null)
            {
                Grdaaward.DataSource = (DataTable)Session["TempTable"];
                Grdaaward.DataBind();
            }
        }


        if (Resources.HercResource.Lang_Id == "1")
        {
            PageTitle = Resources.HercResource.AwardsPronounced;
        }
        else
        {
            PageTitle = Resources.HercResource.AwardsPronounced;
        }
    }

    #endregion


    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    //Area for all the buttons, linkButtons, imageButtons click events

    #region button btnsearch click event to search records

    protected void btnsearch_click(object sender, EventArgs e)
    {

        if (ViewState["date"] != null)
        {
            ViewState.Remove("date");
        }
        ViewState["search"] = "2";
        Bind_Award_Search(1, 2);

    }

    #endregion

    //End

    protected void btndate_click(object sender, EventArgs e)
    {
        ViewState["date"] = "3";
        if (ViewState["search"] != null)
        {
            ViewState.Remove("search");
        }
        Bind_Award_Search(1, 3);
        
    }


    protected void btnsearchYearwise_click(object sender, EventArgs e)
    {
        ViewState.Remove("date");
        ViewState.Remove("search");
        obj_petOB.ActionType = 1;
        obj_petOB.AppealNo = drpreference.SelectedValue;
        obj_petOB.year = drpyear.SelectedValue;

        p_Val.dsFileName = obj_petBL.SearchAward(obj_petOB, out p_Val.k);
        if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
        {
            MetaKeyword = p_Val.dsFileName.Tables[0].Rows[0]["MetaKeywords"].ToString();
            MetaDescription = p_Val.dsFileName.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            MetaLng = p_Val.dsFileName.Tables[0].Rows[0]["MetaLanguage"].ToString();
            MetaTitles = p_Val.dsFileName.Tables[0].Rows[0]["MetaTitle"].ToString();
            Session["TempTable"] = p_Val.dsFileName.Tables[0];
            Grdaaward.DataSource = p_Val.dsFileName;
            Grdaaward.DataBind();



            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        else
        {
            Grdaaward.DataSource = p_Val.dsFileName;
            Grdaaward.DataBind();
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        p_Val.Result = p_Val.k;
       

       
    }
    public void Bind_Award_Search(int pageindex,int actionType)
    {
        obj_petOB.ActionType = actionType;

        obj_petOB.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        obj_petOB.PageIndex = pageindex;
        if (actionType == 2)
        {
            obj_petOB.AppealNo = null;

            if (txtname.Text != null && txtname.Text != "")
            {
                obj_petOB.ApplicantName = txtname.Text;
            }
            else
            {
                obj_petOB.ApplicantName = null;
            }
            if (txtrespodent.Text != null && txtrespodent.Text != "")
            {
                obj_petOB.RespondentName = txtrespodent.Text;
            }
            else
            {
                obj_petOB.RespondentName = null;
            }
            if (txtsubject.Text != null && txtsubject.Text != "")
            {
                obj_petOB.subject = txtsubject.Text;
            }
            else
            {
                obj_petOB.subject = null;
            }
        }
        else
        {
            if (txtDate.Text != null && txtDate.Text != "")
            {
                if (Resources.HercResource.Lang_Id == "1")
                {
                    obj_petOB.Date = Convert.ToDateTime(miscellBL.getDateFormat(txtDate.Text));
                }
                else
                {
                    obj_petOB.Date = Convert.ToDateTime(txtDate.Text);
                }
                
            }
            else
            {
                obj_petOB.Date = null;
            }

        }

        p_Val.dSetCompare = obj_petBL.SearchAward(obj_petOB, out p_Val.k);
        if (p_Val.dSetCompare.Tables[0].Rows.Count > 0)
        {
            if (p_Val.dSetCompare.Tables[0].Rows[0]["MetaKeywords"] != null && p_Val.dSetCompare.Tables[0].Rows[0]["MetaKeywords"].ToString() != "")
            {
                MetaKeyword = p_Val.dSetCompare.Tables[0].Rows[0]["MetaKeywords"].ToString();
            }
            if (p_Val.dSetCompare.Tables[0].Rows[0]["MetaDescriptions"] != null && p_Val.dSetCompare.Tables[0].Rows[0]["MetaDescriptions"].ToString() != "")
            {
                MetaDescription = p_Val.dSetCompare.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            }
            if (p_Val.dSetCompare.Tables[0].Rows[0]["MetaLanguage"] != null && p_Val.dSetCompare.Tables[0].Rows[0]["MetaLanguage"].ToString() != "")
            {
                MetaLng = p_Val.dSetCompare.Tables[0].Rows[0]["MetaLanguage"].ToString();

            }
            if (p_Val.dSetCompare.Tables[0].Rows[0]["MetaTitle"] != null && p_Val.dSetCompare.Tables[0].Rows[0]["MetaTitle"].ToString() != "")
            {
                MetaTitles = p_Val.dSetCompare.Tables[0].Rows[0]["MetaTitle"].ToString();
            }
            Session["TempTable"] = p_Val.dSetCompare.Tables[0];
            Grdaaward.DataSource = p_Val.dSetCompare;
            Grdaaward.DataBind();
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
           

            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
            
        }
        else
        {
            Grdaaward.DataSource = p_Val.dSetCompare;
            Grdaaward.DataBind();
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }
        p_Val.Result = p_Val.k;
        if (p_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(p_Val.Result, pageindex, ddlPageSize, rptPager);
            rptPager.Visible = true;
        }
        else
        {
            rptPager.Visible = false;
        }
        


    }

    protected void drpyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drpyear.SelectedValue != "0")
        //{
            BindAppealNumber();
        //}
        //else
        //{
        //    drpreference.Items.Insert(0, new ListItem("Select", "0"));
        //}

    }
    protected void Grdaaward_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "ViewDoc")
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

        if (e.CommandName == "View")
        {
              p_Val.stringTypeID = e.CommandArgument.ToString();
                p_Val.strPopupID = "<script language='javascript'>" +
                                   "window.open('ViewDetails.aspx?Petition_id=" + p_Val.stringTypeID + "', 'blank' + new Date().getTime()," +
                                   "' menubar=no, resizable=yes, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);
            
        }
        if (e.CommandName == "getappeal_Detail")
        {
            
                    p_Val.id = e.CommandArgument.ToString();
                    p_Val.strPopupID = "<script language='javascript'>" +
                                       "window.open('" + ResolveUrl("~/") + "Ombudsman/ViewAppealDetails.aspx?Appeal_id=" + p_Val.id + "', 'blank' + new Date().getTime()," +
                                       "' menubar=no, resizable=no, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                       "</script>";
                    this.Page.RegisterStartupScript("PopupScript", p_Val.strPopupID);

                   
               

        }
    }

    protected void Grdaaward_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            //LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");

            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");


            //connected Award files
            Literal orderAwardFile = (Literal)e.Row.FindControl("ltrlConnectedAwardProunced");
            obj_petOB.AppealId = Convert.ToInt16(hdf.Value);
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            p_Val.dsFileName = obj_petBL.getConnectedAwardFiles(obj_petOB);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Val.sbuilder.Append(" <a title='" + Resources.HercResource.ViewDocument + "' href='" + p_Val.Path + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "' target='_blank'>" + "Award,Dated:" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='" + Resources.HercResource.ViewDocument + "' width='15' alt='" + Resources.HercResource.ViewDocument + "' height=\"15\" /> ");
                    if (p_Val.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Val.Path) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(p_Val.Path) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }
                    p_Val.sbuilder.Append("</a><br/><hr/>");

                }
                orderAwardFile.Text = p_Val.sbuilder.ToString();

            }
            else
            {

            }
            //string filename = DataBinder.Eval(e.Row.DataItem, "File_Name").ToString();
            string temID = Convert.ToString(e.Row.Cells[0].Text);
            string year = Convert.ToString(e.Row.Cells[1].Text);

            temID = "Ref No.-" + temID + " of " + year;
            e.Row.Cells[0].Text = temID;

            Label lblPetitioner = (Label)e.Row.FindControl("lblPetitioner");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            LinkButton lblSubject = (LinkButton)e.Row.FindControl("lblSubject");
            if (lblPetitioner.Text != null && lblPetitioner.Text != "")
            {
                lblPetitioner.Text = HttpUtility.HtmlDecode(lblPetitioner.Text);
            }
            if (lblRespondent.Text != null && lblRespondent.Text != "")
            {
                lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
            }
            if (lblSubject.Text != null && lblSubject.Text != "")
            {
                lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            }
        }
    }

    #region Datalist datalistYear itemCommand event zone

    protected void datalistYear_ItemCommand(object source, DataListCommandEventArgs e)
    {
        

    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {

        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        if (ViewState["date"] != null)
        {
            ViewState.Remove("search");
            this.Bind_Award_Search(pageIndex, 3);
        }
        else
        {
            this.Bind_Award_Search(pageIndex, 2);
            ViewState.Remove("date");
        }
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["date"] != null)
        {
            ViewState.Remove("search");
            this.Bind_Award_Search(1, 3);
        }
        else
        {
            this.Bind_Award_Search(1, 2);
            ViewState.Remove("date");
        }
       
    }

    #endregion

    #region function to Bind data in a data list for year

    public void BindYear()
    {
   
        // p_Val.dSetChildData = obj_petBL.GetAwardUnderAppealYear();
        p_Val.dSetChildData = obj_petBL.GetAward_UnderAppealYear();
        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drpyear.DataSource = p_Val.dSetChildData;
            drpyear.DataTextField = "Year";
            drpyear.DataValueField = "year";
            drpyear.DataBind();
        }
       
            drpyear.Items.Insert(0, new ListItem("Select", "0"));
       


    }


    #endregion 


    #region function to Bind data in a data list for year

    public void BindAppealNumber()
    {
        obj_petOB.year = drpyear.SelectedValue;
        // p_Val.dSetChildData = obj_petBL.GetAwardUnderAppealYear();
        p_Val.dSetChildData = obj_petBL.GetAppealNumberAward(obj_petOB);
        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            drpreference.DataSource = p_Val.dSetChildData;
            drpreference.DataTextField = "AppealNumber";
            drpreference.DataValueField = "AppealNumber";
            drpreference.DataBind();
        }
        else
        {
            drpreference.DataSource = p_Val.dSetChildData;
            drpreference.DataBind();
            drpreference.Items.Insert(0, new ListItem("Select", "0"));
        }
       
    }


    #endregion 

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitleOmbudsman;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
        MetaLang = MetaLng;
        MetaTitle = MetaTitles;
    }

    #endregion
}
