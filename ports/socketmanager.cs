using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace ports {
    class SocketManager {
        public SocketManager(ICommand commandInterface) {
            sockets = new List<TCPSocket>();
            packetisers = new List<Packetiser>();
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 54540);
            listener.Start();
            portID_Counter = 0;
            this.commands = commandInterface;
            codec = new Codec(commands);
        }

        public List<TCPSocket> sockets { get; private set; }
        public List<Packetiser> packetisers {get; private set;}

        private ICommand commands;
        private Codec codec;
        private TcpListener listener;
        private int portID_Counter;

        private void cleanUpDisconnectedClients() {

            List<TCPSocket> toRemove = new List<TCPSocket>();

            foreach (var c in sockets) {
                if (!c.client.Connected) {
                    toRemove.Add(c);
                }
            }

            foreach (var r in toRemove) {
                sockets.Remove(r);
            }
        }

        private void createNewStack(TcpClient client, int portID){
            Packetiser newPacketiser = new Packetiser(codec);
            TCPSocket newSocket = new TCPSocket(portID, listener.AcceptTcpClient(), newPacketiser);

            packetisers.Add(newPacketiser);
            sockets.Add(newSocket);
        }

        private void makePendingConnections() {
            if (listener.Pending()) {
                createNewStack(listener.AcceptTcpClient(), getUniquePortID());
            }
        }

        private int getUniquePortID(){
            return portID_Counter++;
        }

        private void echoData() {
            foreach (var c in sockets) {
                if (c.Connected) {

                    Byte[] bytes = new Byte[200];
                    NetworkStream stream = c.GetStream();

                    if (stream.DataAvailable) {
                        int i = stream.Read(bytes, 0, bytes.Length);
                        String data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        data = data.ToUpper();
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }
                }
            }
        }

        private int numClientsConnected() {
            int i = 0;
            foreach (var c in sockets) {
                if (c.Connected) {
                    i++;
                }
            }
            return i;
        }

        public void runOnce() {
            makePendingConnections();
            cleanUpDisconnectedClients();
            echoData();
            Console.WriteLine("Num clients connected: {0}", numClientsConnected());
        }

    }
}