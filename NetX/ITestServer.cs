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
    }

    public class HelloRequest
    {
        public string Name { get; set; }
    }

    public class HelloReply
    {
        public string Message { get; set; }
    }
}
