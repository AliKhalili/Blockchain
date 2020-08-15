using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SHPA.Blockchain.Configuration;

namespace SHPA.Blockchain.Blocks
{
    public class Blockchain : IBlockchain
    {
        private readonly IProofOfWork _proofOfWork;
        private readonly IList<Block<Transaction>> _chain;
        private IList<Transaction> _transactions;
        private readonly NodeConfiguration _option;
        public Blockchain(IProofOfWork proofOfWork, IOptions<NodeConfiguration> option)
        {
            _proofOfWork = proofOfWork;
            _chain = new List<Block<Transaction>>
            {
                new Block<Transaction>(0,DateTime.UtcNow,new Transaction[0],"previous_hash",proofOfWork.InitialProof())
            };
            _transactions = new List<Transaction>();
            _option = option.Value;
        }

        private Block<Transaction> AddBlock(long proofOfWork)
        {
            var (lastIndex, previousHash) = GetLastBlock().Hash();
            var newBlock = new Block<Transaction>(lastIndex++, DateTime.UtcNow, _transactions.ToArray(), previousHash, proofOfWork);
            _chain.Add(newBlock);
            _transactions = new List<Transaction>();
            return newBlock;

        }

        public void AddTransaction(string sender, string receiver, double amount)
        {
            _transactions.Add(new Transaction(sender, receiver, amount));
        }

        public Block<Transaction> Mine()
        {
            var proof = _proofOfWork.GetNext(GetLastBlock().ProofOfWork);
            AddTransaction("reward", _option.Name, 1);
            return AddBlock(proof);
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