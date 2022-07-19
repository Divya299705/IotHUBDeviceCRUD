using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Iot_Device_Crud.Models;
using Iot_Device_Crud.Repository;

namespace Iot_Device_Crud.Controllers
{
    [ApiController] 
    [Route("[controller]")]
    public class IotDevicePropertiesController : ControllerBase
    {
        [HttpPut]
        [Route("UpdateReportedProperty")]
        public async Task UpdateDeviceReportedProperties(string deviceName, ReportedProperties properties)
        {
            await IotDeviceProperties.UpdateReportedProperties(deviceName,properties);
        }

        [HttpPut]
        [Route("UpdateDesiredProperty")]
        public async Task UpdateDesiredProperties(string deviceName)
        {
            await IotDeviceProperties.UpdateDesiredProperties(deviceName);
        }

        [HttpPut]
        [Route("UpdateTags")]
        public async Task UpdateTagsAndQuery(string deviceName)
        {
            await IotDeviceProperties.AddTagsAndQuery(deviceName);
        }
    }
}