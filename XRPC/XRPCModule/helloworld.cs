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
