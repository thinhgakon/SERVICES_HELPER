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
        public static List<ServiceInfo> GetServices(string filter, string searchKey)
        {
            var services = ServiceController.GetServices()
                .Where(s => filter == "ALL" || s.ServiceName.StartsWith(filter))
                .Where(s => s.ServiceName.ToUpper().Contains(searchKey.ToUpper()))
                .Select(s => new ServiceInfo
                {
                    Name = s.ServiceName,
                    Status = s.Status.ToString(),
                    StartType = s.StartType.ToString()
                })
                .ToList();

            return services;
        }
    }
}
