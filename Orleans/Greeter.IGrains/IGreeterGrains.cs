using OGreeter.IGrains.Messages;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OGreeter.IGrains
{
    public interface IGreeterGrains : IGrainWithIntegerKey
    {
        Task<HelloReply> SayHello(HelloRequest request);

        Task<User> Register(string name, string email, string password, string title, string city);

        Task<List<User>> List(int count);

        Task<int> Add(int a, int b);

        Task<World> Get();

        Task<IList<Fortune>> List();
    }
}
