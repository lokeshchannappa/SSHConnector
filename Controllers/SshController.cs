//-----------------------------------------------------------------------
// <copyright file="SshController.cs" company="Weir Group PLC">
//    Copyright (c) Weir Group PLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using SSHConnector.Model;
    using SSHConnector.Streaming;
    using SSHConnector.Filter.Exceptions;

    /// <summary>
    /// SshController will be used for ssh related endpoints
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SshController : ControllerBase
    {
        [HttpPost("connect")]
        public async Task<ActionResult> ConnectSSH([FromBody] SshRequest request)
        {
            // return Bad request
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            try
            {
                // Create SSH streaming. This will run in separate thread, so that it won't block the main process
                DeviceStream deviceStream = new DeviceStream();
                await Task.Factory.StartNew(() => deviceStream.StartAsync(request));
                return Ok("Connected");
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
