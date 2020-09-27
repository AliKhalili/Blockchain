namespace SHPA.Blockchain.CQRS.Bus
{
    public interface IRequest<out TResponse> : IMessage
    {
        //Type GetType();
        //Guid GetId();
        //DateTime GetTimespan();
    }
}