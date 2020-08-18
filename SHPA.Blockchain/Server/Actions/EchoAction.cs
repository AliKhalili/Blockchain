using System.Net;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions
{
    public class EchoAction : IAction
    {
        public IActionResult Execute(HttpListenerRequest request)
        {
            return new ActionResult<string>().AddResult(request.QueryString["term"]);
        }
    }
}