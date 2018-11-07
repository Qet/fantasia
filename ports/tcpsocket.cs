using System;
using System.Net.Sockets;

public class TCPSocket : ITCPSocket {
    
    public TcpClient client {get; private set;}
    
    private int portID;
    private IBytesReceived bytesReceived;

    public TCPSocket(int portID, TcpClient client, IBytesReceived bytesReceived) {
        this.portID = portID;
        this.client = client;
        this.bytesReceived = bytesReceived;
    }

    public void run() {
        readBytes();
    }

    

    private void readBytes() {
        if (client.Connected) {
            Byte[] bytes = new Byte[200];
            NetworkStream stream = client.GetStream();

            if (stream.DataAvailable) {
                int i = stream.Read(bytes, 0, bytes.Length);
                if (i > 0){
                    Array.Resize<Byte>(ref bytes, i);
                    bytesReceived.bytesReceived(portID, bytes);
                }
            }
        }

    }

}