//-----------------------------------------------------------------------
// <copyright file="DeviceStreamInitializer.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector.Streaming
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Net.WebSockets;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.Devices;
    using SSHConnector.Streaming.Common;

    /// <summary>
    /// Device Stream Initializer
    /// </summary>
    public class DeviceStreamInitializer
    {
        /// <summary>
        /// service client
        /// </summary>
        private ServiceClient serviceClient;

        /// <summary>
        /// device Id
        /// </summary>
        private string deviceId;

        /// <summary>
        /// local port
        /// </summary>
        private int localPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceStreamInitializer" /> class.
        /// </summary>
        /// <param name="deviceClient">device client</param>
        /// <param name="deviceId">device id</param>
        /// <param name="localPort">local port</param>
        public DeviceStreamInitializer(ServiceClient deviceClient, string deviceId, int localPort)
        {
            this.serviceClient = deviceClient;
            this.deviceId = deviceId;
            this.localPort = localPort;
        }

        /// <summary>
        /// Handle incoming connections and create Streams
        /// </summary>
        /// <returns>it is void</returns>
        public async Task RunAsync()
        {
            var tcpListener = new TcpListener(IPAddress.Loopback, this.localPort);
            tcpListener.Start();

            while (true)
            {
                var tcpClient = await tcpListener.AcceptTcpClientAsync().ConfigureAwait(false);
                HandleIncomingConnectionsAndCreateStreams(this.deviceId, this.serviceClient, tcpClient);
            }
        }

        /// <summary>
        /// Handle incoming data 
        /// </summary>
        /// <param name="localStream">local Stream</param>
        /// <param name="remoteStream">remote Stream</param>
        /// <param name="cancellationToken">cancellation Token</param>
        /// <returns>it is void</returns>
        private static async Task HandleIncomingDataAsync(NetworkStream localStream, ClientWebSocket remoteStream, CancellationToken cancellationToken)
        {
            byte[] receiveBuffer = new byte[10240];
            while (localStream.CanRead)
            {
                var receiveResult = await remoteStream.ReceiveAsync(receiveBuffer, cancellationToken).ConfigureAwait(false);
                await localStream.WriteAsync(receiveBuffer, 0, receiveResult.Count).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Handle outgoing data
        /// </summary>
        /// <param name="localStream">local Stream</param>
        /// <param name="remoteStream">remote Stream</param>
        /// <param name="cancellationToken">cancellation Token</param>
        /// <returns>it is void</returns>
        private static async Task HandleOutgoingDataAsync(NetworkStream localStream, ClientWebSocket remoteStream, CancellationToken cancellationToken)
        {
            byte[] buffer = new byte[10240];
            while (remoteStream.State == WebSocketState.Open)
            {
                int receiveCount = await localStream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                await remoteStream.SendAsync(new ArraySegment<byte>(buffer, 0, receiveCount), WebSocketMessageType.Binary, true, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Handle incoming connections and create streams
        /// </summary>
        /// <param name="deviceId">device Id</param>
        /// <param name="serviceClient">service Client</param>
        /// <param name="tcpClient">TCP Client</param>
        private static async void HandleIncomingConnectionsAndCreateStreams(string deviceId, ServiceClient serviceClient, TcpClient tcpClient)
        {
            DeviceStreamRequest deviceStreamRequest = new DeviceStreamRequest(streamName: "TestStream");

            using (var localStream = tcpClient.GetStream())
            {
                DeviceStreamResponse result = await serviceClient.CreateStreamAsync(deviceId, deviceStreamRequest, CancellationToken.None).ConfigureAwait(false);
                if (result.IsAccepted)
                {
                    using (var cancellationTokenSource = new CancellationTokenSource())
                    using (var remoteStream = await DeviceStreamingHelper.GetStreamingClientAsync(result.Url, result.AuthorizationToken, cancellationTokenSource.Token).ConfigureAwait(false))
                    {
                        await Task.WhenAny(
                            HandleIncomingDataAsync(localStream, remoteStream, cancellationTokenSource.Token),
                            HandleOutgoingDataAsync(localStream, remoteStream, cancellationTokenSource.Token)).ConfigureAwait(false);
                    }
                }
            }

            tcpClient.Close();
        }
    }
}
