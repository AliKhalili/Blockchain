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
            Hash = ComputeHash();
        }
        public long ProofOfWork { get; }
        public int Index { get; }
        public DateTime Time { get; }
        public T[] Transactions { get; }
        public string PreviousHash { get; }
        public string Hash { get; }

        public override string ToString()
        {
            return $"i:{Index}| t:{Time:s}| d:{Transactions.Length}| p:{ProofOfWork}| h:{PreviousHash}";
        }
        public string ComputeHash()
        {
            return ToString().SHA256();
        }
    }
}