using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure
{
    static public class DiagLog
    {
        static public void logging(string type,string text)
        {
          //  if (HttpContext.Current.IsDebuggingEnabled)
            {
                System.Diagnostics.Trace.TraceInformation(type+"_"+text);              
            }
            return;
        }
    }
}
