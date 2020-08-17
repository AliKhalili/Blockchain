using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Options;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Client;
using SHPA.Blockchain.Configuration;
using SHPA.Blockchain.Server.ActionResult;

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
            var watch = new Stopwatch();
            foreach (var node in _nodes)
            {
                watch.Start();
                var ping = RestClient.Make(node.Value.Address).Get().Execute<JsonResultModel<DateTime>>("ping");
                watch.Reset();
                var nodeTime = "NAN";
                if (ping != null && ping.Success)
                {
                    nodeTime = ping.Result.ToString("s");
                }
                Console.WriteLine($"Reply from {node.Key}: time={watch.Elapsed.Milliseconds}ms node_time={nodeTime}");

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