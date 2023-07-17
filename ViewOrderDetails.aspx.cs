using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

public partial class ViewOrderDetails : System.Web.UI.Page
{

    #region variable declaration zone

    string str = string.Empty;
    Project_Variables p_Val = new Project_Variables();
    OrderBL objOrdersBL = new OrderBL();
    PetitionOB objPetOB = new PetitionOB();
    PaginationBL pagingBL = new PaginationBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    public static string UrlPrint = string.Empty;
    public string lastUpdatedDate = string.Empty;
    public string headerName = string.Empty;

    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;

    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Val.urlname = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Orders"].ToString() + "/";
        if (!IsPostBack)
        {
            bindOrders(Convert.ToInt16(Request.QueryString["OrderId"]));
        }

    }

    #region function to bind order details

    public void bindOrders(int OrderId)
    {
        objPetOB.OrderID = OrderId;
        p_Val.dSet = objOrdersBL.get_OrderByIdDetails(objPetOB);
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            

            gvDailyOrders.DataSource = p_Val.dSet;

            gvDailyOrders.DataBind();

            Label lblNumber = gvDailyOrders.Rows[0].FindControl("lblnumber") as Label;
           
         
                if (p_Val.dSet.Tables[0].Rows[0]["PRO_No"] == DBNull.Value)
                {
                    lblNumber.Text = p_Val.dSet.Tables[0].Rows[0]["rp_no"].ToString();
                    if (lblNumber.Text != null && lblNumber.Text != "")
                    {
                        gvDailyOrders.Columns[1].Visible = true;
                    }
                    else
                    {
                        gvDailyOrders.Columns[1].Visible = false;
                    }

                }
                else if (p_Val.dSet.Tables[0].Rows[0]["rp_no"] == DBNull.Value)
                {
                    lblNumber.Text = p_Val.dSet.Tables[0].Rows[0]["PRO_No"].ToString();
                    if (lblNumber.Text != null && lblNumber.Text != "")
                    {
                        gvDailyOrders.Columns[1].Visible = true;
                    }
                    else
                    {
                        gvDailyOrders.Columns[1].Visible = false;
                    }


                }
                
                
            


            if (Convert.ToInt16(p_Val.dSet.Tables[0].Rows[0]["OrderTypeID"]) == 8)
            {
                gvDailyOrders.Caption = "DAILY / INTERIM ORDER DETAILS";
            }
            else
            {
                gvDailyOrders.Caption = "ORDER DETAILS";
            }
        }

    }
    #endregion


    protected void gvDailyOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");
            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            HiddenField hdfdate = (HiddenField)e.Row.FindControl("hidDate");
            Label lbl = (Label)e.Row.FindControl("lblsubject");
            Label lblNumber = (Label)e.Row.FindControl("lblnumber");
            HiddenField hydSubject = (HiddenField)e.Row.FindControl("hydSubject");
            Literal finality=(Literal)e.Row.FindControl("ltrlFinality");
            string filename = DataBinder.Eval(e.Row.DataItem, "orderFile").ToString();

            Label lblOrderDate = (Label)e.Row.FindControl("lblOrderDate");

            Label lblOrderRemarks = (Label)e.Row.FindControl("lblOrderRemarks");
            lblOrderRemarks.Text = HttpUtility.HtmlDecode(lblOrderRemarks.Text);

            Label lblPetitioner = (Label)e.Row.FindControl("lblPetitioner");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");

            ////////if (lblNumber.Text != null && lblNumber.Text != "")
            ////////{
            ////////    gvDailyOrders.Columns[1].Visible = true;
            ////////}
            ////////else
            ////////{
            ////////    gvDailyOrders.Columns[1].Visible = false;
            ////////}


            if (lblPetitioner.Text != null && lblPetitioner.Text != "")
            {
                lblPetitioner.Text = HttpUtility.HtmlDecode(lblPetitioner.Text);
                gvDailyOrders.Columns[3].Visible = true;
            }
            else
            {
                gvDailyOrders.Columns[3].Visible = false;
            }
            if (lblRespondent.Text != null && lblRespondent.Text != "")
            {
                lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
                gvDailyOrders.Columns[4].Visible = true;
            }
            else
            {
                gvDailyOrders.Columns[4].Visible = false;
            }
            if (lblSubject.Text != null && lblSubject.Text != "")
            {
                lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
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

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            objPetOB.OrderID = Convert.ToInt16(gvDailyOrders.DataKeys[e.Row.RowIndex].Value.ToString());

            p_Val.dsFileName = objOrdersBL.getConnectedOrders(objPetOB);
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            if (lblOrderRemarks.Text != null && lblOrderRemarks.Text != "" && lblOrderRemarks.Text != "&nbsp;")
            {
                gvDailyOrders.Columns[7].Visible = true;
            }
            else
            {
                gvDailyOrders.Columns[7].Visible = false;
            }
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                gvDailyOrders.Columns[6].Visible = true;
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Val.sbuilder.Append("<a href='" + p_Val.urlname + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank' title='View Document'>");
                    if (p_Val.dsFileName.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value)
                    {
                        p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["OrdertypeName"] + ", ");

                        if (p_Val.dsFileName.Tables[0].Rows[i]["SubCategoryName"] != DBNull.Value)
                        {
                            p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["SubCategoryName"] + ", ");
                        }
                        if (p_Val.dsFileName.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Val.dsFileName.Tables[0].Rows[i]["Comments"].ToString() != "")
                        {
                            p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        }

                        p_Val.sbuilder.Append(" Dated: " + p_Val.dsFileName.Tables[0].Rows[i]["Date"]);
                        //// p_Val.sbuilder.Append("<br /><a href='" + p_Val.urlname + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                        p_Val.sbuilder.Append("<br />" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");

                        if (p_Val.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Val.urlname) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {
                                FileInfo finfo = new FileInfo(Server.MapPath(p_Val.urlname) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }

                        p_Val.sbuilder.Append("<br/><hr/>");

                    }
                    else
                    {
                        //This is for amendment/clarification etc
                        p_Val.sbuilder.Append(p_Val.dsFileName.Tables[0].Rows[i]["Comments"] + ", ");
                        ////p_Val.sbuilder.Append("<br /><a href='" + p_Val.urlname + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");

                        p_Val.sbuilder.Append("<br />" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");

                        if (p_Val.dsFileName.Tables[0].Rows[i]["FileName"] != DBNull.Value)
                        {
                            if (File.Exists(Server.MapPath(p_Val.urlname) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]))
                            {
                                FileInfo finfo = new FileInfo(Server.MapPath(p_Val.urlname) + p_Val.dsFileName.Tables[0].Rows[i]["FileName"]);
                                double FileInBytes = finfo.Length;
                                p_Val.sbuilder.Append("(" + miscellBL.fileSize(FileInBytes) + ")");
                            }
                        }
                        p_Val.sbuilder.Append("<br/><hr/>");
                    }
                }
                p_Val.sbuilder.Append("<a/>");
                orderConnectedFile.Text = p_Val.sbuilder.ToString();

            }
            else
            {
                gvDailyOrders.Columns[6].Visible = false;
            }

            //End
        }
    }
  
}