//-----------------------------------------------------------------------
// <copyright file="ApplicationException.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector.Filter.Exceptions
{
    using System;

    /// <summary>
    /// Application Exception
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException" /> class.
        /// </summary>
        public ApiException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException" /> class with error message.
        /// </summary>
        /// <param name="message">Error Message</param>
        public ApiException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException" /> class with error message and inner exception
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="innerException">Inner Exception</param>
        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
