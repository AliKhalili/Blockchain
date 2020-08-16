namespace SHPA.Blockchain.Blocks
{
    public interface IBlockchain
    {
        void AddTransaction(string sender, string receiver, double amount);
        Block<Transaction> Mine();
        Block<Transaction>[] Chain();
        bool IsValidChain();
    }
}