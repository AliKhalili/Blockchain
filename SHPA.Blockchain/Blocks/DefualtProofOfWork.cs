namespace SHPA.Blockchain.Blocks
{
    public class DefaultProofOfWork : IProofOfWork
    {
        public long InitialProof()
        {
            return 0;
        }

        public bool IsValid(long proof)
        {
            if ($"{proof}{(proof / 2)}".SHA256().EndsWith("0"))
                return true;
            return false;
        }

        public long GetNext(long previousProof)
        {
            long proof = previousProof + 1;
            while (!IsValid(proof))
                proof++;
            return proof;
        }
    }
}