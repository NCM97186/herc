﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string str = BreadcrumDL.DisplayBreadCrum(Resources.HercResource.Search);
        ltrlBreadcrum.Text = str;
    }
}