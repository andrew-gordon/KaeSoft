namespace Kae.Networking.NetBIOS
{
    public enum ResourceUsage
    {
        Connectable = 0x00000001,
        Container = 0x00000002,
        NoLocalDevice = 0x00000004,
        Sibling = 0x00000008,
        Attached = 0x00000010,
        All = (Connectable | Container | Attached),
    };
}