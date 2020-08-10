using System;
using System.Net;
using System.Text;

namespace SHPA.Blockchain.Server.ActionResult
{
    public class NotFoundActionResult : ActionResult
    {
        public NotFoundActionResult() : base(new { success = false }, HttpStatusCode.NotFound)
        {
        }
    }
}