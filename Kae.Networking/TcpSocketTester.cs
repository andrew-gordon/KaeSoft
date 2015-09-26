using System;
using System.Net.Sockets;

namespace Kae.Networking
{
    public static class SocketTester
    {
        public static bool ConnectTest(string host, int port, TimeSpan timeout,
            ProtocolType protocolType = ProtocolType.Tcp)
        {
            try
            {
                var result = false;
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, protocolType))
                {
                    try
                    {
                        var asyncResult = socket.BeginConnect(host, port, null, null);
                        result = asyncResult.AsyncWaitHandle.WaitOne(timeout, true);
                        socket.Close();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}
