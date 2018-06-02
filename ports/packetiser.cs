using System;

class Packetiser : IBytesReceived{

    public void bytesReceived(int portID, Byte[] bytes){
        //buffer bytes

        //call packet received when there's a packet. 
    }

    private IPacketReceived packetReceived;

    public Packetiser(int portID, IPacketReceived packetReceived){
        this.packetReceived = packetReceived;
    }

}