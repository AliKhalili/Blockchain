using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace SHPA.Blockchain.Server.ActionResult
{
    public class ActionResult<T> : IActionResult<T>
    {
        private T _result;
        private IList<string> _errors;
        private HttpStatusCode _statusCode;

        public IActionResult<T> AddResult(T result)
        {
            _result = result;
            _statusCode = HttpStatusCode.OK;
            return this;
        }

        public IActionResult<T> AddErrors(string[] errors, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            _errors ??= new List<string>();
            foreach (var error in errors)
            {
                _errors.Add(error);
            }

            _statusCode = status;
            return this;

        }

        public (int HttpStatusCode, string Content) GetResult()
        {
            if (_errors != null && _errors.Any())
                return ((int)_statusCode, JsonConvert.SerializeObject(
                    new JsonResultModel<T>
                    {
                        Success = false,
                        Errors = _errors.ToArray(),
                    }, Formatting.Indented));

            return ((int)HttpStatusCode.OK, JsonConvert.SerializeObject(
                new JsonResultModel<T>
                {
                    Success = true,
                    Errors = null,
                    Result = _result
                }, Formatting.Indented));
        }


    }
}
