using System.Web;
using System.Web.Mvc;

namespace PassionProject_PhoneBlog_n01364240
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
