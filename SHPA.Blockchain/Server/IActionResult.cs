using System.Net;

namespace SHPA.Blockchain.Server
{
    public interface IActionResult
    {
        (int HttpStatusCode, string Content) GetResult();
    }


}