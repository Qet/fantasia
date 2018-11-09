using System;
using System.Collections.Generic;
using System.IO;
using System.Text;  

public class Packetiser : IBytesReceived{

    private IPacketReceived packetReceived;
    
    private Dictionary<int, MemoryStream> buffers;

    private string delimeter;

    public Packetiser(IPacketReceived packetReceived, string delimeter){
        this.packetReceived = packetReceived;
        buffers = new Dictionary<int, MemoryStream>();
        this.delimeter = delimeter;
    }

    public void bytesReceived(int portID, Byte[] bytes){
        //buffer bytes
        string data = Encoding.ASCII.GetString(bytes);
        
        if (data.Contains(delimeter)){
            string[] spl = data.Split(new string[] {delimeter}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in spl){
                packetReceived.handlePacket(portID, s);
            }
        }
    }
}