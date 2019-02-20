using Exam.Back.Mvc.Filter;
using System.Web;
using System.Web.Mvc;

namespace Exam.Back.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleErrorFilter());
            //filters.Add(new AuthFilter());
        }
    }
}
