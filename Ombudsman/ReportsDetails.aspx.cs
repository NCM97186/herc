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

public partial class Ombudsman_ReportsDetails : BasePageOmbudsman
{
    #region variable declaration

    TariffOB obj = new TariffOB();
    TariffBL obj_tariffBL = new TariffBL();
    Project_Variables P_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    public string lastUpdatedDate = string.Empty;

    string str;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        P_Val.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/"; 
        if (!IsPostBack)
        {
            Bind_TariffDetails();
        }
        if (RewriteModule.RewriteContext.Current.Params["menuid"]== null || Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]) == null)
        {

        }
        else
        {
           
            P_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
            P_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        }
        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.Details);
        ltrlBreadcrum.Text = str.ToString();
        if (!string.IsNullOrEmpty(Request.QueryString["format"]))
        {
            HtmlLink cssRef = new HtmlLink();
            cssRef.Href = "css/print.css";
            cssRef.Attributes["rel"] = "stylesheet";
            cssRef.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssRef);
        }
    }

    #region function to bind tariff details

    public void Bind_TariffDetails()
    {

        if (ViewState["year"] != null)
        {
            obj.Year = ViewState["year"].ToString();
        }
        else
        {
            obj.Year = null;
        }
        obj.CatId = null;
        obj.linkID = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["id"]);
        obj.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Reports);
        P_Val.dSet = obj_tariffBL.GetdetailsData(obj);

        // This code for multiple files
        obj.TempLinkId = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["id"]);
        P_Val.dsFileName = obj_tariffBL.getFileName(obj);
        //End

        if (P_Val.dSet.Tables[0].Rows.Count > 0)
        {

                lastUpdatedDate = P_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
                lrtTariff.Text = HttpUtility.HtmlDecode(P_Val.dSet.Tables[0].Rows[0]["details"].ToString());
                lblyear.Text =P_Val.dSet.Tables[0].Rows[0]["year"].ToString();

                if (P_Val.dsFileName.Tables[0].Rows.Count > 0)
                {
                    ltrlPdf.Visible = true;
                    for (int j = 0; j < P_Val.dsFileName.Tables[0].Rows.Count; j++)
                    {
                        
                        ltrlPdf.Text += " , " + "<a href='" + P_Val.url + P_Val.dsFileName.Tables[0].Rows[j]["FileName"] + "' Text='" + P_Val.dsFileName.Tables[0].Rows[j]["FileName"] + "' target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>";

                    }
                }
                else
                {
                    ltrlPdf.Visible = false;
                }

                //////////if (P_Val.dSet.Tables[0].Rows[0]["file_name"] != DBNull.Value && P_Val.dSet.Tables[0].Rows[0]["file_name"].ToString() != "")
                //////////{
                //////////    ltrlPdf.Visible = true;
                //////////    ltrlPdf.Text += " , " + "<a href='" + P_Val.url + P_Val.dSet.Tables[0].Rows[0]["file_name"] + "' Text='" + P_Val.dSet.Tables[0].Rows[0]["file_name"] + "' target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>";
                //////////}
                //////////else
                //////////{
                //////////    ltrlPdf.Visible = false;
                //////////}
   



        }
        else
        {

        }



    }

    #endregion 


    #region link button click event zone

    public void Lnkback_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {
            Response.Redirect(ResolveUrl("~/Reports/"+RewriteModule.RewriteContext.Current.Params["menuid"].ToString()+"_"+RewriteModule.RewriteContext.Current.Params["position"].ToString()+"_"+"ReportSubmittedToHERC.aspx"));
        }
        else
        {
            Response.Redirect("~/OmbudsmanContent/Hindi/Reports/" + RewriteModule.RewriteContext.Current.Params["menuid"].ToString() + "_" + RewriteModule.RewriteContext.Current.Params["position"].ToString() + "_" + "ReportSubmittedToHERC.aspx");

        }

    }

    #endregion 
}
