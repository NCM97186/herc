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

public partial class Ombudsman_viewfaa :BasePageOmbudsman
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
   static  int FAA_RTI;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        p_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        FAA_RTI = Convert.ToInt32(RewriteModule.RewriteContext.Current.Params["rtiid"]);
        if (!Page.IsPostBack)
        {
            str = BreadcrumDL.DisplayBreadCrumRTIOmbudsman(Resources.HercResource.RTI, Resources.HercResource.CurrentYearApplications);
            ltrlBreadcrum.Text = str.ToString();
            Bind_RTI();
            BindFAA_RTI();
            //PSAA.Style.Add("display", "none");
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
        obj_petOB.LangId = Convert.ToInt32( Resources.HercResource.Lang_Id);
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
                ((Label)e.Row.FindControl("lblFAAstatus")).Text = ((Label)e.Row.FindControl("lblFAAstatus")).Text +  "<br/>vide "+ "Memo No:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/>Dated:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
            }
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.TransferedAuthority))
            {
                ((Label)e.Row.FindControl("lblFAAstatus")).Text = ((Label)e.Row.FindControl("lblFAAstatus")).Text + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + " <br/>vide " + "Memo No:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/>Dated:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
            }
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
    //protected void btnback_Click(object sender, EventArgs e)
    //{

    //    pfaa.Style.Add("display", "block");
    //    PSAA.Style.Add("display", "none");
    //}
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
                ((Label)e.Row.FindControl("lblstatus")).Text = ((Label)e.Row.FindControl("lblstatus")).Text + "<br/>vide "+ "Memo No:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/>Dated:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
            }
            if (RtistatusId == Convert.ToInt16(Module_ID_Enum.rti_Status.TransferedAuthority))
            {
                ((Label)e.Row.FindControl("lblstatus")).Text = ((Label)e.Row.FindControl("lblstatus")).Text + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + " <br/>vide " + "Memo No:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated:" + p_Val.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
            }
            ((Label)e.Row.FindControl("lblrefno")).Text = "HERC/RTI-" + ((Label)e.Row.FindControl("lblrefno")).Text;

        }
    }
}
