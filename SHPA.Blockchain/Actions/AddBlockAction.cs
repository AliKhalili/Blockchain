using System.Net;
using SHPA.Blockchain.Actions.Models;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;
using SHPA.Blockchain.Server.Actions;

namespace SHPA.Blockchain.Actions
{
    public class AddBlockAction : ActionBase
    {
        private readonly IBlockchain _blockchain;

        public AddBlockAction(IBlockchain blockchain)
        {
            _blockchain = blockchain;
        }
        public override IActionResult Execute(HttpListenerRequest request)
        {
            var input = ParseBody<Block<Transaction>>(request);
            if (input != null)
            {
                var result = _blockchain.AddBlock(input);
                return new ActionResult<AddBlockResultModel>().AddResult(result);
            }
            return new NotFoundActionResult();
        }
    }
}