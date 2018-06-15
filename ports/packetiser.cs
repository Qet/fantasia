using System;
using System.Collections.Generic;
using System.IO;
using System.Text;  

class Packetiser : IBytesReceived{

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
        }

        //call packet received when there's a packet. 
    }

    private void sendBuffer(){
        packetReceived.handlePacket(portID)
    }

}