using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSHConnector.Model
{
    public class RequestBase
    {
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
