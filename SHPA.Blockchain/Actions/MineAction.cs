using System.Net;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Nodes;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Actions
{
    public class MineAction : IAction
    {
        private readonly IBlockchain _blockchain;
        private readonly INodeManager _nodeManager;

        public MineAction(IBlockchain blockchain, INodeManager nodeManager)
        {
            _blockchain = blockchain;
            _nodeManager = nodeManager;
        }
        public IActionResult Execute(HttpListenerRequest request)
        {
            if (request.HttpMethod != "POST")
            {
                return new NotFoundActionResult();
            }

            var newBlock = _blockchain.Mine();
            var (result, errors) = _nodeManager.BroadcastNewBlock(newBlock);
            if (result)
                return new ActionResult<Block<Transaction>>().AddResult(newBlock);
            return new ActionResult<Block<Transaction>>().AddErrors(errors);
        }
    }
}