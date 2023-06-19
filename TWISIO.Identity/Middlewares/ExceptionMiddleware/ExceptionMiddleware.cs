using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Xml;
using TWISIO.Identity.API.Common.Exceptions;
using TWISIO.Identity.API.Entities.Enums;
using TWISIO.Identity.API.Interfaces.Repositories;
using TWISIO.Identity.API.Models;

namespace TWISIO.Identity.API.Middlewares.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogRepository? _logRepository;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogRepository logRepository)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logRepository = logRepository;
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        private async Task<Task> HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            int statusCode;
            var message = string.Empty;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                case BadRequestException:
                case ValidationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ForbiddenException:
                    statusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case XmlException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = "Некорректный файл";
                    break;
                default:
                    await _logRepository!.AddAsync(exception.Message, exception.InnerException?.Message, LogType.ERROR);
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonConvert.SerializeObject(new ErrorModel
            {
                StatusCode = statusCode,
                Message = message == string.Empty ? exception.Message : message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
