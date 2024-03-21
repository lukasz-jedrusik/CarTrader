using System.Diagnostics;
using System.Net;
using System.Text.Json;
using CarTrader.Services.ParkingPlaces.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CarTrader.Services.ParkingPlaces.Api.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger,
            IWebHostEnvironment hostingEnvironment
            )
        {
            _next = next;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var problemDetails = new ProblemDetails()
                {
                    Detail = error?.Message
                };

                var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;
                problemDetails.Extensions.Add("traceId", traceId);

                if (_hostingEnvironment.IsDevelopment()
                    || _hostingEnvironment.EnvironmentName.ToUpper().Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
                {
                    problemDetails.Extensions.Add("errors", new { message = error?.Message, stackTrace = error?.StackTrace} );
                }

                switch(error)
                {
                    default:
                        // Unhandled errors
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        problemDetails.Title = "An error occured while processing your request";
                        problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
                        problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                _logger.LogError("{traceId}|{error}", traceId, error.ToString());
                var result = JsonSerializer.Serialize(problemDetails);
                await response.WriteAsync(result);
            }
        }
    }
}