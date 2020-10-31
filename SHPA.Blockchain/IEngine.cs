using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Nodes;

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
        bool IsValidChain();
    }
}