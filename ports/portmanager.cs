using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

public class PortManager
{
    public PortManager()
    {
        clients = new List<TcpClient>();
        listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 54540);
        listener.Start();
    }    

    public List<TcpClient> clients{ get; private set;}

    private TcpListener listener;

    public void runOnce()
    {
        // make any pending connections.      
        if (listener.Pending())
        {
            clients.Add(listener.AcceptTcpClient());
        }
    }



}