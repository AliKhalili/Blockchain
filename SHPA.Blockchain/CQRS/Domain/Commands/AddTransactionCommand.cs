using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.CQRS.Bus;

namespace SHPA.Blockchain.CQRS.Domain.Commands
{
    
    public class AddTransactionCommand: IRequest<DefaultResponse>
    {
        public AddTransactionCommand(Transaction transaction)
        {
            Transaction = transaction;
        }

        public Transaction Transaction { get; }

    }
}
