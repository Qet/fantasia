using System;
using System.Collections.Generic;
using System.IO;
using System.Text;  

public class Packetiser : IBytesReceived{

    private IPacketReceived packetReceived;
    
    private Dictionary<int, MemoryStream> buffers;

    public Packetiser(IPacketReceived packetReceived){
        this.packetReceived = packetReceived;
        buffers = new Dictionary<int, MemoryStream>();
    }

    public void bytesReceived(int portID, Byte[] bytes){
        //buffer bytes
        string data = Encoding.ASCII.GetString(bytes);
        char delimeter = '\r';
        
        if (data.Contains(delimeter.ToString())){
            string[] spl = data.Split(new char[] {delimeter} );
            packetReceived.handlePacket(portID, spl);
        }
    }
}