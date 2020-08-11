using System;
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
        public void Run(CancellationTokenSource cancellationToken)
        {
            _server.Start(cancellationToken.Token);
            WaitForCommand(cancellationToken);
        }

        private void WaitForCommand(CancellationTokenSource cancellationToken)
        {
            while (true)
            {
                Console.Write("enter command:>");
                var command = Console.ReadLine().ToLower();
                if (command == "help")
                {

                }
                else if (command == "quit")
                {
                    cancellationToken.Cancel();
                    break;
                }
            }
        }
    }
}