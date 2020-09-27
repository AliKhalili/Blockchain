using System;
using System.Threading.Tasks;

namespace SHPA.Blockchain.CQRS
{
    internal class RequestHandlerWrapper<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
    {
        public Task<TResponse> Handle(TRequest request, IServiceProvider factory)
        {
            var handler = (IRequestHandler<TRequest, TResponse>)factory.GetService(typeof(IRequestHandler<TRequest, TResponse>));
            return handler.Handle(request);
        }
    }
}