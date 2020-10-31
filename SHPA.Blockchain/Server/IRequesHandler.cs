using System.Net;
using System.Threading.Tasks;

namespace SHPA.Blockchain.Server
{
    public interface IRequestHandler
    {
        Task HandleAsync(HttpListenerContext taskListener);
    }
}