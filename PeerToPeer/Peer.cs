using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerToPeer
{
    class Peer : IComparable
    {
        public const int TTL = 1;
        public int data;
        string Name;
        bool alive;
        SortedSet<Peer> Peers = new SortedSet<Peer>();

        public Peer(string name, int data)
        {
            this.alive = true;
            this.Name = name;
            this.data = data;
        }

        public SortedSet<Peer> Ping(Peer p, int ttl)
        {
            SortedSet<Peer> ReturnPeers = new SortedSet<Peer>();
            if (p.Equals(this) || !this.alive)
            {
                return ReturnPeers;
            }
            ReturnPeers.Add(this);

            if (ttl > 0)
            {
                SortedSet<Peer> PeersCopy = new SortedSet<Peer>();
                PeersCopy.UnionWith(Peers);
                foreach (Peer peer in PeersCopy)
                {
                    ReturnPeers.UnionWith(peer.Ping(p, ttl - 1));
                }

                Peers.Add(p);
            }

            return ReturnPeers;
        }

        public void Netzeintritt()
        {
            SortedSet<Peer> peers2 = new SortedSet<Peer>();
            foreach (Peer peer in Peers)
            {
                peers2.UnionWith(peer.Ping(this, TTL));
            }
            Peers = peers2;
        }

        public void Add(Peer p)
        {
            Peers.Add(p);
            p.Peers.Add(this);
        }

        public Peer search(int suchwort, int ttl)
        {
            if (this.data == suchwort)
            {
                return this;
            }

            if (ttl > 0)
            {
                foreach (Peer p in Peers)
                {
                    Peer result = p.search(suchwort, ttl - 1);
                    if (result != null)
                    {
                        return result;
                    }
                }
            } 

            return null;
        }

        public void PrintList()
        {
            Console.WriteLine("Ich bin " + this.Name);
            foreach (Peer p in Peers)
            {
                Console.Write(p.Name + ", ");
            }
            Console.WriteLine(" ");
        }

        public int CompareTo(object obj)
        {
            return Name.CompareTo(((Peer)obj).Name);
        }

        public String ToString()
        {
            return this.Name;
        }
    }
}
