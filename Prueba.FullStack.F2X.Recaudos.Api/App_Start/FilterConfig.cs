using System.Web;
using System.Web.Mvc;

namespace Prueba.FullStack.F2X.Recaudos.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
