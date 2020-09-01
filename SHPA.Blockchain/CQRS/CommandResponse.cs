using System;

namespace SHPA.Blockchain.CQRS
{
    public abstract class CommandResponse : ICommandResponse
    {
        private readonly Guid _id;
        private readonly Guid _commandId;
        private readonly DateTime _time;

        public CommandResponse(Guid commandId)
        {
            _id = Guid.NewGuid();
            _commandId = commandId;
            _time = DateTime.UtcNow;
        }

        public Guid GetCommandId()
        {
            return _commandId;
        }

        public Guid GetId()
        {
            return _id;
        }

        public DateTime GetTimespan()
        {
            return _time;
        }
    }
}