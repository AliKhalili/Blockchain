﻿using System.Net;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Actions
{
    public class ChainAction : IAction
    {
        private readonly IBlockchain _blockchain;

        public ChainAction(IBlockchain blockchain)
        {
            _blockchain = blockchain;
        }
        public IActionResult Execute(HttpListenerRequest request)
        {
            if (request.HttpMethod != "GET")
            {
                return new NotFoundActionResult();
            }
            return new ActionResult<Block<Transaction>[]>().AddResult(_blockchain.Chain());
        }
    }
}