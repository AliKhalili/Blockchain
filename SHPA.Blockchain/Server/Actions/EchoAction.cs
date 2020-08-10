using System;
using System.Net;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions
{
    public class EchoAction : IAction
    {
        public IActionResult Execute(HttpListenerRequest request)
        {
            return new JsonActionResult<string>(request.QueryString["term"]);
        }
    }
}