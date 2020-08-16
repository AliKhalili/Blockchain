using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Server;

namespace SHPA.Blockchain
{
    public class Application
    {
        private readonly IServer _server;
        private readonly IBlockchain _blockchain;
        private readonly Dictionary<string, (string Help, Func<string, CancellationTokenSource, bool> Func)> _commands;

        public Application(IServer server, IBlockchain blockchain)
        {
            _server = server;
            _blockchain = blockchain;
            _commands = new Dictionary<string, (string Help, Func<string, CancellationTokenSource, bool> Func)>
            {
                {"quit", ("in order to quit and close application completely",QuitCommand)},
                {"help", ("show help", HelpCommand)},
                {"mine", ("mine and add new block to blockchain, transaction list will be empty", MineCommand)},

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
                var command = Console.ReadLine().ToLower();
                if (_commands.ContainsKey(command))
                {
                    if (!_commands[command].Func(command, cancellationToken))
                        break;
                }
                else
                {
                    if (!_commands["help"].Func(command, cancellationToken))
                        break;
                }
            }
        }



        private bool HelpCommand(string command, CancellationTokenSource cancellationToken)
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

        private bool QuitCommand(string command, CancellationTokenSource cancellationToken)
        {
            cancellationToken.Cancel();
            return false;
        }
        private bool MineCommand(string command, CancellationTokenSource cancellationToken)
        {
            var result = _blockchain.Mine();
            Console.WriteLine($"mine complete, block_id :{result.Index}, proof :{result.ProofOfWork}, datetime :{result.Time:s}, pre_hash:{result.PreviousHash}, hash :{result.Hash}");
            return true;
        }
    }
}