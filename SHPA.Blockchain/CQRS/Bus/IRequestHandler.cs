using System.Threading.Tasks;

namespace SHPA.Blockchain.CQRS.Bus
{
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
}