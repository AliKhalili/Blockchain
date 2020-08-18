using System.IO;
using System.Net;
using Newtonsoft.Json;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions
{
    public class ValidateChainAction : IAction
    {
        private readonly IBlockchain _blockchain;

        public ValidateChainAction(IBlockchain blockchain)
        {
            _blockchain = blockchain;
        }
        public IActionResult Execute(HttpListenerRequest request)
        {
            if (request.HttpMethod != "GET")
            {
                return new NotFoundActionResult();
            }
            return new ActionResult<bool>().AddResult(_blockchain.IsValidChain());
        }
    }
}