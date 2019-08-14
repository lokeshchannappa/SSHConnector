//-----------------------------------------------------------------------
// <copyright file="BaseRequest.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector.Model
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Base request
    /// </summary>
    public class BaseRequest
    {
        /// <summary>
        /// Gets or sets device connection string
        /// </summary>
        [Required(ErrorMessage = "Device Connection String required")]
        public string DeviceConnectionString { get; set; }

        /// <summary>
        /// Gets or sets deviceId
        /// </summary>
        [Required(ErrorMessage = "DeviceId required")]
        public string DeviceId { get; set; }
        
        /// <summary>
        /// Gets or sets port, default port is 22
        /// </summary>
        public int Port { get; set; } = 22;

        /// <summary>
        /// Gets or sets userName
        /// </summary>
        [Required(ErrorMessage = "UserName required")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets password
        /// </summary>
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets host, default host is 127.0.0.1
        /// </summary>
        public string Host { get; set; } = "127.0.0.1";
    }
}
