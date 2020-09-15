using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;
using SHPA.Blockchain.Server.Actions;
using System.Net;
using System.Threading.Tasks;

namespace SHPA.Blockchain.Actions
{
    public class ValidateChainAction : ActionBase
    {
        private readonly IEngine _engine;
        public ValidateChainAction(IEngine engine)
        {
            _engine = engine;
        }
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            if (request.HttpMethod != "GET")
            {
                return new NotFoundActionResult();
            }
            return new ActionResult<bool>().AddResult(_engine.IsValidChain());
        }
    }
}