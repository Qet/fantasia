using System.Net.Sockets;
using System.Net;

public class blah
{
    
    public void go()
    {
        
        TcpListener listener = new TcpListener(IPAddress.Parse("localhost"), 1);
        //Task<Socket> acceptTask = Task.Factory.FromAsync<Socket>(TcpListener);
        
        if (listener.Pending())
        {
            
        }

    }
    



}