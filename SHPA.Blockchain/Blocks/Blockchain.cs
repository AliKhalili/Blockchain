﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SHPA.Blockchain.Actions.Models;
using SHPA.Blockchain.Configuration;
using SHPA.Blockchain.Nodes;

namespace SHPA.Blockchain.Blocks
{
    public class Blockchain : IBlockchain
    {
        private readonly IProofOfWork _proofOfWork;
        private readonly IList<Block<Transaction>> _chain;
        private IList<Transaction> _transactions;
        private readonly NodeConfiguration _option;
        private readonly INodeManager _nodeManager;
        public Blockchain(IProofOfWork proofOfWork, IOptions<NodeConfiguration> option, INodeManager nodeManager)
        {
            _proofOfWork = proofOfWork;
            _nodeManager = nodeManager;
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
            var newBlock = AddBlock(proof);
            var (result, errors) = _nodeManager.BroadcastNewBlock(newBlock);
            return newBlock;
        }

        public Block<Transaction>[] Chain()
        {
            return _chain.ToArray();
        }

        public bool IsValidChain()
        {
            return IsValidChain(_chain);
        }

        public (bool Result, string[] Errors) AddBlock(Block<Transaction> input)
        {
            var lastBlock = GetLastBlock();
            if (lastBlock.Index > input.Index)
                return (false, new[] { "new block index is lower than last block of current node" });
            if (!lastBlock.Hash.Equals(input.PreviousHash))
                return (false, new[] { "new previous hash is not equal to last block previous hash of current node" });
            _chain.Add(input);
            return (true, null);
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