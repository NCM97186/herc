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

public partial class Ombudsman_FSAOmbudsman : BasePage
{

    #region variable declaration

    TariffOB obj = new TariffOB();
    TariffBL obj_tariffBL = new TariffBL();
    Project_Variables P_Val = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    string str;
    public string lastUpdatedDate = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        str = BreadcrumDL.DisplayBreadCrumTariffOmbudsman(Resources.HercResource.ConsumerEmpowerment, Resources.HercResource.FSA, null);
        ltrlBreadcrum.Text = str.ToString();
        P_Val.PageID = RewriteModule.RewriteContext.Current.Params["menuid"].ToString();
        P_Val.position = Convert.ToInt16(RewriteModule.RewriteContext.Current.Params["position"]);
        if (!IsPostBack)
        {
            BindYear();
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


    public void Bind_Tariff(int pageIndex)
    {
        obj.PageIndex = pageIndex;
        obj.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        obj.LangId = Convert.ToInt16(Resources.HercResource.Lang_Id);
        P_Val.sbuilder.Remove(0, P_Val.sbuilder.Length);
        if (ViewState["year"] != null)
        {
            obj.Year = ViewState["year"].ToString();
        }
        else
        {
            obj.Year = null;
        }
        obj.CatId = Convert.ToInt16(Module_ID_Enum.Tariff_subcategory.FSA);

        P_Val.dSet = obj_tariffBL.GetdetailsFSA(obj, out P_Val.k);

        if (P_Val.dSet.Tables[0].Rows.Count > 0)
        {
            lblmsg.Visible = false;

            for (int i = 0; i < P_Val.dSet.Tables[0].Rows.Count; i++)
            {

                P_Val.sbuilder.Append("<ul>");
                P_Val.sbuilder.Append("<li>");
                P_Val.sbuilder.Append("<a href='" + ResolveUrl("~/Ombudsman/TariffOmbudsmanDetails.aspx?id=" + P_Val.dSet.Tables[0].Rows[i]["link_id"].ToString() + "&menuId=" + P_Val.PageID + "&Position=" + P_Val.position) + "' Text='" + P_Val.dSet.Tables[0].Rows[i]["Name"] + "'>" + P_Val.dSet.Tables[0].Rows[i]["year"] + "</a>");
                P_Val.sbuilder.Append("</li>");
                P_Val.sbuilder.Append("</ul>");
            }
            lrtTariff.Text = P_Val.sbuilder.ToString();
            lastUpdatedDate = P_Val.dSet.Tables[0].Rows[0]["Lastupdate"].ToString();
            P_Val.sbuilder.Remove(0, P_Val.sbuilder.Length);
            rptPager.Visible = true;
            ddlPageSize.Visible = true;
            lblPageSize.Visible = true;
        }
        else
        {
            lblmsg.Text = Resources.HercResource.NoDataAvailable.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
            rptPager.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
        }

        P_Val.Result = P_Val.k;
        if (P_Val.Result > Convert.ToInt16(ddlPageSize.SelectedValue))
        {
            pagingBL.Paging_Show(P_Val.Result, pageIndex, ddlPageSize, rptPager);
            rptPager.Visible = true;
        }
        else
        {
            rptPager.Visible = false;
        }

    }

    #region function to Bind data in a data list for year

    public void BindYear()
    {
        pyear.Visible = true;
        ddlPageSize.Visible = false;
        obj.CatId = Convert.ToInt16(Module_ID_Enum.Tariff_subcategory.FSA);

        P_Val.dSetChildData = obj_tariffBL.getYear(obj);

        if (P_Val.dSetChildData.Tables[0].Rows.Count > 0)
        {
            datalistYear.DataSource = P_Val.dSetChildData;
            datalistYear.DataBind();
        }
        else
        {
            pyear.Visible = false;
        }

    }


    #endregion 


    #region Datalist datalistYear itemCommand event zone

    protected void datalistYear_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            P_Val.str = e.CommandArgument.ToString();
            ViewState["year"] = P_Val.str;
            Bind_Tariff(1);
        }
    }

    #endregion


    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Bind_Tariff(pageIndex);
    }

    #endregion

    //End

    //Area for all the dropDownlist events


    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Bind_Tariff(1);
    }

    #endregion
}
