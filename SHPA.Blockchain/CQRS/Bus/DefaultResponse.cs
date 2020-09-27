namespace SHPA.Blockchain.CQRS.Bus
{
    public class DefaultResponse:IResponse
    {
        public bool IsSuccess()
        {
            return true;
        }

        public string[] Errors()
        {
            throw new System.NotImplementedException();
        }
    }
}