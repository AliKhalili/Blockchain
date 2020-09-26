namespace SHPA.Blockchain.CQRS
{
    public class DefaultResponse:IResponse
    {
        public bool IsSuccess()
        {
            throw new System.NotImplementedException();
        }

        public string[] Errors()
        {
            throw new System.NotImplementedException();
        }
    }
}