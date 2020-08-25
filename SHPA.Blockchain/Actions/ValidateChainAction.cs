using System.Net;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;
using SHPA.Blockchain.Server.Actions;

namespace SHPA.Blockchain.Actions
{
    public class ValidateChainAction : ActionBase
    {
        private readonly IBlockchain _blockchain;

        public ValidateChainAction(IBlockchain blockchain)
        {
            _blockchain = blockchain;
        }
        public override IActionResult Execute(HttpListenerRequest request)
        {
            if (request.HttpMethod != "GET")
            {
                return new NotFoundActionResult();
            }
            return new ActionResult<bool>().AddResult(_blockchain.IsValidChain());
        }
    }
}