using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Kae.Networking.Interop;

namespace Kae.Networking
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public static class NetServerEnum
    {
        // ReSharper disable UnusedMember.Local
        // ReSharper disable InconsistentNaming
        private const int ERROR_ACCESS_DENIED = 5;
        private const int ERROR_INVALID_PARAMETER = 87;
        private const int ERROR_MORE_DATA = 234;
        private const int ERROR_NOT_SUPPORTED = 50;
        private const int ERROR_NO_BROWSER_SERVERS_FOUND = 6118;
        private const int ERROR_SUCCESS = 0;
        private const int NERR_ServerNotStarted = 2114;
        private const int NERR_ServiceNotInstalled = 2184;
        private const int NERR_WkstaNotStarted = 2138;
        // ReSharper restore UnusedMember.Local
        // ReSharper restore InconsistentNaming


        /// <summary>
        /// Finds computers of the given serverType on the named domain.
        /// </summary>
        /// <param name="serverType">Server type filter</param>
        /// <param name="domain">The domain in which to search for computers</param>
        public static IEnumerable<SERVER_INFO_101> Find(SV_101_TYPES serverType, string domain)
        {
            var entriesRead = 0;
            var totalEntries = 0;

            do
            {
                IntPtr buffer;

                // http://msdn.microsoft.com/en-us/library/aa370623%28VS.85%29.aspx

                var ret = UnsafeNativeMethods.NetServerEnum(null, 101, out buffer, -1,
                    ref entriesRead, ref totalEntries, serverType, domain, IntPtr.Zero);

                // if NetServerEnum returned any data....
                if (ret == ERROR_SUCCESS || ret == ERROR_MORE_DATA || entriesRead > 0)
                {
                    var ptr = buffer;

                    for (var i = 0; i < entriesRead; i++)
                    {
                        // cast pointer to a SERVER_INFO_101 structure
                        var server = (SERVER_INFO_101)Marshal.PtrToStructure(ptr, typeof(SERVER_INFO_101));

                        //Cast the pointer to a ulong so this addition will work on 32-bit or 64-bit systems.
                        ptr = (IntPtr)((ulong)ptr + (ulong)Marshal.SizeOf(typeof(SERVER_INFO_101)));

                        yield return server;
                    }
                }

                // free the buffer
                UnsafeNativeMethods.NetApiBufferFree(buffer);

            }
            while (entriesRead < totalEntries && entriesRead != 0);
        }

        private static class UnsafeNativeMethods
        {
            #region Methods

            // Frees buffer created by NetServerEnum
            [DllImport("netapi32.dll")]
            internal static extern int NetApiBufferFree(
                IntPtr buf);

            // enumerates network computers
            [DllImport("Netapi32", CharSet = CharSet.Unicode)]
            internal static extern int NetServerEnum([MarshalAs(UnmanagedType.LPWStr)]string servername,
                int level,
                out IntPtr bufptr,
                int prefmaxlen,
                ref int entriesread,
                ref int totalentries,
                SV_101_TYPES servertype,
                [MarshalAs(UnmanagedType.LPWStr)]string domain,
                IntPtr resumeHandle);

            #endregion Methods

        }
    }
}