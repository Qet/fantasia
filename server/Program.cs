using System;
using world;
using System.Net.Sockets;
using System.Net;

using ports;

namespace server
{
    public class DummyCommandHandler : ICommand{
        public void move(Directions directions){ Console.WriteLine("Moving: " + directions.ToString());}
    }

    class Program
    {
        static void Main(string[] args)
        {
            Realm realm = new Realm();
            SocketManager socketManager = new SocketManager(realm);
            while(true){
                socketManager.runOnce();
                System.Threading.Thread.Sleep(1000);
                Console.Write(".");
            } 
        }
    }
}

