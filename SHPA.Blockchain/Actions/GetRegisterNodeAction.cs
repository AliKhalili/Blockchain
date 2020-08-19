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
    public class GetRegisterNodeAction : IAction
    {
        private readonly INodeManager _nodeManager;
        public GetRegisterNodeAction(INodeManager nodeManager)
        {
            _nodeManager = nodeManager;
        }
        public IActionResult Execute(HttpListenerRequest request)
        {
            return new ActionResult<Node[]>().AddResult(_nodeManager.GetRegisterNodes());
        }
    }
}