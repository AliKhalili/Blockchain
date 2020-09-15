using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;
using SHPA.Blockchain.Server.Actions;
using System.Net;
using System.Threading.Tasks;

namespace SHPA.Blockchain.Actions
{
    public class NotFoundAction : ActionBase
    {
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            return new NotFoundActionResult();
        }
    }
}