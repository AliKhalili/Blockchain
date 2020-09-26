using System.Threading.Tasks;
using SHPA.Blockchain.CQRS.Domain.Commands;

namespace SHPA.Blockchain.CQRS.Domain
{
    public class RequestHandler :
        IRequestHandler<AddBlockCommand, DefaultResponse>
        ,
        IRequestHandler<AddTransactionCommand,DefaultResponse>
    {
        private readonly IEngine _engine;

        public RequestHandler(IEngine engine)
        {
            this._engine = engine;
        }

        public async Task<DefaultResponse> Handle(AddBlockCommand command)
        {
            var (result, error) = _engine.AddBlock(command.NewBlock);
            return new DefaultResponse();
        }
        public async Task<DefaultResponse> Handle(AddTransactionCommand command)
        {
            _engine.AddTransaction(command.Transaction);
            return new DefaultResponse();
        }



    }
}