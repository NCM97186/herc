using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;



public class PaginationBL
{
    #region Default constructor zone

    public PaginationBL()
	{

    }

    #endregion

    #region Data declaration zone

    PaginationDL pagingDL = new PaginationDL();

    #endregion

    #region Function to doing pagination

    public void Paging_Show(int recordCount, int currentPage, DropDownList ddlName, Repeater repeaterName)
    {
        pagingDL.Paging_Show(recordCount, currentPage, ddlName, repeaterName);
    }

    #endregion

}
