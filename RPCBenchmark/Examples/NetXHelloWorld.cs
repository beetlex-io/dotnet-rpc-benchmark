using CodeBenchmark;
using Netx;
using Netx.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RPCBenchmark.Examples
{
    public class NetxHandler
    {
        static NetxHandler()
        {
            for(int i=0;i<3;i++)
            {
               var client = new NetxSClientBuilder()
               .ConfigConnection(p =>
               {
                   p.Host = Setting.SERVER_HOST;
                   p.Port = 50054;
                   p.VerifyKey = "11111";
                   p.ServiceName = "hellowordservice";
                   p.RequestTimeOut = 0;
               })
               .Build();
                mClients.Add(client);
            }
        }

        private static long mIndex;

        public static ITestServer GetClient()
        {
            long index = System.Threading.Interlocked.Increment(ref mIndex);
            return mClients[(int)(index % mClients.Count)].Get<ITestServer>();
        }


        private static List<NetxSClient> mClients = new List<NetxSClient>();

       
    }
    [System.ComponentModel.Category("RPC")]
    public class Netx_HelloWorld : CodeBenchmark.IExample
    {
        public void Dispose()
        {

        }

        public async Task Execute()
        {
            await _greeter.SayHello(new NetxHelloRequest { Name = "you" });
        }

        public void Initialize(Benchmark benchmark)
        {
            _greeter = NetxHandler.GetClient();
        }

        private ITestServer _greeter;
    }


    [Build]
    public interface ITestServer
    {
        [TAG(1000)]
        Task<NetxHelloReply> SayHello(NetxHelloRequest msg);
    }

    public class NetxHelloRequest
    {
        public string Name { get; set; }
    }

    public class NetxHelloReply
    {
        public string Message { get; set; }
    }
}
