using SHPA.Blockchain.Nodes;
using SHPA.Blockchain.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SHPA.Blockchain
{
    public class Application
    {
        private readonly IServer _server;
        private readonly IEngine _engine;
        private readonly Dictionary<string, (string Help, Func<string, CancellationTokenSource, Dictionary<string, string>, bool> Func)> _commands;

        public Application(IServer server, IEngine engine)
        {
            _server = server;
            _engine = engine;
            _commands = new Dictionary<string, (string Help, Func<string, CancellationTokenSource, Dictionary<string, string>, bool> Func)>
            {
                {"quit", ("in order to quit and close application completely",QuitCommand)},
                {"help", ("show help", HelpCommand)},
                {"mine", ("mine and add new block to blockchain, transaction list will be empty", MineCommand)},
                {"register_node", ("register new node by adding -n name -u address", RegisterNewNodeCommand)},

            };
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
                var (command, args) = ParseCommand(Console.ReadLine());
                if (_commands.ContainsKey(command))
                {
                    if (!_commands[command].Func(command, cancellationToken, args))
                        break;
                }
                else
                {
                    if (!_commands["help"].Func(command, cancellationToken, args))
                        break;
                }
            }
        }

        private (string, Dictionary<string, string>) ParseCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
                return ("help", null);
            var args = command.Split(" ");
            if (args.Length == 1)
                return (args[0].ToLower(), null);
            var argskw = new Dictionary<string, string>();
            for (int i = 1; i < args.Length; i += 2)
            {
                argskw.Add(args[i], args[i + 1]);
            }

            return (args[0].ToLower(), argskw);
        }


        private bool HelpCommand(string command, CancellationTokenSource cancellationToken, Dictionary<string, string> args)
        {
            Console.WriteLine("-".Multiply(70));
            Console.WriteLine($"{"-".Multiply(30)}BLOCKCHAIN{"-".Multiply(30)}");
            foreach (var cm in _commands.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{cm.Key} :{cm.Value.Help}");
            }
            Console.WriteLine("-".Multiply(70));

            return true;
        }
        private bool QuitCommand(string command, CancellationTokenSource cancellationToken, Dictionary<string, string> args)
        {
            cancellationToken.Cancel();
            return false;
        }
        private bool MineCommand(string command, CancellationTokenSource cancellationToken, Dictionary<string, string> args)
        {
            var (result, error, newBlock) = _engine.Mine();
            Console.WriteLine($"mine complete, block_id :{newBlock.Index}, proof :{newBlock.ProofOfWork}, datetime :{newBlock.Time:s}, pre_hash:{newBlock.PreviousHash}, hash :{newBlock.Hash}");
            return true;
        }
        private bool RegisterNewNodeCommand(string command, CancellationTokenSource cancellationToken, Dictionary<string, string> args)
        {
            var (result, error) = _engine.RegisterNode(new Node(new Uri(args["-u"]), args["-n"]));
            Console.WriteLine($"new node register status: {(result ? "success" : "failed")}");
            return true;
        }
    }
}