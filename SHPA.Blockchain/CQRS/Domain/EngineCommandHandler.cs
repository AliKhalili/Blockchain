using System.Threading;
using System.Threading.Tasks;
using SHPA.Blockchain.CQRS.Domain.Commands;

namespace SHPA.Blockchain.CQRS.Domain
{
    public class EngineCommandHandler:ICommandHandler<AddBlockCommand,CommandResponse>
    {
        private readonly IEngine _engine;

        public EngineCommandHandler(IEngine engine)
        {
            this._engine = engine;
        }

        public async Task<ICommandResponse> Handle(AddBlockCommand command, CancellationToken cancellationToken)
        {
            var (result, error) = _engine.AddBlock(command.NewBlock);
            return new DefaultCommandResponse(command.GetId());
        }
        public async Task<ICommandResponse> Handle(AddTransactionCommand command, CancellationToken cancellationToken)
        {
            _engine.AddTransaction(command.Transaction);
            return new DefaultCommandResponse(command.GetId());
        }
    }
}