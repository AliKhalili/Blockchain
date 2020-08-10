using System;
using System.Net;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions
{
    public class PingAction : IAction
    {
        public IActionResult Execute(HttpListenerRequest request)
        {
            return new JsonActionResult<DateTime>(DateTime.Now);
        }
    }
}