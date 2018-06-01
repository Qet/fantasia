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

        private void echoData(){
            for (var c in clients){
                byte [] 
                NetworkStream stream = c.GetStream()
                stream.Read()
            }

//  NetworkStream stream = client.GetStream();

            //         int i;

            //         // Loop to receive all the data sent by the client.
            //         while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            //         {
            //             // Translate data bytes to a ASCII string.
            //             data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            //             Console.WriteLine("Received: {0}", data);

            //             // Process the data sent by the client.
            //             data = data.ToUpper();

            //             byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

            //             // Send back a response.
            //             stream.Write(msg, 0, msg.Length);
            //             Console.WriteLine("Sent: {0}", data);
            //         }


        }

        public void runOnce() {
            makePendingConnections();
            cleanUpDisconnectedClients();
        }

    }
}