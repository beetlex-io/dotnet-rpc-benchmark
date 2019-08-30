using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeetleX.XRPC.Clients;
using CodeBenchmark;
using Grpc.Core;
using Helloworld;
namespace RPCBenchmark
{

    public class GRPCHandler
    {
        static GRPCHandler()
        {
            Client = new Channel($"{Setting.SERVER_HOST}:50051", ChannelCredentials.Insecure);

            Greeter = new Greeter.GreeterClient(Client);
        }

        public readonly static Channel Client;

        public readonly static Greeter.GreeterClient Greeter;
    }


    [System.ComponentModel.Category("RPC")]
    public class GRPC_HelloWorld : CodeBenchmark.IExample
    {

        public void Dispose()
        {

        }

        public async Task Execute()
        {
            var result = await GRPCHandler.Greeter.SayHelloAsync(new HelloRequest { Name = "you" });
        }

        public void Initialize(Benchmark benchmark)
        {

        }
    }

}
