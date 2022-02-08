using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace ContosoPizza.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException e)
            {
                await HandleExceptionAsync(context, e);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, ValidationException ex)
        {
            var Errors = ex.Errors;

            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(Errors);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
