using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Kae.Networking
{
    static class AddressResolutionProtocol
    {
        /// <summary>
        /// Gets the MAC address for the host at the given IP address.
        /// </summary>
        /// <param name="destination">Destination IP address</param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        static public string GetMACAddress(IPAddress destination)
        {
            if (destination.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentException("The destination address must be IPv4");

            byte[] macAddressBuffer = new byte[6];

            uint length = (uint)macAddressBuffer.Length;

            if (UnsafeNativeMethods.SendARP((int)destination.Address, 0, macAddressBuffer, ref length) != 0)
                throw new InvalidOperationException("SendARP failed.");

            var str = new string[(int)length];

            for (var i = 0; i < length; i++)
                str[i] = macAddressBuffer[i].ToString("x2");

            return string.Join(":", str);
        }


        private static class UnsafeNativeMethods
        {
            [DllImport("iphlpapi.dll", ExactSpelling = true)]
            public static extern int SendARP(int destIp, int srcIp, byte[] pMacAddr, ref uint phyAddrLen);
        }

    }
}
