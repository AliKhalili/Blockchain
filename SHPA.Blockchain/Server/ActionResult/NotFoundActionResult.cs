using System.Net;

namespace SHPA.Blockchain.Server.ActionResult
{
    public class NotFoundActionResult : ActionResult<string>
    {
        public NotFoundActionResult()
        {
            AddErrors(new[] { "action is not founded" }, HttpStatusCode.NotFound);
        }
    }
}