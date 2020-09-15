using System.Net;
using System.Threading.Tasks;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions.Custom
{
    public class NotFoundAction : ActionBase
    {
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            return new NotFoundActionResult();
        }
    }
}