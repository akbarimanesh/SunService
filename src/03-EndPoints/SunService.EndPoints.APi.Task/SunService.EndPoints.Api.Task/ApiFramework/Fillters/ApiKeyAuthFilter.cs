using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SunService.EndPoints.Api.Task.ApiFramework.Fillters
{
    public class ApiKeyAuthFilter : Attribute, IActionFilter
    {
        private readonly string _apiKey;

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            _apiKey = configuration["ApiSettings:ApiKey"]; 
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey) || extractedApiKey != _apiKey)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }



       
    }
}
