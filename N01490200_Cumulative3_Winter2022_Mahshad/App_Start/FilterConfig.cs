using System.Web;
using System.Web.Mvc;

namespace N01490200_Cumulative3_Winter2022_Mahshad
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
