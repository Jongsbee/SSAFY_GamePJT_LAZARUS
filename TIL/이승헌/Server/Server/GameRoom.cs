namespace Server;

public class GameRoom
{
    private List<ClientSession> _sessions = new List<ClientSession>();
    private object _lock = new object();

    public void Broadcast(ClientSession session, string chat)
    {
        S_Chat packet = new S_Chat();
        packet.playerId = session.SessionId;
        packet.chat = chat;
        ArraySegment<byte> segment = packet.Write();

        lock (_lock)
        {
            foreach (var s in _sessions)
                s.Send(segment);
        }
        
    }
    public void Enter(ClientSession session)
    {
        lock (_lock)
        {
            _sessions.Add(session);
            session.Room = this;
        }
        
    }

    public void Leave(ClientSession session)
    {
        lock (_lock)
        {
            _sessions.Remove(session);
        }
        
    }
}