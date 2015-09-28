using Sitecore.Pipelines;
using WebFormsForMarketers.Extensions.Web;

namespace WebFormsForMarketers.Extensions.Sample.Mvc
{
    public class RegisterWebApiRoute
    {
        public void Process(PipelineArgs args)
        {
            RouteRegistration registration = new RouteRegistration();
            registration.Register();
        }
    }
}