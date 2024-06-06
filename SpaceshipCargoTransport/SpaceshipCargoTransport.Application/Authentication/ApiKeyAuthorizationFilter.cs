using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SpaceshipCargoTransport.Application.Authentication
{
    internal class ApiKeyAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string ApiKeyHeaderName;

        private readonly IApiKeyValidator _apiKeyValidator;

        public ApiKeyAuthorizationFilter(IOptions<ApiKeyAuthorizationFilterOptions> options, IApiKeyValidator apiKeyValidator)
        {
            ApiKeyHeaderName = options.Value.HeaderName;
            _apiKeyValidator = apiKeyValidator;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];

            if (!_apiKeyValidator.IsValid(apiKey))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
