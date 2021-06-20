using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace MovieShop.MVC.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> _logger;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Inside the Logger Middleware");
            var datetime = DateTime.Now;
            var ipAddress = httpContext.Connection?.RemoteIpAddress?.ToString();

            _logger.LogInformation( "Date Time the request came: {0}", datetime.ToString());
            _logger.LogInformation("Ip Address Of User: {0}", ipAddress);

            if (httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated)
            {
                var email = httpContext?.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                var fullname = httpContext?.User.Claims
                                   .FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value + " " +
                               httpContext?.User.Claims
                                   .FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;

                // log this information Database, text file, json file
                // System.IO ...I can use this one....
                // 3rd party logging frameworks.... Serilog, NLog
                // ASP.NET Core has built-in functionality for Logging, MS provided us with interface for Logging.
                // ILogger using Microsoft.Extensions.Logging;

                // login

                _logger.LogInformation("Email Address {0}", email);
                _logger.LogInformation("Name: {0} ", fullname);
            }
           

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggerMiddleware>();
        }
    }
}
