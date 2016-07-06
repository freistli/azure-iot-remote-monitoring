namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Models
{
    public class DeviceRuleBlobEntity
    {
        public DeviceRuleBlobEntity(string deviceId)
        {
            DeviceId = deviceId;
        }

        public string DeviceId { get; private set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? PM25 { get; set; }
        public double? Value { get; set; }
        public double? ExternalTemperature { get; set; }

        public string Operator { get; set; }
        public string TemperatureRuleOutput { get; set; }
        public string HumidityRuleOutput { get; set; }
        public string RuleOutput { get; set; }
    }
}
