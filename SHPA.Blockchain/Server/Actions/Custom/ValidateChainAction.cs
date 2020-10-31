using System.Net;
using System.Threading.Tasks;
using SHPA.Blockchain.CQRS.Bus;
using SHPA.Blockchain.CQRS.Domain;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions.Custom
{
    public class ValidateChainAction : ActionBase
    {
        private readonly IQueryService _queryService;
        public ValidateChainAction(IQueryService queryService) : base(null)
        {
            _queryService = queryService;
        }
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            if (request.HttpMethod != "GET")
            {
                return new NotFoundActionResult();
            }
            return new ActionResult<bool>().AddResult(await _queryService.IsValidChainAsync());
        }
    }
}