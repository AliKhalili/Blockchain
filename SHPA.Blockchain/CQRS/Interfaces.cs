using System;
using System.Threading;
using System.Threading.Tasks;

namespace SHPA.Blockchain.CQRS
{
    public interface IMessage
    {

    }
    public interface ICommand<TResponse>:IMessage where TResponse : ICommandResponse
    {
        string GetType();
        Guid GetId();
        DateTime GetTimespan();
    }

    public interface ICommandResponse
    {
        Guid GetCommandId();
        Guid GetId();
        DateTime GetTimespan();
    }

    public interface ICommandHandler<in TCommand, out TCommandResponse> where TCommand : ICommand<TCommandResponse>
        where TCommandResponse : ICommandResponse
    {
        Task<ICommandResponse> Handle(TCommand command, CancellationToken cancellationToken);
    }


    public interface IMediatorHandler
    {
        Task<ICommandResponse> Send(ICommand<ICommandResponse> command);
        Task Publish(IMessage message);
    }
}