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
                  
            for(int i=0;i<3;i++)
            {
                var client = new Channel($"{Setting.SERVER_HOST}:50051", ChannelCredentials.Insecure);
                mClients.Add(new Greeter.GreeterClient(client));
            }
        }

        static List<Greeter.GreeterClient> mClients = new List<Greeter.GreeterClient>();

        static long mIndex;

        public static Greeter.GreeterClient GetClient()
        {
            var index = System.Threading.Interlocked.Increment(ref mIndex);
            return mClients[(int)(mIndex % mClients.Count)];
        }

    }


    [System.ComponentModel.Category("RPC")]
    public class GRPC_HelloWorld : CodeBenchmark.IExample
    {

        public void Dispose()
        {

        }

        public async Task Execute()
        {
            var result = await Greeter.SayHelloAsync(new HelloRequest { Name = "you" });
        }

        public void Initialize(Benchmark benchmark)
        {
            Greeter = GRPCHandler.GetClient();
        }

        private Greeter.GreeterClient Greeter;
    }

}
