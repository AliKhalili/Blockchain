using System;
using System.Collections.Generic;
using System.Linq;

namespace SHPA.Blockchain.Blocks
{
    public class Blockchain: IBlockchain
    {
        private readonly IList<Block<Transaction>> _chain;
        private IList<Transaction> _transactions;
        public Blockchain(IProofOfWork proofOfWork)
        {
            _chain = new List<Block<Transaction>>
            {
                new Block<Transaction>(0,DateTime.UtcNow,new Transaction[0],"previous_hash",proofOfWork.InitialProof())
            };
            _transactions = new List<Transaction>();
        }

        public void AddBlock(long proofOfWork)
        {
            var (lastIndex, previousHash) = GetLastBlock().Hash();
            _chain.Add(new Block<Transaction>(lastIndex++, DateTime.UtcNow, _transactions.ToArray(), previousHash, proofOfWork));
            _transactions = new List<Transaction>();

        }

        public void AddTransaction(string sender, string receiver, double amount)
        {
            _transactions.Add(new Transaction(sender, receiver, amount));
        }

        public Block<Transaction>[] Chain()
        {
            return _chain.ToArray();
        }

        private Block<Transaction> GetLastBlock()
        {
            return _chain.Last();
        }
    }
}