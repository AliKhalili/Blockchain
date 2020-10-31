using System.Net;
using System.Threading.Tasks;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.CQRS.Bus;
using SHPA.Blockchain.CQRS.Domain.Commands;
using SHPA.Blockchain.CQRS.Domain.Response;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions.Custom
{
    public class MineAction : ActionBase
    {
        public MineAction(IMediatorHandler bus) : base(null)
        {
        }
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            if (request.HttpMethod != "POST")
            {
                return new NotFoundActionResult();
            }
            var result = await Bus.Send<MineCommand, MineResponse>(new MineCommand());
            if (result.IsSuccess())
                return new ActionResult<Block<Transaction>>().AddResult(result.NewBlock);
            return new ActionResult<Block<Transaction>>().AddErrors(result.Errors());
        }
    }
}