using System;

namespace SHPA.Blockchain.Nodes
{
    public class Node
    {
        private DateTime _lastSyncDate;
        public Node(Uri address, string name)
        {
            Address = address;
            Name = name;
        }

        public string Name { get; }
        public Uri Address { get; }

        public void Sync()
        {
            _lastSyncDate = DateTime.UtcNow;
        }
    }
}