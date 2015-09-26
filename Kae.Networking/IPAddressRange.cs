using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Kae.Networking
{
    /// <summary>
    /// Range of IP addresses
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class IPAddressRange
    {
        private readonly IPAddress _start;
        private readonly IPAddress _end;

        public IPAddressRange(IPAddress start, IPAddress end)
        {
            _start = start;
            _end = end;
        }

        public IPAddressRange(IPAddress address, byte bits)
        {
            // The ~ operator performs a bitwise complement operation on its operand, 
            // which has the effect of reversing each bit.

            var mask = ~(uint.MaxValue >> bits);

            // Convert the IP address to bytes.
            var ipBytes = address.GetAddressBytes();

            // BitConverter gives bytes in opposite order to GetAddressBytes().
            var maskBytes = BitConverter.GetBytes(mask).Reverse().ToArray();

            var startIpBytes = new byte[ipBytes.Length];
            var endIpBytes = new byte[ipBytes.Length];

            // Calculate the bytes of the start and end IP addresses.
            for (int i = 0; i < ipBytes.Length; i++)
            {
                startIpBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
                endIpBytes[i] = (byte)(ipBytes[i] | ~maskBytes[i]);
            }

            // Convert the bytes to IP addresses.
            _start = new IPAddress(startIpBytes);
            _end = new IPAddress(endIpBytes);
        }

        public IEnumerable<IPAddress> Addresses
        {
            get
            {
                var start = IPBytesToUint(_start.GetAddressBytes());
                var end = IPBytesToUint(_end.GetAddressBytes());

                while (start <= end)
                {
                    var bytes = BitConverter.GetBytes(start).Reverse().ToArray(); ;

                    // Don't return the broadcast address (.255) or the network (.0)
                    if (bytes[3] != 255 && bytes[3] != 0)
                    {
                        //long addressValue = (uint)BitConverter.ToInt32(bytes, 0);
                        yield return new IPAddress(ReverseBytesArray(start));
                    }

                    start++;
                }
            }
        }

        /* reverse byte order in array */
        static private uint ReverseBytesArray(uint ip)
        {
            byte[] bytes = BitConverter.GetBytes(ip);
            bytes = bytes.Reverse().ToArray();
            return (uint)BitConverter.ToInt32(bytes, 0);
        }


        /* Convert bytes array to 32 bit long value */
        // ReSharper disable once InconsistentNaming
        static private uint IPBytesToUint(byte[] ipBytes)
        {
            var byteConverter = new ByteConverter();
            uint ipUint = 0;

            var shift = 24; // indicates number of bits left for shifting

            foreach (byte b in ipBytes)
            {
                if (ipUint == 0)
                {
                    ipUint = (uint)byteConverter.ConvertTo(b, typeof(uint)) << shift;
                    shift -= 8;
                    continue;
                }

                if (shift >= 8)
                    ipUint += (uint)byteConverter.ConvertTo(b, typeof(uint)) << shift;
                else
                    ipUint += (uint)byteConverter.ConvertTo(b, typeof(uint));

                shift -= 8;
            }

            return ipUint;
        }

        public bool Contains(IPAddress address)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                var start = IPBytesToUint(_start.GetAddressBytes());
                var end = IPBytesToUint(_start.GetAddressBytes());
                var test = IPBytesToUint(address.GetAddressBytes());
                return (test >= start) && (test <= end);
            }
            else
                throw new NotImplementedException("IPAddressRange.Contains(IPAddress address) is not implemented for IPv6");
        }
    }
}