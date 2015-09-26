using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using Kae.Networking.NetBIOS;
using ResourceScope = Kae.Networking.NetBIOS.ResourceScope;
using ResourceType = Kae.Networking.NetBIOS.ResourceType;

namespace Kae.Networking
{
    /// <summary>
    /// Represents a connection to a network share.
    /// </summary>
    sealed public class NetworkShareConnection : IDisposable
    {
        private readonly string _networkName;
        private readonly Mutex _mutex;
        private readonly bool _connected;

        public NetworkShareConnection(string networkSharePath, NetworkCredential credentials)
            : this(networkSharePath, credentials, TimeSpan.FromMilliseconds(1)) 
        { 
        }

        public NetworkShareConnection(string networkSharePath, NetworkCredential credentials, TimeSpan timeout)
        {
            _networkName = networkSharePath;

            bool createdMutex;

            var mutexName = "NCS_" + networkSharePath.ToLowerInvariant().Replace('\\', '_');

            
            _mutex = new Mutex(true, mutexName, out createdMutex);

            if (!createdMutex)
            {
                var signalled = _mutex.WaitOne(TimeSpan.FromMinutes(1));

                if (!signalled)
                    throw new TimeoutException("Share already connected on this machine.  Timed out waiting for existing connection to close.");
            }

            NetResource netResource = new NetResource()
            {
                Scope = ResourceScope.GlobalNetwork,
                Type = ResourceType.Disk,
                DisplayType = ResourceDisplayType.Share,
                RemoteName = networkSharePath
            };

            int result = NativeMethods.WNetAddConnection2(
                netResource,
                credentials.Password,
                credentials.UserName,
                0);

            if (result != 0)
            {
                _mutex.ReleaseMutex();
                _mutex.Dispose();
     
                throw new IOException(string.Format("System error code: {0}", result), result);
            }
            
            _connected = true;
        }

        ~NetworkShareConnection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connected)
                {
                    NativeMethods.WNetCancelConnection2(_networkName, 0, true);
                }

                _mutex.ReleaseMutex();
                _mutex.Dispose();
            }
        }

        private static class NativeMethods
        {
            [DllImport("mpr.dll")]
            internal static extern int WNetAddConnection2(NetResource netResource,
                string password, string username, int flags);

            //[DllImport("mpr.dll", EntryPoint = "WNetAddConnection2W",
            //    CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            //public static extern int WNetAddConnection2(
            //    NetResource netResource, string password, string username, int flags);

            [DllImport("mpr.dll")]
            internal static extern int WNetCancelConnection2(string name, int flags, bool force);
        }

    }
}