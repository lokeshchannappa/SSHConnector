//-----------------------------------------------------------------------
// <copyright file="CustomException.cs" company="Weir Group PLC">
//    Copyright (c) Weir Group PLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector.Filter
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Serilog;
    using SSHConnector.Filter.Exceptions;

    /// <summary>
    /// Custom Exception to handle global exceptions
    /// </summary>
    public class GlobalExceptionHandler : IExceptionFilter
    {
        /// <summary>
        /// On Exception capture the error.
        /// </summary>
        /// <param name="context"> Exception Context</param>
        public void OnException(ExceptionContext context)
        {
            string message = string.Empty;
            HttpStatusCode status = HttpStatusCode.InternalServerError;

            var exceptionType = context.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized Access";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "A server error occurred.";
                status = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType == typeof(ApiException))
            {
                /// way of handling custom exception
                message = context.Exception.Message;
                status = HttpStatusCode.InternalServerError;
            }
            else
            {
                message = context.Exception.Message;
                status = HttpStatusCode.NotFound;
            }


            Log.Logger = new LoggerConfiguration().Enrich.WithCorrelationId()
                .WriteTo.File("log.txt")
                .CreateLogger();

            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            response.WriteAsync(new ErrorDetails()
            {
                Service = "SSH Connector",
                Message = message,
            }.ToString());

            Log.Error(message);
        }
    }
}
