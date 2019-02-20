using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Back.Mvc.Filter
{
    public class AuthFilter: AuthorizeAttribute
    {
     

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                  || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }
            
                if (HttpContext.Current.Session["username"] == null)
                {
                    filterContext.Result = new RedirectResult("/Administrator/DahinterLogin");
                }
             
        }
    }
}