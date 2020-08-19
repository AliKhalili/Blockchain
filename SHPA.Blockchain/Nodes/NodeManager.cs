using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Options;
using SHPA.Blockchain.Actions.Models;
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
        public string GetName()
        {
            return _option.Name;
        }
        public void Ping()
        {
            var watch = new Stopwatch();
            foreach (var node in _nodes)
            {
                watch.Start();
                var ping = RestClient.Make(node.Value.Address).Get().Execute<JsonResultModel<PingResultModel>>("ping");
                var elapsedTime = watch.ElapsedMilliseconds;
                watch.Reset();
                if (ping != null && ping.Success)
                {
                    Console.WriteLine($"Reply from {node.Key} is success: time={elapsedTime}ms, return_node_name:{ping.Result.NodeName}");
                }
                else
                {
                    Console.WriteLine($"Reply from {node.Key} is failed: time={elapsedTime}ms");
                }
            }
        }



        public (bool Result, string Message) RegisterNode(Node node)
        {
            if (_nodes.Count >= _option.MaxNodeCapacity)
                return (false, "maximum capacity is exceeded");
            if (_nodes.ContainsKey(node.Name))
                return (false, $"node {node.Name} was registered previously");

            var ping = RestClient.Make(node.Address).Get().Execute<JsonResultModel<DateTime>>("ping");
            if (ping == null || !ping.Success)
                return (false, $"node {node.Name} is not reachable");

            _nodes.Add(node.Name, node);
            return (true, string.Empty);
        }

        public Node[] GetRegisterNodes()
        {
            return _nodes.Values.ToArray();
        }
    }
}