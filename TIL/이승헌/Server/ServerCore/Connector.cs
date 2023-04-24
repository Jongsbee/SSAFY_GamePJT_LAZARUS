using System.Net;
using System.Net.Sockets;

namespace ServerCore;

public class Connector
{
    private Func<Session> _sessionFactory;
    
    public void Connect(IPEndPoint endPoint, Func<Session> sessionFactory)
    {
        //휴대폰 설정
        Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        _sessionFactory = sessionFactory;
        
        SocketAsyncEventArgs args = new SocketAsyncEventArgs();
        args.Completed += OnConnectCompleted;
        args.RemoteEndPoint = endPoint;
        args.UserToken = socket;
        
        RegisterConnect(args);
    }

    void RegisterConnect(SocketAsyncEventArgs args)
    {
        var socket = args.UserToken as Socket;
        if (socket == null)
            return;
        var pending = socket.ConnectAsync(args);
        if (pending == false)
            OnConnectCompleted(null, args);
    }

    void OnConnectCompleted(object sender, SocketAsyncEventArgs args)
    {
        if (args.SocketError == SocketError.Success)
        {
            Session session = _sessionFactory.Invoke();
            session.Start(args.ConnectSocket);
            session.OnConnected(args.RemoteEndPoint);
        }
        else
        {
            Console.WriteLine($"OnSocketConnectedFail: {args.SocketError}");
        }
    }
}