using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Iot_Device_Crud.Repository;

namespace Iot_Device_Crud.Controllers
{
    [ApiController] 
    [Route("[controller]")]
    public class TelementaryMessagesController : ControllerBase
    {
        [HttpPost]
        [Route("SendTelemetryMessages")]
        public async Task SendDeviceToCloudMessagesAsync(string deviceName)
        {
            await TelementryMessages.SendDeviceToCloudMessagesAsync(deviceName);
        }
    }
}