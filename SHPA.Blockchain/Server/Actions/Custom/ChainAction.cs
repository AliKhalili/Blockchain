using System.Net;
using System.Threading.Tasks;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.CQRS.Domain;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions.Custom
{
    public class ChainAction : ActionBase
    {
        private readonly IQueryService _queryService;

        public ChainAction(IQueryService queryService) : base()
        {
            _queryService = queryService;
        }
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            if (request.HttpMethod != "GET")
            {
                return new NotFoundActionResult();
            }
            return new ActionResult<Block<Transaction>[]>().AddResult(await _queryService.GetChainAsync());
        }
    }
}