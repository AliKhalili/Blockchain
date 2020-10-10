using System.Net;
using System.Threading.Tasks;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.CQRS.Bus;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions.Custom
{
    public class MineAction : ActionBase
    {
        private readonly IEngine _engine;
        public MineAction(IMediatorHandler bus) : base(bus)
        {
            _engine = engine;
        }
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
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