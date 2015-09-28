using System.Web;
using System.Web.Routing;
using Sitecore.Pipelines.HttpRequest;

namespace WebFormsForMarketers.Extensions.Web.Processors
{
    public class AbortSitecoreForKnownRoutes : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            var routeCollection = RouteTable.Routes;
            var routeData = routeCollection.GetRouteData(new HttpContextWrapper(args.Context));

            if (routeData == null || args.Url.ItemPath.Contains("api/sitecore")) return;

            HttpContext.Current.RemapHandler(routeData.RouteHandler.GetHttpHandler(HttpContext.Current.Request.RequestContext));
            args.AbortPipeline();
        }
    }
}