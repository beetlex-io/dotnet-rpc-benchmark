using BeetleX.XRPC.Clients;
using CodeBenchmark;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XRPCModule;

namespace RPCBenchmark
{
    public class XRPCHandler
    {

        public XRPCHandler()
        {
            Client = new XRPCClient("192.168.2.19", 50052, 3);
            Client.Connect();
            Greeter = Client.Create<XRPCModule.IGreeter>();
        }

        public XRPCClient Client { get; private set; }

        public XRPCModule.IGreeter Greeter { get; private set; }

        private static XRPCHandler mSingle;

        public static XRPCHandler Single
        {
            get
            {
                if (mSingle == null)
                    mSingle = new XRPCHandler();
                return mSingle;
            }
        }
    }
    [System.ComponentModel.Category("RPC")]
    public class XRPCHelloWorld : CodeBenchmark.IExample
    {
        public void Dispose()
        {

        }

        public async Task Execute()
        {
            var result = await _greeter.SayHello(new HelloRequest { Name = "you" });
        }

        public void Initialize(Benchmark benchmark)
        {
            _greeter = XRPCHandler.Single.Greeter;
        }

        private XRPCModule.IGreeter _greeter;
    }
}
