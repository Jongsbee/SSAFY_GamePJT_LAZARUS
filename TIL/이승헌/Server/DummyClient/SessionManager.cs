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
    private Random _rand = new Random();

    public void SendForEach()
    {
        lock (_lock)
        {
            foreach (var session in _sessions)
            {
                C_Move movePacket = new C_Move();
                movePacket.posX = _rand.Next(-50, 50);
                movePacket.posY = 0;
                movePacket.posZ = _rand.Next(-50, 50);
                session.Send(movePacket.Write());
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