namespace SHPA.Blockchain.Nodes
{
    public interface INodeManager
    {
        string GetName();
        (bool Result, string Message) RegisterNode(Node node);
        Node[] GetRegisterNodes();
    }
}