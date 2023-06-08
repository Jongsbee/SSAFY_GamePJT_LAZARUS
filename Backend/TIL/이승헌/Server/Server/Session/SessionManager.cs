namespace Server;

public class SessionManager
{
    private static SessionManager _session = new SessionManager();

    public static SessionManager instance
    {
        get { return _session; }
    }

    private int _sessionId = 0;
    private Dictionary<int, ClientSession> _sessions = new Dictionary<int, ClientSession>();
    private object _lock = new object();

    public ClientSession Generate()
    {
        lock (_lock)
        {
            int sessionId = ++_sessionId;

            ClientSession session = new ClientSession();
            session.SessionId = sessionId;
            _sessions.Add(sessionId, session);

            Console.WriteLine($"Connected : {sessionId}");

            return session;
        }
    }

    public ClientSession Find(int id)
    {
        lock (_lock)
        {
            ClientSession session = null;
            _sessions.TryGetValue(id, out session);
            return session;
        }
    }

    public void Remove(ClientSession session)
    {
        lock (_lock)
        {
            _sessions.Remove(session.SessionId);
        }
    }
}