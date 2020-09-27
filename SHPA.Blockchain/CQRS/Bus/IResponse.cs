namespace SHPA.Blockchain.CQRS.Bus
{
    public interface IResponse
    {
        //Guid GetCommandId();
        //Guid GetId();
        //DateTime GetTimespan();
        bool IsSuccess();
        string[] Errors();
    }
}