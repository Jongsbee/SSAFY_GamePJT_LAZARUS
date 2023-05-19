
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

    private Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>> _makeFunc =
        new Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>>();

    private Dictionary<ushort, Action<PacketSession, IPacket>> _handler =
        new Dictionary<ushort, Action<PacketSession, IPacket>>();

    public void Register()
    {
 
        _makeFunc.Add((ushort)PacketId.S_BroadcastEnterGame, MakePacket<S_BroadcastEnterGame>);
        _handler.Add((ushort)PacketId.S_BroadcastEnterGame, PacketHandler.S_BroadcastEnterGameHandler);
 
        _makeFunc.Add((ushort)PacketId.S_BroadcastLeaveGame, MakePacket<S_BroadcastLeaveGame>);
        _handler.Add((ushort)PacketId.S_BroadcastLeaveGame, PacketHandler.S_BroadcastLeaveGameHandler);
 
        _makeFunc.Add((ushort)PacketId.S_PlayerList, MakePacket<S_PlayerList>);
        _handler.Add((ushort)PacketId.S_PlayerList, PacketHandler.S_PlayerListHandler);
 
        _makeFunc.Add((ushort)PacketId.S_BroadCastMove, MakePacket<S_BroadCastMove>);
        _handler.Add((ushort)PacketId.S_BroadCastMove, PacketHandler.S_BroadCastMoveHandler);

    }


    public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer,  Action<PacketSession, IPacket> onRecvCallback = null)
    {
        ushort count = 0;

        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
        count += 2;
        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += 2;

        Func<PacketSession, ArraySegment<byte>, IPacket> func = null;
        if (_makeFunc.TryGetValue(id, out func))
        {
            IPacket packet = func.Invoke(session, buffer);
            if(onRecvCallback != null)
                onRecvCallback.Invoke(session, packet);
            else
                HandlePacket(session, packet);
            
        }
    }

    T MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket, new()
    {
        T pkt = new T();
        pkt.Read(buffer);
        return pkt;

    }

    public void HandlePacket(PacketSession session, IPacket packet)
    {
        Action<PacketSession, IPacket> action = null;
        if (_handler.TryGetValue(packet.Protocol, out action))
            action.Invoke(session, packet);

    }
    
}
