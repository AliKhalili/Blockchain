using System.Net;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions
{
    public class MineAction : IAction
    {
        private readonly IBlockchain _blockchain;

        public MineAction(IBlockchain blockchain)
        {
            _blockchain = blockchain;
        }
        public IActionResult Execute(HttpListenerRequest request)
        {
            if (request.HttpMethod != "POST")
            {
                return new NotFoundActionResult();
            }
            return new ActionResult<Block<Transaction>>().AddResult(_blockchain.Mine());
        }
    }
}