using System.Net;
using System.Text;
using ServerCore;

namespace DummyClient;

class ServerSession : Session
{
    public override void OnConnected(EndPoint endPoint)
    {
        Console.WriteLine($"OnConnected {endPoint}");

        C_PlayerInfoReq pakcet = new C_PlayerInfoReq() { playerId = 1001, name = "ABCD" };
        var skill = new C_PlayerInfoReq.Skill() { id = 101, level = 1, duration = 3.0f };
        skill.attributes.Add(new C_PlayerInfoReq.Skill.Attribute() { att = 77});
        pakcet.skills.Add(skill);
        pakcet.skills.Add(new C_PlayerInfoReq.Skill(){id = 102, level = 2, duration = 4.0f});
        pakcet.skills.Add(new C_PlayerInfoReq.Skill(){id = 103, level = 3, duration = 5.0f});
        pakcet.skills.Add(new C_PlayerInfoReq.Skill(){id = 104, level = 4, duration = 6.0f});
        
        // 보낸다
        //for (int i = 0; i < 5; i++)
        {
            ArraySegment<byte> s = pakcet.Write();
            if(s != null)
                Send(s);
        }
    }

    public override int OnRecv(ArraySegment<byte> buffer)
    {
        var recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
        Console.WriteLine($"[From Server] {recvData}");
        return buffer.Count;
    }

    public override void OnSend(int numOfBytes)
    {
        Console.WriteLine($"Transferred bytes: {numOfBytes}");
    }

    public override void OnDisconnected(EndPoint endPoint)
    {
        Console.WriteLine($"OnDisconnected {endPoint}");
    }
}