using System.Threading;
using SHPA.Blockchain.Server;

namespace SHPA.Blockchain
{
    public class Application
    {
        private readonly IServer _server;

        public Application(IServer server)
        {
            _server = server;
        }
        public void Run(CancellationToken cancellationToken)
        {
            _server.Start(cancellationToken);
        }
    }
}