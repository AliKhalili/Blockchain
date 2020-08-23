using SHPA.Blockchain.Actions.Models;

namespace SHPA.Blockchain.Blocks
{
    public interface IBlockchain
    {
        void AddTransaction(string sender, string receiver, double amount);
        Block<Transaction> Mine();
        Block<Transaction>[] Chain();
        bool IsValidChain();
        AddBlockResultModel AddBlock(Block<Transaction> input);
    }
}