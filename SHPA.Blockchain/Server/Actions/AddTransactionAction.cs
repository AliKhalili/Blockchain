using System.IO;
using System.Net;
using Newtonsoft.Json;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions
{
    public class AddTransactionAction : IAction
    {
        private readonly IBlockchain _blockchain;

        public AddTransactionAction(IBlockchain blockchain)
        {
            _blockchain = blockchain;
        }
        public IActionResult Execute(HttpListenerRequest request)
        {
            if (!request.HasEntityBody || request.HttpMethod != "POST")
            {
                return new NotFoundActionResult();
            }

            using Stream body = request.InputStream;
            using StreamReader reader = new StreamReader(body, request.ContentEncoding);
            var content = reader.ReadToEnd();
            if (!string.IsNullOrEmpty(content))
            {
                var input = JsonConvert.DeserializeObject<Transaction>(content);
                _blockchain.AddTransaction(input.Sender, input.Receiver, input.Amount);
                return new JsonActionResult<Transaction>(input);
            }

            return new NotFoundActionResult();
        }
    }
}