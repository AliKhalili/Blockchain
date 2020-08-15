namespace SHPA.Blockchain.Blocks
{
    public interface IProofOfWork
    {
        long InitialProof();
        bool IsValid(long proof);
        long GetNext(long previousProof);
    }
}