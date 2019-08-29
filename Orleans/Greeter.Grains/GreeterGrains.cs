using OGreeter.IGrains;
using OGreeter.IGrains.Messages;
using Orleans.Concurrency;
using System.Threading.Tasks;

namespace OGreeter.Grains
{
    [Reentrant]
    public class GreeterGrains :Orleans.Grain, IGreeterGrains
    {
        public Task<HelloReply> SayHello(HelloRequest request)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }
}
