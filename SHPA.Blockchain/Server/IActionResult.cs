using System.Net;

namespace SHPA.Blockchain.Server
{
    public interface IActionResult<in T> : IActionResult
    {
        IActionResult<T> AddResult(T result);
        IActionResult<T> AddErrors(string[] errors, HttpStatusCode status = HttpStatusCode.BadRequest);
    }

    public interface IActionResult
    {
        (int HttpStatusCode, string Content) GetResult();
    }
}