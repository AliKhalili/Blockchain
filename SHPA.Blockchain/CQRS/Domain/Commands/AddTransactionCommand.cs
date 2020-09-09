using SHPA.Blockchain.Blocks;

namespace SHPA.Blockchain.CQRS.Domain.Commands
{
    
    public class AddTransactionCommand: Command
    {
        public AddTransactionCommand(Transaction transaction)
        {
            Transaction = transaction;
        }

        public Transaction Transaction { get; }

    }
}
