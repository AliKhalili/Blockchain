using System.Net;

namespace SHPA.Blockchain.Server
{
    public abstract class Action : IAction
    {
        public IActionResult Execute(HttpListenerRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}