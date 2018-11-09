using NUnit.Framework;
using ports;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;

namespace Tests
{

    public class PacketiserTests : IPacketReceived
    {

        struct MiniPacket{
            public int portID;
            public string packet;
        }
        public void handlePacket(int portID, string packet){
            MiniPacket m;
            
        }

        private Packetiser put;
        
        private List<int> r;
        private string lastRecvPacket;


        [SetUp]
        public void Setup()
        {
            lastRecvPacket = "";
            lastRecvPortID = 0;
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

            Assert.AreEqual(portID, lastRecvPortID);
            Assert.AreEqual()



        }
        
    }
} 