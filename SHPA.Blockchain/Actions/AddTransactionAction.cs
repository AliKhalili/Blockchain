using System.Net;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;
using SHPA.Blockchain.Server.Actions;

namespace SHPA.Blockchain.Actions
{
    public class AddTransactionAction : ActionBase
    {
        private readonly IEngine _engine;
        public AddTransactionAction(IEngine engine)
        {
            _engine = engine;
        }
        public override IActionResult Execute(HttpListenerRequest request)
        {
            var input = ParseBody<Transaction>(request);
            if (input != null)
            {
                _engine.AddTransaction(input);
                return new ActionResult<Transaction>().AddResult(input);
            }
            return new NotFoundActionResult();
        }
    }
}