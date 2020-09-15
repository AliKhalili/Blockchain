using System.Threading.Tasks;
using SHPA.Blockchain.CQRS.Domain.Commands;

namespace SHPA.Blockchain.CQRS.Domain
{
    public class CommandHandler : ICommandHandler<AddBlockCommand, DefaultResponse>
    {
        private readonly IEngine _engine;

        public CommandHandler(IEngine engine)
        {
            this._engine = engine;
        }

        public async Task<DefaultResponse> Handle(AddBlockCommand command)
        {
            var (result, error) = _engine.AddBlock(command.NewBlock);
            return new DefaultResponse(command.GetId());
        }
        public async Task<IResponse> Handle(AddTransactionCommand command)
        {
            _engine.AddTransaction(command.Transaction);
            return new DefaultResponse(command.GetId());
        }
    }
}