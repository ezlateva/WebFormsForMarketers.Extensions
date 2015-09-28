using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http.Cors;
using WebFormsForMarketers.Extensions.Configuration;

namespace WebFormsForMarketers.Extensions.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class CorsPolicyAttribute : Attribute, ICorsPolicyProvider
    {
        private CorsPolicy _policy;

        private static readonly ConfigurationSection Configuration = ConfigurationSection.Create();

        public CorsPolicyAttribute()
        {
            // Create a CORS policy.
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };

            // Add allowed origins.
            foreach (OriginElement origin in Configuration.Origins)
            {
                _policy.Origins.Add(origin.Name);
            }
        }

        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(_policy);
        }
    }
}