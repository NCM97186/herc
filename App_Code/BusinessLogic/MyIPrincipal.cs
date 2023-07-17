using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
/// <summary>
/// GAllentry User Login
/// </summary>
public interface MyIPrincipal : IPrincipal
{
    string Id { get; set; }
    string UserName { get; set; }

}