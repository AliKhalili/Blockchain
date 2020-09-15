using System.Net;
using System.Threading.Tasks;
using SHPA.Blockchain.Nodes;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions.Custom
{
    public class RegisterNodeAction : ActionBase
    {
        private readonly IEngine _engine;

        public RegisterNodeAction(IEngine engine)
        {
            _engine = engine;
        }
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            var input = ParseBody<Node>(request);
            if (input != null)
            {
                var enableRegisterBack = ParseQuery<bool>(request, "enableRegisterBack");
                var (result, message) = _engine.RegisterNode(input, enableRegisterBack);
                if (!result)
                    return new ActionResult<bool>().AddErrors(new[] { message });
                return new ActionResult<bool>();

            }
            return new NotFoundActionResult();
        }
    }
}