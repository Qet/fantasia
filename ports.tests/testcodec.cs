using NUnit.Framework;
using ports;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.Collections.Generic;

namespace Tests
{
    public struct PacketMapping{
        public string packet {get; private set;}
        public Directions dir {get;private set;}

        public PacketMapping(string packet, Directions dir){
            this.dir = dir;
            this.packet = packet;
        }

    }
    public class CodecTests : ICommand
    {

        public void move(Directions dir){
            recvdMoves.Add(dir);
        }

        public Codec put;
        private const int portID = 123;

        private List<Directions> recvdMoves;


        [SetUp]
        public void Setup()
        {
            put = new Codec(this);
            recvdMoves = new List<Directions>();
        }

        [TearDown]
        public void TearDown(){
            put = null;
            GC.Collect();
        }

        [Test]
        public void TestMove(){
            // Set up the expected results. 
            List<PacketMapping> expectedPackets = new List<PacketMapping>();
            expectedPackets.Add(new PacketMapping("move north", Directions.North));
            recvdMoves.Clear();

            // Hit the codec on the interface
            foreach (PacketMapping p in expectedPackets){
                put.handlePacket(portID, p.packet);
            }

            // Check results
            Assert.AreEqual(recvdMoves.Count, expectedPackets.Count);
            for(int i=0;i<recvdMoves.Count;i++){
                Assert.AreEqual(expectedPackets[i].dir, recvdMoves[i]);
            }
        }
        
    }
} 