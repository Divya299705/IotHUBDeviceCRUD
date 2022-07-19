using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Iot_Device_Crud.Models;


namespace Iot_Device_Crud.Repository
{
    public class TelementryMessages
    {
        static string connectionString="HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=";
        static RegistryManager resgistryManager=RegistryManager.CreateFromConnectionString(connectionString);
        static DeviceClient client= null; 
        static string deviceConnectionString="HostName=NxTIoTTraining.azure-devices.net;DeviceId=DivyaIotDevice;SharedAccessKey=JGHp0lk+lHE3feb4Pma6CHKcyJ+wYhwJm0z4/UZwi98=";  
  
        public static async Task SendDeviceToCloudMessagesAsync(string deviceName)  
         {  
            try  
            {  
                client=DeviceClient.CreateFromConnectionString(deviceConnectionString,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                var device=await resgistryManager.GetTwinAsync(deviceName);
                ReportedProperties properties=new ReportedProperties();
                TwinCollection reportedProperties;
                reportedProperties = device.Properties.Reported; 
  
                while (true)  
                {  
                    var telemetryDataPoint = new  
                    {    
                        temperature = reportedProperties["temperature"],
                        pressure = reportedProperties["pressure"],
                        supplyVoltageLevel = reportedProperties["supplyVoltageLevel"],
                        fullScale = reportedProperties["fullScale"],
                        frequency = reportedProperties["frequency"],
                        accuracy = reportedProperties["accuracy"],
                        resolution = reportedProperties["resolution"],
                        drift = reportedProperties["drift"],
                        sensorType = reportedProperties["sensorType"]
                    };  
  
                    string messageString = "";  
                    messageString = JsonConvert.SerializeObject(telemetryDataPoint);  
  
                    var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));  
   
                    await client.SendEventAsync(message);  
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);  
                    await Task.Delay(1000 * 10);  
                  
                }  
            }  
            catch (Exception ex)  
            {  
                throw ex;  
            }  
        }    
    }
}