using Core.BackEnd;
using Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Rest.Middleware
{
    // Este Middleware maneja las excepciones que se produzcan de manera global.
    // Estas excepciones son principalmente relacionadas a SQL y la Base de Datos, pero también maneja
    // excepciones que no hayan sido consideradas.
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string message;
            context.Response.ContentType = "application/json";

            if (ExceptionTypes.IsSqlException(exception))
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                message = "There has been a conflict with the DataBase. Check Northwind's documentation and try again.";
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                message = "An unexcpected error ocurred.";
            }

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
