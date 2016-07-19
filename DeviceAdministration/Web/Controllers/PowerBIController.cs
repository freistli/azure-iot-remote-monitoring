using Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Web.Controllers
{
    public class PowerBIController : Controller
    {
        // GET: PowerBI
        [HttpGet]
        [ActionName("ReportData")]
        [RequirePermission(Permission.ViewActions)]
        public ActionResult ReportData()
        {
            return View();
        }
        
        [HttpGet]
        [ActionName("GeoData")]
        [RequirePermission(Permission.ViewActions)]
        public ActionResult GeoData()
        {
            return View();
        }
    }
}