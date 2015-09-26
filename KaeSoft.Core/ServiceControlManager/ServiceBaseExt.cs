using System;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace KaeSoft.Core.ServiceControlManager
{
    public class ServiceBaseExt : ServiceBase
    {
        [DllImport("advapi32.dll", SetLastError=true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        // https://msdn.microsoft.com/en-us/library/zt39148a%28v=vs.110%29.aspx

        // Services report their status to the Service Control Manager, so that users can tell whether 
        // a service is functioning correctly. By default, services that inherit from ServiceBase report 
        // a limited set of status settings, including Stopped, Paused, and Running. If a service takes 
        // a little while to start up, it might be helpful to report a Start Pending status. You can also 
        // implement the Start Pending and Stop Pending status settings by adding code that calls into the 
        // Windows SetServiceStatus function.

        public void SetStartPendingStatus(long waitHint = 100000)
        {
            ServiceStatus serviceStatus = new ServiceStatus
            {
                dwCurrentState = ServiceState.SERVICE_START_PENDING,
                dwWaitHint = waitHint
            };
            SetServiceStatus(ServiceHandle, ref serviceStatus);
        }

        public void SetRunningStatus()
        {
            ServiceStatus serviceStatus = new ServiceStatus {dwCurrentState = ServiceState.SERVICE_RUNNING};
            SetServiceStatus(ServiceHandle, ref serviceStatus);
        }
    }
}
