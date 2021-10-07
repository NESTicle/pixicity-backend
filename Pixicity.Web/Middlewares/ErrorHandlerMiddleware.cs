using Microsoft.AspNetCore.Http;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pixicity.Web.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogsService _logService;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogsService logService)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    KeyNotFoundException e => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                _logService.CreateSystemLogs(new Data.Models.Logs.SystemLogs()
                {
                    Type = Data.Models.Logs.LogsType.SYSTEM,
                    Message = error?.InnerException?.Message,
                    Code = System.Net.HttpStatusCode.InternalServerError.ToString(),
                    Data = JsonSerializer.Serialize(new { }, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true
                    }),
                    URL = "/api/redi/startContractValidation",
                    StackTrace = error?.StackTrace,
                });

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
