using SHPA.Blockchain.Blocks;

namespace SHPA.Blockchain.Nodes
{
    public interface INodeManager
    {
        string GetName();
        (bool Result, string Message) RegisterNode(Node node);
        (bool Result, string[] errors) BroadcastNewBlock(Block<Transaction> input);
        Node[] GetRegisterNodes();
    }
}