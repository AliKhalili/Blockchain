namespace SHPA.Blockchain.Blocks
{
    public interface IBlockchain
    {
        void AddBlock(long proofOfWork);
        void AddTransaction(string sender, string receiver, double amount);

        Block<Transaction>[] Chain();
    }
}