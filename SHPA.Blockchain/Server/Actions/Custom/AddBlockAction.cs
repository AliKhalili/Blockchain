using System.Net;
using System.Threading.Tasks;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.CQRS.Bus;
using SHPA.Blockchain.CQRS.Domain.Commands;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions.Custom
{
    public class AddBlockAction : ActionBase
    {
        public AddBlockAction(IMediatorHandler bus) :base(bus)
        {
        }
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            var input = ParseBody<Block<Transaction>>(request);
            if (input != null)
            {
                var result =await _bus.Send<AddBlockCommand,DefaultResponse>(new AddBlockCommand(input));
                if (result.IsSuccess())
                    return new ActionResult<bool>().AddResult(true);
                return new ActionResult<bool>().AddErrors(result.Errors());
            }
            return new NotFoundActionResult();
        }
    }
}