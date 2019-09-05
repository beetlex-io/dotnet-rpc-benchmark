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


    [System.ComponentModel.Category("Hello")]
    public class XRPC_Hello : CodeBenchmark.IExample
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
    [System.ComponentModel.Category("Register")]
    public class XRPC_Register : CodeBenchmark.IExample
    {
        public void Dispose()
        {

        }
        public async Task Execute()
        {
            var result = await XRPCHandler.Greeter.Register("henryfan", "henryfan@msn.com", "12345678", "cxo", "guangzhou");
        }
        public void Initialize(Benchmark benchmark)
        {

        }

    }

    [System.ComponentModel.Category("List")]
    public class XRPC_List : CodeBenchmark.IExample
    {
        public void Dispose()
        {

        }
        public async Task Execute()
        {
            var result = await XRPCHandler.Greeter.List(10);
            if (result.Count < 10)
                throw new Exception("list error");
        }
        public void Initialize(Benchmark benchmark)
        {

        }

    }
}
