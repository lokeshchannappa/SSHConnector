//-----------------------------------------------------------------------
// <copyright file="CustomException.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector.Filter
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Custom Exception to handle global exceptions
    /// </summary>
    public class CustomException : IExceptionFilter
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
            else if (exceptionType == typeof(ApplicationException))
            {
                message = context.Exception.ToString();
                status = HttpStatusCode.InternalServerError;
            }
            else
            {
                message = context.Exception.Message;
                status = HttpStatusCode.NotFound;
            }

            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            var err = message + " " + context.Exception.StackTrace;
            response.WriteAsync(err);
        }
    }
}
