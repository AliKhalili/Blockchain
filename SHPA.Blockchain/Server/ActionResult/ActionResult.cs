using System.Net;
using Newtonsoft.Json;

namespace SHPA.Blockchain.Server.ActionResult
{
    public abstract class ActionResult : IActionResult
    {
        private readonly object _result;
        private readonly HttpStatusCode _statusCode;
        protected ActionResult(object result, HttpStatusCode statusCode)
        {
            _result = result;
            _statusCode = statusCode;
        }

        public (int HttpStatusCode, string Content) GetResult()
        {
            return ((int)_statusCode, JsonConvert.SerializeObject(_result, Formatting.Indented));
        }
    }
}
