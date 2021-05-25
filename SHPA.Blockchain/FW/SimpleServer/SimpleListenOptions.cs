using System.Net;

namespace SHPA.Blockchain.FW.SimpleServer
{
    public class SimpleListenOptions
    {
        private IPEndPoint _ipEndPoint;

        public SimpleListenOptions(IPEndPoint iPEndPoint)
        {
            _ipEndPoint = iPEndPoint;
        }

        public string ToUrl()
        {
            return $"http://{_ipEndPoint.Address}:{_ipEndPoint.Port}/";
        }
    }
}