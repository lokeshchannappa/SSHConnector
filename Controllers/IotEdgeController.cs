//-----------------------------------------------------------------------
// <copyright file="IotEdgeController.cs" company="Weir Group PLC">
//    Copyright (c) Weir Group PLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SSHConnector.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using SSHConnector.Filter.Exceptions;
    using SSHConnector.Model;

    /// <summary>
    /// IOTEdgeController will be used to query IOT Edge related commands
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IotEdgeController : ControllerBase
    {
        /// <summary>
        /// Get list of IOT Edge devices
        /// </summary>
        /// <param name="request">request object, which contains connection string, port, host, deviceId, userName, password</param>
        /// <returns>List of devices with status</returns>
        [HttpPost("list")]
        public async Task<ActionResult<IEnumerable<IotEdgeResponse>>> GetIotedgeList([FromBody] IotEdgeRequest request)
        {
            // return Bad request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var list = new List<IotEdgeResponse>();
            try
            {
                string response = await SshConnector.GetCommandResponse(request, @"cd C:\Program Files\iotedge && iotedge list");
                if (!string.IsNullOrEmpty(response))
                {
                    var infos = SshConnector.GetResponse(response);
                    list = infos.Select(element => new IotEdgeResponse { Name = element[0], Status = element[1], Description = string.Concat(element[2], " ", element[3], " ", element[4]), Config = element[5] }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }

            return Ok(list);
        }
    }
}
