using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XRPCModule
{

    public interface IGreeter
    {
        Task<HelloReply> SayHello(HelloRequest request);

        Task<User> Register(string name, string email, string password, string title, string city);

        Task<List<User>> List(int count);
        

    }

    [MessagePackObject]
    public class User
    {
        [Key(0)]
        public string Name { get; set; }

        [Key(1)]
        public string Email { get; set; }

        [Key(2)]
        public string Password { get; set; }

        [Key(3)]
        public string Title { get; set; }

        [Key(4)]
        public string City { get; set; }

        [Key(5)]
        public DateTime CreateTime { get; set; }

        [Key(6)]
        public string ID { get; set; }
    }



    [MessagePackObject]
    public class HelloRequest
    {
        [Key(0)]
        public string Name { get; set; }
    }

    [MessagePackObject]
    public class HelloReply
    {
        [Key(0)]
        public string Message { get; set; }
    }

}
