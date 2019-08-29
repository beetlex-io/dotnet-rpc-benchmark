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

        public GRPCHandler()
        {
            Client = new Channel("192.168.2.19:50051", ChannelCredentials.Insecure);

            Greeter = new Greeter.GreeterClient(Client);
        }

        public Channel Client { get; private set; }

        public Greeter.GreeterClient Greeter { get; private set; }

        private static GRPCHandler mSingle;

        public static GRPCHandler Single
        {
            get
            {
                if (mSingle == null)
                    mSingle = new GRPCHandler();
                return mSingle;
            }
        }
    }
    [System.ComponentModel.Category("RPC")]
    public class GRPCHelloWorld : CodeBenchmark.IExample
    {
        public void Dispose()
        {

        }

        public async Task Execute()
        {
            var result = await _greeter.SayHelloAsync(new HelloRequest { Name = "you" });
        }

        public void Initialize(Benchmark benchmark)
        {
            _greeter = GRPCHandler.Single.Greeter;
        }

        private Greeter.GreeterClient _greeter;
    }

}
