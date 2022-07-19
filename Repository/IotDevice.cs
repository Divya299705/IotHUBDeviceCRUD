using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace Iot_Device_Crud.Repository
{
    public class IotDevice
    {
         static RegistryManager resgistryManager;
         static string connectionString="HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=";
         static Device device;

        public static async Task<Device> AddDeviceAsync(string deviceId){
            resgistryManager=RegistryManager.CreateFromConnectionString(connectionString);
            device=await resgistryManager.AddDeviceAsync(new Device(deviceId));
            return device;
        }
        public static async Task<Device> GetDeviceAsync(string deviceId){
            resgistryManager=RegistryManager.CreateFromConnectionString(connectionString);
            device=await resgistryManager.GetDeviceAsync(deviceId);
            return device;
        }
        public static async Task UpdateDeviceAsync(string deviceId){
            Device OldDevice;
            resgistryManager=RegistryManager.CreateFromConnectionString(connectionString);
            OldDevice=await resgistryManager.GetDeviceAsync(deviceId);
            OldDevice.Status=DeviceStatus.Enabled;
            await resgistryManager.UpdateDeviceAsync(OldDevice);
        }
        public static async Task RemoveDeviceAsync(string deviceId){
            resgistryManager=RegistryManager.CreateFromConnectionString(connectionString);
            await resgistryManager.RemoveDeviceAsync(deviceId);
        }
    }
}