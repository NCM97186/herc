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
using System.Text;

public partial class Ombudsman_ViewAppealDetails : System.Web.UI.Page
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    string Url = string.Empty;
    #endregion 
    protected void Page_Load(object sender, EventArgs e)
    {
        p_Val.Path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/";

         Url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["scheduleofHearing"] + "/"; 
        if (!IsPostBack)
        {
            if (Request.QueryString["Sohid"] != null)
            {
                Bind_SOHDetails(Request.QueryString["Sohid"].ToString());
            }
            else if (Request.QueryString["Appeal_id"] != null)
            {
                Bind_Appeal();
                //BindAwardUnderAppeal(Request.QueryString["Appeal_id"].ToString());
                //BindAward();
            }
            else if (Request.QueryString["Awardid"] != null)
            {
                Bind_Appeal();
                BindAwardUnderAppeal(Request.QueryString["Awardid"].ToString());
            }
            else
            {

                Bind_Appeal();
            }
        }
    }

    #region function declaration zone

    public void Bind_Appeal()
    {
        if (Request.QueryString["Appealid"] != null)
        {
            obj_petOB.TempAppealId = Convert.ToInt16(Request.QueryString["Appealid"]);
        }
        else
        {
            if (Request.QueryString["Awardid"] != null)
            {
                obj_petOB.TempAppealId = Convert.ToInt16(Request.QueryString["Awardid"]);
            }
            else
            {
                obj_petOB.TempAppealId = Convert.ToInt16(Request.QueryString["Appeal_id"]);
            }
        }
        obj_petOB.StatusId = 6;
        //p_Val.dSet = obj_petBL.getAppealRecordForEdit(obj_petOB);
        p_Val.dSet = obj_petBL.getAppealRecord(obj_petOB);
        
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {

            Grdappeal.DataSource = p_Val.dSet;
            Grdappeal.DataBind();
          
        }
        else
        {
            Grdappeal.DataSource = p_Val.dSet;
            Grdappeal.DataBind();
       
        }
  

    }


    #endregion

    protected void Grdappeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblConnectedSoh_ID = (Label)e.Row.FindControl("lblConnectedSoh_ID");
            LinkButton lnkConnectedSoh = (LinkButton)e.Row.FindControl("lnkConnectedSoh");
            Literal ltrlAwardUnderAppeal = (Literal)e.Row.FindControl("ltrlAwardUnderAppeal");
            HiddenField hyappealId = (HiddenField)e.Row.FindControl("hyappealId");

            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            if (lblRemarks.Text == "" )
            {
                Grdappeal.Columns[9].Visible=false;
            }
            else
            {
                lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
                 Grdappeal.Columns[9].Visible=true;
            }

            //connected Award files
            Literal orderAwardFile = (Literal)e.Row.FindControl("ltrlConnectedAwardProunced");
            obj_petOB.AppealId = Convert.ToInt16(hyappealId.Value);
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            p_Val.dsFileName = obj_petBL.getConnectedAwardFiles(obj_petOB);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                Grdappeal.Columns[7].Visible = true;
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    //p_Val.sbuilder.Append(" <a href='" + p_Val.Path + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "' target='_blank'>" +"Award,Dated:"+ p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "</a>");
                    p_Val.sbuilder.Append(" <a title='View Document' href='" + p_Val.Path + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "' target='_blank'>" + "Award,Dated:" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='View Document' height=\"15\" /> ");

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
                Grdappeal.Columns[7].Visible = false;
            }


           


            HiddenField hyAppeal = (HiddenField)e.Row.FindControl("hyappealId");
            obj_petOB.AppealId = Convert.ToInt16(hyAppeal.Value);
            p_Val.dSetCompare = obj_petBL.getConnectedSOH_Appeal(obj_petOB);
            if (p_Val.dSetCompare.Tables[0].Rows.Count > 0)
            {

                Grdappeal.Columns[8].Visible = true;
                lnkConnectedSoh.Visible = true;
                lblConnectedSoh_ID.Visible = false;
                lnkConnectedSoh.Text = "Yes";

            }
            else
            {
                lnkConnectedSoh.Visible = false;
                lblConnectedSoh_ID.Visible = true;
                Grdappeal.Columns[8].Visible = false;
            }


            //////obj_petOB.AppealId = Convert.ToInt16(hyAppeal.Value);
            //////p_Val.dSetChildData = obj_petBL.GetAwardForAppeal(obj_petOB);

            //This is for Appeal under appeal
            Label lblAwardunderappeal = (Label)e.Row.FindControl("lblAwardunderappeal");
            LinkButton lnkAwardunderappeal = (LinkButton)e.Row.FindControl("lnkAwardunderappeal");

            obj_petOB.AppealId = Convert.ToInt16(hyAppeal.Value);
            p_Val.dSetChildData = obj_petBL.getConnectedAwardApealFiles(obj_petOB);

            if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
            {
                if (Request.QueryString["Awardid"] != null)
                {
                    lnkAwardunderappeal.Visible = false;
                    ltrlAwardUnderAppeal.Visible = true;
                }
                else
                {
                    Grdappeal.Columns[10].Visible = true;
                    lnkAwardunderappeal.Visible = true;
                    lblAwardunderappeal.Visible = false;
                    lnkAwardunderappeal.Text = "Yes";
                }

            }
            else
            {
                lnkAwardunderappeal.Visible = false;
                lblAwardunderappeal.Visible = true;
               Grdappeal.Columns[10].Visible = false;
            }
            //This is for hide respondent column

            Label lblAppellant = (Label)e.Row.FindControl("lblAppellant");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");

            if (lblSubject.Text != "" && lblSubject.Text!=null)
            {
                lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            }
            
            //string Respondent =HttpUtility.HtmlDecode(e.Row.Cells[4].Text).Trim();
           // string Applicant = HttpUtility.HtmlDecode(e.Row.Cells[3].Text).Trim();
            if (lblRespondent.Text!= "")
            {
                lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
                Grdappeal.Columns[4].Visible = true;
            }
            else
            {
                Grdappeal.Columns[4].Visible = false;
            }

            if (lblAppellant.Text != "")
            {
                lblAppellant.Text = HttpUtility.HtmlDecode(lblAppellant.Text);
                Grdappeal.Columns[3].Visible = true;
            }
            else
            {
                Grdappeal.Columns[3].Visible = false;
            }

        }
    }


    protected void Grdappeal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        if (e.CommandName == "connectedSoh")
        {
            string id =e.CommandArgument.ToString();

            LinkButton lnkConnectedSoh = (LinkButton)gvrow.FindControl("lnkConnectedSoh");
            Literal ltrlConnectedSoh = (Literal)gvrow.FindControl("ltrlConnectedSoh");
            lnkConnectedSoh.Visible = false;
            ltrlConnectedSoh.Visible = true;

            Bind_SOH(id);
        }
        ////////if (e.CommandName == "connectedAward")
        ////////{
        ////////    p_Val.id = e.CommandArgument.ToString();

        ////////    BindAward(p_Val.id);
        ////////}
        if (e.CommandName == "connectedAwardunderappeal")
        {
            p_Val.categoryID = e.CommandArgument.ToString();

            LinkButton lnkAwardunderappeal = (LinkButton)gvrow.FindControl("lnkAwardunderappeal");
            Literal ltrlAwardUnderAppeal = (Literal)gvrow.FindControl("ltrlAwardUnderAppeal");
            lnkAwardunderappeal.Visible = false;
            ltrlAwardUnderAppeal.Visible = true;

            BindAwardUnderAppeal(p_Val.categoryID);
        }
    }



    public void BindAwardUnderAppeal(string Id)
    {
        PAwardUnderAppeal.Visible = true;
        obj_petOB.AppealId = Convert.ToInt16(Id);
        p_Val.dsFileName = obj_petBL.getAwardUnderAppealDetails(obj_petOB);
        if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
        {
           
            GvAwardUnderAppeal.DataSource = p_Val.dsFileName;
            GvAwardUnderAppeal.DataBind();
        }
    }


    public void Bind_SOH(string Id)
    {
        PnlSOH.Visible = true;
       obj_petOB.AppealId  = Convert.ToInt16(Id);
       p_Val.dSetChildData = obj_petBL.getConnectedSOH_Appeal(obj_petOB);
        if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            //Dreview.Visible = true;
            gvSOH.DataSource = p_Val.dSetChildData;
            gvSOH.DataBind();


            //This is for Download option for SOH
            for (int j = 0; j < p_Val.dSetChildData.Tables[0].Rows.Count; j++)
            {
                obj_petOB.soh_ID = Convert.ToInt16(p_Val.dSetChildData.Tables[0].Rows[j]["Soh_ID"]);
                p_Val.dSetCompare = obj_petBL1.getSohFileNames(obj_petOB);
                if (p_Val.dSetCompare.Tables[0].Rows.Count > 0)
                {

                    Literal ltrlConnectedFileSOHDetails = gvSOH.Rows[j].FindControl("ltrlConnectedFileSOHDetails") as Literal;
                    p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
                    for (int i = 0; i < p_Val.dSetCompare.Tables[0].Rows.Count; i++)
                    {
                        if (p_Val.dSetCompare.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Val.dSetCompare.Tables[0].Rows[i]["Comments"].ToString() != "")
                        {
                            p_Val.sbuilder.Append(p_Val.dSetCompare.Tables[0].Rows[i]["Comments"] + ", ");
                        }

                        p_Val.sbuilder.Append("<a title='View Document' href='" + Url + p_Val.dSetCompare.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='View Document' height=\"15\" />");

                        if (p_Val.dSetCompare.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(Url) + p_Val.dSetCompare.Tables[0].Rows[i]["FileName"]))
                            {
                                FileInfo finfo = new FileInfo(Server.MapPath(Url) + p_Val.dSetCompare.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }

                        p_Val.sbuilder.Append("</a><br/><hr/>");

                    }

                    ltrlConnectedFileSOHDetails.Text = p_Val.sbuilder.ToString();

                }
            }

            DataSet dSet = new DataSet();

            dSet = obj_petBL1.getScheduleOfHearingURLwithFile(obj_petOB);
            if (dSet.Tables[0].Rows.Count > 0)
            {

                SOHFiles.Visible = true;
                //loop through cells in that row
                string strUrl = dSet.Tables[0].Rows[0]["placeholderTwo"].ToString();
                string[] split = strUrl.Split(';');
                ArrayList list = new ArrayList();

                //loop through cells in that row
                string strUrl1 = dSet.Tables[0].Rows[0]["placeholderOne"].ToString();

                string[] split1 = strUrl1.Split(';');
                ArrayList list1 = new ArrayList();

                foreach (string item in split)
                {
                    list.Add(item.Trim());
                }
                foreach (string item1 in split1)
                {
                    list1.Add(item1.Trim());
                }


                 Literal ltrlConnectedFileSOHDetails = gvSOH.Rows[0].FindControl("ltrlConnectedFileSOHDetails") as Literal;
                //string concatsplit;
                StringBuilder ctbbld = new StringBuilder();
                for (int i = 0; i < split1.Count(); i++)
                {
                    //concatsplit = split[i].ToString() + " " + split1[i].ToString();

                    ctbbld.Append(" <a href='" + split1[i].ToString() + "'  target='_blank' title='Click Here to Go" + "&nbsp;" + "&nbsp;" + split1[i].ToString() + "'>");
                    ctbbld.Append(split[i].ToString());
                    ctbbld.Append("</a><br/>");



                }
                ltrlConnectedFileSOHDetails.Text += ctbbld.ToString();
            }
        }
    }


    public void BindAward()
    {
      
        obj_petOB.AppealId = Convert.ToInt16(Request.QueryString["Appeal_id"]);
        p_Val.dSetCompare = obj_petBL.GetAwardForAppeal(obj_petOB);
        if (p_Val.dSetCompare.Tables[0].Rows.Count > 0)
        {
            pnlAward.Visible = true;
            gvAward.DataSource = p_Val.dSetCompare;
            gvAward.DataBind();
        }
        else
        {
            pnlAward.Visible = false;

        }
    }


    protected void gvSOH_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDoc")
        {
            p_Val.url = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["scheduleofHearing"] + "/");
            string file = e.CommandArgument.ToString();
            p_Val.url = p_Val.url + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(p_Val.url);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(p_Val.url));
                Response.Clear();
                Response.WriteFile(p_Val.url);
                Response.End();
            }


        }
    }
    protected void gvSOH_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //////LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            //////string filename = DataBinder.Eval(e.Row.DataItem, "soh_file").ToString();
            ////////string temID = Convert.ToString(e.Row.Cells[0].Text);
            ////////string year = Convert.ToString(e.Row.Cells[1].Text);

            ////////temID = "EO/Appeal No-" + temID + " of " + year;
            ////////e.Row.Cells[0].Text = temID;
            //////if (filename == null || filename == "")
            //////{
            //////    lnk.Visible = false;
            //////}
        }
    }



    protected void GvAwardUnderAppeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            // connected Award under Appeal files

            HiddenField hyAppeal = (HiddenField)e.Row.FindControl("hyappealId");
            Label lblAwardStatus = (Label)e.Row.FindControl("lblAwardStatus");
            HyperLink Hypjudgement = (HyperLink)e.Row.FindControl("Hypjudgement");
            string Description = DataBinder.Eval(e.Row.DataItem, "Description").ToString();
            string OtherDescription = DataBinder.Eval(e.Row.DataItem, "OtherDescription").ToString();
            HtmlImage img1 = (HtmlImage)e.Row.FindControl("img1");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            

            if (lblRemarks.Text != "")
            {
                GvAwardUnderAppeal.Columns[8].Visible = true;
                lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
            }
            else
            {
                GvAwardUnderAppeal.Columns[8].Visible = false;
            }

            if (OtherDescription != null && OtherDescription != "")
            {
                if (lblAwardStatus.Text != null && lblAwardStatus.Text != "")
                {
                    lblAwardStatus.Text = lblAwardStatus.Text + ",";
                    img1.Visible = true;
                    img1.Src = ResolveUrl("~/images/external.png");
                }
                else
                {

                    img1.Visible = false;
                }
                
            }

            if (Description != null && Description != "")
            {
                if (Hypjudgement.Text != "" && Hypjudgement.Text != null)
                {
                    Hypjudgement.Text = "," + "Judgement";
                    img1.Visible = true;
                    img1.Src = ResolveUrl("~/images/external.png");
                }
                else
                {
                    Hypjudgement.Text = "," + "";
                    img1.Visible = false;
                }
               

            }
            else
            {
                if (Hypjudgement.Text != "" && Hypjudgement.Text != null)
                {
                    img1.Visible = true;
                    Hypjudgement.Text =  "Judgement";
                    img1.Src = ResolveUrl("~/images/external.png");
                }
                else
                {
                    img1.Visible = false;
                    GvAwardUnderAppeal.Columns[7].Visible=false;
                }

            }
           
            Literal ltrlAwardUnderAppeal = (Literal)e.Row.FindControl("ltrlAwardUnderAppeal");
            obj_petOB.AppealId = Convert.ToInt16(hyAppeal.Value);
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            p_Val.dsFileName = obj_petBL.getAppealAwardPronouncedFiles(obj_petOB);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    GvAwardUnderAppeal.Columns[5].Visible = true;
                    p_Val.sbuilder.Append("<a title='View Document' href='" + p_Val.Path + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document' >");
                    if (p_Val.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Val.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["Comments"] + ",  ");
                    }
                    p_Val.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='View Document' height=\"15\" />");

                    if (p_Val.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Val.Path) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(p_Val.Path) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }

                    p_Val.sbuilder.Append("<br/><hr/>");
                }
                p_Val.sbuilder.Append("<a/>");
                ltrlAwardUnderAppeal.Text = p_Val.sbuilder.ToString();

            }
            else
            {
                GvAwardUnderAppeal.Columns[5].Visible = false;
            }

            string remarks = HttpUtility.HtmlDecode(e.Row.Cells[8].Text).Trim();
          
        }
    }



    protected void gvAward_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDocAward")
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
    protected void gvAward_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            //connected Award files
            HiddenField hdfFile = (HiddenField)e.Row.FindControl("hdfFileAward");
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            Literal ltrlAwardProunced = (Literal)e.Row.FindControl("ltrlAwardProunced");
            obj_petOB.AppealId = Convert.ToInt16(hdfFile.Value);
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            p_Val.dsFileName = obj_petBL.getConnectedAwardFiles(obj_petOB);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                gvAward.Columns[5].Visible = true;
               for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Val.sbuilder.Append(" <a title='View Document' href='" + p_Val.Path + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "' target='_blank'>" + "Award,Dated:" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='View Document' height=\"15\" /> ");

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
                ltrlAwardProunced.Text = p_Val.sbuilder.ToString();

            }
            else
            {
                gvAward.Columns[5].Visible = false;
            }


            Label lblRemarksAward = (Label)e.Row.FindControl("lblRemarksAward");
            if (lblRemarksAward.Text == "")
            {
                gvAward.Columns[6].Visible = false;
            }
            else
            {
                lblRemarksAward.Text = HttpUtility.HtmlDecode(lblRemarksAward.Text);
                gvAward.Columns[6].Visible = true;
            }


            //This is for hide respondent column
            Label Applicant = (Label)e.Row.FindControl("lblPetitioner");
            Label Respondent = (Label)e.Row.FindControl("lblRespondent");
             Label Subject=(Label)e.Row.FindControl("lblSubject");
             if (Subject.Text != "")
             {
                 gvAward.Columns[4].Visible = true;
                 Subject.Text = HttpUtility.HtmlDecode(Subject.Text);
             }
             else
             {
                 gvAward.Columns[4].Visible = false;
             }
            if (Respondent.Text != "")
            {
                gvAward.Columns[3].Visible = true;
                Respondent.Text = HttpUtility.HtmlDecode(Respondent.Text);
            }
            else
            {
                gvAward.Columns[3].Visible = false;
            }

            if (Applicant.Text != "")
            {
                gvAward.Columns[2].Visible = true;
                Applicant.Text = HttpUtility.HtmlDecode(Applicant.Text);
            }
            else
            {
                gvAward.Columns[2].Visible = false;
            }

          
       
        }
    }


    public void Bind_SOHDetails(string Id)
    {
        PSOHDetails.Visible = true;
        obj_petOB.soh_ID = Convert.ToInt16(Id);
        obj_petOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
        p_Val.dsFileName = obj_petBL1.GetCurrentSOHForOmbudsmanDetails(obj_petOB);
        if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
        {
            //Dreview.Visible = true;
            gvSOHDetails.DataSource = p_Val.dsFileName;
            gvSOHDetails.DataBind();

            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            obj_petOB.soh_ID = Convert.ToInt16(Id);
            p_Val.dSetCompare = obj_petBL1.getSohFileNames(obj_petOB);
            if (p_Val.dSetCompare.Tables[0].Rows.Count > 0)
            {
                SOHFiles.Visible = true;
                for (int i = 0; i < p_Val.dSetCompare.Tables[0].Rows.Count; i++)
                {
                    p_Val.sbuilder.Append("<a href='" + Url + p_Val.dSetCompare.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document'>");
                    if (p_Val.dSetCompare.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Val.dSetCompare.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        p_Val.sbuilder.Append(p_Val.dSetCompare.Tables[0].Rows[i]["Comments"] + ", ");
                    }
                    //p_Var.sbuilder.Append("Dated: " + p_Var.dsFileName.Tables[0].Rows[i]["Date"] + " ");

                   // p_Val.sbuilder.Append("<a title='View Document' href='" + Url + p_Val.dSetCompare.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='View Document' height=\"15\" />" + "</a>");
                    p_Val.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='View Document' height=\"15\" />" + "</a>");
                    if (p_Val.dSetCompare.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(Url) + p_Val.dSetCompare.Tables[0].Rows[i]["FileName"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(Url) + p_Val.dSetCompare.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }
                    p_Val.sbuilder.Append("<br/><hr/>");

                }
                ltrlConnectedFileSOH.Text = p_Val.sbuilder.ToString();

            }
            else
            {
                SOHFiles.Visible = false;
            }

            DataSet dSet = new DataSet();

            dSet = obj_petBL1.getScheduleOfHearingURLwithFile(obj_petOB);
            if (dSet.Tables[0].Rows.Count > 0)
            {

                SOHFiles.Visible = true;
                //loop through cells in that row
                string strUrl = dSet.Tables[0].Rows[0]["placeholderTwo"].ToString();
                string[] split = strUrl.Split(';');
                ArrayList list = new ArrayList();

                //loop through cells in that row
                string strUrl1 = dSet.Tables[0].Rows[0]["placeholderOne"].ToString();

                string[] split1 = strUrl1.Split(';');
                ArrayList list1 = new ArrayList();

                foreach (string item in split)
                {
                    list.Add(item.Trim());
                }
                foreach (string item1 in split1)
                {
                    list1.Add(item1.Trim());
                }


                // Literal ltrlConnectedFileSOHDetails = gvSOH.Rows[0].FindControl("ltrlConnectedFileSOHDetails") as Literal;
                //string concatsplit;
                StringBuilder ctbbld = new StringBuilder();
                for (int i = 0; i < split1.Count(); i++)
                {
                    //concatsplit = split[i].ToString() + " " + split1[i].ToString();

                    ctbbld.Append(" <a href='" + split1[i].ToString() + "'  target='_blank' title='Click Here to Go" + "&nbsp;" + "&nbsp;" + split1[i].ToString() + "'>");
                    ctbbld.Append(split[i].ToString());
                    ctbbld.Append("</a><br/>");



                }
                ltrlConnectedFileSOH.Text += ctbbld.ToString();
            }
        }
    }

    protected void gvSOHDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblApplicantSoh = (Label)e.Row.FindControl("lblApplicantSoh");
            Label lblRespondentSoh = (Label)e.Row.FindControl("lblRespondentSoh");
            Label lblSubjectSoh = (Label)e.Row.FindControl("lblSubjectSoh");
            Label lblRemarksSoh = (Label)e.Row.FindControl("lblRemarksSoh");


            if (lblRemarksSoh.Text != "" && lblRemarksSoh.Text != null)
            {
                lblRemarksSoh.Text = HttpUtility.HtmlDecode(lblRemarksSoh.Text);
                gvSOHDetails.Columns[7].Visible = true;
            }
            else
            {
                gvSOHDetails.Columns[7].Visible = false;
            }
            if (lblSubjectSoh.Text != "" && lblSubjectSoh.Text != null)
            {
                lblSubjectSoh.Text = HttpUtility.HtmlDecode(lblSubjectSoh.Text);
                gvSOHDetails.Columns[5].Visible = true;
            }
            else
            {
                gvSOHDetails.Columns[5].Visible = false;
            }

          
            if (lblRespondentSoh.Text != "")
            {
                lblRespondentSoh.Text = HttpUtility.HtmlDecode(lblRespondentSoh.Text);
                gvSOHDetails.Columns[4].Visible = true;
            }
            else
            {
                gvSOHDetails.Columns[4].Visible = false;
            }

            if (lblApplicantSoh.Text != "")
            {
                lblApplicantSoh.Text = HttpUtility.HtmlDecode(lblApplicantSoh.Text);
                gvSOHDetails.Columns[3].Visible = true;
            }
            else
            {
                gvSOHDetails.Columns[3].Visible = false;
            }
        }
    }
}
