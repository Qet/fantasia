using System;
interface IPacketReceived{
    void handlePacket(int portID, Byte[] bytes);
}