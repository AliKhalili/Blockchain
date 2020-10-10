using System.Net;
using System.Threading.Tasks;
using SHPA.Blockchain.CQRS.Bus;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions.Custom
{
    public class ValidateChainAction : ActionBase
    {
        public ValidateChainAction(IMediatorHandler bus) : base(bus)
        {
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