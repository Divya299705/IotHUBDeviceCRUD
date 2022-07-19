using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot_Device_Crud.Models;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Transport;

namespace Iot_Device_Crud.Repository
{
    public class IotDeviceProperties
    {
         static string connectionString="HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=";
         static RegistryManager resgistryManager=RegistryManager.CreateFromConnectionString(connectionString);
         
         static DeviceClient client= null; 
         static string deviceConnectionString="HostName=NxTIoTTraining.azure-devices.net;DeviceId=DivyaIotDevice;SharedAccessKey=JGHp0lk+lHE3feb4Pma6CHKcyJ+wYhwJm0z4/UZwi98=";

         public static async Task UpdateReportedProperties(string deviceName, ReportedProperties properties)
         {
            client=DeviceClient.CreateFromConnectionString(deviceConnectionString,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            TwinCollection reportedProperties, connectivity;
            reportedProperties = new TwinCollection();  
            connectivity = new TwinCollection();  
            connectivity["type"] = "cellular";  
            reportedProperties["connectivity"] = connectivity;  
            reportedProperties["temperature"] = properties.temperature;  
            reportedProperties["pressure"] = properties.pressure; 
            reportedProperties["drift"] = properties.drift; 
            reportedProperties["accuracy"] = properties.accuracy; 
            reportedProperties["supplyVoltageLevel"] = properties.supplyVoltageLevel; 
            reportedProperties["fullScale"] = properties.fullScale; 
            reportedProperties["frequency"] = properties.frequency; 
            reportedProperties["resolution"] = properties.resolution; 
            reportedProperties["sensorType"] = properties.sensorType; 
            reportedProperties["DateTimeLastAppLaunch"] = properties.DateTimeLastAppLaunch; 
            
            await client.UpdateReportedPropertiesAsync(reportedProperties);  
         }

         public static async Task UpdateDesiredProperties(string deviceName)
         {
            client=DeviceClient.CreateFromConnectionString(deviceConnectionString,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            var device=await resgistryManager.GetTwinAsync(deviceName);
            TwinCollection desiredProperties, telemetryconfig;
            desiredProperties = new TwinCollection();  
            telemetryconfig = new TwinCollection();  
            telemetryconfig["frequency"] = "sHz";  
            desiredProperties["telemetryconfig"] = telemetryconfig; 
            device.Properties.Desired["telemetryConfig"] = telemetryconfig; 
            await resgistryManager.UpdateTwinAsync(device.DeviceId,device, device.ETag); 
         }
         public static async Task<Twin> GetDevicePropertyAsync(string deviceId)
         {
            var device=await resgistryManager.GetTwinAsync(deviceId);
            return device;
         }

         public static async Task AddTagsAndQuery(string deviceId)
         {
            var device=await resgistryManager.GetTwinAsync(deviceId);
            var patch=
            @"{
                tags:{
                    location:{
                        region:'US',
                        plant:'IotDevice1'
                    }
                }

            }";
            await resgistryManager.UpdateTwinAsync(device.DeviceId, patch, device.ETag);
         }
    }
}