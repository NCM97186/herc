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

public partial class Ombudsman_ViewDetailsWhatsNew : System.Web.UI.Page
{
    PetitionOB obj_petOB = new PetitionOB();
	Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    AppealBL obj_petBL = new AppealBL();
    Project_Variables p_Val = new Project_Variables();
    protected void Page_Load(object sender, EventArgs e)
    {
        p_Val.Path = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Pdf"] + "/";
        if (!IsPostBack)
        {
           
                BindDetails(Request.QueryString["ID"].ToString());
           
        }
    }
    public void BindDetails(string ID)
    {
        obj_petOB.PageIndex = 1;
        obj_petOB.PageSize = 1;
        obj_petOB.LangId = Convert.ToInt32(Resources.HercResource.Lang_Id);
        obj_petOB.DepttId = 2;//Dept Id Ombandson
        obj_petOB.AppealNo = ID;
        p_Val.dSet = obj_petBL.GetLatestAward(obj_petOB, out p_Val.k);
        if (p_Val.dSet.Tables[0].Rows.Count > 0)
        {
            Grdaaward.Visible = true;
            Grdaaward.DataSource = p_Val.dSet;
            Grdaaward.DataBind();

        }
        else
        {
            Grdaaward.Visible = false;
        }
    }




   


    protected void Grdaaward_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            HiddenField hdf = (HiddenField)e.Row.FindControl("hdfFile");

            //connected Award files
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            Literal orderAwardFile = (Literal)e.Row.FindControl("ltrlConnectedAwardProunced");
            obj_petOB.AppealId = Convert.ToInt16(hdf.Value);
            p_Val.sbuilder.Remove(0, p_Val.sbuilder.Length);
            p_Val.dsFileName = obj_petBL.getConnectedAwardFiles(obj_petOB);
            if (p_Val.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Val.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Val.sbuilder.Append(" <a href='" + p_Val.Path + p_Val.dsFileName.Tables[0].Rows[i]["FileName"] + "' Text='" + p_Val.dsFileName.Tables[0].Rows[i]["Date"] + "' target='_blank'>" + "Award,Dated:" + p_Val.dsFileName.Tables[0].Rows[i]["Date"]+ " <img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> ");
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
        }
    }
}
