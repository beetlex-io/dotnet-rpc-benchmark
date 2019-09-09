using Netx;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetxTestServer
{
    [Build]
    public interface ITestServer
    {
        [TAG(1000)]
        Task<HelloReply> SayHello(HelloRequest msg);

        [TAG(1001)]
        Task<User> Register(string name, string email, string password, string title, string city);

        [TAG(1002)]
        Task<List<User>> List(int count);

        [TAG(1003)]
        Task<int> Add(int a, int b);

        [TAG(1004)]
        Task<World> Get();

        [TAG(1005)]
        Task<IList<Fortune>> List();
    }


    public class User
    {

        public string ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public class HelloRequest
    {
        public string Name { get; set; }
    }

    public class HelloReply
    {
        public string Message { get; set; }
    }

    public class Fortune
    {

        public int Id { get; set; }

        public string Message { get; set; }

    }
    public class World
    {

        public int Id { get; set; }

        public int RandomNumber { get; set; }
    }
}
