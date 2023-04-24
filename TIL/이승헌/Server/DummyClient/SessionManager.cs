namespace DummyClient;

public class SessionManager
{
    static SessionManager _session = new SessionManager();

    public static SessionManager Instance
    {
        get { return _session; }
    }

    private List<ServerSession> _sessions = new List<ServerSession>();
    private object _lock = new object();

    public void SendForEach()
    {
        lock (_lock)
        {
            foreach (var session in _sessions)
            {
                C_Chat chatPacket = new C_Chat();
                chatPacket.chat = $"Hello Server !";
                ArraySegment<byte> segment = chatPacket.Write();
                
                session.Send(segment);
            }
        }
    }

    public ServerSession Generate()
    {
        lock (_lock)
        {
            ServerSession session = new ServerSession();
            _sessions.Add(session);
            return session;
        }
    }
}