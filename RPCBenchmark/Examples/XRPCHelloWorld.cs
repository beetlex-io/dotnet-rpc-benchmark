using BeetleX.XRPC.Clients;
using CodeBenchmark;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XRPCModule;

namespace RPCBenchmark
{

    class XRPCHandler
    {

        static XRPCHandler()
        {

            Client = new XRPCClient(Setting.SERVER_HOST, 50052, 3);
            Client.Connect();
            Greeter = Client.Create<XRPCModule.IGreeter>();
        }

        public readonly static XRPCClient Client;

        public readonly static XRPCModule.IGreeter Greeter;
    }


    [System.ComponentModel.Category("RPC")]
    public class XRPC_HelloWorld : CodeBenchmark.IExample
    {


        public void Dispose()
        {

        }

        public async Task Execute()
        {
            var result = await XRPCHandler.Greeter.SayHello(new HelloRequest { Name = "you" });
        }

        public void Initialize(Benchmark benchmark)
        {
            
        }

       
    }
}
