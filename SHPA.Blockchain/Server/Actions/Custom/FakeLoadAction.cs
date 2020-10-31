using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SHPA.Blockchain.Server.ActionResult;

namespace SHPA.Blockchain.Server.Actions.Custom
{
    public class FakeLoadAction : ActionBase
    {
        public override async Task<IActionResult> Execute(HttpListenerRequest request)
        {
            var startTime = DateTimeOffset.Now;
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Thread.Sleep(500);
            double powerOfTwo = 0;
            //powerOfTwo = Math.Pow(2, 1000);
            //for (int i = 0; i < 10_000_000; i++)
            //{
            //    powerOfTwo += Math.Pow(2, 100);
            //}
            var elapsedTime = stopWatch.ElapsedMilliseconds;
            return new ActionResult<object>().AddResult(new
            {
                startTime,
                elapsedTime,
                powerOfTwo
            });
        }
    }
}