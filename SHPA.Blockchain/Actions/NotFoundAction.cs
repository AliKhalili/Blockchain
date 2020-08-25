using System.Net;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;
using SHPA.Blockchain.Server.Actions;

namespace SHPA.Blockchain.Actions
{
    public class NotFoundAction : ActionBase
    {
        public override IActionResult Execute(HttpListenerRequest request)
        {
            return new NotFoundActionResult();
        }
    }
}