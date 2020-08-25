using System.Net;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;
using SHPA.Blockchain.Server.Actions;

namespace SHPA.Blockchain.Actions
{
    public class MineAction : ActionBase
    {
        private readonly IEngine _engine;
        public MineAction(IEngine engine)
        {
            _engine = engine;
        }
        public override IActionResult Execute(HttpListenerRequest request)
        {
            if (request.HttpMethod != "POST")
            {
                return new NotFoundActionResult();
            }

            var (result, error, newBlock) = _engine.Mine();
            if (result)
                return new ActionResult<Block<Transaction>>().AddResult(newBlock);
            return new ActionResult<Block<Transaction>>().AddErrors(new[] { error });
        }
    }
}