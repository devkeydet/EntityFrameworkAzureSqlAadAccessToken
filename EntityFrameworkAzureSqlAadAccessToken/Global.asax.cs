using EntityFrameworkAzureSqlAadAccessToken.Utils;
using System.Web.Mvc;
using System.Web.Routing;

namespace EntityFrameworkAzureSqlAadAccessToken
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // Call get token to put it in the in memory cache
            TokenHelper.GetAccessToken();
        }
    }
}