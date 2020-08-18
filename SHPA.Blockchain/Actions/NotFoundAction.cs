using System.Net;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Actions
{
    public class NotFoundAction : IAction
    {
        public IActionResult Execute(HttpListenerRequest request)
        {
            return new NotFoundActionResult();
        }
    }
}