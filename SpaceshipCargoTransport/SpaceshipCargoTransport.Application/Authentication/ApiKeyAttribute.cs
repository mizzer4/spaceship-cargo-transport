using Microsoft.AspNetCore.Mvc;

namespace SpaceshipCargoTransport.Application.Authentication
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute()
            : base(typeof(ApiKeyAuthorizationFilter))
        {
        }
    }
}
