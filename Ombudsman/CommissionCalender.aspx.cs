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
using System.Drawing;

public partial class Ombudsman_CommissionCalender : BasePageOmbudsman
{
    #region variable declaretion

    string str = string.Empty;
    PetitionOB petObject = new PetitionOB();
    PetitionBL scheduleOfHearingBL = new PetitionBL();
    Miscelleneous_DL miscellDL = new Miscelleneous_DL();
    DataSet ds = new DataSet();
    public string lastUpdatedDate = string.Empty;

    string PageTitle = string.Empty;
    string MetaKeyword = string.Empty;
    string MetaDescription = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        str = BreadcrumDL.DisplayBreadCrumOmbudsman(Resources.HercResource.OmbudsmanCalendar);
        ltrlBreadcrum.Text = str.ToString();
       
       PageTitle = Resources.HercResource.OmbudsmanCalendar;

       if (!string.IsNullOrEmpty(Request.QueryString["format"]))
       {

           HtmlLink cssRef = new HtmlLink();
           cssRef.Href = "css/print.css";
           cssRef.Attributes["rel"] = "stylesheet";
           cssRef.Attributes["type"] = "text/css";
           Page.Header.Controls.Add(cssRef);
       }

       if (!IsPostBack)
       {

           FillCalendarChoices();
           SelectCorrectValues();
           Get_Date_forcalenderEvent(System.DateTime.Now.Month.ToString(), System.DateTime.Now.Year.ToString()); 
       }
      
    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
	try{
        string date;
        int count = ds.Tables[0].Rows.Count;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            date = dr["date"].ToString();
            if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                if (Convert.ToDateTime(date) == Convert.ToDateTime(e.Day.Date.ToString()))
                {
                    if (Resources.HercResource.Lang_Id == "1")
                    {
                        e.Cell.Text = "<a href=" + ResolveUrl("~/Ombudsman/ScheduleOfHearing.aspx?date=") + date + ">" + e.Day.DayNumberText + "</a>";
                        e.Cell.ToolTip = "Hearing Scheduled: " + dr["Datesoh"].ToString() + ", " + dr["subject"].ToString() + " Venue: " + dr["venue"].ToString();
                    }
                    else
                    {
                        e.Cell.Text = "<a href=" + ResolveUrl("~/OmbudsmanContent/Hindi/Ombudsman/ScheduleOfHearing.aspx?date=") + date + ">" + e.Day.DayNumberText + "</a>";
                        e.Cell.ToolTip = "Hearing Scheduled: " + dr["Datesoh"].ToString() + ", " + dr["subject"].ToString() + " Venue: " + dr["venue"].ToString();
                    }
                    //e.Cell.BackColor = System.Drawing.Color.Pink;
                    e.Cell.CssClass = "pink";

                }
                //if (e.Day.IsToday)
                //{
                //    e.Cell.BackColor = ColorTranslator.FromHtml("#CBD5D6");
                //}
            }
            else
            {
                if (Convert.ToDateTime(date) == Convert.ToDateTime(e.Day.Date.ToString()))
                {
                    if (Resources.HercResource.Lang_Id == "1")
                    {
                        e.Cell.Text = "<a href=" + ResolveUrl("~/Ombudsman/ScheduleOfHearing.aspx?date=") + dr["date1"].ToString() + ">" + e.Day.DayNumberText + "</a>";
                        e.Cell.ToolTip = "Hearing Scheduled: " + dr["Datesoh"].ToString() + ", " + dr["subject"].ToString() + " Venue: " + dr["venue"].ToString();
                    }
                    else
                    {
                        e.Cell.Text = "<a href=" + ResolveUrl("~/OmbudsmanContent/Hindi/Ombudsman/ScheduleOfHearing.aspx?date=") + dr["date1"].ToString() + ">" + e.Day.DayNumberText + "</a>";
                        e.Cell.ToolTip = "Hearing Scheduled: " + dr["Datesoh"].ToString() + ", " + dr["subject"].ToString() + " Venue: " + dr["venue"].ToString();
                    }
                    //e.Cell.BackColor = System.Drawing.Color.Pink;
                    e.Cell.CssClass = "pink";
                }
            }
            //if (e.Day.IsToday)
            //{
            //    e.Cell.BackColor = ColorTranslator.FromHtml("#CBD5D6");
            //}
        }
		}
		catch{}
    }
    public void Get_Date_forcalenderEvent(string month, string year)
    {
        petObject.Month = month;
        petObject.year = year;
        if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
        {

            petObject.LangId = Convert.ToInt16(Module_ID_Enum.Language_ID.English);
            petObject.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            ds = scheduleOfHearingBL.get_dateforcalenderevant(petObject);
        }
        else
        {
            petObject.LangId = Convert.ToInt16(Module_ID_Enum.Language_ID.Hindi);
            petObject.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            ds = scheduleOfHearingBL.get_dateforcalenderevant(petObject);
        }
    }
    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/Ombudsman/ScheduleOfHearing.aspx"));

    }

    #region Function to set Keywords and meta-keywords

    protected override void PageMetaTags()
    {

        base.Page.Title = PageTitle + ": " + Resources.HercResource.WebsiteTitleOmbudsman;
        PageDescription = MetaDescription;
        PageKeywords = MetaKeyword;
    }

    #endregion

  

    protected void SelectCorrectValues()
    {
       
    }

    protected void FillCalendarChoices()
    {
        DateTime thisdate1 = (DateTime.Now);
        string datetime = "01/01/" + thisdate1.Year.ToString();
        DateTime thisdate = Convert.ToDateTime(datetime);

        // Fills in month values
        for (int x = 0; x < 12; x++)
        {
            // Loops through 12 months of the year and fills in each month value
            ListItem li = new ListItem(thisdate.ToString("MMMM"), thisdate.Month.ToString());
            MonthSelect.Items.Add(li);
            //to add next next month name to the monthselect drop downlist control like aug then sept and so on....
            thisdate = thisdate.AddMonths(1);
        }

        // Fills in year values and change y value to other years if necessary
        for (int y = thisdate1.Year+1; y > 2011; y--)
        {
            YearSelect.Items.Add(y.ToString());
        }
        YearSelect.SelectedValue = DateTime.Now.Year.ToString();
        ListItem li1 = new ListItem(thisdate1.Month.ToString());
        MonthSelect.SelectedIndex = Convert.ToInt16(li1.Text) - 1;
    }

    protected void MonthChange(Object sender, MonthChangedEventArgs e)
    {
        Calendar1.TodaysDate = e.NewDate.AddMonths(-1);
        //YearSelect.SelectedItem.Text = e.NewDate.Year.ToString();
		YearSelect.SelectedValue = e.NewDate.Year.ToString();
        ListItem li1 = new ListItem(e.NewDate.ToString("MMMM"), e.NewDate.Month.ToString());
       //MonthSelect.SelectedItem.Text = li1.Value;
        MonthSelect.SelectedValue = li1.Value;
        Get_Date_forcalenderEvent(e.NewDate.Month.ToString(), e.NewDate.Year.ToString()); 

    }
    protected void btnCalender_Click(object sender, EventArgs e)
    {
        Calendar1.SelectedDate = Calendar1.VisibleDate
            = new DateTime(Convert.ToInt32(YearSelect.SelectedItem.Value),
                           Convert.ToInt32(MonthSelect.SelectedItem.Value), 1);
        Calendar1.SelectedDate = Calendar1.VisibleDate
            = new DateTime(Convert.ToInt32(YearSelect.SelectedItem.Value),
                           Convert.ToInt32(MonthSelect.SelectedItem.Value), 1);

        Get_Date_forcalenderEvent(MonthSelect.SelectedItem.Value.ToString(), YearSelect.SelectedItem.Value.ToString()); 
    }
}
