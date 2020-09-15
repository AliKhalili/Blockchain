using SHPA.Blockchain.Nodes;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;
using SHPA.Blockchain.Server.Actions;
using System.Net;
using System.Threading.Tasks;

namespace SHPA.Blockchain.Actions
{
    public class GetRegisterNodeAction : ActionBase
    {
        private readonly IEngine _engine;
        public GetRegisterNodeAction(IEngine engine)
        {
            _engine = engine;
        }
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            return new ActionResult<Node[]>().AddResult(_engine.GetRegisterNodes());
        }
    }
}