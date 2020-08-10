using System;
using System.Net;
using System.Text;

namespace SHPA.Blockchain.Server.ActionResult
{
    public class NotFoundActionResult : IActionResult
    {
        public (int HttpStatusCode, string Content) GetResult()
        {
            return ((int)HttpStatusCode.NotFound, "{ 'success':false, 'error':'not found' }");
        }
    }
}