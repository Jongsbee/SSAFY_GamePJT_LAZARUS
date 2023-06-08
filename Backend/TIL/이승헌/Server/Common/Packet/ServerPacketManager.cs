
using ServerCore;

public class PacketManager
{
    #region Singleton

    private static PacketManager _instance = new PacketManager();

    public static PacketManager Instance
    {
        get
        {
            return _instance;
        }
    }

    #endregion

    PacketManager()
    {
        Register();
    }

    private Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>> _onRecv =
        new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>>();

    private Dictionary<ushort, Action<PacketSession, IPacket>> _handler =
        new Dictionary<ushort, Action<PacketSession, IPacket>>();

    public void Register()
    {
 
        _onRecv.Add((ushort)PacketId.C_LeaveGame, MakePacket<C_LeaveGame>);
        _handler.Add((ushort)PacketId.C_LeaveGame, PacketHandler.C_LeaveGameHandler);
 
        _onRecv.Add((ushort)PacketId.C_Move, MakePacket<C_Move>);
        _handler.Add((ushort)PacketId.C_Move, PacketHandler.C_MoveHandler);

    }


    public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
    {
        ushort count = 0;

        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
        count += 2;
        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += 2;

        Action<PacketSession, ArraySegment<byte>> action = null;
        if(_onRecv.TryGetValue(id, out action))
            action.Invoke(session, buffer);
    }

    void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket, new()
    {
            T pkt = new T();
            pkt.Read(buffer);

            Action<PacketSession, IPacket> action = null;
            if(_handler.TryGetValue(pkt.Protocol, out action))
                action.Invoke(session, pkt);

    }
    
}
