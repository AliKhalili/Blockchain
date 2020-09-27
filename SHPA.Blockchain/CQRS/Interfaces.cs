using System;
using System.Runtime.ExceptionServices;
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
        public abstract Task<object?> Handle(object request, IServiceProvider serviceFactory);
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

    internal abstract class RequestHandlerWrapper<TResponse> : RequestHandlerBase
    {
        //public abstract Task<TResponse> Handle(IRequest<TResponse> request, IServiceProvider serviceFactory);

    }
    internal class RequestHandlerWrapperImpl<TRequest, TResponse> : RequestHandlerWrapper<TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
    {
        //public override Task<TResponse> Handle(IRequest<TResponse> request, IServiceProvider serviceFactory)
        //{
        //    Task<TResponse> Handler() => GetHandler<IRequestHandler<TRequest, TResponse>>(serviceFactory).Handle((TRequest)request);
        //    return Handle((IRequest<TResponse>)request, serviceFactory);
        //}

        public override Task<object?> Handle(object request, IServiceProvider serviceFactory)
        {
            return Handle((IRequest<TResponse>)request, serviceFactory).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    ExceptionDispatchInfo.Capture(t.Exception.InnerException).Throw();
                }

                return (object?)t.Result;
            }, new CancellationToken(false));
        }
    }
}