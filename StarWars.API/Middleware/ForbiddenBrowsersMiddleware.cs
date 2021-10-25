using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StarWars.API.Middleware
{
    public class ForbiddenBrowsersMiddleware
    {
        private const string UserAgentHeaderAlias = "User-Agent";
        private const string EdgeBrowserAlias = "edg";
        private readonly RequestDelegate _nextDelegate;

        public ForbiddenBrowsersMiddleware(RequestDelegate next) => _nextDelegate = next;

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Headers[UserAgentHeaderAlias].Any(header => header.ToLower().Contains(EdgeBrowserAlias)))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            else
            {
                await _nextDelegate.Invoke(httpContext);
            }
        }
    }
}
