using System;
using System.Linq;

namespace SHPA.Blockchain.Blocks
{
    public class DefaultProofOfWork : IProofOfWork
    {
        private readonly string _difficultyHash;
        public DefaultProofOfWork(int difficulty = 3)
        {
            _difficultyHash = "0".Multiply(difficulty);
        }

        public long InitialProof()
        {
            return 0;
        }

        public bool IsValid(long proof)
        {
            if ($"{proof}".SHA256().EndsWith(_difficultyHash) && $"{proof / 2}".SHA256().EndsWith(_difficultyHash))
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