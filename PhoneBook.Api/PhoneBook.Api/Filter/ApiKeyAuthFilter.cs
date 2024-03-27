using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Net.Http;

namespace PhoneBook.Middileware
{
    public class ApiKeyAuthFilter:IAuthorizationFilter
    {
        private const string ApiKey = "x-api-key";
        private const string ErrorMessage = "Unauthrozied request";
        private readonly IConfiguration configuration;

        public ApiKeyAuthFilter( IConfiguration configuration)
        {
           this.configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            StringValues requestApiKey;
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKey, out requestApiKey))
            {
                context.Result =new UnauthorizedObjectResult(ErrorMessage);
                return;
            }

            var apiKey = configuration[ApiKey]?.ToString();

            if (!requestApiKey.Equals(apiKey))
            {
                context.Result = new UnauthorizedObjectResult(ErrorMessage);
            }
        }
    }
}
