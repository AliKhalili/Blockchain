using System.Net;

namespace SHPA.Blockchain.Server
{
    public interface IActionFactory
    {
        IAction Create(HttpListenerRequest request);
    }
}