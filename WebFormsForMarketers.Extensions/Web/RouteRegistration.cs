using System.Web.Http;
using WebFormsForMarketers.Extensions.Web.Handlers;

namespace WebFormsForMarketers.Extensions.Web
{
    public class RouteRegistration
    {
        public void Register()
        {
            //Enable CORS support
            GlobalConfiguration.Configuration.EnableCors();

            // Deserialize / Model Bind IE 8 and 9 Ajax Requests
            GlobalConfiguration.Configuration.MessageHandlers.Add(new XDomainRequestDelegatingHandler());

            var config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute("DefaultApiRoute",
                                     "api/{controller}/{id}",
                                     new { id = RouteParameter.Optional });
        }
    }
}