using System.Net;
using System.Threading.Tasks;
using SHPA.Blockchain.CQRS.Bus;
using SHPA.Blockchain.CQRS.Domain;
using SHPA.Blockchain.Nodes;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions.Custom
{
    public class GetRegisterNodeAction : ActionBase
    {
        private readonly IQueryService _queryService;
        public GetRegisterNodeAction(IQueryService queryService) : base(null)
        {
            _queryService = queryService;
        }
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            return new ActionResult<Node[]>().AddResult(await _queryService.GetRegisterNodesAsync());
        }
    }
}