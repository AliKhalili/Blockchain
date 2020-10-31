namespace SHPA.Blockchain.Configuration
{
    /// <summary>
    /// A class for loading node configuration from json file
    /// </summary>
    public class NodeConfiguration
    {
        /// <summary>
        /// An optional name for node
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// An optional port for node to listen on it
        /// </summary>
        public uint Port { get; set; }

        /// <summary>
        /// IP address of host that node provided
        /// </summary>
        public string Host { get; set; }

        public int MaxNodeCapacity { get; set; }
        public int MaxThread { get; set; }

        public string GetFullAddress()
        {
            return $"http://{Host}:{Port}";
        }
    }
}