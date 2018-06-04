using System;

class TCPSocket : ITCPSocket {
    public void run(){
        // read some bytes

        // call bytes received. 
    }

    private IBytesReceived bytesReceived;

    public TCPSocket(IBytesReceived bytesReceived){
        this.bytesReceived = bytesReceived;
    }

}