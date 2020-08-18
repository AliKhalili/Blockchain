using System;
using System.Net;

namespace SHPA.Blockchain.Server.ActionResult
{
    public class ExceptionActionResult : ActionResult<Exception>
    {
        public ExceptionActionResult(Exception exception)
        {
            AddErrors(
                new[] { exception?.InnerException?.Message ?? exception?.Message ?? "an unknown exception was occured" },
                HttpStatusCode.InternalServerError);
        }
    }
}