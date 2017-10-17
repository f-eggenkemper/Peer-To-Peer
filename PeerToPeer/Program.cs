using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerToPeer
{
    class Program
    {
        static void Main(string[] args)
        {
            Peer p1 = new Peer("Peer 1", 110);
            Peer p2 = new Peer("Peer 2", 22);
            Peer p3 = new Peer("Peer 3", 33);
            Peer p4 = new Peer("Peer 4", 33);
            Peer p5 = new Peer("Peer 5", 33);
            Peer p6 = new Peer("Peer 6", 666);

            p1.Add(p2);
            p2.Add(p3);
            p1.Add(p4);
            p4.Add(p5);
            p4.Add(p6);
            p5.Add(p6);

            Peer p7 = new Peer("Peer 7", 77);
            p7.Add(p2);
            p7.Add(p3);
            p7.Netzeintritt();

            p1.PrintList();
            p7.PrintList();

            Console.WriteLine(p7.search(110, Peer.TTL).ToString());
            Console.WriteLine(p7.search(666, Peer.TTL).ToString());
        }
    }
}
