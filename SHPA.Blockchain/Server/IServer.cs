using System.Threading;

namespace SHPA.Blockchain.Server
{
    public interface IServer
    {
        void Start(CancellationToken cancellationToken);
        void Stop();
    }
}