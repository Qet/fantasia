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

        [SetUp]
        public void Setup()
        {
            //start with no clients. 
            put = new SocketManager(this);  //PUT = product under test
        }

        [Test]
        public void TestConnectClient()
        {
            
            Assert.AreEqual(0, put.sockets.Count);

            //create a client and connect.             
            TcpClient c = new TcpClient();
            try {
                c.Connect("127.0.0.1", 54540);
            }
            catch {
                Assert.Fail("Failed to connect to the port manager!");
            }

            put.runOnce();

            //verify that we have connected
            Assert.IsTrue(c.Connected);

            //verify there's now 1 client. 
            Assert.AreEqual(1, put.sockets.Count);

            
        }
        
    }
}