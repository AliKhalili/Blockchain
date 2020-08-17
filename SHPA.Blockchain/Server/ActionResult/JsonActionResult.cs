using System.Net;

namespace SHPA.Blockchain.Server.ActionResult
{
    public class JsonActionResult<T> : ActionResult
    {
        public JsonActionResult(T result, bool success = true, string[] messages = null) : base(
            new JsonResultModel<T>
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

    public class JsonResultModel<T>
    {
        public bool Success { get; set; }
        public string[] Message { get; set; }
        public T Result { get; set; }
    }
}