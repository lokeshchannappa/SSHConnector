//-----------------------------------------------------------------------
// <copyright file="DeviceStream.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector.Streaming
{
    using System.Threading.Tasks;
    using Microsoft.Azure.Devices;
    using SSHConnector.Model;

    /// <summary>
    /// Device Stream is responsible for starting the device stream using Connection string, deviceId and port
    /// </summary>
    public class DeviceStream
    {
        /// <summary>
        /// Create device streaming.
        /// </summary>
        /// <param name="request">request contains Connection string, deviceId, port</param>
        /// <returns>No response, it is void</returns>
        public async Task StartAsync(SshRequest request)
        {
            using (ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(request.DeviceConnectionString, TransportType.Amqp))
            {
                var deviceStream = new DeviceStreamInitializer(serviceClient, request.DeviceId, request.Port);
                await deviceStream.RunAsync();
            }
        }
    }
}
