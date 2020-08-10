using System.Net;

namespace SHPA.Blockchain.Server.ActionResult
{
    public class JsonActionResult<T> : ActionResult
    {
        public JsonActionResult(T result, bool success = true, string[] messages = null) : base(
            new
            {
                Success = success,
                Message = messages,
                Result = result
            }, HttpStatusCode.Accepted)
        {
        }

        public JsonActionResult(T result, HttpStatusCode statusCode) : base(result, statusCode)
        {
        }
    }
}