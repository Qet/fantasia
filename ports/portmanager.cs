using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace ports {
    public class PortManager {
        public PortManager() {
            clients = new List<TcpClient>();
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 54540);
            listener.Start();
        }

        public List<TcpClient> clients { get; private set; }

        private TcpListener listener;

        private void cleanUpDisconnectedClients() {

            List<TcpClient> toRemove = new List<TcpClient>();

            foreach (var c in clients) {
                if (!c.Connected) {
                    toRemove.Add(c);
                }
            }

            foreach (var r in toRemove) {
                clients.Remove(r);
            }
        }

        private void makePendingConnections() {
            // make any pending connections.      
            if (listener.Pending()) {
                clients.Add(listener.AcceptTcpClient());
            }
        }

        private void echoData() {
            foreach (var c in clients) {
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
            foreach (var c in clients) {
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