using System;
using System.Threading;
using System.Threading.Tasks;

namespace SHPA.Blockchain.CQRS
{
    public interface IMessage
    {

    }
    public interface ICommand : IMessage
    {
        string GetType();
        Guid GetId();
        DateTime GetTimespan();
    }

    public interface IResponse
    {
        Guid GetCommandId();
        Guid GetId();
        DateTime GetTimespan();
        bool IsSuccess();
        string[] Errors();
    }

    public interface ICommandHandler<in TCommand, TResponse>
        where TCommand : ICommand
        where TResponse : IResponse
    {
        Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
    }


    public interface IMediatorHandler
    {
        Task<IResponse> Send(ICommand command);
        Task Publish(IMessage message);
    }
}