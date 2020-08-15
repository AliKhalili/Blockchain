using System;

namespace SHPA.Blockchain.Blocks
{
    public class Block<T> where T : class
    {
        public Block(int index, DateTime time, T[] transactions, string previousHash, long proofOfWork)
        {
            Index = index;
            Time = time;
            Transactions = transactions;
            PreviousHash = previousHash;
            ProofOfWork = proofOfWork;
        }
        public long ProofOfWork { get; }
        public int Index { get; }
        public DateTime Time { get; }
        public T[] Transactions { get; }
        public string PreviousHash { get; }
        public string BlockHash => Hash().Hash;

        public override string ToString()
        {
            return $"i:{Index}| t:{Time:s}| d:{Transactions.Length}| p:{ProofOfWork}| h:{PreviousHash}";
        }
        public (int Index, string Hash) Hash()
        {
            return (Index, ToString().SHA256());
        }
    }
}