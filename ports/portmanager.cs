using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

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

    public void runOnce() {
        makePendingConnections();
        cleanUpDisconnectedClients();
    }

}