using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomPrincipalSerializer
/// </summary>
public class CustomPrincipalSerializer
{
    public CustomPrincipalSerializer()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string Id { get; set; }
    public string UserName { get; set; }
    public int IsAdmin { get; set; }
    public int UserType { get; set; }
    public int[] Modules { get; set; }
}