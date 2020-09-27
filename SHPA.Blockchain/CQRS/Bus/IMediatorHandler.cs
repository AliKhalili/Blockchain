using System.Threading.Tasks;

namespace SHPA.Blockchain.CQRS.Bus
{
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