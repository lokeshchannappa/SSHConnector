//-----------------------------------------------------------------------
// <copyright file="DeviceStreamingHelper.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector.Streaming.Common
{
    using System;
    using System.Net.WebSockets;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Device Streaming Helper
    /// </summary>
    public static class DeviceStreamingHelper
    {
        /// <summary>
        /// Creates a ClientWebSocket with the proper authorization header for Device Streaming.
        /// </summary>
        /// <param name="url">URL to the Streaming Gateway.</param>
        /// <param name="authorizationToken">Authorization token to connect to the Streaming Gateway.</param>
        /// <param name="cancellationToken">The token used for canceling this operation if desired.</param>
        /// <returns>A ClientWebSocket instance connected to the Device Streaming gateway, if successful.</returns>
        public static async Task<ClientWebSocket> GetStreamingClientAsync(Uri url, string authorizationToken, CancellationToken cancellationToken)
        {
            ClientWebSocket webSockerClient = new ClientWebSocket();
            webSockerClient.Options.SetRequestHeader("Authorization", "Bearer " + authorizationToken);
            await webSockerClient.ConnectAsync(url, cancellationToken).ConfigureAwait(false);
            return webSockerClient;
        }
    }
}
