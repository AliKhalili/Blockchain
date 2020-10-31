using System.Threading.Tasks;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Nodes;

namespace SHPA.Blockchain.CQRS.Domain
{
    public class QueryService: IQueryService
    {
        private readonly IEngine _engine;
        public QueryService(IEngine engine)
        {
            _engine = engine;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<Block<Transaction>[]> GetChainAsync() => _engine.GetChain();
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<Node[]> GetRegisterNodesAsync() => _engine.GetRegisterNodes();
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<bool> IsValidChainAsync() => _engine.IsValidChain();
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}