using System.Net;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions
{
    public class AddTransactionAction : ActionBase
    {
        private readonly IBlockchain _blockchain;

        public AddTransactionAction(IBlockchain blockchain)
        {
            _blockchain = blockchain;
        }
        public override IActionResult Execute(HttpListenerRequest request)
        {
            var input = ParseBody<Transaction>(request);
            if (input != null)
            {
                _blockchain.AddTransaction(input.Sender, input.Receiver, input.Amount);
                return new JsonActionResult<Transaction>(input);
            }
            return new NotFoundActionResult();
        }
    }
}