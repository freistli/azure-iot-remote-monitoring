using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.BusinessLogic;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.DeviceSchema;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.Helpers;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Repository
{
    /// <summary>
    /// Repository storing available actions for rules.
    /// </summary>
    public class ActionRepository : IActionRepository
    {
        // Currently this list is not editable in the app
        private List<string> _actionIds = new List<string>()
        {
            "Send Message",
            "Raise Alarm",
            "Power On",
            "Power Off"
        };

        public async Task<List<string>> GetAllActionIdsAsync()
        {
            return await Task.Run(() => { return _actionIds; });
        }

        public async Task<bool> ExecuteLogicAppAsync(string actionId, string deviceId, string measurementName, double measuredValue)
        {
          //  Debug.WriteLine("ExecuteLogicAppAsync is not yet implemented");

             
                
                dynamic command = CommandHistorySchemaHelper.BuildNewCommandHistoryItem("Power");
                dynamic parameters = new Dictionary<string,object>();
                parameters.Add("Status",true);
                CommandHistorySchemaHelper.AddParameterCollectionToCommandHistoryItem(command, parameters);
                
                ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(ConfigurationManager.AppSettings["iotHub.ConnectionString"]);
                Trace.TraceInformation("Connect iotHub before send message");
                byte[] commandAsBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(command));
                var notificationMessage = new Message(commandAsBytes);

                notificationMessage.Ack = DeliveryAcknowledgement.Full;
                notificationMessage.MessageId = command.MessageId;

                Task x = Task.Run(()=> AzureRetryHelper.OperationWithBasicRetryAsync( () => 
                     serviceClient.SendAsync(deviceId, notificationMessage)));
                x.Wait();
                Trace.TraceInformation("Connect iotHub after send message " + deviceId + commandAsBytes);
                 await serviceClient.CloseAsync();
          
            return false;
        }
    }
}
