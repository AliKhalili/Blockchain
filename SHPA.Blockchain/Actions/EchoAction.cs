using System.Net;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;
using SHPA.Blockchain.Server.Actions;

namespace SHPA.Blockchain.Actions
{
    public class EchoAction : ActionBase
    {
        public override IActionResult Execute(HttpListenerRequest request)
        {
            return new ActionResult<string>().AddResult(request.QueryString["term"]);
        }
    }
}