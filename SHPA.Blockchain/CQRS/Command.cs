using System;

namespace SHPA.Blockchain.CQRS
{
    public abstract class Command : ICommand<CommandResponse>
    {
        private readonly string _type;
        private readonly Guid _id;
        private readonly DateTime _time;

        public Command()
        {
            _type = base.GetType().ToString();
            _id=Guid.NewGuid();
            _time=DateTime.UtcNow;
        }
        public new string GetType()
        {
            return _type;
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