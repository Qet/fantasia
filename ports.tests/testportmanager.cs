using NUnit.Framework;
using System.Net.Sockets;
using ports;

namespace Tests
{

    public class PortManagerTests : ICommand
    {
        public void move(Directions dir){
 
        }

        // Port manager requirements:
        // accept client connections as they come in
        // and store them in a list. 

        
        private SocketManager put;
        private TcpClient client;

        [SetUp]
        public void Setup()
        {
            //start with no clients. 
            put = new SocketManager(this);  //PUT = product under test
            client = new TcpClient();
        }

        [TearDown]
        public void TearDown(){
            client.Close();
            client.Dispose();
        }

        [Test]
        public void TestConnectClient()
        {
            
            Assert.AreEqual(0, put.sockets.Count);
            Assert.AreEqual(0, put.packetisers.Count);

            //create a client and connect.             
            try {
                client.Connect("127.0.0.1", 54540);
            }
            catch {
                Assert.Fail("Failed to connect to the port manager!");
            }

            put.runOnce();

            //verify that we have connected
            Assert.IsTrue(client.Connected);

            //verify there's now 1 client and packetiser
            Assert.AreEqual(1, put.sockets.Count);
            Assert.AreEqual(1, put.packetisers.Count);
            
        }
        
    }
}