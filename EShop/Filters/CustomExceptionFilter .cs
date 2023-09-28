using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EShop.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred.");
            var StatusCode = DetermineStatusCode(context.Exception);


            // You can customize the response here based on the exception type.
            var Result = new ObjectResult(new
            {
                Message = "An error occurred.",
                Detail = context.Exception.Message,
                StackTrace = context.Exception.StackTrace,
                StatusCode = StatusCode

            });
            

            context.Result = Result;
        }

        private int DetermineStatusCode(Exception exception)
        {
            if (exception is ArgumentException)
            {
                return (int)HttpStatusCode.BadRequest;  
            }
            else if (exception is UnauthorizedAccessException)
            {
                return (int)HttpStatusCode.Unauthorized;  
            }
            else
            {
                return (int)HttpStatusCode.InternalServerError; 
            }
        }
    }
}
