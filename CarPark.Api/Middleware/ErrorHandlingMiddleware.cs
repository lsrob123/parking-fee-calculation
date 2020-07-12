using CarPark.Abstractions;
using CarPark.Api.Models;
using CarPark.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarPark.Api.Middleware
{
    public class ErrorHandlingMiddleware : MiddlewareBase
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, RequestDelegate next) : base(next)
        {
            _logger = logger;
        }

        public override async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ProcessException processException)
            {
                _logger.LogError(processException,
                    $"[{processException.ErrorId}] {processException.InternalErrorMessages}");
                await CreateErrorResponse(context,
                    processException.HttpStatusCode,
                    processException.ErrorId,
                    processException.Message);
            }
            catch (Exception exception)
            {
                var errorId = Guid.NewGuid();
                _logger.LogError(exception, $"[{errorId}] {exception.Message}");
                await CreateErrorResponse(context,
                    HttpStatusCode.InternalServerError,
                    errorId,
                    "Error encountered");
            }
        }

        private Task CreateErrorResponse(HttpContext context, HttpStatusCode httpStatusCode,
            Guid errorId, string message)
        {
            var response = new ErrorResponse(errorId, message, httpStatusCode);
            var json = JsonSerializer.Serialize(response,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(json);
        }
    }
}