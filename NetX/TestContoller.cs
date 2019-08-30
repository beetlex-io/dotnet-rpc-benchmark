using Netx.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetxTestServer
{
    public class TestContoller : AsyncController, ITestServer
    {
        public Task<HelloReply> SayHello(HelloRequest msg)
        => Task.FromResult(new HelloReply{ Message= "Hello" + msg.Name });

        public Task<string> SayHello(string msg)
         => Task.FromResult("Hello" + msg);
    }
}
