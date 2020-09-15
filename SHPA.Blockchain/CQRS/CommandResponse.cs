using System;
using System.Collections.Generic;
using System.Linq;

namespace SHPA.Blockchain.CQRS
{
    public class DefaultResponse : Response
    {
        public DefaultResponse(Guid commandId) : base(commandId)
        {
        }
    }

    public abstract class Response : IResponse
    {
        private readonly Guid _id;
        private readonly Guid _commandId;
        private readonly DateTime _time;
        private readonly Dictionary<string, string> _validation;
        public Response(Guid commandId)
        {
            _id = Guid.NewGuid();
            _commandId = commandId;
            _time = DateTime.UtcNow;
            _validation = new Dictionary<string, string>();
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

        public bool IsSuccess()
        {
            return !_validation.Any();
        }

        public string[] Errors()
        {
            return _validation.Values.ToArray();
        }
    }
}