using System;
using System.Threading;
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

    public interface IRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
    {
        Task<TResponse> Handle(TRequest request);
    }

    public interface IMediatorHandler
    {
        Task<TResponse> Send<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            where TResponse : IResponse;
        Task Publish(IMessage message);
    }

    internal abstract class RequestHandlerBase
    {
        public abstract Task<object> Handle(object request, IServiceProvider serviceFactory);

        protected static THandler GetHandler<THandler>(IServiceProvider factory)
        {
            THandler handler;

            try
            {
                handler = (THandler)factory.GetService(typeof(THandler));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error constructing handler for request of type {typeof(THandler)}. Register your handlers with the container. See the samples in GitHub for examples.", e);
            }

            if (handler == null)
            {
                throw new InvalidOperationException($"Handler was not found for request of type {typeof(THandler)}. Register your handlers with the container. See the samples in GitHub for examples.");
            }

            return handler;
        }
    }

    internal class RequestHandlerWrapper<TResponse> : RequestHandlerBase
    {
        public Task<TResponse> Handle(IRequest<TResponse> command, IServiceProvider serviceFactory)
        {
            throw new NotImplementedException();
        }
    }
    internal class RequestHandlerWrapperImpl<TRequest, TResponse> : RequestHandlerWrapper<TResponse>
    {
        public Task<TResponse> Handle(TRequest command)
        {
            throw new NotImplementedException();
        }
    }


}