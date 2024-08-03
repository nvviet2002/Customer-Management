﻿using CustomerManagement.Commons;
using CustomerManagement.Exceptions;

namespace CustomerManagement.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                var apiResponse = new ApiResponse<dynamic>();
                if (ex.GetType() == typeof(NotFoundException))
                {
                    apiResponse.StatusCode = StatusCodes.Status404NotFound;
                    apiResponse.Message = ex.Message;
                    apiResponse.Data = null;
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                }
                else if (ex.GetType() == typeof(AuthException))
                {
                    apiResponse.StatusCode = StatusCodes.Status401Unauthorized;
                    apiResponse.Message = ex.Message;
                    apiResponse.Data = null;
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
                else if (ex.GetType() == typeof(BadRequestException))
                {
                    apiResponse.StatusCode = StatusCodes.Status400BadRequest;
                    apiResponse.Message = ex.Message;
                    apiResponse.Data = null;
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
                else
                {
                    apiResponse.StatusCode = StatusCodes.Status500InternalServerError;
                    apiResponse.Message = ex.Message;
                    apiResponse.Data = null;
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                await httpContext.Response.WriteAsJsonAsync<ApiResponse<dynamic>>(apiResponse);
            }
        }
    }
}
