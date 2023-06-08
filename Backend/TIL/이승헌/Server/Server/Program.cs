using System.Net;
using ServerCore;

namespace Server // Note: actual namespace depends on the project name.
{
    
    internal class Program
    {
        private static Listener _listener = new Listener();
        public static GameRoom Room = new GameRoom();

        static void FlushRoom()
        {
            Room.Push(() => Room.Flush());
            JobTimer.Instance.Push(FlushRoom, 250);
        }
        
        static void Main(string[] args)
        {
            // DNS(Domain name system) 사용
            // 서버주소를 하드코딩하는건 좋지 못함 => 도메인 등록하는것이 좋다 ( 관리 용이 )
            string host = Dns.GetHostName();
            var ipHost = Dns.GetHostEntry(host);
            var ipAddr = ipHost.AddressList[0];
            var endPoint = new IPEndPoint(ipAddr, 7777);

            _listener.init(endPoint, () =>
            {
                return SessionManager.instance.Generate();
            });
            Console.WriteLine("Listening.....");
            
            //FlushRoom();
            JobTimer.Instance.Push(FlushRoom);

            while (true)
            {
                JobTimer.Instance.Flush();
            }
        }
    }
}