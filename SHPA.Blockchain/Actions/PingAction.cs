using System;
using System.Net;
using SHPA.Blockchain.Actions.Models;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Configuration;
using SHPA.Blockchain.Nodes;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Actions
{
    public class PingAction : IAction
    {
        private readonly INodeManager _nodeManager;
        public PingAction(INodeManager nodeManager)
        {
            _nodeManager = nodeManager;
        }
        public IActionResult Execute(HttpListenerRequest request)
        {
            return new ActionResult<PingResultModel>().AddResult(new PingResultModel() { NodeName = _nodeManager.GetName() });
        }
    }
}