using Danske.MTM.Common.Exceptions;
using Danske.MTM.Common.Interfaces;
using Danske.MTM.WebApi.Middlewares;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Danske.MTM.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var errorDetails = new ErrorDetails();

            if (exception is BaseApplicationException)
            {
                errorDetails.Message = exception.Message;

                switch (exception)
                {
                    case NotFoundException notFoundException:
                    case InvalidRequestException invalidException:
                        errorDetails.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    case ValidationException validationException:
                        errorDetails.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorDetails.Errors = validationException.Failures;
                        break;

                    default:
                        break;
                }

                context.Response.StatusCode = errorDetails.StatusCode;
            }
            else
            {
                await logger.LogErrorAsync(exception);
                errorDetails.Message = "Internal Server Error, please contact your administrator.";
                errorDetails.StatusCode = (int)HttpStatusCode.InternalServerError;

                context.Response.StatusCode = errorDetails.StatusCode;
            }

            await context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}