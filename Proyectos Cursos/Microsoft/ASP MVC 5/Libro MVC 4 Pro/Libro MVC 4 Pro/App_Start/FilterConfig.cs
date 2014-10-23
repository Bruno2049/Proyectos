using System.Web;
using System.Web.Mvc;

namespace Libro_MVC_4_Pro
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}