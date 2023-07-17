using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
/// <summary>
/// GAllery User Login
/// </summary>
public class MyCustomPrincipal :MyIPrincipal
{

    public IIdentity Identity { get; private set; }
    public bool IsInRole(string role) { return false; }
    public MyCustomPrincipal(string email)
    {
        this.Identity = new GenericIdentity(email);
    }
    public string Id { get; set; }
    public string UserName { get; set; }
    public int IsAdmin { get; set; }
    public int UserType { get; set; }
    public int[] Modules { get; set; }
    public bool IsModule(int Moduleid)
    {
        var result = Array.FindAll(Modules, s => s.Equals(Moduleid));
        if (result != null && result.Count() > 0)
        {
            return true;

        }
        else
        {
            return false;
        }
    }
}