using System;
public interface IPacketReceived{
    void handlePacket(int portID, string packet);
}