using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Client;
using SHPA.Blockchain.Nodes;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain
{
    public interface IEngine
    {
        (bool, string) RegisterNode(Node newNode, bool enableRegisterBack = false);
        Node[] GetRegisterNodes();
        Block<Transaction>[] GetChain();

        (bool, string) AddBlock(Block<Transaction> newBlock);
        void AddTransaction(Transaction newTransaction);
        (bool, string, Block<Transaction>) Mine();
    }
    public class Engine : IEngine
    {
        private readonly IBlockchain _blockchain;
        private readonly INodeManager _nodeManager;
        public Engine(IBlockchain blockchain, INodeManager nodeManager)
        {
            _blockchain = blockchain;
            _nodeManager = nodeManager;
        }


        public (bool, string) RegisterNode(Node newNode, bool enableRegisterBack = false)
        {
            var (result, message) = _nodeManager.RegisterNode(node: newNode);
            if (result)
            {
                RestClient.Make(newNode.Address)
                    .Post()
                    .AddQuery(nameof(enableRegisterBack), enableRegisterBack.ToString())
                    .AddBody(_nodeManager.Node())
                    .Execute<JsonResultModel<bool>>("registernode");
            }

            return (result, message);
        }

        public Node[] GetRegisterNodes()
        {
            return _nodeManager.GetRegisterNodes();
        }

        public Block<Transaction>[] GetChain()
        {
            return _blockchain.Chain();
        }

        public (bool, string) AddBlock(Block<Transaction> newBlock)
        {
            return _blockchain.AddBlock(newBlock);
        }

        public void AddTransaction(Transaction newTransaction)
        {
            _blockchain.AddTransaction(newTransaction.Sender, newTransaction.Receiver, newTransaction.Amount);
        }

        public (bool, string, Block<Transaction>) Mine()
        {
            var newBlock = _blockchain.Mine();
            var (result, errors) = _nodeManager.BroadcastNewBlock(newBlock);
            return (true, string.Empty, newBlock);
        }
    }
}