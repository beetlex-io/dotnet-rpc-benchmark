using OGreeter.IGrains.Messages;
using Orleans;
using System.Threading.Tasks;

namespace OGreeter.IGrains
{
    public interface IGreeterGrains : IGrainWithIntegerKey
    {
        Task<HelloReply> SayHello(HelloRequest request);
    }
}
