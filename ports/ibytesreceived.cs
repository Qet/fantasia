using System;
public interface IBytesReceived{
    // called when some bytes have been received to process.
    void bytesReceived(Byte[] bytes);
}
