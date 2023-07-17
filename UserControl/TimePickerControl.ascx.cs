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

public partial class TimePicker_TimePickerControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.Page.IsPostBack)
        {


        }

    }

    public string GetTime
    {

        get
        {

            string time = lstHour.Text + ":" + lstmin.Text + ":" + lstsec.Text + " " + lstDtpart.Text;

            return time;

        }

        set
        {

            try
            {

                string mytime = value;

                string[] times = mytime.Split(':');

                lstHour.Text = Convert.ToInt32(times[0]).ToString("00");

                lstmin.Text = Convert.ToInt32(times[1]).ToString("00");

                string[] times1 = times[2].Split(' ');

                lstsec.Text = Convert.ToInt32(times1[0]).ToString("00");

                lstDtpart.Text = times1[1].ToString();

            }

            catch { }

        }

    }
}
