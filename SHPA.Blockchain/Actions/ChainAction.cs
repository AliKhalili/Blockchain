using System.Net;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;
using SHPA.Blockchain.Server.Actions;

namespace SHPA.Blockchain.Actions
{
    public class ChainAction : ActionBase
    {
        private readonly IEngine _engine;

        public ChainAction(IEngine engine)
        {
            _engine = engine;
        }
        public override IActionResult Execute(HttpListenerRequest request)
        {
            if (request.HttpMethod != "GET")
            {
                return new NotFoundActionResult();
            }
            return new ActionResult<Block<Transaction>[]>().AddResult(_engine.GetChain());
        }
    }
}