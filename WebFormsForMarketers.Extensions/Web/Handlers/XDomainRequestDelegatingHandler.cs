using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace WebFormsForMarketers.Extensions.Web.Handlers
{
    public class XDomainRequestDelegatingHandler : DelegatingHandler
    {
        /// <summary>
        /// For Reference: http://stackoverflow.com/questions/18989088/cors-web-api-ie8-post-complex-data
        /// </summary>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // XDomainRequest objects set the Content Type to null, which we cannot change

            // XDomainRequest object limitations:
            // http://blogs.msdn.com/b/ieinternals/archive/2010/05/13/xdomainrequest-restrictions-limitations-and-workarounds.aspx

            // By international specification, a null content type is supposed to result in application/octect-stream (spelling mistake?),
            // WebAPI framework doesn't convert that for us, so we would get an 'unsupported media type' error
            // Hence, we handle and set it here
            if (request.Content.Headers.ContentType == null)
            {
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}