using DummyClient;
using ServerCore;
public class PacketHandler
{
    public static void S_BroadcastEnterGameHandler(PacketSession session, IPacket packet)
    {
        S_BroadcastEnterGame chatPacket = packet as S_BroadcastEnterGame;
        ServerSession serverSession = session as ServerSession;
    }
    
    public static void S_BroadcastLeaveGameHandler(PacketSession session, IPacket packet)
    {
        S_BroadcastLeaveGame chatPacket = packet as S_BroadcastLeaveGame;
        ServerSession serverSession = session as ServerSession;
    }
    
    public static void S_PlayerListHandler(PacketSession session, IPacket packet)
    {
        S_PlayerList chatPacket = packet as S_PlayerList;
        ServerSession serverSession = session as ServerSession;
    }
    
    public static void S_BroadCastMoveHandler(PacketSession session, IPacket packet)
    {
        S_BroadCastMove chatPacket = packet as S_BroadCastMove;
        ServerSession serverSession = session as ServerSession;
    }
}