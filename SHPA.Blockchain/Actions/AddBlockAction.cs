using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;
using SHPA.Blockchain.Server.Actions;
using System.Net;
using System.Threading.Tasks;

namespace SHPA.Blockchain.Actions
{
    public class AddBlockAction : ActionBase
    {
        private readonly IEngine _engine;

        public AddBlockAction(IEngine engine)
        {
            _engine = engine;
        }
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            var input = ParseBody<Block<Transaction>>(request);
            if (input != null)
            {
                var (result, error) = _engine.AddBlock(input);
                var actionResult = new ActionResult<bool>().AddResult(result);
                if (error != null)
                    return actionResult.AddErrors(new[] { error });
                return actionResult;
            }
            return new NotFoundActionResult();
        }
    }
}