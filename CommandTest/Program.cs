using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.DeviceSchema;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandTest
{
    class Program
    {
        static double ExtractDouble(dynamic value)
        {
            if (value == null)
            {
                Trace.TraceError("ActionProcessor: unable to parse null double value");
                return -1;
            }

            string valueAsString = value.ToString();
            return double.Parse(valueAsString, CultureInfo.CurrentCulture);
        }
        static bool ExtractBool(dynamic value)
        {
            if (value == null)
            {
                Trace.TraceError("ActionProcessor: unable to parse null double value");
                return false;
            }

            string valueAsString = value.ToString();
            return bool.Parse(valueAsString);
        }
        static void Main(string[] args)
        {
            //dynamic command = CommandHistorySchemaHelper.BuildNewCommandHistoryItem("Power");
            //dynamic parameters = new Dictionary<string, object>();
            //parameters.Add("Status", true);
            //CommandHistorySchemaHelper.AddParameterCollectionToCommandHistoryItem(command, parameters);

            //ServiceClient serviceClient = ServiceClient.CreateFromConnectionString("HostName=eyesonair.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=ls0x0E2svbdpq0I/ruG5hYibzO24uUpIiIbIaXJJccg=");
            //Trace.TraceInformation("Connect iotHUb before send message");
            //byte[] commandAsBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(command));
            //var notificationMessage = new Message(commandAsBytes);

            //notificationMessage.Ack = DeliveryAcknowledgement.Full;
            //notificationMessage.MessageId = command.MessageId;

            //Task x = Task.Run(()=> AzureRetryHelper.OperationWithBasicRetryAsync( () =>
            //     serviceClient.SendAsync("PM25", notificationMessage)));
            //x.Wait();            
            //serviceClient.CloseAsync();

            Console.WriteLine(ExtractBool(true));
        }
    }
}
