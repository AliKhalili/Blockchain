using System;
using System.Threading;
using System.Threading.Tasks;

namespace SHPA.Blockchain.CQRS
{
    public interface ICommand<TResponse> where TResponse : ICommandResponse
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
        Task<ICommandResponse> Handle(ICommand<ICommandResponse> command, CancellationToken cancellationToken);
    }
}