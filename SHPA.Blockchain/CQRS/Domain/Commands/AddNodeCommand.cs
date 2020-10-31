using SHPA.Blockchain.CQRS.Bus;

namespace SHPA.Blockchain.CQRS.Domain.Commands
{
    public class AddNodeCommand: IRequest<DefaultResponse>
    {
        public AddNodeCommand(bool enableRegisterBack)
        {
            EnableRegisterBack = enableRegisterBack;
        }

        public readonly bool EnableRegisterBack;
    }
}