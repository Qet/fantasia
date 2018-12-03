using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace ports {
    public class SocketManager {
        public SocketManager(IRealm realmInterface) {
            sockets = new List<TCPSocket>();
            packetisers = new List<Packetiser>();
            codecs = new List<Codec>();
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 54540);
            listener.Start();
            portID_Counter = 0;
            this.realm = realmInterface;
        }

        ~SocketManager(){
            listener.Stop();
        }

        public List<TCPSocket> sockets { get; private set; }
        public List<Packetiser> packetisers {get; private set;}

        public List<Codec> codecs {get ; private set;}
        private IRealm realm;
        private TcpListener listener;
        private int portID_Counter;

        public void runOnce() {
            makePendingConnections();
            cleanUpDisconnectedClients();
            runSockets();
            Console.WriteLine("Num clients connected: {0}", sockets.Count);
        }

        private void cleanUpDisconnectedClients() {

            List<TCPSocket> socketsToRemove = new List<TCPSocket>();

            foreach (var c in sockets) {
                
                if (c.client.Client.Poll(1, SelectMode.SelectRead)
                 && !c.client.GetStream().DataAvailable) { 
                     // Check if connection is closed: When using SelectRead, Poll() will return true only if:
                     // (1) We are listening; which we are not. 
                     // (2) New data is available; so we check networkStream.DataAvailable AFTER calling Poll(), 
                     //     because it could change during Poll()
                     // (3) The connection was indeed closed. 
                    Console.WriteLine("Client disconnected");
                    c.client.Close();
                    socketsToRemove.Add(c);
                }
            } 
            foreach (var c in socketsToRemove){
                sockets.Remove(c);
            }

        }

        private void createNewStack(TcpClient client, int portID){
            ICommand user = realm.AddUser();
            Codec newCodec = new Codec(user);
            Packetiser newPacketiser = new Packetiser(newCodec, "\r\n");
            TCPSocket newSocket = new TCPSocket(client, newPacketiser);

            packetisers.Add(newPacketiser);
            sockets.Add(newSocket);
            codecs.Add(newCodec);
        }

        private void makePendingConnections() {
            if (listener.Pending()) {
                createNewStack(listener.AcceptTcpClient(), getUniquePortID());
            }
        }

        private int getUniquePortID(){
            return portID_Counter++;
        }

        private void runSockets(){
            foreach (var s in sockets){
                s.run();
            }
        }

    }
}