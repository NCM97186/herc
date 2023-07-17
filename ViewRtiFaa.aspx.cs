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

public partial class ViewRtiFaa :BasePage
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
        p_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        p_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        FAA_RTI = Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["rtiid"]);
        if (!Page.IsPostBack)
        {
            str = BreadcrumDL.DisplayBreadCrumRTI(Resources.HercResource.RTI, Resources.HercResource.CurrentYearApplications);
            ltrlBreadcrum.Text = str.ToString();

            Bind_RTI();
            BindFAA_RTI();
            //PSAA.Style.Add("display", "none");
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

            //obj_petOB.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
            //obj_petOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_petOB.StatusId = 6;
            obj_petOB.RTIFAAId = RTID;
            p_Val.dSetChildData = obj_rtBL.getTempRTIFAARecordsBYID(obj_petOB);
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.replysent))
            {
                ((Label)e.Row.FindControl("lblFAAstatus")).Text = ((Label)e.Row.FindControl("lblFAAstatus")).Text + "<br/>vide "+ "Memo No:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
            }
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.TransferedAuthority))
            {
                ((Label)e.Row.FindControl("lblFAAstatus")).Text = ((Label)e.Row.FindControl("lblFAAstatus")).Text + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + "<br/> vide " + "Memo No:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/>Dated:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
            }



            //((Label)e.Row.FindControl("lblrefno")).Text = "HERC/RTI-" + ((Label)e.Row.FindControl("lblrefno")).Text;
            HiddenField hyd = (HiddenField)e.Row.FindControl("hdfrefNo");
            Label lblyear = (Label)e.Row.FindControl("lblyear");
            ((Label)e.Row.FindControl("lblrefno")).Text = "HERC/RTI-" + ((Label)e.Row.FindControl("lblrefno")).Text + " of " + hyd.Value;

            ((Label)e.Row.FindControl("lblFAArefno")).Text = "HERC/FAA-" + ((Label)e.Row.FindControl("lblFAArefno")).Text + " of " + lblyear.Text;
            obj_rtBL.Check_RTI_FAA_SAA(RTID, out p_Val.i);
            if (p_Val.i == 0)
            {
                ((LinkButton)e.Row.FindControl("lnlbtn")).Visible = false;
                ((LinkButton)e.Row.FindControl("lnlbtn")).Text = "N";
            }
            else
            {
                if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.inprocess))
                {
                    ((LinkButton)e.Row.FindControl("lnlbtn")).Visible = false;
                }
                else
                {
                    ((LinkButton)e.Row.FindControl("lnlbtn")).Visible = true;
                    ((LinkButton)e.Row.FindControl("lnlbtn")).Text = "YES";
                }
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
        if (e.CommandName == "vdetail")
        {
            //pfaa.Style.Add("display", "none");
            //PSAA.Style.Add("display", "block");
            Bind_FAA_SAA_GRID(Convert.ToInt32(e.CommandArgument));
        }
    }
   
    #region function declaration zone

    public void Bind_RTI()
    {
        obj_petOB.LangId = 1;
        obj_petOB.DepttId = 1; // HERC Dept ID          
        obj_petOB.RTIId = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["rtiid"]);
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
            obj_petOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_petOB.RTIId = RTID;
            p_Val.dSetChildData = obj_rtBL.Get_RTIById(obj_petOB);
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.replysent))
            {
                ((Label)e.Row.FindControl("lblstatus")).Text = ((Label)e.Row.FindControl("lblstatus")).Text + "<br/>vide "+ "Memo No:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
            }
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.TransferedAuthority))
            {
                ((Label)e.Row.FindControl("lblstatus")).Text = ((Label)e.Row.FindControl("lblstatus")).Text + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + "<br/> vide " + "Memo No:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
            }
           //((Label)e.Row.FindControl("lblrefno")).Text = "HERC/RTI-" + ((Label)e.Row.FindControl("lblrefno")).Text;

            Label lblyear = (Label)e.Row.FindControl("lblyear");
            ((Label)e.Row.FindControl("lblrefno")).Text = "HERC/RTI-" + ((Label)e.Row.FindControl("lblrefno")).Text + " of " + lblyear.Text;
            if (string.IsNullOrEmpty(((HiddenField)e.Row.FindControl("hdfFile")).Value))
            {
                ((LinkButton)e.Row.FindControl("lbl_ViewDoc")).Visible = false;
            }
        }
    }

    protected void grdFAA_SAA_RTI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //((Label)e.Row.FindControl("lblrefno")).Text = "HERC/RTI-" + ((Label)e.Row.FindControl("lblrefno")).Text;
            HiddenField hyd = (HiddenField)e.Row.FindControl("hdfRefNo");

            ((Label)e.Row.FindControl("lblrtino")).Text = "HERC/RTI-" + ((Label)e.Row.FindControl("lblrtino")).Text + " of " + hyd.Value;

        }
    }

    protected void Grdappeal_RowCommand(object sender, GridViewCommandEventArgs e)
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

