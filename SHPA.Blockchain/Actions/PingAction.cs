using System.Net;
using SHPA.Blockchain.Actions.Models;
using SHPA.Blockchain.Nodes;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;
using SHPA.Blockchain.Server.Actions;

namespace SHPA.Blockchain.Actions
{
    public class PingAction : ActionBase
    {
        private readonly INodeManager _nodeManager;
        public PingAction(INodeManager nodeManager)
        {
            _nodeManager = nodeManager;
        }
        public override IActionResult Execute(HttpListenerRequest request)
        {
            return new ActionResult<PingResultModel>().AddResult(new PingResultModel { NodeName = _nodeManager.Node().Name });
        }
    }
}