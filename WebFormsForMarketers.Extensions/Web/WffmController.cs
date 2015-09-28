using System.Web.Http;
using System.Web.Http.Results;
using WebFormsForMarketers.Extensions.Processors;
using WebFormsForMarketers.Extensions.Web.Attributes;

namespace WebFormsForMarketers.Extensions.Web
{
    [CorsPolicy]
    public class WffmController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(FormData data)
        {
            FormProcessor processor = new FormProcessor();
            FormProcessorResult result = processor.Process(data);

            if (!result.Success)
            {
                return new BadRequestErrorMessageResult(result.ResultMessage, this);
            }

            return new OkResult(this);
        }
    }
}