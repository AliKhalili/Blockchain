using System;

namespace SHPA.Blockchain.Blocks
{
    public class Block<T> where T : class
    {
        private readonly int _index;
        private readonly long _proofOfWork;
        private readonly DateTime _time;
        private readonly T[] _transactions;
        private readonly string _previousHash;

        public Block(int index, DateTime time, T[] transactions, string previousHash, long proofOfWork)
        {
            _index = index;
            _time = time;
            _transactions = transactions;
            _previousHash = previousHash;
            _proofOfWork = proofOfWork;
        }

        public override string ToString()
        {
            return $"i:{_index}| t:{_time:s}| d:{_transactions.Length}| p:{_proofOfWork}| h:{_previousHash}";
        }

        public (int Index, string Hash) Hash()
        {
            return (_index, ToString().SHA256());
        }
    }
}