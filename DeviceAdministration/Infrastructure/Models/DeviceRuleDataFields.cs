using System.Collections.Generic;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Models
{
    public static class DeviceRuleDataFields
    {
        public static string Temperature
        { 
            get 
            { 
                return "Temperature"; 
            } 
        }

        public static string Humidity
        {
            get
            {
                return "Humidity";
            }
        }

        public static string PM25
        {
            get
            {
                return "PM25";
            }
        }

        public static string PM10
        {
            get
            {
                return "PM10";
            }
        }
        public static string ExternalTemp
        {
            get
            {
                return "ExternalTemperature";
            }
        }
        private static List<string> _availableDataFields = new List<string>
        {
            Temperature, Humidity, PM25, PM10, ExternalTemp
        };

        public static List<string> GetListOfAvailableDataFields()
        {
            return _availableDataFields;
        }
    }
}
