namespace SHPA.Blockchain.Blocks
{
    public class Transaction
    {

        public Transaction(string sender, string receiver, double amount)
        {
            Sender = sender;
            Receiver = receiver;
            Amount = amount;
        }

        public string Sender { get; }
        public string Receiver { get; }
        public double Amount { get; }
    }
}