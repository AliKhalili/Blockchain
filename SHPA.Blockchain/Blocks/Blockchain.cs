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
                GetGenesisBlock()
            };
            _transactions = new List<Transaction>();
            _option = option.Value;
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

        public bool IsValidChain()
        {
            return IsValidChain(_chain);
        }

        private Block<Transaction> GetGenesisBlock()
        {
            return new Block<Transaction>(0, DateTime.UtcNow, new Transaction[0], "genesis_block", _proofOfWork.InitialProof());
        }

        private bool IsValidChain(IList<Block<Transaction>> chain)
        {
            if (chain.Count <= 1)
            {
                return true;
            }

            var index = 0;
            var previousHash = chain[0].Hash;
            foreach (var block in chain)
            {
                if (index == 0)
                {
                    index++;
                    continue;
                }
                if (!block.PreviousHash.Equals(previousHash))
                    return false;
                if (!block.Hash.Equals(block.ComputeHash()))
                    return false;
                
                previousHash = block.Hash;
                index++;
            }

            return true;
        }
        private Block<Transaction> GetLastBlock()
        {
            return _chain.Last();
        }
        private Block<Transaction> AddBlock(long proofOfWork)
        {
            var lastBlock = GetLastBlock();
            var newBlock = new Block<Transaction>((lastBlock.Index + 1), DateTime.UtcNow, _transactions.ToArray(), lastBlock.Hash, proofOfWork);
            _chain.Add(newBlock);
            _transactions.Clear();
            return newBlock;

        }
    }
}