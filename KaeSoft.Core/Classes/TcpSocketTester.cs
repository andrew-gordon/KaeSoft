using System;
using System.Net.Sockets;

namespace Andy.Lib.Classes
{
    public static class SocketTester
    {
        public static bool ConnectTest(string address, int port, TimeSpan timeout, ProtocolType protocolType = ProtocolType.Tcp)
        {
            try
            {
                var result = false;
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, protocolType))
                {
                    try
                    {
                        var asyncResult = socket.BeginConnect(address, port, null, null);
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
