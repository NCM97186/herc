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

public partial class DetailsPage : System.Web.UI.Page
{

    #region variable declaration zone
    PetitionBL petPetitionBL = new PetitionBL();
    PetitionOB petitionObject = new PetitionOB();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    LinkOB obj_linkOB = new LinkOB();
    LinkBL obj_linkBL = new LinkBL();
    Miscelleneous_DL obj_Miscel = new Miscelleneous_DL();
    PublicNoticeBL objPublic = new PublicNoticeBL();
    OrderBL obj_OrderBL = new OrderBL();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.urlname = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Orders"].ToString() + "/";
        p_Var.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Petition"].ToString() + "/" + ConfigurationManager.AppSettings["Public_Notice"].ToString() + "/";
        try
        {
            if (Request.QueryString["PulicNoticeId"] != null)
            {

                BindPublicNotice_Details();
                Bind_Details(Convert.ToInt16(Request.QueryString["PulicNoticeId"]));

            }
            else
            {
                Bind_Orders();
            }
        }
        catch { }
    }


    #region Function to display Module Details

    public void BindPublicNotice_Details()
    {
        try
        {
            if (Request.QueryString["PulicNoticeId"] != null)
            {
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                petitionObject.PublicNoticeID = Convert.ToInt32(Request.QueryString["PulicNoticeId"]);
                p_Var.dSetChildData = objPublic.Get_ConnectedpubliceNotceById(petitionObject);
                if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
                {
                    PnlPublicNotice.Visible = true;
                    lblPublicNoticeTitle.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Title"].ToString();


                    if (p_Var.dSetChildData.Tables[0].Rows[0]["Description"].ToString() != null && p_Var.dSetChildData.Tables[0].Rows[0]["Description"].ToString() != "")
                    {
                        TrDes.Visible = true;
                        lblPublicNoticeDesc.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Description"].ToString();
                    }
                    else
                    {
                        TrDes.Visible = false;
                    }

                    if (p_Var.dSetChildData.Tables[0].Rows[0]["PlaceHolderFive"].ToString() != null && p_Var.dSetChildData.Tables[0].Rows[0]["PlaceHolderFive"].ToString() != "")
                    {
                        trremarks.Visible = true;
                        lblRemarks.Text = p_Var.dSetChildData.Tables[0].Rows[0]["PlaceHolderFive"].ToString();
                    }
                    else
                    {
                        trremarks.Visible = false;
                    }
                    //lblPublicNoticeFile.Text = p_Var.dSetChildData.Tables[0].Rows[0]["PublicNotice"].ToString();


                    //connected Public notices

                    //Literal orderConnectedFile = (Literal)e.i.FindControl("ltrlConnectedFile");
                    petitionObject.PublicNoticeID = Convert.ToInt16(Request.QueryString["PulicNoticeId"].ToString());
                    p_Var.dsFileName = objPublic.getPublicNoticeFileNames(petitionObject);
                    p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                    if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
                    {
                        trFileName.Visible = true;
                        for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                        {
                            p_Var.sbuilder.Append("<a title='View Document' href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>");
                            if (p_Var.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                            {
                                p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                            }


                            // p_Var.sbuilder.Append("<a title='View Document' href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='View Document' height=\"15\" />");
                            p_Var.sbuilder.Append("<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='View Document' height=\"15\" />");
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

                        }
                        p_Var.sbuilder.Append("</a>");
                        ltrlConnectedFile.Text = p_Var.sbuilder.ToString();

                    }
                    else
                    {
                        trFileName.Visible = false;
                    }
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
                    ltrlConnectedFile.Text += ctbbld.ToString();
                }
                else
                {
                    trFileName.Visible = false;
                }

            }
        }

        catch
        {
            //throw;
        }
    }

    #endregion


    public void Bind_Details(int PublicNoticeId)
    {
        try
        {
            petitionObject.PublicNoticeID = PublicNoticeId;
            p_Var.dsFileName = objPublic.Get_ConnectedpubliceNotceByIdDetails(petitionObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                gvPubNotice.Visible = true;
                if (p_Var.dsFileName.Tables[0].Rows[0]["PRO_No"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[0]["PRO_No"].ToString() != "")
                {
                    gvPubNotice.Columns[0].HeaderText = "PRO No";
                }
                else if (p_Var.dsFileName.Tables[0].Rows[0]["RP_No"] != DBNull.Value && p_Var.dsFileName.Tables[0].Rows[0]["RP_No"].ToString() != "")
                {
                    gvPubNotice.Columns[0].HeaderText = "RA No";
                }
                else
                {
                    gvPubNotice.Visible = false;
                }

                gvPubNotice.DataSource = p_Var.dsFileName;
                gvPubNotice.DataBind();
            }
            else
            {
                gvPubNotice.Visible = false;
            }

        }
        catch { }
    }


    protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDoc")
        {


        }
    }

    protected void gvPubNotice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPetitioner = (Label)e.Row.FindControl("lblPetitionerName");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondentName");
            if (lblPetitioner.Text != null && lblPetitioner.Text != "")
            {
                lblPetitioner.Text = HttpUtility.HtmlDecode(lblPetitioner.Text);
            }
            if (lblRespondent.Text != null && lblRespondent.Text != "")
            {
                lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
            }
        }

    }

    protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            //  LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            string filename = DataBinder.Eval(e.Row.DataItem, "orderFile").ToString();

            Label lblPetitioner = (Label)e.Row.FindControl("lblPetitioner");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            if (lblRemarks.Text != null && lblRemarks.Text != "")
            {
                lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
                gvOrders.Columns[7].Visible = true;
            }
            else
            {
                gvOrders.Columns[7].Visible = false;
            }
            if (lblPetitioner.Text != null && lblPetitioner.Text != "")
            {
                lblPetitioner.Text = HttpUtility.HtmlDecode(lblPetitioner.Text);
                gvOrders.Columns[3].Visible = true;
            }
            else
            {
                gvOrders.Columns[3].Visible = false;
            }
            if (lblRespondent.Text != null && lblRespondent.Text != "")
            {
                lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
                gvOrders.Columns[4].Visible = true;
            }
            else
            {
                gvOrders.Columns[4].Visible = false;
            }

            if (lblSubject.Text != null && lblSubject.Text != "")
            {
                lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
                gvOrders.Columns[5].Visible = true;
            }
            else
            {
                gvOrders.Columns[5].Visible = false;
            }
            if (filename == null || filename == "")
            {
                // lnk.Visible = false;
            }

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            petitionObject.OrderID = Convert.ToInt16(gvOrders.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = obj_OrderBL.getConnectedOrders(petitionObject);
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    // line added on date 4 Oct 2013
                    p_Var.sbuilder.Append("<a title='View Document' href='" + p_Var.urlname + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>");
                    //End
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
                        // p_Var.sbuilder.Append("<br /><a title='View Document' href='" + p_Var.urlname + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='View Document' height=\"15\" /> ");
                        p_Var.sbuilder.Append("<br />" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='View Document' height=\"15\" /> ");

                        if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Var.urlname) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {

                                FileInfo finfo = new FileInfo(Server.MapPath(p_Var.urlname) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }
                        p_Var.sbuilder.Append("</a><br/><hr/>");

                    }
                    else
                    {
                        //This is for amendment/clarification etc
                        p_Var.sbuilder.Append(p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        //p_Var.sbuilder.Append("<br /><a title='View Document' href='" + p_Var.urlname + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" +  "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='View Document' height=\"15\" /> ");
                        p_Var.sbuilder.Append("<br />" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "'  width='15' alt='View Document' height=\"15\" /> ");

                        //  p_Var.sbuilder.Append("<br /><a href='" + p_Var.urlname + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");

                        if (p_Var.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Var.urlname) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {

                                FileInfo finfo = new FileInfo(Server.MapPath(p_Var.urlname) + p_Var.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Var.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }

                        p_Var.sbuilder.Append("</a><br/><hr/>");
                    }
                }
                p_Var.sbuilder.Append("</a>");// line added on date 4 Oct 2013
                orderConnectedFile.Text = p_Var.sbuilder.ToString();



            }

            // End
        }
    }


    #region function to bind orders in gridview

    public void Bind_Orders()
    {
        try
        {
            petitionObject.OrderID = Convert.ToInt16(Request.QueryString["OrderId"]);
            p_Var.dSetChildData = obj_OrderBL.Get_OrderDetails(petitionObject);
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {
                PGridDetails.Visible = true;
                PnlPublicNotice.Visible = false;
                gvOrders.DataSource = p_Var.dSetChildData;
                gvOrders.DataBind();

                if (Convert.ToInt16(p_Var.dSetChildData.Tables[0].Rows[0]["orderTypeId"]) == 9)
                {
                    gvOrders.Caption = "ORDER DETAILS";
                }
                else
                {
                    gvOrders.Caption = "DAILY / INTERIM ORDER DETAILS";
                }

                for (int i = 0; i < gvOrders.Rows.Count; i++)
                {
                    Label lblNumber = gvOrders.Rows[i].FindControl("lblnumber") as Label;

                    if (p_Var.dSetChildData.Tables[0].Rows[i]["pro_no"] == DBNull.Value || p_Var.dSetChildData.Tables[0].Rows[i]["pro_no"].ToString() == "")
                    {
                        if (p_Var.dSetChildData.Tables[0].Rows[i]["RP_No"] == DBNull.Value || p_Var.dSetChildData.Tables[0].Rows[i]["RP_No"].ToString() == "")
                        {
                            gvOrders.Columns[1].Visible = false;
                        }
                        else
                        {
                            lblNumber.Text = p_Var.dSetChildData.Tables[0].Rows[i]["RP_No"].ToString();
                            gvOrders.Columns[1].Visible = true;
                        }

                    }
                    else if (p_Var.dSetChildData.Tables[0].Rows[i]["RP_No"] == DBNull.Value || p_Var.dSetChildData.Tables[0].Rows[i]["RP_No"].ToString() == "")
                    {
                        lblNumber.Text = p_Var.dSetChildData.Tables[0].Rows[i]["pro_no"].ToString();
                        gvOrders.Columns[1].Visible = true;
                    }
                    else
                    {
                        gvOrders.Columns[1].Visible = false;
                    }

                }
            }
        }
        catch { }

    }

    #endregion

}
