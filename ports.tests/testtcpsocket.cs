using NUnit.Framework;
using ports;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;

namespace Tests
{

    public class TcpSocketTests : IBytesReceived
    {
        public void bytesReceived(int portID, byte[] bytes){
            lastReceivedString = Encoding.ASCII.GetString(bytes);
        }

        private TCPSocket put;
        private TcpClient client;
        private TcpListener listener;
        private TcpClient acceptedClient;

        private string lastReceivedString;


        [SetUp]
        public void Setup()
        {
            lastReceivedString = "";
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 54540);
            listener.Start();
            client = new TcpClient("127.0.0.1", 54540);
            acceptedClient = listener.AcceptTcpClient();
            put = new TCPSocket(0, acceptedClient, this);   
            
        }

        [TearDown]
        public void TearDown(){
            listener.Stop();
            put = null;
            GC.Collect();
        }

        [Test]
        public void TestConstruct()
        {
            string testString = "this is a test";
            byte[] bytes = Encoding.ASCII.GetBytes(testString);
            client.GetStream().Write(bytes, 0, bytes.Length);
            put.run();

            Assert.AreEqual(testString, lastReceivedString);

        }
        
    }
} 