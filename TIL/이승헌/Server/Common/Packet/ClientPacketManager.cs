
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
 
        _onRecv.Add((ushort)PacketId.S_BroadcastEnterGame, MakePacket<S_BroadcastEnterGame>);
        _handler.Add((ushort)PacketId.S_BroadcastEnterGame, PacketHandler.S_BroadcastEnterGameHandler);
 
        _onRecv.Add((ushort)PacketId.S_BroadcastLeaveGame, MakePacket<S_BroadcastLeaveGame>);
        _handler.Add((ushort)PacketId.S_BroadcastLeaveGame, PacketHandler.S_BroadcastLeaveGameHandler);
 
        _onRecv.Add((ushort)PacketId.S_PlayerList, MakePacket<S_PlayerList>);
        _handler.Add((ushort)PacketId.S_PlayerList, PacketHandler.S_PlayerListHandler);
 
        _onRecv.Add((ushort)PacketId.S_BroadCastMove, MakePacket<S_BroadCastMove>);
        _handler.Add((ushort)PacketId.S_BroadCastMove, PacketHandler.S_BroadCastMoveHandler);

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
