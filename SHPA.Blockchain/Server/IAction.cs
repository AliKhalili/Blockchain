using System.Net;
using System.Threading.Tasks;

namespace SHPA.Blockchain.Server
{
    public interface IAction
    {
        Task<IActionResult> Execute(HttpListenerRequest request);
        string GetRout();
    }
}