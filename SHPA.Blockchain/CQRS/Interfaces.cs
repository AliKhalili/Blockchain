using System.Threading.Tasks;

namespace SHPA.Blockchain.CQRS
{
    public interface IMessage
    {

    }

    public interface IRequest<out TResponse> : IMessage
    {
        //Type GetType();
        //Guid GetId();
        //DateTime GetTimespan();
    }

    public interface IResponse
    {
        //Guid GetCommandId();
        //Guid GetId();
        //DateTime GetTimespan();
        bool IsSuccess();
        string[] Errors();
    }

    /// <summary>
    /// copy from MediatR.IRequestHandler.
    /// Defines a handler for a request
    /// </summary>
    /// <typeparam name="TRequest">The type of request being handled</typeparam>
    /// <typeparam name="TResponse">The type of response from the handler</typeparam>
    public interface IRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
    {
        Task<TResponse> Handle(TRequest request);
    }

    /// <summary>
    /// copy from MediatR.IMediator
    /// </summary>
    public interface IMediatorHandler
    {
        Task<TResponse> Send<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            where TResponse : IResponse;
        Task Publish(IMessage message);
    }
}