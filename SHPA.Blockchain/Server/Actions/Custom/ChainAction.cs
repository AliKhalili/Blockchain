using System.Net;
using System.Threading.Tasks;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.CQRS.Bus;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions.Custom
{
    public class ChainAction : ActionBase
    {
        private readonly IEngine _engine;

        public ChainAction(IMediatorHandler bus) : base(bus)
        {
            _engine = engine;
        }
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            if (request.HttpMethod != "GET")
            {
                return new NotFoundActionResult();
            }
            return new ActionResult<Block<Transaction>[]>().AddResult(_engine.GetChain());
        }
    }
}