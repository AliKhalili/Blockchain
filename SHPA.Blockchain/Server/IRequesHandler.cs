using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SHPA.Blockchain.Server
{
    public interface IRequestHandler
    {
        Task HandleAsync(Task<HttpListenerContext> taskListener);
    }
}