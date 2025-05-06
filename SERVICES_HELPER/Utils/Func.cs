using SERVICES_HELPER.Models;
using System;
using System.ServiceProcess;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES_HELPER.Utils
{
    public static class Func
    {
        public static List<ServiceInfo> GetServices(string searchKey)
        {
            var services = ServiceController.GetServices()
                .Where(s => s.ServiceName.StartsWith("PROD_") || s.ServiceName.StartsWith("XHTD_"))
                .Where(s => s.ServiceName.ToUpper().Contains(searchKey.ToUpper()))
                .Select(s => new ServiceInfo
                {
                    Name = s.ServiceName,
                    Status = s.Status.ToString()
                })
                .ToList();

            return services;
        }
    }
}
