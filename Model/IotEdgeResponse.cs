//-----------------------------------------------------------------------
// <copyright file="IotEdgeResponse.cs" company="Weir Group PLC">
//    Copyright (c) Weir Group PLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector.Model
{
    /// <summary>
    /// IOT Edge Response Model
    /// </summary>
    public class IotEdgeResponse
    {
        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets config
        /// </summary>
        public string Config { get; set; }
    }
}
