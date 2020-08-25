using System.Net;

namespace SHPA.Blockchain.Server
{
    public interface IAction
    {
        IActionResult Execute(HttpListenerRequest request);
        string GetRout();
    }
}