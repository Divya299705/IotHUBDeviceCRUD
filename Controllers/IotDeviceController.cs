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
    public class IotDeviceController : ControllerBase
    { 
        [HttpPost]
        [Route("AddDevice")]
        public async Task AddDevice(string deviceId)
        {
            await IotDevice.AddDeviceAsync(deviceId);
        }
        
        [HttpGet]
        [Route("GetDevice/{deviceId}")]
        public async Task GetDevice(string deviceId)
        {
            await IotDevice.GetDeviceAsync(deviceId);
        }

        [HttpPut]
        [Route("UpdateDevice/{deviceId}")]
        public async Task UpdateDevice(string deviceId)
        {
            await IotDevice.UpdateDeviceAsync(deviceId);
        }

        [HttpDelete]
        [Route("RemoveDevice/{deviceId}")]
        public async Task RemoveDevice(string deviceId)
        {
            await IotDevice.RemoveDeviceAsync(deviceId);
        }
    }
}