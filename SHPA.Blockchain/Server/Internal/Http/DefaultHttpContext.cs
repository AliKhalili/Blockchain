namespace SHPA.Blockchain.Server.Http
{
    /// <summary>
    /// Represents an implementation of the HTTP Context class. 
    /// </summary>
    public sealed class DefaultHttpContext : HttpContext
    {
        public override IFeatureCollection Features { get; }
        public override void Abort()
        {
            throw new System.NotImplementedException();
        }
    }
}