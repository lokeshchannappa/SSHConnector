//-----------------------------------------------------------------------
// <copyright file="SshRequest.cs" company="Weir Group PLC">
//    Copyright (c) Weir Group PLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector.Model
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Base request
    /// </summary>
    public class SshRequest
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
    }
}
