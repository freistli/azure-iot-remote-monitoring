using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.DeviceSchema;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.Models;
using System.Net;
using System.IO;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.Factory
{
    public static class SampleDeviceFactory
    {
        public const string OBJECT_TYPE_DEVICE_INFO = "DeviceInfo";

        public const string VERSION_1_0 = "1.0";

        private const int MAX_COMMANDS_SUPPORTED = 7;

        private const bool IS_SIMULATED_DEVICE = true;

        private static List<string> DefaultDeviceNames = new List<string>{
            "SampleDevice001", 
            "SampleDevice002", 
            "SampleDevice003", 
            "SampleDevice004"
        };

        private class Location
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            
            public Location(double latitude, double longitude)
            {
                Latitude = latitude;
                Longitude = longitude;
            }

        }

        private static List<Location> _possibleDeviceLocations = new List<Location>{
            new Location(47.659159, -122.141515),  // Microsoft Red West Campus, Building A
            new Location(47.593307, -122.332165),  // 800 Occidental Ave S, Seattle, WA 98134
            new Location(47.617025, -122.191285),  // 11111 NE 8th St, Bellevue, WA 98004
            new Location(47.583582, -122.130622),  // 3003 160th Ave SE Bellevue, WA 98008
            new Location(39.9, 116.26), //BeiJin
            new Location(39.1, 117.1), //TianJin
            new Location(31.03, 121.38),  //MinHang
            new Location(-0.24, 115.27),//indonesia
            new Location(3.100255, 101.652334),
            new Location(28.713040, 76.867178),
            new Location(30.848759, 102.707022),
            new Location (40.131263, 116.066397)

        };

        public static dynamic GetSampleSimulatedDevice(string deviceId, string key)
        {
            dynamic device = DeviceSchemaHelper.BuildDeviceStructure(deviceId, true, null);

            AssignDeviceProperties(deviceId, device);
            device.ObjectType = OBJECT_TYPE_DEVICE_INFO;
            device.Version = VERSION_1_0;
            device.IsSimulatedDevice = IS_SIMULATED_DEVICE;

            AssignCommands(device);

            return device;
        }

        public static dynamic GetSampleDevice(Random randomNumber, SecurityKeys keys)
        {
            string deviceId = 
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Air-DEV-{0}C-{1}LK",
                    MAX_COMMANDS_SUPPORTED, 
                    randomNumber.Next(999999)
                   );

            dynamic device = DeviceSchemaHelper.BuildDeviceStructure(deviceId, false, null);
            device.ObjectName = "IoT Device Description";
            //specific for test
            device.IsSimulatedDevice = IS_SIMULATED_DEVICE;

            AssignDeviceProperties(deviceId, device);
            AssignCommands(device);

            return device;
        }
        private static int RealRandom()
        {
            WebRequest request = WebRequest.Create("https://www.random.org/integers/?num=1&min=-100&max=100&col=1&base=10&format=plain&rnd=new");

            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return Convert.ToInt32(responseFromServer);
        }
        private static void AssignDeviceProperties(string deviceId, dynamic device)
        {
            dynamic deviceProperties = DeviceSchemaHelper.GetDeviceProperties(device);
            deviceProperties.HubEnabledState = true;
            deviceProperties.Manufacturer = "Contoso Inc.";
            deviceProperties.ModelNumber = "MD-" + GetIntBasedOnString(deviceId + "ModelNumber", 1000);
            deviceProperties.SerialNumber = "SER" + GetIntBasedOnString(deviceId + "SerialNumber", 10000);
            deviceProperties.FirmwareVersion = "1." + GetIntBasedOnString(deviceId + "FirmwareVersion", 100);
            deviceProperties.Platform = "Plat-" + GetIntBasedOnString(deviceId + "Platform", 100);
            deviceProperties.Processor = "i3-" + GetIntBasedOnString(deviceId + "Processor", 10000);
            deviceProperties.InstalledRAM = GetIntBasedOnString(deviceId + "InstalledRAM", 100) + " MB";
            
            // Choose a location between the 3 above and set Lat and Long for device properties
            int chosenLocation = GetIntBasedOnString(deviceId + "Location", _possibleDeviceLocations.Count);
            deviceProperties.Latitude = _possibleDeviceLocations[chosenLocation].Latitude + (double)RealRandom()/1000.0;
            deviceProperties.Longitude = _possibleDeviceLocations[chosenLocation].Longitude + (double)RealRandom()/1000.0;
        }

        private static int GetIntBasedOnString(string input, int maxValueExclusive)
        {
            int hash = input.GetHashCode();

            //Keep the result positive
            if(hash < 0)
            {
                hash = -hash;
            }

            return hash % maxValueExclusive;
        }

        private static void AssignCommands(dynamic device)
        {
            dynamic command = CommandSchemaHelper.CreateNewCommand("PingDevice");
            CommandSchemaHelper.AddCommandToDevice(device, command);
            
            command = CommandSchemaHelper.CreateNewCommand("StartTelemetry");
            CommandSchemaHelper.AddCommandToDevice(device, command);
            
            command = CommandSchemaHelper.CreateNewCommand("StopTelemetry");
            CommandSchemaHelper.AddCommandToDevice(device, command);

            command = CommandSchemaHelper.CreateNewCommand("Power");
            CommandSchemaHelper.DefineNewParameterOnCommand(command, "Status", "boolean");
            CommandSchemaHelper.AddCommandToDevice(device, command);
            
            command = CommandSchemaHelper.CreateNewCommand("ChangeSetPointTemp");
            CommandSchemaHelper.DefineNewParameterOnCommand(command, "SetPointTemp", "double");
            CommandSchemaHelper.AddCommandToDevice(device, command);
            
            command = CommandSchemaHelper.CreateNewCommand("DiagnosticTelemetry");
            CommandSchemaHelper.DefineNewParameterOnCommand(command, "Active", "boolean");
            CommandSchemaHelper.AddCommandToDevice(device, command);
            
            command = CommandSchemaHelper.CreateNewCommand("ChangeDeviceState");
            CommandSchemaHelper.DefineNewParameterOnCommand(command, "DeviceState", "string");
            CommandSchemaHelper.AddCommandToDevice(device, command);
        }

        public static List<string> GetDefaultDeviceNames()
        {
            long milliTime = DateTime.Now.Millisecond;
            return DefaultDeviceNames.Select(r => string.Concat(r, "_" + milliTime)).ToList();
        }
    }
}
