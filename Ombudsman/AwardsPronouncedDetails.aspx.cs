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

public partial class Ombudsman_AwardsPronouncedDetails : System.Web.UI.Page
{
    #region variable declaration

    string str = string.Empty;
   
    PetitionBL obj_petBL1 = new PetitionBL();
    AppealBL obj_petBL = new AppealBL();
    PetitionOB obj_petOB = new PetitionOB();
    Project_Variables p_Val = new Project_Variables();
 

    #endregion 
    protected void Page_Load(object sender, EventArgs e)
    {
        p_Val.Path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Pdf"] + "/";
        if(!IsPostBack)
        {
            Bind_Data();
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

    public void Bind_Data()
    {
        obj_petOB.AppealId =Convert.ToInt16(Request.QueryString["AwardsPronouncedID"]);
       
        //p_Val.dSet = obj_petBL.Get_Appeal_Detail_By_ID(obj_petOB);
        //p_Val.dSet = obj_petBL.GetAwardUnderAppealDetails(obj_petOB);
        p_Val.dSet = obj_petBL.get_AwardDetails(obj_petOB);
        
        pappeal.Visible = true;
        grdappeal.DataSource = p_Val.dSet;
        grdappeal.DataBind();
        Bind_Appeal();
    }




    #region function declaration zone

    public void Bind_Appeal()
    {
        obj_petOB.TempAppealId = Convert.ToInt16(Request.QueryString["AwardsPronouncedID"]);
        obj_petOB.StatusId = 6;
        //p_Val.dSet = obj_petBL.getAppealRecordForEdit(obj_petOB);
        p_Val.dSet = obj_petBL.getAppealRecord(obj_petOB);

        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {

            grdappealDetails.DataSource = p_Val.dSet;
            grdappealDetails.DataBind();

        }
        else
        {
            grdappealDetails.DataSource = p_Val.dSet;
            grdappealDetails.DataBind();

        }


    }


    #endregion

    protected void grdappeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            //LinkButton lnk = (LinkButton)e.Row.FindControl("lbl_ViewDoc");

            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");
            HyperLink Hypjudgement = (HyperLink)e.Row.FindControl("Hypjudgement");
            HtmlImage img1 = (HtmlImage)e.Row.FindControl("img1");
            string Description = DataBinder.Eval(e.Row.DataItem, "Description").ToString();

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
                    Hypjudgement.Text = "Judgement";
                    img1.Src = ResolveUrl("~/images/external.png");
                }
                else
                {
                    img1.Visible = false;
                }

            }

            //connected Award files
            Literal orderAwardFile = (Literal)e.Row.FindControl("ltrlConnectedAwardProunced");
            obj_petOB.AppealId = Convert.ToInt16(hdf.Value);
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            //p_Val.dsFileName = obj_petBL.getConnectedAwardFiles(obj_petOB);
            p_Val.dsFileName = obj_petBL.getAppealAwardPronounced(obj_petOB);

            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Val.sbuilder.Append(" <a href='" + p_Val.Path + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "' target='_blank'>" + "Award,Dated:" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> "+ "</a>");
                    p_Val.sbuilder.Append("<br/><hr/>");

                }
                orderAwardFile.Text = p_Val.sbuilder.ToString();

            }
            else
            {

            }
            
        }
    }





    protected void grdappealDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblConnectedSoh_ID = (Label)e.Row.FindControl("lblConnectedSoh_ID");
            LinkButton lnkConnectedSoh = (LinkButton)e.Row.FindControl("lnkConnectedSoh");

            HiddenField hyappealId = (HiddenField)e.Row.FindControl("hyappealId");
            

            //connected Award files
            Literal orderAwardFile = (Literal)e.Row.FindControl("ltrlConnectedAwardProunced");
            obj_petOB.AppealId = Convert.ToInt16(hyappealId.Value);
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            p_Val.dsFileName = obj_petBL.getConnectedAwardFiles(obj_petOB);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Val.sbuilder.Append(" <a href='" + p_Val.Path + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "' target='_blank'>" + "Award,Dated:" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] +"<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> "+ "</a>");
                    p_Val.sbuilder.Append("<br/><hr/>");

                }
                orderAwardFile.Text = p_Val.sbuilder.ToString();

            }
            else
            {

            }





            HiddenField hyAppeal = (HiddenField)e.Row.FindControl("hyappealId");
            obj_petOB.AppealId = Convert.ToInt16(hyAppeal.Value);
            p_Val.dSetCompare = obj_petBL.getConnectedSOH_Appeal(obj_petOB);
            if (p_Val.dSetCompare.Tables[0].Rows.Count > 0)
            {

                lnkConnectedSoh.Visible = true;
                lblConnectedSoh_ID.Visible = false;
                lnkConnectedSoh.Text = "Yes";

            }
            else
            {
                lnkConnectedSoh.Visible = false;
                lblConnectedSoh_ID.Visible = true;
                grdappealDetails.Columns[9].Visible = false;
            }



            //This is for Appeal under appeal on 2 sep 2013
            Label lblAwardunderappeal = (Label)e.Row.FindControl("lblAwardunderappeal");
            LinkButton lnkAwardunderappeal = (LinkButton)e.Row.FindControl("lnkAwardunderappeal");

            obj_petOB.AppealId = Convert.ToInt16(hyAppeal.Value);
            p_Val.dSetChildData = obj_petBL.getConnectedAwardApealFiles(obj_petOB);

            if (p_Val.dSetChildData.Tables[0].Rows.Count > 0)
            {

                lnkAwardunderappeal.Visible = true;
                lblAwardunderappeal.Visible = false;
                lnkAwardunderappeal.Text = "Yes";

            }
            else
            {
                lnkAwardunderappeal.Visible = false;
                lblAwardunderappeal.Visible = true;
               
            }
           
        }
    }


    protected void grdappealDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "connectedAwardunderappeal")
        {
            p_Val.categoryID = e.CommandArgument.ToString();
            Bind_Data();
        }
    }



}
