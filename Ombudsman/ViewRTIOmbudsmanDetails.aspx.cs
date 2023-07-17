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

public partial class Ombudsman_ViewRTIOmbudsmanDetails : System.Web.UI.Page
{
    #region variable declaration

    string str = string.Empty;
    Miscelleneous_DL obj_miscel = new Miscelleneous_DL();
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
    RtiBL obj_rtBL = new RtiBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    static int FAA_RTI;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Val.Path = Server.MapPath(ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Rti"] + "/");
        p_Val.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Rti"] + "/";
        p_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        p_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        FAA_RTI = Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["rtiid"]);
        if (!Page.IsPostBack)
        {      
            Bind_RTI();        
        }

        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssRef);
        }
    }

    public void BindFAA_RTI()
    {
        obj_petOB.RTIId = FAA_RTI;
        obj_petOB.LangId = Convert.ToInt32(Resources.HercResource.Lang_Id);
        p_Val.dSet = obj_rtBL.Get_FAA_RTI(obj_petOB);

        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            grdrtifAA.DataSource = p_Val.dSet;
            grdrtifAA.DataBind();
        }
    }

    protected void grdrtifAA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int RTID = Convert.ToInt32(((HiddenField)e.Row.FindControl("hyd")).Value);
            int RtistatusId = Convert.ToInt32(((HiddenField)e.Row.FindControl("status")).Value);
            obj_petOB.StatusId = 6;
            obj_petOB.RTIFAAId = RTID;
            p_Val.dSetChildData = obj_rtBL.getTempRTIFAARecordsBYID(obj_petOB);
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.replysent))
            {
                ((Label)e.Row.FindControl("lblFAAstatus")).Text = ((Label)e.Row.FindControl("lblFAAstatus")).Text + "<br/>vide " + "Memo No: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/>Dated: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo1"].ToString();
            }
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.TransferedAuthority))
            {
                ((Label)e.Row.FindControl("lblFAAstatus")).Text = ((Label)e.Row.FindControl("lblFAAstatus")).Text + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + " <br/>vide " + "Memo No: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/>Dated: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo1"].ToString();
            }
            HiddenField hyd = (HiddenField)e.Row.FindControl("hdfrefNo");
            Label lblyear = (Label)e.Row.FindControl("lblyear");
            //((Label)e.Row.FindControl("lblrefno")).Text = "HERC/RTI-" + ((Label)e.Row.FindControl("lblrefno")).Text + " of " + hyd.Value;

            ((Label)e.Row.FindControl("lblFAArefno")).Text = "EO/FAA-" + ((Label)e.Row.FindControl("lblFAArefno")).Text + " of " + lblyear.Text;
            obj_rtBL.Check_RTI_FAA_SAA(RTID, out p_Val.i);
            if (p_Val.i == 0)
            {
                grdrtifAA.Columns[7].Visible = false;
                //((LinkButton)e.Row.FindControl("lnlbtn")).Visible = false;
                //((LinkButton)e.Row.FindControl("lnlbtn")).Text = "N";
            }
            else
            {
                grdrtifAA.Columns[7].Visible = true;
                //if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.inprocess))
                //{
                //    ((LinkButton)e.Row.FindControl("lnlbtn")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnlbtn")).Text = "YES";
                //}
                //else
                //{
                //    ((LinkButton)e.Row.FindControl("lnlbtn")).Visible = true;
                //    ((LinkButton)e.Row.FindControl("lnlbtn")).Text = "YES";
                //}
            }
            if (string.IsNullOrEmpty(((HiddenField)e.Row.FindControl("hdfFile")).Value))
            {
               // ((LinkButton)e.Row.FindControl("lbl_ViewDoc")).Visible = false;
            }

            if (e.Row.Cells[8].Text == "" || e.Row.Cells[8].Text == null || e.Row.Cells[8].Text == "&nbsp;")
            {
                grdrtifAA.Columns[8].Visible = false;
            }
            else
            {
                grdrtifAA.Columns[8].Visible = true;
            }

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            if (orderConnectedFile.Text != null && orderConnectedFile.Text != "")
            {
                p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
                p_Val.sbuilder.Append("<a href='" + p_Val.url + orderConnectedFile.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                orderConnectedFile.Text = p_Val.sbuilder.ToString();
            }
        }
    }

    void Bind_FAA_SAA_GRID(int Faaid)
    {
        DataSet ds = new DataSet();
        obj_petOB.RTIFAAId = Faaid;
        ds = obj_rtBL.Get_Saa_Faa(obj_petOB);
        if (ds.Tables[0].Rows.Count > 0)
        {
            grdFAA_SAA_RTI.DataSource = ds;
            grdFAA_SAA_RTI.DataBind();
        }
    }
    protected void grdrtifAA_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdrtifAA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

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
        if (e.CommandName == "vdetail")
        {
            LinkButton lnklink = (LinkButton)gvrow.FindControl("lnlbtn");
            Literal ltrlAppealToSAA = (Literal)gvrow.FindControl("ltrlAppealToSAA");
            lnklink.Visible = false;
            ltrlAppealToSAA.Visible = true;
            Bind_FAA_SAA_GRID(Convert.ToInt32(e.CommandArgument));
        }
    }
  
    #region function declaration zone

    public void Bind_RTI()
    {
        obj_petOB.LangId = 1;
        obj_petOB.DepttId = 2; // Ombudsman Dept ID          
        obj_petOB.RTIId = Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["rtiid"]);
        p_Val.dSet = obj_rtBL.Get_RTIById(obj_petOB);

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
            int RTID = Convert.ToInt32(((HiddenField)e.Row.FindControl("hyd")).Value);
            int RtistatusId = Convert.ToInt32(((HiddenField)e.Row.FindControl("rtistatus")).Value);

           
            //use for format of Reply sent status

            obj_petOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            obj_petOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_petOB.RTIId = RTID;
            p_Val.dSetChildData = obj_rtBL.Get_RTIById(obj_petOB);
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.replysent))
            {
                ((Label)e.Row.FindControl("lblstatus")).Text = ((Label)e.Row.FindControl("lblstatus")).Text + "<br/>vide " + "Memo No: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/>Dated: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
            }
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.TransferedAuthority))
            {
                ((Label)e.Row.FindControl("lblstatus")).Text = ((Label)e.Row.FindControl("lblstatus")).Text + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + " <br/>vide " + "Memo No: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
            }
            Label lblyear = (Label)e.Row.FindControl("lblyear");
            ((Label)e.Row.FindControl("lblrefno")).Text = "EO/RTI-" + ((Label)e.Row.FindControl("lblrefno")).Text + " of " + lblyear.Text;
            obj_rtBL.Check_RTI_FAA(RTID, out p_Val.i);

            if (p_Val.i == 0)
            {
                Grdappeal.Columns[7].Visible = false;
                //((LinkButton)e.Row.FindControl("lnklink")).Visible = false;
                //((LinkButton)e.Row.FindControl("lnklink")).Text = "N";
            }
            else
            {
                Grdappeal.Columns[7].Visible = true;
                //if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.inprocess))
                //{
                //    ((LinkButton)e.Row.FindControl("lnklink")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnklink")).Text = "YES";
                //}
                //else
                //{
                //    ((LinkButton)e.Row.FindControl("lnklink")).Visible = true;
                //    ((LinkButton)e.Row.FindControl("lnklink")).Text = "YES";
                //}
            }

            if (e.Row.Cells[8].Text == "" || e.Row.Cells[8].Text == null || e.Row.Cells[8].Text == "&nbsp;")
            {
                Grdappeal.Columns[8].Visible = false;
            }
            else
            {
                Grdappeal.Columns[8].Visible = true;
            }
            if (string.IsNullOrEmpty(((HiddenField)e.Row.FindControl("hdfFile")).Value))
            {
               // ((LinkButton)e.Row.FindControl("lbl_ViewDoc")).Visible = false;
            }

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            if (orderConnectedFile.Text != null && orderConnectedFile.Text != "")
            {
                p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
                p_Val.sbuilder.Append("<a href='" + p_Val.url + orderConnectedFile.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                orderConnectedFile.Text = p_Val.sbuilder.ToString();
            }
        }
    }

    protected void Grdappeal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
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

        if (e.CommandName == "Vdetail")
        {
            LinkButton lnklink = (LinkButton)gvrow.FindControl("lnklink");
            Literal ltrlAppealToFAA = (Literal)gvrow.FindControl("ltrlAppealToFAA");
            lnklink.Visible = false;
            ltrlAppealToFAA.Visible = true;
            BindFAA_RTI();
        }
    }

    protected void grdFAA_SAA_RTI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hyd = (HiddenField)e.Row.FindControl("hdfRefNo");

            ((Label)e.Row.FindControl("lblrtino")).Text = "EO/SIC-" + ((Label)e.Row.FindControl("lblrtino")).Text + " of " + hyd.Value;
            int RTID = Convert.ToInt32(((HiddenField)e.Row.FindControl("hyd")).Value);
            int RtistatusId = Convert.ToInt32(((HiddenField)e.Row.FindControl("Hystatus")).Value);
            HtmlImage img1 = (HtmlImage)e.Row.FindControl("img1");
            obj_petOB.StatusId = 6;
            obj_petOB.RTISaaId = RTID;
            p_Val.dSetChildData = obj_rtBL.getTempRTISAARecordsBYID(obj_petOB);
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.TransferedAuthority))
            {
                ((Label)e.Row.FindControl("lblSAAstatus")).Text = ((Label)e.Row.FindControl("lblSAAstatus")).Text + ":" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + "<br/> vide " + "Memo No: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/>Dated: " + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo1"].ToString();
            }

            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.judgement))
            {
                ((Label)e.Row.FindControl("lblSAAstatus")).Visible = false;

                HyperLink Hypjudgement = (HyperLink)e.Row.FindControl("Hypjudgement");
                if (Hypjudgement.Text != "" && Hypjudgement.Text != null)
                {
                    Hypjudgement.Text = "Judgement";
                    img1.Visible = true;
                    img1.Src = ResolveUrl("~/images/external.png");
                }
                else
                {
                    Hypjudgement.Text = "Judgement";
                    img1.Visible = false;
                }

            }
            else
            {
                img1.Visible = false;
            }
            HiddenField lblyear = (HiddenField)e.Row.FindControl("HypYear");
            ((Label)e.Row.FindControl("lblSAArefno1")).Text = "EO/SAA-" + ((Label)e.Row.FindControl("lblSAArefno1")).Text + " of " + lblyear.Value;


            if (e.Row.Cells[8].Text == "" || e.Row.Cells[8].Text == null || e.Row.Cells[8].Text == "&nbsp;")
            {
                grdFAA_SAA_RTI.Columns[8].Visible = false;
            }
            else
            {
                grdFAA_SAA_RTI.Columns[8].Visible = true;
            }

            Literal orderConnectedFile = (Literal)e.Row.FindControl("ltrlConnectedFile");
            if (orderConnectedFile.Text != null && orderConnectedFile.Text != "")
            {
                p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
                p_Val.sbuilder.Append("<a href='" + p_Val.url + orderConnectedFile.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                orderConnectedFile.Text = p_Val.sbuilder.ToString();
            }
        }
    }

    protected void grdFAA_SAA_RTI_RowCommand(object sender, GridViewCommandEventArgs e)
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
    }
}
