using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for WhatNewsOB
/// </summary>
public class WhatNewsOB
{
	public WhatNewsOB()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Variable declrations

    public int AwardId { get; set; }
    public int CorrigendumId { get; set; }
    public int TenderId { get; set; }
    public int TenderIdTmp { get; set; }
    public int LangId { get; set; }
    public int ActionType { get; set; }
    public int Flag { get; set; }
    public int InsertedBy { get; set; }
    public int ApproveStatus { get; set; }
    public int ModuleID { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }

    public int Status { get; set; }
    public int UserId { get; set; }
    public int FileTypeID { get; set; }
    public String RefNo { get; set; }
    public String FileType { get; set; }
    public String TenderTitle { get; set; }
    public String NewsTitle { get; set; }
    public String TenderDescription { get; set; }
    public String NewsDescription { get; set; }
    public String TenderCost { get; set; }
    public String FileExtension { get; set; }
    public String EarnestMoney { get; set; }
    public String FileTitle { get; set; }
    public String CorrigendumDescription { get; set; }
    public String FileName { get; set; }
    public String Name { get; set; }
    public String IpAddress { get; set; }
    public String EmailId { get; set; }
    public String MobileNo { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime Expirydate { get; set; }
    public String TenderAwarded_Amount { get; set; }
    public String TenderAwardedto { get; set; }


    #endregion
}
