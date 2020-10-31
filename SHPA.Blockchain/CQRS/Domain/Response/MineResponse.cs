using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.CQRS.Bus;

namespace SHPA.Blockchain.CQRS.Domain.Response
{
    public class MineResponse : DefaultResponse
    {
        public Block<Transaction> NewBlock { get; set; }
    }
}