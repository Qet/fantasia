using System;
public interface IPacketReceived{
    void handlePacket(int portID, Byte[] bytes);
}