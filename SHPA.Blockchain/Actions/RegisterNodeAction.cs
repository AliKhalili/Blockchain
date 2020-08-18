using System.Net;
using SHPA.Blockchain.Nodes;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;
using SHPA.Blockchain.Server.Actions;

namespace SHPA.Blockchain.Actions
{
    public class RegisterNodeAction : ActionBase
    {
        private readonly INodeManager _nodeManager;

        public RegisterNodeAction(INodeManager nodeManager)
        {
            _nodeManager = nodeManager;
        }
        public override IActionResult Execute(HttpListenerRequest request)
        {
            var input = ParseBody<Node>(request);
            if (request != null)
            {
                var (result, message) = _nodeManager.RegisterNode(input);
                if (!result)
                    return new ActionResult<object>().AddErrors(new[] { message });
                return new ActionResult<object>();

            }
            return new NotFoundActionResult();
        }
    }
}