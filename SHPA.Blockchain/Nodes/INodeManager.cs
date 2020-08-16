namespace SHPA.Blockchain.Nodes
{
    public interface INodeManager
    {
        (bool Result, string Message) RegisterNode(Node node);
        Node[] GetRegisterNodes();
    }
}