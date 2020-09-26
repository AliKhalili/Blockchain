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
        Task<TResponse> Handle(TRequest command);
    }

    internal class RequestHandlerWrapper<TResponse> : IRequestHandler<IRequest<TResponse>, TResponse>
        where TResponse : IResponse
    {

        public Task<TResponse> Handle(IRequest<TResponse> command)
        {
            throw new NotImplementedException();
        }
    }
    internal class RequestHandlerWrapperImpl<TRequest, TResponse> : RequestHandlerWrapper<TResponse> 
        where TResponse : IResponse 
        where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest command)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMediatorHandler
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request) where TResponse : IResponse;
        Task Publish(IMessage message);
    }


}