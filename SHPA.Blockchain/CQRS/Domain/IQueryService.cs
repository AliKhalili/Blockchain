using System.Threading.Tasks;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Nodes;

namespace SHPA.Blockchain.CQRS.Domain
{
    public interface IQueryService
    {
        Task<Block<Transaction>[]> GetChainAsync();
        Task<Node[]> GetRegisterNodesAsync();
        Task<bool> IsValidChainAsync();
    }
}