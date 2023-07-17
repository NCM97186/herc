using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class ViewDetails : System.Web.UI.Page
{
    #region variable declaration zone

    PetitionBL petPetitionBL = new PetitionBL();
    PetitionOB petitionObject = new PetitionOB();
    Project_Variables p_Var = new Project_Variables();
    LinkOB obj_linkOB = new LinkOB();
    LinkBL obj_linkBL = new LinkBL();
    Miscelleneous_DL obj_Miscel = new Miscelleneous_DL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    PublicNoticeBL objPublic = new PublicNoticeBL();
    PetitionOB objPet = new PetitionOB();
    AppealBL objapepalBL = new AppealBL();
    OrderBL objOrdersBL = new OrderBL();
    WhatNewsOB objwhatsOB = new WhatNewsOB();
    WhatsNewBL objNewBL = new WhatsNewBL();
    #endregion

    #region page load zone

    protected void Page_Load(object sender, EventArgs e)
    {
        //p_Var.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/");
        p_Var.Path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/";
        p_Var.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Orders"].ToString() + "/";
        p_Var.FilenameUrl = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/";

        p_Var.str = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Petition"].ToString() + "/" + ConfigurationManager.AppSettings["Public_Notice"].ToString() + "/";

        // p_Var.urlname = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Orders"].ToString() + "/";
        if (!IsPostBack)
        {
		try{
            if (Request.QueryString["Link_Id"] == null && Request.QueryString["PDID"] == null)
            {
                DisplayDetails(Convert.ToInt16(Request.QueryString["Petition_id"]));
            }

            else
            {
                if (Request.QueryString["Link_Id"] != null)
                {
                    bind_Module_Details();
                }
            }

            if (Request.QueryString["RPID"] == null)
            {
            }
            else
            {
                // BindReviewPetitionDetails(Request.QueryString["RPID"].ToString());
                BindReviewDetails(Request.QueryString["RPID"].ToString());
            }
            if (Request.QueryString["Soh_id"] == null)
            {

            }
            else
            {
                Bind_SOHDetails(Request.QueryString["Soh_id"].ToString());
            }
            if (Request.QueryString["PDID"] == null)
            {

            }
            else
            {
                BindAppealPetitionDetails(Request.QueryString["PDID"].ToString());
            }
            if (Request.QueryString["WhatsNewId"] == null)
            {


            }
            else
            {

                //heading.InnerText = Resources.HercResource.WhatNew;
                BindWhatsNew();
            }

            Session["update1"] = Server.UrlEncode(System.DateTime.Now.ToString());
			}
			catch{}
        }

    }
    #endregion

    void Page_PreRender(object obj, EventArgs e)
    {
        ViewState["update1"] = Session["update1"];
    }

    #region function to display Details

    public void DisplayDetails(int value)
    {
        PnlModule.Visible = false;
        petitionObject.TempPetitionId = value;
        p_Var.dSet = petPetitionBL.get_Petition_Details(petitionObject);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            gvDetails.DataSource = p_Var.dSet;
            gvDetails.DataBind();
        }
    }

    #endregion

    #region Gridview gvDetails event RowCommand

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        if (e.CommandName == "ViewDoc")
        {

            string file = e.CommandArgument.ToString();
            p_Var.Path = p_Var.Path + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(p_Var.Path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(p_Var.Path));
                Response.Clear();
                Response.WriteFile(p_Var.Path);
                Response.End();
            }
        }
        if (e.CommandName == "Review")
        {
            gvReview.Visible = true;
            p_Var.id = e.CommandArgument.ToString();
            BindReviewDetails(p_Var.id);

        }
        if (e.CommandName == "connected")
        {

            p_Var.commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });

            p_Var.petition_id = Convert.ToInt16(p_Var.commandArgs[0]);


            p_Var.id = p_Var.commandArgs[0].ToString();
            p_Var.pa_id = Convert.ToInt16(p_Var.commandArgs[1]);

            LinkButton lnkConnectedPetition = (LinkButton)gvrow.FindControl("lnkConnectedPetition");
            Literal ltrlConnectedPetition = (Literal)gvrow.FindControl("ltrlConnectedPetition");
            lnkConnectedPetition.Visible = false;
            ltrlConnectedPetition.Visible = true;


            Bind_Grid(p_Var.id);

        }

        if (e.CommandName == "connectedPetition")
        {
            p_Var.pa_id = Convert.ToInt16(e.CommandArgument.ToString());

            LinkButton lnkpetition = (LinkButton)gvrow.FindControl("lnkpetition");
            Literal ltrlConnectedPetition = (Literal)gvrow.FindControl("ltrlConnectedPetition");
            lnkpetition.Visible = false;
            ltrlConnectedPetition.Visible = true;

            Bind_GridConnected(p_Var.pa_id);

        }
        if (e.CommandName == "connectedPublic")
        {
            string id = e.CommandArgument.ToString();

            LinkButton lnkConnectedPublicNotice = (LinkButton)gvrow.FindControl("lnkConnectedPublicNotice");
            Literal ltrlConnectedPublicNotic = (Literal)gvrow.FindControl("ltrlConnectedPublicNotic");
            lnkConnectedPublicNotice.Visible = false;
            ltrlConnectedPublicNotic.Visible = true;

            ConnectedPublicNoticeDetails(id);
        }
        if (e.CommandName == "connectedSoh")
        {
            string idSOH = e.CommandArgument.ToString();
            Bind_SOH(idSOH);
            LinkButton lnkConnectedSoh = (LinkButton)gvrow.FindControl("lnkConnectedSoh");
            Literal ltrlConnectedSoh = (Literal)gvrow.FindControl("ltrlConnectedSoh");
            lnkConnectedSoh.Visible = false;
            ltrlConnectedSoh.Visible = true;

        }
    }

    #endregion

    #region Function to bind gridView with petitions

    public void Bind_Grid(string petitionid)
    {
        petitionObject.PetitionId = Convert.ToInt16(petitionid);
        p_Var.dSet = petPetitionBL.get_ConnectedPetition(petitionObject);

        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            pPetitionGridDetatils.Visible = true;
            gvPetition.DataSource = p_Var.dSet;
            gvPetition.DataBind();
            p_Var.dSet = null;

        }
        else
        {
            pPetitionGridDetatils.Visible = false;
        }

    }

    #endregion



    #region Function to bind gridView with petitions

    public void Bind_GridPetitionReview(string RPId)
    {
        petitionObject.RPId = Convert.ToInt16(RPId);
        p_Var.dSet = petPetitionBL.get_ConnectedPetitionReview(petitionObject);

        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            pnlReviewConnectedPetition.Visible = true;
            gvConnectedPetitionWithPetition.DataSource = p_Var.dSet;
            gvConnectedPetitionWithPetition.DataBind();
            p_Var.dSet = null;

        }
        else
        {
            pPetitionGridDetatils.Visible = false;
        }

    }

    #endregion


    #region Function to bind gridView with petitions

    public void Bind_GridReview(string RP_ID)
    {
        petitionObject.RPId = Convert.ToInt16(RP_ID);
        p_Var.dSet = petPetitionBL.get_ConnectedReviewPetition(petitionObject);

        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            pnlconnectedReview.Visible = true;
            GrdReviewConnected.DataSource = p_Var.dSet;
            GrdReviewConnected.DataBind();
            p_Var.dSet = null;

        }
        else
        {
            pnlconnectedReview.Visible = false;
        }

    }

    #endregion

    #region Gridview dvDetails event RowDataBound

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet dsNew = new DataSet();
            //  LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            string filename = DataBinder.Eval(e.Row.DataItem, "Petition_File").ToString();
            LinkButton lnkPetition = (LinkButton)e.Row.FindControl("lnkConnectedPetition");
            Label lblPetitionID = (Label)e.Row.FindControl("lblPetition_ID");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            Literal ltrlConnectedFile1 = (Literal)e.Row.FindControl("ltrlConnectedFile1");
            LinkButton lnkPetition1 = (LinkButton)e.Row.FindControl("lnkpetition");

            Label lblpetition = (Label)e.Row.FindControl("lblConnectedPetition_ID");

            Label lblPetitionerName = (Label)e.Row.FindControl("lblPetitionerName");
            Label lblRespondentName = (Label)e.Row.FindControl("lblRespondentName");
            Label lblSubject7 = (Label)e.Row.FindControl("lblSubject7");
            lblPetitionerName.Text = HttpUtility.HtmlDecode(lblPetitionerName.Text);
            //lblRespondentName.Text = HttpUtility.HtmlDecode(lblRespondentName.Text);
            lblSubject7.Text = HttpUtility.HtmlDecode(lblSubject7.Text);
            Label lblpetition1 = (Label)e.Row.FindControl("lblConnectedPetition_ID1");
            LinkButton lnkConnectedPublicNotice = (LinkButton)e.Row.FindControl("lnkConnectedPublicNotice");

            // by ruchi 6 march 2013
            if (lblRespondentName.Text != null && lblRespondentName.Text != "")
            {
                gvDetails.Columns[4].Visible = true;
                lblRespondentName.Text = HttpUtility.HtmlDecode(lblRespondentName.Text);
            }
            else
            {
                gvDetails.Columns[4].Visible = false;
            }
            if (lblPetitionerName.Text != null && lblPetitionerName.Text != "")
            {
                gvDetails.Columns[3].Visible = true;
                lblPetitionerName.Text = HttpUtility.HtmlDecode(lblPetitionerName.Text);
            }
            else
            {
                gvDetails.Columns[3].Visible = false;
            }

            if (lblRemarks.Text != null && lblRemarks.Text != "")
            {
                gvDetails.Columns[14].Visible = true;
                lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
            }
            else
            {
                gvDetails.Columns[14].Visible = false;
            }

            petitionObject.PetitionId = Convert.ToInt16(lnkPetition1.Text);
            dsNew = petPetitionBL.getConnectedPetition(petitionObject);
            if (dsNew.Tables[0].Rows.Count > 0)
            {

                lnkPetition1.Text = dsNew.Tables[0].Rows[0]["Connected_Petition_id"].ToString();
                if (lnkPetition1.Text != null && lnkPetition1.Text != "")
                {
                    lnkPetition1.Text = "Yes";
                    lblpetition.Visible = false;
                    lnkPetition1.Visible = true;
                    lblpetition1.Visible = false;
                    gvDetails.Columns[13].Visible = true;

                }
                else
                {
                    lnkPetition1.Visible = false;
                    lblpetition.Visible = true;
                    lblpetition.Text = "No";
                    lblpetition1.Visible = false;
                    gvDetails.Columns[13].Visible = false;
                }
            }
            else
            {
                gvDetails.Columns[13].Visible = false;
            }
            //END

            Label lblConnectedSoh_ID = (Label)e.Row.FindControl("lblConnectedSoh_ID");
            LinkButton lnkConnectedSoh = (LinkButton)e.Row.FindControl("lnkConnectedSoh");

            HiddenField hidPublicNotice = (HiddenField)e.Row.FindControl("hidPublicNotice");
            Label lblPublicNotice = (Label)e.Row.FindControl("lblPublicNotice");
            DataSet dSetPublicNotice = new DataSet();
            PublicNoticeBL publicnoticeBL = new PublicNoticeBL();
            dSetPublicNotice = publicnoticeBL.Get_ConnectedpubliceNotce(petitionObject);
            //if (hidPublicNotice.Value != null && hidPublicNotice.Value != "")
            if (dSetPublicNotice.Tables[0].Rows.Count > 0)
            {

                lblPublicNotice.Visible = false;
                lnkConnectedPublicNotice.Visible = true;
                lnkConnectedPublicNotice.Text = "Yes";
                gvDetails.Columns[9].Visible = true;
            }
            else
            {
                gvDetails.Columns[9].Visible = false;
                lnkConnectedPublicNotice.Visible = false;
                lblPublicNotice.Visible = true;
            }
            if (lnkPetition.Text != null && lnkPetition.Text != "")
            {
                lnkPetition1.Visible = false;
                lnkPetition.Text = "Yes";

                //lblpetition.Visible = false;
                lnkPetition.Visible = true;
                lblpetition1.Visible = false;
            }
            else
            {

                lnkPetition.Visible = false;
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    lblpetition1.Visible = false;
                }
                else
                {
                    lblpetition1.Visible = true;
                }
                lblpetition.Visible = false; //on date 6 march 2013
            }


            if (filename == null || filename == "")
            {
                //lnk.Visible = false;
            }

            Label lblReview = (Label)e.Row.FindControl("lblReview");
            LinkButton lnkReview = (LinkButton)e.Row.FindControl("lnkReview");


            petitionObject.PetitionId = Convert.ToInt16(lblPetitionID.Text);
            p_Var.dSetCompare = petPetitionBL.get_PetitionID_From_Temp_PetReview(petitionObject);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(lblPetitionID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[0]["Petition_id"]))
                {
                    lnkReview.Visible = true;
                    lblReview.Visible = false;
                    lnkReview.Text = "Yes";
                }
                else
                {
                    lnkReview.Visible = false;
                    lblReview.Visible = true;
                    lblReview.Text = "No";

                }
            }
            else
            {
                lnkReview.Visible = false;
                lblReview.Visible = true;
                lblReview.Text = "No";

            }

            //connected Petition

            Literal orderConnectedFile1 = (Literal)e.Row.FindControl("ltrlConnectedFile1");
            petitionObject.PetitionId = Convert.ToInt16(gvDetails.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = petPetitionBL.getPetitionFileNames(petitionObject);

            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                gvDetails.Columns[6].Visible = true;
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Var.sbuilder.Append(" <a href='" + p_Var.FilenameUrl + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document'>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        string aa=Convert.ToString(p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString());
                        string[] array = aa.Split(';');
                        foreach (string email in array)
                        {
                            p_Var.sbuilder.Append(email + ";");
                        //    p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");
                            p_Var.sbuilder.Append("<br/><hr/>");
                        }
                    }
                     else
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + ", ");
                    }

                    // p_Var.sbuilder.Append(" <a href='" + p_Var.FilenameUrl + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>"  + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                   p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");

                    if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Var.FilenameUrl) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(p_Var.FilenameUrl) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                         p_Var.sbuilder.Append("<br/><hr/>");
                    }
                   // p_Var.sbuilder.Append("<br/><hr/>");

                }
              //   p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");
                p_Var.sbuilder.Append("<a/>");
               
                orderConnectedFile1.Text = p_Var.sbuilder.ToString();
               // p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            }
            else
            {
                gvDetails.Columns[6].Visible = false;
            }
            DataSet dSet = new DataSet();

            dSet = petPetitionBL.getPetitionURLwithFile(petitionObject);
            if (dSet.Tables[0].Rows.Count > 0)
            {

                gvDetails.Columns[6].Visible = true;
                //loop through cells in that row
                string strUrl = dSet.Tables[0].Rows[0]["placeholderfour"].ToString();
                string[] split = strUrl.Split(';');
                      //  ArrayList list = new ArrayList();

                        ////loop through cells in that row
                       // string strUrl1 = dSet.Tables[0].Rows[0]["placeholderfive"].ToString();

                       // string[] split1 = strUrl1.Split(';');
                        //ArrayList list1 = new ArrayList();

                       // foreach (string item in split)
                      //  {
                      //      list.Add(item.Trim());
                      //  }
                      // foreach (string item1 in split1)
                      //  {
                      //      list1.Add(item1.Trim());
                       // }



                      //  string concatsplit;
                       // StringBuilder ctbbld = new StringBuilder();
                       // for (int i = 0; i < split1.Count(); i++)
                       // {
                      //      concatsplit = split[i].ToString() + " " + split1[i].ToString();
                       //     ctbbld.Append(" <a href='" + split1[i].ToString() + "'  target='_blank' title='Click Here to Go" + "&nbsp;" + "&nbsp;" + split1[i].ToString() + "'>");
                        //   ctbbld.Append(split[i].ToString());

                        //   ctbbld.Append("<br/><hr/>");
                         //  ctbbld.Append("<a/>");



                        }

                        //orderConnectedFile1.Text += ctbbld.ToString();
                     
                  
                   // ctbbld.Append("</a>");
                 
                   // orderConnectedFile1.Text += ctbbld.ToString();
                    //change by preeti
                if ((strUrl1 != null || strUrl1 != "") && (strUrl != ""))
                {
                    string[] split = strUrl.Split(';');
                    ArrayList list = new ArrayList();

                    foreach (string item in split)
                    {
                        list.Add(item.Trim());
                    }


                    string[] split1 = strUrl1.Split(';');
                    ArrayList list1 = new ArrayList();
                    //string concatsplit;
                    foreach (string item1 in split1)
                    {
                        list1.Add(item1.Trim());
                    }
                    
                    StringBuilder ctbbld = new StringBuilder();
                    for (int i = 0; i < split1.Count(); i++)
                    {
                        //concatsplit = split[i].ToString() + " " + split1[i].ToString();
                        try

                        {
                            ctbbld.Append("<a href='" + split1[i].ToString() + "'  target='_blank' title='Click Here to Go" + "&nbsp;" + "&nbsp;" + split1[i].ToString() + "'>");
                            ctbbld.Append(split[i].ToString());
                            ctbbld.Append("<br/><hr/>");
                            
                        }
                        catch(Exception ex)
                        {

                        }
                        //for (int j = 0; j < split.Count(); j++)
                        //{
                        //    ctbbld.Append(split[j].ToString());
                        //    ctbbld.Append("<br/><hr/>");
                        //}

                    }
                  
                    ctbbld.Append("</a>");
                 
                    orderConnectedFile1.Text += ctbbld.ToString();
                }
              //change by preeti
               // }

               
            }
            else
            {
                gvDetails.Columns[6].Visible = false;
            }
            //End

            //connected orders
            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            petitionObject.PetitionId = Convert.ToInt16(lblPetitionID.Text);
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            p_Var.dsFileName = petPetitionBL.getConnectedOrders(petitionObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {

                    p_Var.sbuilder.Append("<a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document'>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value)
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + ", ");

                        if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                        {
                            p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        }
                        p_Var.sbuilder.Append(" Dated: " + p_Var.dsFileName.Tables[0].Rows[i]["Date"]);
                        //// p_Var.sbuilder.Append(" <a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>"  + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                        p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");
                        if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {
                                FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }

                        p_Var.sbuilder.Append("<br/><hr/>");
                        gvDetails.Columns[8].Visible = true;
                    }
                    else
                    {
                        //This is for amendment/clarification etc
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        //p_Var.sbuilder.Append("<br /><a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>"  + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                        p_Var.sbuilder.Append("<br />" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");
                        if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {
                                FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }

                        p_Var.sbuilder.Append("<br/><hr/>");
                    }
                }
                p_Var.sbuilder.Append("<a/>");
                orderConnectedFile.Text = p_Var.sbuilder.ToString();

            }
            else
            {
                gvDetails.Columns[8].Visible = false;
            }


            petitionObject.PetitionId = Convert.ToInt16(lblPetitionID.Text);
            p_Var.dSetCompare = petPetitionBL.getConnectedSOH(petitionObject);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {

                lnkConnectedSoh.Visible = true;
                lblConnectedSoh_ID.Visible = false;
                lnkConnectedSoh.Text = "Yes";
                gvDetails.Columns[10].Visible = true;
            }
            else
            {
                lnkConnectedSoh.Visible = false;
                lblConnectedSoh_ID.Visible = true;
                gvDetails.Columns[10].Visible = false;
            }
            //End
            //public Notice
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            Literal ConnectedPublicNotice = (Literal)e.Row.FindControl("ltrlConnectedPublicNotice");

            petitionObject.PetitionId = Convert.ToInt16(lblPetitionID.Text);
            p_Var.dSetChildData = petPetitionBL.getConnectedPublicNotice(petitionObject);
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dSetChildData.Tables[0].Rows.Count; i++)
                {
                    p_Var.sbuilder.Append("<a href='" + p_Var.url + p_Var.dSetChildData.Tables[0].Rows[i]["PublicNotice"] + "' Text='" + p_Var.dSetChildData.Tables[0].Rows[i]["PublicNotice"] + "' target='_blank'>" + p_Var.dSetChildData.Tables[0].Rows[i]["PublicNotice"] + "</a>");
                    p_Var.sbuilder.Append("<br/>");
                    gvDetails.Columns[10].Visible = true;
                }
                ConnectedPublicNotice.Text = p_Var.sbuilder.ToString();

                //gvDetails.Columns[9].Visible = true;
            }
            else
            {
                // gvDetails.Columns[9].Visible = false;
            }



        }
    }

    #endregion

    #region function to bind review Details of Petition

    public void BindReviewDetails(string PRONo)
    {
        if (Request.QueryString["RPID"] != null)
        {
            petitionObject.RPId = Convert.ToInt16(Request.QueryString["RPID"]);
            petitionObject.PRONo = null;
        }
        else
        {
            petitionObject.RPId = null;
            petitionObject.PRONo = PRONo;
        }
        petitionObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);

        p_Var.dSetChildData = petPetitionBL.get_ReviewPetition_Details(petitionObject);
        if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
        {

            gvReview.Visible = true;
            gvReview.DataSource = p_Var.dSetChildData;
            gvReview.DataBind();
            DisplayDetails(Convert.ToInt16(p_Var.dSetChildData.Tables[0].Rows[0]["Petition_Id"].ToString()));
            BindDataOrder();// THis is connected order Date With Description
        }

    }

    #endregion

    #region function to bind review Details of Petition

    public void BindReview(string PRONo)
    {

        petitionObject.RPId = Convert.ToInt16(PRONo);
        petitionObject.PRONo = null;

        petitionObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);

        p_Var.dSet = petPetitionBL.get_ReviewPetition_Details(petitionObject);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            gvReview.Caption = "REVIEW PETITION DETAILS";
            gvReview.Visible = true;
            gvReview.DataSource = p_Var.dSet;
            gvReview.DataBind();
            DisplayDetails(Convert.ToInt16(p_Var.dSet.Tables[0].Rows[0]["Petition_Id"].ToString()));


            // BindDataOrderAppeal(); // 8 Sep 2013
        }

    }

    #endregion


    protected void gvReview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        if (e.CommandName == "ViewDoc")
        {

            string file = e.CommandArgument.ToString();
            p_Var.Path = p_Var.Path + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(p_Var.Path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(p_Var.Path));
                Response.Clear();
                Response.WriteFile(p_Var.Path);
                Response.End();
            }

        }

        if (e.CommandName == "connectedReview")
        {
            p_Var.commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });

            p_Var.petition_id = Convert.ToInt16(p_Var.commandArgs[0]);

            LinkButton lnkReviewpetition = (LinkButton)gvrow.FindControl("lnkReviewpetition");
            Literal ltrlConnectedRpPetition = (Literal)gvrow.FindControl("ltrlConnectedRpPetition");
            LinkButton lnkConnectedReviewPetition = (LinkButton)gvrow.FindControl("lnkConnectedReviewPetition");
            lnkReviewpetition.Visible = false;
            lnkConnectedReviewPetition.Visible = false;
            ltrlConnectedRpPetition.Visible = true;


            p_Var.id = p_Var.commandArgs[0].ToString();
            p_Var.pa_id = Convert.ToInt16(p_Var.commandArgs[1]);
            //p_Var.id = e.CommandArgument.ToString();
            Bind_GridReview(p_Var.id);



        }

        if (e.CommandName == "Appeal")
        {
            grdAppeal.Visible = true;
            p_Var.id = e.CommandArgument.ToString();
            BindAppealDetails(p_Var.id);

        }

        // 7 march by ruchi 
        if (e.CommandName == "ReviewconnectedSoh")
        {
            string ReviewidSOH = e.CommandArgument.ToString();

            LinkButton lnkReviewConnectedSoh = (LinkButton)gvrow.FindControl("lnkReviewConnectedSoh");
            Literal ltrlConnectedRpSoh = (Literal)gvrow.FindControl("ltrlConnectedRpSoh");
            lnkReviewConnectedSoh.Visible = false;
            ltrlConnectedRpSoh.Visible = true;

            Bind_SOH_Review(ReviewidSOH);
        }

        if (e.CommandName == "connectedPublicReview")
        {
            string id = e.CommandArgument.ToString();
            ConnectedPublicNoticeDetailsForReviewPetition(id);

            LinkButton lnkConnectedPublicNoticeReview = (LinkButton)gvrow.FindControl("lnkConnectedPublicNoticeReview");
            Literal ltrlConnectedRpPublicNotice = (Literal)gvrow.FindControl("ltrlConnectedRpPublicNotice");
            lnkConnectedPublicNoticeReview.Visible = false;
            ltrlConnectedRpPublicNotice.Visible = true;

        }


        if (e.CommandName == "ReviewconnectedPetition")
        {
            // Commented on 8 Sep 2013
            //////// string RP_ID =e.CommandArgument.ToString();  
            ////////Bind_GridPetitionReview(RP_ID);
        }
    }
    protected void gvReview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            string filename = DataBinder.Eval(e.Row.DataItem, "File_Name").ToString();

            LinkButton lnkReview = (LinkButton)e.Row.FindControl("lnkConnectedReviewPetition");
            Label lblReview = (Label)e.Row.FindControl("lblConnectedReviewPetition_ID");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks1");
            Literal ltrlrEVIEW = (Literal)e.Row.FindControl("ltrlrEVIEW");
            LinkButton lnkReviewpetition = (LinkButton)e.Row.FindControl("lnkReviewpetition");

            Label lblApplicant = (Label)e.Row.FindControl("lblApplicant");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            Label lblSubject5 = (Label)e.Row.FindControl("lblSubject5");

            lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
            lblSubject5.Text = HttpUtility.HtmlDecode(lblSubject5.Text);


            // 3 Aug 2013


            PetitionOB objnew = new PetitionOB();

            if (lblApplicant.Text != null && lblApplicant.Text != "")
            {
                gvReview.Columns[3].Visible = true;
                lblApplicant.Text = HttpUtility.HtmlDecode(lblApplicant.Text);
            }
            else
            {
                gvReview.Columns[3].Visible = false;
            }
            if (lblRespondent.Text != null && lblRespondent.Text != "")
            {
                gvReview.Columns[4].Visible = true;
                lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
            }
            else
            {
                gvReview.Columns[4].Visible = false;
            }

            if (lblRemarks.Text != null && lblRemarks.Text != "")
            {
                gvReview.Columns[13].Visible = true;
            }
            else
            {
                gvReview.Columns[13].Visible = false;
            }

            if (lnkReview.Text != null && lnkReview.Text != "")
            {

                lnkReview.Text = "Yes";

                lblReview.Visible = false;
                lnkReview.Visible = true;
                gvReview.Columns[12].Visible = true;
            }
            else
            {
                gvReview.Columns[12].Visible = false;
                lblReview.Visible = true;
                lnkReview.Visible = false;

                //lblpetition.Visible  = true; //on date 6 march 2013
            }



            // 17 jan 2013
            Label lblReview1 = (Label)e.Row.FindControl("lblReview1");
            LinkButton lnkReview1 = (LinkButton)e.Row.FindControl("lnkReview1");
            LinkButton lnkconnectedPetition = (LinkButton)e.Row.FindControl("lnkConnectedReview");
            Label lblPetitionID1 = (Label)e.Row.FindControl("lblPetitionID");


            // by ruchi on date 7 march 2013
            Label lblReviewConnectedSoh_ID = (Label)e.Row.FindControl("lblReviewConnectedSoh_ID");
            LinkButton lnkReviewConnectedSoh = (LinkButton)e.Row.FindControl("lnkReviewConnectedSoh");

            //connected Petition
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            Literal orderConnectedFile3 = (Literal)e.Row.FindControl("ltrlrEVIEW");
            objPet.RPId = Convert.ToInt16(gvReview.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = petPetitionBL.getReviewPetitionFileNames(objPet);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                gvReview.Columns[6].Visible = true;
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Var.sbuilder.Append("<a href='" + p_Var.FilenameUrl + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document'>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Comment"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comment"].ToString() != "")
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comment"] + ",  ");
                    }

                    //// p_Var.sbuilder.Append("  <a href='" + p_Var.FilenameUrl + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>"  + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                    p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Var.FilenameUrl) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(p_Var.FilenameUrl) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }

                    p_Var.sbuilder.Append("<br/><hr/>");
                    gvReview.Columns[6].Visible = true;
                }
                p_Var.sbuilder.Append("<a/>");
                orderConnectedFile3.Text = p_Var.sbuilder.ToString();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            }
            else
            {
                gvReview.Columns[6].Visible = true;
            }
            //End

            objPet.RPId = Convert.ToInt16(lblReview1.Text);
            p_Var.dSetCompare = petPetitionBL.getReviewConnectedSOH(objPet);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {

                lnkReviewConnectedSoh.Visible = true;
                lblReviewConnectedSoh_ID.Visible = false;
                lnkReviewConnectedSoh.Text = "Yes";
                gvReview.Columns[10].Visible = true;

            }
            else
            {
                lnkReviewConnectedSoh.Visible = false;
                lblReviewConnectedSoh_ID.Visible = true;
                gvReview.Columns[10].Visible = true;

            }

            petitionObject.RPId = Convert.ToInt16(lblPetitionID1.Text);
            p_Var.dSetCompare = petPetitionBL.get_RP_From_Temp_PetReview(petitionObject);



            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(lblPetitionID1.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[0]["RP_Id"]))
                {
                    lnkReview1.Visible = true;
                    lblReview1.Visible = false;
                    lnkReview1.Text = "Yes";

                }
                else
                {
                    lnkReview1.Visible = false;
                    lblReview1.Visible = true;
                    lblReview1.Text = "No";


                }
            }
            else
            {
                lnkReview1.Visible = false;
                lblReview1.Visible = true;
                lblReview1.Text = "No";


            }



            //connected orders by ruchi on date 7 march 2013
            p_Var.dsFileName = null;
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            Literal RevieworderConnectedFile = (Literal)e.Row.FindControl("ltrlReviewConnectedFile");
            petitionObject.RPNo = lblPetitionID1.Text;
            p_Var.dsFileName = petPetitionBL.getConnectedOrdersForReview(petitionObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Var.sbuilder.Append("<a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document'>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value)
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + ", ");

                        if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                        {
                            p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        }
                        p_Var.sbuilder.Append(" Dated: " + p_Var.dsFileName.Tables[0].Rows[i]["Date"]);
                        // p_Var.sbuilder.Append("<a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                        p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");

                        if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {

                                FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }

                        p_Var.sbuilder.Append("<br/><hr/>");
                        gvReview.Columns[8].Visible = true;

                    }
                    else
                    {
                        //This is for amendment/clarification etc
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        ////p_Var.sbuilder.Append("<br /><a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>"  + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                        p_Var.sbuilder.Append("<br />" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");

                        if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {

                                FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }


                        p_Var.sbuilder.Append("<br/><hr/>");
                    }
                }
                p_Var.sbuilder.Append("<a/>");
                RevieworderConnectedFile.Text = p_Var.sbuilder.ToString();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            }
            else
            {
                gvReview.Columns[8].Visible = false;
                //gvReview.Columns[7].Visible = false;
            }
            //End
            DataSet dSet = new DataSet();

            dSet = petPetitionBL.getPetitionURLwithFileReview(petitionObject);
            if (dSet.Tables[0].Rows.Count > 0)
            {

                gvDetails.Columns[6].Visible = true;
                //loop through cells in that row
                string strUrl = dSet.Tables[0].Rows[0]["placeholderfour"].ToString();
                string[] split = strUrl.Split(';');
                ArrayList list = new ArrayList();

                //loop through cells in that row
                string strUrl1 = dSet.Tables[0].Rows[0]["placeholderfive"].ToString();

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


                //string concatsplit;
                StringBuilder ctbbld = new StringBuilder();
                for (int i = 0; i < split1.Count(); i++)
                {
                    //concatsplit = split[i].ToString() + " " + split1[i].ToString();
                    ctbbld.Append(" <a href='" + split1[i].ToString() + "'  target='_blank' title='Click Here to Go" + "&nbsp;" + "&nbsp;" + split1[i].ToString() + "'>");
                    ctbbld.Append(split[i].ToString());
                    ctbbld.Append("</a><br/>");



                }
                ltrlrEVIEW.Text += ctbbld.ToString();
            }
            else
            {
                gvDetails.Columns[6].Visible = false;
            }
            // connected public notice for review petition
            HiddenField hidPublicNoticeReview = (HiddenField)e.Row.FindControl("hidPublicNoticeReview");
            LinkButton lnkConnectedPublicNoticeReview = (LinkButton)e.Row.FindControl("lnkConnectedPublicNoticeReview");
            Label lblPublicNoticeReview = (Label)e.Row.FindControl("lblPublicNoticeReview");
            DataSet dSetPublicNotice = new DataSet();
            dSetPublicNotice = objPublic.Get_ConnectedpubliceNotceForReview(petitionObject);
            //if (hidPublicNoticeReview.Value != null && hidPublicNoticeReview.Value != "")
            if (dSetPublicNotice.Tables[0].Rows.Count > 0)
            {
                lblPublicNoticeReview.Visible = false;
                lnkConnectedPublicNoticeReview.Visible = true;
                lnkConnectedPublicNoticeReview.Text = "Yes";
                gvReview.Columns[9].Visible = true;
            }
            else
            {
                gvReview.Columns[9].Visible = false;
                lnkConnectedPublicNoticeReview.Visible = false;
                lblPublicNoticeReview.Visible = true;
            }



        }
    }


    #region Function to display Module Details

    public void bind_Module_Details()
    {
        try
        {
            if (Request.QueryString["Link_Id"] != null && Request.QueryString["Link_Id"] != "")
            {

                PGridDetails.Visible = false;
                pnlReview.Visible = false;
                PnlModule.Visible = true;
                int recid = Convert.ToInt32(Request.QueryString["Link_Id"]);
                obj_linkOB.linkID = recid;
                //obj_linkOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New);
                obj_linkOB.ModuleId = Convert.ToInt16(Request.QueryString["ModuleId"]);
                obj_linkOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
                p_Var.dSetChildData = obj_linkBL.Link_DisplayDetails(obj_linkOB);
                if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
                {
                    LblTitle.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Name"].ToString();
                    if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 12 || Convert.ToInt16(Request.QueryString["ModuleId"]) == 13)
                    {
                        trDesc.Visible = false;

                    }
                    else
                    {
                        trDesc.Visible = true;
                        if (p_Var.dSetChildData.Tables[0].Rows[0]["Details"] == DBNull.Value)
                        {
                            Lbldescription.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Details"].ToString();
                        }
                        else if (Convert.ToInt16(Request.QueryString["ModuleId"]) == Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Notification))
                        {
                            Lbldescription.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Details"].ToString();
                        }
                        else
                        {
                            Lbldescription.Text = p_Var.dSetChildData.Tables[0].Rows[0]["name"].ToString();
                        }
                    }
                    if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 12 || Convert.ToInt16(Request.QueryString["ModuleId"]) == 13)
                    {
                        ltrlConnectedFile.Visible = true;
                        obj_linkOB.linkID = recid;
                        obj_linkOB.ModuleId = Convert.ToInt16(Request.QueryString["ModuleId"]);
                        p_Var.dsFileName = obj_linkBL.getDiscussionFileNames(obj_linkOB);
                        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                            {
                                trdownload.Visible = true;
                                p_Var.sbuilder.Append("<a href='" + p_Var.Path + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document'>");
                                if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString().Trim() != "")
                                {
                                    p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                                }


                                // p_Var.sbuilder.Append("<a href='" + p_Var.Path + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>"  + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                                p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");
                                if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                                {
                                    if (File.Exists(Server.MapPath(p_Var.Path) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                                    {
                                        FileInfo finfo = new FileInfo(Server.MapPath(p_Var.Path) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                        double FileInBytes = finfo.Length;
                                        p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                                    }
                                }
                                p_Var.sbuilder.Append("<br/><hr/>");
                            }
                            p_Var.sbuilder.Append("<a/>");
                            ltrlConnectedFile.Text = p_Var.sbuilder.ToString();
                        }
                        else
                        {
                            trdownload.Visible = false;
                        }
                    }
                    else
                    {

                        LinkOB objlnkOB = new LinkOB();
                        LinkBL objlnkBL = new LinkBL();
                        objlnkOB.TempLinkId = Convert.ToInt16(Request.QueryString["Link_Id"]);
                        objlnkOB.ModuleId = Convert.ToInt16(Request.QueryString["ModuleId"]);
                        p_Var.dsFileName = objlnkBL.getFileName(objlnkOB);
                        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
                        {
                            ltrNotification.Visible = true;

                            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                            for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                            {

                                p_Var.sbuilder.Append("<a href='" + p_Var.Path + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "' target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");
                                if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                                {
                                    if (File.Exists(Server.MapPath(p_Var.Path) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                                    {
                                        FileInfo finfo = new FileInfo(Server.MapPath(p_Var.Path) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                        double FileInBytes = finfo.Length;
                                        p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                                    }
                                }

                                p_Var.sbuilder.Append("</a>");

                            }
                        }
                        else
                        {
                            trdownload.Visible = false;
                        }


                        ltrNotification.Text = p_Var.sbuilder.ToString();
                        p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

                        //LblFileName.Text = p_Var.dSetChildData.Tables[0].Rows[0]["File_Name"].ToString();
                    }
                    if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 12 || Convert.ToInt16(Request.QueryString["ModuleId"]) == 13)
                    {
                        if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 12)
                        {
                            if (p_Var.dSetChildData.Tables[0].Rows[0]["LastDateOfReceivingComment"].ToString() != null && p_Var.dSetChildData.Tables[0].Rows[0]["LastDateOfReceivingComment"].ToString() != "")
                            {
                                trlastdate.Visible = true;
                                lbllastdate.Text = "Last Date of Receiving Comments";
                                LblStartDate.Text = p_Var.dSetChildData.Tables[0].Rows[0]["LastDateOfReceivingComment"].ToString();
                            }
                            else
                            {
                                trlastdate.Visible = false;

                            }
                        }
                        else if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 13)
                        {
                            if (p_Var.dSetChildData.Tables[0].Rows[0]["LastDateOfReceivingComment"].ToString() != null && p_Var.dSetChildData.Tables[0].Rows[0]["LastDateOfReceivingComment"].ToString() != "")
                            {
                                trlastdate.Visible = true;
                                lbllastdate.Text = "Last Date of Receiving Applications";
                                LblStartDate.Text = p_Var.dSetChildData.Tables[0].Rows[0]["LastDateOfReceivingComment"].ToString();
                            }
                            else
                            {
                                trlastdate.Visible = false;

                            }
                        }
                        if (p_Var.dSetChildData.Tables[0].Rows[0]["PublicHearingDate"].ToString() != null && p_Var.dSetChildData.Tables[0].Rows[0]["PublicHearingDate"].ToString() != "")
                        {
                            trhearingDate.Visible = true;
                            LblEndDate.Text = p_Var.dSetChildData.Tables[0].Rows[0]["PublicHearingDate"].ToString();
                            trvenu.Visible = true;
                            lblvenu.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Venu"].ToString();
                        }
                        else
                        {
                            trhearingDate.Visible = false;
                            trvenu.Visible = false;
                        }
                        //if (p_Var.dSetChildData.Tables[0].Rows[0]["Venu"].ToString() != null && p_Var.dSetChildData.Tables[0].Rows[0]["Venu"].ToString() != "")
                        //{
                        //    trvenu.Visible = true;
                        //    lblvenu.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Venu"].ToString();
                        //}
                        //else
                        //{
                        //    trvenu.Visible = false;

                        //}

                    }

                }
            }
        }

        catch
        {
            //throw;
        }
    }

    #endregion

    protected void grdAppeal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDocAppeal")
        {
            string path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Judgement_Pronounced"] + "/";
            string file = e.CommandArgument.ToString();
            path = path + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(path));
                Response.Clear();
                Response.WriteFile(path);
                Response.End();
            }


        }
    }

    protected void grdAppeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal ltrlConnectedAppeal = (Literal)e.Row.FindControl("ltrlConnectedAppeal");
            petitionObject.AppealId = Convert.ToInt16(grdAppeal.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = petPetitionBL.getAppealFileNames(petitionObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ",  ");
                    }

                    p_Var.sbuilder.Append(" <a href='" + p_Var.FilenameUrl + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "");

                    if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Var.FilenameUrl) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {

                            FileInfo finfo = new FileInfo(Server.MapPath(p_Var.FilenameUrl) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }

                    p_Var.sbuilder.Append("</a><br/><hr/>");

                }
                ltrlConnectedAppeal.Text = p_Var.sbuilder.ToString();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            }
        }


    }

    protected void gvPetition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDoc")
        {
            string path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/");
            string file = e.CommandArgument.ToString();
            path = path + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(path));
                Response.Clear();
                Response.WriteFile(path);
                Response.End();
            }


        }
    }
    protected void gvPetition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            string filename = DataBinder.Eval(e.Row.DataItem, "Petition_File").ToString();

            Label lblPetitionerName = (Label)e.Row.FindControl("lblPetitionerName");
            Label lblRespondentName = (Label)e.Row.FindControl("lblRespondentName");
            Label lblSubject6 = (Label)e.Row.FindControl("lblSubject6");

            lblPetitionerName.Text = HttpUtility.HtmlDecode(lblPetitionerName.Text);
            lblRespondentName.Text = HttpUtility.HtmlDecode(lblRespondentName.Text);
            lblSubject6.Text = HttpUtility.HtmlDecode(lblSubject6.Text);


            //connected Petition

            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            Literal orderConnectedFile2 = (Literal)e.Row.FindControl("ltrlConnectedFile2");
            petitionObject.PetitionId = Convert.ToInt16(gvPetition.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = petPetitionBL.getPetitionFileNames(petitionObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Var.sbuilder.Append("<a href='" + p_Var.Path + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document'>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ",  ");
                    }

                    //p_Var.sbuilder.Append( "<img src=\"images/pdf-icon.jpg\" title=\"View Document\" width=\"15\" alt=\"View Document\" height=\"15\" /> ");
                    p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");

                    if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Var.Path) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {

                            FileInfo finfo = new FileInfo(Server.MapPath(p_Var.Path) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }

                    p_Var.sbuilder.Append("<br/><hr/>");

                }
                p_Var.sbuilder.Append("<a/>");
                orderConnectedFile2.Text = p_Var.sbuilder.ToString();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            }
            else
            {

            }
            //End


        }
    }

    #region function to bind review Details of Petition

    public void BindAppealDetails(string PRONo)
    {

        petitionObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        petitionObject.PRONo = PRONo;
        p_Var.dSetChildData = petPetitionBL.get_AppealPetition_Details(petitionObject);
        if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
        {
            //Dreview.Visible = true;
            PnlAppealDetails.Visible = true;
            grdAppealDetails.DataSource = p_Var.dSetChildData;
            grdAppealDetails.DataBind();

            //BindDataOrderAppeal(); // This is done on date 8 Sep 2013

        }
    }

    #endregion


    protected void gvPubNotice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        p_Var.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["pdf"] + "/");
        if (e.CommandName == "ViewDoc")
        {

            string file = e.CommandArgument.ToString();
            p_Var.Path = p_Var.Path + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(p_Var.Path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(p_Var.Path));
                Response.Clear();
                Response.WriteFile(p_Var.Path);
                Response.End();

            }
        }
        if (e.CommandName == "ViewDetails")
        {
		try{
				if (Session["update1"].ToString() == ViewState["update1"].ToString())
				{
                p_Var.stringTypeID = e.CommandArgument.ToString();
                p_Var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "DetailsPage.aspx?PulicNoticeId=" + p_Var.stringTypeID) + "', 'mywindow', " +
                                   "'menubar=no, resizable=no,target=_blank, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", p_Var.strPopupID);
                Session["update1"] = Server.UrlEncode(System.DateTime.Now.ToString());
				}
			}
		catch{}
        }
    }

    #region function to display Details

    public void ConnectedPublicNoticeDetails(string Id)
    {
        PnlPublic.Visible = true;
        petitionObject.PetitionId = Convert.ToInt16(Id);
        p_Var.dSetCompare = objPublic.Get_ConnectedpubliceNotce(petitionObject);
        if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
        {
            gvPubNotice.DataSource = p_Var.dSetCompare;
            gvPubNotice.DataBind();
        }
    }

    #endregion


    #region function to display Details of review petition

    public void ConnectedPublicNoticeDetailsForReviewPetition(string Id)
    {
        pnlPublicNoticeReview.Visible = true;
        petitionObject.RPId = Convert.ToInt16(Id);
        p_Var.dSetCompare = objPublic.Get_ConnectedpubliceNotceForReview(petitionObject);
        if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
        {
            gvPubNoticeReview.DataSource = p_Var.dSetCompare;
            gvPubNoticeReview.DataBind();
        }
    }

    #endregion


    protected void gvPubNotice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            string URL = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Petition"].ToString() + "/" + ConfigurationManager.AppSettings["Public_Notice"].ToString() + "/";

            LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            string filename = DataBinder.Eval(e.Row.DataItem, "PublicNotice").ToString();
            Label lblDesc = (Label)e.Row.FindControl("lblDesc");
            LinkButton lnkTitle = (LinkButton)e.Row.FindControl("lnkTitle");
            Label lblTitle = (Label)e.Row.FindControl("lblTitle");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
            lblDesc.Text = HttpUtility.HtmlDecode(lblDesc.Text);
            if (filename == null || filename == "")
            {
                //lnk.Visible = false;
            }

            if (lblDesc.Text == null || lblDesc.Text == "")
            {
                lblTitle.Visible = true;
                lnkTitle.Visible = false;
            }
            else
            {
                lblTitle.Visible = false;
                lnkTitle.Visible = true;
            }

            //connected Petition

            Literal ltrlConnectedFilePublic = (Literal)e.Row.FindControl("ltrlConnectedFilePublic");
            petitionObject.PublicNoticeID = Convert.ToInt16(gvPubNotice.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = objPublic.getPublicNoticeFileNames(petitionObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Var.sbuilder.Append("<a href='" + URL + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'  title='View Document'>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ",  ");
                    }
                    ////p_Var.sbuilder.Append("<a href='" + URL + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                    p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");

                    if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(URL) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {

                            FileInfo finfo = new FileInfo(Server.MapPath(URL) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }

                    p_Var.sbuilder.Append("<br/>");

                }
                p_Var.sbuilder.Append("<a/><hr/>");
                ltrlConnectedFilePublic.Text = p_Var.sbuilder.ToString();

            }
            DataSet dSet = new DataSet();

            dSet = objPublic.getPublicNoticeURLwithFile(petitionObject);

            if (dSet.Tables[0].Rows.Count > 0)
            {


                //loop through cells in that row
                trFileName.Visible = true;
                string strUrl = dSet.Tables[0].Rows[0]["placeholderseven"].ToString();
                string[] split = strUrl.Split(';');
                ArrayList list = new ArrayList();

                //loop through cells in that row
                string strUrl1 = dSet.Tables[0].Rows[0]["placeholdersix"].ToString();

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



                //string concatsplit;
                StringBuilder ctbbld = new StringBuilder();
                for (int i = 0; i < split1.Count(); i++)
                {
                    //concatsplit = split[i].ToString() + " " + split1[i].ToString();
                    ctbbld.Append(" <a href='" + split1[i].ToString() + "'  target='_blank' title='Click Here to Go" + "&nbsp;" + "&nbsp;" + split1[i].ToString() + "'>");
                    ctbbld.Append(split[i].ToString());
                    ctbbld.Append("</a><br/>");



                }
                ltrlConnectedFilePublic.Text += ctbbld.ToString();
            }

        }
    }


    protected void gvPubNoticeReview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string URL = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["pdf"] + "/";
            string URL = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Petition"].ToString() + "/" + ConfigurationManager.AppSettings["Public_Notice"].ToString() + "/";
            LinkButton lnkReview = (LinkButton)e.Row.FindControl("lblViewDocReview");
            string filenameReview = DataBinder.Eval(e.Row.DataItem, "PublicNotice").ToString();
            Label lblDescReview = (Label)e.Row.FindControl("lblDescReview");
            LinkButton lnkTitleReview = (LinkButton)e.Row.FindControl("lnkTitleReview");
            Label lblTitleReview = (Label)e.Row.FindControl("lblTitleReview");

            Label lblRemarksReview = (Label)e.Row.FindControl("lblRemarksReview");

            lblRemarksReview.Text = HttpUtility.HtmlDecode(lblRemarksReview.Text);

            if (lblDescReview.Text == null || lblDescReview.Text == "")
            {
                lblTitleReview.Visible = true;
                lnkTitleReview.Visible = false;
            }
            else
            {
                lblTitleReview.Visible = false;
                lnkTitleReview.Visible = true;
            }
            //connected Petition

            Literal ltrlFilePublicConnected = (Literal)e.Row.FindControl("ltrlFilePublicConnected");
            petitionObject.PublicNoticeID = Convert.ToInt16(gvPubNoticeReview.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = objPublic.getPublicNoticeFileNames(petitionObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Var.sbuilder.Append("<a href='" + URL + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document'>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ",  ");
                    }

                    //p_Var.sbuilder.Append("<a href='" + URL + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>"  + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                    p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");

                    if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(URL) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {

                            FileInfo finfo = new FileInfo(Server.MapPath(URL) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }
                    p_Var.sbuilder.Append("<br/><hr/>");

                }
                p_Var.sbuilder.Append("</a>");
                ltrlFilePublicConnected.Text = p_Var.sbuilder.ToString();


                DataSet dSet = new DataSet();

                dSet = objPublic.getPublicNoticeURLwithFile(petitionObject);

                if (dSet.Tables[0].Rows.Count > 0)
                {


                    //loop through cells in that row
                    trFileName.Visible = true;
                    string strUrl = dSet.Tables[0].Rows[0]["placeholderseven"].ToString();
                    string[] split = strUrl.Split(';');
                    ArrayList list = new ArrayList();

                    //loop through cells in that row
                    string strUrl1 = dSet.Tables[0].Rows[0]["placeholdersix"].ToString();

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



                    //string concatsplit;
                    StringBuilder ctbbld = new StringBuilder();
                    for (int i = 0; i < split1.Count(); i++)
                    {
                        //concatsplit = split[i].ToString() + " " + split1[i].ToString();
                        ctbbld.Append(" <a href='" + split1[i].ToString() + "'  target='_blank' title='Click Here to Go" + "&nbsp;" + "&nbsp;" + split1[i].ToString() + "'>");
                        ctbbld.Append(split[i].ToString());
                        ctbbld.Append("</a><br/>");



                    }
                    ltrlFilePublicConnected.Text += ctbbld.ToString();
                }

            }
            else
            {
                // gvPetition.Columns[8].Visible = false;
            }
            //End
        }
    }



    protected void gvPubNoticeReview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        p_Var.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["pdf"] + "/");

        if (e.CommandName == "ViewDetails1")
        {
            if (Session["update1"].ToString() == ViewState["update1"].ToString())
            {
                p_Var.stringTypeID = e.CommandArgument.ToString();
                p_Var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/" + "DetailsPage.aspx?PulicNoticeId=" + p_Var.stringTypeID) + "', 'mywindow', " +
                                   "'menubar=no, resizable=no, scrollbars=yes,width=700,height=530,top=0,left=0 ')" +
                                   "</script>";
                this.Page.RegisterStartupScript("PopupScript", p_Var.strPopupID);
                Session["update1"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }

        }
    }

    public void Bind_SOH(string Id)
    {
        string Url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["scheduleofHearing"] + "/";
        PnlSOH.Visible = true;
        petitionObject.PetitionId = Convert.ToInt16(Id);
        p_Var.dSetChildData = petPetitionBL.getConnectedSOH(petitionObject);
        // Literal ltrlConnectedFileSOHDetails = gvSOH.Rows[j].FindControl("ltrlConnectedFileSOHDetails") as Literal;
        if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
        {
            //Dreview.Visible = true;
            gvSOH.DataSource = p_Var.dSetChildData;
            gvSOH.DataBind();

            //This is for Download option for SOH
            for (int j = 0; j < p_Var.dSetChildData.Tables[0].Rows.Count; j++)
            {
                Literal ltrlConnectedFileSOHDetails = gvSOH.Rows[j].FindControl("ltrlConnectedFileSOHDetails") as Literal;
                petitionObject.soh_ID = Convert.ToInt16(p_Var.dSetChildData.Tables[0].Rows[j]["Soh_ID"]);

                //Code to display files
                p_Var.dSetCompare = petPetitionBL.getSohFileNames(petitionObject);
                if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
                {


                    p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                    for (int i = 0; i < p_Var.dSetCompare.Tables[0].Rows.Count; i++)
                    {
                        p_Var.sbuilder.Append("<a href='" + Url + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"] + "'  target='_blank'  title='View Document' >");
                        if (p_Var.dSetCompare.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dSetCompare.Tables[0].Rows[i]["Comments"].ToString() != "")
                        {
                            p_Var.sbuilder.Append(p_Var.dSetCompare.Tables[0].Rows[i]["Comments"] + ", ");
                        }

                        ////p_Var.sbuilder.Append("<a href='" + Url + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                        p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");

                        if (p_Var.dSetCompare.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(Url) + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"]))
                            {

                                FileInfo finfo = new FileInfo(Server.MapPath(Url) + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }

                        p_Var.sbuilder.Append("<br/><hr/>");

                    }
                    p_Var.sbuilder.Append("</a>");
                    ltrlConnectedFileSOHDetails.Text = p_Var.sbuilder.ToString();

                }//End

                //Code to display links

                DataSet dSet1 = new DataSet();

                dSet1 = petPetitionBL.getScheduleOfHearingURLwithFile(petitionObject);
                if (dSet1.Tables[0].Rows.Count > 0)
                {

                    SOHFiles.Visible = true;
                    //loop through cells in that row
                    string strUrl = dSet1.Tables[0].Rows[0]["placeholderTwo"].ToString();
                    string[] split = strUrl.Split(';');
                    ArrayList list = new ArrayList();

                    //loop through cells in that row
                    string strUrl1 = dSet1.Tables[0].Rows[0]["placeholderOne"].ToString();

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
                } //End

            }//End of for loop


        }
        //DataSet dSet = new DataSet();

        //dSet = petPetitionBL.getScheduleOfHearingURLwithFile(petitionObject);
        //if (dSet.Tables[0].Rows.Count > 0)
        //{


        //    //loop through cells in that row
        //    string strUrl = dSet.Tables[0].Rows[0]["placeholdertwo"].ToString();
        //    string[] split = strUrl.Split(';');
        //    ArrayList list = new ArrayList();

        //    //loop through cells in that row
        //    string strUrl1 = dSet.Tables[0].Rows[0]["placeholderone"].ToString();

        //    string[] split1 = strUrl1.Split(';');
        //    ArrayList list1 = new ArrayList();

        //    foreach (string item in split)
        //    {
        //        list.Add(item.Trim());
        //    }
        //    foreach (string item1 in split1)
        //    {
        //        list1.Add(item1.Trim());
        //    }


        //    Literal ltrlConnectedFileSOHDetails = gvSOH.Rows[0].FindControl("ltrlConnectedFileSOHDetails") as Literal;
        //    //string concatsplit;
        //    StringBuilder ctbbld = new StringBuilder();
        //    for (int i = 0; i < split1.Count(); i++)
        //    {
        //        //concatsplit = split[i].ToString() + " " + split1[i].ToString();

        //        ctbbld.Append(" <a href='" + split1[i].ToString() + "'  target='_blank' title='Click Here to Go" + "&nbsp;" + "&nbsp;" + split1[i].ToString() + "'>");
        //        ctbbld.Append(split[i].ToString());
        //        ctbbld.Append("</a><br/>");



        //    }
        //    ltrlConnectedFileSOHDetails.Text += ctbbld.ToString();
        //}
    }


    public void Bind_SOHDetails(string Id)
    {
        string Url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["scheduleofHearing"] + "/";
        PnlSohDetails.Visible = true;
        petitionObject.SohTempID = Convert.ToInt16(Id);
        petitionObject.LangId = Convert.ToInt16(Module_ID_Enum.Language_ID.English);
        p_Var.dsFileName = petPetitionBL.get_ScheduleOfHearingDetails(petitionObject);
        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
        {
            //Dreview.Visible = true;
            GvSohDetails.DataSource = p_Var.dsFileName;
            GvSohDetails.DataBind();
            for (int i = 0; i < GvSohDetails.Rows.Count; i++)
            {
                Label lblNumber = GvSohDetails.Rows[i].FindControl("lblnumber") as Label;
                Label lblPetitioner = GvSohDetails.Rows[i].FindControl("lblPetitioner") as Label;

                Label lblRespondent = GvSohDetails.Rows[i].FindControl("lblRespondent") as Label;
                if (p_Var.dsFileName.Tables[0].Rows[i]["Petition_ID"] == DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Petition_ID"].ToString() == "")
                {
                    if (p_Var.dsFileName.Tables[0].Rows[i]["RP_No"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["RP_No"].ToString() != "")
                    {
                        GvSohDetails.Columns[1].Visible = true;
                        lblNumber.Text = p_Var.dsFileName.Tables[0].Rows[i]["RP_No"].ToString();
                    }
                    else
                    {

                        GvSohDetails.Columns[1].Visible = false;
                    }
                }
                else
                {
                    if (p_Var.dsFileName.Tables[0].Rows[i]["PRO_No"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["PRO_No"].ToString() != "")
                    {
                        GvSohDetails.Columns[1].Visible = true;
                        lblNumber.Text = p_Var.dsFileName.Tables[0].Rows[i]["PRO_No"].ToString();
                    }
                    else
                    {
                        GvSohDetails.Columns[1].Visible = false;
                    }

                }
                if (p_Var.dsFileName.Tables[0].Rows[i]["Petitioner_Name"] == DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Petitioner_Name"].ToString() == "")
                {
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Applicant_Name"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Applicant_Name"].ToString() != "")
                    {
                        GvSohDetails.Columns[3].Visible = true;
                        lblPetitioner.Text = HttpUtility.HtmlDecode(p_Var.dsFileName.Tables[0].Rows[i]["Applicant_Name"].ToString());
                    }
                    else
                    {
                        GvSohDetails.Columns[3].Visible = false;
                    }
                }
                else
                {
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Petitioner_Name"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Petitioner_Name"].ToString() != "")
                    {
                        GvSohDetails.Columns[3].Visible = true;
                        lblPetitioner.Text = HttpUtility.HtmlDecode(p_Var.dsFileName.Tables[0].Rows[i]["Petitioner_Name"].ToString());
                    }
                    else
                    {
                        GvSohDetails.Columns[3].Visible = false;
                    }

                }
                if (p_Var.dsFileName.Tables[0].Rows[i]["Respondent_name"] == DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Respondent_name"].ToString() != "")
                {
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Respondent_name"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Respondent_name"].ToString() != "")
                    {
                        GvSohDetails.Columns[4].Visible = true;
                        lblRespondent.Text = HttpUtility.HtmlDecode(p_Var.dsFileName.Tables[0].Rows[i]["Respondent_name"].ToString());
                    }
                    else
                    {
                        GvSohDetails.Columns[4].Visible = false;
                    }
                }
                else
                {
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Respondent_name"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Respondent_name"].ToString() != "")
                    {
                        GvSohDetails.Columns[4].Visible = true;
                        lblRespondent.Text = HttpUtility.HtmlDecode(p_Var.dsFileName.Tables[0].Rows[i]["Respondent_name"].ToString());
                    }
                    else
                    {
                        GvSohDetails.Columns[4].Visible = false;
                    }

                }
            }
        }

        petitionObject.soh_ID = Convert.ToInt16(Id);
        p_Var.dSetCompare = petPetitionBL.getSohFileNames(petitionObject);
        if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
        {
            SOHFiles.Visible = true;
            p_Var.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Petition"].ToString() + "/" + ConfigurationManager.AppSettings["scheduleofHearing"].ToString() + "/";
            for (int i = 0; i < p_Var.dSetCompare.Tables[0].Rows.Count; i++)
            {
                p_Var.sbuilder.Append("<a href='" + Url + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document'>");
                if (p_Var.dSetCompare.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dSetCompare.Tables[0].Rows[i]["Comments"].ToString() != "")
                {
                    p_Var.sbuilder.Append(p_Var.dSetCompare.Tables[0].Rows[i]["Comments"] + ", ");
                }


                //// p_Var.sbuilder.Append("<a href='" + Url + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>"  + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");
                if (p_Var.dSetCompare.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                {
                    if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"]))
                    {
                        FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"]);
                        double FileInBytes = finfo.Length;
                        p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                    }
                }

                p_Var.sbuilder.Append("<br/><hr/>");

            }
            p_Var.sbuilder.Append("<a/>");
            ltrlConnectedFileSOH.Text = p_Var.sbuilder.ToString();

        }
        else
        {
            SOHFiles.Visible = false;
        }

        DataSet dSet = new DataSet();

        dSet = petPetitionBL.getScheduleOfHearingURLwithFile(petitionObject);
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

    protected void gvSOH_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvSOH_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblRemarksSoh = (Label)e.Row.FindControl("lblRemarksSoh");
            if (lblRemarksSoh.Text != null && lblRemarksSoh.Text != "")
            {
                lblRemarksSoh.Text = HttpUtility.HtmlDecode(lblRemarksSoh.Text);
            }

        }
    }

    protected void GvSohDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GvSohDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPetitioner = (Label)e.Row.FindControl("lblPetitioner");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject4");
            Label lblRemarksSohDetails = (Label)e.Row.FindControl("lblRemarksSohDetails");


            if (lblPetitioner.Text != null && lblPetitioner.Text != "")
            {
                lblPetitioner.Text = HttpUtility.HtmlDecode(lblPetitioner.Text);
                // GvSohDetails.Columns[3].Visible = true;
            }
            else
            {
                // GvSohDetails.Columns[3].Visible = false;
            }
            if (lblRespondent.Text != null && lblRespondent.Text != "")
            {
                lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
                //GvSohDetails.Columns[4].Visible = true;
            }
            else
            {
                //GvSohDetails.Columns[4].Visible = false;
            }
            if (lblSubject.Text != null && lblSubject.Text != "")
            {
                lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            }
            if (lblRemarksSohDetails.Text != null && lblRemarksSohDetails.Text != "")
            {
                lblRemarksSohDetails.Text = HttpUtility.HtmlDecode(lblRemarksSohDetails.Text);
                GvSohDetails.Columns[7].Visible = true;
            }
            else
            {
                GvSohDetails.Columns[7].Visible = false;
            }
        }
    }

    #region function to display Review petition

    public void BindReviewPetitionDetails(string RPId)
    {

        pnlreviewPetition.Visible = true;
        petitionObject.RPId = Convert.ToInt16(RPId);
        petitionObject.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        p_Var.dSetChildData = petPetitionBL.get_ReviewPetitionForPopup(petitionObject);

        if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
        {
            GvReviewPetition.DataSource = p_Var.dSetChildData;
            GvReviewPetition.DataBind();
        }
        // DisplayPetitionDetails(p_Var.dSetChildData.Tables[0].Rows[0]["Petition_Id"].ToString());
        DisplayDetails(Convert.ToInt16(p_Var.dSetChildData.Tables[0].Rows[0]["Petition_Id"].ToString()));
    }

    #endregion



    #region function to display Details

    public void DisplayPetitionDetails(string PetitionId)
    {
        PnlModule.Visible = false;
        petitionObject.TempPetitionId = Convert.ToInt16(PetitionId);
        p_Var.dSet = petPetitionBL.get_Petition_Details(petitionObject);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            gvReview1.DataSource = p_Var.dSet;
            gvReview1.DataBind();
        }
    }

    #endregion



    protected void GvReviewPetition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //filename

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            petitionObject.RPId = Convert.ToInt16(GvReviewPetition.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = petPetitionBL.getReviewPetitionFileNames(petitionObject);

            Label lblApplicant = (Label)e.Row.FindControl("lblApplicant");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            Label lblSubject2 = (Label)e.Row.FindControl("lblSubject2");

            lblApplicant.Text = HttpUtility.HtmlDecode(lblApplicant.Text);
            lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
            lblSubject2.Text = HttpUtility.HtmlDecode(lblSubject2.Text);

            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Var.sbuilder.Append("<a href='" + p_Var.FilenameUrl + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document'>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Comment"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comment"].ToString() != "")
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comment"] + ",  ");
                    }

                    //p_Var.sbuilder.Append("<a href='" + p_Var.FilenameUrl + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                    p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Var.FilenameUrl) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(p_Var.FilenameUrl) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }

                    p_Var.sbuilder.Append("<br/><hr/>");

                }
                p_Var.sbuilder.Append("<a/>");
                orderConnectedFile.Text = p_Var.sbuilder.ToString();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            }
            else
            {
                // gvPetition.Columns[8].Visible = false;
            }
            //End



            //connected orders by ruchi on date 7 march 2013
            p_Var.dsFileName = null;
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            Literal RevieworderConnectedFile1 = (Literal)e.Row.FindControl("ltrlReviewConnectedFile1");
            Label lblPetitionID1 = (Label)e.Row.FindControl("lblPetitionID");
            petitionObject.RPNo = lblPetitionID1.Text;
            p_Var.dsFileName = petPetitionBL.getConnectedOrdersForReview(petitionObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    //p_Var.sbuilder.Append("<a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "' target='_blank'>" + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                    //p_Var.sbuilder.Append("<asp:label >" + p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] + "," + p_Var.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + "," + p_Var.dsFileName.Tables[0].Rows[i]["Date"] + "</Label>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value)
                    {
                        p_Var.sbuilder.Append("<asp:label >" + p_Var.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + ", " + p_Var.dsFileName.Tables[0].Rows[i]["Date"] + " " + "</Label>");
                    }
                    else
                    {
                        p_Var.sbuilder.Append("<asp:label >" + p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] + "," + p_Var.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + " " + "</Label>");
                    }
                    p_Var.sbuilder.Append("<a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");


                    if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {

                            FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }
                    p_Var.sbuilder.Append("</a><br/><hr/>");

                    GvReviewPetition.Columns[7].Visible = true;
                }
                RevieworderConnectedFile1.Text = p_Var.sbuilder.ToString();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            }
            else
            {
                GvReviewPetition.Columns[7].Visible = false;
            }
            //End



            // connected public notice for review petition
            HiddenField hidPublicNoticeRv = (HiddenField)e.Row.FindControl("hidPublicNoticeRv");
            LinkButton lnkConnectedPublicNoticeRv = (LinkButton)e.Row.FindControl("lnkConnectedPublicNoticeRv");
            Label lblPublicNoticeRv = (Label)e.Row.FindControl("lblPublicNoticeRv");
            if (hidPublicNoticeRv.Value != null && hidPublicNoticeRv.Value != "")
            {
                lblPublicNoticeRv.Visible = false;
                lnkConnectedPublicNoticeRv.Visible = true;
                lnkConnectedPublicNoticeRv.Text = "Yes";
            }
            else
            {
                lnkConnectedPublicNoticeRv.Visible = false;
                lblPublicNoticeRv.Visible = true;
            }



            // by ruchi on date 7 march 2013
            Label lblRvConnectedSoh_ID = (Label)e.Row.FindControl("lblRvConnectedSoh_ID");
            LinkButton lnkRvConnectedSoh = (LinkButton)e.Row.FindControl("lnkRvConnectedSoh");


            objPet.RPId = Convert.ToInt16(lblPetitionID1.Text);
            p_Var.dSetCompare = petPetitionBL.getReviewConnectedSOH(objPet);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {

                lnkRvConnectedSoh.Visible = true;
                lblRvConnectedSoh_ID.Visible = false;
                lnkRvConnectedSoh.Text = "Yes";

            }
            else
            {
                lnkRvConnectedSoh.Visible = false;
                lblRvConnectedSoh_ID.Visible = true;

            }
        }
    }


    protected void gvReview1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            string filename = DataBinder.Eval(e.Row.DataItem, "Petition_File").ToString();

            Label lblSubject3 = (Label)e.Row.FindControl("lblSubject3");
            lblSubject3.Text = HttpUtility.HtmlDecode(lblSubject3.Text);
            if (filename == null || filename == "")
            {
                // lnk.Visible = false;
            }


            //connected Petition

            Literal orderConnectedFile1 = (Literal)e.Row.FindControl("ltrlConnectedFile1");
            petitionObject.PetitionId = Convert.ToInt16(gvReview1.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = petPetitionBL.getPetitionFileNames(petitionObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {

                    p_Var.sbuilder.Append("<a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                    p_Var.sbuilder.Append("<asp:label >" + p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + "," + p_Var.dsFileName.Tables[0].Rows[i]["Date"] + "</Label>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }
                    p_Var.sbuilder.Append("<br/><hr/>");

                }
                orderConnectedFile1.Text = p_Var.sbuilder.ToString();

            }
            else
            {
                // gvPetition.Columns[8].Visible = false;
            }
            //End


        }
    }




    #region Function to bind gridView with petitions

    public void Bind_GridConnected(int Connectedpetitionid)
    {
        p_Var.dSet = null;
        petitionObject.PetitionId = Connectedpetitionid;
        //petitionObject.ConnectionID = Convert.ToInt16(Session["connection_ID"]);
        p_Var.dSet = petPetitionBL.getConnectedForPetition(petitionObject);

        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            pPetitionGridDetatils.Visible = true;
            gvPetition.DataSource = p_Var.dSet;
            gvPetition.DataBind();
            p_Var.dSet = null;

        }
        else
        {
            pPetitionGridDetatils.Visible = false;
        }

    }

    #endregion

    public void Bind_SOH_Review(string Id)
    {
        p_Var.dSetChildData = null;
        PnlReviewSoh.Visible = true;
        petitionObject.RPId = Convert.ToInt16(Id);
        p_Var.dSetChildData = petPetitionBL.getReviewConnectedSOH(petitionObject);
        if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
        {
            //Dreview.Visible = true;
            gvSOHReview.DataSource = p_Var.dSetChildData;
            gvSOHReview.DataBind();
        }
    }


    protected void gvSOHReview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["scheduleofHearing"] + "/";
            //LinkButton lnk = (LinkButton)e.Row.FindControl("lblViewDoc");
            //string filename = DataBinder.Eval(e.Row.DataItem, "soh_file").ToString();
            string temID = Convert.ToString(e.Row.Cells[1].Text);
            string year = Convert.ToString(e.Row.Cells[2].Text);
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);

            temID = "HERC/RA-" + temID + " of " + year;
            e.Row.Cells[1].Text = temID;

            Literal ltrlFileReviewConnected = (Literal)e.Row.FindControl("ltrlFileReviewConnected");
            petitionObject.soh_ID = Convert.ToInt16(gvSOHReview.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dSetCompare = petPetitionBL.getFileNameForSoh(petitionObject);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dSetCompare.Tables[0].Rows.Count; i++)
                {
                    if (p_Var.dSetCompare.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dSetCompare.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        p_Var.sbuilder.Append(p_Var.dSetCompare.Tables[0].Rows[i]["Comments"] + ",  ");
                    }

                    p_Var.sbuilder.Append("<a href='" + Url + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");

                    if (p_Var.dSetCompare.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(Url) + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"]))
                        {

                            FileInfo finfo = new FileInfo(Server.MapPath(Url) + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }

                    p_Var.sbuilder.Append("</a><br/><hr/>");

                }
                ltrlFileReviewConnected.Text = p_Var.sbuilder.ToString();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

                DataSet dSet1 = new DataSet();

                dSet1 = petPetitionBL.getScheduleOfHearingURLwithFile(petitionObject);
                if (dSet1.Tables[0].Rows.Count > 0)
                {

                    SOHFiles.Visible = true;
                    //loop through cells in that row
                    string strUrl = dSet1.Tables[0].Rows[0]["placeholderTwo"].ToString();
                    string[] split = strUrl.Split(';');
                    ArrayList list = new ArrayList();

                    //loop through cells in that row
                    string strUrl1 = dSet1.Tables[0].Rows[0]["placeholderOne"].ToString();

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



                    //string concatsplit;
                    StringBuilder ctbbld = new StringBuilder();
                    for (int i = 0; i < split1.Count(); i++)
                    {
                        //concatsplit = split[i].ToString() + " " + split1[i].ToString();

                        ctbbld.Append(" <a href='" + split1[i].ToString() + "'  target='_blank' title='Click Here to Go" + "&nbsp;" + "&nbsp;" + split1[i].ToString() + "'>");
                        ctbbld.Append(split[i].ToString());
                        ctbbld.Append("</a><br/>");



                    }
                    ltrlFileReviewConnected.Text += ctbbld.ToString();
                }
            }
        }
    }

    protected void gvSOHReview_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GvReviewPetition_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        //7 march by ruchi 
        if (e.CommandName == "RvconnectedSoh")
        {
            string RvconnectedSoh = e.CommandArgument.ToString();
            Bind_SOH_Review(RvconnectedSoh);
        }

        if (e.CommandName == "connectedPublicRv")
        {
            string id = e.CommandArgument.ToString();
            ConnectedPublicNoticeDetailsForReviewPetition(id);
        }
    }

    protected void grdAppealDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Judgement_Pronounced"] + "/");

        if (e.CommandName == "ViewDocAppealetails")
        {
            // Bind_Appeal(1);
            string file = e.CommandArgument.ToString();
            Path = Path + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(Path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(Path));
                Response.Clear();
                Response.WriteFile(Path);
                Response.End();
            }
        }

        if (e.CommandName == "connectedReviewAppeal")
        {
            string PA_Id = e.CommandArgument.ToString();
            Bind_GridReviewAppeal(PA_Id);
        }
    }

    protected void grdAppealDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            HyperLink Hypjudgement = (HyperLink)e.Row.FindControl("Hypjudgement");
            string Description = DataBinder.Eval(e.Row.DataItem, "JudgementDesc").ToString();
            HtmlImage img1 = (HtmlImage)e.Row.FindControl("img1");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            if (lblRemarks.Text != null && lblRemarks.Text != "")
            {
                lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
                grdAppealDetails.Columns[12].Visible = true;
            }
            else
            {
                grdAppealDetails.Columns[12].Visible = false;
            }

            if (Description != null && Description != "")
            {
                if (Hypjudgement.Text != "" && Hypjudgement.Text != null)
                {
                    Hypjudgement.Text = " Judgement";
                    img1.Visible = true;
                    img1.Src = ResolveUrl("~/images/external.png");
                    grdAppealDetails.Columns[10].Visible = true;// This is for judgement link column
                }
                else
                {
                    Hypjudgement.Text = "";
                    img1.Visible = false;
                    grdAppealDetails.Columns[10].Visible = false;// This is for judgement link column
                }


            }
            else
            {
                if (Hypjudgement.Text != "" && Hypjudgement.Text != null)
                {
                    img1.Visible = true;
                    Hypjudgement.Text = "Judgement";
                    img1.Src = ResolveUrl("~/images/external.png");
                    grdAppealDetails.Columns[10].Visible = true; // This is for judgement link column
                }
                else
                {
                    img1.Visible = false;
                    grdAppealDetails.Columns[10].Visible = false; // This is for judgement link column
                }

            }





            Literal ltrlConnectedAppealfiles = (Literal)e.Row.FindControl("ltrlConnectedAppealfiles");
            petitionObject.AppealId = Convert.ToInt16(grdAppealDetails.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = petPetitionBL.getAppealFileNames(petitionObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                grdAppealDetails.Columns[11].Visible = true;
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Var.sbuilder.Append(" <a href='" + p_Var.FilenameUrl + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document'>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ",  ");
                    }

                    //   p_Var.sbuilder.Append(" <a href='" + p_Var.FilenameUrl + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>"  + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                    p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");

                    if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Var.FilenameUrl) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {

                            FileInfo finfo = new FileInfo(Server.MapPath(p_Var.FilenameUrl) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }

                    p_Var.sbuilder.Append("<br/><hr/>");

                }
                p_Var.sbuilder.Append("<a/>");
                ltrlConnectedAppealfiles.Text = p_Var.sbuilder.ToString();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            }
            else
            {
                grdAppealDetails.Columns[11].Visible = false;
            }



            Label lblApplicantAppeal = (Label)e.Row.FindControl("lblApplicantAppeal");
            Label lblRespondentAppeal = (Label)e.Row.FindControl("lblRespondentAppeal");
            Label lblSubjectAppeal = (Label)e.Row.FindControl("lblSubjectAppeal");
            if (lblApplicantAppeal.Text != null && lblApplicantAppeal.Text != "")
            {
                lblApplicantAppeal.Text = HttpUtility.HtmlDecode(lblApplicantAppeal.Text);
                grdAppealDetails.Columns[4].Visible = true;
            }
            else
            {
                grdAppealDetails.Columns[4].Visible = false;
            }
            if (lblRespondentAppeal.Text != null && lblRespondentAppeal.Text != "")
            {
                lblRespondentAppeal.Text = HttpUtility.HtmlDecode(lblRespondentAppeal.Text);
                grdAppealDetails.Columns[5].Visible = true;
            }
            else
            {
                grdAppealDetails.Columns[5].Visible = false;
            }

            if (lblSubjectAppeal.Text != null && lblSubjectAppeal.Text != "")
            {
                lblSubjectAppeal.Text = HttpUtility.HtmlDecode(lblSubjectAppeal.Text);
                grdAppealDetails.Columns[6].Visible = true;
            }
            else
            {
                grdAppealDetails.Columns[6].Visible = false;
            }

        }
    }
    public void BindAppealPetitionDetails(string Id)
    {
        p_Var.dSetChildData = null;
        PnlAppealDetails.Visible = true;
        PnlModule.Visible = false;
        petitionObject.PAId = Convert.ToInt16(Id);
        p_Var.dSetChildData = petPetitionBL.Get_CurrentPetitionAppealDetails(petitionObject);
        if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
        {

            //Dreview.Visible = true;
            grdAppealDetails.DataSource = p_Var.dSetChildData;
            grdAppealDetails.DataBind();

            if (p_Var.dSetChildData.Tables[0].Rows[0]["RP_ID"] != DBNull.Value && p_Var.dSetChildData.Tables[0].Rows[0]["RP_ID"] != "")
            {
                BindReview(p_Var.dSetChildData.Tables[0].Rows[0]["RP_ID"].ToString());
            }

            BindDataOrderAppeal(); // 8 sep 2013

        }
    }

    protected void GrdReviewConnected_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //connected Petition
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            Literal orderConnectedReviewFile = (Literal)e.Row.FindControl("ltrlrEVIEWConnected");
            objPet.RPId = Convert.ToInt16(GrdReviewConnected.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dSetCompare = petPetitionBL.getReviewPetitionFileNames(objPet);

            Label lblApplicant = (Label)e.Row.FindControl("lblApplicant");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            Label lblSubject5 = (Label)e.Row.FindControl("lblSubject5");

            lblApplicant.Text = HttpUtility.HtmlDecode(lblApplicant.Text);
            lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
            lblSubject5.Text = HttpUtility.HtmlDecode(lblSubject5.Text);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dSetCompare.Tables[0].Rows.Count; i++)
                {
                    if (p_Var.dSetCompare.Tables[0].Rows[i]["Comment"] != DBNull.Value && p_Var.dSetCompare.Tables[0].Rows[i]["Comment"].ToString() != "")
                    {
                        p_Var.sbuilder.Append(p_Var.dSetCompare.Tables[0].Rows[i]["Comment"] + ",  ");
                    }

                    p_Var.sbuilder.Append("  <a href='" + p_Var.FilenameUrl + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");

                    if (p_Var.dSetCompare.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Var.FilenameUrl) + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"]))
                        {

                            FileInfo finfo = new FileInfo(Server.MapPath(p_Var.FilenameUrl) + p_Var.dSetCompare.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }

                    p_Var.sbuilder.Append("</a><br/><hr/>");

                }
                orderConnectedReviewFile.Text = p_Var.sbuilder.ToString();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            }
            else
            {

            }
            //End
        }
    }



    //5 aug 2013


    protected void gvConnectedPetitionWithPetition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDoc")
        {
            string path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/" + ConfigurationManager.AppSettings["Petition"] + "/");
            string file = e.CommandArgument.ToString();
            path = path + file;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
            if (fileInfo.Exists)
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(path));
                Response.Clear();
                Response.WriteFile(path);
                Response.End();
            }


        }
    }
    protected void gvConnectedPetitionWithPetition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            string filename = DataBinder.Eval(e.Row.DataItem, "Petition_File").ToString();

            Label lblPetitionerName = (Label)e.Row.FindControl("lblPetitionerName");
            Label lblRespondentName = (Label)e.Row.FindControl("lblRespondentName");
            Label lblSubject6 = (Label)e.Row.FindControl("lblSubject6");

            lblPetitionerName.Text = HttpUtility.HtmlDecode(lblPetitionerName.Text);
            lblRespondentName.Text = HttpUtility.HtmlDecode(lblRespondentName.Text);
            lblSubject6.Text = HttpUtility.HtmlDecode(lblSubject6.Text);
            //connected Petition

            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            Literal orderConnectedFile2 = (Literal)e.Row.FindControl("ltrlConnectedFile2");
            petitionObject.PetitionId = Convert.ToInt16(gvConnectedPetitionWithPetition.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = petPetitionBL.getPetitionFileNames(petitionObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ",  ");
                    }


                    p_Var.sbuilder.Append(" <a href='" + p_Var.Path + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");

                    if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Var.Path) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {

                            FileInfo finfo = new FileInfo(Server.MapPath(p_Var.Path) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }
                    p_Var.sbuilder.Append("</a><br/><hr/>");

                }
                orderConnectedFile2.Text = p_Var.sbuilder.ToString();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            }
            else
            {

            }
            //End


        }
    }

    //End



    protected void grdAppealReview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //filename

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            petitionObject.RPId = Convert.ToInt16(grdAppealReview.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = petPetitionBL.getReviewPetitionFileNames(petitionObject);

            Label lblApplicant = (Label)e.Row.FindControl("lblApplicant");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            Label lblSubject2 = (Label)e.Row.FindControl("lblSubject2");

            lblApplicant.Text = HttpUtility.HtmlDecode(lblApplicant.Text);
            lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
            lblSubject2.Text = HttpUtility.HtmlDecode(lblSubject2.Text);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    if (p_Var.dsFileName.Tables[0].Rows[i]["Comment"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comment"].ToString() != "")
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comment"] + ",  ");
                    }
                    //p_Var.sbuilder.Append(" Dated: " + p_Var.dsFileName.Tables[0].Rows[i]["Date"] + " ");
                    p_Var.sbuilder.Append("<a href='" + p_Var.FilenameUrl + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");

                    if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Var.FilenameUrl) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(p_Var.FilenameUrl) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }
                    p_Var.sbuilder.Append("</a><br/><hr/>");

                }
                orderConnectedFile.Text = p_Var.sbuilder.ToString();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            }
            else
            {
                // gvPetition.Columns[8].Visible = false;
            }
            //End

            //connected orders by ruchi on date 7 march 2013
            p_Var.dsFileName = null;
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            Literal RevieworderConnectedFile1 = (Literal)e.Row.FindControl("ltrlReviewConnectedFile1");
            Label lblPetitionID1 = (Label)e.Row.FindControl("lblPetitionID");
            petitionObject.RPNo = lblPetitionID1.Text;
            p_Var.dsFileName = petPetitionBL.getConnectedOrdersForReview(petitionObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    //p_Var.sbuilder.Append("<a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "' target='_blank'>" + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                    //p_Var.sbuilder.Append("<asp:label >" + p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] + "," + p_Var.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + "," + p_Var.dsFileName.Tables[0].Rows[i]["Date"] + "</Label>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value)
                    {
                        p_Var.sbuilder.Append("<asp:label >" + p_Var.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + ", " + p_Var.dsFileName.Tables[0].Rows[i]["Date"] + " " + "</Label>");
                    }
                    else
                    {
                        p_Var.sbuilder.Append("<asp:label >" + p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] + "," + p_Var.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + " " + "</Label>");
                    }
                    p_Var.sbuilder.Append("<a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");

                    if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                    {
                        if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                        {
                            FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                            double FileInBytes = finfo.Length;
                            p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                        }
                    }

                    p_Var.sbuilder.Append("</a><br/><hr/>");

                    GvReviewPetition.Columns[7].Visible = true;
                }
                RevieworderConnectedFile1.Text = p_Var.sbuilder.ToString();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            }
            else
            {
                GvReviewPetition.Columns[7].Visible = false;
            }
            //End

            Label lblRvConnectedSoh_ID = (Label)e.Row.FindControl("lblRvConnectedSoh_ID");
            LinkButton lnkRvConnectedSoh = (LinkButton)e.Row.FindControl("lnkRvConnectedSoh");


            objPet.RPId = Convert.ToInt16(lblPetitionID1.Text);
            p_Var.dSetCompare = petPetitionBL.getReviewConnectedSOH(objPet);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {

                lnkRvConnectedSoh.Visible = true;
                lblRvConnectedSoh_ID.Visible = false;
                lnkRvConnectedSoh.Text = "Yes";

            }
            else
            {
                lnkRvConnectedSoh.Visible = false;
                lblRvConnectedSoh_ID.Visible = true;

            }
        }
    }

    protected void grdAppealReview_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        //7 march by ruchi 
        if (e.CommandName == "RvconnectedSoh")
        {
            string RvconnectedSoh = e.CommandArgument.ToString();
            Bind_SOH_Review(RvconnectedSoh);
        }

        if (e.CommandName == "connectedPublicRv")
        {
            string id = e.CommandArgument.ToString();
            ConnectedPublicNoticeDetailsForReviewPetition(id);
        }
    }

    #region Function to bind gridView with Review Appeal

    public void Bind_GridReviewAppeal(string PA_Id)
    {
        petitionObject.PAId = Convert.ToInt16(PA_Id);
        p_Var.dsFileName = objapepalBL.get_ConnectedReviewAppeal(petitionObject);

        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
        {
            PAppealReview.Visible = true;
            grdAppealReview.DataSource = p_Var.dsFileName;
            grdAppealReview.DataBind();
            p_Var.dsFileName = null;

        }
        else
        {
            PAppealReview.Visible = false;
        }

    }

    #endregion


    #region Connecred Review Petition Order Date Bind with table

    public void BindDataOrder()
    {

        p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
        petitionObject.RPNo = Request.QueryString["RPID"].ToString();
        p_Var.dsFileName = petPetitionBL.getOrdersForReview(petitionObject);
        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
        {
            TblConnectedOrderWithREview.Visible = true;
            p_Var.id = p_Var.dsFileName.Tables[0].Rows[0]["OrderId"].ToString();
            for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
            {
                // p_Var.sbuilder.Append("<asp:LinkButton href='' onclick=\'ConnectedOrder_Click()\' >");
                p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + ", ");
                p_Var.sbuilder.Append("Dated: " + p_Var.dsFileName.Tables[0].Rows[i]["Date"] + " ,");
                p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Description"]);
                //p_Var.sbuilder.Append("</a>");

            }
            lnkReviewPetitionConnectedOrder.Text = p_Var.sbuilder.ToString();
            lnkReviewPetitionConnectedOrder.CommandArgument = p_Var.id;

        }
        else
        {
            gvReview.Caption = "REVIEW PETITION DETAILS";
            //gvReview.Columns[7].Visible = false;
        }
    }

    #endregion



    //This is for to connect order 6 Sep 2013

    protected void gvDailyOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            Label lbl = (Label)e.Row.FindControl("lblsubject");
            HiddenField hydSubject = (HiddenField)e.Row.FindControl("hydSubject");
            string filename = DataBinder.Eval(e.Row.DataItem, "orderFile").ToString();

            Label lblPetitioner = (Label)e.Row.FindControl("lblPetitioner");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");
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

            if (lbl.Text == "" || lbl.Text == null)
            {
                lbl.Text = hydSubject.Value;
            }
            else
            {
                lbl.Text = lbl.Text;
            }

            if (filename == null || filename == "")
            {
                // lnk.Visible = false;
            }

            //Code modified by birendra on date 28-02-2013 in chandigarh

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            petitionObject.OrderID = Convert.ToInt16(gvDailyOrders.DataKeys[e.Row.RowIndex].Value.ToString());

            p_Var.dsFileName = objOrdersBL.getConnectedOrders(petitionObject);
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Var.sbuilder.Append("<a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' alt='View Document'>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value)
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + ", ");

                        if (p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] != DBNull.Value)
                        {
                            p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] + ", ");
                        }
                        if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                        {
                            p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        }

                        p_Var.sbuilder.Append(" Dated: " + p_Var.dsFileName.Tables[0].Rows[i]["Date"]);
                        // p_Var.sbuilder.Append("<br /><a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                        p_Var.sbuilder.Append("<br />" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");
                        if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {
                                FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }
                        p_Var.sbuilder.Append("<br/><hr/>");

                    }
                    else
                    {
                        //This is for amendment/clarification etc
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        //p_Var.sbuilder.Append("<br /><a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                        p_Var.sbuilder.Append("<br />" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");

                        if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {
                                FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }
                        p_Var.sbuilder.Append("<br/><hr/>");
                    }
                }
                p_Var.sbuilder.Append("<a/>");
                orderConnectedFile.Text = p_Var.sbuilder.ToString();

            }
            //End
        }
    }




    #region function to bind order details

    public void bindOrders(int OrderId)
    {
        petitionObject.OrderID = OrderId;
        p_Var.dSet = objOrdersBL.get_OrderByIdDetails(petitionObject);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            gvDailyOrders.DataSource = p_Var.dSet;
            gvDailyOrders.DataBind();

            Label lblNumber = gvDailyOrders.Rows[0].FindControl("lblnumber") as Label;

            if (p_Var.dSet.Tables[0].Rows[0]["PRO_No"] == DBNull.Value)
            {
                lblNumber.Text = p_Var.dSet.Tables[0].Rows[0]["rp_no"].ToString();

            }
            else
            {
                lblNumber.Text = p_Var.dSet.Tables[0].Rows[0]["PRO_No"].ToString();

            }
        }

    }
    #endregion
    protected void lnkReviewPetitionConnectedOrder_Click(object sender, EventArgs e)
    {

        bindOrders(Convert.ToInt16(lnkReviewPetitionConnectedOrder.CommandArgument));
        lnkReviewPetitionConnectedOrder.Enabled = false;
    }

    protected void LnkconnectedorderAppeal_Click(object sender, EventArgs e)
    {
        LnkconnectedorderAppeal.Enabled = false;
        bindOrdersAppeal(Convert.ToInt16(LnkconnectedorderAppeal.CommandArgument));
    }




    // This function is for coonected Appeal Order

    #region function to bind order details

    public void bindOrdersAppeal(int OrderId)
    {
        petitionObject.OrderID = OrderId;
        p_Var.dSet = objOrdersBL.get_OrderByIdDetails(petitionObject);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            PnlAppealOrderconnected.Visible = true;
            GvAppealConnectedorder.DataSource = p_Var.dSet;
            GvAppealConnectedorder.DataBind();

            Label lblNumber = GvAppealConnectedorder.Rows[0].FindControl("lblnumberAppeal") as Label;

            if (p_Var.dSet.Tables[0].Rows[0]["PRO_No"] == DBNull.Value)
            {
                lblNumber.Text = p_Var.dSet.Tables[0].Rows[0]["rp_no"].ToString();

            }
            else
            {
                lblNumber.Text = p_Var.dSet.Tables[0].Rows[0]["PRO_No"].ToString();

            }
        }

    }
    #endregion


    //This is for to connect order 6 Sep 2013

    protected void GvAppealConnectedorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFileAppeal");
            Label lbl = (Label)e.Row.FindControl("lblsubjectAppeal");
            HiddenField hydSubject = (HiddenField)e.Row.FindControl("hydSubjectAppeal");
            string filename = DataBinder.Eval(e.Row.DataItem, "orderFile").ToString();

            Label lblPetitionerAppeal = (Label)e.Row.FindControl("lblPetitionerAppeal");
            Label lblRespondentAppeal = (Label)e.Row.FindControl("lblRespondentAppeal");
            Label lblSubjectAppeal = (Label)e.Row.FindControl("lblsubjectAppeal");
            if (lblPetitionerAppeal.Text != null && lblPetitionerAppeal.Text != "")
            {
                lblPetitionerAppeal.Text = HttpUtility.HtmlDecode(lblPetitionerAppeal.Text);
            }
            if (lblRespondentAppeal.Text != null && lblRespondentAppeal.Text != "")
            {
                lblRespondentAppeal.Text = HttpUtility.HtmlDecode(lblRespondentAppeal.Text);
            }
            if (lblSubjectAppeal.Text != null && lblSubjectAppeal.Text != "")
            {
                lblSubjectAppeal.Text = HttpUtility.HtmlDecode(lblSubjectAppeal.Text);
            }

            if (lbl.Text == "" || lbl.Text == null)
            {
                lbl.Text = HttpUtility.HtmlDecode(hydSubject.Value);
            }
            else
            {
                lbl.Text = lbl.Text;
            }

            if (filename == null || filename == "")
            {
                // lnk.Visible = false;
            }

            //Code modified by birendra on date 28-02-2013 in chandigarh

            Literal orderConnectedFileAppeal = (Literal)e.Row.FindControl("ltrlConnectedFileAppeal");
            petitionObject.OrderID = Convert.ToInt16(GvAppealConnectedorder.DataKeys[e.Row.RowIndex].Value.ToString());

            p_Var.dsFileName = objOrdersBL.getConnectedOrders(petitionObject);
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Var.sbuilder.Append("<br /><a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document'>");
                    if (p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value)
                    {
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + ", ");

                        if (p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] != DBNull.Value)
                        {
                            p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["SubCategoryName"] + ", ");
                        }
                        if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                        {
                            p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        }

                        p_Var.sbuilder.Append(" Dated: " + p_Var.dsFileName.Tables[0].Rows[i]["Date"]);
                        //p_Var.sbuilder.Append("<br /><a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                        p_Var.sbuilder.Append("<br />" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");

                        if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {
                                FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }

                        p_Var.sbuilder.Append("<br/><hr/>");

                    }
                    else
                    {
                        //This is for amendment/clarification etc
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        //p_Var.sbuilder.Append("<br /><a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                        p_Var.sbuilder.Append("<br />" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />");

                        if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {
                                FileInfo finfo = new FileInfo(Server.MapPath(p_Var.url) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }

                        p_Var.sbuilder.Append("<br/><hr/>");
                    }
                }
                p_Var.sbuilder.Append("<a/>");
                orderConnectedFileAppeal.Text = p_Var.sbuilder.ToString();

            }
            //End
        }
    }





    #region Connecred Review Petition Order Date Bind with table

    public void BindDataOrderAppeal()
    {

        p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
        petitionObject.RPNo = Request.QueryString["PDID"].ToString();
        p_Var.dSetCompare = petPetitionBL.getOrdersForAppeal(petitionObject);
        if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
        {
            TblAppealOrder.Visible = true;
            PnlAppealDetails.Visible = true;
            p_Var.id = p_Var.dSetCompare.Tables[0].Rows[0]["OrderId"].ToString();
            for (int i = 0; i < p_Var.dSetCompare.Tables[0].Rows.Count; i++)
            {

                p_Var.sbuilder.Append(p_Var.dSetCompare.Tables[0].Rows[i]["OrdertypeName"] + ", ");
                p_Var.sbuilder.Append("Dated: " + p_Var.dSetCompare.Tables[0].Rows[i]["Date"] + " ,");
                p_Var.sbuilder.Append(HttpUtility.HtmlDecode(p_Var.dSetCompare.Tables[0].Rows[i]["Description"].ToString()));


            }
            LnkconnectedorderAppeal.Text = p_Var.sbuilder.ToString();
            LnkconnectedorderAppeal.CommandArgument = p_Var.id;

        }
        else
        {
            grdAppealDetails.Caption = "APPEAL DETAILS";
            //gvReview.Columns[7].Visible = false;
        }
    }

    #endregion



    #region Function to bind What's new

    public void BindWhatsNew()
    {
        try
        {
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            objwhatsOB.AwardId = Convert.ToInt16(Request.QueryString["WhatsNewId"]);
            p_Var.dSetCompare = objNewBL.GetWhatsNewById(objwhatsOB);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {
                pnlWhatsNew.Visible = true;
                lblWhatsnewTitle.Text = p_Var.dSetCompare.Tables[0].Rows[0]["News_TITLE"].ToString();
                lblWhatsnewDesc.Text = p_Var.dSetCompare.Tables[0].Rows[0]["NewsDecription"].ToString();


                if (p_Var.dSetCompare.Tables[0].Rows[0]["FileName"] != null && p_Var.dSetCompare.Tables[0].Rows[0]["FileName"].ToString() != "")
                {
                    trfile.Visible = true;
                    p_Var.sbuilder.Append("<a href='" + p_Var.Path + p_Var.dSetCompare.Tables[0].Rows[0]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                    if (File.Exists(Server.MapPath(p_Var.Path) + p_Var.dSetCompare.Tables[0].Rows[0]["FileName"]))
                    {
                        FileInfo finfo = new FileInfo(Server.MapPath(p_Var.Path) + p_Var.dSetCompare.Tables[0].Rows[0]["FileName"]);
                        double FileInBytes = finfo.Length;
                        p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                    }
                    ltrDownload.Text = p_Var.sbuilder.ToString();
                }
                else
                {
                    trfile.Visible = false;
                }

            }
            else
            {
                pnlWhatsNew.Visible = false;
            }
        }
        catch
        {
            throw;
        }

    }

    #endregion
}
