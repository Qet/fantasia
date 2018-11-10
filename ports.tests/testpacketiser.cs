using NUnit.Framework;
using ports;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.Collections.Generic;

namespace Tests
{
    public struct MiniPacket{
        public MiniPacket(int portID, string packet){
            this.portID = portID;
            this.packet = packet;
        }
        public int portID { get; private set;}
        public string packet { get; private set;}
    }

    public class PacketiserTests : IPacketReceived
    {

        public void handlePacket(int portID, string packet){
            recvdMiniPackets.Add(new MiniPacket(portID, packet));
        }

        private Packetiser put;
        
        private List<MiniPacket> recvdMiniPackets;

        [SetUp]
        public void Setup()
        {
            recvdMiniPackets = new List<MiniPacket>();
        }

        [TearDown]
        public void TearDown(){
            put = null;
            GC.Collect();
        }

        [Test]
        public void TestPacketise()
        {
            //construct some test data
            string delimeter = "qwe";
            string[] testStrings = {"some test string", "another test string", "123 anything without delimeter in it"};
            string inputString = String.Join(delimeter, testStrings);
            byte[] inputBytes = Encoding.ASCII.GetBytes(inputString);
            int portID = 123;
            put = new Packetiser(this, delimeter);
            put.bytesReceived(portID, inputBytes);

            //Check we got the right number of packets. 
            Assert.AreEqual(testStrings.Length, recvdMiniPackets.Count);

            //Check that the content of the packets is correcr. 
            for(int i=0;i<recvdMiniPackets.Count;i++){
                Assert.AreEqual(portID, recvdMiniPackets[i].portID);
                Assert.AreEqual(testStrings[i], recvdMiniPackets[i].packet);
            }
        }
        
    }
} 