using Microsoft.AspNetCore.Http;
using ProductManagement.Application.DTOs.Response;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Text.Json;

namespace ProductManagement.Presentation.Middlewares
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new BaseResponseDto
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Errors = new List<string> { exception.Message }
            };

            var result = JsonSerializer.Serialize(responseModel);
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return response.WriteAsync(result);
        }
    }
}
