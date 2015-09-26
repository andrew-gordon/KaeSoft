using System.Runtime.InteropServices;

namespace Kae.Networking.NetBIOS
{
    [StructLayout(LayoutKind.Sequential)]
    public class NetResource
    {
        public ResourceScope Scope = 0;
        public ResourceType Type = 0;
        public ResourceDisplayType DisplayType = ResourceDisplayType.Generic;
        public ResourceUsage Usage = ResourceUsage.All;
        public string LocalName = null;
        public string RemoteName = null;
        public string Comment = null;
        public string Provider = null;
    };
}