//-----------------------------------------------------------------------
// <copyright file="SshController.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
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
                await Task.Factory.StartNew(() => DeviceStream.StartAsync(request));
                return Ok("Connected");
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
