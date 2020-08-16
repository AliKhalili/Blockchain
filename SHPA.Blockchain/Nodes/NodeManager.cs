using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Configuration;

namespace SHPA.Blockchain.Nodes
{
    public class NodeManager : INodeManager
    {
        private IBlockchain _blockchain;
        private readonly NodeConfiguration _option;
        private readonly Dictionary<string, Node> _nodes;
        public NodeManager(IBlockchain blockchain, IOptions<NodeConfiguration> option)
        {
            _blockchain = blockchain;
            _option = option.Value;
            _nodes = new Dictionary<string, Node>();
        }

        public void Ping()
        {
            foreach (var node in _nodes)
            {

            }
        }

        public (bool Result, string Message) RegisterNode(Node node)
        {
            if (_nodes.Count >= _option.MaxNodeCapacity)
                return (false, "maximum capacity is exceeded");
            if (_nodes.ContainsKey(node.Name))
                return (false, $"node {node.Name} was registered previously");

            _nodes.Add(node.Name, node);
            return (true, string.Empty);
        }

        public Node[] GetRegisterNodes()
        {
            return _nodes.Values.ToArray();
        }
    }
}