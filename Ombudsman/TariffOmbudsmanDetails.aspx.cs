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

public partial class Ombudsman_TariffOmbudsmanDetails : BasePage
{
    #region variable declaration

    TariffOB obj = new TariffOB();
    TariffBL obj_tariffBL = new TariffBL();
    Project_Variables P_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    string str;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["menuid"] == null || Convert.ToInt16(Request.QueryString["position"]) == null)
        {

        }
        else
        {
            P_Val.PageID = Request.QueryString["menuid"].ToString();
            P_Val.position = Convert.ToInt16(Request.QueryString["position"]);
        }
        str = BreadcrumDL.DisplayBreadCrumTariffOmbudsman(Resources.HercResource.ConsumerEmpowerment, Resources.HercResource.TariffDetails, null);
        ltrlBreadcrum.Text = str.ToString();
        if (Request.QueryString["catid"] == "1")
        {
            Dist.Attributes.Add("class", "current");
        }
        else if (Request.QueryString["catid"] == "2")
        {
            Trans.Attributes.Add("class", "current");
        }
        else if (Request.QueryString["catid"] == "3")
        {
            Gen.Attributes.Add("class", "current");
        }
        else if (Request.QueryString["catid"] == "4")
        {
            Wheeling.Attributes.Add("class", "current");
        }
        else if (Request.QueryString["catid"] == "5")
        {
            cross.Attributes.Add("class", "current");
        }
        else
        {
            renewal.Attributes.Add("class", "current");
        }
        if (!IsPostBack)
        {

            Bind_TariffDetails();
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
        obj.linkID = Convert.ToInt16(Request.QueryString["id"]);

        P_Val.dSet = obj_tariffBL.GetdetailsData(obj);

        if (P_Val.dSet.Tables[0].Rows.Count > 0)
        {
            if (Request.QueryString["menuid"] != null && Convert.ToInt16(Request.QueryString["menuid"]) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA_ombudsman))
            {
                lrtTariff.Text = HttpUtility.HtmlDecode(P_Val.dSet.Tables[0].Rows[0]["details"].ToString());
                lblyear.Text = "<strong>" + "FSA" + "</strong>" + ":- " + P_Val.dSet.Tables[0].Rows[0]["year"];
            }
            ////////else if (Convert.ToInt16(P_Val.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.GeneralMiscCharges))
            ////////{
            ////////    lrtTariff.Text = HttpUtility.HtmlDecode(P_Val.dSet.Tables[0].Rows[0]["details"].ToString());
            ////////    lblyear.Text = "<strong>" + "General & Misc Charges" + "</strong>" + ":- " + P_Val.dSet.Tables[0].Rows[0]["year"];

            ////////}
            else
            {
                lrtTariff.Text = HttpUtility.HtmlDecode(P_Val.dSet.Tables[0].Rows[0]["details"].ToString());
                lblyear.Text = "<strong>" + P_Val.dSet.Tables[0].Rows[0]["catName"] + "</strong>" + ":- " + P_Val.dSet.Tables[0].Rows[0]["year"];

            }

            ////////lrtTariff.Text = HttpUtility.HtmlDecode(P_Val.dSet.Tables[0].Rows[0]["details"].ToString());
            ////////lblyear.Text = "<strong>" + P_Val.dSet.Tables[0].Rows[0]["catName"] + "</strong>" + ":- " + P_Val.dSet.Tables[0].Rows[0]["year"];

        }
        else
        {

        }



    }

    #endregion 


    #region link button click event zone

    public void Lnkback_Click(object sender, EventArgs e)
    {
        if ((Convert.ToInt16(Request.QueryString["catid"]) == Convert.ToInt16(Module_ID_Enum.Tariff_category.DistributionCharges)))
        {
            Response.Redirect("~/Ombudsman/DistributionOmbudsman.aspx");
        }
        else if ((Convert.ToInt16(Request.QueryString["catid"]) == Convert.ToInt16(Module_ID_Enum.Tariff_category.GenerationTariff)))
        {
            Response.Redirect("~/Ombudsman/GenerationOmbudsman.aspx");
        }
        else if ((Convert.ToInt16(Request.QueryString["catid"]) == Convert.ToInt16(Module_ID_Enum.Tariff_category.RenewalEnergy)))
        {
            Response.Redirect("~/Ombudsman/RenewalEnergyOmbudsman.aspx");
        }
        else if ((Convert.ToInt16(Request.QueryString["catid"]) == Convert.ToInt16(Module_ID_Enum.Tariff_category.CrossSubsidydditionalSurcharge)))
        {
            Response.Redirect("~/Ombudsman/CrosssubsidyOmbudsman.aspx");
        }
        else if ((Convert.ToInt16(Request.QueryString["catid"]) == Convert.ToInt16(Module_ID_Enum.Tariff_category.WheelingCharges)))
        {
            Response.Redirect("~/Ombudsman/WheelingOmbudsman.aspx");
        }
        else if ((Convert.ToInt16(Request.QueryString["catid"]) == Convert.ToInt16(Module_ID_Enum.Tariff_category.TransmissionCharges)))
        {
            Response.Redirect("~/Ombudsman/TransmissionOmbudsman.aspx");
        }
        else if (Convert.ToInt16(P_Val.PageID) == Convert.ToInt16(Module_ID_Enum.Menu_ID_Fixed.FSA_ombudsman))
        {
            Response.Redirect("~/FSAOmbudsman/" + P_Val.PageID + "_" + P_Val.position + "_FSA.aspx");
        }
        //else
        //{
        //    Response.Redirect("~/GeneralMiscCharges/" + P_Val.PageID + "_" + P_Val.position + "_GeneralCharges.aspx");
        //}


    }

    #endregion 
}
