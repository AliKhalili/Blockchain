using System.Threading.Tasks;
using SHPA.Blockchain.CQRS.Bus;
using SHPA.Blockchain.CQRS.Domain.Commands;

namespace SHPA.Blockchain.CQRS.Domain
{
    public class RequestHandler :
        IRequestHandler<AddBlockCommand, DefaultResponse>,
        IRequestHandler<AddTransactionCommand,DefaultResponse>
    {
        private readonly IEngine _engine;

        public RequestHandler(IEngine engine)
        {
            _engine = engine;
        }

#pragma warning disable 1998
        public async Task<DefaultResponse> Handle(AddBlockCommand command)
#pragma warning restore 1998
        {
            var (result, error) = _engine.AddBlock(command.NewBlock);
            return new DefaultResponse();
        }
#pragma warning disable 1998
        public async Task<DefaultResponse> Handle(AddTransactionCommand command)
#pragma warning restore 1998
        {
            _engine.AddTransaction(command.Transaction);
            return new DefaultResponse();
        }



    }
}