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

            for (int i = 0; i < 3; i++)
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


    [System.ComponentModel.Category("Hello")]
    public class GRPC_Hello : CodeBenchmark.IExample
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

    [System.ComponentModel.Category("Register")]
    public class GRPC_Register : CodeBenchmark.IExample
    {

        public void Dispose()
        {

        }

        public async Task Execute()
        {
            var request = new RegisterRequest
            {
                Name = "henryfan",
                City = "guangzhou",
                Email = "henryfan@msn.com",
                Title = "cxo",
                Password = "12345678"
            };
            var result = await Greeter.RegisterAsync(request);
        }

        public void Initialize(Benchmark benchmark)
        {
            Greeter = GRPCHandler.GetClient();
        }

        private Greeter.GreeterClient Greeter;
    }

    [System.ComponentModel.Category("List")]
    public class GRPC_List : CodeBenchmark.IExample
    {

        public void Dispose()
        {

        }

        public async Task Execute()
        {
            var result = await Greeter.ListAsync(new SearchRequest { Count = 10 });
            if (result.Items.Count < 10)
                throw new Exception("list error");
        }

        public void Initialize(Benchmark benchmark)
        {
            Greeter = GRPCHandler.GetClient();
        }

        private Greeter.GreeterClient Greeter;
    }

}
