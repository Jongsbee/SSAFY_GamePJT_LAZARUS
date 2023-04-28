using System.Net;
using System.Text;
using ServerCore;

namespace Server;

public class ClientSession : PacketSession
{
    public int SessionId { get; set; }
    public GameRoom Room { get; set; }
    public float PosX { get; set; }
    public float PosY { get; set; }
    public float PosZ { get; set; }
    public override void OnConnected(EndPoint endPoint)
    {
        Console.WriteLine($"OnConnected { endPoint }");
        
        Program.Room.Push(() => Program.Room.Enter(this));
    }

    public override void OnRecvPacket(ArraySegment<byte> buffer)
    {
        PacketManager.Instance.OnRecvPacket(this, buffer);
    }

    public override void OnSend(int numOfBytes)
    {
        //Console.WriteLine($"Transferred bytes: {numOfBytes}");
    }

    public override void OnDisconnected(EndPoint endPoint)
    {
        SessionManager.instance.Remove(this);
        if (Room != null)
        {
            GameRoom room = Room;
            room.Push(() =>
            {
                room.Leave(this);
                
            });
            Room = null;
            
        }
            
        Console.WriteLine($"OnDisconnected { endPoint }");
    }
}

