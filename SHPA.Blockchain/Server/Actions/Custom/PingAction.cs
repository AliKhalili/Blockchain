using System.Net;
using System.Threading.Tasks;
using SHPA.Blockchain.Actions.Models;
using SHPA.Blockchain.Nodes;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions.Custom
{
    public class PingAction : ActionBase
    {
        private readonly INodeManager _nodeManager;
        public PingAction(INodeManager nodeManager)
        {
            _nodeManager = nodeManager;
        }
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            return new ActionResult<PingResultModel>().AddResult(new PingResultModel { NodeName = _nodeManager.Node().Name });
        }
    }
}