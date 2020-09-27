using System;
using System.Threading.Tasks;

namespace SHPA.Blockchain.CQRS.Bus
{
    internal class RequestHandlerWrapper<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
    {
        public Task<TResponse> Handle(TRequest request, IServiceProvider factory)
        {
            var handler = (IRequestHandler<TRequest, TResponse>)factory.GetService(typeof(IRequestHandler<TRequest, TResponse>));
            if (handler == null)
            {
                throw new InvalidOperationException($@"Handler was not found for request of type {request.GetType()}. Register your handlers with the container.");
            }
            return handler.Handle(request);
        }
    }
}