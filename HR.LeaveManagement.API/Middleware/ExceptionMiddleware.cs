using HR.LeaveManagement.API.Models;
using HR.LeaveManagement.Application.Exceptions;
using System.Net;

namespace HR.LeaveManagement.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomValidationProblemDetails problem = new();

            switch(ex)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomValidationProblemDetails
                    {
                        Title   = badRequestException.Message,
                        Status  = (int)statusCode,
                        Detail  = badRequestException.InnerException?.Message,
                        Type    = nameof(BadRequestException),
                        Errors  = badRequestException.ValidationErrors
,                    };
                break;

                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomValidationProblemDetails
                    {
                        Title   = notFoundException.Message,
                        Status  = (int)statusCode,
                        Type    = nameof(NotFoundException),
                        Detail  = notFoundException.InnerException?.Message,
                    };
                break;
                
                default:
                    problem = new CustomValidationProblemDetails
                    { 
                        Title   = ex.Message,
                        Status  = (int)statusCode,
                        Type    = nameof(HttpStatusCode.InternalServerError),
                        Detail  = ex.StackTrace,
                    };
                break;
            }

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
