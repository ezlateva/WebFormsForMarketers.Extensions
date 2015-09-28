using System;
using Sitecore.Web;
using WebFormsForMarketers.Extensions.Web;

namespace WebFormsForMarketers.Extensions.Sample
{
    public class Global : Application
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteRegistration registration = new RouteRegistration();
            registration.Register();
        }
    }
}